using STO.Print.UserControl;

namespace STO.Print
{
    partial class FrmProcessBar
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.circularProgressBarEx4 = new STO.Print.UserControl.CircularProgressBarEx();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(47, 233);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(87, 27);
            this.button1.TabIndex = 1;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // circularProgressBarEx4
            // 
            this.circularProgressBarEx4.BackColor = System.Drawing.Color.White;
            this.circularProgressBarEx4.Color = System.Drawing.Color.DeepSkyBlue;
            this.circularProgressBarEx4.CompleteText = "完成";
            this.circularProgressBarEx4.DoingText = "正在加载";
            this.circularProgressBarEx4.Location = new System.Drawing.Point(8, 14);
            this.circularProgressBarEx4.Name = "circularProgressBarEx4";
            this.circularProgressBarEx4.Size = new System.Drawing.Size(162, 197);
            this.circularProgressBarEx4.TabIndex = 13;
            this.circularProgressBarEx4.Text = "circularProgressBarEx4";
            this.circularProgressBarEx4.Value = 0;
            // 
            // FrmProcessBar
            // 
            this.Appearance.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Appearance.Options.UseForeColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(175, 276);
            this.Controls.Add(this.circularProgressBarEx4);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmProcessBar";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "加载";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private CircularProgressBarEx circularProgressBarEx4;
    }
}

