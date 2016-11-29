namespace STO.Print
{
    partial class FrmBackElecBillByOrderNumber
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
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.txtOrderNumbers = new DevExpress.XtraEditors.MemoEdit();
            this.gcStatus = new DevExpress.XtraGrid.GridControl();
            this.gvStatus = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panelSignInput = new DevExpress.XtraEditors.PanelControl();
            this.btnExportToExcel = new DevExpress.XtraEditors.SimpleButton();
            this.btnBackBill = new DevExpress.XtraEditors.SimpleButton();
            this.lblUploadStatus = new DevExpress.XtraEditors.LabelControl();
            this.lblTotalBillNum = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtOrderNumbers.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelSignInput)).BeginInit();
            this.panelSignInput.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.CollapsePanel = DevExpress.XtraEditors.SplitCollapsePanel.Panel1;
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.txtOrderNumbers);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.gcStatus);
            this.splitContainerControl1.Panel2.Controls.Add(this.panelSignInput);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(1054, 699);
            this.splitContainerControl1.SplitterPosition = 363;
            this.splitContainerControl1.TabIndex = 0;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // txtOrderNumbers
            // 
            this.txtOrderNumbers.AllowDrop = true;
            this.txtOrderNumbers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtOrderNumbers.EditValue = "";
            this.txtOrderNumbers.Location = new System.Drawing.Point(0, 0);
            this.txtOrderNumbers.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtOrderNumbers.Name = "txtOrderNumbers";
            this.txtOrderNumbers.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOrderNumbers.Properties.Appearance.Options.UseFont = true;
            this.txtOrderNumbers.Properties.MaxLength = 14000;
            this.txtOrderNumbers.Properties.NullValuePrompt = "根据订单号取消";
            this.txtOrderNumbers.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtOrderNumbers.Properties.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtOrderNumbers.Size = new System.Drawing.Size(363, 699);
            this.txtOrderNumbers.TabIndex = 802;
            // 
            // gcStatus
            // 
            this.gcStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcStatus.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gcStatus.Location = new System.Drawing.Point(0, 46);
            this.gcStatus.MainView = this.gvStatus;
            this.gcStatus.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gcStatus.Name = "gcStatus";
            this.gcStatus.Size = new System.Drawing.Size(686, 653);
            this.gcStatus.TabIndex = 814;
            this.gcStatus.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvStatus});
            // 
            // gvStatus
            // 
            this.gvStatus.GridControl = this.gcStatus;
            this.gvStatus.IndicatorWidth = 40;
            this.gvStatus.Name = "gvStatus";
            this.gvStatus.OptionsBehavior.Editable = false;
            this.gvStatus.OptionsView.ShowGroupPanel = false;
            // 
            // panelSignInput
            // 
            this.panelSignInput.Controls.Add(this.labelControl1);
            this.panelSignInput.Controls.Add(this.btnExportToExcel);
            this.panelSignInput.Controls.Add(this.btnBackBill);
            this.panelSignInput.Controls.Add(this.lblUploadStatus);
            this.panelSignInput.Controls.Add(this.lblTotalBillNum);
            this.panelSignInput.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSignInput.Location = new System.Drawing.Point(0, 0);
            this.panelSignInput.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelSignInput.Name = "panelSignInput";
            this.panelSignInput.Size = new System.Drawing.Size(686, 46);
            this.panelSignInput.TabIndex = 813;
            // 
            // btnExportToExcel
            // 
            this.btnExportToExcel.Location = new System.Drawing.Point(210, 7);
            this.btnExportToExcel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnExportToExcel.Name = "btnExportToExcel";
            this.btnExportToExcel.Size = new System.Drawing.Size(103, 28);
            this.btnExportToExcel.TabIndex = 813;
            this.btnExportToExcel.Text = "导出取消结果";
            this.btnExportToExcel.Click += new System.EventHandler(this.btnExportToExcel_Click);
            // 
            // btnBackBill
            // 
            this.btnBackBill.Location = new System.Drawing.Point(101, 7);
            this.btnBackBill.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnBackBill.Name = "btnBackBill";
            this.btnBackBill.Size = new System.Drawing.Size(103, 28);
            this.btnBackBill.TabIndex = 812;
            this.btnBackBill.Text = "一键取消";
            this.btnBackBill.Click += new System.EventHandler(this.btnBackBill_Click);
            // 
            // lblUploadStatus
            // 
            this.lblUploadStatus.Location = new System.Drawing.Point(10, 11);
            this.lblUploadStatus.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lblUploadStatus.Name = "lblUploadStatus";
            this.lblUploadStatus.Size = new System.Drawing.Size(60, 14);
            this.lblUploadStatus.TabIndex = 808;
            this.lblUploadStatus.Text = "总订单数：";
            // 
            // lblTotalBillNum
            // 
            this.lblTotalBillNum.Location = new System.Drawing.Point(76, 12);
            this.lblTotalBillNum.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lblTotalBillNum.Name = "lblTotalBillNum";
            this.lblTotalBillNum.Size = new System.Drawing.Size(7, 14);
            this.lblTotalBillNum.TabIndex = 809;
            this.lblTotalBillNum.Text = "0";
            // 
            // labelControl1
            // 
            this.labelControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControl1.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.labelControl1.Location = new System.Drawing.Point(421, 13);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(237, 14);
            this.labelControl1.TabIndex = 814;
            this.labelControl1.Text = "根据订单号取消，建议一次取消不超过100条";
            // 
            // FrmBackElecBillByOrderNumber
            // 
            this.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1054, 699);
            this.Controls.Add(this.splitContainerControl1);
            this.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.Name = "FrmBackElecBillByOrderNumber";
            this.Text = "取消申通电子面单";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmBackElecBillByOrderNumber_FormClosing);
            this.Load += new System.EventHandler(this.FrmBackElecBillByOrderNumber_Load);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtOrderNumbers.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelSignInput)).EndInit();
            this.panelSignInput.ResumeLayout(false);
            this.panelSignInput.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraEditors.MemoEdit txtOrderNumbers;
        private DevExpress.XtraEditors.PanelControl panelSignInput;
        public DevExpress.XtraEditors.SimpleButton btnBackBill;
        private DevExpress.XtraEditors.LabelControl lblUploadStatus;
        private DevExpress.XtraEditors.LabelControl lblTotalBillNum;
        private DevExpress.XtraGrid.GridControl gcStatus;
        private DevExpress.XtraGrid.Views.Grid.GridView gvStatus;
        public DevExpress.XtraEditors.SimpleButton btnExportToExcel;
        private DevExpress.XtraEditors.LabelControl labelControl1;


    }
}