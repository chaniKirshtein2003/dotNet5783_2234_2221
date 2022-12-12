using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class OrderItem
    {
        /// <summary>
        /// Unique ID of  OrderItem
        /// </summary>
        public int OrderItemId { get; set; }
        /// <summary>
        /// The name of the item in the order
        /// </summary>
        public string? OrderItemName { get; set; }
        /// <summary>
        /// Product ID number
        /// </summary>
        public int ProductId { get; set; }
        /// <summary>
        /// Price per unit
        /// </summary>
        public double Price { get; set; }
        /// <summary>
        /// Quantity
        /// </summary>
        public int Amount { get; set; }
        /// <summary>
        /// The total price of the orderItems
        /// </summary>
        public double TotalPrice { get; set; }
        /// <summary>
        /// Overriding method of the ToString 
        /// </summary>
        /// <returns>Returns a string describing a orderItem</returns>
        public override string ToString()
        {
            return ClassToString.ToStringProperty(this);
        }
    }
}
