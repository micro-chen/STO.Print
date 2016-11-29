//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd.  
//-----------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace DotNet.Business
{
    using DotNet.Utilities;

    /// <summary>
    ///	DbLogic
    /// 通用基类
    /// 
    /// 修改记录
    /// 
    ///		2016.02.27 版本：2.1	JiRiGaLa 提高数据库中的读取性能。
    ///		2012.03.20 版本：2.0	JiRiGaLa 整理参数传递方法。
    ///		2012.02.05 版本：1.0	JiRiGaLa 分离程序。
    ///	
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2016.02.27</date>
    /// </author> 
    /// </summary>
    public partial class DbLogic
    {
        #region public static string GetProperty(IDbHelper dbHelper, string tableName, List<KeyValuePair<string, object>> parameters, string targetField, int? topLimit = null, string orderBy = null)
        /// <summary>
        /// 读取属性
        /// </summary>
        /// <param name="dbHelper">数据库连接</param>
        /// <param name="tableName">目标表名</param>
        /// <param name="parameters">字段名,键值</param>
        /// <param name="targetField">获取字段</param>
        /// <returns>属性</returns>
        public static string GetProperty(IDbHelper dbHelper, string tableName, List<KeyValuePair<string, object>> parameters, string targetField, int topLimit = 1, string orderBy = null)
        {
            string result = string.Empty;

            if (string.IsNullOrEmpty(targetField))
            {
                targetField = BaseBusinessLogic.FieldId;
            }
            // 这里是需要完善的功能，完善了这个，是一次重大突破           
            string sqlQuery = "SELECT " + targetField + " FROM " + tableName;
            string whereSql = string.Empty;
            if (topLimit != null && topLimit > 0)
            {
                switch (dbHelper.CurrentDbType)
                {
                    case CurrentDbType.Access:
                    case CurrentDbType.SqlServer:
                        sqlQuery = "SELECT TOP " + topLimit.ToString() + targetField + " FROM " + tableName;
                        break;
                    case CurrentDbType.Oracle:
                        whereSql = " ROWNUM < = " + topLimit;
                        break;
                }
            }
            string subSql = GetWhereString(dbHelper, parameters, BaseBusinessLogic.SQLLogicConditional);
            if (subSql.Length > 0)
            {
                if (whereSql.Length > 0)
                {
                    whereSql = whereSql + BaseBusinessLogic.SQLLogicConditional + subSql;
                }
                else
                {
                    whereSql = subSql;
                }
            }
            if (whereSql.Length > 0)
            {
                sqlQuery += " WHERE " + whereSql;
            }

            if (!string.IsNullOrEmpty(orderBy))
            {
                sqlQuery += " ORDER BY " + orderBy;
            }

            if (topLimit != null && topLimit > 0)
            {
                switch (dbHelper.CurrentDbType)
                {
                    case CurrentDbType.MySql:
                        sqlQuery += " LIMIT 0, " + topLimit;
                        break;
                }
            }

            object returnObject = dbHelper.ExecuteScalar(sqlQuery, dbHelper.MakeParameters(parameters));
            if (returnObject != null && returnObject != DBNull.Value)
            {
                result = returnObject.ToString();
            }

            return result;
        }
        #endregion
    }
}