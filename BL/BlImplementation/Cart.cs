using Dal;
using DalApi;


namespace BlImplementation
{
    internal class Cart:BlApi.ICart
    {
        IDal idal = new Dallist();
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
        public void MakeAnOrder(BO.Cart cart)
        {
            if (cart.customerAddress == "" || cart.customerName == "" || cart.customerEmail == "")
                throw new Exception("missing details");
            
            DO.Order order=new DO.Order();
            int id;
            order.customerAddress = cart.customerAddress;
            order.customerName = cart.customerName;
            order.customerEmail = cart.customerEmail;
            order.deliveryDate = DateTime.MinValue;
            order.shipDate = DateTime.MinValue;
            order.orderDate = DateTime.Now;
            try
            {
                id = idal.Order.Add(order);
            }
            catch (Exception)
            {

                throw;
            }
            DO.OrderItem orderItem = new DO.OrderItem();
            foreach(BO.OrderItem item in cart.items)
            {
                orderItem.orderId = id;
                orderItem.productId = item.productId;
                orderItem.pricePerUnit = item.price;
                orderItem.amount = item.amount;
                idal.OrderItem.Add(orderItem);
            }
            ///צריך לעדכן את המלאי של המוצרים שהוזמנו!!!
        }
    }
}
