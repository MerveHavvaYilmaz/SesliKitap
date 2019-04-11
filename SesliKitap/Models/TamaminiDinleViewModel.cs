using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SesliKitap.Models
{
    public class TamaminiDinleViewModel
    {
        public int UrunID { get; set; }
        public string UrunAdi { get; set; }
        public string Resim { get; set; }
        public string SesDosyasi { get; set; }
    }
}