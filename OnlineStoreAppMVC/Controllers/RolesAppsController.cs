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

namespace OnlineStoreAppMVC.Controllers
{
    public class RolesAppsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: RolesApps
        public ActionResult Index()
        {
            return View(db.IdentityRoles.ToList());
        }

        // GET: RolesApps/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RolesApp rolesApp = db.IdentityRoles.Find(id);
            if (rolesApp == null)
            {
                return HttpNotFound();
            }
            return View(rolesApp);
        }

        // GET: RolesApps/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RolesApps/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name")] RolesApp rolesApp)
        {
            if (ModelState.IsValid)
            {
                db.IdentityRoles.Add(rolesApp);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(rolesApp);
        }

        // GET: RolesApps/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RolesApp rolesApp = db.IdentityRoles.Find(id);
            if (rolesApp == null)
            {
                return HttpNotFound();
            }
            return View(rolesApp);
        }

        // POST: RolesApps/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] RolesApp rolesApp)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rolesApp).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(rolesApp);
        }

        // GET: RolesApps/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RolesApp rolesApp = db.IdentityRoles.Find(id);
            if (rolesApp == null)
            {
                return HttpNotFound();
            }
            return View(rolesApp);
        }

        // POST: RolesApps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            RolesApp rolesApp = db.IdentityRoles.Find(id);
            db.IdentityRoles.Remove(rolesApp);
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
