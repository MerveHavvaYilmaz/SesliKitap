using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SesliKitap.Models
{
    public class Kategori
    {
        [Key]
        public int KategoriID { get; set; }
        public string KategoriAdi { get; set; }

        //Bir kategoride birden çok kitap olabilir.
        public virtual ICollection<Urun> Uruns { get; set; }
    }
}