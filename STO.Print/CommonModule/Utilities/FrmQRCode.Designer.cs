namespace STO.Print
{
    partial class FrmQRCode
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmQRCode));
            this.label1 = new System.Windows.Forms.Label();
            this.txtSize = new DevExpress.XtraEditors.TextEdit();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cboVersion = new DevExpress.XtraEditors.ComboBoxEdit();
            this.btnCreateQRCode = new DevExpress.XtraEditors.SimpleButton();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.tbDecodeResult = new DevExpress.XtraEditors.TextEdit();
            this.label4 = new System.Windows.Forms.Label();
            this.btnClipboard = new DevExpress.XtraEditors.SimpleButton();
            this.btnDecodeQRCode = new DevExpress.XtraEditors.SimpleButton();
            this.btnOpenQRCode = new DevExpress.XtraEditors.SimpleButton();
            this.txtQRCodeContent = new DevExpress.XtraEditors.MemoEdit();
            this.picQRcode = new DevExpress.XtraEditors.PictureEdit();
            this.label5 = new System.Windows.Forms.Label();
            this.btnAddLogo = new DevExpress.XtraEditors.SimpleButton();
            this.txtLogoSize = new DevExpress.XtraEditors.TextEdit();
            this.label6 = new System.Windows.Forms.Label();
            this.picLogo = new DevExpress.XtraEditors.PictureEdit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSize.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboVersion.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbDecodeResult.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtQRCodeContent.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picQRcode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLogoSize.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "二维码内容";
            // 
            // txtSize
            // 
            this.txtSize.EditValue = "4";
            this.txtSize.Location = new System.Drawing.Point(85, 62);
            this.txtSize.Name = "txtSize";
            this.txtSize.Size = new System.Drawing.Size(66, 20);
            this.txtSize.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 14);
            this.label2.TabIndex = 2;
            this.label2.Text = "二维码尺寸";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(163, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 14);
            this.label3.TabIndex = 4;
            this.label3.Text = "二维码版本";
            // 
            // cboVersion
            // 
            this.cboVersion.Location = new System.Drawing.Point(236, 62);
            this.cboVersion.Name = "cboVersion";
            this.cboVersion.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboVersion.Size = new System.Drawing.Size(94, 20);
            this.cboVersion.TabIndex = 5;
            // 
            // btnCreateQRCode
            // 
            this.btnCreateQRCode.Location = new System.Drawing.Point(346, 61);
            this.btnCreateQRCode.Name = "btnCreateQRCode";
            this.btnCreateQRCode.Size = new System.Drawing.Size(75, 23);
            this.btnCreateQRCode.TabIndex = 6;
            this.btnCreateQRCode.Text = "生成";
            this.btnCreateQRCode.Click += new System.EventHandler(this.btnCreateQRCode_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(427, 61);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "保存";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.tbDecodeResult);
            this.groupControl1.Controls.Add(this.label4);
            this.groupControl1.Controls.Add(this.btnClipboard);
            this.groupControl1.Controls.Add(this.btnDecodeQRCode);
            this.groupControl1.Controls.Add(this.btnOpenQRCode);
            this.groupControl1.Location = new System.Drawing.Point(15, 88);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(585, 110);
            this.groupControl1.TabIndex = 8;
            this.groupControl1.Text = "二维码解码";
            // 
            // tbDecodeResult
            // 
            this.tbDecodeResult.Location = new System.Drawing.Point(82, 74);
            this.tbDecodeResult.Name = "tbDecodeResult";
            this.tbDecodeResult.Size = new System.Drawing.Size(494, 20);
            this.tbDecodeResult.TabIndex = 11;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 76);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 14);
            this.label4.TabIndex = 10;
            this.label4.Text = "二维码内容";
            // 
            // btnClipboard
            // 
            this.btnClipboard.Location = new System.Drawing.Point(221, 34);
            this.btnClipboard.Name = "btnClipboard";
            this.btnClipboard.Size = new System.Drawing.Size(113, 23);
            this.btnClipboard.TabIndex = 9;
            this.btnClipboard.Text = "复制到剪贴板";
            this.btnClipboard.Click += new System.EventHandler(this.btnClipboard_Click);
            // 
            // btnDecodeQRCode
            // 
            this.btnDecodeQRCode.Location = new System.Drawing.Point(123, 34);
            this.btnDecodeQRCode.Name = "btnDecodeQRCode";
            this.btnDecodeQRCode.Size = new System.Drawing.Size(75, 23);
            this.btnDecodeQRCode.TabIndex = 8;
            this.btnDecodeQRCode.Text = "解码";
            this.btnDecodeQRCode.Click += new System.EventHandler(this.btnDecodeQRCode_Click);
            // 
            // btnOpenQRCode
            // 
            this.btnOpenQRCode.Location = new System.Drawing.Point(16, 34);
            this.btnOpenQRCode.Name = "btnOpenQRCode";
            this.btnOpenQRCode.Size = new System.Drawing.Size(89, 23);
            this.btnOpenQRCode.TabIndex = 7;
            this.btnOpenQRCode.Text = "打开二维码...";
            this.btnOpenQRCode.Click += new System.EventHandler(this.btnOpenQRCode_Click);
            // 
            // txtQRCodeContent
            // 
            this.txtQRCodeContent.Location = new System.Drawing.Point(85, 11);
            this.txtQRCodeContent.Name = "txtQRCodeContent";
            this.txtQRCodeContent.Size = new System.Drawing.Size(510, 46);
            this.txtQRCodeContent.TabIndex = 1;
            // 
            // picQRcode
            // 
            this.picQRcode.Location = new System.Drawing.Point(15, 204);
            this.picQRcode.Name = "picQRcode";
            this.picQRcode.Size = new System.Drawing.Size(280, 280);
            this.picQRcode.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(303, 219);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 14);
            this.label5.TabIndex = 11;
            this.label5.Text = "自定义Logo";
            // 
            // btnAddLogo
            // 
            this.btnAddLogo.Location = new System.Drawing.Point(381, 215);
            this.btnAddLogo.Name = "btnAddLogo";
            this.btnAddLogo.Size = new System.Drawing.Size(79, 23);
            this.btnAddLogo.TabIndex = 12;
            this.btnAddLogo.Text = "添加...";
            this.btnAddLogo.Click += new System.EventHandler(this.btnAddLogo_Click);
            // 
            // txtLogoSize
            // 
            this.txtLogoSize.EditValue = "30";
            this.txtLogoSize.Location = new System.Drawing.Point(533, 217);
            this.txtLogoSize.Name = "txtLogoSize";
            this.txtLogoSize.Size = new System.Drawing.Size(67, 20);
            this.txtLogoSize.TabIndex = 14;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(469, 220);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(58, 14);
            this.label6.TabIndex = 13;
            this.label6.Text = "Logo尺寸";
            // 
            // picLogo
            // 
            this.picLogo.Location = new System.Drawing.Point(306, 255);
            this.picLogo.Name = "picLogo";
            this.picLogo.Size = new System.Drawing.Size(200, 200);
            this.picLogo.TabIndex = 15;
            // 
            // FrmQRCode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(616, 493);
            this.Controls.Add(this.picLogo);
            this.Controls.Add(this.txtLogoSize);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnAddLogo);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.picQRcode);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCreateQRCode);
            this.Controls.Add(this.cboVersion);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtSize);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtQRCodeContent);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmQRCode";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "二维码工具";
            this.Load += new System.EventHandler(this.FrmQRCode_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtSize.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboVersion.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbDecodeResult.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtQRCodeContent.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picQRcode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLogoSize.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.TextEdit txtSize;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private DevExpress.XtraEditors.ComboBoxEdit cboVersion;
        private DevExpress.XtraEditors.SimpleButton btnCreateQRCode;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.MemoEdit txtQRCodeContent;
        private DevExpress.XtraEditors.SimpleButton btnClipboard;
        private DevExpress.XtraEditors.SimpleButton btnDecodeQRCode;
        private DevExpress.XtraEditors.SimpleButton btnOpenQRCode;
        private DevExpress.XtraEditors.TextEdit tbDecodeResult;
        private System.Windows.Forms.Label label4;
        private DevExpress.XtraEditors.PictureEdit picQRcode;
        private System.Windows.Forms.Label label5;
        private DevExpress.XtraEditors.SimpleButton btnAddLogo;
        private DevExpress.XtraEditors.TextEdit txtLogoSize;
        private System.Windows.Forms.Label label6;
        private DevExpress.XtraEditors.PictureEdit picLogo;


    }
}