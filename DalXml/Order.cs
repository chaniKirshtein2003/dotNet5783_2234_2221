using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Dal;
using DalApi;
using DO;
using System.Xml.Linq;

internal class Order : IOrder
{
    const string s_orders = @"Order"; //XML Serializer

    public IEnumerable<DO.Order?> GetAll(Func<DO.Order?, bool>? filter = null)
    {
        List<DO.Order?> listOrders = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_orders);

        if (filter == null)
            return listOrders.Select(ord => ord).OrderBy(ord => ord?.OrderId);
        else
            return listOrders.Where(filter).OrderBy(ord => ord?.OrderId);
    }
    public DO.Order Get(int id)
    {
        List<DO.Order?> listOrders = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_orders);

        return listOrders.FirstOrDefault(ord => ord?.OrderId == id) ??
            throw new DO.NotExistException(id,"order");
    }
    public DO.Order? GetByCondition(Func<DO.Order?, bool>? check)
    {
        List<DO.Order?> listOrders = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_orders);

        return listOrders.Select(ord => ord).FirstOrDefault() ?? throw new DO.NotExistException(0, "order");
    }
    public int Add(DO.Order order)
    {
        order.OrderId = Config.orderNextId();
        List<DO.Order?> listOrders = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_orders);

        if (listOrders.FirstOrDefault(ord => ord?.OrderId == order.OrderId) != null)
            throw new DO.ExistException(order.OrderId,"order");

        listOrders.Add(order);

        XMLTools.SaveListToXMLSerializer(listOrders, s_orders);

        return order.OrderId;
    }

    public void Delete(int id)
    {
        List<DO.Order?> listOrders = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_orders);

        if (listOrders.RemoveAll(ord => ord?.OrderId == id) == 0)
            throw new DO.NotExistException(id,"order"); 

        XMLTools.SaveListToXMLSerializer(listOrders, s_orders);
    }
    public void Update(DO.Order order)
    {
        Delete(order.OrderId);
        Add(order);
    }

}

