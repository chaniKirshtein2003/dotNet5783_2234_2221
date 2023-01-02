

namespace BlApi
{
    public interface IProduct
    {
        public IEnumerable<BO.ProductForList> GetProducts();
        public BO.Product GetProduct(int id);
        public int Add(BO.Product product);
        public void Delete(int id);
        public void Update(BO.Product product);
        public IEnumerable<BO.ProductItem> ListProductsToBuy();
        public BO.ProductItem ProductForBuyer(int idProduct);
        public IEnumerable<BO.ProductForList> GetProductsListByCategory(BO.Categories _category);
        public IEnumerable<BO.ProductItem> GetProductsItemByCategory(BO.Categories _category);
    }
}
