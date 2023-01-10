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
                if (cart.Items == null) cart.Items = new List<BO.OrderItem?>();

                bool exist = false;
                if (cart.Items?.Any(item => item?.ProductId == id) == true)
                {
                    exist = true;
                }
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
        //The purpose of the function is to update the quantity of a product in the current shopping basket and returns the updated shopping basket.
        public BO.Cart Update(BO.Cart cart, int idProduct, int amount)
        {
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
                return cart;
            }
            catch (DO.NotExistException ex)
            {
                throw new BO.NotExistBlException("not exist", ex);
            }
        }
        //The purpose of the function is to make the order that is in the customer's shopping cart.
        public void MakeAnOrder(BO.Cart cart)
        {
            if (cart.CustomerAddress == "" || cart.CustomerName == "" || cart.CustomerEmail == "")
                throw new BO.NotValidException("missing details");

            //var updatedCart = cart.Items!.FindAll(item => item?.Amount > 0);

            foreach (BO.OrderItem? item in cart.Items!)
            {
                if (item?.Amount < 0)
                    throw new BO.NotValidException("cannot be negative amount");
                DO.Product doProduct = idal!.Product.Get(item?.ProductId ?? 0);
                if (doProduct.AmountInStock - item?.Amount < 0)
                    throw new BO.NotValidException("there is not enough products in stock");
            }
            DO.Order order = new DO.Order()
            {
                CustomerAddress = cart.CustomerAddress,
                CustomerName = cart.CustomerName,
                CustomerEmail = cart.CustomerEmail,
                DeliveryDate = null,
                ShipDate = null,
                OrderDate = DateTime.Now
            };
            int id;
            try
            {
                id = idal!.Order.Add(order);
                DO.OrderItem orderItem = new DO.OrderItem();
                foreach (BO.OrderItem? itemBO in cart.Items)
                {
                    try
                    {
                        DO.Product doProduct = idal.Product.Get(itemBO?.ProductId ?? 0);
                        doProduct.AmountInStock -= itemBO?.Amount ?? 0;
                        idal.Product.Update(doProduct);
                        orderItem.OrderId = id;
                        orderItem.ProductId = itemBO?.ProductId ?? 0;
                        orderItem.PricePerUnit = itemBO?.TotalPrice ?? 0;
                        orderItem.Amount = itemBO?.Amount ?? 0;
                        idal.OrderItem.Add(orderItem);
                    }
                    catch (DO.ExistException x)
                    {
                        throw new BO.AlreadyExistBlException("alerady exist", x);
                    }
                }
            }
            catch (DO.ExistException ex)
            {
                throw new BO.AlreadyExistBlException("alerady exist", ex);
            }
        }

        public void OrderConfirmation(BO.Cart boCart)
        {
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
        }
    }
}