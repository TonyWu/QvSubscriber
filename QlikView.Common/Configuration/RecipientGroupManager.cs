using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace QlikView.Common
{
    public class RecipientGroupManager:ReportConfigManagerBase<RecipientGroup>
    {
        protected override RecipientGroup CreateItemFromXmlNode(System.Xml.XmlNode xmlNode)
        {
            RecipientGroup group = new RecipientGroup();
            group.Name = xmlNode.GetAttributeValueIf("Name");
            group.Description = xmlNode.GetAttributeValueIf("Description");

            XmlNodeList recipientNodeList = xmlNode.SelectNodes("Recipient");
            for (int i = 0; i < recipientNodeList.Count; i++)
            {
                string recipient = recipientNodeList[i].GetNodeValueIf();
                if (ReportConfig.RecipientManager.ItemCollection.ContainsKey(recipient))
                    group.RecipientList.Add(recipient, ReportConfig.RecipientManager.ItemCollection[recipient]);
            }

            return group;
        }

        protected override System.Xml.XmlNode GetItemRootNode()
        {
            return this._xmlDoc.SelectSingleNode("ReportConfig/RecipientGroups");
        }     

        protected override void RecreateItemXmlNodes(XmlNode itemRootNode, ReportItemDictionary<RecipientGroup> itemCollection)
        {
            foreach (var item in itemCollection.Values)
            {
                XmlNode node = this.CreateNodeBase("RecipientGroup", item.Name, item.Description);
                foreach (var recipient in item.RecipientList.Values)
                {
                    node.AddChildNode("Recipient", recipient.Name);
                }

                itemRootNode.AppendChild(node);
            }
        }
    }
}
