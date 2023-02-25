using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal;
using DalApi;
using DO;

internal class OrderItem : IOrderItem
{
    const string s_orderItem = @"OrderItem";

    public int Add(DO.OrderItem orderItem)
    {
        orderItem.OrderItemId = Config.NextOrderItemNumber();
        List<DO.OrderItem?> listOrderItems = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_orderItem);

        if (listOrderItems.FirstOrDefault(ordItem => ordItem?.OrderItemId == orderItem.OrderItemId) != null)
            throw new DO.ExistException(orderItem.OrderItemId, "orderItem");

        listOrderItems.Add(orderItem);

        XMLTools.SaveListToXMLSerializer(listOrderItems, s_orderItem);

        return orderItem.OrderItemId;
    }

    public void Delete(int id)
    {
        List<DO.OrderItem?> listOrderItem = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_orderItem);

        if (listOrderItem.RemoveAll(ordItem => ordItem?.OrderItemId == id) == 0)
            throw new DO.NotExistException(id, "orderItem");

        XMLTools.SaveListToXMLSerializer(listOrderItem, s_orderItem);
    }

    public DO.OrderItem Get(int id)
    {
        List<DO.OrderItem?> listOrderItem = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_orderItem);

        return listOrderItem.FirstOrDefault(ordItem => ordItem?.OrderItemId == id) ??
            throw new DO.NotExistException(id, "orderItem");
    }

    public IEnumerable<DO.OrderItem?> GetAll(Func<DO.OrderItem?, bool>? check = null)
    {
        List<DO.OrderItem?> listOrderItem = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_orderItem);

        if (check == null)
            return listOrderItem.Select(ordItem => ordItem).OrderBy(ordItem => ordItem?.OrderItemId);
        else
            return listOrderItem.Where(check).OrderBy(ordItem => ordItem?.OrderItemId);
    }

    public DO.OrderItem? GetByCondition(Func<DO.OrderItem?, bool>? check)
    {
        List<DO.OrderItem?> listOrderItem = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_orderItem);

        return listOrderItem.Select(ordItem => ordItem).FirstOrDefault() ?? throw new DO.NotExistException(0, "ordItem");
    }

    public DO.OrderItem GetItemById(int product_id, int order_id)
    {
        List<DO.OrderItem?> listOrderItem = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_orderItem);
        return listOrderItem.FindAll(ordItem => ordItem?.OrderId == order_id && ordItem?.ProductId == product_id).FirstOrDefault() ?? throw new DO.NotExistException(product_id, "orderItem");
    }

    public void Update(DO.OrderItem updateObject)
    {
        Delete(updateObject.OrderItemId);
        Add(updateObject);
    }
}

