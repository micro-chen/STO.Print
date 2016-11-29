//-------------------------------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2015 , STO , Ltd .
//-------------------------------------------------------------------------------------

using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using Xilium.CefGlue;
using Xilium.CefGlue.Client;

namespace STO.Print
{
    using DevExpress.LookAndFeel;
    using DotNet.Utilities;
    using STO.Print.Utilities;

    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        private static int Main(string[] args)
        {
            try
            {
                try
                {
                    CefRuntime.Load();
                }
                catch (DllNotFoundException ex)
                {
                    MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return 1;
                }
                catch (CefRuntimeException ex)
                {
                    MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return 2;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return 3;
                }

                var mainArgs = new CefMainArgs(args);
                var app = new CefApplication();

                int exitCode = CefRuntime.ExecuteProcess(mainArgs, app);
                if (exitCode != -1)
                    return exitCode;

                var settings = new CefSettings
                {
                    SingleProcess = false,
                    MultiThreadedMessageLoop = true,
                    LogSeverity = CefLogSeverity.Disable,
                    LogFile = "CefGlue.log",
                };

                CefRuntime.Initialize(mainArgs, settings, app);

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                if (!settings.MultiThreadedMessageLoop)
                {
                    Application.Idle += (sender, e) => { CefRuntime.DoMessageLoopWork(); };
                }
                InitDevExpress();
                UpdateHelper.ForcedUpdate();
                Application.ThreadException += Application_ThreadException;
                AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
                BaseSystemInfo.StartupPath = Application.StartupPath;
                BaseSystemInfo.CustomerCompanyName = "申通快递";
                BaseSystemInfo.SoftFullName = "申通打印专家";
                BaseSystemInfo.SoftName = BaseSystemInfo.SoftFullName;
                BaseSystemInfo.MailUserName = "766096823@qq.com";
                BaseSystemInfo.MailServer = "smtp.qq.com";
                BaseSystemInfo.MailPassword = "yhlsyx2015";
                BaseSystemInfo.SystemCode = "ZTOPrint";
                //  ZipFile();
                Synchronous.Synchronous.BeforeLogOn();
                // new FrmWaiting().ShowDialog();
                CheckPrinterWindowsServer();
                CheckInitData();
                var t = new Task(() => ComputerHelper.GetServerDataTime());
                t.Start();

               // Application.Run(new FrmLogOnByMobile());
                Application.Run(new FrmMain());
                return 0;
            }
            catch (Exception ex)
            {
                LogUtil.WriteException(ex);
                MessageBox.Show(ex.Message);
            }
            CefRuntime.Shutdown();
            return 0;
        }

        /// <summary>
        /// 重启客户端【通用】
        /// </summary>
        public static void Restart()
        {
            System.Diagnostics.Process.Start(Application.ExecutablePath, String.Format("/wait {0}", System.Diagnostics.Process.GetCurrentProcess().Id));
            // 直接杀掉进程就可以了，如果要重启【打印专家】
            Application.ExitThread();
        }


        /// <summary>
        /// 处理应用程序的未处理异常（UI线程异常）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            var frmException = new FrmException(e.Exception);
            frmException.ShowDialog();
        }

        /// <summary>
        /// 处理应用程序域内的未处理异常（非UI线程异常）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = e.ExceptionObject as Exception;
            var frmException = new FrmException(ex);
            frmException.ShowDialog();
        }

        static void InitDevExpress()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            DevExpress.Skins.SkinManager.EnableFormSkins();
            DevExpress.UserSkins.BonusSkins.Register();

            #region 设置默认字体、日期格式、汉化dev

            //string font = BillPrintHelper.GetSystemFont();
            //if (!string.IsNullOrEmpty(font))
            //{
            //    System.Drawing.Font systemFont = JsonConvert.DeserializeObject<System.Drawing.Font>(font);
            //    DevExpress.Utils.AppearanceObject.DefaultFont = systemFont;
            //}
            //try
            //{
            //    AppearanceObject.DefaultFont = new System.Drawing.Font("微软雅黑", 9);
            //}
            //catch (Exception exception)
            //{
            //    LogUtil.WriteException(exception);
            //}
            //   System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("zh-CHS");//使用DEV汉化资源文件
            // 设置程序区域语言设置中日期格式
            //   System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("zh-CHS");
            // System.Globalization.DateTimeFormatInfo di = (System.Globalization.DateTimeFormatInfo)System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.Clone();
            // di.DateSeparator = "-";
            // di.ShortDatePattern = "yyyy-MM-dd";
            // di.LongDatePattern = "yyyy'年'M'月'd'日'";
            // di.ShortTimePattern = "H:mm:ss";
            // di.LongTimePattern = "H'时'mm'分'ss'秒'";
            // ci.DateTimeFormat = di;
            // System.Threading.Thread.CurrentThread.CurrentCulture = ci;
            #endregion

            //   UserLookAndFeel.Default.SetSkinStyle("DevExpress Style");

            //System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("zh-CN");
            //System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("zh-CN");
            // System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("zh-CHS");
            UserLookAndFeel.Default.SkinName = "Summer 2008";//"Darkroom"; //"Summer 2008";  //DevExpress Dark Style
            UserLookAndFeel.Default.SetSkinStyle("Summer 2008");
            UserLookAndFeel.Default.SetSkinMaskColors(Color.FromArgb(255, 237, 105, 00), Color.FromArgb(255, 102, 101, 101));
            // UserLookAndFeel.Default.SetSkinStyle("Visual Studio 2013 Light");
        }

        internal static void SetDefaultSkin()
        {
            UserLookAndFeel.Default.SkinName = "Summer 2008";//"Darkroom"; //"Summer 2008";  //DevExpress Dark Style
            UserLookAndFeel.Default.SetSkinStyle("Summer 2008");
            UserLookAndFeel.Default.SetSkinMaskColors(Color.FromArgb(255, 237, 105, 00), Color.FromArgb(255, 102, 101, 101));
        }


        /// <summary>
        /// 检查是否设置了默认打印机和打印模板
        /// </summary>
        static void CheckInitData()
        {
            var defaultTemplate = BillPrintHelper.GetDefaultTemplatePath();
            var defaultPrinter = BillPrintHelper.GetDefaultPrinter();
            if (string.IsNullOrEmpty(defaultPrinter) || string.IsNullOrEmpty(defaultTemplate))
            {
                new FrmWizard().ShowDialog();
            }
        }
        
        /// <summary>
        /// 压缩文件夹，防止数据库挂机
        /// </summary>
        static void ZipFile()
        {
            string fileName = string.Format("{0}{1}{2}{3}{4}{5}", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Millisecond);
            string directoryPath = BaseSystemInfo.StartupPath + "\\backup\\";
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            ZipHelper zipHelper = new ZipHelper();
            zipHelper.ZipDir(BaseSystemInfo.StartupPath + "\\DataBase\\", fileName);
            string zipFileFullName = BaseSystemInfo.StartupPath + "\\" + fileName + ".zip";
            if (File.Exists(zipFileFullName))
            {
                File.Copy(zipFileFullName, directoryPath + fileName + ".zip", true);
                File.Delete(zipFileFullName);
            }
        }

        /// <summary>
        /// 程序启动检查打印的Windows服务是否启动
        /// </summary>
        static void CheckPrinterWindowsServer()
        {
            var result = PrinterHelper.CheckServerState("RpcSs");
            var result1 = PrinterHelper.CheckServerState("Spooler");
            if (result != "服务已经启动")
            {
                PrinterHelper.StartWindowsServer("RpcSs");
            }
            if (result1 != "服务已经启动")
            {
                PrinterHelper.StartWindowsServer("Spooler");
            }
        }
    }
}
