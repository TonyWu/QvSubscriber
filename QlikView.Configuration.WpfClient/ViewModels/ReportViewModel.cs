using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QlikView.Common;
using System.Windows;
using System.Collections.ObjectModel;

namespace QlikView.Configuration.WpfClient.ViewModels
{
    public class ReportViewModel : ViewModelBase
    {
        public IEnumerable<ReportConnection> ConnectionList { get; set; }
        public IEnumerable<ReportType> ReportTypeList { get; set; }
        public ObservableCollection<Filter> FilterList { get; set; }

        public ReportViewModel()
        {
            this.ConnectionList = ReportConfig.ConnectionManager.ItemCollection.Values.ToList();
            this.ReportTypeList = new List<ReportType>()
            {
                ReportType.Excel,
                ReportType.JPG,
                ReportType.Html
            };
        }

        public override void Initialize()
        {
            //just list the filters that the task owns and there is no task owned
            //var query = from q in ReportConfig.FilterManager.ItemCollection.Values
            //            where q.Parents.Count == 0 || (q.Parents.ContainsKey(this.ReportItem.Name))
            //            select q;

            this.FilterList = new ObservableCollection<Filter>();
            this.FilterList.Add(new Filter()
            {
                Name = "NotSet"
            });
            foreach (var item in ReportConfig.FilterManager.ItemCollection.Values.ToList())
            {
                this.FilterList.Add(item);
            }
        }

        protected override bool OnQvItemAdd(IReportItem qvItem)
        {
            try
            {
                QlikViewReport report = qvItem as QlikViewReport;

                if (string.IsNullOrWhiteSpace(report.Name) || string.IsNullOrWhiteSpace(report.OutputFielName) || report.Connection == null)
                {
                    MessageBox.Show("Name, OutputFielName and connection are required.");
                    return false;
                }

                ReportConfig.QvReportManager.AddItem(qvItem);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

            return true;
        }
    }
}
