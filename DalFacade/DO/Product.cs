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
    public int productId { get; set; }
    /// <summary>
    /// Product name
    /// </summary>
    public string productName { get; set; }
    /// <summary>
    /// The category of the product
    /// </summary>
    public Categories category { get; set; }
    /// <summary>
    /// The price of the product
    /// </summary>
    public double price { get; set; }
    /// <summary>
    /// Amount in stock of the product
    /// </summary>
    public int amountInStock { get; set; }
    /// <summary>
    /// Overriding method of the ToString 
    /// </summary>
    /// <returns>Returns a string describing a product</returns>
    public override string ToString() => $@"
        Product ID={productId}: {productName},
        category - {category}
    	Price: {price}
    	Amount in stock: {amountInStock}
    ";
}
