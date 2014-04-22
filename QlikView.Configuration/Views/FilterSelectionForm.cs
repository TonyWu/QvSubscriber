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
    public partial class FilterSelectionForm : Form
    {
        public ReportConnection Connection { get; set; }
        public ReportItemDictionary<QVField> Fields { get; set; }

        private IQlikViewConnector _qlikViewConnector;

        public FilterSelectionForm()
        {
            InitializeComponent();
            this._qlikViewConnector = this.qvConnectorComponent1;
        }

        private void FilterSelectionForm_Load(object sender, EventArgs e)
        {
            this._qlikViewConnector.SetConnection(this.Connection);
            this._qlikViewConnector.SetSelections(Fields);
            this._qlikViewConnector.OpenConnector();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            this.Fields = this._qlikViewConnector.GetCurrentSelection();
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this._qlikViewConnector.Clear();
        }

        private void btnClearBack_Click(object sender, EventArgs e)
        {
            this._qlikViewConnector.ClearBackForward();
        }
    }
}
