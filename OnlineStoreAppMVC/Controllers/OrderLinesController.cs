using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OnlineStoreCORE;
using OnlineStoreDAL;
using OnlineStoreApplication;

namespace OnlineStoreAppMVC.Controllers
{
    public class OrderLinesController : Controller
    {
        private ShoppingCartManager manager = new ShoppingCartManager(new ApplicationDbContext());


        // GET: OrderLines
        public ActionResult Index()
        {
            return View(manager.GetrderLinesByUser(User.Identity.Name));
            ;
        }

      

        // GET: OrderLines/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OrderLines/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Price,Quantity")] OrderLine orderLine)
        {
            if (ModelState.IsValid)
            {
                manager.Add(orderLine);
                return RedirectToAction("Index");
            }

            return View(orderLine);
        }

        // GET: OrderLines/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderLine orderLine = manager.GetById((int)id);
            if (orderLine == null)
            {
                return HttpNotFound();
            }
            return View(orderLine);
        }

        // POST: OrderLines/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Quantity")] OrderLine orderLine)
        {
            if (ModelState.IsValid)
            {
                manager.Edit(orderLine);
                return RedirectToAction("Index");
            }
            return View(orderLine);
        }

        // GET: OrderLines/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderLine orderLine = manager.GetById((int)id);
            if (orderLine == null)
            {
                return HttpNotFound();
            }
            return View(orderLine);
        }

        // POST: OrderLines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var currentuser = manager.GetCurrentUser(User.Identity.Name);
            var shoppingCart = manager.Delete(id, currentuser);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                manager.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
