using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BO
{
    public class Cart
    {
        public string customerName { get; set; }
        public string customerEmail { get; set; }
        public string customerAddress { get; set; }
        public List<OrderItem> items { get; set; }
        public double totalPrice { get; set; }
        public override string ToString()
        {
            return ClassToString.ToStringProperty(this);
        }

    }
}
