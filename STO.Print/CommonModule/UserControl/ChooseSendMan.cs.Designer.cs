namespace STO.Print.UserControl
{
    partial class ChooseSendMan
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.ckChooseSendMan = new DevExpress.XtraEditors.CheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.ckChooseSendMan.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // ckChooseSendMan
            // 
            this.ckChooseSendMan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ckChooseSendMan.Location = new System.Drawing.Point(0, 0);
            this.ckChooseSendMan.Name = "ckChooseSendMan";
            this.ckChooseSendMan.Properties.Caption = "选择发件人";
            this.ckChooseSendMan.Size = new System.Drawing.Size(83, 19);
            this.ckChooseSendMan.TabIndex = 0;
            this.ckChooseSendMan.CheckedChanged += new System.EventHandler(this.CkChooseSendManCheckedChanged);
            // 
            // ChooseSendMan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ckChooseSendMan);
            this.Name = "ChooseSendMan";
            this.Size = new System.Drawing.Size(83, 20);
            ((System.ComponentModel.ISupportInitialize)(this.ckChooseSendMan.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.CheckEdit ckChooseSendMan;
    }
}
