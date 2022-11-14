using DO;

using Dal;

public class Program
{
    //function for orders
    public static void OrderFunction()
    {
        int number;
        Console.WriteLine("Insert number");
        number = int.Parse(Console.ReadLine());
        DalOrder dalOrder = new DalOrder();

        switch (number)
        {
            case 0:
                break;
            //To add a new order
            case 1:
                {
                    //Entering the new data
                    Console.WriteLine("Insert name & address & email to customer");

                    string name = Console.ReadLine();
                    string address = Console.ReadLine();
                    string email = Console.ReadLine();

                    Order order = new Order();
                    order.customerName = name;
                    order.email = email;
                    order.shippingAddress = address;
                    order.orderCreationDate = DateTime.Today;
                    order.deliveryDate = DateTime.MinValue;
                    order.dateOfDelivery = DateTime.MinValue;
                    Console.WriteLine(dalOrder.AddOrder(order));
                }
                break;
            //To receive a particular order by its ID
            case 2:
                {
                    int id;
                    Console.WriteLine("Insert id");
                    id = int.Parse(Console.ReadLine());
                    Order order = new Order();
                    order = dalOrder.GetOrder(id);
                    Console.WriteLine(order);
                }
                break;
            //To receive all the orders that exist in the order array
            case 3:
                {
                    Order[] orders = dalOrder.GetAllOrders();
                    for (int i = 0; i < orders.Length; i++)
                    {
                        Console.WriteLine(orders[i]);
                        Console.WriteLine();
                    }
                }
                break;
            //To update the time details
            case 4:
                {
                    int id;
                    //Finding the order you want to update by its ID
                    Console.WriteLine("Insert id");
                    id = int.Parse(Console.ReadLine());
                    Order order = new Order();
                    order = dalOrder.GetOrder(id);
                    Console.WriteLine(order);
                    Console.WriteLine("Insert name,address,email to customer");
                    string name = Console.ReadLine();
                    string address = Console.ReadLine();
                    string email = Console.ReadLine();
                    if (name != "" && address != "" && email != "")
                    {
                        Order newOrder = new Order();
                        order.orderId = id;
                        order.customerName = name;
                        order.shippingAddress = address;
                        order.email = email;
                        order.orderCreationDate = DateTime.Today;
                        order.deliveryDate = DateTime.MinValue;
                        order.dateOfDelivery = DateTime.MinValue;
                        dalOrder.UpdateOrder(newOrder);
                    }
                }
                break;
            //To delete a specific order
            case 5:
                {
                    int id;
                    Console.WriteLine("insert id");
                    id = int.Parse(Console.ReadLine());
                    dalOrder.DeletOrder(id);
                }
                break;
        }
    }
    //function for products
    public static void ProductFunction()
    {
        int num2;
        Console.WriteLine("Insert number");
        num2 = int.Parse(Console.ReadLine());
        DalProduct dalProduct = new DalProduct();

        switch (num2)
        {
            case 0:
                break;
            //To add a new product
            case 1:
                {
                    //Entering the new data
                    Console.WriteLine("Insert name,price,amount in stock and category 1- Chagim, 2-HomeAccessories, 3-HomeTextiles,4- Judaica, 5-DesignedGifts");
                    string name = Console.ReadLine();
                    int price = int.Parse(Console.ReadLine());
                    int amountInStock = int.Parse(Console.ReadLine());
                    int category = int.Parse(Console.ReadLine());
                    Product product = new Product();
                    product.productName = name;
                    product.category = (Categories)category; 
                    product.price = price;
                    product.amountInStock = amountInStock;
                    Console.WriteLine(dalProduct.AddProduct(product));
                }
                break;
            //To receive a particular product by its ID
            case 2:
                {
                    int id;
                    Console.WriteLine("Insert id");
                    id = int.Parse(Console.ReadLine());
                    Product product = new Product();
                    product = dalProduct.GetProduct(id);
                    Console.WriteLine(product);
                }
                break;
            //To receive all the products that exist in the product array  
            case 3:
                {
                    Product[] products = dalProduct.GetAllProducts();
                    for (int i = 0; i < products.Length; i++)
                    {
                        Console.WriteLine(products[i]);
                        Console.WriteLine();
                    }
                }
                break;
            //To update the time details
            case 4:
                {
                    int id;

                    //Finding the product you want to update by its ID
                    Console.WriteLine("Insert id");
                    id = int.Parse(Console.ReadLine());
                    Product product = new Product();
                    product = dalProduct.GetProduct(id);
                    Console.WriteLine(product);
                    Console.WriteLine("Insert name,price,amount in stock and category 1- Chagim, 2-HomeAccessories, 3-HomeTextiles,4- Judaica, 5-DesignedGifts to product");
                    string name = Console.ReadLine();
                    int price = int.Parse(Console.ReadLine());
                    int amountInStock = int.Parse(Console.ReadLine());
                    int category = int.Parse(Console.ReadLine());
                    if (name != "" && price != 0 && amountInStock != 0 && category != 0)
                    {
                        Product newProduct = new Product();
                        product.productId = id;
                        product.productName = name;
                        product.category = (Categories)category;
                        product.price = price;
                        product.amountInStock = amountInStock;
                        dalProduct.UpdateProduct(newProduct);
                    }
                }
                break;
            //To delete a specific product
            case 5:
                {
                    int id;
                    Console.WriteLine("Insert id");
                    id = int.Parse(Console.ReadLine());
                    dalProduct.DeletProduct(id);
                }
                break;
        }
    }
    //function for orderItems
    public static void OrderItemFunction()
    {
        int num2;
        Console.WriteLine("Insert number");
        num2 = int.Parse(Console.ReadLine());
        DalOrderItem dalOrderItem = new DalOrderItem();
        DalProduct dalProduct = new DalProduct();
        switch (num2)
        {
            case 0:
                break;
                //To add a new orderItem
            case 1:
                {
                    //Entering the new data
                    Console.WriteLine("Inser amount, productId,orderId");
                    int amount = int.Parse(Console.ReadLine());
                    int product_id = int.Parse(Console.ReadLine());
                    int order_id = int.Parse(Console.ReadLine());
                    double price = dalProduct.GetProduct(product_id).price;
                    OrderItem orderItem = new OrderItem();
                    orderItem.productId = product_id;
                    orderItem.amount = amount;
                    orderItem.orderId = order_id;
                    orderItem.pricePerUnit = price;
                    Console.WriteLine(dalOrderItem.AddOrderItem(orderItem));

                }
                break;
                //To receive a particular orderItem by its ID
            case 2:
                {
                    int id;
                    Console.WriteLine("Insert id");
                    id = int.Parse(Console.ReadLine());
                    OrderItem orderItem = new OrderItem();
                    orderItem = dalOrderItem.GetOrderItem(id);
                    Console.WriteLine(orderItem);
                }
                break;
            //To receive all the orderItems that exist in the orderItem array           
            case 3:
                {
                    OrderItem[] ordersItem = dalOrderItem.GetAllOrderItems();
                    for (int i = 0; i < ordersItem.Length; i++)
                    {
                        Console.WriteLine(ordersItem[i]);
                        Console.WriteLine();
                    }
                }
                break;
                //To update the time details
            case 4:
                {

                    int id;
                    //Finding the orderItem you want to update by its ID
                    Console.WriteLine("Insert id");
                    id = int.Parse(Console.ReadLine());
                    OrderItem orderItem = new OrderItem();
                    orderItem = dalOrderItem.GetOrderItem(id);
                    Console.WriteLine(orderItem);
                    Console.WriteLine("Insert name,price,in stock and category 1- Chagim, 2-HomeAccessories, 3-HomeTextiles,4- Judaica, 5-DesignedGifts");
                    string name = Console.ReadLine();
                    int amount = int.Parse(Console.ReadLine());
                    int price = int.Parse(Console.ReadLine());
                    if (price != 0 && amount != 0)
                    {

                        OrderItem newOrderItem = new OrderItem();
                        orderItem.amount = amount;
                        orderItem.pricePerUnit = price;
                        dalOrderItem.UpdateOrderItem(newOrderItem);
                    }
                }
                break;
            //To delete a specific orderItem
            case 5:
                {
                    int id;
                    Console.WriteLine("Insert id");
                    id = int.Parse(Console.ReadLine());
                    dalOrderItem.DeletOrderItem(id);
                }
                break;
            //Returning an order item object that matches a product code and an order code
            case 6:
                {
                    Console.WriteLine("Enter product id,order id");
                    int prod = int.Parse(Console.ReadLine());
                    int ord = int.Parse(Console.ReadLine());
                    Console.WriteLine(dalOrderItem.GetItemById(prod, ord));
                }
                break;
            //Array of order details by order ID number
            case 7:
                {
                    Console.WriteLine("Enter order id");
                    int order = int.Parse(Console.ReadLine());
                    OrderItem[] arr = dalOrderItem.GetAllProductsOfOrder(order);
                    for (int i = 0; i < arr.Length; i++)
                    {
                        Console.WriteLine(arr[i]);
                        Console.WriteLine();
                    }
                    break;
                }
        }
    }
    public static void Main()
    {
        int number;
        do
        {
            Console.WriteLine("Enter 1 to product");
            Console.WriteLine("Enter 2 to order");
            Console.WriteLine("Enter 3 to orderItem");
            Console.WriteLine("Enter 0 to exit");
            number = int.Parse(Console.ReadLine());
            //Switch to select the desired entity and throws a comment in case it is not selected
            switch (number)
            {
                case 1:
                    try
                    {
                        ProductFunction();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                    break;
                case 2:
                    try
                    {
                        OrderFunction();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                    break;
                case 3:
                    try
                    {
                        OrderItemFunction();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                    break;

                default:
                    {
                        Console.WriteLine("Enter again");
                        number = int.Parse(Console.ReadLine());
                        break;
                    }
            }
        }
        while (number != 0);
    }
}