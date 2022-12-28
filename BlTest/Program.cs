using DO;

public class Program
{
   static BlApi.IBl? bl = BlApi.Factory.Get();

    //Functions for orders
    public static void Order()
    {
        BO.Order order;
        int number, id;
        Console.WriteLine("Insert 1 to get all orders");
        Console.WriteLine("2 to get order by ID");
        Console.WriteLine("3 to update sending of order");
        Console.WriteLine("4 to supply update");
        Console.WriteLine("5 to orderTracking");
        number = int.Parse(Console.ReadLine()?? throw new EmptyInput("your choice"));
        switch (number)
        {
            case 1:
                IEnumerable<BO.OrderForList> orderForLists = bl!.Order.GetOrders();
                foreach (BO.OrderForList orderForList in orderForLists)
                    Console.WriteLine(orderForList);
                break;
            case 2:
                Console.WriteLine("insert order ID");
                id = int.Parse(Console.ReadLine() ?? throw new EmptyInput("id"));
                order = bl!.Order.GetOrderDetails(id);
                Console.WriteLine(order);
                break;
            case 3:
                Console.WriteLine("insert ID");
                id = int.Parse(Console.ReadLine() ?? throw new EmptyInput("id"));
                order = bl!.Order.UpdateSending(id);
                Console.WriteLine("update done succesfully");
                Console.WriteLine();
                Console.WriteLine(order);
                break;
            case 4:
                Console.WriteLine("insert ID");
                id = int.Parse(Console.ReadLine() ?? throw new EmptyInput("id"));
                order = bl!.Order.supplyUpdate(id);
                Console.WriteLine(order);
                break;
            case 5:
                BO.OrderTracking orderTracking = new BO.OrderTracking();
                Console.WriteLine("insert ID");
                id = int.Parse(Console.ReadLine() ?? throw new EmptyInput("id"));
                orderTracking = bl!.Order.OrderTracking(id);
                Console.WriteLine(orderTracking);
                break;
        }
    }
        //Functions for products
        public static void Product()
    {
        int number, id;
        Console.WriteLine("enter 1 to get all product,2 to get profuct by id,3 to add product,4 to delete product,5 to update,6 to get catalog,7 to get product item by id");
        number = int.Parse(Console.ReadLine() ?? throw new EmptyInput("your choice"));
        switch (number)
        {
            case 0:
                break;
            //Get all products
            case 1:
                {
                    IEnumerable<BO.ProductForList> products = bl!.Product.GetProducts();
                    foreach (BO.ProductForList product in products)
                    {
                        Console.WriteLine(product);
                    }
                }
                break;
            //Get product by ID
            case 2:
                {
                    Console.WriteLine("Insert id of product");
                    id = int.Parse(Console.ReadLine() ?? throw new EmptyInput("id"));
                    Console.WriteLine(bl!.Product.GetProduct(id)); 
                }
                break;
            //Add a new product
            case 3:
                {
                    //Entering the new data
                    Console.WriteLine("Insert id, name, price, amount in stock, and category: 1 for Chagim, 2 for HomeAccessories, 3 for HomeTextiles,4 for Judaica, 5 for DesignedGifts");
                    id = int.Parse(Console.ReadLine() ?? throw new EmptyInput("id"));
                    string name = Console.ReadLine() ?? throw new EmptyInput("name");
                    int price = int.Parse(Console.ReadLine()?? throw new EmptyInput("price"));
                    int amountInStock = int.Parse(Console.ReadLine()?? throw new EmptyInput("amount in stock"));
                    int category = int.Parse(Console.ReadLine()?? throw new EmptyInput("category"));
                    BO.Product product = new BO.Product();
                    product.ProductId = id;
                    product.ProductName = name;
                    product.Category = (BO.Categories)category;
                    product.Price = price;
                    product.AmountInStock = amountInStock;
                    Console.WriteLine(bl!.Product.Add(product));
                }
                break;
            //Delete a product
            case 4:
                {
                    Console.WriteLine("Insert id");
                    id = int.Parse(Console.ReadLine() ?? throw new EmptyInput("id"));
                    bl!.Product.Delete(id);
                }
                break;
            //Update the time details
            case 5:
                {
                    //Finding the product you want to update by its ID
                    Console.WriteLine("Insert id, name, price, amount in stock, and category: 1 for Chagim, 2 for HomeAccessories, 3 for HomeTextiles,4 for Judaica, 5 for DesignedGifts");
                    id = int.Parse(Console.ReadLine() ?? throw new EmptyInput("id"));
                    string name = Console.ReadLine() ?? throw new EmptyInput("name");
                    int price = int.Parse(Console.ReadLine()?? throw new EmptyInput("price"));
                    int amountInStock = int.Parse(Console.ReadLine()?? throw new EmptyInput("amount in stock"));
                    int category = int.Parse(Console.ReadLine()?? throw new EmptyInput("category"));
                    BO.Product product = new BO.Product();
                    product.ProductId = id;
                    product.ProductName = name;
                    product.Category = (BO.Categories)category;
                    product.Price = price;
                    product.AmountInStock = amountInStock;
                    bl!.Product.Update(product);
                    Console.WriteLine("done");
                }
                break;
            //get catalog of product
            case 6:
                foreach (var item in bl!.Product.ListProductsToBuy())
                {
                    Console.WriteLine(item);
                }
                break;
            //get product item by id
            case 7:
                {
                    Console.WriteLine("insert id");
                    id = int.Parse(Console.ReadLine() ?? throw new EmptyInput("id"));
                    Console.WriteLine(bl!.Product.ProductForBuyer(id));
                }
                break;
        }
    }
    //Functions for cart
    public static void Cart()
    {
        int number;
        Console.WriteLine("enter 1 to Add item to the cart, 2 to update amount of item, 3 to confirm your order");
        number = int.Parse(Console.ReadLine() ?? throw new EmptyInput("your choice"));
        //add product to cart
        Console.WriteLine("insert customer name, email, address");
        string name = Console.ReadLine() ?? throw new EmptyInput("name");
        string email = Console.ReadLine() ?? throw new EmptyInput("email");
        string adress = Console.ReadLine() ?? throw new EmptyInput("adress");
        BO.Cart cart = new BO.Cart();
        cart.CustomerName = name;
        cart.CustomerEmail = email;
        cart.CustomerAddress = adress;
        cart.Items = new List<BO.OrderItem?>();
        cart.TotalPrice = 0;
        switch (number)
        {
            case 1:
                {
                    Console.WriteLine("insert product id");
                    int id = int.Parse(Console.ReadLine() ?? throw new EmptyInput("id"));
                    Console.WriteLine(bl!.Cart.Add(cart, id));
                    break;
                }
            case 2:
                {
                    //update amount of product in cart
                    Console.Write("insert id of product you want to update: ");
                    int id = int.Parse(Console.ReadLine() ?? throw new EmptyInput("id"));
                    Console.Write("insert new amount: ");
                    int amount = int.Parse(Console.ReadLine()?? throw new EmptyInput("amount"));
                    bl!.Cart.Update(cart, id, amount);
                    break;
                }
            case 3:
                {
                    //to confirmation Order
                    bl!.Cart.MakeAnOrder(cart);
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
            Console.WriteLine("Enter 3 to cart");
            Console.WriteLine("Enter 0 to exit");
            number = int.Parse(Console.ReadLine()?? throw new EmptyInput("your choice"));
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
                        Console.WriteLine(ex.Message);
                    }
                    break;
                case 2:
                    try
                    {
                        Order();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;
                case 3:
                    try
                    {
                        Cart();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;

                default:
                    {
                        Console.WriteLine("Enter again");
                        number = int.Parse(Console.ReadLine()?? throw new EmptyInput("your choice"));
                        break;
                    }
            }
        }
        while (number != 0);
    }
}