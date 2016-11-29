namespace STO.Print
{
    partial class FrmBarCode
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmBarCode));
            this.label1 = new System.Windows.Forms.Label();
            this.btnCreateQRCode = new DevExpress.XtraEditors.SimpleButton();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.txtQRCodeContent = new DevExpress.XtraEditors.MemoEdit();
            this.picQRcode = new DevExpress.XtraEditors.PictureEdit();
            this.cboVersion = new DevExpress.XtraEditors.ComboBoxEdit();
            this.label2 = new System.Windows.Forms.Label();
            this.txtInfo = new DevExpress.XtraEditors.MemoEdit();
            ((System.ComponentModel.ISupportInitialize)(this.txtQRCodeContent.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picQRcode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboVersion.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtInfo.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "条形码内容";
            // 
            // btnCreateQRCode
            // 
            this.btnCreateQRCode.Location = new System.Drawing.Point(437, 63);
            this.btnCreateQRCode.Name = "btnCreateQRCode";
            this.btnCreateQRCode.Size = new System.Drawing.Size(75, 23);
            this.btnCreateQRCode.TabIndex = 6;
            this.btnCreateQRCode.Text = "生成";
            this.btnCreateQRCode.Click += new System.EventHandler(this.btnCreateQRCode_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(518, 63);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "保存";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
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
            this.picQRcode.Location = new System.Drawing.Point(85, 100);
            this.picQRcode.Name = "picQRcode";
            this.picQRcode.Size = new System.Drawing.Size(510, 280);
            this.picQRcode.TabIndex = 9;
            // 
            // cboVersion
            // 
            this.cboVersion.Location = new System.Drawing.Point(85, 66);
            this.cboVersion.Name = "cboVersion";
            this.cboVersion.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboVersion.Size = new System.Drawing.Size(165, 20);
            this.cboVersion.TabIndex = 10;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 14);
            this.label2.TabIndex = 11;
            this.label2.Text = "条形码类型";
            // 
            // txtInfo
            // 
            this.txtInfo.EditValue = "";
            this.txtInfo.Location = new System.Drawing.Point(83, 386);
            this.txtInfo.Name = "txtInfo";
            this.txtInfo.Size = new System.Drawing.Size(510, 95);
            this.txtInfo.TabIndex = 12;
            // 
            // FrmBarCode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(616, 493);
            this.Controls.Add(this.txtInfo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cboVersion);
            this.Controls.Add(this.picQRcode);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCreateQRCode);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtQRCodeContent);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmBarCode";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "条形码工具";
            this.Load += new System.EventHandler(this.FrmBarCode_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtQRCodeContent.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picQRcode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboVersion.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtInfo.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.SimpleButton btnCreateQRCode;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.MemoEdit txtQRCodeContent;
        private DevExpress.XtraEditors.PictureEdit picQRcode;
        private DevExpress.XtraEditors.ComboBoxEdit cboVersion;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.MemoEdit txtInfo;


    }
}