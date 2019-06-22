using GezenKitap.DATA.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GezenKitap.DAL.Mappings
{
    public class AuthorMapping : EntityTypeConfiguration<Author>
    {
        public AuthorMapping()
        {
            HasKey(x => x.AuthorID);
            Property(x => x.FirstName).HasMaxLength(50).IsRequired();
            Property(x => x.LastName).HasMaxLength(50).IsRequired();
        }
    }
}
