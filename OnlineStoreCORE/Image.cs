using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStoreCORE
{
    public class Image
    {
        public int Id { get; set; }
        public string path { get; set; }
        public int order { get; set; }
       
    }
}

