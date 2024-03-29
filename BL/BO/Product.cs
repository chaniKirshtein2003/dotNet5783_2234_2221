﻿using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Product
    {
        /// <summary>
        /// Unique ID of Product
        /// </summary>
        public int ProductId { get; set; }
        /// <summary>
        /// Product name
        /// </summary>
        public string? ProductName { get; set; }
        /// <summary>
        /// The price of the product
        /// </summary>
        public double Price { get; set; }
        /// <summary>
        /// The category of the product
        /// </summary>
        public Categories? Category { get; set; }
        /// <summary>
        /// Amount in stock of the product
        /// </summary>
        public int AmountInStock { get; set; }
        /// <summary>
        /// Overriding method of the ToString 
        /// </summary>
        /// <returns>Returns a string describing a product</returns>
        public override string ToString()
        {
            return ClassToString.ToStringProperty(this);
        }
    }
}
