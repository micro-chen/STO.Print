//-------------------------------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2015 , STO , Ltd .
//-------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using STO.Print.Utilities;

namespace STO.Print
{
    using STO.Print.AddBillForm;

    /// <summary>
    /// 导入EXCEL数据错误信息页面
    ///  
    /// 2015-07-18  版本：1.0 YangHengLian 创建
    /// 
    /// 版本：1.0
    ///
    /// <author>
    ///		<name>YangHengLian</name>
    ///		<date>2015-07-18</date>
    /// </author>
    /// </summary>
    public partial class FrmImportError : BaseForm
    {
        public List<object> errorList { get; set; }

        public FrmImportError()
        {
            InitializeComponent();
        }

        void FrmImportError_Load(object sender, EventArgs e)
        {
            gridControlImportError.DataSource = errorList;
            gridViewImportError.BestFitColumns();

            // 设置gridview列头的字体大小
            gridViewImportError.Appearance.HeaderPanel.Font = new Font("Tahoma", 9);

            // 设置gridview列头居中
            gridViewImportError.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
        }

        /// <summary>
        /// 隔行变色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewImportError_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            gridViewImportError.Appearance.OddRow.BackColor = Color.White;       // 设置奇数行颜色
            gridViewImportError.OptionsView.EnableAppearanceOddRow = true;       // 和上面代码绑定 同时使用有效 
            gridViewImportError.Appearance.EvenRow.BackColor = Color.LightBlue;  // 设置偶数行颜色 
            gridViewImportError.OptionsView.EnableAppearanceEvenRow = true;      // 和上面代码绑定 同时使用有效
        }

        /// <summary>
        /// 窗体快捷键
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmImportError_KeyDown(object sender, KeyEventArgs e)
        {
            // Ctrl+E 导出错误信息
            if (e.KeyCode == Keys.E && e.Modifiers == Keys.Control)
            {
                if (btnExportErrorExcel.Enabled)
                {
                    btnExportErrorExcel_Click(this, null);
                }
            }
        }

        /// <summary>
        /// 导出错误信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExportErrorExcel_Click(object sender, System.EventArgs e)
        {
            STO.Print.Utilities.ExportHelper.Export(ExportEnum.Xlsx, gridControlImportError, gridViewImportError);
        }

        /// <summary>
        /// 关闭窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FrmImportError_FormClosed(object sender, FormClosedEventArgs e)
        {
            btnClose_Click(sender, e);
        }
    }
}
