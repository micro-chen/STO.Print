using STO.Print.UserControl;

namespace STO.Print.AddBillForm
{
    partial class FrmAddZto180ElecBill
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
            this.myEnterBillControl1 = new STO.Print.UserControl.MyEnterBillControl();
            this.SuspendLayout();
            // 
            // myEnterBillControl1
            // 
            this.myEnterBillControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.myEnterBillControl1.Location = new System.Drawing.Point(0, 0);
            this.myEnterBillControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.myEnterBillControl1.Name = "myEnterBillControl1";
            this.myEnterBillControl1.PrintBillId = null;
            this.myEnterBillControl1.Size = new System.Drawing.Size(837, 291);
            this.myEnterBillControl1.TabIndex = 0;
            // 
            // FrmAddZto180ElecBill
            // 
            this.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(837, 291);
            this.Controls.Add(this.myEnterBillControl1);
            this.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.MaximizeBox = false;
            this.Name = "FrmAddZto180ElecBill";
            this.Text = "新增申通普通电子面单";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmAddZto180ElecBill_FormClosing);
            this.Load += new System.EventHandler(this.FrmAddZto180ElecBill_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private MyEnterBillControl myEnterBillControl1;




    }
}