using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SesliKitap.Models
{
    public class FotoViewModel
    {
        public string Resim { get; set; }
        public int UrunID { get; set; }
        public string UrunAdi { get; set; }
        public int DinlenmeSayisi { get; set; }

    }
}