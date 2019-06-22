using GezenKitap.DAL;
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
            ApplicationDbContext db = new ApplicationDbContext();
            TempData["Bilim ve Sanat"] = db.Books.Where(x => x.CategoryID == 1)
                                           .OrderBy(r => Guid.NewGuid()).Take(4).ToList();
            TempData["Turizm"] = db.Books.Where(x => x.CategoryID == 2)
                                          .OrderBy(r => Guid.NewGuid()).Take(4).ToList();
            TempData["Tarih"] = db.Books.Where(x => x.CategoryID == 3)
                                          .OrderBy(r => Guid.NewGuid()).Take(4).ToList();
            TempData["Çocuk"] = db.Books.Where(x => x.CategoryID == 4)
                                          .OrderBy(r => Guid.NewGuid()).Take(4).ToList();
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