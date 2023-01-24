using DalApi;


namespace BlImplementation
{
    internal class Cart : BlApi.ICart
    {
        DalApi.IDal? idal = DalApi.Factory.Get();
        /// <summary>
        ///function that adds a product to the cart
        /// </summary>
        /// <param name="cart"></param>
        /// <param name="id"></param>
        /// <returns>update cart</returns>
        /// <exception cref="BO.AlreadyExistBlException"></exception>
        /// <exception cref="BO.NotExistBlException"></exception>
        public BO.Cart Add(BO.Cart cart, int id)
        {
            try
            {
                DO.Product product = new DO.Product();
                BO.OrderItem orderItem = new BO.OrderItem();
                product = (DO.Product)idal!.Product.GetByCondition(product2 => product2?.ProductId == id)!;
                if (cart.Items == null)
                    cart.Items = new List<BO.OrderItem?>();
                bool exist = false;
                if (cart.Items?.Any(item => item?.ProductId == id) == true)
                    exist = true;
                if (exist)
                {
                    BO.OrderItem? ord = cart.Items?.FirstOrDefault(orderItem => orderItem?.ProductId == id);//find this item in cart
                    cart.Items?.Remove(ord);//delete it
                    ord!.Amount += 1;
                    ord.TotalPrice += ord.Price;//for only one item
                    cart.TotalPrice += product.Price;//update the total price of all the cart
                    cart.Items?.Add(ord);//and add the updated
                    return cart;
                }
                else
                {
                    BO.OrderItem newOrderItem = new BO.OrderItem()//creat a new OrderItem
                    {
                        // OrderItemId = id,
                        OrderItemName = product.ProductName,
                        Price = product.Price,
                        ProductId = product.ProductId,
                        Amount = 1,
                        TotalPrice = product.Price
                    };
                    cart.Items?.Add(newOrderItem);//add to list of cart
                    cart.TotalPrice += product.Price;//update the total price of all the cart
                    return cart;// return the cart
                }
            }
            catch (DO.NotExistException ex)
            {
                throw new BO.NotExistBlException("not exist", ex);
            }
        }
        /// <summary>
        /// The purpose of the function is to update the quantity of a product in the current shopping basket and returns the updated shopping basket.
        /// </summary>
        /// <param name="cart"></param>
        /// <param name="idProduct"></param>
        /// <param name="amount"></param>
        /// <returns>Return the cart after update</returns>
        /// <exception cref="BO.NotExistBlException"></exception>
        public BO.Cart Update(BO.Cart cart, int idProduct, int amount)
        {
            BO.Cart cart1 = new BO.Cart();
            try
            {
                DO.Product product = idal!.Product.Get(idProduct);
                var item = cart.Items!.FirstOrDefault(x => x?.ProductId == idProduct);
                if (amount == 0)
                    cart.Items!.Remove(item);
                else
                    if (product.AmountInStock >= amount)
                {
                    item!.Amount = amount;
                    item.Price = product.Price;
                    item.TotalPrice = product.Price * amount;
                    cart.TotalPrice += product.Price * amount;

                }
                cart1.CustomerAddress = cart.CustomerAddress;
                cart1.CustomerEmail = cart.CustomerEmail;
                cart1.CustomerName = cart.CustomerName;
                cart1.Items = new(cart.Items!);
                cart1.TotalPrice = cart.TotalPrice;
                return cart1;
            }
            catch (DO.NotExistException ex)
            {
                throw new BO.NotExistBlException("not exist", ex);
            }
        }
        /// <summary>
        /// The purpose of the function is to make the order that is in the customer's shopping cart.
        /// </summary>
        /// <param name="cart"></param>
        /// <exception cref="BO.NotValidException"></exception>
        /// <exception cref="Exception"></exception>
        /// <exception cref="BO.AlreadyExistBlException"></exception>
        //public void MakeAnOrder(BO.Cart cart)
        //{
            //if (cart.CustomerAddress == "" || cart.CustomerName == "" || cart.CustomerEmail == "")
            //    throw new BO.NotValidException("missing details");
            //if (cart.Items == null)
            //    throw new Exception("The cart is empty");
            ////var updatedCart = cart.Items!.FindAll(item => item?.Amount > 0);

            //foreach (BO.OrderItem? item in cart.Items!)
            //{
            //    if (item?.Amount < 0)
            //        throw new BO.NotValidException("cannot be negative amount");
            //    DO.Product doProduct = idal!.Product.Get(item?.ProductId ?? 0);
            //    if (doProduct.AmountInStock - item?.Amount < 0)
            //        throw new BO.NotValidException("there is not enough products in stock");
            //}
            //DO.Order order = new DO.Order()
            //{
            //    CustomerAddress = cart.CustomerAddress,
            //    CustomerName = cart.CustomerName,
            //    CustomerEmail = cart.CustomerEmail,
            //    DeliveryDate = null,
            //    ShipDate = null,
            //    OrderDate = DateTime.Now
            //};
            //int id;
            //try
            //{
            //    id = idal!.Order.Add(order);
            //    DO.OrderItem orderItem;
            //    foreach (BO.OrderItem? itemBO in cart.Items)
            //    {
            //        try
            //        {
            //            DO.Product doProduct = idal.Product.Get(itemBO?.ProductId ?? 0);
            //            doProduct.AmountInStock -= itemBO?.Amount ?? 0;
            //            idal.Product.Update(doProduct);
            //            orderItem = new DO.OrderItem();
            //            orderItem.OrderId = id;
            //            orderItem.ProductId = itemBO?.ProductId ?? 0;
            //            orderItem.PricePerUnit = itemBO?.TotalPrice ?? 0;
            //            orderItem.Amount = itemBO?.Amount ?? 0;
            //            idal.OrderItem.Add(orderItem);
            //        }
            //        catch (DO.ExistException x)
            //        {
            //            throw new BO.AlreadyExistBlException("alerady exist", x);
            //        }
            //    }
            //}
            //catch (DO.ExistException ex)
            //{
            //    throw new BO.AlreadyExistBlException("alerady exist", ex);
            //}

            //if (boCart.CustomerAdress == "")
            //    throw new BO.NotEnoughDetailsException("customerAsress");
            //if (boCart.CustomerEmail == "")
            //    throw new BO.NotEnoughDetailsException("customerEmail");
            //if (boCart.CustomerName == "")
            //    throw new BO.NotEnoughDetailsException("customerName");
            //if (boCart.Items == null)
            //    throw new Exception("the cart is empty");
            //int negativeAmount = boCart.Items!.Count(x => x!.Amount < 0);
            //if (negativeAmount > 0)
        //    //    throw new BO.NotValidException("amount");


        //    try
        //    {
        //        cart.Items!.FindAll(x => idal!.Product.Get(x!.ProductId).AmountInStock - x.Amount > 0 ? true : throw new BO.NotInStockException(x.ProductId, x.OrderItemName!));
        //        DO.Order doOrder = new DO.Order() { CustomerAddress = cart.CustomerAddress, CustomerName = cart.CustomerName, CustomerEmail = cart.CustomerEmail, ShipDate = null, DeliveryDate = null, OrderDate = DateTime.Now };
        //        int id = idal!.Order.Add(doOrder);
        //        var allItems = from item in cart.Items
        //                       let product = idal!.Product.Get(item.ProductId)
        //                       let newProd = new DO.Product { ProductId = product.ProductId, ProductName = product.ProductName, Price = product.Price, AmountInStock = product.AmountInStock - item.Amount, Category = product.Category }
        //                       let updateAmount = UpdateAmountDal(newProd)
        //                       select new DO.OrderItem() { Amount = item!.Amount, ProductId = item.ProductId, OrderId = id, PricePerUnit = newProd.Price };
        //        allItems.All(x => idal.OrderItem.Add(x) > 0 ? true : false);
        //    }
        //    catch (DO.ExistException ex)
        //    {
        //        throw new BO.AlreadyExistBlException("order alredy exist cannot add- ", ex);
        //    }
        //    catch (DO.NotExistException ex)
        //    {
        //        throw new BO.NotExistBlException("product does not exist-", ex);
        //    }
        //}

        //public void OrderConfirmation(BO.Cart boCart)
        //{
        //if (boCart.CustomerAdress == "")
        //    throw new BO.NotEnoughDetailsException("customerAsress");
        //if (boCart.CustomerEmail == "")
        //    throw new BO.NotEnoughDetailsException("customerEmail");
        //if (boCart.CustomerName == "")
        //    throw new BO.NotEnoughDetailsException("customerName");
        //if (boCart.Items == null)
        //    throw new Exception("the cart is empty");
        //int negativeAmount = boCart.Items!.Count(x => x!.Amount < 0);
        //if (negativeAmount > 0)
        //    throw new BO.NotValidException("amount");


        //    try
        //    {
        //        boCart.Items!.FindAll(x => idal!.Product.Get(x!.ProductId).AmountInStock - x.Amount > 0 ? true : throw new BO.NotInStockException(x.ProductId, x.ProductName!));
        //        DO.Order doOrder = new DO.Order() { CustomerAddress = boCart.CustomerAddress, CustomerName = boCart.CustomerName, CustomerEmail = boCart.CustomerEmail, ShipDate = null, DeliveryDate = null, OrderDate = DateTime.Now };
        //        int id = idal!.Order.Add(doOrder);
        //        var allItems = from item in boCart.Items
        //                       let product = idal!.Product.Get(item.ProductId)
        //                       let newProd = new DO.Product { ID = product.ID, Name = product.Name, Price = product.Price, InStock = product.InStock - item.Amount, CategoryP = product.CategoryP }
        //                       let updateAmount = UpdateAmountDal(newProd)
        //                       select new DO.OrderItem() { Amount = item!.Amount, ProductId = item.ProductId, OrderId = id, Price = newProd.Price };
        //        allItems.All(x => idal.OrderItem.Add(x) > 0 ? true : false);
        //    }
        //    catch (DO.ExistException ex)
        //    {
        //        throw new BO.AlreadyExistBlException("order alredy exist cannot add- ", ex);
        //    }
        //    catch (DO.NotExistException ex)
        //    {
        //        throw new BO.NotExistBlException("product does not exist-", ex);
        //    }
        //}
        //}
        public void MakeAnOrder(BO.Cart cart)
        {
            if (cart.Items?.Count() == 0)//there aren't items in cart
                throw new BO.NotValidException("Cart list of items");
            //checking propriety of the customer's name (not empty)
            if (cart.CustomerName == "" || cart.CustomerName == null)
                throw new BO.NotValidException("Customer-Name");
            //checking propriety of the customer's address (not empty)
            if (cart.CustomerAddress == "" || cart.CustomerAddress == null)
                throw new BO.NotValidException("Customer-Address");
            //checking propriety of the customer's email (not empty and contains '@' in the middle)
            if ((cart.CustomerEmail == "" || cart.CustomerEmail == null)
                || (cart.CustomerEmail?.IndexOf('@') == -1 || cart.CustomerEmail?.IndexOf('@') == 0
                || cart.CustomerEmail?.IndexOf('@') == cart.CustomerEmail?.Length-1))
                throw new BO.NotValidException("Customer-Email");

            //for each item, checking if the item exists in the dal list:
            IEnumerable<DO.Product> doProducts;
            try
            {
                cart.Items!.FindAll(x => idal!.Product.Get(x!.ProductId).AmountInStock - x.Amount > 0 ? true : throw new BO.NotInStockException(x.ProductId, x.OrderItemName!));
                DO.Order doOrder = new DO.Order() { CustomerAddress = cart.CustomerAddress, CustomerName = cart.CustomerName, CustomerEmail = cart.CustomerEmail, ShipDate = null, DeliveryDate = null, OrderDate = DateTime.Now };
                int id = idal!.Order.Add(doOrder);
                var allItems = from item in cart.Items
                               let product = idal!.Product.Get(item.ProductId)
                               let newProd = new DO.Product { ProductId = product.ProductId, ProductName = product.ProductName, Price = product.Price, AmountInStock = product.AmountInStock - item.Amount, Category = product.Category }
                            //   let updateAmount = UpdateAmountDal(newProd)
                               select new DO.OrderItem() { Amount = item!.Amount, ProductId = item.ProductId, OrderId = id, PricePerUnit = newProd.Price };
                allItems.All(x => idal.OrderItem.Add(x) > 0 ? true : false);
                doProducts = from item in cart.Items
                             select idal?.Product.Get(item.ProductId) ?? throw new Exception("");
            }
            catch (DO.NotExistException ex) //it doesn't exist
            {
                throw new BO.NotExistBlException("Product does not exist", ex);
            }
            int indexItem = cart.Items!.ToList().FindIndex(x => x?.Amount <= 0);
            if (indexItem != -1) //there is an item which has a non-positive amount
                throw new BO.NotValidException($"Amount (of item {cart.Items!.ToList()[indexItem]?.OrderItemName})");
            //for each item, checking if the item is in stock
            try
            {
                indexItem = cart.Items!.ToList().FindIndex(x => x.Amount > doProducts.First(y => y.ProductId == x.ProductId).AmountInStock);
            }
            catch (DO.NotExistException ex) //it doesn't exist
            {
                throw new BO.NotExistBlException("Product does not exist", ex);
            }
            if (indexItem != -1) //there is an item which isn't in stock
                throw new BO.NotInStockException(cart.Items!.ToList()[indexItem]!.ProductId, cart.Items!.ToList()[indexItem]!.OrderItemName!);
            //create a new order due to the details in the cart+add it to dal and get the ID:
            DO.Order newOrder = new DO.Order
            {
                CustomerName = cart.CustomerName,
                CustomerAddress = cart.CustomerAddress,
                CustomerEmail = cart.CustomerEmail,
                OrderDate = DateTime.Now,
                DeliveryDate = null,
                ShipDate = null,
            };
            int orderID = idal!.Order.Add(newOrder);
            //creating a list of DO.orderItems due to cart and orderID:
            var orderItem = from item in cart.Items
                            select new DO.OrderItem
                            {
                                ProductId = item?.ProductId ?? throw new BO.NotValidException("Product ID"),
                                OrderId = orderID,
                                PricePerUnit = item?.Price ?? 0,
                                Amount = item?.Amount ?? 0,
                                //PictureName = item?.PictureName ?? @"\pics\img.jpg",
                            };
            //add each item to dal:
            orderItem.Select(x => idal.OrderItem.Add(x)).ToList();  //ToList so the method will be immidiate
                                                                    //creating a list of updated DO.product:
            var updateProduct = from item in doProducts
                                select new DO.Product
                                {
                                    ProductId = item.ProductId,   //while building the doProducts we already made sure that id exist so there is no need in exception
                                    ProductName = item.ProductName ?? "",
                                    Price = item.Price,
                                    Category = item.Category,
                                    AmountInStock = item.AmountInStock - cart.Items!.First(x => x?.ProductId == item.ProductId)!.Amount,  //update the amount
                                                                                                                                         //PictureName = item.PictureName ?? @"\pics\img.jpg",
                                };
            //add each updated product to dal:
            try
            {
                updateProduct.ToList().ForEach(x => idal.Product.Update(x));
            }
            catch (DO.NotExistException ex)
            {
                throw new BO.NotExistBlException("Product does not exist", ex);
            }
            //after the order was made the cart is empty:
            ((List<BO.OrderItem?>)(cart.Items!)).Clear();
            cart.TotalPrice = 0;
        }
    }
}