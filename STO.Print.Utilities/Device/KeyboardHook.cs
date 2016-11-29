﻿using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace STO.Print.Utilities
{
    /// <summary>
    /// 全局键盘钩子。这可以用来在全球范围内捕捉键盘输入。
    /// </summary>
    public static class KeyboardHook
    {
        // 钩子的句柄（用于安装/卸载）.
        private static IntPtr hHook = IntPtr.Zero;

        //委托该点的过滤功能
        private static Hooks.HookProc hookproc = new Hooks.HookProc(Filter);

        /// <summary>
        /// 请检查如果任一控制修饰符是积极的。
        /// </summary>
        public static bool Control = false;
        /// <summary>
        /// 检查，看看是否要么移位修饰符是积极的。
        /// </summary>
        public static bool Shift = false;
        /// <summary>
        /// 检查看看或者ALT修饰符是积极的。
        /// </summary>
        public static bool Alt = false;
        /// <summary>
        ///检查，看看是否要么运修饰符是积极的。
        /// </summary>
        public static bool Win = false;

        /// <summary>
        /// 按键的函数原型。
        /// </summary>
        public delegate bool KeyPressed();

        /// <summary>
        /// 键处理和他们的回调
        /// </summary>
        private static System.Collections.Generic.Dictionary<Keys, KeyPressed> handledKeysDown = new System.Collections.Generic.Dictionary<Keys, KeyPressed>();
        private static System.Collections.Generic.Dictionary<Keys, KeyPressed> handledKeysUp = new System.Collections.Generic.Dictionary<Keys, KeyPressed>();

        /// <summary>
        /// 委托处理KeyDown事件。
        /// </summary>
        /// <param name="key">被按下的键。检查控制，按住Shift，Alt键，修饰符和赢。</param>
        /// <returns>如此，如果你要的关键，通过（被确认为应用程序），假的，如果你想ITTO被困（应用程序从来没有看到它）。</returns>
        public delegate bool KeyboardHookHandler(Keys key);

        /// <summary>
        /// 在此添加一个钩子处理程序委托，以激活热键。
        /// </summary>
        public static KeyboardHookHandler KeyDown;

        /// <summary>
        ///在挂机状态下保持跟踪。
        /// </summary>
        private static bool Enabled;

        /// <summary>
        /// 启动键盘钩子。
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
                        hHook = Hooks.SetWindowsHookEx((int)Hooks.HookType.WH_KEYBOARD_LL, hookproc, Hooks.GetModuleHandle(curModule.ModuleName), 0);
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
        /// 禁用键盘挂钩。
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
                if (wParam == (IntPtr)Hooks.WM_KEYDOWN
                    || wParam == (IntPtr)Hooks.WM_SYSKEYDOWN)
                {
                    int vkCode = Marshal.ReadInt32(lParam);
                    if ((Keys)vkCode == Keys.LControlKey ||
                        (Keys)vkCode == Keys.RControlKey)
                        Control = true;
                    else if ((Keys)vkCode == Keys.LShiftKey ||
                        (Keys)vkCode == Keys.RShiftKey)
                        Shift = true;
                    else if ((Keys)vkCode == Keys.RMenu ||
                        (Keys)vkCode == Keys.LMenu)
                        Alt = true;
                    else if ((Keys)vkCode == Keys.RWin ||
                        (Keys)vkCode == Keys.LWin)
                        Win = true;
                    else
                        result = OnKeyDown((Keys)vkCode);
                }
                else if (wParam == (IntPtr)Hooks.WM_KEYUP
                    || wParam == (IntPtr)Hooks.WM_SYSKEYUP)
                {
                    int vkCode = Marshal.ReadInt32(lParam);
                    if ((Keys)vkCode == Keys.LControlKey ||
                        (Keys)vkCode == Keys.RControlKey)
                        Control = false;
                    else if ((Keys)vkCode == Keys.LShiftKey ||
                        (Keys)vkCode == Keys.RShiftKey)
                        Shift = false;
                    else if ((Keys)vkCode == Keys.RMenu ||
                        (Keys)vkCode == Keys.LMenu)
                        Alt = false;
                    else if ((Keys)vkCode == Keys.RWin ||
                        (Keys)vkCode == Keys.LWin)
                        Win = false;
                    else
                        result = OnKeyUp((Keys)vkCode);
                }
            }

            return result ? Hooks.CallNextHookEx(hHook, nCode, wParam, lParam) : new IntPtr(1);
        }

        /// <summary>
        /// 增加了一个关键的钩。
        /// </summary>
        /// <param name="key">要添加的关键。</param>
        /// <param name="callback">被称为该功能时，按下此键。</param>
        public static bool AddKeyDown(Keys key, KeyPressed callback)
        {
            KeyDown = null;
            if (!handledKeysDown.ContainsKey(key))
            {
                handledKeysDown.Add(key, callback);
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// 增加了一个关键的钩。
        /// </summary>
        /// <param name="key">要添加的关键。</param>
        /// <param name="callback">被称为该功能时，按下此键。</param>
        public static bool AddKeyUp(Keys key, KeyPressed callback)
        {
            if (!handledKeysUp.ContainsKey(key))
            {
                handledKeysUp.Add(key, callback);
                return true;
            }
            else
                return false;
        }

        /// <summary>
        ///新增的KeyDown包装
        /// </summary>
        /// <param name="key">要添加的关键。</param>
        /// <param name="callback">被称为该功能时，按下此键。</param>
        public static bool Add(Keys key, KeyPressed callback)
        {
            return AddKeyDown(key, callback);
        }

        /// <summary>
        /// 从钩取出一个关键。
        /// </summary>
        /// <param name="key">被删除的关键。</param>
        public static bool RemoveDown(Keys key)
        {
            return handledKeysDown.Remove(key);
        }

        /// <summary>
        /// 将重点从钩起来。
        /// </summary>
        /// <param name="key">被删除的关键。</param>
        public static bool RemoveUp(Keys key)
        {
            return handledKeysUp.Remove(key);
        }

        /// <summary>
        /// 删除一个挂机键。
        /// </summary>
        /// <param name="key">被删除的关键。</param>
        public static bool Remove(Keys key)
        {
            return RemoveDown(key);
        }

        private static bool OnKeyDown(Keys key)
        {
            if (KeyDown != null)
                return KeyDown(key);
            if (handledKeysDown.ContainsKey(key))
                return handledKeysDown[key]();
            else
                return true;
        }

        private static bool OnKeyUp(Keys key)
        {
            if (handledKeysUp.ContainsKey(key))
                return handledKeysUp[key]();
            else
                return true;
        }

        /// <summary>
        /// 返回一个给定的键根据当前修饰符的字符串表示形式。
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string KeyToString(Keys key)
        {
            return (KeyboardHook.Control ? "Ctrl + " : "") +
                            (KeyboardHook.Alt ? "Alt + " : "") +
                            (KeyboardHook.Shift ? "Shift + " : "") +
                            (KeyboardHook.Win ? "Win + " : "") +
                            key.ToString();
        }
    }
}
