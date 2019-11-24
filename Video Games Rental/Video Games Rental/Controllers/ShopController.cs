using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Video_Games_Rental.Models;
using PagedList;

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

        public PartialViewResult GameListPartial(int? page)
        {           
            var pageNumber = page ?? 1;
            var pageSize = 9;
            var gameList = db.games.OrderByDescending(x => x.game_id).ToPagedList(pageNumber, pageSize);
            return PartialView(gameList);                    
        }

        public ActionResult Details(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            game product = db.games.Find(id);
            if(product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

    }
}