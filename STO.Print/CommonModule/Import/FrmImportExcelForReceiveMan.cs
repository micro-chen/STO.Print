//-------------------------------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2015 , STO , Ltd .
//-------------------------------------------------------------------------------------

using System;
using System.Collections.Specialized;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace STO.Print
{
    using AddBillForm;
    using DevExpress.XtraEditors;
    using DotNet.Utilities;
    using Utilities;

    /// <summary>
    /// 导入收件人
    ///
    /// 修改纪录
    ///
    ///		2015-07-17  版本：1.0 YangHengLian 创建主键，注意命名空间的排序。
    ///       2016-2-1  复制Excel的加入
    /// 
    /// 版本：1.0
    ///
    /// <author>
    ///		<name>YangHengLian</name>
    ///		<date>2015-07-17</date>
    /// </author>
    /// </summary>
    public partial class FrmImportExcelForReceiveMan : BaseForm
    {

        public FrmImportExcelForReceiveMan()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 浏览Excel文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnOpenExcelClick(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog { Filter = STO.Print.Properties.Resources.FrmImportFreeExcel_BtnOpenExcelClick_Excel文件___xls___xlsx___csv })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    txtFileFullPath.Text = ofd.FileName;
                }
            }
        }

        /// <summary>
        /// 打开Excel文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnOpenFileClick(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtFileFullPath.Text))
                {
                    if (File.Exists(txtFileFullPath.Text.Trim()))
                    {
                        Process.Start(txtFileFullPath.Text);
                    }
                    else
                    {
                        btnOpenFile.ShowTip("源文件不存在");
                    }
                }
                else
                {
                    btnOpenFile.ShowTip("请选择Excel文件");
                }
            }
            catch (Exception exception)
            {
                ProcessException(exception);
            }
        }

        /// <summary>
        /// 导入Excel数据到表格中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnImportClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtFileFullPath.Text))
            {
                if (!File.Exists(txtFileFullPath.Text))
                {
                    btnImport.ShowTip("文件不存在，请选择正确的路径");
                    return;
                }
                DataTable chooseDt;
                if (Path.GetExtension(txtFileFullPath.Text) == ".csv")
                {
                    var readCsv = new ReadCsvHelper(txtFileFullPath.Text);
                    readCsv.CreateTable();
                    chooseDt = readCsv.GetResoultTable();
                }
                else
                {
                    chooseDt = ExcelHelper.ExcelToDataTable(txtFileFullPath.Text, 0, null);
                }
                gridControlBills.DataSource = chooseDt;
                gridViewBills.OptionsView.ColumnAutoWidth = false;
                gridViewBills.OptionsFind.AlwaysVisible = true;
                gridViewBills.FindPanelVisible = true;
                gridViewBills.ShowFindPanel();
                gridViewBills.BestFitColumns();
                if (chooseDt != null && chooseDt.Rows.Count > 0)
                {
                    var selectField = new FrmSelectFieldForReceiveMan(chooseDt);
                    selectField.ShowDialog();
                    selectField.Dispose();
                }
            }
            else
            {
                btnImport.ShowTip("请选择Excel文件");
            }
        }

        #region public void OpenFile(string fileName) 打开导出Excel文件
        /// <summary>
        /// 打开导出Excel文件
        /// </summary>
        /// <param name="fileName">文件完整路径</param>
        public void OpenFile(string fileName)
        {
            if (XtraMessageBox.Show(@"导出文件成功，是否打开？", AppMessage.MSG0000, MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                var process = new Process
                {
                    StartInfo =
                    {
                        FileName = fileName,
                        Verb = "Open",
                        WindowStyle = ProcessWindowStyle.Normal
                    }
                };
                process.Start();
            }
        }
        #endregion

        /// <summary>
        /// 窗体正在关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmImportFreeExcelFormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// 复制Excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCopyExcelClick(object sender, EventArgs e)
        {
            if (File.Exists(txtFileFullPath.Text))
            {
                var fileList = new StringCollection { txtFileFullPath.Text };
                STO.Print.Utilities.ClipboardHepler.SetFileDropList(fileList);
                btnCopyExcel.ShowTip("复制成功");
            }
            else
            {
                btnCopyExcel.ShowTip("源文件不存在");
            }
        }
    }
}
