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
    public class AsiakkaatController : Controller
    {
        private TacosDBEntities3 db = new TacosDBEntities3();

        // GET: Asiakkaat
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
            return View(db.Asiakkaat.ToList());
        }

        // GET: Asiakkaat/Details/5
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
            Asiakkaat asiakkaat = db.Asiakkaat.Find(id);
            if (asiakkaat == null)
            {
                return HttpNotFound();
            }
            return View(asiakkaat);
        }

        // GET: Asiakkaat/Details/5
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
            Asiakkaat asiakkaat = db.Asiakkaat.Find(id);
            if (asiakkaat == null)
            {
                return HttpNotFound();
            }
            return PartialView("_ModalDetails", asiakkaat);
        }

        // GET: Asiakkaat/Create
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
            return View();
        }

        // POST: Asiakkaat/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AsiakasID,Etunimi,Sukunimi,Osoite,Postinumero,Postitoimipaikka")] Asiakkaat asiakkaat)
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
                db.Asiakkaat.Add(asiakkaat);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(asiakkaat);
        }

        // GET: Asiakkaat/Create
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
            return PartialView();
        }



        // GET: Asiakkaat/Edit/5
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
            Asiakkaat asiakkaat = db.Asiakkaat.Find(id);
            if (asiakkaat == null)
            {
                return HttpNotFound();
            }
            return View(asiakkaat);
        }

        // POST: Asiakkaat/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AsiakasID,Etunimi,Sukunimi,Osoite,Postinumero,Postitoimipaikka")] Asiakkaat asiakkaat)
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
                db.Entry(asiakkaat).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(asiakkaat);
        }

        // GET: Asiakkaat/Edit/5
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
            Asiakkaat asiakkaat = db.Asiakkaat.Find(id);
            if (asiakkaat == null)
            {
                return HttpNotFound();
            }
            return PartialView("_ModalEdit", asiakkaat);
        }

        // POST: Asiakkaat/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult _ModalEdit([Bind(Include = "AsiakasID,Etunimi,Sukunimi,Osoite,Postinumero,Postitoimipaikka")] Asiakkaat asiakkaat)
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
                db.Entry(asiakkaat).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return PartialView("_ModalEdit", asiakkaat);
        }

        // GET: Asiakkaat/Delete/5
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
            Asiakkaat asiakkaat = db.Asiakkaat.Find(id);
            if (asiakkaat == null)
            {
                return HttpNotFound();
            }
            return View(asiakkaat);
        }

        // POST: Asiakkaat/Delete/5
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
            Asiakkaat asiakkaat = db.Asiakkaat.Find(id);
            db.Asiakkaat.Remove(asiakkaat);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Asiakkaat/Delete/5
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
            Asiakkaat asiakkaat = db.Asiakkaat.Find(id);
            if (asiakkaat == null)
            {
                return HttpNotFound();
            }
            return PartialView("_ModalDelete", asiakkaat);
        }

        // POST: Asiakkaat/Delete/5
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
            Asiakkaat asiakkaat = db.Asiakkaat.Find(id);
            db.Asiakkaat.Remove(asiakkaat);
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
