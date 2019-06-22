using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GezenKitap.DATA.Entities
{
    public class Order
    {
        public int OrderID { get; set; }

        public int BookID { get; set; }

        //Alıcı ID
        public string ApplicationUser_Id { get; set; }

        //Toplam Tutar (Yani kitabın tutarı, kredi cinsinden)
        public int TotalAmount { get; set; }

        public DateTime OrderDate { get; set; }

        // Kargo Takip No
        public bool TrackingNumber { get; set; }

        public DateTime TrackingAddedDate { get; set; }

        public string Notes { get; set; }

        //Kargo Numarası girildiyes true olacak.
        public bool IsCompleted { get; set; }


        public virtual Book Book { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
