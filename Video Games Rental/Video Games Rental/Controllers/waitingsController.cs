using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using PagedList;
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
    public class waitingsController : Controller
    {
        private VGR_DBEntities db = new VGR_DBEntities();

        // GET: waitings
        public ActionResult Index()
        {
            var waitings = db.waitings.Include(w => w.condition).Include(w => w.customer).Include(w => w.genre).Include(w => w.language).Include(w => w.platform).Include(w => w.status);
            return View(waitings.ToList());
        }

        // GET: waitings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            waiting waiting = db.waitings.Find(id);
            if (waiting == null)
            {
                return HttpNotFound();
            }
            return View(waiting);
        }

        public ActionResult WaitingList(int? page)
        {
            var pageNumber = page ?? 1;
            var PageSize = 5;           
            var orderList = db.waitings.OrderByDescending(x => x.waiting_id).ToPagedList(pageNumber, PageSize);
            return View(orderList);
        }
        // GET: waitings/Create
        public ActionResult Create()
        {
            ViewBag.condiiton_id = new SelectList(db.conditions, "condition_id", "condition1");
            ViewBag.customer_id = new SelectList(db.customers, "customer_id", "AspNetUsers_id");
            ViewBag.genre_id = new SelectList(db.genres, "genre_id", "genre1");
            ViewBag.langugae_id = new SelectList(db.languages, "language_id", "language1");
            ViewBag.platform_id = new SelectList(db.platforms, "platform_id", "platform1");
            ViewBag.status_id = new SelectList(db.status, "status_id", "status1");
            return View();
        }

        // POST: waitings/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "waiting_id,customer_id,platform_id,langugae_id,genre_id,condiiton_id,status_id,title,price")] waiting waiting)
        {
            if (ModelState.IsValid)
            {
                db.waitings.Add(waiting);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.condiiton_id = new SelectList(db.conditions, "condition_id", "condition1", waiting.condiiton_id);
            ViewBag.customer_id = new SelectList(db.customers, "customer_id", "AspNetUsers_id", waiting.customer_id);
            ViewBag.genre_id = new SelectList(db.genres, "genre_id", "genre1", waiting.genre_id);
            ViewBag.langugae_id = new SelectList(db.languages, "language_id", "language1", waiting.langugae_id);
            ViewBag.platform_id = new SelectList(db.platforms, "platform_id", "platform1", waiting.platform_id);
            ViewBag.status_id = new SelectList(db.status, "status_id", "status1", waiting.status_id);
            return View(waiting);
        }

        // GET: waitings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            waiting waiting = db.waitings.Find(id);
            if (waiting == null)
            {
                return HttpNotFound();
            }
            ViewBag.condiiton_id = new SelectList(db.conditions, "condition_id", "condition1", waiting.condiiton_id);
            ViewBag.customer_id = new SelectList(db.customers, "customer_id", "AspNetUsers_id", waiting.customer_id);
            ViewBag.genre_id = new SelectList(db.genres, "genre_id", "genre1", waiting.genre_id);
            ViewBag.langugae_id = new SelectList(db.languages, "language_id", "language1", waiting.langugae_id);
            ViewBag.platform_id = new SelectList(db.platforms, "platform_id", "platform1", waiting.platform_id);
            ViewBag.status_id = new SelectList(db.status, "status_id", "status1", waiting.status_id);
            return View(waiting);
        }

        // POST: waitings/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "waiting_id,customer_id,platform_id,langugae_id,genre_id,condiiton_id,status_id,title,price")] waiting waiting)
        {
            if (ModelState.IsValid)
            {
                db.Entry(waiting).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.condiiton_id = new SelectList(db.conditions, "condition_id", "condition1", waiting.condiiton_id);
            ViewBag.customer_id = new SelectList(db.customers, "customer_id", "AspNetUsers_id", waiting.customer_id);
            ViewBag.genre_id = new SelectList(db.genres, "genre_id", "genre1", waiting.genre_id);
            ViewBag.langugae_id = new SelectList(db.languages, "language_id", "language1", waiting.langugae_id);
            ViewBag.platform_id = new SelectList(db.platforms, "platform_id", "platform1", waiting.platform_id);
            ViewBag.status_id = new SelectList(db.status, "status_id", "status1", waiting.status_id);
            return View(waiting);
        }

        // GET: waitings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            waiting waiting = db.waitings.Find(id);
            if (waiting == null)
            {
                return HttpNotFound();
            }
            return View(waiting);
        }

        // POST: waitings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            waiting waiting = db.waitings.Find(id);
            db.waitings.Remove(waiting);
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
