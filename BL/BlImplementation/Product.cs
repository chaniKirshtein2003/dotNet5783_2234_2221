using DalApi;
using System.Diagnostics;
using System.Xml.Linq;

namespace BlImplementation
{
    internal class Product : BlApi.IProduct
    {
        DalApi.IDal? idal = DalApi.Factory.Get();
        //The purpose of the function is to show the manager a list of products.
        public IEnumerable<BO.ProductForList> GetProducts()
        {
            IEnumerable<DO.Product?> products = idal!.Product.GetAll();
            var productsForList = from p in products
                                  select new BO.ProductForList
                                  {
                                      Name = p?.ProductName,
                                      Price = p?.Price ?? 0,
                                      Category = (BO.Categories?)p?.Category,
                                      ID = p?.ProductId ?? 0
                                  };
            return productsForList;
        }
        //The purpose of the function is to show the manager by product code the details of that product.
        public BO.Product GetProduct(int id)
        {
            try
            {
                DO.Product product = idal!.Product.Get(id);
                BO.Product newProduct = new BO.Product();
                newProduct.ProductName = product.ProductName;
                newProduct.Price = product.Price;
                newProduct.ProductId = product.ProductId;
                newProduct.Category = (BO.Categories?)product.Category;
                newProduct.AmountInStock = product.AmountInStock;
                return newProduct;
            }
            catch (DO.NotExistException x)
            {
                throw new BO.NotExistBlException("product not exist", x);
            }
        }
        public IEnumerable<BO.ProductForList> GetProductsListByCategory(BO.Categories _category)
        {
            IEnumerable<DO.Product?> products = idal!.Product.GetAll(x => x?.Category == (DO.Categories)_category);
            var productsForList = from product in products
                                  select new BO.ProductForList
                                  {
                                      Name = product?.ProductName,
                                      Price = product?.Price ?? 0,
                                      ID = product?.ProductId ?? 0,
                                      Category = (BO.Categories?)product?.Category
                                  };
            return productsForList;
        }
        //The purpose of the function is to get the product details from the user and add a new product with the product details.
        public int Add(BO.Product product)
        {
            if (product.ProductId < 0)
                throw new BO.NotValidException("Id must be positive");
            if (product.Price < 0)
                throw new BO.NotValidException("Price must be positive");
            if (product.AmountInStock < 0)
                throw new BO.NotValidException("AmountInStock must be positive");
            if (product.ProductName == "")
                throw new BO.NotValidException("Price must be positive");
            DO.Product newProduct = new DO.Product();
            newProduct.ProductName = product.ProductName;
            newProduct.Price = product.Price;
            newProduct.ProductId = product.ProductId;
            newProduct.AmountInStock = product.AmountInStock;
            newProduct.Category = (DO.Categories?)product.Category;
            try
            {
                int id = idal!.Product.Add(newProduct);
                return id;
            }
            catch (Exception x)
            {
                throw new BO.AlreadyExistBlException("exist", x);
            }
        }
        //The purpose of the function is to delete a product by product code.
        public void Delete(int idOrder)
        {
            var orderItemList = idal!.OrderItem.GetAll(x => x?.OrderId == idOrder);
            if(orderItemList != null)
                throw new BO.AlreadyExistBlException("The product is exist");
            try
            {
                idal!.Product.Delete(idOrder);
            }
            catch (DO.NotExistException x)
            {
                throw new BO.NotExistBlException("not exist", x);
            }
        }
        public void Dlete(int id)
        {
            var items = idal!.OrderItem.GetAll(x => x?.ProductId == id);
            if (items != null)
                throw new BO.AlreadyExistBlException("the product is exist in orders so it cannot delete");
        }

        //The purpose of the function is to update a product.
        public void Update(BO.Product product)
        {
            if (product.ProductId < 0)
                throw new BO.NotValidException("Id must be positive");
            if (product.Price < 0)
                throw new BO.NotValidException("Price must be positive");
            if (product.AmountInStock < 0)
                throw new BO.NotValidException("AmountInStock must be positive");
            if (product.ProductName == "")
                throw new BO.NotValidException("Price must be positive");
            try
            {
                DO.Product updateProduct = new DO.Product();
                updateProduct.ProductName = product.ProductName;
                updateProduct.Price = product.Price;
                updateProduct.ProductId = product.ProductId;
                updateProduct.AmountInStock = product.AmountInStock;
                updateProduct.Category = (DO.Categories?)product.Category;
                idal!.Product.Update(updateProduct);
            }
            catch (DO.NotExistException x)
            {
                throw new BO.NotExistBlException("not exist", x);
            }
        }

        //The purpose of the function is to show the buyer a list of products.
        public IEnumerable<BO.ProductItem> ListProductsToBuy()
        {
            IEnumerable<DO.Product?> products = idal!.Product.GetAll();
            var productList = from product in products
                              select new BO.ProductItem
                              {
                                  ID = product?.ProductId ?? 0,
                                  Name = product?.ProductName,
                                  Price = product?.Price ?? 0,
                                  Category = (BO.Categories?)product?.Category,
                                  InStock = product?.AmountInStock > 0 ? true : false,
                                  Amount = product?.AmountInStock ?? 0
                              };
            return productList;
        }
        //The purpose of the function is to present the product details to the buyer according to the product code.
        public BO.ProductItem ProductForBuyer(int idProduct)
        {
            DO.Product product;
            try
            {
                product = idal!.Product.Get(idProduct);
                BO.ProductItem newProduct = new BO.ProductItem();
                newProduct.ID = idProduct;
                newProduct.Name = product.ProductName;
                newProduct.Price = product.Price;
                newProduct.Amount = product.AmountInStock;
                newProduct.Category = (BO.Categories?)product.Category;
                newProduct.InStock = product.AmountInStock > 0 ? true : false;
                return newProduct;
            }
            catch (DO.NotExistException x)
            {
                throw new BO.NotExistBlException("not exist", x);
            }
        }
        public IEnumerable<BO.ProductItem> GetProductsItemByCategory(BO.Categories _category)
        {
            return ListProductsToBuy().ToList().FindAll(x => x?.Category == _category);
        }
    }
}
