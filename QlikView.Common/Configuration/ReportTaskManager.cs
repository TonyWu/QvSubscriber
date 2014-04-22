using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.IO;

namespace QlikView.Common
{
    public class ReportTaskManager:ReportConfigManagerBase<ReportTask>
    {
        public override void AddItem(IReportItem item)
        {
            if (this.ItemCollection.ContainsKey(item.Name))
                throw new Exception("Contains the same name Item.");
            ReportTask temp = (ReportTask)item;
            temp.Include = "Tasks/" + temp.Name + ".xml";
            this.ItemCollection.Add(temp.Name, temp);
            string folder = System.Reflection.Assembly.GetExecutingAssembly().CodeBase.Remove(System.Reflection.Assembly.GetExecutingAssembly().CodeBase.LastIndexOf("/"));
            var file = folder.Replace("file:///", "") + "/" + temp.Include;
            if (!File.Exists(file))
            {
                //var stream = File.Create(file);
                //stream.Close();

                string xml = @"<ReportConfig><Tasks></Tasks><Reports></Reports><Filters></Filters></ReportConfig>";
                var doc = new XmlDocument();
                doc.LoadXml(xml);
                doc.Save(file);
            }
                
            ReportConfig.ConfigLocations.Add(temp.Name, file);
            this.Save();
        }

        public override IError RemoveItem(string name)
        {
            IError error = new QvError();

            foreach (var item in ReportConfig.SchedulerManager.ItemCollection.Values)
            {
                if (item.Tasks.ContainsKey(name))
                {
                    error.HasError = true;
                    error.ErrorMessage.AppendLine(string.Format("The scheduler {0} connect to this task.", item.Name));
                    break;
                }
            }

            if (error.HasError == false)
            {
                this.ItemCollection.Remove(name);
                ReportConfig.ConfigLocations.Remove(name);
                this.Save();
            }

            return error;
        }

        public override void Save()
        {
            //Save task to main config          
            this._xmlDoc = new XmlDocument();
            this._xmlDoc.Load(ReportConfig.ConfigLocations["ReportConfig"]);
            XmlNode rootNode = this.GetItemRootNode();
            //Clear all the children nodes
            rootNode.RemoveAll();
            this.RecreateItemXmlNodesForMainConfig(rootNode);
            this._xmlDoc.Save(ReportConfig.ConfigLocations["ReportConfig"]);

            foreach (var item in ReportConfig.ConfigLocations.Keys)
            {
                if (item != "ReportConfig")
                {
                    this._xmlDoc = new XmlDocument();
                    this._xmlDoc.Load(ReportConfig.ConfigLocations[item]);
                    XmlNode rootNode1 = this.GetItemRootNode();
                    //Clear all the children nodes
                    rootNode1.RemoveAll();
                    var itemCollection = new ReportItemDictionary<ReportTask>();
                    itemCollection.Add(item,this.ItemCollection[item]);
                    this.RecreateItemXmlNodes(rootNode1, itemCollection);
                    this._xmlDoc.Save(ReportConfig.ConfigLocations[item]);
                }
            }
        }

        private void RecreateItemXmlNodesForMainConfig(XmlNode rootNode)
        {
            foreach (var item in this.ItemCollection.Values)
            {
                XmlNode node = this.CreateNodeBase("Task", item.Name, item.Description);
                node.Attributes.Append(this._xmlDoc.CreateAttribute("Include"));
                node.Attributes["Include"].Value = item.Include;
                rootNode.AppendChild(node);
            }
        }

        protected override void LoadItemCollection()
        {
            try
            {
                //get the include from main config
                Dictionary<string, string> taskInclude = new Dictionary<string, string>();
                XDocument xdoc = XDocument.Load(ReportConfig.ConfigLocations["ReportConfig"]);
                var query = from q in xdoc.Element("ReportConfig").Element("Tasks").Elements("Task")
                            select new
                            {
                                Task = q.Attribute("Name").Value,
                                Include = q.Attribute("Include").Value
                            };

                foreach (var item in query)
                {
                    taskInclude.Add(item.Task, item.Include);
                }

                //get the task detail info from task config file
                foreach (var config in ReportConfig.ConfigLocations.Keys)
                {
                    if (config != "ReportConfig")
                    {
                        this._xmlDoc = new XmlDocument();
                        this._xmlDoc.Load(ReportConfig.ConfigLocations[config]);
                        XmlNode rootNode = this.GetItemRootNode();

                        foreach (var item in rootNode.ChildNodes)
                        {
                            XmlNode node = item as XmlNode;
                            var Item = this.CreateItemFromXmlNode(node);
                            Item.Include = taskInclude[config];
                            this.ItemCollection.Add(Item.Name, Item);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            

        }

        protected override ReportTask CreateItemFromXmlNode(System.Xml.XmlNode xmlNode)
        {
            ReportTask task = new ReportTask();
            task.Name = xmlNode.GetAttributeValueIf("Name");
            task.Description = xmlNode.GetAttributeValueIf("Description");
            task.OutputFolder = xmlNode.GetNodeValueIf("OutputFolder");
            task.IsSendMailInSingleMail = bool.Parse(xmlNode.GetNodeValueIf("SendInSingleMail"));
            task.IsMergeInSingleExcel = bool.Parse(xmlNode.GetNodeValueIf("MergeInSingleExcel"));

            XmlNodeList reportNodeList = xmlNode.SelectNodes("Reports/Report");
            for (int i = 0; i < reportNodeList.Count; i++)
            {
                string report = reportNodeList[i].GetNodeValueIf();
                if (ReportConfig.QvReportManager.ItemCollection.ContainsKey(report))
                {
                    var qvReport = ReportConfig.QvReportManager.ItemCollection[report];
                    task.Reports.Add(report, qvReport);
                }
            }

            XmlNodeList recipientNodeList = xmlNode.SelectNodes("Recipients/Recipient");
            for (int i = 0; i < recipientNodeList.Count; i++)
            {
                string recipient = recipientNodeList[i].GetNodeValueIf();
                if (ReportConfig.RecipientManager.ItemCollection.ContainsKey(recipient))
                    task.Recipients.Add(recipient, ReportConfig.RecipientManager.ItemCollection[recipient]);
            }

            string recipientGroup = xmlNode.GetNodeValueIf("RecipientGroup");
            if (ReportConfig.RecipientGroupManager.ItemCollection.ContainsKey(recipientGroup))
                task.Group = ReportConfig.RecipientGroupManager.ItemCollection[recipientGroup];

            task.MessageDefinition = new Message();
            task.MessageDefinition.From = xmlNode.GetNodeValueIf("Message/From");
            task.MessageDefinition.CC = xmlNode.GetNodeValueIf("Message/CC");
            task.MessageDefinition.BCC = xmlNode.GetNodeValueIf("Message/BCC");
            task.MessageDefinition.Subject = xmlNode.GetNodeValueIf("Message/Subject");
            task.MessageDefinition.Body = xmlNode.GetNodeValueIf("Message/Body");

            return task;
        }

        protected override System.Xml.XmlNode GetItemRootNode()
        {
            return this._xmlDoc.SelectSingleNode("ReportConfig/Tasks");
        }

        protected override void RecreateItemXmlNodes(System.Xml.XmlNode itemRootNode, ReportItemDictionary<ReportTask> itemCollection)
        {
            foreach (var item in itemCollection.Values)
            {
                XmlNode node = this.CreateNodeBase("Task", item.Name, item.Description);
                node.AddChildNode("OutputFolder", item.OutputFolder);
                node.AddChildNode("SendInSingleMail", item.IsSendMailInSingleMail.ToString());
                node.AddChildNode("MergeInSingleExcel", item.IsMergeInSingleExcel.ToString());

                XmlNode reportsNode = node.AddChildNode("Reports", string.Empty);
                foreach (var report in item.Reports.Values)
                {
                    reportsNode.AddChildNode("Report", report.Name);
                }

                XmlNode recipientsNode = node.AddChildNode("Recipients", string.Empty);
                foreach (var recipient in item.Recipients.Values)
                {
                    recipientsNode.AddChildNode("Recipient", recipient.Name);
                }

                node.AddChildNode("RecipientGroup", item.Group == null ? string.Empty : item.Group.Name);

                XmlNode messageNode = node.AddChildNode("Message", string.Empty);
                messageNode.AddChildNode("From", item.MessageDefinition.From);
                messageNode.AddChildNode("CC", item.MessageDefinition.CC ?? string.Empty);
                messageNode.AddChildNode("BCC", item.MessageDefinition.BCC ?? string.Empty);
                messageNode.AddChildNode("Subject", item.MessageDefinition.Subject ?? string.Empty);

                XmlNode bodyNode = messageNode.AddChildNode("Body", string.Empty);
                XmlCDataSection dateSec = this._xmlDoc.CreateCDataSection(item.MessageDefinition.Body);
                bodyNode.AppendChild(dateSec);

                itemRootNode.AppendChild(node);
            }
        }


    }
}
