
using DalApi;
using Dal;


namespace BlImplementation
{
    internal class Product : BlApi.IProduct
    {
        IDal idal = new Dallist();
        public IEnumerable<BO.ProductForList> GetProductForList()
        {
            List<BO.ProductForList> products = new List<BO.ProductForList>();
            foreach (var item in idal.Product.GetAll())
            {
                BO.ProductForList product = new BO.ProductForList();
                product.Name = item.productName;
                product.price = item.price;
                product.category = (BO.Categories)item.category;  
                product.ID = item.productId;
                products.Add(product);
            }
            return products;
        }
        public BO.Product GetProductById(int id)
        {
            DO.Product product = idal.Product.Get(id);
            BO.Product newProduct = new BO.Product();
            newProduct.productName= product.productName;
            newProduct.price= product.price;
            newProduct.productId= product.productId;
            newProduct.category = (BO.Categories)product.category;
            newProduct.amountInStock = product.amountInStock;
            return newProduct;
        }
        public int Add(BO.Product product)
        {
            if (product.productId < 0)
                throw new ArgumentException("Id must be positive");
            if (product.price < 0)
                throw new ArgumentException("Price must be positive");
            if (product.amountInStock < 0)
                throw new ArgumentException("AmountInStock must be positive");
            if (product.productName == "")
                throw new ArgumentException("Price must be positive");
            DO.Product newProduct = new DO.Product();
            newProduct.productName = product.productName;
            newProduct.price = product.price;   
            newProduct.productId= product.productId;
            newProduct.amountInStock= product.amountInStock;
            newProduct.category = (DO.Categories)product.category;
            int id=idal.Product.Add(newProduct);
            return id;
        }
        public void Delete(int id)
        {
            IEnumerable<DO.OrderItem> orderItemList;
            IEnumerable<DO.Order> orderList=new List<DO.Order>();
            foreach(DO.Order order in orderList)
            {
                orderItemList = idal.OrderItem.GetAllProductsOfOrder(order.orderId);
                foreach (DO.OrderItem item in orderItemList)
                    if (item.orderItemId == id)
                        throw new Exception("the product is exist");
            }
            idal.Product.Delete(id);
        }
        public void Update(BO.Product product)
        {
            if (product.productId < 0)
                throw new ArgumentException("Id must be positive");
            if (product.price < 0)
                throw new ArgumentException("Price must be positive");
            if (product.amountInStock < 0)
                throw new ArgumentException("AmountInStock must be positive");
            if (product.productName == "")
                throw new ArgumentException("Price must be positive");
            DO.Product updateProduct = new DO.Product();
            updateProduct.productName = product.productName;
            updateProduct.price = product.price;
            updateProduct.productId = product.productId;
            updateProduct.amountInStock = product.amountInStock;
            updateProduct.category = (DO.Categories)product.category;
            idal.Product.Update(updateProduct);
        }
        public IEnumerable<BO.ProductItem> ListProductsToBuy()
        {
            IEnumerable<DO.Product> Product = idal.Product.GetAll();
            List<BO.ProductItem> ProductList = new List<BO.ProductItem>();
            BO.ProductItem newProduct;
            foreach(DO.Product item in Product)
            {
                newProduct = new BO.ProductItem();
                newProduct.ID=item.productId;
                newProduct.Name=item.productName;
                newProduct.price=item.price;
                newProduct.category = (BO.Categories)item.category;
                newProduct.inStock = item.amountInStock > 0 ? true : false;
                newProduct.amount = item.amountInStock;
                ProductList.Add(newProduct);
            }
            return ProductList;
        }
        public void ProductForBuyer(int idProduct)
        {
            DO.Product Product = idal.Product.Get(idProduct);
            BO.ProductItem newProduct = new BO.ProductItem();
            newProduct.ID = Product.productId;
            newProduct.Name = Product.productName;
            newProduct.price = Product.price;
            newProduct.category = (BO.Categories)Product.category;
            newProduct.amount = Product.amountInStock;
        }
    }
}
