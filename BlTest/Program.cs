using BlApi;
using BlImplementation;


public class Program
{
    static IBl bl = new Bl();

    //Functions for orders
    public static void Order()
    {
        BO.Order order;
        int number, id;
        Console.WriteLine("Insert number from 1 to 5");
        number = int.Parse(Console.ReadLine());
        switch (number)
        {
            case 1:
                IEnumerable<BO.OrderForList> orderForLists = bl.Order.GetOrders();
                foreach (BO.OrderForList orderForList in orderForLists)
                    Console.WriteLine(orderForList);
                break;
            case 2:
                Console.WriteLine("insert order ID");
                 id=int.Parse(Console.ReadLine());
                 order = bl.Order.GetOrderDetails(id);
                Console.WriteLine(order);
                break;
            case 3:
                Console.WriteLine("insert ID");
                id = int.Parse(Console.ReadLine());
                 order = bl.Order.UpdateSending(id);
                Console.WriteLine(order);
                break;
            case 4:
                Console.WriteLine("insert ID");
                id = int.Parse(Console.ReadLine());
                order = bl.Order.supplyUpdate(id);
                Console.WriteLine(order);
                break;
            case 5:
                BO.OrderTracking orderTracking = new BO.OrderTracking();
                Console.WriteLine("insert ID");
                id = int.Parse(Console.ReadLine());
                orderTracking = bl.Order.OrderTracking(id);
                break;
        }
    }
        //Functions for products
        public static void Product()
    {
        int number;
        Console.WriteLine("Insert number from 1 to 5");
        number = int.Parse(Console.ReadLine());
        switch (number)
        {
            case 0:
                break;
            //Add a new product
            case 1:
                {
                    //Entering the new data
                    Console.WriteLine("Insert id, name, price, amount in stock, and category: 1 for Chagim, 2 for HomeAccessories, 3 for HomeTextiles,4 for Judaica, 5 for DesignedGifts");
                    int id = int.Parse(Console.ReadLine());
                    string name = Console.ReadLine();
                    int price = int.Parse(Console.ReadLine());
                    int amountInStock = int.Parse(Console.ReadLine());
                    int category = int.Parse(Console.ReadLine());
                    BO.Product product = new BO.Product();
                    product.productId = id;
                    product.productName = name;
                    product.category = (BO.Categories)category;
                    product.price = price;
                    product.amountInStock = amountInStock;
                    Console.WriteLine(bl.Product.Add(product));
                }
                break;
            //Get product by ID
            case 2:
                {
                    Console.WriteLine("Insert id of product");
                    int id = int.Parse(Console.ReadLine());
                    Console.WriteLine(bl.Product.GetProduct(id)); 
                }
                break;
            //Get all products
            case 3:
                {
                    IEnumerable<BO.ProductForList> products=bl.Product.GetProducts();
                    foreach (BO.ProductForList product in products)
                    {
                        Console.WriteLine(product);
                    }
                }
                break;
            //Update the time details
            case 4:
                {
                    //Finding the product you want to update by its ID
                    Console.WriteLine("Insert id, name, price, amount in stock, and category: 1 for Chagim, 2 for HomeAccessories, 3 for HomeTextiles,4 for Judaica, 5 for DesignedGifts");
                    int id = int.Parse(Console.ReadLine());
                    string name = Console.ReadLine();
                    int price = int.Parse(Console.ReadLine());
                    int amountInStock = int.Parse(Console.ReadLine());
                    int category = int.Parse(Console.ReadLine());
                    BO.Product product = new BO.Product();
                    product.productId = id;
                    product.productName = name;
                    product.category = (BO.Categories)category;
                    product.price = price;
                    product.amountInStock = amountInStock;
                    bl.Product.Update(product);
                }
                break;
            //Delete a product
            case 5:
                {
                    Console.WriteLine("Insert id");
                    int id = int.Parse(Console.ReadLine());
                    bl.Product.Delete(id);
                }
                break;
        }
    }
    //Functions for cart
    public static void Cart()
    {
        Console.WriteLine("enter 1 to Add item to the cart, 2 to update amount of item, 3 to confirm your order");
        int x =int.Parse(Console.ReadLine());
        int id;
        switch (x)
        {
            case 1:
                {
                    Console.WriteLine("enter id of product");
                    id=int.Parse(Console.ReadLine());

                }
                break;
            case 2:
                {

                }
                break;
            case 3:
                {

                }
                break;

            default:
                {
                    Console.WriteLine("enter 1 to Add item to the cart, 2 to update amount of item, 3 to confirm your order");
                    x = int.Parse(Console.ReadLine());
                }
                break;
        }
    }
    public static void Main()
    {
        int number;
        do
        {
            Console.WriteLine("Enter 1 to product");
            Console.WriteLine("Enter 2 to order");
            Console.WriteLine("Enter 3 to cart");
            Console.WriteLine("Enter 0 to exit");
            number = int.Parse(Console.ReadLine());
            //Switch to select the desired entity and throws a comment in case it is not selected
            switch (number)
            {
                case 1:
                    try
                    {
                        Product();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                    break;
                case 2:
                    try
                    {
                        Order();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                    break;
                case 3:
                    try
                    {
                        Cart();
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