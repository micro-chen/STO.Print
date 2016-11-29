//-------------------------------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2015 , STO , Ltd .
//-------------------------------------------------------------------------------------

using System;
using System.IO;
using System.Windows.Forms;

namespace STO.Print.Utilities
{
    /// <summary>
    /// 更新检查类
    ///
    /// 修改纪录
    ///
    ///		  2015-07-29  版本：1.0 YangHengLian 创建主键，注意命名空间的排序。
    ///       2015-11-30  加上异常处理，点击几次发现异常情况了
    /// 
    /// 版本：1.0
    ///
    /// <author>
    ///		<name>YangHengLian</name>
    /// 	<date>2015-07-29</date>
    /// </author>
    /// </summary>
    public class UpdateHelper
    {
        /// <summary>
        /// 检查更新 1:自动更新，0：手动更新
        /// 拷贝自动更新文件</summary>
        /// <param name="args">1:自动更新，0：手动更新</param>
        public static void CheckUpdate(string args)
        {
            /*
            * 1.将自动更新组件下载到更新文件临时目录下
            * 2.如果是压缩文件则进行解压缩操作
            * 3.删除所有的STO.Print.Update进程防止无法复制
            * 4.删除安装目录下的STO.Print.Update文件
            * 5.从更新临时目录下拷贝自动更新文件到运行目录下
            * 6.删除更新临时目录
            */

            // 关闭其他的打印专家进程
            ProcessHelper.KillOther("STO.Print");
            // 关闭所有的自动更新进程
            ProcessHelper.Kill("STO.Print.Update");
            try
            {
                // 下载文件临时目录
                string tempFolderPath = Application.StartupPath + "\\TemporaryFolder";
                string tempUpdateExeFilePath = tempFolderPath + "\\STO.Update.exe";
                // 升级exe文件的全路径
                string oldUpdateExeFilePath = Application.StartupPath + "\\STO.Update.exe";
                // 老版本的自动更新(STO.Print.Update)如果存在，则删除掉
                if (File.Exists(oldUpdateExeFilePath))
                {
                    File.Delete(oldUpdateExeFilePath);
                }
                // 判断临时目录下的自动更新组件是否存在
                if (Directory.Exists(tempFolderPath) && File.Exists(tempUpdateExeFilePath))
                {
                    // 从临时目录下拷贝自动升级到
                    File.Copy(tempUpdateExeFilePath, oldUpdateExeFilePath);
                    // 删除安装目录下的自动更新组件
                    if (File.Exists(tempUpdateExeFilePath))
                    {
                        File.Delete(tempUpdateExeFilePath);
                    }
                    // 删除临时目录
                    Directory.Delete(tempFolderPath, true);
                }
                ProcessHelper.Start("STO.Print.Update", args);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 手动升级
        /// </summary>
        public static void ManualUpdate()
        {
            CheckUpdate("0");
        }

        /// <summary>
        /// 强制升级
        /// </summary>
        public static void ForcedUpdate()
        {
            CheckUpdate("1");
        }
    }
}
