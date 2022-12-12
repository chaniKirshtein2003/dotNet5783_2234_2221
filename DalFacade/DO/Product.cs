using System.Xml.Linq;

namespace DO;
/// <summary>
/// Structure for Product
/// </summary>
public struct Product
{
    /// <summary>
    /// Unique ID of Product
    /// </summary>
    public int ProductId { get; set; }
    /// <summary>
    /// Product name
    /// </summary>
    public string? ProductName { get; set; }
    /// <summary>
    /// The category of the product
    /// </summary>
    public Categories? Category { get; set; }
    /// <summary>
    /// The price of the product
    /// </summary>
    public double Price { get; set; }
    /// <summary>
    /// Amount in stock of the product
    /// </summary>
    public int AmountInStock { get; set; }
    /// <summary>
    /// Overriding method of the ToString 
    /// </summary>
    /// <returns>Returns a string describing a product</returns>
    public override string ToString() => $@"
        Product ID={ProductId}: {ProductName},
        category - {Category}
    	Price: {Price}
    	Amount in stock: {AmountInStock}
    ";
}
