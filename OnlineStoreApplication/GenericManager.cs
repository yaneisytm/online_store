using OnlineStoreCORE;
using OnlineStoreDAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStoreApplication
{
    public class GenericManager<T> where T :class
    {
        public ApplicationDbContext Context { get; private set; }

        public GenericManager(ApplicationDbContext context)
        {
            Context = context;

        }
        public ApplicationUser GetCurrentUser(string name) {
            return Context.Users.Where(u => u.UserName == name).First();


        }
 
        public void Add(T entity)
        {
            Context.Set<T>().Add(entity);
            Context.SaveChanges();
        }

        public void Remove(T entity)
        {
            Context.Set<T>().Remove(entity);
            Context.SaveChanges();

        }
        public void Update(ApplicationUser user)
        {
            Context.Entry(user).State = EntityState.Modified;
            Context.SaveChanges();
        }

        public void Dispose() {
            Context.Dispose();
        }

    }
}
