namespace QlikView.ReportVerification
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
            this.txtSourceFile = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDestinationFile = new System.Windows.Forms.TextBox();
            this.btnSourceOpen = new System.Windows.Forms.Button();
            this.btnDestinationOpen = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtSourceHeaderNumber = new System.Windows.Forms.TextBox();
            this.txtDestinationHeaderNumber = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnVerify = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblMsg = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtSourceFile
            // 
            this.txtSourceFile.Location = new System.Drawing.Point(85, 15);
            this.txtSourceFile.Name = "txtSourceFile";
            this.txtSourceFile.Size = new System.Drawing.Size(424, 20);
            this.txtSourceFile.TabIndex = 0;
            this.txtSourceFile.Text = "D:\\ReportExport\\UnitTest\\ReportVerification\\FunnelMonthly_2012Month1.xls";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Source: ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Destination: ";
            // 
            // txtDestinationFile
            // 
            this.txtDestinationFile.Location = new System.Drawing.Point(85, 52);
            this.txtDestinationFile.Name = "txtDestinationFile";
            this.txtDestinationFile.Size = new System.Drawing.Size(424, 20);
            this.txtDestinationFile.TabIndex = 2;
            this.txtDestinationFile.Text = "D:\\ReportExport\\UnitTest\\ReportVerification\\FunnelMonthly_2012Month2.xls";
            // 
            // btnSourceOpen
            // 
            this.btnSourceOpen.Location = new System.Drawing.Point(515, 12);
            this.btnSourceOpen.Name = "btnSourceOpen";
            this.btnSourceOpen.Size = new System.Drawing.Size(56, 23);
            this.btnSourceOpen.TabIndex = 4;
            this.btnSourceOpen.Text = "Open...";
            this.btnSourceOpen.UseVisualStyleBackColor = true;
            this.btnSourceOpen.Click += new System.EventHandler(this.btnSourceOpen_Click);
            // 
            // btnDestinationOpen
            // 
            this.btnDestinationOpen.Location = new System.Drawing.Point(516, 50);
            this.btnDestinationOpen.Name = "btnDestinationOpen";
            this.btnDestinationOpen.Size = new System.Drawing.Size(55, 23);
            this.btnDestinationOpen.TabIndex = 5;
            this.btnDestinationOpen.Text = "Open...";
            this.btnDestinationOpen.UseVisualStyleBackColor = true;
            this.btnDestinationOpen.Click += new System.EventHandler(this.btnDestinationOpen_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(588, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(104, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Header RowNumber";
            // 
            // txtSourceHeaderNumber
            // 
            this.txtSourceHeaderNumber.Location = new System.Drawing.Point(699, 12);
            this.txtSourceHeaderNumber.Name = "txtSourceHeaderNumber";
            this.txtSourceHeaderNumber.Size = new System.Drawing.Size(62, 20);
            this.txtSourceHeaderNumber.TabIndex = 7;
            this.txtSourceHeaderNumber.Text = "3";
            // 
            // txtDestinationHeaderNumber
            // 
            this.txtDestinationHeaderNumber.Location = new System.Drawing.Point(698, 52);
            this.txtDestinationHeaderNumber.Name = "txtDestinationHeaderNumber";
            this.txtDestinationHeaderNumber.Size = new System.Drawing.Size(63, 20);
            this.txtDestinationHeaderNumber.TabIndex = 9;
            this.txtDestinationHeaderNumber.Text = "3";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(588, 55);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(104, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Header RowNumber";
            // 
            // btnVerify
            // 
            this.btnVerify.Location = new System.Drawing.Point(698, 94);
            this.btnVerify.Name = "btnVerify";
            this.btnVerify.Size = new System.Drawing.Size(63, 23);
            this.btnVerify.TabIndex = 10;
            this.btnVerify.Text = "Verify";
            this.btnVerify.UseVisualStyleBackColor = true;
            this.btnVerify.Click += new System.EventHandler(this.btnVerify_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(11, 52);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(654, 304);
            this.dataGridView1.TabIndex = 11;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 461);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(775, 22);
            this.statusStrip1.TabIndex = 12;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(100, 16);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 28);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(39, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Tables";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(69, 25);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(230, 21);
            this.comboBox1.TabIndex = 14;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblMsg);
            this.groupBox1.Controls.Add(this.comboBox1);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.dataGridView1);
            this.groupBox1.Location = new System.Drawing.Point(16, 87);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(676, 362);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Verified Result";
            // 
            // lblMsg
            // 
            this.lblMsg.AutoSize = true;
            this.lblMsg.ForeColor = System.Drawing.Color.Red;
            this.lblMsg.Location = new System.Drawing.Point(327, 25);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(0, 13);
            this.lblMsg.TabIndex = 15;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(775, 483);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.btnVerify);
            this.Controls.Add(this.txtDestinationHeaderNumber);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtSourceHeaderNumber);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnDestinationOpen);
            this.Controls.Add(this.btnSourceOpen);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtDestinationFile);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtSourceFile);
            this.Name = "MainForm";
            this.Text = "Verification";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtSourceFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDestinationFile;
        private System.Windows.Forms.Button btnSourceOpen;
        private System.Windows.Forms.Button btnDestinationOpen;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtSourceHeaderNumber;
        private System.Windows.Forms.TextBox txtDestinationHeaderNumber;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnVerify;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblMsg;
    }
}

