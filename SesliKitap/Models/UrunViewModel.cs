using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SesliKitap.Models
{
    public class UrunViewModel
    {
        public string UrunAdi { get; set; }
        public string KategoriID { get; set; }      
        public string Süre { get; set; }
        public string Yazar { get; set; }       
        public string Seslendiren { get; set; }
        public string Ozet { get; set; }
        public string Resim { get; set; }
        public string Demo { get; set; }
        public string SesDosyası { get; set; }
        public int DinlenmeSayisi { get; set; }


    }
}