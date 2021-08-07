using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStoreCORE
{
    public class Order
    {
        public int Id { get; set; }
        public ApplicationUser User { get; set; }
        public List<OrderLine> OrderLines { get; set; }
        public ShoppingCart Shoppingcart { get; set; }
        public string Status { get; set; }
        public string DeliveryAddress { get; set; }

    }
    public class OrderLine {
        public int Id { get; set; }
        public Product Product { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public OrderLine(Product product, int quantity)
        {
            Product = product;
            Quantity = quantity;
            Price =  product.Price * quantity;

        }
    }
}
