﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class OrderForList
    {
        // <summary>
        /// Unique ID of OrderForList
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// The customer's name of this order
        /// </summary>
        public string? CustomerName { get; set; }
        /// <summary>
        /// The status of this order
        /// </summary>
        public OrderStatus? Status { get; set; }
        /// <summary>
        /// The amount of the items in this order
        /// </summary>
        public int AmountOfItems { get; set; }
        /// <summary>
        /// The total price of this order
        /// </summary>
        public double TotalPrice { get; set; }
        /// <summary>
        /// Overriding method of the ToString 
        /// </summary>
        /// <returns>Returns a string describing a orderForList</returns>
        public override string ToString()
        {
            return ClassToString.ToStringProperty(this);
        }

    }
}
