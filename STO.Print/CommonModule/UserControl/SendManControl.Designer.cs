namespace STO.Print.UserControl
{
    partial class SendManControl
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SendManControl));
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.btnExportSendMan = new DevExpress.XtraEditors.SimpleButton();
            this.btnChooseOk = new DevExpress.XtraEditors.SimpleButton();
            this.ckTodaySend = new DevExpress.XtraEditors.CheckEdit();
            this.lblPrintNumber = new DevExpress.XtraEditors.LabelControl();
            this.btnPrintMore = new DevExpress.XtraEditors.SimpleButton();
            this.btnImportSendMan = new DevExpress.XtraEditors.SimpleButton();
            this.btnEditSendMan = new DevExpress.XtraEditors.SimpleButton();
            this.btnDeleteSendMan = new DevExpress.XtraEditors.SimpleButton();
            this.btnAddSendMan = new DevExpress.XtraEditors.SimpleButton();
            this.btnRefreshSendMan = new DevExpress.XtraEditors.SimpleButton();
            this.cmbPrintNumber = new DevExpress.XtraEditors.SpinEdit();
            this.gcSendMan = new DevExpress.XtraGrid.GridControl();
            this.contentMenuMain = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tspCopyCellText = new System.Windows.Forms.ToolStripMenuItem();
            this.tspSelectAll = new System.Windows.Forms.ToolStripMenuItem();
            this.tspUnSelectAll = new System.Windows.Forms.ToolStripMenuItem();
            this.tspSetDefaultSendMan = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tspbtnSetFont = new System.Windows.Forms.ToolStripMenuItem();
            this.gvSendMan = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ckTodaySend.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbPrintNumber.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcSendMan)).BeginInit();
            this.contentMenuMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvSendMan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelControl3
            // 
            this.panelControl3.Controls.Add(this.btnExportSendMan);
            this.panelControl3.Controls.Add(this.btnChooseOk);
            this.panelControl3.Controls.Add(this.ckTodaySend);
            this.panelControl3.Controls.Add(this.lblPrintNumber);
            this.panelControl3.Controls.Add(this.btnPrintMore);
            this.panelControl3.Controls.Add(this.btnImportSendMan);
            this.panelControl3.Controls.Add(this.btnEditSendMan);
            this.panelControl3.Controls.Add(this.btnDeleteSendMan);
            this.panelControl3.Controls.Add(this.btnAddSendMan);
            this.panelControl3.Controls.Add(this.btnRefreshSendMan);
            this.panelControl3.Controls.Add(this.cmbPrintNumber);
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl3.Location = new System.Drawing.Point(0, 491);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(1027, 40);
            this.panelControl3.TabIndex = 8;
            // 
            // btnExportSendMan
            // 
            this.btnExportSendMan.Image = ((System.Drawing.Image)(resources.GetObject("btnExportSendMan.Image")));
            this.btnExportSendMan.Location = new System.Drawing.Point(469, 9);
            this.btnExportSendMan.Name = "btnExportSendMan";
            this.btnExportSendMan.Size = new System.Drawing.Size(91, 23);
            this.btnExportSendMan.TabIndex = 16;
            this.btnExportSendMan.Text = "导出发件人";
            this.btnExportSendMan.Click += new System.EventHandler(this.btnExportSendMan_Click);
            // 
            // btnChooseOk
            // 
            this.btnChooseOk.Image = ((System.Drawing.Image)(resources.GetObject("btnChooseOk.Image")));
            this.btnChooseOk.Location = new System.Drawing.Point(10, 9);
            this.btnChooseOk.Name = "btnChooseOk";
            this.btnChooseOk.Size = new System.Drawing.Size(80, 23);
            this.btnChooseOk.TabIndex = 15;
            this.btnChooseOk.Text = "确定选择";
            this.btnChooseOk.Click += new System.EventHandler(this.btnChooseOk_Click);
            // 
            // ckTodaySend
            // 
            this.ckTodaySend.Location = new System.Drawing.Point(765, 12);
            this.ckTodaySend.Name = "ckTodaySend";
            this.ckTodaySend.Properties.Caption = "是否今天发货（发件日期默认为今天）";
            this.ckTodaySend.Size = new System.Drawing.Size(223, 19);
            this.ckTodaySend.TabIndex = 10;
            // 
            // lblPrintNumber
            // 
            this.lblPrintNumber.Location = new System.Drawing.Point(668, 14);
            this.lblPrintNumber.Name = "lblPrintNumber";
            this.lblPrintNumber.Size = new System.Drawing.Size(36, 14);
            this.lblPrintNumber.TabIndex = 9;
            this.lblPrintNumber.Text = "份数：";
            // 
            // btnPrintMore
            // 
            this.btnPrintMore.Image = ((System.Drawing.Image)(resources.GetObject("btnPrintMore.Image")));
            this.btnPrintMore.Location = new System.Drawing.Point(566, 9);
            this.btnPrintMore.Name = "btnPrintMore";
            this.btnPrintMore.Size = new System.Drawing.Size(91, 23);
            this.btnPrintMore.TabIndex = 7;
            this.btnPrintMore.Text = "打印多份";
            this.btnPrintMore.Click += new System.EventHandler(this.btnPrintMore_Click);
            // 
            // btnImportSendMan
            // 
            this.btnImportSendMan.Image = ((System.Drawing.Image)(resources.GetObject("btnImportSendMan.Image")));
            this.btnImportSendMan.Location = new System.Drawing.Point(372, 9);
            this.btnImportSendMan.Name = "btnImportSendMan";
            this.btnImportSendMan.Size = new System.Drawing.Size(91, 23);
            this.btnImportSendMan.TabIndex = 6;
            this.btnImportSendMan.Text = "导入发件人";
            this.btnImportSendMan.Click += new System.EventHandler(this.btnImportSendMan_Click);
            // 
            // btnEditSendMan
            // 
            this.btnEditSendMan.Image = ((System.Drawing.Image)(resources.GetObject("btnEditSendMan.Image")));
            this.btnEditSendMan.Location = new System.Drawing.Point(234, 9);
            this.btnEditSendMan.Name = "btnEditSendMan";
            this.btnEditSendMan.Size = new System.Drawing.Size(63, 23);
            this.btnEditSendMan.TabIndex = 3;
            this.btnEditSendMan.Text = "编辑";
            this.btnEditSendMan.Click += new System.EventHandler(this.btnEditSendMan_Click);
            // 
            // btnDeleteSendMan
            // 
            this.btnDeleteSendMan.Image = ((System.Drawing.Image)(resources.GetObject("btnDeleteSendMan.Image")));
            this.btnDeleteSendMan.Location = new System.Drawing.Point(165, 9);
            this.btnDeleteSendMan.Name = "btnDeleteSendMan";
            this.btnDeleteSendMan.Size = new System.Drawing.Size(63, 23);
            this.btnDeleteSendMan.TabIndex = 2;
            this.btnDeleteSendMan.Text = "删除";
            this.btnDeleteSendMan.Click += new System.EventHandler(this.btnDeleteSendMan_Click);
            // 
            // btnAddSendMan
            // 
            this.btnAddSendMan.Image = ((System.Drawing.Image)(resources.GetObject("btnAddSendMan.Image")));
            this.btnAddSendMan.Location = new System.Drawing.Point(96, 9);
            this.btnAddSendMan.Name = "btnAddSendMan";
            this.btnAddSendMan.Size = new System.Drawing.Size(63, 23);
            this.btnAddSendMan.TabIndex = 1;
            this.btnAddSendMan.Text = "新增";
            this.btnAddSendMan.Click += new System.EventHandler(this.btnAddSendMan_Click);
            // 
            // btnRefreshSendMan
            // 
            this.btnRefreshSendMan.Image = ((System.Drawing.Image)(resources.GetObject("btnRefreshSendMan.Image")));
            this.btnRefreshSendMan.Location = new System.Drawing.Point(303, 9);
            this.btnRefreshSendMan.Name = "btnRefreshSendMan";
            this.btnRefreshSendMan.Size = new System.Drawing.Size(63, 23);
            this.btnRefreshSendMan.TabIndex = 0;
            this.btnRefreshSendMan.Text = "刷新";
            this.btnRefreshSendMan.Click += new System.EventHandler(this.btnRefreshSendMan_Click);
            // 
            // cmbPrintNumber
            // 
            this.cmbPrintNumber.EditValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.cmbPrintNumber.Location = new System.Drawing.Point(706, 10);
            this.cmbPrintNumber.Name = "cmbPrintNumber";
            this.cmbPrintNumber.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.cmbPrintNumber.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.cmbPrintNumber.Properties.Mask.BeepOnError = true;
            this.cmbPrintNumber.Properties.Mask.EditMask = "[1-9]+?";
            this.cmbPrintNumber.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.cmbPrintNumber.Size = new System.Drawing.Size(54, 20);
            this.cmbPrintNumber.TabIndex = 8;
            this.cmbPrintNumber.EditValueChanged += new System.EventHandler(this.cmbPrintNumber_EditValueChanged);
            // 
            // gcSendMan
            // 
            this.gcSendMan.ContextMenuStrip = this.contentMenuMain;
            this.gcSendMan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcSendMan.Location = new System.Drawing.Point(2, 2);
            this.gcSendMan.LookAndFeel.UseDefaultLookAndFeel = false;
            this.gcSendMan.MainView = this.gvSendMan;
            this.gcSendMan.Name = "gcSendMan";
            this.gcSendMan.Size = new System.Drawing.Size(1023, 487);
            this.gcSendMan.TabIndex = 7;
            this.gcSendMan.TabStop = false;
            this.gcSendMan.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvSendMan});
            // 
            // contentMenuMain
            // 
            this.contentMenuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tspCopyCellText,
            this.tspSelectAll,
            this.tspUnSelectAll,
            this.tspSetDefaultSendMan,
            this.toolStripSeparator1,
            this.tspbtnSetFont});
            this.contentMenuMain.Name = "contextMenuStrip1";
            this.contentMenuMain.Size = new System.Drawing.Size(161, 120);
            // 
            // tspCopyCellText
            // 
            this.tspCopyCellText.Image = ((System.Drawing.Image)(resources.GetObject("tspCopyCellText.Image")));
            this.tspCopyCellText.Name = "tspCopyCellText";
            this.tspCopyCellText.Size = new System.Drawing.Size(160, 22);
            this.tspCopyCellText.Text = "复制单元格内容";
            this.tspCopyCellText.Click += new System.EventHandler(this.tspCopyCellText_Click);
            // 
            // tspSelectAll
            // 
            this.tspSelectAll.Image = ((System.Drawing.Image)(resources.GetObject("tspSelectAll.Image")));
            this.tspSelectAll.Name = "tspSelectAll";
            this.tspSelectAll.Size = new System.Drawing.Size(160, 22);
            this.tspSelectAll.Text = "全选";
            this.tspSelectAll.Click += new System.EventHandler(this.tspSelectAll_Click);
            // 
            // tspUnSelectAll
            // 
            this.tspUnSelectAll.Image = ((System.Drawing.Image)(resources.GetObject("tspUnSelectAll.Image")));
            this.tspUnSelectAll.Name = "tspUnSelectAll";
            this.tspUnSelectAll.Size = new System.Drawing.Size(160, 22);
            this.tspUnSelectAll.Text = "反选";
            this.tspUnSelectAll.Click += new System.EventHandler(this.tspUnSelectAll_Click);
            // 
            // tspSetDefaultSendMan
            // 
            this.tspSetDefaultSendMan.Name = "tspSetDefaultSendMan";
            this.tspSetDefaultSendMan.Size = new System.Drawing.Size(160, 22);
            this.tspSetDefaultSendMan.Text = "设为默认发件人";
            this.tspSetDefaultSendMan.Click += new System.EventHandler(this.tspSetDefaultSendMan_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(157, 6);
            // 
            // tspbtnSetFont
            // 
            this.tspbtnSetFont.Image = ((System.Drawing.Image)(resources.GetObject("tspbtnSetFont.Image")));
            this.tspbtnSetFont.Name = "tspbtnSetFont";
            this.tspbtnSetFont.Size = new System.Drawing.Size(160, 22);
            this.tspbtnSetFont.Text = "设置表格字体";
            this.tspbtnSetFont.Click += new System.EventHandler(this.tspbtnSetFont_Click);
            // 
            // gvSendMan
            // 
            this.gvSendMan.GridControl = this.gcSendMan;
            this.gvSendMan.IndicatorWidth = 50;
            this.gvSendMan.Name = "gvSendMan";
            this.gvSendMan.OptionsBehavior.Editable = false;
            this.gvSendMan.OptionsPrint.AutoWidth = false;
            this.gvSendMan.OptionsSelection.MultiSelect = true;
            this.gvSendMan.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.gvSendMan.OptionsView.ShowFooter = true;
            this.gvSendMan.OptionsView.ShowGroupPanel = false;
            this.gvSendMan.CustomDrawColumnHeader += new DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventHandler(this.gvSendMan_CustomDrawColumnHeader);
            this.gvSendMan.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.gvSendMan_CustomDrawRowIndicator);
            this.gvSendMan.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.gvSendMan_RowCellStyle);
            this.gvSendMan.SelectionChanged += new DevExpress.Data.SelectionChangedEventHandler(this.gvSendMan_SelectionChanged);
            this.gvSendMan.MouseUp += new System.Windows.Forms.MouseEventHandler(this.gvSendMan_MouseUp);
            this.gvSendMan.ValidatingEditor += new DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventHandler(this.gvSendMan_ValidatingEditor);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.gcSendMan);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1027, 491);
            this.panelControl1.TabIndex = 9;
            // 
            // SendManControl
            // 
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.panelControl3);
            this.Name = "SendManControl";
            this.Size = new System.Drawing.Size(1027, 531);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            this.panelControl3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ckTodaySend.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbPrintNumber.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcSendMan)).EndInit();
            this.contentMenuMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvSendMan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraEditors.SimpleButton btnEditSendMan;
        private DevExpress.XtraEditors.SimpleButton btnDeleteSendMan;
        private DevExpress.XtraEditors.SimpleButton btnAddSendMan;
        public DevExpress.XtraGrid.GridControl gcSendMan;
        private DevExpress.XtraGrid.Views.Grid.GridView gvSendMan;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton btnImportSendMan;
        private DevExpress.XtraEditors.SimpleButton btnPrintMore;
        private DevExpress.XtraEditors.LabelControl lblPrintNumber;
        public DevExpress.XtraEditors.SimpleButton btnRefreshSendMan;
        private DevExpress.XtraEditors.SpinEdit cmbPrintNumber;
        private DevExpress.XtraEditors.CheckEdit ckTodaySend;
        private System.Windows.Forms.ContextMenuStrip contentMenuMain;
        private System.Windows.Forms.ToolStripMenuItem tspCopyCellText;
        private System.Windows.Forms.ToolStripMenuItem tspSelectAll;
        private System.Windows.Forms.ToolStripMenuItem tspUnSelectAll;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem tspbtnSetFont;
        private DevExpress.XtraEditors.SimpleButton btnChooseOk;
        private System.Windows.Forms.ToolStripMenuItem tspSetDefaultSendMan;
        private DevExpress.XtraEditors.SimpleButton btnExportSendMan;

    }
}
