//-------------------------------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2015 , STO , Ltd .
//-------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Printing;
using System.Management;
using System.Printing;
using System.Runtime.InteropServices;
using System.ServiceProcess;

namespace STO.Print.Utilities
{
    /// <summary>
    /// 打印机操作类
    ///
    /// 修改纪录
    ///
    ///		2015-10-25 版本：1.0 YangHengLian 创建主键，注意命名空间的排序。
    /// 
    /// 版本：1.0
    ///
    /// <author>
    ///		<name>YangHengLian</name>
    ///		<date>2015-11-01</date>
    /// </author>
    /// <remarks>c# 获取打印任务 http://bbs.csdn.net/topics/390538574</remarks>
    /// </summary>
    public class PrinterHelper
    {
        private static readonly PrintDocument PrintDocument = new PrintDocument();

        /// <summary>
        /// 获取本机默认打印机名称
        /// </summary>
        /// <returns></returns>
        public static string DefaultPrinter()
        {
            return PrintDocument.PrinterSettings.PrinterName;
        }

        /// <summary>
        /// 获取电脑打印机列表
        /// </summary>
        /// <returns></returns>
        public static List<string> GetLocalPrinters()
        {
            var fPrinters = new List<string>();
            // 默认打印机始终出现在列表的第一项
            fPrinters.Add(DefaultPrinter());
            foreach (String fPrinterName in PrinterSettings.InstalledPrinters)
            {
                if (!fPrinters.Contains(fPrinterName))
                {
                    fPrinters.Add(fPrinterName);
                }
            }
            return fPrinters;
        }

        /// <summary>
        /// 设置默认打印机，测试有效
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        /// <remarks>调用win api将指定名称的打印机设置为默认打印机</remarks>
        [DllImport("winspool.drv")]
        public static extern bool SetDefaultPrinter(string name);

        /// <summary>
        /// C# windows服务：如何检测指定的Windows服务是否启动
        /// http://www.cnblogs.com/lujin49/p/3527704.html
        /// RPC服务器不可用？解决办法 http://m.myexception.cn/c-sharp/79953.html
        /// </summary>
        /// <param name="serviceName">这里使用的是 服务名称,不是服务显示名称 ,要使用服务显示名称,请在里面代码外使用service[i].DisplayName</param>
        /// <remarks> CheckServerState("IISADMIN"); 或者打印服务： CheckServerState("RpcSs"); </remarks>
        public static string CheckServerState(string serviceName)
        {
            string result;
            ServiceController[] serviceControllers = ServiceController.GetServices();
            // 是否启动此服务
            bool isStart = false;
            // 是否存在此服务
            bool isExite = false;
            for (int i = 0; i < serviceControllers.Length; i++)
            {
                if (serviceControllers[i].ServiceName.ToUpper().Equals(serviceName.ToUpper()))
                {
                    isExite = true;
                    if (serviceControllers[i].Status == ServiceControllerStatus.Running)
                    {
                        isStart = true;
                        break;
                    }
                }
            }

            if (!isExite)
            {
                result = "不存在此服务";
            }
            else
            {
                result = isStart ? "服务已经启动" : "服务没启动";
            }
            return result;
        }

        /// <summary>
        /// C# windows服务：启动Windows服务
        /// </summary>
        /// <param name="serviceName">这里使用的是 服务名称,不是服务显示名称 ,要使用服务显示名称,请在里面代码外使用service[i].DisplayName</param>
        public static bool StartWindowsServer(string serviceName)
        {
            bool result = false;
            ServiceController[] serviceControllers = ServiceController.GetServices();
            for (int i = 0; i < serviceControllers.Length; i++)
            {
                if (serviceControllers[i].ServiceName.ToUpper().Equals(serviceName.ToUpper()))
                {
                    if (serviceControllers[i].Status != ServiceControllerStatus.Running)
                    {
                        serviceControllers[i].Start();
                        result = true;
                        break;
                    }
                }
            }
            return result;
        }

        ///// <summary>
        ///// 获取打印机的打印任务列表
        ///// </summary>
        ///// <param name="printerPath">打印服务器的名称和完整路径</param>
        ///// <returns></returns>
        //public static PrintQueueCollection GetPrintQueues(string printerPath)
        //{
        //    PrintServer myPrintServer = new PrintServer(printerPath);
        //    PrintQueueCollection myPrintQueues = myPrintServer.GetPrintQueues();
        //    return myPrintQueues;
        //}

        /// <summary>
        /// 获取指定打印机的打印任务列表
        /// </summary>
        /// <param name="printerName"></param>
        /// <returns></returns>
        public static PrintQueue GetPrintQueue(string printerName)
        {
            var localPrintServer = new LocalPrintServer();
            localPrintServer.Refresh();
            EnumeratedPrintQueueTypes[] enumerationFlags = {
                                                               EnumeratedPrintQueueTypes.Local,
                                                            EnumeratedPrintQueueTypes.Connections,
                                                           };
            foreach (PrintQueue pq in localPrintServer.GetPrintQueues(enumerationFlags))
            {
                if (pq.Name == printerName)
                {
                    return pq;
                }
            }
            return null;
        }

        /// <summary>
        /// 查看打印队列任务状态
        /// </summary>
        /// <param name="printQueue"></param>
        /// <returns></returns>
        public static string GetPrintQueueStatus(PrintQueue printQueue)
        {
            string printStateText = "";
            if (printQueue.IsBusy)
                printStateText = "打印机正忙";
            else if (printQueue.IsDoorOpened)
                printStateText = "打印机门被打开";
            else if (printQueue.IsInError)
                printStateText = "打印机出错";
            else if (printQueue.IsInitializing)
                printStateText = "打印机正在初始化";
            else if (printQueue.IsIOActive)
                printStateText = "打印机正在与打印服务器交换数据";
            else if (printQueue.IsManualFeedRequired)
                printStateText = "打印机出错";
            else if (printQueue.IsNotAvailable)
                printStateText = "打印机状态信息不可用";
            else if (printQueue.IsTonerLow)
                printStateText = "打印机墨粉用完";
            else if (printQueue.IsOffline)
                printStateText = "打印机脱机";
            else if (printQueue.IsOutOfMemory)
                printStateText = "打印机无可用内存";
            else if (printQueue.IsOutputBinFull)
                printStateText = "打印机输出纸盒已满";
            else if (printQueue.IsPaperJammed)
                printStateText = "打印机卡纸";
            else if (printQueue.IsOutOfPaper)
                printStateText = "打印机中没有或已用完当前打印作业所需的纸张类型";
            else if (printQueue.QueueStatus == PrintQueueStatus.PaperProblem)
                printStateText = "打印机中的纸张导致未指定的错误情况";
            else if (printQueue.IsPaused)
                printStateText = "打印队列已暂停";
            else if (printQueue.IsPendingDeletion)
                printStateText = "打印队列正在删除打印作业";
            else if (printQueue.IsPrinting)
                printStateText = "设备正在打印";
            else if (printQueue.IsProcessing)
                printStateText = "设备正在执行某种工作，如果此设备是集打印机、传真机和扫描仪于一体的多功能设备，则不需要打印.";
            else if (printQueue.IsServerUnknown)
                printStateText = "打印机处于错误状态";
            else if (printQueue.IsWarmingUp)
                printStateText = "打印机正在预热";
            return printStateText;
        }

        [DllImport("printui.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        private static extern void PrintUIEntryW(IntPtr hwnd,
         IntPtr hinst, string lpszCmdLine, int nCmdShow);
        /// <summary>
        /// 打印Windows测试页面
        /// </summary>
        /// <param name="printerName">
        /// Format: \\Server\printer name, for example:
        /// \\myserver\sap3
        /// </param>
        public static void Print(string printerName)
        {
            var printParams = string.Format(@"/k /n{0}", printerName);
            PrintUIEntryW(IntPtr.Zero, IntPtr.Zero, printParams, 0);
        }

        /// <summary>
        /// 打印机属性转换
        /// </summary>
        /// <param name="printerDevice">打印机名（DeviceID）</param>
        /// <param name="iCount">当前任务数</param>
        /// <returns>打印机状态名字</returns>
        public static PrinterStatus GetPrinterState(string printerDevice, ref int iCount)
        {
            PrinterStatus ret = 0;
            string path = @"win32_printer.DeviceId='" + printerDevice + "'";
            ManagementObject printer = new ManagementObject(path);
            printer.Get();
            ret = (PrinterStatus)Convert.ToInt32(printer.Properties["PrinterStatus"].Value);
            iCount = Convert.ToInt32(printer.Properties["JobCountSinceLastReset"].Value);
            return ret;
        }

        /// <summary>
        /// 打开windows打印机的任务列表，也就是windows系统打印机右击菜单的【查看现在正在打印什么】菜单项
        /// </summary>
        /// <param name="printerName"></param>
        public static void OpenPrinterStatusWindow(string printerName)
        {
            // windows打印机执行命令 http://wenku.baidu.com/link?url=mfX024uRpMFnXAZvsYHatfenOVImM7Jjxn7Hv9G0BDvYBLyEEW2FbjxM5VdyQENHzZ_-hG9jCuqo7lC7jnhLwcutx22B0JfUlEfNhNpVdzW&qq-pf-to=pcqq.c2c
            var p = new Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.UseShellExecute = false;    //是否使用操作系统shell启动
            p.StartInfo.RedirectStandardInput = true;//接受来自调用程序的输入信息
            p.StartInfo.RedirectStandardOutput = true;//由调用程序获取输出信息
            p.StartInfo.RedirectStandardError = true;//重定向标准错误输出
            p.StartInfo.CreateNoWindow = true;//不显示程序窗口
            p.Start();
            string cmd = string.Format("rundll32 printui.dll,PrintUIEntry /o /n \"{0}\"", printerName);
            p.StandardInput.WriteLine(cmd);
            p.StandardInput.WriteLine("exit");
        }
    }

    public enum PrinterStatus
    {
        其他状态 = 1,
        未知,
        空闲,
        正在打印,
        预热,
        停止打印,
        打印中,
        离线
    }
}
