using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;

namespace QlikView.Configuration.WpfClient
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            e.Handled = true;

            MessageBox.Show(GetDeepException(e.Exception).Message);
        }

        private Exception GetDeepException(Exception ex)
        {
            if (ex.InnerException == null)
                return ex;
            else
                return GetDeepException(ex.InnerException);
        }
    }
}
