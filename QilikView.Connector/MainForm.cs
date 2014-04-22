using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QlikView.Connector
{
    public partial class MainForm : Form
    {
        IQlikViewConnector connector;
        public MainForm()
        {
            InitializeComponent();
            connector = new QlikViewConnector();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.connector.SetConnection(new QlikView.Common.ReportConnection()
            {
                ServerName = "",
                IsLocal = false,
                User = "tony.wu",
                Password = "Help$123",
                QlikViewDocument = "qvp://usb-etbiapp/b2c/etag_marketing.qvw"
            });
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.connector.OpenConnector();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            List<string> list = this.connector.GetFieldList();
        }
    }
}
