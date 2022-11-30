using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using DalApi;

namespace BlImplementation
{
    sealed public class Bl : IBl
    {
        public BlApi.IOrder Order => new Order();
        public ICart Cart => new Cart();
        public BlApi.IProduct Product => new Product();

    }
}
