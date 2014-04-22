using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using QlikView.Connector;

namespace QlikView.Configuration
{
    public partial class DocumentSelectionForm : Form,ISingleFieldSelection
    {
        public DocumentSelectionForm()
        {
            InitializeComponent();
        }

        #region IObjectIdSelection Members

        public Common.ReportConnection Connection
        {
            get;
            set;
        }

        public event Action<string> ItemSelected;

        public void Open()
        {
            this.Show();
        }

        #endregion

        private void ObjectIdSelectionForm_Load(object sender, EventArgs e)
        {
            string host = string.IsNullOrWhiteSpace(this.Connection.ServiceHost) ? this.Connection.ServerName : this.Connection.ServiceHost;
            string port = string.IsNullOrWhiteSpace(this.Connection.ServicePort) ? "4799" : this.Connection.ServicePort;

            IService service = new QlikViewService(host,port);

            var query = from q in service.GetDocumentList()
                                            select new
                                            {
                                                Document = q
                                            };

            this.dataGridView1.DataSource = query.ToList();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string objectId = this.dataGridView1.CurrentRow.Cells[0].Value.ToString();

            if (this.ItemSelected != null)
                this.ItemSelected(objectId);

            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
