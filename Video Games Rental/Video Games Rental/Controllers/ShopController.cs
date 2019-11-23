using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Video_Games_Rental.Models;

namespace Video_Games_Rental.Controllers
{
    public class ShopController : Controller
    {
        private VGR_DBEntities db = new VGR_DBEntities();
        // GET: Shop
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult GameListPartial()
        {
            var gameList = db.games.OrderByDescending(x => x.game_id).ToList();           
            return PartialView(gameList);                    
        }
    }
}