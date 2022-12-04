using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class NotExist:Exception
    {
        public NotExist(string message) : base(message) { }
    }
    public class Exist:Exception
    {
        public Exist(string message) : base(message) { }
    }
}
