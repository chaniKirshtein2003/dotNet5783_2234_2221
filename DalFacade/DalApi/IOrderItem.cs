using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi
{
    public interface IOrderItem:ICrud<OrderItem>
    {
        public OrderItem GetItemById(int idProduct, int idOrder);
        public IEnumerable<OrderItem?> GetOrderItems(int idOrder);
    }
}
