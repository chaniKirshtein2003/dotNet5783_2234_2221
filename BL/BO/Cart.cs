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
        /// <summary>
        /// The customer's name of this buying
        /// </summary>
        public string customerName { get; set; }
        /// <summary>
        /// The customer's email of this buying
        /// </summary>
        public string customerEmail { get; set; }
        /// <summary>
        /// The customer's address of this buying
        /// </summary>
        public string customerAddress { get; set; }
        /// <summary>
        /// The list of the items in this buying
        /// </summary>
        public List<OrderItem> items { get; set; }
        /// <summary>
        /// The total price of this buying
        /// </summary>
        public double totalPrice { get; set; }
        /// <summary>
        /// Overriding method of the ToString 
        /// </summary>
        /// <returns>Returns a string describing a cart</returns>
        public override string ToString()
        {
            return ClassToString.ToStringProperty(this);
        }

    }
}
