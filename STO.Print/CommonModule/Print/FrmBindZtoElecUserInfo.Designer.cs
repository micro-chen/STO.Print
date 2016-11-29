namespace STO.Print
{
    partial class FrmBindZtoElecUserInfo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmBindZtoElecUserInfo));
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.txtQQNumber = new DevExpress.XtraEditors.ButtonEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.btnGetElecCount = new DevExpress.XtraEditors.SimpleButton();
            this.lblBindElecUserInfo = new System.Windows.Forms.LinkLabel();
            this.ckProclaimText = new DevExpress.XtraEditors.CheckEdit();
            this.txtCustomerPassword = new DevExpress.XtraEditors.ButtonEdit();
            this.lblCustomerPassword = new DevExpress.XtraEditors.LabelControl();
            this.txtCustomerID = new DevExpress.XtraEditors.TextEdit();
            this.lblCustomerID = new DevExpress.XtraEditors.LabelControl();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.txtSiteCode = new DevExpress.XtraEditors.ButtonEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtSiteName = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtQQNumber.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckProclaimText.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCustomerPassword.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCustomerID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSiteCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSiteName.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.txtSiteCode);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.txtSiteName);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.txtQQNumber);
            this.groupControl1.Controls.Add(this.labelControl1);
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
            this.groupControl1.Size = new System.Drawing.Size(486, 276);
            this.groupControl1.TabIndex = 12;
            this.groupControl1.Text = "绑定商家ID";
            // 
            // txtQQNumber
            // 
            this.txtQQNumber.Location = new System.Drawing.Point(116, 201);
            this.txtQQNumber.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtQQNumber.Name = "txtQQNumber";
            this.txtQQNumber.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.txtQQNumber.Properties.Appearance.Options.UseBackColor = true;
            this.txtQQNumber.Properties.NullValuePrompt = "用于接收商家ID和密码邮件，非必填项";
            this.txtQQNumber.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtQQNumber.Size = new System.Drawing.Size(357, 20);
            this.txtQQNumber.TabIndex = 24;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(62, 203);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(46, 14);
            this.labelControl1.TabIndex = 23;
            this.labelControl1.Text = "QQ号码:";
            // 
            // btnGetElecCount
            // 
            this.btnGetElecCount.Location = new System.Drawing.Point(245, 238);
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
            this.lblBindElecUserInfo.Location = new System.Drawing.Point(330, 4);
            this.lblBindElecUserInfo.Name = "lblBindElecUserInfo";
            this.lblBindElecUserInfo.Size = new System.Drawing.Size(138, 14);
            this.lblBindElecUserInfo.TabIndex = 21;
            this.lblBindElecUserInfo.TabStop = true;
            this.lblBindElecUserInfo.Text = "如何获取商家ID和密码";
            this.lblBindElecUserInfo.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblBindElecUserInfo_LinkClicked);
            // 
            // ckProclaimText
            // 
            this.ckProclaimText.Location = new System.Drawing.Point(113, 245);
            this.ckProclaimText.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ckProclaimText.Name = "ckProclaimText";
            this.ckProclaimText.Properties.Caption = "明文显示密码";
            this.ckProclaimText.Size = new System.Drawing.Size(98, 19);
            this.ckProclaimText.TabIndex = 20;
            this.ckProclaimText.CheckedChanged += new System.EventHandler(this.ckProclaimText_CheckedChanged);
            // 
            // txtCustomerPassword
            // 
            this.txtCustomerPassword.Location = new System.Drawing.Point(116, 87);
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
            this.lblCustomerPassword.Location = new System.Drawing.Point(12, 93);
            this.lblCustomerPassword.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lblCustomerPassword.Name = "lblCustomerPassword";
            this.lblCustomerPassword.Size = new System.Drawing.Size(96, 14);
            this.lblCustomerPassword.TabIndex = 18;
            this.lblCustomerPassword.Text = "申通商家ID密码：";
            // 
            // txtCustomerID
            // 
            this.txtCustomerID.Location = new System.Drawing.Point(116, 49);
            this.txtCustomerID.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtCustomerID.Name = "txtCustomerID";
            this.txtCustomerID.Properties.Appearance.BackColor = System.Drawing.Color.LemonChiffon;
            this.txtCustomerID.Properties.Appearance.Options.UseBackColor = true;
            this.txtCustomerID.Properties.NullValuePrompt = "申通电子面单线下商家ID";
            this.txtCustomerID.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtCustomerID.Size = new System.Drawing.Size(357, 20);
            this.txtCustomerID.TabIndex = 17;
            // 
            // lblCustomerID
            // 
            this.lblCustomerID.Location = new System.Drawing.Point(44, 51);
            this.lblCustomerID.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lblCustomerID.Name = "lblCustomerID";
            this.lblCustomerID.Size = new System.Drawing.Size(64, 14);
            this.lblCustomerID.TabIndex = 16;
            this.lblCustomerID.Text = "申通商家ID:";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(383, 238);
            this.btnSave.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(87, 33);
            this.btnSave.TabIndex = 14;
            this.btnSave.Text = "保存并关闭";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtSiteCode
            // 
            this.txtSiteCode.Location = new System.Drawing.Point(116, 163);
            this.txtSiteCode.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtSiteCode.Name = "txtSiteCode";
            this.txtSiteCode.Properties.Appearance.BackColor = System.Drawing.Color.LemonChiffon;
            this.txtSiteCode.Properties.Appearance.Options.UseBackColor = true;
            this.txtSiteCode.Properties.NullValuePrompt = "所属网点编号必填";
            this.txtSiteCode.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtSiteCode.Size = new System.Drawing.Size(357, 20);
            this.txtSiteCode.TabIndex = 30;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(32, 164);
            this.labelControl2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(76, 14);
            this.labelControl2.TabIndex = 29;
            this.labelControl2.Text = "所属网点编号:";
            // 
            // txtSiteName
            // 
            this.txtSiteName.Location = new System.Drawing.Point(116, 125);
            this.txtSiteName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtSiteName.Name = "txtSiteName";
            this.txtSiteName.Properties.Appearance.BackColor = System.Drawing.Color.LemonChiffon;
            this.txtSiteName.Properties.Appearance.Options.UseBackColor = true;
            this.txtSiteName.Properties.NullValuePrompt = "所属网点名称必填";
            this.txtSiteName.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtSiteName.Size = new System.Drawing.Size(357, 20);
            this.txtSiteName.TabIndex = 28;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(32, 124);
            this.labelControl3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(76, 14);
            this.labelControl3.TabIndex = 27;
            this.labelControl3.Text = "所属网点名称:";
            // 
            // FrmBindZtoElecUserInfo
            // 
            this.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(486, 276);
            this.Controls.Add(this.groupControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmBindZtoElecUserInfo";
            this.Text = "绑定申通电子面单商家ID";
            this.Load += new System.EventHandler(this.FrmBindZtoElecUserInfo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtQQNumber.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckProclaimText.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCustomerPassword.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCustomerID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSiteCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSiteName.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.CheckEdit ckProclaimText;
        private DevExpress.XtraEditors.ButtonEdit txtCustomerPassword;
        private DevExpress.XtraEditors.LabelControl lblCustomerPassword;
        private DevExpress.XtraEditors.TextEdit txtCustomerID;
        private DevExpress.XtraEditors.LabelControl lblCustomerID;
        private System.Windows.Forms.LinkLabel lblBindElecUserInfo;
        private DevExpress.XtraEditors.SimpleButton btnGetElecCount;
        private DevExpress.XtraEditors.ButtonEdit txtQQNumber;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.ButtonEdit txtSiteCode;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit txtSiteName;
        private DevExpress.XtraEditors.LabelControl labelControl3;
    }
}