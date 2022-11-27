using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalApi;
using Dal;
using System.Reflection.Metadata.Ecma335;
using DO;
using BO;
using BlApi;

namespace BlImplementation
{
    internal class Product : BlApi.IProduct
    {
        IDal idal = new Dallist();
        public IEnumerable<BO.ProductForList> GetProductForList()
        {
            List<BO.ProductForList> products = new List<BO.ProductForList>();
            foreach (var item in idal.Product.GetAll())
            {
                BO.ProductForList product = new BO.ProductForList();
                product.Name = item.productName;
                product.price = item.price;
                //product.category = item.CategoryP;  
                product.ID = item.productId;
                products.Add(product);
            }
            return products;
        }
        public BO.Product GetProductById(int id)
        {
            DO.Product product = idal.Product.Get(id);
            BO.Product newProduct = new BO.Product();
            newProduct.productName= product.productName;
            newProduct.price= product.price;
            newProduct.productId= product.productId;
            // newProduct.category = product.category;
            newProduct.amountInStock = product.amountInStock;
            return newProduct;
        }
        public int Add(BO.Product product)
        {
            if (product.productId < 0)
                throw new ArgumentException("Id must be positive");
            if (product.price < 0)
                throw new ArgumentException("Price must be positive");
            if (product.amountInStock < 0)
                throw new ArgumentException("AmountInStock must be positive");
            if (product.productName == "")
                throw new ArgumentException("Price must be positive");
            DO.Product newProduct = new DO.Product();
            newProduct.productName = product.productName;
            newProduct.price = product.price;   
            newProduct.productId= product.productId;
            newProduct.amountInStock= product.amountInStock;
            //newProduct.category = product.category;
            int id=idal.Product.Add(newProduct);
            return id;
        }
        public void Delete(int id)
        {
            IEnumerable<DO.OrderItem> orderItemList;
            IEnumerable<DO.Order> orderList=new List<DO.Order>();
            foreach(DO.Order order in orderList)
            {
                orderItemList = idal.OrderItem.GetAllProductsOfOrder(order.orderId);
                foreach (DO.OrderItem item in orderItemList)
                    if (item.orderItemId == id)
                        throw new Exception("the product is exist");
            }
            idal.Product.Delete(id);
        }
        public void Update(BO.Product product)
        {
            if (product.productId < 0)
                throw new ArgumentException("Id must be positive");
            if (product.price < 0)
                throw new ArgumentException("Price must be positive");
            if (product.amountInStock < 0)
                throw new ArgumentException("AmountInStock must be positive");
            if (product.productName == "")
                throw new ArgumentException("Price must be positive");
            DO.Product updateProduct = new DO.Product();
            updateProduct.productName = product.productName;
            updateProduct.price = product.price;
            updateProduct.productId = product.productId;
            updateProduct.amountInStock = product.amountInStock;
            //newProduct.category = product.category;
            idal.Product.Update(updateProduct);
        }
        public IEnumerable<OrderForList> ListOrderToManager()
        {
            IEnumerable<DO.OrderItem> orderItemList;
            IEnumerable<DO.Order> orderList = idal.Order.GetAll();
            IEnumerable<BO.OrderForList> orderForList = new List<BO.OrderForList>();
            BO.OrderForList order = new BO.OrderForList();
            foreach (DO.Order item in orderList)
            {
                orderItemList = idal.OrderItem.GetAllProductsOfOrder(item.orderId);
                order.ID = item.orderId;
                order.customerName = item.customerName;
               // order.status = 
               order.amountOfItems=
            }
            
        }
    }
}
