namespace STO.Print.UserControl
{
    partial class ChooseReceiveMan
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
            this.ckChooseReceiveMan = new DevExpress.XtraEditors.CheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.ckChooseReceiveMan.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // ckChooseReceiveMan
            // 
            this.ckChooseReceiveMan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ckChooseReceiveMan.Location = new System.Drawing.Point(0, 0);
            this.ckChooseReceiveMan.Name = "ckChooseReceiveMan";
            this.ckChooseReceiveMan.Properties.Caption = "选择收件人";
            this.ckChooseReceiveMan.Size = new System.Drawing.Size(83, 19);
            this.ckChooseReceiveMan.TabIndex = 0;
            this.ckChooseReceiveMan.CheckedChanged += new System.EventHandler(this.CkChooseReceiveManCheckedChanged);
            // 
            // ChooseReceiveMan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ckChooseReceiveMan);
            this.Name = "ChooseReceiveMan";
            this.Size = new System.Drawing.Size(83, 20);
            ((System.ComponentModel.ISupportInitialize)(this.ckChooseReceiveMan.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.CheckEdit ckChooseReceiveMan;
    }
}
