using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace QlikView.Common
{
    public class RecipientManager:ReportConfigManagerBase<Recipient>
    {
        protected override Recipient CreateItemFromXmlNode(System.Xml.XmlNode xmlNode)
        {
            Recipient recipient = new Recipient();
            recipient.Name = xmlNode.GetAttributeValueIf("Name");
            recipient.Description = xmlNode.GetAttributeValueIf("Description");
            recipient.Email = xmlNode.GetNodeValueIf("Email");
            recipient.EmailCC = xmlNode.GetNodeValueIf("EmailCC");

            return recipient;
        }

        protected override System.Xml.XmlNode GetItemRootNode()
        {
            return this._xmlDoc.SelectSingleNode("ReportConfig/Recipients");
        }

        public override IError RemoveItem(string name)
        {
            IError error = new QvError();

            foreach (var item in ReportConfig.ReportTaskManager.ItemCollection.Values)
            {
                if (item.Recipients.ContainsKey(name))
                {
                    error.HasError = true;
                    error.ErrorMessage.AppendLine(string.Format("The task {0} connect to this recipient.", item.Name));
                    break;
                }
            }

            foreach (var item in ReportConfig.RecipientGroupManager.ItemCollection.Values)
            {
                if (item.RecipientList.ContainsKey(name))
                {
                    error.HasError = true;
                    error.ErrorMessage.AppendLine(string.Format("The recipient group {0} connect to this recipient.", item.Name));
                    break;
                }
            }

            if (error.HasError == false)
            {
                this.ItemCollection.Remove(name);
                this.Save();
            }

            return error;
        }

        protected override void RecreateItemXmlNodes(XmlNode itemRootNode, ReportItemDictionary<Recipient> itemCollection)
        {
            foreach (var item in itemCollection.Values)
            {
                XmlNode node = this.CreateNodeBase("Recipient", item.Name, item.Description);
                node.AddChildNode("Email", item.Email);
                node.AddChildNode("EmailCC", item.EmailCC ?? string.Empty);

                itemRootNode.AppendChild(node);
            }
        }
    }
}
