using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
/// <summary>
/// all the do exception
/// </summary>
namespace DO
{
    /// <summary>
    /// Exception to product/order/orderItem does not exist in database
    /// </summary>
    [Serializable]
    public class DalConfigException : Exception
    {
        public DalConfigException(string msg) : base(msg) { }
        public DalConfigException(string msg, Exception ex) : base(msg, ex) { }
    }


    public class NotExistException : Exception
    {
        string name;
        int id;
        public NotExistException(int _id, string _name) : base()
        {
            id = _id;
            name = _name;
        }

        public override string ToString()
        {
            return $"{name} number {id} does not exist";
        }
    }
    [Serializable]
    /// <summary>
    /// Exception to product/order/orderItem already exist in database
    /// </summary>
    public class ExistException : Exception
    {
        string name;
        int id;
        public ExistException(int _id, string _name) : base()
        {
            id = _id;
            name = _name;
        }
        public ExistException(int _id, string _name, string massage) : base(massage)
        {
            id = _id;
            name = _name;
        }
        public ExistException(int _id, string _name, string massage, Exception innerExcption) : base(massage, innerExcption)
        {
            id = _id;
            name = _name;
        }
        public override string ToString() =>
            $"{name} number {id} Already exist";
    }
    public class EmptyInput : Exception
    {
        string field;
        public EmptyInput(string _field) : base()
        {
            field = _field;
        }
        public EmptyInput(string _field, string massage) : base(massage)
        {
            field = _field;
        }
        public EmptyInput(string _field, string massage, Exception innerExcption) : base(massage, innerExcption)
        {
            field = _field;
        }
        public override string ToString() => $"{field} can not be empty";

    }
}