using Dal;
using DalApi;


namespace BlImplementation
{
    internal class Cart:BlApi.ICart
    {
        IDal idal = new Dallist();
        //The purpose of the function is to allow the customer to add a product to the current shopping cart.
        public BO.Cart Add(BO.Cart cart, int id)
        {
            bool isExist = false, isInStock = false;
            DO.Product product = idal.Product.Get(id);
            foreach (BO.OrderItem item in cart.Items)
            {
                if (item.ProductId == id)
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
        //The purpose of the function is to update the quantity of a product in the current shopping basket and returns the updated shopping basket.
        public BO.Cart Update(BO.Cart cart, int id, int amount)
        {
            DO.Product product = idal.Product.Get(id);
            foreach(BO.OrderItem item in cart.Items)
            {
                if (id==item.ProductId)
                {
                    if (amount == 0)
                        cart.Items.Remove(item);
                    else if(product.AmountInStock>=amount) 
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
        //The purpose of the function is to make the order that is in the customer's shopping cart.
        public void MakeAnOrder(BO.Cart cart)
        {
            if (cart.CustomerAddress == "" || cart.CustomerName == "" || cart.CustomerEmail == "")
                throw new Exception("missing details");
            foreach (var item in cart.Items)
            {
                if (item.Amount < 0)
                    throw new Exception("cannot be negative amount");
                DO.Product doProduct = idal.Product.Get(item.ProductId);
                if (doProduct.AmountInStock - item.Amount < 0)
                    throw new Exception("there is not enough products in stock ");
            }
            DO.Order order=new DO.Order();
            int id;
            order.CustomerAddress = cart.CustomerAddress;
            order.CustomerName = cart.CustomerName;
            order.CustomerEmail = cart.CustomerEmail;
            order.DeliveryDate = DateTime.MinValue;
            order.ShipDate = DateTime.MinValue;
            order.OrderDate = DateTime.Now;
            id = idal.Order.Add(order);
            DO.OrderItem orderItem = new DO.OrderItem();
            foreach(var itemBO in cart.Items)
            {
                DO.Product doProduct = idal.Product.Get(itemBO.ProductId);
                doProduct.AmountInStock -= itemBO.Amount;
                idal.Product.Update(doProduct);
                orderItem.OrderId = id;
                orderItem.ProductId = itemBO.ProductId;
                orderItem.PricePerUnit = itemBO.TotalPrice;
                orderItem.Amount = itemBO.Amount;
                idal.OrderItem.Add(orderItem);
            }
        }
    }
}
