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
    public class order_detailController : Controller
    {
        private VGR_DBEntities db = new VGR_DBEntities();

        // GET: order_detail
        public ActionResult Index()
        {
            var order_detail = db.order_detail.Include(o => o.game).Include(o => o.order);
            return View(order_detail.ToList());
        }

        // GET: order_detail/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            order_detail order_detail = db.order_detail.Find(id);
            if (order_detail == null)
            {
                return HttpNotFound();
            }
            return View(order_detail);
        }
 
        // GET: order_detail/Create
        public ActionResult Create()
        {
            ViewBag.game_id = new SelectList(db.games, "game_id", "title");
            ViewBag.order_id = new SelectList(db.orders, "order_id", "order_id");
            return View();
        }

        // POST: order_detail/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "order_detail_id,order_id,game_id,amount")] order_detail order_detail)
        {
            if (ModelState.IsValid)
            {
                db.order_detail.Add(order_detail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.game_id = new SelectList(db.games, "game_id", "title", order_detail.game_id);
            ViewBag.order_id = new SelectList(db.orders, "order_id", "order_id", order_detail.order_id);
            return View(order_detail);
        }

        // GET: order_detail/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            order_detail order_detail = db.order_detail.Find(id);
            if (order_detail == null)
            {
                return HttpNotFound();
            }
            ViewBag.game_id = new SelectList(db.games, "game_id", "title", order_detail.game_id);
            ViewBag.order_id = new SelectList(db.orders, "order_id", "order_id", order_detail.order_id);
            return View(order_detail);
        }

        // POST: order_detail/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "order_detail_id,order_id,game_id,amount")] order_detail order_detail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order_detail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.game_id = new SelectList(db.games, "game_id", "title", order_detail.game_id);
            ViewBag.order_id = new SelectList(db.orders, "order_id", "order_id", order_detail.order_id);
            return View(order_detail);
        }

        // GET: order_detail/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            order_detail order_detail = db.order_detail.Find(id);
            if (order_detail == null)
            {
                return HttpNotFound();
            }
            return View(order_detail);
        }

        // POST: order_detail/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            order_detail order_detail = db.order_detail.Find(id);
            db.order_detail.Remove(order_detail);
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
