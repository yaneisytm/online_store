using OnlineStoreCORE;
using OnlineStoreDAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStoreApplication
{
   public class ImageManager:GenericManager<Image>
    {
        public ImageManager(ApplicationDbContext context) : base(context)
        {
        }

        public List<Image> GetAll()
        {
            return Context.Images.ToList();
        }

        public Image GetById(int id)
        {
            return Context.Images.Find(id);
        }

        public void Edit(Image image)
        {
            Context.Entry(image).State = EntityState.Modified;
            Context.SaveChanges();
        }

        public void Delete(int id)
        {
            Image image = Context.Images.Find(id);
            if (image != null)
            {
                Remove(image);
                Context.SaveChanges();

            }

        }
    }
}
