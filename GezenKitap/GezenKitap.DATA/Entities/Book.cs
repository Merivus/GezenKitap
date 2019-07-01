using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GezenKitap.DATA.Entities
{
    public class Book
    {

        public int BookID { get; set; }

        public int CategoryID { get; set; }

        public string UserID { get; set; }

        public int AuthorID { get; set; }

        //Yıpranmış, az yıpranmış, temiz, sıfır gibi durumları tutacak tablodan gelen ID
        public int StatusID { get; set; }

        public string BookName { get; set; }

        public string Description { get; set; }

        //Yayınlanma tarihi
        public DateTime PurchaseDate { get; set; }

        public string ImageUrl { get; set; }

        public string AltText { get; set; }

        //Sayfa sayısı
        public int PageCount { get; set; }

        //Kitabın Kredisi. Yani fiyatı
        public int CreditAmount { get; set; }

        public string Notes { get; set; }

        //1 ise aramalarda görünecek. Kargo takip no girildiyse 0 olacak.
        public bool IsActive { get; set; }

        public bool IsDelete { get; set; } = false;


        public virtual Category Category { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        public virtual Author Author { get; set; }

        public virtual Status Status { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }
    }
}
