using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    internal class ClassToString
    {
        public static string ToStringProperty<T>(T t)
        {

            string str = "";
            foreach (PropertyInfo item in t.GetType().GetProperties())
            {
                if (!(item.GetValue(t, null) is string) && item.GetValue(t, null) is IEnumerable<object>)
                {
                    foreach (var item2 in (IEnumerable<object>)item.GetValue(t, null))
                        str += item2.ToString();
                }
                else
                {
                    str += "\n" + item.Name

                        + ": " + item.GetValue(t, null);
                }
            }
            return str;
        }
    }
}
