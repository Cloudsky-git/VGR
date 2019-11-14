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
    public class platformsController : Controller
    {
        private VGR_DBEntities db = new VGR_DBEntities();

        // GET: platforms
        public ActionResult Index()
        {
            return View(db.platforms.ToList());
        }

        // GET: platforms/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            platform platform = db.platforms.Find(id);
            if (platform == null)
            {
                return HttpNotFound();
            }
            return View(platform);
        }

        // GET: platforms/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: platforms/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "platform_id,platform1")] platform platform)
        {
            if (ModelState.IsValid)
            {
                db.platforms.Add(platform);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(platform);
        }

        // GET: platforms/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            platform platform = db.platforms.Find(id);
            if (platform == null)
            {
                return HttpNotFound();
            }
            return View(platform);
        }

        // POST: platforms/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "platform_id,platform1")] platform platform)
        {
            if (ModelState.IsValid)
            {
                db.Entry(platform).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(platform);
        }

        // GET: platforms/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            platform platform = db.platforms.Find(id);
            if (platform == null)
            {
                return HttpNotFound();
            }
            return View(platform);
        }

        // POST: platforms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            platform platform = db.platforms.Find(id);
            db.platforms.Remove(platform);
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
