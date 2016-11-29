//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2015 , STO TECH, Ltd. 
//-----------------------------------------------------------------

using System;
using System.Threading;

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
    ///     2015.05.25 版本：3.1 YangHengLian  分步骤可以进行升级的功能实现。
    ///     2015.05.14 版本：3.0 YangHengLian  去掉客户端执行脚本的功能。
    ///     2015.05.04 版本：2.0 YangHengLian  简化同步方式、简化同步数据。
    ///     2015.03.23 版本：1.0 YangHengLian  添加项目详细资料功能页面编写。
    ///		
    /// <author>
    ///		<name>YangHengLian</name>
    ///		<date>2015.05.25</date>
    /// </author> 
    /// </summary>
    public partial class Synchronous
    {
        /// <summary>
        /// 数据库文件路径
        /// </summary>
        private static readonly string SqLiteDb = System.Windows.Forms.Application.StartupPath + @"\DataBase\STO.Bill.db";

        public static void GetFromService()
        {
            if (BaseSystemInfo.OnInternet)
            {
                Thread messageThread = new Thread(new ThreadStart(Synchronous.SynchronousDb));
                messageThread.Start();
            }
        }


        /// <summary>
        /// 同步省市区数据
        /// </summary>
        /// <param name="delete"></param>
        /// <returns></returns>
        public static int SynchronousArea(bool delete = false)
        {
            int result = 0;

            if (!System.IO.File.Exists(SqLiteDb))
            {
                return result;
            }

            DateTime? modifiedOn = new DateTime(2014, 01, 01);

            string dbConnection = "Data Source={StartupPath}/DataBase/STO.Bill.db;Pooling=true;FailIfMissing=false;Password=ZTO20149988";
            dbConnection = dbConnection.Replace("{StartupPath}", System.Windows.Forms.Application.StartupPath);
            using (IDbHelper dbHelper = DbHelperFactory.GetHelper(CurrentDbType.SQLite, dbConnection))
            {
                BaseParameterManager parameterManager = new BaseParameterManager(dbHelper, BaseSystemInfo.UserInfo,BaseParameterEntity.TableName);
                var tableName = BaseAreaEntity.TableName;

                if (!delete)
                {
                    var synchronous = parameterManager.GetParameter(BaseParameterEntity.TableName, "System", tableName, "Synchronous");
                    if (!string.IsNullOrEmpty(synchronous))
                    {
                        modifiedOn = DateTime.Parse(synchronous);
                    }
                }

                if (delete)
                {
                    dbHelper.ExecuteNonQuery("DELETE FROM " + tableName);
                }
                result = SynchronousTable("UserCenter", tableName, new string[] { BaseAreaEntity.FieldId }, modifiedOn, "Bill", tableName, 20000, false);
                if (result > 0)
                {
                    parameterManager.SetParameter(BaseParameterEntity.TableName, "System", tableName, "Synchronous", DateTime.Now.ToString(BaseSystemInfo.DateTimeFormat));
                }
            }

            return result;
        }

        public static void SynchronousDb()
        {
            // 是否需要同步数据？
            if (!BaseSystemInfo.Synchronous)
            {
                return;
            }
            // 是否在互联网上
            if (!BaseSystemInfo.OnInternet)
            {
                return;
            }
            // 0：若文件不存在，就退出数据同步，不进行数据同步
            if (!System.IO.File.Exists(SqLiteDb))
            {
                return;
            }
            try
            {
                SynchronousArea(true);
                SynchronousPrintMark(true);
                BaseSystemInfo.Synchronized = true;
            }
            catch (Exception ex)
            {
                // 在本地记录异常
                LogUtil.WriteException(ex);
            }
        }

        /// <summary>
        /// 同步大头笔数据
        /// </summary>
        /// <param name="delete"></param>
        /// <returns></returns>
        public static int SynchronousPrintMark(bool delete = false)
        {
            int result = 0;

            if (!System.IO.File.Exists(SqLiteDb))
            {
                return result;
            }

            DateTime? modifiedOn = new DateTime(2014, 01, 01);

            string dbConnection = "Data Source={StartupPath}/DataBase/STO.Bill.db;Pooling=true;FailIfMissing=false;Password=ZTO20149988";
            dbConnection = dbConnection.Replace("{StartupPath}", System.Windows.Forms.Application.StartupPath);
            using (IDbHelper dbHelper = DbHelperFactory.GetHelper(CurrentDbType.SQLite, dbConnection))
            {
                BaseParameterManager parameterManager = new BaseParameterManager(dbHelper, BaseSystemInfo.UserInfo,BaseParameterEntity.TableName);
                var tableName = BaseAreaProvinceMarkEntity.TableName;

                if (!delete)
                {
                    var synchronous = parameterManager.GetParameter(BaseParameterEntity.TableName, "System", tableName, "Synchronous");
                    if (!string.IsNullOrEmpty(synchronous))
                    {
                        modifiedOn = DateTime.Parse(synchronous);
                    }
                }

                if (delete)
                {
                    dbHelper.ExecuteNonQuery("DELETE FROM " + tableName);
                }
                result = SynchronousTable("UserCenter", tableName, new[] { BaseAreaProvinceMarkEntity.FieldId }, modifiedOn, "Bill", tableName, 20000, false);
                if (result > 0)
                {
                    parameterManager.SetParameter(BaseParameterEntity.TableName, "System", tableName, "Synchronous", DateTime.Now.ToString(BaseSystemInfo.DateTimeFormat));
                }
            }

            return result;
        }
    }
}