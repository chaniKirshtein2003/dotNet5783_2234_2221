using DO;
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
        if (CheckOrderItem(orderItem.OrderItemId))
            throw new DO.ExistException(orderItem.OrderItemId, "OrderItem");
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
        //look for the orderItem with the same id
        return DataSource.orderItemsList.FirstOrDefault(orItem => orItem?.OrderItemId == idOrderItem) ?? throw new NotExistException(idOrderItem, "OrderItem");
    }
    /// <summary>
    /// Request/read method of the list of all objects of the entity
    /// </summary>
    /// <returns>Returns all objects of the entity</returns>
    public IEnumerable<OrderItem?> GetAll(Func<OrderItem?, bool>? pred = null)
    {
        ////Building a new layout where all the orderItems will be displayed
        //List<OrderItem?> newOrderItemList = new List<OrderItem?>();
        ////A loop that transfers all orderItem data to the new list
        //foreach (OrderItem? item in DataSource.orderItemsList)
        //{
        //    if (pred == null || pred(item))
        //        newOrderItemList.Add(item);
        //}
        //return newOrderItemList;
        //copy to new arr all the products that exist the arr
        if (pred != null)
            //    return from ordItem in DataSource.orderItemsList
            //           where pred(ordItem) select ordItem;
            return DataSource.orderItemsList.FindAll(x => pred(x));
        else
            return from ordItem in DataSource.orderItemsList
                   select ordItem;
    }
    /// <summary>
    /// A method to delete a orderItem object that receives a orderItem ID number
    /// </summary>
    /// <param name="idProduct"></param>
    /// <exception cref="Exception">Throws an error as soon as no suitable orderItem is found</exception>
    public void Delete(int idOrderItem)
    {
        int count = DataSource.orderItemsList.RemoveAll(orItem => orItem?.OrderItemId == idOrderItem);
        if (count == 0)
            throw new DO.NotExistException(idOrderItem, "OrderItem");
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
        int count = DataSource.orderItemsList.RemoveAll(ordItem => orderItem.OrderItemId == ordItem?.OrderItemId);
        if (count == 0)
            throw new DO.NotExistException(orderItem.OrderItemId, "OrderItem");
        DataSource.orderItemsList.Add(orderItem);
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
        List<OrderItem?> productsList = new List<OrderItem?>();
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
        //return DataSource.orderItemsList.FirstOrDefault(x=>check(x))??throw new
        foreach (OrderItem item in DataSource.orderItemsList)
        {
            if (check(item))
                return item;
        }
        throw new DO.NotExistException(1, "OrderItem");
    }
    public bool CheckOrderItem(int id)
    {
        return DataSource.orderItemsList.Any(ordItem => ordItem?.OrderItemId == id);
    }
}
