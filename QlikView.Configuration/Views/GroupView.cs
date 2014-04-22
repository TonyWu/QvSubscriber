using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using QlikView.Common;

namespace QlikView.Configuration
{
    public partial class GroupView : Form,IBaseView<RecipientGroup>
    {
        private ReportItemDictionary<Recipient> _recipients = new ReportItemDictionary<Recipient>();
        public GroupView()
        {
            InitializeComponent();
        }

        #region IBaseView<RecipientGroup> Members

        public RecipientGroup Item
        {
            get;
            set;
        }

        public void Open()
        {
            this.Show();
        }

        public event Action<RecipientGroup> ItemAdded;

        public event Action<RecipientGroup> ItemUpdated;

        #endregion

        private void GroupView_Load(object sender, EventArgs e)
        {
            this.comboBoxRecipients.DataSource = ReportConfig.RecipientManager.ItemCollection.Select(x => x.Key).ToList();

            if (string.IsNullOrEmpty(this.Item.Name))
            {
                this.btnAdd.Visible = true;
                this.btnUpdate.Visible = false;
            }
            else
            {
                this.txtName.Enabled = false;
                this.btnAdd.Visible = false;
                this.btnUpdate.Visible = true;

                this.txtName.Text = this.Item.Name;
                this.txtDesc.Text = this.Item.Description;

                foreach (var item in this.Item.RecipientList.Values)
                {
                    this._recipients.Add(item.Name, item);
                }

                this.dataGridViewRecipient.DataSource = this._recipients.Values.ToList();
            }
        }

        private void btnAddRecipient_Click(object sender, EventArgs e)
        {
            if (this.comboBoxRecipients.SelectedItem != null)
            {
                Recipient r = ReportConfig.RecipientManager.ItemCollection[this.comboBoxRecipients.SelectedItem.ToString()];

                if (!this._recipients.ContainsKey(r.Name))
                {
                    this._recipients.Add(r.Name, r);

                    this.dataGridViewRecipient.DataSource = this._recipients.Values.ToList();
                }
            }
        }

        private void btnRemoveRecipient_Click(object sender, EventArgs e)
        {
            if (this.dataGridViewRecipient.CurrentRow != null)
            {
                Recipient r = this.dataGridViewRecipient.CurrentRow.DataBoundItem as Recipient;
                this._recipients.Remove(r.Name);
                this.dataGridViewRecipient.DataSource = this._recipients.Values.ToList();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(this.txtName.Text))
                {
                    MessageBox.Show("Name can not be emtpty");
                    return;
                }

                this.Item.Name = this.txtName.Text;
                this.Item.Description = this.txtDesc.Text;
                this.Item.RecipientList = this._recipients;


                if (this.ItemAdded != null)
                    this.ItemAdded(this.Item);

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            this.Item.Description = this.txtDesc.Text;
            this.Item.RecipientList = this._recipients;

            if (this.ItemUpdated != null)
                this.ItemUpdated(this.Item);

            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
