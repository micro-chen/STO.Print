namespace STO.Print
{
    partial class FrmPrintBillChart
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
            DevExpress.XtraCharts.XYDiagram xyDiagram2 = new DevExpress.XtraCharts.XYDiagram();
            DevExpress.XtraCharts.Series series2 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.SideBySideBarSeriesLabel sideBySideBarSeriesLabel3 = new DevExpress.XtraCharts.SideBySideBarSeriesLabel();
            DevExpress.XtraCharts.SideBySideBarSeriesLabel sideBySideBarSeriesLabel4 = new DevExpress.XtraCharts.SideBySideBarSeriesLabel();
            DevExpress.XtraCharts.ChartTitle chartTitle2 = new DevExpress.XtraCharts.ChartTitle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPrintBillChart));
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.dgvEnteBill = new DevExpress.XtraGrid.GridControl();
            this.gvEnterBill = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcEnterTime = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcBillCount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.repositoryItemCheckEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.chartBill = new DevExpress.XtraCharts.ChartControl();
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.btnExportData = new DevExpress.XtraBars.BarSubItem();
            this.btnExportExcelForxls = new DevExpress.XtraBars.BarButtonItem();
            this.btnExcelForxlsx = new DevExpress.XtraBars.BarButtonItem();
            this.btnExportImage = new DevExpress.XtraBars.BarButtonItem();
            this.barStaticItem1 = new DevExpress.XtraBars.BarStaticItem();
            this.barEditItem1 = new DevExpress.XtraBars.BarEditItem();
            this.dtMin = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.barStaticItem2 = new DevExpress.XtraBars.BarStaticItem();
            this.barEditItem2 = new DevExpress.XtraBars.BarEditItem();
            this.dtMax = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.btnSearch = new DevExpress.XtraBars.BarButtonItem();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl18 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl19 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl21 = new DevExpress.XtraBars.BarDockControl();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEnteBill)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvEnterBill)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartBill)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtMin.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtMax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtMax.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Horizontal = false;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 33);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.dgvEnteBill);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.chartBill);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(1124, 468);
            this.splitContainerControl1.SplitterPosition = 176;
            this.splitContainerControl1.TabIndex = 0;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // dgvEnteBill
            // 
            this.dgvEnteBill.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvEnteBill.Location = new System.Drawing.Point(0, 0);
            this.dgvEnteBill.MainView = this.gvEnterBill;
            this.dgvEnteBill.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.dgvEnteBill.Name = "dgvEnteBill";
            this.dgvEnteBill.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1,
            this.repositoryItemCheckEdit2});
            this.dgvEnteBill.Size = new System.Drawing.Size(1124, 176);
            this.dgvEnteBill.TabIndex = 8;
            this.dgvEnteBill.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvEnterBill});
            // 
            // gvEnterBill
            // 
            this.gvEnterBill.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcEnterTime,
            this.gcBillCount});
            this.gvEnterBill.GridControl = this.dgvEnteBill;
            this.gvEnterBill.Name = "gvEnterBill";
            this.gvEnterBill.OptionsBehavior.Editable = false;
            this.gvEnterBill.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.Click;
            this.gvEnterBill.OptionsView.ShowGroupPanel = false;
            // 
            // gcEnterTime
            // 
            this.gcEnterTime.Caption = "打印日期";
            this.gcEnterTime.FieldName = "EnterTime";
            this.gcEnterTime.Name = "gcEnterTime";
            this.gcEnterTime.Visible = true;
            this.gcEnterTime.VisibleIndex = 0;
            // 
            // gcBillCount
            // 
            this.gcBillCount.Caption = "打印数量";
            this.gcBillCount.FieldName = "BillCount";
            this.gcBillCount.Name = "gcBillCount";
            this.gcBillCount.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum)});
            this.gcBillCount.Visible = true;
            this.gcBillCount.VisibleIndex = 1;
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            this.repositoryItemCheckEdit1.ReadOnly = true;
            this.repositoryItemCheckEdit1.ValueChecked = "1";
            this.repositoryItemCheckEdit1.ValueUnchecked = "0";
            // 
            // repositoryItemCheckEdit2
            // 
            this.repositoryItemCheckEdit2.AutoHeight = false;
            this.repositoryItemCheckEdit2.Name = "repositoryItemCheckEdit2";
            // 
            // chartBill
            // 
            xyDiagram2.AxisX.Range.ScrollingRange.SideMarginsEnabled = true;
            xyDiagram2.AxisX.Range.SideMarginsEnabled = true;
            xyDiagram2.AxisX.VisibleInPanesSerializable = "-1";
            xyDiagram2.AxisY.Range.ScrollingRange.SideMarginsEnabled = true;
            xyDiagram2.AxisY.Range.SideMarginsEnabled = true;
            xyDiagram2.AxisY.VisibleInPanesSerializable = "-1";
            this.chartBill.Diagram = xyDiagram2;
            this.chartBill.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartBill.EmptyChartText.Text = "运单量\r\n";
            this.chartBill.Legend.AlignmentHorizontal = DevExpress.XtraCharts.LegendAlignmentHorizontal.Center;
            this.chartBill.Legend.AlignmentVertical = DevExpress.XtraCharts.LegendAlignmentVertical.TopOutside;
            this.chartBill.Location = new System.Drawing.Point(0, 0);
            this.chartBill.Name = "chartBill";
            this.chartBill.PaletteBaseColorNumber = 1;
            series2.ArgumentDataMember = "EnterTime";
            series2.ArgumentScaleType = DevExpress.XtraCharts.ScaleType.DateTime;
            sideBySideBarSeriesLabel3.Border.Color = System.Drawing.SystemColors.ControlDark;
            sideBySideBarSeriesLabel3.LineVisible = true;
            series2.Label = sideBySideBarSeriesLabel3;
            series2.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            series2.LegendText = "日期";
            series2.Name = "s_bill_count";
            series2.ValueDataMembersSerializable = "BillCount";
            this.chartBill.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series2};
            sideBySideBarSeriesLabel4.LineVisible = true;
            this.chartBill.SeriesTemplate.Label = sideBySideBarSeriesLabel4;
            this.chartBill.SideBySideEqualBarWidth = true;
            this.chartBill.Size = new System.Drawing.Size(1124, 287);
            this.chartBill.TabIndex = 0;
            chartTitle2.Text = "";
            this.chartBill.Titles.AddRange(new DevExpress.XtraCharts.ChartTitle[] {
            chartTitle2});
            // 
            // bar2
            // 
            this.bar2.BarName = "工具";
            this.bar2.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Top;
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.FloatLocation = new System.Drawing.Point(71, 192);
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnExportData, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(this.barStaticItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.barEditItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.barStaticItem2),
            new DevExpress.XtraBars.LinkPersistInfo(this.barEditItem2),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnSearch, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar2.OptionsBar.AllowQuickCustomization = false;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "工具";
            // 
            // btnExportData
            // 
            this.btnExportData.Caption = "导出";
            this.btnExportData.Glyph = ((System.Drawing.Image)(resources.GetObject("btnExportData.Glyph")));
            this.btnExportData.Id = 9;
            this.btnExportData.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.btnExportExcelForxls),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnExcelForxlsx),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnExportImage, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.btnExportData.Name = "btnExportData";
            // 
            // btnExportExcelForxls
            // 
            this.btnExportExcelForxls.Caption = "Excel(xls)";
            this.btnExportExcelForxls.Id = 13;
            this.btnExportExcelForxls.Name = "btnExportExcelForxls";
            this.btnExportExcelForxls.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnExportExcelForxls_ItemClick);
            // 
            // btnExcelForxlsx
            // 
            this.btnExcelForxlsx.Caption = "Excel(xlsx)";
            this.btnExcelForxlsx.Id = 14;
            this.btnExcelForxlsx.Name = "btnExcelForxlsx";
            this.btnExcelForxlsx.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnExcelForxlsx_ItemClick);
            // 
            // btnExportImage
            // 
            this.btnExportImage.Caption = "统计报表图片";
            this.btnExportImage.Id = 23;
            this.btnExportImage.Name = "btnExportImage";
            this.btnExportImage.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnExportImage_ItemClick);
            // 
            // barStaticItem1
            // 
            this.barStaticItem1.Caption = "起始日期";
            this.barStaticItem1.Id = 18;
            this.barStaticItem1.Name = "barStaticItem1";
            this.barStaticItem1.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // barEditItem1
            // 
            this.barEditItem1.Edit = this.dtMin;
            this.barEditItem1.Id = 19;
            this.barEditItem1.Name = "barEditItem1";
            this.barEditItem1.Width = 100;
            // 
            // dtMin
            // 
            this.dtMin.AutoHeight = false;
            this.dtMin.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtMin.Name = "dtMin";
            this.dtMin.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            // 
            // barStaticItem2
            // 
            this.barStaticItem2.Caption = "- 截止日期：";
            this.barStaticItem2.Id = 20;
            this.barStaticItem2.Name = "barStaticItem2";
            this.barStaticItem2.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // barEditItem2
            // 
            this.barEditItem2.Edit = this.dtMax;
            this.barEditItem2.Id = 21;
            this.barEditItem2.Name = "barEditItem2";
            this.barEditItem2.Width = 100;
            // 
            // dtMax
            // 
            this.dtMax.AutoHeight = false;
            this.dtMax.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtMax.Name = "dtMax";
            this.dtMax.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            // 
            // btnSearch
            // 
            this.btnSearch.Caption = "查询";
            this.btnSearch.Glyph = ((System.Drawing.Image)(resources.GetObject("btnSearch.Glyph")));
            this.btnSearch.Id = 22;
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnSearch_ItemClick);
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar2});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControl18);
            this.barManager1.DockControls.Add(this.barDockControl19);
            this.barManager1.DockControls.Add(this.barDockControl21);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.btnExportData,
            this.btnExportExcelForxls,
            this.btnExcelForxlsx,
            this.barStaticItem1,
            this.barEditItem1,
            this.barStaticItem2,
            this.barEditItem2,
            this.btnSearch,
            this.btnExportImage});
            this.barManager1.MaxItemId = 25;
            this.barManager1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.dtMin,
            this.dtMax});
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(1124, 33);
            // 
            // barDockControl18
            // 
            this.barDockControl18.CausesValidation = false;
            this.barDockControl18.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControl18.Location = new System.Drawing.Point(0, 501);
            this.barDockControl18.Size = new System.Drawing.Size(1124, 0);
            // 
            // barDockControl19
            // 
            this.barDockControl19.CausesValidation = false;
            this.barDockControl19.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControl19.Location = new System.Drawing.Point(0, 33);
            this.barDockControl19.Size = new System.Drawing.Size(0, 468);
            // 
            // barDockControl21
            // 
            this.barDockControl21.CausesValidation = false;
            this.barDockControl21.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControl21.Location = new System.Drawing.Point(1124, 33);
            this.barDockControl21.Size = new System.Drawing.Size(0, 468);
            // 
            // FrmPrintBillChart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1124, 501);
            this.Controls.Add(this.splitContainerControl1);
            this.Controls.Add(this.barDockControl19);
            this.Controls.Add(this.barDockControl21);
            this.Controls.Add(this.barDockControl18);
            this.Controls.Add(this.barDockControlTop);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.Name = "FrmPrintBillChart";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "面单打印统计";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvEnteBill)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvEnterBill)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartBill)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtMin.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtMax.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtMax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraCharts.ChartControl chartBill;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.BarSubItem btnExportData;
        private DevExpress.XtraBars.BarButtonItem btnExportExcelForxls;
        private DevExpress.XtraBars.BarButtonItem btnExcelForxlsx;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControl18;
        private DevExpress.XtraBars.BarDockControl barDockControl19;
        private DevExpress.XtraBars.BarDockControl barDockControl21;
        private DevExpress.XtraBars.BarStaticItem barStaticItem1;
        private DevExpress.XtraBars.BarEditItem barEditItem1;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit dtMin;
        private DevExpress.XtraBars.BarStaticItem barStaticItem2;
        private DevExpress.XtraBars.BarEditItem barEditItem2;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit dtMax;
        private DevExpress.XtraBars.BarButtonItem btnSearch;
        private DevExpress.XtraBars.BarButtonItem btnExportImage;
        private DevExpress.XtraGrid.GridControl dgvEnteBill;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit2;
        private DevExpress.XtraGrid.Views.Grid.GridView gvEnterBill;
        private DevExpress.XtraGrid.Columns.GridColumn gcEnterTime;
        private DevExpress.XtraGrid.Columns.GridColumn gcBillCount;

    }
}