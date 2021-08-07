using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineStoreDAL;
using OnlineStoreCORE;
using System.Data.Entity;

namespace OnlineStoreApplication
{
    public class ProductManager : GenericManager<Product>
    {
        public ProductManager(ApplicationDbContext context) : base(context)
        {
            

        }
        public List<Product> GetAll()
        {
            return Context.Products.ToList();
        }

        public void Remove(Product prod) {

            foreach (var item in prod.Images)
            {
                Context.Images.Remove(item);
            }
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

       
      
    }

}

