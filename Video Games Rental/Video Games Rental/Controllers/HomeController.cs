using Microsoft.AspNet.Identity;
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
            var gameList = db.games.OrderByDescending(x => x.game_id).ToList();
            //int gameID = db.games.Select(x => x.game_id).Last();
            //Session["gameID"] = gameID;
            return View(gameList);
        }

        public ActionResult AddProduct()
        {
            ViewBag.condiiton_id = new SelectList(db.conditions, "condition_id", "condition1");
            ViewBag.genre_id = new SelectList(db.genres, "genre_id", "genre1");
            ViewBag.langugae_id = new SelectList(db.languages, "language_id", "language1");
            ViewBag.platform_id = new SelectList(db.platforms, "platform_id", "platform1");            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddProduct([Bind(Include = "waiting_id,customer_id,platform_id,langugae_id,genre_id,condiiton_id,title,price")] waiting waiting)
        {
            if (ModelState.IsValid)
            {
                string aspnetuserID = User.Identity.GetUserId();
                List<customer> custList = db.customers.Where(x => x.AspNetUsers_id == aspnetuserID).ToList();                                

                if (custList.Count() > 0)
                {
                    waiting.status_id = 1;
                    int cust = custList.First().customer_id;
                    waiting.customer_id = cust;                    
                }
                db.waitings.Add(waiting);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.condiiton_id = new SelectList(db.conditions, "condition_id", "condition1", waiting.condiiton_id);           
            ViewBag.genre_id = new SelectList(db.genres, "genre_id", "genre1", waiting.genre_id);
            ViewBag.langugae_id = new SelectList(db.languages, "language_id", "language1", waiting.langugae_id);
            ViewBag.platform_id = new SelectList(db.platforms, "platform_id", "platform1", waiting.platform_id);
            return View(waiting);
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Contact([Bind(Include = "request_id,name,e_mail,request1,phonenumber")] request request)
        {
            if (ModelState.IsValid)
            {
                db.requests.Add(request);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(request);
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