using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QlikView.Common;
using Microsoft.Practices.Prism.Commands;
using System.Windows;

namespace QlikView.Configuration.WpfClient.ViewModels
{
    public class GroupViewModel:ViewModelBase
    {
        public IEnumerable<Recipient> RecipientList { get; set; }

        public DelegateCommand<Recipient> RecipientAddCommand { get; private set; }
        public DelegateCommand<Recipient> RecipientDeleteCommand { get; private set; }

        public GroupViewModel()
        {
            this.RecipientList = ReportConfig.RecipientManager.ItemCollection.Values.ToList();


            this.RecipientAddCommand = new DelegateCommand<Recipient>(this.RecipientAdd);
            this.RecipientDeleteCommand = new DelegateCommand<Recipient>(this.RecipientDelete);
        }

        protected override bool OnQvItemAdd(IReportItem qvItem)
        {
            try
            {
                RecipientGroup group = qvItem as RecipientGroup;

                if (string.IsNullOrWhiteSpace(group.Name) || group.RecipientList.Count == 0)
                {
                    MessageBox.Show("Name and recipients are required.");
                    return false;
                }


                ReportConfig.RecipientGroupManager.AddItem(qvItem);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

            return true;
        }

        public void RecipientAdd(Recipient recipient)
        {
            RecipientGroup group = this.ReportItem as RecipientGroup;

            if (group.RecipientList.ContainsKey(recipient.Name))
            {
                MessageBox.Show("The same name recipient exists, cannot add.");
                return;
            }

            group.RecipientList.Add(recipient.Name, recipient);
        }

        public void RecipientDelete(Recipient recipient)
        {
            if (recipient == null)
            {
                MessageBox.Show("Please select a recipient.");
                return;
            }

            if (MessageBox.Show("Do you want to delete the recipient " + recipient.Name, "Delete", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                RecipientGroup group = this.ReportItem as RecipientGroup;
                group.RecipientList.Remove(recipient.Name);
            }
        }
    }
}
