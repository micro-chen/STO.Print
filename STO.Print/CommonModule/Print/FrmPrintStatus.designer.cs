namespace STO.Print
{
    partial class FrmPrintStatus
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPrintStatus));
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.btnExportErrorExcel = new DevExpress.XtraEditors.SimpleButton();
            this.gridControlImportError = new DevExpress.XtraGrid.GridControl();
            this.gridViewImportError = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlImportError)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewImportError)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Horizontal = false;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.btnClose);
            this.splitContainerControl1.Panel1.Controls.Add(this.btnExportErrorExcel);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.gridControlImportError);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(755, 546);
            this.splitContainerControl1.SplitterPosition = 45;
            this.splitContainerControl1.TabIndex = 0;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(160, 11);
            this.btnClose.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(65, 28);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "退出";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnExportErrorExcel
            // 
            this.btnExportErrorExcel.Location = new System.Drawing.Point(10, 11);
            this.btnExportErrorExcel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnExportErrorExcel.Name = "btnExportErrorExcel";
            this.btnExportErrorExcel.Size = new System.Drawing.Size(143, 28);
            this.btnExportErrorExcel.TabIndex = 0;
            this.btnExportErrorExcel.Text = "导出错误信息Excel(E)";
            this.btnExportErrorExcel.Click += new System.EventHandler(this.btnExportErrorExcel_Click);
            // 
            // gridControlImportError
            // 
            this.gridControlImportError.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlImportError.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gridControlImportError.Location = new System.Drawing.Point(0, 0);
            this.gridControlImportError.MainView = this.gridViewImportError;
            this.gridControlImportError.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gridControlImportError.Name = "gridControlImportError";
            this.gridControlImportError.Size = new System.Drawing.Size(755, 496);
            this.gridControlImportError.TabIndex = 9;
            this.gridControlImportError.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewImportError});
            // 
            // gridViewImportError
            // 
            this.gridViewImportError.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridViewImportError.GridControl = this.gridControlImportError;
            this.gridViewImportError.Name = "gridViewImportError";
            this.gridViewImportError.OptionsCustomization.AllowFilter = false;
            this.gridViewImportError.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridViewImportError.OptionsSelection.MultiSelect = true;
            this.gridViewImportError.OptionsView.ColumnAutoWidth = false;
            this.gridViewImportError.OptionsView.ShowFooter = true;
            this.gridViewImportError.OptionsView.ShowGroupPanel = false;
            this.gridViewImportError.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.gridViewImportError_RowCellStyle);
            // 
            // timer1
            // 
            this.timer1.Interval = 2000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // FrmPrintStatus
            // 
            this.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(755, 546);
            this.Controls.Add(this.splitContainerControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.Name = "FrmPrintStatus";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "打印机任务列表";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmImportError_FormClosed);
            this.Load += new System.EventHandler(this.FrmPrintStatus_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmImportError_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlImportError)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewImportError)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraEditors.SimpleButton btnExportErrorExcel;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        public DevExpress.XtraGrid.GridControl gridControlImportError;
        public DevExpress.XtraGrid.Views.Grid.GridView gridViewImportError;
        private System.Windows.Forms.Timer timer1;
    }
}