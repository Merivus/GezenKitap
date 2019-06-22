using GezenKitap.DAL;
using GezenKitap.DATA.Entities;
using GezenKitap.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GezenKitap.UI.Controllers
{
    public class ShoppingController : Controller
    {
        ApplicationDbContext db;
        public ShoppingController()
        {
            db = new ApplicationDbContext();
        }
        // Burası dolacak.
        public ActionResult GoToPayment()
        {
            List<Order> cart = db.Orders.Where(z => z.IsCompleted == false && z.ApplicationUser_Id == TemporaryUserData.UserID).ToList();

            // Action yazılacak.
            //foreach (var item in cart)
            //{
            //    if (item.Book.UnitsInStock < 1)
            //        return RedirectToAction("Cart", "Shopping");
            //}

            ViewBag.Orders = cart;
            //ViewBag.PaymentTypes = db.PaymentTypes.ToList();
            return View(db.Users.Find(TemporaryUserData.UserID));
        }

        //[HttpPost]
        //public ActionResult CompleteShopping(FormCollection frm)
        //{
        //    Payment payment = new Payment()
        //    {
        //        Type = int.Parse(frm["paymentType"]),
        //        Balance = 50000,
        //        CreditAmount = 50000,
        //        DebitAmount = 50000,
        //        PaymentDateTime = DateTime.Now,
        //    };

        //    db.Payments.Add(payment);

        //    if (frm["update"] == "on")
        //    {
        //        Customer customer = db.Customers.Find(TemporaryUserData.UserID);

        //        customer.FirstName = frm["FirstName"];
        //        customer.LastName = frm["LastName"];
        //        customer.Address = frm["Address"];
        //        customer.City = frm["City"];

        //    }

        //    ShippingDetail spdetail = new ShippingDetail()
        //    {
        //        FirstName = frm["FirstName"],
        //        LastName = frm["LastName"],
        //        Address = frm["Address"],
        //        City = frm["City"]
        //    };

        //    db.ShippingDetails.Add(spdetail);
        //    db.SaveChanges();

        //    List<OrderDetail> cart = db.OrderDetails.Where(z => z.IsCompleted == false && z.CustomerID == TemporaryUserData.UserID).ToList();

        //    foreach (var item in cart)
        //    {
        //        item.IsCompleted = true;
        //        item.Product.UnitsInStock -= item.Quantity;

        //        Order order = new Order()
        //        {
        //            PaymentID = payment.PaymentID,
        //            ShippingID = spdetail.ShippingID,
        //            OrderDetailID = item.OrderDetailID,
        //            Discount = item.Discount,
        //            TotalAmount = item.TotalAmount,
        //            IsCompleted = true,
        //            OrderDate = DateTime.Now,
        //            Dispatched = false,
        //            DispatchDate = DateTime.Now.AddDays(3),
        //            Shipped = false,
        //            ShippedDate = DateTime.Now.AddDays(4),
        //            Deliver = false,
        //            DeliveryDate = DateTime.Now.AddDays(5),
        //            CancelOrder = false
        //        };

        //        db.Orders.Add(order);
        //    }
        //    db.SaveChanges();

        //    return RedirectToAction("FinishShopping", "Shopping");
        //}

        public ActionResult FinishShopping()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddToCart(int id, FormCollection frm)
        {
            if (Session["OnlineKullanici"] == null)
                return RedirectToAction("Login", "Login");

            int miktar = Convert.ToInt32(frm["quantity"]);

            ControlCart(id, miktar);
            return RedirectToAction("BookDetail", "Book", new { id = id });
        }

        //public ActionResult AddToWishlist(int id)
        //{
        //    if (Session["OnlineKullanici"] == null)
        //        return RedirectToAction("Login", "Login");

        //    ControlWishlist(id);


        //    return RedirectToAction("BookDetail", "Book", new { id = id });
        //}

        //private void ControlWishlist(int id)
        //{
        //    Wishlist wishlist = db.Wishlists.FirstOrDefault(x => x.ProductID == id && x.CustomerID == TemporaryUserData.UserID && x.IsActive == true);

        //    if (wishlist == null)
        //    {
        //        wishlist = new Wishlist();
        //        wishlist.ProductID = id;
        //        wishlist.CustomerID = TemporaryUserData.UserID;
        //        wishlist.IsActive = true;

        //        db.Wishlists.Add(wishlist);
        //        db.SaveChanges();
        //    }
        //}

        public ActionResult Cart()
        {

            return View(db.Orders.Where(x => x.ApplicationUser_Id == TemporaryUserData.UserID && x.IsCompleted == false).ToList());
        }

        public ActionResult RemoveFromCart(int id)
        {
            db.Orders.Remove(db.Orders.Find(id));
            db.SaveChanges();

            return Redirect(Request.UrlReferrer.ToString());

        }

        //public ActionResult AddToWishlistFromCart(int id)
        //{
        //    ControlWishlist(db.Orders.Find(id).BookID); // Wishlist'e ekliyor.

        //    db.Orders.Remove(db.Orders.Find(id)); //OrderDetails'ten siliyor...
        //    db.SaveChanges();

        //    return Redirect(Request.UrlReferrer.ToString());
        //}

        //public ActionResult Wishlist()
        //{
        //    return View(db.Wishlists.Where(x => x.CustomerID == TemporaryUserData.UserID && x.IsActive == true).ToList());
        //}
        //public ActionResult AddToCartFromWishlist(int id)
        //{
        //    ControlCart(db.Wishlists.Find(id).ProductID); // Cart'a ekliyor

        //    Wishlist wishlist = db.Wishlists.Find(id); //gerçek silme işlemi değil. 
        //    wishlist.IsActive = false;
        //    db.SaveChanges();

        //    return Redirect(Request.UrlReferrer.ToString());
        //}

        //public ActionResult RemoveFromWishlist(int id) // gerçek silme işlemi yapmıyoruz. Listeden siliyoruz.
        //{
        //    Wishlist wishlist = db.Wishlists.Find(id);
        //    wishlist.IsActive = false;
        //    db.SaveChanges();

        //    // return RedirectToAction("Wishlist","Shopping"); -- alttaki satır ile aynı işlevi yapar
        //    return Redirect(Request.UrlReferrer.ToString());
        //}

        //[HttpPost]
        //public ActionResult UpdateQuantity(int id, FormCollection frm)
        //{
        //    OrderDetail od = db.OrderDetails.Find(id);
        //    int miktar = Convert.ToInt32(frm["quantity"]);

        //    od.Quantity = miktar;
        //    od.TotalAmount = od.Quantity * od.UnitPrice * (1 - od.Discount);

        //    db.SaveChanges();

        //    return Redirect(Request.UrlReferrer.ToString());
        //}
        public void ControlCart(int id, int miktar = 1)
        {
            Order od = db.Orders.Where(x => x.BookID == id && x.IsCompleted == false && x.ApplicationUser_Id == TemporaryUserData.UserID).FirstOrDefault();

            if (od == null) // yeni kayıt
            {
                od = new Order();
                od.BookID = id;
                od.ApplicationUser_Id = TemporaryUserData.UserID;
                od.IsCompleted = false;
                od.TotalAmount = db.Books.Find(id).CreditAmount;
                //od.Discount = db.Products.Find(id).Discount;
                od.OrderDate = DateTime.Now;

                //if (db.Books.Find(id).UnitsInStock >= miktar)
                //    od.Quantity = miktar;
                //else
                //    od.Quantity = db.Products.Find(id).UnitsInStock;

                // od.TotalAmount = od.Quantity * od.UnitPrice * (1 - od.Discount);
                db.Orders.Add(od);
            }
            //else // update
            //{
            //    if (db.Books.Find(id).UnitsInStock > od.Quantity + miktar)
            //    {
            //        od.Quantity += miktar;
            //        od.TotalAmount = od.Quantity * od.UnitPrice * (1 - od.Discount);
            //    }
            //}
            db.SaveChanges();
        }
    }
}