namespace STO.Print.UserControl
{
    partial class BindZtoElecUserInfo
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
            this.btnGetElecCount = new DevExpress.XtraEditors.SimpleButton();
            this.lblBindElecUserInfo = new System.Windows.Forms.LinkLabel();
            this.ckProclaimText = new DevExpress.XtraEditors.CheckEdit();
            this.txtCustomerPassword = new DevExpress.XtraEditors.ButtonEdit();
            this.lblCustomerPassword = new DevExpress.XtraEditors.LabelControl();
            this.txtCustomerID = new DevExpress.XtraEditors.TextEdit();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.txtSiteCode = new DevExpress.XtraEditors.ButtonEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtSiteName = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.lblCustomerID = new DevExpress.XtraEditors.LabelControl();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.ckProclaimText.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCustomerPassword.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCustomerID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSiteCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSiteName.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnGetElecCount
            // 
            this.btnGetElecCount.Location = new System.Drawing.Point(245, 215);
            this.btnGetElecCount.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnGetElecCount.Name = "btnGetElecCount";
            this.btnGetElecCount.Size = new System.Drawing.Size(132, 33);
            this.btnGetElecCount.TabIndex = 22;
            this.btnGetElecCount.Text = "查询剩余电子面单数量";
            this.btnGetElecCount.Click += new System.EventHandler(this.btnGetElecCount_Click);
            // 
            // lblBindElecUserInfo
            // 
            this.lblBindElecUserInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lblBindElecUserInfo.AutoSize = true;
            this.lblBindElecUserInfo.BackColor = System.Drawing.Color.Transparent;
            this.lblBindElecUserInfo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBindElecUserInfo.Location = new System.Drawing.Point(328, 3);
            this.lblBindElecUserInfo.Name = "lblBindElecUserInfo";
            this.lblBindElecUserInfo.Size = new System.Drawing.Size(138, 14);
            this.lblBindElecUserInfo.TabIndex = 21;
            this.lblBindElecUserInfo.TabStop = true;
            this.lblBindElecUserInfo.Text = "如何获取商家ID和密码";
            this.lblBindElecUserInfo.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblBindElecUserInfo_LinkClicked);
            // 
            // ckProclaimText
            // 
            this.ckProclaimText.Location = new System.Drawing.Point(111, 220);
            this.ckProclaimText.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ckProclaimText.Name = "ckProclaimText";
            this.ckProclaimText.Properties.Caption = "明文显示密码";
            this.ckProclaimText.Size = new System.Drawing.Size(98, 19);
            this.ckProclaimText.TabIndex = 20;
            this.ckProclaimText.CheckedChanged += new System.EventHandler(this.ckProclaimText_CheckedChanged);
            // 
            // txtCustomerPassword
            // 
            this.txtCustomerPassword.Location = new System.Drawing.Point(111, 90);
            this.txtCustomerPassword.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtCustomerPassword.Name = "txtCustomerPassword";
            this.txtCustomerPassword.Properties.Appearance.BackColor = System.Drawing.Color.LemonChiffon;
            this.txtCustomerPassword.Properties.Appearance.Options.UseBackColor = true;
            this.txtCustomerPassword.Properties.NullValuePrompt = "申通电子面单线下商家密码";
            this.txtCustomerPassword.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtCustomerPassword.Properties.UseSystemPasswordChar = true;
            this.txtCustomerPassword.Size = new System.Drawing.Size(357, 20);
            this.txtCustomerPassword.TabIndex = 19;
            // 
            // lblCustomerPassword
            // 
            this.lblCustomerPassword.Location = new System.Drawing.Point(13, 91);
            this.lblCustomerPassword.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lblCustomerPassword.Name = "lblCustomerPassword";
            this.lblCustomerPassword.Size = new System.Drawing.Size(88, 14);
            this.lblCustomerPassword.TabIndex = 18;
            this.lblCustomerPassword.Text = "申通商家ID密码:";
            // 
            // txtCustomerID
            // 
            this.txtCustomerID.Location = new System.Drawing.Point(111, 49);
            this.txtCustomerID.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtCustomerID.Name = "txtCustomerID";
            this.txtCustomerID.Properties.Appearance.BackColor = System.Drawing.Color.LemonChiffon;
            this.txtCustomerID.Properties.Appearance.Options.UseBackColor = true;
            this.txtCustomerID.Properties.NullValuePrompt = "申通电子面单线下商家ID";
            this.txtCustomerID.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtCustomerID.Size = new System.Drawing.Size(357, 20);
            this.txtCustomerID.TabIndex = 17;
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.txtSiteCode);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Controls.Add(this.txtSiteName);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.btnGetElecCount);
            this.groupControl1.Controls.Add(this.lblBindElecUserInfo);
            this.groupControl1.Controls.Add(this.ckProclaimText);
            this.groupControl1.Controls.Add(this.txtCustomerPassword);
            this.groupControl1.Controls.Add(this.lblCustomerPassword);
            this.groupControl1.Controls.Add(this.txtCustomerID);
            this.groupControl1.Controls.Add(this.lblCustomerID);
            this.groupControl1.Controls.Add(this.btnSave);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(501, 282);
            this.groupControl1.TabIndex = 13;
            this.groupControl1.Text = "绑定商家ID";
            // 
            // txtSiteCode
            // 
            this.txtSiteCode.Location = new System.Drawing.Point(111, 169);
            this.txtSiteCode.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtSiteCode.Name = "txtSiteCode";
            this.txtSiteCode.Properties.Appearance.BackColor = System.Drawing.Color.LemonChiffon;
            this.txtSiteCode.Properties.Appearance.Options.UseBackColor = true;
            this.txtSiteCode.Properties.NullValuePrompt = "所属网点编号必填";
            this.txtSiteCode.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtSiteCode.Size = new System.Drawing.Size(357, 20);
            this.txtSiteCode.TabIndex = 26;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(25, 171);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(76, 14);
            this.labelControl1.TabIndex = 25;
            this.labelControl1.Text = "所属网点编号:";
            // 
            // txtSiteName
            // 
            this.txtSiteName.Location = new System.Drawing.Point(111, 128);
            this.txtSiteName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtSiteName.Name = "txtSiteName";
            this.txtSiteName.Properties.Appearance.BackColor = System.Drawing.Color.LemonChiffon;
            this.txtSiteName.Properties.Appearance.Options.UseBackColor = true;
            this.txtSiteName.Properties.NullValuePrompt = "所属网点名称必填";
            this.txtSiteName.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtSiteName.Size = new System.Drawing.Size(357, 20);
            this.txtSiteName.TabIndex = 24;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(25, 131);
            this.labelControl2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(76, 14);
            this.labelControl2.TabIndex = 23;
            this.labelControl2.Text = "所属网点名称:";
            // 
            // lblCustomerID
            // 
            this.lblCustomerID.Location = new System.Drawing.Point(37, 51);
            this.lblCustomerID.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lblCustomerID.Name = "lblCustomerID";
            this.lblCustomerID.Size = new System.Drawing.Size(64, 14);
            this.lblCustomerID.TabIndex = 16;
            this.lblCustomerID.Text = "申通商家ID:";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(383, 215);
            this.btnSave.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(87, 33);
            this.btnSave.TabIndex = 14;
            this.btnSave.Text = "确定";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // BindZtoElecUserInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupControl1);
            this.Name = "BindZtoElecUserInfo";
            this.Size = new System.Drawing.Size(501, 282);
            ((System.ComponentModel.ISupportInitialize)(this.ckProclaimText.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCustomerPassword.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCustomerID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSiteCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSiteName.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnGetElecCount;
        private System.Windows.Forms.LinkLabel lblBindElecUserInfo;
        private DevExpress.XtraEditors.CheckEdit ckProclaimText;
        private DevExpress.XtraEditors.ButtonEdit txtCustomerPassword;
        private DevExpress.XtraEditors.LabelControl lblCustomerPassword;
        private DevExpress.XtraEditors.TextEdit txtCustomerID;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.LabelControl lblCustomerID;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.ButtonEdit txtSiteCode;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtSiteName;
        private DevExpress.XtraEditors.LabelControl labelControl2;

    }
}
