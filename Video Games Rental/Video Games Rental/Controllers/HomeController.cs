using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace video_games_rental.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Shop()
        {

            return View();
        }

        public ActionResult Reservation()
        {

            return View();
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

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        public ActionResult PS3()
        {

            return View();
        }

        public ActionResult PS4()
        {

            return View();
        }

        public ActionResult X360()
        {

            return View();
        }

        public ActionResult XO()
        {

            return View();
        }

        public ActionResult PC()
        {

            return View();
        }
    }
}