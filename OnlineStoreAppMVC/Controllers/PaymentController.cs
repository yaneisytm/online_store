using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineStoreAppMVC.Models;
using OnlineStoreApplication;
using OnlineStoreDAL;
using OnlineStoreCORE;

namespace OnlineStoreAppMVC.Controllers
{
    public class PaymentController : Controller
    {
        private ShoppingCartManager manager = new ShoppingCartManager(new ApplicationDbContext());

        // GET: Payment
        public ActionResult Index(int? amount)
        {
            int real_amount = amount == null ? 0 : (int) amount;
            //ViewBag.amount = amount;
            //setCardDefaults();
            var card = new CardViewModel {
                Amount = real_amount
            };
            return View(card);
        }

        // POST: Payment/Index
        [HttpPost, ActionName("Index")]
        [ValidateAntiForgeryToken]
        public ActionResult CreateOrder(CardViewModel card, string address)
        {
            if (!ModelState.IsValid)
            {
                return View(card);
            }
            DateTime dateTime = DateTime.UtcNow;
            var user = manager.GetCurrentUser(User.Identity.Name);
            var orderLines = manager.GetOrderLinesByUser(User.Identity.Name);
            Order order = new Order
            {
                OrderLines = orderLines,
                DeliveryAddress = address,
                Date = dateTime,
                Status = "Created",
            };
            user.Orders.Add(order);
            manager.Update(user);
            //manager.AddOrder(order);
            manager.EmptyShoppingCart(User.Identity.Name);



            return RedirectToAction("Success", new { address = address});
        }

        // Get: Payment/Success
        public ActionResult Success(string address)
        {
            ViewBag.address = address;
            return View();
        }
    }
 
}