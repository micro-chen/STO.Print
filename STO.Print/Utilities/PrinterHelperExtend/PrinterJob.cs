using System;
using System.IO;
using System.Runtime.InteropServices;

namespace STO.Print.Utilities.PrinterHelperExtend
{
    /// <summary>
    /// 该类主要是发送指令，添加打印任务
    /// </summary>
    public class PrinterJob
    {
        /// <summary>
        /// OpenPrinter 打开指定的打印机，并获取打印机的句柄 
        /// </summary>
        /// <param name="szPrinter">要打开的打印机的名字</param>
        /// <param name="hPrinter">用于装载打印机的句柄</param>
        /// <param name="pd">PRINTER_DEFAULTS，这个结构保存要载入的打印机信息</param>
        /// <returns>bool</returns>
        [DllImport("winspool.Drv", EntryPoint = "OpenPrinterA", SetLastError = true, CharSet = CharSet.Auto, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool OpenPrinter([MarshalAs(UnmanagedType.LPStr)] string szPrinter, out IntPtr hPrinter, IntPtr pd);
        /// <summary>
        /// ClosePrinter 关闭一个打开的打印机对象
        /// </summary>
        /// <param name="hPrinter">一个打开的打印机对象的句柄</param>
        /// <returns></returns>
        [DllImport("winspool.Drv", EntryPoint = "ClosePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool ClosePrinter(IntPtr hPrinter);
        /// <summary>
        /// StartDocPrinter 在后台打印的级别启动一个新文档
        /// </summary>
        /// <param name="hPrinter">指定一个已打开的打印机的句柄（用openprinter取得）</param>
        /// <param name="level">1或2（仅用于win95）</param>
        /// <param name="di">包含一个DOC_INFO_1或DOC_INFO_2结构得缓冲区</param>
        /// <returns>bool 注: 在应用程序的级别并非有用。后台打印程序用它标识一个文档的开始</returns>
        [DllImport("winspool.Drv", EntryPoint = "StartDocPrinterA", SetLastError = true, CharSet = CharSet.Auto, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool StartDocPrinter(IntPtr hPrinter, Int32 level, [In, MarshalAs(UnmanagedType.LPStruct)] DOCINFOA di);
        /// <summary>
        /// EndDocPrinter 在后台打印程序的级别指定一个文档的结束
        /// </summary>
        /// <param name="hPrinter">一个已打开的打印机的句柄（用用OpenPrinter获得）</param>
        /// <returns>bool</returns>
        [DllImport("winspool.Drv", EntryPoint = "EndDocPrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool EndDocPrinter(IntPtr hPrinter);
        /// <summary>
        /// StartPagePrinter 在打印作业中指定一个新页的开始 
        /// </summary>
        /// <param name="hPrinter">指定一个已打开的打印机的句柄（用openprinter取得）</param>
        /// <returns>bool注:在应用程序的级别并非特别有用</returns>
        [DllImport("winspool.Drv", EntryPoint = "StartPagePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool StartPagePrinter(IntPtr hPrinter);
        /// <summary>
        /// EndPagePrinter 指定一个页在打印作业中的结尾 
        /// </summary>
        /// <param name="hPrinter">一个已打开的打印机对象的句柄（用OpenPrinter获得）</param>
        /// <returns>bool</returns>
        [DllImport("winspool.Drv", EntryPoint = "EndPagePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool EndPagePrinter(IntPtr hPrinter);
        /// <summary>
        /// WritePrinter 将发送目录中的数据写入打印机 
        /// </summary>
        /// <param name="hPrinter">指定一个已打开的打印机的句柄（用openprinter取得）</param>
        /// <param name="pBytes">任何类型，包含了要写入打印机的数据的一个缓冲区或结构</param>
        /// <param name="dwCount">dwCount缓冲区的长度</param>
        /// <param name="dwWritten">指定一个Long型变量，用于装载实际写入的字节数</param>
        /// <returns>bool</returns>
        [DllImport("winspool.Drv", EntryPoint = "WritePrinter", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool WritePrinter(IntPtr hPrinter, IntPtr pBytes, Int32 dwCount, out Int32 dwWritten);
        /// <summary>
        /// GetPrinter 取得与指定打印机有关的信息
        /// </summary>
        /// <param name="handle">一个已打开的打印机的句柄（用OpenPrinter获得）</param>
        /// <param name="level">1，2，3（仅适用于NT），4（仅适用于NT），或者5（仅适用于Windows 95 和 NT 4.0）</param>
        /// <param name="buffer">包含PRINTER_INFO_x结构的缓冲区。x代表级别</param>
        /// <param name="size">pPrinterEnum缓冲区中的字符数量</param>
        /// <param name="sizeNeeded">指向一个Long型变量的指针，该变量用于保存请求的缓冲区长度，或者实际读入的字节数量</param>
        /// <returns></returns>
        [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool GetPrinter(IntPtr handle, UInt32 level, IntPtr buffer, UInt32 size, out UInt32 sizeNeeded);
        /// <summary>
        /// EnumPrinters 枚举系统中安装的打印机
        /// </summary>
        /// <param name="flags">一个或多个下述标志
        /// PRINTER_ENUM_LOCAL 枚举本地打印机（包括Windows 95中的网络打印机）。名字会被忽略 
        ///PRINTER_ENUM_NAME 枚举由name参数指定的打印机。其中的名字可以是一个供应商、域或服务器。如name为NULL，则枚举出可用的打印机 
        ///PRINTER_ENUM_SHARE 枚举共享打印机（必须同其他常数组合使用） 
        ///PRINTER_ENUM_CONNECTIONS 枚举网络连接列表中的打印机（即使目前没有连接——仅适用于NT） 
        ///PRINTER_ENUM_NETWORK 枚举通过网络连接的打印机。级别（Level）必须为1。仅适用于NT 
        ///PRINTER_ENUM_REMOTE 枚举通过网络连接的打印机和打印服务器。级别必须为1。仅适用于NT 
        ///</param>
        /// <param name="name">null表示枚举同本机连接的打印机。否则由标志和级别决定</param>
        /// <param name="level">1，2，4或5（4仅适用于NT；5仅适用于Win95和NT 4.0），指定欲枚举的结构的类型。如果是1，则name参数由标志设置决定。如果是2或5，那么name就代表欲对其打印机进行枚举的服务器的名字；或者为vbNullString。如果是4，那么只有PRINTER_ENUM_LOCAL和PRINTER_ENUM_CONNECTIONS才有效。名字必须是vbNullString</param>
        /// <param name="pPrinterEnum">包含PRINTER_ENUM_x结构的缓冲区，其中的x代表级别（Level）</param>
        /// <param name="cbBuf">pPrinterEnum缓冲区中的字符数量</param>
        /// <param name="pcbNeeded">指向一个out Long型变量，该变量用于保存请求的缓冲区长度，或者实际读入的字节数量</param>
        /// <param name="pcReturned">载入缓冲区的结构数量（用于那些能返回多个结构的函数）</param>
        /// <returns>bool</returns>
        [DllImport("winspool.drv", CharSet = CharSet.Auto)]
        public static extern bool EnumPrinters(int flags, string name, int level, IntPtr pPrinterEnum, int cbBuf, out int pcbNeeded, out int pcReturned);
        /// <summary>
        /// 获取与指定作业有关的信息
        /// </summary>
        /// <param name="hPrinter">一个已打开的打印机的句柄（用OpenPrinter获得）</param>
        /// <param name="JobId">作业编号</param>
        /// <param name="level">1，2，3（仅适用于NT），4（仅适用于NT），或者5（仅适用于Windows 95 和 NT 4.0）</param>
        /// <param name="pPrinterEnum">包含PRINTER_INFO_x结构的缓冲区。x代表级别</param>
        /// <param name="cbBuf">pPrinterEnum缓冲区中的字符数量</param>
        /// <param name="pcbNeeded">指向一个uint32型变量的指针，该变量用于保存请求的缓冲区长度，或者实际读入的字节数量</param>
        /// <returns></returns>
        [DllImport("winspool.drv", CharSet = CharSet.Auto)]
        public static extern bool GetJob(IntPtr hPrinter, UInt32 JobId, UInt32 level, IntPtr pPrinterEnum, UInt32 cbBuf, out UInt32 pcbNeeded);
        /// <summary>
        /// 枚举打印队列中的作业
        /// </summary>
        /// <param name="hPrinter">一个已打开的打印机对象的句柄（用OpenPrinter获得）</param>
        /// <param name="FirstJob">作业列表中要枚举的第一个作业的索引（注意编号从0开始）</param>
        /// <param name="NoJobs">要枚举的作业数量</param>
        /// <param name="level">1或2</param>
        /// <param name="pJob">包含 JOB_INFO_1 或 JOB_INFO_2 结构的缓冲区</param>
        /// <param name="cbBuf">pJob缓冲区中的字符数量</param>
        /// <param name="pcbNeeded">指向一个Uint32型变量的指针，该变量用于保存请求的缓冲区长度，或者实际读入的字节数量</param>
        /// <param name="pcReturned">载入缓冲区的结构数量（用于那些能返回多个结构的函数）</param>
        /// <returns>bool</returns>
        [DllImport("winspool.drv", CharSet = CharSet.Auto)]
        public static extern bool EnumJobs(IntPtr hPrinter, UInt32 FirstJob, UInt32 NoJobs, UInt32 level, IntPtr pJob, UInt32 cbBuf, out UInt32 pcbNeeded, out UInt32 pcReturned);
        /// <summary>
        /// 提交一个要打印的作业
        /// </summary>
        /// <param name="hPrinter">一台已打开的打印机句柄</param>
        /// <param name="JobID">作业编号</param>
        /// <returns>bool</returns>
        [DllImport("winspool.drv", CharSet = CharSet.Auto)]
        public static extern bool ScheduleJob(IntPtr hPrinter, out UInt32 JobID);

        /// <summary>
        /// 取得与指定表单有关的信息
        /// </summary>
        /// <param name="hPrinter">打印机的句柄</param>
        /// <param name="pFormName">想获取信息的一个表单的名字</param>
        /// <param name="Level">设为1</param>
        /// <param name="pForm">包含FORM_INFO_1结构的缓冲区</param>
        /// <param name="cbBuf">pForm缓冲区中的字符数量</param>
        /// <param name="pcbNeeded">指向一个Long型变量的指针，该变量用于保存请求的缓冲区长度，或者实际读入的字节数量</param>
        /// <returns></returns>
        [DllImport("winspool.drv", CharSet = CharSet.Auto)]
        public static extern bool GetForm(IntPtr hPrinter, string pFormName, UInt32 Level, IntPtr pForm, UInt32 cbBuf, out UInt32 pcbNeeded);

        /// <summary>
        /// 枚举一台打印机可用的表单
        /// </summary>
        /// <param name="hPrinter"></param>
        /// <param name="Level"></param>
        /// <param name="pForm"></param>
        /// <param name="cbBuf"></param>
        /// <param name="pcbNeeded"></param>
        /// <param name="pcReturned"></param>
        /// <returns></returns>
        [DllImport("winspool.drv", CharSet = CharSet.Auto)]
        public static extern bool EnumForms(IntPtr hPrinter, UInt32 Level, IntPtr pForm, UInt32 cbBuf, out UInt32 pcbNeeded, out　UInt32 pcReturned);

        /// <summary>
        /// 为系统添加一个打印机监视器
        /// </summary>
        /// <param name="pName"></param>
        /// <param name="Level"></param>
        /// <param name="pMonitors"></param>
        /// <returns></returns>
        [DllImport("winspool.drv", CharSet = CharSet.Auto)]
        public static extern bool AddMonitor(IntPtr pName, UInt32 Level, IntPtr pMonitors);

        /// <summary>
        /// 枚举可用的打印监视器
        /// </summary>
        /// <param name="hPrinter"></param>
        /// <param name="Level"></param>
        /// <param name="pForm"></param>
        /// <param name="cbBuf"></param>
        /// <param name="pcbNeeded"></param>
        /// <param name="pcReturned"></param>
        /// <returns></returns>
        [DllImport("winspool.drv", CharSet = CharSet.Auto)]
        public static extern bool EnumMonitors(string hPrinter, UInt32 Level, IntPtr pForm, UInt32 cbBuf, out UInt32 pcbNeeded, out　UInt32 pcReturned);

        /// <summary>
        /// 针对指定的打印机，获取与打印机驱动程序有关的信息
        /// </summary>
        /// <param name="pName"></param>
        /// <param name="pEnvironment"></param>
        /// <param name="Level"></param>
        /// <param name="pDriverInfo"></param>
        /// <param name="cbBuf"></param>
        /// <param name="pcbNeeded"></param>
        /// <returns></returns>
        [DllImport("winspool.drv", CharSet = CharSet.Auto)]
        public static extern bool GetPrinterDriver(IntPtr pName, string pEnvironment, UInt32 Level, IntPtr pDriverInfo, UInt32 cbBuf, out  UInt32 pcbNeeded);

        /// <summary>
        /// 一个灵活的设备控制函数
        /// </summary>
        /// <param name="pName"></param>
        /// <param name="nEscape"></param>
        /// <param name="nCount"></param>
        /// <param name="lpInData"></param>
        /// <param name="lpOutData"></param>
        /// <returns></returns>
        [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
        public static extern short Escape(IntPtr pName, UInt32 nEscape, UInt32 nCount, IntPtr lpInData, out IntPtr lpOutData);

        /// <summary>
        /// 这是一个灵活的打印机配置控制函数。
        /// 该函数定义了两个DEVMODE结构，可在创建一个设备场景时为单个应用程序改变打印机设置。
        /// 甚至能在文档打印期间改变打印机设置
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="hPrinter"></param>
        /// <param name="pDeviceName"></param>
        /// <param name="pDevModeOutput"></param>
        /// <param name="pDevModeInput"></param>
        /// <param name="fMode"></param>
        /// <returns></returns>
        [DllImport("winspool.drv", CharSet = CharSet.Auto)]
        public static extern bool DocumentProperties(IntPtr hwnd, IntPtr hPrinter, string pDeviceName, out IntPtr pDevModeOutput, out IntPtr pDevModeInput, int fMode);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hPrinter"></param>
        /// <returns></returns>
        [DllImport("gdi32.dll", EntryPoint = "EndDoc", CharSet = CharSet.Auto)]
        public static extern short EndDocAPI(IntPtr hPrinter);


        #region 定义的结构体

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct PRINTER_INFO_2
        {
            public string pServerName;
            public string pPrinterName;
            public string pShareName;
            public string pPortName;
            public string pDriverName;
            public string pComment;
            public string pLocation;
            public IntPtr pDevMode;
            public string pSepFile;
            public string pPrintProcessor;
            public string pDatatype;
            public string pParameters;
            public IntPtr pSecurityDescriptor;
            public UInt32 Attributes;
            public UInt32 Priority;
            public UInt32 DefaultPriority;
            public UInt32 StartTime;
            public UInt32 UntilTime;
            public UInt32 Status;
            public UInt32 cJobs;
            public UInt32 AveragePPM;

        }
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct PRINTER_INFO_1
        {
            public int flags;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string pDescription;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string pName;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string pComment;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public class DOCINFOA
        {
            [MarshalAs(UnmanagedType.LPStr)]
            public string pDocName;
            [MarshalAs(UnmanagedType.LPStr)]
            public string pOutputFile;
            [MarshalAs(UnmanagedType.LPStr)]
            public string pDataType;
        }
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct RECT
        {
            public UInt32 Left;
            public UInt32 Top;
            public UInt32 Right;
            public UInt32 Bottom;
        }
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct FORM_INFO_1
        {
            public UInt32 Flags;
            public string pName;
            public UInt32 Size;
            public IntPtr ImageableArea;

        }
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct MONINTOR_INFO_2
        {
            public string pName;
            public string pEnvironment;
            public string pDLLName;
        }
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct JOB_INFO_1
        {
            public UInt32 Jobid;
            //[MarshalAs(UnmanagedType.LPStr)]
            public string pPrinterName;
            //[MarshalAs(UnmanagedType.LPStr)]
            public string pMachineName;
            //[MarshalAs(UnmanagedType.LPStr)]
            public string pUserName;
            //[MarshalAs(UnmanagedType.LPStr)]
            public string pDocument;
            //[MarshalAs(UnmanagedType.LPStr)]
            public string pDatatype;
            //[MarshalAs(UnmanagedType.LPStr)]
            public string pStatus;
            public UInt32 Status;
            public UInt32 Priority;
            public UInt32 Position;
            public UInt32 TotalPages;
            public UInt32 PagesPrinted;
            public IntPtr Submitted;
        }
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct JOB_INFO_2
        {
            public int Jobid;
            public string pPrinterName;
            public string pMachineName;
            public string pUserName;
            public string pDocument;
            public string pNotifyName;
            public string pDatatype;
            public string pPrintProcessor;
            //[MarshalAs(UnmanagedType.LPStr)]
            public string pParameters;
            public string pDriverName;
            public IntPtr pDevMode;
            public string pStatus;
            public IntPtr pSecurityDescriptor;
            public int Status;
            public int Priority;
            public int Position;
            public int StartTime;
            public int UntilTime;
            public int TotalPages;
            public int Size;
            public IntPtr Submitted;
            public int Time;
            public int PagesPrinted;

        }
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct SYSTEMTIME
        {
            public short wYear;
            public short wMonth;
            public short wDayOfWeek;
            public short wDay;
            public short wHour;
            public short wMinute;
            public short wSecond;
            public short wMilliseconds;

        }
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct DEVMODE
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string dmDeviceName;

            public short dmSpecVersion;
            public short dmDriverVersion;
            public short dmSize;
            public short dmDriverExtra;
            public int dmFields;
            public int dmPositionX;
            public int dmPositionY;
            public int dmDisplayOrientation;
            public int dmDisplayFixedOutput;
            public short dmColor;
            public short dmDuplex;
            public short dmYResolution;
            public short dmTTOption;
            public short dmCollate;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string dmFormName;

            public short dmLogPixels;
            public short dmBitsPerPel;
            public int dmPelsWidth;
            public int dmPelsHeight;
            public int dmDisplayFlags;
            public int dmDisplayFrequency;
            public int dmICMMethod;
            public int dmICMIntent;
            public int dmMediaType;
            public int dmDitherType;
            public int dmReserved1;
            public int dmReserved2;
            public int dmPanningWidth;
            public int dmPanningHeight;
        };
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct MONITOR_INFO_1
        {
            public string pName;
            //public string pEnvironment;
            //public string pDLLName; 

        };
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct DRIVER_INFO_1
        {
            public string pName;
        };
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct DRIVER_INFO_2
        {
            public int cVersion;
            public string pName;
            public string pEnvironment;
            public string pDriverPath;
            public string pDataFile;
            public string pConfigFile;

        };
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct DRIVER_INFO_3
        {
            public int cVersion;
            public string pName;
            public string pEnvironment;
            public string pDriverPath;
            public string pDataFile;
            public string pConfigFile;
            public string pHelpFile;
            public string pDependentFiles;
            public string pMonitorName;
            public string pDefaultDataType;
        };

        #endregion

        /// <summary>
        /// 为专门设备创建设备场景
        /// </summary>
        /// <param name="pDrive"></param>
        /// <param name="pName"></param>
        /// <param name="pOutput"></param>
        /// <param name="pDevMode"></param>
        /// <returns></returns>
        [DllImport("GDI32.dll", EntryPoint = "CreateDC", SetLastError = true, CharSet = CharSet.Auto, ExactSpelling = false,
                    CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr CreateDC([MarshalAs(UnmanagedType.LPTStr)] string pDrive,
            [MarshalAs(UnmanagedType.LPTStr)] string pName,
            [MarshalAs(UnmanagedType.LPTStr)] string pOutput,
           ref DEVMODE pDevMode);


        /// <summary>
        /// 为专用设备创建一个信息场景。
        /// 信息场景可用来快速获取某设备的信息而无须创建设备场景这样的系统开销。
        /// 它可作为参数传递给GetDeviceCaps一类的信息函数以替代设备场景参数
        /// </summary>
        /// <param name="pDrive">用vbNullString传递null值给该参数，
        /// 除非：1、用DISPLAY，是获取整个屏幕的设备场景；2、用WINSPOOL，则是访问打印驱动</param>
        /// <param name="pName"></param>
        /// <param name="pOutput"></param>
        /// <param name="pDevMode"></param>
        /// <returns></returns>
        [DllImport("GDI32.dll", EntryPoint = "CreateIC", SetLastError = true, CharSet = CharSet.Auto, ExactSpelling = false,
                    CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr CreateIC([MarshalAs(UnmanagedType.LPTStr)]
            string pDrive,
            [MarshalAs(UnmanagedType.LPTStr)] string pName,
            [MarshalAs(UnmanagedType.LPTStr)] string pOutput,
           ref DEVMODE pDevMode);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
        public static extern bool Rectangle(IntPtr hwnd, int x, int y, int x1, int y1);
        [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr CreateEnhMetaFile(IntPtr hdcRef, string lpFileName, ref RECT lpRect, string lpDescription);
        [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr CloseEnhMetaFile(IntPtr hdcRef);
        [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr CopyEnhMetaFile(IntPtr hdcRef, string lpszFile);
        [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr GetDC(IntPtr hdcRef);


        [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr GetDeviceCaps(IntPtr hdc, int nIndex);
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr GetModuleHandle(string name);

        public struct TempFile
        {
            public string pFullName;
            public long leng;
            public string pName;
            //public string pEnvironment;
            //public string pDLLName; 

        };

        #region 发送数据，添加作业

        public static bool SendBytesToPrinter(string szPrinterName, IntPtr pBytes, Int32 dwCount)
        {
            Int32 dwError = 0, dwWritten = 0;
            IntPtr hPrinter = new IntPtr(0);
            DOCINFOA di = new DOCINFOA();
            bool bSuccess = false; // Assume failure unless you specifically succeed.

            di.pDocName = "测试文档";
            di.pDataType = "TEXT";  //RAW     在虚拟打印机上测试必须将pDataType = "RAW"  改为 TEXT

            // Open the printer.
            if (OpenPrinter(szPrinterName.Normalize(), out hPrinter, IntPtr.Zero))
            {
                // Start a document.
                if (StartDocPrinter(hPrinter, 1, di))
                {
                    // Start a page.
                    if (StartPagePrinter(hPrinter))
                    {
                        // Write your bytes.
                        bSuccess = WritePrinter(hPrinter, pBytes, dwCount, out dwWritten);
                        EndPagePrinter(hPrinter);
                    }
                    EndDocPrinter(hPrinter);

                }
                //int aa= EndDocAPI(hPrinter);

                ClosePrinter(hPrinter);
            }
            // If you did not succeed, GetLastError may give more information
            // about why not.
            if (bSuccess == false)
            {
                dwError = Marshal.GetLastWin32Error();
            }
            return bSuccess;
        }

        public static bool SendFileToPrinter(string szPrinterName, string szFileName)
        {
            // Open the file.
            FileStream fs = new FileStream(szFileName, FileMode.Open);
            // Create a BinaryReader on the file.
            BinaryReader br = new BinaryReader(fs);
            // Dim an array of bytes big enough to hold the file's contents.
            Byte[] bytes = new Byte[fs.Length];
            bool bSuccess = false;
            // Your unmanaged pointer.
            IntPtr pUnmanagedBytes = new IntPtr(0);
            int nLength;

            nLength = Convert.ToInt32(fs.Length);
            // Read the contents of the file into the array.
            bytes = br.ReadBytes(nLength);
            // Allocate some unmanaged memory for those bytes.
            pUnmanagedBytes = Marshal.AllocCoTaskMem(nLength);
            // Copy the managed byte array into the unmanaged array.
            Marshal.Copy(bytes, 0, pUnmanagedBytes, nLength);
            // Send the unmanaged bytes to the printer.
            bSuccess = SendBytesToPrinter(szPrinterName, pUnmanagedBytes, nLength);
            // Free the unmanaged memory that you allocated earlier.
            Marshal.FreeCoTaskMem(pUnmanagedBytes);
            return bSuccess;
        }

        public static bool SendStringToPrinter(string szPrinterName, string szString)
        {
            IntPtr pBytes;
            Int32 dwCount;
            // How many characters are in the string?
            dwCount = szString.Length;
            // Assume that the printer is expecting ANSI text, and then convert
            // the string to ANSI text.
            pBytes = Marshal.StringToCoTaskMemAnsi(szString);
            // Send the converted ANSI string to the printer.
            SendBytesToPrinter(szPrinterName, pBytes, dwCount);
            Marshal.FreeCoTaskMem(pBytes);
            return true;
        }

        #endregion
    }

}