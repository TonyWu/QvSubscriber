using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using QlikView.Common;
using Microsoft.Practices.Prism.Commands;
using QlikView.Configuration.WpfClient.ViewModels;
using System.Windows;
using QlikView.Connector;
using System.IO;

namespace QlikView.Configuration.WpfClient
{
    public class MainViewModel : ViewModelBase
    {
        public ObservableCollection<IReportItem> Tasks
        {
            get;
            set;
        }

        public ObservableCollection<IReportItem> Reports
        {
            get;
            set;
        }

        public ObservableCollection<IReportItem> Filters
        {
            get;
            set;
        }

        public ObservableCollection<IReportItem> Recipients
        {
            get;
            set;
        }

        public ObservableCollection<IReportItem> Groups
        {
            get;
            set;
        }

        public ObservableCollection<IReportItem> Connections
        {
            get;
            set;
        }

        private bool _isDeleteEnabled;
        public bool IsDeleteEnabled 
        {
            get
            {
                return this._isDeleteEnabled;
            }
            set
            {
                this._isDeleteEnabled = value;
                this.OnPropertyChanged(() => this.IsDeleteEnabled);
            }
        }

        private bool _isRunEnabled;
        public bool IsRunEnabled
        {
            get
            {
                return this._isRunEnabled;
            }
            set
            {
                this._isRunEnabled = value;
                this.OnPropertyChanged(() => this.IsRunEnabled);
            }
        }

        public string CurrentReportItemType { get; set; }

        public DelegateCommand<IReportItem> ReportItemDeleteCommand { get; private set; }
        public DelegateCommand<IReportItem> RunTaskCommand { get; private set; }

        public event EventHandler ReportItemDeleted;
        public event EventHandler TaskRunning;
        public event Action<IError> TaskRunCompleted;

        public void Initialize()
        {
            this.Tasks = new ObservableCollection<IReportItem>();
            foreach (var item in ReportConfig.ReportTaskManager.ItemCollection.Values)
            {
                this.Tasks.Add(item);
            }

            this.Reports = new ObservableCollection<IReportItem>();
            foreach (var item in ReportConfig.QvReportManager.ItemCollection.Values)
            {
                this.Reports.Add(item);
            }

            this.Filters = new ObservableCollection<IReportItem>();
            foreach (var item in ReportConfig.FilterManager.ItemCollection.Values)
            {
                this.Filters.Add(item);
            }

            this.Groups = new ObservableCollection<IReportItem>();
            foreach (var item in ReportConfig.RecipientGroupManager.ItemCollection.Values)
            {
                this.Groups.Add(item);
            }

            this.Recipients = new ObservableCollection<IReportItem>();
            foreach (var item in ReportConfig.RecipientManager.ItemCollection.Values)
            {
                this.Recipients.Add(item);
            }

            this.Connections = new ObservableCollection<IReportItem>();
            foreach (var item in ReportConfig.ConnectionManager.ItemCollection.Values)
            {
                this.Connections.Add(item);
            }

            this.ReportItemDeleteCommand = new DelegateCommand<IReportItem>(this.ReportItemDelete);
            this.RunTaskCommand = new DelegateCommand<IReportItem>(this.RunTask);
        }

        protected override bool OnQvItemAdd(IReportItem qvItem)
        {
            //Do nothing
            return true;
        }

        protected void ReportItemDelete(IReportItem reportItem)
        {
            if (MessageBox.Show("Do you want to delete the item " + reportItem.Name, "Delete", MessageBoxButton.YesNo) == MessageBoxResult.No)
                return;

            IError error = null;
            switch (this.CurrentReportItemType)
            {
                case "Task":
                    {
                        error = ReportConfig.ReportTaskManager.RemoveItem(reportItem.Name);

                        if (error.HasError == false)
                            this.Tasks.Remove(reportItem);
                        break;
                    }
                case "Report":
                    {
                        error = ReportConfig.QvReportManager.RemoveItem(reportItem.Name);
                        if (error.HasError == false)
                        this.Reports.Remove(reportItem);
                        break;
                    }
                case "Filter":
                    {
                        error = ReportConfig.FilterManager.RemoveItem(reportItem.Name);
                        if (error.HasError == false)
                        this.Filters.Remove(reportItem);
                        break;
                    }
                case "Group":
                    {
                        error = ReportConfig.RecipientGroupManager.RemoveItem(reportItem.Name);
                        if (error.HasError == false)
                        this.Groups.Remove(reportItem);
                        break;
                    }
                case "Recipient":
                    {
                        error = ReportConfig.RecipientManager.RemoveItem(reportItem.Name);
                        if (error.HasError == false)
                        this.Recipients.Remove(reportItem);
                        break;
                    }
                case "Connection":
                    {
                        error = ReportConfig.ConnectionManager.RemoveItem(reportItem.Name);
                        if (error.HasError == false)
                            this.Connections.Remove(reportItem);
                        break;
                    }
                default:
                    break;
            }

            if (error != null)
            {
                if (error.HasError)
                    MessageBox.Show(error.ErrorMessage.ToString());
                else
                {
                    if (this.ReportItemDeleted != null)
                        this.ReportItemDeleted(this, new EventArgs());
                }
            }
        }

        protected void RunTask(IReportItem reportItem)
        {
            if (this.TaskRunning != null)
            {
                this.TaskRunning(this, new EventArgs());
            }

            IError error = null;
            ReportTask task = null;

            try
            {
                task = reportItem as ReportTask;
                ExportEngine engine = new ExportEngine(QlikViewConnectorProxy.Instance);
                engine.Logger = new QVConfigLog();
                error = engine.RunTask(task, ReportConfig.SmtpServerManager.SmtpServer);
            }
            catch (Exception ex)
            {
                if (error == null)
                    error = new QvError();
                error.ErrorMessage.Append(ex.StackTrace);
                error.HasError = true;
            }

            this.DeleteAllExportedFiles(task);
            if (this.TaskRunCompleted != null)
                this.TaskRunCompleted(error);
        }

        private void DeleteAllExportedFiles(ReportTask task)
        {
            string folder = string.Empty;
            if (task.OutputFolder.EndsWith(@"\"))
            {
                folder = task.OutputFolder.Remove(task.OutputFolder.Length - 1);
            }
            else
            {
                folder = task.OutputFolder;
            }

            if (File.Exists(folder + @"\DeleteCmd.bat"))
            {
                System.Diagnostics.Process.Start(folder + @"\DeleteCmd.bat");
            }
        }
    }
}
