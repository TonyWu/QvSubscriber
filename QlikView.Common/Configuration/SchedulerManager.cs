using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace QlikView.Common
{
    public class SchedulerManager:ReportConfigManagerBase<ReportSchedulerDefinition>
    {
        protected override ReportSchedulerDefinition CreateItemFromXmlNode(System.Xml.XmlNode xmlNode)
        {
            ReportSchedulerDefinition scheduler = new ReportSchedulerDefinition();

            scheduler.Name = xmlNode.GetAttributeValueIf("Name");
            scheduler.Description = xmlNode.GetAttributeValueIf("Description");
            //scheduler.ScheduleType = (ScheduleType)int.Parse(xmlNode.GetNodeValueIf("SchedulerType"));
            //scheduler.StartDate = DateTime.Parse(xmlNode.GetNodeValueIf("StartDate"));
            //scheduler.ShotTime = DateTime.Parse(xmlNode.GetNodeValueIf("ShotTime"));

            XmlNodeList taskNodeList = xmlNode.SelectNodes("Tasks/Task");
            for (int i = 0; i < taskNodeList.Count; i++)
            {
                string task = taskNodeList[i].GetNodeValueIf();
                if (ReportConfig.ReportTaskManager.ItemCollection.ContainsKey(task))
                    scheduler.Tasks.Add(task, ReportConfig.ReportTaskManager.ItemCollection[task]);
            }

            return scheduler;
        }

        protected override System.Xml.XmlNode GetItemRootNode()
        {
            return this._xmlDoc.SelectSingleNode("ReportConfig/Schedulers");
        }     

        protected override void RecreateItemXmlNodes(XmlNode itemRootNode, ReportItemDictionary<ReportSchedulerDefinition> itemCollection)
        {
            foreach (var item in itemCollection.Values)
            {
                XmlNode node = this.CreateNodeBase("Scheduler", item.Name, item.Description);
                XmlNode tasksNode = node.AddChildNode("Tasks", string.Empty);
                foreach (var task in item.Tasks.Values)
                {
                    tasksNode.AddChildNode("Task", task.Name);
                }

                itemRootNode.AppendChild(node);
            }
        }
    }
}
