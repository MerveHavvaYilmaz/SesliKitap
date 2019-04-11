using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SesliKitap.Models
{
    public class Urun
    {
        [Key]
        public int UrunID { get; set; }
        public string UrunAdi { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayName("Eklenme Tarihi")]
        public DateTime EklenmeTarihi { get; set; } = DateTime.Now;
        public int DinlenmeSayisi { get; set; }
        public string Ozet { get; set; }
        public string Süre { get; set; }
        public string Yazar { get; set; }
        public string Seslendiren { get; set; }
        public string Demo { get; set; }
        public string SesDosyası { get; set; }
        public string Resim { get; set; }
        //Bir ürün bir kategoriye ait olur.
        public int KategoriID { get; set; }
        public virtual Kategori Kategori { get; set; }

        //Bir ürün bir kullanıcıya ait olur
        public string UserID { get; set; }
        public virtual User Users { get; set; }
    }
}