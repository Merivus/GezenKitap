using GezenKitap.DATA.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GezenKitap.DAL.Mappings
{
    public class CategoryMapping : EntityTypeConfiguration<Category>
    {
        public CategoryMapping()
        {
            HasKey(x => x.CategoryID);
            Property(x => x.CategoryName).HasMaxLength(50).IsRequired();
            Property(x => x.Description).HasMaxLength(500).IsRequired();
            Property(x => x.IsActive);

        }
    }
}
