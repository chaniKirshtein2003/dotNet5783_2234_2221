using DO;
using DalApi;

namespace Dal;
public class DalProduct : IProduct
{
    /// <summary>
    /// An add object method that accepts an entity object and returns the id of the added object
    /// </summary>
    /// <param name="order">Product to add</param>
    /// <returns>Return the id number of the object that added</returns>
    /// <exception cref="Exception">Throws an error if there is no room for another product</exception>
    public int Add(DO.Product product)
    {
        if (CheckProduct(product.ProductId))
            throw new DO.ExistException(product.ProductId, "Product");
        //add product to the list
        DataSource.productsList.Add(product);
        return product.ProductId;
    }
    /// <summary>
    /// A request/call method of a single object receives an ID number of the entity and returns the corresponding object
    /// </summary>
    /// <param name="idProduct"></param>
    /// <returns>Returns the corresponding object</returns>
    /// <exception cref="Exception">Returns an error as soon as no suitable product is found</exception>
    public DO.Product Get(int idProduct)
    {
        //look for the product with the same id
        return DataSource.productsList.FirstOrDefault(pr => pr?.ProductId == idProduct) ?? throw new NotExistException(idProduct, "Product");
    }
    /// <summary>
    /// Request/read method of the list of all objects of the entity
    /// </summary>
    /// <returns>Returns all objects of the entity</returns>
    public IEnumerable<Product?> GetAll(Func<Product?, bool>? pred = null)
    {
        if (pred != null)
            return DataSource.productsList.FindAll(x => pred(x));
        else
            return from product in DataSource.productsList
                   select product;
    }
    /// <summary>
    /// A method to delete a product object that receives a product ID number
    /// </summary>
    /// <param name="idProduct"></param>
    /// <exception cref="Exception">Throws an error as soon as no suitable product is found</exception>
    public void Delete(int idProduct)
    {
        int count = DataSource.productsList.RemoveAll(prod => prod?.ProductId == idProduct);
        if (count == 0)
            throw new DO.NotExistException(idProduct, "Product");
    }
    /// <summary>
    /// An object update method that will receive a new object
    ///The method will overwrite the old object with the new object at the same place in the array
    ///At the beginning of the update, make sure that the object exists - according to an ID number
    /// </summary>
    /// <param name="orderItem"></param>
    /// <exception cref="Exception">Returns an error once no matching object is found</exception>
    public void Update(DO.Product product)
    {
        int count = DataSource.productsList.RemoveAll(pr => product.ProductId == pr?.ProductId);
        if (count == 0)
            throw new DO.NotExistException(product.ProductId, "Product");
        DataSource.productsList.Add(product);
    }
    public Product? GetByCondition(Func<Product?, bool>? check)
    {
        //return DataSource.productsList.FirstOrDefault(x=>check(x))?? throw new NotExistException
        return DataSource.productsList.FirstOrDefault(x => check!(x)) ?? throw new NotExistException(1, "product");

    }
    public bool CheckProduct(int id)
    {
        return DataSource.productsList.Any(prod => prod?.ProductId == id);
    }
}

