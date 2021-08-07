using System.Collections.Generic;
using System.Linq;
using OnlineStoreDAL;
using OnlineStoreCORE;
using System.Data.Entity;
using System.Web;
using System.IO;

namespace OnlineStoreApplication
{
    public class ProductManager : GenericManager<Product>
    {
        private ImageManager imageManager = new ImageManager(new ApplicationDbContext());

        private string imgServerPath { get; set; }
        private string defaultImg { get; set; }


        public ProductManager(ApplicationDbContext context) : base(context)
        {
            imgServerPath = "/Uploads/";
            defaultImg = "product-image-placeholder.jpg";
        }

        public List<Product> GetAll()
        {
            return Context.Products.ToList();
        }

        public void Remove(Product prod) {
            var imgs = new List<Image>(prod.Images);
            foreach (var item in imgs)
                Context.Images.Remove(item);
            Context.Products.Remove(prod);
            Context.SaveChanges();
        }

        public Product GetById(int id)
        {
            return Context.Products.Find(id);
        }
 
        public void Edit(Product product)
        {
            Context.Entry(product).State = EntityState.Modified;
            Context.SaveChanges();
        }

        public void Delete(int id)
        {
            Product product = Context.Products.Find(id);
            if (product != null) {
                Remove(product);
                Context.SaveChanges();
            }
        }

       public void Insert(Product product, List<HttpPostedFileBase> files, HttpServerUtilityBase Server)
        {
            List<Image> images = new List<Image>();

            for (int i = 0; i < files.Count; i++)
            {
                var file = files[i];
                if (file == null)
                    break;
                var originalFilename = Path.GetFileName(file.FileName);
                // TODO: add random name
                originalFilename = "user_" + originalFilename;

                string path = Server.MapPath(imgServerPath);
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                string path_save = Path.Combine(path, originalFilename);

                file.SaveAs(path_save);
                Image image = new Image { path = Path.Combine(imgServerPath, originalFilename), order = i };

                images.Add(image);
            }
            if (images.Count == 0)
            {
                // insert default image
                images.Add(new Image { path = Path.Combine(imgServerPath, defaultImg), order = 0 });
            }
            product.Images = images;

            this.Add(product);
        }
      
    }

}

