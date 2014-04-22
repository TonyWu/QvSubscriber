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

namespace QlikView.Configuration.WpfClient.Views
{
    /// <summary>
    /// Interaction logic for GroupView.xaml
    /// </summary>
    public partial class GroupView : UserControl,IView
    {
        public GroupView()
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
    }
}
