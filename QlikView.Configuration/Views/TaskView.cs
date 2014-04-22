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
    public partial class TaskView : Form,IBaseView<ReportTask>
    {
        ReportItemDictionary<QlikViewReport> _reports = new ReportItemDictionary<QlikViewReport>();
        ReportItemDictionary<Recipient> _recipients = new ReportItemDictionary<Recipient>();
        public TaskView()
        {
            InitializeComponent();
        }

        #region IBaseView<ReportTask> Members

        public ReportTask Item
        {
            get;
            set;
        }

        public void Open()
        {
            this.Show();
        }

        public event Action<ReportTask> ItemAdded;

        public event Action<ReportTask> ItemUpdated;

        #endregion

        private void TaskView_Load(object sender, EventArgs e)
        {
            this.comboBoxReports.DataSource = ReportConfig.QvReportManager.ItemCollection.Select(x => x.Key).ToList();
            this.comboBoxRecipients.DataSource = ReportConfig.RecipientManager.ItemCollection.Select(x => x.Key).ToList();
            
            //Recipient groups
            List<string> groups = ReportConfig.RecipientGroupManager.ItemCollection.Select(x => x.Key).ToList();
            groups.Insert(0,"No Group");
            this.comboBoxGroups.DataSource = groups;

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
                this.txtOutputFolder.Text = this.Item.OutputFolder;
                this.checkBoxSendMailInSingle.Checked = this.Item.IsSendMailInSingleMail;
                this.checkBoxMergeInSingleExcel.Checked = this.Item.IsMergeInSingleExcel;

                foreach (var item in this.Item.Reports.Values)
                {
                    this._reports.Add(item.Name, item);
                }

                foreach (var item in this.Item.Recipients.Values)
                {
                    this._recipients.Add(item.Name, item);
                }

                this.RefreshGridView();

                this.comboBoxGroups.SelectedItem = this.Item.Group == null ? "No Group" : this.Item.Group.Name;

                this.txtMessageFrom.Text = this.Item.MessageDefinition.From;
                this.txtMessageCC.Text = this.Item.MessageDefinition.CC;
                this.txtMessageSubject.Text = this.Item.MessageDefinition.Subject;
                this.richTextBoxBody.Text = this.Item.MessageDefinition.Body;
            }
        }

        private void RefreshGridView()
        {
            this.dataGridViewReports.DataSource = this._reports.Values.ToList();
            this.dataGridViewRecipient.DataSource = this._recipients.Values.ToList();
        }

        private void btnAddReport_Click(object sender, EventArgs e)
        {
            if (this.comboBoxReports.Items.Count > 0)
            {
                QlikViewReport report = ReportConfig.QvReportManager.ItemCollection[this.comboBoxReports.SelectedItem.ToString()];

                if (!this._reports.ContainsKey(report.Name))
                    this._reports.Add(report.Name, report);

                this.RefreshGridView();
            }
        }

        private void btnDeleteReport_Click(object sender, EventArgs e)
        {
            if (this.dataGridViewReports.CurrentRow != null)
            {
                QlikViewReport report = this.dataGridViewReports.CurrentRow.DataBoundItem as QlikViewReport;
                this._reports.Remove(report.Name);
                this.RefreshGridView();
            }
        }

        private void btnAddRecipient_Click(object sender, EventArgs e)
        {
            if (this.comboBoxRecipients.Items.Count > 0)
            {
                Recipient recipient = ReportConfig.RecipientManager.ItemCollection[this.comboBoxRecipients.SelectedItem.ToString()];

                if (!this._recipients.ContainsKey(recipient.Name))
                    this._recipients.Add(recipient.Name, recipient);
                this.RefreshGridView();
            }
        }

        private void btnDeleteRecipient_Click(object sender, EventArgs e)
        {
            if (this.dataGridViewRecipient.CurrentRow != null)
            {
                Recipient recipient = this.dataGridViewRecipient.CurrentRow.DataBoundItem as Recipient;
                this._recipients.Remove(recipient.Name);
                this.RefreshGridView();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(this.txtName.Text) || string.IsNullOrWhiteSpace(this.txtOutputFolder.Text))
                {
                    MessageBox.Show("Name, Output Folder Name or Object Id can not be emtpty");
                    return;
                }

                this.PopulateTask();

                if (this.comboBoxGroups.SelectedItem.ToString() != "No Group")
                    this.Item.Group = ReportConfig.RecipientGroupManager.ItemCollection[this.comboBoxGroups.SelectedItem.ToString()];

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

            this.PopulateTask();

            if (this.comboBoxGroups.SelectedItem.ToString() != "No Group")
                this.Item.Group = ReportConfig.RecipientGroupManager.ItemCollection[this.comboBoxGroups.SelectedItem.ToString()];
            else
                this.Item.Group = null;

            if (this.ItemUpdated != null)
                this.ItemUpdated(this.Item);

            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void PopulateTask()
        {
            this.Item.Name = this.txtName.Text;
            this.Item.Description = this.txtDesc.Text;
            this.Item.OutputFolder = this.txtOutputFolder.Text;
            this.Item.IsSendMailInSingleMail = this.checkBoxSendMailInSingle.Checked;
            this.Item.IsMergeInSingleExcel = this.checkBoxMergeInSingleExcel.Checked;

            this.Item.Reports = this._reports;
            this.Item.Recipients = this._recipients;
            this.Item.MessageDefinition = new Common.Message();
            this.Item.MessageDefinition.From = this.txtMessageFrom.Text;
            this.Item.MessageDefinition.CC = this.txtMessageCC.Text;
            this.Item.MessageDefinition.Subject = this.txtMessageSubject.Text;
            this.Item.MessageDefinition.Body = this.richTextBoxBody.Text;
        }
    }
}
