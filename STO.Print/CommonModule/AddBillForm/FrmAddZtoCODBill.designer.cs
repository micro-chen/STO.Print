namespace STO.Print.AddBillForm
{
    partial class FrmAddZtoCODBill
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
            DevExpress.Utils.SuperToolTip superToolTip1 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipItem toolTipItem1 = new DevExpress.Utils.ToolTipItem();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAddZtoCODBill));
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.ckKeep = new System.Windows.Forms.CheckBox();
            this.tspReceive = new DevExpress.Utils.ToolTipController(this.components);
            this.ckTodaySend = new DevExpress.XtraEditors.CheckEdit();
            this.ckGetDefaultSendMan = new System.Windows.Forms.CheckBox();
            this.ckChooseReceiveMan = new System.Windows.Forms.CheckBox();
            this.txtSendAddress = new DevExpress.XtraEditors.MemoEdit();
            this.txtSendMan = new DevExpress.XtraEditors.TextEdit();
            this.txtReceiveMan = new DevExpress.XtraEditors.TextEdit();
            this.txtReceiveCompany = new DevExpress.XtraEditors.TextEdit();
            this.txtSendPhone = new DevExpress.XtraEditors.TextEdit();
            this.txtReceivePhone = new DevExpress.XtraEditors.TextEdit();
            this.txtPrintMark = new DevExpress.XtraEditors.TextEdit();
            this.txtSendDeparture = new DevExpress.XtraEditors.TextEdit();
            this.txtReceiveDestination = new DevExpress.XtraEditors.TextEdit();
            this.txtReceiveAddress = new System.Windows.Forms.TextBox();
            this.dgvSearchSendArea = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.dbFULLNAME = new DevExpress.XtraGrid.Columns.GridColumn();
            this.dbSIMPLESPELLING = new DevExpress.XtraGrid.Columns.GridColumn();
            this.dgvSearchReceiveArea = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridView6 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colFULLNAME = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSIMPLESPELLING = new DevExpress.XtraGrid.Columns.GridColumn();
            this.txtRemark = new DevExpress.XtraEditors.TextEdit();
            this.txtGoodsName = new DevExpress.XtraEditors.TextEdit();
            this.txtSendDate = new DevExpress.XtraEditors.DateEdit();
            this.txtWeight = new DevExpress.XtraEditors.TextEdit();
            this.txtNumber = new DevExpress.XtraEditors.TextEdit();
            this.txtSendCompany = new DevExpress.XtraEditors.TextEdit();
            this.txtGoodsPayMent = new DevExpress.XtraEditors.TextEdit();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.ckTodaySend.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSendAddress.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSendMan.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtReceiveMan.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtReceiveCompany.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSendPhone.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtReceivePhone.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPrintMark.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSendDeparture.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtReceiveDestination.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSearchSendArea.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSearchReceiveArea.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRemark.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtGoodsName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSendDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSendDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWeight.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumber.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSendCompany.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtGoodsPayMent.Properties)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(15, 276);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(87, 33);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "取消(&C)";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(15, 184);
            this.btnSave.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(87, 33);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "保存(&O)";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(15, 230);
            this.btnPrint.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(87, 33);
            this.btnPrint.TabIndex = 1;
            this.btnPrint.Text = "打印（&P）";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // ckKeep
            // 
            this.ckKeep.AutoSize = true;
            this.ckKeep.Checked = true;
            this.ckKeep.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckKeep.Location = new System.Drawing.Point(15, 405);
            this.ckKeep.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ckKeep.Name = "ckKeep";
            this.ckKeep.Size = new System.Drawing.Size(135, 21);
            this.ckKeep.TabIndex = 5;
            this.ckKeep.Text = "保存后清空页面数据";
            this.ckKeep.UseVisualStyleBackColor = true;
            // 
            // tspReceive
            // 
            this.tspReceive.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(139)))), ((int)(((byte)(222)))));
            this.tspReceive.Appearance.Options.UseBorderColor = true;
            this.tspReceive.Rounded = true;
            this.tspReceive.ShowBeak = true;
            // 
            // ckTodaySend
            // 
            this.ckTodaySend.Location = new System.Drawing.Point(15, 356);
            this.ckTodaySend.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ckTodaySend.Name = "ckTodaySend";
            this.ckTodaySend.Properties.Caption = "是否今天发货";
            this.ckTodaySend.Size = new System.Drawing.Size(132, 19);
            this.ckTodaySend.TabIndex = 6;
            this.ckTodaySend.CheckedChanged += new System.EventHandler(this.ckTodaySend_CheckedChanged);
            // 
            // ckGetDefaultSendMan
            // 
            this.ckGetDefaultSendMan.AutoSize = true;
            this.ckGetDefaultSendMan.Location = new System.Drawing.Point(15, 322);
            this.ckGetDefaultSendMan.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ckGetDefaultSendMan.Name = "ckGetDefaultSendMan";
            this.ckGetDefaultSendMan.Size = new System.Drawing.Size(135, 21);
            this.ckGetDefaultSendMan.TabIndex = 4;
            this.ckGetDefaultSendMan.Text = "加载系统默认发件人";
            this.ckGetDefaultSendMan.UseVisualStyleBackColor = true;
            this.ckGetDefaultSendMan.CheckedChanged += new System.EventHandler(this.ckGetDefaultSendMan_CheckedChanged);
            // 
            // ckChooseReceiveMan
            // 
            this.ckChooseReceiveMan.AutoSize = true;
            this.ckChooseReceiveMan.Location = new System.Drawing.Point(1121, 769);
            this.ckChooseReceiveMan.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ckChooseReceiveMan.Name = "ckChooseReceiveMan";
            this.ckChooseReceiveMan.Size = new System.Drawing.Size(87, 21);
            this.ckChooseReceiveMan.TabIndex = 7;
            this.ckChooseReceiveMan.Text = "选择收件人";
            this.ckChooseReceiveMan.UseVisualStyleBackColor = true;
            this.ckChooseReceiveMan.CheckedChanged += new System.EventHandler(this.ckChooseReceiveMan_CheckedChanged);
            // 
            // txtSendAddress
            // 
            this.txtSendAddress.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.txtSendAddress.Location = new System.Drawing.Point(164, 240);
            this.txtSendAddress.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtSendAddress.Name = "txtSendAddress";
            this.txtSendAddress.Properties.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.txtSendAddress.Properties.Appearance.BorderColor = System.Drawing.Color.Black;
            this.txtSendAddress.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 13F);
            this.txtSendAddress.Properties.Appearance.Options.UseBackColor = true;
            this.txtSendAddress.Properties.Appearance.Options.UseBorderColor = true;
            this.txtSendAddress.Properties.Appearance.Options.UseFont = true;
            this.txtSendAddress.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.txtSendAddress.Properties.MaxLength = 13;
            this.txtSendAddress.Properties.NullValuePrompt = "发件单位";
            this.txtSendAddress.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtSendAddress.Size = new System.Drawing.Size(335, 55);
            this.txtSendAddress.TabIndex = 3;
            this.txtSendAddress.Tag = "";
            this.txtSendAddress.Leave += new System.EventHandler(this.txtSendAddress_Leave);
            // 
            // txtSendMan
            // 
            this.txtSendMan.Location = new System.Drawing.Point(164, 168);
            this.txtSendMan.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtSendMan.Name = "txtSendMan";
            this.txtSendMan.Properties.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.txtSendMan.Properties.Appearance.BorderColor = System.Drawing.Color.Black;
            this.txtSendMan.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 13F);
            this.txtSendMan.Properties.Appearance.Options.UseBackColor = true;
            this.txtSendMan.Properties.Appearance.Options.UseBorderColor = true;
            this.txtSendMan.Properties.Appearance.Options.UseFont = true;
            this.txtSendMan.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
            this.txtSendMan.Properties.Mask.EditMask = "90:00:00>LL";
            this.txtSendMan.Properties.MaxLength = 10;
            this.txtSendMan.Properties.NullValuePrompt = "寄件人姓名";
            this.txtSendMan.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtSendMan.Size = new System.Drawing.Size(141, 28);
            this.txtSendMan.TabIndex = 0;
            this.txtSendMan.Tag = "";
            // 
            // txtReceiveMan
            // 
            this.txtReceiveMan.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.txtReceiveMan.Location = new System.Drawing.Point(613, 166);
            this.txtReceiveMan.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtReceiveMan.Name = "txtReceiveMan";
            this.txtReceiveMan.Properties.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.txtReceiveMan.Properties.Appearance.BorderColor = System.Drawing.Color.Black;
            this.txtReceiveMan.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 13F);
            this.txtReceiveMan.Properties.Appearance.Options.UseBackColor = true;
            this.txtReceiveMan.Properties.Appearance.Options.UseBorderColor = true;
            this.txtReceiveMan.Properties.Appearance.Options.UseFont = true;
            this.txtReceiveMan.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.txtReceiveMan.Properties.MaxLength = 10;
            this.txtReceiveMan.Properties.NullValuePrompt = "收件人姓名";
            this.txtReceiveMan.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtReceiveMan.Size = new System.Drawing.Size(147, 26);
            this.txtReceiveMan.TabIndex = 9;
            this.txtReceiveMan.Tag = "";
            // 
            // txtReceiveCompany
            // 
            this.txtReceiveCompany.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.txtReceiveCompany.Location = new System.Drawing.Point(604, 301);
            this.txtReceiveCompany.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtReceiveCompany.Name = "txtReceiveCompany";
            this.txtReceiveCompany.Properties.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.txtReceiveCompany.Properties.Appearance.BorderColor = System.Drawing.Color.Black;
            this.txtReceiveCompany.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 13F);
            this.txtReceiveCompany.Properties.Appearance.Options.UseBackColor = true;
            this.txtReceiveCompany.Properties.Appearance.Options.UseBorderColor = true;
            this.txtReceiveCompany.Properties.Appearance.Options.UseFont = true;
            this.txtReceiveCompany.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.txtReceiveCompany.Properties.MaxLength = 15;
            this.txtReceiveCompany.Size = new System.Drawing.Size(341, 26);
            this.txtReceiveCompany.TabIndex = 13;
            // 
            // txtSendPhone
            // 
            this.txtSendPhone.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.txtSendPhone.Location = new System.Drawing.Point(164, 349);
            this.txtSendPhone.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtSendPhone.Name = "txtSendPhone";
            this.txtSendPhone.Properties.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.txtSendPhone.Properties.Appearance.BorderColor = System.Drawing.Color.Black;
            this.txtSendPhone.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 13F);
            this.txtSendPhone.Properties.Appearance.Options.UseBackColor = true;
            this.txtSendPhone.Properties.Appearance.Options.UseBorderColor = true;
            this.txtSendPhone.Properties.Appearance.Options.UseFont = true;
            this.txtSendPhone.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.txtSendPhone.Properties.Mask.BeepOnError = true;
            this.txtSendPhone.Properties.NullValuePrompt = "手机或座机";
            this.txtSendPhone.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtSendPhone.Size = new System.Drawing.Size(335, 26);
            this.txtSendPhone.TabIndex = 5;
            this.txtSendPhone.Tag = "寄件人电话";
            // 
            // txtReceivePhone
            // 
            this.txtReceivePhone.Location = new System.Drawing.Point(604, 347);
            this.txtReceivePhone.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtReceivePhone.Name = "txtReceivePhone";
            this.txtReceivePhone.Properties.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.txtReceivePhone.Properties.Appearance.BorderColor = System.Drawing.Color.Black;
            this.txtReceivePhone.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 13F);
            this.txtReceivePhone.Properties.Appearance.Options.UseBackColor = true;
            this.txtReceivePhone.Properties.Appearance.Options.UseBorderColor = true;
            this.txtReceivePhone.Properties.Appearance.Options.UseFont = true;
            this.txtReceivePhone.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.txtReceivePhone.Properties.Mask.BeepOnError = true;
            this.txtReceivePhone.Properties.NullValuePrompt = "手机或座机";
            this.txtReceivePhone.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtReceivePhone.Size = new System.Drawing.Size(341, 26);
            this.txtReceivePhone.TabIndex = 14;
            this.txtReceivePhone.Tag = "收件人电话";
            // 
            // txtPrintMark
            // 
            this.txtPrintMark.EditValue = "";
            this.txtPrintMark.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.txtPrintMark.Location = new System.Drawing.Point(721, 555);
            this.txtPrintMark.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtPrintMark.Name = "txtPrintMark";
            this.txtPrintMark.Properties.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.txtPrintMark.Properties.Appearance.BorderColor = System.Drawing.Color.Black;
            this.txtPrintMark.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 42F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPrintMark.Properties.Appearance.Options.UseBackColor = true;
            this.txtPrintMark.Properties.Appearance.Options.UseBorderColor = true;
            this.txtPrintMark.Properties.Appearance.Options.UseFont = true;
            this.txtPrintMark.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.txtPrintMark.Properties.MaxLength = 15;
            this.txtPrintMark.Properties.NullValuePrompt = "申通机打大头笔";
            this.txtPrintMark.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtPrintMark.Size = new System.Drawing.Size(259, 72);
            toolTipItem1.Text = "1.发件省市区未填写，提取为手写大头笔\r\n2.发件和收件省市区都填写，提取机打大头笔\r\n";
            superToolTip1.Items.Add(toolTipItem1);
            this.txtPrintMark.SuperTip = superToolTip1;
            this.txtPrintMark.TabIndex = 18;
            this.txtPrintMark.Tag = "大头笔";
            // 
            // txtSendDeparture
            // 
            this.txtSendDeparture.EditValue = "";
            this.txtSendDeparture.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.txtSendDeparture.Location = new System.Drawing.Point(369, 168);
            this.txtSendDeparture.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtSendDeparture.Name = "txtSendDeparture";
            this.txtSendDeparture.Properties.Appearance.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.txtSendDeparture.Properties.Appearance.BorderColor = System.Drawing.Color.Black;
            this.txtSendDeparture.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSendDeparture.Properties.Appearance.Options.UseBackColor = true;
            this.txtSendDeparture.Properties.Appearance.Options.UseBorderColor = true;
            this.txtSendDeparture.Properties.Appearance.Options.UseFont = true;
            this.txtSendDeparture.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.txtSendDeparture.Properties.MaxLength = 10;
            this.txtSendDeparture.Properties.NullValuePrompt = "始发地";
            this.txtSendDeparture.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtSendDeparture.Size = new System.Drawing.Size(130, 26);
            this.txtSendDeparture.TabIndex = 1;
            // 
            // txtReceiveDestination
            // 
            this.txtReceiveDestination.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.txtReceiveDestination.Location = new System.Drawing.Point(821, 164);
            this.txtReceiveDestination.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtReceiveDestination.Name = "txtReceiveDestination";
            this.txtReceiveDestination.Properties.Appearance.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.txtReceiveDestination.Properties.Appearance.BorderColor = System.Drawing.Color.Black;
            this.txtReceiveDestination.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReceiveDestination.Properties.Appearance.Options.UseBackColor = true;
            this.txtReceiveDestination.Properties.Appearance.Options.UseBorderColor = true;
            this.txtReceiveDestination.Properties.Appearance.Options.UseFont = true;
            this.txtReceiveDestination.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.txtReceiveDestination.Properties.MaxLength = 10;
            this.txtReceiveDestination.Properties.NullValuePrompt = "目的地";
            this.txtReceiveDestination.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtReceiveDestination.Size = new System.Drawing.Size(110, 26);
            this.txtReceiveDestination.TabIndex = 10;
            this.txtReceiveDestination.Tag = "目的地";
            // 
            // txtReceiveAddress
            // 
            this.txtReceiveAddress.BackColor = System.Drawing.SystemColors.Menu;
            this.txtReceiveAddress.Location = new System.Drawing.Point(604, 249);
            this.txtReceiveAddress.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtReceiveAddress.MaxLength = 50;
            this.txtReceiveAddress.Multiline = true;
            this.txtReceiveAddress.Name = "txtReceiveAddress";
            this.txtReceiveAddress.Size = new System.Drawing.Size(340, 44);
            this.txtReceiveAddress.TabIndex = 12;
            this.txtReceiveAddress.Tag = "收件人详细地址";
            this.txtReceiveAddress.Leave += new System.EventHandler(this.txtReceiveAddress_Leave);
            // 
            // dgvSearchSendArea
            // 
            this.dgvSearchSendArea.Location = new System.Drawing.Point(164, 213);
            this.dgvSearchSendArea.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dgvSearchSendArea.Name = "dgvSearchSendArea";
            this.dgvSearchSendArea.Properties.Appearance.BackColor = System.Drawing.Color.LemonChiffon;
            this.dgvSearchSendArea.Properties.Appearance.Options.UseBackColor = true;
            this.dgvSearchSendArea.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dgvSearchSendArea.Properties.MaxLength = 50;
            this.dgvSearchSendArea.Properties.NullText = "";
            this.dgvSearchSendArea.Properties.NullValuePrompt = "格式：江苏省-苏州市-吴中区";
            this.dgvSearchSendArea.Properties.View = this.gridLookUpEdit1View;
            this.dgvSearchSendArea.Size = new System.Drawing.Size(335, 20);
            this.dgvSearchSendArea.TabIndex = 2;
            this.dgvSearchSendArea.ToolTip = "寄件区域来自系统数据，如无数据表示该省市区在系统无记录";
            this.dgvSearchSendArea.EditValueChanged += new System.EventHandler(this.dgvSearchSendArea_EditValueChanged);
            // 
            // gridLookUpEdit1View
            // 
            this.gridLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.dbFULLNAME,
            this.dbSIMPLESPELLING});
            this.gridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridLookUpEdit1View.Name = "gridLookUpEdit1View";
            this.gridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // dbFULLNAME
            // 
            this.dbFULLNAME.Caption = "区域名称";
            this.dbFULLNAME.FieldName = "FullName";
            this.dbFULLNAME.Name = "dbFULLNAME";
            this.dbFULLNAME.Visible = true;
            this.dbFULLNAME.VisibleIndex = 0;
            this.dbFULLNAME.Width = 260;
            // 
            // dbSIMPLESPELLING
            // 
            this.dbSIMPLESPELLING.Caption = "区县简拼";
            this.dbSIMPLESPELLING.FieldName = "SimpleSpelling";
            this.dbSIMPLESPELLING.Name = "dbSIMPLESPELLING";
            this.dbSIMPLESPELLING.Visible = true;
            this.dbSIMPLESPELLING.VisibleIndex = 1;
            this.dbSIMPLESPELLING.Width = 124;
            // 
            // dgvSearchReceiveArea
            // 
            this.dgvSearchReceiveArea.Location = new System.Drawing.Point(604, 214);
            this.dgvSearchReceiveArea.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dgvSearchReceiveArea.Name = "dgvSearchReceiveArea";
            this.dgvSearchReceiveArea.Properties.Appearance.BackColor = System.Drawing.Color.LemonChiffon;
            this.dgvSearchReceiveArea.Properties.Appearance.Options.UseBackColor = true;
            this.dgvSearchReceiveArea.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dgvSearchReceiveArea.Properties.MaxLength = 50;
            this.dgvSearchReceiveArea.Properties.NullText = "";
            this.dgvSearchReceiveArea.Properties.NullValuePrompt = "格式：江苏省-苏州市-吴中区";
            this.dgvSearchReceiveArea.Properties.View = this.gridView6;
            this.dgvSearchReceiveArea.Size = new System.Drawing.Size(341, 20);
            this.dgvSearchReceiveArea.TabIndex = 11;
            this.dgvSearchReceiveArea.ToolTip = "收件区域来自系统数据，如无数据表示该省市区在系统无记录";
            this.dgvSearchReceiveArea.EditValueChanged += new System.EventHandler(this.dgvSearchReceiveArea_EditValueChanged);
            // 
            // gridView6
            // 
            this.gridView6.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colFULLNAME,
            this.colSIMPLESPELLING});
            this.gridView6.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView6.Name = "gridView6";
            this.gridView6.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView6.OptionsView.ShowGroupPanel = false;
            // 
            // colFULLNAME
            // 
            this.colFULLNAME.Caption = "区域名称";
            this.colFULLNAME.FieldName = "FullName";
            this.colFULLNAME.Name = "colFULLNAME";
            this.colFULLNAME.Visible = true;
            this.colFULLNAME.VisibleIndex = 0;
            this.colFULLNAME.Width = 260;
            // 
            // colSIMPLESPELLING
            // 
            this.colSIMPLESPELLING.Caption = "区县简拼";
            this.colSIMPLESPELLING.FieldName = "SimpleSpelling";
            this.colSIMPLESPELLING.Name = "colSIMPLESPELLING";
            this.colSIMPLESPELLING.Visible = true;
            this.colSIMPLESPELLING.VisibleIndex = 1;
            this.colSIMPLESPELLING.Width = 124;
            // 
            // txtRemark
            // 
            this.txtRemark.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.txtRemark.Location = new System.Drawing.Point(133, 424);
            this.txtRemark.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Properties.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.txtRemark.Properties.Appearance.BorderColor = System.Drawing.Color.Black;
            this.txtRemark.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 13F);
            this.txtRemark.Properties.Appearance.Options.UseBackColor = true;
            this.txtRemark.Properties.Appearance.Options.UseBorderColor = true;
            this.txtRemark.Properties.Appearance.Options.UseFont = true;
            this.txtRemark.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.txtRemark.Properties.NullValuePrompt = "备注";
            this.txtRemark.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtRemark.Size = new System.Drawing.Size(170, 26);
            this.txtRemark.TabIndex = 7;
            // 
            // txtGoodsName
            // 
            this.txtGoodsName.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.txtGoodsName.Location = new System.Drawing.Point(133, 387);
            this.txtGoodsName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtGoodsName.Name = "txtGoodsName";
            this.txtGoodsName.Properties.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.txtGoodsName.Properties.Appearance.BorderColor = System.Drawing.Color.Black;
            this.txtGoodsName.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 13F);
            this.txtGoodsName.Properties.Appearance.Options.UseBackColor = true;
            this.txtGoodsName.Properties.Appearance.Options.UseBorderColor = true;
            this.txtGoodsName.Properties.Appearance.Options.UseFont = true;
            this.txtGoodsName.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.txtGoodsName.Properties.MaxLength = 15;
            this.txtGoodsName.Properties.NullValuePrompt = "品名";
            this.txtGoodsName.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtGoodsName.Size = new System.Drawing.Size(170, 26);
            this.txtGoodsName.TabIndex = 6;
            // 
            // txtSendDate
            // 
            this.txtSendDate.EditValue = null;
            this.txtSendDate.Location = new System.Drawing.Point(415, 616);
            this.txtSendDate.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtSendDate.Name = "txtSendDate";
            this.txtSendDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtSendDate.Properties.Mask.BeepOnError = true;
            this.txtSendDate.Properties.NullValuePrompt = "寄件时间";
            this.txtSendDate.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtSendDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtSendDate.Size = new System.Drawing.Size(112, 20);
            this.txtSendDate.TabIndex = 8;
            // 
            // txtWeight
            // 
            this.txtWeight.Location = new System.Drawing.Point(756, 441);
            this.txtWeight.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtWeight.Name = "txtWeight";
            this.txtWeight.Properties.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.txtWeight.Properties.Appearance.Options.UseBackColor = true;
            this.txtWeight.Properties.NullValuePrompt = "重量";
            this.txtWeight.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtWeight.Size = new System.Drawing.Size(94, 20);
            this.txtWeight.TabIndex = 15;
            // 
            // txtNumber
            // 
            this.txtNumber.Location = new System.Drawing.Point(889, 441);
            this.txtNumber.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtNumber.Name = "txtNumber";
            this.txtNumber.Properties.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.txtNumber.Properties.Appearance.Options.UseBackColor = true;
            this.txtNumber.Properties.NullValuePrompt = "数量";
            this.txtNumber.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtNumber.Size = new System.Drawing.Size(89, 20);
            this.txtNumber.TabIndex = 16;
            // 
            // txtSendCompany
            // 
            this.txtSendCompany.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.txtSendCompany.Location = new System.Drawing.Point(164, 305);
            this.txtSendCompany.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtSendCompany.Name = "txtSendCompany";
            this.txtSendCompany.Properties.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.txtSendCompany.Properties.Appearance.BorderColor = System.Drawing.Color.Black;
            this.txtSendCompany.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 13F);
            this.txtSendCompany.Properties.Appearance.Options.UseBackColor = true;
            this.txtSendCompany.Properties.Appearance.Options.UseBorderColor = true;
            this.txtSendCompany.Properties.Appearance.Options.UseFont = true;
            this.txtSendCompany.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.txtSendCompany.Properties.Mask.BeepOnError = true;
            this.txtSendCompany.Properties.Mask.EditMask = "(((\\d{3,4}-)|\\d{3,4}-)?\\d{7,8})|(\\d{11})";
            this.txtSendCompany.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txtSendCompany.Properties.MaxLength = 13;
            this.txtSendCompany.Properties.NullValuePrompt = "发件单位";
            this.txtSendCompany.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtSendCompany.Size = new System.Drawing.Size(335, 26);
            this.txtSendCompany.TabIndex = 4;
            this.txtSendCompany.Tag = "";
            // 
            // txtGoodsPayMent
            // 
            this.txtGoodsPayMent.Location = new System.Drawing.Point(597, 554);
            this.txtGoodsPayMent.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtGoodsPayMent.Name = "txtGoodsPayMent";
            this.txtGoodsPayMent.Properties.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.txtGoodsPayMent.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 42F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGoodsPayMent.Properties.Appearance.Options.UseBackColor = true;
            this.txtGoodsPayMent.Properties.Appearance.Options.UseFont = true;
            this.txtGoodsPayMent.Properties.NullValuePrompt = "代收货款";
            this.txtGoodsPayMent.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtGoodsPayMent.Size = new System.Drawing.Size(122, 74);
            this.txtGoodsPayMent.TabIndex = 17;
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Controls.Add(this.txtGoodsPayMent);
            this.panel1.Controls.Add(this.txtSendCompany);
            this.panel1.Controls.Add(this.txtNumber);
            this.panel1.Controls.Add(this.txtWeight);
            this.panel1.Controls.Add(this.txtSendDate);
            this.panel1.Controls.Add(this.txtGoodsName);
            this.panel1.Controls.Add(this.txtRemark);
            this.panel1.Controls.Add(this.dgvSearchReceiveArea);
            this.panel1.Controls.Add(this.dgvSearchSendArea);
            this.panel1.Controls.Add(this.txtReceiveAddress);
            this.panel1.Controls.Add(this.txtReceiveDestination);
            this.panel1.Controls.Add(this.txtSendDeparture);
            this.panel1.Controls.Add(this.txtPrintMark);
            this.panel1.Controls.Add(this.txtReceivePhone);
            this.panel1.Controls.Add(this.txtSendPhone);
            this.panel1.Controls.Add(this.txtReceiveCompany);
            this.panel1.Controls.Add(this.txtReceiveMan);
            this.panel1.Controls.Add(this.txtSendMan);
            this.panel1.Controls.Add(this.txtSendAddress);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1118, 730);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.labelControl1);
            this.panel2.Controls.Add(this.btnCancel);
            this.panel2.Controls.Add(this.btnPrint);
            this.panel2.Controls.Add(this.btnSave);
            this.panel2.Controls.Add(this.ckTodaySend);
            this.panel2.Controls.Add(this.ckGetDefaultSendMan);
            this.panel2.Controls.Add(this.ckKeep);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(1118, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(211, 730);
            this.panel2.TabIndex = 8;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(15, 382);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(132, 14);
            this.labelControl1.TabIndex = 7;
            this.labelControl1.Text = "（发件日期默认为今天）";
            // 
            // FrmAddZtoCODBill
            // 
            this.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(1329, 730);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.ckChooseReceiveMan);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.Name = "FrmAddZtoCODBill";
            this.Text = "添加申通COD面单";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Konad_FormClosing);
            this.Load += new System.EventHandler(this.FrmAddBill_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ckTodaySend.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSendAddress.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSendMan.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtReceiveMan.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtReceiveCompany.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSendPhone.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtReceivePhone.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPrintMark.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSendDeparture.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtReceiveDestination.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSearchSendArea.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSearchReceiveArea.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRemark.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtGoodsName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSendDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSendDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWeight.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumber.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSendCompany.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtGoodsPayMent.Properties)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.CheckBox ckKeep;
        private DevExpress.Utils.ToolTipController tspReceive;
        private DevExpress.XtraEditors.CheckEdit ckTodaySend;
        private System.Windows.Forms.CheckBox ckGetDefaultSendMan;
        private System.Windows.Forms.CheckBox ckChooseReceiveMan;
        private DevExpress.XtraEditors.MemoEdit txtSendAddress;
        private DevExpress.XtraEditors.TextEdit txtSendMan;
        private DevExpress.XtraEditors.TextEdit txtReceiveMan;
        private DevExpress.XtraEditors.TextEdit txtReceiveCompany;
        private DevExpress.XtraEditors.TextEdit txtSendPhone;
        private DevExpress.XtraEditors.TextEdit txtReceivePhone;
        private DevExpress.XtraEditors.TextEdit txtPrintMark;
        private DevExpress.XtraEditors.TextEdit txtSendDeparture;
        private DevExpress.XtraEditors.TextEdit txtReceiveDestination;
        private System.Windows.Forms.TextBox txtReceiveAddress;
        private DevExpress.XtraEditors.GridLookUpEdit dgvSearchSendArea;
        private DevExpress.XtraGrid.Views.Grid.GridView gridLookUpEdit1View;
        private DevExpress.XtraGrid.Columns.GridColumn dbFULLNAME;
        private DevExpress.XtraGrid.Columns.GridColumn dbSIMPLESPELLING;
        private DevExpress.XtraEditors.GridLookUpEdit dgvSearchReceiveArea;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView6;
        private DevExpress.XtraGrid.Columns.GridColumn colFULLNAME;
        private DevExpress.XtraGrid.Columns.GridColumn colSIMPLESPELLING;
        private DevExpress.XtraEditors.TextEdit txtRemark;
        private DevExpress.XtraEditors.TextEdit txtGoodsName;
        private DevExpress.XtraEditors.DateEdit txtSendDate;
        private DevExpress.XtraEditors.TextEdit txtWeight;
        private DevExpress.XtraEditors.TextEdit txtNumber;
        private DevExpress.XtraEditors.TextEdit txtSendCompany;
        private DevExpress.XtraEditors.TextEdit txtGoodsPayMent;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
    }
}