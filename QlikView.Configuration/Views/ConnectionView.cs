using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using QlikView.Common;
using QlikView.Connector;

namespace QlikView.Configuration
{
    public partial class ConnectionView : Form, IBaseView<ReportConnection>
    {
        public ConnectionView()
        {
            InitializeComponent();
        }

        #region IBaseView<ReportConnection> Members

        public ReportConnection Item
        {
            get;
            set;
        }

        public void Open()
        {
            this.Show();
        }

        public event Action<ReportConnection> ItemAdded;

        public event Action<ReportConnection> ItemUpdated;

        #endregion

        private void ConnectionView_Load(object sender, EventArgs e)
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

                if (this.Item.IsLocal)
                {
                    this.txtServerName.Enabled = false;
                    this.txtServiceHost.Enabled = false;
                    this.txtServicePort.Enabled = false;
                }
                else
                {
                    this.txtServerName.Enabled = true;
                    this.txtServiceHost.Enabled = true;
                    this.txtServicePort.Enabled = true;
                }

                this.txtName.Text = this.Item.Name;
                this.txtDesc.Text = this.Item.Description;
                this.checkBoxIsLocal.Checked = this.Item.IsLocal;
                this.txtServerName.Text = this.Item.ServerName;
                this.txtServiceHost.Text = this.Item.ServiceHost;
                this.txtServicePort.Text = this.Item.ServicePort;
                this.txtQlikDoc.Text = this.Item.QlikViewDocument;
                this.txtUser.Text = this.Item.User;
                this.txtPassword.Text = this.Item.Password;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(this.txtName.Text) || string.IsNullOrWhiteSpace(this.txtQlikDoc.Text))
                {
                    MessageBox.Show("Name and doc can not be emtpty");
                    return;
                }

                this.Item.Name = this.txtName.Text;
                this.Item.Description = this.txtDesc.Text;
                this.Item.IsLocal = this.checkBoxIsLocal.Checked;
                this.Item.ServerName = this.txtServerName.Text;
                this.Item.ServiceHost = this.txtServiceHost.Text;
                this.Item.ServicePort = this.txtServicePort.Text;
                this.Item.QlikViewDocument = this.txtQlikDoc.Text;
                this.Item.User = this.txtUser.Text;
                this.Item.Password = this.txtPassword.Text;

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
            this.Item.IsLocal = this.checkBoxIsLocal.Checked;
            this.Item.ServerName = this.txtServerName.Text;
            this.Item.ServiceHost = this.txtServiceHost.Text;
            this.Item.ServicePort = this.txtServicePort.Text;
            this.Item.QlikViewDocument = this.txtQlikDoc.Text;
            this.Item.User = this.txtUser.Text;
            this.Item.Password = this.txtPassword.Text;

            if (this.ItemUpdated != null)
                this.ItemUpdated(this.Item);

            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSelectQvDoc_Click(object sender, EventArgs e)
        {
            try
            {
                ReportConnection c = this.NewConnection();

                if (c.IsLocal)
                {
                    OpenFileDialog dialog = new OpenFileDialog();
                    dialog.Filter = "qvw file (*.qvw)|*.qvw|All Files (*.*)|*.*";

                    if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        this.txtQlikDoc.Text = dialog.FileName;
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(c.ServerName) && string.IsNullOrEmpty(c.ServiceHost))
                    {
                        MessageBox.Show("Please set the server name or service host first.");
                        return;
                    }

                    ISingleFieldSelection selection = new DocumentSelectionForm();
                    selection.Connection = c;
                    selection.ItemSelected += new Action<string>(selection_ItemSelected);
                    selection.Open();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
        }

        void selection_ItemSelected(string obj)
        {
            this.txtQlikDoc.Text = obj;
        }

        private ReportConnection NewConnection()
        {
            ReportConnection Item = new ReportConnection();
            Item.Name = this.txtName.Text;
            Item.Description = this.txtDesc.Text;
            Item.IsLocal = this.checkBoxIsLocal.Checked;
            Item.ServerName = this.txtServerName.Text;
            Item.ServiceHost = this.txtServiceHost.Text;
            Item.ServicePort = this.txtServicePort.Text;
            Item.QlikViewDocument = this.txtQlikDoc.Text;
            Item.User = this.txtUser.Text;
            Item.Password = this.txtPassword.Text;

            return Item;
        }

        private void checkBoxIsLocal_CheckedChanged(object sender, EventArgs e)
        {
            this.txtServerName.Text = string.Empty;
            this.txtServiceHost.Text = string.Empty;
            this.txtServicePort.Text = string.Empty;
            this.txtQlikDoc.Text = string.Empty;

            if (this.checkBoxIsLocal.Checked)
            {
                this.txtServerName.Enabled = false;
                this.txtServiceHost.Enabled = false;
                this.txtServicePort.Enabled = false;
            }
            else
            {
                this.txtServerName.Enabled = true;
                this.txtServiceHost.Enabled = true;
                this.txtServicePort.Enabled = true;
            }
        }
    }
}
