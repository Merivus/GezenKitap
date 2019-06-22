using GezenKitap.DATA.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GezenKitap.DAL.Mappings
{
    public class BookMapping : EntityTypeConfiguration<Book>
    {
        public BookMapping()
        {
            HasKey(x => x.BookID);
            Property(x => x.BookName).HasMaxLength(100).IsRequired();
            Property(x => x.Description).HasMaxLength(1000).IsRequired();
            Property(x => x.PurchaseDate).HasColumnType("datetime2");
            Property(x => x.ImageUrl).HasMaxLength(300).IsRequired();
            Property(x => x.AltText).HasMaxLength(300).IsRequired();
            Property(x => x.PageCount).IsRequired();
            Property(x => x.CreditAmount).IsRequired();
            Property(x => x.Notes).HasMaxLength(300);
            Property(x => x.IsActive).IsOptional();



            HasRequired(x => x.Category).WithMany(x => x.Books).HasForeignKey(x => x.CategoryID);
            HasRequired(x => x.ApplicationUser).WithMany(x => x.Books).HasForeignKey(x => x.UserID);
            HasRequired(x => x.Author).WithMany(x => x.Books).HasForeignKey(x => x.AuthorID);
            HasRequired(x => x.Status).WithMany(x => x.Books).HasForeignKey(x => x.StatusID);
        }
    }
}
