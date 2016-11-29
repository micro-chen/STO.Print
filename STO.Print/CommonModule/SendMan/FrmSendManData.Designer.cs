namespace STO.Print
{
    partial class FrmSendManData
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
            this.sendManControl1 = new STO.Print.UserControl.SendManControl();
            this.SuspendLayout();
            // 
            // sendManControl1
            // 
            this.sendManControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sendManControl1.Location = new System.Drawing.Point(0, 0);
            this.sendManControl1.Name = "sendManControl1";
            this.sendManControl1.Size = new System.Drawing.Size(816, 465);
            this.sendManControl1.TabIndex = 0;
            // 
            // FrmSendManData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(816, 465);
            this.Controls.Add(this.sendManControl1);
            this.Name = "FrmSendManData";
            this.Text = "常用发件人";
            this.ResumeLayout(false);

        }

        #endregion

        private UserControl.SendManControl sendManControl1;
    }
}