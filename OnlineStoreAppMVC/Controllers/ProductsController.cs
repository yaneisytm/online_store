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
            return View(manager.GetAll());
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
            var user = manager.GetCurrentUser(User.Identity.Name);
            int id_ = int.Parse(id);
            int quantity_ = int.Parse(quantity);

            Product product = manager.GetById(id_);
            product.Stock = product.Stock - quantity_;
            manager.Edit(product);

            var old_ol = user.ShoppingCart.OrderLines.Where(o => o.Product.Id == product.Id).ToList();
            var orderline = new OrderLine();
            if (old_ol.Count > 0)
            {
                orderline = old_ol.First();
                orderline.Quantity += quantity_;
                orderline.Price = orderline.Quantity * orderline.Product.Price;

            }
            else {
                orderline.Product = product;
                orderline.Quantity = quantity_;
                orderline.Price = orderline.Quantity * orderline.Product.Price;
                user.ShoppingCart.OrderLines.Add(orderline);
            }

            manager.Update(user);
            return RedirectToAction("Index");
        }
    }
}
