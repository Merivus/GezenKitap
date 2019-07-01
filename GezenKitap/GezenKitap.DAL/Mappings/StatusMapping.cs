using GezenKitap.DATA.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GezenKitap.DAL.Mappings
{
    public class StatusMapping : EntityTypeConfiguration<Status>
    {
        public StatusMapping()
        {
            HasKey(x => x.StatusID);
            Property(x => x.StatusName).HasMaxLength(50).IsRequired();
        }
    }
}
