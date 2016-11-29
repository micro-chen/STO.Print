namespace STO.Print
{
    partial class FrmAddPrintBySelectedRow
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
            DevExpress.Utils.SuperToolTip superToolTip1 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipItem toolTipItem1 = new DevExpress.Utils.ToolTipItem();
            this.listPrintList = new DevExpress.XtraEditors.ListBoxControl();
            this.lblAddCount = new DevExpress.XtraEditors.LabelControl();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.cmbPrintNumber = new DevExpress.XtraEditors.SpinEdit();
            ((System.ComponentModel.ISupportInitialize)(this.listPrintList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbPrintNumber.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // listPrintList
            // 
            this.listPrintList.Dock = System.Windows.Forms.DockStyle.Left;
            this.listPrintList.Location = new System.Drawing.Point(0, 0);
            this.listPrintList.Name = "listPrintList";
            this.listPrintList.Size = new System.Drawing.Size(153, 277);
            this.listPrintList.TabIndex = 0;
            // 
            // lblAddCount
            // 
            this.lblAddCount.Location = new System.Drawing.Point(169, 22);
            this.lblAddCount.Name = "lblAddCount";
            this.lblAddCount.Size = new System.Drawing.Size(60, 14);
            this.lblAddCount.TabIndex = 1;
            this.lblAddCount.Text = "新增条数：";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(235, 234);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(88, 31);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "保存";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(342, 234);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(88, 31);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Location = new System.Drawing.Point(169, 124);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(143, 14);
            this.labelControl1.TabIndex = 5;
            this.labelControl1.Text = "此功能适合一票多件打印";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Location = new System.Drawing.Point(169, 158);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(221, 14);
            this.labelControl2.TabIndex = 6;
            this.labelControl2.Text = "保存后在关闭界面，获取电子面单号码";
            // 
            // cmbPrintNumber
            // 
            this.cmbPrintNumber.EditValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.cmbPrintNumber.Location = new System.Drawing.Point(169, 51);
            this.cmbPrintNumber.Name = "cmbPrintNumber";
            this.cmbPrintNumber.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbPrintNumber.Properties.Appearance.Options.UseFont = true;
            this.cmbPrintNumber.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.cmbPrintNumber.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.cmbPrintNumber.Properties.Mask.BeepOnError = true;
            this.cmbPrintNumber.Properties.Mask.EditMask = "[0-9]+?";
            this.cmbPrintNumber.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.cmbPrintNumber.Size = new System.Drawing.Size(261, 48);
            toolTipItem1.Text = "支持自定义打印数量";
            superToolTip1.Items.Add(toolTipItem1);
            this.cmbPrintNumber.SuperTip = superToolTip1;
            this.cmbPrintNumber.TabIndex = 12;
            this.cmbPrintNumber.EditValueChanged += new System.EventHandler(this.cmbPrintNumber_EditValueChanged);
            // 
            // FrmAddPrintBySelectedRow
            // 
            this.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(442, 277);
            this.Controls.Add(this.cmbPrintNumber);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.lblAddCount);
            this.Controls.Add(this.listPrintList);
            this.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmAddPrintBySelectedRow";
            this.Text = "新增选中打印记录";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmAddPrintBySelectedRow_FormClosing);
            this.Load += new System.EventHandler(this.FrmAddPrintBySelectedRow_Load);
            ((System.ComponentModel.ISupportInitialize)(this.listPrintList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbPrintNumber.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.ListBoxControl listPrintList;
        private DevExpress.XtraEditors.LabelControl lblAddCount;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.SpinEdit cmbPrintNumber;

    }
}