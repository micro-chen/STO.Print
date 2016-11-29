namespace STO.Print.UserControl
{
    partial class MyInputStartBillAndEndBill
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.txtEndBill = new DevExpress.XtraEditors.TextEdit();
            this.txtStartBill = new DevExpress.XtraEditors.TextEdit();
            this.btnSaveBill = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.lblTotal = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.cmbPrintBillCount = new DevExpress.XtraEditors.SpinEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtEndBill.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtStartBill.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbPrintBillCount.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.txtEndBill);
            this.panelControl1.Controls.Add(this.txtStartBill);
            this.panelControl1.Controls.Add(this.btnSaveBill);
            this.panelControl1.Controls.Add(this.labelControl4);
            this.panelControl1.Controls.Add(this.lblTotal);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.cmbPrintBillCount);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(508, 469);
            this.panelControl1.TabIndex = 0;
            // 
            // txtEndBill
            // 
            this.txtEndBill.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.txtEndBill.Location = new System.Drawing.Point(5, 179);
            this.txtEndBill.Name = "txtEndBill";
            this.txtEndBill.Properties.Appearance.BackColor = System.Drawing.Color.LemonChiffon;
            this.txtEndBill.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEndBill.Properties.Appearance.Options.UseBackColor = true;
            this.txtEndBill.Properties.Appearance.Options.UseFont = true;
            this.txtEndBill.Properties.Mask.EditMask = "[0-9]*";
            this.txtEndBill.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txtEndBill.Properties.NullValuePrompt = "例如：751811111111";
            this.txtEndBill.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtEndBill.Size = new System.Drawing.Size(498, 48);
            this.txtEndBill.TabIndex = 3;
            this.txtEndBill.Enter += new System.EventHandler(this.txtEndBill_Enter);
            this.txtEndBill.Leave += new System.EventHandler(this.txtEndBill_Leave);
            // 
            // txtStartBill
            // 
            this.txtStartBill.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.txtStartBill.Location = new System.Drawing.Point(5, 61);
            this.txtStartBill.Name = "txtStartBill";
            this.txtStartBill.Properties.Appearance.BackColor = System.Drawing.Color.LemonChiffon;
            this.txtStartBill.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtStartBill.Properties.Appearance.Options.UseBackColor = true;
            this.txtStartBill.Properties.Appearance.Options.UseFont = true;
            this.txtStartBill.Properties.Mask.EditMask = "[0-9]*";
            this.txtStartBill.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txtStartBill.Properties.NullValuePrompt = "例如：751811111110";
            this.txtStartBill.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtStartBill.Size = new System.Drawing.Size(498, 48);
            this.txtStartBill.TabIndex = 1;
            // 
            // btnSaveBill
            // 
            this.btnSaveBill.Appearance.Font = new System.Drawing.Font("Tahoma", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveBill.Appearance.Options.UseFont = true;
            this.btnSaveBill.Location = new System.Drawing.Point(5, 393);
            this.btnSaveBill.Name = "btnSaveBill";
            this.btnSaveBill.Size = new System.Drawing.Size(498, 60);
            this.btnSaveBill.TabIndex = 7;
            this.btnSaveBill.Text = "保存";
            this.btnSaveBill.Click += new System.EventHandler(this.btnSaveBill_Click);
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Tahoma", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl4.Location = new System.Drawing.Point(5, 320);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(245, 42);
            this.labelControl4.TabIndex = 5;
            this.labelControl4.Text = "需要打印数量：";
            // 
            // lblTotal
            // 
            this.lblTotal.Appearance.Font = new System.Drawing.Font("Tahoma", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.lblTotal.Location = new System.Drawing.Point(5, 245);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(105, 42);
            this.lblTotal.TabIndex = 4;
            this.lblTotal.Text = "总计：";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Location = new System.Drawing.Point(5, 116);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(210, 42);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "结束单号为：";
            // 
            // cmbPrintBillCount
            // 
            this.cmbPrintBillCount.EditValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.cmbPrintBillCount.Location = new System.Drawing.Point(256, 317);
            this.cmbPrintBillCount.Name = "cmbPrintBillCount";
            this.cmbPrintBillCount.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbPrintBillCount.Properties.Appearance.Options.UseFont = true;
            this.cmbPrintBillCount.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.cmbPrintBillCount.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.cmbPrintBillCount.Properties.Mask.BeepOnError = true;
            this.cmbPrintBillCount.Properties.Mask.EditMask = "[0-9]+?";
            this.cmbPrintBillCount.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.cmbPrintBillCount.Size = new System.Drawing.Size(247, 48);
            this.cmbPrintBillCount.TabIndex = 6;
            this.cmbPrintBillCount.EditValueChanged += new System.EventHandler(this.cmbPrintBillCount_EditValueChanged);
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Location = new System.Drawing.Point(5, 2);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(210, 42);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "起始单号为：";
            // 
            // MyInputStartBillAndEndBill
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControl1);
            this.Name = "MyInputStartBillAndEndBill";
            this.Size = new System.Drawing.Size(508, 469);
            this.Load += new System.EventHandler(this.MyInputStartBillAndEndBill_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtEndBill.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtStartBill.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbPrintBillCount.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SpinEdit cmbPrintBillCount;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl lblTotal;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.SimpleButton btnSaveBill;
        public DevExpress.XtraEditors.TextEdit txtStartBill;
        public DevExpress.XtraEditors.TextEdit txtEndBill;

    }
}
