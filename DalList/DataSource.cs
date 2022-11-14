using DO;

namespace Dal;

internal static class DataSource
{
    //Statement on new sets of products orders and order details
    internal static Product[] productsArr = new Product[50];
    internal static Order[] ordersArr = new Order[100];
    internal static OrderItem[] orderItemsArr = new OrderItem[200];
    //A class containing fields for indexes of the first free element and additional fields for the last ID number
    internal static class Config
    {
        private static int productNextId = 0;
        private static int orderNextId = 0;
        private static int orderItemNextId = 0;
        internal static int productNextIndex = 0;
        internal static int orderNextIndex = 0;
        internal static int orderItemNextIndex = 0;
        //functions for each field of the last ID number that you advance the
        //The field is automatic so that each time a number is received that is greater than the previous one by 1
        public static int GetProductNextId() 
        {
            productNextId++;
            return productNextId; 
        }
        public static int GetOrderNextId() 
        {
            orderNextId++;
            return orderNextId; 
        }
        public static int GetOrderItemNextId()
        {
            orderItemNextId++;
            return orderItemNextId;
        }
    }
    private static readonly Random s_rand = new();
    static DataSource()
    {
        s_Initialize();
    }
    //Initialization function for product data
    public static void createInitializeProduct()
    {
        string[] productNameArr = { "flowers", "ships beds", "watches", "Halat trays", "Napkins to get sick", "Burdens", "Covers for braces", "Furniture details", "Mirror", "Palm tree", "Picture frame", "vases", "Crystal candlestick", "A tissue box", "Crystal chess","a","b","c","d","e","f" };
        //A loop that runs over the array of products and fills it with values.
        for (int i = 0; i < 20; i++)
        {
            int instock;
            if (i % 4 == 0)
                instock = 0;
            else
                instock = s_rand.Next(10) + 200;
            Product product = new Product();
            product.productId = i;
            product.productName = productNameArr[i];
            product.category = (Categories)i;
            product.price = s_rand.Next(10) + 450;
            product.amountInStock = instock;
            productsArr[Config.productNextIndex++] = product;
        }
    }
    //Initialization function for order data
    public static void createInitializeOrder()
    {
        string[] customerNameArr = { "Chani kirshtein", "Shuli Levi", "David Cohen", "Chaim weiss", "Lali pal", "Israel Lubin", "Moshe duek", "Ruth Gross", "Chaya Grin", "Yehuda Cazt" };
        string[] customerAddressArr = { "Gotlib 2", "ovadia 1", "hanasi 3", "rabbi akiva 23", "ahronovith 8", "Gordon 4", "Pinkas 77", "Dakar 2", "Tarfon 3", "Golomb 4" };
        string[] customerEmailArr = { "s@gmail.com", "m@gmail.com", "n@gmail.com", "t@gmail.com", "y@gmail.com", "a@gmail.com", "d@gmail.com", "w@gmail.com", "q@gmail.com", "p@gmail.com" };
        //A loop that runs over the array of orders and fills it with values.
        for (int i = 0; i < 10; i++)
        {
            TimeSpan timeS = new TimeSpan(s_rand.Next(5));
            TimeSpan timeS1 = new TimeSpan(s_rand.Next(20));
            DateTime date;
            if (i % 3 == 0)
            {
                date = new DateTime();
            }
            else
            {
                date = DateTime.MinValue + timeS;
            }
            Order order = new Order();
            order.orderId = i;
            order.customerName = customerNameArr[i];
            order.email = customerEmailArr[s_rand.Next(customerNameArr.Length)];
            order.shippingAddress = customerAddressArr[i];
            order.orderCreationDate = DateTime.MinValue;
            order.deliveryDate = date;
            order.dateOfDelivery = DateTime.MinValue + timeS + timeS1;
            ordersArr[Config.orderNextIndex++] = order;
        }
    }
    //Initialization function for orderItem data
    public static void createInitializeOrderItem()
    {
        int product = s_rand.Next(20);
        //A loop that runs over the array of orderItems and fills it with values.
        for (int i = 0; i < 40; i++)
        {
            OrderItem orderItem = new OrderItem();
            orderItem.orderItemId = Config.GetOrderItemNextId();
            orderItem.orderId = s_rand.Next(10);
            orderItem.productId = product;
            orderItem.amount = s_rand.Next(10) + 1;
            orderItem.pricePerUnit = productsArr[product].price;
            orderItemsArr[i] = orderItem;
            orderItemsArr[Config.orderItemNextIndex] = orderItem;
        }
    }
    //A function that calls all the initialization operations
    private static void s_Initialize()
    {
        createInitializeProduct();
        createInitializeOrder();
        createInitializeOrderItem();
    }
}


