using GezenKitap.DATA.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GezenKitap.DAL.Mappings
{
    public class ReviewMapping : EntityTypeConfiguration<Review>
    {
        public ReviewMapping()
        {
            HasKey(x => x.ReviewID);
            Property(x => x.Name).HasMaxLength(50).IsRequired();
            Property(x => x.Email).HasMaxLength(50);
            Property(x => x.Comment).HasMaxLength(200).IsRequired();
            Property(x => x.Rate);
            Property(x => x.DateTime).HasColumnType("datetime2");
            Property(x => x.IsDeleted);


            HasRequired(x => x.Book).WithMany(x => x.Reviews).HasForeignKey(x => x.BookID);
            // HasRequired(x => x.Recipient).WithMany(x => x.Reviews).HasForeignKey(x => x.RecipientID);

        }
    }
}
