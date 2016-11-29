//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd.  
//-----------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data;

namespace DotNet.Business
{
    using DotNet.Utilities;

    /// <summary>
    ///	DbLogic
    /// 通用基类
    /// 
    /// 修改记录
    /// 
    ///		2012.02.05 版本：1.0	JiRiGaLa 分离程序。
    ///	
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2012.02.05</date>
    /// </author> 
    /// </summary>
    public partial class DbLogic
    {
        //
        // 获取个数
        //

        #region public static int GetCount(IDbHelper dbHelper, string tableName, List<KeyValuePair<string, object>> parameters, KeyValuePair<string, object> parameter = new KeyValuePair<string, object>()) 获取个数
        /// <summary>
        /// 获取个数
        /// </summary>
        /// <param name="dbHelper">数据库连接</param>
        /// <param name="tableName">目标表名</param>
        /// <param name="parameters">目标字段,值</param>
        /// <returns>行数</returns>
        public static int GetCount(IDbHelper dbHelper, string tableName, List<KeyValuePair<string, object>> parameters, KeyValuePair<string, object> parameter = new KeyValuePair<string, object>())
        {
            int result = 0;
            string sqlQuery = "SELECT COUNT(1) "
                + " FROM " + tableName
                + " WHERE " + GetWhereString(dbHelper, parameters, BaseBusinessLogic.SQLLogicConditional);

            if (!string.IsNullOrEmpty(parameter.Key))
            {
                if (parameter.Value != null)
                {
                    sqlQuery += BaseBusinessLogic.SQLLogicConditional + "( " + parameter.Key + " <> '" + parameter.Value + "' ) ";
                }
                else
                {
                    sqlQuery += BaseBusinessLogic.SQLLogicConditional + "( " + parameter.Key + " IS NOT NULL) ";
                }
            }

            object returnObject = null;
            if (parameters != null)
            {
                returnObject = dbHelper.ExecuteScalar(sqlQuery, dbHelper.MakeParameters(parameters));
            }
            else
            {
                returnObject = dbHelper.ExecuteScalar(sqlQuery);
            }
            if (returnObject != null)
            {
                result = int.Parse(returnObject.ToString());
            }
            return result;
        }
        #endregion

        #region public static int GetCount(IDbHelper dbHelper, string tableName, string whereClause = null, IDbDataParameter[] dbParameters = null) 获取个数
        /// <summary>
        /// 获取个数
        /// </summary>
        /// <param name="dbHelper">数据库连接</param>
        /// <param name="tableName">目标表名</param>
        /// <param name="whereClause">条件</param>
        /// <param name="dbParameters">参数</param>
        /// <returns>行数</returns>
        public static int GetCount(IDbHelper dbHelper, string tableName, string whereClause = null, IDbDataParameter[] dbParameters = null, string currentIndex = null)
        {
            int result = 0;

            if (currentIndex == null)
            {
                currentIndex = string.Empty;
            }
            string sqlQuery = "SELECT " + currentIndex + " COUNT(1) FROM " + tableName;
            if (!string.IsNullOrEmpty(whereClause))
            {
                sqlQuery += " WHERE " + whereClause;
            }
            // with (nolock)
            object returnObject = null;
            try
            {
                returnObject = dbHelper.ExecuteScalar(sqlQuery, dbParameters);
            }
            catch (System.Exception ex)
            {
                string writeMessage = "DbLogic.GetCount:发生时间:" + DateTime.Now
                    + System.Environment.NewLine + "dbHelper.CurrentDbType:" + dbHelper.CurrentDbType.ToString()
                    + System.Environment.NewLine + "sqlQuery:" + sqlQuery
                    + System.Environment.NewLine + "Message:" + ex.Message
                    + System.Environment.NewLine + "Source:" + ex.Source
                    + System.Environment.NewLine + "StackTrace:" + ex.StackTrace
                    + System.Environment.NewLine + "TargetSite:" + ex.TargetSite
                    + System.Environment.NewLine;

                FileUtil.WriteMessage(writeMessage, BaseSystemInfo.StartupPath + "//Exception//Exception" + DateTime.Now.ToString(BaseSystemInfo.DateFormat) + ".txt");
            }
            if (returnObject != null)
            {
                result = int.Parse(returnObject.ToString());
            }

            return result;
        }
        #endregion

        //
        // 表是否存在
        //
        public static bool Exists(IDbHelper dbHelper, string userTable)
        {
            string commandText = string.Empty;
            if (dbHelper.CurrentDbType == CurrentDbType.SqlServer)
            {
                commandText = string.Format("SELECT COUNT(1) FROM sysobjects WHERE id = object_id(N'[{0}]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1", userTable);
            }
            else if (dbHelper.CurrentDbType == CurrentDbType.Oracle)
            {
                commandText = string.Format("SELECT COUNT(1) FROM User_tables WHERE table_name = '{0}'", userTable.ToUpper());
            }
            else if (dbHelper.CurrentDbType == CurrentDbType.MySql)
            {
                commandText = string.Format("SELECT COUNT(1) FROM information_schema.TABLES WHERE table_name = '{0}'", userTable);
            }
            else if (dbHelper.CurrentDbType == CurrentDbType.SQLite)
            {
                commandText = string.Format("SELECT COUNT(1) FROM sqlite_master WHERE type='table' AND name = '{0}'", userTable);
            }
            object result = dbHelper.ExecuteScalar(commandText);
            return int.Parse(result.ToString()) > 0;
        }

        //
        // 记录是否存在
        //

        #region public static bool Exists(IDbHelper dbHelper, string tableName, List<KeyValuePair<string, object>> parameters, KeyValuePair<string, object> parameter = null) 记录是否存在
        /// <summary>
        /// 记录是否存在
        /// </summary>
        /// <param name="dbHelper">数据库连接</param>
        /// <param name="tableName">目标表名</param>
        /// <param name="parameters">参数</param>
        /// <param name="targetField">获取字段</param>
        /// <param name="name">目标字段名</param>
        /// <param name="parameters">目标字段值</param>
        /// <returns>存在</returns>
        public static bool Exists(IDbHelper dbHelper, string tableName, List<KeyValuePair<string, object>> parameters, KeyValuePair<string, object> parameter = new KeyValuePair<string, object>())
        {
            return GetCount(dbHelper, tableName, parameters, parameter) > 0;
        }
        #endregion
    }
}