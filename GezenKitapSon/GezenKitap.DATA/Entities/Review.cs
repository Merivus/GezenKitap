using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GezenKitap.DATA.Entities
{
    public class Review
    {
        public int ReviewID { get; set; }

        public string ApplicationUser_Id { get; set; }

        public int BookID { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Comment { get; set; }

        public int Rate { get; set; }

        public DateTime DateTime { get; set; }

        public bool IsDeleted { get; set; }


        public virtual ApplicationUser ApplicationUser { get; set; }

        public virtual Book Book { get; set; }
    }
}
