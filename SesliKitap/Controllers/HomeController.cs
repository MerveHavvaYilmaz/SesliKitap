using SesliKitap.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SesliKitap.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
      
        public ActionResult Index()
        {
            var result = (from a in db.Uruns
                          select new FotoViewModel()
                          {
                              Resim = a.Resim,
                              UrunID = a.UrunID,
                              UrunAdi = a.UrunAdi
                          }).OrderByDescending(x => x.UrunID).Take(12).ToList();

            return View(result);
        }

        public ActionResult KitapAra(string Ara=null)
        {
            var aranan = db.Uruns.Where(x => x.UrunAdi.Contains(Ara)).ToList();
            return View(aranan.OrderByDescending(x=>x.EklenmeTarihi));
        }

        public ActionResult KitapAraPartial()
        {
            return PartialView();
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}