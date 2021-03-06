﻿//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd.  
//-----------------------------------------------------------------

using System;
using System.Data;
using System.IO;
using System.Text;
using System.Web;

namespace DotNet.Utilities
{
    /// <summary>
    /// BaseExportCSV
    /// 导出CSV格式数据
    /// 
    /// 修改记录
    /// 
    ///     2009.07.08 版本：3.0 JiRiGaLa	更新完善程序，将方法修改为静态方法。
    ///     2007.08.11 版本：2.0 JiRiGaLa	更新完善程序。
    ///     2006.12.01 版本：1.0 JiRiGaLa	新创建。
    /// 
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2009.07.08</date>
    /// </author> 
    /// </summary>
    public class BaseExportCSV
    {
        #region public static void ExportCSV(IDataReader dataReader, string fileName) 导出CSV格式文件
        /// <summary>
        /// 导出CSV格式文件
        /// </summary>
        /// <param name="dataReader"></param>
        /// <param name="fileName">文件全路径</param>
        public static void ExportCSV(IDataReader dataReader, string fileName)
        {
            if (File.Exists(fileName))
            {
                FileUtil.DeleteFile(fileName);
            }
            using(FileStream fileStream = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                StreamWriter streamWriter = new StreamWriter(fileStream, Encoding.Default);
                streamWriter.WriteLine(GetCSVFormatData(dataReader).ToString());
                streamWriter.Close();
                fileStream.Close();
            }
        }
        #endregion

        #region public static StringBuilder GetCSVFormatData(IDataReader dataReader) 通过dataReader获得CSV格式数据
        /// <summary>
        /// 通过dataReader获得CSV格式数据
        /// </summary>
        /// <param name="dataReader"></param>
        /// <returns></returns>
        public static StringBuilder GetCSVFormatData(IDataReader dataReader)
        {
            const string separator = ",";
            // 返回总字符串
            StringBuilder csvRows = new StringBuilder();
            // 表头内容字符串
            StringBuilder stringBuilder = new StringBuilder();
            // 循环输出表头内容
            for (int index = 0; index < dataReader.FieldCount; index++)
            {
                //如果表头名字不为空，获取内容
                if (dataReader.GetName(index) != null)
                {
                    stringBuilder.Append(dataReader.GetName(index));
                }
                //在获取表头内容之后加上,
                if (index < dataReader.FieldCount - 1)
                {
                    stringBuilder.Append(separator);
                }
            }
            // 先把表头正行数据加载到StringBuilder对象csvRows中
            csvRows.AppendLine(stringBuilder.ToString());
            // 循环获取表中的所有内容
            while (dataReader.Read())
            {
                stringBuilder = new StringBuilder();
                for (int index = 0; index < dataReader.FieldCount - 1; index++)
                {
                    if (!dataReader.IsDBNull(index))
                    {
                        string value = dataReader.GetValue(index).ToString();
                        if (dataReader.GetFieldType(index) == typeof(string))
                        {
                            // 如果值中存在一个双引号时，空值替换
                            if (value.IndexOf("\"") >= 0)
                            {
                                value = value.Replace("\"", "");
                            }
                            // 如果值中存在回车换行，空值替换
                            if (value.IndexOf("\r\n") >= 0)
                            {
                                value = value.Replace("\r\n", "");
                            }
                            // 字符串替换替换
                            value = value.Replace("\r", "").Replace("\n", "").Replace(",", "，");
                            // 让值在双引号之间
                            if (value.IndexOf(separator) >= 0)
                            {
                                value = "\"" + value + "\"";
                            }
                        }
                        stringBuilder.Append(value);
                    }
                    if (index < dataReader.FieldCount - 1)
                    {
                        stringBuilder.Append(separator);
                    }
                }
                // 最后一个逗号用空来替代
                if (!dataReader.IsDBNull(dataReader.FieldCount - 1))
                {
                    stringBuilder.Append(dataReader.GetValue(dataReader.FieldCount - 1).ToString().Replace(separator, " "));
                }
                csvRows.AppendLine(stringBuilder.ToString());
            }
            dataReader.Close();
            return csvRows;
        }
        #endregion

        #region public static StringBuilder GetCSVFormatData(DataTable dt) 通过DataTable获得CSV格式数据
        /// <summary>
        /// 通过DataTable获得CSV格式数据
        /// </summary>
        /// <param name="dt">数据表</param>
        /// <returns>CSV字符串数据</returns>
        public static StringBuilder GetCSVFormatData(DataTable dt)
        {
            StringBuilder stringBuilder = new StringBuilder();
            // 写出表头
            foreach (DataColumn dataColumn in dt.Columns)
            {
                stringBuilder.Append(dataColumn.ColumnName.ToString() + ",");
            }
            stringBuilder.Append("\n");
            // 写出数据
            foreach (DataRowView dataRowView in dt.DefaultView)
            {
                foreach (DataColumn dataColumn in dt.Columns)
                {
                    stringBuilder.Append(dataRowView[dataColumn.ColumnName].ToString() + ",");
                }
                stringBuilder.Append("\n");
            }
            return stringBuilder;
        }
        #endregion

        #region public static StringBuilder GetCSVFormatData(DataSet dataSet) 通过DataSet获得CSV格式数据
        /// <summary>
        /// 通过DataSet获得CSV格式数据
        /// </summary>
        /// <param name="dataSet">数据权限</param>
        /// <returns>CSV字符串数据</returns>
        public static StringBuilder GetCSVFormatData(DataSet dataSet)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (DataTable dt in dataSet.Tables)
            {
                stringBuilder.Append(GetCSVFormatData(dt));
            }
            return stringBuilder;
        }
        #endregion

        #region public static void ExportCSV(DataTable dt, string fileName) 导出CSV格式文件
        /// <summary>
        /// 导出CSV格式文件
        /// </summary>
        /// <param name="dt">数据表</param>
        /// <param name="fileName">文件名</param>
        public static void ExportCSV(DataTable dt, string fileName)
        {
            StreamWriter streamWriter = null;
            if (BaseSystemInfo.CurrentLanguage.Equals("zh-CN"))
            {
                streamWriter = new StreamWriter(fileName, false, System.Text.Encoding.GetEncoding("gb2312"));
            }
            else
            {
                streamWriter = new StreamWriter(fileName, false, System.Text.Encoding.GetEncoding("utf-8"));
            }
            streamWriter.WriteLine(GetCSVFormatData(dt).ToString());
            streamWriter.Flush();
            streamWriter.Close();
        }
        #endregion

        #region public static void ExportCSV(DataSet dataSet, string fileName) 导出CSV格式文件
        /// <summary>
        /// 导出CSV格式文件
        /// </summary>
        /// <param name="dataSet">数据权限</param>
        /// <param name="fileName">文件名</param>
        public static void ExportCSV(DataSet dataSet, string fileName)
        {
            StreamWriter streamWriter = null;
            if (BaseSystemInfo.CurrentLanguage.Equals("zh-CN"))
            {
                streamWriter = new StreamWriter(fileName, false, System.Text.Encoding.GetEncoding("gb2312"));
            }
            else
            {
                streamWriter = new StreamWriter(fileName, false, System.Text.Encoding.GetEncoding("utf-8"));
            }
            streamWriter.WriteLine(GetCSVFormatData(dataSet).ToString());
            streamWriter.Flush();
            streamWriter.Close();
        }
        #endregion



        #region public static void GetResponseCSV(DataTable dt, string fileName) 在浏览器中获得CSV格式文件
        /// <summary>
        /// 在浏览器中获得CSV格式文件
        /// </summary>
        /// <param name="dt">数据表</param>
        /// <param name="fileName">输出文件名</param>
        public static void GetResponseCSV(DataTable dt, string fileName)
        {
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-8");
            HttpContext.Current.Response.AppendHeader("Content-disposition", "attachment;filename=" + fileName);
            HttpContext.Current.Response.ContentType = "application/ms-excel";
            HttpContext.Current.Response.Write(GetCSVFormatData(dt).ToString());
            HttpContext.Current.Response.End();
        }
        #endregion

        #region public static void GetResponseCSV(DataSet dataSet, string fileName) 在浏览器中获得CSV格式文件
        /// <summary>
        /// 在浏览器中获得CSV格式文件
        /// </summary>
        /// <param name="dataSet">数据权限</param>
        /// <param name="fileName">输出文件名</param>
        public static void GetResponseCSV(DataSet dataSet, string fileName)
        {
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-8");
            HttpContext.Current.Response.AppendHeader("Content-disposition", "attachment;filename=" + fileName);
            HttpContext.Current.Response.ContentType = "application/ms-excel";
            HttpContext.Current.Response.Write(GetCSVFormatData(dataSet).ToString());
            HttpContext.Current.Response.End();
            // 读取文件下载
            //String OutTemplateCSV = Server.MapPath("~/DownLoadFiles/ExcelExport/Common/Log/LogGeneral.csv");
            //StreamWriter StreamWriter = new StreamWriter(OutTemplateCSV, false, System.Text.Encoding.GetEncoding("gb2312"));
            //StreamWriter.WriteLine(this.GetCSVFormatData(dataSet).ToString());
            //StreamWriter.Flush();
            //StreamWriter.Close();
            //Response.Redirect("../../../DownLoadFiles/ExcelExport/Common/Log/LogGeneral.csv");
        }
        #endregion 
    }
}