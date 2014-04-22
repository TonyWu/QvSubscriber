using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using QlikView.Common;
using QlikView.Connector;

namespace QlikView.ScheduleRunning
{
    public partial class HostForm : Form
    {
        public ILog Logger { get; set; }

        public HostForm()
        {
            InitializeComponent();
        }

        public void Run(string task)
        {
            ReportItemDictionary<ReportTask> tasks = ReportConfig.ReportTaskManager.ItemCollection;

            ExportEngine engine = new ExportEngine(new QlikViewConnector());
            engine.Logger = this.Logger;

            ReportTask taskItem = tasks.Values.FirstOrDefault(x => x.Name.ToString() == task);

            if (taskItem != null)
            {
                Console.WriteLine("Running task first time: " + taskItem.Name + ".......");
                engine.Logger.Message("Running task first time: " + taskItem.Name + ".......");
                IError error = engine.RunTask(taskItem, ReportConfig.SmtpServerManager.SmtpServer);

                if (error.HasError == false)
                {
                    Console.WriteLine("Running task fisrt time" + taskItem.Name + " complete.......");
                    engine.Logger.Message("Running task fisrt time" + taskItem.Name + " complete.......");
                }
                else
                {
                    Console.WriteLine("Running task fisrt time " + taskItem.Name + " failed.......");
                    engine.Logger.Error("Running task fisrt time " + taskItem.Name + " failed....... \n" + error.ErrorMessage.ToString());
                    //MailHelper.ExceptionNotify("Running task fisrt time " + taskItem.Name + " failed", error.ErrorMessage.ToString(), ReportConfig.Instance.SmtpServer);

                    Console.WriteLine("Running task second time: " + taskItem.Name + ".......");
                    engine.Logger.Message("Running task second time: " + taskItem.Name + ".......");                    

                    error = engine.RunTask(taskItem, ReportConfig.SmtpServerManager.SmtpServer);

                    if (error.HasError)
                    {
                        MailHelper.ExceptionNotify("Running task second time " + taskItem.Name + " failed", error.ErrorMessage.ToString(), ReportConfig.SmtpServerManager.SmtpServer);
                    }                   
                }
            }
            else
            {
                engine.Logger.Error(string.Format("There is no task {0}. ", task));
            }
           
            this.Close();
        }
    }
}
