namespace STO.Print
{
    partial class FrmSearchBill
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSearchBill));
            this.cmbExpress = new System.Windows.Forms.ComboBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtBillCode = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.lblInfo = new DevExpress.XtraEditors.LabelControl();
            this.btnOpenFolder = new System.Windows.Forms.Button();
            this.splitContainerControl2 = new DevExpress.XtraEditors.SplitContainerControl();
            this.gcBill = new DevExpress.XtraGrid.GridControl();
            this.gvBill = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcTime = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcContext = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.picBillBox = new DevExpress.XtraEditors.PictureEdit();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.trackPercent = new DevExpress.XtraEditors.ZoomTrackBarControl();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2)).BeginInit();
            this.splitContainerControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcBill)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvBill)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBillBox.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackPercent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackPercent.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbExpress
            // 
            this.cmbExpress.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbExpress.FormattingEnabled = true;
            this.cmbExpress.Location = new System.Drawing.Point(77, 16);
            this.cmbExpress.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbExpress.Name = "cmbExpress";
            this.cmbExpress.Size = new System.Drawing.Size(152, 25);
            this.cmbExpress.TabIndex = 0;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(463, 11);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(87, 33);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.Text = "查询";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "快递单位";
            // 
            // txtBillCode
            // 
            this.txtBillCode.Location = new System.Drawing.Point(300, 15);
            this.txtBillCode.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.txtBillCode.Name = "txtBillCode";
            this.txtBillCode.Size = new System.Drawing.Size(152, 23);
            this.txtBillCode.TabIndex = 4;
            this.txtBillCode.TextChanged += new System.EventHandler(this.txtBillCode_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(235, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = "快递单号";
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.CollapsePanel = DevExpress.XtraEditors.SplitCollapsePanel.Panel1;
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Horizontal = false;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.lblInfo);
            this.splitContainerControl1.Panel1.Controls.Add(this.btnOpenFolder);
            this.splitContainerControl1.Panel1.Controls.Add(this.txtBillCode);
            this.splitContainerControl1.Panel1.Controls.Add(this.label2);
            this.splitContainerControl1.Panel1.Controls.Add(this.cmbExpress);
            this.splitContainerControl1.Panel1.Controls.Add(this.btnSearch);
            this.splitContainerControl1.Panel1.Controls.Add(this.label1);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.splitContainerControl2);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(1259, 733);
            this.splitContainerControl1.SplitterPosition = 49;
            this.splitContainerControl1.TabIndex = 6;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // lblInfo
            // 
            this.lblInfo.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.lblInfo.Location = new System.Drawing.Point(685, 22);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(384, 14);
            this.lblInfo.TabIndex = 7;
            this.lblInfo.Text = "查申通快递必须要有申通的商家ID才可以查看跟踪记录，否则查不到的哦";
            // 
            // btnOpenFolder
            // 
            this.btnOpenFolder.Location = new System.Drawing.Point(561, 11);
            this.btnOpenFolder.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnOpenFolder.Name = "btnOpenFolder";
            this.btnOpenFolder.Size = new System.Drawing.Size(104, 33);
            this.btnOpenFolder.TabIndex = 6;
            this.btnOpenFolder.Text = "打开图片文件夹";
            this.btnOpenFolder.UseVisualStyleBackColor = true;
            this.btnOpenFolder.Click += new System.EventHandler(this.btnOpenFolder_Click);
            // 
            // splitContainerControl2
            // 
            this.splitContainerControl2.CollapsePanel = DevExpress.XtraEditors.SplitCollapsePanel.Panel1;
            this.splitContainerControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl2.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.splitContainerControl2.Name = "splitContainerControl2";
            this.splitContainerControl2.Panel1.Controls.Add(this.gcBill);
            this.splitContainerControl2.Panel1.Text = "Panel1";
            this.splitContainerControl2.Panel2.Controls.Add(this.panelControl1);
            this.splitContainerControl2.Panel2.Controls.Add(this.panelControl2);
            this.splitContainerControl2.Panel2.Text = "Panel2";
            this.splitContainerControl2.Size = new System.Drawing.Size(1259, 679);
            this.splitContainerControl2.SplitterPosition = 469;
            this.splitContainerControl2.TabIndex = 7;
            this.splitContainerControl2.Text = "splitContainerControl2";
            // 
            // gcBill
            // 
            this.gcBill.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcBill.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gcBill.Location = new System.Drawing.Point(0, 0);
            this.gcBill.MainView = this.gvBill;
            this.gcBill.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gcBill.Name = "gcBill";
            this.gcBill.Size = new System.Drawing.Size(469, 679);
            this.gcBill.TabIndex = 0;
            this.gcBill.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvBill});
            // 
            // gvBill
            // 
            this.gvBill.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcTime,
            this.gcContext});
            this.gvBill.GridControl = this.gcBill;
            this.gvBill.IndicatorWidth = 40;
            this.gvBill.Name = "gvBill";
            this.gvBill.OptionsView.ShowGroupPanel = false;
            this.gvBill.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(this.gvBill_CustomDrawRowIndicator);
            this.gvBill.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.gvBill_RowCellStyle);
            // 
            // gcTime
            // 
            this.gcTime.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gcTime.AppearanceCell.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(135)))), ((int)(((byte)(135)))), ((int)(((byte)(135)))));
            this.gcTime.AppearanceCell.Options.UseFont = true;
            this.gcTime.AppearanceCell.Options.UseForeColor = true;
            this.gcTime.AppearanceCell.Options.UseTextOptions = true;
            this.gcTime.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcTime.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gcTime.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gcTime.AppearanceHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(3)))), ((int)(((byte)(51)))));
            this.gcTime.AppearanceHeader.Options.UseFont = true;
            this.gcTime.AppearanceHeader.Options.UseForeColor = true;
            this.gcTime.AppearanceHeader.Options.UseTextOptions = true;
            this.gcTime.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcTime.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gcTime.Caption = "时间";
            this.gcTime.FieldName = "time";
            this.gcTime.Name = "gcTime";
            this.gcTime.Visible = true;
            this.gcTime.VisibleIndex = 0;
            this.gcTime.Width = 180;
            // 
            // gcContext
            // 
            this.gcContext.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gcContext.AppearanceCell.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(135)))), ((int)(((byte)(135)))), ((int)(((byte)(135)))));
            this.gcContext.AppearanceCell.Options.UseFont = true;
            this.gcContext.AppearanceCell.Options.UseForeColor = true;
            this.gcContext.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gcContext.AppearanceHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(3)))), ((int)(((byte)(51)))));
            this.gcContext.AppearanceHeader.Options.UseFont = true;
            this.gcContext.AppearanceHeader.Options.UseForeColor = true;
            this.gcContext.AppearanceHeader.Options.UseTextOptions = true;
            this.gcContext.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcContext.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gcContext.Caption = "地点和跟踪进度";
            this.gcContext.FieldName = "context";
            this.gcContext.Name = "gcContext";
            this.gcContext.Visible = true;
            this.gcContext.VisibleIndex = 1;
            this.gcContext.Width = 363;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.picBillBox);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 24);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(785, 655);
            this.panelControl1.TabIndex = 84;
            // 
            // picBillBox
            // 
            this.picBillBox.AllowDrop = true;
            this.picBillBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picBillBox.Location = new System.Drawing.Point(2, 2);
            this.picBillBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.picBillBox.Name = "picBillBox";
            this.picBillBox.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.picBillBox.Properties.Appearance.Options.UseFont = true;
            this.picBillBox.Properties.NullText = "底单图片";
            this.picBillBox.Properties.ReadOnly = true;
            this.picBillBox.Properties.ShowMenu = false;
            this.picBillBox.Properties.ShowScrollBars = true;
            this.picBillBox.Size = new System.Drawing.Size(781, 651);
            this.picBillBox.TabIndex = 82;
            this.picBillBox.DoubleClick += new System.EventHandler(this.picBillBox_DoubleClick);
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.labelControl6);
            this.panelControl2.Controls.Add(this.trackPercent);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl2.Location = new System.Drawing.Point(0, 0);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(785, 24);
            this.panelControl2.TabIndex = 83;
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(11, 4);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(48, 14);
            this.labelControl6.TabIndex = 69;
            this.labelControl6.Text = "缩放比例";
            // 
            // trackPercent
            // 
            this.trackPercent.EditValue = 100;
            this.trackPercent.Location = new System.Drawing.Point(63, 2);
            this.trackPercent.Name = "trackPercent";
            this.trackPercent.Properties.Maximum = 200;
            this.trackPercent.Properties.Middle = 5;
            this.trackPercent.Properties.Minimum = 5;
            this.trackPercent.Properties.ScrollThumbStyle = DevExpress.XtraEditors.Repository.ScrollThumbStyle.ArrowDownRight;
            this.trackPercent.Properties.SmallChange = 10;
            this.trackPercent.Size = new System.Drawing.Size(200, 23);
            this.trackPercent.TabIndex = 70;
            this.trackPercent.Value = 100;
            this.trackPercent.EditValueChanged += new System.EventHandler(this.trackPercent_EditValueChanged);
            // 
            // FrmSearchBill
            // 
            this.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1259, 733);
            this.Controls.Add(this.splitContainerControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.Name = "FrmSearchBill";
            this.Text = "查快递-暂时支持申通快递的查询";
            this.Load += new System.EventHandler(this.FrmSearchBill_Load);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2)).EndInit();
            this.splitContainerControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcBill)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvBill)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picBillBox.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.panelControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackPercent.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackPercent)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbExpress;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtBillCode;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraGrid.GridControl gcBill;
        private DevExpress.XtraGrid.Views.Grid.GridView gvBill;
        private DevExpress.XtraGrid.Columns.GridColumn gcTime;
        private DevExpress.XtraGrid.Columns.GridColumn gcContext;
        private DevExpress.XtraEditors.PictureEdit picBillBox;
        private System.Windows.Forms.Button btnOpenFolder;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl2;
        private DevExpress.XtraEditors.LabelControl lblInfo;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.ZoomTrackBarControl trackPercent;
    }
}

