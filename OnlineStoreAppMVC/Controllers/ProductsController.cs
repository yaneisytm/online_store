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


namespace OnlineStore.Controllers
{


    public class ProductsController : Controller
    {
        private ProductManager manager = new ProductManager(new ApplicationDbContext());
        

        // GET: Products
        public ActionResult Index()
        {
            if(User.Identity.IsAuthenticated && User.IsInRole("Admin"))
                return View(manager.GetAll());

            return View(manager.GetAllinStock());
        }

    
        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Price,Stock")] Product product, List<HttpPostedFileBase> files)
        {
            if (ModelState.IsValid)
            {
                manager.Insert(product, files, Server);
                return RedirectToAction("Index");
            }
            return View(product);
        }


     

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = manager.GetById((int)id);
                if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = manager.GetById((int)id);
            manager.Remove(product);
            return RedirectToAction("Index");
        }

        public ActionResult AddCart(string id, string quantity)
        {
            int id_ = int.Parse(id);
            int quantity_ = int.Parse(quantity);

            Product product = manager.GetById(id_);
            product.Stock = product.Stock - quantity_;
            manager.Edit(product);
            var orderLine = new OrderLine(product, quantity_);
            manager.AddInOrder(orderLine);
            return RedirectToAction("Index");
        }
    }
}
