using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Web;
namespace OnlineStoreCORE
{
    public class Product
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }

        public virtual List<Image> Images { get; set; }
    }
}
