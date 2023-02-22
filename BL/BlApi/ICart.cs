using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi
{
    public interface ICart
    {
        public BO.Cart Add(BO.Cart cart, int id);
        public BO.Cart Update(BO.Cart cart, int id, int amount);
        public void MakeAnOrder(BO.Cart cart);
        public BO.Cart UpdateAmount(BO.Cart cart, int id, int amount);
    }
}
