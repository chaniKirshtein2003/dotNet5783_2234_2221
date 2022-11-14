using DO;
namespace Dal;
    public class DalProduct
    {
    /// <summary>
    /// An add object method that accepts an entity object and returns the id of the added object
    /// </summary>
    /// <param name="order">Product to add</param>
    /// <returns>Return the id number of the object that added</returns>
    /// <exception cref="Exception">Throws an error if there is no room for another product</exception>
    public int AddProduct(Product product)
    {
        product.productId = DataSource.Config.productNextId;
        if (DataSource.productsArr.Length - 1 != DataSource.Config.productNextIndex)
        {
            DataSource.productsArr[DataSource.Config.productNextIndex++] = product;
        }
        else
        {
            throw new Exception("there is no place");
        }
        return product.productId;
    }
    /// <summary>
    /// A request/call method of a single object receives an ID number of the entity and returns the corresponding object
    /// </summary>
    /// <param name="idProduct"></param>
    /// <returns>Returns the corresponding object</returns>
    /// <exception cref="Exception">Returns an error as soon as no suitable product is found</exception>
    public Product GetProduct(int idProduct)
    {
        int i = 0;
        //A loop that runs until it reaches the desired index
        while (i < DataSource.Config.productNextIndex && DataSource.productsArr[i].productId != idProduct)
        {
            i++;
        }
        //A condition that checks whether the id matches is displayed at the end of a message if it is not found
        if (DataSource.productsArr[i].productId != idProduct)
            return DataSource.productsArr[i];
        throw new Exception("there is no product with this id");
    }
    /// <summary>
    /// Request/read method of the list of all objects of the entity
    /// </summary>
    /// <returns>Returns all objects of the entity</returns>
    public Product[] GetAllProducts()
    {
        Product[] products = DataSource.productsArr;
        //Building a new layout where all the products will be displayed
        Product[] newProductArr = new Product[DataSource.productsArr.Length];
        //A loop that transfers all product data to the new array
        for (int i = 0; i < DataSource.productsArr.Length; i++)
            newProductArr[i] = DataSource.productsArr[i];
        return newProductArr;
    }
    /// <summary>
    /// A method to delete a product object that receives a product ID number
    /// </summary>
    /// <param name="idProduct"></param>
    /// <exception cref="Exception">Throws an error as soon as no suitable product is found</exception>
    public void DeletProduct(int idProduct)
    {
        int i;
        //A loop that runs through the products until you find the product you want to delete.
        for (i = 0; i < DataSource.productsArr.Length && DataSource.productsArr[i].productId != idProduct; i++)
        {
            //A condition that checks whether the product you want to delete exists and throws an error accordingly.
            if (i >= DataSource.productsArr.Length)
                throw new Exception("The product does not exist");
        }
        //A loop that ran to delete the product and updates the number of products
        for (int j = i + 1; j < DataSource.productsArr.Length; j++)
            DataSource.productsArr[j - 1] = DataSource.productsArr[j];
        DataSource.Config.productNextIndex--;
    }
    /// <summary>
    /// An object update method that will receive a new object
    ///The method will overwrite the old object with the new object at the same place in the array
    ///At the beginning of the update, make sure that the object exists - according to an ID number
    /// </summary>
    /// <param name="orderItem"></param>
    /// <exception cref="Exception">Returns an error once no matching object is found</exception>
    public void UpdateProduct(Product product)
    {
        int i;
        //A loop that runs through the products until you find the product you want to update.
        for (i = 0; i < DataSource.productsArr.Length && DataSource.productsArr[i].productId != product.productId; i++)
        {
            //A condition that checks whether the product you want to update exists and throws an error accordingly.
            if (i >= DataSource.productsArr.Length)
                throw new Exception("The product does not exist");
        }
        //Updating the product in the product system
        DataSource.productsArr[i] = product;
    }
}

