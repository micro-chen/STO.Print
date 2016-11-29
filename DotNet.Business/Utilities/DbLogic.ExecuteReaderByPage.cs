//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd.  
//-----------------------------------------------------------------

using System.Collections.Generic;
using System.Data;
using DotNet.Utilities;

namespace DotNet.Business
{
    using DotNet.Utilities;

    /// <summary>
    ///	DbLogic
    /// 通用基类
    /// 
    /// 修改记录
    /// 
    ///     2015.01.31 版本：2.2    潘齐民   修改分页方法，SqlServer库查询条件从Between strat And end 改成小于等于end 大于start
    ///     2014.08.08 版本：2.1    SongBiao 修改分页方法 多表联合显示查询字段的位置
    ///     2014.01.23 版本：2.o    JiRiGaLa 整理 Oracle 分页功能
    ///     2013.11.03 版本：1.1    HongMing Oracle 获取分页数据 增加MySQL
    ///		2012.02.05 版本：1.0	JiRiGaLa 分离程序。
    ///	
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2012.02.05</date>
    /// </author> 
    /// </summary>
    public partial class DbLogic
    {
        // SqlServer By StoredProcedure

        #region public static IDataReader ExecuteReaderByPage(IDbHelper dbHelper, out int recordCount, int pageIndex = 0, int pageSize = 20, string sortExpression = null, string sortDire = null, string tableName = null, string whereClause = null, string selectField = null)
        /// <summary>
        /// 使用存储过程获取分页数据
        /// </summary>
        /// <param name="dbHelper">数据源</param>
        /// <param name="recordCount">返回的记录数</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="sortExpression">排序字段</param>
        /// <param name="sortDire">排序</param>
        /// <param name="tableName">表名</param>
        /// <param name="whereClause">查询条件</param>
        /// <param name="selectField">查询字段</param>
        /// <returns></returns>
        public static IDataReader ExecuteReaderByPage(IDbHelper dbHelper, out int recordCount, int pageIndex = 0, int pageSize = 20, string sortExpression = null, string sortDire = null, string tableName = null, string whereClause = null, string selectField = null)
        {
            IDataReader dataReader = null;
            recordCount = 0;
            if (string.IsNullOrEmpty(selectField))
            {
                selectField = "*";
            }
            if (string.IsNullOrEmpty(whereClause))
            {
                whereClause = string.Empty;
            }
            List<IDbDataParameter> dbParameters = new List<IDbDataParameter>();
            IDbDataParameter dbDataParameter = dbHelper.MakeParameter("RecordCount", recordCount, DbType.Int64, 0, ParameterDirection.Output);
            dbParameters.Add(dbDataParameter);
            dbParameters.Add(dbHelper.MakeParameter("PageIndex", pageIndex));
            dbParameters.Add(dbHelper.MakeParameter("PageSize", pageSize));
            dbParameters.Add(dbHelper.MakeParameter("SortExpression", sortExpression));
            dbParameters.Add(dbHelper.MakeParameter("SortDire", sortDire));
            dbParameters.Add(dbHelper.MakeParameter("TableName", tableName));
            dbParameters.Add(dbHelper.MakeParameter("SelectField", selectField));
            dbParameters.Add(dbHelper.MakeParameter("WhereConditional", whereClause));
            string commandText = "GetRecordByPage";
            dataReader = dbHelper.ExecuteReader(commandText, dbParameters.ToArray(), CommandType.StoredProcedure);
            recordCount = int.Parse(dbDataParameter.Value.ToString());

            return dataReader;
        }

        /// <summary>
        /// 使用存储过程获取分页数据
        /// </summary>
        /// <param name="dbHelper">数据源</param>
        /// <param name="recordCount">返回的记录数</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="sortExpression">排序字段</param>
        /// <param name="sortDire">排序</param>
        /// <param name="tableName">表名</param>
        /// <param name="whereClause">查询条件</param>
        /// <param name="selectField">查询字段</param>
        /// <returns></returns>
        public static List<TModel> ExecuteReaderByPage<TModel>(IDbHelper dbHelper, out int recordCount, int pageIndex = 0,
            int pageSize = 20, string sortExpression = null, string sortDire = null, string tableName = null,
            string whereClause = null, string selectField = null) where TModel : new()
        {
            return
                ExecuteReaderByPage(dbHelper, out recordCount, pageIndex, pageSize, sortExpression, sortDire, tableName,
                    whereClause, selectField).ToList<TModel>();
        }

        #endregion

        /*
        #region public static DataTable GetDataTableByPage(IDbHelper dbHelper, int recordCount, int pageIndex, int pageSize, string sqlQuery, string sortExpression = null, string sortDire = null) 分页获取指定数量的数据
        /// <summary>
        /// Mysql分页获取指定数量的数据
        /// </summary>
        /// <param name="dbHelper">数据源</param>
        /// <param name="recordCount">获取多少条</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="sqlQuery"></param>
        /// <param name="sortExpression">排序字段</param>
        /// <param name="sortDire">排序</param>
        /// <returns></returns>
        public static DataTable GetDataTableByPage(IDbHelper dbHelper, string tableName, int recordCount, int pageIndex, int pageSize, string conditions, string sortExpression = null, string sortDire = null)
        {
            if (string.IsNullOrEmpty(sortExpression))
            {
                sortExpression = BaseBusinessLogic.FieldCreateOn;
            }
            if (string.IsNullOrEmpty(sortDire))
            {
                sortDire = " DESC";
            }
            string sqlCount = recordCount - ((pageIndex - 1) * pageSize) > pageSize ? pageSize.ToString() : (recordCount - ((pageIndex - 1) * pageSize)).ToString();
            string sqlStart = ((pageIndex - 1) * pageSize).ToString();
            string sqlEnd = pageSize.ToString();
            string commandText = string.Empty;
            if (tableName.ToUpper().IndexOf("SELECT") >= 0)
            {
                tableName = " (" + tableName + ") ";
            }
            string whereClause = string.Empty;
            if (!string.IsNullOrEmpty(conditions))
            {
                whereClause = string.Format(" WHERE {0}", conditions);
            }
            commandText = string.Format("SELECT * FROM {0} {1} ORDER BY {2} {3} LIMIT {4},{5}", tableName, whereClause, sortExpression, sortDire, sqlStart, sqlEnd);
            return dbHelper.Fill(commandText);
        }
        #endregion
        */

        #region public static IDataReader ExecuteReaderByPage(IDbHelper dbHelper, int recordCount, int pageIndex, int pageSize, string sqlQuery, IDbDataParameter[] dbParameters, string sortExpression = null, string sortDire = null)
        /// <summary>
        /// 分页获取指定数量的数据
        /// </summary>
        /// <param name="dbHelper">数据源</param>
        /// <param name="recordCount">获取多少条</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="sqlQuery"></param>
        /// <param name="sortExpression">排序字段</param>
        /// <param name="sortDire">排序</param>
        /// <returns></returns>
        public static IDataReader ExecuteReaderByPage(IDbHelper dbHelper, int recordCount, int pageIndex, int pageSize, string sqlQuery, IDbDataParameter[] dbParameters, string sortExpression = null, string sortDire = null)
        {
            if (string.IsNullOrEmpty(sortExpression))
            {
                sortExpression = BaseBusinessLogic.FieldCreateOn;
            }
            if (string.IsNullOrEmpty(sortDire))
            {
                sortDire = " DESC";
            }
            string sqlCount = recordCount - ((pageIndex - 1) * pageSize) > pageSize ? pageSize.ToString() : (recordCount - ((pageIndex - 1) * pageSize)).ToString();
            // string sqlStart = (pageIndex * pageSize).ToString();
            // string sqlEnd = ((pageIndex + 1) * pageSize).ToString();
            string sqlStart = ((pageIndex - 1) * pageSize).ToString();
            string sqlEnd = (pageIndex * pageSize).ToString();

            string commandText = string.Empty;

            switch (dbHelper.CurrentDbType)
            {
                case CurrentDbType.SqlServer:
                case CurrentDbType.DB2:
                    sqlStart = ((pageIndex - 1) * pageSize).ToString();
                    sqlEnd = (pageIndex * pageSize).ToString();
                    commandText = "SELECT * FROM ( "
                               + "SELECT ROW_NUMBER() OVER(ORDER BY " + sortExpression + ") AS ROWNUM, "
                               + sqlQuery.Substring(7) + "  ) A "
                               + " WHERE ROWNUM > " + sqlStart + " AND ROWNUM < " + sqlEnd;
                    break;
                case CurrentDbType.Access:
                    if (sqlQuery.ToUpper().IndexOf("SELECT") >= 0)
                    {
                        sqlQuery = " (" + sqlQuery + ") ";
                    }
                    commandText = string.Format("SELECT * FROM (SELECT TOP {0} * FROM (SELECT TOP {1} * FROM {2} T ORDER BY {3} " + sortDire + ") T1 ORDER BY {4} DESC ) T2 ORDER BY {5} " + sortDire
                                    , sqlCount, sqlStart, sqlQuery, sortExpression, sortExpression, sortExpression);
                    break;
                case CurrentDbType.Oracle:
                    commandText = string.Format(@"SELECT T.*, ROWNUM RN 
                            FROM ({0} AND ROWNUM <= {1} ORDER BY {2}) T WHERE ROWNUM > {3}", sqlQuery, sqlEnd, sortExpression, sqlStart);
                    break;
                case CurrentDbType.MySql:
                    if (sqlQuery.ToUpper().IndexOf("SELECT") >= 0)
                    {
                        sqlQuery = " (" + sqlQuery + ") ";
                    }
                    sqlStart = ((pageIndex - 1) * pageSize).ToString();
                    sqlEnd = (pageIndex * pageSize).ToString();
                    commandText = string.Format("SELECT * FROM {0} ORDER BY {1} {2} LIMIT {3},{4}", sqlQuery, sortExpression, sortDire, sqlStart, sqlEnd);
                    break;
            }

            return dbHelper.ExecuteReader(commandText, dbParameters);
        }

        /// <summary>
        /// 分页获取指定数量的数据
        /// </summary>
        /// <param name="dbHelper">数据源</param>
        /// <param name="recordCount">获取多少条</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="sqlQuery"></param>
        /// <param name="sortExpression">排序字段</param>
        /// <param name="sortDire">排序</param>
        /// <returns></returns>
        public static List<TModel> ExecuteReaderByPage<TModel>(IDbHelper dbHelper, int recordCount, int pageIndex, int pageSize,
            string sqlQuery, IDbDataParameter[] dbParameters, string sortExpression = null, string sortDire = null) where TModel : new()
        {
            return
                ExecuteReaderByPage(dbHelper, recordCount, pageIndex, pageSize, sqlQuery, dbParameters, sortExpression,
                    sortDire).ToList<TModel>();
        }

        #endregion

        // Oracle GetDataTableByPage

        #region public static IDataReader ExecuteReaderByPage(IDbHelper dbHelper, string tableName, string selectField, int pageIndex, int pageSize, string conditions, string orderBy)
        /// <summary>
        /// Oracle 获取分页数据
        /// </summary>
        /// <param name="dbHelper">数据库连接</param>
        /// <param name="tableName">数据来源表名</param>
        /// <param name="selectField">选择字段</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页显示多少条</param>
        /// <param name="conditions">查询条件</param>
        /// <param name="orderBy">排序字段</param>
        /// <returns>数据表</returns>
        public static IDataReader ExecuteReaderByPage(IDbHelper dbHelper, string tableName, string selectField, int pageIndex, int pageSize, string conditions, string orderBy)
        {
            return ExecuteReaderByPage(dbHelper, tableName, selectField, pageIndex, pageSize, conditions, null, orderBy);
        }

        /// <summary>
        /// Oracle 获取分页数据
        /// </summary>
        /// <param name="dbHelper">数据库连接</param>
        /// <param name="tableName">数据来源表名</param>
        /// <param name="selectField">选择字段</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页显示多少条</param>
        /// <param name="conditions">查询条件</param>
        /// <param name="orderBy">排序字段</param>
        /// <returns>数据表</returns>
        public static List<TModel> ExecuteReaderByPage<TModel>(IDbHelper dbHelper, string tableName, string selectField,
            int pageIndex, int pageSize, string conditions, string orderBy) where TModel : new()
        {
            return
                ExecuteReaderByPage(dbHelper, tableName, selectField, pageIndex, pageSize, conditions, orderBy)
                    .ToList<TModel>();
        }

        #endregion

        #region public static IDataReader ExecuteReaderByPage(IDbHelper dbHelper, string tableName, string selectField, int pageIndex, int pageSize, string conditions, IDbDataParameter[] dbParameters, string orderBy, string currentIndex = null)
        /// <summary>
        /// Oracle 获取分页数据（防注入功能的）
        /// </summary>
        /// <param name="dbHelper">数据库连接</param>
        /// <param name="tableName">数据来源表名</param>
        /// <param name="selectField">选择字段</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页显示多少条</param>
        /// <param name="conditions">查询条件</param>
        /// <param name="dbParameters">查询参数</param>
        /// <param name="orderBy">排序字段</param>
        /// <returns>数据表</returns>
        public static IDataReader ExecuteReaderByPage(IDbHelper dbHelper, string tableName, string selectField, int pageIndex, int pageSize, string conditions, IDbDataParameter[] dbParameters, string orderBy, string currentIndex = null)
        {
            string sqlStart = ((pageIndex - 1) * pageSize).ToString();
            string sqlEnd = (pageIndex * pageSize).ToString();
            if (currentIndex == null)
            {
                currentIndex = string.Empty;
            }
            if (!string.IsNullOrEmpty(conditions))
            {
                conditions = "WHERE " + conditions;
            }
            string sqlQuery = string.Empty;

            if (dbHelper.CurrentDbType == CurrentDbType.Oracle)
            {
                if (!string.IsNullOrWhiteSpace(orderBy))
                {
                    orderBy = " ORDER BY " + orderBy;
                }
                //SELECT * FROM(SELECT ROWNUM RN,h.* FROM ((SELECT T.* FROM WULIAO_SUOYOUGONGSI T WHERE DENG_JI_GONG_SI_DAI_MA='02100' ORDER BY CreateOn desc )H)) zWHERE z.RN <=110  and z.RN >104;
                //原始的               
                //sqlQuery = string.Format("SELECT " + selectField + " FROM(SELECT ROWNUM RN, H.* FROM ((SELECT " + currentIndex + " * FROM {0} {1} {2} )H)) Z WHERE Z.RN <={3} AND Z.RN >{4}"
                //    , tableName, conditions, orderBy, sqlEnd, sqlStart);
                //宋彪修改 2014.8.8
                sqlQuery = string.Format("SELECT * FROM (SELECT ROWNUM RN, H.* FROM ((SELECT " + currentIndex + " " + selectField + " FROM {0} {1} {2} )H)) Z WHERE Z.RN <={3} AND Z.RN >{4} "
    , tableName, conditions, orderBy, sqlEnd, sqlStart);
                //sqlQuery = string.Format("SELECT " + selectField + " FROM (SELECT T.*, ROWNUM RN FROM (SELECT * FROM {0} {1} ORDER BY {2}) T WHERE ROWNUM <= {3}) WHERE RN > {4}"
                //    , tableName, conditions, orderby, sqlEnd, sqlStart);
            }
            else if (dbHelper.CurrentDbType == CurrentDbType.SqlServer)
            {
                sqlQuery = string.Format("SELECT * FROM (SELECT ROW_NUMBER() OVER(ORDER BY {0}) AS RowIndex, " + selectField + " FROM {1} {2}) AS PageTable WHERE RowIndex <={3} AND RowIndex >{4} "
                    , orderBy, tableName, conditions, sqlEnd, sqlStart);
            }
            else if (dbHelper.CurrentDbType == CurrentDbType.MySql
                || dbHelper.CurrentDbType == CurrentDbType.SQLite)
            {
                sqlQuery = string.Format("SELECT {0} FROM {1} {2} ORDER BY {3} LIMIT {4}, {5}", selectField, tableName, conditions, orderBy, sqlStart, pageSize);
            }

            //var dt = new DataTable(tableName);
            if (dbParameters != null && dbParameters.Length > 0)
            {
                return dbHelper.ExecuteReader(sqlQuery, dbParameters);
                //dt = dbHelper.Fill(sqlQuery, dbParameters);
            }
            else
            {
                return dbHelper.ExecuteReader(sqlQuery);
                //dt = dbHelper.Fill(sqlQuery);
            }
        }

        /// <summary>
        /// Oracle 获取分页数据（防注入功能的）
        /// </summary>
        /// <param name="dbHelper">数据库连接</param>
        /// <param name="tableName">数据来源表名</param>
        /// <param name="selectField">选择字段</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页显示多少条</param>
        /// <param name="conditions">查询条件</param>
        /// <param name="dbParameters">查询参数</param>
        /// <param name="orderBy">排序字段</param>
        /// <returns>数据表</returns>
        public static List<TModel> ExecuteReaderByPage<TModel>(IDbHelper dbHelper, string tableName, string selectField,
            int pageIndex, int pageSize, string conditions, IDbDataParameter[] dbParameters, string orderBy,
            string currentIndex = null) where TModel : new()
        {
            return
                ExecuteReaderByPage(dbHelper, tableName, selectField, pageIndex, pageSize, conditions, dbParameters,
                    orderBy, currentIndex).ToList<TModel>();
        }
        #endregion

        #region public static IDataReader ExecuteReaderByPage(IDbHelper dbHelper, out int recordCount, string tableName, string selectField, int pageIndex, int pageSize, string conditions, List<KeyValuePair<string, object>> dbParameters, string orderBy)
        /// <summary>
        /// 获取分页数据（防注入功能的） 
        /// 宋彪  2014-06-25 构造List<KeyValuePair<string, object>>比IDbDataParameter[]方便一些
        /// dbHelper.MakeParameters(dbParameters)--》IDbDataParameter[]
        /// </summary>
        /// <param name="recordCount">记录条数</param>
        /// <param name="dbHelper">dbHelper</param>
        /// <param name="tableName">数据来源表名</param>
        /// <param name="selectField">选择字段</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页显示多少条</param>
        /// <param name="conditions">查询条件</param>
        /// <param name="dbParameters">查询参数</param>
        /// <param name="orderBy">排序字段</param>
        /// <returns>数据表</returns>
        public static IDataReader ExecuteReaderByPage(IDbHelper dbHelper, out int recordCount, string tableName, string selectField, int pageIndex, int pageSize, string conditions, IDbDataParameter[] dbParameters, string orderBy)
        {
            IDataReader result = null;
            recordCount = 0;
            if (null != dbHelper)
            {
                recordCount = DbLogic.GetCount(dbHelper, tableName, conditions, dbParameters);
                result = DbLogic.ExecuteReaderByPage(dbHelper, tableName, selectField, pageIndex, pageSize, conditions, dbParameters, orderBy);
            }
            return result;
        }

        /// <summary>
        /// 获取分页数据（防注入功能的） 
        /// 宋彪  2014-06-25 构造List<KeyValuePair<string, object>>比IDbDataParameter[]方便一些
        /// dbHelper.MakeParameters(dbParameters)--》IDbDataParameter[]
        /// </summary>
        /// <param name="recordCount">记录条数</param>
        /// <param name="dbHelper">dbHelper</param>
        /// <param name="tableName">数据来源表名</param>
        /// <param name="selectField">选择字段</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页显示多少条</param>
        /// <param name="conditions">查询条件</param>
        /// <param name="dbParameters">查询参数</param>
        /// <param name="orderBy">排序字段</param>
        /// <returns>数据表</returns>
        public static List<TModel> ExecuteReaderByPage<TModel>(IDbHelper dbHelper, out int recordCount, string tableName,
            string selectField, int pageIndex, int pageSize, string conditions, IDbDataParameter[] dbParameters,
            string orderBy) where TModel : new()
        {
            return ExecuteReaderByPage(dbHelper, out recordCount, tableName, selectField, pageIndex, pageSize, conditions, dbParameters, orderBy).ToList<TModel>();
        }

        #endregion
    }
}