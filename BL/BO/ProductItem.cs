using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class ProductItem
    {
        /// <summary>
        /// Unique ID of ProductItem
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// ProductItem name
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// The price of the productItem
        /// </summary>
        public double price { get; set; }
        /// <summary>
        /// The category of the productItem
        /// </summary>
        public Categories? category { get; set; }
        /// <summary>
        /// Amount in stock of the productItem
        /// </summary>
        public int amount { get; set; }
        /// <summary>
        /// Check if there is this productItem in the stock
        /// </summary>
        public bool inStock { get; set; }
        /// <summary>
        /// Overriding method of the ToString 
        /// </summary>
        /// <returns>Returns a string describing a productItem</returns>
        public override string ToString()
        {
            return ClassToString.ToStringProperty(this);
        }
    }
}
