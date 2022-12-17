
using DalApi;
using Dal;
using DO;

namespace BlImplementation
{
    internal class Product : BlApi.IProduct
    {
        IDal idal = new Dallist();
        //The purpose of the function is to show the manager a list of products.
        public IEnumerable<BO.ProductForList?> GetProducts()
        {
            List<BO.ProductForList> products = new List<BO.ProductForList>();
            foreach (DO.Product item in idal.Product.GetAll())
            {
                BO.ProductForList product = new BO.ProductForList();
                product.Name = item.ProductName;
                product.Price = item.Price;
                product.Category = (BO.Categories)item.Category;
                product.ID = item.ProductId;
                products.Add(product);
            }
            return products;
        }
        //The purpose of the function is to show the manager by product code the details of that product.
        public BO.Product GetProduct(int id)
        {
            DO.Product product = idal.Product.Get(id);
            BO.Product newProduct = new BO.Product();
            newProduct.ProductName = product.ProductName;
            newProduct.Price = product.Price;
            newProduct.ProductId = product.ProductId;
            newProduct.Category = (BO.Categories)product.Category;
            newProduct.AmountInStock = product.AmountInStock;
            return newProduct;
        }
        public IEnumerable<BO.ProductForList> GetProductsListByCategory(BO.Categories _category)
        {
            IEnumerable<BO.ProductForList> products = GetProducts().Where(x => x.Category == _category);
            return products;
        }
        //The purpose of the function is to get the product details from the user and add a new product with the product details.
        public int Add(BO.Product product)
        {
            if (product.ProductId < 0)
                throw new ArgumentException("Id must be positive");
            if (product.Price < 0)
                throw new ArgumentException("Price must be positive");
            if (product.AmountInStock < 0)
                throw new ArgumentException("AmountInStock must be positive");
            if (product.ProductName == "")
                throw new ArgumentException("Price must be positive");
            DO.Product newProduct = new DO.Product();
            newProduct.ProductName = product.ProductName;
            newProduct.Price = product.Price;
            newProduct.ProductId = product.ProductId;
            newProduct.AmountInStock = product.AmountInStock;
            newProduct.Category = (DO.Categories)product.Category;
            int id = idal.Product.Add(newProduct);
            return id;
        }
        //The purpose of the function is to delete a product by product code.
        public void Delete(int id)
        {
            IEnumerable<DO.OrderItem?> orderItemList;
            IEnumerable<DO.Order?> orderList = idal.Order.GetAll();
            foreach (var order in orderList)
            {
                orderItemList = idal.OrderItem.GetOrderItems(order?.OrderId??throw new Exception());
                foreach (DO.OrderItem item in orderItemList)
                    if (item.OrderItemId == id)
                        throw new ExistException(id,"the product is exist");
            }
            idal.Product.Delete(id);
        }
        //The purpose of the function is to update a product.
        public void Update(BO.Product product)
        {
            if (product.ProductId < 0)
                throw new Exception("Id must be positive");
            if (product.Price < 0)
                throw new Exception("Price must be positive");
            if (product.AmountInStock < 0)
                throw new Exception("AmountInStock must be positive");
            if (product.ProductName == "")
                throw new Exception("Price must be positive");
            DO.Product updateProduct = new DO.Product();
            updateProduct.ProductName = product.ProductName;
            updateProduct.Price = product.Price;
            updateProduct.ProductId = product.ProductId;
            updateProduct.AmountInStock = product.AmountInStock;
            updateProduct.Category = (DO.Categories)product.Category;
            idal.Product.Update(updateProduct);
        }
        //The purpose of the function is to show the buyer a list of products.
        public IEnumerable<BO.ProductItem?> ListProductsToBuy()
        {
            IEnumerable<DO.Product?> Product = idal.Product.GetAll();
            List<BO.ProductItem> ProductList = new List<BO.ProductItem>();
            BO.ProductItem newProduct;
            foreach (DO.Product item in Product)
            {
                newProduct = new BO.ProductItem();
                newProduct.ID = item.ProductId;
                newProduct.Name = item.ProductName;
                newProduct.Price = item.Price;
                newProduct.Category = (BO.Categories)item.Category;
                newProduct.InStock = item.AmountInStock > 0 ? true : false;
                newProduct.Amount = item.AmountInStock;
                ProductList.Add(newProduct);
            }
            return ProductList;
        }
        //The purpose of the function is to present the product details to the buyer according to the product code.
        public BO.ProductItem ProductForBuyer(int idProduct)
        {
            DO.Product product;
            product = idal.Product.Get(idProduct);
            BO.ProductItem newProduct = new BO.ProductItem();
            newProduct.ID = idProduct;
            newProduct.Name = product.ProductName;
            newProduct.Price = product.Price;
            newProduct.Amount = product.AmountInStock;
            newProduct.Category = (BO.Categories)product.Category;
            newProduct.InStock = product.AmountInStock > 0 ? true : false;
            return newProduct;
        }
    }
}
