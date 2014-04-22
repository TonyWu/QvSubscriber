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
using System.Threading.Tasks;

namespace QlikView.Configuration
{
    public partial class MainForm : Form, IMainView
    {
        public MainForm()
        {
            InitializeComponent();
        }     

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.RefreshUI();
            this.RefreshSmtpUI();
        }

        #region IMainView Members

        public void RefreshUI()
        {
            this.dataGridViewConnection.DataSource = ReportConfig.ConnectionManager.ItemCollection.Values.ToList();
            this.dataGridViewTask.DataSource = ReportConfig.ReportTaskManager.ItemCollection.Values.ToList();
            this.dataGridViewReport.DataSource = ReportConfig.QvReportManager.ItemCollection.Values.ToList();
            this.dataGridViewGroup.DataSource = ReportConfig.RecipientGroupManager.ItemCollection.Values.ToList();
            this.dataGridViewFilter.DataSource = ReportConfig.FilterManager.ItemCollection.Values.ToList();
            this.dataGridViewRecipient.DataSource = ReportConfig.RecipientManager.ItemCollection.Values.ToList();
        }

        public void RefreshSmtpUI()
        {
            this.txtSmtpServer.Text = ReportConfig.SmtpServerManager.SmtpServer.Server;
            this.txtSmtpUser.Text = ReportConfig.SmtpServerManager.SmtpServer.User;
            this.txtSmtpPassword.Text = ReportConfig.SmtpServerManager.SmtpServer.Password;
            this.txtExceptionFrom.Text = ReportConfig.SmtpServerManager.SmtpServer.ExceptionFrom;
            this.txtExceptionTo.Text = ReportConfig.SmtpServerManager.SmtpServer.ExceptionTo;

            this.txtPickupDirectoryLocation.Text = ReportConfig.SmtpServerManager.SmtpServer.PickupDirectoryLocation;
            this.checkBoxPickupDirectoryLocation.Checked = ReportConfig.SmtpServerManager.SmtpServer.UsePickupDirectoryLocation;

            if (this.checkBoxPickupDirectoryLocation.Checked)
                this.txtPickupDirectoryLocation.Enabled = true;
            else
                this.txtPickupDirectoryLocation.Enabled = false;
        }

        #endregion

        private void OpenView<T>(object sender, CommandArgs<T> e, ReportConfigManagerBase<T> configMgr) where T:IReportItem
        {
            Type viewType = System.Reflection.Assembly.GetExecutingAssembly().GetType("QlikView.Configuration." + e.TypeProfix + "View");
            IBaseView<T> view = Activator.CreateInstance(viewType) as IBaseView<T>;
            view.Item = e.Target;
         
            GeneralPresenter<T> presenter = new GeneralPresenter<T>();
            presenter.MainView = e.MainView;
            presenter.View = view;
            presenter.ConfigManager = configMgr;

            presenter.Run();
        }

        private void OnRaiseView<T>(object sender, DataGridView dataGrdView, ReportConfigManagerBase<T> configMgr, string typeProfix) where T:IReportItem,new()
        {
            try
            {
                Button button = sender as Button;
                T target = default(T);
                if (button.Text == "Add")
                    target = new T();
                else
                    target = (T)dataGrdView.CurrentRow.DataBoundItem;

                this.OpenView<T>(sender, new CommandArgs<T>()
                {
                    MainView = this,
                    Target = target,
                    TypeProfix = typeProfix
                },configMgr);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #region Events

        private void btnRecipient_Click(object sender, EventArgs e)
        {
            this.OnRaiseView<Recipient>(sender, this.dataGridViewRecipient, ReportConfig.RecipientManager, "Recipient");
        }      
      
        private void btnGroup_Click(object sender, EventArgs e)
        {
            this.OnRaiseView<RecipientGroup>(sender, this.dataGridViewGroup, ReportConfig.RecipientGroupManager, "Group");
        }

        private void btnConnection_Click(object sender, EventArgs e)
        {
            this.OnRaiseView<ReportConnection>(sender, this.dataGridViewConnection, ReportConfig.ConnectionManager, "Connection");
        }

        private void btnExcelReport_Click(object sender, EventArgs e)
        {
            this.OnRaiseView<QlikViewReport>(sender, this.dataGridViewReport, ReportConfig.QvReportManager, "ExcelReport");
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            this.OnRaiseView<Filter>(sender, this.dataGridViewFilter, ReportConfig.FilterManager, "Filter");
        }

        private void btnTask_Click(object sender, EventArgs e)
        {
            this.OnRaiseView<ReportTask>(sender, this.dataGridViewTask, ReportConfig.ReportTaskManager, "Task");
        }

        #endregion

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.dataGridViewTask.CurrentRow != null)
                {
                    ExportEngine engine = new ExportEngine(QlikViewConnectorProxy.Instance);
                    engine.Logger = new QVConfigLog();
                    this.lblRunning.Text = "It is Running.....";
                    IError error = engine.RunTask(this.dataGridViewTask.CurrentRow.DataBoundItem as ReportTask, ReportConfig.SmtpServerManager.SmtpServer);
                    if (error.HasError)
                        this.lblRunning.Text = "Run Test failed.";
                    else
                        this.lblRunning.Text = "Run Test Succeed.";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
        }

        private void btnUpdateSmtp_Click(object sender, EventArgs e)
        {
            ReportConfig.SmtpServerManager.SmtpServer.Server = this.txtSmtpServer.Text;
            ReportConfig.SmtpServerManager.SmtpServer.User = this.txtSmtpUser.Text;
            ReportConfig.SmtpServerManager.SmtpServer.Password = this.txtSmtpPassword.Text;
            ReportConfig.SmtpServerManager.SmtpServer.ExceptionFrom = this.txtExceptionFrom.Text;
            ReportConfig.SmtpServerManager.SmtpServer.ExceptionTo = this.txtExceptionTo.Text;
            ReportConfig.SmtpServerManager.SmtpServer.UsePickupDirectoryLocation = this.checkBoxPickupDirectoryLocation.Checked;
            ReportConfig.SmtpServerManager.SmtpServer.PickupDirectoryLocation = this.txtPickupDirectoryLocation.Text;

            ReportConfig.SmtpServerManager.Save();

            MessageBox.Show("Update complete.");
        }

        #region Item delete

        private void btnTaskDalete_Click(object sender, EventArgs e)
        {
            this.ExecuteDelete<ReportTask>(this.dataGridViewTask.CurrentRow, ReportConfig.ReportTaskManager);
        }

        private void btnReportDelete_Click(object sender, EventArgs e)
        {
            this.ExecuteDelete<QlikViewReport>(this.dataGridViewReport.CurrentRow, ReportConfig.QvReportManager);
        }

        private void btnFilterDelete_Click(object sender, EventArgs e)
        {
            this.ExecuteDelete<Filter>(this.dataGridViewFilter.CurrentRow, ReportConfig.FilterManager);
        }

        private void btnGroupDelete_Click(object sender, EventArgs e)
        {
            this.ExecuteDelete<RecipientGroup>(this.dataGridViewGroup.CurrentRow, ReportConfig.RecipientGroupManager);           
        }

        private void btnRecipientDelete_Click(object sender, EventArgs e)
        {
            this.ExecuteDelete<Recipient>(this.dataGridViewRecipient.CurrentRow, ReportConfig.RecipientManager);
        }

        private void btnConnectionDelete_Click(object sender, EventArgs e)
        {
            this.ExecuteDelete<ReportConnection>(this.dataGridViewConnection.CurrentRow, ReportConfig.ConnectionManager);
        }

        private void ExecuteDelete<T>(DataGridViewRow currentRow, ReportConfigManagerBase<T> mgr) where T:IReportItem
        {
            if (this.ComfirmDeleteItem(currentRow))
            {
                T item = (T)currentRow.DataBoundItem;

                IError error = mgr.RemoveItem(item.Name);
                if (error.HasError)
                {
                    MessageBox.Show("Can not remove the item. \n" + error.ErrorMessage);
                }
                else
                {
                    this.RefreshUI();
                }
            }
        }

        bool ComfirmDeleteItem(DataGridViewRow row)
        {
            if (row == null)
            {
                MessageBox.Show("Please select a item.");
                return false;
            }

            DialogResult result = MessageBox.Show("Do you want to delete this item?", "Delete confirm", MessageBoxButtons.YesNo);
            return result == System.Windows.Forms.DialogResult.Yes;
        }

        #endregion

        private void dataGridViewConnection_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            //Hide the password column
            if (this.dataGridViewConnection.ColumnCount > 9)
                this.dataGridViewConnection.Columns[9].Visible = false;
        }

        private void checkBoxPickupDirectoryLocation_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBoxPickupDirectoryLocation.Checked)
                this.txtPickupDirectoryLocation.Enabled = true;
            else
                this.txtPickupDirectoryLocation.Enabled = false;
        }

        private void btnReportRunTest_Click(object sender, EventArgs e)
        {
            if (this.dataGridViewReport.CurrentRow != null)
            {
                ExportEngine engine = new ExportEngine(QlikViewConnectorProxy.Instance);
                engine.Logger = new QVConfigLog();
                this.lblRunning.Text = "It is Running.....";
                bool succeed = engine.RunReport(this.dataGridViewReport.CurrentRow.DataBoundItem as QlikViewReport, ReportConfig.SmtpServerManager.SmtpServer);
                if (succeed)
                    this.lblReportRunTestMsg.Text = "Run Test Succeed.";
                else
                    this.lblReportRunTestMsg.Text = "Run Test failed.";
            }
        }

    }
}
