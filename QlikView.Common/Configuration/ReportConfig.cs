using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace QlikView.Common
{
    public class ReportConfig
    {
        private static object locker = new object();
        private static Dictionary<string, string> _configLocations;
        public static Dictionary<string, string> ConfigLocations
        {
            get
            {
                if (_configLocations == null)
                {
                    lock (locker)
                    {
                        if (_configLocations == null)
                        {
                            string folder = System.Reflection.Assembly.GetExecutingAssembly().CodeBase.Remove(System.Reflection.Assembly.GetExecutingAssembly().CodeBase.LastIndexOf("/"));
                            folder = folder.Replace("file:///", "");
                            string configFile = folder + "/" + System.Configuration.ConfigurationManager.AppSettings["ReportConfig"];
                            //add main file
                            _configLocations = new Dictionary<string, string>();
                            _configLocations.Add("ReportConfig", configFile);

                            XDocument xdoc = XDocument.Load(configFile);
                            var query = from q in xdoc.Element("ReportConfig").Element("Tasks").Elements("Task")
                                        select new
                                        {
                                            Task = q.Attribute("Name").Value,
                                            Location = q.Attribute("Include").Value
                                        };

                            foreach (var item in query)
                            {
                                _configLocations.Add(item.Task, folder + "/" + item.Location);
                            }
                        }
                    }
                }

                return _configLocations;
            }
        }


        public static ReportConfigManagerBase<ReportConnection> ConnectionManager
        {
            get
            {
                return ReportConfigManagerFactory<ConnectionManager>.Create();
            }
        }

        public static ReportConfigManagerBase<Filter> FilterManager
        {
            get
            {
                return ReportConfigManagerFactory<FilterManager>.Create();
            }
        }

        public static ReportConfigManagerBase<QlikViewReport> QvReportManager
        {
            get
            {
                return ReportConfigManagerFactory<QvReportManager>.Create();
            }
        }

        public static ReportConfigManagerBase<RecipientGroup> RecipientGroupManager
        {
            get
            {
                return ReportConfigManagerFactory<RecipientGroupManager>.Create();
            }
        }

        public static ReportConfigManagerBase<Recipient> RecipientManager
        {
            get
            {
                return ReportConfigManagerFactory<RecipientManager>.Create();
            }
        }

        public static ReportConfigManagerBase<ReportTask> ReportTaskManager
        {
            get
            {
                return ReportConfigManagerFactory<ReportTaskManager>.Create();
            }
        }

        public static ReportConfigManagerBase<ReportSchedulerDefinition> SchedulerManager
        {
            get
            {
                return ReportConfigManagerFactory<SchedulerManager>.Create();
            }
        }

        private static SmtpServerManager smtpServerMgr;
        public static SmtpServerManager SmtpServerManager
        {
            get
            {
                if (smtpServerMgr == null)
                    smtpServerMgr = new SmtpServerManager();
                return smtpServerMgr;
            }
        }

        public static void Save()
        {
            ReportConfig.ReportTaskManager.Save();
            ReportConfig.QvReportManager.Save();
            ReportConfig.FilterManager.Save();
            ReportConfig.ConnectionManager.Save();
            ReportConfig.RecipientGroupManager.Save();
            ReportConfig.RecipientManager.Save();
            ReportConfig.SmtpServerManager.Save();
        }
    }
}
