namespace STO.Print
{
    partial class FrmImportExcelByTaoBao
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
            DevExpress.Utils.SuperToolTip superToolTip1 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem1 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem1 = new DevExpress.Utils.ToolTipItem();
            DevExpress.Utils.SuperToolTip superToolTip2 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem2 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem2 = new DevExpress.Utils.ToolTipItem();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmImportExcelByTaoBao));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnCopyExcel = new DevExpress.XtraEditors.SimpleButton();
            this.btnOpenFile = new DevExpress.XtraEditors.SimpleButton();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnOpenExcel = new DevExpress.XtraEditors.SimpleButton();
            this.txtFileFullPath = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.lblDownload = new DevExpress.XtraEditors.LabelControl();
            this.ckTodaySend = new DevExpress.XtraEditors.CheckEdit();
            this.gridControlImportExcel = new DevExpress.XtraGrid.GridControl();
            this.gridViewImportExcel = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.splashScreenManagerImportExcel = new DevExpress.XtraSplashScreen.SplashScreenManager(this, typeof(global::STO.Print.FrmWait), true, true);
            this.ckUserDefaultSendMan = new DevExpress.XtraEditors.CheckEdit();
            this.ckGetServerPrintMark = new DevExpress.XtraEditors.CheckEdit();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.lblTaoBaoEducation = new DevExpress.XtraEditors.LabelControl();
            this.ckChooseSendMan = new DevExpress.XtraEditors.CheckEdit();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.marqueeProgressBarControl1 = new DevExpress.XtraEditors.MarqueeProgressBarControl();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtFileFullPath.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckTodaySend.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlImportExcel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewImportExcel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckUserDefaultSendMan.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckGetServerPrintMark.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ckChooseSendMan.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.marqueeProgressBarControl1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btnCopyExcel);
            this.panel1.Controls.Add(this.btnOpenFile);
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Controls.Add(this.btnOpenExcel);
            this.panel1.Controls.Add(this.txtFileFullPath);
            this.panel1.Controls.Add(this.labelControl4);
            this.panel1.Controls.Add(this.lblDownload);
            this.panel1.Location = new System.Drawing.Point(4, 35);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(999, 47);
            this.panel1.TabIndex = 7;
            // 
            // btnCopyExcel
            // 
            this.btnCopyExcel.Location = new System.Drawing.Point(803, 8);
            this.btnCopyExcel.Name = "btnCopyExcel";
            this.btnCopyExcel.Size = new System.Drawing.Size(67, 26);
            this.btnCopyExcel.TabIndex = 188;
            this.btnCopyExcel.Text = "复制Excel";
            this.btnCopyExcel.Click += new System.EventHandler(this.btnCopyExcel_Click);
            // 
            // btnOpenFile
            // 
            this.btnOpenFile.Location = new System.Drawing.Point(730, 8);
            this.btnOpenFile.Name = "btnOpenFile";
            this.btnOpenFile.Size = new System.Drawing.Size(67, 26);
            this.btnOpenFile.TabIndex = 187;
            this.btnOpenFile.Text = "打开Excel";
            this.btnOpenFile.Click += new System.EventHandler(this.btnOpenFile_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(633, 8);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(91, 26);
            this.btnSave.TabIndex = 186;
            this.btnSave.Text = "导入运单(Q)";
            this.btnSave.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnOpenExcel
            // 
            this.btnOpenExcel.Location = new System.Drawing.Point(558, 8);
            this.btnOpenExcel.Name = "btnOpenExcel";
            this.btnOpenExcel.Size = new System.Drawing.Size(69, 26);
            this.btnOpenExcel.TabIndex = 184;
            this.btnOpenExcel.Text = "浏览(O)";
            this.btnOpenExcel.Click += new System.EventHandler(this.btnOpenExcel_Click);
            // 
            // txtFileFullPath
            // 
            this.txtFileFullPath.Location = new System.Drawing.Point(88, 12);
            this.txtFileFullPath.Name = "txtFileFullPath";
            this.txtFileFullPath.Properties.Appearance.BackColor = System.Drawing.Color.LemonChiffon;
            this.txtFileFullPath.Properties.Appearance.Options.UseBackColor = true;
            this.txtFileFullPath.Properties.MaxLength = 50;
            this.txtFileFullPath.Size = new System.Drawing.Size(455, 20);
            this.txtFileFullPath.TabIndex = 22;
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl4.Location = new System.Drawing.Point(6, 15);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(76, 14);
            this.labelControl4.TabIndex = 4;
            this.labelControl4.Text = "Excel数据文件";
            // 
            // lblDownload
            // 
            this.lblDownload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDownload.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblDownload.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.lblDownload.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblDownload.Location = new System.Drawing.Point(878, 14);
            this.lblDownload.Name = "lblDownload";
            this.lblDownload.Size = new System.Drawing.Size(108, 14);
            toolTipTitleItem1.Text = "系统消息";
            toolTipItem1.LeftIndent = 6;
            toolTipItem1.Text = "下载运单导入模板，按要求进行数据填写";
            superToolTip1.Items.Add(toolTipTitleItem1);
            superToolTip1.Items.Add(toolTipItem1);
            this.lblDownload.SuperTip = superToolTip1;
            this.lblDownload.TabIndex = 6;
            this.lblDownload.Text = "下载Excel导入模板";
            this.lblDownload.Click += new System.EventHandler(this.lblDownload_Click);
            // 
            // ckTodaySend
            // 
            this.ckTodaySend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ckTodaySend.Location = new System.Drawing.Point(514, 9);
            this.ckTodaySend.Name = "ckTodaySend";
            this.ckTodaySend.Properties.Caption = "是否今天发货（发件日期默认为今天）";
            this.ckTodaySend.Size = new System.Drawing.Size(222, 19);
            this.ckTodaySend.TabIndex = 188;
            this.ckTodaySend.CheckedChanged += new System.EventHandler(this.ckTodaySend_CheckedChanged);
            // 
            // gridControlImportExcel
            // 
            this.gridControlImportExcel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlImportExcel.Location = new System.Drawing.Point(2, 2);
            this.gridControlImportExcel.MainView = this.gridViewImportExcel;
            this.gridControlImportExcel.Name = "gridControlImportExcel";
            this.gridControlImportExcel.Size = new System.Drawing.Size(1000, 586);
            this.gridControlImportExcel.TabIndex = 8;
            this.gridControlImportExcel.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewImportExcel});
            // 
            // gridViewImportExcel
            // 
            this.gridViewImportExcel.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridViewImportExcel.GridControl = this.gridControlImportExcel;
            this.gridViewImportExcel.IndicatorWidth = 50;
            this.gridViewImportExcel.Name = "gridViewImportExcel";
            this.gridViewImportExcel.OptionsBehavior.Editable = false;
            this.gridViewImportExcel.OptionsCustomization.AllowFilter = false;
            this.gridViewImportExcel.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridViewImportExcel.OptionsSelection.MultiSelect = true;
            this.gridViewImportExcel.OptionsView.ColumnAutoWidth = false;
            this.gridViewImportExcel.OptionsView.ShowFooter = true;
            this.gridViewImportExcel.OptionsView.ShowGroupPanel = false;
            this.gridViewImportExcel.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.gridViewImportExcel_CustomDrawRowIndicator);
            // 
            // ckUserDefaultSendMan
            // 
            this.ckUserDefaultSendMan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ckUserDefaultSendMan.EditValue = true;
            this.ckUserDefaultSendMan.Location = new System.Drawing.Point(742, 9);
            this.ckUserDefaultSendMan.Name = "ckUserDefaultSendMan";
            this.ckUserDefaultSendMan.Properties.Caption = "使用系统默认发件人";
            this.ckUserDefaultSendMan.Size = new System.Drawing.Size(136, 19);
            this.ckUserDefaultSendMan.TabIndex = 189;
            this.ckUserDefaultSendMan.CheckedChanged += new System.EventHandler(this.ckUserDefaultSendMan_CheckedChanged);
            // 
            // ckGetServerPrintMark
            // 
            this.ckGetServerPrintMark.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ckGetServerPrintMark.EditValue = true;
            this.ckGetServerPrintMark.Location = new System.Drawing.Point(871, 9);
            this.ckGetServerPrintMark.Name = "ckGetServerPrintMark";
            this.ckGetServerPrintMark.Properties.Caption = "联网获取申通大头笔";
            this.ckGetServerPrintMark.Size = new System.Drawing.Size(132, 19);
            this.ckGetServerPrintMark.TabIndex = 190;
            this.ckGetServerPrintMark.CheckedChanged += new System.EventHandler(this.ckGetServerPrintMark_CheckedChanged);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.lblTaoBaoEducation);
            this.panelControl1.Controls.Add(this.ckChooseSendMan);
            this.panelControl1.Controls.Add(this.ckGetServerPrintMark);
            this.panelControl1.Controls.Add(this.ckUserDefaultSendMan);
            this.panelControl1.Controls.Add(this.panel1);
            this.panelControl1.Controls.Add(this.ckTodaySend);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1008, 88);
            this.panelControl1.TabIndex = 191;
            // 
            // lblTaoBaoEducation
            // 
            this.lblTaoBaoEducation.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblTaoBaoEducation.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.lblTaoBaoEducation.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblTaoBaoEducation.Location = new System.Drawing.Point(11, 11);
            this.lblTaoBaoEducation.Name = "lblTaoBaoEducation";
            this.lblTaoBaoEducation.Size = new System.Drawing.Size(134, 14);
            this.lblTaoBaoEducation.TabIndex = 192;
            this.lblTaoBaoEducation.Text = "如何导入淘宝订单Excel";
            this.lblTaoBaoEducation.Click += new System.EventHandler(this.lblTaoBaoEducation_Click);
            // 
            // ckChooseSendMan
            // 
            this.ckChooseSendMan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ckChooseSendMan.Location = new System.Drawing.Point(372, 9);
            this.ckChooseSendMan.Name = "ckChooseSendMan";
            this.ckChooseSendMan.Properties.Caption = "从系统中选择发件人";
            this.ckChooseSendMan.Size = new System.Drawing.Size(136, 19);
            toolTipTitleItem2.Text = "提示信息";
            toolTipItem2.LeftIndent = 6;
            toolTipItem2.Text = "从发件人中选择发件人来作为打印的发件人，就不从Excel读取发件人信息了，这样也无需切换多个默认发件人了";
            superToolTip2.Items.Add(toolTipTitleItem2);
            superToolTip2.Items.Add(toolTipItem2);
            this.ckChooseSendMan.SuperTip = superToolTip2;
            this.ckChooseSendMan.TabIndex = 191;
            this.ckChooseSendMan.CheckedChanged += new System.EventHandler(this.ckChooseSendMan_CheckedChanged);
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.panelControl3);
            this.panelControl2.Controls.Add(this.marqueeProgressBarControl1);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(0, 88);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(1008, 612);
            this.panelControl2.TabIndex = 192;
            // 
            // panelControl3
            // 
            this.panelControl3.Controls.Add(this.gridControlImportExcel);
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl3.Location = new System.Drawing.Point(2, 20);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(1004, 590);
            this.panelControl3.TabIndex = 10;
            // 
            // marqueeProgressBarControl1
            // 
            this.marqueeProgressBarControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.marqueeProgressBarControl1.EditValue = "请稍后";
            this.marqueeProgressBarControl1.Location = new System.Drawing.Point(2, 2);
            this.marqueeProgressBarControl1.Name = "marqueeProgressBarControl1";
            this.marqueeProgressBarControl1.Properties.ShowTitle = true;
            this.marqueeProgressBarControl1.Properties.Stopped = true;
            this.marqueeProgressBarControl1.Size = new System.Drawing.Size(1004, 18);
            this.marqueeProgressBarControl1.TabIndex = 9;
            // 
            // FrmImportExcelByTaoBao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 700);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.Name = "FrmImportExcelByTaoBao";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "导入淘宝订单Excel";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmImportExcel_FormClosing);
            this.Load += new System.EventHandler(this.FrmImportExcel_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtFileFullPath.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckTodaySend.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlImportExcel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewImportExcel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckUserDefaultSendMan.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckGetServerPrintMark.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ckChooseSendMan.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.marqueeProgressBarControl1.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnOpenExcel;
        private DevExpress.XtraEditors.TextEdit txtFileFullPath;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl lblDownload;
        public DevExpress.XtraGrid.GridControl gridControlImportExcel;
        public DevExpress.XtraGrid.Views.Grid.GridView gridViewImportExcel;
        private DevExpress.XtraEditors.SimpleButton btnOpenFile;
        private DevExpress.XtraEditors.CheckEdit ckTodaySend;
        private DevExpress.XtraEditors.CheckEdit ckUserDefaultSendMan;
        private DevExpress.XtraEditors.CheckEdit ckGetServerPrintMark;
        private DevExpress.XtraEditors.SimpleButton btnCopyExcel;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.MarqueeProgressBarControl marqueeProgressBarControl1;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraEditors.CheckEdit ckChooseSendMan;
        private DevExpress.XtraEditors.LabelControl lblTaoBaoEducation;
        private DevExpress.XtraSplashScreen.SplashScreenManager splashScreenManagerImportExcel;
    }
}