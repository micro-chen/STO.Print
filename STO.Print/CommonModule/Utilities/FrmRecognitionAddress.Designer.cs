namespace STO.Print
{
    partial class FrmRecognitionAddress
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRecognitionAddress));
            this.grpContent = new DevExpress.XtraEditors.GroupControl();
            this.ckAutoPasteAndRecognition = new DevExpress.XtraEditors.CheckEdit();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.txtContent = new DevExpress.XtraEditors.MemoEdit();
            this.btnPasteAndRecognition = new DevExpress.XtraEditors.SimpleButton();
            this.btnRecognition = new DevExpress.XtraEditors.SimpleButton();
            this.btnDemoContent = new DevExpress.XtraEditors.SimpleButton();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.btnSaveAsReceiveMan = new DevExpress.XtraEditors.SimpleButton();
            this.btnSaveAsSendMan = new DevExpress.XtraEditors.SimpleButton();
            this.grpResult = new DevExpress.XtraEditors.GroupControl();
            this.txtPostCode = new DevExpress.XtraEditors.TextEdit();
            this.labelControl10 = new DevExpress.XtraEditors.LabelControl();
            this.txtAddress = new DevExpress.XtraEditors.TextEdit();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.txtCounty = new DevExpress.XtraEditors.TextEdit();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.txtCity = new DevExpress.XtraEditors.TextEdit();
            this.txtProvince = new DevExpress.XtraEditors.TextEdit();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.txtMobile = new DevExpress.XtraEditors.TextEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.txtTelephone = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.txtRealName = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtCompany = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.tspReceive = new DevExpress.Utils.ToolTipController(this.components);
            this.btnPasteReceiveMan = new DevExpress.XtraEditors.SimpleButton();
            this.btnPasteSendMan = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.grpContent)).BeginInit();
            this.grpContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ckAutoPasteAndRecognition.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtContent.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpResult)).BeginInit();
            this.grpResult.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPostCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAddress.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCounty.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCity.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtProvince.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMobile.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTelephone.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRealName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCompany.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // grpContent
            // 
            this.grpContent.Controls.Add(this.ckAutoPasteAndRecognition);
            this.grpContent.Controls.Add(this.panelControl1);
            this.grpContent.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpContent.Location = new System.Drawing.Point(0, 0);
            this.grpContent.Name = "grpContent";
            this.grpContent.Size = new System.Drawing.Size(720, 245);
            this.grpContent.TabIndex = 0;
            this.grpContent.Text = "需要识别的内容";
            // 
            // ckAutoPasteAndRecognition
            // 
            this.ckAutoPasteAndRecognition.Location = new System.Drawing.Point(438, 1);
            this.ckAutoPasteAndRecognition.Name = "ckAutoPasteAndRecognition";
            this.ckAutoPasteAndRecognition.Properties.Caption = "打开本界面时自动粘贴剪贴板并识别地址";
            this.ckAutoPasteAndRecognition.Size = new System.Drawing.Size(280, 19);
            this.ckAutoPasteAndRecognition.TabIndex = 0;
            this.ckAutoPasteAndRecognition.CheckedChanged += new System.EventHandler(this.ckAutoPasteAndRecognition_CheckedChanged);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.txtContent);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(2, 22);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(716, 221);
            this.panelControl1.TabIndex = 0;
            // 
            // txtContent
            // 
            this.txtContent.AllowDrop = true;
            this.txtContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtContent.EditValue = "";
            this.txtContent.Location = new System.Drawing.Point(2, 2);
            this.txtContent.Name = "txtContent";
            this.txtContent.Properties.Appearance.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtContent.Properties.Appearance.Options.UseFont = true;
            this.txtContent.Properties.MaxLength = 100;
            this.txtContent.Properties.NullValuePrompt = "淘宝收件人/拍拍收件人信息";
            this.txtContent.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtContent.Properties.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtContent.Size = new System.Drawing.Size(712, 217);
            this.txtContent.TabIndex = 10;
            // 
            // btnPasteAndRecognition
            // 
            this.btnPasteAndRecognition.Location = new System.Drawing.Point(14, 251);
            this.btnPasteAndRecognition.Name = "btnPasteAndRecognition";
            this.btnPasteAndRecognition.Size = new System.Drawing.Size(145, 40);
            this.btnPasteAndRecognition.TabIndex = 1;
            this.btnPasteAndRecognition.Text = "粘贴剪贴板并识别";
            this.btnPasteAndRecognition.Click += new System.EventHandler(this.btnPasteAndRecognition_Click);
            // 
            // btnRecognition
            // 
            this.btnRecognition.Location = new System.Drawing.Point(181, 251);
            this.btnRecognition.Name = "btnRecognition";
            this.btnRecognition.Size = new System.Drawing.Size(145, 40);
            this.btnRecognition.TabIndex = 2;
            this.btnRecognition.Text = "只识别以上内容";
            this.btnRecognition.Click += new System.EventHandler(this.btnRecognition_Click);
            // 
            // btnDemoContent
            // 
            this.btnDemoContent.Location = new System.Drawing.Point(573, 251);
            this.btnDemoContent.Name = "btnDemoContent";
            this.btnDemoContent.Size = new System.Drawing.Size(141, 40);
            this.btnDemoContent.TabIndex = 3;
            this.btnDemoContent.Text = "查看示例并识别";
            this.btnDemoContent.Click += new System.EventHandler(this.btnDemoContent_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(623, 521);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(91, 40);
            this.btnClose.TabIndex = 6;
            this.btnClose.Text = "关闭";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSaveAsReceiveMan
            // 
            this.btnSaveAsReceiveMan.Location = new System.Drawing.Point(129, 521);
            this.btnSaveAsReceiveMan.Name = "btnSaveAsReceiveMan";
            this.btnSaveAsReceiveMan.Size = new System.Drawing.Size(117, 40);
            this.btnSaveAsReceiveMan.TabIndex = 5;
            this.btnSaveAsReceiveMan.Text = "保存为收件人";
            this.btnSaveAsReceiveMan.Click += new System.EventHandler(this.btnSaveAsReceiveMan_Click);
            // 
            // btnSaveAsSendMan
            // 
            this.btnSaveAsSendMan.Location = new System.Drawing.Point(6, 521);
            this.btnSaveAsSendMan.Name = "btnSaveAsSendMan";
            this.btnSaveAsSendMan.Size = new System.Drawing.Size(117, 40);
            this.btnSaveAsSendMan.TabIndex = 4;
            this.btnSaveAsSendMan.Text = "保存为发件人";
            this.btnSaveAsSendMan.Click += new System.EventHandler(this.btnSaveAsSendMan_Click);
            // 
            // grpResult
            // 
            this.grpResult.Controls.Add(this.txtPostCode);
            this.grpResult.Controls.Add(this.labelControl10);
            this.grpResult.Controls.Add(this.txtAddress);
            this.grpResult.Controls.Add(this.labelControl9);
            this.grpResult.Controls.Add(this.txtCounty);
            this.grpResult.Controls.Add(this.labelControl8);
            this.grpResult.Controls.Add(this.txtCity);
            this.grpResult.Controls.Add(this.txtProvince);
            this.grpResult.Controls.Add(this.labelControl7);
            this.grpResult.Controls.Add(this.labelControl6);
            this.grpResult.Controls.Add(this.txtMobile);
            this.grpResult.Controls.Add(this.labelControl5);
            this.grpResult.Controls.Add(this.txtTelephone);
            this.grpResult.Controls.Add(this.labelControl4);
            this.grpResult.Controls.Add(this.txtRealName);
            this.grpResult.Controls.Add(this.labelControl3);
            this.grpResult.Controls.Add(this.txtCompany);
            this.grpResult.Controls.Add(this.labelControl2);
            this.grpResult.Location = new System.Drawing.Point(2, 297);
            this.grpResult.Name = "grpResult";
            this.grpResult.Size = new System.Drawing.Size(717, 217);
            this.grpResult.TabIndex = 7;
            this.grpResult.Text = "识别内容如下";
            // 
            // txtPostCode
            // 
            this.txtPostCode.Location = new System.Drawing.Point(497, 178);
            this.txtPostCode.Name = "txtPostCode";
            this.txtPostCode.Size = new System.Drawing.Size(117, 20);
            this.txtPostCode.TabIndex = 17;
            // 
            // labelControl10
            // 
            this.labelControl10.Location = new System.Drawing.Point(448, 182);
            this.labelControl10.Name = "labelControl10";
            this.labelControl10.Size = new System.Drawing.Size(36, 14);
            this.labelControl10.TabIndex = 16;
            this.labelControl10.Text = "邮编：";
            // 
            // txtAddress
            // 
            this.txtAddress.Location = new System.Drawing.Point(83, 182);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(352, 20);
            this.txtAddress.TabIndex = 15;
            // 
            // labelControl9
            // 
            this.labelControl9.Location = new System.Drawing.Point(12, 185);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(60, 14);
            this.labelControl9.TabIndex = 14;
            this.labelControl9.Text = "街道地址：";
            // 
            // txtCounty
            // 
            this.txtCounty.Location = new System.Drawing.Point(497, 149);
            this.txtCounty.Name = "txtCounty";
            this.txtCounty.Size = new System.Drawing.Size(117, 20);
            this.txtCounty.TabIndex = 13;
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(443, 152);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(41, 14);
            this.labelControl8.TabIndex = 12;
            this.labelControl8.Text = "区/县：";
            // 
            // txtCity
            // 
            this.txtCity.Location = new System.Drawing.Point(268, 149);
            this.txtCity.Name = "txtCity";
            this.txtCity.Size = new System.Drawing.Size(167, 20);
            this.txtCity.TabIndex = 11;
            // 
            // txtProvince
            // 
            this.txtProvince.Location = new System.Drawing.Point(83, 149);
            this.txtProvince.Name = "txtProvince";
            this.txtProvince.Size = new System.Drawing.Size(119, 20);
            this.txtProvince.TabIndex = 10;
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(219, 152);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(36, 14);
            this.labelControl7.TabIndex = 9;
            this.labelControl7.Text = "城市：";
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(40, 153);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(36, 14);
            this.labelControl6.TabIndex = 8;
            this.labelControl6.Text = "省份：";
            // 
            // txtMobile
            // 
            this.txtMobile.Location = new System.Drawing.Point(376, 115);
            this.txtMobile.Name = "txtMobile";
            this.txtMobile.Size = new System.Drawing.Size(238, 20);
            this.txtMobile.TabIndex = 7;
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(331, 117);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(36, 14);
            this.labelControl5.TabIndex = 6;
            this.labelControl5.Text = "手机：";
            // 
            // txtTelephone
            // 
            this.txtTelephone.Location = new System.Drawing.Point(83, 115);
            this.txtTelephone.Name = "txtTelephone";
            this.txtTelephone.Size = new System.Drawing.Size(240, 20);
            this.txtTelephone.TabIndex = 5;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(40, 115);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(36, 14);
            this.labelControl4.TabIndex = 4;
            this.labelControl4.Text = "电话：";
            // 
            // txtRealName
            // 
            this.txtRealName.Location = new System.Drawing.Point(83, 78);
            this.txtRealName.Name = "txtRealName";
            this.txtRealName.Size = new System.Drawing.Size(238, 20);
            this.txtRealName.TabIndex = 3;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(40, 82);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(36, 14);
            this.labelControl3.TabIndex = 2;
            this.labelControl3.Text = "姓名：";
            // 
            // txtCompany
            // 
            this.txtCompany.Location = new System.Drawing.Point(83, 41);
            this.txtCompany.Name = "txtCompany";
            this.txtCompany.Size = new System.Drawing.Size(531, 20);
            this.txtCompany.TabIndex = 1;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(12, 44);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(60, 14);
            this.labelControl2.TabIndex = 0;
            this.labelControl2.Text = "单位名称：";
            // 
            // tspReceive
            // 
            this.tspReceive.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(139)))), ((int)(((byte)(222)))));
            this.tspReceive.Appearance.Options.UseBorderColor = true;
            this.tspReceive.Rounded = true;
            this.tspReceive.ShowBeak = true;
            // 
            // btnPasteReceiveMan
            // 
            this.btnPasteReceiveMan.Location = new System.Drawing.Point(377, 521);
            this.btnPasteReceiveMan.Name = "btnPasteReceiveMan";
            this.btnPasteReceiveMan.Size = new System.Drawing.Size(117, 40);
            this.btnPasteReceiveMan.TabIndex = 9;
            this.btnPasteReceiveMan.Text = "粘贴为收件人";
            this.btnPasteReceiveMan.Click += new System.EventHandler(this.btnPasteReceiveMan_Click);
            // 
            // btnPasteSendMan
            // 
            this.btnPasteSendMan.Location = new System.Drawing.Point(253, 521);
            this.btnPasteSendMan.Name = "btnPasteSendMan";
            this.btnPasteSendMan.Size = new System.Drawing.Size(117, 40);
            this.btnPasteSendMan.TabIndex = 8;
            this.btnPasteSendMan.Text = "粘贴为发件人";
            this.btnPasteSendMan.Click += new System.EventHandler(this.btnPasteSendMan_Click);
            // 
            // FrmRecognitionAddress
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(720, 569);
            this.Controls.Add(this.btnPasteReceiveMan);
            this.Controls.Add(this.btnPasteSendMan);
            this.Controls.Add(this.grpResult);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSaveAsReceiveMan);
            this.Controls.Add(this.btnSaveAsSendMan);
            this.Controls.Add(this.btnDemoContent);
            this.Controls.Add(this.btnRecognition);
            this.Controls.Add(this.btnPasteAndRecognition);
            this.Controls.Add(this.grpContent);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmRecognitionAddress";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "智能识别地址";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmRecognitionAddress_FormClosing);
            this.Load += new System.EventHandler(this.FrmRecognitionTaoBao_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grpContent)).EndInit();
            this.grpContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ckAutoPasteAndRecognition.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtContent.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpResult)).EndInit();
            this.grpResult.ResumeLayout(false);
            this.grpResult.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPostCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAddress.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCounty.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCity.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtProvince.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMobile.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTelephone.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRealName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCompany.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl grpContent;
        private DevExpress.XtraEditors.CheckEdit ckAutoPasteAndRecognition;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton btnPasteAndRecognition;
        private DevExpress.XtraEditors.SimpleButton btnRecognition;
        private DevExpress.XtraEditors.MemoEdit txtContent;
        private DevExpress.XtraEditors.SimpleButton btnDemoContent;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.SimpleButton btnSaveAsReceiveMan;
        private DevExpress.XtraEditors.SimpleButton btnSaveAsSendMan;
        private DevExpress.XtraEditors.GroupControl grpResult;
        private DevExpress.XtraEditors.TextEdit txtCompany;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit txtPostCode;
        private DevExpress.XtraEditors.LabelControl labelControl10;
        private DevExpress.XtraEditors.TextEdit txtAddress;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.XtraEditors.TextEdit txtCounty;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.TextEdit txtCity;
        private DevExpress.XtraEditors.TextEdit txtProvince;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.TextEdit txtMobile;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.TextEdit txtTelephone;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.TextEdit txtRealName;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.Utils.ToolTipController tspReceive;
        private DevExpress.XtraEditors.SimpleButton btnPasteReceiveMan;
        private DevExpress.XtraEditors.SimpleButton btnPasteSendMan;
    }
}