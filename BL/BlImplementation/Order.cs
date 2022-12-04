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
                foreach (DO.OrderItem orderItem in idal.OrderItem.GetOrderItems(item.orderId))
                {
                    totalAmount += orderItem.amount;
                    totalPrice += orderItem.pricePerUnit;
                }
                order = new BO.OrderForList();
                order.ID = item.orderId;
                order.customerName = item.customerName;
                order.totalPrice = totalPrice;
                order.amountOfItems = totalAmount;
                order.status = item.deliveryDate != DateTime.MinValue ? OrderStatus.delivered : item.shipDate != DateTime.MinValue ? OrderStatus.sent : OrderStatus.approved;
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
                order.orderId = DOorder.orderId;
                order.customerName = DOorder.customerName;
                order.customerAddress = DOorder.customerAddress;
                order.customerEmail = DOorder.customerEmail;
                order.orderDate = DOorder.orderDate;
                order.shipDate = DOorder.shipDate;
                order.deliveryDate = DOorder.deliveryDate;
                order.items = new List<OrderItem>();
                BO.OrderItem orderItem = new BO.OrderItem();
                foreach (DO.OrderItem DOorderItem in idal.OrderItem.GetOrderItems(idOrder))
                {
                    orderItem.orderItemId = DOorderItem.orderItemId;
                    orderItem.productId = DOorderItem.productId;
                    orderItem.orderItemName = idal.Product.Get(DOorderItem.productId).productName;
                    orderItem.amount = DOorderItem.amount;
                    orderItem.price = DOorderItem.pricePerUnit;
                    orderItem.totalPrice = DOorderItem.pricePerUnit * DOorderItem.amount;
                    order.items.Add(orderItem);
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
            if (order.shipDate.Date < DateTime.Now.Date)
            {
                order.shipDate = DateTime.Now;
                idal.Order.Update(order);
            }
            BO.Order newOrder = GetOrderDetails(id);
            newOrder.status = OrderStatus.sent;
            return newOrder;
        }
        //The purpose of the function is to allow the manager to update that the order has been delivered to the customer.
        public BO.Order supplyUpdate(int id)
        {
            DO.Order order = idal.Order.Get(id);
            if (order.deliveryDate.Date < DateTime.Now.Date)
            {
                order.deliveryDate = DateTime.Now;
                idal.Order.Update(order);
            }
            BO.Order newOrder = GetOrderDetails(id);
            newOrder.status = OrderStatus.delivered;
            return newOrder;
        }
        //The purpose of the function is to allow the manager to track the status of the order.
        public OrderTracking OrderTracking(int id)
        {
            try
            {
                DO.Order DOorder = idal.Order.Get(id);
                OrderTracking OrderTrack = new OrderTracking();
                OrderTrack.ID = DOorder.orderId;
                if (DOorder.deliveryDate != DateTime.MinValue)
                    OrderTrack.status = OrderStatus.delivered;
                else if (DOorder.shipDate != DateTime.MinValue)
                    OrderTrack.status = OrderStatus.sent;
                else
                    OrderTrack.status = OrderStatus.approved;
                OrderTrack.Tracking = new List<Tuple<DateTime, string>>();
                Tuple<DateTime, string> tuple = new Tuple<DateTime, string>(DOorder.orderDate, "approved");
                OrderTrack.Tracking.Add(tuple);
                if (DOorder.deliveryDate != DateTime.MinValue)
                {
                    tuple = new Tuple<DateTime, string>(DOorder.deliveryDate, "delivered");
                    OrderTrack.Tracking.Add(tuple);
                }
                else if (DOorder.shipDate != DateTime.MinValue)
                {
                    tuple = new Tuple<DateTime, string>(DOorder.shipDate, "sent");
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
