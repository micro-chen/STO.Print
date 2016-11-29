//-------------------------------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2015 , STO , Ltd .
//-------------------------------------------------------------------------------------

using DevExpress.XtraCharts;
using System;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace STO.Print.Utilities
{
    using DevExpress.XtraEditors;
    using DevExpress.XtraGrid;
    using DevExpress.XtraGrid.Views.Grid;
    using DevExpress.XtraPrinting;
    using DotNet.Utilities;

    /// <summary>
    /// 导出GridView内容管理类
    ///
    /// 修改纪录
    ///
    ///		  2014-08-07  版本：1.0 YangHengLian 创建主键，注意命名空间的排序。
    ///       2014-08-23  导出功能里，提示该文件已存在，是否覆盖，点击否，无法取消弹框(bug修复）
    /// 
    /// 版本：1.0
    ///
    /// <author>
    ///		<name>YangHengLian</name>
    ///		<date>2014-08-07</date>
    /// </author>
    /// </summary>
    public static class ExportHelper
    {

        public static void Export(ExportEnum exportEnum, GridControl grid, GridView gv)
        {
            if (gv.RowCount == 0)
            {
                XtraMessageBox.Show("本地暂无数据，请添加", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            try
            {
                switch (exportEnum)
                {
                    case ExportEnum.Word:
                        gv.ShowPrintPreview();
                        // ExprotWord(grid);
                        break;
                    case ExportEnum.Pdf:
                        ExportPdf(gv);
                        break;
                    case ExportEnum.Csv:
                        ExportCsv(gv);
                        break;
                    case ExportEnum.Xls:
                        ExportXls(gv);
                        break;
                    case ExportEnum.Xlsx:
                        ExportXlsx(gv);
                        break;
                    case ExportEnum.Html:
                        ExportHtml(gv);
                        break;

                }
            }
            catch (Exception ex)
            {
                LogUtil.WriteException(ex);
            }
        }
        /// <summary>
        /// 导出PDF
        /// </summary>
        private static void ExportPdf(GridView gv)
        {
            var saveFileDialog = new SaveFileDialog { FileName = DateTime.Now.ToString(BaseSystemInfo.DateFormat) + "打印数据", Title = @"导出PDF", Filter = @"PDF文件(*.pdf)|*.pdf", OverwritePrompt = false };//已存在文件是否覆盖提示
            var dialogResult = saveFileDialog.ShowDialog();
            if (dialogResult != DialogResult.OK) return;
            while (System.IO.File.Exists(saveFileDialog.FileName) && XtraMessageBox.Show("该文件名已存在，是否覆盖？", AppMessage.MSG0000, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                if (dialogResult != DialogResult.Yes)
                {
                    return;
                }
            }
            if (string.IsNullOrEmpty(saveFileDialog.FileName)) return;
            try
            {
                gv.ExportToPdf(saveFileDialog.FileName);
                OpenFile(saveFileDialog.FileName);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.Contains("正由另一进程使用") ? "数据导出失败！文件正由另一个程序占用！" : ex.Message, AppMessage.MSG0000);
            }
        }

        /// <summary>
        /// 导出CSV
        /// </summary>
        /// <param name="gv"></param>
        private static void ExportCsv(GridView gv)
        {
            var saveFileDialog = new SaveFileDialog { FileName = DateTime.Now.ToString(BaseSystemInfo.DateFormat) + "打印数据", Title = @"导出Csv", Filter = @"Csv文件(*.csv)|*.csv", OverwritePrompt = false };//已存在文件是否覆盖提示
            var dialogResult = saveFileDialog.ShowDialog();
            if (dialogResult != DialogResult.OK) return;
            while (System.IO.File.Exists(saveFileDialog.FileName) && XtraMessageBox.Show("该文件名已存在，是否覆盖？", AppMessage.MSG0000, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                if (dialogResult != DialogResult.Yes) return;
            }
            if (string.IsNullOrEmpty(saveFileDialog.FileName)) return;
            try
            {
                var options = new CsvExportOptions
                                  {
                                      TextExportMode = TextExportMode.Text
                                  };
                if (gv.Columns.ColumnByFieldName("Check") != null)
                {
                    gv.Columns["Check"].Visible = false;
                }
                gv.ExportToCsv(saveFileDialog.FileName, options);
                if (gv.Columns.ColumnByFieldName("Check") != null)
                {
                    gv.Columns["Check"].Visible = true;
                }
                OpenFile(saveFileDialog.FileName);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.Contains("正由另一进程使用") ? "数据导出失败！文件正由另一个程序占用！" : ex.Message, AppMessage.MSG0000);
            }
        }

        /// <summary>
        /// 导出Excel(xls)
        /// </summary>
        /// <param name="gv"></param>
        private static void ExportXls(GridView gv)
        {
            var saveFileDialog = new SaveFileDialog { FileName = DateTime.Now.ToString(BaseSystemInfo.DateFormat) + "打印数据", Title = @"导出Excel", Filter = @"Excel文件(*.xls)|*.xls", OverwritePrompt = false };//已存在文件是否覆盖提示
            var dialogResult = saveFileDialog.ShowDialog();
            if (dialogResult != DialogResult.OK) return;
            while (System.IO.File.Exists(saveFileDialog.FileName) && XtraMessageBox.Show("该文件名已存在，是否覆盖？", AppMessage.MSG0000, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                if (dialogResult != DialogResult.Yes) return;
            }
            if (string.IsNullOrEmpty(saveFileDialog.FileName)) return;
            try
            {
                var options = new XlsExportOptions
                {
                    // Excel 导出Sheet名称
                    SheetName = string.Format("{0}商家的打印数据-{1}", BaseSystemInfo.UserInfo.NickName, DateTime.Now.ToString(BaseSystemInfo.DateTimeFormat)),
                    TextExportMode = TextExportMode.Text,
                    ShowGridLines = true
                };
                if (gv.Columns.ColumnByFieldName("Check") != null)
                {
                    gv.Columns["Check"].Visible = false;
                }
                gv.ExportToXls(saveFileDialog.FileName, options);
                if (gv.Columns.ColumnByFieldName("Check") != null)
                {
                    gv.Columns["Check"].Visible = true;
                }
                //  ExcelHelper.SetAutoFilter(saveFileDialog.FileName);
                OpenFile(saveFileDialog.FileName);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.Contains("正由另一进程使用") ? "数据导出失败！文件正由另一个程序占用！" :ex.Message, AppMessage.MSG0000);
            }
        }

        /// <summary>
        /// 导出Excel(xlsx)
        /// </summary>
        /// <param name="gv"></param>
        private static void ExportXlsx(GridView gv)
        {
            var saveFileDialog = new SaveFileDialog { FileName = DateTime.Now.ToString(BaseSystemInfo.DateFormat) + "打印数据", Title = @"导出Excel", Filter = @"Excel文件(*.xlsx)|*.xlsx", OverwritePrompt = false };//已存在文件是否覆盖提示
            var dialogResult = saveFileDialog.ShowDialog();
            if (dialogResult != DialogResult.OK) return;
            while (System.IO.File.Exists(saveFileDialog.FileName) && XtraMessageBox.Show("该文件名已存在，是否覆盖？", AppMessage.MSG0000, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                if (dialogResult != DialogResult.Yes) return;
            }
            if (string.IsNullOrEmpty(saveFileDialog.FileName)) return;
            try
            {
                var options = new XlsxExportOptions
                {
                    //Excel 导出Sheet名称
                    SheetName = string.Format("{0}商家的打印数据-{1}", BaseSystemInfo.UserInfo.NickName, DateTime.Now.ToString(BaseSystemInfo.DateTimeFormat)),
                    TextExportMode = TextExportMode.Text,
                    ShowGridLines = true
                };
                if (gv.Columns.ColumnByFieldName("Check") != null)
                {
                    gv.Columns["Check"].Visible = false;
                }
                gv.ExportToXlsx(saveFileDialog.FileName, options);
                if (gv.Columns.ColumnByFieldName("Check") != null)
                {
                    gv.Columns["Check"].Visible = true;
                }
                // ExcelHelper.SetAutoFilter(saveFileDialog.FileName);
                OpenFile(saveFileDialog.FileName);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.Contains("正由另一进程使用") ? "数据导出失败！文件正由另一个程序占用！" : ex.Message, AppMessage.MSG0000);
            }
        }

        /// <summary>
        /// 导出HTML
        /// </summary>
        /// <param name="gv"></param>
        private static void ExportHtml(GridView gv)
        {
            var saveFileDialog = new SaveFileDialog { Title = @"导出HTML", Filter = @"HTML文件(*.html)|*.html", OverwritePrompt = false };//已存在文件是否覆盖提示
            var dialogResult = saveFileDialog.ShowDialog();
            if (dialogResult != DialogResult.OK) return;
            while (System.IO.File.Exists(saveFileDialog.FileName) && XtraMessageBox.Show("该文件名已存在，是否覆盖？", AppMessage.MSG0000, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                if (dialogResult != DialogResult.Yes) return;
            }
            if (string.IsNullOrEmpty(saveFileDialog.FileName)) return;
            try
            {
                gv.ExportToHtml(saveFileDialog.FileName);
                if (XtraMessageBox.Show("导出html成功，是否打开？", AppMessage.MSG0000, MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    var process = new Process
                        {
                            StartInfo =
                                {
                                    FileName = saveFileDialog.FileName,
                                    Verb = "Open",
                                    WindowStyle = ProcessWindowStyle.Normal
                                }
                        };
                    process.Start();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.Contains("正由另一进程使用") ? "数据导出失败！文件正由另一个程序占用！" : ex.Message, AppMessage.MSG0000);
            }
        }

        /// <summary>
        /// 图标控件导出Excel（Xlsx）
        /// </summary>
        /// <param name="chart"></param>
        public static void ChartExportXlsx(ChartControl chart)
        {
            var saveFileDialog = new SaveFileDialog { FileName = DateTime.Now.ToString(BaseSystemInfo.DateFormat) + "打印数据", Title = @"导出Excel", Filter = @"Excel文件(*.xlsx)|*.xlsx", OverwritePrompt = false };//已存在文件是否覆盖提示
            var dialogResult = saveFileDialog.ShowDialog();
            if (dialogResult != DialogResult.OK) return;
            while (System.IO.File.Exists(saveFileDialog.FileName) && XtraMessageBox.Show("该文件名已存在，是否覆盖？", AppMessage.MSG0000, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                if (dialogResult != DialogResult.Yes) return;
            }
            if (string.IsNullOrEmpty(saveFileDialog.FileName)) return;
            try
            {
                chart.ExportToXlsx(saveFileDialog.FileName);
                OpenFile(saveFileDialog.FileName);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.Contains("正由另一进程使用") ? "数据导出失败！文件正由另一个程序占用！" : ex.Message, AppMessage.MSG0000);
            }
        }
        /// <summary>
        /// 图标控件导出Excel（Xls）
        /// </summary>
        /// <param name="chart"></param>
        public static void ChartExportXls(ChartControl chart)
        {
            var saveFileDialog = new SaveFileDialog { FileName = DateTime.Now.ToString(BaseSystemInfo.DateFormat) + "打印数据", Title = @"导出Excel", Filter = @"Excel文件(*.xls)|*.xls", OverwritePrompt = false };//已存在文件是否覆盖提示
            var dialogResult = saveFileDialog.ShowDialog();
            if (dialogResult != DialogResult.OK) return;
            while (System.IO.File.Exists(saveFileDialog.FileName) && XtraMessageBox.Show("该文件名已存在，是否覆盖？", AppMessage.MSG0000, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                if (dialogResult != DialogResult.Yes) return;
            }
            if (string.IsNullOrEmpty(saveFileDialog.FileName)) return;
            try
            {
                chart.ExportToXls(saveFileDialog.FileName);
                OpenFile(saveFileDialog.FileName);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.Contains("正由另一进程使用") ? "数据导出失败！文件正由另一个程序占用！" :ex.Message, AppMessage.MSG0000);
            }
        }

        /// <summary>
        /// 图标控件导出Excel（Image,jpg）
        /// </summary>
        /// <param name="chart"></param>
        public static void ChartExportImage(ChartControl chart)
        {
            var saveFileDialog = new SaveFileDialog { FileName = DateTime.Now.ToString(BaseSystemInfo.DateFormat) + "打印数据", Title = @"导出JPG", Filter = @"图片文件(*.jpg)|*.jpg", OverwritePrompt = false };//已存在文件是否覆盖提示
            var dialogResult = saveFileDialog.ShowDialog();
            if (dialogResult != DialogResult.OK) return;
            while (System.IO.File.Exists(saveFileDialog.FileName) && XtraMessageBox.Show("该文件名已存在，是否覆盖？", AppMessage.MSG0000, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                if (dialogResult != DialogResult.Yes) return;
            }
            if (string.IsNullOrEmpty(saveFileDialog.FileName)) return;
            try
            {
                chart.ExportToImage(saveFileDialog.FileName, ImageFormat.Jpeg);
                OpenFile(saveFileDialog.FileName);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.Contains("正由另一进程使用") ? "数据导出失败！文件正由另一个程序占用！" : ex.Message, AppMessage.MSG0000);
            }
        }

        /// <summary>
        /// 打开文件
        /// </summary>
        /// <param name="fileName">文件完整路径</param>
        public static void OpenFile(string fileName)
        {
            if (XtraMessageBox.Show("导出文件成功，是否打开？", AppMessage.MSG0000, MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
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


    }
    /// <summary>
    /// 导出文件枚举
    /// </summary>
    public enum ExportEnum
    {
        Word = 0,
        Pdf = 1,
        Csv = 2,
        Xls = 3,
        Xlsx = 4,
        Html = 5
    }
}
