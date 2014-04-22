namespace QlikView.Configuration
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.TabMain = new System.Windows.Forms.TabControl();
            this.tabPageTask = new System.Windows.Forms.TabPage();
            this.dataGridViewTask = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lblRunning = new System.Windows.Forms.Label();
            this.btnTaskDalete = new System.Windows.Forms.Button();
            this.btnTaskRun = new System.Windows.Forms.Button();
            this.btnTaskEdit = new System.Windows.Forms.Button();
            this.btnTaskAdd = new System.Windows.Forms.Button();
            this.tabPageReport = new System.Windows.Forms.TabPage();
            this.dataGridViewReport = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnReportDelete = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.tabPageFilters = new System.Windows.Forms.TabPage();
            this.dataGridViewFilter = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel5 = new System.Windows.Forms.Panel();
            this.btnFilterDelete = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.tabPageGroups = new System.Windows.Forms.TabPage();
            this.dataGridViewGroup = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel6 = new System.Windows.Forms.Panel();
            this.btnGroupDelete = new System.Windows.Forms.Button();
            this.btnGroupEdit = new System.Windows.Forms.Button();
            this.btnGroupAdd = new System.Windows.Forms.Button();
            this.tabPageRecipients = new System.Windows.Forms.TabPage();
            this.dataGridViewRecipient = new System.Windows.Forms.DataGridView();
            this.columnRecipientName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnRecipientDelete = new System.Windows.Forms.Button();
            this.btnRecipientEdit = new System.Windows.Forms.Button();
            this.btnRecipientAdd = new System.Windows.Forms.Button();
            this.tabPageConnection = new System.Windows.Forms.TabPage();
            this.dataGridViewConnection = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel7 = new System.Windows.Forms.Panel();
            this.btnConnectionDelete = new System.Windows.Forms.Button();
            this.btnConnectionEdit = new System.Windows.Forms.Button();
            this.btnConnectionAdd = new System.Windows.Forms.Button();
            this.tabPageSmtpServer = new System.Windows.Forms.TabPage();
            this.checkBoxPickupDirectoryLocation = new System.Windows.Forms.CheckBox();
            this.txtPickupDirectoryLocation = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtExceptionTo = new System.Windows.Forms.TextBox();
            this.txtExceptionFrom = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnUpdateSmtp = new System.Windows.Forms.Button();
            this.txtSmtpPassword = new System.Windows.Forms.TextBox();
            this.txtSmtpUser = new System.Windows.Forms.TextBox();
            this.txtSmtpServer = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnReportRunTest = new System.Windows.Forms.Button();
            this.lblReportRunTestMsg = new System.Windows.Forms.Label();
            this.TabMain.SuspendLayout();
            this.tabPageTask.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTask)).BeginInit();
            this.panel3.SuspendLayout();
            this.tabPageReport.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewReport)).BeginInit();
            this.panel4.SuspendLayout();
            this.tabPageFilters.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFilter)).BeginInit();
            this.panel5.SuspendLayout();
            this.tabPageGroups.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewGroup)).BeginInit();
            this.panel6.SuspendLayout();
            this.tabPageRecipients.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewRecipient)).BeginInit();
            this.panel2.SuspendLayout();
            this.tabPageConnection.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewConnection)).BeginInit();
            this.panel7.SuspendLayout();
            this.tabPageSmtpServer.SuspendLayout();
            this.SuspendLayout();
            // 
            // TabMain
            // 
            this.TabMain.Controls.Add(this.tabPageTask);
            this.TabMain.Controls.Add(this.tabPageReport);
            this.TabMain.Controls.Add(this.tabPageFilters);
            this.TabMain.Controls.Add(this.tabPageGroups);
            this.TabMain.Controls.Add(this.tabPageRecipients);
            this.TabMain.Controls.Add(this.tabPageConnection);
            this.TabMain.Controls.Add(this.tabPageSmtpServer);
            this.TabMain.Location = new System.Drawing.Point(0, 1);
            this.TabMain.Name = "TabMain";
            this.TabMain.SelectedIndex = 0;
            this.TabMain.Size = new System.Drawing.Size(828, 473);
            this.TabMain.TabIndex = 0;
            // 
            // tabPageTask
            // 
            this.tabPageTask.Controls.Add(this.dataGridViewTask);
            this.tabPageTask.Controls.Add(this.panel3);
            this.tabPageTask.Location = new System.Drawing.Point(4, 22);
            this.tabPageTask.Name = "tabPageTask";
            this.tabPageTask.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageTask.Size = new System.Drawing.Size(820, 447);
            this.tabPageTask.TabIndex = 1;
            this.tabPageTask.Text = "Task";
            this.tabPageTask.UseVisualStyleBackColor = true;
            // 
            // dataGridViewTask
            // 
            this.dataGridViewTask.AllowUserToAddRows = false;
            this.dataGridViewTask.AllowUserToDeleteRows = false;
            this.dataGridViewTask.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewTask.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewTask.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn2});
            this.dataGridViewTask.Location = new System.Drawing.Point(3, 49);
            this.dataGridViewTask.Name = "dataGridViewTask";
            this.dataGridViewTask.ReadOnly = true;
            this.dataGridViewTask.Size = new System.Drawing.Size(815, 391);
            this.dataGridViewTask.TabIndex = 7;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Name";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Visible = false;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.lblRunning);
            this.panel3.Controls.Add(this.btnTaskDalete);
            this.panel3.Controls.Add(this.btnTaskRun);
            this.panel3.Controls.Add(this.btnTaskEdit);
            this.panel3.Controls.Add(this.btnTaskAdd);
            this.panel3.Location = new System.Drawing.Point(3, 7);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(813, 36);
            this.panel3.TabIndex = 6;
            // 
            // lblRunning
            // 
            this.lblRunning.AutoSize = true;
            this.lblRunning.ForeColor = System.Drawing.Color.Red;
            this.lblRunning.Location = new System.Drawing.Point(391, 14);
            this.lblRunning.Name = "lblRunning";
            this.lblRunning.Size = new System.Drawing.Size(0, 13);
            this.lblRunning.TabIndex = 4;
            // 
            // btnTaskDalete
            // 
            this.btnTaskDalete.Location = new System.Drawing.Point(198, 10);
            this.btnTaskDalete.Name = "btnTaskDalete";
            this.btnTaskDalete.Size = new System.Drawing.Size(75, 23);
            this.btnTaskDalete.TabIndex = 3;
            this.btnTaskDalete.Text = "Delete";
            this.btnTaskDalete.UseVisualStyleBackColor = true;
            this.btnTaskDalete.Click += new System.EventHandler(this.btnTaskDalete_Click);
            // 
            // btnTaskRun
            // 
            this.btnTaskRun.Location = new System.Drawing.Point(299, 10);
            this.btnTaskRun.Name = "btnTaskRun";
            this.btnTaskRun.Size = new System.Drawing.Size(75, 23);
            this.btnTaskRun.TabIndex = 2;
            this.btnTaskRun.Text = "Run Test";
            this.btnTaskRun.UseVisualStyleBackColor = true;
            this.btnTaskRun.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnTaskEdit
            // 
            this.btnTaskEdit.Location = new System.Drawing.Point(100, 10);
            this.btnTaskEdit.Name = "btnTaskEdit";
            this.btnTaskEdit.Size = new System.Drawing.Size(75, 23);
            this.btnTaskEdit.TabIndex = 1;
            this.btnTaskEdit.Text = "Edit";
            this.btnTaskEdit.UseVisualStyleBackColor = true;
            this.btnTaskEdit.Click += new System.EventHandler(this.btnTask_Click);
            // 
            // btnTaskAdd
            // 
            this.btnTaskAdd.Location = new System.Drawing.Point(5, 10);
            this.btnTaskAdd.Name = "btnTaskAdd";
            this.btnTaskAdd.Size = new System.Drawing.Size(75, 23);
            this.btnTaskAdd.TabIndex = 0;
            this.btnTaskAdd.Text = "Add";
            this.btnTaskAdd.UseVisualStyleBackColor = true;
            this.btnTaskAdd.Click += new System.EventHandler(this.btnTask_Click);
            // 
            // tabPageReport
            // 
            this.tabPageReport.Controls.Add(this.dataGridViewReport);
            this.tabPageReport.Controls.Add(this.panel4);
            this.tabPageReport.Location = new System.Drawing.Point(4, 22);
            this.tabPageReport.Name = "tabPageReport";
            this.tabPageReport.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageReport.Size = new System.Drawing.Size(820, 447);
            this.tabPageReport.TabIndex = 2;
            this.tabPageReport.Text = "Excel Report";
            this.tabPageReport.UseVisualStyleBackColor = true;
            // 
            // dataGridViewReport
            // 
            this.dataGridViewReport.AllowUserToAddRows = false;
            this.dataGridViewReport.AllowUserToDeleteRows = false;
            this.dataGridViewReport.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewReport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewReport.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn3});
            this.dataGridViewReport.Location = new System.Drawing.Point(3, 45);
            this.dataGridViewReport.Name = "dataGridViewReport";
            this.dataGridViewReport.ReadOnly = true;
            this.dataGridViewReport.Size = new System.Drawing.Size(815, 399);
            this.dataGridViewReport.TabIndex = 6;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "Name";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Visible = false;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.lblReportRunTestMsg);
            this.panel4.Controls.Add(this.btnReportRunTest);
            this.panel4.Controls.Add(this.btnReportDelete);
            this.panel4.Controls.Add(this.button3);
            this.panel4.Controls.Add(this.button4);
            this.panel4.Location = new System.Drawing.Point(3, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(813, 36);
            this.panel4.TabIndex = 5;
            // 
            // btnReportDelete
            // 
            this.btnReportDelete.Location = new System.Drawing.Point(198, 10);
            this.btnReportDelete.Name = "btnReportDelete";
            this.btnReportDelete.Size = new System.Drawing.Size(75, 23);
            this.btnReportDelete.TabIndex = 3;
            this.btnReportDelete.Text = "Delete";
            this.btnReportDelete.UseVisualStyleBackColor = true;
            this.btnReportDelete.Click += new System.EventHandler(this.btnReportDelete_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(100, 10);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 1;
            this.button3.Text = "Edit";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.btnExcelReport_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(5, 10);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 0;
            this.button4.Text = "Add";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.btnExcelReport_Click);
            // 
            // tabPageFilters
            // 
            this.tabPageFilters.Controls.Add(this.dataGridViewFilter);
            this.tabPageFilters.Controls.Add(this.panel5);
            this.tabPageFilters.Location = new System.Drawing.Point(4, 22);
            this.tabPageFilters.Name = "tabPageFilters";
            this.tabPageFilters.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageFilters.Size = new System.Drawing.Size(820, 447);
            this.tabPageFilters.TabIndex = 3;
            this.tabPageFilters.Text = "Filters";
            this.tabPageFilters.UseVisualStyleBackColor = true;
            // 
            // dataGridViewFilter
            // 
            this.dataGridViewFilter.AllowUserToAddRows = false;
            this.dataGridViewFilter.AllowUserToDeleteRows = false;
            this.dataGridViewFilter.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewFilter.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewFilter.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn4});
            this.dataGridViewFilter.Location = new System.Drawing.Point(3, 45);
            this.dataGridViewFilter.Name = "dataGridViewFilter";
            this.dataGridViewFilter.ReadOnly = true;
            this.dataGridViewFilter.Size = new System.Drawing.Size(815, 399);
            this.dataGridViewFilter.TabIndex = 6;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "Name";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Visible = false;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.btnFilterDelete);
            this.panel5.Controls.Add(this.button6);
            this.panel5.Controls.Add(this.button7);
            this.panel5.Location = new System.Drawing.Point(3, 3);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(813, 36);
            this.panel5.TabIndex = 5;
            // 
            // btnFilterDelete
            // 
            this.btnFilterDelete.Location = new System.Drawing.Point(198, 10);
            this.btnFilterDelete.Name = "btnFilterDelete";
            this.btnFilterDelete.Size = new System.Drawing.Size(75, 23);
            this.btnFilterDelete.TabIndex = 3;
            this.btnFilterDelete.Text = "Delete";
            this.btnFilterDelete.UseVisualStyleBackColor = true;
            this.btnFilterDelete.Click += new System.EventHandler(this.btnFilterDelete_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(100, 10);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(75, 23);
            this.button6.TabIndex = 1;
            this.button6.Text = "Edit";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.btnFilter_Click);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(5, 10);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(75, 23);
            this.button7.TabIndex = 0;
            this.button7.Text = "Add";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.btnFilter_Click);
            // 
            // tabPageGroups
            // 
            this.tabPageGroups.Controls.Add(this.dataGridViewGroup);
            this.tabPageGroups.Controls.Add(this.panel6);
            this.tabPageGroups.Location = new System.Drawing.Point(4, 22);
            this.tabPageGroups.Name = "tabPageGroups";
            this.tabPageGroups.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageGroups.Size = new System.Drawing.Size(820, 447);
            this.tabPageGroups.TabIndex = 4;
            this.tabPageGroups.Text = "Groups";
            this.tabPageGroups.UseVisualStyleBackColor = true;
            // 
            // dataGridViewGroup
            // 
            this.dataGridViewGroup.AllowUserToAddRows = false;
            this.dataGridViewGroup.AllowUserToDeleteRows = false;
            this.dataGridViewGroup.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewGroup.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewGroup.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn5});
            this.dataGridViewGroup.Location = new System.Drawing.Point(3, 45);
            this.dataGridViewGroup.Name = "dataGridViewGroup";
            this.dataGridViewGroup.ReadOnly = true;
            this.dataGridViewGroup.Size = new System.Drawing.Size(815, 399);
            this.dataGridViewGroup.TabIndex = 6;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.HeaderText = "Name";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.Visible = false;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.btnGroupDelete);
            this.panel6.Controls.Add(this.btnGroupEdit);
            this.panel6.Controls.Add(this.btnGroupAdd);
            this.panel6.Location = new System.Drawing.Point(3, 3);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(813, 36);
            this.panel6.TabIndex = 5;
            // 
            // btnGroupDelete
            // 
            this.btnGroupDelete.Location = new System.Drawing.Point(198, 10);
            this.btnGroupDelete.Name = "btnGroupDelete";
            this.btnGroupDelete.Size = new System.Drawing.Size(75, 23);
            this.btnGroupDelete.TabIndex = 3;
            this.btnGroupDelete.Text = "Delete";
            this.btnGroupDelete.UseVisualStyleBackColor = true;
            this.btnGroupDelete.Click += new System.EventHandler(this.btnGroupDelete_Click);
            // 
            // btnGroupEdit
            // 
            this.btnGroupEdit.Location = new System.Drawing.Point(100, 10);
            this.btnGroupEdit.Name = "btnGroupEdit";
            this.btnGroupEdit.Size = new System.Drawing.Size(75, 23);
            this.btnGroupEdit.TabIndex = 1;
            this.btnGroupEdit.Text = "Edit";
            this.btnGroupEdit.UseVisualStyleBackColor = true;
            this.btnGroupEdit.Click += new System.EventHandler(this.btnGroup_Click);
            // 
            // btnGroupAdd
            // 
            this.btnGroupAdd.Location = new System.Drawing.Point(5, 10);
            this.btnGroupAdd.Name = "btnGroupAdd";
            this.btnGroupAdd.Size = new System.Drawing.Size(75, 23);
            this.btnGroupAdd.TabIndex = 0;
            this.btnGroupAdd.Text = "Add";
            this.btnGroupAdd.UseVisualStyleBackColor = true;
            this.btnGroupAdd.Click += new System.EventHandler(this.btnGroup_Click);
            // 
            // tabPageRecipients
            // 
            this.tabPageRecipients.Controls.Add(this.dataGridViewRecipient);
            this.tabPageRecipients.Controls.Add(this.panel2);
            this.tabPageRecipients.Location = new System.Drawing.Point(4, 22);
            this.tabPageRecipients.Name = "tabPageRecipients";
            this.tabPageRecipients.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageRecipients.Size = new System.Drawing.Size(820, 447);
            this.tabPageRecipients.TabIndex = 5;
            this.tabPageRecipients.Text = "Recipients";
            this.tabPageRecipients.UseVisualStyleBackColor = true;
            // 
            // dataGridViewRecipient
            // 
            this.dataGridViewRecipient.AllowUserToAddRows = false;
            this.dataGridViewRecipient.AllowUserToDeleteRows = false;
            this.dataGridViewRecipient.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewRecipient.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewRecipient.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columnRecipientName});
            this.dataGridViewRecipient.Location = new System.Drawing.Point(2, 45);
            this.dataGridViewRecipient.Name = "dataGridViewRecipient";
            this.dataGridViewRecipient.ReadOnly = true;
            this.dataGridViewRecipient.Size = new System.Drawing.Size(815, 399);
            this.dataGridViewRecipient.TabIndex = 4;
            // 
            // columnRecipientName
            // 
            this.columnRecipientName.HeaderText = "Name";
            this.columnRecipientName.Name = "columnRecipientName";
            this.columnRecipientName.ReadOnly = true;
            this.columnRecipientName.Visible = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnRecipientDelete);
            this.panel2.Controls.Add(this.btnRecipientEdit);
            this.panel2.Controls.Add(this.btnRecipientAdd);
            this.panel2.Location = new System.Drawing.Point(2, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(813, 36);
            this.panel2.TabIndex = 3;
            // 
            // btnRecipientDelete
            // 
            this.btnRecipientDelete.Location = new System.Drawing.Point(198, 10);
            this.btnRecipientDelete.Name = "btnRecipientDelete";
            this.btnRecipientDelete.Size = new System.Drawing.Size(75, 23);
            this.btnRecipientDelete.TabIndex = 3;
            this.btnRecipientDelete.Text = "Delete";
            this.btnRecipientDelete.UseVisualStyleBackColor = true;
            this.btnRecipientDelete.Click += new System.EventHandler(this.btnRecipientDelete_Click);
            // 
            // btnRecipientEdit
            // 
            this.btnRecipientEdit.Location = new System.Drawing.Point(100, 10);
            this.btnRecipientEdit.Name = "btnRecipientEdit";
            this.btnRecipientEdit.Size = new System.Drawing.Size(75, 23);
            this.btnRecipientEdit.TabIndex = 1;
            this.btnRecipientEdit.Text = "Edit";
            this.btnRecipientEdit.UseVisualStyleBackColor = true;
            this.btnRecipientEdit.Click += new System.EventHandler(this.btnRecipient_Click);
            // 
            // btnRecipientAdd
            // 
            this.btnRecipientAdd.Location = new System.Drawing.Point(5, 10);
            this.btnRecipientAdd.Name = "btnRecipientAdd";
            this.btnRecipientAdd.Size = new System.Drawing.Size(75, 23);
            this.btnRecipientAdd.TabIndex = 0;
            this.btnRecipientAdd.Text = "Add";
            this.btnRecipientAdd.UseVisualStyleBackColor = true;
            this.btnRecipientAdd.Click += new System.EventHandler(this.btnRecipient_Click);
            // 
            // tabPageConnection
            // 
            this.tabPageConnection.Controls.Add(this.dataGridViewConnection);
            this.tabPageConnection.Controls.Add(this.panel7);
            this.tabPageConnection.Location = new System.Drawing.Point(4, 22);
            this.tabPageConnection.Name = "tabPageConnection";
            this.tabPageConnection.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageConnection.Size = new System.Drawing.Size(820, 447);
            this.tabPageConnection.TabIndex = 6;
            this.tabPageConnection.Text = "Connections";
            this.tabPageConnection.UseVisualStyleBackColor = true;
            // 
            // dataGridViewConnection
            // 
            this.dataGridViewConnection.AllowUserToAddRows = false;
            this.dataGridViewConnection.AllowUserToDeleteRows = false;
            this.dataGridViewConnection.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewConnection.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewConnection.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn6});
            this.dataGridViewConnection.Location = new System.Drawing.Point(3, 45);
            this.dataGridViewConnection.Name = "dataGridViewConnection";
            this.dataGridViewConnection.ReadOnly = true;
            this.dataGridViewConnection.Size = new System.Drawing.Size(815, 399);
            this.dataGridViewConnection.TabIndex = 6;
            this.dataGridViewConnection.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dataGridViewConnection_DataBindingComplete);
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.HeaderText = "Name";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            this.dataGridViewTextBoxColumn6.Visible = false;
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.btnConnectionDelete);
            this.panel7.Controls.Add(this.btnConnectionEdit);
            this.panel7.Controls.Add(this.btnConnectionAdd);
            this.panel7.Location = new System.Drawing.Point(3, 3);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(813, 36);
            this.panel7.TabIndex = 5;
            // 
            // btnConnectionDelete
            // 
            this.btnConnectionDelete.Location = new System.Drawing.Point(198, 10);
            this.btnConnectionDelete.Name = "btnConnectionDelete";
            this.btnConnectionDelete.Size = new System.Drawing.Size(75, 23);
            this.btnConnectionDelete.TabIndex = 3;
            this.btnConnectionDelete.Text = "Delete";
            this.btnConnectionDelete.UseVisualStyleBackColor = true;
            this.btnConnectionDelete.Click += new System.EventHandler(this.btnConnectionDelete_Click);
            // 
            // btnConnectionEdit
            // 
            this.btnConnectionEdit.Location = new System.Drawing.Point(100, 10);
            this.btnConnectionEdit.Name = "btnConnectionEdit";
            this.btnConnectionEdit.Size = new System.Drawing.Size(75, 23);
            this.btnConnectionEdit.TabIndex = 1;
            this.btnConnectionEdit.Text = "Edit";
            this.btnConnectionEdit.UseVisualStyleBackColor = true;
            this.btnConnectionEdit.Click += new System.EventHandler(this.btnConnection_Click);
            // 
            // btnConnectionAdd
            // 
            this.btnConnectionAdd.Location = new System.Drawing.Point(5, 10);
            this.btnConnectionAdd.Name = "btnConnectionAdd";
            this.btnConnectionAdd.Size = new System.Drawing.Size(75, 23);
            this.btnConnectionAdd.TabIndex = 0;
            this.btnConnectionAdd.Text = "Add";
            this.btnConnectionAdd.UseVisualStyleBackColor = true;
            this.btnConnectionAdd.Click += new System.EventHandler(this.btnConnection_Click);
            // 
            // tabPageSmtpServer
            // 
            this.tabPageSmtpServer.Controls.Add(this.checkBoxPickupDirectoryLocation);
            this.tabPageSmtpServer.Controls.Add(this.txtPickupDirectoryLocation);
            this.tabPageSmtpServer.Controls.Add(this.label5);
            this.tabPageSmtpServer.Controls.Add(this.txtExceptionTo);
            this.tabPageSmtpServer.Controls.Add(this.txtExceptionFrom);
            this.tabPageSmtpServer.Controls.Add(this.label7);
            this.tabPageSmtpServer.Controls.Add(this.label6);
            this.tabPageSmtpServer.Controls.Add(this.label2);
            this.tabPageSmtpServer.Controls.Add(this.btnUpdateSmtp);
            this.tabPageSmtpServer.Controls.Add(this.txtSmtpPassword);
            this.tabPageSmtpServer.Controls.Add(this.txtSmtpUser);
            this.tabPageSmtpServer.Controls.Add(this.txtSmtpServer);
            this.tabPageSmtpServer.Controls.Add(this.label4);
            this.tabPageSmtpServer.Controls.Add(this.label3);
            this.tabPageSmtpServer.Controls.Add(this.label1);
            this.tabPageSmtpServer.Location = new System.Drawing.Point(4, 22);
            this.tabPageSmtpServer.Name = "tabPageSmtpServer";
            this.tabPageSmtpServer.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSmtpServer.Size = new System.Drawing.Size(820, 447);
            this.tabPageSmtpServer.TabIndex = 7;
            this.tabPageSmtpServer.Text = "Smtp Server";
            this.tabPageSmtpServer.UseVisualStyleBackColor = true;
            // 
            // checkBoxPickupDirectoryLocation
            // 
            this.checkBoxPickupDirectoryLocation.AutoSize = true;
            this.checkBoxPickupDirectoryLocation.Location = new System.Drawing.Point(179, 174);
            this.checkBoxPickupDirectoryLocation.Name = "checkBoxPickupDirectoryLocation";
            this.checkBoxPickupDirectoryLocation.Size = new System.Drawing.Size(170, 17);
            this.checkBoxPickupDirectoryLocation.TabIndex = 17;
            this.checkBoxPickupDirectoryLocation.Text = "Use Pickup Directory Location";
            this.checkBoxPickupDirectoryLocation.UseVisualStyleBackColor = true;
            this.checkBoxPickupDirectoryLocation.CheckedChanged += new System.EventHandler(this.checkBoxPickupDirectoryLocation_CheckedChanged);
            // 
            // txtPickupDirectoryLocation
            // 
            this.txtPickupDirectoryLocation.Location = new System.Drawing.Point(179, 210);
            this.txtPickupDirectoryLocation.Name = "txtPickupDirectoryLocation";
            this.txtPickupDirectoryLocation.Size = new System.Drawing.Size(209, 20);
            this.txtPickupDirectoryLocation.TabIndex = 16;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(14, 213);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(129, 13);
            this.label5.TabIndex = 15;
            this.label5.Text = "Pickup Directory Location";
            // 
            // txtExceptionTo
            // 
            this.txtExceptionTo.Location = new System.Drawing.Point(523, 108);
            this.txtExceptionTo.Name = "txtExceptionTo";
            this.txtExceptionTo.Size = new System.Drawing.Size(268, 20);
            this.txtExceptionTo.TabIndex = 14;
            // 
            // txtExceptionFrom
            // 
            this.txtExceptionFrom.Location = new System.Drawing.Point(523, 63);
            this.txtExceptionFrom.Name = "txtExceptionFrom";
            this.txtExceptionFrom.Size = new System.Drawing.Size(268, 20);
            this.txtExceptionFrom.TabIndex = 13;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(475, 110);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(20, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "To";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(465, 64);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(30, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "From";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(465, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Exception Settings";
            // 
            // btnUpdateSmtp
            // 
            this.btnUpdateSmtp.Location = new System.Drawing.Point(313, 290);
            this.btnUpdateSmtp.Name = "btnUpdateSmtp";
            this.btnUpdateSmtp.Size = new System.Drawing.Size(75, 23);
            this.btnUpdateSmtp.TabIndex = 9;
            this.btnUpdateSmtp.Text = "Update";
            this.btnUpdateSmtp.UseVisualStyleBackColor = true;
            this.btnUpdateSmtp.Click += new System.EventHandler(this.btnUpdateSmtp_Click);
            // 
            // txtSmtpPassword
            // 
            this.txtSmtpPassword.Location = new System.Drawing.Point(179, 110);
            this.txtSmtpPassword.Name = "txtSmtpPassword";
            this.txtSmtpPassword.Size = new System.Drawing.Size(209, 20);
            this.txtSmtpPassword.TabIndex = 8;
            this.txtSmtpPassword.UseSystemPasswordChar = true;
            // 
            // txtSmtpUser
            // 
            this.txtSmtpUser.Location = new System.Drawing.Point(179, 65);
            this.txtSmtpUser.Name = "txtSmtpUser";
            this.txtSmtpUser.Size = new System.Drawing.Size(209, 20);
            this.txtSmtpUser.TabIndex = 7;
            // 
            // txtSmtpServer
            // 
            this.txtSmtpServer.Location = new System.Drawing.Point(179, 21);
            this.txtSmtpServer.Name = "txtSmtpServer";
            this.txtSmtpServer.Size = new System.Drawing.Size(209, 20);
            this.txtSmtpServer.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(84, 110);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Password";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(108, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "User";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(99, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Server";
            // 
            // btnReportRunTest
            // 
            this.btnReportRunTest.Location = new System.Drawing.Point(302, 10);
            this.btnReportRunTest.Name = "btnReportRunTest";
            this.btnReportRunTest.Size = new System.Drawing.Size(75, 23);
            this.btnReportRunTest.TabIndex = 4;
            this.btnReportRunTest.Text = "RunTest";
            this.btnReportRunTest.UseVisualStyleBackColor = true;
            this.btnReportRunTest.Click += new System.EventHandler(this.btnReportRunTest_Click);
            // 
            // lblReportRunTestMsg
            // 
            this.lblReportRunTestMsg.AutoSize = true;
            this.lblReportRunTestMsg.Location = new System.Drawing.Point(410, 15);
            this.lblReportRunTestMsg.Name = "lblReportRunTestMsg";
            this.lblReportRunTestMsg.Size = new System.Drawing.Size(0, 13);
            this.lblReportRunTestMsg.TabIndex = 5;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(827, 475);
            this.Controls.Add(this.TabMain);
            this.MaximumSize = new System.Drawing.Size(843, 513);
            this.MinimumSize = new System.Drawing.Size(843, 513);
            this.Name = "MainForm";
            this.Text = "Configuration";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.TabMain.ResumeLayout(false);
            this.tabPageTask.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTask)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.tabPageReport.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewReport)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.tabPageFilters.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFilter)).EndInit();
            this.panel5.ResumeLayout(false);
            this.tabPageGroups.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewGroup)).EndInit();
            this.panel6.ResumeLayout(false);
            this.tabPageRecipients.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewRecipient)).EndInit();
            this.panel2.ResumeLayout(false);
            this.tabPageConnection.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewConnection)).EndInit();
            this.panel7.ResumeLayout(false);
            this.tabPageSmtpServer.ResumeLayout(false);
            this.tabPageSmtpServer.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl TabMain;
        private System.Windows.Forms.TabPage tabPageTask;
        private System.Windows.Forms.TabPage tabPageReport;
        private System.Windows.Forms.TabPage tabPageFilters;
        private System.Windows.Forms.TabPage tabPageGroups;
        private System.Windows.Forms.TabPage tabPageRecipients;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnRecipientDelete;
        private System.Windows.Forms.Button btnRecipientEdit;
        private System.Windows.Forms.Button btnRecipientAdd;
        private System.Windows.Forms.DataGridView dataGridViewRecipient;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnRecipientName;
        private System.Windows.Forms.DataGridView dataGridViewTask;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnTaskDalete;
        private System.Windows.Forms.Button btnTaskRun;
        private System.Windows.Forms.Button btnTaskEdit;
        private System.Windows.Forms.Button btnTaskAdd;
        private System.Windows.Forms.DataGridView dataGridViewReport;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button btnReportDelete;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.DataGridView dataGridViewFilter;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button btnFilterDelete;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.DataGridView dataGridViewGroup;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Button btnGroupDelete;
        private System.Windows.Forms.Button btnGroupEdit;
        private System.Windows.Forms.Button btnGroupAdd;
        private System.Windows.Forms.TabPage tabPageConnection;
        private System.Windows.Forms.DataGridView dataGridViewConnection;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Button btnConnectionDelete;
        private System.Windows.Forms.Button btnConnectionEdit;
        private System.Windows.Forms.Button btnConnectionAdd;
        private System.Windows.Forms.TabPage tabPageSmtpServer;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSmtpPassword;
        private System.Windows.Forms.TextBox txtSmtpUser;
        private System.Windows.Forms.TextBox txtSmtpServer;
        private System.Windows.Forms.Button btnUpdateSmtp;
        private System.Windows.Forms.TextBox txtExceptionTo;
        private System.Windows.Forms.TextBox txtExceptionFrom;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblRunning;
        private System.Windows.Forms.TextBox txtPickupDirectoryLocation;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox checkBoxPickupDirectoryLocation;
        private System.Windows.Forms.Button btnReportRunTest;
        private System.Windows.Forms.Label lblReportRunTestMsg;
    }
}

