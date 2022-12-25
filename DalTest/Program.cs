using DO;
using Dal;

public class Program
{
    static DalApi.IDal? idal = DalApi.Factory.Get();

    //function for orders
    public static void ActionsOnOrder()
    {
        int number;
        Console.WriteLine("insert number 0 to exit,1 to add order,2 to get order by id,3 to get all orders,4 to update order and 5 to delete order");
        number = int.Parse(Console.ReadLine() ?? throw new Exception("missing details"));
        switch (number)
        {
            case 0:
                break;
            //To add a new order
            case 1:
                {
                    //Entering the new data
                    Console.WriteLine("Insert name & address & email to customer and orderDate,shipDate,deliveryDate");
                    string name = Console.ReadLine() ?? throw new Exception("missing details");
                    string address = Console.ReadLine() ?? throw new Exception("missing details");
                    string email = Console.ReadLine() ?? throw new Exception("missing details");
                    string orderDate = Console.ReadLine() ?? throw new Exception("missing details");
                    string shippingDate = Console.ReadLine() ?? throw new Exception("missing details");
                    string deliveryDate = Console.ReadLine() ?? throw new Exception("missing details");
                    DateTime.TryParse(orderDate, out DateTime dtOrder);
                    DateTime.TryParse(shippingDate, out DateTime dtShip);
                    DateTime.TryParse(deliveryDate, out DateTime dtDelivery);
                    Order order = new Order();
                    order.CustomerName = name;
                    order.CustomerEmail = email;
                    order.CustomerAddress = address;
                    order.OrderDate = dtOrder;
                    order.DeliveryDate = dtDelivery;
                    order.ShipDate = dtShip;
                    Console.WriteLine(idal?.Order.Add(order));
                }
                break;
            //To receive a particular order by its ID
            case 2:
                {
                    int id;
                    Console.WriteLine("Insert id");
                    id = int.Parse(Console.ReadLine() ?? throw new Exception("missing details"));
                    Order order = idal!.Order.Get(id);
                    Console.WriteLine(order);
                }
                break;
            //To receive all the orders that exist in the order array
            case 3:
                {
                    IEnumerable<Order?> orders = idal?.Order.GetAll();
                    foreach (Order? item in orders)
                    {
                        Console.WriteLine(item);
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
                    id = int.Parse(Console.ReadLine() ?? throw new Exception("missing details"));
                    Order order = new Order();
                    order = idal!.Order.Get(id);
                    Console.WriteLine(order);
                    Console.WriteLine("Insert name,address,email to customer and orderDate,shipDate,deliveryDate");
                    string name = Console.ReadLine() ?? throw new Exception("missing details");
                    string address = Console.ReadLine() ?? throw new Exception("missing details");
                    string email = Console.ReadLine() ?? throw new Exception("missing details");
                    string orderDate = Console.ReadLine() ?? throw new Exception("missing details");
                    string shippingDate = Console.ReadLine() ?? throw new Exception("missing details");
                    string deliveryDate = Console.ReadLine() ?? throw new Exception("missing details");
                    DateTime.TryParse(orderDate, out DateTime dtOrder);
                    DateTime.TryParse(shippingDate, out DateTime dtShip);
                    DateTime.TryParse(deliveryDate, out DateTime dtDelivery);
                    order.OrderId = id;
                    order.CustomerName = name;
                    order.CustomerAddress = address;
                    order.CustomerEmail = email;
                    order.OrderDate = dtOrder;
                    order.DeliveryDate = dtDelivery;
                    order.ShipDate = dtShip;
                    idal?.Order.Update(order);
                }
                break;
            //To delete a specific order
            case 5:
                {
                    int id;
                    Console.WriteLine("insert id");
                    id = int.Parse(Console.ReadLine() ?? throw new Exception("missing details"));
                    idal?.Order.Delete(id);
                }
                break;
        }
    }

    //function for products
    public static void ActionsOnProduct()
    {
        int number2;
        Console.WriteLine("insert number 0 to exit,1 to add product,2 to get product by id,3 to get all products,4 to update product and 5 to delete product");
        number2 = int.Parse(Console.ReadLine() ?? throw new Exception("missing details"));
        //DalProduct dalProduct = new DalProduct();

        switch (number2)
        {
            case 0:
                break;
            //To add a new product
            case 1:
                {
                    //Entering the new data
                    Console.WriteLine("Insert id, name,price,amount in stock and category 1- Chagim, 2-HomeAccessories, 3-HomeTextiles,4- Judaica, 5-DesignedGifts");
                    int id = int.Parse(Console.ReadLine() ?? throw new Exception("missing details"));
                    string name = Console.ReadLine() ?? throw new Exception("missing details");
                    int price = int.Parse(Console.ReadLine() ?? throw new Exception("missing details"));
                    int amountInStock = int.Parse(Console.ReadLine() ?? throw new Exception("missing details"));
                    int category = int.Parse(Console.ReadLine() ?? throw new Exception("missing details"));
                    Product product = new Product();
                    product.ProductId = id;
                    product.ProductName = name;
                    product.Category = (Categories)category;
                    product.Price = price;
                    product.AmountInStock = amountInStock;
                    Console.WriteLine(idal?.Product.Add(product));
                }
                break;
            //To receive a particular product by its ID
            case 2:
                {
                    int id;
                    Console.WriteLine("Insert id");
                    id = int.Parse(Console.ReadLine() ?? throw new Exception("missing details"));
                    Product product = idal!.Product.Get(id);
                    Console.WriteLine(product);
                }
                break;
            //To receive all the products that exist in the product array  
            case 3:
                {
                    IEnumerable<Product?> products = idal!.Product.GetAll();
                    foreach (Product? item in products)
                    {
                        Console.WriteLine(item);
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
                    id = int.Parse(Console.ReadLine() ?? throw new Exception("missing details"));
                    Product product = new Product();
                    product = idal!.Product.Get(id);
                    Console.WriteLine(product);
                    Console.WriteLine("Insert name,price,amount in stock and category 1- Chagim, 2-HomeAccessories, 3-HomeTextiles,4- Judaica, 5-DesignedGifts to product");
                    string name = Console.ReadLine() ?? throw new Exception("missing details");
                    int price = int.Parse(Console.ReadLine() ?? throw new Exception("missing details"));
                    int amountInStock = int.Parse(Console.ReadLine() ?? throw new Exception("missing details"));
                    int category = int.Parse(Console.ReadLine() ?? throw new Exception("missing details"));
                    product.ProductId = id;
                    product.ProductName = name;
                    product.Category = (Categories)category;
                    product.Price = price;
                    product.AmountInStock = amountInStock;
                    idal?.Product.Update(product);
                }
                break;
            //To delete a specific product
            case 5:
                {
                    int id;
                    Console.WriteLine("Insert id");
                    id = int.Parse(Console.ReadLine() ?? throw new Exception("missing details"));
                    idal?.Product.Delete(id);
                }
                break;
        }
    }

    //function for orderItems
    public static void ActionsOnOrderItems()
    {
        int number2;
        Console.WriteLine("insert number 0 to exit,1 to add orderItem,2 to get orderItem by id,3 to get all orderItems,4 to update orderItem and 5 to delete orderItem");
        number2 = int.Parse(Console.ReadLine()?? throw new Exception("missing details"));
        //DalOrderItem dalOrderItem = new DalOrderItem();
        //DalProduct dalProduct = new DalProduct();
        switch (number2)
        {
            case 0:
                break;
                //To add a new orderItem
            case 1:
                {
                    //Entering the new data
                    Console.WriteLine("Inser amount, productId,orderId");
                    int amount = int.Parse(Console.ReadLine()?? throw new Exception("missing details"));
                    int product_id = int.Parse(Console.ReadLine()?? throw new Exception("missing details"));
                    int order_id = int.Parse(Console.ReadLine()?? throw new Exception("missing details"));
                    double price = idal!.Product.Get(product_id).Price;
                    OrderItem orderItem = new OrderItem();
                    orderItem.ProductId = product_id;
                    orderItem.Amount = amount;
                    orderItem.OrderId = order_id;
                    orderItem.PricePerUnit = price;
                    Console.WriteLine(idal?.OrderItem.Add(orderItem));

                }
                break;
                //To receive a particular orderItem by its ID
            case 2:
                {
                    int id;
                    Console.WriteLine("Insert id");
                    id = int.Parse(Console.ReadLine()?? throw new Exception("missing details"));
                    OrderItem orderItem = idal!.OrderItem.Get(id);
                    Console.WriteLine(orderItem);
                }
                break;
            //To receive all the orderItems that exist in the orderItem array           
            case 3:
                {
                    IEnumerable<OrderItem?> ordersItem = idal?.OrderItem.GetAll();
                    foreach(OrderItem? item in ordersItem)
                    {
                        Console.WriteLine(item);
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
                    id = int.Parse(Console.ReadLine()?? throw new Exception("missing details"));
                    OrderItem orderItem = idal!.OrderItem.Get(id);
                    Console.WriteLine(orderItem);
                    Console.WriteLine("Inser amount, productId,orderId");
                    int amount = int.Parse(Console.ReadLine()?? throw new Exception("missing details"));
                    int product_id = int.Parse(Console.ReadLine()?? throw new Exception("missing details"));
                    int order_id = int.Parse(Console.ReadLine()?? throw new Exception("missing details"));
                    double price = idal!.OrderItem.Get(product_id).PricePerUnit;
                    orderItem.Amount = amount;
                    orderItem.ProductId = product_id;
                    orderItem.OrderId = order_id;
                    idal?.OrderItem.Update(orderItem);
                }
                break;
            //To delete a specific orderItem
            case 5:
                {
                    int id;
                    Console.WriteLine("Insert id");
                    id = int.Parse(Console.ReadLine()?? throw new Exception("missing details"));
                    idal?.OrderItem.Delete(id);
                }
                break;
            //Returning an order item object that matches a product code and an order code
            case 6:
                {
                    Console.WriteLine("Enter product id,order id");
                    int prod = int.Parse(Console.ReadLine()?? throw new Exception("missing details"));
                    int ord = int.Parse(Console.ReadLine()?? throw new Exception("missing details"));
                    Console.WriteLine(idal?.OrderItem.GetItemById(prod, ord));
                }
                break;
            //Array of order details by order ID number
            case 7:
                {
                    Console.WriteLine("Enter order id");
                    int order = int.Parse(Console.ReadLine()?? throw new Exception("missing details"));
                    IEnumerable<OrderItem?> list = idal?.OrderItem.GetOrderItems(order);
                    foreach(OrderItem? item in list)
                    {
                        Console.WriteLine(item);
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
            number = int.Parse(Console.ReadLine()?? throw new Exception("missing details"));
            //Switch to select the desired entity and throws a comment in case it is not selected
            switch (number)
            {
                case 1:
                    try
                    {
                        ActionsOnProduct();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;
                case 2:
                    try
                    {
                        ActionsOnOrder();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;
                case 3:
                    try
                    {
                        ActionsOnOrderItems();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;

                default:
                    {
                        Console.WriteLine("Enter again");
                        number = int.Parse(Console.ReadLine()?? throw new Exception("missing details"));
                        break;
                    }
            }
        }
        while (number != 0);
    }
}