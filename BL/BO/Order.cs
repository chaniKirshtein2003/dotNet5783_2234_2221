using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Order
    {
        /// <summary>
        /// Unique ID of Order
        /// </summary>
        public int orderId { get; set; }
        /// <summary>
        /// The name of the ordering customer
        /// </summary>
        public string customerName { get; set; }
        /// <summary>
        /// mail adress
        /// </summary>
        public string customerEmail { get; set; }
        /// <summary>
        /// shipping address
        /// </summary>
        public string customerAddress { get; set; }
        public OrderStatus status { get; set; }

        /// <summary>
        /// Order creation date
        /// </summary>
        /// 
        public DateTime orderDate { get; set; }
        public DateTime paymentDate { get; set; }
        /// <summary>
        /// shipping date
        /// </summary>
        public DateTime shipDate { get; set; }
        /// <summary>
        /// Date of delivery
        /// </summary>
        public DateTime deliveryDate { get; set; }
        public List<OrderItem> items { get; set; }
        public double totalPrice { get; set; }
        /// <summary>
        /// Overriding method of the ToString 
        /// </summary>
        /// <returns>Returns a string describing a order</returns>
        public override string ToString()
        {
            return ClassToString.ToStringProperty(this);
        }
    }
}
