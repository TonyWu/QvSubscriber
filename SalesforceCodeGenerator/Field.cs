using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SalesforceCodeGenerator
{
    class TypeMapping
    {
        public static Dictionary<string,Type> Mappings { get; set; }
        public static Dictionary<Type, string> MappingsDBType { get; set; }
        static TypeMapping()
        {
            Mappings = new Dictionary<string, Type>();
            Mappings.Add("varchar", typeof(string));
            Mappings.Add("nvarchar", typeof(string));
            Mappings.Add("decimal", typeof(double));
            Mappings.Add("datetime", typeof(DateTime));
            Mappings.Add("bit", typeof(bool));
            Mappings.Add("integer", typeof(int));
            Mappings.Add("int", typeof(int));

            //MappingsDBType = new Dictionary<Type, string>();
            //MappingsDBType.Add(typeof(string), "varchar");
            //MappingsDBType.Add(typeof(string), "nvarchar");
            //MappingsDBType.Add(typeof(double), "decimal");
            //MappingsDBType.Add(typeof(DateTime), "datetime");
            //MappingsDBType.Add(typeof(bool), "bit");
            //MappingsDBType.Add(typeof(int), "integer");
            //MappingsDBType.Add(typeof(int), "int");
        }
    }
    
    class Field
    {
        public string FieldName { get; set; }
        public string DbType { get; set; }
        public string Length { get; set; }

        public Type Type {
            get
            {
                return TypeMapping.Mappings[this.DbType];
            }
        }
    }
}
