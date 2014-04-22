using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace SalesforceCodeGenerator
{
    class MetadataRefector
    {
        public static List<PropertyInfo> LoadProperties(Type type)
        {
            List<PropertyInfo> list = new List<PropertyInfo>();
            foreach (var item in type.GetProperties())
            {
                if (item.PropertyType.IsValueType || item.PropertyType == typeof(string)
                    || item.PropertyType == typeof(DateTime))
                    list.Add(item);
            }

            return list;
        }
    }
}
