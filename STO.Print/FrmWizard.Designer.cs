using DotNet.Utilities;
namespace STO.Print
{
    partial class FrmWizard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmWizard));
            this.wizardControlInit = new DevExpress.XtraWizard.WizardControl();
            this.welcomeWizardPage1 = new DevExpress.XtraWizard.WelcomeWizardPage();
            this.pictureEdit2 = new DevExpress.XtraEditors.PictureEdit();
            this.pictureEdit1 = new DevExpress.XtraEditors.PictureEdit();
            this.wizardPageTemplateSetting = new DevExpress.XtraWizard.WizardPage();
            this.lsbTemplate = new DevExpress.XtraEditors.ListBoxControl();
            this.completionWizardPage1 = new DevExpress.XtraWizard.CompletionWizardPage();
            this.wizardPagePrinterSetting = new DevExpress.XtraWizard.WizardPage();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.PrintTestPage = new DevExpress.XtraEditors.SimpleButton();
            this.lsbPrinter = new DevExpress.XtraEditors.ListBoxControl();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            ((System.ComponentModel.ISupportInitialize)(this.wizardControlInit)).BeginInit();
            this.wizardControlInit.SuspendLayout();
            this.welcomeWizardPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).BeginInit();
            this.wizardPageTemplateSetting.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lsbTemplate)).BeginInit();
            this.wizardPagePrinterSetting.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lsbPrinter)).BeginInit();
            this.SuspendLayout();
            // 
            // wizardControlInit
            // 
            this.wizardControlInit.Appearance.AeroWizardTitle.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.wizardControlInit.Appearance.AeroWizardTitle.Options.UseFont = true;
            this.wizardControlInit.Appearance.ExteriorPage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(243)))), ((int)(((byte)(254)))));
            this.wizardControlInit.Appearance.ExteriorPage.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.wizardControlInit.Appearance.ExteriorPage.Options.UseBackColor = true;
            this.wizardControlInit.Appearance.ExteriorPage.Options.UseFont = true;
            this.wizardControlInit.Appearance.ExteriorPageTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(243)))), ((int)(((byte)(254)))));
            this.wizardControlInit.Appearance.ExteriorPageTitle.Font = new System.Drawing.Font("微软雅黑", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.wizardControlInit.Appearance.ExteriorPageTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(129)))), ((int)(((byte)(178)))));
            this.wizardControlInit.Appearance.ExteriorPageTitle.Options.UseBackColor = true;
            this.wizardControlInit.Appearance.ExteriorPageTitle.Options.UseFont = true;
            this.wizardControlInit.Appearance.ExteriorPageTitle.Options.UseForeColor = true;
            this.wizardControlInit.Appearance.Page.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(243)))), ((int)(((byte)(254)))));
            this.wizardControlInit.Appearance.Page.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.wizardControlInit.Appearance.Page.Options.UseBackColor = true;
            this.wizardControlInit.Appearance.Page.Options.UseFont = true;
            this.wizardControlInit.Appearance.PageTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(243)))), ((int)(((byte)(254)))));
            this.wizardControlInit.Appearance.PageTitle.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.wizardControlInit.Appearance.PageTitle.Options.UseBackColor = true;
            this.wizardControlInit.Appearance.PageTitle.Options.UseFont = true;
            this.wizardControlInit.CancelText = "取消";
            this.wizardControlInit.Controls.Add(this.welcomeWizardPage1);
            this.wizardControlInit.Controls.Add(this.wizardPageTemplateSetting);
            this.wizardControlInit.Controls.Add(this.completionWizardPage1);
            this.wizardControlInit.Controls.Add(this.wizardPagePrinterSetting);
            this.wizardControlInit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wizardControlInit.FinishText = "&完成";
            this.wizardControlInit.HelpText = "&帮助";
            this.wizardControlInit.Image = ((System.Drawing.Image)(resources.GetObject("wizardControlInit.Image")));
            this.wizardControlInit.ImageWidth = 140;
            this.wizardControlInit.Location = new System.Drawing.Point(0, 0);
            this.wizardControlInit.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.wizardControlInit.MinimumSize = new System.Drawing.Size(117, 142);
            this.wizardControlInit.Name = "wizardControlInit";
            this.wizardControlInit.NextText = "&下一步";
            this.wizardControlInit.Pages.AddRange(new DevExpress.XtraWizard.BaseWizardPage[] {
            this.welcomeWizardPage1,
            this.wizardPageTemplateSetting,
            this.wizardPagePrinterSetting,
            this.completionWizardPage1});
            this.wizardControlInit.PreviousText = "&返回";
            this.wizardControlInit.Size = new System.Drawing.Size(574, 342);
            this.wizardControlInit.Text = BaseSystemInfo.SoftFullName;
            this.wizardControlInit.FinishClick += new System.ComponentModel.CancelEventHandler(this.wizardControlInit_FinishClick);
            this.wizardControlInit.NextClick += new DevExpress.XtraWizard.WizardCommandButtonClickEventHandler(this.wizardControlInit_NextClick);
            // 
            // welcomeWizardPage1
            // 
            this.welcomeWizardPage1.Controls.Add(this.pictureEdit2);
            this.welcomeWizardPage1.Controls.Add(this.pictureEdit1);
            this.welcomeWizardPage1.IntroductionText = "";
            this.welcomeWizardPage1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.welcomeWizardPage1.Name = "welcomeWizardPage1";
            this.welcomeWizardPage1.ProceedText = "";
            this.welcomeWizardPage1.Size = new System.Drawing.Size(402, 201);
            this.welcomeWizardPage1.Text = "欢迎使用" + BaseSystemInfo.SoftFullName;
            // 
            // pictureEdit2
            // 
            this.pictureEdit2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureEdit2.EditValue = ((object)(resources.GetObject("pictureEdit2.EditValue")));
            this.pictureEdit2.Location = new System.Drawing.Point(-2, 101);
            this.pictureEdit2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pictureEdit2.Name = "pictureEdit2";
            this.pictureEdit2.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.pictureEdit2.Properties.Appearance.Options.UseBackColor = true;
            this.pictureEdit2.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pictureEdit2.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            this.pictureEdit2.Size = new System.Drawing.Size(405, 65);
            this.pictureEdit2.TabIndex = 1;
            // 
            // pictureEdit1
            // 
            this.pictureEdit1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureEdit1.EditValue = ((object)(resources.GetObject("pictureEdit1.EditValue")));
            this.pictureEdit1.Location = new System.Drawing.Point(-2, 17);
            this.pictureEdit1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pictureEdit1.Name = "pictureEdit1";
            this.pictureEdit1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.pictureEdit1.Properties.Appearance.Options.UseBackColor = true;
            this.pictureEdit1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pictureEdit1.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            this.pictureEdit1.Size = new System.Drawing.Size(405, 65);
            this.pictureEdit1.TabIndex = 0;
            this.pictureEdit1.EditValueChanged += new System.EventHandler(this.pictureEdit1_EditValueChanged);
            // 
            // wizardPageTemplateSetting
            // 
            this.wizardPageTemplateSetting.Controls.Add(this.lsbTemplate);
            this.wizardPageTemplateSetting.DescriptionText = "设置默认打印模板有利于快速打印";
            this.wizardPageTemplateSetting.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.wizardPageTemplateSetting.Name = "wizardPageTemplateSetting";
            this.wizardPageTemplateSetting.Size = new System.Drawing.Size(542, 197);
            this.wizardPageTemplateSetting.Text = "设置默认打印模板";
            // 
            // lsbTemplate
            // 
            this.lsbTemplate.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lsbTemplate.Appearance.Options.UseFont = true;
            this.lsbTemplate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lsbTemplate.Location = new System.Drawing.Point(0, 0);
            this.lsbTemplate.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lsbTemplate.Name = "lsbTemplate";
            this.lsbTemplate.Size = new System.Drawing.Size(542, 197);
            this.lsbTemplate.TabIndex = 0;
            // 
            // completionWizardPage1
            // 
            this.completionWizardPage1.FinishText = "设置成功，开始使用！";
            this.completionWizardPage1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.completionWizardPage1.Name = "completionWizardPage1";
            this.completionWizardPage1.ProceedText = "已完成";
            this.completionWizardPage1.Size = new System.Drawing.Size(402, 201);
            this.completionWizardPage1.Text = "设置完成";
            // 
            // wizardPagePrinterSetting
            // 
            this.wizardPagePrinterSetting.Controls.Add(this.panelControl1);
            this.wizardPagePrinterSetting.Controls.Add(this.lsbPrinter);
            this.wizardPagePrinterSetting.DescriptionText = "设置默认打印机方便快速打印";
            this.wizardPagePrinterSetting.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.wizardPagePrinterSetting.Name = "wizardPagePrinterSetting";
            this.wizardPagePrinterSetting.Size = new System.Drawing.Size(542, 197);
            this.wizardPagePrinterSetting.Text = "设置默认打印机";
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.PrintTestPage);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 153);
            this.panelControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(542, 44);
            this.panelControl1.TabIndex = 2;
            // 
            // PrintTestPage
            // 
            this.PrintTestPage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PrintTestPage.Location = new System.Drawing.Point(458, 8);
            this.PrintTestPage.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.PrintTestPage.Name = "PrintTestPage";
            this.PrintTestPage.Size = new System.Drawing.Size(79, 28);
            this.PrintTestPage.TabIndex = 1;
            this.PrintTestPage.Text = "打印测试页";
            this.PrintTestPage.Click += new System.EventHandler(this.PrintTestPage_Click);
            // 
            // lsbPrinter
            // 
            this.lsbPrinter.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lsbPrinter.Appearance.Options.UseFont = true;
            this.lsbPrinter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lsbPrinter.Location = new System.Drawing.Point(0, 0);
            this.lsbPrinter.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lsbPrinter.Name = "lsbPrinter";
            this.lsbPrinter.Size = new System.Drawing.Size(542, 197);
            this.lsbPrinter.TabIndex = 0;
            // 
            // printPreviewDialog1
            // 
            this.printPreviewDialog1.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.ClientSize = new System.Drawing.Size(400, 300);
            this.printPreviewDialog1.Enabled = true;
            this.printPreviewDialog1.Icon = ((System.Drawing.Icon)(resources.GetObject("printPreviewDialog1.Icon")));
            this.printPreviewDialog1.Name = "printPreviewDialog1";
            this.printPreviewDialog1.Visible = false;
            // 
            // FrmWizard
            // 
            this.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(574, 342);
            this.Controls.Add(this.wizardControlInit);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmWizard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "初始化向导";
            this.Load += new System.EventHandler(this.FrmWizard_Load);
            ((System.ComponentModel.ISupportInitialize)(this.wizardControlInit)).EndInit();
            this.wizardControlInit.ResumeLayout(false);
            this.welcomeWizardPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).EndInit();
            this.wizardPageTemplateSetting.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lsbTemplate)).EndInit();
            this.wizardPagePrinterSetting.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lsbPrinter)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraWizard.WizardControl wizardControlInit;
        private DevExpress.XtraWizard.WelcomeWizardPage welcomeWizardPage1;
        private DevExpress.XtraWizard.WizardPage wizardPageTemplateSetting;
        private DevExpress.XtraWizard.CompletionWizardPage completionWizardPage1;
        private DevExpress.XtraWizard.WizardPage wizardPagePrinterSetting;
        private DevExpress.XtraEditors.ListBoxControl lsbTemplate;
        private DevExpress.XtraEditors.ListBoxControl lsbPrinter;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton PrintTestPage;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
        private DevExpress.XtraEditors.PictureEdit pictureEdit1;
        private DevExpress.XtraEditors.PictureEdit pictureEdit2;

    }
}