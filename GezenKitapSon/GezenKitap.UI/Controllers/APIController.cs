using GezenKitap.DATA.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GezenKitap.DAL;
using GezenKitap.BLL.Repository;

namespace GezenKitap.UI.Controllers
{
    public class APIController : ApiController
    {
        static ApplicationDbContext db = new ApplicationDbContext();

        [HttpPost]
        public int AddAuthor(Author item)
        {
            EFRepository<Author> rep = new EFRepository<Author>(db);

            rep.Add(item);


            //db.Authors.Add(item);
            db.SaveChanges();

            return item.AuthorID;
        }
    }
}

