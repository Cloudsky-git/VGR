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
    public class ordersController : Controller
    {
        private VGR_DBEntities db = new VGR_DBEntities();

        // GET: orders
        public ActionResult Index()
        {
            var orders = db.orders.Include(o => o.order_type).Include(o => o.status).Include(o => o.user);
            return View(orders.ToList());
        }

        // GET: orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            order order = db.orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: orders/Create
        public ActionResult Create()
        {
            ViewBag.order_type_id = new SelectList(db.order_type, "order_type_id", "type");
            ViewBag.status_id = new SelectList(db.status, "status_id", "status1");
            ViewBag.user_id = new SelectList(db.users, "user_id", "username");
            return View();
        }

        // POST: orders/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "order_id,order_type_id,user_id,status_id,date,price")] order order)
        {
            if (ModelState.IsValid)
            {
                db.orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.order_type_id = new SelectList(db.order_type, "order_type_id", "type", order.order_type_id);
            ViewBag.status_id = new SelectList(db.status, "status_id", "status1", order.status_id);
            ViewBag.user_id = new SelectList(db.users, "user_id", "username", order.user_id);
            return View(order);
        }

        // GET: orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            order order = db.orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.order_type_id = new SelectList(db.order_type, "order_type_id", "type", order.order_type_id);
            ViewBag.status_id = new SelectList(db.status, "status_id", "status1", order.status_id);
            ViewBag.user_id = new SelectList(db.users, "user_id", "username", order.user_id);
            return View(order);
        }

        // POST: orders/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "order_id,order_type_id,user_id,status_id,date,price")] order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.order_type_id = new SelectList(db.order_type, "order_type_id", "type", order.order_type_id);
            ViewBag.status_id = new SelectList(db.status, "status_id", "status1", order.status_id);
            ViewBag.user_id = new SelectList(db.users, "user_id", "username", order.user_id);
            return View(order);
        }

        // GET: orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            order order = db.orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            order order = db.orders.Find(id);
            db.orders.Remove(order);
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
