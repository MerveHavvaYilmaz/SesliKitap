using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using SesliKitap.Models;

namespace SesliKitap.Controllers
{
    public class SoruVeCevapsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: SoruVeCevaps
        public ActionResult Index()
        {
            var soruVeCevaps = db.SoruVeCevaps.Include(s => s.Users);
            return View(soruVeCevaps.ToList());
        }

        public ActionResult SoruVeCevaplar()
        {
            var soruVeCevaps = db.SoruVeCevaps.Include(s => s.Users);
            return View(soruVeCevaps.ToList());
        }

        [HttpPost]
        public ActionResult SoruCevapEkle(SoruVeCevap sorucevap)
        {
            SoruVeCevap sc = new SoruVeCevap();
            if (ModelState.IsValid)
            {
                var userid = User.Identity.GetUserId();
                sc.UserID = userid;
                sc.Soru = sorucevap.Soru;
                sc.Cevap = sorucevap.Cevap;
                db.SoruVeCevaps.Add(sc);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("SoruCevapEkle");

        }
        public ActionResult SoruCevapEkle()
        {
            return View();
        }

        // GET: SoruVeCevaps/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SoruVeCevap soruVeCevap = db.SoruVeCevaps.Find(id);
            if (soruVeCevap == null)
            {
                return HttpNotFound();
            }
            return View(soruVeCevap);
        }

        // GET: SoruVeCevaps/Create
        public ActionResult Create()
        {
            //ViewBag.UserID = new SelectList(db.Users, "Id", "UserRole");
            return View();
        }

        // POST: SoruVeCevaps/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SoruCevapID,Soru,Cevap,UserID")] SoruVeCevap soruVeCevap)
        {
            if (ModelState.IsValid)
            {
                db.SoruVeCevaps.Add(soruVeCevap);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //ViewBag.UserID = new SelectList(db.Users, "Id", "UserRole", soruVeCevap.UserID);
            return View(soruVeCevap);
        }

        // GET: SoruVeCevaps/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SoruVeCevap soruVeCevap = db.SoruVeCevaps.Find(id);
            if (soruVeCevap == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserID = new SelectList(db.Users, "Id", "UserRole", soruVeCevap.UserID);
            return View(soruVeCevap);
        }

        // POST: SoruVeCevaps/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SoruCevapID,Soru,Cevap,UserID")] SoruVeCevap soruVeCevap)
        {
            if (ModelState.IsValid)
            {
                db.Entry(soruVeCevap).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserID = new SelectList(db.Users, "Id", "UserRole", soruVeCevap.UserID);
            return View(soruVeCevap);
        }

        // GET: SoruVeCevaps/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SoruVeCevap soruVeCevap = db.SoruVeCevaps.Find(id);
            if (soruVeCevap == null)
            {
                return HttpNotFound();
            }
            return View(soruVeCevap);
        }

        // POST: SoruVeCevaps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SoruVeCevap soruVeCevap = db.SoruVeCevaps.Find(id);
            db.SoruVeCevaps.Remove(soruVeCevap);
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
