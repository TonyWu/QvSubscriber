using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;

namespace SalesforceCodeGenerator
{
    class TemplateMgr
    {
        public static Dictionary<string, Field> LoadFieldsByObject(string objectName)
        {
            string file = string.Format(@"Template\Objects\{0}.xml",objectName);

            if (File.Exists(file))
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(file);

                Dictionary<string, Field> fields = new Dictionary<string, Field>();

                XmlNodeList list = doc.SelectNodes(@"Columns/Field");
                foreach (var item in list)
                {
                    XmlNode node = item as XmlNode;
                    fields.Add(node.GetAttributeValueIf("Name"), new Field()
                    {
                        FieldName = node.GetAttributeValueIf("Name"),
                        DbType = node.GetNodeValueIf("DbType"),
                        Length = node.GetNodeValueIf("Length")
                    });
                }

                return fields;
            }
            else
            {
                return new Dictionary<string, Field>();
            }
        }

        public static void Save(Dictionary<string, Field> fields, string objectName)
        {
            string file = string.Format(@"Template\Objects\{0}.xml", objectName);

            string xml = "<?xml version=\"1.0\" encoding=\"utf-8\" ?><Columns></Columns>";
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);

            XmlNode root = doc.SelectSingleNode("Columns");
            foreach (var item in fields.Values)
            {
                XmlNode fieldNode = root.AddChildNode("Field", string.Empty);
                fieldNode.AddAttribute("Name", item.FieldName);
                fieldNode.AddChildNode("DbType", item.DbType);
                fieldNode.AddChildNode("Length", item.Length);
            }

            doc.Save(file);
        }
    }
}
