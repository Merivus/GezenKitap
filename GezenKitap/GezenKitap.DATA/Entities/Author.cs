using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GezenKitap.DATA.Entities
{
    public class Author
    {
        //Yazar tablosu
        public int AuthorID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }


        public virtual ICollection<Book> Books { get; set; }
    }
}
