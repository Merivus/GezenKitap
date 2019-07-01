using GezenKitap.DAL;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GezenKitap.UI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var userid = User.Identity.GetUserId();
            ApplicationDbContext db = new ApplicationDbContext();
            TempData["Kitaplar"] = db.Books.Where(x => x.UserID != userid && x.IsDelete == false && x.IsActive == true).OrderBy(r => Guid.NewGuid()).Take(8).ToList();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}