using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Cart
    {
        public string customerName { get; set; }
        public string customerEmail { get; set; }
        public string customerAdress { get; set; }
        public OrderItem items { get; set; }
        public double totalPrice { get; set; }
    }
}
