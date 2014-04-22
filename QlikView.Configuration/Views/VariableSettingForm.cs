using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QlikView.Configuration
{
    public partial class VariableSettingForm : Form
    {
        public string VariableName { get; set; }
        public string Value { get; set; }
        public string Expression { get; set; }

        public VariableSettingForm()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.txtName.Text))
            {
                MessageBox.Show("Please input name.");
                return;
            }

            if (string.IsNullOrWhiteSpace(this.txtValue.Text) && string.IsNullOrWhiteSpace(this.txtExpression.Text))
            {
                MessageBox.Show("Value or expression is required.");
                return;
            }

            this.VariableName = this.txtName.Text.Trim();
            this.Value = this.txtValue.Text.Trim();
            this.Expression = this.txtExpression.Text.Trim();

            DialogResult = System.Windows.Forms.DialogResult.OK;

            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }
    }
}
