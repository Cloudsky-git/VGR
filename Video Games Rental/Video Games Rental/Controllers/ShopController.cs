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

        public PartialViewResult GameListPartial(int? page,int? platform, int? language, int? condition)
        {           
            var pageNumber = page ?? 1;
            var pageSize = 9;
           
            if (platform != null)
            {
                ViewBag.platform = platform;
                var gameList = db.games
                    .OrderByDescending(x => x.game_id)
                    .Where(x=> x.platform_id == platform)
                    .ToPagedList(pageNumber, pageSize);
                    
                return PartialView(gameList);
            }

            else if (language != null)
            {
                ViewBag.language = platform;
                var gameList = db.games
                    .OrderByDescending(x => x.game_id)
                    .Where(x => x.language_id == language)
                    .ToPagedList(pageNumber, pageSize);

                return PartialView(gameList);
            }

            else if (condition != null)
            {
                ViewBag.condition = platform;
                var gameList = db.games
                    .OrderByDescending(x => x.game_id)
                    .Where(x => x.condition_id == condition)
                    .ToPagedList(pageNumber, pageSize);

                return PartialView(gameList);
            }

            else
            {
                var gameList = db.games.OrderByDescending(x => x.game_id).ToPagedList(pageNumber, pageSize);
                return PartialView(gameList);
            }   
        }

        public PartialViewResult PlatformListPartial()
        {
            var platformList = db.platforms.OrderByDescending(x => x.platform1).ToList();
            return PartialView(platformList);
        }

        public PartialViewResult LanguageListPartial()
        {
            var languageList = db.languages.OrderByDescending(x => x.language1).ToList();
            return PartialView(languageList);
        }


        public PartialViewResult ConditionListPartial()
        {
            var languageList = db.conditions.OrderByDescending(x => x.condition1).ToList();
            return PartialView(languageList);
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