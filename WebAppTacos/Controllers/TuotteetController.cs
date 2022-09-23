using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebAppTacos.Models;
using PagedList;
using WebAppTacos.ViewModels;

namespace WebAppTacos.Controllers
{
    public class TuotteetController : Controller
    {
        public TacosDBEntities3 db = new TacosDBEntities3();

        // GET: Tuotteet
        public ActionResult Index(string sortOrder, string currentFilter1, string TuoteRyhma, string searchString1, int? page, int? pagesize) //lisätään parametrit, viimeiset kaksi sivutusparametrejä
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

            ViewBag.CurrentSort = sortOrder;
            ViewBag.ProductNameSortParm = String.IsNullOrEmpty(sortOrder) ? "productname_desc" : "";
            ViewBag.UnitPriceSortParm = sortOrder == "unitprice" ? "unitprice_desc" : "unitprice";

            if (searchString1 !=null)
            {
                page = 1;
            }
            else
            {
                searchString1 = currentFilter1;
            }

            ViewBag.currentFilter1 = searchString1;

            var tuotteet = from t in db.Tuotteet //muokataan tuotteiden listausta, jos hakukenttä ei ole tyhjä haetaan listaukseen tuotteet joihin parametri searchString 1 sisältyy
                           select t;
            if (!String.IsNullOrEmpty(searchString1))
            {
                tuotteet = tuotteet.Where(p => p.Nimi.Contains(searchString1));
            }
            if (!String.IsNullOrEmpty(TuoteRyhma) && (TuoteRyhma != "0"))
            {
                int para = int.Parse(TuoteRyhma);
                tuotteet = tuotteet.Where(t => t.TuoteryhmaID == para);
            }

            if (!String.IsNullOrEmpty(searchString1)) //jos hakufiltteri on käytössä (ei tyhjä), käytetään sitä ja lisäksi lajitellaan tulokset
            {
                switch (sortOrder)
                {
                    case "productname_desc":
                        tuotteet = tuotteet.Where(t => t.Nimi.Contains(searchString1)).OrderByDescending(t => t.Nimi);
                        break;
                    case "unitprice":
                        tuotteet = tuotteet.Where(t => t.Nimi.Contains(searchString1)).OrderBy(t => t.Hinta);
                        break;
                    case "unitprice_desc":
                        tuotteet = tuotteet.Where(t => t.Nimi.Contains(searchString1)).OrderByDescending(t => t.Hinta);
                        break;
                    default:
                        tuotteet = tuotteet.Where(t => t.Nimi.Contains(searchString1)).OrderBy(t => t.Nimi);
                        break;
                }
            }
            else //jos hakufoltteri EI OLE käytössä -> lajitellaan koko tulosjoukko ilman suodatuksia
            {
                switch (sortOrder)
                {
                    case "productname_desc":
                        tuotteet = tuotteet.OrderByDescending(t => t.Nimi);
                        break;
                    case "unitprice":
                        tuotteet = tuotteet.OrderBy(t => t.Hinta);
                        break;
                    case "unitprice_desc":
                        tuotteet = tuotteet.OrderByDescending(t => t.Hinta);
                        break;
                    default:
                        tuotteet = tuotteet.OrderBy(t => t.Nimi);
                        break;
                }
            };
            

            List<Tuoteryhmat> lstTuoteryhmat = new List<Tuoteryhmat>();

            var ryhmaLista = from ryh in db.Tuoteryhmat
                             select ryh;

            Tuoteryhmat tyhjaTuoteryhma = new Tuoteryhmat();
            tyhjaTuoteryhma.TuoteryhmaID = 0;
            tyhjaTuoteryhma.Tuoteryhmanimi = "";
            tyhjaTuoteryhma.TuoteryhmaIDtuoteryhmaNimi = "";
            lstTuoteryhmat.Add(tyhjaTuoteryhma);

            foreach (Tuoteryhmat tuoteryhma in ryhmaLista)
            {
                Tuoteryhmat yksiTuoteryhma = new Tuoteryhmat();
                yksiTuoteryhma.TuoteryhmaID = tuoteryhma.TuoteryhmaID;
                yksiTuoteryhma.Tuoteryhmanimi = tuoteryhma.Tuoteryhmanimi;
                yksiTuoteryhma.TuoteryhmaIDtuoteryhmaNimi = tuoteryhma.TuoteryhmaID.ToString() + " " + tuoteryhma.Tuoteryhmanimi;
                lstTuoteryhmat.Add(yksiTuoteryhma);
            }
            ViewBag.TuoteryhmaID = new SelectList(lstTuoteryhmat, "TuoteryhmaID", "TuoteryhmaIDtuoteryhmaNimi", TuoteRyhma);

            int pageSize = (pagesize ?? 6); //palauttaa sivukoon tai jos page on null niin 6 per sivu
            int pageNumber = (page ?? 1); //palauttaa sivunumeron tai jos page null, palauttaa nron yksi
            return View(tuotteet.ToPagedList(pageNumber, pageSize));
            //var tuotteet = db.Tuotteet.Include(t => t.Tuoteryhmat); //tehdään eräänlainen join kahden taulun välille -incluudataan Tuotteet -tauluun Tuoteryhmat -taulu
            //return View(tuotteet.ToList());
        }

        // GET: Tuotteet/Details/5
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
            Tuotteet tuotteet = db.Tuotteet.Find(id);
            if (tuotteet == null)
            {
                return HttpNotFound();
            }
            return View(tuotteet);
        }

        // GET: Tuotteet/Details/5
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
            Tuotteet tuotteet = db.Tuotteet.Find(id);
            if (tuotteet == null)
            {
                return HttpNotFound();
            }
            return PartialView("_ModalDetails", tuotteet);
        }

        // GET: Tuotteet/Create
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
            ViewBag.TuoteryhmaID = new SelectList(db.Tuoteryhmat, "TuoteryhmaID", "Tuoteryhmanimi");
            return View();
        }

        // GET: Tuotteet/Create
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
            ViewBag.TuoteryhmaID = new SelectList(db.Tuoteryhmat, "TuoteryhmaID", "Tuoteryhmanimi");
            return PartialView();
        }

        // POST: Tuotteet/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TuoteID,Nimi,varMaara,Hinta,TuoteryhmaID")] Tuotteet tuotteet)
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
                db.Tuotteet.Add(tuotteet);
                db.SaveChanges();
                ViewBag.TuoteryhmaID = new SelectList(db.Tuoteryhmat, "TuoteryhmaID", "Tuoteryhmanimi");
                return RedirectToAction("Index");
            }
            return View(tuotteet);
        }

        // GET: Tuotteet/Edit/5
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
            Tuotteet tuotteet = db.Tuotteet.Find(id);
            if (tuotteet == null)
            {
                return HttpNotFound();
            }
            ViewBag.TuoteryhmaID = new SelectList(db.Tuoteryhmat, "TuoteryhmaID", "Tuoteryhmanimi", tuotteet.TuoteryhmaID);
            return View(tuotteet);
        }

        // POST: Tuotteet/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TuoteID,Nimi,varMaara,Hinta,TuoteryhmaID")] Tuotteet tuotteet)
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
                db.Entry(tuotteet).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TuoteryhmaID = new SelectList(db.Tuoteryhmat, "TuoteryhmaID", "Tuoteryhmanimi", tuotteet.TuoteryhmaID);
            //edellä määritellään uuden valintalistan jälkeen käytettävä taulu, avaintieto, näytettävä tieto sekä aiemmin valittu arvo)
            return View(tuotteet);
        }

        // GET: Tuotteet/Edit/5
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
            Tuotteet tuotteet = db.Tuotteet.Find(id);
            if (tuotteet == null)
            {
                return HttpNotFound();
            }
            ViewBag.TuoteryhmaID = new SelectList(db.Tuoteryhmat, "TuoteryhmaID", "Tuoteryhmanimi", tuotteet.TuoteryhmaID);
            return PartialView("_ModalEdit", tuotteet);
        }

        // POST: Tuotteet/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult _ModalEdit([Bind(Include = "TuoteID,Nimi,varMaara,Hinta,TuoteryhmaID")] Tuotteet tuotteet)
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
                db.Entry(tuotteet).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TuoteryhmaID = new SelectList(db.Tuoteryhmat, "TuoteryhmaID", "Tuoteryhmanimi", tuotteet.TuoteryhmaID);
            //edellä määritellään uuden valintalistan jälkeen käytettävä taulu, avaintieto, näytettävä tieto sekä aiemmin valittu arvo)
            return PartialView("_ModalEdit", tuotteet);
        }

        // GET: Tuotteet/Delete/5
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
            Tuotteet tuotteet = db.Tuotteet.Find(id);
            if (tuotteet == null)
            {
                return HttpNotFound();
            }
            return View(tuotteet);
        }

        // POST: Tuotteet/Delete/5
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
            Tuotteet tuotteet = db.Tuotteet.Find(id);
            db.Tuotteet.Remove(tuotteet);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        // GET: Tuotteet/Delete/5
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
            Tuotteet tuotteet = db.Tuotteet.Find(id);
            if (tuotteet == null)
            {
                return HttpNotFound();
            }
            return PartialView("_ModalDelete", tuotteet);
        }
        // POST: Tuotteet/Delete/5
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
            Tuotteet tuotteet = db.Tuotteet.Find(id);
            db.Tuotteet.Remove(tuotteet);
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
