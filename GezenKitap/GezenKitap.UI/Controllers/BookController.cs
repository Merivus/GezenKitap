using GezenKitap.BLL.Repository;
using GezenKitap.BLL.UnitOfWork;
using GezenKitap.DAL;
using GezenKitap.DATA.Entities;
using GezenKitap.UI.Models;
using Microsoft.AspNet.Identity;
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
        EFRepository<Book> repBook;
        EFUnitOfWork uow;

        public BookController()
        {
            db = new ApplicationDbContext();
            repBook = new EFRepository<Book>(db);
            uow = new EFUnitOfWork(db);
        }

        public ActionResult Book(int id)
        {
            var UserID = User.Identity.GetUserId();
            return View(db.Books.Where(x => x.CategoryID == id 
                        && x.UserID != UserID
                        && x.IsActive && !x.IsDelete).ToList());
        }

        [Authorize]
        public ActionResult AddBook(int? id)
        {
            ViewData["CategoryID"] = new SelectList(db.Categories.ToList().Select(x => new
            {
                x.CategoryID,
                x.CategoryName
            }), "CategoryID", "CategoryName");

            ViewData["AuthorID"] = new SelectList(
                db.Authors.ToList().Select(x => new {
                    x.AuthorID,
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


        [Authorize]
        [HttpPost]
        public ActionResult AddBook(Book model)
        {
            ViewData["CategoryID"] = new SelectList(db.Categories.ToList().Select(x => new
            {
                x.CategoryID,
                x.CategoryName
            }), "CategoryID", "CategoryName");

            ViewData["AuthorID"] = new SelectList(
                db.Authors.ToList().Select(x => new {
                    x.AuthorID,
                    AuthorName = x.FirstName + " " + x.LastName
                })
                , "AuthorID", "AuthorName");

            ViewData["StatusID"] = new SelectList(db.Statuses.ToList().Select(x => new
            {
                x.StatusID,
                x.StatusName
            }), "StatusID", "StatusName");

            model.UserID = User.Identity.GetUserId();
            model.IsDelete = false;
            model.IsActive = true;

            if (Request.Files.Count > 0)
            {
                try
                {
                    var file = Request.Files[0];
                    string pic = System.IO.Path.GetFileName(file.FileName);
                    string path = System.IO.Path.Combine(
                                           Server.MapPath("~/img/Books"), pic);
                    // file is uploaded
                    file.SaveAs(path);

                    model.ImageUrl = "~/img/Books/" + pic;
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("Error", "Resim yüklenemiyor!" + ex.Message);

                    return View(model);
                }
            }

            if (false && !ModelState.IsValid)
            {
                ModelState.AddModelError("Error", "Bir hata oluştu!");

                return View(model);
            }
            else
            {
                try
                {
                    repBook.Add(model);
                    uow.SaveChanges();

                    return RedirectToAction("MyBooks","Book");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("Error", ex);

                    return View(model);
                }
            }
        }

        [Authorize]
        //int dolu geleceği için  int in yanındaki ? işaretini kaldırdık...
        public ActionResult UpdateBook(int id)
        {
            ViewData["CategoryID"] = new SelectList(db.Categories.ToList().Select(x => new
            {
                x.CategoryID,
                x.CategoryName
            }), "CategoryID", "CategoryName");

            ViewData["AuthorID"] = new SelectList(
                db.Authors.ToList().Select(x => new {
                    x.AuthorID,
                    AuthorName = x.FirstName + " " + x.LastName
                })
                , "AuthorID", "AuthorName");

            ViewData["StatusID"] = new SelectList(db.Statuses.ToList().Select(x => new
            {
                x.StatusID,
                x.StatusName
            }), "StatusID", "StatusName");

            var model = repBook.GetById(id);
            return View(model);
        }


        [Authorize]
        [HttpPost]
        public ActionResult UpdateBook(Book model)
        {
            ViewData["CategoryID"] = new SelectList(db.Categories.ToList().Select(x => new
            {
                x.CategoryID,
                x.CategoryName
            }), "CategoryID", "CategoryName");

            ViewData["AuthorID"] = new SelectList(
                db.Authors.ToList().Select(x => new {
                    x.AuthorID,
                    AuthorName = x.FirstName + " " + x.LastName
                })
                , "AuthorID", "AuthorName");

            ViewData["StatusID"] = new SelectList(db.Statuses.ToList().Select(x => new
            {
                x.StatusID,
                x.StatusName
            }), "StatusID", "StatusName");
            
            if (Request.Files.Count > 0 && Request.Files[0].ContentLength > 0)
            {
                try
                {
                    var file = Request.Files[0];
                    string pic = System.IO.Path.GetFileName(file.FileName);
                    string path = System.IO.Path.Combine(
                                           Server.MapPath("~/img/Books"), pic);
                    // file is uploaded
                    file.SaveAs(path);
                    model.ImageUrl = "~/img/Books/" + pic;
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("Error", "Resim yüklenemiyor!" + ex.Message);

                    return View(model);
                }
            }

            if (false && !ModelState.IsValid)
            {
                ModelState.AddModelError("Error", "Bir hata oluştu!");

                return View(model);
            }
            else
            {
                try
                {
                    repBook.Update(model);
                    uow.SaveChanges();

                    return RedirectToAction("MyBooks");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("Error", ex);

                    return View(model);
                }
            }
        }


        public ActionResult MyBooks()
        {
            var UserID = User.Identity.GetUserId();
            return View(db.Books.Where(x => x.UserID == UserID && !x.IsDelete).ToList());
        }

        public ActionResult BookDetail(int id)
        {
            ViewData["Reviews"] = db.Reviews.Where(x => x.BookID == id && x.IsDeleted == false).ToList();
            return View(db.Books.Find(id));
        }
                
        [HttpPost]
        public ActionResult AddReview(int id, FormCollection frm)
        {
            Review review = new Review()
            {
                Comment = frm["review"],
                ApplicationUser_Id = User.Identity.GetUserId(),
                DateTime = DateTime.Now,
                BookID = id,
                Name = frm["name"] == "" ? "Misafir Kullanıcı" : frm["name"],
                Rate = int.Parse(frm["rate"])
            };

            db.Reviews.Add(review);
            db.SaveChanges();

            return RedirectToAction("BookDetail", new { id = id });
        }
    }
}
