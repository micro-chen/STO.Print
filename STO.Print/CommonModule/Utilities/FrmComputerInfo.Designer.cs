namespace STO.Print
{
    partial class FrmComputerInfo
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
            this.txtComputerInfo = new DevExpress.XtraEditors.MemoEdit();
            ((System.ComponentModel.ISupportInitialize)(this.txtComputerInfo.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // txtComputerInfo
            // 
            this.txtComputerInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtComputerInfo.Location = new System.Drawing.Point(0, 0);
            this.txtComputerInfo.Name = "txtComputerInfo";
            this.txtComputerInfo.Size = new System.Drawing.Size(414, 353);
            this.txtComputerInfo.TabIndex = 0;
            // 
            // FrmComputerInfo
            // 
            this.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(414, 353);
            this.Controls.Add(this.txtComputerInfo);
            this.Name = "FrmComputerInfo";
            this.Text = "电脑配置信息";
            this.Load += new System.EventHandler(this.FrmComputerInfo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtComputerInfo.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.MemoEdit txtComputerInfo;



    }
}