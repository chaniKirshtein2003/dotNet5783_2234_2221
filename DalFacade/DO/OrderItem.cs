namespace DO;
/// <summary>
/// Structure for OrderItem
/// </summary>
public struct OrderItem
{
    /// <summary>
    /// Unique ID of  OrderItem
    /// </summary>
    public int orderItemId { get; set; }
    /// <summary>
    /// Order ID number
    /// </summary>
    public int orderId { get; set; }
    /// <summary>
    /// Product ID number
    /// </summary>
    public int productId { get; set; }
    /// <summary>
    /// Price per unit
    /// </summary>
    public double pricePerUnit { get; set; }
    /// <summary>
    /// Quantity
    /// </summary>
    public int amount { get; set; }
    /// <summary>
    /// Overriding method of the ToString 
    /// </summary>
    /// <returns>Returns a string describing a orderItem</returns>
    public override string ToString() => $@"
        OrderItem ID={orderItemId},
        Order Id - {orderId}
    	Product Id: {productId}
    	Price Per Unit: {pricePerUnit}
        Amount: {amount}
    ";
}
