namespace DO;

/// <summary>
/// Structure for Order
/// </summary>
public struct Order
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
    ///  The address of the customer
    /// </summary>
    public string? CustomerAddress { get; set; }
    /// <summary>
    /// mail address
    /// </summary>
    public string? CustomerEmail { get; set; }
    /// <summary>
    /// shipping address
    /// </summary>

    public DateTime? OrderDate { get; set; }
    /// <summary>
    /// delivery date
    /// </summary>
    public DateTime? DeliveryDate { get; set; }
    /// <summary>
    /// date of shipping
    /// </summary>

    
    public DateTime? ShipDate { get; set; }


    /// <summary>
    /// Overriding method of the ToString 
    /// </summary>
    /// <returns>Returns a string describing a order</returns>
    public override string ToString() => $@"
        Order ID={OrderId},
        Customer Name - {CustomerName}
    	Email: {CustomerEmail}
    	Shipping Address: {CustomerAddress}
        Order Date: {OrderDate}
        Delivery Date: {DeliveryDate}
    ";
}


