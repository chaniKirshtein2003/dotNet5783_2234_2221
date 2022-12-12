﻿using DO;
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
    public void Add(DO.Product product)
    {
        //A loop that runs through the list and adds a new product
        //foreach (var item in DataSource.productsList)
        //{
        //    if (product.ProductId == item.ProductId)
        //        throw new Exception("this id is already exist");
        //}
        //add product to the list
        //DataSource.productsList.Add(product);
        //return product.ProductId;

        if (CheckProduct(product.ProductId))
            throw new DO.ExistException(product.ProductId, "Product");
        DataSource.productsList.Add(product);
    }
    /// <summary>
    /// A request/call method of a single object receives an ID number of the entity and returns the corresponding object
    /// </summary>
    /// <param name="idProduct"></param>
    /// <returns>Returns the corresponding object</returns>
    /// <exception cref="Exception">Returns an error as soon as no suitable product is found</exception>
    public Product Get(int idProduct)
    {
        //A loop that runs until it reaches the desired index
        foreach (var item in DataSource.productsList)
        {
            if (item.ProductId == idProduct)
                return item;
        }
        throw new Exception("there is no product with this id");
    }
    /// <summary>
    /// Request/read method of the list of all objects of the entity
    /// </summary>
    /// <returns>Returns all objects of the entity</returns>
    public IEnumerable<Product?> GetAll(Func<Product?,bool>? pred=null)
    {
        //Building a new layout where all the products will be displayed
        List<Product?> newProductList = new List<Product?>();
        //A loop that transfers all product data to the new list
        foreach (Product? item in DataSource.productsList)
        {
            if (pred == null || pred(item))
                newProductList.Add(item);
        }
        return newProductList;
    }
    /// <summary>
    /// A method to delete a product object that receives a product ID number
    /// </summary>
    /// <param name="idProduct"></param>
    /// <exception cref="Exception">Throws an error as soon as no suitable product is found</exception>
    public void Delete(int idProduct)
    {
        //A loop that runs through the products until you find the product you want to delete.
        foreach (var item in DataSource.productsList)
        {
            if (item.ProductId == idProduct)
            {
                DataSource.productsList.Remove(item);
                return;
            }
        }
        throw new Exception("The product does not exist");
    }
    /// <summary>
    /// An object update method that will receive a new object
    ///The method will overwrite the old object with the new object at the same place in the array
    ///At the beginning of the update, make sure that the object exists - according to an ID number
    /// </summary>
    /// <param name="orderItem"></param>
    /// <exception cref="Exception">Returns an error once no matching object is found</exception>
    public void Update(Product product)
    {
        bool flag = false;
        //A loop that runs through the products until you find the product you want to update.
        foreach (Product item in DataSource.productsList)
        {
            if (item.ProductId == product.ProductId)
            {
                DataSource.productsList.Remove(item);
                flag = true;
                break;
            }
        }
        //Updating the product in the product system
        if (flag == true)
        {
            DataSource.productsList.Add(product);
        }
        else
            throw new Exception("The product does not exist");
    }
    public Product GetByCondition(Func<Product, bool>? check)
    {
        foreach (Product item in DataSource.productsList)
        {
            if (check(item))
                return item;
        }
        throw new DO.NotExistException(1, "Product");
    }
    public bool CheckProduct(int id)
    {
        return DataSource.productsList.Any(product => product?.ProductId == id);
    }

}

