using System.Security.Permissions;
using System.Windows.Forms;
using SendKeysProxy = System.Windows.Forms.SendKeys;

namespace STO.Print.Utilities
{
    /// <summary>
    /// 提供访问键盘当前状态的属性，
    /// 如什么键当前按下，提供了一种方法，以发送击键到活动窗口。
    /// </summary>
    [HostProtection(SecurityAction.LinkDemand, Resources = HostProtectionResource.ExternalProcessMgmt)]
    public class KeyboardHelper
    {
        #region 属性
        /// <summary>获取一个布尔值，表示如果ALT键是向下。</summary>
        /// <returns>一个布尔值：如果ALT键，否则为false。</returns>
        public static bool AltKeyDown
        {
            get
            {
                return ((Control.ModifierKeys & Keys.Alt) > Keys.None);
            }
        }

        /// <summary>获取一个布尔值，指示，如果已开启CAPS LOCK键。 </summary>
        /// <returns>一个布尔值：如果已开启CAPS LOCK键，则为true，否则为false。</returns>
        /// <filterpriority>1</filterpriority>
        public static bool CapsLock
        {
            get
            {
                return ((UnsafeNativeMethods.GetKeyState(20) & 1) > 0);
            }
        }

        /// <summary>获取一个布尔值，表示如果CTRL键是向下。</summary>
        /// <returns>一个布尔值。真正如果CTRL键，否则为false。</returns>
        /// <filterpriority>1</filterpriority>
        public static bool CtrlKeyDown
        {
            get
            {
                return ((Control.ModifierKeys & Keys.Control) > Keys.None);
            }
        }

        /// <summary>获取一个布尔值，表示如果NUM LOCK键是。</summary>
        /// <returns>一个布尔值。true，如果数字锁定，否则为false。</returns>
        /// <filterpriority>1</filterpriority>
        public static bool NumLock
        {
            get
            {
                return ((UnsafeNativeMethods.GetKeyState(0x90) & 1) > 0);
            }
        }

        /// <summary>获取一个布尔值，指示是否SCROLL LOCK键是。 </summary>
        /// <returns>一个布尔值。True如果滚动锁被，否则为false。</returns>
        /// <filterpriority>1</filterpriority>
        public static bool ScrollLock
        {
            get
            {
                return ((UnsafeNativeMethods.GetKeyState(0x91) & 1) > 0);
            }
        }

        /// <summary>获取一个布尔值，表示如果SHIFT键是向下。</summary>
        /// <returns>一个布尔值。真正如果SHIFT键是向下，否则为false。</returns>
        /// <filterpriority>1</filterpriority>
        public static bool ShiftKeyDown
        {
            get
            {
                return ((Control.ModifierKeys & Keys.Shift) > Keys.None);
            }
        }


        #endregion

        #region Methods
        /// <summary>发送一个或多个击键到活动窗口，如果在键盘上输入。</summary>
        /// <param name="keys">一个字符串，定义发送键。</param>
        public static void SendKeys(string keys)
        {
            SendKeys(keys, false);
        }

        /// <summary>发送一个或多个击键到活动窗口，如果在键盘上输入。</summary>
        /// <param name="keys">一个字符串，定义发送键。</param>
        /// <param name="wait">可选的。一个布尔值，指定是否等待的应用程序继续之前得到处理的击键。默认为true。</param>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" /><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" /></PermissionSet>
        public static void SendKeys(string keys, bool wait)
        {
            if (wait)
            {
                SendKeysProxy.SendWait(keys);
            }
            else
            {
                SendKeysProxy.Send(keys);
            }
        }

        #endregion
    }
}
