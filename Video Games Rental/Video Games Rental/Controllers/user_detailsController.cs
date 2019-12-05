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
    public class user_detailsController : Controller
    {
        private VGR_DBEntities db = new VGR_DBEntities();

        // GET: user_details
        public ActionResult Index()
        {
            return View(db.user_details.ToList());
        }

        // GET: user_details/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            user_details user_details = db.user_details.Find(id);
            if (user_details == null)
            {
                return HttpNotFound();
            }
            return View(user_details);
        }

        // GET: user_details/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: user_details/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "user_details_id,name,surname,address_line1,address_line2,postal_code")] user_details user_details)
        {
            if (ModelState.IsValid)
            {
                db.user_details.Add(user_details);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user_details);
        }

        // GET: user_details/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            user_details user_details = db.user_details.Find(id);
            if (user_details == null)
            {
                return HttpNotFound();
            }
            return View(user_details);
        }

        // POST: user_details/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "user_details_id,name,surname,address_line1,address_line2,postal_code")] user_details user_details)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user_details).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user_details);
        }

        // GET: user_details/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            user_details user_details = db.user_details.Find(id);
            if (user_details == null)
            {
                return HttpNotFound();
            }
            return View(user_details);
        }

        // POST: user_details/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            user_details user_details = db.user_details.Find(id);
            db.user_details.Remove(user_details);
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
