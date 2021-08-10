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
    public class OrderLineManager: GenericManager<OrderLine>
    {
        public OrderLineManager(ApplicationDbContext context) : base(context)
        {
        }

        public List<OrderLine> GetAll()
        {
            return Context.OrdersLine.ToList();
        }

        public OrderLine GetById(int id)
        {
            return Context.OrdersLine.Find(id);
        }

        public void Edit(OrderLine orderL)
        {
            var orderLine = GetById(orderL.Id);
            orderLine.Quantity = orderL.Quantity;
            var price = orderLine.Quantity * orderLine.Product.Price;
            orderLine.Price = price;
            Context.Entry(orderLine).State = EntityState.Modified;
            Context.SaveChanges();
        }

        public OrderLine Delete(int id)
        {
            OrderLine order = Context.OrdersLine.Find(id);
            if (order != null)
            {
                Remove(order);
                Context.SaveChanges();

            }
            return order;

        }
    }
}
