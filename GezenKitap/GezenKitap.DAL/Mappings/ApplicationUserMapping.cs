using GezenKitap.DATA.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GezenKitap.DAL.Mappings
{
    public class ApplicationUserMapping : EntityTypeConfiguration<ApplicationUser>
    {
        public ApplicationUserMapping()
        {
            Property(x => x.FirstName).HasMaxLength(50).IsRequired();
            Property(x => x.LastName).HasMaxLength(50).IsRequired();
            Property(x => x.Gender);
            Property(x => x.BirthDate).HasColumnType("datetime2").IsOptional();
            Property(x => x.Address).HasMaxLength(250);
            Property(x => x.PostalCode).HasMaxLength(250);
            Property(x => x.City).HasMaxLength(20);
            Property(x => x.Country).HasMaxLength(20);
            Property(x => x.Picture).HasMaxLength(500);
            Property(x => x.Credit);
            Property(x => x.CreatedDate).HasColumnType("datetime2").IsOptional();
            Property(x => x.LastLogin).HasColumnType("datetime2");
            Property(x => x.Notes).HasMaxLength(250);


        }
    }
}
