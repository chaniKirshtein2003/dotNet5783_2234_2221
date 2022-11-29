using Dal;
using DalApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlImplementation
{
    internal class Cart:BlApi.ICart
    {
        IDal idal = new Dallist();
        public IEnumerable<BO.Cart> Add(BO.Cart cart,int id)
        {
            DO.Product Product = idal.Product.Get(id);
            IEnumerable<BO.OrderItem> orderItems=   
        }
       public IEnumerable<BO.Cart> Update(BO.Cart cart,int id,int amount)
        {
        }
        public void MakeAnOrder(Cart cart)
        {
        }
        public 
    }
}
