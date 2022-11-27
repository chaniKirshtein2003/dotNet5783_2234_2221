using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class ProductItem
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public double price { get; set; }
        public Categories category { get; set; }
        public int amount { get; set; }
        public bool inStock { get; set; }
    }
}
