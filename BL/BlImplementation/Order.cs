using Dal;
using DalApi;
namespace BlImplementation
{
    internal class Order:BlApi.IOrder
    {
        IDal idal = new Dallist();
        public IEnumerable<BO.OrderForList> GetOrders()
        {
            List<BO.OrderForList> orderForList = new List<BO.OrderForList>();
            BO.OrderForList order;
            foreach (DO.Order item in idal.Order.GetAll())
            {
                order = new BO.OrderForList();
                order.ID = item.orderId;
                order.customerName = item.customerName;
                order.totalPrice = idal.OrderItem.GetAllProductsOfOrder(item.orderId).Sum(x => x.pricePerUnit);
                order.amountOfItems = idal.OrderItem.GetAllProductsOfOrder(item.orderId).Sum(x => x.amount);
                //order.status = 
                orderForList.Add(order);
            }
            return orderForList;
        }
        public IEnumerable<Order> GetOrder(int idOrder)
        {

        }
    }
}
