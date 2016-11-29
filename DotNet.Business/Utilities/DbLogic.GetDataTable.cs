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
        #region public static DataTable GetDataTable(IDbHelper dbHelper, string tableName, string name, object[] values, string order = null) 获取数据表 一参 参数为数组
        /// <summary>
        /// 获取数据表 一参 参数为数组
        /// </summary>
        /// <param name="dbHelper">数据库连接</param>
        /// <param name="tableName">数据来源表名</param>
        /// <param name="name">字段名</param>
        /// <param name="value">字段值</param>
        /// <param name="order">排序</param>
        /// <returns>数据表</returns>
        public static DataTable GetDataTable(IDbHelper dbHelper, string tableName, string name, object[] values, string order = null)
        {
            string sqlQuery = "SELECT * "
                            + "   FROM " + tableName;
            if (values == null || values.Length == 0)
            {
                sqlQuery += "  WHERE " + name + " IS NULL";
            }
            else
            {
                sqlQuery += "  WHERE " + name + " IN (" + BaseBusinessLogic.ObjectsToList(values, "'") + ")";
                // sqlQuery += "  WHERE " + name + " IN (" + string.Join(",", values) + ")";
            }
            if (!String.IsNullOrEmpty(order))
            {
                sqlQuery += " ORDER BY " + order;
            }
            return dbHelper.Fill(sqlQuery);
        }
        #endregion
        
        public static DataTable GetDataTable(IDbHelper dbHelper, string tableName, List<KeyValuePair<string, object>> parameters, int topLimit = 0, string order = null, string sqlLogicConditional = null)
        {
            // 这里是需要完善的功能，完善了这个，是一次重大突破           
            string sqlQuery = "SELECT * FROM " + tableName;
            string whereSql = string.Empty;
            if (topLimit != 0)
            {
                switch (dbHelper.CurrentDbType)
                {
                    case CurrentDbType.Access:
                    case CurrentDbType.SqlServer:
                        sqlQuery = "SELECT TOP " + topLimit.ToString() + " * FROM " + tableName;
                        break;
                    case CurrentDbType.Oracle:
                        if (string.IsNullOrEmpty(order))
                        {
                            whereSql = AddWhere(whereSql, " ROWNUM < = " + topLimit);
                        }
                        break;
                }
            }
            if (string.IsNullOrEmpty(sqlLogicConditional))
            {
                sqlLogicConditional = BaseBusinessLogic.SQLLogicConditional;
            }
            string subSql = GetWhereString(dbHelper, parameters, sqlLogicConditional);
            if (!string.IsNullOrEmpty(subSql))
            {
                if (whereSql.Length > 0)
                {
                    whereSql = whereSql + sqlLogicConditional + subSql;
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
            if ((order != null) && (order.Length > 0))
            {
                sqlQuery += " ORDER BY " + order;
            }
            if (topLimit != 0)
            {
                switch (dbHelper.CurrentDbType)
                {
                    case CurrentDbType.MySql:
                        sqlQuery += " LIMIT 0, " + topLimit;
                        break;
                    case CurrentDbType.Oracle:
                        if (!string.IsNullOrEmpty(order))
                        {
                            sqlQuery = "SELECT * FROM (" + sqlQuery + ") WHERE ROWNUM < = " + topLimit;
                        }
                        break;
                }
            }
            var dt = new DataTable(tableName);
            if (parameters != null && parameters.Count > 0)
            {
                dt = dbHelper.Fill(sqlQuery, dbHelper.MakeParameters(parameters));
            }
            else
            {
                dt = dbHelper.Fill(sqlQuery);
            }
            return dt;
        }

        public static DataTable GetDataTable(IDbHelper dbHelper, string tableName, IDbDataParameter[] dbParameters, string conditions, int topLimit = 0, string order = null, string selectField = " * ")
        {
            string sqlQuery = "SELECT " + selectField + " FROM " + tableName;
            string whereSql = string.Empty;
            if (topLimit != 0)
            {
                switch (dbHelper.CurrentDbType)
                {
                    case CurrentDbType.Access:
                    case CurrentDbType.SqlServer:
                        sqlQuery = "SELECT TOP " + topLimit.ToString() + selectField + " FROM " + tableName;
                        break;
                    case CurrentDbType.Oracle:
                        if (string.IsNullOrEmpty(order))
                        {
                            whereSql = AddWhere(whereSql, " ROWNUM < = " + topLimit);
                        }
                        break;
                }
            }
            // 要传入 conditions
            if (!string.IsNullOrEmpty(conditions))
            {
                conditions = " WHERE " + conditions;
            }
            sqlQuery += conditions + whereSql;
            if ((order != null) && (order.Length > 0))
            {
                sqlQuery += " ORDER BY " + order;
            }
            var dt = new DataTable(tableName);
            if (topLimit != 0)
            {
                switch (dbHelper.CurrentDbType)
                {
                    case CurrentDbType.MySql:
                        sqlQuery += " LIMIT 0, " + topLimit;
                        break;
                    case CurrentDbType.Oracle:
                        if (!string.IsNullOrEmpty(order))
                        {
                            sqlQuery = "SELECT * FROM (" + sqlQuery + ") WHERE ROWNUM < = " + topLimit;
                        }
                        break;
                }
            }
            if (dbParameters != null && dbParameters.Length > 0)
            {
                dt = dbHelper.Fill(sqlQuery, dbParameters);
            }
            else
            {
                dt = dbHelper.Fill(sqlQuery);
            }
            return dt;
        }
    }
}