//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2015 , STO TECH, Ltd. 
//-----------------------------------------------------------------

using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace STO.Print.Utilities
{
    /// <summary>
    /// 进程管理帮助类
    /// </summary>
    public class ProcessHelper
    {
        /// <summary>
        /// 关闭同名的其他进程
        /// </summary>
        /// <param name="processName"></param>
        public static void KillOther(string processName)
        {
            foreach (var item in Process.GetProcesses())
            {
                if (item.ProcessName == processName && item.Id != Process.GetCurrentProcess().Id)
                {
                    item.Kill();
                }
            }
        }
        /// <summary>
        /// 关闭进程
        /// </summary>
        /// <param name="processName">进程名称</param>
        public static void Kill(string processName)
        {
            Process[] processes = Process.GetProcesses();
            foreach (var item in processes)
            {
                if (item.ProcessName == processName)
                {
                    item.Kill();
                }
            }
        }

        /// <summary>
        /// 启动进程
        /// </summary>
        public static void Start()
        {
            Start("STO.Print.Update", "1");
        }

        /// <summary>
        /// 启动进程
        /// </summary>
        /// <param name="processName">进程名称</param>
        /// <param name="args">进程参数</param>
        public static void Start(string processName, string args)
        {
            try
            {
                var mainProcess = new Process
                {
                    StartInfo =
                    {
                        FileName = Application.StartupPath + "\\" + processName + ".exe",
                        Arguments = args,
                        // 这里一定要设置成false，2016-2-8 15:23:55，手动升级才可以，不然就会出bug
                        UseShellExecute = false
                    }
                };
                if (File.Exists(mainProcess.StartInfo.FileName))
                {
                    mainProcess.Start();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
