namespace QlikView.Configuration
{
    partial class ExcelReportView
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtDesc = new System.Windows.Forms.TextBox();
            this.comboBoxConnection = new System.Windows.Forms.ComboBox();
            this.checkBoxEnableDynamicNaming = new System.Windows.Forms.CheckBox();
            this.txtOutputFileName = new System.Windows.Forms.TextBox();
            this.txtObjectId = new System.Windows.Forms.TextBox();
            this.comboBoxFilters = new System.Windows.Forms.ComboBox();
            this.btnAddObjectId = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.checkBoxEmbedded = new System.Windows.Forms.CheckBox();
            this.comboBoxReportType = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(80, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(55, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Description";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(54, 107);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Connection";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(26, 239);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Output File Name";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(32, 276);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(83, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Export Object Id";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(86, 316);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Filter";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(145, 24);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(211, 20);
            this.txtName.TabIndex = 6;
            // 
            // txtDesc
            // 
            this.txtDesc.Location = new System.Drawing.Point(145, 61);
            this.txtDesc.Name = "txtDesc";
            this.txtDesc.Size = new System.Drawing.Size(211, 20);
            this.txtDesc.TabIndex = 7;
            // 
            // comboBoxConnection
            // 
            this.comboBoxConnection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxConnection.FormattingEnabled = true;
            this.comboBoxConnection.Location = new System.Drawing.Point(145, 104);
            this.comboBoxConnection.Name = "comboBoxConnection";
            this.comboBoxConnection.Size = new System.Drawing.Size(211, 21);
            this.comboBoxConnection.TabIndex = 8;
            this.comboBoxConnection.SelectedIndexChanged += new System.EventHandler(this.comboBoxConnection_SelectedIndexChanged);
            // 
            // checkBoxEnableDynamicNaming
            // 
            this.checkBoxEnableDynamicNaming.AutoSize = true;
            this.checkBoxEnableDynamicNaming.Location = new System.Drawing.Point(145, 212);
            this.checkBoxEnableDynamicNaming.Name = "checkBoxEnableDynamicNaming";
            this.checkBoxEnableDynamicNaming.Size = new System.Drawing.Size(142, 17);
            this.checkBoxEnableDynamicNaming.TabIndex = 9;
            this.checkBoxEnableDynamicNaming.Text = "Enable Dynamic Naming";
            this.checkBoxEnableDynamicNaming.UseVisualStyleBackColor = true;
            // 
            // txtOutputFileName
            // 
            this.txtOutputFileName.Location = new System.Drawing.Point(145, 239);
            this.txtOutputFileName.Name = "txtOutputFileName";
            this.txtOutputFileName.Size = new System.Drawing.Size(211, 20);
            this.txtOutputFileName.TabIndex = 10;
            // 
            // txtObjectId
            // 
            this.txtObjectId.Location = new System.Drawing.Point(145, 274);
            this.txtObjectId.Name = "txtObjectId";
            this.txtObjectId.Size = new System.Drawing.Size(211, 20);
            this.txtObjectId.TabIndex = 11;
            // 
            // comboBoxFilters
            // 
            this.comboBoxFilters.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFilters.FormattingEnabled = true;
            this.comboBoxFilters.Location = new System.Drawing.Point(145, 315);
            this.comboBoxFilters.Name = "comboBoxFilters";
            this.comboBoxFilters.Size = new System.Drawing.Size(211, 21);
            this.comboBoxFilters.TabIndex = 12;
            // 
            // btnAddObjectId
            // 
            this.btnAddObjectId.Location = new System.Drawing.Point(367, 271);
            this.btnAddObjectId.Name = "btnAddObjectId";
            this.btnAddObjectId.Size = new System.Drawing.Size(27, 23);
            this.btnAddObjectId.TabIndex = 13;
            this.btnAddObjectId.Text = "...";
            this.btnAddObjectId.UseVisualStyleBackColor = true;
            this.btnAddObjectId.Click += new System.EventHandler(this.btnAddObjectId_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(277, 355);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 14;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(277, 354);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 23);
            this.btnUpdate.TabIndex = 15;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(367, 354);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 16;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // checkBoxEmbedded
            // 
            this.checkBoxEmbedded.AutoSize = true;
            this.checkBoxEmbedded.Location = new System.Drawing.Point(145, 180);
            this.checkBoxEmbedded.Name = "checkBoxEmbedded";
            this.checkBoxEmbedded.Size = new System.Drawing.Size(110, 17);
            this.checkBoxEmbedded.TabIndex = 17;
            this.checkBoxEmbedded.Text = "Embedded in Mail";
            this.checkBoxEmbedded.UseVisualStyleBackColor = true;
            // 
            // comboBoxReportType
            // 
            this.comboBoxReportType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxReportType.FormattingEnabled = true;
            this.comboBoxReportType.Items.AddRange(new object[] {
            "Excel",
            "JPG"});
            this.comboBoxReportType.Location = new System.Drawing.Point(145, 144);
            this.comboBoxReportType.Name = "comboBoxReportType";
            this.comboBoxReportType.Size = new System.Drawing.Size(211, 21);
            this.comboBoxReportType.TabIndex = 19;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(54, 147);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(66, 13);
            this.label7.TabIndex = 18;
            this.label7.Text = "Report Type";
            // 
            // ExcelReportView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(471, 418);
            this.Controls.Add(this.comboBoxReportType);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.checkBoxEmbedded);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnAddObjectId);
            this.Controls.Add(this.comboBoxFilters);
            this.Controls.Add(this.txtObjectId);
            this.Controls.Add(this.txtOutputFileName);
            this.Controls.Add(this.checkBoxEnableDynamicNaming);
            this.Controls.Add(this.comboBoxConnection);
            this.Controls.Add(this.txtDesc);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "ExcelReportView";
            this.Text = "ExcelReportView";
            this.Load += new System.EventHandler(this.ExcelReportView_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtDesc;
        private System.Windows.Forms.ComboBox comboBoxConnection;
        private System.Windows.Forms.CheckBox checkBoxEnableDynamicNaming;
        private System.Windows.Forms.TextBox txtOutputFileName;
        private System.Windows.Forms.TextBox txtObjectId;
        private System.Windows.Forms.ComboBox comboBoxFilters;
        private System.Windows.Forms.Button btnAddObjectId;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.CheckBox checkBoxEmbedded;
        private System.Windows.Forms.ComboBox comboBoxReportType;
        private System.Windows.Forms.Label label7;
    }
}