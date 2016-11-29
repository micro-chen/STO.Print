namespace STO.Print.UserControl
{
    partial class ReceiveManControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReceiveManControl));
            DevExpress.Utils.SuperToolTip superToolTip1 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem1 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem1 = new DevExpress.Utils.ToolTipItem();
            DevExpress.Utils.SuperToolTip superToolTip2 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem2 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem2 = new DevExpress.Utils.ToolTipItem();
            DevExpress.Utils.SuperToolTip superToolTip3 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipItem toolTipItem3 = new DevExpress.Utils.ToolTipItem();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.btnExportReceiveMan = new DevExpress.XtraEditors.SimpleButton();
            this.btnChooseOk = new DevExpress.XtraEditors.SimpleButton();
            this.ckUserDefaultSendMan = new DevExpress.XtraEditors.CheckEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.btnPrintMore = new DevExpress.XtraEditors.SimpleButton();
            this.btnRecognitionAddress = new DevExpress.XtraEditors.SimpleButton();
            this.btnImportReceiveMan = new DevExpress.XtraEditors.SimpleButton();
            this.btnEditReceiveMan = new DevExpress.XtraEditors.SimpleButton();
            this.btnDeleteReceiveMan = new DevExpress.XtraEditors.SimpleButton();
            this.btnAddReceiveMan = new DevExpress.XtraEditors.SimpleButton();
            this.btnRefreshReceiveMan = new DevExpress.XtraEditors.SimpleButton();
            this.cmbPrintNumber = new DevExpress.XtraEditors.SpinEdit();
            this.gcReceiveMan = new DevExpress.XtraGrid.GridControl();
            this.contentMenuMain = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tspCopyCellText = new System.Windows.Forms.ToolStripMenuItem();
            this.tspSelectAll = new System.Windows.Forms.ToolStripMenuItem();
            this.tspUnSelectAll = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tspbtnSetFont = new System.Windows.Forms.ToolStripMenuItem();
            this.gvReceiveMan = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ckUserDefaultSendMan.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbPrintNumber.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcReceiveMan)).BeginInit();
            this.contentMenuMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvReceiveMan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelControl3
            // 
            this.panelControl3.Controls.Add(this.btnExportReceiveMan);
            this.panelControl3.Controls.Add(this.btnChooseOk);
            this.panelControl3.Controls.Add(this.ckUserDefaultSendMan);
            this.panelControl3.Controls.Add(this.labelControl1);
            this.panelControl3.Controls.Add(this.btnPrintMore);
            this.panelControl3.Controls.Add(this.btnRecognitionAddress);
            this.panelControl3.Controls.Add(this.btnImportReceiveMan);
            this.panelControl3.Controls.Add(this.btnEditReceiveMan);
            this.panelControl3.Controls.Add(this.btnDeleteReceiveMan);
            this.panelControl3.Controls.Add(this.btnAddReceiveMan);
            this.panelControl3.Controls.Add(this.btnRefreshReceiveMan);
            this.panelControl3.Controls.Add(this.cmbPrintNumber);
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl3.Location = new System.Drawing.Point(0, 491);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(1195, 40);
            this.panelControl3.TabIndex = 8;
            // 
            // btnExportReceiveMan
            // 
            this.btnExportReceiveMan.Image = ((System.Drawing.Image)(resources.GetObject("btnExportReceiveMan.Image")));
            this.btnExportReceiveMan.Location = new System.Drawing.Point(465, 9);
            this.btnExportReceiveMan.Name = "btnExportReceiveMan";
            this.btnExportReceiveMan.Size = new System.Drawing.Size(91, 23);
            this.btnExportReceiveMan.TabIndex = 15;
            this.btnExportReceiveMan.Text = "导出收件人";
            this.btnExportReceiveMan.Click += new System.EventHandler(this.btnExportReceiveMan_Click);
            // 
            // btnChooseOk
            // 
            this.btnChooseOk.Image = ((System.Drawing.Image)(resources.GetObject("btnChooseOk.Image")));
            this.btnChooseOk.Location = new System.Drawing.Point(6, 9);
            this.btnChooseOk.Name = "btnChooseOk";
            this.btnChooseOk.Size = new System.Drawing.Size(80, 23);
            this.btnChooseOk.TabIndex = 14;
            this.btnChooseOk.Text = "确定选择";
            this.btnChooseOk.Click += new System.EventHandler(this.btnChooseOk_Click);
            // 
            // ckUserDefaultSendMan
            // 
            this.ckUserDefaultSendMan.Location = new System.Drawing.Point(857, 11);
            this.ckUserDefaultSendMan.Name = "ckUserDefaultSendMan";
            this.ckUserDefaultSendMan.Properties.Caption = "使用默认发件人";
            this.ckUserDefaultSendMan.Size = new System.Drawing.Size(112, 19);
            toolTipTitleItem1.Text = "提示信息";
            toolTipItem1.LeftIndent = 6;
            toolTipItem1.Text = "使用默认发件人可以快速打印完整的面单信息（包含发件人和收件人）";
            superToolTip1.Items.Add(toolTipTitleItem1);
            superToolTip1.Items.Add(toolTipItem1);
            this.ckUserDefaultSendMan.SuperTip = superToolTip1;
            this.ckUserDefaultSendMan.TabIndex = 13;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(761, 13);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(36, 14);
            this.labelControl1.TabIndex = 12;
            this.labelControl1.Text = "份数：";
            // 
            // btnPrintMore
            // 
            this.btnPrintMore.Image = ((System.Drawing.Image)(resources.GetObject("btnPrintMore.Image")));
            this.btnPrintMore.Location = new System.Drawing.Point(664, 9);
            this.btnPrintMore.Name = "btnPrintMore";
            this.btnPrintMore.Size = new System.Drawing.Size(91, 23);
            this.btnPrintMore.TabIndex = 10;
            this.btnPrintMore.Text = "打印多份";
            this.btnPrintMore.Click += new System.EventHandler(this.btnPrintMore_Click);
            // 
            // btnRecognitionAddress
            // 
            this.btnRecognitionAddress.Location = new System.Drawing.Point(562, 9);
            this.btnRecognitionAddress.Name = "btnRecognitionAddress";
            this.btnRecognitionAddress.Size = new System.Drawing.Size(96, 23);
            toolTipTitleItem2.Text = "提示信息";
            toolTipItem2.LeftIndent = 6;
            toolTipItem2.Text = "适合淘宝和微商等收件人信息识别使用";
            superToolTip2.Items.Add(toolTipTitleItem2);
            superToolTip2.Items.Add(toolTipItem2);
            this.btnRecognitionAddress.SuperTip = superToolTip2;
            this.btnRecognitionAddress.TabIndex = 7;
            this.btnRecognitionAddress.Text = "智能识别收件人";
            this.btnRecognitionAddress.Click += new System.EventHandler(this.btnRecognitionAddress_Click);
            // 
            // btnImportReceiveMan
            // 
            this.btnImportReceiveMan.Image = ((System.Drawing.Image)(resources.GetObject("btnImportReceiveMan.Image")));
            this.btnImportReceiveMan.Location = new System.Drawing.Point(368, 9);
            this.btnImportReceiveMan.Name = "btnImportReceiveMan";
            this.btnImportReceiveMan.Size = new System.Drawing.Size(91, 23);
            this.btnImportReceiveMan.TabIndex = 5;
            this.btnImportReceiveMan.Text = "导入收件人";
            this.btnImportReceiveMan.Click += new System.EventHandler(this.btnImportReceiveMan_Click);
            // 
            // btnEditReceiveMan
            // 
            this.btnEditReceiveMan.Image = ((System.Drawing.Image)(resources.GetObject("btnEditReceiveMan.Image")));
            this.btnEditReceiveMan.Location = new System.Drawing.Point(230, 9);
            this.btnEditReceiveMan.Name = "btnEditReceiveMan";
            this.btnEditReceiveMan.Size = new System.Drawing.Size(63, 23);
            this.btnEditReceiveMan.TabIndex = 3;
            this.btnEditReceiveMan.Text = "编辑";
            this.btnEditReceiveMan.Click += new System.EventHandler(this.btnEditSendMan_Click);
            // 
            // btnDeleteReceiveMan
            // 
            this.btnDeleteReceiveMan.Image = ((System.Drawing.Image)(resources.GetObject("btnDeleteReceiveMan.Image")));
            this.btnDeleteReceiveMan.Location = new System.Drawing.Point(161, 9);
            this.btnDeleteReceiveMan.Name = "btnDeleteReceiveMan";
            this.btnDeleteReceiveMan.Size = new System.Drawing.Size(63, 23);
            this.btnDeleteReceiveMan.TabIndex = 2;
            this.btnDeleteReceiveMan.Text = "删除";
            this.btnDeleteReceiveMan.Click += new System.EventHandler(this.btnDeleteSendMan_Click);
            // 
            // btnAddReceiveMan
            // 
            this.btnAddReceiveMan.Image = ((System.Drawing.Image)(resources.GetObject("btnAddReceiveMan.Image")));
            this.btnAddReceiveMan.Location = new System.Drawing.Point(92, 9);
            this.btnAddReceiveMan.Name = "btnAddReceiveMan";
            this.btnAddReceiveMan.Size = new System.Drawing.Size(63, 23);
            this.btnAddReceiveMan.TabIndex = 1;
            this.btnAddReceiveMan.Text = "新增";
            this.btnAddReceiveMan.Click += new System.EventHandler(this.btnAddSendMan_Click);
            // 
            // btnRefreshReceiveMan
            // 
            this.btnRefreshReceiveMan.Image = ((System.Drawing.Image)(resources.GetObject("btnRefreshReceiveMan.Image")));
            this.btnRefreshReceiveMan.Location = new System.Drawing.Point(299, 9);
            this.btnRefreshReceiveMan.Name = "btnRefreshReceiveMan";
            this.btnRefreshReceiveMan.Size = new System.Drawing.Size(63, 23);
            this.btnRefreshReceiveMan.TabIndex = 0;
            this.btnRefreshReceiveMan.Text = "刷新";
            this.btnRefreshReceiveMan.Click += new System.EventHandler(this.btnRefreshSendMan_Click);
            // 
            // cmbPrintNumber
            // 
            this.cmbPrintNumber.EditValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.cmbPrintNumber.Location = new System.Drawing.Point(796, 10);
            this.cmbPrintNumber.Name = "cmbPrintNumber";
            this.cmbPrintNumber.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.cmbPrintNumber.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.cmbPrintNumber.Properties.Mask.BeepOnError = true;
            this.cmbPrintNumber.Properties.Mask.EditMask = "[1-9]+?";
            this.cmbPrintNumber.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.cmbPrintNumber.Size = new System.Drawing.Size(58, 20);
            toolTipItem3.Text = "支持自定义打印数量";
            superToolTip3.Items.Add(toolTipItem3);
            this.cmbPrintNumber.SuperTip = superToolTip3;
            this.cmbPrintNumber.TabIndex = 11;
            this.cmbPrintNumber.EditValueChanged += new System.EventHandler(this.cmbPrintNumber_EditValueChanged);
            // 
            // gcReceiveMan
            // 
            this.gcReceiveMan.ContextMenuStrip = this.contentMenuMain;
            this.gcReceiveMan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcReceiveMan.Location = new System.Drawing.Point(2, 2);
            this.gcReceiveMan.LookAndFeel.UseDefaultLookAndFeel = false;
            this.gcReceiveMan.MainView = this.gvReceiveMan;
            this.gcReceiveMan.Name = "gcReceiveMan";
            this.gcReceiveMan.Size = new System.Drawing.Size(1191, 487);
            this.gcReceiveMan.TabIndex = 7;
            this.gcReceiveMan.TabStop = false;
            this.gcReceiveMan.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvReceiveMan});
            // 
            // contentMenuMain
            // 
            this.contentMenuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tspCopyCellText,
            this.tspSelectAll,
            this.tspUnSelectAll,
            this.toolStripSeparator1,
            this.tspbtnSetFont});
            this.contentMenuMain.Name = "contextMenuStrip1";
            this.contentMenuMain.Size = new System.Drawing.Size(161, 98);
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
            // gvReceiveMan
            // 
            this.gvReceiveMan.GridControl = this.gcReceiveMan;
            this.gvReceiveMan.IndicatorWidth = 50;
            this.gvReceiveMan.Name = "gvReceiveMan";
            this.gvReceiveMan.OptionsBehavior.Editable = false;
            this.gvReceiveMan.OptionsPrint.AutoWidth = false;
            this.gvReceiveMan.OptionsSelection.MultiSelect = true;
            this.gvReceiveMan.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.gvReceiveMan.OptionsView.ShowFooter = true;
            this.gvReceiveMan.OptionsView.ShowGroupPanel = false;
            this.gvReceiveMan.CustomDrawColumnHeader += new DevExpress.XtraGrid.Views.Grid.ColumnHeaderCustomDrawEventHandler(this.gvSendMan_CustomDrawColumnHeader);
            this.gvReceiveMan.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.gvSendMan_CustomDrawRowIndicator);
            this.gvReceiveMan.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.gvSendMan_RowCellStyle);
            this.gvReceiveMan.SelectionChanged += new DevExpress.Data.SelectionChangedEventHandler(this.gvReceiveMan_SelectionChanged);
            this.gvReceiveMan.MouseUp += new System.Windows.Forms.MouseEventHandler(this.gvSendMan_MouseUp);
            this.gvReceiveMan.ValidatingEditor += new DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventHandler(this.gvReceiveMan_ValidatingEditor);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.gcReceiveMan);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1195, 491);
            this.panelControl1.TabIndex = 9;
            // 
            // ReceiveManControl
            // 
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.panelControl3);
            this.Name = "ReceiveManControl";
            this.Size = new System.Drawing.Size(1195, 531);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            this.panelControl3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ckUserDefaultSendMan.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbPrintNumber.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcReceiveMan)).EndInit();
            this.contentMenuMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvReceiveMan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraEditors.SimpleButton btnEditReceiveMan;
        private DevExpress.XtraEditors.SimpleButton btnDeleteReceiveMan;
        private DevExpress.XtraEditors.SimpleButton btnAddReceiveMan;
        public DevExpress.XtraGrid.GridControl gcReceiveMan;
        private DevExpress.XtraGrid.Views.Grid.GridView gvReceiveMan;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton btnImportReceiveMan;
        private DevExpress.XtraEditors.SimpleButton btnRecognitionAddress;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton btnPrintMore;
        private DevExpress.XtraEditors.CheckEdit ckUserDefaultSendMan;
        public DevExpress.XtraEditors.SimpleButton btnRefreshReceiveMan;
        private DevExpress.XtraEditors.SimpleButton btnChooseOk;
        private DevExpress.XtraEditors.SpinEdit cmbPrintNumber;
        private DevExpress.XtraEditors.SimpleButton btnExportReceiveMan;
        private System.Windows.Forms.ContextMenuStrip contentMenuMain;
        private System.Windows.Forms.ToolStripMenuItem tspCopyCellText;
        private System.Windows.Forms.ToolStripMenuItem tspSelectAll;
        private System.Windows.Forms.ToolStripMenuItem tspUnSelectAll;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem tspbtnSetFont;

    }
}
