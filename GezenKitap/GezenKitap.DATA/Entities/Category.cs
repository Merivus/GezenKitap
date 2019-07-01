using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GezenKitap.DATA.Entities
{
    public class Category
    {
        //Kategori Tablosu
        public int CategoryID { get; set; }

        public string CategoryName { get; set; }

        public string Description { get; set; }

        public bool IsActive { get; set; }


        public virtual ICollection<Book> Books { get; set; }
    }
}
