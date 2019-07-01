using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GezenKitap.DATA.Entities
{
    public class ApplicationUser : IdentityUser
    {
        
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public bool Gender { get; set; }
        [Column(TypeName = "DateTime2")]
        public DateTime? BirthDate { get; set; }

        public string Password { get; set; }

        public string Address { get; set; }

        public string PostalCode { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public string Picture { get; set; }

        //Kullanıcının toplam kredisi
        public int Credit { get; set; }
        [Column(TypeName = "DateTime2")]
        public DateTime? CreatedDate { get; set; }

        public DateTime LastLogin { get; set; }

        public string Notes { get; set; }


        public virtual ICollection<Order> Orders { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }

        public virtual ICollection<Book> Books { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}
