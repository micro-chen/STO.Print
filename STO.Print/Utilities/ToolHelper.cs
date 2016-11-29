//-------------------------------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2015 , STO , Ltd .
//-------------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace STO.Print.Utilities
{
    using DotNet.Utilities;
    using Microsoft.Win32;

    /// <summary>
    /// 系统小工具类
    ///
    /// 修改纪录
    ///
    ///		  2015-03-11  版本：1.0 YangHengLian 创建主键，注意命名空间的排序。
    ///       2015-05-27  打开默认浏览器如果是IE的时候有时候会打开C盘的路径，所以报错误了已经解决了
    /// 
    /// 版本：1.0
    ///
    /// <author>
    ///		<name>YangHengLian</name>
    /// 	<date>2015-03-11</date>
    /// </author>
    /// </summary>
    public static class ToolHelper
    {
        /// <summary>
        /// 打开系统计算器
        /// </summary>
        /// <param name="arguments">外部程序的启动参数</param>
        public static void OpenCalculator(string arguments = null)
        {
            var info = new ProcessStartInfo();
            // 设置外部程序名(记事本用 notepad.exe 计算器用 calc.exe) 
            info.FileName = "calc.exe";
            // 设置外部程序的启动参数
            info.Arguments = string.IsNullOrEmpty(arguments) ? "" : arguments;
            // 设置外部程序工作目录为c:\windows
            info.WorkingDirectory = "c:/windows/";
            try
            {
                Process.Start(info);
            }
            catch (Exception ex)
            {
                LogUtil.WriteException(ex);
            }
        }

        /// <summary>
        /// 打开系统记事本
        /// </summary>
        /// <param name="arguments">外部程序的启动参数</param>
        public static void OpenNotePad(string arguments = null)
        {
            var info = new ProcessStartInfo();
            // 设置外部程序名(记事本用 notepad.exe 计算器用 calc.exe) 
            info.FileName = "notepad.exe";
            // 设置外部程序的启动参数
            info.Arguments = string.IsNullOrEmpty(arguments) ? "" : arguments;
            // 设置外部程序工作目录为c:\windows
            info.WorkingDirectory = "c:/windows/";
            try
            {
                Process.Start(info);
            }
            catch (Exception ex)
            {
                LogUtil.WriteException(ex);
            }
        }

        /// <summary>
        /// 调用系统浏览器打开网页
        /// http://m.jb51.net/article/44622.htm
        /// http://www.2cto.com/kf/201412/365633.html
        /// </summary>
        /// <param name="url">打开网页的链接</param>
        public static void OpenBrowserUrl(string url)
        {
            try
            {
                // 64位注册表路径
                var openKey = @"SOFTWARE\Wow6432Node\Google\Chrome";
                if (IntPtr.Size == 4)
                {
                    // 32位注册表路径
                    openKey = @"SOFTWARE\Google\Chrome";
                }
                RegistryKey appPath = Registry.LocalMachine.OpenSubKey(openKey);
                // 谷歌浏览器就用谷歌打开，没找到就用系统默认的浏览器
                // 谷歌卸载了，注册表还没有清空，程序会返回一个"系统找不到指定的文件。"的bug
                if (appPath != null)
                {
                    var result = Process.Start("chrome.exe", url);
                    if (result == null)
                    {
                        OpenIe(url);
                    }
                }
                else
                {
                    var result = Process.Start("chrome.exe", url);
                    if (result == null)
                    {
                        OpenDefaultBrowserUrl(url);
                    }
                }
            }
            catch
            {
                // 出错调用用户默认设置的浏览器，还不行就调用IE
                OpenDefaultBrowserUrl(url);
            }
        }

        /// <summary>
        /// 用IE打开浏览器
        /// </summary>
        /// <param name="url"></param>
        public static void OpenIe(string url)
        {
            try
            {
                Process.Start("iexplore.exe", url);
            }
            catch (Exception ex)
            {
                // IE浏览器路径安装：C:\Program Files\Internet Explorer
                // at System.Diagnostics.process.StartWithshellExecuteEx(ProcessStartInfo startInfo)注意这个错误
                try
                {
                    if (File.Exists(@"C:\Program Files\Internet Explorer\iexplore.exe"))
                    {
                        ProcessStartInfo processStartInfo = new ProcessStartInfo
                        {
                            FileName = @"C:\Program Files\Internet Explorer\iexplore.exe",
                            Arguments = url,
                            UseShellExecute = false,
                            CreateNoWindow = true
                        };
                        Process.Start(processStartInfo);
                    }
                    else
                    {
                        if (File.Exists(@"C:\Program Files (x86)\Internet Explorer\iexplore.exe"))
                        {
                            ProcessStartInfo processStartInfo = new ProcessStartInfo
                            {
                                FileName = @"C:\Program Files (x86)\Internet Explorer\iexplore.exe",
                                Arguments = url,
                                UseShellExecute = false,
                                CreateNoWindow = true
                            };
                            Process.Start(processStartInfo);
                        }
                        else
                        {
                            if (MessageBox.Show(@"系统未安装IE浏览器，是否下载安装？", null, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                // 打开下载链接，从微软官网下载
                                OpenDefaultBrowserUrl("http://windows.microsoft.com/zh-cn/internet-explorer/download-ie");
                            }
                        }
                    }
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message, AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// 打开系统默认浏览器（用户自己设置了默认浏览器）
        /// </summary>
        /// <param name="url"></param>
        public static void OpenDefaultBrowserUrl(string url)
        {
            try
            {
                // 方法1
                //从注册表中读取默认浏览器可执行文件路径
                RegistryKey key = Registry.ClassesRoot.OpenSubKey(@"http\shell\open\command\");
                if (key != null)
                {
                    string s = key.GetValue("").ToString();
                    //s就是你的默认浏览器，不过后面带了参数，把它截去，不过需要注意的是：不同的浏览器后面的参数不一样！
                    //"D:\Program Files (x86)\Google\Chrome\Application\chrome.exe" -- "%1"
                    var lastIndex = s.IndexOf(".exe", StringComparison.Ordinal);
                    if (lastIndex == -1)
                    {
                        lastIndex = s.IndexOf(".EXE", StringComparison.Ordinal);
                    }
                    var path = s.Substring(1, lastIndex + 3);
                    var result = Process.Start(path, url);
                    if (result == null)
                    {
                        // 方法2
                        // 调用系统默认的浏览器 
                        var result1 = Process.Start("explorer.exe", url);
                        if (result1 == null)
                        {
                            // 方法3
                            Process.Start(url);
                        }
                    }
                }
                else
                {
                    // 方法2
                    // 调用系统默认的浏览器 
                    var result1 = Process.Start("explorer.exe", url);
                    if (result1 == null)
                    {
                        // 方法3
                        Process.Start(url);
                    }
                }
            }
            catch
            {
                OpenIe(url);
            }
        }

        /// <summary>
        /// 火狐浏览器打开网页
        /// </summary>
        /// <param name="url"></param>
        public static void OpenFireFox(string url)
        {
            try
            {
                // 64位注册表路径
                var openKey = @"SOFTWARE\Wow6432Node\Mozilla\Mozilla Firefox";
                if (IntPtr.Size == 4)
                {
                    // 32位注册表路径
                    openKey = @"SOFTWARE\Mozilla\Mozilla Firefox";
                }
                RegistryKey appPath = Registry.LocalMachine.OpenSubKey(openKey);
                if (appPath != null)
                {
                    var result = Process.Start("firefox.exe", url);
                    if (result == null)
                    {
                        OpenIe(url);
                    }
                }
                else
                {
                    var result = Process.Start("firefox.exe", url);
                    if (result == null)
                    {
                        OpenDefaultBrowserUrl(url);
                    }
                }
            }
            catch
            {
                OpenDefaultBrowserUrl(url);
            }
        }

        /// <summary>
        /// 自写四舍五入法
        /// </summary>
        /// <param name="sourceNum">要进行处理的数据</param>
        /// <param name="toRemainIndex">保留的小数位数</param>
        /// <returns>四舍五入后的结果</returns>
        /// <remarks>http://www.cnblogs.com/tsunami/archive/2011/11/30/2269529.html</remarks>
        public static decimal Round(decimal sourceNum, int toRemainIndex)
        {
            decimal result = sourceNum;
            string sourceString = sourceNum.ToString();
            // 没有小数点,则返回原数据+"."+"保留小数位数个0"
            if (!sourceString.Contains("."))
            {
                result = Convert.ToDecimal(sourceString + "." + CreateZeros(toRemainIndex));
                return result;
            }
            // 小数点的位数没有超过要保留的位数,则返回原数据+"保留小数位数 - 已有的小数位"个0
            if ((sourceString.Length - sourceString.IndexOf(".") - 1) <= toRemainIndex)
            {
                result = Convert.ToDecimal(sourceString + CreateZeros(toRemainIndex - (sourceString.Length - sourceString.IndexOf(".") - 1)));
                return result;
            }
            string beforeAbandon_String = string.Empty;
            beforeAbandon_String = sourceString.Substring(0, sourceString.IndexOf(".") + toRemainIndex + 1);
            // 取得如3.1415926保小数点后4位(原始的,还没开始取舍)，中的3.1415
            decimal beforeAbandon_Decial = Convert.ToDecimal(beforeAbandon_String);

            // 如果保留小数点后N位，则判断N+1位是否大于等于5，大于，则进一，否则舍弃。
            if (int.Parse(sourceString.Substring(sourceString.IndexOf(".") + toRemainIndex + 1, 1)) >= 5)
            {
                #region 四舍五入算法说明
                //进一的方法举例 3.1415926,因为5后面的是9，所以5要进一位：如下：
                // 3.1415
                //         +
                // 0.0001
                //_________
                // 3.1416
                //保留N位时：
                // 3.14.......15
                //             +
                // 0.00.......01
                //_________
                // 3.14.......16
                #endregion
                string toAddAfterPoint = "0." + CreateZeros(toRemainIndex - 1) + "1";
                result = beforeAbandon_Decial + Convert.ToDecimal(toAddAfterPoint);
            }
            else
            {
                result = beforeAbandon_Decial;
            }
            return result;
        }


        /// <summary>
        /// 补 "0"方法.
        /// </summary>
        /// <param name="zeroCounts">生成个数.</param>
        /// <returns></returns>
        /// <Author> frd  2011-11-2511:06</Author>
        private static string CreateZeros(int zeroCounts)
        {
            string result = string.Empty;
            if (zeroCounts == 0)
            {
                result = "";
                return result;
            }
            for (int i = 0; i < zeroCounts; i++)
            {
                result += "0";
            }
            return result;
        }

        /// <summary>
        /// 设置本地电脑的年月日
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="day"></param>
        public static void SetLocalDate(int year, int month, int day)
        {
            //实例一个Process类，启动一个独立进程 
            Process p = new Process();
            //Process类有一个StartInfo属性 
            //设定程序名 
            p.StartInfo.FileName = "cmd.exe";
            //设定程式执行参数 “/C”表示执行完命令后马上退出
            p.StartInfo.Arguments = string.Format("/c date {0}-{1}-{2}", year, month, day);
            //关闭Shell的使用 
            p.StartInfo.UseShellExecute = false;
            //重定向标准输入 
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            //重定向错误输出 
            p.StartInfo.RedirectStandardError = true;
            //设置不显示doc窗口 
            p.StartInfo.CreateNoWindow = true;
            //启动 
            p.Start();
            //从输出流取得命令执行结果 
            p.StandardOutput.ReadToEnd();
        }

        /// <summary>
        /// 设置本机电脑的时分秒
        /// </summary>
        /// <param name="hour"></param>
        /// <param name="min"></param>
        /// <param name="sec"></param>
        public static void SetLocalTime(int hour, int min, int sec)
        {
            //实例一个Process类，启动一个独立进程 
            Process p = new Process();
            //Process类有一个StartInfo属性 
            //设定程序名 
            p.StartInfo.FileName = "cmd.exe";
            //设定程式执行参数 “/C”表示执行完命令后马上退出
            p.StartInfo.Arguments = string.Format("/c time {0}:{1}:{2}", hour, min, sec);
            //关闭Shell的使用 
            p.StartInfo.UseShellExecute = false;
            //重定向标准输入 
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            //重定向错误输出 
            p.StartInfo.RedirectStandardError = true;
            //设置不显示doc窗口 
            p.StartInfo.CreateNoWindow = true;
            //启动 
            p.Start();
            //从输出流取得命令执行结果 
            p.StandardOutput.ReadToEnd();
        }

        /// <summary>
        /// 设置本机电脑的年月日和时分秒
        /// </summary>
        /// <param name="time"></param>
        public static void SetLocalDateTime(DateTime time)
        {
            SetLocalDate(time.Year, time.Month, time.Day);
            SetLocalTime(time.Hour, time.Minute, time.Second);
        }

        /// <summary>
        /// 设置应用程序开机自动运行
        /// </summary>
        /// <param name="fileName">应用程序的文件名</param>
        /// <param name="isAutoRun">是否自动运行,为false时，取消自动运行</param>
        /// <remarks> 调用代码：ToolHelper.SetAutoRun(Application.StartupPath + @"\STO.Print.exe", true);</remarks>
        /// <returns>返回1自动启动成功，返回2取消自动启动，其他不成功</returns>
        public static string SetAutoRunWhenStart(string fileName, bool isAutoRun)
        {
            string reSet = string.Empty;
            RegistryKey reg = null;
            try
            {
                if (!System.IO.File.Exists(fileName))
                {
                    reSet = "该文件不存在!";
                }
                else
                {
                    string name = fileName.Substring(fileName.LastIndexOf(@"\", System.StringComparison.Ordinal) + 1);
                    reg = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
                    if (reg == null)
                    {
                        reg = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run");
                    }
                    if (isAutoRun)
                    {
                        if (reg != null) reg.SetValue(name, fileName);
                        reSet = "1";
                    }
                    else
                    {
                        if (reg != null) reg.SetValue(name, false);
                        reSet = "2";
                    }
                }
            }
            catch (Exception ex)
            {
                //“记录异常”
            }
            finally
            {
                if (reg != null)
                {
                    reg.Close();
                }
            }
            return reSet;
        }
    }
}
