using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace QlikView.Common
{
    public class SmtpServerManager
    {
        private string ConfigFile;
        private XmlDocument _xmlDoc;

        public SmtpServer SmtpServer
        {
            get;
            private set;
        }

        public SmtpServerManager()
        {
            string folder = System.Reflection.Assembly.GetExecutingAssembly().CodeBase.Remove(System.Reflection.Assembly.GetExecutingAssembly().CodeBase.LastIndexOf("/"));
            folder = folder.Replace("file:///", "");
            ConfigFile = folder + "/" + System.Configuration.ConfigurationManager.AppSettings["ReportConfig"];

            this._xmlDoc = new XmlDocument();
            this._xmlDoc.Load(ConfigFile);

            XmlNode smtpServerRootNode = this._xmlDoc.SelectSingleNode("ReportConfig/SmtpServer");

            //Smtp Server
            this.SmtpServer = new SmtpServer();
            this.SmtpServer.Server = smtpServerRootNode.GetNodeValueIf("Server");
            this.SmtpServer.PickupDirectoryLocation = smtpServerRootNode.GetNodeValueIf("PickupDirectoryLocation");
            this.SmtpServer.User = smtpServerRootNode.GetNodeValueIf("User");
            this.SmtpServer.Password = EncryptionDecryption.Decode(smtpServerRootNode.GetNodeValueIf("Password"));
            this.SmtpServer.ExceptionFrom = smtpServerRootNode.GetNodeValueIf("ExceptionMailFrom");
            this.SmtpServer.ExceptionTo = smtpServerRootNode.GetNodeValueIf("ExceptionMailTo");
            this.SmtpServer.UsePickupDirectoryLocation = bool.Parse(smtpServerRootNode.GetNodeValueIf("UsePickupDirectoryLocation"));
            
        }

        public void Save()
        {
            this._xmlDoc = new XmlDocument();
            this._xmlDoc.Load(ConfigFile);
            XmlNode smtpServerRootNode = this._xmlDoc.SelectSingleNode("ReportConfig/SmtpServer");
            smtpServerRootNode.RemoveAll();
            smtpServerRootNode.AddChildNode("Server", this.SmtpServer.Server);
            smtpServerRootNode.AddChildNode("PickupDirectoryLocation", this.SmtpServer.PickupDirectoryLocation);
            smtpServerRootNode.AddChildNode("User", this.SmtpServer.User);
            smtpServerRootNode.AddChildNode("Password", EncryptionDecryption.Encode(this.SmtpServer.Password));
            smtpServerRootNode.AddChildNode("ExceptionMailFrom", this.SmtpServer.ExceptionFrom);
            smtpServerRootNode.AddChildNode("ExceptionMailTo", this.SmtpServer.ExceptionTo);
            smtpServerRootNode.AddChildNode("UsePickupDirectoryLocation", this.SmtpServer.UsePickupDirectoryLocation.ToString());

            this._xmlDoc.Save(ConfigFile);
        }
    }
}
