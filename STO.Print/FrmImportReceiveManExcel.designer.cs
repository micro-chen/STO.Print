namespace ZTO.Print
{
    partial class FrmImportReceiveManExcel
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
            DevExpress.Utils.ToolTipItem toolTipItem1 = new DevExpress.Utils.ToolTipItem();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmImportReceiveManExcel));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnOpenFile = new DevExpress.XtraEditors.SimpleButton();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnOpenExcel = new DevExpress.XtraEditors.SimpleButton();
            this.txtFileFullPath = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.gridControlImportExcel = new DevExpress.XtraGrid.GridControl();
            this.gridViewImportExcel = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.splashScreenManagerImportExcel = new DevExpress.XtraSplashScreen.SplashScreenManager(this, typeof(global::ZTO.Print.FrmWait), true, true);
            this.cmbPrintNumber = new DevExpress.XtraEditors.SpinEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtFileFullPath.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlImportExcel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewImportExcel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbPrintNumber.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
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
            this.panel1.Location = new System.Drawing.Point(12, 46);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(937, 57);
            this.panel1.TabIndex = 7;
            // 
            // btnOpenFile
            // 
            this.btnOpenFile.Location = new System.Drawing.Point(834, 12);
            this.btnOpenFile.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnOpenFile.Name = "btnOpenFile";
            this.btnOpenFile.Size = new System.Drawing.Size(79, 32);
            this.btnOpenFile.TabIndex = 187;
            this.btnOpenFile.Text = "打开Excel";
            this.btnOpenFile.Click += new System.EventHandler(this.btnOpenFile_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(715, 12);
            this.btnSave.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(111, 32);
            this.btnSave.TabIndex = 186;
            this.btnSave.Text = "导入收件人(Q)";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnOpenExcel
            // 
            this.btnOpenExcel.Location = new System.Drawing.Point(621, 12);
            this.btnOpenExcel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnOpenExcel.Name = "btnOpenExcel";
            this.btnOpenExcel.Size = new System.Drawing.Size(86, 32);
            this.btnOpenExcel.TabIndex = 184;
            this.btnOpenExcel.Text = "浏览(O)";
            this.btnOpenExcel.Click += new System.EventHandler(this.btnOpenExcel_Click);
            // 
            // txtFileFullPath
            // 
            this.txtFileFullPath.Location = new System.Drawing.Point(102, 16);
            this.txtFileFullPath.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtFileFullPath.Name = "txtFileFullPath";
            this.txtFileFullPath.Properties.Appearance.BackColor = System.Drawing.Color.LemonChiffon;
            this.txtFileFullPath.Properties.Appearance.Options.UseBackColor = true;
            this.txtFileFullPath.Size = new System.Drawing.Size(510, 20);
            this.txtFileFullPath.TabIndex = 22;
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl4.Location = new System.Drawing.Point(13, 18);
            this.labelControl4.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(88, 14);
            this.labelControl4.TabIndex = 4;
            this.labelControl4.Text = "Excel数据文件：";
            // 
            // gridControlImportExcel
            // 
            this.gridControlImportExcel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlImportExcel.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gridControlImportExcel.Location = new System.Drawing.Point(2, 2);
            this.gridControlImportExcel.MainView = this.gridViewImportExcel;
            this.gridControlImportExcel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gridControlImportExcel.Name = "gridControlImportExcel";
            this.gridControlImportExcel.Size = new System.Drawing.Size(957, 723);
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
            // cmbPrintNumber
            // 
            this.cmbPrintNumber.EditValue = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.cmbPrintNumber.Location = new System.Drawing.Point(115, 12);
            this.cmbPrintNumber.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbPrintNumber.Name = "cmbPrintNumber";
            this.cmbPrintNumber.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.cmbPrintNumber.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.cmbPrintNumber.Properties.Mask.BeepOnError = true;
            this.cmbPrintNumber.Properties.Mask.EditMask = "[0-9]+?";
            this.cmbPrintNumber.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.cmbPrintNumber.Size = new System.Drawing.Size(58, 20);
            toolTipItem1.Text = "支持自定义打印数量";
            superToolTip1.Items.Add(toolTipItem1);
            this.cmbPrintNumber.SuperTip = superToolTip1;
            this.cmbPrintNumber.TabIndex = 12;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl1.Location = new System.Drawing.Point(13, 16);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(96, 14);
            this.labelControl1.TabIndex = 13;
            this.labelControl1.Text = "从第几行开始导入";
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.panel1);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Controls.Add(this.cmbPrintNumber);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(961, 123);
            this.panelControl1.TabIndex = 14;
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.gridControlImportExcel);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(0, 123);
            this.panelControl2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(961, 727);
            this.panelControl2.TabIndex = 15;
            // 
            // FrmImportReceiveManExcel
            // 
            this.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(961, 850);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.MinimizeBox = false;
            this.Name = "FrmImportReceiveManExcel";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "导入收件人";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmImportExcel_FormClosing);
            this.Load += new System.EventHandler(this.FrmImportExcel_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtFileFullPath.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlImportExcel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewImportExcel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbPrintNumber.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnOpenExcel;
        private DevExpress.XtraEditors.TextEdit txtFileFullPath;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        public DevExpress.XtraGrid.GridControl gridControlImportExcel;
        public DevExpress.XtraGrid.Views.Grid.GridView gridViewImportExcel;
        private DevExpress.XtraEditors.SimpleButton btnOpenFile;
        private DevExpress.XtraEditors.SpinEdit cmbPrintNumber;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraSplashScreen.SplashScreenManager splashScreenManagerImportExcel;
    }
}