

namespace BlImplementation
{
    internal class Order : BlApi.IOrder
    {
        DalApi.IDal? idal = DalApi.Factory.Get();
        //The purpose of the function is to show the manager all orders.
        public IEnumerable<BO.OrderForList?> GetOrders()
        {
            List<BO.OrderForList?> orderForList = new List<BO.OrderForList?>();
            BO.OrderForList order;
            double totalPrice = 0;
            int totalAmount = 0;
            foreach (DO.Order? item in idal!.Order.GetAll())
            {
                try
                {
                    foreach (DO.OrderItem? orderItem in idal!.OrderItem.GetOrderItems(item?.OrderId??0))
                    {
                        totalAmount += orderItem?.Amount??0;
                        totalPrice += orderItem?.PricePerUnit??0;
                    }
                    order = new BO.OrderForList();
                    order.ID = item?.OrderId??0;
                    order.CustomerName = item?.CustomerName;
                    order.TotalPrice = totalPrice;
                    order.AmountOfItems = totalAmount;
                    order.Status = item?.DeliveryDate != DateTime.MinValue ? BO.OrderStatus.delivered : item?.ShipDate != DateTime.MinValue ? BO.OrderStatus.sent : BO.OrderStatus.approved;
                    orderForList.Add(order);
                }
                catch (Exception ex)
                {
                    throw new BO.NotExistBlException("not exist", ex);
                }
            }
            return orderForList;
        }
        //The purpose of the function is to display order details including customer details and details of all its products with product names and prices.
        public BO.Order GetOrderDetails(int idOrder)
        {
            if (idOrder < 0)
                throw new BO.NotValidException("id");
            BO.Order order = new BO.Order();
            DO.Order DOorder;
            try
            {
                DOorder = idal!.Order.Get(idOrder);
                order.OrderId = DOorder.OrderId;
                order.CustomerName = DOorder.CustomerName;
                order.CustomerAddress = DOorder.CustomerAddress;
                order.CustomerEmail = DOorder.CustomerEmail;
                order.OrderDate = DOorder.OrderDate;
                order.ShipDate = DOorder.ShipDate;
                order.DeliveryDate = DOorder.DeliveryDate;
                order.Items = new List<BO.OrderItem?>();
                BO.OrderItem orderItem = new BO.OrderItem();
                foreach (DO.OrderItem? DOorderItem in idal!.OrderItem.GetOrderItems(idOrder))
                {
                    orderItem.OrderItemId = DOorderItem?.OrderItemId??0;
                    orderItem.ProductId = DOorderItem?.ProductId??0;
                    orderItem.OrderItemName = idal.Product.Get(DOorderItem?.ProductId??0).ProductName;
                    orderItem.Amount = DOorderItem?.Amount??0;
                    orderItem.Price = DOorderItem?.PricePerUnit??0;
                    orderItem.TotalPrice = DOorderItem?.PricePerUnit * DOorderItem?.Amount??0;
                    order.Items.Add(orderItem);
                }
                return order;
            }
            catch (Exception X)
            {
                throw new BO.NotExistBlException("not exist", X);
            }
        }
        //The purpose of the function is to allow the manager to update that the order has been sent to the customer.
        public BO.Order UpdateSending(int id)
        {
            try
            {
                DO.Order order = idal!.Order.Get(id);
                if (order.ShipDate?.Date < DateTime.Now.Date)
                {
                    order.ShipDate = DateTime.Now;
                    idal!.Order.Update(order);
                }
                BO.Order newOrder = GetOrderDetails(id);
                newOrder.Status = BO.OrderStatus.sent;
                return newOrder;
            }
            catch (Exception ex)
            {
                throw new BO.NotExistBlException("not exist", ex);
            }
        }
        //The purpose of the function is to allow the manager to update that the order has been delivered to the customer.
        public BO.Order supplyUpdate(int id)
        {
            try
            {
                DO.Order order = idal!.Order.Get(id);
                if (order.DeliveryDate?.Date < DateTime.Now.Date)
                {
                    order.DeliveryDate = DateTime.Now;
                    idal!.Order.Update(order);
                }
                BO.Order newOrder = GetOrderDetails(id);
                newOrder.Status = BO.OrderStatus.delivered;
                return newOrder;
            }
            catch (Exception ex)
            {
                throw new BO.NotExistBlException("not exist", ex);
            }
        }
        //The purpose of the function is to allow the manager to track the status of the order.
        public BO.OrderTracking OrderTracking(int id)
        {
            try
            {
                DO.Order DOorder = idal!.Order.Get(id);
                BO.OrderTracking OrderTrack = new BO.OrderTracking();
                OrderTrack.ID = DOorder.OrderId;
                if (DOorder.DeliveryDate != DateTime.MinValue)
                    OrderTrack.Status = BO.OrderStatus.delivered;
                else if (DOorder.ShipDate != DateTime.MinValue)
                    OrderTrack.Status = BO.OrderStatus.sent;
                else
                    OrderTrack.Status = BO.OrderStatus.approved;
                OrderTrack.Tracking = new List<Tuple<DateTime?, string?>>();
                Tuple<DateTime?, string?> tuple = new Tuple<DateTime?, string?>(DOorder.OrderDate, "approved");
                OrderTrack.Tracking.Add(tuple);
                if (DOorder.DeliveryDate != null)
                {
                    tuple = new Tuple<DateTime?, string?>(DOorder.DeliveryDate, "delivered");
                    OrderTrack.Tracking.Add(tuple);
                }
                else if (DOorder.ShipDate != DateTime.MinValue)
                {
                    tuple = new Tuple<DateTime?, string?>(DOorder.ShipDate, "sent");
                    OrderTrack.Tracking.Add(tuple);
                }
                return OrderTrack;
            }
            catch (Exception x)
            {
                throw new BO.NotExistBlException("not exist", x);
            }
        }
    }
}
