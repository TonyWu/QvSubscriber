namespace QlikView.Connector
{
    partial class QvConnectorComponent
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QvConnectorComponent));
            this.axQlikOCX1 = new AxQlikOCXLib.AxQlikOCX();
            ((System.ComponentModel.ISupportInitialize)(this.axQlikOCX1)).BeginInit();
            this.SuspendLayout();
            // 
            // axQlikOCX1
            // 
            this.axQlikOCX1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axQlikOCX1.Enabled = true;
            this.axQlikOCX1.Location = new System.Drawing.Point(0, 0);
            this.axQlikOCX1.Name = "axQlikOCX1";
            this.axQlikOCX1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axQlikOCX1.OcxState")));
            this.axQlikOCX1.Size = new System.Drawing.Size(938, 746);
            this.axQlikOCX1.TabIndex = 1;
            // 
            // QvConnectorComponent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.axQlikOCX1);
            this.Name = "QvConnectorComponent";
            this.Size = new System.Drawing.Size(938, 746);
            ((System.ComponentModel.ISupportInitialize)(this.axQlikOCX1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private AxQlikOCXLib.AxQlikOCX axQlikOCX1;
    }
}
