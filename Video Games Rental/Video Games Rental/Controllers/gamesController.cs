using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Video_Games_Rental.Models;

namespace Video_Games_Rental.Controllers
{
    public class gamesController : Controller
    {
        private VGR_DBEntities db = new VGR_DBEntities();

        // GET: games
        public ActionResult Index()
        {                       
            var games = db.games.Include(g => g.condition).Include(g => g.genre).Include(g => g.language).Include(g => g.platform);
            return View(games.ToList());            
        }

        // GET: games/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            game game = db.games.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            return View(game);
        }

        // GET: games/Create
        public ActionResult Create()
        {
            ViewBag.condition_id = new SelectList(db.conditions, "condition_id", "condition1");
            ViewBag.genre_id = new SelectList(db.genres, "genre_id", "genre1");
            ViewBag.language_id = new SelectList(db.languages, "language_id", "language1");
            ViewBag.platform_id = new SelectList(db.platforms, "platform_id", "platform1");
            return View();
        }

        // POST: games/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "game_id,platform_id,language_id,genre_id,condition_id,title,amount,price,image,description")] game game)
        {
            if (ModelState.IsValid)
            {
                db.games.Add(game);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.condition_id = new SelectList(db.conditions, "condition_id", "condition1", game.condition_id);
            ViewBag.genre_id = new SelectList(db.genres, "genre_id", "genre1", game.genre_id);
            ViewBag.language_id = new SelectList(db.languages, "language_id", "language1", game.language_id);
            ViewBag.platform_id = new SelectList(db.platforms, "platform_id", "platform1", game.platform_id);
            return View(game);
        }

        // GET: games/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            game game = db.games.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            ViewBag.condition_id = new SelectList(db.conditions, "condition_id", "condition1", game.condition_id);
            ViewBag.genre_id = new SelectList(db.genres, "genre_id", "genre1", game.genre_id);
            ViewBag.language_id = new SelectList(db.languages, "language_id", "language1", game.language_id);
            ViewBag.platform_id = new SelectList(db.platforms, "platform_id", "platform1", game.platform_id);
            return View(game);
        }

        // POST: games/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "game_id,platform_id,language_id,genre_id,condition_id,title,amount,price,image,description")] game game)
        {
            if (ModelState.IsValid)
            {
                db.Entry(game).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.condition_id = new SelectList(db.conditions, "condition_id", "condition1", game.condition_id);
            ViewBag.genre_id = new SelectList(db.genres, "genre_id", "genre1", game.genre_id);
            ViewBag.language_id = new SelectList(db.languages, "language_id", "language1", game.language_id);
            ViewBag.platform_id = new SelectList(db.platforms, "platform_id", "platform1", game.platform_id);
            return View(game);
        }

        // GET: games/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            game game = db.games.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            return View(game);
        }

        // POST: games/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            game game = db.games.Find(id);
            db.games.Remove(game);
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
