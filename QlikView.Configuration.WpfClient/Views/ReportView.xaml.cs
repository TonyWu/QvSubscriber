using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using QlikView.Common;

namespace QlikView.Configuration.WpfClient.Views
{
    /// <summary>
    /// Interaction logic for ReportView.xaml
    /// </summary>
    public partial class ReportView : UserControl,IView
    {
        private QlikViewReport report;
        
        public ReportView()
        {
            InitializeComponent();
        }

        public ViewModels.IViewModel ViewModel
        {
            get;
            set;
        }


        public void Bind()
        {
            this.DataContext = this.ViewModel;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            report = this.ViewModel.ReportItem as QlikViewReport;

            this.comboBoxConnection.SelectedValue = report.Connection;
            this.comboBoxReportType.SelectedValue = report.ReportType;

            if (report.Filter == null)
            {
                this.comboBoxFilter.SelectedIndex = 0;
            }
            else
            {
                this.comboBoxFilter.SelectedValue = report.Filter;
            }

            if (this.ViewModel.IsNew)
            {
                this.gridName.Visibility = System.Windows.Visibility.Visible;
                this.stackNewActions.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                this.gridName.Visibility = System.Windows.Visibility.Collapsed;
                this.stackNewActions.Visibility = System.Windows.Visibility.Collapsed;
            }
        }

        private void comboBoxConnection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.comboBoxConnection.SelectedValue != null)
            {
                report.Connection = this.comboBoxConnection.SelectedItem as ReportConnection;
            }
        }

        private void comboBoxReportType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.comboBoxReportType.SelectedValue != null)
            {
                report.ReportType = (ReportType)this.comboBoxReportType.SelectedItem;
            }
        }

        private void comboBoxFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.comboBoxFilter.SelectedValue != null)
            {
                if (this.comboBoxFilter.SelectedValue.ToString() == "NotSet")
                {
                    report.Filter = null;
                }
                else
                {
                    report.Filter = this.comboBoxFilter.SelectedItem as Filter;
                }
            }
        }
    }
}
