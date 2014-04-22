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
    /// Interaction logic for FilterView.xaml
    /// </summary>
    public partial class FilterView : UserControl,IView
    {
        private Filter filter;

        public FilterView()
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
            filter = this.ViewModel.ReportItem as Filter;

            this.comboBoxConnection.SelectedValue = filter.Connection;

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
                filter.Connection = this.comboBoxConnection.SelectedItem as ReportConnection;
            }
        }     
    }
}
