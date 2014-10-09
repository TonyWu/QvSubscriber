using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace QlikView.Common
{
    public abstract class ReportConfigManagerBase<T> where T : IReportItem
    {

        public ReportConfigManagerBase()
        {
            this.ItemCollection = new ReportItemDictionary<T>();
            this.LoadItemCollection();
        }

        public ReportItemDictionary<T> ItemCollection { get; private set; }
        //protected string ConfigFile;
        protected XmlDocument _xmlDoc;

        protected abstract T CreateItemFromXmlNode(XmlNode xmlNode);   
        protected abstract XmlNode GetItemRootNode();
        protected abstract void RecreateItemXmlNodes(XmlNode itemRootNode, ReportItemDictionary<T> itemCollection);

        public virtual void AddItem(IReportItem item)
        {
            if (this.ItemCollection.ContainsKey(item.Name))
                throw new Exception("Contains the same name Item.");
            T temp = (T)item;
            this.ItemCollection.Add(temp.Name, temp);
            this.Save();
        }

        public virtual IError RemoveItem(string name)
        {
            IError error = new QvError();
            this.ItemCollection.Remove(name);
            this.Save();
            return error;
        }

        public virtual void Save()
        {
            this._xmlDoc = new XmlDocument();
            this._xmlDoc.Load(ReportConfig.ConfigLocations["ReportConfig"]);

            XmlNode rootNode = this.GetItemRootNode();

            //Clear all the children nodes
            rootNode.RemoveAll();
            this.RecreateItemXmlNodes(rootNode,this.ItemCollection);

            this._xmlDoc.Save(ReportConfig.ConfigLocations["ReportConfig"]);
        }

        protected virtual void LoadItemCollection()
        {
            try
            {
                foreach (var config in ReportConfig.ConfigLocations.Keys)
                {
                    this._xmlDoc = new XmlDocument();
                    this._xmlDoc.Load(ReportConfig.ConfigLocations[config]);
                    XmlNode rootNode = this.GetItemRootNode();

                    if (rootNode != null && rootNode.HasChildNodes)
                    {
                        foreach (var item in rootNode.ChildNodes)
                        {
                            XmlNode node = item as XmlNode;
                            var Item = this.CreateItemFromXmlNode(node);

                            if (!this.ItemCollection.ContainsKey(Item.Name))
                                this.ItemCollection.Add(Item.Name, Item);
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected XmlNode CreateNodeBase(string name, string nameValue, string descriptionValue)
        {
            XmlNode node = this._xmlDoc.CreateElement(name);

            XmlAttribute nameAttribute = this._xmlDoc.CreateAttribute("Name");
            nameAttribute.Value = nameValue;
            node.Attributes.Append(nameAttribute);

            XmlAttribute descriptionAttribute = this._xmlDoc.CreateAttribute("Description");
            descriptionAttribute.Value = descriptionValue;
            node.Attributes.Append(descriptionAttribute);

            return node;
        }
    }
}
