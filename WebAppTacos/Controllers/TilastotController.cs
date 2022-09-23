using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppTacos.Models;
using WebAppTacos.ViewModels;

namespace WebAppTacos.Controllers
{
    public class TilastotController : Controller
    {
        private TacosDBEntities3 db = new TacosDBEntities3();
        // GET: Tilastot
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult PaivaMyynnit()
        {
            string viikonPaivaLista;
            string tuoteMyyntiLista;

            List<PaivaMyyntiLuokka> PaivaMyyntiLista = new List<PaivaMyyntiLuokka>();

            var paivaMyyntiData = from pm in db.TilausMaarat
                                  select pm;

            foreach (TilausMaarat tuotteidenpaivamyynnit in paivaMyyntiData)
            {
                PaivaMyyntiLuokka YksiMyyntiRivi = new PaivaMyyntiLuokka();
                YksiMyyntiRivi.ViikonPaiva = tuotteidenpaivamyynnit.Weekday;
                YksiMyyntiRivi.TuoteMyynti = (int)tuotteidenpaivamyynnit.Total;
                PaivaMyyntiLista.Add(YksiMyyntiRivi);
            }

            viikonPaivaLista = "'" + string.Join("','", PaivaMyyntiLista.Select(n => n.ViikonPaiva).ToList()) + "'";
            tuoteMyyntiLista = string.Join(",", PaivaMyyntiLista.Select(n => n.TuoteMyynti).ToList());

            ViewBag.viikonPaiva = viikonPaivaLista;
            ViewBag.tuoteMyynti = tuoteMyyntiLista;

            return View();
        }
        public ActionResult Top10Myynnit()
        {
            string topNimiLista;
            string topMyyntiLista;

            List<TopMyyntiLuokka> TopMyyntiLista = new List<TopMyyntiLuokka>();

            var topMyyntiData = from tm in db.Top10Tuotteet
                                  select tm;

            foreach (Top10Tuotteet tuotteidentopmyynnit in topMyyntiData)
            {
                TopMyyntiLuokka YksiTopRivi = new TopMyyntiLuokka();
                YksiTopRivi.TopNimi = tuotteidentopmyynnit.Nimi;
                YksiTopRivi.TopMyynti = (int)tuotteidentopmyynnit.Myynnit;
                TopMyyntiLista.Add(YksiTopRivi);
            }

            topNimiLista = "'" + string.Join("','", TopMyyntiLista.Select(n => n.TopNimi).ToList()) + "'";
            topMyyntiLista = string.Join(",", TopMyyntiLista.Select(n => n.TopMyynti).ToList());

            ViewBag.topNimi = topNimiLista;
            ViewBag.topMyynti = topMyyntiLista;

            return View();
        }
    }
}