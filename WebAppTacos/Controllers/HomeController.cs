using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppTacos.Models;

namespace WebAppTacos.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (Session["Kayttajatunnus"] == null)
            {
                ViewBag.LoggedStatus = "Ei kirjautunut";
            }
            else
            {
                ViewBag.LoggedStatus = "Kirjautunut";
            }
            ViewBag.LoginError = 0;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            if (Session["Kayttajatunnus"] == null)
            {
                ViewBag.LoggedStatus = "Ei kirjautunut";
            }
            else
            {
                ViewBag.LoggedStatus = "Kirjautunut";
            }
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            if (Session["Kayttajatunnus"] == null)
            {
                ViewBag.LoggedStatus = "Ei kirjautunut";
            }
            else
            {
                ViewBag.LoggedStatus = "Kirjautunut";
            }
            return View();
        }
        public ActionResult Map()
        {
            ViewBag.Message = "Sijaintimme kartalla";
            if (Session["Kayttajatunnus"] == null)
            {
                ViewBag.LoggedStatus = "Ei kirjautunut";
            }
            else
            {
                ViewBag.LoggedStatus = "Kirjautunut";
            }
            return View();
        }
        public ActionResult Kirjautuminen()
        {
            if (Session["UserName"] == null)
            {
                ViewBag.LoggedStatus = "Ei kirjautunut";
            }
            else ViewBag.LoggedStatus = "Kirjautunut";
            return View();
        }
        [HttpPost]
        public ActionResult Authorize(Kirjautuminen LoginModel)
        {
            TacosDBEntities3 db = new TacosDBEntities3();
            var LoggedUser = db.Kirjautuminen.SingleOrDefault(x => x.Kayttajatunnus == LoginModel.Kayttajatunnus && x.Salasana == LoginModel.Salasana);
            if (LoggedUser != null)
            {
                ViewBag.LoginMessage = "Kirjautuminen onnistui";
                ViewBag.LoggedStatus = "Kirjautunut";
                ViewBag.LoginError = 0;
                Session["Kayttajatunnus"] = LoggedUser.Kayttajatunnus;
                Session["LoginId"] = LoggedUser.KirjautumisID;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.LoginMessage = "Kirjautuminen epäonnistui";
                ViewBag.LoggedStatus = "Ei kirjautunut";
                ViewBag.LoginError = 1;
                LoginModel.LoginErrorMessage = "Tuntematon käyttäjätunnus tai salasana!";
                return View("Index", LoginModel);
            }
        }
        public ActionResult LogOut()
        {
            Session.Abandon();
            ViewBag.LoggedStatus = "Ei kirjautunut";
            return RedirectToAction("Index", "Home"); 
        }
    }
}