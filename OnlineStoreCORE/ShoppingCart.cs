using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStoreCORE
{
    public class ShoppingCart
    {
        public int Id { get; set; }
        public List<ShoppingCartLine> ShoppingCartLines { get; set; }
       
        public  DateTime Date { get; set; }

        public ShoppingCart(List<ShoppingCartLine> shoppingCartLines, DateTime date)
        {
            ShoppingCartLines = shoppingCartLines;
            Date = date;
        }
        public ShoppingCart()
        {

        }
    }
    public class ShoppingCartLine {
        public int Id { get; set; }
        public int quantity { get; set; }
        public Product product {get; set;}

    }
}
