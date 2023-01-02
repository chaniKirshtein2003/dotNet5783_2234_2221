using BO;


namespace BlImplementation
{
    internal class Cart : BlApi.ICart
    {
        DalApi.IDal? idal = DalApi.Factory.Get();
        //The purpose of the function is to allow the customer to add a product to the current shopping cart.
        public BO.Cart Add(BO.Cart cart, int id)
        {
            bool isExist = false, isInStock = false;
            try
            {
                DO.Product product = idal!.Product.Get(id);
                foreach (BO.OrderItem? item in cart.Items!)
                {
                    if (item?.ProductId == id)
                    {
                        isExist = true;
                        if (product.AmountInStock > 0)
                        {
                            isInStock = true;
                            item.TotalPrice += item.Price;
                            item.Amount++;
                        }
                    }
                }
                if (!isExist && product.AmountInStock > 0)
                {
                    isInStock = true;
                    BO.OrderItem item = new BO.OrderItem();
                    item.OrderItemName = product.ProductName;
                    item.Price = product.Price;
                    item.ProductId = id;
                    item.Amount = 1;
                    item.TotalPrice = product.Price;
                    cart.Items.Add(item);
                }
                if (isInStock)
                    cart.TotalPrice += product.Price;
                return cart;
            }
            catch (Exception ex)
            {
                throw new BO.NotExistBlException("not exist", ex);
            }

        }
        //The purpose of the function is to update the quantity of a product in the current shopping basket and returns the updated shopping basket.
        public BO.Cart Update(BO.Cart cart, int id, int amount)
        {
            try
            {
                DO.Product product = idal!.Product.Get(id);
                foreach (BO.OrderItem? item in cart.Items!)
                {
                    if (id == item?.ProductId)
                    {
                        if (amount == 0)
                            cart.Items.Remove(item);
                        else if (product.AmountInStock >= amount)
                        {
                            item.Amount = amount;
                            item.Price = product.Price;
                            item.TotalPrice = product.Price * amount;
                            cart.TotalPrice += product.Price;
                        }
                    }
                }
                return cart;

            }
            catch (Exception ex)
            {
                throw new BO.NotExistBlException("not exist", ex);
            }
        }
        //The purpose of the function is to make the order that is in the customer's shopping cart.
        public void MakeAnOrder(BO.Cart cart)
        {
            if (cart.CustomerAddress == "" || cart.CustomerName == "" || cart.CustomerEmail == "")
                throw new BO.NotValidException("missing details");
            foreach (BO.OrderItem? item in cart.Items!)
            {
                if (item?.Amount < 0)
                    throw new BO.NotValidException("cannot be negative amount");
                DO.Product doProduct = idal!.Product.Get(item?.ProductId??0);
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
            order.OrderDate = null;
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
                    catch (Exception x)
                    {
                        throw new BO.AlreadyExistBlException("alerady exist", x);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new BO.AlreadyExistBlException("alerady exist", ex);
            }
        }
    }
}
