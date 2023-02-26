using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalApi;
using System.Xml.Linq;
using DO;
using System.Runtime.CompilerServices;

namespace Dal;

internal class Product : IProduct
{
    const string s_product = @"Product";

    [MethodImpl(MethodImplOptions.Synchronized)]
    static DO.Product? createProductfromXElement(XElement p)
    {
        return new DO.Product()
        {
            ProductId= p.ToIntNullable("ProductId") ?? throw new FormatException("id"),
            ProductName = (string?)p.Element("ProductName"),
            Category = convertCategory((string?)p.Element("Category")),
            Price = Convert.ToInt32(p.Element("Price")!.Value),
            AmountInStock = Convert.ToInt32(p.Element("AmountInStock")!.Value),
        };
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public int Add(DO.Product product)
    {
        XElement productsRootElem = XMLTools.LoadListFromXMLElement(s_product);
        XElement? prod = (from pr in productsRootElem.Elements()
                          where pr.ToIntNullable("ProductId") == product.ProductId
                          select pr).FirstOrDefault();
        if (prod != null)
            throw new DO.ExistException(product.ProductId, "product");

        XElement prodElement = new XElement("Product",
                                   new XElement("ProductId", product.ProductId),
        new XElement("ProductName", product.ProductName),
        new XElement("Price", product.Price),
        new XElement("AmountInStock", product.AmountInStock),
        new XElement("Category", product.Category));
        productsRootElem.Add(prodElement);

        XMLTools.SaveListToXMLElement(productsRootElem, s_product);

        return product.ProductId; ;
    }


    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Delete(int id)
    {
        XElement productsRootElem = XMLTools.LoadListFromXMLElement(s_product);

        XElement? prod = (from pr in productsRootElem.Elements()
                          where (int?)pr.Element("ProductId") == id
                          select pr).FirstOrDefault() ?? throw new DO.NotExistException(id, "product");

        prod.Remove(); //<==>   Remove prod from productsRootElem

        XMLTools.SaveListToXMLElement(productsRootElem, s_product);
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public DO.Product Get(int id)
    {
        XElement productsRootElem = XMLTools.LoadListFromXMLElement(s_product);

        return (from p in productsRootElem?.Elements()
                where p.ToIntNullable("ProductId") == id
                select (DO.Product?)createProductfromXElement(p)).FirstOrDefault()
                ?? throw new DO.NotExistException(id, "product");
   
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<DO.Product?> GetAll(Func<DO.Product?, bool>? check = null)
    {
        XElement? productsRootElem = XMLTools.LoadListFromXMLElement(s_product);
        if (check != null)
        {
            return from p in productsRootElem.Elements()
                   let doProduct = createProductfromXElement(p)
                   where check(doProduct)
                   select (DO.Product?)doProduct;
        }
        else
        {
            return from p in productsRootElem.Elements()
                   select createProductfromXElement(p);
        }
    }


    [MethodImpl(MethodImplOptions.Synchronized)]
    public DO.Product? GetByCondition(Func<DO.Product?, bool>? check)
    {
        XElement? productsRootElem = XMLTools.LoadListFromXMLElement(s_product);
        return (from p in productsRootElem.Elements()
                let doProduct = createProductfromXElement(p)
                where check!(doProduct)
                select (DO.Product?)doProduct).FirstOrDefault() ?? throw new DO.NotExistException(0, "product");
    }


    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Update(DO.Product updateProd)
    {
        Delete(updateProd.ProductId);
        Add(updateProd);
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    private static DO.Categories convertCategory(string category)
    {
        switch (category)
        {
            case "Chagim":
                return DO.Categories.Chagim;
                break;
            case "HomeTextiles":
                return Categories.HomeTextiles;
                break;
            case "Judaica":
                return Categories.Judaica;
                break;
            case "DesignedGifts":
                return Categories.DesignedGifts;
                break;
            case "HomeAccessories":
                return Categories.HomeAccessories;
                break;
            default: return Categories.Chagim;
        }
    }
}
