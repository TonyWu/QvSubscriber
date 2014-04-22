namespace QlikView.Configuration
{
    partial class FilterView
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
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtDesc = new System.Windows.Forms.TextBox();
            this.comboBoxConnection = new System.Windows.Forms.ComboBox();
            this.btnFieldAdd = new System.Windows.Forms.Button();
            this.btnFieldRemove = new System.Windows.Forms.Button();
            this.dataGridViewFields = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnFilterPreview = new System.Windows.Forms.Button();
            this.btnVariableRemove = new System.Windows.Forms.Button();
            this.btnVariableAdd = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.dataGridViewVariables = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFields)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewVariables)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(42, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Description";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 96);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Connection";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(43, 139);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Fields";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(89, 19);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(210, 20);
            this.txtName.TabIndex = 4;
            // 
            // txtDesc
            // 
            this.txtDesc.Location = new System.Drawing.Point(89, 57);
            this.txtDesc.Name = "txtDesc";
            this.txtDesc.Size = new System.Drawing.Size(210, 20);
            this.txtDesc.TabIndex = 5;
            // 
            // comboBoxConnection
            // 
            this.comboBoxConnection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxConnection.FormattingEnabled = true;
            this.comboBoxConnection.Location = new System.Drawing.Point(89, 96);
            this.comboBoxConnection.Name = "comboBoxConnection";
            this.comboBoxConnection.Size = new System.Drawing.Size(210, 21);
            this.comboBoxConnection.TabIndex = 6;
            // 
            // btnFieldAdd
            // 
            this.btnFieldAdd.Location = new System.Drawing.Point(89, 132);
            this.btnFieldAdd.Name = "btnFieldAdd";
            this.btnFieldAdd.Size = new System.Drawing.Size(52, 23);
            this.btnFieldAdd.TabIndex = 7;
            this.btnFieldAdd.Text = "Add";
            this.btnFieldAdd.UseVisualStyleBackColor = true;
            this.btnFieldAdd.Click += new System.EventHandler(this.btnFieldAdd_Click);
            // 
            // btnFieldRemove
            // 
            this.btnFieldRemove.Location = new System.Drawing.Point(159, 132);
            this.btnFieldRemove.Name = "btnFieldRemove";
            this.btnFieldRemove.Size = new System.Drawing.Size(60, 23);
            this.btnFieldRemove.TabIndex = 8;
            this.btnFieldRemove.Text = "Remove";
            this.btnFieldRemove.UseVisualStyleBackColor = true;
            this.btnFieldRemove.Click += new System.EventHandler(this.btnFieldRemove_Click);
            // 
            // dataGridViewFields
            // 
            this.dataGridViewFields.AllowUserToAddRows = false;
            this.dataGridViewFields.AllowUserToDeleteRows = false;
            this.dataGridViewFields.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewFields.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewFields.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1});
            this.dataGridViewFields.Location = new System.Drawing.Point(89, 161);
            this.dataGridViewFields.Name = "dataGridViewFields";
            this.dataGridViewFields.ReadOnly = true;
            this.dataGridViewFields.Size = new System.Drawing.Size(355, 152);
            this.dataGridViewFields.TabIndex = 9;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Name";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Visible = false;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(268, 469);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 10;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(267, 470);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 23);
            this.btnUpdate.TabIndex = 11;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(369, 471);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 12;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnFilterPreview
            // 
            this.btnFilterPreview.Location = new System.Drawing.Point(242, 132);
            this.btnFilterPreview.Name = "btnFilterPreview";
            this.btnFilterPreview.Size = new System.Drawing.Size(101, 23);
            this.btnFilterPreview.TabIndex = 13;
            this.btnFilterPreview.Text = "Filter Preview";
            this.btnFilterPreview.UseVisualStyleBackColor = true;
            this.btnFilterPreview.Click += new System.EventHandler(this.btnFilterPreview_Click);
            // 
            // btnVariableRemove
            // 
            this.btnVariableRemove.Location = new System.Drawing.Point(159, 334);
            this.btnVariableRemove.Name = "btnVariableRemove";
            this.btnVariableRemove.Size = new System.Drawing.Size(60, 23);
            this.btnVariableRemove.TabIndex = 16;
            this.btnVariableRemove.Text = "Remove";
            this.btnVariableRemove.UseVisualStyleBackColor = true;
            this.btnVariableRemove.Click += new System.EventHandler(this.btnVariableRemove_Click);
            // 
            // btnVariableAdd
            // 
            this.btnVariableAdd.Location = new System.Drawing.Point(89, 334);
            this.btnVariableAdd.Name = "btnVariableAdd";
            this.btnVariableAdd.Size = new System.Drawing.Size(52, 23);
            this.btnVariableAdd.TabIndex = 15;
            this.btnVariableAdd.Text = "Add";
            this.btnVariableAdd.UseVisualStyleBackColor = true;
            this.btnVariableAdd.Click += new System.EventHandler(this.btnVariableAdd_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(29, 341);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(50, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "Variables";
            // 
            // dataGridViewVariables
            // 
            this.dataGridViewVariables.AllowUserToAddRows = false;
            this.dataGridViewVariables.AllowUserToDeleteRows = false;
            this.dataGridViewVariables.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewVariables.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewVariables.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn2});
            this.dataGridViewVariables.Location = new System.Drawing.Point(89, 367);
            this.dataGridViewVariables.Name = "dataGridViewVariables";
            this.dataGridViewVariables.ReadOnly = true;
            this.dataGridViewVariables.Size = new System.Drawing.Size(355, 92);
            this.dataGridViewVariables.TabIndex = 17;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Name";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Visible = false;
            // 
            // FilterView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(485, 503);
            this.Controls.Add(this.dataGridViewVariables);
            this.Controls.Add(this.btnVariableRemove);
            this.Controls.Add(this.btnVariableAdd);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnFilterPreview);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.dataGridViewFields);
            this.Controls.Add(this.btnFieldRemove);
            this.Controls.Add(this.btnFieldAdd);
            this.Controls.Add(this.comboBoxConnection);
            this.Controls.Add(this.txtDesc);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "FilterView";
            this.Text = "FilterView";
            this.Load += new System.EventHandler(this.FilterView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFields)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewVariables)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtDesc;
        private System.Windows.Forms.ComboBox comboBoxConnection;
        private System.Windows.Forms.Button btnFieldAdd;
        private System.Windows.Forms.Button btnFieldRemove;
        private System.Windows.Forms.DataGridView dataGridViewFields;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnFilterPreview;
        private System.Windows.Forms.Button btnVariableRemove;
        private System.Windows.Forms.Button btnVariableAdd;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView dataGridViewVariables;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
    }
}