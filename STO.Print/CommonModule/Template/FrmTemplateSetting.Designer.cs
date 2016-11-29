namespace STO.Print
{
    partial class FrmTemplateSetting
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmTemplateSetting));
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.tab1 = new DevExpress.XtraTab.XtraTabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.lblPPI = new DevExpress.XtraEditors.LabelControl();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.trackPercent = new DevExpress.XtraEditors.ZoomTrackBarControl();
            this.picBillBox = new DevExpress.XtraEditors.PictureEdit();
            this.tab2 = new DevExpress.XtraTab.XtraTabPage();
            this.axGRDesigner1 = new AxgrdesLib.AxGRDesigner();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.btnSaveOther = new DevExpress.XtraEditors.SimpleButton();
            this.btnOpen = new DevExpress.XtraEditors.SimpleButton();
            this.txtFileFullPath = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem2 = new DevExpress.XtraBars.BarButtonItem();
            this.btnSaveAs = new DevExpress.XtraBars.BarButtonItem();
            this.btnPrintSetting = new DevExpress.XtraBars.BarButtonItem();
            this.btnPreview = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.tab1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackPercent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackPercent.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBillBox.Properties)).BeginInit();
            this.tab2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axGRDesigner1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtFileFullPath.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControl1.Location = new System.Drawing.Point(0, 0);
            this.xtraTabControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.tab1;
            this.xtraTabControl1.Size = new System.Drawing.Size(1008, 702);
            this.xtraTabControl1.TabIndex = 0;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tab1,
            this.tab2});
            // 
            // tab1
            // 
            this.tab1.Controls.Add(this.splitContainer1);
            this.tab1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tab1.Name = "tab1";
            this.tab1.Size = new System.Drawing.Size(1002, 673);
            this.tab1.Text = "选择系统模板";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeView1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panelControl1);
            this.splitContainer1.Panel2.Controls.Add(this.picBillBox);
            this.splitContainer1.Size = new System.Drawing.Size(1002, 673);
            this.splitContainer1.SplitterDistance = 179;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 6;
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(179, 673);
            this.treeView1.TabIndex = 1;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.lblPPI);
            this.panelControl1.Controls.Add(this.btnSave);
            this.panelControl1.Controls.Add(this.panelControl2);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 580);
            this.panelControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(818, 40);
            this.panelControl1.TabIndex = 210;
            // 
            // lblPPI
            // 
            this.lblPPI.Location = new System.Drawing.Point(15, 11);
            this.lblPPI.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lblPPI.Name = "lblPPI";
            this.lblPPI.Size = new System.Drawing.Size(48, 14);
            this.lblPPI.TabIndex = 70;
            this.lblPPI.Text = "分辨率：";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(382, 4);
            this.btnSave.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(105, 32);
            this.btnSave.TabIndex = 209;
            this.btnSave.Text = "保存默认模板";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.labelControl6);
            this.panelControl2.Controls.Add(this.trackPercent);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelControl2.Location = new System.Drawing.Point(509, 2);
            this.panelControl2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(307, 36);
            this.panelControl2.TabIndex = 68;
            // 
            // labelControl6
            // 
            this.labelControl6.Dock = System.Windows.Forms.DockStyle.Right;
            this.labelControl6.Location = new System.Drawing.Point(24, 2);
            this.labelControl6.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(48, 14);
            this.labelControl6.TabIndex = 69;
            this.labelControl6.Text = "缩放比例";
            // 
            // trackPercent
            // 
            this.trackPercent.Dock = System.Windows.Forms.DockStyle.Right;
            this.trackPercent.EditValue = 100;
            this.trackPercent.Location = new System.Drawing.Point(72, 2);
            this.trackPercent.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.trackPercent.Name = "trackPercent";
            this.trackPercent.Properties.Maximum = 200;
            this.trackPercent.Properties.Middle = 5;
            this.trackPercent.Properties.Minimum = 5;
            this.trackPercent.Properties.ScrollThumbStyle = DevExpress.XtraEditors.Repository.ScrollThumbStyle.ArrowDownRight;
            this.trackPercent.Properties.SmallChange = 10;
            this.trackPercent.Size = new System.Drawing.Size(233, 32);
            this.trackPercent.TabIndex = 70;
            this.trackPercent.Value = 100;
            this.trackPercent.EditValueChanged += new System.EventHandler(this.trackPercent_EditValueChanged);
            // 
            // picBillBox
            // 
            this.picBillBox.AllowDrop = true;
            this.picBillBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.picBillBox.Location = new System.Drawing.Point(0, 0);
            this.picBillBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.picBillBox.Name = "picBillBox";
            this.picBillBox.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.picBillBox.Properties.Appearance.Options.UseFont = true;
            this.picBillBox.Properties.NullText = "模板图片";
            this.picBillBox.Properties.ReadOnly = true;
            this.picBillBox.Properties.ShowMenu = false;
            this.picBillBox.Properties.ShowScrollBars = true;
            this.picBillBox.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            this.picBillBox.Size = new System.Drawing.Size(818, 580);
            this.picBillBox.TabIndex = 82;
            // 
            // tab2
            // 
            this.tab2.Controls.Add(this.axGRDesigner1);
            this.tab2.Controls.Add(this.panel1);
            this.tab2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tab2.Name = "tab2";
            this.tab2.Size = new System.Drawing.Size(1002, 673);
            this.tab2.Text = "选择外部模板";
            // 
            // axGRDesigner1
            // 
            this.axGRDesigner1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axGRDesigner1.Enabled = true;
            this.axGRDesigner1.Location = new System.Drawing.Point(0, 57);
            this.axGRDesigner1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.axGRDesigner1.Name = "axGRDesigner1";
            this.axGRDesigner1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axGRDesigner1.OcxState")));
            this.axGRDesigner1.Size = new System.Drawing.Size(1002, 616);
            this.axGRDesigner1.TabIndex = 192;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Controls.Add(this.btnSaveOther);
            this.panel1.Controls.Add(this.btnOpen);
            this.panel1.Controls.Add(this.txtFileFullPath);
            this.panel1.Controls.Add(this.labelControl4);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1002, 57);
            this.panel1.TabIndex = 191;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(833, 12);
            this.btnClose.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(86, 32);
            this.btnClose.TabIndex = 186;
            this.btnClose.Text = "关闭(Q)";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSaveOther
            // 
            this.btnSaveOther.Location = new System.Drawing.Point(712, 12);
            this.btnSaveOther.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnSaveOther.Name = "btnSaveOther";
            this.btnSaveOther.Size = new System.Drawing.Size(114, 32);
            this.btnSaveOther.TabIndex = 185;
            this.btnSaveOther.Text = "保存默认模板(S)";
            this.btnSaveOther.Click += new System.EventHandler(this.btnSaveOther_Click);
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(618, 12);
            this.btnOpen.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(86, 32);
            this.btnOpen.TabIndex = 184;
            this.btnOpen.Text = "浏览(O)";
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // txtFileFullPath
            // 
            this.txtFileFullPath.Location = new System.Drawing.Point(114, 15);
            this.txtFileFullPath.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtFileFullPath.Name = "txtFileFullPath";
            this.txtFileFullPath.Properties.Appearance.BackColor = System.Drawing.Color.LemonChiffon;
            this.txtFileFullPath.Properties.Appearance.Options.UseBackColor = true;
            this.txtFileFullPath.Properties.MaxLength = 50;
            this.txtFileFullPath.Size = new System.Drawing.Size(497, 20);
            this.txtFileFullPath.TabIndex = 22;
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.ForeColor = System.Drawing.Color.Black;
            this.labelControl4.Location = new System.Drawing.Point(20, 18);
            this.labelControl4.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(63, 14);
            this.labelControl4.TabIndex = 4;
            this.labelControl4.Text = "grf数据文件";
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "打开";
            this.barButtonItem1.Id = 0;
            this.barButtonItem1.Name = "barButtonItem1";
            // 
            // barButtonItem2
            // 
            this.barButtonItem2.Caption = "保存";
            this.barButtonItem2.Id = 9;
            this.barButtonItem2.Name = "barButtonItem2";
            // 
            // btnSaveAs
            // 
            this.btnSaveAs.Caption = "另存为";
            this.btnSaveAs.Id = 10;
            this.btnSaveAs.Name = "btnSaveAs";
            // 
            // btnPrintSetting
            // 
            this.btnPrintSetting.Caption = "打印设置";
            this.btnPrintSetting.Id = 6;
            this.btnPrintSetting.Name = "btnPrintSetting";
            // 
            // btnPreview
            // 
            this.btnPreview.Caption = "预览";
            this.btnPreview.Id = 7;
            this.btnPreview.Name = "btnPreview";
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1008, 0);
            this.barDockControlRight.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 702);
            // 
            // FrmTemplateSetting
            // 
            this.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 702);
            this.Controls.Add(this.xtraTabControl1);
            this.Controls.Add(this.barDockControlRight);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmTemplateSetting";
            this.Text = "设置默认模板";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmTemplateSetting_FormClosing);
            this.Load += new System.EventHandler(this.FrmTemplateSetting_Load);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.tab1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.panelControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackPercent.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackPercent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBillBox.Properties)).EndInit();
            this.tab2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axGRDesigner1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtFileFullPath.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage tab1;
        private DevExpress.XtraTab.XtraTabPage tab2;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView treeView1;
        private DevExpress.XtraEditors.PictureEdit picBillBox;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.ZoomTrackBarControl trackPercent;
        private DevExpress.XtraEditors.LabelControl lblPPI;
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.SimpleButton btnSaveOther;
        private DevExpress.XtraEditors.SimpleButton btnOpen;
        private DevExpress.XtraEditors.TextEdit txtFileFullPath;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private AxgrdesLib.AxGRDesigner axGRDesigner1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem2;
        private DevExpress.XtraBars.BarButtonItem btnSaveAs;
        private DevExpress.XtraBars.BarButtonItem btnPrintSetting;
        private DevExpress.XtraBars.BarButtonItem btnPreview;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;


    }
}