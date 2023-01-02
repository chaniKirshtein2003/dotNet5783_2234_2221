namespace BlImplementation
{
    internal class Product : BlApi.IProduct
    {
        DalApi.IDal? idal = DalApi.Factory.Get();
        //The purpose of the function is to show the manager a list of products.
        public IEnumerable<BO.ProductForList> GetProducts()
        {
            List<BO.ProductForList> products = new List<BO.ProductForList>();

            foreach (DO.Product? item in idal!.Product.GetAll())
            {
                BO.ProductForList product = new BO.ProductForList();
                product.Name = item?.ProductName;
                product.Price = item?.Price??0;
                product.Category = (BO.Categories?)item?.Category;
                product.ID = item?.ProductId??0;
                products.Add(product);
            }
            return products;
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
            catch (Exception x)
            {
                throw new BO.NotExistBlException("product not exist", x);
            }
        }
        public IEnumerable<BO.ProductForList> GetProductsListByCategory(BO.Categories _category)
        {
            IEnumerable<DO.Product?> products = idal!.Product.GetAll(x => x?.Category == (DO.Categories)_category);
            List<BO.ProductForList?> pro = new List<BO.ProductForList?>();
            foreach (DO.Product? item in products)
            {
                BO.ProductForList newPro = new BO.ProductForList();
                newPro.Name = item?.ProductName;
                newPro.Price = item?.Price ?? 0;
                newPro.ID = item?.ProductId ?? 0;
                newPro.Category = (BO.Categories?)item?.Category;
                pro.Add(newPro);
            }
            return pro;
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
        public void Delete(int id)
        {
            IEnumerable<DO.OrderItem?> orderItemList;
            IEnumerable<DO.Order?> orderList = idal!.Order.GetAll();            
            foreach (var order in orderList)
            {
                orderItemList = idal.OrderItem.GetAll(x => x?.OrderId == (order?.OrderId ?? throw new BO.NotExistBlException()));
                foreach (DO.OrderItem? item in orderItemList)
                    if (item?.OrderItemId == id)
                        throw new BO.AlreadyExistBlException("The product is exist");
            }
            try
            {
                idal!.Product.Delete(id);
            }
            catch (Exception x)
            {
                throw new BO.NotExistBlException("not exist", x);
            }
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
            catch (Exception x)
            {

                throw new BO.NotExistBlException("not exist", x);
            }
        }
        //The purpose of the function is to show the buyer a list of products.
        public IEnumerable<BO.ProductItem> ListProductsToBuy()
        {
            IEnumerable<DO.Product?> products = idal!.Product.GetAll();
            List<BO.ProductItem?> productList = new List<BO.ProductItem?>();
            BO.ProductItem? newProduct;
            foreach (DO.Product? item in products)
            {
                newProduct = new BO.ProductItem();
                newProduct.ID = item?.ProductId??0;
                newProduct.Name = item?.ProductName;
                newProduct.Price = item?.Price??0;
                newProduct.Category = (BO.Categories?)item?.Category;
                newProduct.InStock = item?.AmountInStock > 0 ? true : false;
                newProduct.Amount = item?.AmountInStock??0;
                productList.Add(newProduct);
            }
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
            catch (Exception x)
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
