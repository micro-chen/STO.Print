namespace STO.Print
{
    partial class FrmReceiveManData
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
            this.receiveManControl1 = new STO.Print.UserControl.ReceiveManControl();
            this.SuspendLayout();
            // 
            // receiveManControl1
            // 
            this.receiveManControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.receiveManControl1.Location = new System.Drawing.Point(0, 0);
            this.receiveManControl1.Name = "receiveManControl1";
            this.receiveManControl1.Size = new System.Drawing.Size(1037, 423);
            this.receiveManControl1.TabIndex = 0;
            // 
            // FrmReceiveManData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1037, 423);
            this.Controls.Add(this.receiveManControl1);
            this.Name = "FrmReceiveManData";
            this.Text = "常用收件人";
            this.ResumeLayout(false);

        }

        #endregion

        private UserControl.ReceiveManControl receiveManControl1;
    }
}