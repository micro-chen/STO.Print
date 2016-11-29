namespace STO.Print
{
    partial class FrmInputStartBillAndEndBill
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
            this.myInputStartBillAndEndBill1 = new STO.Print.UserControl.MyInputStartBillAndEndBill();
            this.SuspendLayout();
            // 
            // myInputStartBillAndEndBill1
            // 
            this.myInputStartBillAndEndBill1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.myInputStartBillAndEndBill1.Location = new System.Drawing.Point(0, 0);
            this.myInputStartBillAndEndBill1.Name = "myInputStartBillAndEndBill1";
            this.myInputStartBillAndEndBill1.PrintBillEntities = null;
            this.myInputStartBillAndEndBill1.Size = new System.Drawing.Size(514, 464);
            this.myInputStartBillAndEndBill1.TabIndex = 0;
            // 
            // FrmInputStartBillAndEndBill
            // 
            this.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(514, 464);
            this.Controls.Add(this.myInputStartBillAndEndBill1);
            this.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.Name = "FrmInputStartBillAndEndBill";
            this.Text = "补填单号";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmInputStartBillAndEndBill_FormClosing);
            this.Load += new System.EventHandler(this.FrmInputStartBillAndEndBill_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private UserControl.MyInputStartBillAndEndBill myInputStartBillAndEndBill1;
    }
}