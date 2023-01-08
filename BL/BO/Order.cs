namespace BO
{
    public class Order
    {
        /// <summary>
        /// Unique ID of Order
        /// </summary>
        public int OrderId { get; set; }
        /// <summary>
        /// The name of the ordering customer
        /// </summary>
        public string? CustomerName { get; set; }
        /// <summary>
        /// mail adress
        /// </summary>
        public string? CustomerEmail { get; set; }
        /// <summary>
        /// shipping address
        /// </summary>
        public string? CustomerAddress { get; set; }
        /// <summary>
        /// The status of the order
        /// </summary>
        public OrderStatus? Status { get; set; }

        /// <summary>
        /// Order creation date
        /// </summary>
        public DateTime? OrderDate { get; set; }

        /// <summary>
        /// shipping date
        /// </summary>
        public DateTime? ShipDate { get; set; }

        /// <summary>
        /// Date of delivery
        /// </summary>
        public DateTime? DeliveryDate { get; set; }

        /// <summary>
        /// The list of the items in the order
        /// </summary>
        public List<OrderItem?>? Items { get; set; }

        /// <summary>
        /// The total price of the order
        /// </summary>
        public double TotalPrice { get; set; }

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
