using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace STO.Print.Utilities
{
    /// <summary>
    /// 只运行一个实例及系统自动启动辅助类
    /// </summary>
    public class StartupHelper
    {
        #region 设置软件自动启动
        static RegistryKey registryKeyApp = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);

        /// <summary>
        /// 软件是否设置系统自动启动
        /// </summary>
        /// <param name="app">软件名称</param>
        /// <returns></returns>
        public static bool WillRunAtStartup(string app)
        {
            try
            {
                return string.Equals(registryKeyApp.GetValue(app), Environment.CommandLine);
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 系统设置/取消自动启动
        /// </summary>
        /// <param name="app">软件名称</param>
        /// <param name="shouldRun">设置/取消自动启动</param>
        public static void RunAtStartup(string app, bool shouldRun)
        {
            RunAtStartup(app, shouldRun, Environment.CommandLine);
        }

        /// <summary>
        /// 系统设置/取消自动启动
        /// </summary>
        /// <param name="app">软件名称</param>
        /// <param name="shouldRun">是否设置</param>
        /// <param name="exePath">系统执行路径（可增加配置参数）</param>
        public static void RunAtStartup(string app, bool shouldRun, string exePath)
        {
            try
            {
                if (shouldRun)
                {
                    registryKeyApp.SetValue(app, exePath);
                }
                else
                {
                    registryKeyApp.DeleteValue(app, false);
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine("Unable to RunAtStartup: " + ex);
            }
        } 
        #endregion

        #region 只运行一个实例

        /// <summary>
        /// 获取软件运行的系统进程对象
        /// Process instance = StartupHelper.RunningInstance();
        /// if (instance == null)
        /// {
        ///     //正常启动的代码
        /// }
        /// else
        /// {
        ///      StartupHelper.HandleRunningInstance(instance);
        /// }
        /// </summary>
        /// <returns></returns>
        public static Process RunningInstance()
        {
            Process current = Process.GetCurrentProcess();
            Process[] processes = Process.GetProcessesByName(current.ProcessName);
            //遍历与当前进程名称相同的进程列表 
            foreach (Process process in processes)
            {
                //Ignore the current process 
                if (process.Id != current.Id)
                {
                    return process;
                }
            }
            return null;
        }

        /// <summary>
        /// 处理重复运行的事件
        /// </summary>
        /// <param name="instance">系统进程对象</param>
        /// <param name="message">提示消息</param>
        public static void HandleRunningInstance(Process instance)
        {
            HandleRunningInstance(instance, null);
        }

        /// <summary>
        /// 处理重复运行的事件
        /// </summary>
        /// <param name="instance">系统进程对象</param>
        /// <param name="message">提示消息</param>
        public static void HandleRunningInstance(Process instance, string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                MessageUtil.ShowWarning(message);
            }
            
            ShowWindowAsync(instance.MainWindowHandle, WS_SHOWNORMAL); //调用api函数，正常显示窗口 
            SetForegroundWindow(instance.MainWindowHandle); //将窗口放置最前端。 
        }

        [DllImport("User32.dll")]
        private static extern bool ShowWindowAsync(System.IntPtr hWnd, int cmdShow);
        [DllImport("User32.dll")]
        private static extern bool SetForegroundWindow(System.IntPtr hWnd);
        private const int WS_SHOWNORMAL = 1;

        #endregion 
    }
}
