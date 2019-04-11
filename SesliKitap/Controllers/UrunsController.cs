using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using SesliKitap.Models;

namespace SesliKitap.Controllers
{
    public class UrunsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Uruns
        public ActionResult Index(string aranacakkelime)
        {
            var uruns = db.Uruns.Include(u => u.Kategori);
            return View(uruns.ToList());
        }
        [HttpPost]
        public ActionResult Ara(FormCollection Nesneler)
        {
            string AranacakKelime = Nesneler["txtAra"];
            return RedirectToAction("Index", new { AranacakKelime = AranacakKelime });
        }

        // GET: Uruns/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Urun urun = db.Uruns.Find(id);
            if (urun == null)
            {
                return HttpNotFound();
            }
            return View(urun);
        }

        // GET: Uruns/Create
        public ActionResult Create()
        {
            ViewBag.KategoriID = new SelectList(db.Kategoris, "KategoriID", "KategoriAdi");
            return View();
        }

        // POST: Uruns/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UrunID,UrunAdi,EklenmeTarihi,Ozet,Süre,Yazar,Seslendiren,Demo,SesDosyası,Resim,KategoriID")] Urun urun)
        {
            if (ModelState.IsValid)
            {
                db.Uruns.Add(urun);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.KategoriID = new SelectList(db.Kategoris, "KategoriID", "KategoriAdi", urun.KategoriID);
            return View(urun);
        }
        [HttpPost]
        public ActionResult KitapEkle(Urun urun, HttpPostedFileBase Resim,HttpPostedFileBase Demo,HttpPostedFileBase SesDosyasi)
        {
            Urun u = new Urun();
            if (ModelState.IsValid)
            {
                if (Demo != null)
                {
                    string fileName = Path.GetFileName(Demo.FileName);
                    int fileSize = Demo.ContentLength;
                    int Size = fileSize / 1000000;
                    Demo.SaveAs(Server.MapPath("../Uploads/Demo/" + fileName));
                    urun.Demo = "../Uploads/Demo/" + fileName;       
                }

                if(Resim!=null)
                {
                    WebImage img = new WebImage(Resim.InputStream);
                    FileInfo fotoinfo = new FileInfo(Resim.FileName);
                    string newfoto = Guid.NewGuid().ToString() + fotoinfo.Extension;
                    img.Resize(800, 350);
                    img.Save("~/Uploads/Resim/" + newfoto);
                    urun.Resim = "../Uploads/Resim/" + newfoto;
                }
                if (SesDosyasi != null)
                {
                    string fileName = Path.GetFileName(Demo.FileName);
                    int fileSize = Demo.ContentLength;
                    int Size = fileSize / 1000000;
                    SesDosyasi.SaveAs(Server.MapPath("~/Uploads/SesDosyasi/" + fileName));
                    urun.SesDosyası = "../Uploads/SesDosyasi/" + fileName;
                }
                ViewBag.KategoriID = new SelectList(db.Kategoris, "KategoriID", "KategoriAdi", urun.KategoriID);
                var userid = User.Identity.GetUserId();
                u.UserID = userid;
                u.KategoriID = urun.KategoriID;
                u.Ozet = urun.Ozet;
                u.DinlenmeSayisi = urun.DinlenmeSayisi;
                u.Seslendiren = urun.Seslendiren;
                u.Süre = urun.Süre;
                u.UrunAdi = urun.UrunAdi;
                u.Yazar = urun.Yazar;
                u.Demo = urun.Demo;
                u.Resim = urun.Resim;
                u.SesDosyası = urun.SesDosyası;
                db.Uruns.Add(u);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("KitapEkle");
        }

  

        public ActionResult KitapEkle()
        {
            ViewBag.KategoriID = new SelectList(db.Kategoris, "KategoriID", "KategoriAdi");
            return View();
        }
        [HttpPost]
        public ActionResult KitapDüzenle(int id, Urun urun, HttpPostedFileBase Resim, HttpPostedFileBase Demo, HttpPostedFileBase SesDosyasi)
        {
            var kitap = db.Uruns.Where(x => x.UrunID == id).SingleOrDefault();
            if (Resim != null & Demo != null & SesDosyasi != null)
            {
                if (System.IO.File.Exists(Server.MapPath(urun.Resim)))
                {
                    System.IO.File.Delete(Server.MapPath(urun.Resim));
                }
                WebImage img = new WebImage(Resim.InputStream);
                FileInfo fotoinfo = new FileInfo(Resim.FileName);
                string newfoto = Guid.NewGuid().ToString() + fotoinfo.Extension;
                img.Resize(800, 350);
                img.Save("~/Uploads/Resim/" + newfoto);
                urun.Resim = "../Uploads/Resim/" + newfoto;

                string fileName = Path.GetFileName(Demo.FileName);
                int fileSize = Demo.ContentLength;
                int Size = fileSize / 1000000;
                Demo.SaveAs(Server.MapPath("~/Uploads/Demo/" + fileName));
                urun.Demo = "../Uploads/Demo/" + fileName;

                string fileNamee = Path.GetFileName(Demo.FileName);
                int fileSizee = Demo.ContentLength;
                int Sizee = fileSize / 1000000;
                SesDosyasi.SaveAs(Server.MapPath("~/Uploads/SesDosyasi/" + fileName));
                urun.SesDosyası = "../Uploads/SesDosyasi/" + fileName;

                ViewBag.KategoriID = new SelectList(db.Kategoris, "KategoriID", "KategoriAdi", urun.KategoriID);
                kitap.KategoriID = urun.KategoriID;
                kitap.DinlenmeSayisi = urun.DinlenmeSayisi;
                kitap.EklenmeTarihi=urun.EklenmeTarihi;
                kitap.Ozet = urun.Ozet;
                kitap.Seslendiren = urun.Seslendiren;
                kitap.Süre = urun.Süre;
                kitap.UrunAdi = urun.UrunAdi;
                kitap.Yazar = urun.Yazar;
                kitap.Demo = urun.Demo;
                kitap.Resim = urun.Resim;
                kitap.SesDosyası = urun.SesDosyası;
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
        public ActionResult KitapDüzenle(int id)
        {
            var kitap = db.Uruns.Where(x => x.UrunID == id).SingleOrDefault();
            if (kitap == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Urun urun = db.Uruns.Find(id);
            if (urun == null)
            {
                return HttpNotFound();
            }
            ViewBag.KategoriID = new SelectList(db.Kategoris, "KategoriID", "KategoriAdi", urun.KategoriID);
            return View(urun);
        }
        public ActionResult Anasayfa()
        {
            var result = (from a in db.Uruns
                          select new FotoViewModel()
                          {
                              Resim = a.Resim,
                              UrunID = a.UrunID,
                              UrunAdi = a.UrunAdi
                          }).OrderByDescending(x => x.UrunID).Take(30).ToList();          
            return View(result);
        }

        public ActionResult SonEklenenler()
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
        public ActionResult KategoriUrunleriGetir(int id)
        {
            var result = (from a in db.Uruns
                          where a.KategoriID == id
                          select new KategoriViewModel()
                          {
                              Resim = a.Resim,
                              UrunID = a.UrunID
                          }).OrderByDescending(x => x.UrunID).ToList();
            return View(result);
        }

        public ActionResult BilgileriGetir(int id)
        {
            var result = (from a in db.Uruns
                          where a.UrunID == id
                          select new BilgilerViewModel()
                          {
                              Resim=a.Resim,
                              UrunAdi = a.UrunAdi,
                              Yazar = a.Yazar,
                              Seslendiren = a.Seslendiren,
                              Süre = a.Süre,
                              Demo = a.Demo,
                              Ozet=a.Ozet
                          }).FirstOrDefault();
            var result2 = db.Uruns.Where(x => x.UrunID == id).SingleOrDefault();
            result2.DinlenmeSayisi += 1;
            db.SaveChanges();
            return View(result);
        }

        public ActionResult TamaminiDinle(int id)
        {
            var result = (from a in db.Uruns
                          where a.UrunID == id
                          select new TamaminiDinleViewModel()
                          {
                              Resim = a.Resim,
                              UrunAdi = a.UrunAdi,
                              SesDosyasi = a.SesDosyası
                          }).FirstOrDefault();
                          return View(result);
        }

        public ActionResult EnCokDinlenenler()
        {
            var result = (from a in db.Uruns
                          select new FotoViewModel()
                          {
                              Resim = a.Resim,
                              UrunID = a.UrunID,
                              UrunAdi = a.UrunAdi,
                              DinlenmeSayisi=a.DinlenmeSayisi
                          }).OrderByDescending(x => x.DinlenmeSayisi).Take(18).ToList();
     
            return View(result);
        }




            // GET: Uruns/Edit/5
            public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Urun urun = db.Uruns.Find(id);
            if (urun == null)
            {
                return HttpNotFound();
            }
            ViewBag.KategoriID = new SelectList(db.Kategoris, "KategoriID", "KategoriAdi", urun.KategoriID);
            return View(urun);
        }

        // POST: Uruns/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UrunID,UrunAdi,EklenmeTarihi,Ozet,Süre,Yazar,Seslendiren,Demo,SesDosyası,Resim,KategoriID")] Urun urun)
        {
            if (ModelState.IsValid)
            {
                db.Entry(urun).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.KategoriID = new SelectList(db.Kategoris, "KategoriID", "KategoriAdi", urun.KategoriID);
            return View(urun);
        }

        // GET: Uruns/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Urun urun = db.Uruns.Find(id);
            if (urun == null)
            {
                return HttpNotFound();
            }
            return View(urun);
        }

        // POST: Uruns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Urun urun = db.Uruns.Find(id);
            db.Uruns.Remove(urun);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
