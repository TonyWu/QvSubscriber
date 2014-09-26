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
    /// Interaction logic for TaskView.xaml
    /// </summary>
    public partial class TaskView : UserControl,IView
    {
        public TaskView()
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

        private void comboBoxGroup_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.comboBoxGroup.SelectedValue != null)
            {
                RecipientGroup group = this.comboBoxGroup.SelectedValue as RecipientGroup;

                if (group.Name != "NotSet")
                {
                    (this.ViewModel.ReportItem as ReportTask).Group = group;
                }
                else
                {
                    (this.ViewModel.ReportItem as ReportTask).Group = null;
                }
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ReportTask task = this.ViewModel.ReportItem as ReportTask;

            if (task.Group == null)
            {
                this.comboBoxGroup.SelectedIndex = 0;
            }
            else
            {
                this.comboBoxGroup.SelectedValue = task.Group;
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

        private void btnFtpSetting_Click(object sender, RoutedEventArgs e)
        {
            ReportTask task = this.ViewModel.ReportItem as ReportTask;
            FtpSettings settingForm = new FtpSettings();
            settingForm.FtpServer = task.FtpServer;

            if (settingForm.ShowDialog() == true)
            {
                task.FtpServer = settingForm.FtpServer;
            }
        }
    }
}
