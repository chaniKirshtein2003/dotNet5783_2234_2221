using DO;
using System.Diagnostics;
using System.Linq;

namespace Dal;

public class DalOrderItem
{
    Product[] product = DataSource.productsArr;
    Order[] order=DataSource.ordersArr;
    /// <summary>
    /// An add object method that accepts an entity object and returns the id of the added object
    /// </summary>
    /// <param name="order">OrderItem to add</param>
    /// <returns>Return the id number of the object that added</returns>
    /// <exception cref="Exception">Throws an error if there is no room for another orderItem</exception>
    public int AddOrderItem(OrderItem orderItem)
    {
        orderItem.orderItemId = DataSource.Config.GetOrderItemNextId();
        //Checks if there is room to insert another order and throws an error if there is none
        if (DataSource.orderItemsArr.Length - 1 != DataSource.Config.orderItemNextIndex)
        {
            DataSource.orderItemsArr[DataSource.Config.orderItemNextIndex++] = orderItem;
        }
        else
        {
            throw new Exception("there is no place");
        }
        return orderItem.orderItemId;
    }

/// <summary>
/// A request/call method of a single object receives an ID number of the entity and returns the corresponding object
/// </summary>
/// <param name="idProduct"></param>
/// <returns>Returns the corresponding object</returns>
/// <exception cref="Exception">Returns an error as soon as no suitable orderItem is found</exception>
public OrderItem GetOrderItem(int idOrderItem)
{
    int i = 0;
        //A loop that runs until it reaches the desired index
        while (i <= DataSource.Config.orderItemNextIndex && DataSource.orderItemsArr[i].orderItemId != idOrderItem)
        {
            i++;
        }
        //A condition that checks whether the id matches is displayed at the end of a message if it is not found
        if (DataSource.orderItemsArr[i].orderItemId == idOrderItem)
        return DataSource.orderItemsArr[i];
    throw new Exception("there is no orderItem with this id");
}
/// <summary>
/// Request/read method of the list of all objects of the entity
/// </summary>
/// <returns>Returns all objects of the entity</returns>
public OrderItem[] GetAllOrderItems()
    {
        OrderItem[] orderItem = DataSource.orderItemsArr;
        //Building a new layout where all the orderItems will be displayed
        OrderItem[] newOrderItemsArr = new OrderItem[DataSource.Config.orderItemNextIndex];
        //A loop that transfers all orderItem data to the new array
        for (int i = 0; i < DataSource.Config.orderItemNextIndex; i++)
            newOrderItemsArr[i] = orderItem[i];
        return newOrderItemsArr;
    }
/// <summary>
/// A method to delete a orderItem object that receives a orderItem ID number
/// </summary>
/// <param name="idProduct"></param>
/// <exception cref="Exception">Throws an error as soon as no suitable orderItem is found</exception>
public void DeletOrderItem(int idOrderItem)
    {
        int i;
        //A loop that runs through the orderItems until you find the orderItem you want to delete.
        for (i = 0; i < DataSource.orderItemsArr.Length && DataSource.orderItemsArr[i].orderItemId != idOrderItem; i++)
        {
            //A condition that checks whether the orderItem you want to delete exists and throws an error accordingly.
            if (i >= DataSource.orderItemsArr.Length)
                throw new Exception("The orderItem does not exist");
        }
        //A loop that ran to delete the orderItem and updates the number of orderItems
        for (int j = i + 1; j < DataSource.orderItemsArr.Length; j++)
            DataSource.orderItemsArr[j - 1] = DataSource.orderItemsArr[j];
        DataSource.Config.orderItemNextIndex--;
    }
/// <summary>
/// An object update method that will receive a new object
///The method will overwrite the old object with the new object at the same place in the array
///At the beginning of the update, make sure that the object exists - according to an ID number
/// </summary>
/// <param name="orderItem"></param>
/// <exception cref="Exception">Returns an error once no matching object is found</exception>
public void UpdateOrderItem(OrderItem orderItem)
    {
        if (orderItem.pricePerUnit != 0 && orderItem.amount != 0)
        {
            int i = 0;
            //A loop that runs through the orderItems until you find the orderItem you want to update.
            while (i <= DataSource.Config.orderItemNextIndex && DataSource.orderItemsArr[i].orderItemId != orderItem.orderItemId)
            {
                i++;
            }
            //A condition that checks whether the orderItem you want to update exists and throws an error accordingly.
            if (i >= DataSource.orderItemsArr.Length)
                throw new Exception("The orderItem does not exist");
            //Updating the orderItem in the orderItem system
            DataSource.orderItemsArr[i] = orderItem;
        }
    }
    //return object of orderItem by idProuct and idOrder
    public OrderItem GetItemById(int idProduct, int idOrder)
    {
        //A loop that ran over the order items until the product code and the corresponding order code were found
        for (int i = 0; i < DataSource.Config.orderItemNextIndex; i++)
        {
            //The checker conditions whether the product code and the order code match
            if (idProduct == DataSource.orderItemsArr[i].productId && idOrder == DataSource.orderItemsArr[i].orderId)
            {
                return DataSource.orderItemsArr[i];
            }
        }
        //if this id does not exist in array
        throw new Exception("This id of orderItem does not exist");
    }

    //return array of products by idOrder
    public OrderItem[] GetAllProductsOfOrder(int idOrder)
    {
        int index = 0;
        OrderItem[] productsArr = new OrderItem[4];
        //A loop that runs through the order items until the appropriate ID is found
        for (int i = 0; i < DataSource.Config.orderItemNextIndex; i++)
        {
            //The condition checks whether the IDs are the same
            if (idOrder == DataSource.ordersArr[i].orderId)
            {
                productsArr[index++] = DataSource.orderItemsArr[i];
            }
        }
        if (productsArr.Length > 0)
            return productsArr;
        //if this id does not exist in array
        throw new Exception("This id of orderItem does not exist");
    }
}


