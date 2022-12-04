using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi
{
    public interface IProduct
    {
        public IEnumerable<BO.ProductForList> GetProducts();
        public BO.Product GetProduct(int id);
        public void Delete(int id);
        public void Update(BO.Product product);
        public int Add(BO.Product product);
        public IEnumerable<BO.ProductItem> ListProductsToBuy();
        public void ProductForBuyer(int idProduct);

    }
}
