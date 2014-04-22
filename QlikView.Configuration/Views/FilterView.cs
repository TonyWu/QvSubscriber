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
    public partial class FilterView : Form,IBaseView<Filter>
    {
        public FilterView()
        {
            InitializeComponent();
        }

        #region IBaseView<Filter> Members

        public Filter Item
        {
            get;
            set;
        }

        public void Open()
        {
            this.Show();
        }

        public event Action<Filter> ItemAdded;

        public event Action<Filter> ItemUpdated;

        #endregion

        private ReportItemDictionary<QVField> FieldCollection = new ReportItemDictionary<QVField>();
        private ReportItemDictionary<QvVariable> VariableCollection = new ReportItemDictionary<QvVariable>();

        private ReportConnection CurrentConnection
        {
            get
            {
                if (this.comboBoxConnection.SelectedItem != null)
                    return ReportConfig.ConnectionManager.ItemCollection[this.comboBoxConnection.SelectedValue.ToString()];
                else
                    return null;
            }
        }

        private void FilterView_Load(object sender, EventArgs e)
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

                this.comboBoxConnection.SelectedItem = this.Item.Connection.Name;

                foreach (var item in this.Item.Fields)
                {
                    FieldCollection.Add(item.Key, item.Value);
                }

                foreach (var item in this.Item.Variables)
                {
                    VariableCollection.Add(item.Key, item.Value);
                }

                this.RefreshFieldDataGrid();
                this.RefreshVariableDataGrid();
            }
        }

        private void RefreshVariableDataGrid()
        {
            var query = from q in VariableCollection
                        select new
                        {
                            Name = q.Key,
                            Value = q.Value.Value,
                            Expression = q.Value.Expression
                        };

            this.dataGridViewVariables.DataSource = query.ToList();
        }

        private void RefreshFieldDataGrid()
        {
            var query = from q in FieldCollection
                        select new
                        {
                            Name = q.Key,
                            Values = q.ToString(),
                            Expression = q.Value.Expression
                        };

            this.dataGridViewFields.DataSource = query.ToList();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(this.txtName.Text))
                {
                    MessageBox.Show("Name and doc can not be emtpty");
                    return;
                }

                this.Item.Name = this.txtName.Text;
                this.Item.Description = this.txtDesc.Text;
                this.Item.Connection = this.CurrentConnection;
                this.Item.Fields = FieldCollection;
                this.Item.Variables = VariableCollection;

                if (this.ItemAdded != null)
                    this.ItemAdded(this.Item);

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnFieldAdd_Click(object sender, EventArgs e)
        {
            QlikViewConnectorProxy.Instance.SetConnection(this.CurrentConnection);
            QlikViewConnectorProxy.Instance.SetSelections(this.FieldCollection);

            IFieldSelection selection = new FieldSelectionForm();
            selection.Connection = this.CurrentConnection;

            selection.FieldAdded += new Action<QVField>(selection_FieldAdded);

            selection.Open();

        }

        void selection_FieldAdded(QVField obj)
        {
            if (FieldCollection.ContainsKey(obj.Name))
                FieldCollection.Remove(obj.Name);
            FieldCollection.Add(obj.Name, obj);
            this.RefreshFieldDataGrid();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            this.Item.Description = this.txtDesc.Text;
            this.Item.Connection = this.CurrentConnection;
            this.Item.Fields = FieldCollection;
            this.Item.Variables = VariableCollection;

            if (this.ItemUpdated != null)
                this.ItemUpdated(this.Item);

            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnFieldRemove_Click(object sender, EventArgs e)
        {
            if (this.dataGridViewFields.CurrentRow != null)
            {
                string field = this.dataGridViewFields.CurrentRow.Cells[1].Value.ToString();
                FieldCollection.Remove(field);
                this.RefreshFieldDataGrid();
            }
        }

        private void btnFilterPreview_Click(object sender, EventArgs e)
        {
            if (this.CurrentConnection == null)
            {
                MessageBox.Show("Please select the connection.");
                return;
            }

            FilterSelectionForm form = new FilterSelectionForm();
            form.Connection = this.CurrentConnection;
            form.Fields = this.FieldCollection;

            DialogResult result = form.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                this.FieldCollection = form.Fields;
                this.RefreshFieldDataGrid();
            }
        }

        private void btnVariableAdd_Click(object sender, EventArgs e)
        {
            VariableSettingForm form = new VariableSettingForm();

            DialogResult result = form.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                QvVariable variable = new QvVariable();
                variable.Name = form.VariableName;
                variable.Value = form.Value;
                variable.Expression = form.Expression;

                if (!VariableCollection.ContainsKey(variable.Name))
                {
                    VariableCollection.Add(variable.Name, variable);
                }

                this.RefreshVariableDataGrid();
            }
        }

        private void btnVariableRemove_Click(object sender, EventArgs e)
        {
            if (this.dataGridViewVariables.CurrentRow != null)
            {
                string v = this.dataGridViewVariables.CurrentRow.Cells[1].Value.ToString();
                VariableCollection.Remove(v);
                this.RefreshVariableDataGrid();
            }
        }
    }
}
