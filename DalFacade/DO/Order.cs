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
    /// mail adress
    /// </summary>
    public string email { get; set; }
    /// <summary>
    /// shipping address
    /// </summary>
    public string shippingAddress { get; set; }
    /// <summary>
    /// Order creation date
    /// </summary>
    public DateTime orderCreationDate { get; set; }
    /// <summary>
    /// delivery date
    /// </summary>
    public DateTime deliveryDate { get; set; }
    /// <summary>
    /// Date of delivery
    /// </summary>
    public DateTime dateOfDelivery { get; set; }
    /// <summary>
    /// Overriding method of the ToString 
    /// </summary>
    /// <returns>Returns a string describing a order</returns>
    public override string ToString() => $@"
        Order ID={orderId},
        Customer Name - {customerName}
    	Email: {email}
    	Shipping Address: {shippingAddress}
        Order Creation Date: {orderCreationDate}
        Delivery Date: {deliveryDate}
        Date Of Delivery: {dateOfDelivery}
    ";
}


