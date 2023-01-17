namespace BlImplementation
{
    internal class Order : BlApi.IOrder
    {
        DalApi.IDal? idal = DalApi.Factory.Get();

        //The purpose of the function is to show the manager all orders.
        public IEnumerable<BO.OrderForList?> GetOrders()
        {
            try
            {
                IEnumerable<DO.Order?> orders = idal!.Order.GetAll();
                IEnumerable<BO.OrderForList?> ordersForList = from order in orders
                                    let orderItems = idal!.OrderItem.GetAll(orderItem => orderItem?.OrderId == (order?.OrderId ?? 0))
                                    let totalAmount = orderItems.Sum(x => ((DO.OrderItem)x!).Amount)
                                    let totalPrice = orderItems.Sum(x => ((DO.OrderItem)x!).Amount * ((DO.OrderItem)x!).PricePerUnit)
                                    select new BO.OrderForList
                                    {
                                        ID = ((DO.Order)order).OrderId,
                                        CustomerName = ((DO.Order)order).CustomerName,
                                        TotalPrice = totalPrice,
                                        AmountOfItems = totalAmount,
                                        Status = ((DO.Order)order).DeliveryDate != null ?
                                                 BO.OrderStatus.delivered : ((DO.Order)order).ShipDate != null ?
                                                 BO.OrderStatus.sent : BO.OrderStatus.approved
                                    };
                return ordersForList;
            }
            catch (DO.NotExistException ex)
            {
                throw new BO.NotExistBlException("order does not exist", ex);
            }
        }

        //The purpose of the function is to display order details including customer details and details of all its products with product names and prices.
        public BO.Order GetOrderDetails(int idOrder)
        {
            if (idOrder < 0)
                throw new BO.NotValidException("id");
            try
            {
                DO.Order DOorder = idal!.Order.Get(idOrder);
                BO.Order order = new BO.Order();
                order.OrderId = DOorder.OrderId;
                order.CustomerName = DOorder.CustomerName;
                order.CustomerAddress = DOorder.CustomerAddress;
                order.CustomerEmail = DOorder.CustomerEmail;
                order.OrderDate = DOorder.OrderDate;
                order.ShipDate = DOorder.ShipDate;
                order.DeliveryDate = DOorder.DeliveryDate;
                IEnumerable<DO.OrderItem?> orderItems = idal!.OrderItem.GetAll(orderItem => orderItem?.OrderId == idOrder);
                IEnumerable<BO.OrderItem> items = from orderItem in orderItems
                            select new BO.OrderItem
                            {
                                OrderItemId = orderItem?.OrderItemId ?? 0,
                                ProductId = orderItem?.ProductId ?? 0,
                                OrderItemName = idal.Product.Get(orderItem?.ProductId ?? 0).ProductName,
                                Amount = orderItem?.Amount ?? 0,
                                Price = orderItem?.PricePerUnit ?? 0,
                                TotalPrice = orderItem?.PricePerUnit * orderItem?.Amount ?? 0
                            };
                order.Items = items.ToList()!;
                order.TotalPrice = items.Sum(x => x.TotalPrice);
                order.Status = order.DeliveryDate != null ?
                                BO.OrderStatus.delivered : order.ShipDate != null ?
                                BO.OrderStatus.sent : BO.OrderStatus.approved;
                return order;
            }
            catch (DO.NotExistException ex)
            {
                throw new BO.NotExistBlException("not exist", ex);
            }
        }
        //The purpose of the function is to allow the manager to update that the order has been sent to the customer.
        public BO.Order UpdateSending(int id)
        {
            try
            {
                DO.Order order = idal!.Order.Get(id);
                if (order.ShipDate?.Date == null)
                {
                    order.ShipDate = DateTime.Now;
                    idal!.Order.Update(order);
                }
                BO.Order newOrder = GetOrderDetails(id);
                return newOrder;
            }
            catch (DO.NotExistException ex)
            {
                throw new BO.NotExistBlException("not exist", ex);
            }
        }
        //The purpose of the function is to allow the manager to update that the order has been delivered to the customer.
        public BO.Order supplyUpdate(int id)
        {
            DO.Order order;
            try
            {
                order = idal!.Order.Get(id);
            }
            catch (Exception ex)
            {
                throw new BO.NotExistBlException("not exist", ex);
            }
            if (order.ShipDate == null)
                throw new Exception("Delivery date can not be updated before shipping date");

            if (order.DeliveryDate?.Date == null)
            {
                order.DeliveryDate = DateTime.Now;
                idal!.Order.Update(order);
            }
            BO.Order newOrder = GetOrderDetails(id);
            return newOrder;
        }
        //The purpose of the function is to allow the manager to track the status of the order.
        public BO.OrderTracking OrderTracking(int id)
        {
            try
            {
                DO.Order DOorder = idal!.Order.Get(id);
                BO.OrderTracking OrderTrack = new BO.OrderTracking();
                OrderTrack.ID = DOorder.OrderId;
                if (DOorder.DeliveryDate != null)
                    OrderTrack.Status = BO.OrderStatus.delivered;
                else if (DOorder.ShipDate != null)
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
                else if (DOorder.ShipDate != null)
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
