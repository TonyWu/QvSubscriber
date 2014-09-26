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
using System.Windows.Shapes;
using QlikView.Common;

namespace QlikView.Configuration.WpfClient.Views
{
    /// <summary>
    /// Interaction logic for FtpSettings.xaml
    /// </summary>
    public partial class FtpSettings : Window
    {
        public FtpServer FtpServer { get; set; }

        public FtpSettings()
        {
            InitializeComponent();
        }

        private void txtOK_Click(object sender, RoutedEventArgs e)
        {
            if (this.FtpServer == null)
                this.FtpServer = new FtpServer();

            this.FtpServer.Host = this.txtHost.Text.Trim();
            this.FtpServer.Username = this.txtUsername.Text.Trim();
            this.FtpServer.Password = this.txtPassword.Text.Trim();
            this.FtpServer.Folder = this.txtFolder.Text.Trim();
            this.FtpServer.Port = this.txtPort.Text.Trim();

            this.DialogResult = true;
            this.Close();
        }

        private void txtCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (FtpServer != null)
            {
                this.checkBoxUpload.IsChecked = true;
                this.txtHost.Text = this.FtpServer.Host;
                this.txtUsername.Text = this.FtpServer.Username;
                this.txtPassword.Text = this.FtpServer.Password;
                this.txtFolder.Text = this.FtpServer.Folder;
                this.txtPort.Text = this.FtpServer.Port;
            }
            else
            {
                this.checkBoxUpload.IsChecked = false;
                this.txtHost.IsEnabled = false;
                this.txtUsername.IsEnabled = false;
                this.txtPassword.IsEnabled = false;
                this.txtFolder.IsEnabled = false;
                this.txtPort.IsEnabled = false;
            }
        }

        private void checkBoxUpload_Checked(object sender, RoutedEventArgs e)
        {
            if (this.checkBoxUpload.IsChecked == true)
            {
                this.txtHost.IsEnabled = true;
                this.txtUsername.IsEnabled = true;
                this.txtPassword.IsEnabled = true;
                this.txtFolder.IsEnabled = true;
                this.txtPort.IsEnabled = true;
            }
            else
            {
                this.txtHost.IsEnabled = false;
                this.txtUsername.IsEnabled = false;
                this.txtPassword.IsEnabled = false;
                this.txtFolder.IsEnabled = false;
                this.txtPort.IsEnabled = false;
            }
        }
    }
}
