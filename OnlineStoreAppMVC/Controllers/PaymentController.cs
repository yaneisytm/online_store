using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineStoreAppMVC.Models;

namespace OnlineStoreAppMVC.Controllers
{
    public class PaymentController : Controller
    {
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
        public ActionResult Index(CardViewModel card)
        {
            if (!ModelState.IsValid)
            {
                return View(card);
            }

            return RedirectToAction("Success");
        }

        // Get: Payment/Success
        public ActionResult Success()
        {
            var a = "".Split();
            return Redirect("Home");
        }
    }
 
}