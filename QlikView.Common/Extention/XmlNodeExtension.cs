using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace QlikView.Common
{
    public static class XmlNodeExtension
    {
        public static string GetAttributeValueIf(this XmlNode xmlNode, string name)
        {
            return xmlNode.Attributes[name] == null ? string.Empty : xmlNode.Attributes[name].Value;
        }

        public static string GetNodeValueIf(this XmlNode xmlNode, string name = "")
        {
            if (string.IsNullOrEmpty(name))
                return xmlNode.InnerText??string.Empty;

            XmlNode node = xmlNode.SelectSingleNode(name);

            return node == null ? string.Empty : node.InnerText;
        }

        public static XmlNode AddChildNode(this XmlNode xmlNode, string name, string value)
        {
            XmlNode node = xmlNode.OwnerDocument.CreateElement(name);
            node.InnerText = value??string.Empty;
            xmlNode.AppendChild(node);

            return node;
        }
    }
}
