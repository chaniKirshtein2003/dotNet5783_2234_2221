using DO;
using Dal;

public class Program
{
    //function for orders
    public static void ActionsOnOrder()
    {
        int number;
        Console.WriteLine("insert number 0 to exit,1 to add order,2 to get order by id,3 to get all orders,4 to update order and 5 to delete order");
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
                    Console.WriteLine("Insert name & address & email to customer and orderDate,shipDate,deliveryDate");
                    string name = Console.ReadLine();
                    string address = Console.ReadLine();
                    string email = Console.ReadLine();
                    string orderDate = Console.ReadLine();
                    string shippingDate = Console.ReadLine();
                    string deliveryDate = Console.ReadLine();
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
                    Console.WriteLine(dalOrder.Add(order));
                }
                break;
            //To receive a particular order by its ID
            case 2:
                {
                    int id;
                    Console.WriteLine("Insert id");
                    id = int.Parse(Console.ReadLine());
                    Order order = dalOrder.Get(id);
                    Console.WriteLine(order);
                }
                break;
            //To receive all the orders that exist in the order array
            case 3:
                {
                    IEnumerable<Order?> orders = dalOrder.GetAll();
                    foreach (var item in orders)
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
                    id = int.Parse(Console.ReadLine());
                    Order order = new Order();
                    order = dalOrder.Get(id);
                    Console.WriteLine(order);
                    Console.WriteLine("Insert name,address,email to customer and orderDate,shipDate,deliveryDate");
                    string name = Console.ReadLine();
                    string address = Console.ReadLine();
                    string email = Console.ReadLine();
                    string orderDate = Console.ReadLine();
                    string shippingDate = Console.ReadLine();
                    string deliveryDate = Console.ReadLine();
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
                    dalOrder.Update(order);
                }
                break;
            //To delete a specific order
            case 5:
                {
                    int id;
                    Console.WriteLine("insert id");
                    id = int.Parse(Console.ReadLine());
                    dalOrder.Delete(id);
                }
                break;
        }
    }
    //function for products
    public static void ActionsOnProduct()
    {
        int number2;
        Console.WriteLine("insert number 0 to exit,1 to add product,2 to get product by id,3 to get all products,4 to update product and 5 to delete product");
        number2 = int.Parse(Console.ReadLine());
        DalProduct dalProduct = new DalProduct();

        switch (number2)
        {
            case 0:
                break;
            //To add a new product
            case 1:
                {
                    //Entering the new data
                    Console.WriteLine("Insert id, name,price,amount in stock and category 1- Chagim, 2-HomeAccessories, 3-HomeTextiles,4- Judaica, 5-DesignedGifts");
                    int id = int.Parse(Console.ReadLine());
                    string name = Console.ReadLine();
                    int price = int.Parse(Console.ReadLine());
                    int amountInStock = int.Parse(Console.ReadLine());
                    int category = int.Parse(Console.ReadLine());
                    Product product = new Product();
                    product.ProductId = id;
                    product.ProductName = name;
                    product.Category = (Categories)category; 
                    product.Price = price;
                    product.AmountInStock = amountInStock;
                    Console.WriteLine(dalProduct.Add(product));
                }
                break;
            //To receive a particular product by its ID
            case 2:
                {
                    int id;
                    Console.WriteLine("Insert id");
                    id = int.Parse(Console.ReadLine());
                    Product product = dalProduct.Get(id);
                    Console.WriteLine(product);
                }
                break;
            //To receive all the products that exist in the product array  
            case 3:
                {
                    IEnumerable<Product?> products = dalProduct.GetAll();
                    foreach(var item in products)
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
                    id = int.Parse(Console.ReadLine());
                    Product product = new Product();
                    product = dalProduct.Get(id);
                    Console.WriteLine(product);
                    Console.WriteLine("Insert name,price,amount in stock and category 1- Chagim, 2-HomeAccessories, 3-HomeTextiles,4- Judaica, 5-DesignedGifts to product");
                    string name = Console.ReadLine();
                    int price = int.Parse(Console.ReadLine());
                    int amountInStock = int.Parse(Console.ReadLine());
                    int category = int.Parse(Console.ReadLine());
                    product.ProductId = id;
                    product.ProductName = name;
                    product.Category = (Categories)category;
                    product.Price = price;
                    product.AmountInStock = amountInStock;
                    dalProduct.Update(product);
                }
                break;
            //To delete a specific product
            case 5:
                {
                    int id;
                    Console.WriteLine("Insert id");
                    id = int.Parse(Console.ReadLine());
                    dalProduct.Delete(id);
                }
                break;
        }
    }
    //function for orderItems
    public static void ActionsOnOrderItems()
    {
        int number2;
        Console.WriteLine("insert number 0 to exit,1 to add orderItem,2 to get orderItem by id,3 to get all orderItems,4 to update orderItem and 5 to delete orderItem");
        number2 = int.Parse(Console.ReadLine());
        DalOrderItem dalOrderItem = new DalOrderItem();
        DalProduct dalProduct = new DalProduct();
        switch (number2)
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
                    double price = dalProduct.Get(product_id).Price;
                    OrderItem orderItem = new OrderItem();
                    orderItem.ProductId = product_id;
                    orderItem.Amount = amount;
                    orderItem.OrderId = order_id;
                    orderItem.PricePerUnit = price;
                    Console.WriteLine(dalOrderItem.Add(orderItem));

                }
                break;
                //To receive a particular orderItem by its ID
            case 2:
                {
                    int id;
                    Console.WriteLine("Insert id");
                    id = int.Parse(Console.ReadLine());
                    OrderItem orderItem = dalOrderItem.Get(id);
                    Console.WriteLine(orderItem);
                }
                break;
            //To receive all the orderItems that exist in the orderItem array           
            case 3:
                {
                    IEnumerable<OrderItem?> ordersItem = dalOrderItem.GetAll();
                    foreach(var item in ordersItem)
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
                    id = int.Parse(Console.ReadLine());
                    OrderItem orderItem = dalOrderItem.Get(id);
                    Console.WriteLine(orderItem);
                    Console.WriteLine("Inser amount, productId,orderId");
                    int amount = int.Parse(Console.ReadLine());
                    int product_id = int.Parse(Console.ReadLine());
                    int order_id = int.Parse(Console.ReadLine());
                    double price = dalProduct.Get(product_id).Price;
                    orderItem.Amount = amount;
                    orderItem.ProductId = product_id;
                    orderItem.OrderId = order_id;
                    dalOrderItem.Update(orderItem);
                }
                break;
            //To delete a specific orderItem
            case 5:
                {
                    int id;
                    Console.WriteLine("Insert id");
                    id = int.Parse(Console.ReadLine());
                    dalOrderItem.Delete(id);
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
                    IEnumerable<OrderItem?> list = dalOrderItem.GetOrderItems(order);
                    foreach(var item in list)
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
            number = int.Parse(Console.ReadLine());
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
                        number = int.Parse(Console.ReadLine());
                        break;
                    }
            }
        }
        while (number != 0);
    }
}