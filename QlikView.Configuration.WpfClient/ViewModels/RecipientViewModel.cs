using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QlikView.Common;
using System.Windows;

namespace QlikView.Configuration.WpfClient.ViewModels
{
    public class RecipientViewModel: ViewModelBase
    {
        protected override bool OnQvItemAdd(Common.IReportItem qvItem)
        {
            try
            {
                Recipient recipient = qvItem as Recipient;

                if (string.IsNullOrWhiteSpace(recipient.Name) || string.IsNullOrWhiteSpace(recipient.Email))
                {
                    MessageBox.Show("Name and Email are required.");
                    return false;
                }

                ReportConfig.RecipientManager.AddItem(qvItem);
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
