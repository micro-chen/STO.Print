namespace STO.Print
{
    partial class FrmCancelRecord
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCancelRecord));
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.gridViewBills = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridControlBills = new DevExpress.XtraGrid.GridControl();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.barStartDate = new DevExpress.XtraBars.BarEditItem();
            this.txtStartDate = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.barStaticItem2 = new DevExpress.XtraBars.BarStaticItem();
            this.barEndDate = new DevExpress.XtraBars.BarEditItem();
            this.txtEndDate = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.btnSearch = new DevExpress.XtraBars.BarButtonItem();
            this.barSubItem1 = new DevExpress.XtraBars.BarSubItem();
            this.btnExport2003Excel = new DevExpress.XtraBars.BarButtonItem();
            this.btnExport2007Excel = new DevExpress.XtraBars.BarButtonItem();
            this.btnExportCSV = new DevExpress.XtraBars.BarButtonItem();
            this.repositoryItemMemoEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.btnImportExcel = new DevExpress.XtraBars.BarButtonItem();
            this.btnImportFreeExcel = new DevExpress.XtraBars.BarButtonItem();
            this.barManager1 = new DevExpress.XtraBars.BarManager();
            this.barStaticItem1 = new DevExpress.XtraBars.BarStaticItem();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewBills)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlBills)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtStartDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtStartDate.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEndDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEndDate.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            this.SuspendLayout();
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 31);
            this.barDockControlLeft.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 723);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1078, 31);
            this.barDockControlRight.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 723);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 754);
            this.barDockControlBottom.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.barDockControlBottom.Size = new System.Drawing.Size(1078, 0);
            // 
            // gridViewBills
            // 
            this.gridViewBills.Appearance.FocusedRow.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridViewBills.Appearance.FocusedRow.Options.UseFont = true;
            this.gridViewBills.GridControl = this.gridControlBills;
            this.gridViewBills.IndicatorWidth = 50;
            this.gridViewBills.Name = "gridViewBills";
            this.gridViewBills.OptionsBehavior.Editable = false;
            this.gridViewBills.OptionsPrint.AutoWidth = false;
            this.gridViewBills.OptionsSelection.MultiSelect = true;
            this.gridViewBills.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.gridViewBills.OptionsView.ShowFooter = true;
            this.gridViewBills.OptionsView.ShowGroupPanel = false;
            // 
            // gridControlBills
            // 
            this.gridControlBills.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlBills.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gridControlBills.Location = new System.Drawing.Point(0, 31);
            this.gridControlBills.LookAndFeel.UseDefaultLookAndFeel = false;
            this.gridControlBills.MainView = this.gridViewBills;
            this.gridControlBills.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gridControlBills.Name = "gridControlBills";
            this.gridControlBills.Size = new System.Drawing.Size(1078, 723);
            this.gridControlBills.TabIndex = 5;
            this.gridControlBills.TabStop = false;
            this.gridControlBills.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewBills});
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.barDockControlTop.Size = new System.Drawing.Size(1078, 31);
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barStartDate),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barStaticItem2, DevExpress.XtraBars.BarItemPaintStyle.Caption),
            new DevExpress.XtraBars.LinkPersistInfo(this.barEndDate),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnSearch, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barSubItem1, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "Tools";
            // 
            // barStartDate
            // 
            this.barStartDate.Caption = "开始日期";
            this.barStartDate.Edit = this.txtStartDate;
            this.barStartDate.Id = 35;
            this.barStartDate.Name = "barStartDate";
            this.barStartDate.Width = 140;
            // 
            // txtStartDate
            // 
            this.txtStartDate.AutoHeight = false;
            this.txtStartDate.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtStartDate.Name = "txtStartDate";
            this.txtStartDate.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            // 
            // barStaticItem2
            // 
            this.barStaticItem2.Caption = "到";
            this.barStaticItem2.Id = 38;
            this.barStaticItem2.Name = "barStaticItem2";
            this.barStaticItem2.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // barEndDate
            // 
            this.barEndDate.Caption = "结束日期";
            this.barEndDate.Edit = this.txtEndDate;
            this.barEndDate.Id = 36;
            this.barEndDate.Name = "barEndDate";
            this.barEndDate.Width = 144;
            // 
            // txtEndDate
            // 
            this.txtEndDate.AutoHeight = false;
            this.txtEndDate.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtEndDate.Name = "txtEndDate";
            this.txtEndDate.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            // 
            // btnSearch
            // 
            this.btnSearch.Caption = "查询";
            this.btnSearch.Glyph = ((System.Drawing.Image)(resources.GetObject("btnSearch.Glyph")));
            this.btnSearch.Id = 39;
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnSearch_ItemClick);
            // 
            // barSubItem1
            // 
            this.barSubItem1.Caption = "导出";
            this.barSubItem1.Id = 18;
            this.barSubItem1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.btnExport2003Excel),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnExport2007Excel),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnExportCSV)});
            this.barSubItem1.Name = "barSubItem1";
            // 
            // btnExport2003Excel
            // 
            this.btnExport2003Excel.Caption = "Excel(xls)";
            this.btnExport2003Excel.Glyph = ((System.Drawing.Image)(resources.GetObject("btnExport2003Excel.Glyph")));
            this.btnExport2003Excel.Id = 19;
            this.btnExport2003Excel.Name = "btnExport2003Excel";
            this.btnExport2003Excel.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnExport2003Excel_ItemClick);
            // 
            // btnExport2007Excel
            // 
            this.btnExport2007Excel.Caption = "Excel(xlsx)";
            this.btnExport2007Excel.Glyph = ((System.Drawing.Image)(resources.GetObject("btnExport2007Excel.Glyph")));
            this.btnExport2007Excel.Id = 20;
            this.btnExport2007Excel.Name = "btnExport2007Excel";
            this.btnExport2007Excel.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnExport2007Excel_ItemClick);
            // 
            // btnExportCSV
            // 
            this.btnExportCSV.Caption = "CSV";
            this.btnExportCSV.Glyph = ((System.Drawing.Image)(resources.GetObject("btnExportCSV.Glyph")));
            this.btnExportCSV.Id = 21;
            this.btnExportCSV.Name = "btnExportCSV";
            this.btnExportCSV.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnExportCSV_ItemClick);
            // 
            // repositoryItemMemoEdit1
            // 
            this.repositoryItemMemoEdit1.Name = "repositoryItemMemoEdit1";
            // 
            // btnImportExcel
            // 
            this.btnImportExcel.Caption = "导入指定Excel";
            this.btnImportExcel.Glyph = ((System.Drawing.Image)(resources.GetObject("btnImportExcel.Glyph")));
            this.btnImportExcel.Id = 16;
            this.btnImportExcel.Name = "btnImportExcel";
            // 
            // btnImportFreeExcel
            // 
            this.btnImportFreeExcel.Caption = "导入任意Excel";
            this.btnImportFreeExcel.Glyph = ((System.Drawing.Image)(resources.GetObject("btnImportFreeExcel.Glyph")));
            this.btnImportFreeExcel.Id = 17;
            this.btnImportFreeExcel.Name = "btnImportFreeExcel";
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
            this.btnImportExcel,
            this.btnImportFreeExcel,
            this.barSubItem1,
            this.btnExport2003Excel,
            this.btnExport2007Excel,
            this.btnExportCSV,
            this.barStartDate,
            this.barEndDate,
            this.barStaticItem1,
            this.barStaticItem2,
            this.btnSearch});
            this.barManager1.MaxItemId = 43;
            this.barManager1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.txtStartDate,
            this.txtEndDate,
            this.repositoryItemTextEdit1,
            this.repositoryItemMemoEdit1});
            // 
            // barStaticItem1
            // 
            this.barStaticItem1.Caption = "到";
            this.barStaticItem1.Id = 37;
            this.barStaticItem1.Name = "barStaticItem1";
            this.barStaticItem1.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // repositoryItemTextEdit1
            // 
            this.repositoryItemTextEdit1.AutoHeight = false;
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            // 
            // FrmCancelRecord
            // 
            this.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1078, 754);
            this.Controls.Add(this.gridControlBills);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.Name = "FrmCancelRecord";
            this.Text = "取消订单回收站";
            this.Load += new System.EventHandler(this.FrmPrintHistoryRecord_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridViewBills)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlBills)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtStartDate.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtStartDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEndDate.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEndDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewBills;
        public DevExpress.XtraGrid.GridControl gridControlBills;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarButtonItem btnImportExcel;
        private DevExpress.XtraBars.BarButtonItem btnImportFreeExcel;
        private DevExpress.XtraBars.BarSubItem barSubItem1;
        private DevExpress.XtraBars.BarButtonItem btnExport2003Excel;
        private DevExpress.XtraBars.BarButtonItem btnExport2007Excel;
        private DevExpress.XtraBars.BarButtonItem btnExportCSV;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.BarEditItem barStartDate;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit txtStartDate;
        private DevExpress.XtraBars.BarEditItem barEndDate;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit txtEndDate;
        private DevExpress.XtraBars.BarStaticItem barStaticItem2;
        private DevExpress.XtraBars.BarStaticItem barStaticItem1;
        private DevExpress.XtraBars.BarButtonItem btnSearch;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit1;
    }
}