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
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using OnlineStoreAppMVC;
using Microsoft.AspNet.Identity.Owin;

namespace OnlineStore
{
    public class AppUsersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        // GET: AppUsers
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        // GET: AppUsers/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser appUser = db.Users.Find(id);
            if (appUser == null)
            {
                return HttpNotFound();
            }
            return View(appUser);
        }
        private List<SelectListItem> getRoles()
        {

            var allRoles = (new ApplicationDbContext()).IdentityRoles.OrderBy(r => r.Name).ToList().Select(rr =>

                      new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();

            return allRoles;

        }
        // GET: AppUsers/Create
        public ActionResult Create()
        {
            ViewBag.Roles = getRoles();
            return View();
        }

        // POST: AppUsers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserName,Surname,Email,PasswordHash, Roles")] ApplicationUser appUser, object UserRole)
        {
            if (ModelState.IsValid)
            {

                //ApplicationUserManager managerUser = new ApplicationUserManager(new UserStore<ApplicationUser>(db));
                //ApplicationUserManager managerUser = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

                IdentityResult result = UserManager.Create(appUser, appUser.PasswordHash);

                UserManager.AddToRole(appUser.Id, (string)UserRole);
               

                return RedirectToAction("Index");
            }
            ViewBag.Roles = getRoles();
            return View(appUser);
        }

        // GET: AppUsers/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser appUser = db.Users.Find(id);
            if (appUser == null)
            {
                return HttpNotFound();
            }
            ViewBag.Roles = getRoles();
            return View(appUser);
        }

        // POST: AppUsers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserName,Surname,Email,IsAdmin,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount")] ApplicationUser appUser)
        {
            if (ModelState.IsValid)
            {
                db.Entry(appUser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Roles = getRoles();
            return View(appUser);
        }

        // GET: AppUsers/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser appUser = db.Users.Find(id);
            if (appUser == null)
            {
                return HttpNotFound();
            }
            return View(appUser);
        }

        // POST: AppUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ApplicationUser appUser = db.Users.Find(id);
            db.Users.Remove(appUser);
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
