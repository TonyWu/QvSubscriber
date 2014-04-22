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
using Microsoft.Windows.Controls.Ribbon;
using QlikView.Configuration.WpfClient.Views;
using QlikView.Configuration.WpfClient.ViewModels;
using QlikView.Common;

namespace QlikView.Configuration.WpfClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : RibbonWindow
    {
        private MainViewModel _viewModel;
        private System.Windows.Controls.ListBox _currentListBox;

        public MainWindow()
        {
            InitializeComponent();

            // Insert code required on object creation below this point.
        }

        private void RibbonWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this._viewModel = new MainViewModel();
            this._viewModel.Initialize();
            this._viewModel.ReportItemDeleted += new EventHandler(_viewModel_ReportItemDeleted);
            this._viewModel.TaskRunning += new EventHandler(_viewModel_TaskRunning);
            this._viewModel.TaskRunCompleted += new Action<IError>(_viewModel_TaskRunCompleted);
            this.DataContext = this._viewModel;
        }

        void _viewModel_TaskRunCompleted(IError obj)
        {
            if (obj.HasError)
            {
                this.lblMessage.Content = "Task running failed.";
            }
            else
            {
                this.lblMessage.Content = "Task running succeed.";
            }
        }

        void _viewModel_TaskRunning(object sender, EventArgs e)
        {
            this.lblMessage.Content = "Task is running....";
        }

        void _viewModel_ReportItemDeleted(object sender, EventArgs e)
        {
            if (this.ViewContainer != null)
                this.ViewContainer.Content = null;
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            System.Windows.Controls.ListBox listBox = sender as System.Windows.Controls.ListBox;
            
            if (listBox.SelectedIndex < 0)
                return;

            string prefix = listBox.Tag.ToString();
            IView view = ViewFactory.CreateView(prefix);
            view.ViewModel = ViewModelFactory.CreateViewModel(prefix);
            this._viewModel.IsRunEnabled = false;
            this._viewModel.CurrentReportItemType = prefix;
            switch (prefix)
            {
                case "Task":
                    {
                        view.ViewModel.ReportItem = (sender as System.Windows.Controls.ListBox).SelectedItem as ReportTask;
                        this._viewModel.IsRunEnabled = true;
                        break;
                    }
                case "Report":
                    {
                        view.ViewModel.ReportItem = (sender as System.Windows.Controls.ListBox).SelectedItem as QlikViewReport;
                        break;
                    }
                case "Filter":
                    {
                        view.ViewModel.ReportItem = (sender as System.Windows.Controls.ListBox).SelectedItem as Filter;
                        break;
                    }
                case "Group":
                    {
                        view.ViewModel.ReportItem = (sender as System.Windows.Controls.ListBox).SelectedItem as RecipientGroup;
                        break;
                    }
                case "Recipient":
                    {
                        view.ViewModel.ReportItem = (sender as System.Windows.Controls.ListBox).SelectedItem as Recipient;
                        break;
                    }
                case "Connection":
                    {
                        view.ViewModel.ReportItem = (sender as System.Windows.Controls.ListBox).SelectedItem as ReportConnection;
                        break;
                    }
                default:
                    break;
            }
            view.ViewModel.Initialize();

            view.Bind();
            this.ViewContainer.Content = view;

            this._viewModel.ReportItem = view.ViewModel.ReportItem; 
            this._viewModel.IsDeleteEnabled = true;
        }

        private void bar_SelectedSectionChanged(object sender, RoutedPropertyChangedEventArgs<Odyssey.Controls.OutlookSection> e)
        {
            this._currentListBox = e.NewValue.Content as System.Windows.Controls.ListBox;
            if (this.ViewContainer != null)
                this.ViewContainer.Content = null;

            if (this._viewModel != null)
            {
                this._viewModel.ReportItem = null;
                this._viewModel.IsDeleteEnabled = false;
                this._viewModel.IsRunEnabled = false;
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            ReportConfig.Save();
            base.OnClosed(e);
        }

        private void New_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.ListBox listBox = this.bar.SelectedSection.Content as System.Windows.Controls.ListBox;
            listBox.SelectedIndex = -1;
            string prefix = listBox.Tag.ToString();

            IView view = ViewFactory.CreateView(prefix);
            view.ViewModel = ViewModelFactory.CreateViewModel(prefix);
            view.ViewModel.IsNew = true;
            view.ViewModel.QvItemAdded += new Action<IReportItem>(_viewModel_QvItemAdded);
            view.ViewModel.QvItemAddCanceled += new EventHandler(ViewModel_QvItemAddCanceled);

            this._viewModel.IsRunEnabled = false;
            this._viewModel.IsDeleteEnabled = false;
            this._viewModel.CurrentReportItemType = prefix;
            
            switch (prefix)
            {
                case "Task":
                    {
                        view.ViewModel.ReportItem = new ReportTask();
                        break;
                    }
                case "Report":
                    {
                        view.ViewModel.ReportItem = new QlikViewReport();
                        break;
                    }
                case "Filter":
                    {
                        view.ViewModel.ReportItem = new Filter();
                        break;
                    }
                case "Group":
                    {
                        view.ViewModel.ReportItem = new RecipientGroup();
                        break;
                    }
                case "Recipient":
                    {
                        view.ViewModel.ReportItem = new Recipient();
                        break;
                    }
                case "Connection":
                    {
                        view.ViewModel.ReportItem = new ReportConnection();
                        break;
                    }
                default:
                    break;
            }
            view.ViewModel.Initialize();
            view.Bind();

            this.ViewContainer.Content = null;
            this.ViewContainer.Content = view;
        }

        void ViewModel_QvItemAddCanceled(object sender, EventArgs e)
        {
            IView view = this.ViewContainer.Content as IView;
            view.ViewModel.QvItemAdded -= new Action<IReportItem>(_viewModel_QvItemAdded);
            this.ViewContainer.Content = null;
        }

        void _viewModel_QvItemAdded(IReportItem obj)
        {
            IView view = this.ViewContainer.Content as IView;
            view.ViewModel.QvItemAdded -= new Action<IReportItem>(_viewModel_QvItemAdded);
            this.ViewContainer.Content = null;

            System.Windows.Controls.ListBox listBox = this.bar.SelectedSection.Content as System.Windows.Controls.ListBox;
            string prefix = listBox.Tag.ToString();
            switch (prefix)
            {
                case "Task":
                    {
                        this._viewModel.Tasks.Add(obj);
                        break;
                    }
                case "Report":
                    {
                        this._viewModel.Reports.Add(obj);
                        break;
                    }
                case "Filter":
                    {
                        this._viewModel.Filters.Add(obj);
                        break;
                    }
                case "Group":
                    {
                        this._viewModel.Groups.Add(obj);
                        break;
                    }
                case "Recipient":
                    {
                        this._viewModel.Recipients.Add(obj);
                        break;
                    }
                case "Connection":
                    {
                        this._viewModel.Connections.Add(obj);
                        break;
                    }
                default:
                    break;
            }

            listBox.SelectedItem = obj;
        }

        private void MenuItemSmtpSettings_Click(object sender, RoutedEventArgs e)
        {
            SmtpWindow smtpWindow = new SmtpWindow();
            smtpWindow.StmpServer = ReportConfig.SmtpServerManager.SmtpServer;

            smtpWindow.Show();
        }

        private void MenuItemExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (this.txtSearch.IsFocused)
            {
                this._currentListBox.Items.Filter = (obj) =>
                {
                    IReportItem item = obj as IReportItem;
                    if (string.IsNullOrWhiteSpace(txtSearch.Text) || item.Name.ToUpper().Contains(txtSearch.Text.ToUpper()))
                        return true;
                    else
                        return false;
                };
            }
        }

        private void txtSearch_FocusableChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (!this.txtSearch.IsFocused && string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                this.txtSearch.Text = "Search...";
            }
        }

        private void txtSearch_GotFocus(object sender, RoutedEventArgs e)
        {
            if (this.txtSearch.Text == "Search...")
                this.txtSearch.Text = "";
        }

        private void txtSearch_LostFocus(object sender, RoutedEventArgs e)
        {
            if ( string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                this.txtSearch.Text = "Search...";
            }
        }
    }
}
