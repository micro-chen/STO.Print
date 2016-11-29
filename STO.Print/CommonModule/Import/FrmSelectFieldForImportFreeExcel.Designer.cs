namespace STO.Print
{
    partial class FrmSelectFieldForImportFreeExcel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSelectFieldForImportFreeExcel));
            this.butAllAdd = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lbOpposite = new System.Windows.Forms.ListBox();
            this.butRemove = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lbTarget = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lbSource = new System.Windows.Forms.ListBox();
            this.list1Next = new System.Windows.Forms.Button();
            this.list1Up = new System.Windows.Forms.Button();
            this.lblPrintMark = new System.Windows.Forms.Label();
            this.ImprnBut = new System.Windows.Forms.Button();
            this.butOnAdd = new System.Windows.Forms.Button();
            this.ckUseDefaultSendMan = new System.Windows.Forms.CheckBox();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // butAllAdd
            // 
            this.butAllAdd.Location = new System.Drawing.Point(457, 188);
            this.butAllAdd.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.butAllAdd.Name = "butAllAdd";
            this.butAllAdd.Size = new System.Drawing.Size(99, 33);
            this.butAllAdd.TabIndex = 5;
            this.butAllAdd.Text = "全部添加";
            this.butAllAdd.UseVisualStyleBackColor = true;
            this.butAllAdd.Click += new System.EventHandler(this.butAllAdd_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.AutoSize = true;
            this.groupBox3.BackColor = System.Drawing.Color.Transparent;
            this.groupBox3.Controls.Add(this.lbOpposite);
            this.groupBox3.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.groupBox3.Location = new System.Drawing.Point(561, 7);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox3.Size = new System.Drawing.Size(240, 533);
            this.groupBox3.TabIndex = 11;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "对应数据字段";
            // 
            // lbOpposite
            // 
            this.lbOpposite.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbOpposite.FormattingEnabled = true;
            this.lbOpposite.ItemHeight = 17;
            this.lbOpposite.Location = new System.Drawing.Point(3, 20);
            this.lbOpposite.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lbOpposite.Name = "lbOpposite";
            this.lbOpposite.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbOpposite.Size = new System.Drawing.Size(234, 509);
            this.lbOpposite.TabIndex = 2;
            this.lbOpposite.SelectedIndexChanged += new System.EventHandler(this.listBox3_SelectedIndexChanged);
            // 
            // butRemove
            // 
            this.butRemove.Enabled = false;
            this.butRemove.Location = new System.Drawing.Point(457, 305);
            this.butRemove.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.butRemove.Name = "butRemove";
            this.butRemove.Size = new System.Drawing.Size(99, 33);
            this.butRemove.TabIndex = 7;
            this.butRemove.Text = "移除";
            this.butRemove.UseVisualStyleBackColor = true;
            this.butRemove.Click += new System.EventHandler(this.butRemove_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.AutoSize = true;
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.Controls.Add(this.lbTarget);
            this.groupBox2.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.groupBox2.Location = new System.Drawing.Point(257, 7);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Size = new System.Drawing.Size(196, 565);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "数据库字段";
            // 
            // lbTarget
            // 
            this.lbTarget.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbTarget.FormattingEnabled = true;
            this.lbTarget.ItemHeight = 17;
            this.lbTarget.Location = new System.Drawing.Point(3, 20);
            this.lbTarget.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lbTarget.Name = "lbTarget";
            this.lbTarget.Size = new System.Drawing.Size(190, 541);
            this.lbTarget.TabIndex = 3;
            this.lbTarget.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.AutoSize = true;
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.lbSource);
            this.groupBox1.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.groupBox1.Location = new System.Drawing.Point(10, 7);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Size = new System.Drawing.Size(196, 565);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Excel数据字段";
            // 
            // lbSource
            // 
            this.lbSource.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbSource.FormattingEnabled = true;
            this.lbSource.ItemHeight = 17;
            this.lbSource.Location = new System.Drawing.Point(3, 20);
            this.lbSource.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lbSource.Name = "lbSource";
            this.lbSource.Size = new System.Drawing.Size(190, 541);
            this.lbSource.TabIndex = 4;
            this.lbSource.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // list1Next
            // 
            this.list1Next.Enabled = false;
            this.list1Next.Location = new System.Drawing.Point(216, 301);
            this.list1Next.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.list1Next.Name = "list1Next";
            this.list1Next.Size = new System.Drawing.Size(33, 85);
            this.list1Next.TabIndex = 2;
            this.list1Next.Text = "下移";
            this.list1Next.UseVisualStyleBackColor = true;
            this.list1Next.Click += new System.EventHandler(this.list1Next_Click);
            // 
            // list1Up
            // 
            this.list1Up.Enabled = false;
            this.list1Up.Location = new System.Drawing.Point(216, 191);
            this.list1Up.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.list1Up.Name = "list1Up";
            this.list1Up.Size = new System.Drawing.Size(33, 80);
            this.list1Up.TabIndex = 1;
            this.list1Up.Text = "上移";
            this.list1Up.UseVisualStyleBackColor = true;
            this.list1Up.Click += new System.EventHandler(this.list1Up_Click);
            // 
            // lblPrintMark
            // 
            this.lblPrintMark.AutoSize = true;
            this.lblPrintMark.Location = new System.Drawing.Point(481, 551);
            this.lblPrintMark.Name = "lblPrintMark";
            this.lblPrintMark.Size = new System.Drawing.Size(68, 17);
            this.lblPrintMark.TabIndex = 8;
            this.lblPrintMark.Text = "提取大头笔";
            this.lblPrintMark.Visible = false;
            // 
            // ImprnBut
            // 
            this.ImprnBut.Enabled = false;
            this.ImprnBut.Location = new System.Drawing.Point(457, 363);
            this.ImprnBut.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ImprnBut.Name = "ImprnBut";
            this.ImprnBut.Size = new System.Drawing.Size(99, 33);
            this.ImprnBut.TabIndex = 8;
            this.ImprnBut.Text = "导入";
            this.ImprnBut.UseVisualStyleBackColor = true;
            this.ImprnBut.Click += new System.EventHandler(this.ImprnBut_Click);
            // 
            // butOnAdd
            // 
            this.butOnAdd.Enabled = false;
            this.butOnAdd.Location = new System.Drawing.Point(457, 247);
            this.butOnAdd.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.butOnAdd.Name = "butOnAdd";
            this.butOnAdd.Size = new System.Drawing.Size(99, 33);
            this.butOnAdd.TabIndex = 6;
            this.butOnAdd.Text = "单个添加";
            this.butOnAdd.UseVisualStyleBackColor = true;
            this.butOnAdd.Click += new System.EventHandler(this.ButOnAddClick);
            // 
            // ckUseDefaultSendMan
            // 
            this.ckUseDefaultSendMan.AutoSize = true;
            this.ckUseDefaultSendMan.Checked = true;
            this.ckUseDefaultSendMan.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckUseDefaultSendMan.ForeColor = System.Drawing.Color.Blue;
            this.ckUseDefaultSendMan.Location = new System.Drawing.Point(456, 546);
            this.ckUseDefaultSendMan.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ckUseDefaultSendMan.Name = "ckUseDefaultSendMan";
            this.ckUseDefaultSendMan.Size = new System.Drawing.Size(243, 21);
            this.ckUseDefaultSendMan.TabIndex = 14;
            this.ckUseDefaultSendMan.Text = "使用默认发件人（建议使用默认发件人）";
            this.ckUseDefaultSendMan.UseVisualStyleBackColor = true;
            this.ckUseDefaultSendMan.CheckedChanged += new System.EventHandler(this.ckUseDefaultSendMan_CheckedChanged);
            // 
            // FrmSelectField
            // 
            this.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(804, 580);
            this.Controls.Add(this.ckUseDefaultSendMan);
            this.Controls.Add(this.butOnAdd);
            this.Controls.Add(this.ImprnBut);
            this.Controls.Add(this.list1Next);
            this.Controls.Add(this.butRemove);
            this.Controls.Add(this.list1Up);
            this.Controls.Add(this.butAllAdd);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblPrintMark);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.MaximizeBox = false;
            this.Name = "FrmSelectField";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "导入Excel数据";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Improve_FormClosing);
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button butRemove;
        private System.Windows.Forms.Button butOnAdd;
        private System.Windows.Forms.ListBox lbOpposite;
        private System.Windows.Forms.ListBox lbTarget;
        private System.Windows.Forms.ListBox lbSource;
        private System.Windows.Forms.Button ImprnBut;
        private System.Windows.Forms.Label lblPrintMark;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button butAllAdd;
        private System.Windows.Forms.Button list1Next;
        private System.Windows.Forms.Button list1Up;
        private System.Windows.Forms.CheckBox ckUseDefaultSendMan;
    }
}