using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace QlikView.Common
{
    public class FilterManager:ReportConfigManagerBase<Filter>
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
                ReportItemDictionary<Filter> itemCollection = this.GetFiltersByTask(item);
                this.RecreateItemXmlNodes(rootNode, itemCollection);

                this._xmlDoc.Save(ReportConfig.ConfigLocations[item]);
            } 
        }

        private ReportItemDictionary<Filter> GetFiltersByTask(string task)
        {
            ReportItemDictionary<Filter> itemCollection = new ReportItemDictionary<Filter>();
            foreach (var item in this.ItemCollection)
            {
                if (item.Value.Parents.Count > 0 && item.Value.Parents.Where(x=>x.Value.Parents.ContainsKey(task)).Count() > 0)
                    itemCollection.Add(item);
                //if the filter has no parents or all the parents have no perants, save to the main config 
                else if ((item.Value.Parents.Count == 0 || (item.Value.Parents.Count > 0 && item.Value.Parents.Where(x => x.Value.Parents.Count > 0).Count() == 0))
                    && task == "ReportConfig")
                    itemCollection.Add(item);
            }

            return itemCollection;
        }

        protected override Filter CreateItemFromXmlNode(System.Xml.XmlNode xmlNode)
        {
            Filter filter = new Filter();
            filter.Name = xmlNode.GetAttributeValueIf("Name");
            filter.Description = xmlNode.GetAttributeValueIf("Description");
            string connection = xmlNode.GetNodeValueIf("Connection");
            if (ReportConfig.ConnectionManager.ItemCollection.ContainsKey(connection))
                filter.Connection = ReportConfig.ConnectionManager.ItemCollection[connection];

            XmlNodeList list = xmlNode.SelectNodes("Fields/Field");
            for (int i = 0; i < list.Count; i++)
            {
                QVField field = new QVField();
                field.Name = list[i].GetAttributeValueIf("Name");
                field.Expression = list[i].GetAttributeValueIf("Expression");
                XmlNodeList valueList = list[i].SelectNodes("Value");
                for (int j = 0; j < valueList.Count; j++)
                {
                    FieldValue value = new FieldValue();
                    value.Value = valueList[j].GetNodeValueIf();
                    value.Number = double.Parse(valueList[j].GetAttributeValueIf("Number"));
                    value.IsNumeric = bool.Parse(valueList[j].GetAttributeValueIf("IsNumeric"));
                    field.Values.Add(value);
                }

                filter.Fields.Add(field.Name, field);
            }

            list = xmlNode.SelectNodes("Variables/Variable");
            for (int i = 0; i < list.Count; i++)
            {
                QvVariable variable = new QvVariable();
                variable.Name = list[i].GetAttributeValueIf("Name");
                variable.Expression = list[i].GetAttributeValueIf("Expression");
                variable.Value = list[i].GetNodeValueIf();

                filter.Variables.Add(variable.Name, variable);
            }

            return filter;
        }

        protected override System.Xml.XmlNode GetItemRootNode()
        {
            return this._xmlDoc.SelectSingleNode("ReportConfig/Filters");
        }

        protected override void RecreateItemXmlNodes(System.Xml.XmlNode itemRootNode, ReportItemDictionary<Filter> itemCollection)
        {
            foreach (var item in itemCollection.Values)
            {
                XmlNode node = this.CreateNodeBase("Filter", item.Name, item.Description);

                node.AddChildNode("Connection", item.Connection.Name);
                node.AddChildNode("Fields", string.Empty);
                foreach (var fieldItem in item.Fields.Values)
                {
                    XmlNode fieldNode = this.CreateNodeBase("Field", fieldItem.Name, string.Empty);
                    fieldNode.Attributes.Append(_xmlDoc.CreateAttribute("Expression"));
                    fieldNode.Attributes["Expression"].Value = fieldItem.Expression ?? string.Empty;
                    foreach (var valueItem in fieldItem.Values)
                    {
                        XmlNode valueNode = fieldNode.AddChildNode("Value", valueItem.Value);
                        XmlAttribute numberAttr = valueNode.OwnerDocument.CreateAttribute("Number");
                        numberAttr.Value = valueItem.Number.ToString();
                        valueNode.Attributes.Append(numberAttr);
                        XmlAttribute isNumericAttr = valueNode.OwnerDocument.CreateAttribute("IsNumeric");
                        isNumericAttr.Value = valueItem.IsNumeric.ToString();
                        valueNode.Attributes.Append(isNumericAttr);
                    }

                    node.ChildNodes[1].AppendChild(fieldNode);
                }

                node.AddChildNode("Variables", string.Empty);
                foreach (var variableItem in item.Variables.Values)
                {
                    XmlNode variableNode = node.ChildNodes[2].AddChildNode("Variable", variableItem.Value);
                    variableNode.Attributes.Append(_xmlDoc.CreateAttribute("Name"));
                    variableNode.Attributes["Name"].Value = variableItem.Name;

                    variableNode.Attributes.Append(_xmlDoc.CreateAttribute("Expression"));
                    variableNode.Attributes["Expression"].Value = variableItem.Expression ?? string.Empty;
                }

                itemRootNode.AppendChild(node);
            }
        }

        public override IError RemoveItem(string name)
        {
            IError error = new QvError();

            foreach (var item in ReportConfig.QvReportManager.ItemCollection.Values)
            {
                if (item.Filter != null && item.Filter.Name == name)
                {
                    error.HasError = true;
                    error.ErrorMessage.AppendLine(string.Format("The Report {0} connect to this filter.", item.Name));
                    break;
                }
            }

            return error;
        }
    }
}
