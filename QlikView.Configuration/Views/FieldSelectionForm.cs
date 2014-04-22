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
    public partial class FieldSelectionForm : Form, IFieldSelection
    {
        public FieldSelectionForm()
        {
            InitializeComponent();
        }

        #region IFieldSelection Members

        public Common.ReportConnection Connection
        {
            get;
            set;
        }

        public event Action<Common.QVField> FieldAdded;

        public void Open()
        {
            this.Show();
        }
        #endregion

        private void FieldSelectionForm_Load(object sender, EventArgs e)
        {
            QlikViewConnectorProxy.Instance.SetConnection(this.Connection);
            this.comboBoxFields.DataSource = QlikViewConnectorProxy.Instance.GetFieldList();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            QVField field = new QVField();
            field.Name = this.comboBoxFields.SelectedValue.ToString();

            if (string.IsNullOrWhiteSpace(this.txtExpression.Text))
            {
                for (int i = 0; i < this.dataGridView1.SelectedRows.Count; i++)
                {
                    FieldValue value = this.dataGridView1.SelectedRows[i].DataBoundItem as FieldValue;
                    field.Values.Add(value);
                }
            }
            else
            {
                field.Expression = this.txtExpression.Text.Trim();
            }

            if (this.FieldAdded != null)
                this.FieldAdded(field);

            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBoxFields_SelectedIndexChanged(object sender, EventArgs e)
        {
            QVField field = QlikViewConnectorProxy.Instance.GetQVFieldByName(this.comboBoxFields.SelectedValue.ToString());

            this.dataGridView1.DataSource = field.Values;
        }

    }
}
