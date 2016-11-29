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
    ///		2015.06.16 版本：1.0	JiRiGaLa 分离程序。
    ///	
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2015.06.16</date>
    /// </author> 
    /// </summary>
    public partial class DbLogic
    {
        #region public static IDataReader ExecuteReader(IDbHelper dbHelper, string tableName, string name, object[] values, string order = null) 获取数据表 一参 参数为数组
        /// <summary>
        /// 获取数据表 一参 参数为数组
        /// </summary>
        /// <param name="dbHelper">数据库连接</param>
        /// <param name="tableName">数据来源表名</param>
        /// <param name="name">字段名</param>
        /// <param name="value">字段值</param>
        /// <param name="order">排序</param>
        /// <returns>数据表</returns>
        public static IDataReader ExecuteReader(IDbHelper dbHelper, string tableName, string name, object[] values, string order = null)
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
            return dbHelper.ExecuteReader(sqlQuery);
        }

        public static List<TModel> ExecuteReader<TModel>(IDbHelper dbHelper, string tableName, string name,
            object[] values, string order = null) where TModel : new()
        {
            return ExecuteReader(dbHelper, tableName, name, values, order).ToList<TModel>();
        }
        #endregion

        //
        // 读取列表部分 填充IDataReader 常用
        //
        private static string AddWhere(string where, string appendWhere)
        {
            if (string.IsNullOrEmpty(where))
            {
                return appendWhere;
            }
            return where + BaseBusinessLogic.SQLLogicConditional + appendWhere;
        }

        public static IDataReader ExecuteReader(IDbHelper dbHelper, string tableName, string where)
        {
            return ExecuteReader2(dbHelper, tableName, where);
        }

        public static List<TModel> ExecuteReader<TModel>(IDbHelper dbHelper, string tableName, string where) where TModel : new()
        {
            return ExecuteReader(dbHelper, tableName, where).ToList<TModel>();
        }

        public static IDataReader ExecuteReader2(IDbHelper dbHelper, string tableName, string where, int topLimit = 0, string order = null)
        {
            // 这里是需要完善的功能，完善了这个，是一次重大突破 
            string sqlQuery = ExecuteReaderQueryString(dbHelper, tableName, "*", where, topLimit, order);
            return dbHelper.ExecuteReader(sqlQuery);
        }

        public static List<TModel> ExecuteReader2<TModel>(IDbHelper dbHelper, string tableName, string where, int topLimit = 0,
            string order = null) where TModel : new()
        {
            return ExecuteReader2(dbHelper, tableName, where, topLimit, order).ToList<TModel>();
        }

        public static string ExecuteReaderQueryString(IDbHelper dbHelper, string tableName, string selectFields, string where, int topLimit, string order)
        {
            string reslut = string.Empty;

            // 2015-12-28 吉日嘎拉，简化程序，简化逻辑。
            switch (dbHelper.CurrentDbType)
            {
                case CurrentDbType.Oracle:
                    reslut = "SELECT " + selectFields;
                    if (tableName.Trim().IndexOf(" ") > 0)
                    {
                        reslut += " FROM (" + tableName + ")";
                    }
                    else
                    {
                        reslut += " FROM " + tableName;
                    }
                    if (!string.IsNullOrEmpty(where))
                    {
                        reslut += " WHERE " + where;
                    }
                    if (!string.IsNullOrEmpty(order))
                    {
                        reslut += " ORDER BY " + order;
                    }
                    if (topLimit > 0)
                    {
                        reslut = "SELECT * FROM (" + reslut + ") WHERE ROWNUM < = " + topLimit;
                    }
                    break;

                case CurrentDbType.Access:
                case CurrentDbType.SqlServer:
                    reslut = "SELECT " + selectFields;
                    if (topLimit > 0)
                    {
                        reslut = "SELECT TOP " + topLimit.ToString() + " " + selectFields;
                    }
                    if (tableName.Trim().IndexOf(" ") > 0)
                    {
                        reslut += " FROM  (" + tableName + ")";
                    }
                    else
                    {
                        reslut += " FROM " + tableName;
                    }
                    if (!string.IsNullOrEmpty(where))
                    {
                        reslut += " WHERE " + where;
                    }
                    if (!string.IsNullOrEmpty(order))
                    {
                        reslut += " ORDER BY " + order;
                    }
                    break;

                case CurrentDbType.MySql:
                case CurrentDbType.SQLite:
                    reslut = "SELECT " + selectFields;
                    if (tableName.Trim().IndexOf(" ") > 0)
                    {
                        reslut += " FROM (" + tableName + ")";
                    }
                    else
                    {
                        reslut += " FROM " + tableName;
                    }
                    if (!string.IsNullOrEmpty(where))
                    {
                        reslut += " WHERE " + where;
                    }
                    if (!string.IsNullOrEmpty(order))
                    {
                        reslut += " ORDER BY " + order;
                    }
                    if (topLimit > 0)
                    {
                        reslut += " LIMIT 0, " + topLimit;
                    }
                    break;
            }

            return reslut;
        }

        #region public static IDataReader ExecuteReader(IDbHelper dbHelper, string tableName, string name, object[] values, string order = null) 获取数据表 一参 参数为数组
        /// <summary>
        /// 获取数据表 一参 参数为数组
        /// </summary>
        /// <param name="dbHelper">数据库连接</param>
        /// <param name="tableName">数据来源表名</param>
        /// <param name="name">字段名</param>
        /// <param name="value">字段值</param>
        /// <param name="order">排序</param>
        /// <returns>数据表</returns>
        public static IDataReader ExecuteReader(IDbHelper dbHelper, string tableName, string selectField, string name, object[] values, string order = null)
        {
            string sqlQuery = "SELECT " + selectField
                            + "   FROM " + tableName;

            if (values == null || values.Length == 0)
            {
                sqlQuery += "  WHERE " + name + " IS NULL";
            }
            else
            {
                sqlQuery += "  WHERE " + name + " IN (" + string.Join(",", values) + ")";
            }
            if (!String.IsNullOrEmpty(order))
            {
                sqlQuery += " ORDER BY " + order;
            }
            return dbHelper.ExecuteReader(sqlQuery);
        }

        public static List<TModel> ExecuteReader<TModel>(IDbHelper dbHelper, string tableName, string selectField, string name,
            object[] values, string order = null) where TModel : new()
        {
            return ExecuteReader(dbHelper, tableName, selectField, name, values, order).ToList<TModel>();
        }

        #endregion

        public static IDataReader ExecuteReader(IDbHelper dbHelper, string tableName, List<KeyValuePair<string, object>> parameters, int topLimit = 0, string order = null)
        {
            // 这里是需要完善的功能，完善了这个，是一次重大突破           
            //string sqlQuery = "SELECT * FROM " + tableName;
            //string whereSql = string.Empty;
            //if (topLimit != 0)
            //{
            //	switch (dbHelper.CurrentDbType)
            //	{
            //		case CurrentDbType.Access:
            //		case CurrentDbType.SqlServer:
            //			sqlQuery = "SELECT TOP " + topLimit.ToString() + " * FROM " + tableName;
            //			break;
            //		case CurrentDbType.Oracle:
            //			whereSql = " ROWNUM < = " + topLimit;
            //			break;
            //	}
            //}
            //string subSql = GetWhereString(dbHelper, parameters, BaseBusinessLogic.SQLLogicConditional);
            //if (!string.IsNullOrEmpty(subSql))
            //{
            //	if (whereSql.Length > 0)
            //	{
            //		whereSql = whereSql + BaseBusinessLogic.SQLLogicConditional + subSql;
            //	}
            //	else
            //	{
            //		whereSql = subSql;
            //	}
            //}
            //if (whereSql.Length > 0)
            //{
            //	sqlQuery += " WHERE " + whereSql;
            //}
            //if ((order != null) && (order.Length > 0))
            //{
            //	sqlQuery += " ORDER BY " + order;
            //}
            //if (topLimit != 0)
            //{
            //	switch (dbHelper.CurrentDbType)
            //	{
            //		case CurrentDbType.MySql:
            //			sqlQuery += " LIMIT 0, " + topLimit;
            //			break;
            //	}
            //}
            string sqlQuery = ExecuteReaderQueryString(dbHelper, tableName, "*", GetWhereString(dbHelper, parameters, BaseBusinessLogic.SQLLogicConditional), topLimit, order);
            if (parameters != null && parameters.Count > 0)
            {
                return dbHelper.ExecuteReader(sqlQuery, dbHelper.MakeParameters(parameters));
            }
            else
            {
                return dbHelper.ExecuteReader(sqlQuery);
            }
        }

        public static List<TModel> ExecuteReader<TModel>(IDbHelper dbHelper, string tableName,
            List<KeyValuePair<string, object>> parameters, int topLimit = 0, string order = null) where TModel : new()
        {
            return ExecuteReader(dbHelper, tableName, parameters, topLimit, order).ToList<TModel>();
        }

        /// <summary>
        /// 参数化查询 
        /// </summary>
        /// <param name="dbHelper">数据库连接</param>
        /// <param name="tableName">表名</param>
        /// <param name="parameters">参数</param>
        /// <param name="conditions">条件</param>
        /// <param name="topLimit">前多少条</param>
        /// <param name="order">派讯</param>
        /// <param name="selectField">查询的字段</param>
        /// <returns></returns>
        public static IDataReader ExecuteReader(IDbHelper dbHelper, string tableName, List<KeyValuePair<string, object>> parameters, string conditions, int topLimit = 0, string order = null, string selectField = " * ")
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
            if (parameters != null && parameters.Count > 0)
            {
                return dbHelper.ExecuteReader(sqlQuery, dbHelper.MakeParameters(parameters));
            }
            else
            {
                return dbHelper.ExecuteReader(sqlQuery);
            }
        }

        public static List<TModel> ExecuteReader<TModel>(IDbHelper dbHelper, string tableName,
            List<KeyValuePair<string, object>> parameters, string conditions, int topLimit = 0, string order = null,
            string selectField = " * ") where TModel : new()
        {
            return ExecuteReader(dbHelper, tableName, parameters, conditions, topLimit, order, selectField)
                    .ToList<TModel>();
        }
    }
}