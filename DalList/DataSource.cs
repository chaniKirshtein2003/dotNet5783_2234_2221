﻿using DO;

namespace Dal;

internal static class DataSource
{
    //Statement on new sets of products orders and order details
    //internal static Product[] productsArr = new Product[50];
    internal static List<Product?> productsList = new List<Product?>();
    internal static List<Order?> ordersList = new List<Order?>();
    internal static List<OrderItem?> orderItemsList = new List<OrderItem?>();
    //A class containing fields for indexes of the first free element and additional fields for the last ID number
    internal static class Config
    {
        private static int orderNextId = 0;
        private static int orderItemNextId = 0;
        /*functions for each field of the last ID number that you advance the
        The field is automatic so that each time a number is received that is greater than the previous one by 1*/
        public static int GetOrderNextId() 
        {
            return orderNextId++; 
        }
        public static int GetOrderItemNextId()
        {
            return orderItemNextId++;
        }
    }
    private static readonly Random s_rand = new();
    static DataSource()
    {
        s_Initialize();
    }
    //Initialization function for product data
    private static void createInitializeProduct()
    {
        string[] productNameArr = { "flowers", "ships beds", "watches", "Halat trays", "Napkins to get sick", "Burdens", "Covers for braces", "Furniture details", "Mirror", "Palm tree", "Picture frame", "vases", "Crystal candlestick", "A tissue box", "Crystal chess","chocolate","gift","gift","gift", "gift", "gift" };
        //A loop that runs over the array of products and fills it with values.
        for (int i = 0; i < 20; i++)
        {
            int amountInstock;
            if (i % 4 == 0)
                amountInstock = 0;
            else
                amountInstock = s_rand.Next(10) + 200;
            Product product = new Product();
            product.ProductId = i + 100000;
            product.ProductName = productNameArr[i];
            product.Category = (Categories)(i%5+1);
            product.Price = s_rand.Next(10) + 300;
            product.AmountInStock = amountInstock;
            productsList.Add(product);
        }
    }
    //Initialization function for order data
    private static void createInitializeOrder()
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
            order.OrderId = Config.GetOrderNextId();
            order.CustomerName = customerNameArr[i];
            order.CustomerEmail = customerEmailArr[s_rand.Next(customerNameArr.Length)];
            order.CustomerAddress = customerAddressArr[i];
            order.OrderDate = DateTime.Today;
            order.DeliveryDate = i%3==0? date:null;
            order.ShipDate = i%2==0? date: null;
            ordersList.Add(order);
        }
    }
    //Initialization function for orderItem data
    private static void createInitializeOrderItem()
    {
        //A loop that runs over the array of orderItems and fills it with values.
        int countOrderItems = 0;
        for (int i = 0; i < 20; i++)
        {
            for (int j = 0; j < s_rand.Next(1, 5); j++)
            {
                OrderItem orderItem = new OrderItem();
                orderItem.OrderItemId = Config.GetOrderItemNextId();
                orderItem.OrderId = i/2-1;
                orderItem.ProductId = s_rand.Next(20) + 100000;
                orderItem.Amount = s_rand.Next(10) + 1;
                orderItem.PricePerUnit = findPrice(orderItem.ProductId);
                orderItemsList.Add(orderItem);
            }
            if (i == 19 && countOrderItems < 40)
                i = i - countOrderItems;
        }
    }
    private static double findPrice(int idProduct)
    {
        foreach(var item in productsList)
        {
            if (item?.ProductId == idProduct)
                return item?.Price??throw new NotExistException(idProduct,"There is no product with this id");
        }
        return -1;
    }
    //A function that calls all the initialization operations
    private static void s_Initialize()
    {
        createInitializeProduct();
        createInitializeOrder();
        createInitializeOrderItem();
    }
}


