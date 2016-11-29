namespace STO.Print
{
    partial class FrmFeedBack
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmFeedBack));
            this.txtToUser = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtSubject = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.gbFeedBack = new System.Windows.Forms.GroupBox();
            this.txtLine = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.btnSend = new DevExpress.XtraBars.BarButtonItem();
            this.btnQQ = new DevExpress.XtraBars.BarButtonItem();
            this.btnJoinQQGroup = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.richTextBox1 = new STO.Print.UserControl.MyRichEdit();
            ((System.ComponentModel.ISupportInitialize)(this.txtToUser.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSubject.Properties)).BeginInit();
            this.panel1.SuspendLayout();
            this.gbFeedBack.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtLine.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtToUser
            // 
            this.txtToUser.EditValue = "766096823@qq.com";
            this.txtToUser.Enabled = false;
            this.txtToUser.Location = new System.Drawing.Point(66, 35);
            this.txtToUser.Name = "txtToUser";
            this.txtToUser.Properties.Appearance.BackColor = System.Drawing.Color.LemonChiffon;
            this.txtToUser.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.txtToUser.Properties.Appearance.Options.UseBackColor = true;
            this.txtToUser.Properties.Appearance.Options.UseFont = true;
            this.txtToUser.Properties.Mask.EditMask = "[0-9]*";
            this.txtToUser.Properties.MaxLength = 12;
            this.txtToUser.Properties.NullValuePrompt = "申通运单号";
            this.txtToUser.Size = new System.Drawing.Size(545, 20);
            this.txtToUser.TabIndex = 1;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(20, 38);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(36, 14);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "收件人";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(30, 68);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(24, 14);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "标题";
            // 
            // txtSubject
            // 
            this.txtSubject.EditValue = "";
            this.txtSubject.Location = new System.Drawing.Point(66, 66);
            this.txtSubject.Name = "txtSubject";
            this.txtSubject.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.txtSubject.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.txtSubject.Properties.Appearance.Options.UseBackColor = true;
            this.txtSubject.Properties.Appearance.Options.UseFont = true;
            this.txtSubject.Properties.MaxLength = 50;
            this.txtSubject.Size = new System.Drawing.Size(545, 20);
            this.txtSubject.TabIndex = 3;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(30, 132);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(24, 14);
            this.labelControl3.TabIndex = 6;
            this.labelControl3.Text = "内容";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.richTextBox1);
            this.panel1.Location = new System.Drawing.Point(66, 132);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(545, 297);
            this.panel1.TabIndex = 7;
            // 
            // gbFeedBack
            // 
            this.gbFeedBack.Controls.Add(this.txtLine);
            this.gbFeedBack.Controls.Add(this.labelControl4);
            this.gbFeedBack.Controls.Add(this.panel1);
            this.gbFeedBack.Controls.Add(this.labelControl3);
            this.gbFeedBack.Controls.Add(this.txtSubject);
            this.gbFeedBack.Controls.Add(this.labelControl2);
            this.gbFeedBack.Controls.Add(this.labelControl1);
            this.gbFeedBack.Controls.Add(this.txtToUser);
            this.gbFeedBack.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbFeedBack.Location = new System.Drawing.Point(0, 31);
            this.gbFeedBack.Name = "gbFeedBack";
            this.gbFeedBack.Size = new System.Drawing.Size(635, 441);
            this.gbFeedBack.TabIndex = 0;
            this.gbFeedBack.TabStop = false;
            this.gbFeedBack.Text = "用户反馈";
            // 
            // txtLine
            // 
            this.txtLine.EditValue = "";
            this.txtLine.Location = new System.Drawing.Point(66, 95);
            this.txtLine.Name = "txtLine";
            this.txtLine.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.txtLine.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.txtLine.Properties.Appearance.Options.UseBackColor = true;
            this.txtLine.Properties.Appearance.Options.UseFont = true;
            this.txtLine.Properties.MaxLength = 50;
            this.txtLine.Properties.NullValuePrompt = "建议填写QQ号码或邮箱";
            this.txtLine.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtLine.Size = new System.Drawing.Size(545, 20);
            this.txtLine.TabIndex = 5;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(6, 97);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(48, 14);
            this.labelControl4.TabIndex = 4;
            this.labelControl4.Text = "联系方式";
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.btnSend,
            this.btnQQ,
            this.btnJoinQQGroup});
            this.barManager1.MaxItemId = 6;
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnSend, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnQQ, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnJoinQQGroup, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "Tools";
            // 
            // btnSend
            // 
            this.btnSend.Caption = "发送";
            this.btnSend.Id = 0;
            this.btnSend.Name = "btnSend";
            this.btnSend.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnSend_ItemClick);
            // 
            // btnQQ
            // 
            this.btnQQ.Caption = "QQ客服";
            this.btnQQ.Id = 1;
            this.btnQQ.Name = "btnQQ";
            this.btnQQ.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnQQ_ItemClick);
            // 
            // btnJoinQQGroup
            // 
            this.btnJoinQQGroup.Caption = "加入交流群";
            this.btnJoinQQGroup.Id = 4;
            this.btnJoinQQGroup.Name = "btnJoinQQGroup";
            this.btnJoinQQGroup.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnJoinQQGroup_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(635, 31);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 472);
            this.barDockControlBottom.Size = new System.Drawing.Size(635, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 31);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 441);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(635, 31);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 441);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.Location = new System.Drawing.Point(0, 0);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(545, 297);
            this.richTextBox1.TabIndex = 0;
            // 
            // FrmFeedBack
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(635, 472);
            this.Controls.Add(this.gbFeedBack);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmFeedBack";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "用户反馈";
            this.Load += new System.EventHandler(this.FrmFeedBack_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtToUser.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSubject.Properties)).EndInit();
            this.panel1.ResumeLayout(false);
            this.gbFeedBack.ResumeLayout(false);
            this.gbFeedBack.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtLine.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.TextEdit txtToUser;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit txtSubject;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox gbFeedBack;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarButtonItem btnSend;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem btnQQ;
        private DevExpress.XtraBars.BarButtonItem btnJoinQQGroup;
        private DevExpress.XtraEditors.TextEdit txtLine;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private UserControl.MyRichEdit richTextBox1;

    }
}