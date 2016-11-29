//-------------------------------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2015 , STO , Ltd .
//-------------------------------------------------------------------------------------

using Microsoft.Win32;
using NPOI.SS.Util;
using System;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Windows.Forms;

namespace STO.Print.Utilities
{
    using DotNet.Utilities;
    using NPOI.HSSF.UserModel;
    using NPOI.SS.UserModel;
    using NPOI.XSSF.UserModel;

    /// <summary>
    /// Excel操作类，这里的NPOI使用的是2.0.1 (beta1)版本的
    ///
    /// 修改纪录
    ///
    ///		2013-10-25 版本：1.0 YangHengLian 创建主键，注意命名空间的排序。
    ///		2013-11-01 版本：1.0 YangHengLian 添加了支持B/S的导出和导入以及C/S的导出和导入功能，代码感觉可以在优化，开心。
    ///		2014-01-16 版本：1.0 YangHengLian 添加了支持xlsxExcel文件读取判断
    ///        2015-10-06 添加了检查Excel中当前行是否是空行（整行没有数据）的方法，解决了《c# 读写文件时文件正由另一进程使用，因此该进程无法访问该文件》url：http://blog.csdn.net/superhoy/article/details/7931234
    ///        2015-11-06 发现导入Excel单元格是Numeric类型的时候需要特殊处理不然excel的0.4就被系统读取为.4，网点发现的bug
    /// 
    /// 版本：1.0
    ///
    /// <author>
    ///		<name>YangHengLian</name>
    ///		<date>2015-11-01</date>
    /// </author>
    /// </summary>
    public class ExcelHelper
    {

        #region DataTable ExcelToDataTable(string strFileName, int sheetIndex = 0) 根据索引读取Sheet表数据，默认读取第一个sheet--B/S和C/S都可以使用
        /// <summary>读取excel
        /// 根据索引读取Sheet表数据，默认读取第一个sheet
        /// </summary>
        /// <param name="strFileName">excel文档路径</param>
        /// <param name="columnCount">最后一列的索引，比如总共有11列，那么最后一列的索引就是10</param>
        /// <param name="sheetIndex">sheet表的索引，从0开始</param>
        /// <param name="crossRow">从第几行开始读取数据，需要跨几行的意思</param>
        /// <returns>数据集</returns>
        public static DataTable ExcelToDataTable(string strFileName, int columnCount = 10, int sheetIndex = 0, int crossRow = 0)
        {
            try
            {
                DataTable dt = new DataTable();
                HSSFWorkbook hssfworkbook = null;
                XSSFWorkbook xssfworkbook = null;
                string fileExt = Path.GetExtension(strFileName);//获取文件的后缀名
                using (FileStream file = new FileStream(strFileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    if (fileExt == ".xls")
                        hssfworkbook = new HSSFWorkbook(file);
                    else if (fileExt == ".xlsx")
                        xssfworkbook = new XSSFWorkbook(file);//初始化太慢了，不知道这是什么bug
                }
                if (hssfworkbook != null)
                {
                    HSSFSheet sheet = (HSSFSheet)hssfworkbook.GetSheetAt(sheetIndex);
                    if (sheet != null)
                    {
                        //System.Collections.IEnumerator rows = sheet.GetRowEnumerator();
                        HSSFRow headerRow = null;
                        if (crossRow >= 1)
                        {
                            headerRow = (HSSFRow)sheet.GetRow(crossRow - 1);
                        }
                        else
                        {
                            headerRow = (HSSFRow)sheet.GetRow(crossRow);
                        }
                        int cellCount = headerRow.LastCellNum;
                        for (int j = 0; j < columnCount; j++)
                        {
                            dt.Columns.Add(j.ToString());
                        }
                        var startRowIndex = crossRow == 0 ? sheet.FirstRowNum + 1 : crossRow;
                        for (int i = startRowIndex; i <= sheet.LastRowNum; i++)
                        {
                            HSSFRow row = (HSSFRow)sheet.GetRow(i);
                            if (row == null) continue;
                            //if (row.Cells.Count >= 1)
                            //{
                            //    if (string.IsNullOrEmpty(row.Cells[0].ToString()))//第一列是空值，直接跳出，不允许构建内存表
                            //    {
                            //        continue;
                            //    }
                            //} 
                            if (IsEmptyRow(row, null))//一整行都是空值过滤掉
                            {
                                continue;
                            }
                            DataRow dataRow = dt.NewRow();
                            for (int j = row.FirstCellNum; j < cellCount; j++)
                            {
                                if (row.GetCell(j) != null)
                                {
                                    if (row.GetCell(j).CellType == CellType.Numeric)
                                    {
                                        dataRow[j] = row.GetCell(j).NumericCellValue.ToString();
                                    }
                                    else
                                    {
                                        dataRow[j] = row.GetCell(j).ToString().Trim().Replace(" ","");
                                    }
                                }
                            }
                            dt.Rows.Add(dataRow);
                        }
                    }
                }
                else if (xssfworkbook != null)
                {
                    XSSFSheet xSheet = (XSSFSheet)xssfworkbook.GetSheetAt(sheetIndex);
                    if (xSheet != null)
                    {
                        XSSFRow headerRow = (XSSFRow)xSheet.GetRow(0);
                        int cellCount = headerRow.LastCellNum;
                        for (int j = 0; j < cellCount; j++)
                        {
                            //XSSFCell cell = (XSSFCell)headerRow.GetCell(j);
                            //dt.Columns.Add(cell.ToString());
                            dt.Columns.Add(j.ToString());
                        }
                        var startRowIndex = crossRow == 0 ? xSheet.FirstRowNum + 1 : crossRow;
                        for (int i = startRowIndex; i <= xSheet.LastRowNum; i++)
                        {
                            //for (int i = (xSheet.FirstRowNum + 1); i < xSheet.LastRowNum; i++)
                            //{
                            XSSFRow row = (XSSFRow)xSheet.GetRow(i);
                            if (IsEmptyRow(null, row))//一整行都是空值过滤掉
                            {
                                continue;
                            }
                            DataRow dataRow = dt.NewRow();
                            for (int j = row.FirstCellNum; j < cellCount; j++)
                            {
                                if (row.GetCell(j) != null)
                                {
                                    if (row.GetCell(j).CellType == CellType.Numeric)
                                    {
                                        dataRow[j] = row.GetCell(j).NumericCellValue.ToString();
                                    }
                                    else
                                    {
                                        dataRow[j] = row.GetCell(j).ToString().Trim().Replace(" ", "");
                                    }
                                }
                            }
                            dt.Rows.Add(dataRow);
                        }
                    }
                }
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region DataTable ExcelToDataTable(string strFileName, int sheetIndex = 0) 根据索引读取Sheet表数据，默认读取第一个sheet--B/S和C/S都可以使用

        /// <summary>读取excel
        /// 根据索引读取Sheet表数据，默认读取第一个sheet
        /// </summary>
        /// <param name="strFileName">excel文档路径</param>
        /// <param name="sheetIndex">sheet表的索引，从0开始</param>
        /// <returns>数据集</returns>
        public static DataTable ExcelToDataTable(string strFileName, int sheetIndex = 0, ProgressBar prb = null)
        {
            DataTable dt = new DataTable();
            try
            {
                HSSFWorkbook hssfworkbook = null;
                XSSFWorkbook xssfworkbook = null;
                string fileExt = Path.GetExtension(strFileName);//获取文件的后缀名
                using (FileStream file = new FileStream(strFileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    if (fileExt == ".xls")
                        hssfworkbook = new HSSFWorkbook(file);
                    else if (fileExt == ".xlsx")
                        xssfworkbook = new XSSFWorkbook(file);//初始化太慢了，不知道这是什么bug
                }
                if (hssfworkbook != null)
                {
                    HSSFSheet sheet = (HSSFSheet)hssfworkbook.GetSheetAt(sheetIndex);
                    if (sheet != null)
                    {
                        System.Collections.IEnumerator rows = sheet.GetRowEnumerator();
                        HSSFRow headerRow = (HSSFRow)sheet.GetRow(0);
                        int cellCount = headerRow.LastCellNum;
                        for (int j = 0; j < cellCount; j++)
                        {
                            HSSFCell cell = (HSSFCell)headerRow.GetCell(j);
                            if (cell != null)
                            {
                                var columnName = cell.ToString();
                                foreach (var column in dt.Columns)
                                {
                                    if (column.ToString() == cell.ToString())
                                    {
                                        columnName = columnName + j;
                                        break;
                                    }
                                }
                                dt.Columns.Add(columnName, typeof(string));
                            }
                        }
                        for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
                        {
                            int pro = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(i / 100)));
                            if (i == pro)
                            {
                                prb.Value += 1;
                            }
                            HSSFRow row = (HSSFRow)sheet.GetRow(i);
                            if (row == null)
                            {
                                continue;
                            }
                            if (IsEmptyRow(row, null))//一整行都是空值过滤掉
                            {
                                continue;
                            }
                            DataRow dataRow = dt.NewRow();
                            for (int j = row.FirstCellNum; j < cellCount; j++)
                            {
                                if (row.GetCell(j) != null)
                                    dataRow[j] = row.GetCell(j).ToString();
                            }
                            dt.Rows.Add(dataRow);
                        }
                    }
                }
                else if (xssfworkbook != null)
                {
                    XSSFSheet xSheet = (XSSFSheet)xssfworkbook.GetSheetAt(sheetIndex);
                    if (xSheet != null)
                    {
                        System.Collections.IEnumerator rows = xSheet.GetRowEnumerator();
                        XSSFRow headerRow = (XSSFRow)xSheet.GetRow(0);
                        int cellCount = headerRow.LastCellNum;
                        for (int j = 0; j < cellCount; j++)
                        {
                            XSSFCell cell = (XSSFCell)headerRow.GetCell(j);
                            if (cell != null)
                            {
                                var columnName = cell.ToString();
                                foreach (var column in dt.Columns)
                                {
                                    if (column.ToString() == cell.ToString())
                                    {
                                        columnName = columnName + j;
                                        break;
                                    }
                                }
                                dt.Columns.Add(columnName, typeof(string));
                            }
                        }
                        for (int i = (xSheet.FirstRowNum + 1); i <= xSheet.LastRowNum; i++)
                        {
                            XSSFRow row = (XSSFRow)xSheet.GetRow(i);
                            if (row == null)
                            {
                                continue;
                            }
                            if (IsEmptyRow(null, row))//一整行都是空值过滤掉
                            {
                                continue;
                            }
                            DataRow dataRow = dt.NewRow();
                            for (int j = row.FirstCellNum; j < cellCount; j++)
                            {
                                if (row.GetCell(j) != null)
                                    dataRow[j] = row.GetCell(j).ToString();
                            }
                            dt.Rows.Add(dataRow);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return dt;
        }

        #endregion

        #region public static bool IsExcelInstalled() 判断系统是否装Excel

        /// <summary>
        /// 判断系统是否装Excel
        /// </summary>
        /// <returns></returns>
        public static bool IsExcelInstalled()
        {
            RegistryKey machineKey = Registry.LocalMachine;//读取本机的公共配置信息单元，详情请看，http://baike.baidu.com/view/1387918.htm
            /*
             * 版本  Office
             * 11.0 Office 2003 SP1
             * 12.0 Office 2007
             * 14.0 Office 2015 
             * 15.0 Office 2015
             */
            return IsWordInstalledByVersion("11.0", machineKey) || (IsWordInstalledByVersion("12.0", machineKey) ||
                                                                    (IsWordInstalledByVersion("14.0", machineKey) ||
                                                                     IsWordInstalledByVersion("15.0", machineKey)));
        }

        /// <summary>
        /// 判断系统是否装某版本的word
        /// </summary>
        /// <param name="strVersion">版本号</param>
        /// <param name="machineKey"></param>
        /// <returns></returns>
        private static bool IsWordInstalledByVersion(string strVersion, RegistryKey machineKey)
        {
            try
            {
                var openSubKey = machineKey.OpenSubKey("Software");
                if (openSubKey != null)
                {
                    RegistryKey installKey = openSubKey.OpenSubKey("Microsoft").OpenSubKey("Office").OpenSubKey(strVersion).OpenSubKey("Excel").OpenSubKey("InstallRoot");
                    if (installKey == null)
                    {
                        return false;
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
                return false;
            }
        }
        #endregion

        #region public static bool IsEmptyRow(HSSFRow xlsRow, XSSFRow xlsxRow)  判断xlsx和xls的Excel中的sheet是否存在空行（整行数据都是空值）
        /// <summary>
        /// 判断xlsx和xls的Excel中的sheet是否存在空行（整行数据都是空值）
        /// </summary>
        /// <param name="xlsRow">xls Excel行对象实体</param>
        /// <param name="xlsxRow">xlsx Excel行对象实体</param>
        /// <returns></returns>
        public static bool IsEmptyRow(HSSFRow xlsRow, XSSFRow xlsxRow)
        {
            try
            {
                IRow row = xlsRow ?? (IRow)xlsxRow;
                if (row != null)
                {
                    var emptyCellCount = 0;
                    for (int i = 0; i <= row.Cells.Count - 1; i++)
                    {
                        if (row.Cells[i] != null)
                        {
                            if (string.IsNullOrEmpty(row.Cells[i].ToString()))
                            {
                                ++emptyCellCount;
                            }
                        }
                        else
                        {
                            ++emptyCellCount;
                        }
                    }
                    // return emptyCellCount == row.Cells.Count - 1;
                    return emptyCellCount == row.Cells.Count;
                }
                return true;
            }
            catch (Exception ex)
            {
                LogUtil.WriteException(ex);
            }
            return false;
        }
        #endregion

        #region public static bool ExportExcelFromDataTable(DataTable dt, string saveFileName = null, bool isOpen = false, string saveFilePath = null)

        /// <summary>
        /// DataTable导出数据到Excel
        /// </summary>
        /// <param name="dt">内存表</param>
        /// <param name="saveFileName">保存的文件名称，默认没有，调用的时候最好加上，中英文都支持</param>
        /// <param name="isOpen">导出后是否打开文件和所在文件夹</param>
        /// <param name="saveFilePath">默认保存在“我的文档”中，可自定义保存的文件夹路径</param>
        /// <param name="strHeaderText">Excel中第一行的标题文字，默认没有，可以自定义</param>
        /// <param name="titleNames">Excel中列名的数组，默认绑定DataTable的列名</param>
        public static bool ExportExcelFromDataTable(DataTable dt, string saveFileName = null, bool isOpen = false, string saveFilePath = null)
        {
            try
            {
                using (MemoryStream ms = RenderDataTableToExcel(dt))
                {
                    if (string.IsNullOrEmpty(saveFileName)) //文件名为空
                    {
                        saveFileName = DateTime.Now.Ticks.ToString(CultureInfo.InvariantCulture);
                    }
                    if (string.IsNullOrEmpty(saveFilePath) || !Directory.Exists(saveFilePath)) //保存路径为空或者不存在
                    {
                        saveFilePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments); //默认在文档文件夹中
                    }
                    string saveFullPath = saveFilePath + "\\" + saveFileName + ".xls";
                    if (File.Exists(saveFullPath)) //验证文件重复性
                    {
                        saveFullPath = saveFilePath + "\\" + saveFileName + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Replace(":", "-").Replace(" ", "-") + ".xlsx";
                    }
                    using (var fs = new FileStream(saveFullPath, FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
                    {
                        byte[] data = ms.ToArray();
                        fs.Write(data, 0, data.Length);
                        fs.Flush();
                    }
                    if (isOpen)
                    {
                        if (IsExcelInstalled())
                        {
                            Process.Start(saveFullPath); //打开文件
                        }
                    }
                    Process.Start(saveFilePath); //打开文件夹
                    return true;
                }
            }
            catch (Exception ex)
            {
                LogUtil.WriteException(ex);
                return false;
            }
        }

        #endregion

        private static MemoryStream RenderDataTableToExcel(DataTable sourceTable)
        {
            // 不能超过65536
            XSSFWorkbook workbook = null;
            MemoryStream ms = null;
            ISheet sheet = null;
            XSSFRow headerRow = null;
            try
            {
                workbook = new XSSFWorkbook();
                ms = new MemoryStream();
                sheet = workbook.CreateSheet();
                headerRow = (XSSFRow)sheet.CreateRow(0);
                foreach (DataColumn column in sourceTable.Columns)
                    headerRow.CreateCell(column.Ordinal).SetCellValue(column.ColumnName);
                int rowIndex = 1;
                foreach (DataRow row in sourceTable.Rows)
                {
                    var dataRow = (XSSFRow)sheet.CreateRow(rowIndex);
                    foreach (DataColumn column in sourceTable.Columns)
                        dataRow.CreateCell(column.Ordinal).SetCellValue(row[column].ToString());
                    ++rowIndex;
                }
                //列宽自适应，只对英文和数字有效
                for (int i = 0; i <= sourceTable.Columns.Count; ++i)
                    sheet.AutoSizeColumn(i);
                workbook.Write(ms);
                ms.Flush();
            }
            catch (Exception ex)
            {
                LogUtil.WriteException(ex);
                return null;
            }
            finally
            {
                ms.Close();
                sheet = null; headerRow = null;
                workbook = null;
            }
            return ms;
        }

        /// <summary>
        /// 导出Excel（xls）格式
        /// </summary>
        /// <param name="dt">源数据表格</param>
        /// <param name="fileName">文件名称</param>
        /// <param name="isOpen">是否打开</param>
        /// <returns></returns>
        public static bool ExportExcel(DataTable dt, bool isOpen = false)
        {
            // 这里显示选择文件的对话框，可以取消导出可以确认导出，可以修改文件名。
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = DateTime.Now.Ticks.ToString();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            saveFileDialog.Filter = "Excel(*.xls)|*.xls|所有文件|*.*";
            saveFileDialog.FilterIndex = 0;
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.Title = "导出数据文件";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                FileStream fs = new FileStream(saveFileDialog.FileName, FileMode.Create, FileAccess.Write);
                HSSFWorkbook workbook = new HSSFWorkbook();
                MemoryStream ms = new MemoryStream();
                ISheet sheet = workbook.CreateSheet();
                IRow headerRow = sheet.CreateRow(0);

                // 写出表头
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    //if (result.Columns[i].Visible && (result.Columns[i].ItemName.ToUpper() != "colSelected".ToUpper()))
                    //{
                    //headerRow.CreateCell(i).SetCellValue(fieldList[result.Columns[i].ColumnName.ToLower()]);
                    //}

                    //增加了try Catch，解决字典fieldList中没有table列中项时，会出错。
                    //此处采用跳过的方式,表现方式是此列的表头没值
                    try
                    {
                        headerRow.CreateCell(i).SetCellValue(dt.Columns[i].ColumnName.ToLower());
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }

                // 行索引号
                int rowIndex = 1;

                // 写出数据
                foreach (DataRow dataTableRow in dt.Rows)
                {
                    IRow dataRow = sheet.CreateRow(rowIndex);

                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        //if (dataGridView.Columns[i].Visible && (dataGridView.Columns[i].ItemName.ToUpper() != "colSelected".ToUpper()))
                        //{
                        switch (dt.Columns[i].DataType.ToString())
                        {
                            case "System.String":
                            default:
                                dataRow.CreateCell(i).SetCellValue(
                                    Convert.ToString(Convert.IsDBNull(dataTableRow[i]) ? "" : dataTableRow[i])
                                    );
                                break;
                            case "System.Int16":
                            case "System.Int32":
                            case "System.Int64":
                            case "System.Decimal":
                            case "System.Double":
                                dataRow.CreateCell(i).SetCellValue(
                                    Convert.ToDouble(Convert.IsDBNull(dataTableRow[i]) ? 0 : dataTableRow[i])
                                    );
                                break;
                        }
                        //                    }
                    }
                    rowIndex++;
                }
                workbook.Write(ms);
                byte[] data = ms.ToArray();

                ms.Flush();
                ms.Close();

                fs.Write(data, 0, data.Length);
                fs.Flush();
                fs.Close();
                if (isOpen)
                {
                    if (IsExcelInstalled())
                    {
                        Process.Start(saveFileDialog.FileName); //打开文件
                    }
                }
                Process.Start(saveFileDialog.InitialDirectory); //打开文件夹
                return true;
            }
            return false;
        }

        /// <summary>
        /// 自动为第一个sheet的表头添加表头筛选的功能
        /// 2016-1-21 14:11:09 上海-小杰  693684292 帮忙添加此方法，测试通过
        /// </summary>
        /// <param name="filePath"></param>
        public static void SetAutoFilter(string filePath)
        {
            try
            {
                IWorkbook workbook = null;
                string fileExt = Path.GetExtension(filePath);
                using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    switch (fileExt)
                    {
                        case ".xls":
                            workbook = new HSSFWorkbook(fs);

                            break;
                        case ".xlsx":
                            workbook = new XSSFWorkbook(fs);
                            break;
                    }
                }
                SetAutoFilter(workbook, filePath);
            }
            catch (Exception exception)
            {
               LogUtil.WriteException(exception);
            }
        }

        /// <summary>
        /// 设置某个WorkBook的第一行表头增加自动筛选的功能
        /// </summary>
        /// <param name="wb"></param>
        /// <param name="filePath"></param>
        public static void SetAutoFilter(IWorkbook wb, string filePath)
        {
            ISheet sheet = wb.GetSheetAt(0);
            IRow row = sheet.GetRow(0);
            int lastCol = row.LastCellNum;
            var cellRange = new CellRangeAddress(0, 0, 0, lastCol - 1);
            sheet.SetAutoFilter(cellRange);
            using (var fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                wb.Write(fs);
            }
        }
    }
}
