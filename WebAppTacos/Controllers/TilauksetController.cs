using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebAppTacos.Models;
using WebAppTacos.ViewModels;

using PagedList;

namespace WebAppTacos.Controllers
{
    public class TilauksetController : Controller
    {
        private TacosDBEntities3 db = new TacosDBEntities3();

        // GET: Tilaukset
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
            var tilaukset = db.Tilaukset.Include(t => t.Asiakkaat);
            return View(tilaukset.ToList());
        }

        // GET: Tilaukset/Details/5
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
            Tilaukset tilaukset = db.Tilaukset.Find(id);
            if (tilaukset == null)
            {
                return HttpNotFound();
            }
            return View(tilaukset);
        }

        // GET: Tilaukset/Details/5
        public ActionResult _ModalDetails(int? id)
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
            Tilaukset tilaukset = db.Tilaukset.Find(id);
            if (tilaukset == null)
            {
                return HttpNotFound();
            }
            return PartialView("_ModalDetails", tilaukset);
        }

        // GET: Tilaukset/Create
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
            ViewBag.AsiakasID = new SelectList(db.Asiakkaat, "AsiakasID", "Etunimi");
            return View();
        }

        // GET: Tilaukset/Create
        public ActionResult _ModalCreate()
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
            ViewBag.AsiakasID = new SelectList(db.Asiakkaat, "AsiakasID", "Etunimi");
            return PartialView();
        }

        // POST: Tilaukset/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TilausID,AsiakasID,Toimitusosoite,Postinumero,Tilauspvm,Toimituspvm")] Tilaukset tilaukset)
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
                db.Tilaukset.Add(tilaukset);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AsiakasID = new SelectList(db.Asiakkaat, "AsiakasID", "Etunimi", tilaukset.AsiakasID);
            return View(tilaukset);
        }

        // GET: Tilaukset/Edit/5
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
            Tilaukset tilaukset = db.Tilaukset.Find(id);
            if (tilaukset == null)
            {
                return HttpNotFound();
            }
            ViewBag.AsiakasID = new SelectList(db.Asiakkaat, "AsiakasID", "Etunimi", tilaukset.AsiakasID);
            return View(tilaukset);
        }

        // POST: Tilaukset/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TilausID,AsiakasID,Toimitusosoite,Postinumero,Tilauspvm,Toimituspvm")] Tilaukset tilaukset)
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
                db.Entry(tilaukset).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AsiakasID = new SelectList(db.Asiakkaat, "AsiakasID", "Etunimi", tilaukset.AsiakasID);
            return View(tilaukset);
        }

        //alla modaalin edit-ikkunan koodia

        public ActionResult _ModalEdit(int? id)
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
            Tilaukset tilaukset = db.Tilaukset.Find(id);
            if (tilaukset == null)
            {
                return HttpNotFound();
            }
            ViewBag.AsiakasID = new SelectList(db.Asiakkaat, "AsiakasID", "Etunimi", tilaukset.AsiakasID);
            return PartialView("_ModalEdit", tilaukset);
        }

        // POST: Tilaukset/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult _ModalEdit([Bind(Include = "TilausID,AsiakasID,Toimitusosoite,Postinumero,Tilauspvm,Toimituspvm")] Tilaukset tilaukset)
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
                db.Entry(tilaukset).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AsiakasID = new SelectList(db.Asiakkaat, "AsiakasID", "Etunimi", tilaukset.AsiakasID);
            return PartialView("_ModalEdit", tilaukset);
        }

        // GET: Tilaukset/Delete/5
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
            Tilaukset tilaukset = db.Tilaukset.Find(id);
            if (tilaukset == null)
            {
                return HttpNotFound();
            }
            return View(tilaukset);
        }

        // POST: Tilaukset/Delete/5
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
            Tilaukset tilaukset = db.Tilaukset.Find(id);
            db.Tilaukset.Remove(tilaukset);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Tilaukset/Delete/5
        public ActionResult _ModalDelete(int? id)
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
            Tilaukset tilaukset = db.Tilaukset.Find(id);
            if (tilaukset == null)
            {
                return HttpNotFound();
            }
            return PartialView("_ModalDelete", tilaukset);
        }

        // POST: Tilaukset/Delete/5
        [HttpPost, ActionName("_ModalDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult _ModalDeleteConfirmed(int id)
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
            Tilaukset tilaukset = db.Tilaukset.Find(id);
            db.Tilaukset.Remove(tilaukset);
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
        public ActionResult OrderSummary()
        {
            var orderSummary = from t in db.Tilaukset
                               join tr in db.Tilausrivit on t.TilausID equals tr.TilausID
                               join p in db.Tuotteet on tr.TuoteID equals p.TuoteID
                               join c in db.Tuoteryhmat on p.TuoteryhmaID equals c.TuoteryhmaID
                               //where-lause
                               //orderby-lause
                               select new OrderSummaryData
                               {
                                    TilausID = t.TilausID,
                                    AsiakasID = (int)t.AsiakasID,
                                    Toimitusosoite = t.Toimitusosoite,
                                    Postinumero = (int)t.Postinumero,
                                    Tilauspvm = (DateTime)t.Tilauspvm,
                                    Toimituspvm = (DateTime)t.Toimituspvm,
                                    TilausriviID = tr.TilausriviID,
                                    TuoteID = (int)tr.TuoteID,
                                    Maara = (int)tr.Maara,
                                    Nimi = p.Nimi,
                                    varMaara = (int)p.varMaara,
                                    Hinta = (float)p.Hinta,
                                    TuoteryhmaID = c.TuoteryhmaID,
                                    Tuoteryhmanimi = c.Tuoteryhmanimi,
                                    Kuvaus = c.Kuvaus
                               };

            return View(orderSummary);
        }
        public ActionResult TilausOtsikot(int? page, int? pagesize, string EtsiNimella1)
        {
            int pageSize = (pagesize ?? 4);
            int pageNumber = (page ?? 1);
            if (Session["Kayttajatunnus"] == null)
            {
                ViewBag.LoggedStatus = "Ei kirjautunut";
                return RedirectToAction("Kirjautuminen", "Home");
            }
            else
            {
                ViewBag.LoggedStatus = "Kirjautunut";
            }
            var tilaukset = db.Tilaukset.Include(t => t.Asiakkaat)
                .OrderBy(t => t.TilausID)
                .ToList();

            //if (!String.IsNullOrEmpty(EtsiNimella1)) ei onnistu laittaa if-lausetta, OrderBy sotkee??
            //{
            //    tilaukset = tilaukset.Where(t => t.Asiakkaat.Etunimi.Contains(EtsiNimella1));
            //}

            return View(tilaukset.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult _TilausRivit(int? tilausid)
        {
            var orderRowsList = from tr in db.Tilausrivit 
                               join p in db.Tuotteet on tr.TuoteID equals p.TuoteID
                               join c in db.Tuoteryhmat on p.TuoteryhmaID equals c.TuoteryhmaID
                               where tr.TilausID == tilausid
                               //orderby-lause

                               select new OrderRows
                               {
                                   TilausID = tr.TilausID,
                                   TilausriviID = tr.TilausriviID,
                                   TuoteID = (int)tr.TuoteID,
                                   Maara = (int)tr.Maara,
                                   Nimi = p.Nimi,
                                   varMaara = (int)p.varMaara,
                                   Hinta = (float)p.Hinta,
                                   TuoteryhmaID = c.TuoteryhmaID,
                                   Tuoteryhmanimi = c.Tuoteryhmanimi,
                                   Kuvaus = c.Kuvaus
                               };

            return PartialView(orderRowsList);
        }
    }
}
