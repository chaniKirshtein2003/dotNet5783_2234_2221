using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi
{
    public interface IProduct
    {
        public IEnumerable<BO.ProductForList> GetProductForList();
        public BO.Product GetProductById(int id);
        public void Delete(int id);
        public void Update(BO.Product product);
    }
}
