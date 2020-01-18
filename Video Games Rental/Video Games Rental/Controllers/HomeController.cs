using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Video_Games_Rental.Models;


namespace video_games_rental.Controllers
{
    public class HomeController : Controller
    {
        private VGR_DBEntities db = new VGR_DBEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Shop()
        {

            return View();
        }

        public ActionResult AddProduct()
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

        //public ActionResult GameRegister(FormCollection frc)
        //{
        //    game game = new game()
        //    {
        //        title = frc["gameTitle"],
        //        genre_id = frc[""],
        //        language = frc["gameLanguage"],
        //        platform = frc["gamePlatform"],
        //        condition = frc["gameCondition"],
        //        price = frc["gamePrice"],               
        //    };

        //    db.games.Add(game);
        //    db.SaveChanges();

        //    return View("RegisterSuccess");
        //}
    }
}