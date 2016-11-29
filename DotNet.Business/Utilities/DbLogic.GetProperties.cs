//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd.  
//-----------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

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
        #region public static string[] GetProperties(IDbHelper dbHelper, string tableName, string name, Object[] values, string targetField) 获取数据表
        /// <summary>
        /// 获取数据表
        /// 这个方法按道理目标数据不会非常大，所以可以不优化，问题不大
        /// </summary>
        /// <param name="dbHelper">数据库连接</param>
        /// <param name="tableName">目标表名</param>
        /// <param name="name">字段名</param>
        /// <param name="values">字段值</param>
        /// <param name="targetField">目标字段</param>
        /// <returns>数据表</returns>
        public static string[] GetProperties(IDbHelper dbHelper, string tableName, string name, Object[] values, string targetField)
        {
            string sqlQuery = "SELECT " + targetField
                            + "   FROM " + tableName
                            + "  WHERE " + name + " IN (" + string.Join(",", values) + ")";
            var dt = dbHelper.Fill(sqlQuery);
            return BaseBusinessLogic.FieldToArray(dt, targetField).Distinct<string>().Where(t => !string.IsNullOrEmpty(t)).ToArray();
        }
        #endregion

        #region public static string[] GetProperties(IDbHelper dbHelper, string tableName, List<KeyValuePair<string, object>> parameters, int? topLimit = null, string targetField = null) 获取数据权限
        /// <summary>
        /// 获取数据表
        /// </summary>
        /// <param name="dbHelper">数据库连接</param>
        /// <param name="tableName">数据来源表名</param>
        /// <param name="parameters">字段名,字段值</param>
        /// <param name="topLimit">前几个记录</param>
        /// <param name="targetField">目标字段</param>
        /// <returns>数据表</returns>
        public static string[] GetProperties(IDbHelper dbHelper, string tableName, List<KeyValuePair<string, object>> parameters, int? topLimit = null, string targetField = null)
        {
            List<string> result = new List<string>();

            if (string.IsNullOrEmpty(targetField))
            {
                targetField = BaseBusinessLogic.FieldId;
            }
            // 这里是需要完善的功能，完善了这个，是一次重大突破           
            string sqlQuery = "SELECT DISTINCT " + targetField + " FROM " + tableName;
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
                    case CurrentDbType.MySql:
                        sqlQuery += " LIMIT 0, " + topLimit;
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

            //这里重复了，胡流东
            //switch (dbHelper.CurrentDbType)
            //{
            //    case CurrentDbType.MySql:
            //        sqlQuery += " LIMIT 0, " + topLimit;
            //        break;
            //}

            // var dt = new DataTable(tableName);
            // dbHelper.Fill(dt, sqlQuery, dbHelper.MakeParameters(parameters));
            // return BaseBusinessLogic.FieldToArray(dt, targetField).Distinct<string>().Where(t => !string.IsNullOrEmpty(t)).ToArray();

            // 20151008 吉日嘎拉 优化为 DataReader 读取数据，大量数据读取时，效率高，节约内存，提高处理效率
            using (IDataReader dataReader = dbHelper.ExecuteReader(sqlQuery, dbHelper.MakeParameters(parameters)))
            {
                while (dataReader.Read())
                {
                    result.Add(dataReader[targetField].ToString());
                }
            }

            return result.ToArray();
        }
        #endregion
    }
}