namespace STO.Print
{
    partial class FrmChooseReceiveMan
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmChooseReceiveMan));
            this.receiveManControl1 = new STO.Print.UserControl.ReceiveManControl();
            this.SuspendLayout();
            // 
            // receiveManControl1
            // 
            this.receiveManControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.receiveManControl1.Location = new System.Drawing.Point(0, 0);
            this.receiveManControl1.Name = "receiveManControl1";
            this.receiveManControl1.Size = new System.Drawing.Size(1040, 521);
            this.receiveManControl1.TabIndex = 0;
            // 
            // FrmChooseReceiveMan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1040, 521);
            this.Controls.Add(this.receiveManControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmChooseReceiveMan";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "选择收件人";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmChooseReceiveManFormClosed);
            this.ResumeLayout(false);

        }

        #endregion

        public UserControl.ReceiveManControl receiveManControl1;

    }
}