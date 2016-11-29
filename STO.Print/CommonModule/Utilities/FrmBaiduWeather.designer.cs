namespace STO.Print
{
    partial class FrmBaiduWeather
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmBaiduWeather));
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcDayPictureUrl = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemPictureEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit();
            this.gcNightPictureUrl = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemPictureEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit();
            this.gcWeather = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcWind = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcTemperature = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemPictureEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemPictureEdit2)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemPictureEdit1,
            this.repositoryItemPictureEdit2});
            this.gridControl1.Size = new System.Drawing.Size(863, 435);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcDate,
            this.gcDayPictureUrl,
            this.gcNightPictureUrl,
            this.gcWeather,
            this.gcWind,
            this.gcTemperature});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // gcDate
            // 
            this.gcDate.Caption = "日期";
            this.gcDate.FieldName = "Date";
            this.gcDate.Name = "gcDate";
            this.gcDate.Visible = true;
            this.gcDate.VisibleIndex = 0;
            // 
            // gcDayPictureUrl
            // 
            this.gcDayPictureUrl.Caption = "白天";
            this.gcDayPictureUrl.ColumnEdit = this.repositoryItemPictureEdit1;
            this.gcDayPictureUrl.FieldName = "DayPictureUrl";
            this.gcDayPictureUrl.Name = "gcDayPictureUrl";
            this.gcDayPictureUrl.Visible = true;
            this.gcDayPictureUrl.VisibleIndex = 1;
            // 
            // repositoryItemPictureEdit1
            // 
            this.repositoryItemPictureEdit1.Name = "repositoryItemPictureEdit1";
            // 
            // gcNightPictureUrl
            // 
            this.gcNightPictureUrl.Caption = "夜晚";
            this.gcNightPictureUrl.ColumnEdit = this.repositoryItemPictureEdit2;
            this.gcNightPictureUrl.FieldName = "NightPictureUrl";
            this.gcNightPictureUrl.Name = "gcNightPictureUrl";
            this.gcNightPictureUrl.Visible = true;
            this.gcNightPictureUrl.VisibleIndex = 2;
            // 
            // repositoryItemPictureEdit2
            // 
            this.repositoryItemPictureEdit2.Name = "repositoryItemPictureEdit2";
            // 
            // gcWeather
            // 
            this.gcWeather.Caption = "天气";
            this.gcWeather.FieldName = "Weather";
            this.gcWeather.Name = "gcWeather";
            this.gcWeather.Visible = true;
            this.gcWeather.VisibleIndex = 3;
            // 
            // gcWind
            // 
            this.gcWind.Caption = "风向";
            this.gcWind.FieldName = "Wind";
            this.gcWind.Name = "gcWind";
            this.gcWind.Visible = true;
            this.gcWind.VisibleIndex = 4;
            // 
            // gcTemperature
            // 
            this.gcTemperature.Caption = "温度";
            this.gcTemperature.FieldName = "Temperature";
            this.gcTemperature.Name = "gcTemperature";
            this.gcTemperature.Visible = true;
            this.gcTemperature.VisibleIndex = 5;
            // 
            // FrmBaiduWeather
            // 
            this.Appearance.ForeColor = System.Drawing.Color.White;
            this.Appearance.Options.UseForeColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(863, 435);
            this.Controls.Add(this.gridControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(319, 473);
            this.Name = "FrmBaiduWeather";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "天气";
            this.Load += new System.EventHandler(this.FrmBaiduWeather_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemPictureEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemPictureEdit2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn gcDate;
        private DevExpress.XtraGrid.Columns.GridColumn gcDayPictureUrl;
        private DevExpress.XtraGrid.Columns.GridColumn gcNightPictureUrl;
        private DevExpress.XtraGrid.Columns.GridColumn gcWeather;
        private DevExpress.XtraGrid.Columns.GridColumn gcWind;
        private DevExpress.XtraGrid.Columns.GridColumn gcTemperature;
        private DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit repositoryItemPictureEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit repositoryItemPictureEdit2;

    }
}

