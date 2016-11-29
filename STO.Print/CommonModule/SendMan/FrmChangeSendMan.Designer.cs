namespace STO.Print
{
    partial class FrmChangeSendMan
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
            DevExpress.Utils.SuperToolTip superToolTip2 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem2 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem2 = new DevExpress.Utils.ToolTipItem();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.txtReceiveManList = new DevExpress.XtraEditors.MemoEdit();
            this.btnOk = new DevExpress.XtraEditors.SimpleButton();
            this.ckChooseSendMan = new System.Windows.Forms.CheckBox();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.lblTotalNumber = new DevExpress.XtraEditors.LabelControl();
            this.txtSendMan = new DevExpress.XtraEditors.MemoEdit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtReceiveManList.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSendMan.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.txtReceiveManList);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.btnOk);
            this.splitContainerControl1.Panel2.Controls.Add(this.ckChooseSendMan);
            this.splitContainerControl1.Panel2.Controls.Add(this.labelControl2);
            this.splitContainerControl1.Panel2.Controls.Add(this.lblTotalNumber);
            this.splitContainerControl1.Panel2.Controls.Add(this.txtSendMan);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(649, 541);
            this.splitContainerControl1.SplitterPosition = 280;
            this.splitContainerControl1.TabIndex = 0;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // txtReceiveManList
            // 
            this.txtReceiveManList.AllowDrop = true;
            this.txtReceiveManList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtReceiveManList.EditValue = "";
            this.txtReceiveManList.Location = new System.Drawing.Point(0, 0);
            this.txtReceiveManList.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtReceiveManList.Name = "txtReceiveManList";
            this.txtReceiveManList.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReceiveManList.Properties.Appearance.Options.UseFont = true;
            this.txtReceiveManList.Properties.MaxLength = 14000;
            this.txtReceiveManList.Properties.NullValuePrompt = "一次最多1000个运单号";
            this.txtReceiveManList.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtReceiveManList.Properties.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtReceiveManList.Size = new System.Drawing.Size(280, 541);
            toolTipTitleItem2.Text = "系统提醒";
            toolTipItem2.LeftIndent = 6;
            toolTipItem2.Text = "一次最多1000条运单号！";
            superToolTip2.Items.Add(toolTipTitleItem2);
            superToolTip2.Items.Add(toolTipItem2);
            this.txtReceiveManList.SuperTip = superToolTip2;
            this.txtReceiveManList.TabIndex = 0;
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(13, 395);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(323, 48);
            this.btnOk.TabIndex = 3;
            this.btnOk.Text = "确定";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // ckChooseSendMan
            // 
            this.ckChooseSendMan.AutoSize = true;
            this.ckChooseSendMan.Location = new System.Drawing.Point(103, 109);
            this.ckChooseSendMan.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ckChooseSendMan.Name = "ckChooseSendMan";
            this.ckChooseSendMan.Size = new System.Drawing.Size(87, 21);
            this.ckChooseSendMan.TabIndex = 2;
            this.ckChooseSendMan.Text = "选择发件人";
            this.ckChooseSendMan.UseVisualStyleBackColor = true;
            this.ckChooseSendMan.CheckedChanged += new System.EventHandler(this.ckChooseSendMan_CheckedChanged);
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(13, 111);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(72, 14);
            this.labelControl2.TabIndex = 1;
            this.labelControl2.Text = "选择发件人：";
            // 
            // lblTotalNumber
            // 
            this.lblTotalNumber.Location = new System.Drawing.Point(13, 65);
            this.lblTotalNumber.Name = "lblTotalNumber";
            this.lblTotalNumber.Size = new System.Drawing.Size(48, 14);
            this.lblTotalNumber.TabIndex = 0;
            this.lblTotalNumber.Text = "总数量：";
            // 
            // txtSendMan
            // 
            this.txtSendMan.Location = new System.Drawing.Point(13, 137);
            this.txtSendMan.Name = "txtSendMan";
            this.txtSendMan.Size = new System.Drawing.Size(323, 229);
            this.txtSendMan.TabIndex = 4;
            // 
            // FrmChangeSendMan
            // 
            this.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(649, 541);
            this.Controls.Add(this.splitContainerControl1);
            this.Name = "FrmChangeSendMan";
            this.Text = "切换发件人";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmChangeSendMan_FormClosing);
            this.Load += new System.EventHandler(this.FrmChangeSendMan_Load);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtReceiveManList.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSendMan.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraEditors.MemoEdit txtReceiveManList;
        private DevExpress.XtraEditors.LabelControl lblTotalNumber;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private System.Windows.Forms.CheckBox ckChooseSendMan;
        private DevExpress.XtraEditors.SimpleButton btnOk;
        private DevExpress.XtraEditors.MemoEdit txtSendMan;
    }
}