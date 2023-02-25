using DalApi;
using System.Diagnostics;
using System.Xml.Linq;

namespace BlImplementation
{
    internal class Product : BlApi.IProduct
    {
        DalApi.IDal? idal = DalApi.Factory.Get();
        /// <summary>
        ///The purpose of the function is to show the manager a list of products.
        /// </summary>
        /// <returns>Return list with all the products</returns>
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
        /// <summary>
        ///The purpose of the function is to show the manager by product code the details of that product.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Return product by id</returns>
        /// <exception cref="BO.NotExistBlException"></exception>
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
        /// <summary>
        ///The purpose of the function is to show the manager by category the details of that product.
        /// </summary>
        /// <param name="_category"></param>
        /// <returns>Return list of the products with the choose category</returns>
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
        /// <summary>
        ///The purpose of the function is to get the product details from the user and add a new product with the product details.
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        /// <exception cref="BO.NotValidException"></exception>
        /// <exception cref="BO.AlreadyExistBlException"></exception>
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
        /// <summary>
        ///The purpose of the function is to delete a product by product code.
        /// </summary>
        /// <param name="idOrder"></param>
        /// <exception cref="BO.AlreadyExistBlException"></exception>
        /// <exception cref="BO.NotExistBlException"></exception>
        public void Delete(int idOrder)
        {
            var orderItemList = idal!.OrderItem.GetAll(x => x?.OrderId == idOrder);
            if (orderItemList != null)
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

        /// <summary>
        ///The purpose of the function is to update a product.
        /// </summary>
        /// <param name="product"></param>
        /// <exception cref="BO.NotValidException"></exception>
        /// <exception cref="BO.NotExistBlException"></exception>
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

        /// <summary>
        /// The purpose of the function is to show the buyer a list of products.
        /// </summary>
        /// <returns>Return catalog of products</returns>
        public IEnumerable<BO.ProductItem> ListProductsToBuy()
        {
            IEnumerable<DO.Product?> products = idal!.Product.GetAll();
            var productList = from product in products
                              orderby product?.ProductName

                              select new BO.ProductItem
                              {
                                  ID = product?.ProductId ?? 0,
                                  Name = product?.ProductName,
                                  Price = product?.Price ?? 0,
                                  Category = (BO.Categories?)product?.Category,
                                  InStock = product?.AmountInStock > 0 ? true : false,
                                  Amount = 0
                              };
            return productList;
        }
        /// <summary>
        /// The purpose of the function is to present the product details to the buyer according to the product code.
        /// </summary>
        /// <param name="idProduct"></param>
        /// <returns>Return product item from the catalog by code product</returns>
        /// <exception cref="BO.NotExistBlException"></exception>
        public BO.ProductItem ProductForBuyer(int idProduct, BO.Cart cart)
        {
            DO.Product product;
            int amount;
            try
            {
                amount = cart.Items?.FirstOrDefault(x => x?.ProductId == idProduct)?.Amount ?? 0;
                product = idal!.Product.Get(idProduct);
                BO.ProductItem newProduct = new BO.ProductItem();
                newProduct.ID = idProduct;
                newProduct.Name = product.ProductName;
                newProduct.Price = product.Price;
                newProduct.Amount = amount;
                newProduct.Category = (BO.Categories?)product.Category;
                newProduct.InStock = product.AmountInStock > 0 ? true : false;
                return newProduct;
            }
            catch (DO.NotExistException x)
            {
                throw new BO.NotExistBlException("not exist", x);
            }
        }
        /// <summary>
        ///The function filters the products in the catalog by category
        /// </summary>
        /// <param name="_category"></param>
        /// <returns>Return all the productItems with the choose category</returns>
        public IEnumerable<BO.ProductItem> GetProductsItemByCategory(BO.Categories _category)
        {
            return ListProductsToBuy().ToList().FindAll(x => x?.Category == _category);
        }
        public IEnumerable<BO.ProductForList?> PopularProductItems()
        {
            //creat a list of groups of items that appear in order, by ID
            var popGroup = from item in idal!.OrderItem.GetAll()
                           group item by ((DO.OrderItem?)(item))?.ProductId into g
                           select new { id = g.Key, Items = g };
            //take the 3 that appear in the biggest amount of orders 
            popGroup = popGroup.OrderByDescending(x => x.Items.Count()).Take(3);
            //return the 3 popular items:
            try
            {
                return from item in popGroup
                       let prod = idal.Product.Get(item?.id ?? throw new BO.NotValidException("Prodact ID is null"))
                       select new BO.ProductForList
                       {
                           ID = prod.ProductId,
                           Name = prod.ProductName,
                           Price = prod.Price,
                           Category = (BO.Categories?)prod.Category,
                       };
            }
            catch (BO.NotEnoughDetailsException ex)
            {
                throw new BO.AlreadyExistBlException("Product does not exist", ex);
            }
        }
    }
}
