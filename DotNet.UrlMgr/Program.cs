using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Win32;

namespace DotNet.UrlMgr
{
    public class Program
    {
        static void Main(string[] args)
        {
            Regist();
        }

        /// <summary>
        /// 这是注册的代码
        /// http://www.cnblogs.com/qingyuan/p/5895087.html
        /// </summary>
        static void Regist()
        {
            try
            {
                Console.WriteLine("正在查找已经注册的程序.....");
                RegistryKey key = Registry.ClassesRoot;
                key.DeleteSubKeyTree(@"gitwms");
                Console.WriteLine("已经清除注册程序.....");
            }
            catch (Exception e)
            {
                Console.WriteLine("未找到注册的程序...");
            }

            /*===============================================*/
            Console.WriteLine("开始注册程序....");
            RegistryKey regWrite = Registry.ClassesRoot.CreateSubKey("gitwms");
            if (regWrite != null)
            {
                regWrite.SetValue("", "URL:自定义协议");
                regWrite.SetValue("URL Protocol", "URL Protocol");
                regWrite.Close();
            }


            regWrite = Registry.ClassesRoot.CreateSubKey(@"gitwms\shell");
            if (regWrite != null) regWrite.Close();

            regWrite = Registry.ClassesRoot.CreateSubKey(@"gitwms\shell\open");
            if (regWrite != null) regWrite.Close();

            regWrite = Registry.ClassesRoot.OpenSubKey(@"gitwms\shell\open", true);
            if (regWrite != null)
            {
                RegistryKey aimdir = regWrite.CreateSubKey("command");

                string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                string appPath = Path.Combine(baseDirectory, "STO.Print.exe");

                if (aimdir != null)
                {
                    aimdir.SetValue(@"", "\"" + appPath + "\" \" %1\"");

                    regWrite.Close();
                    aimdir.Close();
                }
            }
        }
    }
}
