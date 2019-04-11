using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SesliKitap.Models
{
    public class SoruVeCevap
    {
        [Key]
        public int SoruCevapID { get; set; }
        public string Soru { get; set; }
        public string Cevap { get; set; }

        //Bir soru ve cevap bir kullanıcıya ait olur.
        public string UserID { get; set; }
        public virtual User Users { get; set; }
    }
}