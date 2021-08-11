using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStoreCORE
{
    public class ShoppingCart
    {
        public int Id { get; set; }
        public virtual List<OrderLine> OrderLines { get; set; }
        public ShoppingCart()
        {
            OrderLines = new List<OrderLine>();

        }
    }
  
}
