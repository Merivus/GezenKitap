using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GezenKitap.DATA.Entities
{
    public class Status
    {
        //Durumlar tablosu. Kitabın durumunu tutacak
        public int StatusID { get; set; }

        public string StatusName { get; set; }


        public virtual ICollection<Book> Books { get; set; }
    }
}
