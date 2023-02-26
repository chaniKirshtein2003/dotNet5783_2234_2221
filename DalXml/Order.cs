using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Dal;
using DalApi;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

internal class Order : IOrder
{
    const string s_orders = @"Order"; //XML Serializer

    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<DO.Order?> GetAll(Func<DO.Order?, bool>? filter = null)
    {
        List<DO.Order?> listOrders = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_orders);

        //if (filter == null)
        //    return listOrders.Select(ord => ord).OrderBy(ord => ord?.OrderId);
        //else
        //    return listOrders.Where(filter).OrderBy(ord => ord?.OrderId);


        if (filter != null)
            return listOrders.FindAll(x => filter(x));
        else
            return from ord in listOrders
                   select ord;
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public DO.Order Get(int id)
    {
        List<DO.Order?> listOrders = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_orders);
        return listOrders.FirstOrDefault(ord => 
        ord?.OrderId == id) ??
            throw new DO.NotExistException(id,"order");
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public DO.Order? GetByCondition(Func<DO.Order?, bool>? check)
    {
        List<DO.Order?> listOrders = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_orders);
        return listOrders.Select(ord => ord).FirstOrDefault() ?? throw new DO.NotExistException(0, "order");
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public int Add(DO.Order order)
    {
        order.OrderId = Config.GetNextOrderNumber();
        Config.SaveNextOrderNumber((order.OrderId)+1);
        List<DO.Order?> listOrders = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_orders);
        listOrders.Add(order);

        XMLTools.SaveListToXMLSerializer(listOrders, s_orders);

        return order.OrderId;
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Delete(int id)
    {
        List<DO.Order?> listOrders = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_orders);

        if (listOrders.RemoveAll(ord => ord?.OrderId == id) == 0)
            throw new DO.NotExistException(id,"order"); 

        XMLTools.SaveListToXMLSerializer(listOrders, s_orders);
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Update(DO.Order order)
    {
        List<DO.Order?> listOrders = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_orders);
        int num= listOrders.RemoveAll(x => x?.OrderId == order.OrderId);
        if(num==0)
            throw new DO.NotExistException(order.OrderId,"order");
        listOrders.Add(order);
        XMLTools.SaveListToXMLSerializer(listOrders, s_orders);
    }
}

