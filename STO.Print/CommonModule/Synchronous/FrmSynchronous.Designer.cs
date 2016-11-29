namespace STO.Print.Synchronous
{
    partial class FrmSynchronous
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSynchronous));
            this.tabControlData = new DevExpress.XtraTab.XtraTabControl();
            this.tabSync = new DevExpress.XtraTab.XtraTabPage();
            this.btnSync = new DevExpress.XtraEditors.SimpleButton();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcIndex = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcCount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcRemark = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lblSyncTime = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.tabControlData)).BeginInit();
            this.tabControlData.SuspendLayout();
            this.tabSync.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControlData
            // 
            this.tabControlData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlData.Location = new System.Drawing.Point(0, 0);
            this.tabControlData.Name = "tabControlData";
            this.tabControlData.SelectedTabPage = this.tabSync;
            this.tabControlData.Size = new System.Drawing.Size(798, 422);
            this.tabControlData.TabIndex = 0;
            this.tabControlData.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tabSync});
            // 
            // tabSync
            // 
            this.tabSync.Controls.Add(this.btnSync);
            this.tabSync.Controls.Add(this.gridControl1);
            this.tabSync.Name = "tabSync";
            this.tabSync.Size = new System.Drawing.Size(792, 393);
            this.tabSync.Text = "同步基础资料";
            // 
            // btnSync
            // 
            this.btnSync.Location = new System.Drawing.Point(695, 353);
            this.btnSync.Name = "btnSync";
            this.btnSync.Size = new System.Drawing.Size(87, 27);
            this.btnSync.TabIndex = 1;
            this.btnSync.Text = "全部同步";
            this.btnSync.Click += new System.EventHandler(this.btnSync_Click);
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(792, 345);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Appearance.GroupPanel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView1.Appearance.GroupPanel.Options.UseFont = true;
            this.gridView1.Appearance.GroupPanel.Options.UseTextOptions = true;
            this.gridView1.Appearance.GroupPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridView1.Appearance.GroupPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcId,
            this.gcName,
            this.gcIndex,
            this.gcCount,
            this.gcRemark});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.IndicatorWidth = 40;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.gridView1_CustomDrawRowIndicator);
            this.gridView1.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.gridView_CustomDrawCell);
            this.gridView1.CustomRowCellEdit += new DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventHandler(this.gridView_CustomRowCellEdit);
            // 
            // gcId
            // 
            this.gcId.Caption = "Id";
            this.gcId.FieldName = "Id";
            this.gcId.Name = "gcId";
            this.gcId.Visible = true;
            this.gcId.VisibleIndex = 0;
            // 
            // gcName
            // 
            this.gcName.Caption = "名称";
            this.gcName.FieldName = "Name";
            this.gcName.Name = "gcName";
            this.gcName.Visible = true;
            this.gcName.VisibleIndex = 1;
            // 
            // gcIndex
            // 
            this.gcIndex.Caption = "进度";
            this.gcIndex.FieldName = "Index";
            this.gcIndex.Name = "gcIndex";
            this.gcIndex.Visible = true;
            this.gcIndex.VisibleIndex = 2;
            // 
            // gcCount
            // 
            this.gcCount.Caption = "总数";
            this.gcCount.FieldName = "Count";
            this.gcCount.Name = "gcCount";
            this.gcCount.Visible = true;
            this.gcCount.VisibleIndex = 3;
            // 
            // gcRemark
            // 
            this.gcRemark.Caption = "状态";
            this.gcRemark.FieldName = "Remark";
            this.gcRemark.Name = "gcRemark";
            this.gcRemark.Visible = true;
            this.gcRemark.VisibleIndex = 4;
            // 
            // lblSyncTime
            // 
            this.lblSyncTime.AutoSize = true;
            this.lblSyncTime.BackColor = System.Drawing.Color.White;
            this.lblSyncTime.Location = new System.Drawing.Point(14, 387);
            this.lblSyncTime.Name = "lblSyncTime";
            this.lblSyncTime.Size = new System.Drawing.Size(79, 14);
            this.lblSyncTime.TabIndex = 1;
            this.lblSyncTime.Text = "上次同步时间";
            // 
            // FrmSynchronous
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(798, 422);
            this.Controls.Add(this.lblSyncTime);
            this.Controls.Add(this.tabControlData);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmSynchronous";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "同步基础资料";
            this.Load += new System.EventHandler(this.Form_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tabControlData)).EndInit();
            this.tabControlData.ResumeLayout(false);
            this.tabSync.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraTab.XtraTabControl tabControlData;
        private DevExpress.XtraTab.XtraTabPage tabSync;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.SimpleButton btnSync;
        private DevExpress.XtraGrid.Columns.GridColumn gcId;
        private DevExpress.XtraGrid.Columns.GridColumn gcName;
        private DevExpress.XtraGrid.Columns.GridColumn gcIndex;
        private DevExpress.XtraGrid.Columns.GridColumn gcCount;
        private DevExpress.XtraGrid.Columns.GridColumn gcRemark;
        private System.Windows.Forms.Label lblSyncTime;
    }
}