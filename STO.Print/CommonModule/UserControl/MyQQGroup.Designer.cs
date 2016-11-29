namespace STO.Print.UserControl
{
    partial class MyQQGroup
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
            this.btnQQGroup1 = new DevExpress.XtraEditors.SimpleButton();
            this.btnGroup2 = new DevExpress.XtraEditors.SimpleButton();
            this.btnGroup3 = new DevExpress.XtraEditors.SimpleButton();
            this.SuspendLayout();
            // 
            // btnQQGroup1
            // 
            this.btnQQGroup1.Location = new System.Drawing.Point(3, 3);
            this.btnQQGroup1.Name = "btnQQGroup1";
            this.btnQQGroup1.Size = new System.Drawing.Size(71, 29);
            this.btnQQGroup1.TabIndex = 0;
            this.btnQQGroup1.Text = "QQ群1";
            this.btnQQGroup1.Click += new System.EventHandler(this.btnQQGroup1_Click);
            // 
            // btnGroup2
            // 
            this.btnGroup2.Location = new System.Drawing.Point(80, 3);
            this.btnGroup2.Name = "btnGroup2";
            this.btnGroup2.Size = new System.Drawing.Size(71, 29);
            this.btnGroup2.TabIndex = 1;
            this.btnGroup2.Text = "QQ群2";
            this.btnGroup2.Click += new System.EventHandler(this.btnGroup2_Click);
            // 
            // btnGroup3
            // 
            this.btnGroup3.Location = new System.Drawing.Point(157, 3);
            this.btnGroup3.Name = "btnGroup3";
            this.btnGroup3.Size = new System.Drawing.Size(71, 29);
            this.btnGroup3.TabIndex = 2;
            this.btnGroup3.Text = "QQ群3";
            this.btnGroup3.Click += new System.EventHandler(this.btnGroup3_Click);
            // 
            // MyQQGroup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnGroup3);
            this.Controls.Add(this.btnGroup2);
            this.Controls.Add(this.btnQQGroup1);
            this.Name = "MyQQGroup";
            this.Size = new System.Drawing.Size(233, 36);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnQQGroup1;
        private DevExpress.XtraEditors.SimpleButton btnGroup2;
        private DevExpress.XtraEditors.SimpleButton btnGroup3;
    }
}
