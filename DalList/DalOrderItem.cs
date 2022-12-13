﻿using DO;
using DalApi;
using System.Linq;

namespace Dal;

public class DalOrderItem :IOrderItem
{
    /// <summary>
    /// An add object method that accepts an entity object and returns the id of the added object
    /// </summary>
    /// <param name="order">OrderItem to add</param>
    /// <returns>Return the id number of the object that added</returns>
    /// <exception cref="Exception">Throws an error if there is no room for another orderItem</exception>
    public int Add(OrderItem orderItem)
    {
        //A loop that runs through the list and adds a new orderItem
        foreach (var item in DataSource.orderItemsList)
        {
            if (orderItem.OrderItemId == item?.OrderItemId)
                throw new Exception("this id is already exist");
        }
        //add orderItem to the list
        DataSource.orderItemsList.Add(orderItem);
        return orderItem.OrderItemId;
    }

    /// <summary>
    /// A request/call method of a single object receives an ID number of the entity and returns the corresponding object
    /// </summary>
    /// <param name="idProduct"></param>
    /// <returns>Returns the corresponding object</returns>
    /// <exception cref="Exception">Returns an error as soon as no suitable orderItem is found</exception>
    public OrderItem Get(int idOrderItem)
    {
        //A loop that runs until it reaches the desired index
        foreach (var item in DataSource.orderItemsList)
        {
            if (item?.OrderItemId == idOrderItem)
                return item??throw new NotExistException(idOrderItem,"There is no orderItem with thid id");
        }
        throw new NotExistException(idOrderItem,"there is no orderItem with this id");
    }
    /// <summary>
    /// Request/read method of the list of all objects of the entity
    /// </summary>
    /// <returns>Returns all objects of the entity</returns>
    public IEnumerable<OrderItem?> GetAll(Func<OrderItem?, bool>? pred = null)
    {
        //Building a new layout where all the orderItems will be displayed
        List<OrderItem?> newOrderItemList = new List<OrderItem?>();
        //A loop that transfers all orderItem data to the new list
        foreach (OrderItem? item in DataSource.orderItemsList)
        {
            if (pred == null || pred(item))
                newOrderItemList.Add(item);
        }
        return newOrderItemList;
    }
    /// <summary>
    /// A method to delete a orderItem object that receives a orderItem ID number
    /// </summary>
    /// <param name="idProduct"></param>
    /// <exception cref="Exception">Throws an error as soon as no suitable orderItem is found</exception>
    public void Delete(int idOrderItem)
    {
        //A loop that runs through the orderItems until you find the orderItem you want to delete.
        foreach (var item in DataSource.orderItemsList)
        {
            if (item?.OrderItemId == idOrderItem)
            {
                DataSource.orderItemsList.Remove(item);
                return;
            }
        }
        throw new Exception("The orderItem does not exist");
    }
    /// <summary>
    /// An object update method that will receive a new object
    ///The method will overwrite the old object with the new object at the same place in the array
    ///At the beginning of the update, make sure that the object exists - according to an ID number
    /// </summary>
    /// <param name="orderItem"></param>
    /// <exception cref="Exception">Returns an error once no matching object is found</exception>
    public void Update(OrderItem orderItem)
    {
        bool flag = false;
        //A loop that runs through the orderItems until you find the orderItem you want to update.
        foreach (var item in DataSource.orderItemsList)
        {
            if (item?.OrderItemId == orderItem.OrderItemId)
            {
                DataSource.orderItemsList.Remove(item);
                flag = true;
            }
        }
        //Updating the orderItem in the orderItem system
        if (flag == true)
        {
            DataSource.orderItemsList.Add(orderItem);
        }
        else
            throw new Exception("The orderItem does not exist");
    }

    //return object of orderItem by idProuct and idOrder
    public OrderItem GetItemById(int idProduct, int idOrder)
    {
        //A loop that ran over the order items until the product code and the corresponding order code were found
        foreach (var item in DataSource.orderItemsList)
        {
            //The checker conditions whether the product code and the order code match
            if (idProduct == item?.ProductId && idOrder == item?.OrderId)
                return item?? throw new Exception("threr is no product and order with tis id");
        }
        //if this id does not exist in array
        throw new NotExistException(idProduct,"This id of orderItem does not exist in the order");
    }

    //return list of products by idOrder
    public IEnumerable<OrderItem?> GetOrderItems(int idOrder)
    {
        List<OrderItem> productsList = new List<OrderItem>();
        //A loop that runs through the order items until the appropriate ID is found
        foreach (var item in DataSource.orderItemsList)
        {
            //The condition checks whether the IDs are the same
            if (idOrder == item?.OrderId)
                productsList.Add(item??throw new NotExistException(idOrder,"There is no order with this id"));
        }
        return productsList;
    }
    public OrderItem GetByCondition(Func<OrderItem?, bool>? check)
    {
        foreach (OrderItem item in DataSource.orderItemsList)
        {
            if (check(item))
                return item;
        }
        throw new DO.NotExistException(1, "OrderItem");
    }
}
