namespace STO.Print
{
    partial class FrmExportBillImage
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
            this.txtBills = new DevExpress.XtraEditors.MemoEdit();
            this.txtWorkFolder = new DevExpress.XtraEditors.TextEdit();
            this.lblWorkFolder = new DevExpress.XtraEditors.LabelControl();
            this.lblTotalBillNum = new DevExpress.XtraEditors.LabelControl();
            this.lblUploadStatus = new DevExpress.XtraEditors.LabelControl();
            this.btnSetWorkFloder = new DevExpress.XtraEditors.SimpleButton();
            this.btnOpenFolder = new DevExpress.XtraEditors.SimpleButton();
            this.panelSignInput = new DevExpress.XtraEditors.PanelControl();
            this.ckAddSystemWaterMark = new DevExpress.XtraEditors.CheckEdit();
            this.btnCompress = new DevExpress.XtraEditors.SimpleButton();
            this.btnBuildImage = new DevExpress.XtraEditors.SimpleButton();
            this.lblWarning = new System.Windows.Forms.Label();
            this.alertBuildImageInfo = new DevExpress.XtraBars.Alerter.AlertControl(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.txtBills.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWorkFolder.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelSignInput)).BeginInit();
            this.panelSignInput.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ckAddSystemWaterMark.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // txtBills
            // 
            this.txtBills.AllowDrop = true;
            this.txtBills.Dock = System.Windows.Forms.DockStyle.Left;
            this.txtBills.EditValue = "";
            this.txtBills.Location = new System.Drawing.Point(0, 0);
            this.txtBills.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtBills.Name = "txtBills";
            this.txtBills.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBills.Properties.Appearance.Options.UseFont = true;
            this.txtBills.Properties.MaxLength = 14000;
            this.txtBills.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtBills.Properties.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtBills.Size = new System.Drawing.Size(281, 742);
            this.txtBills.TabIndex = 801;
            // 
            // txtWorkFolder
            // 
            this.txtWorkFolder.Location = new System.Drawing.Point(76, 39);
            this.txtWorkFolder.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtWorkFolder.Name = "txtWorkFolder";
            this.txtWorkFolder.Size = new System.Drawing.Size(435, 20);
            this.txtWorkFolder.TabIndex = 807;
            // 
            // lblWorkFolder
            // 
            this.lblWorkFolder.Location = new System.Drawing.Point(9, 43);
            this.lblWorkFolder.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lblWorkFolder.Name = "lblWorkFolder";
            this.lblWorkFolder.Size = new System.Drawing.Size(60, 14);
            this.lblWorkFolder.TabIndex = 806;
            this.lblWorkFolder.Text = "输出路径：";
            // 
            // lblTotalBillNum
            // 
            this.lblTotalBillNum.Location = new System.Drawing.Point(76, 11);
            this.lblTotalBillNum.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lblTotalBillNum.Name = "lblTotalBillNum";
            this.lblTotalBillNum.Size = new System.Drawing.Size(7, 14);
            this.lblTotalBillNum.TabIndex = 809;
            this.lblTotalBillNum.Text = "0";
            // 
            // lblUploadStatus
            // 
            this.lblUploadStatus.Location = new System.Drawing.Point(21, 11);
            this.lblUploadStatus.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lblUploadStatus.Name = "lblUploadStatus";
            this.lblUploadStatus.Size = new System.Drawing.Size(48, 14);
            this.lblUploadStatus.TabIndex = 808;
            this.lblUploadStatus.Text = "总单数：";
            // 
            // btnSetWorkFloder
            // 
            this.btnSetWorkFloder.Location = new System.Drawing.Point(517, 36);
            this.btnSetWorkFloder.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnSetWorkFloder.Name = "btnSetWorkFloder";
            this.btnSetWorkFloder.Size = new System.Drawing.Size(75, 28);
            this.btnSetWorkFloder.TabIndex = 810;
            this.btnSetWorkFloder.Text = "选择文件夹";
            this.btnSetWorkFloder.Click += new System.EventHandler(this.btnSetWorkFloder_Click);
            // 
            // btnOpenFolder
            // 
            this.btnOpenFolder.Location = new System.Drawing.Point(598, 36);
            this.btnOpenFolder.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnOpenFolder.Name = "btnOpenFolder";
            this.btnOpenFolder.Size = new System.Drawing.Size(75, 28);
            this.btnOpenFolder.TabIndex = 811;
            this.btnOpenFolder.Text = "打开文件夹";
            this.btnOpenFolder.Click += new System.EventHandler(this.btnOpenFolder_Click);
            // 
            // panelSignInput
            // 
            this.panelSignInput.Controls.Add(this.ckAddSystemWaterMark);
            this.panelSignInput.Controls.Add(this.btnCompress);
            this.panelSignInput.Controls.Add(this.btnBuildImage);
            this.panelSignInput.Controls.Add(this.txtWorkFolder);
            this.panelSignInput.Controls.Add(this.btnOpenFolder);
            this.panelSignInput.Controls.Add(this.lblWorkFolder);
            this.panelSignInput.Controls.Add(this.btnSetWorkFloder);
            this.panelSignInput.Controls.Add(this.lblUploadStatus);
            this.panelSignInput.Controls.Add(this.lblTotalBillNum);
            this.panelSignInput.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSignInput.Location = new System.Drawing.Point(281, 0);
            this.panelSignInput.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelSignInput.Name = "panelSignInput";
            this.panelSignInput.Size = new System.Drawing.Size(728, 98);
            this.panelSignInput.TabIndex = 812;
            // 
            // ckAddSystemWaterMark
            // 
            this.ckAddSystemWaterMark.EditValue = true;
            this.ckAddSystemWaterMark.Location = new System.Drawing.Point(515, 8);
            this.ckAddSystemWaterMark.Name = "ckAddSystemWaterMark";
            this.ckAddSystemWaterMark.Properties.Caption = "加上系统水印";
            this.ckAddSystemWaterMark.Size = new System.Drawing.Size(99, 19);
            this.ckAddSystemWaterMark.TabIndex = 814;
            // 
            // btnCompress
            // 
            this.btnCompress.Location = new System.Drawing.Point(185, 67);
            this.btnCompress.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnCompress.Name = "btnCompress";
            this.btnCompress.Size = new System.Drawing.Size(103, 28);
            this.btnCompress.TabIndex = 813;
            this.btnCompress.Text = "一键压缩";
            this.btnCompress.Visible = false;
            this.btnCompress.Click += new System.EventHandler(this.btnCompress_Click);
            // 
            // btnBuildImage
            // 
            this.btnBuildImage.Location = new System.Drawing.Point(76, 67);
            this.btnBuildImage.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnBuildImage.Name = "btnBuildImage";
            this.btnBuildImage.Size = new System.Drawing.Size(103, 28);
            this.btnBuildImage.TabIndex = 812;
            this.btnBuildImage.Text = "一键生成";
            this.btnBuildImage.Click += new System.EventHandler(this.btnBuildImage_Click);
            // 
            // lblWarning
            // 
            this.lblWarning.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lblWarning.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblWarning.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWarning.ForeColor = System.Drawing.Color.Red;
            this.lblWarning.Location = new System.Drawing.Point(281, 98);
            this.lblWarning.Name = "lblWarning";
            this.lblWarning.Size = new System.Drawing.Size(728, 644);
            this.lblWarning.TabIndex = 813;
            this.lblWarning.Text = "导出的底单可以用来最电子面单的发件扫描";
            this.lblWarning.Visible = false;
            // 
            // alertBuildImageInfo
            // 
            this.alertBuildImageInfo.FormShowingEffect = DevExpress.XtraBars.Alerter.AlertFormShowingEffect.SlideVertical;
            // 
            // FrmExportBillImage
            // 
            this.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1009, 742);
            this.Controls.Add(this.lblWarning);
            this.Controls.Add(this.panelSignInput);
            this.Controls.Add(this.txtBills);
            this.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.Name = "FrmExportBillImage";
            this.Text = "生成底单";
            this.Load += new System.EventHandler(this.FrmExportBillImage_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtBills.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWorkFolder.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelSignInput)).EndInit();
            this.panelSignInput.ResumeLayout(false);
            this.panelSignInput.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ckAddSystemWaterMark.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.MemoEdit txtBills;
        private DevExpress.XtraEditors.TextEdit txtWorkFolder;
        private DevExpress.XtraEditors.LabelControl lblWorkFolder;
        private DevExpress.XtraEditors.LabelControl lblTotalBillNum;
        private DevExpress.XtraEditors.LabelControl lblUploadStatus;
        private DevExpress.XtraEditors.SimpleButton btnSetWorkFloder;
        private DevExpress.XtraEditors.SimpleButton btnOpenFolder;
        private DevExpress.XtraEditors.PanelControl panelSignInput;
        public DevExpress.XtraEditors.SimpleButton btnBuildImage;
        public DevExpress.XtraEditors.SimpleButton btnCompress;
        private System.Windows.Forms.Label lblWarning;
        private DevExpress.XtraEditors.CheckEdit ckAddSystemWaterMark;
        public DevExpress.XtraBars.Alerter.AlertControl alertBuildImageInfo;
    }
}