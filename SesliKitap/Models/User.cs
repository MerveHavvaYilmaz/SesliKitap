using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SesliKitap.Models
{
    public class User : ApplicationUser
    {
        [Key]
        public int UserID { get; set; }
        public string İsim { get; set; }
        public string Soyisim { get; set; }

        //Bir kullanıcının birden çok ürünü olabilir.
        public virtual ICollection<Urun> Uruns { get; set; }
        //Bir kullanıcın birden çok soru ve cevabı olabilir.
        public virtual ICollection<SoruVeCevap> SoruVeCevaps { get; set; }
    }
}