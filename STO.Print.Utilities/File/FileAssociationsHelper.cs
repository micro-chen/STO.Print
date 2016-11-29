using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;

namespace STO.Print.Utilities
{
    /// <summary>
    /// 注册文件关联的辅助类
    /// </summary>
    public class FileAssociationsHelper
    {
        private static RegistryKey classesRoot;
        private static void Process(string[] args)
        {
            if (args.Length < 6)
            {
                string error = ("Usage: <ProgId> <Register in HKCU: true|false> <AppId> <OpenWithSwitch> <Unregister: true|false> <Ext1> [Ext2 [Ext3] ...]");
                throw new ArgumentException(error);
            }
            try
            {
                string progId = args[0];
                bool registerInHKCU = bool.Parse(args[1]);
                string appId = args[2];
                string openWith = args[3];
                bool unregister = bool.Parse(args[4]);

                List<string> argList = new List<string>();
                for (int i = 0; i < args.Length; i++)
                {
                    if (i < 5) continue;

                    argList.Add(args[i]);
                }
                string[] associationsToRegister = argList.ToArray();

                if (registerInHKCU)
                {
                    classesRoot = Registry.CurrentUser.OpenSubKey(@"Software\Classes");
                }
                else
                {
                    classesRoot = Registry.ClassesRoot;
                }

                //首先，注销：
                Array.ForEach(associationsToRegister,
                              assoc => UnregisterFileAssociation(progId, assoc));
                UnregisterProgId(progId);

                if (!unregister)
                {
                    RegisterProgId(progId, appId, openWith);
                    Array.ForEach(associationsToRegister,
                        assoc => RegisterFileAssociation(progId, assoc));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.ReadLine();
            }
        }

        private static void RegisterProgId(string progId, string appId, string openWith)
        {
            RegistryKey progIdKey = classesRoot.CreateSubKey(progId);
            progIdKey.SetValue("FriendlyTypeName", "@shell32.dll,-8975");
            progIdKey.SetValue("DefaultIcon", "@shell32.dll,-47");
            progIdKey.SetValue("CurVer", progId);
            progIdKey.SetValue("AppUserModelID", appId);
            RegistryKey shell = progIdKey.CreateSubKey("shell");
            shell.SetValue(String.Empty, "Open");
            shell = shell.CreateSubKey("Open");
            shell = shell.CreateSubKey("Command");
            shell.SetValue(String.Empty, openWith);

            shell.Close();
            progIdKey.Close();
        }
        private static void UnregisterProgId(string progId)
        {
            try
            {
                classesRoot.DeleteSubKeyTree(progId);
            }
            catch { }
        }
        private static void RegisterFileAssociation(string progId, string extension)
        {
            RegistryKey openWithKey = classesRoot.CreateSubKey(
                Path.Combine(extension, "OpenWithProgIds"));
            openWithKey.SetValue(progId, String.Empty);
            openWithKey.Close();
        }
        private static void UnregisterFileAssociation(string progId, string extension)
        {
            try
            {
                RegistryKey openWithKey = classesRoot.CreateSubKey(
                    Path.Combine(extension, "OpenWithProgIds"));
                openWithKey.DeleteValue(progId);
                openWithKey.Close();
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error while unregistering file association: " + e.Message);
            }
        }

        private static void InternalRegisterFileAssociations(
            bool unregister, string progId, bool registerInHKCU,
            string appId, string openWith, string[] extensions)
        {
            string Arguments = string.Format("{0} {1} {2} \"{3}\" {4} {5}",
                    progId, // 0
                    registerInHKCU, // 1 
                    appId, // 2
                    openWith,
                    unregister,
                    string.Join(" ", extensions));
            try
            {
                Process(Arguments.Split(' '));
            }
            catch (Win32Exception e)
            {
                if (e.NativeErrorCode == 1223) // 1223:用户操作被取消。
                {
                    LogHelper.Info("该操作已经被用户取消。");
                }
            }
        }

        /// <summary>
        /// 为应用程序的注册表文件关联。
        /// </summary>
        /// <param name="progId">应用程序的ProgID。</param>
        /// <param name="registerInHKCU">Whether to register the association per-user (in HKCU).  The only supported value at this time is <b>false</b>.</param>
        /// <param name="appId">The application's app-id.</param>
        /// <param name="openWith">The command and arguments to be used
        /// when opening a shortcut to a document.</param>
        /// <param name="extensions">The extensions to register.</param>
        public static void RegisterFileAssociations(string progId,
            bool registerInHKCU, string appId, string openWith,
            params string[] extensions)
        {
            InternalRegisterFileAssociations(
                false, progId, registerInHKCU, appId, openWith, extensions);
        }

        /// <summary>
        /// Unregisters file associations for an application.
        /// </summary>
        /// <param name="progId">The application's ProgID.</param>
        /// <param name="registerInHKCU">Whether to register the
        /// association per-user (in HKCU).  The only supported value
        /// at this time is <b>false</b>.</param>
        /// <param name="appId">The application's app-id.</param>
        /// <param name="openWith">The command and arguments to be used
        /// when opening a shortcut to a document.</param>
        /// <param name="extensions">The extensions to unregister.</param>
        public static void UnregisterFileAssociations(string progId,
            bool registerInHKCU, string appId, string openWith,
            params string[] extensions)
        {
            InternalRegisterFileAssociations(
                true, progId, registerInHKCU, appId, openWith, extensions);
        }
    }
}
