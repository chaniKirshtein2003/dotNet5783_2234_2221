namespace DO;
/// <summary>
/// Structure for OrderItem
/// </summary>
public struct OrderItem
{
    /// <summary>
    /// Unique ID of  OrderItem
    /// </summary>
    public int OrderItemId { get; set; }
    /// <summary>
    /// Order ID number
    /// </summary>
    public int OrderId { get; set; }
    /// <summary>
    /// Product ID number
    /// </summary>
    public int ProductId { get; set; }
    /// <summary>
    /// Price per unit
    /// </summary>
    public double PricePerUnit { get; set; }
    /// <summary>
    /// Quantity
    /// </summary>
    public int Amount { get; set; }
    /// <summary>
    /// Overriding method of the ToString 
    /// </summary>
    /// <returns>Returns a string describing a orderItem</returns>
    public override string ToString() => $@"
        OrderItem ID={OrderItemId},
        Order Id - {OrderId}
    	Product Id: {ProductId}
    	Price Per Unit: {PricePerUnit}
        Amount: {Amount}
    ";
}
