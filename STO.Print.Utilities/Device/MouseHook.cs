using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace STO.Print.Utilities
{
    /// <summary>
    /// 全局鼠标钩子。这可以用来在全球范围内捕获鼠标输入。
    /// </summary>
    public static class MouseHook
    {
        //钩子的句柄（用于安装/卸载）。
        private static IntPtr hHook = IntPtr.Zero;

        //委托该点的过滤功能
        private static Hooks.HookProc hookproc = new Hooks.HookProc(Filter);

        /// <summary>
        /// 委托处理鼠标输入。
        /// </summary>
        /// <param name="button">鼠标按钮被按下。</param>
        /// <returns>如此，如果你想通过（被认可的应用程序），假的关键，如果你想被困（应用程序从来没有看到它）。</returns>
        public delegate bool MouseButtonHandler(MouseButtons button);

        public delegate bool MouseMoveHandler(Point point);

        public delegate bool MouseScrollHandler(int delta);

        public static MouseButtonHandler ButtonDown;
        public static MouseButtonHandler ButtonUp;
        public static MouseMoveHandler Moved;
        public static MouseScrollHandler Scrolled;

        /// <summary>
        /// 在挂机状态下保持跟踪。
        /// </summary>
        private static bool Enabled;

        /// <summary>
        /// 启动鼠标钩子。
        /// </summary>
        /// <returns>True如果没有例外。</returns>
        public static bool Enable()
        {
            if (Enabled == false)
            {
                try
                {
                    using (Process curProcess = Process.GetCurrentProcess())
                    using (ProcessModule curModule = curProcess.MainModule)
                        hHook = Hooks.SetWindowsHookEx((int)Hooks.HookType.WH_MOUSE_LL, hookproc, Hooks.GetModuleHandle(curModule.ModuleName), 0);
                    Enabled = true;
                    return true;
                }
                catch
                {
                    Enabled = false;
                    return false;
                }
            }
            else
                return false;
        }

        /// <summary>
        /// 禁用老鼠挂钩。
        /// </summary>
        /// <returns>True如果禁用正确。</returns>
        public static bool Disable()
        {
            if (Enabled == true)
            {
                try
                {
                    Hooks.UnhookWindowsHookEx(hHook);
                    Enabled = false;
                    return true;
                }
                catch
                {
                    Enabled = true;
                    return false;
                }
            }
            else
                return false;
        }

        private static IntPtr Filter(int nCode, IntPtr wParam, IntPtr lParam)
        {
            bool result = true;

            if (nCode >= 0)
            {
                Hooks.MouseHookStruct info = (Hooks.MouseHookStruct)Marshal.PtrToStructure(lParam, typeof(Hooks.MouseHookStruct));
                switch ((int)wParam)
                {
                    case Hooks.WM_LBUTTONDOWN:
                        result = OnMouseDown(MouseButtons.Left);
                        break;
                    case Hooks.WM_LBUTTONUP:
                        result = OnMouseUp(MouseButtons.Left);
                        break;
                    case Hooks.WM_RBUTTONDOWN:
                        result = OnMouseDown(MouseButtons.Right);
                        break;
                    case Hooks.WM_RBUTTONUP:
                        result = OnMouseUp(MouseButtons.Right);
                        break;
                    case Hooks.WM_MBUTTONDOWN:
                        result = OnMouseDown(MouseButtons.Middle);
                        break;
                    case Hooks.WM_MBUTTONUP:
                        result = OnMouseUp(MouseButtons.Middle);
                        break;
                    case Hooks.WM_XBUTTONDOWN:
                        if (info.Data >> 16 == Hooks.XBUTTON1)
                            result = OnMouseDown(MouseButtons.XButton1);
                        else if (info.Data >> 16 == Hooks.XBUTTON2)
                            result = OnMouseDown(MouseButtons.XButton2);
                        break;
                    case Hooks.WM_XBUTTONUP:
                        if (info.Data >> 16 == Hooks.XBUTTON1)
                            result = OnMouseUp(MouseButtons.XButton1);
                        else if (info.Data >> 16 == Hooks.XBUTTON2)
                            result = OnMouseUp(MouseButtons.XButton2);
                        break;
                    case Hooks.WM_MOUSEMOVE:
                        result = OnMouseMove(new Point(info.Point.X, info.Point.Y));
                        break;
                    case Hooks.WM_MOUSEWHEEL:
                        result = OnMouseWheel((info.Data >> 16) & 0xffff);
                        break;
                }
            }

            return result ? Hooks.CallNextHookEx(hHook, nCode, wParam, lParam) : new IntPtr(1);
        }

        private static bool OnMouseDown(MouseButtons button)
        {
            if (ButtonDown != null)
                return ButtonDown(button);
            else
                return true;
        }

        private static bool OnMouseUp(MouseButtons button)
        {
            if (ButtonUp != null)
                return ButtonUp(button);
            else
                return true;
        }

        private static bool OnMouseMove(Point point)
        {
            if (Moved != null)
                return Moved(point);
            else
                return true;
        }

        private static bool OnMouseWheel(int delta)
        {
            if (Scrolled != null)
                return Scrolled(delta);
            else
                return true;
        }
    }

    public static class Hooks
    {
        #region Interop

        internal const int WM_SIZE = 0x5;
        internal const int WM_KEYDOWN = 0x0100;
        internal const int WM_KEYUP = 0x0101;
        internal const int WM_CHAR = 0x0102;
        internal const int WM_SYSKEYDOWN = 0x0104;
        internal const int WM_SYSKEYUP = 0x0105;

        //这是SetWindowsHookEx函数导入。
        //使用此功能来安装一个特定线程钩子。
        [DllImport("user32.dll", CharSet = CharSet.Auto,
         CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr SetWindowsHookEx(int idHook, HookProc lpfn,
        IntPtr hInstance, int threadId);

        //这是导入的UnhookWindowsHookEx函数。
        //调用这个函数来卸载钩子。
        [DllImport("user32.dll", CharSet = CharSet.Auto,
         CallingConvention = CallingConvention.StdCall)]
        internal static extern bool UnhookWindowsHookEx(IntPtr idHook);

        //这是导入的CallNextHookEx函数。
        //使用此功能来传递链中的下一个钩子程序钩子信息。
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode,
            IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern IntPtr GetModuleHandle(string lpModuleName);

        #endregion

        // 作为一个处理（过滤器）的方法类型
        public delegate IntPtr HookProc(int nCode, IntPtr wParam, IntPtr lParam);

        // 不同类型的钩子。
        internal enum HookType : int
        {
            WH_KEYBOARD = 2,
            WH_GETMESSAGE = 3,
            WH_CALLWNDPROC = 4,
            WH_CBT = 5,
            WH_SYSMSGFILTER = 6,
            WH_MOUSE = 7,
            WH_HARDWARE = 8,
            WH_DEBUG = 9,
            WH_SHELL = 10,
            WH_FOREGROUNDIDLE = 11,
            WH_CALLWNDPROCRET = 12,
            WH_KEYBOARD_LL = 13,
            WH_MOUSE_LL = 14
        }

        //鼠标的东西
        internal const int WM_MOUSEMOVE = 0x200;
        internal const int WM_MOUSEWHEEL = 0x020A;
        internal const int WM_LBUTTONDOWN = 0x201;
        internal const int WM_RBUTTONDOWN = 0x204;
        internal const int WM_MBUTTONDOWN = 0x207;
        internal const int WM_XBUTTONDOWN = 0x20B;
        internal const int WM_LBUTTONUP = 0x202;
        internal const int WM_RBUTTONUP = 0x205;
        internal const int WM_MBUTTONUP = 0x208;
        internal const int WM_XBUTTONUP = 0x20C;

        internal const int XBUTTON1 = 0x1;
        internal const int XBUTTON2 = 0x2;

        [StructLayout(LayoutKind.Sequential)]
        internal struct MouseHookStruct
        {
            public Point Point;
            public int Data;
            public int Flags;
            public int Time;
            public IntPtr ExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct Point
        {
            public int X;
            public int Y;
        }
    }
}
