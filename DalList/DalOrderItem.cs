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
        orderItem.OrderItemId = DataSource.Config.GetOrderItemNextId();
        //add orderItem to the list
        DataSource.orderItemsList.Add(orderItem);
        return orderItem.OrderItemId;
    }

    /// <summary>
    /// A request/call method of a single object receives an ID number of the entity and returns the corresponding object
    /// </summary>
    /// <param name="idProduct"></param>
    /// <returns>Returns the corresponding object</returns>
    /// <exception cref="NotExistException">Returns an error as soon as no suitable orderItem is found</exception>
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
        if (pred != null)
            //    return from orderItem in DataSource.orderItemsList
            //           where pred(ordItem) select orderItem;
            return DataSource.orderItemsList.FindAll(x => pred(x));
        else
            return from ordItem in DataSource.orderItemsList
                   select ordItem;
    }
    /// <summary>
    /// A method to delete a orderItem object that receives a orderItem ID number
    /// </summary>
    /// <param name="idProduct"></param>
    /// <exception cref="NotExistException">Throws an error as soon as no suitable orderItem is found</exception>
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
    /// <exception cref="NotExistException">Returns an error once no matching object is found</exception>
    public void Update(OrderItem orderItem)
    {
        
        int count = DataSource.orderItemsList.RemoveAll(ordItem => orderItem.OrderItemId == ordItem?.OrderItemId);
        if (count == 0)
            throw new DO.NotExistException(orderItem.OrderItemId, "OrderItem");
        DataSource.orderItemsList.Add(orderItem);
    }

    /// <summary>
    /// The function returns an object of orderItem by idProuct and idOrder
    /// </summary>
    /// <param name="idProduct"></param>
    /// <param name="idOrder"></param>
    /// <returns>Return order by id product and id order</returns>
    /// <exception cref="NotExistException"></exception>
    public OrderItem GetItemById(int idProduct, int idOrder)
    {
        return DataSource.orderItemsList.FirstOrDefault(item => idProduct == item?.ProductId && idOrder == item?.OrderId) ?? throw new NotExistException(idProduct, "OrderItem");
    }

    /// <summary>
    /// The function returns an object of orderItem by a condition
    /// </summary>
    /// <param name="check"></param>
    /// <returns>Return orderItem by condition</returns>
    /// <exception cref="NotExistException"></exception>
    public OrderItem? GetByCondition(Func<OrderItem?, bool>? check)
    {
        return DataSource.orderItemsList.FirstOrDefault(x => check!(x)) ?? throw new NotExistException(1, "OrderItem");
    }
    /// <summary>
    /// The function returns if exists in the list an order items with this id
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Return if there is order item with the id that send or not </returns>
    public bool CheckOrderItem(int id)
    {
        return DataSource.orderItemsList.Any(ordItem => ordItem?.OrderItemId == id);
    }
}
