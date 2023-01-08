namespace BlImplementation
{
    internal class Order : BlApi.IOrder
    {
        DalApi.IDal? idal = DalApi.Factory.Get();
        //The purpose of the function is to show the manager all orders.

        //הפונקציה של שולה וזיסי!!!
        //public IEnumerable<BO.OrderForList> GetOrders()
        //{
        //    try
        //    {
        //        IEnumerable<DO.Order?> orders = idal!.Order.GetAll();
        //        var ordersForList = from order in orders
        //                            let orderItems = idal.OrderItem.GetAll(orditem => orditem?.OrderId == order?.ID)
        //                            let amount = orderItems.Sum(o => ((DO.OrderItem)o!).Amount)
        //                            let totalPrice = orderItems.Sum(o => ((DO.OrderItem)o!).Amount * ((DO.OrderItem)o!).Price)
        //                            select new BO.OrderForList
        //                            {
        //                                ID = ((DO.Order)order!).ID,
        //                                CustomerName = ((DO.Order)order!).CustomerName,
        //                                AmountOfItems = amount,
        //                                TotalPrice = totalPrice,
        //                                Status = (((DO.Order)order!).DeliveryDate != null && ((DO.Order)order!).DeliveryDate < DateTime.Now) ?
        //                             BO.OrderStatus.provided : ((DO.Order)order!).ShipDate != null && ((DO.Order)order!).ShipDate < DateTime.Now ?
        //                             BO.OrderStatus.sent : BO.OrderStatus.approved
        //                            };
        //        return ordersForList;
        //    }
        //    catch (DO.NotExistException ex)
        //    {
        //        throw new BO.NotExistBlException("order doesnot exist", ex);
        //    }
        //}
        public IEnumerable<BO.OrderForList?> GetOrders()
        {
            List<BO.OrderForList?> orderForList = new List<BO.OrderForList?>();
            BO.OrderForList? order;
            double totalPrice = 0;
            int totalAmount = 0;
            foreach (DO.Order? item in idal!.Order.GetAll())
            {
                try
                {
                    foreach (DO.OrderItem? orderItem in idal!.OrderItem.GetAll(x => x?.OrderId == (item?.OrderId ?? 0)))
                    {
                        totalAmount += orderItem?.Amount??0;
                        totalPrice += orderItem?.PricePerUnit??0;
                    }
                    order = new BO.OrderForList();
                    order.ID = item?.OrderId??0;
                    order.CustomerName = item?.CustomerName;
                    order.TotalPrice = totalPrice;
                    order.AmountOfItems = totalAmount;
                    order.Status = item?.DeliveryDate != null ? BO.OrderStatus.delivered : item?.ShipDate != null ? BO.OrderStatus.sent : BO.OrderStatus.approved;
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
            double totalPrice = 0;
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

                foreach (DO.OrderItem? DOorderItem in idal!.OrderItem.GetAll(x => x?.OrderId == idOrder))
                {
                    orderItem = new BO.OrderItem();
                    orderItem.OrderItemId = DOorderItem?.OrderItemId ?? 0;
                    orderItem.ProductId = DOorderItem?.ProductId ?? 0;
                    orderItem.OrderItemName = idal.Product.Get(DOorderItem?.ProductId ?? 0).ProductName;
                    orderItem.Amount = DOorderItem?.Amount ?? 0;
                    orderItem.Price = DOorderItem?.PricePerUnit ?? 0;
                    orderItem.TotalPrice = DOorderItem?.PricePerUnit * DOorderItem?.Amount ?? 0;
                    totalPrice += orderItem.TotalPrice;
                    order.Items.Add(orderItem);
                }
                order.TotalPrice = totalPrice;
                order.Status = order.DeliveryDate != null ? BO.OrderStatus.delivered : order.ShipDate != null ? BO.OrderStatus.sent : BO.OrderStatus.approved;
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
                if (order.ShipDate?.Date ==null)
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
                if (order.DeliveryDate?.Date ==null)
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
