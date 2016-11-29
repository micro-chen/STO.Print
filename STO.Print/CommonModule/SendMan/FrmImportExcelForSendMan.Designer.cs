namespace STO.Print
{
    partial class FrmImportExcelForSendMan
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmImportExcelForSendMan));
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.lblInfo1 = new DevExpress.XtraEditors.LabelControl();
            this.btnCopyExcel = new DevExpress.XtraEditors.SimpleButton();
            this.btnOpenFile = new DevExpress.XtraEditors.SimpleButton();
            this.btnImport = new DevExpress.XtraEditors.SimpleButton();
            this.btnOpenExcel = new DevExpress.XtraEditors.SimpleButton();
            this.txtFileFullPath = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.gridControlBills = new DevExpress.XtraGrid.GridControl();
            this.gridViewBills = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtFileFullPath.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlBills)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewBills)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Horizontal = false;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.panel1);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.gridControlBills);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(1176, 655);
            this.splitContainerControl1.SplitterPosition = 82;
            this.splitContainerControl1.TabIndex = 0;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.labelControl1);
            this.panel1.Controls.Add(this.lblInfo1);
            this.panel1.Controls.Add(this.btnCopyExcel);
            this.panel1.Controls.Add(this.btnOpenFile);
            this.panel1.Controls.Add(this.btnImport);
            this.panel1.Controls.Add(this.btnOpenExcel);
            this.panel1.Controls.Add(this.txtFileFullPath);
            this.panel1.Controls.Add(this.labelControl4);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1176, 82);
            this.panel1.TabIndex = 8;
            // 
            // labelControl1
            // 
            this.labelControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControl1.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.labelControl1.Location = new System.Drawing.Point(23, 61);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(419, 14);
            this.labelControl1.TabIndex = 190;
            this.labelControl1.Text = "2.导入只需要把姓名、手机、详细地址导入进来就可以了，其他字段可以不导入";
            // 
            // lblInfo1
            // 
            this.lblInfo1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblInfo1.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.lblInfo1.Location = new System.Drawing.Point(23, 41);
            this.lblInfo1.Name = "lblInfo1";
            this.lblInfo1.Size = new System.Drawing.Size(335, 14);
            this.lblInfo1.TabIndex = 189;
            this.lblInfo1.Text = "1.程序默认从表格的第二行开始读取数据，所以第一行作为表头";
            // 
            // btnCopyExcel
            // 
            this.btnCopyExcel.Location = new System.Drawing.Point(853, 12);
            this.btnCopyExcel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnCopyExcel.Name = "btnCopyExcel";
            this.btnCopyExcel.Size = new System.Drawing.Size(79, 24);
            this.btnCopyExcel.TabIndex = 188;
            this.btnCopyExcel.Text = "复制Excel";
            this.btnCopyExcel.Click += new System.EventHandler(this.BtnCopyExcelClick);
            // 
            // btnOpenFile
            // 
            this.btnOpenFile.Location = new System.Drawing.Point(768, 12);
            this.btnOpenFile.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnOpenFile.Name = "btnOpenFile";
            this.btnOpenFile.Size = new System.Drawing.Size(79, 24);
            this.btnOpenFile.TabIndex = 187;
            this.btnOpenFile.Text = "打开Excel";
            this.btnOpenFile.Click += new System.EventHandler(this.BtnOpenFileClick);
            // 
            // btnImport
            // 
            this.btnImport.Location = new System.Drawing.Point(683, 12);
            this.btnImport.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(79, 24);
            this.btnImport.TabIndex = 186;
            this.btnImport.Text = "导入运单(Q)";
            this.btnImport.Click += new System.EventHandler(this.BtnImportClick);
            // 
            // btnOpenExcel
            // 
            this.btnOpenExcel.Location = new System.Drawing.Point(598, 12);
            this.btnOpenExcel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnOpenExcel.Name = "btnOpenExcel";
            this.btnOpenExcel.Size = new System.Drawing.Size(79, 24);
            this.btnOpenExcel.TabIndex = 184;
            this.btnOpenExcel.Text = "浏览(O)";
            this.btnOpenExcel.Click += new System.EventHandler(this.BtnOpenExcelClick);
            // 
            // txtFileFullPath
            // 
            this.txtFileFullPath.Location = new System.Drawing.Point(101, 15);
            this.txtFileFullPath.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtFileFullPath.Name = "txtFileFullPath";
            this.txtFileFullPath.Properties.Appearance.BackColor = System.Drawing.Color.LemonChiffon;
            this.txtFileFullPath.Properties.Appearance.Options.UseBackColor = true;
            this.txtFileFullPath.Properties.MaxLength = 50;
            this.txtFileFullPath.Properties.NullValuePrompt = "请选择Excel文件，支持csv,xls,xlsx";
            this.txtFileFullPath.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtFileFullPath.Size = new System.Drawing.Size(491, 20);
            this.txtFileFullPath.TabIndex = 22;
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl4.Location = new System.Drawing.Point(19, 17);
            this.labelControl4.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(76, 14);
            this.labelControl4.TabIndex = 4;
            this.labelControl4.Text = "Excel数据文件";
            // 
            // gridControlBills
            // 
            this.gridControlBills.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlBills.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gridControlBills.Location = new System.Drawing.Point(0, 0);
            this.gridControlBills.LookAndFeel.UseDefaultLookAndFeel = false;
            this.gridControlBills.MainView = this.gridViewBills;
            this.gridControlBills.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gridControlBills.Name = "gridControlBills";
            this.gridControlBills.Size = new System.Drawing.Size(1176, 568);
            this.gridControlBills.TabIndex = 5;
            this.gridControlBills.TabStop = false;
            this.gridControlBills.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewBills});
            // 
            // gridViewBills
            // 
            this.gridViewBills.GridControl = this.gridControlBills;
            this.gridViewBills.IndicatorWidth = 50;
            this.gridViewBills.Name = "gridViewBills";
            this.gridViewBills.OptionsBehavior.Editable = false;
            this.gridViewBills.OptionsPrint.AutoWidth = false;
            this.gridViewBills.OptionsSelection.MultiSelect = true;
            this.gridViewBills.OptionsView.ShowFooter = true;
            this.gridViewBills.OptionsView.ShowGroupPanel = false;
            // 
            // FrmImportExcelForSendMan
            // 
            this.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1176, 655);
            this.Controls.Add(this.splitContainerControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.Name = "FrmImportExcelForSendMan";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "导入发件人";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmImportFreeExcelFormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtFileFullPath.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlBills)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewBills)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.SimpleButton btnOpenFile;
        private DevExpress.XtraEditors.SimpleButton btnOpenExcel;
        private DevExpress.XtraEditors.TextEdit txtFileFullPath;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.SimpleButton btnImport;
        public DevExpress.XtraGrid.GridControl gridControlBills;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewBills;
        private DevExpress.XtraEditors.SimpleButton btnCopyExcel;
        private DevExpress.XtraEditors.LabelControl lblInfo1;
        private DevExpress.XtraEditors.LabelControl labelControl1;


    }
}