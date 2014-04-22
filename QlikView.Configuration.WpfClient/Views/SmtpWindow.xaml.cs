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
    /// Interaction logic for SmtpWindow.xaml
    /// </summary>
    public partial class SmtpWindow : Window
    {
        public SmtpServer StmpServer { get; set; }

        public SmtpWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = this.StmpServer;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnTestSendMail_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MailHelper.ExceptionNotify("Test Mail", "This is a test connection mail!", this.StmpServer);
                MessageBox.Show("Send test mail succeed!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "----" + ex.StackTrace);
            }
        }
    }
}
