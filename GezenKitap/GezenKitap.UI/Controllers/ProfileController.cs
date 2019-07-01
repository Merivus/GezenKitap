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
    public class ProfileController : Controller
    {
        ApplicationDbContext db;
        public ProfileController()
        {
            db = new ApplicationDbContext();
        }
        public ActionResult UpdateProfile()
        {
            return View(db.Users.Find(TemporaryUserData.UserID));
        }

        [HttpPost]
        public ActionResult UpdateProfile(FormCollection frm)
        {
            ApplicationUser customer = db.Users.Find(TemporaryUserData.UserID);

            customer.FirstName = frm["FirstName"];
            customer.LastName = frm["LastName"];
            customer.Password = frm["Password"];
            customer.UserName = frm["UserName"];
            customer.Gender = frm["Gender"] == "false" ? false : true;
            customer.BirthDate = DateTime.Parse(frm["BirthDate"]);
            customer.Address = frm["Address"];
            customer.City = frm["City"];
            customer.Country = frm["Country"];

            db.SaveChanges();
            return RedirectToAction("ProfileUpdated", "Profile");

        }

        public ActionResult ProfileUpdated()
        {
            return View();
        }
    }
}