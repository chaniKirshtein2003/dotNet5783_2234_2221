using BO;
using Dal;
using DalApi;
namespace BlImplementation
{
    internal class Order : BlApi.IOrder
    {
        IDal idal = new Dallist();
        //The purpose of the function is to show the manager all orders.
        public IEnumerable<BO.OrderForList> GetOrders()
        {
            List<BO.OrderForList> orderForList = new List<BO.OrderForList>();
            BO.OrderForList order;
            double totalPrice = 0;
            int totalAmount = 0;
            foreach (DO.Order item in idal.Order.GetAll())
            {
                foreach (DO.OrderItem orderItem in idal.OrderItem.GetOrderItems(item.OrderId))
                {
                    totalAmount += orderItem.Amount;
                    totalPrice += orderItem.PricePerUnit;
                }
                order = new BO.OrderForList();
                order.ID = item.OrderId;
                order.CustomerName = item.CustomerName;
                order.TotalPrice = totalPrice;
                order.AmountOfItems = totalAmount;
                order.Status = item.DeliveryDate != DateTime.MinValue ? OrderStatus.delivered : item.ShipDate != DateTime.MinValue ? OrderStatus.sent : OrderStatus.approved;
                orderForList.Add(order);
            }
            return orderForList;
        }
        //The purpose of the function is to display order details including customer details and details of all its products with product names and prices.
        public BO.Order GetOrderDetails(int idOrder)
        {
            if (idOrder < 0)
                throw new Exception("wrong ID");
            BO.Order order = new BO.Order();
            DO.Order DOorder;
            try
            {
                DOorder = idal.Order.Get(idOrder);
                order.OrderId = DOorder.OrderId;
                order.CustomerName = DOorder.CustomerName;
                order.CustomerAddress = DOorder.CustomerAddress;
                order.CustomerEmail = DOorder.CustomerEmail;
                order.OrderDate = DOorder.OrderDate;
                order.ShipDate = DOorder.ShipDate;
                order.DeliveryDate = DOorder.DeliveryDate;
                order.Items = new List<OrderItem>();
                BO.OrderItem orderItem = new BO.OrderItem();
                foreach (DO.OrderItem DOorderItem in idal.OrderItem.GetOrderItems(idOrder))
                {
                    orderItem.OrderItemId = DOorderItem.OrderItemId;
                    orderItem.ProductId = DOorderItem.ProductId;
                    orderItem.OrderItemName = idal.Product.Get(DOorderItem.ProductId).ProductName;
                    orderItem.Amount = DOorderItem.Amount;
                    orderItem.Price = DOorderItem.PricePerUnit;
                    orderItem.TotalPrice = DOorderItem.PricePerUnit * DOorderItem.Amount;
                    order.Items.Add(orderItem);
                }
                return order;
            }
            catch (Exception)
            {
                throw new Exception("Product request failed");
            }
        }
        //The purpose of the function is to allow the manager to update that the order has been sent to the customer.
        public BO.Order UpdateSending(int id)
        {
            DO.Order order = idal.Order.Get(id);
            if (order.ShipDate.Date < DateTime.Now.Date)
            {
                order.ShipDate = DateTime.Now;
                idal.Order.Update(order);
            }
            BO.Order newOrder = GetOrderDetails(id);
            newOrder.Status = OrderStatus.sent;
            return newOrder;
        }
        //The purpose of the function is to allow the manager to update that the order has been delivered to the customer.
        public BO.Order supplyUpdate(int id)
        {
            DO.Order order = idal.Order.Get(id);
            if (order.DeliveryDate.Date < DateTime.Now.Date)
            {
                order.DeliveryDate = DateTime.Now;
                idal.Order.Update(order);
            }
            BO.Order newOrder = GetOrderDetails(id);
            newOrder.Status = OrderStatus.delivered;
            return newOrder;
        }
        //The purpose of the function is to allow the manager to track the status of the order.
        public OrderTracking OrderTracking(int id)
        {
            try
            {
                DO.Order DOorder = idal.Order.Get(id);
                OrderTracking OrderTrack = new OrderTracking();
                OrderTrack.ID = DOorder.OrderId;
                if (DOorder.DeliveryDate != DateTime.MinValue)
                    OrderTrack.Status = OrderStatus.delivered;
                else if (DOorder.ShipDate != DateTime.MinValue)
                    OrderTrack.Status = OrderStatus.sent;
                else
                    OrderTrack.Status = OrderStatus.approved;
                OrderTrack.Tracking = new List<Tuple<DateTime, string>>();
                Tuple<DateTime, string> tuple = new Tuple<DateTime, string>(DOorder.OrderDate, "approved");
                OrderTrack.Tracking.Add(tuple);
                if (DOorder.DeliveryDate != DateTime.MinValue)
                {
                    tuple = new Tuple<DateTime, string>(DOorder.DeliveryDate, "delivered");
                    OrderTrack.Tracking.Add(tuple);
                }
                else if (DOorder.ShipDate != DateTime.MinValue)
                {
                    tuple = new Tuple<DateTime, string>(DOorder.ShipDate, "sent");
                    OrderTrack.Tracking.Add(tuple);
                }
                return OrderTrack;
            }
            catch (Exception)
            {
                throw new Exception("order not exist");
            }
        }
    }
}
