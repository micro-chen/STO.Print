//-------------------------------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2015 , STO , Ltd .
//-------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace STO.Print.Utilities
{
    using DotNet.Utilities;
    using Microsoft.Win32;
    using System.IO;

    /// <summary>
    /// QQ常用帮助类
    ///
    /// 修改纪录
    ///
    ///	   2014-08-30  版本：1.0 YangHengLian 创建主键，注意命名空间的排序。
    ///       2014-08-30  可以支持QQ聊天（调用qq）的dll，支持开机启动qq
    ///       2014-10-09  添加加入qq群的方法
    ///       2015-05-26  添加获取本机qq客户端登陆的账号实体信息
    /// 
    /// 版本：1.0
    ///
    /// <author>
    ///		<name>YangHengLian</name>
    ///		<date>2014-08-30</date>
    /// </author>
    /// </summary>
    public class QQHelper
    {
        private static WebBrowser _mWeb;
        private static QClientKey[] _mQKey;
        private static int _mNIndex;

        #region public static void ChatQQ(string qqNumber, bool isBoot = false) 打开qq聊天窗体，通过C/S打开
        /// <summary> 
        /// 打开qq聊天窗体，进行聊天，C/S打开
        /// </summary>
        /// <param name="qqNumber">qq号码</param>
        /// <param name="isBoot">开机启动</param>
        public static bool ChatQQ(string qqNumber = "766096823", bool isBoot = false)
        {
            try
            {
                string arg = string.Format("tencent://message/?uin={0}&Site=www.baidu.com&Menu=yes", qqNumber);//请求参数字符串
                var qqFilePath = GetQQInstallPath();
                // 启动QQ自带程序，QQ必须安装
                Process.Start(qqFilePath, arg);
                SetRegistryIsStart(isBoot);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion

        #region public static string GetQQInstallPath() 从注册表中获取qq的安装路径
        /// <summary>
        /// 从注册表中获取qq的安装路径
        /// </summary>
        /// <returns></returns>
        public static string GetQQInstallPath()
        {
            var openKey = @"SOFTWARE\Wow6432Node\Tencent\QQ2009";//64位注册表
            if (IntPtr.Size == 4)
            {
                openKey = @"SOFTWARE\Tencent\QQ2009";//32位注册表路径
            }
            RegistryKey appPath = Registry.LocalMachine.OpenSubKey(openKey);
            // 注册表读取到QQ安装路径
            if (appPath != null)
            {
                // QQ安装具体路径
                string path = appPath.GetValue("Install") + @"\Bin\Timwp.exe";
                if (File.Exists(path))
                {
                    return path;
                }
                openKey = @"SOFTWARE\Wow6432Node\Tencent\QQLite";//64位注册表
                if (IntPtr.Size == 4)
                {
                    openKey = @"SOFTWARE\Tencent\QQLite";//32位注册表路径
                }
                appPath = Registry.LocalMachine.OpenSubKey(openKey);
                if (appPath != null)
                {
                    path = appPath.GetValue("Install") + @"\Bin\Timwp.exe";
                    if (File.Exists(path))
                    {
                        return path;
                    }
                }
                return null;
            }
            return null;
        }
        #endregion

        #region public static void ChatQQForWeb(string qqNumber) 网页方法打开QQ聊天窗体
        /// <summary>
        /// 网页方法打开QQ聊天窗体
        /// </summary>
        /// <param name="qqNumber">qq号码</param>
        public static bool ChatQQForWeb(string qqNumber)
        {
            try
            {
                var openUrl = string.Format("http://wpa.qq.com/msgrd?v=3&uin={0}&site=qq&menu=yes", qqNumber);
                Process.Start(openUrl);
                return true;
            }
            catch (Exception ex)
            {
                LogUtil.WriteException(ex);
                return false;
            }
        }
        #endregion

        #region public static void SetRegistryIsStart(bool isStart) 当前运行程序是否开机启动
        /// <summary>
        /// 当前运行程序是否开机启动
        /// </summary>
        /// <param name="isStart"></param>
        public static void SetRegistryIsStart(bool isStart)
        {
            //获取的系统位数是否是64位，是返回true否返回false
            bool computerbyte = Environment.Is64BitOperatingSystem;
            string shortFileName = Application.ProductName;
            //if (string.IsNullOrEmpty(strAssName))
            //{
            string strAssName = Application.StartupPath + @"\" + Application.ProductName + @".exe";
            //}else
            //{
            //    shortFileName = Path.GetFileName(strAssName);
            //}
            RegistryKey baseNode = null;
            const string regeditPath = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run";
            //初始化注册表类
            baseNode = computerbyte ? RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64) : Registry.LocalMachine;
            if (isStart)
            {
                //调用判断写入注册表的方法
                WriteRegedit(regeditPath, baseNode, shortFileName, strAssName);
            }
            else
            {
                try
                {
                    var openSubKey = baseNode.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
                    if (openSubKey != null)
                        openSubKey.DeleteValue(Application.ProductName, false);
                }
                catch (Exception ex)
                {
                    LogUtil.WriteException(ex);
                }
            }
        }
        #endregion

        #region public static void WriteRegedit(string regeditPath, RegistryKey baseNode, string key, string value) 写入注册表的方法
        /// <summary>
        /// 写入注册表的方法
        /// </summary>
        /// <param name="regeditPath">注册表的路径（SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run）</param>
        /// <param name="baseNode">要改写的表的根节点</param>
        /// <param name="key">键名称（程序名称）</param>
        /// <param name="value">值（程序执行exe文件物理路径）</param>
        public static void WriteRegedit(string regeditPath, RegistryKey baseNode, string key, string value)
        {
            RegistryKey registryKey = baseNode.OpenSubKey(regeditPath, true);
            if (registryKey == null)
                registryKey = registryKey.CreateSubKey(regeditPath, RegistryKeyPermissionCheck.ReadWriteSubTree);
            //在注册表中写入程序名为StupName值为REG_SZ类型的数据
            if (registryKey != null)
            {
                registryKey.SetValue(key, value, RegistryValueKind.String);
                registryKey.Close();
            }
        }
        #endregion

        #region public static void JoinQQGroup() 加入申通打印专家群
        /// <summary>
        /// 加入申通打印专家
        /// <remarks>这里是通过群推广这个功能得到url：http://qun.qq.com/join.html 2014-10-9 16:14:07</remarks>
        /// </summary>
        public static void JoinQQGroup()
        {
            try
            {
                // ToolHelper.OpenBrowserUrl("http://shang.qq.com/wpa/qunwpa?idkey=792038300f86cdacdf908e345104fec4bc85567ffca9fee7dd0fcb8e436bad76");
                ToolHelper.OpenBrowserUrl("http://shang.qq.com/wpa/qunwpa?idkey=041b3dbc84768d3e102cd83b9a8adb0631c46f19a077505dc33f163a2249b6ce");
            }
            catch (Exception ex)
            {
                LogUtil.WriteException(ex);
            }
        }
        #endregion

        #region public static QClientKey[] GetQClientKey() 获取qq客户端登陆后的qq实体
        /// <summary>
        /// 获取qq客户端登陆后的qq实体
        /// </summary>
        /// <returns></returns>
        public static QClientKey[] GetQClientKey()
        {
            try
            {
                if (_mWeb == null)
                {
                    _mWeb = new WebBrowser { ScriptErrorsSuppressed = true };
                    _mWeb.Navigating += m_web_Navigating;
                }
                _mWeb.Navigate("http://xui.ptlogin2.qq.com/cgi-bin/qlogin");
                while (_mWeb.ReadyState != WebBrowserReadyState.Complete)
                {
                    Application.DoEvents();
                }
                HtmlElementCollection elements = _mWeb.Document.GetElementsByTagName("label");
                List<string> lstQ = new List<string>();
                foreach (HtmlElement element in elements)
                {
                    if (Regex.IsMatch(element.InnerText, @"\(\d+\)"))
                        lstQ.Add(element.InnerText);
                }
                if (lstQ.Count == 0) return null;
                _mQKey = new QClientKey[lstQ.Count];
                _mNIndex = 0;
                for (int len = lstQ.Count; _mNIndex < len; _mNIndex++)
                {
                    _mQKey[_mNIndex] = new QClientKey();
                    //获取qq和名称
                    Regex regex = new Regex("(.+?) \\((.+?)\\)");
                    MatchCollection match = regex.Matches(lstQ[_mNIndex]);
                    if (match.Count > 0)
                    {
                        _mQKey[_mNIndex].NickName = match[0].Groups[1].Value;
                        _mQKey[_mNIndex].QQ = match[0].Groups[2].Value;
                    }
                    elements = _mWeb.Document.GetElementsByTagName("label");
                    foreach (HtmlElement element in elements)
                    {
                        if (element.InnerText != lstQ[_mNIndex]) continue;
                        element.InvokeMember("click");
                        _mWeb.Document.GetElementById("loginbtn").InvokeMember("click");
                        while (_mWeb.ReadyState != WebBrowserReadyState.Complete)
                        {
                            Application.DoEvents();
                        }
                        break;
                    }
                    if (_mNIndex + 1 >= len) break;
                    _mWeb.Navigate("http://xui.ptlogin2.qq.com/cgi-bin/qlogin");
                    while (_mWeb.ReadyState != WebBrowserReadyState.Complete)
                    {
                        Application.DoEvents();
                    }
                }
                return _mQKey;
            }
            catch (Exception exception)
            {
                LogUtil.WriteException(exception);
            }
            return null;
        }

        /// <summary>
        /// 请求网页获取qq本地登陆的key值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void m_web_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            string url = e.Url.ToString();
            if (url.IndexOf("clientkey") != -1)
            {
                // qq账号
                string clientuin = Regex.Match(url, @"(?<=(clientuin=)).*?(?=&)").Value;
                // 客户端的key
                string clientkey = Regex.Match(url, @"(?<=(clientkey=)).*?(?=&)").Value;
                _mQKey[_mNIndex].ClientKey = clientkey;
            }
        }
        #endregion

        /// <summary>
        /// 加入申通打印专家2
        /// </summary>
        public static void JoinQQGroup2()
        {
            try
            {
                ToolHelper.OpenBrowserUrl("http://shang.qq.com/wpa/qunwpa?idkey=406a2c0a7faaa5297e416e1974e238a77c07e71a49459e67fa393f60c8bf3cd1");
            }
            catch (Exception ex)
            {
                LogUtil.WriteException(ex);
            }
        }

        /// <summary>
        /// 加入申通打印专家3
        /// </summary>
        public static void JoinQQGroup3()
        {
            try
            {
                ToolHelper.OpenBrowserUrl("http://shang.qq.com/wpa/qunwpa?idkey=9adfec82aa5615561ab6e85d7a82531d8808afac6a796ac7b20ea72fc848c366");
            }
            catch (Exception ex)
            {
                LogUtil.WriteException(ex);
            }
        }
    }

    /// <summary>
    /// qq客户端登陆后实体
    /// </summary>
    public class QClientKey
    {
        private string qq;

        /// <summary>
        /// qq号码
        /// </summary>
        public string QQ
        {
            get { return qq; }
            set { qq = value; }
        }

        private string nickName;

        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName
        {
            get { return nickName; }
            set { nickName = value; }
        }

        /// <summary>
        /// 客户端登陆后的key
        /// </summary>
        private string clientKey;

        public string ClientKey
        {
            get { return clientKey; }
            set { clientKey = value; }
        }
    }
}
