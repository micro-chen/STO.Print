//-------------------------------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2015 , STO , Ltd .
//-------------------------------------------------------------------------------------

using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using STO.Print.AddBillForm;

namespace STO.Print
{
    using DevExpress.Utils;
    using DevExpress.XtraEditors;
    using DotNet.Utilities;
    using Utilities;

    /// <summary>
    /// 设置默认打印机
    /// 
    /// 修改记录
    ///  
    ///     2015-07-20  版本：1.0 YangHengLian 创建
    /// 
    /// 版本：1.0
    ///
    /// <author>
    ///		<name>YangHengLian</name>
    ///		<date>2015-07-20</date>
    /// </author>
    /// </summary>
    public partial class FrmPrinterSetting : BaseForm
    {
        public FrmPrinterSetting()
        {
            InitializeComponent();
        }

        #region private void FrmPrinterSetting_Load(object sender, EventArgs e) 窗体加载事件
        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmPrinterSetting_Load(object sender, EventArgs e)
        {
            // 加载系统默认打印机信息到系统中
            var list = PrinterHelper.GetLocalPrinters();
            gcPrinter.DataSource = list;
            gvPrinter.Columns["Column"].Width = 22;
            gvPrinter.Columns["Column"].OptionsColumn.ShowCaption = false;
            gvPrinter.Columns["Column"].OptionsColumn.AllowSort = DefaultBoolean.False;
            var defaultPrinter = BillPrintHelper.GetDefaultPrinter();
            if (!string.IsNullOrEmpty(defaultPrinter))
            {
                lblDefaultPrinter.Text = @"默认打印机：" + defaultPrinter;
            }
        }
        #endregion

        #region private void gvPrinter_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e) 渲染GridControl序号
        /// <summary>
        /// 渲染GridControl序号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvPrinter_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            e.Appearance.TextOptions.HAlignment = HorzAlignment.Far;
            if (e.Info.IsRowIndicator)
            {
                if (e.RowHandle >= 0)
                {
                    e.Info.DisplayText = (e.RowHandle + 1).ToString(CultureInfo.InvariantCulture);
                }
                else if (e.RowHandle < 0 && e.RowHandle > -1000)
                {
                    e.Info.Appearance.BackColor = Color.AntiqueWhite;
                    e.Info.DisplayText = "G" + e.RowHandle;
                }
            }
        }
        #endregion

        #region private void btnSaveDefaultPrinter_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) 保存默认打印机
        /// <summary>
        /// 保存默认打印机
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveDefaultPrinter_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var printName = gvPrinter.GetFocusedDisplayText();
            if (!string.IsNullOrEmpty(printName))
            {
                BillPrintHelper.SaveDefaultPrinterName(printName);
                PrinterHelper.SetDefaultPrinter(printName);
                XtraMessageBox.Show("保存成功", AppMessage.MSG0000,MessageBoxButtons.OK,MessageBoxIcon.Information);
                var defaultPrinter = BillPrintHelper.GetDefaultPrinter();
                if (!string.IsNullOrEmpty(defaultPrinter))
                {
                    lblDefaultPrinter.Text = @"默认打印机：" + defaultPrinter;
                }
            }
        }
        #endregion

        #region private void FrmPrinterSetting_FormClosing(object sender, FormClosingEventArgs e) 窗体关闭
        /// <summary>
        /// 窗体关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmPrinterSetting_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
        #endregion

    }
}
