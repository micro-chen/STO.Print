namespace STO.Print
{
    partial class FrmException
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmException));
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.btnReport = new DevExpress.XtraEditors.SimpleButton();
            this.btnPrint = new DevExpress.XtraEditors.SimpleButton();
            this.grbException = new System.Windows.Forms.GroupBox();
            this.txtUser = new DevExpress.XtraEditors.TextEdit();
            this.lblOperator = new System.Windows.Forms.Label();
            this.txtException = new DevExpress.XtraEditors.MemoEdit();
            this.lblExceptionInfo = new System.Windows.Forms.Label();
            this.txtOccurTime = new DevExpress.XtraEditors.TextEdit();
            this.txtSoft = new DevExpress.XtraEditors.TextEdit();
            this.txtCompany = new DevExpress.XtraEditors.TextEdit();
            this.lblOccurTime = new System.Windows.Forms.Label();
            this.lblSoftFullName = new System.Windows.Forms.Label();
            this.lblCustomer = new System.Windows.Forms.Label();
            this.grpModify = new DevExpress.XtraEditors.GroupControl();
            this.btnQQ = new DevExpress.XtraEditors.SimpleButton();
            this.btnMessage = new DevExpress.XtraEditors.SimpleButton();
            this.txtQQ = new DevExpress.XtraEditors.TextEdit();
            this.lblCreateBy = new DevExpress.XtraEditors.LabelControl();
            this.lblCreateOn = new DevExpress.XtraEditors.LabelControl();
            this.txtCreateOn = new DevExpress.XtraEditors.TextEdit();
            this.txtModifiedBy = new DevExpress.XtraEditors.TextEdit();
            this.lblModifiedBy = new DevExpress.XtraEditors.LabelControl();
            this.txtModifiedOn = new DevExpress.XtraEditors.TextEdit();
            this.lblModifiedOn = new DevExpress.XtraEditors.LabelControl();
            this.grbException.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtUser.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtException.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOccurTime.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSoft.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCompany.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpModify)).BeginInit();
            this.grpModify.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtQQ.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCreateOn.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtModifiedBy.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtModifiedOn.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(639, 487);
            this.btnClose.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(74, 23);
            this.btnClose.TabIndex = 5;
            this.btnClose.Text = "关闭";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnReport
            // 
            this.btnReport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReport.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.btnReport.Appearance.Options.UseForeColor = true;
            this.btnReport.Location = new System.Drawing.Point(526, 487);
            this.btnReport.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(109, 23);
            this.btnReport.TabIndex = 3;
            this.btnReport.Text = "反馈错误(&R)";
            this.btnReport.ToolTip = "建议及时反馈错误，先按反馈错误然后关闭";
            this.btnReport.Click += new System.EventHandler(this.BtnReportClick);
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrint.Location = new System.Drawing.Point(435, 487);
            this.btnPrint.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(87, 23);
            this.btnPrint.TabIndex = 4;
            this.btnPrint.Text = "打印(&P)";
            this.btnPrint.Visible = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // grbException
            // 
            this.grbException.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grbException.Controls.Add(this.txtUser);
            this.grbException.Controls.Add(this.lblOperator);
            this.grbException.Controls.Add(this.txtException);
            this.grbException.Controls.Add(this.lblExceptionInfo);
            this.grbException.Controls.Add(this.txtOccurTime);
            this.grbException.Controls.Add(this.txtSoft);
            this.grbException.Controls.Add(this.txtCompany);
            this.grbException.Controls.Add(this.lblOccurTime);
            this.grbException.Controls.Add(this.lblSoftFullName);
            this.grbException.Controls.Add(this.lblCustomer);
            this.grbException.Location = new System.Drawing.Point(13, 13);
            this.grbException.Name = "grbException";
            this.grbException.Size = new System.Drawing.Size(700, 368);
            this.grbException.TabIndex = 0;
            this.grbException.TabStop = false;
            this.grbException.Text = "系统异常情况记录";
            // 
            // txtUser
            // 
            this.txtUser.Location = new System.Drawing.Point(102, 53);
            this.txtUser.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtUser.Name = "txtUser";
            this.txtUser.Properties.ReadOnly = true;
            this.txtUser.Size = new System.Drawing.Size(162, 20);
            this.txtUser.TabIndex = 5;
            // 
            // lblOperator
            // 
            this.lblOperator.Location = new System.Drawing.Point(9, 54);
            this.lblOperator.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblOperator.Name = "lblOperator";
            this.lblOperator.Size = new System.Drawing.Size(89, 20);
            this.lblOperator.TabIndex = 4;
            this.lblOperator.Text = "用户：";
            this.lblOperator.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtException
            // 
            this.txtException.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtException.Location = new System.Drawing.Point(102, 84);
            this.txtException.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtException.Name = "txtException";
            this.txtException.Properties.ReadOnly = true;
            this.txtException.Properties.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtException.Size = new System.Drawing.Size(580, 269);
            this.txtException.TabIndex = 8;
            // 
            // lblExceptionInfo
            // 
            this.lblExceptionInfo.Location = new System.Drawing.Point(9, 85);
            this.lblExceptionInfo.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblExceptionInfo.Name = "lblExceptionInfo";
            this.lblExceptionInfo.Size = new System.Drawing.Size(89, 20);
            this.lblExceptionInfo.TabIndex = 7;
            this.lblExceptionInfo.Text = "错误信息：";
            this.lblExceptionInfo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtOccurTime
            // 
            this.txtOccurTime.Location = new System.Drawing.Point(498, 53);
            this.txtOccurTime.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtOccurTime.Name = "txtOccurTime";
            this.txtOccurTime.Properties.ReadOnly = true;
            this.txtOccurTime.Size = new System.Drawing.Size(182, 20);
            this.txtOccurTime.TabIndex = 6;
            // 
            // txtSoft
            // 
            this.txtSoft.Location = new System.Drawing.Point(498, 25);
            this.txtSoft.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtSoft.Name = "txtSoft";
            this.txtSoft.Properties.ReadOnly = true;
            this.txtSoft.Size = new System.Drawing.Size(182, 20);
            this.txtSoft.TabIndex = 3;
            // 
            // txtCompany
            // 
            this.txtCompany.Location = new System.Drawing.Point(102, 25);
            this.txtCompany.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtCompany.Name = "txtCompany";
            this.txtCompany.Properties.ReadOnly = true;
            this.txtCompany.Size = new System.Drawing.Size(162, 20);
            this.txtCompany.TabIndex = 1;
            // 
            // lblOccurTime
            // 
            this.lblOccurTime.Location = new System.Drawing.Point(377, 53);
            this.lblOccurTime.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblOccurTime.Name = "lblOccurTime";
            this.lblOccurTime.Size = new System.Drawing.Size(113, 20);
            this.lblOccurTime.TabIndex = 16;
            this.lblOccurTime.Text = "发生时间：";
            this.lblOccurTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblSoftFullName
            // 
            this.lblSoftFullName.Location = new System.Drawing.Point(377, 24);
            this.lblSoftFullName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSoftFullName.Name = "lblSoftFullName";
            this.lblSoftFullName.Size = new System.Drawing.Size(113, 20);
            this.lblSoftFullName.TabIndex = 2;
            this.lblSoftFullName.Text = "软件名称：";
            this.lblSoftFullName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCustomer
            // 
            this.lblCustomer.Location = new System.Drawing.Point(9, 25);
            this.lblCustomer.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCustomer.Name = "lblCustomer";
            this.lblCustomer.Size = new System.Drawing.Size(89, 20);
            this.lblCustomer.TabIndex = 0;
            this.lblCustomer.Text = "公司：";
            this.lblCustomer.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // grpModify
            // 
            this.grpModify.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpModify.Controls.Add(this.btnQQ);
            this.grpModify.Controls.Add(this.btnMessage);
            this.grpModify.Controls.Add(this.txtQQ);
            this.grpModify.Controls.Add(this.lblCreateBy);
            this.grpModify.Controls.Add(this.lblCreateOn);
            this.grpModify.Controls.Add(this.txtCreateOn);
            this.grpModify.Controls.Add(this.txtModifiedBy);
            this.grpModify.Controls.Add(this.lblModifiedBy);
            this.grpModify.Controls.Add(this.txtModifiedOn);
            this.grpModify.Controls.Add(this.lblModifiedOn);
            this.grpModify.Location = new System.Drawing.Point(13, 392);
            this.grpModify.Name = "grpModify";
            this.grpModify.Padding = new System.Windows.Forms.Padding(3);
            this.grpModify.Size = new System.Drawing.Size(700, 84);
            this.grpModify.TabIndex = 1;
            this.grpModify.Text = "信息中心 - 软件开发部 - 技术支持";
            // 
            // btnQQ
            // 
            this.btnQQ.Image = ((System.Drawing.Image)(resources.GetObject("btnQQ.Image")));
            this.btnQQ.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnQQ.Location = new System.Drawing.Point(185, 29);
            this.btnQQ.Name = "btnQQ";
            this.btnQQ.Size = new System.Drawing.Size(19, 23);
            this.btnQQ.TabIndex = 2;
            this.btnQQ.ToolTip = "QQ 交谈";
            this.btnQQ.Click += new System.EventHandler(this.BtnQqClick);
            // 
            // btnMessage
            // 
            this.btnMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMessage.Appearance.Font = new System.Drawing.Font("Tahoma", 11F);
            this.btnMessage.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.btnMessage.Appearance.Options.UseFont = true;
            this.btnMessage.Appearance.Options.UseForeColor = true;
            this.btnMessage.Location = new System.Drawing.Point(440, 30);
            this.btnMessage.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnMessage.Name = "btnMessage";
            this.btnMessage.Size = new System.Drawing.Size(240, 46);
            this.btnMessage.TabIndex = 5;
            this.btnMessage.Text = "加入打印专家群，协助改进系统";
            this.btnMessage.Click += new System.EventHandler(this.btnMessage_Click);
            // 
            // txtQQ
            // 
            this.txtQQ.EditValue = "766096823";
            this.txtQQ.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtQQ.Location = new System.Drawing.Point(80, 30);
            this.txtQQ.Name = "txtQQ";
            this.txtQQ.Properties.MaxLength = 50;
            this.txtQQ.Properties.ReadOnly = true;
            this.txtQQ.Size = new System.Drawing.Size(102, 20);
            this.txtQQ.TabIndex = 1;
            // 
            // lblCreateBy
            // 
            this.lblCreateBy.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lblCreateBy.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblCreateBy.Location = new System.Drawing.Point(9, 30);
            this.lblCreateBy.Name = "lblCreateBy";
            this.lblCreateBy.Size = new System.Drawing.Size(65, 19);
            this.lblCreateBy.TabIndex = 0;
            this.lblCreateBy.Text = "QQ：";
            // 
            // lblCreateOn
            // 
            this.lblCreateOn.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lblCreateOn.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblCreateOn.Location = new System.Drawing.Point(9, 55);
            this.lblCreateOn.Name = "lblCreateOn";
            this.lblCreateOn.Size = new System.Drawing.Size(65, 19);
            this.lblCreateOn.TabIndex = 6;
            this.lblCreateOn.Text = "电话：";
            // 
            // txtCreateOn
            // 
            this.txtCreateOn.EditValue = "021-59804103";
            this.txtCreateOn.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtCreateOn.Location = new System.Drawing.Point(80, 56);
            this.txtCreateOn.Name = "txtCreateOn";
            this.txtCreateOn.Properties.MaxLength = 50;
            this.txtCreateOn.Properties.ReadOnly = true;
            this.txtCreateOn.Size = new System.Drawing.Size(124, 20);
            this.txtCreateOn.TabIndex = 7;
            // 
            // txtModifiedBy
            // 
            this.txtModifiedBy.EditValue = "梧桐-杨恒连";
            this.txtModifiedBy.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtModifiedBy.Location = new System.Drawing.Point(295, 30);
            this.txtModifiedBy.Name = "txtModifiedBy";
            this.txtModifiedBy.Properties.MaxLength = 50;
            this.txtModifiedBy.Properties.ReadOnly = true;
            this.txtModifiedBy.Size = new System.Drawing.Size(124, 20);
            this.txtModifiedBy.TabIndex = 4;
            // 
            // lblModifiedBy
            // 
            this.lblModifiedBy.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lblModifiedBy.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblModifiedBy.Location = new System.Drawing.Point(223, 30);
            this.lblModifiedBy.Name = "lblModifiedBy";
            this.lblModifiedBy.Size = new System.Drawing.Size(65, 19);
            this.lblModifiedBy.TabIndex = 3;
            this.lblModifiedBy.Text = "联系人：";
            // 
            // txtModifiedOn
            // 
            this.txtModifiedOn.EditValue = "766096823@qq.com";
            this.txtModifiedOn.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.txtModifiedOn.Location = new System.Drawing.Point(295, 56);
            this.txtModifiedOn.Name = "txtModifiedOn";
            this.txtModifiedOn.Properties.MaxLength = 50;
            this.txtModifiedOn.Properties.ReadOnly = true;
            this.txtModifiedOn.Size = new System.Drawing.Size(124, 20);
            this.txtModifiedOn.TabIndex = 9;
            // 
            // lblModifiedOn
            // 
            this.lblModifiedOn.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lblModifiedOn.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblModifiedOn.Location = new System.Drawing.Point(223, 55);
            this.lblModifiedOn.Name = "lblModifiedOn";
            this.lblModifiedOn.Size = new System.Drawing.Size(65, 19);
            this.lblModifiedOn.TabIndex = 8;
            this.lblModifiedOn.Text = "邮件：";
            // 
            // FrmException
            // 
            this.Appearance.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(134)));
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(727, 522);
            this.Controls.Add(this.grpModify);
            this.Controls.Add(this.grbException);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnReport);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.MinimumSize = new System.Drawing.Size(346, 209);
            this.Name = "FrmException";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "系统异常情况记录";
            this.Load += new System.EventHandler(this.FrmException_Load);
            this.grbException.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtUser.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtException.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOccurTime.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSoft.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCompany.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpModify)).EndInit();
            this.grpModify.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtQQ.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCreateOn.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtModifiedBy.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtModifiedOn.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.SimpleButton btnReport;
        private DevExpress.XtraEditors.SimpleButton btnPrint;
        private System.Windows.Forms.GroupBox grbException;
        private DevExpress.XtraEditors.TextEdit txtUser;
        private System.Windows.Forms.Label lblOperator;
        private DevExpress.XtraEditors.MemoEdit txtException;
        private System.Windows.Forms.Label lblExceptionInfo;
        private DevExpress.XtraEditors.TextEdit txtOccurTime;
        private DevExpress.XtraEditors.TextEdit txtSoft;
        private DevExpress.XtraEditors.TextEdit txtCompany;
        private System.Windows.Forms.Label lblOccurTime;
        private System.Windows.Forms.Label lblSoftFullName;
        private System.Windows.Forms.Label lblCustomer;
        private DevExpress.XtraEditors.GroupControl grpModify;
        private DevExpress.XtraEditors.TextEdit txtQQ;
        private DevExpress.XtraEditors.LabelControl lblCreateBy;
        private DevExpress.XtraEditors.LabelControl lblCreateOn;
        private DevExpress.XtraEditors.TextEdit txtCreateOn;
        private DevExpress.XtraEditors.TextEdit txtModifiedBy;
        private DevExpress.XtraEditors.LabelControl lblModifiedBy;
        private DevExpress.XtraEditors.TextEdit txtModifiedOn;
        private DevExpress.XtraEditors.LabelControl lblModifiedOn;
        private DevExpress.XtraEditors.SimpleButton btnMessage;
        private DevExpress.XtraEditors.SimpleButton btnQQ;
    }
}