using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QlikView.Common;
using System.Windows;

namespace QlikView.Configuration.WpfClient.ViewModels
{
    class ConnectionViewModel : ViewModelBase
    {
        protected override bool OnQvItemAdd(Common.IReportItem qvItem)
        {
            try
            {
                ReportConnection connection = qvItem as ReportConnection;

                if (string.IsNullOrWhiteSpace(connection.Name) || connection.QlikViewDocument == null)
                {
                    MessageBox.Show("Name and QlikViewDocument are required.");
                    return false;
                }

                if (connection.IsServer && string.IsNullOrWhiteSpace(connection.ServerName))
                {
                    MessageBox.Show("Server Name is required.");
                    return false;
                }

                ReportConfig.ConnectionManager.AddItem(qvItem);
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
