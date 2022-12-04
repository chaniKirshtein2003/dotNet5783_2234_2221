

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
        /// <summary>
        /// The status of the order
        /// </summary>
        public OrderStatus status { get; set; }

        /// <summary>
        /// Order creation date
        /// </summary>
        /// 
        public DateTime orderDate { get; set; }
        /// <summary>
        /// Order payment date
        /// </summary>
        public DateTime paymentDate { get; set; }
        /// <summary>
        /// shipping date
        /// </summary>
        public DateTime shipDate { get; set; }
        /// <summary>
        /// Date of delivery
        /// </summary>
        public DateTime deliveryDate { get; set; }
        /// <summary>
        /// The list of the items in the order
        /// </summary>
        public List<OrderItem> items { get; set; }
        /// <summary>
        /// The total price of the order
        /// </summary>
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
