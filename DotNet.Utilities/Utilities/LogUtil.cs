//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd.  
//-----------------------------------------------------------------

using System;
using System.IO;

namespace DotNet.Utilities
{
    public class LogUtil
    {
        private static readonly object writeFile = new object();

        private static StreamWriter streamWriter; //写文件  

        public static void WriteException(Exception exception)
        {
            WriteLog(exception);
        }

        /// <summary>
        /// 在本地写入错误日志
        /// </summary>
        /// <param name="exception"></param> 错误信息
        public static void WriteLog(Exception exception)
        {
            lock (writeFile)
            {
                try
                {
                    DateTime dt = DateTime.Now;
                    string directPath = string.Format(@"{0}\Log",
                                                      AppDomain.CurrentDomain.SetupInformation.ApplicationBase);
                    //记录错误日志文件的路径
                    if (!Directory.Exists(directPath))
                    {
                        Directory.CreateDirectory(directPath);
                    }
                    directPath += string.Format(@"\{0}.log", dt.ToString("yyyy-MM-dd"));
                    if (streamWriter == null)
                    {
                        InitLog(directPath);
                    }
                    streamWriter.WriteLine("***********************************************************************");
                    streamWriter.WriteLine(dt.ToString("HH:mm:ss"));
                    // streamWriter.WriteLine("输出信息：错误信息");
                    if (exception != null)
                    {
                        //string message = "错误对象:" + exception.Source.Trim() + Environment.NewLine
                        //               + "异常方法:" + exception.TargetSite.Name + Environment.NewLine
                        //               + "堆栈信息:" + exception.GetType() + ":" + exception.Message.Trim() + Environment.NewLine
                        //               + exception.StackTrace.Replace(" ", "");
                        streamWriter.WriteLine("异常信息：\r\n" + exception.Message);
                    }
                }
                finally
                {
                    if (streamWriter != null)
                    {
                        streamWriter.Flush();
                        streamWriter.Dispose();
                        streamWriter = null;
                    }
                }
            }
        }

        private static void InitLog(string filPath)
        {
            streamWriter = !File.Exists(filPath) ? File.CreateText(filPath) : File.AppendText(filPath);
        }

        /// <summary>
        /// 在本地写入日志
        /// 杨恒连2016-6-8添加
        /// </summary>
        /// <param name="message"></param>
        public static void WriteInfo(string message)
        {
            lock (writeFile)
            {
                try
                {
                    DateTime dt = DateTime.Now;
                    string directPath = string.Format(@"{0}\Log\Info", AppDomain.CurrentDomain.SetupInformation.ApplicationBase);
                    //记录错误日志文件的路径
                    if (!Directory.Exists(directPath))
                    {
                        Directory.CreateDirectory(directPath);
                    }
                    directPath += string.Format(@"\{0}.log", dt.ToString("yyyy-MM-dd"));
                    if (streamWriter == null)
                    {
                        InitLog(directPath);
                    }
                    streamWriter.WriteLine("***********************************************************************");
                    streamWriter.WriteLine(dt.ToString("HH:mm:ss"));
                    // streamWriter.WriteLine("输出信息：错误信息");
                    if (!string.IsNullOrEmpty(message))
                    {
                        streamWriter.WriteLine("Info信息：\r\n" + message);
                    }
                }
                finally
                {
                    if (streamWriter != null)
                    {
                        streamWriter.Flush();
                        streamWriter.Dispose();
                        streamWriter = null;
                    }
                }
            }
        }

    }
}