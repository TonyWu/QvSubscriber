using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace QlikView.Common
{
    public class ConnectionManager:ReportConfigManagerBase<ReportConnection>
    {

        protected override ReportConnection CreateItemFromXmlNode(System.Xml.XmlNode xmlNode)
        {
            ReportConnection connection = new ReportConnection();
            connection.Name = xmlNode.GetAttributeValueIf("Name");
            connection.Description = xmlNode.GetAttributeValueIf("Description");
            connection.IsLocal = bool.Parse(xmlNode.GetNodeValueIf("IsLocal"));
            connection.QlikViewDocument = xmlNode.GetNodeValueIf("QlikViewDocument");
            connection.ServerName = xmlNode.GetNodeValueIf("ServerName");
            connection.ServiceHost = xmlNode.GetNodeValueIf("WebServiceHost");
            connection.ServicePort = xmlNode.GetNodeValueIf("WebServicePort");
            connection.User = xmlNode.GetNodeValueIf("User");
            connection.Password = EncryptionDecryption.Decode(xmlNode.GetNodeValueIf("Password"));

            return connection;
        }

        protected override System.Xml.XmlNode GetItemRootNode()
        {
            return this._xmlDoc.SelectSingleNode("ReportConfig/Connections");
        }

        protected override void RecreateItemXmlNodes(System.Xml.XmlNode itemRootNode, ReportItemDictionary<ReportConnection> itemCollection)
        {
            foreach (var item in itemCollection.Values)
            {
                XmlNode node = this.CreateNodeBase("Connection", item.Name, item.Description);
                node.AddChildNode("IsLocal", item.IsLocal.ToString());
                node.AddChildNode("ServerName", item.ServerName);
                node.AddChildNode("WebServicePort", item.ServicePort);
                node.AddChildNode("WebServiceHost", item.ServiceHost);
                node.AddChildNode("QlikViewDocument", item.QlikViewDocument);
                node.AddChildNode("User", item.User);
                node.AddChildNode("Password", EncryptionDecryption.Encode(item.Password));

                itemRootNode.AppendChild(node);
            }
        }

        public override IError RemoveItem(string name)
        {
            IError error = new QvError();

            foreach (var item in ReportConfig.QvReportManager.ItemCollection.Values)
            {
                if (name == item.Connection.Name)
                {
                    error.HasError = true;
                    error.ErrorMessage.AppendLine(string.Format("The report {0} connect to this connection.", item.Name));
                    break;
                }
            }

            foreach (var item in ReportConfig.QvReportManager.ItemCollection.Values)
            {
                if (name == item.Connection.Name)
                {
                    error.HasError = true;
                    error.ErrorMessage.AppendLine(string.Format("The Filter {0} connect to this connection.", item.Name));
                }
            }

            if (error.HasError == false)
            {
                this.ItemCollection.Remove(name);
                this.Save();
            }

            return error;
        }
    }
}
