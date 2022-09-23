using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebAppTacos.Models;

namespace WebAppTacos.Controllers
{
    public class TilausrivitController : Controller
    {
        private TacosDBEntities3 db = new TacosDBEntities3();

        // GET: Tilausrivit
        public ActionResult Index()
        {
            if (Session["Kayttajatunnus"] == null)
            {
                ViewBag.LoggedStatus = "Ei kirjautunut";
                return RedirectToAction("Kirjautuminen", "Home");
            }
            else
            {
                ViewBag.LoggedStatus = "Kirjautunut";
            }
            var tilausrivit = db.Tilausrivit.Include(t => t.Tilaukset).Include(t => t.Tuotteet);
            return View(tilausrivit.ToList());
        }

        // GET: Tilausrivit/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["Kayttajatunnus"] == null)
            {
                ViewBag.LoggedStatus = "Ei kirjautunut";
                return RedirectToAction("Kirjautuminen", "Home");
            }
            else
            {
                ViewBag.LoggedStatus = "Kirjautunut";
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tilausrivit tilausrivit = db.Tilausrivit.Find(id);
            if (tilausrivit == null)
            {
                return HttpNotFound();
            }
            return View(tilausrivit);
        }

        // GET: Tilausrivit/Create
        public ActionResult Create()
        {
            if (Session["Kayttajatunnus"] == null)
            {
                ViewBag.LoggedStatus = "Ei kirjautunut";
                return RedirectToAction("Kirjautuminen", "Home");
            }
            else
            {
                ViewBag.LoggedStatus = "Kirjautunut";
            }
            ViewBag.TilausID = new SelectList(db.Tilaukset, "TilausID", "Toimitusosoite");
            ViewBag.TuoteID = new SelectList(db.Tuotteet, "TuoteID", "Nimi");
            return View();
        }

        // POST: Tilausrivit/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TilausriviID,TilausID,TuoteID,Maara,Hinta")] Tilausrivit tilausrivit)
        {
            if (Session["Kayttajatunnus"] == null)
            {
                ViewBag.LoggedStatus = "Ei kirjautunut";
                return RedirectToAction("Kirjautuminen", "Home");
            }
            else
            {
                ViewBag.LoggedStatus = "Kirjautunut";
            }
            if (ModelState.IsValid)
            {
                db.Tilausrivit.Add(tilausrivit);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TilausID = new SelectList(db.Tilaukset, "TilausID", "Toimitusosoite", tilausrivit.TilausID);
            ViewBag.TuoteID = new SelectList(db.Tuotteet, "TuoteID", "Nimi", tilausrivit.TuoteID);
            return View(tilausrivit);
        }

        // GET: Tilausrivit/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["Kayttajatunnus"] == null)
            {
                ViewBag.LoggedStatus = "Ei kirjautunut";
                return RedirectToAction("Kirjautuminen", "Home");
            }
            else
            {
                ViewBag.LoggedStatus = "Kirjautunut";
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tilausrivit tilausrivit = db.Tilausrivit.Find(id);
            if (tilausrivit == null)
            {
                return HttpNotFound();
            }
            ViewBag.TilausID = new SelectList(db.Tilaukset, "TilausID", "Toimitusosoite", tilausrivit.TilausID);
            ViewBag.TuoteID = new SelectList(db.Tuotteet, "TuoteID", "Nimi", tilausrivit.TuoteID);
            return View(tilausrivit);
        }

        // POST: Tilausrivit/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TilausriviID,TilausID,TuoteID,Maara,Hinta")] Tilausrivit tilausrivit)
        {
            if (Session["Kayttajatunnus"] == null)
            {
                ViewBag.LoggedStatus = "Ei kirjautunut";
                return RedirectToAction("Kirjautuminen", "Home");
            }
            else
            {
                ViewBag.LoggedStatus = "Kirjautunut";
            }
            if (ModelState.IsValid)
            {
                db.Entry(tilausrivit).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TilausID = new SelectList(db.Tilaukset, "TilausID", "Toimitusosoite", tilausrivit.TilausID);
            ViewBag.TuoteID = new SelectList(db.Tuotteet, "TuoteID", "Nimi", tilausrivit.TuoteID);
            return View(tilausrivit);
        }

        // GET: Tilausrivit/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["Kayttajatunnus"] == null)
            {
                ViewBag.LoggedStatus = "Ei kirjautunut";
                return RedirectToAction("Kirjautuminen", "Home");
            }
            else
            {
                ViewBag.LoggedStatus = "Kirjautunut";
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tilausrivit tilausrivit = db.Tilausrivit.Find(id);
            if (tilausrivit == null)
            {
                return HttpNotFound();
            }
            return View(tilausrivit);
        }

        // POST: Tilausrivit/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["Kayttajatunnus"] == null)
            {
                ViewBag.LoggedStatus = "Ei kirjautunut";
                return RedirectToAction("Kirjautuminen", "Home");
            }
            else
            {
                ViewBag.LoggedStatus = "Kirjautunut";
            }
            Tilausrivit tilausrivit = db.Tilausrivit.Find(id);
            db.Tilausrivit.Remove(tilausrivit);
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
