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
    public partial class ExcelReportView : Form,IBaseView<QlikViewReport>
    {
        public ExcelReportView()
        {
            InitializeComponent();
        }

        #region IBaseView<ExcelReport> Members

        public QlikViewReport Item
        {
            get;
            set;
        }

        public void Open()
        {
            this.Show();
        }

        public event Action<QlikViewReport> ItemAdded;

        public event Action<QlikViewReport> ItemUpdated;

        #endregion

        private void ExcelReportView_Load(object sender, EventArgs e)
        {
            this.comboBoxConnection.DataSource = ReportConfig.ConnectionManager.ItemCollection.Select(x => x.Key).ToList();
            

            if (string.IsNullOrEmpty(this.Item.Name))
            {
                this.btnAdd.Visible = true;
                this.btnUpdate.Visible = false;

                if (ReportConfig.ConnectionManager.ItemCollection.Count == 0)
                    this.btnAdd.Enabled = false;
            }
            else
            {
                this.txtName.Enabled = false;
                this.btnAdd.Visible = false;
                this.btnUpdate.Visible = true;

                this.txtName.Text = this.Item.Name;
                this.txtDesc.Text = this.Item.Description;
                this.checkBoxEnableDynamicNaming.Checked = this.Item.EnableDynamicNaming;
                this.checkBoxEmbedded.Checked = this.Item.IsEmbeddedInMail;
                this.comboBoxConnection.SelectedItem = this.Item.Connection.Name;
                this.comboBoxFilters.SelectedItem = this.Item.Filter == null ? "No Filter" : this.Item.Filter.Name;
                this.comboBoxReportType.SelectedItem = this.Item.ReportType.ToString();

                this.txtObjectId.Text = this.Item.QlikViewExportObjectId;
                this.txtOutputFileName.Text = this.Item.OutputFielName;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(this.txtName.Text) || string.IsNullOrWhiteSpace(this.txtOutputFileName.Text)
                    || string.IsNullOrWhiteSpace(this.txtObjectId.Text))
                {
                    MessageBox.Show("Name, Output File Name or Object Id can not be emtpty");
                    return;
                }

                this.Item.Name = this.txtName.Text;
                this.Item.Description = this.txtDesc.Text;
                this.Item.EnableDynamicNaming = this.checkBoxEnableDynamicNaming.Checked;
                this.Item.IsEmbeddedInMail = this.checkBoxEmbedded.Checked;
                this.Item.Connection = ReportConfig.ConnectionManager.ItemCollection[this.comboBoxConnection.SelectedValue.ToString()];
                if (this.comboBoxFilters.SelectedValue.ToString() != "No Filter")
                    this.Item.Filter = ReportConfig.FilterManager.ItemCollection[this.comboBoxFilters.SelectedValue.ToString()];

                this.Item.QlikViewExportObjectId = this.txtObjectId.Text;
                this.Item.OutputFielName = this.txtOutputFileName.Text;

                if (this.comboBoxReportType.SelectedIndex == 0)
                {
                    this.Item.ReportType = ReportType.Excel;
                }
                else
                {
                    this.Item.ReportType = ReportType.JPG;
                }

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
            this.Item.EnableDynamicNaming = this.checkBoxEnableDynamicNaming.Checked;
            this.Item.IsEmbeddedInMail = this.checkBoxEmbedded.Checked;
            this.Item.Connection = ReportConfig.ConnectionManager.ItemCollection[this.comboBoxConnection.SelectedValue.ToString()];
            if (this.comboBoxFilters.SelectedValue.ToString() != "No Filter")
                this.Item.Filter = ReportConfig.FilterManager.ItemCollection[this.comboBoxFilters.SelectedValue.ToString()];
            else
                this.Item.Filter = null;

            this.Item.QlikViewExportObjectId = this.txtObjectId.Text;
            this.Item.OutputFielName = this.txtOutputFileName.Text;

            if (this.comboBoxReportType.SelectedIndex == 0)
            {
                this.Item.ReportType = ReportType.Excel;
            }
            else
            {
                this.Item.ReportType = ReportType.JPG;
            }

            if (this.ItemUpdated != null)
                this.ItemUpdated(this.Item);

            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddObjectId_Click(object sender, EventArgs e)
        {
            ISingleFieldSelection selection = new ObjectIdSelectionForm();
            selection.Connection = ReportConfig.ConnectionManager.ItemCollection[this.comboBoxConnection.SelectedValue.ToString()];

            selection.ItemSelected += new Action<string>(selection_ObjectIdSelected);

            selection.Open();
        }

        void selection_ObjectIdSelected(string obj)
        {
            this.txtObjectId.Text = obj;
        }

        private void comboBoxConnection_SelectedIndexChanged(object sender, EventArgs e)
        {
            var qeury = from q in ReportConfig.FilterManager.ItemCollection
                        where q.Value.Connection.Name == this.comboBoxConnection.SelectedItem.ToString()
                        select q.Key;
            List<string> filters = qeury.ToList();
            filters.Insert(0, "No Filter");
            this.comboBoxFilters.DataSource = filters;
        }
    }
}
