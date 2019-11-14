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
    public class order_typeController : Controller
    {
        private VGR_DBEntities db = new VGR_DBEntities();

        // GET: order_type
        public ActionResult Index()
        {
            return View(db.order_type.ToList());
        }

        // GET: order_type/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            order_type order_type = db.order_type.Find(id);
            if (order_type == null)
            {
                return HttpNotFound();
            }
            return View(order_type);
        }

        // GET: order_type/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: order_type/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "order_type_id,type")] order_type order_type)
        {
            if (ModelState.IsValid)
            {
                db.order_type.Add(order_type);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(order_type);
        }

        // GET: order_type/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            order_type order_type = db.order_type.Find(id);
            if (order_type == null)
            {
                return HttpNotFound();
            }
            return View(order_type);
        }

        // POST: order_type/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "order_type_id,type")] order_type order_type)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order_type).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(order_type);
        }

        // GET: order_type/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            order_type order_type = db.order_type.Find(id);
            if (order_type == null)
            {
                return HttpNotFound();
            }
            return View(order_type);
        }

        // POST: order_type/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            order_type order_type = db.order_type.Find(id);
            db.order_type.Remove(order_type);
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
