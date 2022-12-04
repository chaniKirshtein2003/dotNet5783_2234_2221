using DO;
using DalApi;

namespace Dal;

public class DalOrder:IOrder
{
    /// <summary>
    /// An add object method that accepts an entity object and returns the id of the added object
    /// </summary>
    /// <param name="order">Order to add</param>
    /// <returns>Return the id number of the object that added</returns>
    /// <exception cref="Exception">Throws an error if there is no room for another order</exception>
    public int Add(Order order)
    {
        if (order.orderDate > order.deliveryDate)
            throw new Exception("the date is not valid");
        if (order.deliveryDate > order.deliveryDate)
            throw new Exception("the date is not valid");
        //A loop that runs through the list and adds a new order
        foreach (var item in DataSource.ordersList)
        {
            if (order.orderId == item.orderId)
                throw new Exception("this id is already exist");
        }
        //add the order to the list
        DataSource.ordersList.Add(order);
        return order.orderId;
    }
/// <summary>
/// A request/call method of a single object receives an ID number of the entity and returns the corresponding object
/// </summary>
/// <param name="idProduct"></param>
/// <returns>Returns the corresponding object</returns>
/// <exception cref="Exception">Returns an error as soon as no suitable order is found</exception>
public Order Get(int idOrder)
    {
        //A loop that runs until it reaches the desired index
        foreach(var item in DataSource.ordersList)
        {
            if (item.orderId == idOrder)
                return item;
        }
        throw new Exception("there is no order with this id");
    }
/// <summary>
/// Request/read method of the list of all objects of the entity
/// </summary>
/// <returns>Returns all objects of the entity</returns>
public IEnumerable<Order> GetAll()
    {
        //Building a new layout where all the orders will be displayed
        List<Order> newOrderList = new List<Order>();
        //A loop that transfers all order data to the new list
        foreach(var item in DataSource.ordersList)
        {
            newOrderList.Add(item);
        }
        return newOrderList;
    }
/// <summary>
/// A method to delete a order object that receives a order ID number
/// </summary>
/// <param name="idProduct"></param>
/// <exception cref="Exception">Throws an error as soon as no suitable order is found</exception>
public void Delete(int idOrder)
    {
        //A loop that runs through the orders until you find the order you want to delete.
        foreach (var item in DataSource.ordersList)
        {
            if (item.orderId == idOrder)
            {
                DataSource.ordersList.Remove(item);
                return;
            }
        }
        throw new Exception("The order does not exist");
    }
/// <summary>
/// An object update method that will receive a new object
///The method will overwrite the old object with the new object at the same place in the array
///At the beginning of the update, make sure that the object exists - according to an ID number
/// </summary>
/// <param name="orderItem"></param>
/// <exception cref="Exception">Returns an error once no matching object is found</exception>
public void Update(Order order)
    {
        if (order.customerName != "" && order.customerAddress != "" && order.customerEmail != "")
        {
            bool flag = false;
            //A loop that runs through the orders until you find the order you want to update.
            foreach (var item in DataSource.ordersList)
            {
                if (item.orderId == order.orderId)
                {
                    DataSource.ordersList.Remove(item);
                    flag = true;
                    break;
                }
            }
            //Updating the order in the order system
            if (flag == true)
            {
                DataSource.ordersList.Add(order);
            }
            else
                throw new Exception("The order does not exist");
        }
    }
}

