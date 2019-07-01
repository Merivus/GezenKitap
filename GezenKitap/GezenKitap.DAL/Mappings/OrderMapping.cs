using GezenKitap.DATA.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GezenKitap.DAL.Mappings
{
    public class OrderMapping : EntityTypeConfiguration<Order>
    {
        public OrderMapping()
        {
            HasKey(x => x.OrderID);
            Property(x => x.TotalAmount);
            Property(x => x.OrderDate).HasColumnType("datetime2");
            Property(x => x.TrackingNumber);
            Property(x => x.TrackingAddedDate).HasColumnType("datetime2");
            Property(x => x.Notes).HasMaxLength(150);
            Property(x => x.State);


            HasRequired(x => x.Book).WithMany(x => x.Orders).HasForeignKey(x => x.BookID);
            //HasRequired(x => x.Recipient).WithMany(x => x.Orders).HasForeignKey(x => x.RecipientID);
        }
    }
}
