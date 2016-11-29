namespace STO.Print
{
    partial class FrmZtoSearchPrintMark
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmZtoSearchPrintMark));
            this.dgvSearchReceiveArea = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridView6 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colFULLNAME = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSIMPLESPELLING = new DevExpress.XtraGrid.Columns.GridColumn();
            this.dgvSearchSendArea = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.dbFULLNAME = new DevExpress.XtraGrid.Columns.GridColumn();
            this.dbSIMPLESPELLING = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lblSendProvinceCityCounty = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.gcGetPrintMarkByArea = new DevExpress.XtraEditors.GroupControl();
            this.btnCopyPrintMark = new DevExpress.XtraEditors.SimpleButton();
            this.txtPrintMark = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.btnCopyPrint = new DevExpress.XtraEditors.SimpleButton();
            this.txtPrint = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.tabZtoPrintMark = new DevExpress.XtraTab.XtraTabControl();
            this.tabGetPrintMarkByArea = new DevExpress.XtraTab.XtraTabPage();
            this.tabGetPrintMarkByAddress = new DevExpress.XtraTab.XtraTabPage();
            this.gcGetPrintMarkByAddress = new DevExpress.XtraEditors.GroupControl();
            this.btnCopy2 = new DevExpress.XtraEditors.SimpleButton();
            this.txtPrintMark2 = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.btnSearch2 = new DevExpress.XtraEditors.SimpleButton();
            this.btnCopy1 = new DevExpress.XtraEditors.SimpleButton();
            this.txtMark2 = new DevExpress.XtraEditors.TextEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.txtSendAddress = new DevExpress.XtraEditors.MemoEdit();
            this.txtReceiveAddress = new DevExpress.XtraEditors.MemoEdit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSearchReceiveArea.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSearchSendArea.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcGetPrintMarkByArea)).BeginInit();
            this.gcGetPrintMarkByArea.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPrintMark.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPrint.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabZtoPrintMark)).BeginInit();
            this.tabZtoPrintMark.SuspendLayout();
            this.tabGetPrintMarkByArea.SuspendLayout();
            this.tabGetPrintMarkByAddress.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcGetPrintMarkByAddress)).BeginInit();
            this.gcGetPrintMarkByAddress.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPrintMark2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMark2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSendAddress.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtReceiveAddress.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvSearchReceiveArea
            // 
            this.dgvSearchReceiveArea.Location = new System.Drawing.Point(118, 76);
            this.dgvSearchReceiveArea.Name = "dgvSearchReceiveArea";
            this.dgvSearchReceiveArea.Properties.Appearance.BackColor = System.Drawing.Color.LemonChiffon;
            this.dgvSearchReceiveArea.Properties.Appearance.Options.UseBackColor = true;
            this.dgvSearchReceiveArea.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dgvSearchReceiveArea.Properties.MaxLength = 50;
            this.dgvSearchReceiveArea.Properties.NullText = "";
            this.dgvSearchReceiveArea.Properties.NullValuePrompt = "格式：江苏省-苏州市-吴中区";
            this.dgvSearchReceiveArea.Properties.View = this.gridView6;
            this.dgvSearchReceiveArea.Size = new System.Drawing.Size(292, 20);
            this.dgvSearchReceiveArea.TabIndex = 3;
            this.dgvSearchReceiveArea.ToolTip = "收件区域来自系统数据，如无数据表示该省市区在系统无记录";
            // 
            // gridView6
            // 
            this.gridView6.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colFULLNAME,
            this.colSIMPLESPELLING});
            this.gridView6.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView6.Name = "gridView6";
            this.gridView6.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView6.OptionsView.ShowGroupPanel = false;
            // 
            // colFULLNAME
            // 
            this.colFULLNAME.Caption = "区域名称";
            this.colFULLNAME.FieldName = "FullName";
            this.colFULLNAME.Name = "colFULLNAME";
            this.colFULLNAME.Visible = true;
            this.colFULLNAME.VisibleIndex = 0;
            this.colFULLNAME.Width = 260;
            // 
            // colSIMPLESPELLING
            // 
            this.colSIMPLESPELLING.Caption = "区县简拼";
            this.colSIMPLESPELLING.FieldName = "SimpleSpelling";
            this.colSIMPLESPELLING.Name = "colSIMPLESPELLING";
            this.colSIMPLESPELLING.Visible = true;
            this.colSIMPLESPELLING.VisibleIndex = 1;
            this.colSIMPLESPELLING.Width = 124;
            // 
            // dgvSearchSendArea
            // 
            this.dgvSearchSendArea.Location = new System.Drawing.Point(118, 37);
            this.dgvSearchSendArea.Name = "dgvSearchSendArea";
            this.dgvSearchSendArea.Properties.Appearance.BackColor = System.Drawing.Color.LemonChiffon;
            this.dgvSearchSendArea.Properties.Appearance.Options.UseBackColor = true;
            this.dgvSearchSendArea.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dgvSearchSendArea.Properties.MaxLength = 50;
            this.dgvSearchSendArea.Properties.NullText = "";
            this.dgvSearchSendArea.Properties.NullValuePrompt = "格式：江苏省-苏州市-吴中区";
            this.dgvSearchSendArea.Properties.View = this.gridLookUpEdit1View;
            this.dgvSearchSendArea.Size = new System.Drawing.Size(292, 20);
            this.dgvSearchSendArea.TabIndex = 1;
            this.dgvSearchSendArea.ToolTip = "寄件区域来自系统数据，如无数据表示该省市区在系统无记录";
            // 
            // gridLookUpEdit1View
            // 
            this.gridLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.dbFULLNAME,
            this.dbSIMPLESPELLING});
            this.gridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridLookUpEdit1View.Name = "gridLookUpEdit1View";
            this.gridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // dbFULLNAME
            // 
            this.dbFULLNAME.Caption = "区域名称";
            this.dbFULLNAME.FieldName = "FullName";
            this.dbFULLNAME.Name = "dbFULLNAME";
            this.dbFULLNAME.Visible = true;
            this.dbFULLNAME.VisibleIndex = 0;
            this.dbFULLNAME.Width = 260;
            // 
            // dbSIMPLESPELLING
            // 
            this.dbSIMPLESPELLING.Caption = "区县简拼";
            this.dbSIMPLESPELLING.FieldName = "SimpleSpelling";
            this.dbSIMPLESPELLING.Name = "dbSIMPLESPELLING";
            this.dbSIMPLESPELLING.Visible = true;
            this.dbSIMPLESPELLING.VisibleIndex = 1;
            this.dbSIMPLESPELLING.Width = 124;
            // 
            // lblSendProvinceCityCounty
            // 
            this.lblSendProvinceCityCounty.Location = new System.Drawing.Point(39, 38);
            this.lblSendProvinceCityCounty.Name = "lblSendProvinceCityCounty";
            this.lblSendProvinceCityCounty.Size = new System.Drawing.Size(72, 14);
            this.lblSendProvinceCityCounty.TabIndex = 0;
            this.lblSendProvinceCityCounty.Text = "发件省市区：";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(39, 78);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(72, 14);
            this.labelControl1.TabIndex = 2;
            this.labelControl1.Text = "收件省市区：";
            // 
            // gcGetPrintMarkByArea
            // 
            this.gcGetPrintMarkByArea.Controls.Add(this.btnCopyPrintMark);
            this.gcGetPrintMarkByArea.Controls.Add(this.txtPrintMark);
            this.gcGetPrintMarkByArea.Controls.Add(this.labelControl3);
            this.gcGetPrintMarkByArea.Controls.Add(this.btnSearch);
            this.gcGetPrintMarkByArea.Controls.Add(this.btnCopyPrint);
            this.gcGetPrintMarkByArea.Controls.Add(this.txtPrint);
            this.gcGetPrintMarkByArea.Controls.Add(this.labelControl2);
            this.gcGetPrintMarkByArea.Controls.Add(this.dgvSearchReceiveArea);
            this.gcGetPrintMarkByArea.Controls.Add(this.labelControl1);
            this.gcGetPrintMarkByArea.Controls.Add(this.dgvSearchSendArea);
            this.gcGetPrintMarkByArea.Controls.Add(this.lblSendProvinceCityCounty);
            this.gcGetPrintMarkByArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcGetPrintMarkByArea.Location = new System.Drawing.Point(0, 0);
            this.gcGetPrintMarkByArea.Name = "gcGetPrintMarkByArea";
            this.gcGetPrintMarkByArea.Size = new System.Drawing.Size(481, 271);
            this.gcGetPrintMarkByArea.TabIndex = 0;
            this.gcGetPrintMarkByArea.Text = "检查大头笔";
            // 
            // btnCopyPrintMark
            // 
            this.btnCopyPrintMark.Location = new System.Drawing.Point(348, 152);
            this.btnCopyPrintMark.Name = "btnCopyPrintMark";
            this.btnCopyPrintMark.Size = new System.Drawing.Size(62, 23);
            this.btnCopyPrintMark.TabIndex = 9;
            this.btnCopyPrintMark.Text = "复制";
            this.btnCopyPrintMark.Click += new System.EventHandler(this.btnCopyPrintMark_Click);
            // 
            // txtPrintMark
            // 
            this.txtPrintMark.Location = new System.Drawing.Point(118, 154);
            this.txtPrintMark.Name = "txtPrintMark";
            this.txtPrintMark.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPrintMark.Properties.Appearance.Options.UseFont = true;
            this.txtPrintMark.Size = new System.Drawing.Size(224, 20);
            this.txtPrintMark.TabIndex = 8;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(15, 158);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(96, 14);
            this.labelControl3.TabIndex = 7;
            this.labelControl3.Text = "申通机打大头笔：";
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(118, 193);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 10;
            this.btnSearch.Text = "查询";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnCopyPrint
            // 
            this.btnCopyPrint.Location = new System.Drawing.Point(348, 115);
            this.btnCopyPrint.Name = "btnCopyPrint";
            this.btnCopyPrint.Size = new System.Drawing.Size(62, 23);
            this.btnCopyPrint.TabIndex = 6;
            this.btnCopyPrint.Text = "复制";
            this.btnCopyPrint.Click += new System.EventHandler(this.btnCopyPrint_Click);
            // 
            // txtPrint
            // 
            this.txtPrint.Location = new System.Drawing.Point(118, 115);
            this.txtPrint.Name = "txtPrint";
            this.txtPrint.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPrint.Properties.Appearance.Options.UseFont = true;
            this.txtPrint.Size = new System.Drawing.Size(224, 20);
            this.txtPrint.TabIndex = 5;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(15, 118);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(96, 14);
            this.labelControl2.TabIndex = 4;
            this.labelControl2.Text = "申通手写大头笔：";
            // 
            // tabZtoPrintMark
            // 
            this.tabZtoPrintMark.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabZtoPrintMark.Location = new System.Drawing.Point(0, 0);
            this.tabZtoPrintMark.Name = "tabZtoPrintMark";
            this.tabZtoPrintMark.SelectedTabPage = this.tabGetPrintMarkByAddress;
            this.tabZtoPrintMark.Size = new System.Drawing.Size(487, 300);
            this.tabZtoPrintMark.TabIndex = 0;
            this.tabZtoPrintMark.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tabGetPrintMarkByArea,
            this.tabGetPrintMarkByAddress});
            // 
            // tabGetPrintMarkByArea
            // 
            this.tabGetPrintMarkByArea.Controls.Add(this.gcGetPrintMarkByArea);
            this.tabGetPrintMarkByArea.Name = "tabGetPrintMarkByArea";
            this.tabGetPrintMarkByArea.Size = new System.Drawing.Size(481, 271);
            this.tabGetPrintMarkByArea.Text = "根据省市区查询";
            // 
            // tabGetPrintMarkByAddress
            // 
            this.tabGetPrintMarkByAddress.Controls.Add(this.gcGetPrintMarkByAddress);
            this.tabGetPrintMarkByAddress.Name = "tabGetPrintMarkByAddress";
            this.tabGetPrintMarkByAddress.Size = new System.Drawing.Size(481, 271);
            this.tabGetPrintMarkByAddress.Text = "根据地址查询";
            // 
            // gcGetPrintMarkByAddress
            // 
            this.gcGetPrintMarkByAddress.Controls.Add(this.txtReceiveAddress);
            this.gcGetPrintMarkByAddress.Controls.Add(this.txtSendAddress);
            this.gcGetPrintMarkByAddress.Controls.Add(this.btnCopy2);
            this.gcGetPrintMarkByAddress.Controls.Add(this.txtPrintMark2);
            this.gcGetPrintMarkByAddress.Controls.Add(this.labelControl4);
            this.gcGetPrintMarkByAddress.Controls.Add(this.btnSearch2);
            this.gcGetPrintMarkByAddress.Controls.Add(this.btnCopy1);
            this.gcGetPrintMarkByAddress.Controls.Add(this.txtMark2);
            this.gcGetPrintMarkByAddress.Controls.Add(this.labelControl5);
            this.gcGetPrintMarkByAddress.Controls.Add(this.labelControl6);
            this.gcGetPrintMarkByAddress.Controls.Add(this.labelControl7);
            this.gcGetPrintMarkByAddress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcGetPrintMarkByAddress.Location = new System.Drawing.Point(0, 0);
            this.gcGetPrintMarkByAddress.Name = "gcGetPrintMarkByAddress";
            this.gcGetPrintMarkByAddress.Size = new System.Drawing.Size(481, 271);
            this.gcGetPrintMarkByAddress.TabIndex = 0;
            this.gcGetPrintMarkByAddress.Text = "检查大头笔";
            // 
            // btnCopy2
            // 
            this.btnCopy2.Location = new System.Drawing.Point(336, 163);
            this.btnCopy2.Name = "btnCopy2";
            this.btnCopy2.Size = new System.Drawing.Size(62, 23);
            this.btnCopy2.TabIndex = 9;
            this.btnCopy2.Text = "复制";
            this.btnCopy2.Click += new System.EventHandler(this.btnCopy2_Click);
            // 
            // txtPrintMark2
            // 
            this.txtPrintMark2.Location = new System.Drawing.Point(106, 165);
            this.txtPrintMark2.Name = "txtPrintMark2";
            this.txtPrintMark2.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPrintMark2.Properties.Appearance.Options.UseFont = true;
            this.txtPrintMark2.Size = new System.Drawing.Size(224, 20);
            this.txtPrintMark2.TabIndex = 8;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(3, 167);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(96, 14);
            this.labelControl4.TabIndex = 7;
            this.labelControl4.Text = "申通机打大头笔：";
            // 
            // btnSearch2
            // 
            this.btnSearch2.Location = new System.Drawing.Point(106, 212);
            this.btnSearch2.Name = "btnSearch2";
            this.btnSearch2.Size = new System.Drawing.Size(75, 23);
            this.btnSearch2.TabIndex = 10;
            this.btnSearch2.Text = "查询";
            this.btnSearch2.Click += new System.EventHandler(this.btnSearch2_Click);
            // 
            // btnCopy1
            // 
            this.btnCopy1.Location = new System.Drawing.Point(336, 126);
            this.btnCopy1.Name = "btnCopy1";
            this.btnCopy1.Size = new System.Drawing.Size(62, 23);
            this.btnCopy1.TabIndex = 6;
            this.btnCopy1.Text = "复制";
            this.btnCopy1.Click += new System.EventHandler(this.btnCopy1_Click);
            // 
            // txtMark2
            // 
            this.txtMark2.Location = new System.Drawing.Point(106, 126);
            this.txtMark2.Name = "txtMark2";
            this.txtMark2.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMark2.Properties.Appearance.Options.UseFont = true;
            this.txtMark2.Size = new System.Drawing.Size(224, 20);
            this.txtMark2.TabIndex = 5;
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(3, 127);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(96, 14);
            this.labelControl5.TabIndex = 4;
            this.labelControl5.Text = "申通手写大头笔：";
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(27, 72);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(72, 14);
            this.labelControl6.TabIndex = 2;
            this.labelControl6.Text = "收件详细地址";
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(27, 32);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(72, 14);
            this.labelControl7.TabIndex = 0;
            this.labelControl7.Text = "发件详细地址";
            // 
            // txtSendAddress
            // 
            this.txtSendAddress.Location = new System.Drawing.Point(105, 30);
            this.txtSendAddress.Name = "txtSendAddress";
            this.txtSendAddress.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSendAddress.Properties.Appearance.Options.UseFont = true;
            this.txtSendAddress.Size = new System.Drawing.Size(369, 40);
            this.txtSendAddress.TabIndex = 1;
            // 
            // txtReceiveAddress
            // 
            this.txtReceiveAddress.Location = new System.Drawing.Point(105, 76);
            this.txtReceiveAddress.Name = "txtReceiveAddress";
            this.txtReceiveAddress.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReceiveAddress.Properties.Appearance.Options.UseFont = true;
            this.txtReceiveAddress.Size = new System.Drawing.Size(369, 40);
            this.txtReceiveAddress.TabIndex = 3;
            // 
            // FrmZtoSearchPrintMark
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(487, 300);
            this.Controls.Add(this.tabZtoPrintMark);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FrmZtoSearchPrintMark";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "检查申通大头笔";
            this.Load += new System.EventHandler(this.FrmCheckPrintMark_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSearchReceiveArea.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSearchSendArea.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcGetPrintMarkByArea)).EndInit();
            this.gcGetPrintMarkByArea.ResumeLayout(false);
            this.gcGetPrintMarkByArea.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPrintMark.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPrint.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabZtoPrintMark)).EndInit();
            this.tabZtoPrintMark.ResumeLayout(false);
            this.tabGetPrintMarkByArea.ResumeLayout(false);
            this.tabGetPrintMarkByAddress.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcGetPrintMarkByAddress)).EndInit();
            this.gcGetPrintMarkByAddress.ResumeLayout(false);
            this.gcGetPrintMarkByAddress.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPrintMark2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMark2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSendAddress.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtReceiveAddress.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GridLookUpEdit dgvSearchReceiveArea;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView6;
        private DevExpress.XtraGrid.Columns.GridColumn colFULLNAME;
        private DevExpress.XtraGrid.Columns.GridColumn colSIMPLESPELLING;
        private DevExpress.XtraEditors.GridLookUpEdit dgvSearchSendArea;
        private DevExpress.XtraGrid.Views.Grid.GridView gridLookUpEdit1View;
        private DevExpress.XtraGrid.Columns.GridColumn dbFULLNAME;
        private DevExpress.XtraGrid.Columns.GridColumn dbSIMPLESPELLING;
        private DevExpress.XtraEditors.LabelControl lblSendProvinceCityCounty;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.GroupControl gcGetPrintMarkByArea;
        private DevExpress.XtraEditors.SimpleButton btnSearch;
        private DevExpress.XtraEditors.TextEdit txtPrint;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.SimpleButton btnCopyPrint;
        private DevExpress.XtraEditors.SimpleButton btnCopyPrintMark;
        private DevExpress.XtraEditors.TextEdit txtPrintMark;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraTab.XtraTabControl tabZtoPrintMark;
        private DevExpress.XtraTab.XtraTabPage tabGetPrintMarkByAddress;
        private DevExpress.XtraTab.XtraTabPage tabGetPrintMarkByArea;
        private DevExpress.XtraEditors.GroupControl gcGetPrintMarkByAddress;
        private DevExpress.XtraEditors.SimpleButton btnCopy2;
        private DevExpress.XtraEditors.TextEdit txtPrintMark2;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.SimpleButton btnSearch2;
        private DevExpress.XtraEditors.SimpleButton btnCopy1;
        private DevExpress.XtraEditors.TextEdit txtMark2;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.MemoEdit txtSendAddress;
        private DevExpress.XtraEditors.MemoEdit txtReceiveAddress;
    }
}