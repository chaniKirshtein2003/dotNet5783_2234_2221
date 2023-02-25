using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Dal
{
    internal class Config
    {
        static string s_config = "config";
        internal static int orderNextId()
        {
            int x= (int)XMLTools.LoadListFromXMLElement(s_config).Element("orderNextId")!;
            return x;
        }
        internal static void SaveNextOrderNumber(int num)
        {
            XElement root = XMLTools.LoadListFromXMLElement(s_config);
            root.Element("orderNextId")!.SetValue(num.ToString());
            XMLTools.SaveListToXMLElement(root, s_config);
        }


        internal static int NextOrderItemNumber()
        {
            return (int)XMLTools.LoadListFromXMLElement(s_config).Element("orderItemNextId")!;
        }
        internal static void SaveNextOrderItemNumber(int num)
        {
            XElement root = XMLTools.LoadListFromXMLElement(s_config);
            root.Element("orderItemNextId")!.SetValue(num.ToString());
            XMLTools.SaveListToXMLElement(root, s_config);
        }
    }
}