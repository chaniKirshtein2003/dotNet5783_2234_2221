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
                if (cart.Items?.FirstOrDefault(item => item?.ProductId == id) != null)
                {
                    throw new BO.AlreadyExistBlException("product exist in cart");
                }
                product = (DO.Product)idal!.Product.GetByCondition(product2 => product2?.ProductId == id)!;

                if (product.AmountInStock <= 0)
                    throw new BO.NotExistBlException("product not exist in stock");
                orderItem.OrderItemName = product.ProductName;
                orderItem.ProductId = id;
                orderItem.Amount = 1;
                orderItem.Price = product.Price;
                orderItem.TotalPrice = orderItem.Price * orderItem.Amount;
                cart.Items?.Add(orderItem);
                cart.TotalPrice += orderItem.TotalPrice;
                return cart;
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
            DO.Order order = new DO.Order();
            int id;
            order.CustomerAddress = cart.CustomerAddress;
            order.CustomerName = cart.CustomerName;
            order.CustomerEmail = cart.CustomerEmail;
            order.DeliveryDate = null;
            order.ShipDate = null;
            order.OrderDate = DateTime.Now;
            try
            {
                id = idal!.Order.Add(order);
                DO.OrderItem orderItem = new DO.OrderItem();
                foreach (BO.OrderItem? itemBO in cart.Items)
                {
                    try
                    {
                        DO.Product doProduct = idal.Product.Get(itemBO?.ProductId??0);
                        doProduct.AmountInStock -= itemBO?.Amount??0;
                        idal.Product.Update(doProduct);
                        orderItem.OrderId = id;
                        orderItem.ProductId = itemBO?.ProductId??0;
                        orderItem.PricePerUnit = itemBO?.TotalPrice??0;
                        orderItem.Amount = itemBO?.Amount??0;
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
    }
}
