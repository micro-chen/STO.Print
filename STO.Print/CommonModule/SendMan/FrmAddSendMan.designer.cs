namespace STO.Print
{
    partial class FrmAddSendMan
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAddSendMan));
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.tabSendMan = new DevExpress.XtraTab.XtraTabControl();
            this.tabPageSendMan = new DevExpress.XtraTab.XtraTabPage();
            this.ckDefault = new DevExpress.XtraEditors.CheckEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtTelePhone = new DevExpress.XtraEditors.TextEdit();
            this.lblPostCode = new DevExpress.XtraEditors.LabelControl();
            this.lblBindElecUserInfo = new System.Windows.Forms.LinkLabel();
            this.txtPostCode = new DevExpress.XtraEditors.TextEdit();
            this.lblPhone = new DevExpress.XtraEditors.LabelControl();
            this.txtMobile = new DevExpress.XtraEditors.TextEdit();
            this.lblCompany = new DevExpress.XtraEditors.LabelControl();
            this.txtCompanyName = new DevExpress.XtraEditors.TextEdit();
            this.lblAddress = new DevExpress.XtraEditors.LabelControl();
            this.dgvSearchSendArea = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.dbFULLNAME = new DevExpress.XtraGrid.Columns.GridColumn();
            this.dbSIMPLESPELLING = new DevExpress.XtraGrid.Columns.GridColumn();
            this.txtSendAddress = new DevExpress.XtraEditors.TextEdit();
            this.lblArea = new DevExpress.XtraEditors.LabelControl();
            this.txtRealName = new DevExpress.XtraEditors.TextEdit();
            this.lblRealName = new DevExpress.XtraEditors.LabelControl();
            this.tabPageRemark = new DevExpress.XtraTab.XtraTabPage();
            this.txtRemark = new DevExpress.XtraEditors.MemoEdit();
            this.lblContentsLength = new DevExpress.XtraEditors.LabelControl();
            this.btnGetElecCount = new DevExpress.XtraEditors.SimpleButton();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.ckProclaimText = new DevExpress.XtraEditors.CheckEdit();
            this.txtCustomerPassword = new DevExpress.XtraEditors.ButtonEdit();
            this.lblCustomerPassword = new DevExpress.XtraEditors.LabelControl();
            this.txtCustomerID = new DevExpress.XtraEditors.TextEdit();
            this.lblCustomerID = new DevExpress.XtraEditors.LabelControl();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnSaveAndClose = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabSendMan)).BeginInit();
            this.tabSendMan.SuspendLayout();
            this.tabPageSendMan.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ckDefault.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTelePhone.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPostCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMobile.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCompanyName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSearchSendArea.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSendAddress.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRealName.Properties)).BeginInit();
            this.tabPageRemark.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtRemark.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckProclaimText.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCustomerPassword.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCustomerID.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.CollapsePanel = DevExpress.XtraEditors.SplitCollapsePanel.Panel2;
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Horizontal = false;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.AutoScroll = true;
            this.splitContainerControl1.Panel1.Controls.Add(this.tabSendMan);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.AutoScroll = true;
            this.splitContainerControl1.Panel2.Controls.Add(this.btnGetElecCount);
            this.splitContainerControl1.Panel2.Controls.Add(this.linkLabel1);
            this.splitContainerControl1.Panel2.Controls.Add(this.ckProclaimText);
            this.splitContainerControl1.Panel2.Controls.Add(this.txtCustomerPassword);
            this.splitContainerControl1.Panel2.Controls.Add(this.lblCustomerPassword);
            this.splitContainerControl1.Panel2.Controls.Add(this.txtCustomerID);
            this.splitContainerControl1.Panel2.Controls.Add(this.lblCustomerID);
            this.splitContainerControl1.Panel2.Controls.Add(this.btnSave);
            this.splitContainerControl1.Panel2.Controls.Add(this.btnSaveAndClose);
            this.splitContainerControl1.Panel2.Controls.Add(this.btnCancel);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(484, 540);
            this.splitContainerControl1.SplitterPosition = 395;
            this.splitContainerControl1.TabIndex = 1;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // tabSendMan
            // 
            this.tabSendMan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabSendMan.Location = new System.Drawing.Point(0, 0);
            this.tabSendMan.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabSendMan.Name = "tabSendMan";
            this.tabSendMan.SelectedTabPage = this.tabPageSendMan;
            this.tabSendMan.Size = new System.Drawing.Size(484, 395);
            this.tabSendMan.TabIndex = 0;
            this.tabSendMan.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tabPageSendMan,
            this.tabPageRemark});
            this.tabSendMan.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(this.xtraTabControl1_SelectedPageChanged);
            // 
            // tabPageSendMan
            // 
            this.tabPageSendMan.Controls.Add(this.ckDefault);
            this.tabPageSendMan.Controls.Add(this.labelControl2);
            this.tabPageSendMan.Controls.Add(this.txtTelePhone);
            this.tabPageSendMan.Controls.Add(this.lblPostCode);
            this.tabPageSendMan.Controls.Add(this.lblBindElecUserInfo);
            this.tabPageSendMan.Controls.Add(this.txtPostCode);
            this.tabPageSendMan.Controls.Add(this.lblPhone);
            this.tabPageSendMan.Controls.Add(this.txtMobile);
            this.tabPageSendMan.Controls.Add(this.lblCompany);
            this.tabPageSendMan.Controls.Add(this.txtCompanyName);
            this.tabPageSendMan.Controls.Add(this.lblAddress);
            this.tabPageSendMan.Controls.Add(this.dgvSearchSendArea);
            this.tabPageSendMan.Controls.Add(this.txtSendAddress);
            this.tabPageSendMan.Controls.Add(this.lblArea);
            this.tabPageSendMan.Controls.Add(this.txtRealName);
            this.tabPageSendMan.Controls.Add(this.lblRealName);
            this.tabPageSendMan.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.tabPageSendMan.Name = "tabPageSendMan";
            this.tabPageSendMan.Size = new System.Drawing.Size(478, 366);
            this.tabPageSendMan.Text = "发件人信息";
            // 
            // ckDefault
            // 
            this.ckDefault.EditValue = true;
            this.ckDefault.Location = new System.Drawing.Point(100, 340);
            this.ckDefault.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ckDefault.Name = "ckDefault";
            this.ckDefault.Properties.Caption = "是否默认发件人";
            this.ckDefault.Size = new System.Drawing.Size(131, 19);
            this.ckDefault.TabIndex = 14;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(38, 256);
            this.labelControl2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(52, 14);
            this.labelControl2.TabIndex = 10;
            this.labelControl2.Text = "电话号码:";
            // 
            // txtTelePhone
            // 
            this.txtTelePhone.Location = new System.Drawing.Point(100, 254);
            this.txtTelePhone.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtTelePhone.Name = "txtTelePhone";
            this.txtTelePhone.Properties.MaxLength = 30;
            this.txtTelePhone.Properties.NullValuePrompt = "电话号码";
            this.txtTelePhone.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtTelePhone.Size = new System.Drawing.Size(357, 20);
            this.txtTelePhone.TabIndex = 11;
            // 
            // lblPostCode
            // 
            this.lblPostCode.Location = new System.Drawing.Point(62, 301);
            this.lblPostCode.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lblPostCode.Name = "lblPostCode";
            this.lblPostCode.Size = new System.Drawing.Size(28, 14);
            this.lblPostCode.TabIndex = 12;
            this.lblPostCode.Text = "邮编:";
            // 
            // lblBindElecUserInfo
            // 
            this.lblBindElecUserInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lblBindElecUserInfo.AutoSize = true;
            this.lblBindElecUserInfo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBindElecUserInfo.Location = new System.Drawing.Point(180, 6);
            this.lblBindElecUserInfo.Name = "lblBindElecUserInfo";
            this.lblBindElecUserInfo.Size = new System.Drawing.Size(177, 14);
            this.lblBindElecUserInfo.TabIndex = 4;
            this.lblBindElecUserInfo.TabStop = true;
            this.lblBindElecUserInfo.Text = "绑定申通电子面单线下商家ID";
            this.lblBindElecUserInfo.Visible = false;
            this.lblBindElecUserInfo.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblBindElecUserInfo_LinkClicked);
            // 
            // txtPostCode
            // 
            this.txtPostCode.Location = new System.Drawing.Point(100, 299);
            this.txtPostCode.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtPostCode.Name = "txtPostCode";
            this.txtPostCode.Properties.Mask.EditMask = "[0-9]*";
            this.txtPostCode.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txtPostCode.Properties.MaxLength = 6;
            this.txtPostCode.Properties.NullValuePrompt = "发件邮编";
            this.txtPostCode.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtPostCode.Size = new System.Drawing.Size(357, 20);
            this.txtPostCode.TabIndex = 13;
            // 
            // lblPhone
            // 
            this.lblPhone.Location = new System.Drawing.Point(38, 209);
            this.lblPhone.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.Size = new System.Drawing.Size(52, 14);
            this.lblPhone.TabIndex = 8;
            this.lblPhone.Text = "手机号码:";
            // 
            // txtMobile
            // 
            this.txtMobile.Location = new System.Drawing.Point(100, 208);
            this.txtMobile.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtMobile.Name = "txtMobile";
            this.txtMobile.Properties.Appearance.BackColor = System.Drawing.Color.LemonChiffon;
            this.txtMobile.Properties.Appearance.Options.UseBackColor = true;
            this.txtMobile.Properties.Mask.EditMask = "[0-9]*";
            this.txtMobile.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txtMobile.Properties.MaxLength = 11;
            this.txtMobile.Properties.NullValuePrompt = "手机号码必填";
            this.txtMobile.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtMobile.Size = new System.Drawing.Size(357, 20);
            this.txtMobile.TabIndex = 9;
            // 
            // lblCompany
            // 
            this.lblCompany.Location = new System.Drawing.Point(38, 165);
            this.lblCompany.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lblCompany.Name = "lblCompany";
            this.lblCompany.Size = new System.Drawing.Size(52, 14);
            this.lblCompany.TabIndex = 6;
            this.lblCompany.Text = "单位名称:";
            // 
            // txtCompanyName
            // 
            this.txtCompanyName.Location = new System.Drawing.Point(100, 163);
            this.txtCompanyName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtCompanyName.Name = "txtCompanyName";
            this.txtCompanyName.Properties.MaxLength = 25;
            this.txtCompanyName.Properties.NullValuePrompt = "发件网点或者单位名称";
            this.txtCompanyName.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtCompanyName.Size = new System.Drawing.Size(357, 20);
            this.txtCompanyName.TabIndex = 7;
            // 
            // lblAddress
            // 
            this.lblAddress.Location = new System.Drawing.Point(38, 120);
            this.lblAddress.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Size = new System.Drawing.Size(52, 14);
            this.lblAddress.TabIndex = 4;
            this.lblAddress.Text = "详细地址:";
            // 
            // dgvSearchSendArea
            // 
            this.dgvSearchSendArea.Location = new System.Drawing.Point(100, 72);
            this.dgvSearchSendArea.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dgvSearchSendArea.Name = "dgvSearchSendArea";
            this.dgvSearchSendArea.Properties.Appearance.BackColor = System.Drawing.Color.LemonChiffon;
            this.dgvSearchSendArea.Properties.Appearance.Options.UseBackColor = true;
            this.dgvSearchSendArea.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dgvSearchSendArea.Properties.MaxLength = 50;
            this.dgvSearchSendArea.Properties.NullText = "";
            this.dgvSearchSendArea.Properties.NullValuePrompt = "格式：江苏省-苏州市-吴中区";
            this.dgvSearchSendArea.Properties.View = this.gridLookUpEdit1View;
            this.dgvSearchSendArea.Size = new System.Drawing.Size(357, 20);
            this.dgvSearchSendArea.TabIndex = 3;
            this.dgvSearchSendArea.ToolTip = "寄件区域来自系统数据，如无数据表示该省市区在系统无记录";
            this.dgvSearchSendArea.EditValueChanged += new System.EventHandler(this.dgvSearchSendArea_EditValueChanged);
            // 
            // gridLookUpEdit1View
            // 
            this.gridLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.dbFULLNAME,
            this.dbSIMPLESPELLING});
            this.gridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridLookUpEdit1View.Name = "gridLookUpEdit1View";
            this.gridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // dbFULLNAME
            // 
            this.dbFULLNAME.Caption = "区域名称";
            this.dbFULLNAME.FieldName = "FullName";
            this.dbFULLNAME.Name = "dbFULLNAME";
            this.dbFULLNAME.Visible = true;
            this.dbFULLNAME.VisibleIndex = 0;
            this.dbFULLNAME.Width = 260;
            // 
            // dbSIMPLESPELLING
            // 
            this.dbSIMPLESPELLING.Caption = "区县简拼";
            this.dbSIMPLESPELLING.FieldName = "SimpleSpelling";
            this.dbSIMPLESPELLING.Name = "dbSIMPLESPELLING";
            this.dbSIMPLESPELLING.Visible = true;
            this.dbSIMPLESPELLING.VisibleIndex = 1;
            this.dbSIMPLESPELLING.Width = 124;
            // 
            // txtSendAddress
            // 
            this.txtSendAddress.Location = new System.Drawing.Point(100, 118);
            this.txtSendAddress.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtSendAddress.Name = "txtSendAddress";
            this.txtSendAddress.Properties.Appearance.BackColor = System.Drawing.Color.LemonChiffon;
            this.txtSendAddress.Properties.Appearance.Options.UseBackColor = true;
            this.txtSendAddress.Properties.MaxLength = 30;
            this.txtSendAddress.Properties.NullValuePrompt = "如实填写详细地址,例如街道名称,门牌号码,楼层和房间号等信息";
            this.txtSendAddress.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtSendAddress.Size = new System.Drawing.Size(357, 20);
            this.txtSendAddress.TabIndex = 5;
            this.txtSendAddress.Leave += new System.EventHandler(this.txtSendAddress_Leave);
            // 
            // lblArea
            // 
            this.lblArea.Location = new System.Drawing.Point(38, 74);
            this.lblArea.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lblArea.Name = "lblArea";
            this.lblArea.Size = new System.Drawing.Size(52, 14);
            this.lblArea.TabIndex = 2;
            this.lblArea.Text = "所在区域:";
            // 
            // txtRealName
            // 
            this.txtRealName.Location = new System.Drawing.Point(100, 27);
            this.txtRealName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtRealName.Name = "txtRealName";
            this.txtRealName.Properties.Appearance.BackColor = System.Drawing.Color.LemonChiffon;
            this.txtRealName.Properties.Appearance.Options.UseBackColor = true;
            this.txtRealName.Properties.MaxLength = 30;
            this.txtRealName.Properties.NullValuePrompt = "建议填写真实姓名，快递已经开始实名制";
            this.txtRealName.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtRealName.Size = new System.Drawing.Size(357, 20);
            this.txtRealName.TabIndex = 1;
            // 
            // lblRealName
            // 
            this.lblRealName.Location = new System.Drawing.Point(62, 30);
            this.lblRealName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lblRealName.Name = "lblRealName";
            this.lblRealName.Size = new System.Drawing.Size(28, 14);
            this.lblRealName.TabIndex = 0;
            this.lblRealName.Text = "姓名:";
            // 
            // tabPageRemark
            // 
            this.tabPageRemark.Controls.Add(this.txtRemark);
            this.tabPageRemark.Controls.Add(this.lblContentsLength);
            this.tabPageRemark.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.tabPageRemark.Name = "tabPageRemark";
            this.tabPageRemark.Size = new System.Drawing.Size(478, 366);
            this.tabPageRemark.Text = "备注";
            // 
            // txtRemark
            // 
            this.txtRemark.AllowDrop = true;
            this.txtRemark.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRemark.EditValue = "";
            this.txtRemark.Location = new System.Drawing.Point(0, 0);
            this.txtRemark.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Properties.Appearance.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRemark.Properties.Appearance.Options.UseFont = true;
            this.txtRemark.Properties.MaxLength = 100;
            this.txtRemark.Properties.NullValuePrompt = "发件人备注信息";
            this.txtRemark.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtRemark.Properties.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtRemark.Size = new System.Drawing.Size(478, 308);
            this.txtRemark.TabIndex = 0;
            this.txtRemark.EditValueChanged += new System.EventHandler(this.txtRemark_EditValueChanged);
            // 
            // lblContentsLength
            // 
            this.lblContentsLength.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblContentsLength.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.lblContentsLength.Location = new System.Drawing.Point(3, 316);
            this.lblContentsLength.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lblContentsLength.Name = "lblContentsLength";
            this.lblContentsLength.Size = new System.Drawing.Size(48, 14);
            this.lblContentsLength.TabIndex = 13;
            this.lblContentsLength.Text = "统计字数";
            // 
            // btnGetElecCount
            // 
            this.btnGetElecCount.Location = new System.Drawing.Point(345, 42);
            this.btnGetElecCount.Name = "btnGetElecCount";
            this.btnGetElecCount.Size = new System.Drawing.Size(112, 23);
            this.btnGetElecCount.TabIndex = 15;
            this.btnGetElecCount.Text = "查看电子面单数量";
            this.btnGetElecCount.Click += new System.EventHandler(this.btnGetElecCount_Click);
            // 
            // linkLabel1
            // 
            this.linkLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel1.Location = new System.Drawing.Point(0, 85);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(99, 14);
            this.linkLabel1.TabIndex = 16;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "如何获取商家ID";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // ckProclaimText
            // 
            this.ckProclaimText.Location = new System.Drawing.Point(99, 84);
            this.ckProclaimText.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ckProclaimText.Name = "ckProclaimText";
            this.ckProclaimText.Properties.Caption = "明文显示";
            this.ckProclaimText.Size = new System.Drawing.Size(80, 19);
            this.ckProclaimText.TabIndex = 15;
            this.ckProclaimText.CheckedChanged += new System.EventHandler(this.ckProclaimText_CheckedChanged);
            // 
            // txtCustomerPassword
            // 
            this.txtCustomerPassword.Location = new System.Drawing.Point(100, 44);
            this.txtCustomerPassword.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtCustomerPassword.Name = "txtCustomerPassword";
            this.txtCustomerPassword.Properties.Appearance.BackColor = System.Drawing.Color.LemonChiffon;
            this.txtCustomerPassword.Properties.Appearance.Options.UseBackColor = true;
            this.txtCustomerPassword.Properties.NullValuePrompt = "密码不可以告诉别人";
            this.txtCustomerPassword.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtCustomerPassword.Properties.UseSystemPasswordChar = true;
            this.txtCustomerPassword.Size = new System.Drawing.Size(239, 20);
            this.txtCustomerPassword.TabIndex = 3;
            // 
            // lblCustomerPassword
            // 
            this.lblCustomerPassword.Location = new System.Drawing.Point(1, 47);
            this.lblCustomerPassword.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lblCustomerPassword.Name = "lblCustomerPassword";
            this.lblCustomerPassword.Size = new System.Drawing.Size(96, 14);
            this.lblCustomerPassword.TabIndex = 2;
            this.lblCustomerPassword.Text = "申通商家ID密码：";
            // 
            // txtCustomerID
            // 
            this.txtCustomerID.Location = new System.Drawing.Point(100, 2);
            this.txtCustomerID.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtCustomerID.Name = "txtCustomerID";
            this.txtCustomerID.Properties.Appearance.BackColor = System.Drawing.Color.LemonChiffon;
            this.txtCustomerID.Properties.Appearance.Options.UseBackColor = true;
            this.txtCustomerID.Properties.NullValuePrompt = "申通电子面单线下商家ID";
            this.txtCustomerID.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtCustomerID.Size = new System.Drawing.Size(357, 20);
            this.txtCustomerID.TabIndex = 1;
            // 
            // lblCustomerID
            // 
            this.lblCustomerID.Location = new System.Drawing.Point(26, 6);
            this.lblCustomerID.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lblCustomerID.Name = "lblCustomerID";
            this.lblCustomerID.Size = new System.Drawing.Size(64, 14);
            this.lblCustomerID.TabIndex = 0;
            this.lblCustomerID.Text = "申通商家ID:";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(277, 77);
            this.btnSave.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(87, 33);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "保存";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnSaveAndClose
            // 
            this.btnSaveAndClose.Location = new System.Drawing.Point(184, 77);
            this.btnSaveAndClose.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnSaveAndClose.Name = "btnSaveAndClose";
            this.btnSaveAndClose.Size = new System.Drawing.Size(87, 33);
            this.btnSaveAndClose.TabIndex = 5;
            this.btnSaveAndClose.Text = "保存并关闭";
            this.btnSaveAndClose.Click += new System.EventHandler(this.btnSaveAndClose_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(370, 77);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(87, 33);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // FrmAddSendMan
            // 
            this.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(484, 540);
            this.Controls.Add(this.splitContainerControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmAddSendMan";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "发件人";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmAddSendMan_FormClosing);
            this.Load += new System.EventHandler(this.FrmSendMan_Load);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tabSendMan)).EndInit();
            this.tabSendMan.ResumeLayout(false);
            this.tabPageSendMan.ResumeLayout(false);
            this.tabPageSendMan.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ckDefault.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTelePhone.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPostCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMobile.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCompanyName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSearchSendArea.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSendAddress.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRealName.Properties)).EndInit();
            this.tabPageRemark.ResumeLayout(false);
            this.tabPageRemark.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtRemark.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckProclaimText.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCustomerPassword.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCustomerID.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraTab.XtraTabControl tabSendMan;
        private DevExpress.XtraTab.XtraTabPage tabPageSendMan;
        private DevExpress.XtraEditors.LabelControl lblRealName;
        private DevExpress.XtraTab.XtraTabPage tabPageRemark;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.TextEdit txtSendAddress;
        private DevExpress.XtraEditors.LabelControl lblArea;
        private DevExpress.XtraEditors.TextEdit txtRealName;
        private DevExpress.XtraEditors.GridLookUpEdit dgvSearchSendArea;
        private DevExpress.XtraGrid.Views.Grid.GridView gridLookUpEdit1View;
        private DevExpress.XtraGrid.Columns.GridColumn dbFULLNAME;
        private DevExpress.XtraGrid.Columns.GridColumn dbSIMPLESPELLING;
        private DevExpress.XtraEditors.LabelControl lblPostCode;
        private DevExpress.XtraEditors.TextEdit txtPostCode;
        private DevExpress.XtraEditors.LabelControl lblPhone;
        private DevExpress.XtraEditors.TextEdit txtMobile;
        private DevExpress.XtraEditors.LabelControl lblCompany;
        private DevExpress.XtraEditors.TextEdit txtCompanyName;
        private DevExpress.XtraEditors.LabelControl lblAddress;
        private DevExpress.XtraEditors.LabelControl lblContentsLength;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit txtTelePhone;
        private DevExpress.XtraEditors.SimpleButton btnSaveAndClose;
        private DevExpress.XtraEditors.CheckEdit ckDefault;
        private DevExpress.XtraEditors.MemoEdit txtRemark;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private System.Windows.Forms.LinkLabel lblBindElecUserInfo;
        private DevExpress.XtraEditors.TextEdit txtCustomerID;
        private DevExpress.XtraEditors.LabelControl lblCustomerID;
        private DevExpress.XtraEditors.LabelControl lblCustomerPassword;
        private DevExpress.XtraEditors.ButtonEdit txtCustomerPassword;
        private DevExpress.XtraEditors.CheckEdit ckProclaimText;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private DevExpress.XtraEditors.SimpleButton btnGetElecCount;


    }
}