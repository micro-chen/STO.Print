//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd.  
//-----------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace DotNet.Business
{
    using DotNet.Utilities;

    /// <summary>
    /// SQLBuilder
    /// SQL语句生成器（适合简单的添加、删除、更新等语句，可以写出编译时强类型检查的效果）
    /// 
    /// 修改记录
    /// 
    ///     2013.04.01 版本：4.0 JiRiGaLa   改进性能，采用StringBuilder。
    ///     2012.03.17 版本：3.7 zhangyi    修改注释
    ///     2010.06.20 版本：3.1 JiRiGaLa	支持Oracle序列功能改进。
    ///     2010.06.13 版本：3.0 JiRiGaLa	改进为支持静态方法，不用数据库Open、Close的方式，AutoOpenClose开关。
    ///     2008.08.30 版本：2.3 JiRiGaLa	确认 BeginSelect 方法的正确性。
    ///     2008.08.29 版本：2.2 JiRiGaLa	改进 public string SetWhere(string targetFiled, Object[] targetValue) 方法。
    ///     2008.08.29 版本：2.1 JiRiGaLa	修正 BeginSelect、BeginInsert、BeginUpdate、BeginDelete。
    ///     2008.05.07 版本：2.0 JiRiGaLa	改进为多种数据库的支持类型。
    ///     2007.05.20 版本：1.8 JiRiGaLa	改进了OleDbCommand使其可以在多个事件穿插使用。
    ///     2006.02.22 版本：1.7 JiRiGaLa	改进了OleDbCommand使其可以在多个事件穿插使用。
    ///		2006.02.05 版本：1.6 JiRiGaLa	重新调整主键的规范化。
    ///		2006.01.20 版本：1.5 JiRiGaLa   修改主键,货币型的插入。
    ///		2005.12.29 版本：1.4 JiRiGaLa   修改主键,将公式的功能完善,提高效率。
    ///		2005.12.29 版本：1.3 JiRiGaLa   修改主键,将公式的功能完善,提高效率。
    ///		2005.08.08 版本：1.2 JiRiGaLa   修改主键，修改格式。
    ///		2005.12.30 版本：1.1 JiRiGaLa   数据库连接进行优化。
    ///		2005.12.29 版本：1.0 JiRiGaLa   主键创建。
    ///		
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2013.04.01</date>
    /// </author> 
    /// </summary>
    public partial class SQLBuilder
    {
        /// <summary>
        /// 是否采用自增量的方式
        /// </summary>
        public bool Identity = false;

        /// <summary>
        ///  是否需要返回主键
        /// </summary>
        public bool ReturnId = true;

        /// <summary>
        /// 主键
        /// </summary>
        public string PrimaryKey = "Id";

        private DbOperation sqlOperation = DbOperation.Update;

        public string CommandText = string.Empty;

        private string SelectSql = string.Empty;

        private string TableName = string.Empty;

        private StringBuilder InsertValue = null;

        private StringBuilder InsertField = null;

        private StringBuilder UpdateSql = null;

        private StringBuilder WhereSql = null;

        private CurrentDbType DbType = CurrentDbType.SqlServer;

        /// <summary>
        /// 获取前几条数据
        /// </summary>
        private int? TopN = null;

        /// <summary>
        /// 排序字段
        /// </summary>
        private string OrderBy = string.Empty;

        private IDbHelper DbHelper = null;

        public List<KeyValuePair<string, object>> DbParameters = new List<KeyValuePair<string, object>>();

        public SQLBuilder(CurrentDbType currentDbType)
        {
            this.DbType = currentDbType;
            this.DbParameters = new List<KeyValuePair<string, object>>();
        }

        public SQLBuilder(IDbHelper dbHelper)
            : this(dbHelper.CurrentDbType)
        {
            DbHelper = dbHelper;
        }

        public SQLBuilder(IDbHelper dbHelper, bool identity)
            : this(dbHelper)
        {
            Identity = identity;
        }

        public SQLBuilder(IDbHelper dbHelper, bool identity, bool returnId)
            : this(dbHelper)
        {
            Identity = identity;
            ReturnId = returnId;
        }

        #region private void Prepare() 获得数据库连接相关
        /// <summary>
        /// 获得数据库连接
        /// </summary>
        private void Prepare()
        {
            this.sqlOperation = DbOperation.Update;
            this.CommandText = string.Empty;
            this.TableName = string.Empty;
            this.InsertValue = new StringBuilder();
            this.InsertField = new StringBuilder();
            this.UpdateSql = new StringBuilder();
            this.WhereSql = new StringBuilder();

            // 2016-02-23 吉日嘎拉，提高性能，释放数据库连接，需要时再打开数据库连接，判断是否为空，要区别静态方法与动态调用方法
            /*
            if (DbHelper != null && !DbHelper.MustCloseConnection)
            {
                DbHelper.GetDbCommand().Parameters.Clear();
            }
            */
        }
        #endregion

        #region public void BeginSelect(string tableName) 开始查询
        /// <summary>
        /// 开始查询
        /// </summary>
        /// <param name="tableName">目标表</param>
        public void BeginSelect(string tableName)
        {
            Begin(tableName, DbOperation.Select);
        }
        #endregion

        /// <summary>
        /// 获取前几条数据
        /// </summary>
        /// <param name="topN">几条</param>
        public void SelectTop(int? topN)
        {
            this.TopN = topN;
        }

        #region public void BeginInsert(string tableName) 开始插入
        /// <summary>
        /// 开始插入
        /// </summary>
        /// <param name="tableName">目标表</param>
        public void BeginInsert(string tableName)
        {
            Begin(tableName, DbOperation.Insert);
        }
        #endregion

        #region public void BeginReplace(string tableName) 开始插入
        /// <summary>
        /// 更新替换
        /// </summary>
        /// <param name="tableName">目标表</param>
        public void BeginReplace(string tableName)
        {
            Begin(tableName, DbOperation.ReplaceInto);
        }
        #endregion

        #region public void BeginInsert(string tableName, bool identity) 开始插入 传入是否自增
        /// <summary>
        /// 开始插入  传入是否自增
        /// </summary>
        /// <param name="tableName">目标表</param>
        /// <param name="identity">自增量方式</param>
        public void BeginInsert(string tableName, bool identity)
        {
            Identity = identity;
            Begin(tableName, DbOperation.Insert);
        }
        #endregion

        #region public void BeginInsert(string tableName, string primaryKey) 开始插入 传入主键
        /// <summary>
        /// 开始插入 传入主键
        /// </summary>
        /// <param name="tableName">目标表</param>
        /// <param name="primaryKey">主键</param>
        public void BeginInsert(string tableName, string primaryKey)
        {
            PrimaryKey = primaryKey;
            Begin(tableName, DbOperation.Insert);
        }
        #endregion

        #region public BeginUpdate(string tableName) 开始更新
        /// <summary>
        /// 开始更新
        /// </summary>
        /// <param name="tableName">目标表</param>
        public void BeginUpdate(string tableName)
        {
            Begin(tableName, DbOperation.Update);
        }
        #endregion

        #region public void BeginDelete(string tableName) 开始删除
        /// <summary>
        /// 开始删除
        /// </summary>
        /// <param name="tableName">目标表</param>
        public void BeginDelete(string tableName)
        {
            Begin(tableName, DbOperation.Delete);
        }
        #endregion

        #region public void BeginInsert(string tableName,string primaryKey, bool identity) 开始插入 传入是否自增
        /// <summary>
        /// 开始插入  传入是否自增
        /// </summary>
        /// <param name="tableName">目标表</param>
        /// <param name="primaryKey">主键</param>
        /// <param name="identity">自增量方式</param>
        public void BeginInsert(string tableName, string primaryKey, bool identity)
        {
            Identity = identity;
            PrimaryKey = primaryKey;
            Begin(tableName, DbOperation.Insert);
        }
        #endregion

        #region private void Begin(string tableName, DbOperation dbOperation) 开始增删改查
        /// <summary>
        /// 开始查询语句
        /// </summary>
        /// <param name="tableName">目标表</param>
        /// <param name="dbOperation">语句操作类别</param>
        private void Begin(string tableName, DbOperation dbOperation)
        {
            // 写入调试信息
#if (DEBUG)
            int milliStart = Environment.TickCount;
            Trace.WriteLine(DateTime.Now.ToString(BaseSystemInfo.TimeFormat) + " :Begin: " + MethodBase.GetCurrentMethod().ReflectedType.Name + "." + MethodBase.GetCurrentMethod().Name);
#endif

            this.Prepare();
            this.TableName = tableName;
            this.sqlOperation = dbOperation;

            // 写入调试信息
#if (DEBUG)
            int milliEnd = Environment.TickCount;
            Trace.WriteLine(DateTime.Now.ToString(BaseSystemInfo.TimeFormat) + " Ticks: " + TimeSpan.FromMilliseconds(milliEnd - milliStart).ToString() + " :End: " + MethodBase.GetCurrentMethod().ReflectedType.Name + "." + MethodBase.GetCurrentMethod().Name);
#endif
        }
        #endregion


        #region public void SetFormula(string targetFiled, string formula, string relation) 设置公式
        /// <summary>
        /// 设置公式
        /// </summary>
        /// <param name="targetFiled">目标字段</param>
        /// <param name="targetFiled">目标字段</param>
        public void SetFormula(string targetFiled, string formula)
        {
            string relation = " = ";
            this.SetFormula(targetFiled, formula, relation);
        }
        /// <summary>
        /// 设置公式
        /// </summary>
        /// <param name="targetFiled">目标字段</param>
        /// <param name="targetFiled">目标字段</param>
        public void SetFormula(string targetFiled, string formula, string relation)
        {
            if (sqlOperation == DbOperation.Insert)
            {
                InsertField.Append(targetFiled + ", ");
                InsertValue.Append(formula + ", ");
            }
            if (sqlOperation == DbOperation.Update)
            {
                UpdateSql.Append(targetFiled + relation + formula + ", ");
            }
        }
        #endregion

        private string GetDbNow()
        {
            string result = string.Empty;
            if (DbHelper != null)
            {
                result = DbHelper.GetDbNow();
            }
            else
            {
                result = DotNet.Utilities.DbHelper.GetDbNow(DbType);
            }
            return result;
        }

        #region public void SetDBNow(string targetFiled) 设置为当前时间
        /// <summary>
        /// 设置为当前时间
        /// </summary>
        /// <param name="targetFiled">目标字段</param>
        public void SetDBNow(string targetFiled)
        {
            if (sqlOperation == DbOperation.Insert)
            {
                InsertField.Append(targetFiled + ", ");
                InsertValue.Append(GetDbNow() + ", ");
            }
            if (sqlOperation == DbOperation.Update)
            {
                UpdateSql.Append(targetFiled + " = " + GetDbNow() + ", ");
            }
        }
        #endregion

        #region public void SetNull(string targetFiled) 设置为Null值
        /// <summary>
        /// 设置为Null值
        /// </summary>
        /// <param name="targetFiled">目标字段</param>
        public void SetNull(string targetFiled)
        {
            this.SetValue(targetFiled, null);
        }
        #endregion

        #region public void SetValue(string targetFiled, object targetValue, string targetFiledName = null) 设置值
        /// <summary>
        /// 设置值
        /// </summary>
        /// <param name="targetFiled">目标字段</param>
        /// <param name="targetValue">值</param>
        /// <param name="targetFiledName">字段名</param>
        public void SetValue(string targetFiled, object targetValue, string targetFiledName = null)
        {
            if (targetFiledName == null)
            {
                targetFiledName = targetFiled;
            }
            switch (this.sqlOperation)
            {
                case DbOperation.Update:
                    if (targetValue == null)
                    {
                        this.UpdateSql.Append(targetFiled + " = Null, ");
                    }
                    else
                    {
                        // 判断数据库连接类型
                        this.UpdateSql.Append(targetFiled + " = " + DotNet.Utilities.DbHelper.GetParameter(this.DbType, targetFiledName) + ", ");
                        this.AddParameter(targetFiledName, targetValue);
                        //else
                        //{
                        //    this.UpdateSql.Append(targetFiled + " = '', ");
                        //}
                    }
                    break;
                case DbOperation.Insert:
                    // if (DbHelper.CurrentDbType == CurrentDbType.SqlServer)
                    // else if (DbHelper.CurrentDbType == CurrentDbType.Access)
                    // 自增量，不需要赋值
                    // if (this.Identity && targetFiled == this.PrimaryKey)
                    this.InsertField.Append(targetFiled + ", ");

                    if (targetValue == null)
                    {
                        this.InsertValue.Append(" Null, ");
                    }
                    else
                    {
                        this.InsertValue.Append(DotNet.Utilities.DbHelper.GetParameter(this.DbType, targetFiledName) + ", ");
                        this.AddParameter(targetFiledName, targetValue);
                    }
                    break;
            }
        }
        #endregion

        #region private void AddParameter(string targetFiled, object targetValue) 添加参数
        /// <summary>
        /// 添加参数
        /// </summary>
        /// <param name="targetFiled">目标字段</param>
        /// <param name="targetValue">值</param>
        private void AddParameter(string targetFiled, object targetValue)
        {
            this.DbParameters.Add(new KeyValuePair<string, object>(targetFiled, targetValue));
        }
        #endregion


        #region public string SetWhere(string sqlWhere) 设置条件
        /// <summary>
        /// 设置条件
        /// </summary>
        /// <param name="sqlWhere">目标字段</param>
        /// <returns>条件语句</returns>
        public void SetWhere(string sqlWhere)
        {
            if (WhereSql == null || WhereSql.Length == 0)
            {
                WhereSql = new StringBuilder(" WHERE ");
            }
            this.WhereSql.Append(sqlWhere);
            // return this.WhereSql;
        }
        #endregion

        #region public string SetWhere(string targetFiled, object targetValue, string targetFiledName = null, string relation = " AND ") 设置条件
        /// <summary>
        /// 设置条件
        /// </summary>
        /// <param name="targetFiled">目标字段</param>
        /// <param name="targetValue">值</param>
        /// <param name="relation">条件 AND OR</param>
        /// <returns>条件语句</returns>
        public void SetWhere(string targetFiled, object targetValue, string targetFiledName = null, string relation = " AND ")
        {
            if (string.IsNullOrEmpty(targetFiledName))
            {
                targetFiledName = targetFiled;
            }
            if (WhereSql.Length == 0)
            {
                WhereSql = new StringBuilder(" WHERE ");
            }
            else
            {
                WhereSql.Append(relation);
            }
            if (targetValue is Array)
            {
                // this.WhereSql.Append(targetFiled + " IN (" + string.Join(",", targetValue) + ")");
                this.WhereSql.Append(targetFiled + " IN (" + BaseBusinessLogic.ObjectsToList((object[])targetValue, "'") + ")");
                return;
            }
            // 这里需要对 null 进行处理
            if ((targetValue == null) || ((targetValue is string) && string.IsNullOrEmpty((string)targetValue)))
            {
                this.WhereSql.Append(targetFiled + " IS NULL ");
            }
            else
            {
                this.WhereSql.Append(targetFiled + " = " + DotNet.Utilities.DbHelper.GetParameter(this.DbType, targetFiledName));
                this.AddParameter(targetFiledName, targetValue);
            }
            // return this.WhereSql;
        }
        #endregion

        #region public string SetOrderBy(string orderBy) 设置排序顺序
        /// <summary>
        /// 设置排序顺序
        /// </summary>
        /// <param name="orderBy">排序顺序</param>
        /// <returns>排序</returns>
        public string SetOrderBy(string orderBy)
        {
            if (string.IsNullOrEmpty(OrderBy))
            {
                OrderBy = " ORDER BY ";
            }
            this.OrderBy += orderBy;
            return this.OrderBy;
        }
        #endregion

        /// <summary>
        /// 数据库中的随机排序功能实现
        /// </summary>
        /// <returns>随机排序函数</returns>
        public string SetOrderByRandom()
        {
            if (string.IsNullOrEmpty(OrderBy))
            {
                OrderBy = " ORDER BY ";
            }
            switch (DbHelper.CurrentDbType)
            {
                case CurrentDbType.Oracle:
                    this.OrderBy += "DBMS_RANDOM.VALUE()";
                    break;
                case CurrentDbType.SqlServer:
                case CurrentDbType.Access:
                    this.OrderBy += "NEWID()";
                    break;
                case CurrentDbType.MySql:
                    this.OrderBy += "Rand()";
                    break;
            }
            return this.OrderBy;
        }

        #region public int EndSelect() 结束查询
        /// <summary>
        /// 结束查询
        /// </summary>
        /// <returns>影响行数</returns>
        public DataTable EndSelect()
        {
            var dt = new DataTable(this.TableName);
            if (this.TopN != null)
            {
                switch (DbHelper.CurrentDbType)
                {
                    case CurrentDbType.Oracle:
                        // 这里还需要把条件进行优化
                        this.CommandText = "SELECT * FROM " + this.TableName + " WHERE ROWNUM <= " + this.TopN.ToString() + this.OrderBy;
                        break;
                    case CurrentDbType.SqlServer:
                    case CurrentDbType.Access:
                        this.CommandText = "SELECT TOP " + this.TopN.ToString() + " * FROM " + this.TableName + this.WhereSql + this.OrderBy;
                        break;
                    case CurrentDbType.MySql:
                        this.CommandText = "SELECT * FROM " + this.TableName + this.WhereSql + this.OrderBy + " LIMIT 1 , " + this.TopN;
                        break;
                }
            }
            else
            {
                this.CommandText = "SELECT * FROM " + this.TableName + this.WhereSql + this.OrderBy;
            }

            // 参数进行规范化
            List<IDbDataParameter> dbParameters = new List<IDbDataParameter>();
            foreach (var parameter in this.DbParameters)
            {
                dbParameters.Add(DbHelper.MakeParameter(parameter.Key, parameter.Value));
            }
            DbHelper.Fill(dt, this.CommandText, dbParameters.ToArray());
            // 清除查询参数
            this.DbParameters.Clear();
            return dt;
        }
        #endregion

        #region public int EndInsert() 结束插入
        /// <summary>
        /// 结束插入
        /// </summary>
        /// <returns>影响行数</returns>
        public int EndInsert()
        {
            return this.Execute();
        }
        #endregion

        #region public int EndReplace() 结束插入
        /// <summary>
        /// 结束插入
        /// </summary>
        /// <returns>影响行数</returns>
        public int EndReplace()
        {
            return this.Execute();
        }
        #endregion

        #region public int EndUpdate() 结束更新
        /// <summary>
        /// 结束更新
        /// </summary>
        /// <returns>影响行数</returns>
        public int EndUpdate()
        {
            return this.Execute();
        }
        #endregion

        #region public int EndDelete() 结束删除
        /// <summary>
        /// 结束删除
        /// </summary>
        /// <returns>影响行数</returns>
        public int EndDelete()
        {
            return this.Execute();
        }
        #endregion

        /// <summary>
        /// 准备生成sql语句
        /// </summary>
        public string PrepareCommand()
        {
            if (this.sqlOperation == DbOperation.Insert || this.sqlOperation == DbOperation.ReplaceInto)
            {
                this.InsertField = new StringBuilder(this.InsertField.ToString().Substring(0, InsertField.Length - 2));
                this.InsertValue = new StringBuilder(this.InsertValue.ToString().Substring(0, InsertValue.Length - 2));
                this.CommandText = "INSERT INTO " + this.TableName + "(" + InsertField + ") VALUES(" + InsertValue + ") ";
                if (this.sqlOperation == DbOperation.ReplaceInto)
                {
                    this.CommandText = "REPLACE INTO " + this.TableName + "(" + InsertField + ") VALUES(" + InsertValue + ") ";
                }
                // 采用了自增量的方式
                if (this.Identity)
                {
                    switch (this.DbType)
                    {
                        case CurrentDbType.SqlServer:
                            // 需要返回主键
                            if (this.ReturnId)
                            {
                                this.CommandText += "; SELECT SCOPE_IDENTITY(); ";
                            }
                            break;
                        case CurrentDbType.Access:
                            // 需要返回主键
                            if (this.ReturnId)
                            {
                                this.CommandText += "; SELECT @@identity AS ID FROM " + this.TableName + "; ";
                            }
                            break;
                        // Mysql 返回自增主键 胡流东
                        case CurrentDbType.MySql:
                            if (this.ReturnId)
                            {
                                this.CommandText += "; SELECT LAST_INSERT_ID(); ";
                            }
                            break;
                    }
                }
            }
            else if (this.sqlOperation == DbOperation.Update)
            {
                this.UpdateSql = new StringBuilder(this.UpdateSql.ToString().Substring(0, UpdateSql.Length - 2));
                this.CommandText = "UPDATE " + this.TableName + " SET " + this.UpdateSql + this.WhereSql;
            }
            else if (this.sqlOperation == DbOperation.Delete)
            {
                this.CommandText = "DELETE FROM " + this.TableName + this.WhereSql;
            }
            else if (this.sqlOperation == DbOperation.Select)
            {
                this.CommandText = "SELECT * FROM " + this.TableName + this.WhereSql;
            }

            return this.CommandText;
        }

        #region private int Execute() 执行语句
        /// <summary>
        /// 执行语句
        /// </summary>
        /// <returns>影响行数</returns>
        private int Execute()
        {
            // 处理返回值
            int result = 0;

            // 准备生成sql语句
            this.PrepareCommand();

            // 参数进行规范化
            List<IDbDataParameter> dbParameters = new List<IDbDataParameter>();
            foreach (var parameter in this.DbParameters)
            {
                dbParameters.Add(DbHelper.MakeParameter(parameter.Key, parameter.Value));
            }

            if (this.Identity && this.sqlOperation == DbOperation.Insert && (DbHelper.CurrentDbType == CurrentDbType.SqlServer || DbHelper.CurrentDbType == CurrentDbType.MySql))
            {
                // 读取返回值
                if (this.ReturnId)
                {
                    result = int.Parse(DbHelper.ExecuteScalar(this.CommandText, dbParameters.ToArray()).ToString());
                }
                else
                {
                    // 执行语句
                    result = DbHelper.ExecuteNonQuery(this.CommandText, dbParameters.ToArray());
                }
            }
            else
            {
                // 执行语句
                result = DbHelper.ExecuteNonQuery(this.CommandText, dbParameters.ToArray());
            }

            if (!DbHelper.MustCloseConnection)
            {
            }
            // 清除查询参数
            this.DbParameters.Clear();
            
            // 写入日志 
            SQLTrace.WriteLog(this.CommandText, dbParameters.ToArray());

            return result;
        }
        #endregion
    }
}