using DotNet.Utilities;
namespace STO.Print
{
    partial class FrmAboutThis
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

        #region Windows 窗体设计器生成的主键

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用主键编辑器修改此方法的内容。

        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAboutThis));
            this.btnConfirm = new DevExpress.XtraEditors.SimpleButton();
            this.lblWarning = new System.Windows.Forms.Label();
            this.grpAboutThis = new System.Windows.Forms.GroupBox();
            this.lblCopyright = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
            this.lblSoftFullName = new System.Windows.Forms.Label();
            this.picCompany = new System.Windows.Forms.PictureBox();
            this.lblTechnicalSupport = new System.Windows.Forms.Label();
            this.lblDownLoad = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picCompany)).BeginInit();
            this.SuspendLayout();
            // 
            // btnConfirm
            // 
            this.btnConfirm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConfirm.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnConfirm.Location = new System.Drawing.Point(533, 312);
            this.btnConfirm.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(87, 33);
            this.btnConfirm.TabIndex = 0;
            this.btnConfirm.Text = "&OK";
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // lblWarning
            // 
            this.lblWarning.Location = new System.Drawing.Point(63, 225);
            this.lblWarning.Name = "lblWarning";
            this.lblWarning.Size = new System.Drawing.Size(528, 91);
            this.lblWarning.TabIndex = 7;
            this.lblWarning.Text = resources.GetString("lblWarning.Text");
            this.lblWarning.Click += new System.EventHandler(this.FrmAboutThis_Click);
            // 
            // grpAboutThis
            // 
            this.grpAboutThis.Location = new System.Drawing.Point(30, 204);
            this.grpAboutThis.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.grpAboutThis.Name = "grpAboutThis";
            this.grpAboutThis.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.grpAboutThis.Size = new System.Drawing.Size(581, 4);
            this.grpAboutThis.TabIndex = 6;
            this.grpAboutThis.TabStop = false;
            // 
            // lblCopyright
            // 
            this.lblCopyright.Location = new System.Drawing.Point(222, 111);
            this.lblCopyright.Name = "lblCopyright";
            this.lblCopyright.Size = new System.Drawing.Size(350, 23);
            this.lblCopyright.TabIndex = 5;
            this.lblCopyright.Text = "Copyright 2015-2016 STO EXPRESS";
            this.lblCopyright.Click += new System.EventHandler(this.FrmAboutThis_Click);
            // 
            // lblVersion
            // 
            this.lblVersion.Location = new System.Drawing.Point(222, 80);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(350, 23);
            this.lblVersion.TabIndex = 4;
            this.lblVersion.Text = "Version V1.0";
            this.lblVersion.Click += new System.EventHandler(this.FrmAboutThis_Click);
            // 
            // lblSoftFullName
            // 
            this.lblSoftFullName.Location = new System.Drawing.Point(222, 49);
            this.lblSoftFullName.Name = "lblSoftFullName";
            this.lblSoftFullName.Size = new System.Drawing.Size(350, 23);
            this.lblSoftFullName.TabIndex = 3;
            this.lblSoftFullName.Text = "STO Soft";
            this.lblSoftFullName.Click += new System.EventHandler(this.FrmAboutThis_Click);
            // 
            // picCompany
            // 
            this.picCompany.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picCompany.Image = global::STO.Print.Properties.Resources.公众号;
            this.picCompany.Location = new System.Drawing.Point(66, 34);
            this.picCompany.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.picCompany.Name = "picCompany";
            this.picCompany.Size = new System.Drawing.Size(142, 142);
            this.picCompany.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picCompany.TabIndex = 15;
            this.picCompany.TabStop = false;
            this.picCompany.Click += new System.EventHandler(this.picCompany_Click);
            // 
            // lblTechnicalSupport
            // 
            this.lblTechnicalSupport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTechnicalSupport.Location = new System.Drawing.Point(63, 315);
            this.lblTechnicalSupport.Name = "lblTechnicalSupport";
            this.lblTechnicalSupport.Size = new System.Drawing.Size(156, 23);
            this.lblTechnicalSupport.TabIndex = 16;
            this.lblTechnicalSupport.Text = "技术支持QQ：766096823";
            this.lblTechnicalSupport.Click += new System.EventHandler(this.lblTechnicalSupport_Click);
            // 
            // lblDownLoad
            // 
            this.lblDownLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblDownLoad.Location = new System.Drawing.Point(222, 146);
            this.lblDownLoad.Name = "lblDownLoad";
            this.lblDownLoad.Size = new System.Drawing.Size(156, 23);
            this.lblDownLoad.TabIndex = 17;
            this.lblDownLoad.Text = "打开下载链接";
            this.lblDownLoad.Click += new System.EventHandler(this.lblDownLoad_Click);
            // 
            // FrmAboutThis
            // 
            this.AcceptButton = this.btnConfirm;
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnConfirm;
            this.ClientSize = new System.Drawing.Size(642, 358);
            this.Controls.Add(this.lblDownLoad);
            this.Controls.Add(this.lblTechnicalSupport);
            this.Controls.Add(this.picCompany);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.lblWarning);
            this.Controls.Add(this.grpAboutThis);
            this.Controls.Add(this.lblCopyright);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.lblSoftFullName);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmAboutThis";
            this.Text = "关于申通打印专家";
            this.Load += new System.EventHandler(this.FrmAboutThis_Load);
            this.Click += new System.EventHandler(this.FrmAboutThis_Click);
            ((System.ComponentModel.ISupportInitialize)(this.picCompany)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnConfirm;
        private System.Windows.Forms.Label lblWarning;
        private System.Windows.Forms.GroupBox grpAboutThis;
        private System.Windows.Forms.Label lblCopyright;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.Label lblSoftFullName;
        private System.Windows.Forms.PictureBox picCompany;
        private System.Windows.Forms.Label lblTechnicalSupport;
        private System.Windows.Forms.Label lblDownLoad;
    }
}