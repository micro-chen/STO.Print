//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2015 , STO TECH, Ltd. 
//-----------------------------------------------------------------

using System;

namespace STO.Print.Synchronous
{
    using DotNet.Business;
    using DotNet.Model;
    using DotNet.Utilities;

    /// <summary>
    /// Synchronous.cs
    /// 数据同步程序
    ///		
    /// 修改记录
    /// 
    ///     2015.03.23 版本：1.0 YangHengLian  添加项目详细资料功能页面编写。
    ///		
    /// <author>
    ///		<name>YangHengLian</name>
    ///		<date>2015.03.23</date>
    /// </author> 
    /// </summary>    
    public partial class Synchronous
    {
        /// <summary>
        /// 登录前运行的方法
        /// </summary>
        public static void BeforeLogOn()
        {
            // 0：若文件不存在，就退出数据同步，不进行数据同步
            string sqLiteDb = System.Windows.Forms.Application.StartupPath + @"\DataBase\STO.Bill.db";
            if (!System.IO.File.Exists(sqLiteDb))
            {
                return;
            }
            try
            {
                // 01：打开业务数据库
                string dbConnection = "Data Source={StartupPath}/DataBase/STO.Bill.db;Pooling=true;FailIfMissing=false;Password=ZTO20149988";
                dbConnection = dbConnection.Replace("{StartupPath}", System.Windows.Forms.Application.StartupPath);
                IDbHelper dbHelper = DbHelperFactory.GetHelper(CurrentDbType.SQLite, dbConnection);
                // 02: 先保存个同步时间标志，什么时间成功同步过本地数据库？这样不用每次都同步所有的数据，只同步那个时间之后的数据就可以了。
                BaseParameterManager parameterManager = new BaseParameterManager(dbHelper, BaseSystemInfo.UserInfo, BaseParameterEntity.TableName);
                // 03：用参数的方式读取同步时间
                // 04：检查数据库里的版本号
                var synchronous = parameterManager.GetParameter("Bill", "DBVersion", "Synchronous");
                if (string.IsNullOrEmpty(synchronous))
                {
                    synchronous = "5.2015.7.15";
                    parameterManager.SetParameter("Bill", "DBVersion", "Synchronous", synchronous);
                }
                var versionEntity = new Version(synchronous);
                if (versionEntity < new Version("5.2015.08.14"))
                {
                    Upgrade20150814();
                }
                if (versionEntity < new Version("5.2015.08.21"))
                {
                    Upgrade20150821();
                }
                if (versionEntity < new Version("5.2015.08.23"))
                {
                    if (Upgrade20150823())
                    {
                      //  InitExpressData();
                    }
                }
                if (versionEntity < new Version("5.2015.08.26"))
                {
                    if (Upgrade20150826())
                    {
                        if (Upgrade20150823())
                        {
                           // InitExpressData();
                            parameterManager.SetParameter(BaseParameterEntity.TableName, "Bill", "DBVersion", "Synchronous", "5.2015.08.26");
                        }
                    }
                }
                if (versionEntity < new Version("5.2015.09.14"))
                {
                    Upgrade20150914();
                }
                if (versionEntity < new Version("5.2015.10.15"))
                {
                    Upgrade20151015();
                }
                if (versionEntity < new Version("5.2015.10.25"))
                {
                    Upgrade20151025();
                }
                if (versionEntity < new Version("5.2015.11.05"))
                {
                    Upgrade20151105();
                }
                if (versionEntity < new Version("5.2015.11.11"))
                {
                    Upgrade20151111();
                }
                if (versionEntity < new Version("5.2015.12.12"))
                {
                    Upgrade20151212();
                }
                if (versionEntity < new Version("5.2016.01.20"))
                {
                    Upgrade20160120();
                }
                if (versionEntity < new Version("6.2016.06.20"))
                {
                    Upgrade20160620();
                }
                if (versionEntity < new Version("6.2016.07.20"))
                {
                    Upgrade20160720();
                }
                //if (versionEntity < new Version("6.2016.03.21"))
                //{
                //    parameterManager.SetParameter(BaseParameterEntity.TableName, "Bill", "DBVersion", "Synchronous", "6.2016.03.21");
                //}
                InitExpressData();
            }
            catch (Exception ex)
            {
                // 在本地记录异常
                LogUtil.WriteException(ex);
            }
        }
    }
}