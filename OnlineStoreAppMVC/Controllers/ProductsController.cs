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
using System.IO;
using OnlineStoreApplication;

namespace OnlineStore.Controllers
{


    public class ProductsController : Controller

    {
        private ProductManager manager = new ProductManager(new ApplicationDbContext());
        private ImageManager imageManager = new ImageManager(new ApplicationDbContext());

        // GET: Products
        public ActionResult Index()
        {
            return View(manager.GetAll());
        }
        public void test(HttpPostedFileBase file) {

            Console.WriteLine(file.FileName);
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = manager.GetById((int)id);

            return View(product);
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
        public ActionResult Create([Bind(Include = "Id,Name,Price,Stock,Files")] Product product, List<HttpPostedFileBase> files)
        {

            List <Image> images = new List<Image>();

      
            if (ModelState.IsValid)
            {
                for (int i = 0; i < files.Count; i++)
                {
                    var file = files[i];
                    var originalFilename = Path.GetFileName(file.FileName);
                    string path1 = @"C:\temp\";
                    string path = Path.Combine(path1, "Uploads");
                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(path);
                    string path_save = Path.Combine(path, originalFilename);

                    file.SaveAs(path_save);
                    Image image = new Image {path=path_save, order=i };

                    images.Add(image);
                    //imageManager.Add(image);

                }
                product.Images = images;

                manager.Add(product);
                return RedirectToAction("/");

            }

                return View(product);
        }


        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
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

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Price,Stock")] Product product)
        {
            if (ModelState.IsValid)
            {
                manager.Edit(product);
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

       

        //private string UploadedFile(EmployeeViewModel model)
        //{
        //    string uniqueFileName = null;

        //    if (model.ProfileImage != null)
        //    {
        //        string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
        //        uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ProfileImage.FileName;
        //        string filePath = Path.Combine(uploadsFolder, uniqueFileName);
        //        using (var fileStream = new FileStream(filePath, FileMode.Create))
        //        {
        //            model.ProfileImage.CopyTo(fileStream);
        //        }
        //    }
        //    return uniqueFileName;
        //}
    }
}
