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
    public class ShoppingCartManager: GenericManager<OrderLine>
    {
        public ShoppingCartManager(ApplicationDbContext context) : base(context)
        {
        }

      
        public OrderLine GetById(int id)
        {
            return Context.OrderLines.Find(id);
        }
        public List<OrderLine> GetOrderLinesByUser(string mail)
        {
            var temp =  Context.Users.Where(s => s.Email == mail).ToList();
            if (temp.Count > 0)
                return temp[0].ShoppingCart.OrderLines;
            return new List<OrderLine>();
        
        }

        public void Edit(OrderLine orderL)
        {
            var orderLine = Context.OrderLines.Find(orderL.Id);
            orderLine.Quantity = orderL.Quantity;
            var price = orderLine.Quantity * orderLine.Product.Price;
            orderLine.Price = price;
            Context.Entry(orderLine).State = EntityState.Modified;
            Context.SaveChanges();
        }

        public void EmptyShoppingCart(string  mail)
        {
            var user = GetCurrentUser(mail);
            user.ShoppingCart = new ShoppingCart();
            Context.SaveChanges();
                
        }

        public OrderLine Delete(int id, ApplicationUser user)
        {
            OrderLine order = Context.OrderLines.Find(id);
            if (order != null) { 

                Product p = order.Product;
                user.ShoppingCart.OrderLines.Remove(order);
                p.Stock += order.Quantity;
                Remove(order);
                Context.SaveChanges();

            }
            return order;

        }

        public void AddOrder(Order order)
        {
            Context.Orders.Add(order);
            Context.SaveChanges();
        }
    }
}
