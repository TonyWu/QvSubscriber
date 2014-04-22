using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QlikView.Common;
using Microsoft.Practices.Prism.Commands;
using System.Windows;
using System.Collections.ObjectModel;

namespace QlikView.Configuration.WpfClient.ViewModels
{
    public class TaskViewModel:ViewModelBase
    {
        public ObservableCollection<QlikViewReport> ReportList { get; set; }
        public IEnumerable<RecipientGroup> GroupList { get; set; }
        public IEnumerable<Recipient> RecipientList { get; set; }

        public TaskViewModel()
        {
            
        }

        public override void Initialize()
        {
            //var query = from q in ReportConfig.QvReportManager.ItemCollection.Values
            //            where q.Parents.Count == 0 || q.Parents.ContainsKey(this.ReportItem.Name)
            //            select q;
            this.ReportList = new ObservableCollection<QlikViewReport>();
            foreach (var item in ReportConfig.QvReportManager.ItemCollection.Values.ToList())
            {
                this.ReportList.Add(item);
            }
            

            var grouplist = ReportConfig.RecipientGroupManager.ItemCollection.Values.ToList();
            grouplist.Insert(0, new RecipientGroup()
            {
                Name = "NotSet"
            });

            this.GroupList = grouplist;

            this.RecipientList = ReportConfig.RecipientManager.ItemCollection.Values.ToList();

            this.ReportAddCommand = new DelegateCommand<QlikViewReport>(this.ReportAdd);
            this.ReportDeleteCommand = new DelegateCommand<QlikViewReport>(this.ReportDelete);

            this.RecipientAddCommand = new DelegateCommand<Recipient>(this.RecipientAdd);
            this.RecipientDeleteCommand = new DelegateCommand<Recipient>(this.RecipientDelete);
        }

        public DelegateCommand<QlikViewReport> ReportAddCommand { get; private set; }
        public DelegateCommand<QlikViewReport> ReportDeleteCommand { get; private set; }

        public DelegateCommand<Recipient> RecipientAddCommand { get; private set; }
        public DelegateCommand<Recipient> RecipientDeleteCommand { get; private set; }

        protected override bool OnQvItemAdd(IReportItem qvItem)
        {
            try
            {
                ReportTask task = qvItem as ReportTask;

                if (string.IsNullOrWhiteSpace(task.Name) || string.IsNullOrWhiteSpace(task.OutputFolder))
                {
                    MessageBox.Show("The name and output folder are required.");
                    return false;
                }

                ReportConfig.ReportTaskManager.AddItem(qvItem);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

            return true;
        }

        public void ReportAdd(QlikViewReport report)
        {
            ReportTask task = this.ReportItem as ReportTask;

            if (task.Reports.ContainsKey(report.Name))
            {
                MessageBox.Show("The same name report exists, cannot add.");
                return;
            }
            task.Reports.Add(report.Name, report);            
        }

        public void ReportDelete(QlikViewReport report)
        {
            if (report == null)
            {
                MessageBox.Show("Please select an report.");
                return;
            }
            if (MessageBox.Show("Do you want to delete the report " + report.Name, "Delete", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                ReportTask task = this.ReportItem as ReportTask;
                task.Reports.Remove(report.Name);
                this.ReportList.Add(report);
            }
        }

        public void RecipientAdd(Recipient recipient)
        {
            ReportTask task = this.ReportItem as ReportTask;

            if (task.Recipients.ContainsKey(recipient.Name))
            {
                MessageBox.Show("The same name recipient exists, cannot add.");
                return;
            }

            task.Recipients.Add(recipient.Name, recipient);
        }

        public void RecipientDelete(Recipient recipient)
        {
            if (recipient == null)
            {
                MessageBox.Show("Please select a recipient.");
                return;
            }

            if (MessageBox.Show("Do you want to delete the recipient " + recipient.Name, "Delete", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                ReportTask task = this.ReportItem as ReportTask;
                task.Recipients.Remove(recipient.Name);
            }
        }
    }
}
