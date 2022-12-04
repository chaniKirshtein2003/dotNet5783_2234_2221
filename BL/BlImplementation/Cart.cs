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
            foreach (BO.OrderItem item in cart.items)
            {
                if (item.productId == id)
                {
                    isExist = true;
                    if (product.amountInStock > 0)
                    {
                        isInStock = true;
                        item.totalPrice += item.price;
                        item.amount++;
                    }
                }
            }
            if (!isExist && product.amountInStock > 0)
            {
                isInStock = true;
                BO.OrderItem item = new BO.OrderItem();
                item.orderItemName = product.productName;
                item.price = product.price;
                item.productId = id;
                item.amount = 1;
                item.totalPrice = product.price;
                cart.items.Add(item);
            }
            if (isInStock)
                cart.totalPrice += product.price;
            return cart;
        }
        //The purpose of the function is to update the quantity of a product in the current shopping basket and returns the updated shopping basket.
        public BO.Cart Update(BO.Cart cart, int id, int amount)
        {
            DO.Product product = idal.Product.Get(id);
            foreach(BO.OrderItem item in cart.items)
            {
                if (id==item.productId)
                {
                    if (amount == 0)
                        cart.items.Remove(item);
                    else if(product.amountInStock>=amount) 
                    {
                        item.amount = amount;
                        item.price = product.price;
                        item.totalPrice = product.price * amount;
                        cart.totalPrice += product.price;
                    }
                }
            }
            return cart;
        }
        //The purpose of the function is to make the order that is in the customer's shopping cart.
        public void MakeAnOrder(BO.Cart cart)
        {
            if (cart.customerAddress == "" || cart.customerName == "" || cart.customerEmail == "")
                throw new Exception("missing details");
            foreach (var item in cart.items)
            {
                if (item.amount < 0)
                    throw new Exception("cannot be negative amount");
                DO.Product doProduct = idal.Product.Get(item.productId);
                if (doProduct.amountInStock - item.amount < 0)
                    throw new Exception("there is not enough products in stock ");
            }
            DO.Order order=new DO.Order();
            int id;
            order.customerAddress = cart.customerAddress;
            order.customerName = cart.customerName;
            order.customerEmail = cart.customerEmail;
            order.deliveryDate = DateTime.MinValue;
            order.shipDate = DateTime.MinValue;
            order.orderDate = DateTime.Now;
            id = idal.Order.Add(order);
            DO.OrderItem orderItem = new DO.OrderItem();
            foreach(var itemBO in cart.items)
            {
                DO.Product doProduct = idal.Product.Get(itemBO.productId);
                doProduct.amountInStock -= itemBO.amount;
                idal.Product.Update(doProduct);
                orderItem.orderId = id;
                orderItem.productId = itemBO.productId;
                orderItem.pricePerUnit = itemBO.totalPrice;
                orderItem.amount = itemBO.amount;
                idal.OrderItem.Add(orderItem);
            }
        }
    }
}
