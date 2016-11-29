namespace ZTO.Print
{
    partial class FrmImportSendManExcel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmImportSendManExcel));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnOpenFile = new DevExpress.XtraEditors.SimpleButton();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnOpenExcel = new DevExpress.XtraEditors.SimpleButton();
            this.txtFileFullPath = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.lblDownload = new DevExpress.XtraEditors.LabelControl();
            this.gridControlImportExcel = new DevExpress.XtraGrid.GridControl();
            this.gridViewImportExcel = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.splashScreenManagerImportExcel = new DevExpress.XtraSplashScreen.SplashScreenManager(this, typeof(global::ZTO.Print.FrmWait), true, true);
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtFileFullPath.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlImportExcel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewImportExcel)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btnOpenFile);
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Controls.Add(this.btnOpenExcel);
            this.panel1.Controls.Add(this.txtFileFullPath);
            this.panel1.Controls.Add(this.labelControl4);
            this.panel1.Location = new System.Drawing.Point(12, 10);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(984, 57);
            this.panel1.TabIndex = 7;
            // 
            // btnOpenFile
            // 
            this.btnOpenFile.Location = new System.Drawing.Point(832, 10);
            this.btnOpenFile.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnOpenFile.Name = "btnOpenFile";
            this.btnOpenFile.Size = new System.Drawing.Size(79, 32);
            this.btnOpenFile.TabIndex = 187;
            this.btnOpenFile.Text = "打开Excel";
            this.btnOpenFile.Click += new System.EventHandler(this.btnOpenFile_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(713, 10);
            this.btnSave.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(111, 32);
            this.btnSave.TabIndex = 186;
            this.btnSave.Text = "导入发件人(Q)";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnOpenExcel
            // 
            this.btnOpenExcel.Location = new System.Drawing.Point(619, 10);
            this.btnOpenExcel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnOpenExcel.Name = "btnOpenExcel";
            this.btnOpenExcel.Size = new System.Drawing.Size(86, 32);
            this.btnOpenExcel.TabIndex = 184;
            this.btnOpenExcel.Text = "浏览(O)";
            this.btnOpenExcel.Click += new System.EventHandler(this.btnOpenExcel_Click);
            // 
            // txtFileFullPath
            // 
            this.txtFileFullPath.Location = new System.Drawing.Point(89, 16);
            this.txtFileFullPath.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtFileFullPath.Name = "txtFileFullPath";
            this.txtFileFullPath.Properties.Appearance.BackColor = System.Drawing.Color.LemonChiffon;
            this.txtFileFullPath.Properties.Appearance.Options.UseBackColor = true;
            this.txtFileFullPath.Properties.MaxLength = 50;
            this.txtFileFullPath.Size = new System.Drawing.Size(523, 20);
            this.txtFileFullPath.TabIndex = 22;
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl4.Location = new System.Drawing.Point(4, 18);
            this.labelControl4.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(88, 14);
            this.labelControl4.TabIndex = 4;
            this.labelControl4.Text = "Excel数据文件：";
            // 
            // lblDownload
            // 
            this.lblDownload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDownload.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblDownload.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.lblDownload.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblDownload.Location = new System.Drawing.Point(1041, 15);
            this.lblDownload.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
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
            // gridControlImportExcel
            // 
            this.gridControlImportExcel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.gridControlImportExcel.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gridControlImportExcel.Location = new System.Drawing.Point(0, 75);
            this.gridControlImportExcel.MainView = this.gridViewImportExcel;
            this.gridControlImportExcel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gridControlImportExcel.Name = "gridControlImportExcel";
            this.gridControlImportExcel.Size = new System.Drawing.Size(1008, 775);
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
            // FrmImportSendManExcel
            // 
            this.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 850);
            this.Controls.Add(this.gridControlImportExcel);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblDownload);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.MinimizeBox = false;
            this.Name = "FrmImportSendManExcel";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "导入发件人";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmImportExcel_FormClosing);
            this.Load += new System.EventHandler(this.FrmImportExcel_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtFileFullPath.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlImportExcel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewImportExcel)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private DevExpress.XtraSplashScreen.SplashScreenManager splashScreenManagerImportExcel;
    }
}