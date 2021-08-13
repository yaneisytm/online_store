using OnlineStoreCORE;
using OnlineStoreDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStoreApplication
{
   
    public class OrderManager : GenericManager<Order>
    {
        public OrderManager(ApplicationDbContext context) : base(context)
        {
        }


        public Order GetById(int id)
        {
            return Context.Orders.Find(id);
        }
        public List<Order> GetOrdersByUser(string mail)
        {
            var currentUser = Context.Users.Where(s => s.Email == mail);
            if (currentUser.Count() > 0) {

                return currentUser.First().Orders;
            }   
            return new List<Order>();

        }

      
      
    }
}
