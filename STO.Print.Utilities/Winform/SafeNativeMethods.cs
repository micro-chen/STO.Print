using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Security;

namespace STO.Print.Utilities
{
    /// <summary>
    /// Has suppress unmanaged code attribute. These methods are safe and can be 
    /// used fairly safely and the caller is not needed to do full security reviews 
    /// even though no stack walk will be performed.
    /// </summary>
    [SuppressUnmanagedCodeSecurity]
    internal static class SafeNativeMethods
    {
        #region Methods

        /// <summary>
        /// obtains extended information about the version of the operating system that is currently running.
        /// </summary>
        /// <param name="ver">data structure that the function fills with operating system version information</param>
        /// <returns>true if the function succeeds, else false.</returns>
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [SuppressMessage("Microsoft.Usage", "CA2205:UseManagedEquivalentsOfWin32Api")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetVersionEx([In, Out] OSVERSIONINFO ver);

        /// <summary>
        /// obtains extended information about the version of the operating system that is currently running.
        /// </summary>
        /// <param name="ver">data structure that the function fills with operating system version information</param>
        /// <returns>true if the function succeeds, else false.</returns>
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [SuppressMessage("Microsoft.Usage", "CA2205:UseManagedEquivalentsOfWin32Api")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetVersionEx([In, Out] OSVERSIONINFOEX ver);

        /// <summary>
        /// determines whether the specified window is enabled for mouse and keyboard input. 
        /// </summary>
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool IsWindowEnabled(IntPtr hWnd);


  
        /// <summary>
        /// determines whether the specified window is minimized (iconic). 
        /// </summary>
        [DllImport("user32.dll")]
        public static extern bool IsIconic(IntPtr hWnd);


        /// <summary>
        /// sets the specified window's show state. 
        /// </summary>
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
 

        #endregion

        #region OSPlatformID
        public enum OSPlatformID
        {
            /// <summary>
            /// Windows 3.1
            /// </summary>
            VER_PLATFORM_WIN32s = 0,

            /// <summary>
            /// Windows 95, 98, ME
            /// </summary>
            VER_PLATFORM_WIN32_WINDOWS = 1,

            /// <summary>
            /// Windows NT, 2000, XP, 2003
            /// </summary>
            VER_PLATFORM_WIN32_NT = 2,

            /// <summary>
            /// Windows CE
            /// </summary>
            VER_PLATFORM_WINCE = 3
        }
        #endregion

        #region OSSuiteMask
        [Flags()]
        public enum OSSuiteMask : ushort
        {
            /// <summary>
            /// Terminal Services is installed.
            /// </summary>
            VER_SUITE_TERMINAL = 16,
            /// <summary>
            /// Microsoft Small Business Server is installed with the restrictive client license in force
            /// </summary>
            VER_SUITE_SMALLBUSINESS_RESTRICTED = 32,
            /// <summary>
            /// Microsoft Small Business Server was once installed on the system, but may have been upgraded to another version of Windows
            /// </summary>
            VER_SUITE_SMALLBUSINESS = 1,
            /// <summary>
            /// Terminal Services is installed, but only one interactive session is supported.
            /// </summary>
            VER_SUITE_SINGLEUSERTS = 256,
            /// <summary>
            /// Windows XP Home Edition is installed.
            /// </summary>
            VER_SUITE_PERSONAL = 512,
            /// <summary>
            /// Windows XP Embedded is installed
            /// </summary>
            VER_SUITE_EMBEDDEDNT = 64,
            /// <summary>
            /// Windows Server 2003, Enterprise Edition, Windows 2000 Advanced Server, or Windows NT 4.0 Enterprise Edition.
            /// </summary>
            VER_SUITE_ENTERPRISE = 2,
            /// <summary>
            /// Windows Server 2003, Datacenter Edition or Windows 2000 Datacenter Server is installed.
            /// </summary>
            VER_SUITE_DATACENTER = 128,
            /// <summary>
            /// Windows Server 2003, Web Edition is installed.
            /// </summary>
            VER_SUITE_BLADE = 1024,
            /// <summary>
            /// Microsoft BackOffice components are installed.
            /// </summary>
            VER_SUITE_BACKOFFICE = 4,
            /// <summary>
            /// Unknown Suite.
            /// </summary>
            VER_UNKNOWN = 0,
            /// <summary>
            /// Windows Storage Server 2003 R2 is installed.
            /// </summary>
            VER_SUITE_STORAGE_SERVER = 8192,
            /// <summary>
            /// Windows Server 2003, Compute Cluster Edition is installed.
            /// </summary>
            VER_SUITE_COMPUTE_SERVER = 16384,
        }
        #endregion

        #region OSProductType

        public enum OSProductType : byte
        {
            /// <summary>
            /// The system is a server
            /// </summary>
            VER_NT_SERVER = 3,

            /// <summary>
            /// The system is a domain controller
            /// </summary>
            VER_NT_DOMAIN_CONTROLLER = 2,

            /// <summary>
            /// Windows Professional
            /// </summary>
            VER_NT_WORKSTATION = 1,

            /// <summary>
            /// UnKnown
            /// </summary>
            VER_UNKNOWN = 0,
        }
        #endregion

        #region OSVERSIONINFO

        /// <summary>
        /// data structure contains operating system version information.
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public class OSVERSIONINFO
        {
            /// <summary>
            /// Size of this data structure, in bytes. Set this member to sizeof(OSVERSIONINFO) before calling the GetVersionEx function.
            /// </summary>
            public int OSVersionInfoSize;
            /// <summary>
            /// Major version number of the operating system.
            /// </summary>
            public int MajorVersion;
            /// <summary>
            /// Minor version number of the operating system.
            /// </summary>
            public int MinorVersion;
            /// <summary>
            /// Build number of the operating system.
            /// </summary>
            /// <remarks>Windows Me/98/95:  The low-order word contains the build number of the operating. The high-order word contains the major and minor version numbers.</remarks>
            public int BuildNumber;
            /// <summary>
            /// Operating system platform
            /// </summary>
            public OSPlatformID PlatformId;
            /// <summary>
            /// Pointer to a null-terminated string, such as "Service Pack 3", that indicates the latest Service Pack installed on the system. If no Service Pack has been installed, the string is empty.
            /// </summary>
            /// <remarks>Windows Me/98/95:  Pointer to a null-terminated string that indicates additional version information. For example, " C" indicates Windows 95 OSR2 and " A" indicates Windows 98 Second Edition.</remarks>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string CSDVersion;

            /// <summary>
            /// Initializes a new instance of the <see cref="T:OSVERSIONINFO"/> class.
            /// </summary>
            public OSVERSIONINFO()
            {
                this.OSVersionInfoSize = Marshal.SizeOf(this);
            } // OSVERSIONINFO
        } // class OSVERSIONINFO
        #endregion

        #region OSVERSIONINFOEX

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public class OSVERSIONINFOEX
        {
            /// <summary>
            /// Size of this data structure, in bytes. Set this member to sizeof(OSVERSIONINFO) before calling the GetVersionEx function.
            /// </summary>
            public int OSVersionInfoSize;
            /// <summary>
            /// Major version number of the operating system.
            /// </summary>
            public int MajorVersion;
            /// <summary>
            /// Minor version number of the operating system.
            /// </summary>
            public int MinorVersion;
            /// <summary>
            /// Build number of the operating system.
            /// </summary>
            /// <remarks>Windows Me/98/95:  The low-order word contains the build number of the operating. The high-order word contains the major and minor version numbers.</remarks>
            public int BuildNumber;
            /// <summary>
            /// Operating system platform
            /// </summary>
            public OSPlatformID PlatformId;
            /// <summary>
            /// Pointer to a null-terminated string, such as "Service Pack 3", that indicates the latest Service Pack installed on the system. If no Service Pack has been installed, the string is empty.
            /// </summary>
            /// <remarks>Windows Me/98/95:  Pointer to a null-terminated string that indicates additional version information. For example, " C" indicates Windows 95 OSR2 and " A" indicates Windows 98 Second Edition.</remarks>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string CSDVersion;

            /// <summary>
            /// Major version number of the latest Service Pack installed on the system
            /// </summary>
            public ushort ServicePackMajor;

            /// <summary>
            /// Minor version number of the latest Service Pack installed on the system
            /// </summary>
            public ushort ServicePackMinor;

            /// <summary>
            /// Bit flags that identify the product suites available on the system
            /// </summary>
            public OSSuiteMask SuiteMask;

            /// <summary>
            /// Additional information about the system
            /// </summary>
            public OSProductType ProductType;

            /// <summary>
            /// Reserved for future use
            /// </summary>
            public byte Reserved;

            /// <summary>
            /// Initializes a new instance of the <see cref="T:OSVERSIONINFO"/> class.
            /// </summary>
            public OSVERSIONINFOEX()
            {
                this.OSVersionInfoSize = Marshal.SizeOf(this);
            } // OSVERSIONINFOEX
        } // class OSVERSIONINFOEX
        #endregion


    }
}
