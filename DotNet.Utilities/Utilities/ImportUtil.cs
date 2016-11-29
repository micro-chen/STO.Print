//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd.  
//-----------------------------------------------------------------

using System;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;
using NPOI.SS.UserModel;

namespace DotNet.Utilities
{
    public class ImportUtil
    {
        private int returnStatus = 0;
        private string returnMessage = null;

        /// <summary>
        /// 执行返回状态
        /// </summary>
        public int ReturnStatus
        {
            get { return returnStatus; }
        }

        /// <summary>
        /// 执行返回信息
        /// </summary>
        public string ReturnMessage
        {
            get { return returnMessage; }
        }

        /// <summary>
        /// 选择要导入的Excel文件
        /// </summary>
        /// <returns></returns>
        public static string SelectExcelFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Excel文件(*.XLS)|*.XLS";

            openFileDialog.Multiselect = false;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileNames[0];
                return filePath;
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 选择要导入的Excel文件(多版本)
        /// </summary>
        /// <returns></returns>
        public static string OpenXlsXlsxFile()
        {
            string filePath = string.Empty;

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Excel 工作簿(*.xls,*.xlsx)|*.xls;*.xlsx|Excel 97-2003 工作簿(*.xls)|*.xls|Excel 2010 工作簿(*.xlsx)|*.xlsx|所有文件|*.*";
            // openFileDialog.Filter = "Excel 97-2003 工作簿(*.xls)|*.xls|Excel2010文件(*.xlsx)|*.xlsx|所有文件|*.*";
            openFileDialog.FilterIndex = 0;
            openFileDialog.RestoreDirectory = true;
            openFileDialog.Title = "选择要导入的EXCEL文件";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                filePath = openFileDialog.FileNames[0];                
            }
            return filePath;
        }

        /// <summary>
        /// 选择要导入的文本文件
        /// </summary>
        /// <returns></returns>
        public static string SelectTxtFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "文本文件(*.txt)|*.txt";

            openFileDialog.Multiselect = false;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileNames[0];
                return filePath;
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 读取Excel
        /// 默认第一行为标头
        /// 支持Office 2007以上版本
        /// 替换原先的方式，不存在非托管方式无法释放资源的问题
        /// 适用于B/S C/S。服务器可免安装Office。
        /// Pcsky 2012.05.01
        /// </summary>
        /// <param name="path">excel文档路径</param>
        /// <returns></returns>
        public static DataTable ImportExcel(string path, int sheetIndex = 0 )
        {
            string columnName;
            var dt = new DataTable();
            //HSSFWorkbook wb;
            IWorkbook wb;
            using (FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                // 只支持2007及以下低版本
                //wb = new HSSFWorkbook(file);
                // 通过接口的方式实现从xls到xlsx 2003、2007以上版本的全部支持
                wb = WorkbookFactory.Create(file);

            }
            ISheet sheet = wb.GetSheetAt(sheetIndex);
            System.Collections.IEnumerator rows = sheet.GetRowEnumerator();
            IRow headerRow = sheet.GetRow(0);
            int cellCount = headerRow.LastCellNum;

            // 添加datatable的标题行


            //for (int i = 0; i < cellCount; i++)
            for (int i = headerRow.FirstCellNum; i < cellCount; i++)
            {
                //ICell cell = headerRow.GetCell(j);
                //dt.Columns.Add(cell.ToString());

                // 2012.09.13 Pcsky 处理空列
                if (headerRow.GetCell(i) == null)
                {
                    columnName = Guid.NewGuid().ToString("N");
                }
                else
                {
                    columnName = headerRow.GetCell(i).StringCellValue;
                }
                DataColumn column = new DataColumn(columnName);
                dt.Columns.Add(column);
            }

            // 从第2行起添加内容行
            for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
            {
                IRow row = sheet.GetRow(i);
                DataRow dr = dt.NewRow();

                // 2012.09.12 Pcsky 设置dataRow的索引号从0开始
                int k = 0;
                for (int j = row.FirstCellNum; j < cellCount; j++)
                {
                    //if (row.GetCell(j) != null)
                    //{
                    //dataRow[j] = row.GetCell(j).ToString();

                    dr[k] = row.GetCell(j);
                    k++;
                    //}
                }

                dt.Rows.Add(dr);
            }
            wb = null;
            sheet = null;
            return dt;
        }

        #region public static string CheckColumnExist(string columnNames, string needCheckColumnName) 判断是否存在这一列
        /// <summary>
        /// 判断是否存在这一列
        /// </summary>
        /// <param name="columnNames">当前存在的列组</param>
        /// <param name="needCheckColumnNames">要求的列名组</param>
        /// <returns>提示信息</returns>
        public static string CheckColumnExist(string columnNames, string needCheckColumnName)
        {
            string result = string.Empty;
            if (!needCheckColumnName.Contains(columnNames))
            {
                result += "\"" + columnNames + "\"这一列不存在，需添加此列。\r\n";
            }
            return result;
        }
        #endregion

        #region public static StringBuilder CheckIsNullOrEmpty(DataTable dt, string checkStrings) 判断是选中段的值否为空
        /// <summary>
        /// 判断是选中段的值否为空
        /// </summary>
        /// <param name="dr">DataTable</param>
        /// <param name="checkStrings">检查的字段串</param>
        /// <returns>返回提示</returns>
        public static string CheckIsNullOrEmpty(DataTable dt, string[] checkStrings)
        {
            StringBuilder result = new StringBuilder();
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                for (int i = 0; i < checkStrings.Length; i++)
                {
                    if (string.IsNullOrEmpty(dt.Rows[j][checkStrings[i]].ToString()))
                    {
                        result.Append("\"" + checkStrings[i] + "\"不能为空。");
                        dt.Rows[j]["错误信息"] = result;
                    }
                }
            }
            return result.ToString();
        }
        #endregion

        #region public static string DataTableColumn2String(DataTable dt)DataTable列转换成字符串
        /// <summary>
        /// DataTable列转换成字符串
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <returns>转换后的字符串</returns>
        public static string DataTableColumn2String(DataTable dt)
        {
            StringBuilder ColumnNames = new StringBuilder();
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                if (i > 0)
                {
                    ColumnNames.Append(",");
                }
                ColumnNames.Append(dt.Columns[i].ColumnName);
            }
            return ColumnNames.ToString();
        }
        #endregion


        ///// <summary>
        ///// 导入EXCEL到DataSet
        ///// </summary>
        ///// <param name="fileName">Excel全路径文件名</param>
        ///// <returns>导入成功的DataSet</returns>
        //public DataTable ImportExcel(string fileName)
        //{
        //    //判断是否安装EXCEL
        //    Excel.Application xlApp = new Excel.ApplicationClass();
        //    if (xlApp == null)
        //    {
        //        returnStatus = -1;
        //        returnMessage = "无法创建Excel对象，可能您的计算机未安装Excel";
        //        return null;
        //    }

        //    //判断文件是否被其他进程使用            
        //    Excel.Workbook workbook;
        //    try
        //    {
        //        workbook = xlApp.Workbooks.Open(fileName, 0, false, 5, "", "", false, Excel.XlPlatform.xlWindows, "", true, false, 0, true, 1, 0);
        //    }
        //    catch
        //    {
        //        returnStatus = -1;
        //        returnMessage = "Excel文件处于打开状态，请保存关闭";
        //        return null;
        //    }

        //    //获得所有Sheet名称
        //    int n = workbook.Worksheets.Count;
        //    string[] SheetSet = new string[n];
        //    System.Collections.ArrayList al = new System.Collections.ArrayList();
        //    for (int i = 1; i <= n; i++)
        //    {
        //        SheetSet[i - 1] = ((Excel.Worksheet)workbook.Worksheets[i]).Name;
        //    }

        //    //释放Excel相关对象
        //    workbook.Close(null, null, null);
        //    xlApp.Quit();
        //    if (workbook != null)
        //    {
        //        System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
        //        workbook = null;
        //    }
        //    if (xlApp != null)
        //    {
        //        System.Runtime.InteropServices.Marshal.ReleaseComObject(xlApp);
        //        xlApp = null;
        //    }
        //    GC.Collect();

        //    //把EXCEL导入到DataSet
        //    DataSet ds = new DataSet();
        //    string connStr = " Provider = Microsoft.Jet.OLEDB.4.0 ; Data Source = " + fileName + ";Extended Properties=Excel 8.0";
        //    using (OleDbConnection conn = new OleDbConnection(connStr))
        //    {
        //        conn.Open();
        //        OleDbDataAdapter da;
        //        for (int i = 1; i <= n; i++)
        //        {
        //            string sql = "SELECT * FROM [" + SheetSet[i - 1] + "$] ";
        //            da = new OleDbDataAdapter(sql, conn);
        //            da.Fill(ds, SheetSet[i - 1]);
        //            da.Dispose();
        //        }
        //        conn.Close();
        //        conn.Dispose();
        //    }
        //    ds.Tables[0].Columns.Add("错误信息");
        //    return ds.Tables[0];
        //}
    }
}
