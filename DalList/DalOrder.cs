using DO;
using System.Net;
using System.Xml.Linq;

namespace Dal;

public class DalOrder
{
    Order[] dalO = DataSource.ordersArr;
    /// <summary>
    /// An add object method that accepts an entity object and returns the id of the added object
    /// </summary>
    /// <param name="order">Order to add</param>
    /// <returns>Return the id number of the object that added</returns>
    /// <exception cref="Exception">Throws an error if there is no room for another order</exception>
    public int AddOrder(Order order)
    {
        if (order.orderCreationDate > order.deliveryDate)
            throw new Exception("the date is not valid");
        if (order.deliveryDate > order.dateOfDelivery)
            throw new Exception("the date is not valid");
        order.orderId = DataSource.Config.GetOrderNextId();
        //A condition that checks whether there is room in the array to add a new order, and if not, throws a suitable acknowledgment at the end
        if (DataSource.ordersArr.Length - 1 != DataSource.Config.orderNextIndex)
            DataSource.ordersArr[DataSource.Config.orderNextIndex++] = order;
        else
        {
            throw new Exception("there is no place");
        }
        return order.orderId;
    }
/// <summary>
/// A request/call method of a single object receives an ID number of the entity and returns the corresponding object
/// </summary>
/// <param name="idProduct"></param>
/// <returns>Returns the corresponding object</returns>
/// <exception cref="Exception">Returns an error as soon as no suitable order is found</exception>
public Order GetOrder(int idOrder)
    {
        int i = 0;
        //A loop that runs until it reaches the desired index
        while (i <= DataSource.Config.orderNextIndex && DataSource.ordersArr[i].orderId != idOrder)
        {
            i++;
        }
        //A condition that checks whether the id matches is displayed at the end of a message if it is not found
        if (DataSource.ordersArr[i].orderId == idOrder)
            return DataSource.ordersArr[i];
        throw new Exception("there are no order with this id");
    }
/// <summary>
/// Request/read method of the list of all objects of the entity
/// </summary>
/// <returns>Returns all objects of the entity</returns>
public Order[] GetAllOrders()
    {
        //Building a new layout where all the orders will be displayed
        Order[] order = DataSource.ordersArr; 
        Order[] newOrderArr = new Order[DataSource.Config.orderNextIndex];
        //A loop that transfers all order data to the new array
        for (int i=0;i< DataSource.Config.orderNextIndex; i++)
            newOrderArr[i] = order[i];
        return newOrderArr;
    }
/// <summary>
/// A method to delete a order object that receives a order ID number
/// </summary>
/// <param name="idProduct"></param>
/// <exception cref="Exception">Throws an error as soon as no suitable order is found</exception>
public void DeletOrder(int idOrder)
    {
        int i=0;
        //A loop that runs through the orders until you find the order you want to delete.
        while (i <= DataSource.Config.orderNextIndex && DataSource.ordersArr[i].orderId != idOrder)
        {
            i++;
        }
        //A condition that checks whether the order you want to delete exists and throws an error accordingly.
        if (i > DataSource.ordersArr.Length)
            throw new Exception("The order does not exist");
        //A loop that ran to delete the order and updates the number of orders
        for (int j = i + 1; j < DataSource.ordersArr.Length; j++)
            DataSource.ordersArr[j - 1] = DataSource.ordersArr[j];
        DataSource.Config.orderNextIndex--;
    }
/// <summary>
/// An object update method that will receive a new object
///The method will overwrite the old object with the new object at the same place in the array
///At the beginning of the update, make sure that the object exists - according to an ID number
/// </summary>
/// <param name="orderItem"></param>
/// <exception cref="Exception">Returns an error once no matching object is found</exception>
public void UpdateOrder(Order order)
    {
        if (order.customerName != "" && order.shippingAddress != "" && order.email != "")
        {
            int i = 0;
            //A loop that runs through the orders until you find the order you want to update.
            while (i < DataSource.Config.orderNextIndex && DataSource.ordersArr[i].orderId != order.orderId)
            {
                i++;
            }
            //A condition that checks whether the order you want to update exists and throws an error accordingly.
            if (i >= DataSource.ordersArr.Length)
                throw new Exception("The order does not exist");
            //Updating the order in the order system
            DataSource.ordersArr[i] = order;
        }
    }
}

