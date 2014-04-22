using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QlikView.ReportVerification
{
    public partial class MainForm : Form,IVerificationView
    {
        public MainForm()
        {
            InitializeComponent();
        }

        public ReportVerificationModel Model
        {
            get;
            set;
        }


        public void RefreshUI()
        {
            this.comboBox1.DataSource = this.Model.DestinationDataSet.Tables.Keys.ToList();

            if (this.Model.IsIdentity == false)
            {
                MessageBox.Show("Ops! There are some cells not identity.");
            }
            else
            {
                MessageBox.Show("Congradulations! All cells are identity.");
            }
        }

        private void btnVerify_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtDestinationFile.Text) || string.IsNullOrEmpty(this.txtSourceFile.Text))
            {
                MessageBox.Show("Please select the source file or destination file.");
                return;
            }

            int count;
            if (int.TryParse(this.txtSourceHeaderNumber.Text.Trim(),out count) == false || int.TryParse(this.txtDestinationHeaderNumber.Text.Trim(), out count) == false)
            {
                MessageBox.Show("The header number is empty or is not the integer.");
                return;
            }

            VerificationPresenter presenter = new VerificationPresenter();
            presenter.View = this;
            presenter.View.Model = new ReportVerificationModel();

            this.Model.DestinationFile = this.txtDestinationFile.Text;
            this.Model.DestinationHeaderRowNumber = int.Parse(this.txtDestinationHeaderNumber.Text.Trim());
            this.Model.SourceFile = this.txtSourceFile.Text;
            this.Model.SourceHeaderRowNumber = int.Parse(this.txtSourceHeaderNumber.Text.Trim());

            presenter.DoVerification();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.comboBox1.SelectedItem != null)
            {
                string name = this.comboBox1.SelectedValue.ToString();
                this.dataGridView1.DataSource = this.Model.VerifiedResultSet.Tables[name].DataTable;
                this.lblMsg.Text = this.Model.VerifiedResultMsg[name];
            }
        }

        private void btnSourceOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();

            dialog.Filter = "Excel 2003(*.xls)|*.xls|Excel File(*.xlsx)|*.xlsx";

            DialogResult result = dialog.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                this.txtSourceFile.Text = dialog.FileName;
            }
        }

        private void btnDestinationOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();

            dialog.Filter = "Excel 2003(*.xls)|*.xls|Excel File(*.xlsx)|*.xlsx";

            DialogResult result = dialog.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                this.txtDestinationFile.Text = dialog.FileName;
            }
        }
    }
}
