using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class OrderTracking
    {
        /// <summary>
        /// Unique ID of OrderTracking
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// The status of orderTracking
        /// </summary>
        
        public OrderStatus? status;
        public List<Tuple<DateTime?, string?>?>? Tracking { get; set; }
        /// <summary>
        /// Overriding method of the ToString 
        /// </summary>
        /// <returns>Returns a string describing a orderTracking</returns>
        public override string ToString()
        {
            return ClassToString.ToStringProperty(this);
        }

    }
}
