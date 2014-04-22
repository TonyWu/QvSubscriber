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
    public partial class RecipientView : Form, IBaseView<Recipient>
    {
        public RecipientView()
        {
            InitializeComponent();
        }

        #region IBaseView<Recipient> Members

        public Recipient Item
        {
            get;
            set;
        }

        public void Open()
        {
            this.Show();
        }

        public event Action<Recipient> ItemAdded;

        public event Action<Recipient> ItemUpdated;

        #endregion

        private void RecipientView_Load(object sender, EventArgs e)
        {
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
                this.txtEmail.Text = this.Item.Email;
                this.txtEmailCC.Text = this.Item.EmailCC;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(this.txtName.Text) || string.IsNullOrWhiteSpace(this.txtEmail.Text))
                {
                    MessageBox.Show("Name and email can not be emtpty");
                    return;
                }

                this.Item.Name = this.txtName.Text;
                this.Item.Description = this.txtDesc.Text;
                this.Item.Email = this.txtEmail.Text;
                this.Item.EmailCC = this.txtEmailCC.Text;

                if (this.ItemAdded != null)
                    this.ItemAdded(this.Item);

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            this.Item.Description = this.txtDesc.Text;
            this.Item.Email = this.txtEmail.Text;
            this.Item.EmailCC = this.txtEmailCC.Text;

            if (this.ItemUpdated != null)
                this.ItemUpdated(this.Item);

            this.Close();
        }
    }
}
