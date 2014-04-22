using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace QlikView.Common
{
    public class QvReportManager:ReportConfigManagerBase<QlikViewReport>
    {
        public override void Save()
        {
            foreach (var item in ReportConfig.ConfigLocations.Keys)
            {
                this._xmlDoc = new XmlDocument();
                this._xmlDoc.Load(ReportConfig.ConfigLocations[item]);

                XmlNode rootNode = this.GetItemRootNode();

                //Clear all the children nodes
                rootNode.RemoveAll();
                ReportItemDictionary<QlikViewReport> itemCollection = this.GetReportsByTask(item);
                this.RecreateItemXmlNodes(rootNode, itemCollection);

                this._xmlDoc.Save(ReportConfig.ConfigLocations[item]);
            }            
        }

        private ReportItemDictionary<QlikViewReport> GetReportsByTask(string task)
        {
            ReportItemDictionary<QlikViewReport> itemCollection = new ReportItemDictionary<QlikViewReport>();
            foreach (var item in this.ItemCollection)
            {
                if (item.Value.Parents.ContainsKey(task))
                    itemCollection.Add(item);
                else if (item.Value.Parents.Count == 0 && task == "ReportConfig")
                    itemCollection.Add(item);
            }

            return itemCollection;
        }
        
        public override IError RemoveItem(string name)
        {
            IError error = new QvError();

            foreach (var item in ReportConfig.ReportTaskManager.ItemCollection.Values)
            {
                if (item.Reports.ContainsKey(name))
                {
                    error.HasError = true;
                    error.ErrorMessage.AppendLine(string.Format("The task {0} connect to this report.", item.Name));
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

        protected override QlikViewReport CreateItemFromXmlNode(System.Xml.XmlNode xmlNode)
        {
            QlikViewReport report = new QlikViewReport();
            report.Name = xmlNode.GetAttributeValueIf("Name");
            report.Description = xmlNode.GetAttributeValueIf("Description");
            report.OutputFielName = xmlNode.GetNodeValueIf("OutputFileName");
            report.EnableDynamicNaming = bool.Parse(xmlNode.GetNodeValueIf("EnableDynamicNaming"));
            report.IsEmbeddedInMail = bool.Parse(xmlNode.GetNodeValueIf("EmbeddedInMail"));
            report.QlikViewExportObjectId = xmlNode.GetNodeValueIf("QlikViewExportObjectId");
            report.ReportType = (ReportType)int.Parse(xmlNode.GetNodeValueIf("ReportType"));

            string connection = xmlNode.GetNodeValueIf("Connection");
            if (ReportConfig.ConnectionManager.ItemCollection.ContainsKey(connection))
                report.Connection = ReportConfig.ConnectionManager.ItemCollection[connection];

            string filter = xmlNode.GetNodeValueIf("Filter");
            if (ReportConfig.FilterManager.ItemCollection.ContainsKey(filter))
            {
                report.Filter = ReportConfig.FilterManager.ItemCollection[filter];
            }

            return report;
        }

        protected override System.Xml.XmlNode GetItemRootNode()
        {
            return this._xmlDoc.SelectSingleNode("ReportConfig/Reports");
        }   

        protected override void RecreateItemXmlNodes(XmlNode itemRootNode, ReportItemDictionary<QlikViewReport> itemCollection)
        {
            foreach (var item in itemCollection.Values)
            {
                XmlNode node = this.CreateNodeBase("Report", item.Name, item.Description);
                node.AddChildNode("Connection", item.Connection.Name);
                node.AddChildNode("OutputFileName", item.OutputFielName);
                node.AddChildNode("EnableDynamicNaming", item.EnableDynamicNaming.ToString());
                node.AddChildNode("EmbeddedInMail", item.IsEmbeddedInMail.ToString());
                node.AddChildNode("QlikViewExportObjectId", item.QlikViewExportObjectId);
                node.AddChildNode("Filter", item.Filter == null ? string.Empty : item.Filter.Name);
                node.AddChildNode("ReportType", ((int)item.ReportType).ToString());

                itemRootNode.AppendChild(node);
            }
        }
    }
}
