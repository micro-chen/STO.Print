//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd.  
//-----------------------------------------------------------------

using System;
using System.Data;
using System.Text;

namespace DotNet.Utilities
{
    /// <summary>
    ///	SQLTrace
    /// 记录SQL执行 Global 中设置 BaseSystemInfo.LogSQL=true 可以跟踪记录
    /// 
    /// 
    /// 修改记录
    /// 
    ///		2016.01.12 版本：1.0	SongBiao
    ///	
    /// <author>
    ///		<name>SongBiao</name>
    ///		<date>2016.01.12</date>
    /// </author> 
    /// </summary>
    public class SQLTrace
    {
        private static string FileName = "SQLTrace.txt";

        #region public static void WriteLog(string commandText,IDbDataParameter[] dbParameters = null,  string fileName = null) 写入sql查询句日志
        /// <summary>
        /// 写入sql查询句日志
        /// </summary>
        /// <param name="commandText">异常</param>
        /// <param name="dbParameters"></param>
        /// <param name="fileName">文件名</param>
        public static void WriteLog(string commandText, IDbDataParameter[] dbParameters = null, string fileName = null)
        {
            // 系统里应该可以配置是否记录异常现象
            if (!BaseSystemInfo.LogSQL)
            {
                return;
            }
            if (string.IsNullOrEmpty(fileName))
            {
                fileName = DateTime.Now.ToString(BaseSystemInfo.DateFormat) + " _ " + FileName;
            }
            string message = string.Empty;
            message = DateTime.Now.ToString(BaseSystemInfo.DateTimeFormat) + System.Environment.NewLine + "commandText内容" + System.Environment.NewLine + commandText;
            if (dbParameters != null)
            {
                StringBuilder sb = new StringBuilder();
                foreach (var parameter in dbParameters)
                {
                    sb.AppendLine(parameter.ParameterName + "=" + parameter.Value);
                }
                message += System.Environment.NewLine + "dbParameters内容" + System.Environment.NewLine + sb.ToString();
            }
            // 将异常信息写入本地文件中
            string logDirectory = BaseSystemInfo.StartupPath + @"\Log\Query";
            if (!System.IO.Directory.Exists(logDirectory))
            {
                System.IO.Directory.CreateDirectory(logDirectory);
            }
            string writerFileName = logDirectory + "\\" + fileName;
            FileUtil.WriteMessage(message, writerFileName);




            //// Web方式
            //string path = "~/Log/Query/" + DateTime.Now.ToString(BaseSystemInfo.DateFormat) + "-" + DateTime.Now.Hour.ToString() + ".log";
            //if (!File.Exists(System.Web.HttpContext.Current.Server.MapPath(path)))
            //{
            //    ///不存在该日志,则创建
            //    File.Create(System.Web.HttpContext.Current.Server.MapPath(path)).Close();
            //}
            /////写日志记录
            //using (StreamWriter w = File.AppendText(System.Web.HttpContext.Current.Server.MapPath(path)))
            //{
            //    w.WriteLine(DateTime.Now.ToString(BaseSystemInfo.DateTimeFormat) + " " + commandText);
            //    w.Flush();
            //    w.Close();
            //}
        }
        #endregion
    }
}
