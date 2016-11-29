namespace STO.Print
{
    partial class FrmBaiduLocation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmBaiduLocation));
            this.txtCountry = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.lblSendProvinceCityCounty = new DevExpress.XtraEditors.LabelControl();
            this.gcBaidu = new DevExpress.XtraEditors.GroupControl();
            this.btnDemo = new DevExpress.XtraEditors.SimpleButton();
            this.txtLocation = new DevExpress.XtraEditors.TextEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.txtStreet = new DevExpress.XtraEditors.TextEdit();
            this.txtTown = new DevExpress.XtraEditors.TextEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtProvinceCityCounty = new DevExpress.XtraEditors.TextEdit();
            this.txtAddress = new DevExpress.XtraEditors.MemoEdit();
            this.txtJson = new DevExpress.XtraEditors.MemoEdit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCountry.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcBaidu)).BeginInit();
            this.gcBaidu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtLocation.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtStreet.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTown.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtProvinceCityCounty.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAddress.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtJson.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // txtCountry
            // 
            this.txtCountry.Location = new System.Drawing.Point(118, 229);
            this.txtCountry.Name = "txtCountry";
            this.txtCountry.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCountry.Properties.Appearance.Options.UseFont = true;
            this.txtCountry.Size = new System.Drawing.Size(210, 20);
            this.txtCountry.TabIndex = 3;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(76, 230);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(36, 14);
            this.labelControl3.TabIndex = 10;
            this.labelControl3.Text = "国家：";
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(443, 281);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.Text = "查询";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(54, 106);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(58, 14);
            this.labelControl2.TabIndex = 9;
            this.labelControl2.Text = "返回json：";
            // 
            // lblSendProvinceCityCounty
            // 
            this.lblSendProvinceCityCounty.Location = new System.Drawing.Point(52, 37);
            this.lblSendProvinceCityCounty.Name = "lblSendProvinceCityCounty";
            this.lblSendProvinceCityCounty.Size = new System.Drawing.Size(60, 14);
            this.lblSendProvinceCityCounty.TabIndex = 8;
            this.lblSendProvinceCityCounty.Text = "地址信息：";
            // 
            // gcBaidu
            // 
            this.gcBaidu.Controls.Add(this.btnDemo);
            this.gcBaidu.Controls.Add(this.txtLocation);
            this.gcBaidu.Controls.Add(this.labelControl6);
            this.gcBaidu.Controls.Add(this.labelControl4);
            this.gcBaidu.Controls.Add(this.txtStreet);
            this.gcBaidu.Controls.Add(this.txtTown);
            this.gcBaidu.Controls.Add(this.labelControl5);
            this.gcBaidu.Controls.Add(this.labelControl1);
            this.gcBaidu.Controls.Add(this.txtProvinceCityCounty);
            this.gcBaidu.Controls.Add(this.txtCountry);
            this.gcBaidu.Controls.Add(this.labelControl3);
            this.gcBaidu.Controls.Add(this.btnSearch);
            this.gcBaidu.Controls.Add(this.labelControl2);
            this.gcBaidu.Controls.Add(this.lblSendProvinceCityCounty);
            this.gcBaidu.Controls.Add(this.txtAddress);
            this.gcBaidu.Controls.Add(this.txtJson);
            this.gcBaidu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcBaidu.Location = new System.Drawing.Point(0, 0);
            this.gcBaidu.Name = "gcBaidu";
            this.gcBaidu.Size = new System.Drawing.Size(678, 322);
            this.gcBaidu.TabIndex = 0;
            this.gcBaidu.Text = "通过百度识别地址";
            // 
            // btnDemo
            // 
            this.btnDemo.Location = new System.Drawing.Point(536, 281);
            this.btnDemo.Name = "btnDemo";
            this.btnDemo.Size = new System.Drawing.Size(75, 23);
            this.btnDemo.TabIndex = 15;
            this.btnDemo.Text = "查看演示";
            this.btnDemo.Click += new System.EventHandler(this.btnDemo_Click);
            // 
            // txtLocation
            // 
            this.txtLocation.Location = new System.Drawing.Point(118, 281);
            this.txtLocation.Name = "txtLocation";
            this.txtLocation.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLocation.Properties.Appearance.Options.UseFont = true;
            this.txtLocation.Size = new System.Drawing.Size(210, 20);
            this.txtLocation.TabIndex = 7;
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(76, 282);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(36, 14);
            this.labelControl6.TabIndex = 14;
            this.labelControl6.Text = "坐标：";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(401, 258);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(36, 14);
            this.labelControl4.TabIndex = 13;
            this.labelControl4.Text = "街道：";
            // 
            // txtStreet
            // 
            this.txtStreet.Location = new System.Drawing.Point(443, 255);
            this.txtStreet.Name = "txtStreet";
            this.txtStreet.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtStreet.Properties.Appearance.Options.UseFont = true;
            this.txtStreet.Size = new System.Drawing.Size(210, 20);
            this.txtStreet.TabIndex = 6;
            // 
            // txtTown
            // 
            this.txtTown.Location = new System.Drawing.Point(118, 255);
            this.txtTown.Name = "txtTown";
            this.txtTown.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTown.Properties.Appearance.Options.UseFont = true;
            this.txtTown.Size = new System.Drawing.Size(210, 20);
            this.txtTown.TabIndex = 5;
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(76, 256);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(36, 14);
            this.labelControl5.TabIndex = 12;
            this.labelControl5.Text = "乡镇：";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(389, 230);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(48, 14);
            this.labelControl1.TabIndex = 11;
            this.labelControl1.Text = "省市区：";
            // 
            // txtProvinceCityCounty
            // 
            this.txtProvinceCityCounty.Location = new System.Drawing.Point(443, 229);
            this.txtProvinceCityCounty.Name = "txtProvinceCityCounty";
            this.txtProvinceCityCounty.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtProvinceCityCounty.Properties.Appearance.Options.UseFont = true;
            this.txtProvinceCityCounty.Size = new System.Drawing.Size(210, 20);
            this.txtProvinceCityCounty.TabIndex = 4;
            // 
            // txtAddress
            // 
            this.txtAddress.Location = new System.Drawing.Point(118, 35);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAddress.Properties.Appearance.Options.UseFont = true;
            this.txtAddress.Size = new System.Drawing.Size(550, 60);
            this.txtAddress.TabIndex = 0;
            // 
            // txtJson
            // 
            this.txtJson.Location = new System.Drawing.Point(118, 103);
            this.txtJson.Name = "txtJson";
            this.txtJson.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtJson.Properties.Appearance.Options.UseFont = true;
            this.txtJson.Size = new System.Drawing.Size(550, 120);
            this.txtJson.TabIndex = 2;
            // 
            // FrmBaiduLocation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(678, 322);
            this.Controls.Add(this.gcBaidu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmBaiduLocation";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "百度地址识别";
            ((System.ComponentModel.ISupportInitialize)(this.txtCountry.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcBaidu)).EndInit();
            this.gcBaidu.ResumeLayout(false);
            this.gcBaidu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtLocation.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtStreet.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTown.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtProvinceCityCounty.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAddress.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtJson.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.TextEdit txtCountry;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.SimpleButton btnSearch;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl lblSendProvinceCityCounty;
        private DevExpress.XtraEditors.GroupControl gcBaidu;
        private DevExpress.XtraEditors.MemoEdit txtAddress;
        private DevExpress.XtraEditors.MemoEdit txtJson;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtProvinceCityCounty;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.TextEdit txtStreet;
        private DevExpress.XtraEditors.TextEdit txtTown;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.TextEdit txtLocation;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.SimpleButton btnDemo;
    }
}