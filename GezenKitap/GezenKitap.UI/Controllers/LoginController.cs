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
    public class LoginController : Controller
    {
        ApplicationDbContext db;
        public LoginController()
        {
            db = new ApplicationDbContext();
        }
        [HttpGet]
        public ActionResult Login()
        {
            TemporaryUserData.ReturnUrl = System.Web.HttpContext.Current.Request.UrlReferrer.ToString();
            return View();
        }

        [HttpPost]
        public ActionResult Login(FormCollection frm)
        {
            string kullaniciAdi = frm["username"];
            string sifre = frm["password"];

            ApplicationUser customer = db.Users.FirstOrDefault(x => x.UserName == kullaniciAdi && x.Password == sifre);

            if (customer != null)
            {
                Session["OnlineKullanici"] = customer.UserName;
                TemporaryUserData.UserID = customer.Id;
                if (TemporaryUserData.ReturnUrl.Contains("Register"))
                    return RedirectToAction("Index", "Home");

                return Redirect(TemporaryUserData.ReturnUrl);
            }

            return View();
        }

        public ActionResult Logout()
        {
            db.Users.Find(TemporaryUserData.UserID).LastLogin = DateTime.Now;
            db.SaveChanges();

            Session["OnlineKullanici"] = null;
            TemporaryUserData.UserID = "0";

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Register()
        {
            TemporaryUserData.ReturnUrl = System.Web.HttpContext.Current.Request.UrlReferrer.ToString();
            return View();
        }

        [HttpPost]
        public ActionResult Register(FormCollection frm)
        {
            string kullaniciAdi = frm["username"];

            ApplicationUser customer = db.Users.FirstOrDefault(x => x.UserName == kullaniciAdi);

            if (customer != null)
                return View();
            else
            {
                customer = new ApplicationUser();
                customer.FirstName = frm["firstname"];
                customer.LastName = frm["lastname"];
                customer.UserName = kullaniciAdi;
                customer.Password = frm["password"];
                customer.Gender = frm["gender"] == "on" ? true : false;
                customer.BirthDate = DateTime.Parse(frm["birthdate"]);
                customer.CreatedDate = DateTime.Now;
                customer.LastLogin = DateTime.Now;

                db.Users.Add(customer);
                db.SaveChanges();

                Session["OnlineKullanici"] = kullaniciAdi;
                TemporaryUserData.UserID = customer.Id;

                return Redirect(TemporaryUserData.ReturnUrl);
            }
        }
    }
}