using System;
using System.Runtime.InteropServices;

namespace STO.Print.Utilities.PrinterHelperExtend
{
    public class PrintLpt
    {
        #region 申明要引用的要调用的API
        //win32 api constants 
        private const uint GENERIC_READ = 0x80000000;
        private const uint GENERIC_WRITE = 0x40000000;
        private const int OPEN_EXISTING = 3;
        private const int INVALID_HANDLE_VALUE = -1;
        private const uint FILE_FLAG_OVERLAPPED = 0x40000000;   //异步通讯
        private const int MAXBLOCK = 4096;

        private const uint PURGE_TXABORT = 0x0001;  // Kill the pending/current writes to the comm port.
        private const uint PURGE_RXABORT = 0x0002;  // Kill the pending/current reads to the comm port.
        private const uint PURGE_TXCLEAR = 0x0004;  // Kill the transmit queue if there.
        private const uint PURGE_RXCLEAR = 0x0008;  // Kill the typeahead buffer if there.

        private const int EV_RXCHAR = 0x0001;

        //private const uint EV_RXCHAR = 

        [StructLayout(LayoutKind.Sequential)]
        private struct DCB
        {
            //taken from c struct in platform sdk  
            public int DCBlength;
            public int BaudRate;
            public int fBinary;
            public int fParity;
            public int fOutxCtsFlow;
            public int fOutxDsrFlow;
            public int fDtrControl;
            public int fDsrSensitivity;


            public int fTXContinueOnXoff;
            public int fOutX;
            public int fInX;
            public int fErrorChar;
            public int fNull;
            public int fRtsControl;

            public int fAbortOnError;
            public int fDummy2;
            public ushort wReserved;

            public ushort XonLim;
            public ushort XoffLim;
            public byte ByteSize;
            public byte Parity;
            public byte StopBits;
            public char XonChar;
            public char XoffChar;
            public char ErrorChar;


            public char EofChar;
            public char EvtChar;
            public ushort wReserved1;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct COMMTIMEOUTS
        {
            public int ReadIntervalTimeout;
            public int ReadTotalTimeoutMultiplier;
            public int ReadTotalTimeoutConstant;
            public int WriteTotalTimeoutMultiplier;
            public int WriteTotalTimeoutConstant;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct OVERLAPPED
        {
            public int Internal;
            public int InternalHigh;
            public int Offset;
            public int OffsetHigh;
            public int hEvent;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct COMSTAT
        {
            /**/
            /*public int fCtsHold;
                                public int fDsrHold;
                                public int fRlsdHold;
                                public int fXoffHold;
                                public int fXoffSent;
                                public int fEof;
                                public int fTxim;
                                public int fReserved;
                                public int cbInQue;
                                public int cbOutQue;*/
            // Should have a reverse, i don't know why!!!!!
            public int cbOutQue;
            public int cbInQue;
            public int fReserved;
            public int fTxim;
            public int fEof;
            public int fXoffSent;
            public int fXoffHold;
            public int fRlsdHold;
            public int fDsrHold;
            public int fCtsHold;
        }
#if FULLFRAMEWORK
  [DllImport("kernel32")] 
  private static extern int CreateFile( 
   string lpFileName,                         // file name 
   uint dwDesiredAccess,                      // access mode 
   int dwShareMode,                          // share mode 
   int lpSecurityAttributes, // SD 
   int dwCreationDisposition,                // how to create 
   int dwFlagsAndAttributes,                 // file attributes 
   int hTemplateFile                        // handle to template file 
   );
#else
        [DllImport("kernel32")]
        private static extern int CreateFile(
         string lpFileName,                         // file name 
         uint dwDesiredAccess,                      // access mode 
         int dwShareMode,                          // share mode 
         int lpSecurityAttributes, // SD 
         int dwCreationDisposition,                // how to create 
         uint dwFlagsAndAttributes,                 // file attributes 
         int hTemplateFile                        // handle to template file 
         );
#endif
#if FULLFRAMEWORK
  [DllImport("kernel32")] 
  private static extern bool GetCommState( 
   int hFile,  // handle to communications device 
   ref DCB lpDCB    // device-control block 
   );   
#else
        //取得並口狀態
        [DllImport("kernel32")]
        private static extern bool GetCommState(
         int hFile,  // handle to communications device 
         ref DCB lpDCB    // device-control block 
         );
#endif
#if FULLFRAMEWORK
  [DllImport("kernel32")] 
  private static extern bool BuildCommDCB( 
   string lpDef,  // device-control string 
   ref DCB lpDCB     // device-control block 
   ); 
#else
        [DllImport("kernel32")]
        private static extern bool BuildCommDCB(
         string lpDef,  // device-control string 
         ref DCB lpDCB     // device-control block 
         );
#endif
#if FULLFRAMEWORK
  [DllImport("kernel32")] 
  private static extern bool SetCommState( 
   int hFile,  // handle to communications device 
   ref DCB lpDCB    // device-control block 
   );
#else
        [DllImport("kernel32")]
        private static extern bool SetCommState(
         int hFile,  // handle to communications device 
         ref DCB lpDCB    // device-control block 
         );
#endif
#if FULLFRAMEWORK
  [DllImport("kernel32")] 
  private static extern bool GetCommTimeouts( 
   int hFile,                  // handle to comm device 
   ref COMMTIMEOUTS lpCommTimeouts  // time-out values 
   );
#else
        [DllImport("kernel32")]
        private static extern bool GetCommTimeouts(
         int hFile,                  // handle to comm device 
         ref COMMTIMEOUTS lpCommTimeouts  // time-out values 
         );
#endif
#if FULLFRAMEWORK
  [DllImport("kernel32")]    
  private static extern bool SetCommTimeouts( 
   int hFile,                  // handle to comm device 
   ref COMMTIMEOUTS lpCommTimeouts  // time-out values 
   );
#else
        [DllImport("kernel32")]
        private static extern bool SetCommTimeouts(
         int hFile,                  // handle to comm device 
         ref COMMTIMEOUTS lpCommTimeouts  // time-out values 
         );
#endif
#if FULLFRAMEWORK
  [DllImport("kernel32")] 
  private static extern bool ReadFile( 
   int hFile,                // handle to file 
   byte[] lpBuffer,             // data buffer 
   int nNumberOfBytesToRead,  // number of bytes to read 
   ref int lpNumberOfBytesRead, // number of bytes read 
   ref OVERLAPPED lpOverlapped    // overlapped buffer 
   );
#else
        [DllImport("kernel32")]
        private static extern bool ReadFile(
         int hFile,                // handle to file 
         byte[] lpBuffer,             // data buffer 
         int nNumberOfBytesToRead,  // number of bytes to read 
         ref int lpNumberOfBytesRead, // number of bytes read 
         ref OVERLAPPED lpOverlapped    // overlapped buffer 
         );
#endif
#if FULLFRAMEWORK
  [DllImport("kernel32")] 
  private static extern bool WriteFile( 
   int hFile,                    // handle to file 
   byte[] lpBuffer,                // data buffer 
   int nNumberOfBytesToWrite,     // number of bytes to write 
   ref int lpNumberOfBytesWritten,  // number of bytes written 
   ref OVERLAPPED lpOverlapped        // overlapped buffer 
   ); 
#else
        [DllImport("kernel32")]
        private static extern bool WriteFile(
         int hFile,                    // handle to file 
         byte[] lpBuffer,                // data buffer 
         int nNumberOfBytesToWrite,     // number of bytes to write 
         ref int lpNumberOfBytesWritten,  // number of bytes written 
         ref OVERLAPPED lpOverlapped        // overlapped buffer 
         );
#endif
#if FULLFRAMEWORK
  [DllImport("kernel32")] 
  private static extern bool CloseHandle( 
   int hObject   // handle to object 
   );
#else
        [DllImport("kernel32")]
        private static extern bool CloseHandle(
         int hObject   // handle to object 
         );
#endif
#if FULLFRAMEWORK
  [DllImport("kernel32")]
  private static extern bool ClearCommError(
   int hFile,     // handle to file
   ref int lpErrors,
   ref COMSTAT lpStat
  );
#else
        [DllImport("kernel32")]
        private static extern bool ClearCommError(
         int hFile,     // handle to file
         ref int lpErrors,
         ref COMSTAT lpStat
         );
#endif
#if FULLFRAMEWORK
  [DllImport("kernel32")]
  private static extern bool PurgeComm(
   int hFile,     // handle to file
   uint dwFlags
   );
#else
        [DllImport("kernel32")]
        private static extern bool PurgeComm(
         int hFile,     // handle to file
         uint dwFlags
         );
#endif
#if FULLFRAMEWORK
  [DllImport("kernel32")]
  private static extern bool SetupComm(
   int hFile,
   int dwInQueue,
   int dwOutQueue
  );
#else
        [DllImport("kernel32")]
        private static extern bool SetupComm(
         int hFile,
         int dwInQueue,
         int dwOutQueue
         );
#endif

#if FULLFRAMEWORK
  [DllImport("kernel32")] 
  private static extern bool SetCommMask( 
  int hFile,
  int dwEvtMask   
   );
#else
        [DllImport("kernel32")]
        private static extern bool SetCommMask(
         int hFile,
         int dwEvtMask   //表明事件的掩码
         );
#endif

#if FULLFRAMEWORK
  [DllImport("kernel32")] 
  private static extern bool WaitCommEvent( 
  int hFile,
  ref int lpEvtMask,
  ref OVERLAPPED lpOverlapped

  );
#else
        [DllImport("kernel32")]
        private static extern bool WaitCommEvent(
         int hFile,
         int dwEvtMask,
         ref OVERLAPPED lpOverlapped
         );
#endif

#if FULLFRAMEWORK
  [DllImport("kernel32")] 
  private static extern int CreateEvent( 
  int lpSecurityAttributes,
  bool bManualReset,
  bool bInitialState,
  string lpName
  );
#else
        [DllImport("kernel32")]
        private static extern int CreateEvent(

         int lpSecurityAttributes,
         bool bManualReset,
         bool bInitialState,
         string lpName
         );
#endif
        //private static extern int
        #endregion

        // SerialPort成員
        private int hComm = INVALID_HANDLE_VALUE;
        private bool bOpened = false;   //标志并口初始化成功与否的标志

        public bool Opened      //用属性返回标志变量
        {
            get
            {
                return bOpened;
            }
        }

        /**/
        /// <summary>
        ///並口的初始化函數
        ///lpFileName 端口名
        ///baudRate  波特率
        ///parity  校驗位
        ///byteSize  數據位
        ///stopBits  停止位
        /// <summary>
        public bool OpenPort(string lpFileName, int baudRate, byte parity, byte byteSize, byte stopBits)
        {
            // OPEN THE COMM PORT.   
            //hComm = CreateFile(lpFileName ,GENERIC_READ | GENERIC_WRITE, 0, 0, OPEN_EXISTING, 0, 0); 
            // IF THE PORT CANNOT BE OPENED, BAIL OUT. 
            if (hComm == INVALID_HANDLE_VALUE)
            {
                // OPEN THE COMM PORT.   
                hComm = CreateFile(lpFileName, GENERIC_READ | GENERIC_WRITE, 0, 0, OPEN_EXISTING, 0, 0);
                SetCommMask(hComm, EV_RXCHAR);
                //hComm = CreateFile("COM" + PortNum, GENERIC_READ | GENERIC_WRITE,0, 0,OPEN_EXISTING,FILE_FLAG_OVERLAPPED,0);
                return true;
            }
            else
            {
                //System.MessageBox.Show("並口已打開");
                throw (new ApplicationException("並口已打開"));
            }
            //SetCommMask();

            SetupComm(hComm, MAXBLOCK, MAXBLOCK);

            // SET THE COMM TIMEOUTS.
            COMMTIMEOUTS ctoCommPort = new COMMTIMEOUTS();
            GetCommTimeouts(hComm, ref ctoCommPort);
            ctoCommPort.ReadIntervalTimeout = Int32.MaxValue;
            ctoCommPort.ReadTotalTimeoutConstant = 0;
            ctoCommPort.ReadTotalTimeoutMultiplier = 0;
            ctoCommPort.WriteTotalTimeoutMultiplier = 10;
            ctoCommPort.WriteTotalTimeoutConstant = 1000;
            SetCommTimeouts(hComm, ref ctoCommPort);

            // SET BAUD RATE, PARITY, WORD SIZE, AND STOP BITS. 
            // THERE ARE OTHER WAYS OF DOING SETTING THESE BUT THIS IS THE EASIEST. 
            // IF YOU WANT TO LATER ADD CODE FOR OTHER BAUD RATES, REMEMBER 
            // THAT THE ARGUMENT FOR BuildCommDCB MUST BE A POINTER TO A STRING. 
            // ALSO NOTE THAT BuildCommDCB() DEFAULTS TO NO HANDSHAKING. 
            DCB dcbCommPort = new DCB();
            dcbCommPort.DCBlength = Marshal.SizeOf(dcbCommPort);
            GetCommState(hComm, ref dcbCommPort);    //取得並口的狀態

            dcbCommPort.BaudRate = baudRate;
            dcbCommPort.Parity = parity;
            dcbCommPort.ByteSize = byteSize;
            dcbCommPort.StopBits = stopBits;

            if (!SetCommState(hComm, ref dcbCommPort))
            {
                throw (new ApplicationException("非法操作不能打开并口"));
            }

            PurgeComm(hComm, PURGE_RXCLEAR | PURGE_RXABORT);  //清空發送緩沖區的所有數據 
            PurgeComm(hComm, PURGE_TXCLEAR | PURGE_TXABORT);  //清空收掃沖的骨有據
            SetCommMask(hComm, EV_RXCHAR);
            bOpened = true;
            return true;
        }

        //清空發送緩沖區的所有數據 
        public void CleanPortData()
        {
            PurgeComm(hComm, PURGE_RXCLEAR | PURGE_RXABORT);
        }

        // 關閉並口
        public bool ClosePort()
        {
            if (hComm == INVALID_HANDLE_VALUE)
            {
                return false;
            }

            if (CloseHandle(hComm))
            {
                hComm = INVALID_HANDLE_VALUE;
                bOpened = false;
                return true;
            }
            else
            {
                return false;
            }
        }

        // 往并口写数据
        public bool WritePort(byte[] WriteBytes)
        {
            if (hComm == INVALID_HANDLE_VALUE)  //如果并口未打开
            {
                return false;
            }

            COMSTAT ComStat = new COMSTAT();
            int dwErrorFlags = 0;

            ClearCommError(hComm, ref dwErrorFlags, ref ComStat);
            if (dwErrorFlags != 0)
                PurgeComm(hComm, PURGE_TXCLEAR | PURGE_TXABORT);

            OVERLAPPED ovlCommPort = new OVERLAPPED();
            int BytesWritten = 0;
            // SET THE COMM TIMEOUTS.
            COMMTIMEOUTS ctoCommPort = new COMMTIMEOUTS();
            GetCommTimeouts(hComm, ref ctoCommPort);
            ctoCommPort.ReadIntervalTimeout = Int32.MaxValue;
            ctoCommPort.ReadTotalTimeoutConstant = 0;
            ctoCommPort.ReadTotalTimeoutMultiplier = 0;
            ctoCommPort.WriteTotalTimeoutMultiplier = 10;
            ctoCommPort.WriteTotalTimeoutConstant = 3000;
            if (!SetCommTimeouts(hComm, ref ctoCommPort))
                return false;
            WriteFile(hComm, WriteBytes, WriteBytes.Length, ref BytesWritten, ref ovlCommPort);
            if (BytesWritten > 0)
                return true;
            else
                return false;
        }

        // 从并口读取数据
        public bool ReadPort(int NumBytes, ref byte[] commRead)
        {
            if (hComm == INVALID_HANDLE_VALUE)
            {
                return false;
            }

            COMSTAT ComStat = new COMSTAT();
            int dwErrorFlags = 0;

            ClearCommError(hComm, ref dwErrorFlags, ref ComStat);
            if (dwErrorFlags != 0)
                PurgeComm(hComm, PURGE_RXCLEAR | PURGE_RXABORT);

            if (ComStat.cbInQue > 0)
            {
                OVERLAPPED ovlCommPort = new OVERLAPPED();
                int BytesRead = 0;
                return ReadFile(hComm, commRead, NumBytes, ref BytesRead, ref ovlCommPort);
            }
            else
            {
                return false;
            }
        }

    }
}
