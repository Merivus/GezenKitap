using GezenKitap.DAL;
using GezenKitap.DATA.Entities;
using GezenKitap.DATA.EnumsInterface;
using GezenKitap.UI.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GezenKitap.BLL.Concrete;

namespace GezenKitap.UI.Controllers
{
    public class ShoppingController : Controller
    {
        ApplicationDbContext db;
        OrderConcrete orderconcrete;
        BookConcrete bookconcrete;
        ApplicationUserConcrete userconcrete;

        public ShoppingController()
        {
            db = new ApplicationDbContext();
            orderconcrete = new OrderConcrete();
            bookconcrete = new BookConcrete();
            userconcrete = new ApplicationUserConcrete();
        }
        public ActionResult GoToPayment()
        {
            List<Order> cart = db.Orders.Where(z => z.State != OrderState.Tamamlandi && z.ApplicationUser_Id == User.Identity.GetUserId() ).ToList();

            

            ViewBag.Orders = cart;
            return View(db.Users.Find(User.Identity.GetUserId() ));
        }

        

        public ActionResult FinishShopping()
        {
            return View();
        }


        

        public ActionResult Cart(int id)
        {
            var userid = User.Identity.GetUserId();

            IEnumerable<Order> orders;
            if (id == 1)
                orders = db.Orders.Where(x => x.ApplicationUser_Id == userid
                && x.State != OrderState.Tamamlandi ).ToList();
            else 
                orders = db.Orders.Where(x => x.Book.UserID == userid
                            && x.State != OrderState.Tamamlandi).ToList();
            

            return View(orders);
        }

        public ActionResult RemoveFromCart(int id)
        {
            var order = orderconcrete.OrderRepository.Get(x => x.OrderID == id);
            order.State = OrderState.Iptal;
            orderconcrete.OrderRepository.Update(order);
            orderconcrete.OrderUnitOfWork.SaveChanges();

            return Redirect(Request.UrlReferrer.ToString());
        }


        

    }
}