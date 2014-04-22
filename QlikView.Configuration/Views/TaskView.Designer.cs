namespace QlikView.Configuration
{
    partial class TaskView
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
            this.txtDesc = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtOutputFolder = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageReports = new System.Windows.Forms.TabPage();
            this.btnDeleteReport = new System.Windows.Forms.Button();
            this.btnAddReport = new System.Windows.Forms.Button();
            this.comboBoxReports = new System.Windows.Forms.ComboBox();
            this.dataGridViewReports = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPageRecipients = new System.Windows.Forms.TabPage();
            this.label8 = new System.Windows.Forms.Label();
            this.comboBoxGroups = new System.Windows.Forms.ComboBox();
            this.btnDeleteRecipient = new System.Windows.Forms.Button();
            this.btnAddRecipient = new System.Windows.Forms.Button();
            this.comboBoxRecipients = new System.Windows.Forms.ComboBox();
            this.dataGridViewRecipient = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPageMessage = new System.Windows.Forms.TabPage();
            this.richTextBoxBody = new System.Windows.Forms.RichTextBox();
            this.txtMessageSubject = new System.Windows.Forms.TextBox();
            this.txtMessageCC = new System.Windows.Forms.TextBox();
            this.txtMessageFrom = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.checkBoxSendMailInSingle = new System.Windows.Forms.CheckBox();
            this.checkBoxMergeInSingleExcel = new System.Windows.Forms.CheckBox();
            this.tabControl1.SuspendLayout();
            this.tabPageReports.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewReports)).BeginInit();
            this.tabPageRecipients.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewRecipient)).BeginInit();
            this.tabPageMessage.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtDesc
            // 
            this.txtDesc.Location = new System.Drawing.Point(117, 58);
            this.txtDesc.Name = "txtDesc";
            this.txtDesc.Size = new System.Drawing.Size(348, 20);
            this.txtDesc.TabIndex = 11;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(117, 21);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(348, 20);
            this.txtName.TabIndex = 10;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(37, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Description";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(62, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Name";
            // 
            // txtOutputFolder
            // 
            this.txtOutputFolder.Location = new System.Drawing.Point(117, 95);
            this.txtOutputFolder.Name = "txtOutputFolder";
            this.txtOutputFolder.Size = new System.Drawing.Size(348, 20);
            this.txtOutputFolder.TabIndex = 13;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(26, 98);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Output Folder";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageReports);
            this.tabControl1.Controls.Add(this.tabPageRecipients);
            this.tabControl1.Controls.Add(this.tabPageMessage);
            this.tabControl1.Location = new System.Drawing.Point(117, 201);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(487, 342);
            this.tabControl1.TabIndex = 14;
            // 
            // tabPageReports
            // 
            this.tabPageReports.Controls.Add(this.btnDeleteReport);
            this.tabPageReports.Controls.Add(this.btnAddReport);
            this.tabPageReports.Controls.Add(this.comboBoxReports);
            this.tabPageReports.Controls.Add(this.dataGridViewReports);
            this.tabPageReports.Location = new System.Drawing.Point(4, 22);
            this.tabPageReports.Name = "tabPageReports";
            this.tabPageReports.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageReports.Size = new System.Drawing.Size(479, 316);
            this.tabPageReports.TabIndex = 0;
            this.tabPageReports.Text = "Excel Reports";
            this.tabPageReports.UseVisualStyleBackColor = true;
            // 
            // btnDeleteReport
            // 
            this.btnDeleteReport.Location = new System.Drawing.Point(393, 15);
            this.btnDeleteReport.Name = "btnDeleteReport";
            this.btnDeleteReport.Size = new System.Drawing.Size(75, 23);
            this.btnDeleteReport.TabIndex = 13;
            this.btnDeleteReport.Text = "Delete";
            this.btnDeleteReport.UseVisualStyleBackColor = true;
            this.btnDeleteReport.Click += new System.EventHandler(this.btnDeleteReport_Click);
            // 
            // btnAddReport
            // 
            this.btnAddReport.Location = new System.Drawing.Point(294, 16);
            this.btnAddReport.Name = "btnAddReport";
            this.btnAddReport.Size = new System.Drawing.Size(75, 23);
            this.btnAddReport.TabIndex = 12;
            this.btnAddReport.Text = "Add";
            this.btnAddReport.UseVisualStyleBackColor = true;
            this.btnAddReport.Click += new System.EventHandler(this.btnAddReport_Click);
            // 
            // comboBoxReports
            // 
            this.comboBoxReports.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxReports.FormattingEnabled = true;
            this.comboBoxReports.Location = new System.Drawing.Point(11, 17);
            this.comboBoxReports.Name = "comboBoxReports";
            this.comboBoxReports.Size = new System.Drawing.Size(255, 21);
            this.comboBoxReports.TabIndex = 11;
            // 
            // dataGridViewReports
            // 
            this.dataGridViewReports.AllowUserToAddRows = false;
            this.dataGridViewReports.AllowUserToDeleteRows = false;
            this.dataGridViewReports.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewReports.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewReports.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1});
            this.dataGridViewReports.Location = new System.Drawing.Point(6, 51);
            this.dataGridViewReports.Name = "dataGridViewReports";
            this.dataGridViewReports.ReadOnly = true;
            this.dataGridViewReports.Size = new System.Drawing.Size(467, 259);
            this.dataGridViewReports.TabIndex = 10;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Name";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Visible = false;
            // 
            // tabPageRecipients
            // 
            this.tabPageRecipients.Controls.Add(this.label8);
            this.tabPageRecipients.Controls.Add(this.comboBoxGroups);
            this.tabPageRecipients.Controls.Add(this.btnDeleteRecipient);
            this.tabPageRecipients.Controls.Add(this.btnAddRecipient);
            this.tabPageRecipients.Controls.Add(this.comboBoxRecipients);
            this.tabPageRecipients.Controls.Add(this.dataGridViewRecipient);
            this.tabPageRecipients.Location = new System.Drawing.Point(4, 22);
            this.tabPageRecipients.Name = "tabPageRecipients";
            this.tabPageRecipients.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageRecipients.Size = new System.Drawing.Size(479, 316);
            this.tabPageRecipients.TabIndex = 1;
            this.tabPageRecipients.Text = "Recipients";
            this.tabPageRecipients.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(11, 31);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(84, 13);
            this.label8.TabIndex = 18;
            this.label8.Text = "Recipient Group";
            // 
            // comboBoxGroups
            // 
            this.comboBoxGroups.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxGroups.FormattingEnabled = true;
            this.comboBoxGroups.Location = new System.Drawing.Point(133, 28);
            this.comboBoxGroups.Name = "comboBoxGroups";
            this.comboBoxGroups.Size = new System.Drawing.Size(195, 21);
            this.comboBoxGroups.TabIndex = 17;
            // 
            // btnDeleteRecipient
            // 
            this.btnDeleteRecipient.Location = new System.Drawing.Point(253, 82);
            this.btnDeleteRecipient.Name = "btnDeleteRecipient";
            this.btnDeleteRecipient.Size = new System.Drawing.Size(75, 23);
            this.btnDeleteRecipient.TabIndex = 16;
            this.btnDeleteRecipient.Text = "Delete";
            this.btnDeleteRecipient.UseVisualStyleBackColor = true;
            this.btnDeleteRecipient.Click += new System.EventHandler(this.btnDeleteRecipient_Click);
            // 
            // btnAddRecipient
            // 
            this.btnAddRecipient.Location = new System.Drawing.Point(154, 83);
            this.btnAddRecipient.Name = "btnAddRecipient";
            this.btnAddRecipient.Size = new System.Drawing.Size(75, 23);
            this.btnAddRecipient.TabIndex = 15;
            this.btnAddRecipient.Text = "Add";
            this.btnAddRecipient.UseVisualStyleBackColor = true;
            this.btnAddRecipient.Click += new System.EventHandler(this.btnAddRecipient_Click);
            // 
            // comboBoxRecipients
            // 
            this.comboBoxRecipients.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxRecipients.FormattingEnabled = true;
            this.comboBoxRecipients.Location = new System.Drawing.Point(8, 84);
            this.comboBoxRecipients.Name = "comboBoxRecipients";
            this.comboBoxRecipients.Size = new System.Drawing.Size(121, 21);
            this.comboBoxRecipients.TabIndex = 14;
            // 
            // dataGridViewRecipient
            // 
            this.dataGridViewRecipient.AllowUserToAddRows = false;
            this.dataGridViewRecipient.AllowUserToDeleteRows = false;
            this.dataGridViewRecipient.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewRecipient.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewRecipient.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn2});
            this.dataGridViewRecipient.Location = new System.Drawing.Point(6, 112);
            this.dataGridViewRecipient.Name = "dataGridViewRecipient";
            this.dataGridViewRecipient.ReadOnly = true;
            this.dataGridViewRecipient.Size = new System.Drawing.Size(467, 198);
            this.dataGridViewRecipient.TabIndex = 10;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Name";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Visible = false;
            // 
            // tabPageMessage
            // 
            this.tabPageMessage.Controls.Add(this.richTextBoxBody);
            this.tabPageMessage.Controls.Add(this.txtMessageSubject);
            this.tabPageMessage.Controls.Add(this.txtMessageCC);
            this.tabPageMessage.Controls.Add(this.txtMessageFrom);
            this.tabPageMessage.Controls.Add(this.label7);
            this.tabPageMessage.Controls.Add(this.label6);
            this.tabPageMessage.Controls.Add(this.label5);
            this.tabPageMessage.Controls.Add(this.label4);
            this.tabPageMessage.Location = new System.Drawing.Point(4, 22);
            this.tabPageMessage.Name = "tabPageMessage";
            this.tabPageMessage.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageMessage.Size = new System.Drawing.Size(479, 316);
            this.tabPageMessage.TabIndex = 2;
            this.tabPageMessage.Text = "Message";
            this.tabPageMessage.UseVisualStyleBackColor = true;
            // 
            // richTextBoxBody
            // 
            this.richTextBoxBody.Location = new System.Drawing.Point(90, 120);
            this.richTextBoxBody.Name = "richTextBoxBody";
            this.richTextBoxBody.Size = new System.Drawing.Size(364, 174);
            this.richTextBoxBody.TabIndex = 7;
            this.richTextBoxBody.Text = "";
            // 
            // txtMessageSubject
            // 
            this.txtMessageSubject.Location = new System.Drawing.Point(90, 84);
            this.txtMessageSubject.Name = "txtMessageSubject";
            this.txtMessageSubject.Size = new System.Drawing.Size(254, 20);
            this.txtMessageSubject.TabIndex = 6;
            // 
            // txtMessageCC
            // 
            this.txtMessageCC.Location = new System.Drawing.Point(90, 51);
            this.txtMessageCC.Name = "txtMessageCC";
            this.txtMessageCC.Size = new System.Drawing.Size(254, 20);
            this.txtMessageCC.TabIndex = 5;
            // 
            // txtMessageFrom
            // 
            this.txtMessageFrom.Location = new System.Drawing.Point(90, 16);
            this.txtMessageFrom.Name = "txtMessageFrom";
            this.txtMessageFrom.Size = new System.Drawing.Size(254, 20);
            this.txtMessageFrom.TabIndex = 4;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(38, 120);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(31, 13);
            this.label7.TabIndex = 3;
            this.label7.Text = "Body";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(26, 86);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(43, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "Subject";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(48, 55);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(21, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "CC";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(39, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "From";
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(423, 568);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 15;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(423, 568);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 23);
            this.btnUpdate.TabIndex = 16;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(529, 568);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 17;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // checkBoxSendMailInSingle
            // 
            this.checkBoxSendMailInSingle.AutoSize = true;
            this.checkBoxSendMailInSingle.Location = new System.Drawing.Point(117, 132);
            this.checkBoxSendMailInSingle.Name = "checkBoxSendMailInSingle";
            this.checkBoxSendMailInSingle.Size = new System.Drawing.Size(156, 17);
            this.checkBoxSendMailInSingle.TabIndex = 18;
            this.checkBoxSendMailInSingle.Text = "Send Reports in Single Mail";
            this.checkBoxSendMailInSingle.UseVisualStyleBackColor = true;
            // 
            // checkBoxMergeInSingleExcel
            // 
            this.checkBoxMergeInSingleExcel.AutoSize = true;
            this.checkBoxMergeInSingleExcel.Location = new System.Drawing.Point(117, 164);
            this.checkBoxMergeInSingleExcel.Name = "checkBoxMergeInSingleExcel";
            this.checkBoxMergeInSingleExcel.Size = new System.Drawing.Size(128, 17);
            this.checkBoxMergeInSingleExcel.TabIndex = 19;
            this.checkBoxMergeInSingleExcel.Text = "Merge in Single Excel";
            this.checkBoxMergeInSingleExcel.UseVisualStyleBackColor = true;
            // 
            // TaskView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(628, 614);
            this.Controls.Add(this.checkBoxMergeInSingleExcel);
            this.Controls.Add(this.checkBoxSendMailInSingle);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.txtOutputFolder);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtDesc);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "TaskView";
            this.Text = "TaskView";
            this.Load += new System.EventHandler(this.TaskView_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPageReports.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewReports)).EndInit();
            this.tabPageRecipients.ResumeLayout(false);
            this.tabPageRecipients.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewRecipient)).EndInit();
            this.tabPageMessage.ResumeLayout(false);
            this.tabPageMessage.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtDesc;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtOutputFolder;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageReports;
        private System.Windows.Forms.TabPage tabPageRecipients;
        private System.Windows.Forms.TabPage tabPageMessage;
        private System.Windows.Forms.DataGridView dataGridViewReports;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridView dataGridViewRecipient;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.Button btnDeleteReport;
        private System.Windows.Forms.Button btnAddReport;
        private System.Windows.Forms.ComboBox comboBoxReports;
        private System.Windows.Forms.Button btnDeleteRecipient;
        private System.Windows.Forms.Button btnAddRecipient;
        private System.Windows.Forms.ComboBox comboBoxRecipients;
        private System.Windows.Forms.RichTextBox richTextBoxBody;
        private System.Windows.Forms.TextBox txtMessageSubject;
        private System.Windows.Forms.TextBox txtMessageCC;
        private System.Windows.Forms.TextBox txtMessageFrom;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox comboBoxGroups;
        private System.Windows.Forms.CheckBox checkBoxSendMailInSingle;
        private System.Windows.Forms.CheckBox checkBoxMergeInSingleExcel;
    }
}