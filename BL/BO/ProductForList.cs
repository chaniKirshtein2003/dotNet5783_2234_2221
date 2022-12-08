using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class ProductForList
    {
        /// <summary>
        /// Unique ID of ProductForList
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// ProductForList name
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// The price of the productForList
        /// </summary>
        public double price { get; set; }
        /// <summary>
        /// The category of the product
        /// </summary>
        public Categories? category { get; set; }
        /// <summary>
        /// Overriding method of the ToString 
        /// </summary>
        /// <returns>Returns a string describing a productForList</returns>
        public override string ToString()
        {
            return ClassToString.ToStringProperty(this);
        }

    }
}
