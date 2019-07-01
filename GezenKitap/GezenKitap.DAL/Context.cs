using GezenKitap.DAL.Mappings;
using GezenKitap.DATA.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GezenKitap.DAL
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base(@"server =(LocalDb)\MsSqlLocalDb; database = GznKitapDB; uid=sa; pwd=123", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<Author> Authors { get; set; }

        public DbSet<Book> Books { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Review> Reviews { get; set; } 

        //public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        public DbSet<Status> Statuses { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ApplicationUserMapping());
            modelBuilder.Configurations.Add(new AuthorMapping());
            modelBuilder.Configurations.Add(new BookMapping());
            modelBuilder.Configurations.Add(new CategoryMapping());
            modelBuilder.Configurations.Add(new OrderMapping());
            modelBuilder.Configurations.Add(new ReviewMapping());
            modelBuilder.Configurations.Add(new StatusMapping());
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }
    }
}
