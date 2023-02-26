using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi
{
    public interface IOrder
    {
        public IEnumerable<BO.OrderForList> GetOrders();
        public BO.Order GetOrderDetails(int idOrder);
        public BO.Order UpdateSending(int id);
        public BO.Order supplyUpdate(int id);
        public OrderTracking OrderTracking(int id);
        void UpdateStatus(int orderId);
        int? GetOldestOrder();
    }
}
