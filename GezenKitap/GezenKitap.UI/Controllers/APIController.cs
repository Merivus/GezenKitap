using GezenKitap.DATA.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GezenKitap.DAL;
using GezenKitap.BLL.Repository;
using GezenKitap.BLL.Concrete;
using Microsoft.AspNet.Identity;
using GezenKitap.DATA.EnumsInterface;
using GezenKitap.UI.Class;

namespace GezenKitap.UI.Controllers
{

    public class APIController : ApiController
    {
        Mail mail;
        ApplicationDbContext db;
        OrderConcrete orderconcrete;
        BookConcrete bookconcrete;
        ApplicationUserConcrete userconcrete;

        public APIController()
        {
            db = new ApplicationDbContext();
            orderconcrete = new OrderConcrete();
            bookconcrete = new BookConcrete();
            userconcrete = new ApplicationUserConcrete();
            mail = new Mail();
        }

        [HttpPost]
        public int AddAuthor(Author item)
        {
            EFRepository<Author> rep = new EFRepository<Author>(db);

            rep.Add(item);

            //db.Authors.Add(item);
            db.SaveChanges();

            return item.AuthorID;
        }

        [HttpPost]
        public bool DeleteBook(int id)
        {
            EFRepository<Book> rep = new EFRepository<Book>(db);
            rep.Delete(id);
            return db.SaveChanges() == 1;
        }


        [Authorize]
        [HttpPost]
        public string AddToCart(int id)
        {
            try
            {
                ControlCart(id);

                return "OK";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            //return RedirectToAction("Cart", new { id = 1 });
        }

        [Authorize]
        [HttpPost]
        public string SubmitFromCart(int id)
        {
            var order = orderconcrete.OrderRepository.GetById(id);
            order.State = OrderState.Tamamlandi;
            orderconcrete.OrderRepository.Update(order);

            var userid = User.Identity.GetUserId();
            var user = userconcrete.ApplicationUserRepository.Get(x => x.Id == userid);
            user.Credit -= order.TotalAmount;
            if (user.Credit < 0)
            {
                //ModelState.AddModelError("Error", "Kitabı almak için yeterli krediniz bulmamaktadır. Krediniz:" + (user.Credit + order.TotalAmount));
                //return View("Cart", db.Orders.Where(x => x.ApplicationUser_Id == userid).ToList());
                return "Kitabı almak için yeterli krediniz bulmamaktadır. Krediniz:" + (user.Credit + order.TotalAmount);
            }

            orderconcrete.OrderUnitOfWork.SaveChanges();
            userconcrete.ApplicationUserRepository.Update(user);
            userconcrete.ApplicationUserUnitOfWork.SaveChanges();
            //ViewBag.Success = true;

            //return Redirect(Request.UrlReferrer.ToString());

            var kitapsahibi = userconcrete.ApplicationUserRepository.Get(x => x.Id == order.Book.UserID);

            mail.SendMail(
                kitapsahibi.Email, "Kitap Talebi Tamamlandı",
                order.OrderID + " nolu talep için istek yapan kişi kitabı teslim almıştır.");
            return "OK";
        }

        [Authorize]
        [HttpPost]
        [Route("api/API/SubmitFromCartTo/{orderid}/{trackingnumber}")]
        public string SubmitFromCartTo(int orderid, string trackingnumber)
        {
            var order = orderconcrete.OrderRepository.GetById(orderid);

            order.State = OrderState.Kargolandi;
            order.TrackingAddedDate = DateTime.Now;
            order.TrackingNumber = trackingnumber;
            
            orderconcrete.OrderRepository.Update(order);

            var kitapsahibi = order.Book.UserID;
            var user = userconcrete.ApplicationUserRepository.Get(x => x.Id == kitapsahibi);
            user.Credit += order.TotalAmount;
            orderconcrete.OrderUnitOfWork.SaveChanges();
            userconcrete.ApplicationUserRepository.Update(user);
            userconcrete.ApplicationUserUnitOfWork.SaveChanges();

            var alici = userconcrete.ApplicationUserRepository.Get(x => x.Id == order.ApplicationUser_Id);
            var alicimail = alici.Email;

            mail.SendMail(
                alicimail, "Kitap isteğiniz Onaylandı", 
                order.OrderID +  " nolu isteğiniz onaylandı. Kargo Takip no:" + trackingnumber);

            return "OK";
        }

        public void ControlCart(int id)
        {
            var userid = User.Identity.GetUserId();
            var alici = userconcrete.ApplicationUserRepository.Get(x => x.Id == userid);

            Book book = bookconcrete.BookRepository.GetById(id);
            if (!book.IsActive || book.IsDelete || book.UserID == userid)
                throw new Exception("Bu kitap talep etmek için uygun değil.");

            if (db.Orders.Any(x => x.BookID == id && x.ApplicationUser_Id == userid
                 && x.State >= OrderState.Istek))
                throw new Exception("Bu kitap için daha önce istek yaptınız.");


            Order od = db.Orders.FirstOrDefault(x => x.BookID == id
                && x.State >= OrderState.Kargolandi);

            if (od == null) // yeni kayıt
            {
                od = new Order();
                od.BookID = id;
                od.ApplicationUser_Id = userid;
                od.State = OrderState.Istek;
                od.TotalAmount = book.CreditAmount;
                //od.Discount = db.Products.Find(id).Discount;
                od.OrderDate = DateTime.Now;

                db.Orders.Add(od);
                db.SaveChanges();
                
                mail.SendMail(
                    alici.Email, "Kitap Talebi Yapıldı",
                    "Size ait '" + book.BookName + "' adlı kitap için talep yapıldı.");
            }
            else
                throw new Exception("Bu kitap daha önce paylaşıldı!");


        }

    }
}

