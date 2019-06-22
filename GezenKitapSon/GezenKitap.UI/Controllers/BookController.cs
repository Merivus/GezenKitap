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
    public class BookController : Controller
    {

        ApplicationDbContext db;
        public BookController()
        {
            db = new ApplicationDbContext();
        }
        public ActionResult Book(int id)
        {
            return View(db.Books.Where(x => x.CategoryID == id).ToList());
        }

        public ActionResult BookDetail(int id)
        {
            ViewData["Reviews"] = db.Reviews.Where(x => x.BookID == id && x.IsDeleted == false).ToList();
            return View(db.Books.Find(id));
        }

        [HttpPost]
        public ActionResult BeginForm(FormCollection frm)
        {
            int deger = int.Parse(frm["miktar"]);
            return View();
        }

        //[Authorize]
        public ActionResult AddBook(int? id)
        {
            if (Session["OnlineKullanici"] == null)
                return RedirectToAction("Login", "Login");

            ViewData["CategoryID"] = new SelectList(db.Categories.ToList().Select(x => new
            {
                x.CategoryID,
                x.CategoryName
            }), "CategoryID", "CategoryName");

            ViewData["AuthorID"] = new SelectList(
                db.Authors.ToList().Select(x => new {
                    AuthorID = x.AuthorID,
                    AuthorName = x.FirstName + " " + x.LastName
                })
                , "AuthorID", "AuthorName");

            ViewData["StatusID"] = new SelectList(db.Statuses.ToList().Select(x => new
            {
                x.StatusID,
                x.StatusName
            }), "StatusID", "StatusName");

            var model = new Book();
            if (id.HasValue) model.CategoryID = id.Value;

            return View(model);
        }

        [HttpPost]
        public ActionResult AddReview(int id, FormCollection frm)
        {
            Review review = new Review()
            {
                Comment = frm["review"],
                ApplicationUser_Id = TemporaryUserData.UserID,
                DateTime = DateTime.Now,
                BookID = id,
                Name = frm["name"] == "" ? "Misafir Kullanıcı" : frm["name"],
                Rate = int.Parse(frm["rate"])
            };

            db.Reviews.Add(review);
            db.SaveChanges();


            return RedirectToAction("ProductDetail", new { id = id });
        }
    }
}
