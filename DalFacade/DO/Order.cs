namespace DO;
/// <summary>
/// Structure for Order
/// </summary>
public struct Order
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
    ///  The address of the customer
    /// </summary>
    public string customerAddress { get; set; }
    /// <summary>
    /// mail address
    /// </summary>
    public string customerEmail { get; set; }
    /// <summary>
    /// shipping address
    /// </summary>

    public DateTime orderDate { get; set; }
    /// <summary>
    /// delivery date
    /// </summary>
    public DateTime deliveryDate { get; set; }

    /// <summary>
    /// date of shipping
    /// </summary>
    public DateTime shipDate { get; set; }


    /// <summary>
    /// Overriding method of the ToString 
    /// </summary>
    /// <returns>Returns a string describing a order</returns>
    public override string ToString() => $@"
        Order ID={orderId},
        Customer Name - {customerName}
    	Email: {customerEmail}
    	Shipping Address: {customerAddress}
        Order Date: {orderDate}
        Delivery Date: {deliveryDate}
    ";
}


