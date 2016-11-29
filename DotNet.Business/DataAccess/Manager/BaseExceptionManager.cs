//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd.  
//-----------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace DotNet.Business
{
    using DotNet.Model;
    using DotNet.Utilities;

    /// <summary>
    ///	BaseExceptionManager
    /// 异常记录（程序OK）
    ///
    /// 修改记录
    /// 
    ///     2008.04.22 版本：3.3 LiangMingMIng 修改了错误的表名 BaseNewsEntity 改成 BaseExceptionEntity.TableName
    ///     2008.04.22 版本：3.2 JiRiGaLa   在新的数据库连接里保存异常，不影响其它程序逻辑的事务处理。
    ///     2007.12.03 版本：3.1 JiRiGaLa   吉日整理主键规范化。
    ///		2007.08.25 版本：3.0 JiRiGaLa   WriteException 本地写入异常信息。
    ///		2007.08.01 版本：2.0 JiRiGaLa   LogException 时判断 ConnectionStat，对函数方法进行优化。
    ///		2007.06.07 版本：1.3 JiRiGaLa   进行EventLog主键优化。
    ///     2007.06.07 版本：1.2 JiRiGaLa   重新整理主键。
    ///		2006.02.06 版本：1.1 JiRiGaLa   重新调整主键的规范化。
    ///		2004.11.04 版本：1.0 JiRiGaLa   建立表结构，准备着手记录系统中的异常处理部分。
    ///		2005.08.13 版本：1.0 JiRiGaLa   整理主键。
    /// 
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2008.04.22</date>
    /// </author> 
    /// </summary>
    public partial class BaseExceptionManager : BaseManager
    {
        public BaseExceptionManager()
        {
            base.CurrentTableName = BaseExceptionEntity.TableName;
        }

        public BaseExceptionManager(IDbHelper dbHelper)
            : this()
        {
            DbHelper = dbHelper;
        }

        public BaseExceptionManager(IDbHelper dbHelper, BaseUserInfo userInfo)
            : this(dbHelper)
        {
            UserInfo = userInfo;
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="id">主键</param>
        public BaseExceptionEntity GetObject(string id)
        {
            return BaseEntity.Create<BaseExceptionEntity>(this.ExecuteReader(new KeyValuePair<string, object>(BaseExceptionEntity.FieldId, id)));
            // return BaseEntity.Create<BaseExceptionEntity>(this.GetDataTableById(id));
        }

        #region public DataTable Search(string searchValue) 查询
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="search">查询</param>
        /// <returns>数据表</returns>
        public DataTable Search(string searchValue)
        {
            string sqlQuery = string.Empty;
            sqlQuery = "SELECT * "
                    + " FROM " + BaseExceptionEntity.TableName
                    + " WHERE 1 = 1 ";

            List<IDbDataParameter> dbParameters = new List<IDbDataParameter>();
            if (!String.IsNullOrEmpty(searchValue))
            {
                sqlQuery += string.Format(" AND ({0} LIKE {1}", BaseExceptionEntity.FieldIPAddress, DbHelper.GetParameter(BaseExceptionEntity.FieldIPAddress));
                sqlQuery += string.Format(" OR {0} LIKE {1}", BaseExceptionEntity.FieldFormattedMessage, DbHelper.GetParameter(BaseExceptionEntity.FieldFormattedMessage));
                sqlQuery += string.Format(" OR {0} LIKE {1}", BaseExceptionEntity.FieldProcessName, DbHelper.GetParameter(BaseExceptionEntity.FieldProcessName));
                sqlQuery += string.Format(" OR {0} LIKE {1}", BaseExceptionEntity.FieldMachineName, DbHelper.GetParameter(BaseExceptionEntity.FieldMachineName));
                sqlQuery += string.Format(" OR {0} LIKE {1})", BaseExceptionEntity.FieldMessage, DbHelper.GetParameter(BaseExceptionEntity.FieldMessage));
                searchValue = searchValue.Trim();
                if (searchValue.IndexOf("%") < 0)
                {
                    searchValue = string.Format("%{0}%", searchValue);
                }
                dbParameters.Add(DbHelper.MakeParameter(BaseExceptionEntity.FieldIPAddress, searchValue));
                dbParameters.Add(DbHelper.MakeParameter(BaseExceptionEntity.FieldFormattedMessage, searchValue));
                dbParameters.Add(DbHelper.MakeParameter(BaseExceptionEntity.FieldProcessName, searchValue));
                dbParameters.Add(DbHelper.MakeParameter(BaseExceptionEntity.FieldMachineName, searchValue));
                dbParameters.Add(DbHelper.MakeParameter(BaseExceptionEntity.FieldMessage, searchValue));
            }
            var dt = new DataTable(BaseExceptionEntity.TableName);
            DbHelper.Fill(dt, sqlQuery, dbParameters.ToArray());
            return dt;
        }
        #endregion

        #region public string AddObject(Exception ex) 记录异常情况
        /// <summary>
        /// 记录异常情况
        /// </summary>
        /// <param name="Exception">异常类</param>
        /// <returns>主键</returns>
        public string AddObject(Exception ex)
        {
            // string result = BaseSequenceManager.Instance.Increment(DbHelper, BaseExceptionEntity.TableName);
            string sequence = Guid.NewGuid().ToString("N");
            SQLBuilder sqlBuilder = new SQLBuilder(DbHelper);
            sqlBuilder.BeginInsert(this.CurrentTableName);
            sqlBuilder.SetValue(BaseExceptionEntity.FieldId, sequence);
            // sqlBuilder.SetValue(BaseExceptionEntity.FieldTitle, UserInfo.ProcessName);
            // sqlBuilder.SetValue(BaseExceptionEntity.FieldProcessId, UserInfo.ProcessId);
            // sqlBuilder.SetValue(BaseExceptionEntity.FieldProcessName, UserInfo.ProcessName);
            sqlBuilder.SetValue(BaseExceptionEntity.FieldMessage, ex.Message);
            sqlBuilder.SetValue(BaseExceptionEntity.FieldThreadName, ex.Source);
            sqlBuilder.SetValue(BaseExceptionEntity.FieldFormattedMessage, ex.StackTrace);
            if (UserInfo != null)
            {
                sqlBuilder.SetValue(BaseExceptionEntity.FieldIPAddress, UserInfo.IPAddress);
                sqlBuilder.SetValue(BaseExceptionEntity.FieldCreateUserId, UserInfo.Id);
                sqlBuilder.SetValue(BaseExceptionEntity.FieldCreateBy, UserInfo.RealName);
            }
            sqlBuilder.SetDBNow(BaseExceptionEntity.FieldCreateOn);
            return sqlBuilder.EndInsert() > 0 ? sequence : string.Empty;
        }
        #endregion

        #region public string LogException(BaseUserInfo userInfo, Exception ex) 记录异常情况
        /// <summary>
        /// 用新数据库连接保存异常情况
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        /// <param name="exception">异常</param>
        /// <returns>主键</returns>
        public static string LogException(BaseUserInfo userInfo, Exception ex)
        {
            IDbHelper dbHelper = DbHelperFactory.GetHelper(BaseSystemInfo.UserCenterDbType, BaseSystemInfo.UserCenterDbConnection);
            return LogException(dbHelper, userInfo, ex);
        }
        #endregion

        #region public static string LogException(IDbHelper dbHelper, BaseUserInfo userInfo, Exception ex) 记录异常情况
        /// <summary>
        /// 记录异常情况
        /// </summary>
        /// <param name="dbHelper">数据库连接</param>
        /// <param name="userInfo">用户</param>
        /// <param name="Exception">异常</param>
        /// <returns>主键</returns>
        public static string LogException(IDbHelper dbHelper, BaseUserInfo userInfo, Exception ex)
        {
            // 在控制台需要输出错误信息
            #if (DEBUG)
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(ex.InnerException);
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(string.Empty);
            #endif

            string result = string.Empty;
            // 系统里应该可以配置是否记录异常现象
            if (!BaseSystemInfo.LogException)
            {
                return result;
            }
            // Windows系统异常中
            if (BaseSystemInfo.EventLog)
            {
                if (!System.Diagnostics.EventLog.SourceExists(BaseSystemInfo.SoftName))
                {
                    System.Diagnostics.EventLog.CreateEventSource(BaseSystemInfo.SoftName, BaseSystemInfo.SoftFullName);
                }
                System.Diagnostics.EventLog eventLog = new System.Diagnostics.EventLog();
                eventLog.Source = BaseSystemInfo.SoftName;
                eventLog.WriteEntry(ex.Message, EventLogEntryType.Error);
            }

            // 判断一下数据库是否打开状态，若数据库都没能打开，还记录啥错误，不是又抛出另一个错误了？
            BaseExceptionManager exceptionManager = new BaseExceptionManager(dbHelper, userInfo);
            result = exceptionManager.AddObject(ex);
            
            return result;
        }
        #endregion
    }
}