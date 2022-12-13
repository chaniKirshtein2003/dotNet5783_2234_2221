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
        if (CheckOrder(order.OrderId))
            throw new DO.ExistException(order.OrderId, "Order");
        //add the order to the list
        DataSource.ordersList.Add(order);
        return order.OrderId;
    }
        /// <summary>
        /// A request/call method of a single object receives an ID number of the entity and returns the corresponding object
        /// </summary>
        /// <param name="idProduct"></param>
        /// <returns>Returns the corresponding object</returns>
        /// <exception cref="Exception">Returns an error as soon as no suitable order is found</exception>
        public Order Get(int idOrder)
    {
        //look for the orderItem with the same id
        return DataSource.ordersList.FirstOrDefault(ord => ord?.OrderId == idOrder) ?? throw new NotExistException(idOrder, "Order");
    }
    /// <summary>
    /// Request/read method of the list of all objects of the entity
    /// </summary>
    /// <returns>Returns all objects of the entity</returns>
    public IEnumerable<Order?> GetAll(Func<Order?, bool>? pred = null)
    {
        if (pred != null)
            //    return from ord in DataSource.ordersList
            //           where pred(ord) select ord;
            return DataSource.ordersList.FindAll(x => pred(x));
        else
            return from ord in DataSource.ordersList
                   select ord;
    }
/// <summary>
/// A method to delete a order object that receives a order ID number
/// </summary>
/// <param name="idProduct"></param>
/// <exception cref="Exception">Throws an error as soon as no suitable order is found</exception>
public void Delete(int idOrder)
    {
        int count = DataSource.ordersList.RemoveAll(ord => ord?.OrderId == idOrder);
        if (count == 0)
            throw new DO.NotExistException(idOrder, "Order");
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
        int count = DataSource.ordersList.RemoveAll(ord => order.OrderId == ord?.OrderId);
        if (count == 0)
            throw new DO.NotExistException(order.OrderId, "Order");
        DataSource.ordersList.Add(order);
    }
    public Order GetByCondition(Func<Order?, bool>? check)
    {
        //return DataSource.ordersList.FirstOrDefault(x=>check(x))?? throw new NotExistException
        foreach (Order item in DataSource.ordersList)
        {
            if (check(item))
                return item;
        }
        throw new DO.NotExistException(1, "Order");
    }
    public bool CheckOrder(int id)
    {
        return DataSource.ordersList.Any(ord => ord?.OrderId == id);
    }
}

