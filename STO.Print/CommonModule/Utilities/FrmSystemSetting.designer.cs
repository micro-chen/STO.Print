namespace STO.Print
{
    partial class FrmSystemSetting
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSystemSetting));
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.btnSend = new DevExpress.XtraBars.BarButtonItem();
            this.btnQQ = new DevExpress.XtraBars.BarButtonItem();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.backViewSetting = new DevExpress.XtraBars.Ribbon.BackstageViewControl();
            this.backstageViewClientControl1 = new DevExpress.XtraBars.Ribbon.BackstageViewClientControl();
            this.btnLookPrinting = new DevExpress.XtraEditors.SimpleButton();
            this.lblDefaultPrinter = new System.Windows.Forms.Label();
            this.btnSavePrinter = new DevExpress.XtraEditors.SimpleButton();
            this.lsbPrinter = new DevExpress.XtraEditors.ListBoxControl();
            this.backstageViewClientControl2 = new DevExpress.XtraBars.Ribbon.BackstageViewClientControl();
            this.btnInitBillTemplate = new DevExpress.XtraEditors.SimpleButton();
            this.lblDefaultPrintTemplate = new System.Windows.Forms.Label();
            this.btnSaveTemplate = new DevExpress.XtraEditors.SimpleButton();
            this.lsbTemplate = new DevExpress.XtraEditors.ListBoxControl();
            this.backstageViewClientControl3 = new DevExpress.XtraBars.Ribbon.BackstageViewClientControl();
            this.gcKeyBoard = new DevExpress.XtraEditors.GroupControl();
            this.btnOpenBackUpFolder = new DevExpress.XtraEditors.SimpleButton();
            this.btnBackUpDataBase = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.backstageViewClientControl4 = new DevExpress.XtraBars.Ribbon.BackstageViewClientControl();
            this.bindZtoElecUserInfo1 = new STO.Print.UserControl.BindZtoElecUserInfo();
            this.backstageViewClientControl5 = new DevExpress.XtraBars.Ribbon.BackstageViewClientControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.btnOpenBaseInfo = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.backstageViewTabItemPrinter = new DevExpress.XtraBars.Ribbon.BackstageViewTabItem();
            this.backstageViewTabItemTemplate = new DevExpress.XtraBars.Ribbon.BackstageViewTabItem();
            this.backstageViewTabItem2 = new DevExpress.XtraBars.Ribbon.BackstageViewTabItem();
            this.backstageViewTabItem3 = new DevExpress.XtraBars.Ribbon.BackstageViewTabItem();
            this.bvBaseInfo = new DevExpress.XtraBars.Ribbon.BackstageViewTabItem();
            this.backstageViewTabItem1 = new DevExpress.XtraBars.Ribbon.BackstageViewTabItem();
            this.backstageViewTabItem4 = new DevExpress.XtraBars.Ribbon.BackstageViewTabItem();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.backViewSetting.SuspendLayout();
            this.backstageViewClientControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lsbPrinter)).BeginInit();
            this.backstageViewClientControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lsbTemplate)).BeginInit();
            this.backstageViewClientControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcKeyBoard)).BeginInit();
            this.gcKeyBoard.SuspendLayout();
            this.backstageViewClientControl4.SuspendLayout();
            this.backstageViewClientControl5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnSend, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnQQ, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "Tools";
            // 
            // btnSend
            // 
            this.btnSend.Caption = "基础设置";
            this.btnSend.Glyph = ((System.Drawing.Image)(resources.GetObject("btnSend.Glyph")));
            this.btnSend.Id = 0;
            this.btnSend.Name = "btnSend";
            // 
            // btnQQ
            // 
            this.btnQQ.Caption = "权限设置";
            this.btnQQ.Glyph = ((System.Drawing.Image)(resources.GetObject("btnQQ.Glyph")));
            this.btnQQ.Id = 1;
            this.btnQQ.Name = "btnQQ";
            this.btnQQ.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.btnSend,
            this.btnQQ});
            this.barManager1.MaxItemId = 6;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.barDockControlTop.Size = new System.Drawing.Size(647, 31);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 573);
            this.barDockControlBottom.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.barDockControlBottom.Size = new System.Drawing.Size(647, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 31);
            this.barDockControlLeft.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 542);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(647, 31);
            this.barDockControlRight.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 542);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.backViewSetting);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 31);
            this.panelControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(647, 542);
            this.panelControl1.TabIndex = 4;
            // 
            // backViewSetting
            // 
            this.backViewSetting.BackColor = System.Drawing.Color.White;
            this.backViewSetting.ColorScheme = DevExpress.XtraBars.Ribbon.RibbonControlColorScheme.Yellow;
            this.backViewSetting.Controls.Add(this.backstageViewClientControl2);
            this.backViewSetting.Controls.Add(this.backstageViewClientControl3);
            this.backViewSetting.Controls.Add(this.backstageViewClientControl4);
            this.backViewSetting.Controls.Add(this.backstageViewClientControl5);
            this.backViewSetting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.backViewSetting.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.backViewSetting.Items.Add(this.backstageViewTabItemPrinter);
            this.backViewSetting.Items.Add(this.backstageViewTabItemTemplate);
            this.backViewSetting.Items.Add(this.backstageViewTabItem2);
            this.backViewSetting.Items.Add(this.backstageViewTabItem3);
            this.backViewSetting.Items.Add(this.bvBaseInfo);
            this.backViewSetting.Location = new System.Drawing.Point(2, 2);
            this.backViewSetting.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.backViewSetting.Name = "backViewSetting";
            this.backViewSetting.SelectedTab = null;
            this.backViewSetting.Size = new System.Drawing.Size(643, 538);
            this.backViewSetting.TabIndex = 0;
            this.backViewSetting.Text = "系统设置";
            // 
            // backstageViewClientControl1
            // 
            this.backstageViewClientControl1.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.backstageViewClientControl1.Appearance.Options.UseFont = true;
            this.backstageViewClientControl1.Controls.Add(this.btnLookPrinting);
            this.backstageViewClientControl1.Controls.Add(this.lblDefaultPrinter);
            this.backstageViewClientControl1.Controls.Add(this.btnSavePrinter);
            this.backstageViewClientControl1.Controls.Add(this.lsbPrinter);
            this.backstageViewClientControl1.Location = new System.Drawing.Point(192, 0);
            this.backstageViewClientControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.backstageViewClientControl1.Name = "backstageViewClientControl1";
            this.backstageViewClientControl1.Size = new System.Drawing.Size(451, 538);
            this.backstageViewClientControl1.TabIndex = 0;
            // 
            // btnLookPrinting
            // 
            this.btnLookPrinting.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLookPrinting.Location = new System.Drawing.Point(274, 475);
            this.btnLookPrinting.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnLookPrinting.Name = "btnLookPrinting";
            this.btnLookPrinting.Size = new System.Drawing.Size(112, 28);
            this.btnLookPrinting.TabIndex = 6;
            this.btnLookPrinting.Text = "查看正在打印什么";
            this.btnLookPrinting.Click += new System.EventHandler(this.btnLookPrinting_Click);
            // 
            // lblDefaultPrinter
            // 
            this.lblDefaultPrinter.AutoSize = true;
            this.lblDefaultPrinter.Location = new System.Drawing.Point(9, 476);
            this.lblDefaultPrinter.Name = "lblDefaultPrinter";
            this.lblDefaultPrinter.Size = new System.Drawing.Size(80, 17);
            this.lblDefaultPrinter.TabIndex = 5;
            this.lblDefaultPrinter.Text = "默认打印机：";
            // 
            // btnSavePrinter
            // 
            this.btnSavePrinter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSavePrinter.Location = new System.Drawing.Point(193, 475);
            this.btnSavePrinter.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnSavePrinter.Name = "btnSavePrinter";
            this.btnSavePrinter.Size = new System.Drawing.Size(75, 28);
            this.btnSavePrinter.TabIndex = 3;
            this.btnSavePrinter.Text = "保存";
            this.btnSavePrinter.Click += new System.EventHandler(this.btnSavePrinter_Click);
            // 
            // lsbPrinter
            // 
            this.lsbPrinter.Dock = System.Windows.Forms.DockStyle.Top;
            this.lsbPrinter.Location = new System.Drawing.Point(0, 0);
            this.lsbPrinter.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lsbPrinter.Name = "lsbPrinter";
            this.lsbPrinter.Size = new System.Drawing.Size(451, 446);
            this.lsbPrinter.TabIndex = 2;
            // 
            // backstageViewClientControl2
            // 
            this.backstageViewClientControl2.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.backstageViewClientControl2.Appearance.Options.UseFont = true;
            this.backstageViewClientControl2.Controls.Add(this.btnInitBillTemplate);
            this.backstageViewClientControl2.Controls.Add(this.lblDefaultPrintTemplate);
            this.backstageViewClientControl2.Controls.Add(this.btnSaveTemplate);
            this.backstageViewClientControl2.Controls.Add(this.lsbTemplate);
            this.backstageViewClientControl2.Location = new System.Drawing.Point(148, 0);
            this.backstageViewClientControl2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.backstageViewClientControl2.Name = "backstageViewClientControl2";
            this.backstageViewClientControl2.Size = new System.Drawing.Size(495, 538);
            this.backstageViewClientControl2.TabIndex = 1;
            // 
            // btnInitBillTemplate
            // 
            this.btnInitBillTemplate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnInitBillTemplate.Location = new System.Drawing.Point(374, 501);
            this.btnInitBillTemplate.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnInitBillTemplate.Name = "btnInitBillTemplate";
            this.btnInitBillTemplate.Size = new System.Drawing.Size(103, 28);
            this.btnInitBillTemplate.TabIndex = 8;
            this.btnInitBillTemplate.Text = "还原默认模板";
            this.btnInitBillTemplate.Click += new System.EventHandler(this.btnInitBillTemplate_Click);
            // 
            // lblDefaultPrintTemplate
            // 
            this.lblDefaultPrintTemplate.AutoSize = true;
            this.lblDefaultPrintTemplate.Location = new System.Drawing.Point(9, 471);
            this.lblDefaultPrintTemplate.Name = "lblDefaultPrintTemplate";
            this.lblDefaultPrintTemplate.Size = new System.Drawing.Size(92, 17);
            this.lblDefaultPrintTemplate.TabIndex = 7;
            this.lblDefaultPrintTemplate.Text = "默认打印模板：";
            // 
            // btnSaveTemplate
            // 
            this.btnSaveTemplate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveTemplate.Location = new System.Drawing.Point(402, 465);
            this.btnSaveTemplate.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnSaveTemplate.Name = "btnSaveTemplate";
            this.btnSaveTemplate.Size = new System.Drawing.Size(75, 28);
            this.btnSaveTemplate.TabIndex = 6;
            this.btnSaveTemplate.Text = "保存";
            this.btnSaveTemplate.Click += new System.EventHandler(this.btnSaveTemplate_Click);
            // 
            // lsbTemplate
            // 
            this.lsbTemplate.Dock = System.Windows.Forms.DockStyle.Top;
            this.lsbTemplate.Location = new System.Drawing.Point(0, 0);
            this.lsbTemplate.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lsbTemplate.Name = "lsbTemplate";
            this.lsbTemplate.Size = new System.Drawing.Size(495, 446);
            this.lsbTemplate.TabIndex = 1;
            // 
            // backstageViewClientControl3
            // 
            this.backstageViewClientControl3.Controls.Add(this.gcKeyBoard);
            this.backstageViewClientControl3.Location = new System.Drawing.Point(148, 0);
            this.backstageViewClientControl3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.backstageViewClientControl3.Name = "backstageViewClientControl3";
            this.backstageViewClientControl3.Size = new System.Drawing.Size(495, 538);
            this.backstageViewClientControl3.TabIndex = 2;
            // 
            // gcKeyBoard
            // 
            this.gcKeyBoard.Controls.Add(this.btnOpenBackUpFolder);
            this.gcKeyBoard.Controls.Add(this.btnBackUpDataBase);
            this.gcKeyBoard.Controls.Add(this.labelControl1);
            this.gcKeyBoard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcKeyBoard.Location = new System.Drawing.Point(0, 0);
            this.gcKeyBoard.LookAndFeel.SkinName = "Office 2013";
            this.gcKeyBoard.LookAndFeel.UseDefaultLookAndFeel = false;
            this.gcKeyBoard.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gcKeyBoard.Name = "gcKeyBoard";
            this.gcKeyBoard.Size = new System.Drawing.Size(495, 538);
            this.gcKeyBoard.TabIndex = 0;
            this.gcKeyBoard.Text = "数据备份";
            // 
            // btnOpenBackUpFolder
            // 
            this.btnOpenBackUpFolder.Location = new System.Drawing.Point(257, 49);
            this.btnOpenBackUpFolder.Name = "btnOpenBackUpFolder";
            this.btnOpenBackUpFolder.Size = new System.Drawing.Size(96, 23);
            this.btnOpenBackUpFolder.TabIndex = 2;
            this.btnOpenBackUpFolder.Text = "打开备份目录";
            this.btnOpenBackUpFolder.Click += new System.EventHandler(this.btnOpenBackUpFolder_Click);
            // 
            // btnBackUpDataBase
            // 
            this.btnBackUpDataBase.Location = new System.Drawing.Point(144, 49);
            this.btnBackUpDataBase.Name = "btnBackUpDataBase";
            this.btnBackUpDataBase.Size = new System.Drawing.Size(96, 23);
            this.btnBackUpDataBase.TabIndex = 1;
            this.btnBackUpDataBase.Text = "点击备份";
            this.btnBackUpDataBase.Click += new System.EventHandler(this.btnBackUpDataBase_Click);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(68, 53);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(72, 14);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "数据库备份：";
            // 
            // backstageViewClientControl4
            // 
            this.backstageViewClientControl4.Controls.Add(this.bindZtoElecUserInfo1);
            this.backstageViewClientControl4.Location = new System.Drawing.Point(148, 0);
            this.backstageViewClientControl4.Name = "backstageViewClientControl4";
            this.backstageViewClientControl4.Size = new System.Drawing.Size(495, 538);
            this.backstageViewClientControl4.TabIndex = 3;
            // 
            // bindZtoElecUserInfo1
            // 
            this.bindZtoElecUserInfo1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bindZtoElecUserInfo1.Location = new System.Drawing.Point(0, 0);
            this.bindZtoElecUserInfo1.Name = "bindZtoElecUserInfo1";
            this.bindZtoElecUserInfo1.Size = new System.Drawing.Size(495, 538);
            this.bindZtoElecUserInfo1.TabIndex = 0;
            // 
            // backstageViewClientControl5
            // 
            this.backstageViewClientControl5.Controls.Add(this.groupControl1);
            this.backstageViewClientControl5.Location = new System.Drawing.Point(148, 0);
            this.backstageViewClientControl5.Name = "backstageViewClientControl5";
            this.backstageViewClientControl5.Size = new System.Drawing.Size(495, 538);
            this.backstageViewClientControl5.TabIndex = 4;
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.btnOpenBaseInfo);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.LookAndFeel.SkinName = "Office 2013";
            this.groupControl1.LookAndFeel.UseDefaultLookAndFeel = false;
            this.groupControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(495, 538);
            this.groupControl1.TabIndex = 1;
            this.groupControl1.Text = "基础资料设置";
            // 
            // btnOpenBaseInfo
            // 
            this.btnOpenBaseInfo.Location = new System.Drawing.Point(152, 49);
            this.btnOpenBaseInfo.Name = "btnOpenBaseInfo";
            this.btnOpenBaseInfo.Size = new System.Drawing.Size(96, 23);
            this.btnOpenBaseInfo.TabIndex = 1;
            this.btnOpenBaseInfo.Text = "开始";
            this.btnOpenBaseInfo.Click += new System.EventHandler(this.btnOpenBaseInfo_Click);
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(68, 53);
            this.labelControl2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(84, 14);
            this.labelControl2.TabIndex = 0;
            this.labelControl2.Text = "同步基础资料：";
            // 
            // backstageViewTabItemPrinter
            // 
            this.backstageViewTabItemPrinter.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.backstageViewTabItemPrinter.Appearance.Options.UseFont = true;
            this.backstageViewTabItemPrinter.AppearanceHover.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.backstageViewTabItemPrinter.AppearanceHover.Options.UseFont = true;
            this.backstageViewTabItemPrinter.AppearanceSelected.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.backstageViewTabItemPrinter.AppearanceSelected.Options.UseFont = true;
            this.backstageViewTabItemPrinter.Caption = "打印机";
            this.backstageViewTabItemPrinter.ContentControl = this.backstageViewClientControl1;
            this.backstageViewTabItemPrinter.Name = "backstageViewTabItemPrinter";
            this.backstageViewTabItemPrinter.Selected = false;
            // 
            // backstageViewTabItemTemplate
            // 
            this.backstageViewTabItemTemplate.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.backstageViewTabItemTemplate.Appearance.Options.UseFont = true;
            this.backstageViewTabItemTemplate.AppearanceDisabled.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.backstageViewTabItemTemplate.AppearanceDisabled.Options.UseFont = true;
            this.backstageViewTabItemTemplate.AppearanceHover.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.backstageViewTabItemTemplate.AppearanceHover.Options.UseFont = true;
            this.backstageViewTabItemTemplate.AppearanceSelected.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.backstageViewTabItemTemplate.AppearanceSelected.Options.UseFont = true;
            this.backstageViewTabItemTemplate.Caption = "打印模板";
            this.backstageViewTabItemTemplate.ContentControl = this.backstageViewClientControl2;
            this.backstageViewTabItemTemplate.Name = "backstageViewTabItemTemplate";
            this.backstageViewTabItemTemplate.Selected = false;
            // 
            // backstageViewTabItem2
            // 
            this.backstageViewTabItem2.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.backstageViewTabItem2.Appearance.Options.UseFont = true;
            this.backstageViewTabItem2.AppearanceDisabled.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.backstageViewTabItem2.AppearanceDisabled.Options.UseFont = true;
            this.backstageViewTabItem2.AppearanceHover.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.backstageViewTabItem2.AppearanceHover.Options.UseFont = true;
            this.backstageViewTabItem2.AppearanceSelected.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.backstageViewTabItem2.AppearanceSelected.Options.UseFont = true;
            this.backstageViewTabItem2.Caption = "备份";
            this.backstageViewTabItem2.ContentControl = this.backstageViewClientControl3;
            this.backstageViewTabItem2.Name = "backstageViewTabItem2";
            this.backstageViewTabItem2.Selected = false;
            // 
            // backstageViewTabItem3
            // 
            this.backstageViewTabItem3.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.backstageViewTabItem3.Appearance.Options.UseFont = true;
            this.backstageViewTabItem3.AppearanceDisabled.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.backstageViewTabItem3.AppearanceDisabled.Options.UseFont = true;
            this.backstageViewTabItem3.AppearanceHover.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.backstageViewTabItem3.AppearanceHover.Options.UseFont = true;
            this.backstageViewTabItem3.AppearanceSelected.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.backstageViewTabItem3.AppearanceSelected.Options.UseFont = true;
            this.backstageViewTabItem3.Caption = "申通电子面单账号";
            this.backstageViewTabItem3.ContentControl = this.backstageViewClientControl4;
            this.backstageViewTabItem3.Name = "backstageViewTabItem3";
            this.backstageViewTabItem3.Selected = false;
            // 
            // bvBaseInfo
            // 
            this.bvBaseInfo.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bvBaseInfo.Appearance.Options.UseFont = true;
            this.bvBaseInfo.AppearanceSelected.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bvBaseInfo.AppearanceSelected.Options.UseFont = true;
            this.bvBaseInfo.Caption = "基础资料";
            this.bvBaseInfo.ContentControl = this.backstageViewClientControl5;
            this.bvBaseInfo.Name = "bvBaseInfo";
            this.bvBaseInfo.Selected = false;
            // 
            // backstageViewTabItem1
            // 
            this.backstageViewTabItem1.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.backstageViewTabItem1.Appearance.Options.UseFont = true;
            this.backstageViewTabItem1.AppearanceHover.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.backstageViewTabItem1.AppearanceHover.Options.UseFont = true;
            this.backstageViewTabItem1.AppearanceSelected.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.backstageViewTabItem1.AppearanceSelected.Options.UseFont = true;
            this.backstageViewTabItem1.Caption = "打印模板";
            this.backstageViewTabItem1.ContentControl = this.backstageViewClientControl2;
            this.backstageViewTabItem1.Name = "backstageViewTabItem1";
            this.backstageViewTabItem1.Selected = false;
            // 
            // backstageViewTabItem4
            // 
            this.backstageViewTabItem4.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.backstageViewTabItem4.Appearance.Options.UseFont = true;
            this.backstageViewTabItem4.AppearanceDisabled.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.backstageViewTabItem4.AppearanceDisabled.Options.UseFont = true;
            this.backstageViewTabItem4.AppearanceHover.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.backstageViewTabItem4.AppearanceHover.Options.UseFont = true;
            this.backstageViewTabItem4.AppearanceSelected.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.backstageViewTabItem4.AppearanceSelected.Options.UseFont = true;
            this.backstageViewTabItem4.Caption = "备份";
            this.backstageViewTabItem4.ContentControl = this.backstageViewClientControl3;
            this.backstageViewTabItem4.Name = "backstageViewTabItem4";
            this.backstageViewTabItem4.Selected = false;
            // 
            // FrmSystemSetting
            // 
            this.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(647, 573);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.MaximizeBox = false;
            this.Name = "FrmSystemSetting";
            this.Text = "系统设置";
            this.Load += new System.EventHandler(this.FrmSystemSetting_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.backViewSetting.ResumeLayout(false);
            this.backstageViewClientControl1.ResumeLayout(false);
            this.backstageViewClientControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lsbPrinter)).EndInit();
            this.backstageViewClientControl2.ResumeLayout(false);
            this.backstageViewClientControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lsbTemplate)).EndInit();
            this.backstageViewClientControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcKeyBoard)).EndInit();
            this.gcKeyBoard.ResumeLayout(false);
            this.gcKeyBoard.PerformLayout();
            this.backstageViewClientControl4.ResumeLayout(false);
            this.backstageViewClientControl5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarButtonItem btnSend;
        private DevExpress.XtraBars.BarButtonItem btnQQ;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraBars.Ribbon.BackstageViewControl backViewSetting;
        private DevExpress.XtraBars.Ribbon.BackstageViewTabItem backstageViewTabItemPrinter;
        private DevExpress.XtraBars.Ribbon.BackstageViewTabItem backstageViewTabItemTemplate;
        private DevExpress.XtraBars.Ribbon.BackstageViewClientControl backstageViewClientControl2;
        private DevExpress.XtraEditors.ListBoxControl lsbTemplate;
        private DevExpress.XtraBars.Ribbon.BackstageViewClientControl backstageViewClientControl1;
        private DevExpress.XtraEditors.ListBoxControl lsbPrinter;
        private DevExpress.XtraEditors.SimpleButton btnSavePrinter;
        private System.Windows.Forms.Label lblDefaultPrinter;
        private System.Windows.Forms.Label lblDefaultPrintTemplate;
        private DevExpress.XtraEditors.SimpleButton btnSaveTemplate;
        private DevExpress.XtraBars.Ribbon.BackstageViewTabItem backstageViewTabItem1;
        private DevExpress.XtraBars.Ribbon.BackstageViewClientControl backstageViewClientControl3;
        private DevExpress.XtraBars.Ribbon.BackstageViewTabItem backstageViewTabItem2;
        private DevExpress.XtraEditors.GroupControl gcKeyBoard;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton btnBackUpDataBase;
        private DevExpress.XtraEditors.SimpleButton btnOpenBackUpFolder;
        private DevExpress.XtraBars.Ribbon.BackstageViewClientControl backstageViewClientControl4;
        private DevExpress.XtraBars.Ribbon.BackstageViewTabItem backstageViewTabItem3;
        private DevExpress.XtraBars.Ribbon.BackstageViewTabItem backstageViewTabItem4;
        private DevExpress.XtraEditors.SimpleButton btnLookPrinting;
        private UserControl.BindZtoElecUserInfo bindZtoElecUserInfo1;
        private DevExpress.XtraEditors.SimpleButton btnInitBillTemplate;
        private DevExpress.XtraBars.Ribbon.BackstageViewClientControl backstageViewClientControl5;
        private DevExpress.XtraBars.Ribbon.BackstageViewTabItem bvBaseInfo;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.SimpleButton btnOpenBaseInfo;
        private DevExpress.XtraEditors.LabelControl labelControl2;



    }
}