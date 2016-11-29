namespace ZTO.Print
{
    partial class FrmMessageRemind
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

        #region Windows 窗体设计器生成的主键

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用主键编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.webMessage = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // webMessage
            // 
            this.webMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webMessage.Location = new System.Drawing.Point(0, 0);
            this.webMessage.MinimumSize = new System.Drawing.Size(23, 23);
            this.webMessage.Name = "webMessage";
            this.webMessage.Size = new System.Drawing.Size(331, 218);
            this.webMessage.TabIndex = 0;
            this.webMessage.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBMsg_DocumentCompleted);
            // 
            // FrmMessageRemind
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(331, 218);
            this.Controls.Add(this.webMessage);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmMessageRemind";
            this.Text = "消息提醒";
            this.TopMost = true;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser webMessage;
    }
}