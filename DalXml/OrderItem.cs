using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Dal;
using DalApi;
using System.Runtime.CompilerServices;

internal class OrderItem : IOrderItem
{
    const string s_orderItem = @"OrderItem";

    [MethodImpl(MethodImplOptions.Synchronized)]
    public int Add(DO.OrderItem orderItem)
    {
        orderItem.OrderItemId = Config.GetNextOrderItemNumber();
        Config.SaveNextOrderItemNumber((orderItem.OrderItemId) + 1);
        List<DO.OrderItem?> listOrderItems = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_orderItem);
        listOrderItems.Add(orderItem);

        XMLTools.SaveListToXMLSerializer(listOrderItems, s_orderItem);

        return orderItem.OrderItemId;
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Delete(int id)
    {
        List<DO.OrderItem?> listOrderItem = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_orderItem);

        if (listOrderItem.RemoveAll(ordItem => ordItem?.OrderItemId == id) == 0)
            throw new DO.NotExistException(id, "orderItem");

        XMLTools.SaveListToXMLSerializer(listOrderItem, s_orderItem);
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public DO.OrderItem Get(int id)
    {
        List<DO.OrderItem?> listOrderItem = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_orderItem);

        return listOrderItem.FirstOrDefault(ordItem => ordItem?.OrderItemId == id) ??
            throw new DO.NotExistException(id, "orderItem");
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<DO.OrderItem?> GetAll(Func<DO.OrderItem?, bool>? check = null)
    {
        List<DO.OrderItem?> listOrderItem = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_orderItem);

        if (check == null)
            return listOrderItem.Select(ordItem => ordItem).OrderBy(ordItem => ordItem?.OrderItemId);
        else
            return listOrderItem.Where(check).OrderBy(ordItem => ordItem?.OrderItemId);
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public DO.OrderItem? GetByCondition(Func<DO.OrderItem?, bool>? check)
    {
        List<DO.OrderItem?> listOrderItem = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_orderItem);

        return listOrderItem.Select(ordItem => ordItem).FirstOrDefault() ?? throw new DO.NotExistException(0, "ordItem");
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public DO.OrderItem GetItemById(int product_id, int order_id)
    {
        List<DO.OrderItem?> listOrderItem = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_orderItem);
        return listOrderItem.FindAll(ordItem => ordItem?.OrderId == order_id && ordItem?.ProductId == product_id).FirstOrDefault() ?? throw new DO.NotExistException(product_id, "orderItem");
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Update(DO.OrderItem updateObject)
    {
        Delete(updateObject.OrderItemId);
        Add(updateObject);
    }
}

