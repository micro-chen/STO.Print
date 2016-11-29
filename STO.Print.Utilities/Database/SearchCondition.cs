using System;
using System.Collections;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;

namespace STO.Print.Utilities
{
    /// <summary>
    /// 查询条件组合辅助类
    /// </summary>
    public class SearchCondition
    {
        #region 添加查询条件

        private Hashtable conditionTable = new Hashtable();

        /// <summary>
        /// 查询条件列表
        /// </summary>
        public Hashtable ConditionTable
        {
            get { return this.conditionTable; }
        }

        /// <summary>
        /// 为查询添加条件
        /// <example>
        /// 用法一：
        /// SearchCondition searchObj = new SearchCondition();
        /// searchObj.AddCondition("Test", 1, SqlOperator.NotEqual);
        /// searchObj.AddCondition("Test2", "Test2Value", SqlOperator.Like);
        /// string conditionSql = searchObj.BuildConditionSql();
        /// 
        /// 用法二：AddCondition函数可以串起来添加多个条件
        /// SearchCondition searchObj = new SearchCondition();
        /// searchObj.AddCondition("Test", 1, SqlOperator.NotEqual).AddCondition("Test2", "Test2Value", SqlOperator.Like);
        /// string conditionSql = searchObj.BuildConditionSql();
        /// </example>
        /// </summary>
        /// <param name="fielName">字段名称</param>
        /// <param name="fieldValue">字段值</param>
        /// <param name="sqlOperator">SqlOperator枚举类型</param>
        /// <returns>增加条件后的Hashtable</returns>
        public SearchCondition AddCondition(string fielName, object fieldValue, SqlOperator sqlOperator)
        {
            this.conditionTable.Add(System.Guid.NewGuid()/*fielName*/, new SearchInfo(fielName, fieldValue, sqlOperator));
            return this;
        }

        /// <summary>
        /// 为查询添加条件
        /// <example>
        /// 用法一：
        /// SearchCondition searchObj = new SearchCondition();
        /// searchObj.AddCondition("Test", 1, SqlOperator.NotEqual, false);
        /// searchObj.AddCondition("Test2", "Test2Value", SqlOperator.Like, true);
        /// string conditionSql = searchObj.BuildConditionSql();
        /// 
        /// 用法二：AddCondition函数可以串起来添加多个条件
        /// SearchCondition searchObj = new SearchCondition();
        /// searchObj.AddCondition("Test", 1, SqlOperator.NotEqual, false).AddCondition("Test2", "Test2Value", SqlOperator.Like, true);
        /// string conditionSql = searchObj.BuildConditionSql();
        /// </example>
        /// </summary>
        /// <param name="fielName">字段名称</param>
        /// <param name="fieldValue">字段值</param>
        /// <param name="sqlOperator">SqlOperator枚举类型</param>
        /// <param name="excludeIfEmpty">如果字段为空或者Null则不作为查询条件</param>
        /// <returns></returns>
        public SearchCondition AddCondition(string fielName, object fieldValue, SqlOperator sqlOperator, bool excludeIfEmpty)
        {
            this.conditionTable.Add(System.Guid.NewGuid()/*fielName*/, new SearchInfo(fielName, fieldValue, sqlOperator, excludeIfEmpty));
            return this;
        }

        /// <summary>
        /// 将多个条件分组归类作为一个条件来查询，
        /// 如需构造一个括号内的条件 ( Test = "AA1" OR Test = "AA2")
        /// </summary>
        /// <param name="fielName">字段名称</param>
        /// <param name="fieldValue">字段值</param>
        /// <param name="sqlOperator">SqlOperator枚举类型</param>
        /// <param name="excludeIfEmpty">如果字段为空或者Null则不作为查询条件</param>
        /// <param name="groupName">分组的名称，如需构造一个括号内的条件 ( Test = "AA1" OR Test = "AA2"), 定义一个组名集中条件</param>
        /// <returns></returns>
        public SearchCondition AddCondition(string fielName, object fieldValue, SqlOperator sqlOperator,
            bool excludeIfEmpty, string groupName)
        {
            this.conditionTable.Add(System.Guid.NewGuid()/*fielName*/, new SearchInfo(fielName, fieldValue, sqlOperator, excludeIfEmpty, groupName));
            return this;
        } 

        #endregion


        /// <summary>
        /// 根据对象构造相关的条件语句（不使用参数），如返回的语句是:
        /// <![CDATA[
        /// Where (1=1)  AND Test4  <  'Value4' AND Test6  >=  'Value6' AND Test7  <=  'value7' AND Test  <>  '1' AND Test5  >  'Value5' AND Test2  Like  '%Value2%' AND Test3  =  'Value3'
        /// ]]>
        /// </summary>
        /// <returns></returns> 
        public string BuildConditionSql(DatabaseType dbType)
        {
            string sql = " Where (1=1) ";
            string fieldName = string.Empty;
            SearchInfo searchInfo = null;

            StringBuilder sb = new StringBuilder();
            sql += BuildGroupCondiction(dbType);

            foreach (DictionaryEntry de in this.conditionTable)
            {
                searchInfo = (SearchInfo)de.Value;
                TypeCode typeCode = Type.GetTypeCode(searchInfo.FieldValue.GetType());

                //如果选择ExcludeIfEmpty为True,并且该字段为空值的话,跳过
                if (searchInfo.ExcludeIfEmpty &&
                    (searchInfo.FieldValue == null || string.IsNullOrEmpty(searchInfo.FieldValue.ToString())))
                {
                    continue;
                }

                //只有组别名称为空才继续，即正常的sql条件
                if (string.IsNullOrEmpty(searchInfo.GroupName))
                {
                    if (searchInfo.SqlOperator == SqlOperator.Like)
                    {
                        sb.AppendFormat(" AND {0} {1} '{2}'", searchInfo.FieldName,
                            this.ConvertSqlOperator(searchInfo.SqlOperator), string.Format("%{0}%", searchInfo.FieldValue));
                    }
                    else if (searchInfo.SqlOperator == SqlOperator.NotLike)
                    {
                        sb.AppendFormat(" AND {0} {1} '{2}'", searchInfo.FieldName,
                            this.ConvertSqlOperator(searchInfo.SqlOperator), string.Format("%{0}%", searchInfo.FieldValue));
                    } 
                    else if (searchInfo.SqlOperator == SqlOperator.LikeStartAt)
                    {
                        sb.AppendFormat(" AND {0} {1} '{2}'", searchInfo.FieldName,
                            this.ConvertSqlOperator(searchInfo.SqlOperator), string.Format("{0}%", searchInfo.FieldValue));
                    }
                    else if (searchInfo.SqlOperator == SqlOperator.In)
                    {
                        sb.AppendFormat(" AND {0} {1} {2}", searchInfo.FieldName,
                            this.ConvertSqlOperator(searchInfo.SqlOperator), string.Format("({0})", searchInfo.FieldValue));
                    }
                    else
                    {
                        if (dbType == DatabaseType.Oracle)
                        {
                            #region 特殊Oracle操作
                            if (IsDate(searchInfo.FieldValue.ToString()))
                            {
                                sb.AppendFormat(" AND {0} {1} to_date('{2}','YYYY-MM-dd')", searchInfo.FieldName,
                                    this.ConvertSqlOperator(searchInfo.SqlOperator), searchInfo.FieldValue);
                            }
                            else if (IsDateHourMinute(searchInfo.FieldValue.ToString()))
                            {
                                sb.AppendFormat(" AND {0} {1} to_date('{2}','YYYY-MM-dd HH:mi')", searchInfo.FieldName,
                                    this.ConvertSqlOperator(searchInfo.SqlOperator), searchInfo.FieldValue);
                            }
                            else if (!searchInfo.ExcludeIfEmpty)
                            {
                                //如果要进行空值查询的时候
                                if (searchInfo.SqlOperator == SqlOperator.Equal)
                                {
                                    sb.AppendFormat(" AND ({0} is null or {0}='')", searchInfo.FieldName);
                                }
                                else if (searchInfo.SqlOperator == SqlOperator.NotEqual)
                                {
                                    sb.AppendFormat(" AND {0} is not null", searchInfo.FieldName);
                                }
                            }
                            else
                            {
                                sb.AppendFormat(" AND {0} {1} '{2}'", searchInfo.FieldName,
                                    this.ConvertSqlOperator(searchInfo.SqlOperator), searchInfo.FieldValue);
                            }
                            #endregion
                        }
                        else if (dbType == DatabaseType.Access)
                        {
                            #region 特殊Access操作
                            if (searchInfo.SqlOperator == SqlOperator.Equal &&
                                typeCode == TypeCode.String && string.IsNullOrEmpty(searchInfo.FieldValue.ToString()))
                            {
                                sb.AppendFormat(" AND ({0} {1} '{2}' OR {0} IS NULL)", searchInfo.FieldName,
                                    this.ConvertSqlOperator(searchInfo.SqlOperator), searchInfo.FieldValue);
                            }
                            else
                            {
                                if (typeCode == TypeCode.DateTime)
                                {
                                    sb.AppendFormat(" AND {0} {1} #{2}#", searchInfo.FieldName,
                                        this.ConvertSqlOperator(searchInfo.SqlOperator), searchInfo.FieldValue);
                                }
                                else if (typeCode == TypeCode.Byte || typeCode == TypeCode.Decimal || typeCode == TypeCode.Double ||
                                    typeCode == TypeCode.Int16 || typeCode == TypeCode.Int32 || typeCode == TypeCode.Int64 ||
                                    typeCode == TypeCode.SByte || typeCode == TypeCode.Single || typeCode == TypeCode.UInt16 ||
                                    typeCode == TypeCode.UInt32 || typeCode == TypeCode.UInt64)
                                {
                                    //数值类型操作
                                    sb.AppendFormat(" AND {0} {1} {2}", searchInfo.FieldName,
                                        this.ConvertSqlOperator(searchInfo.SqlOperator), searchInfo.FieldValue);
                                }
                                else
                                {
                                    sb.AppendFormat(" AND {0} {1} '{2}'", searchInfo.FieldName,
                                        this.ConvertSqlOperator(searchInfo.SqlOperator), searchInfo.FieldValue);
                                }
                            }
                            #endregion
                        }
                        else //if (dbType == DatabaseType.SqlServer)
                        {
                            sb.AppendFormat(" AND {0} {1} '{2}'", searchInfo.FieldName,
                                this.ConvertSqlOperator(searchInfo.SqlOperator), searchInfo.FieldValue);
                        }
                    }
                }
            }

            sql += sb.ToString();

            return sql;
        }

        /// <summary>
        /// 建立分组条件
        /// </summary>
        /// <returns></returns>
        private string BuildGroupCondiction(DatabaseType dbType)
        {
            Hashtable ht = GetGroupNames();
            SearchInfo searchInfo = null;
            StringBuilder sb = new StringBuilder();
            string sql = string.Empty;
            string tempSql = string.Empty;

            foreach (string groupName in ht.Keys)
            {
                sb = new StringBuilder();
                tempSql = " AND ({0})";
                foreach (DictionaryEntry de in this.conditionTable)
                {
                    searchInfo = (SearchInfo)de.Value;
                    TypeCode typeCode = Type.GetTypeCode(searchInfo.FieldValue.GetType());

                    //如果选择ExcludeIfEmpty为True,并且该字段为空值的话,跳过
                    if (searchInfo.ExcludeIfEmpty && 
                        (searchInfo.FieldValue == null || string.IsNullOrEmpty(searchInfo.FieldValue.ToString())) )
                    {
                        continue;
                    }

                    if (groupName.Equals(searchInfo.GroupName, StringComparison.OrdinalIgnoreCase))
                    {
                        if (searchInfo.SqlOperator == SqlOperator.Like)
                        {
                            sb.AppendFormat(" OR {0} {1} '{2}'", searchInfo.FieldName,
                                this.ConvertSqlOperator(searchInfo.SqlOperator), string.Format("%{0}%", searchInfo.FieldValue));
                        }
                        else if (searchInfo.SqlOperator == SqlOperator.NotLike)
                        {
                            sb.AppendFormat(" OR {0} {1} '{2}'", searchInfo.FieldName,
                                this.ConvertSqlOperator(searchInfo.SqlOperator), string.Format("%{0}%", searchInfo.FieldValue));
                        }  
                        else if (searchInfo.SqlOperator == SqlOperator.LikeStartAt)
                        {
                            sb.AppendFormat(" OR {0} {1} '{2}'", searchInfo.FieldName,
                                this.ConvertSqlOperator(searchInfo.SqlOperator), string.Format("{0}%", searchInfo.FieldValue));
                        }
                        else
                        {
                            if (dbType == DatabaseType.Oracle)
                            {
                                #region Oracle分组
                                if (IsDate(searchInfo.FieldValue.ToString()))
                                {
                                    sb.AppendFormat(" OR {0} {1} to_date('{2}','YYYY-MM-dd')", searchInfo.FieldName,
                                    this.ConvertSqlOperator(searchInfo.SqlOperator), searchInfo.FieldValue);
                                }
                                else if (IsDateHourMinute(searchInfo.FieldValue.ToString()))
                                {
                                    sb.AppendFormat(" OR {0} {1} to_date('{2}','YYYY-MM-dd HH:mi')", searchInfo.FieldName,
                                    this.ConvertSqlOperator(searchInfo.SqlOperator), searchInfo.FieldValue);
                                }
                                else if (!searchInfo.ExcludeIfEmpty)
                                {
                                    //如果要进行空值查询的时候
                                    if (searchInfo.SqlOperator == SqlOperator.Equal)
                                    {
                                        sb.AppendFormat(" OR ({0} is null or {0}='')", searchInfo.FieldName);
                                    }
                                    else if (searchInfo.SqlOperator == SqlOperator.NotEqual)
                                    {
                                        sb.AppendFormat(" OR {0} is not null", searchInfo.FieldName);
                                    }
                                }
                                else
                                {
                                    sb.AppendFormat(" OR {0} {1} '{2}'", searchInfo.FieldName,
                                        this.ConvertSqlOperator(searchInfo.SqlOperator), searchInfo.FieldValue);
                                } 
                                #endregion
                            }
                            else if (dbType == DatabaseType.Access)
                            {
                                #region Access分组
                                if (typeCode == TypeCode.DateTime)
                                {
                                    sb.AppendFormat(" OR {0} {1} #{2}#", searchInfo.FieldName,
                                        this.ConvertSqlOperator(searchInfo.SqlOperator), searchInfo.FieldValue);
                                }
                                else if (typeCode == TypeCode.Byte || typeCode == TypeCode.Decimal || typeCode == TypeCode.Double ||
                                        typeCode == TypeCode.Int16 || typeCode == TypeCode.Int32 || typeCode == TypeCode.Int64 ||
                                        typeCode == TypeCode.SByte || typeCode == TypeCode.Single || typeCode == TypeCode.UInt16 ||
                                        typeCode == TypeCode.UInt32 || typeCode == TypeCode.UInt64)
                                {
                                    //数值类型操作
                                    sb.AppendFormat(" OR {0} {1} {2}", searchInfo.FieldName,
                                        this.ConvertSqlOperator(searchInfo.SqlOperator), searchInfo.FieldValue);
                                }
                                else
                                {
                                    sb.AppendFormat(" OR {0} {1} '{2}'", searchInfo.FieldName,
                                        this.ConvertSqlOperator(searchInfo.SqlOperator), searchInfo.FieldValue);
                                } 
                                #endregion
                            }
                            else //if (dbType == DatabaseType.SqlServer)
                            {
                                #region SqlServer分组
                                if (searchInfo.SqlOperator == SqlOperator.Like)
                                {
                                    sb.AppendFormat(" OR {0} {1} '{2}'", searchInfo.FieldName,
                                        this.ConvertSqlOperator(searchInfo.SqlOperator), string.Format("%{0}%", searchInfo.FieldValue));
                                }
                                else
                                {
                                    sb.AppendFormat(" OR {0} {1} '{2}'", searchInfo.FieldName,
                                        this.ConvertSqlOperator(searchInfo.SqlOperator), searchInfo.FieldValue);
                                } 
                                #endregion
                            }
                        }
                    }
                }

                if(!string.IsNullOrEmpty(sb.ToString()))
                {
                    tempSql = string.Format(tempSql, sb.ToString().Substring(3));//从第一个Or开始位置
                    sql += tempSql;
                }
            }

            return sql;
        }

        /// <summary>
        /// 获取给定条件集合的组别对象集合
        /// </summary>
        /// <returns></returns>
        private Hashtable GetGroupNames()
        {
            Hashtable htGroupNames = new Hashtable();
            SearchInfo searchInfo = null;
            foreach (DictionaryEntry de in this.conditionTable)
            {
                searchInfo = (SearchInfo)de.Value;
                if (!string.IsNullOrEmpty(searchInfo.GroupName) && !htGroupNames.Contains(searchInfo.GroupName))
                {
                    htGroupNames.Add(searchInfo.GroupName, searchInfo.GroupName);
                }
            }

            return htGroupNames;
        }

        ///// <summary>
        ///// 创建用于Enterprise Library的DbCommand对象。
        ///// 该对象包含了可以运行的参数化语句和参数列表。
        ///// <example>
        ///// 函数用法如下：
        ///// <code>
        ///// <para>
        ///// Database db = DatabaseFactory.CreateDatabase();
        ///// SearchCondition searchObj = new SearchCondition();
        ///// searchObj.AddCondition("Name", "测试" , SqlOperator.Like)
        /////       .AddCondition("ID", 1, SqlOperator.MoreThanOrEqual);
        ///// DbCommand dbComand = searchObj.BuildDbCommand(db, "select Comments from Test", " Order by Name");
        ///// using (IDataReader dr = db.ExecuteReader(dbComand))
        ///// {
        /////     while (dr.Read())
        /////      {
        /////         this.txtSql.Text += "\r\n" + dr["Comments"].ToString();
        /////      }
        /////  } 
        ///// </para>
        ///// 		</code>
        ///// 	</example>
        ///// </summary>
        ///// <remarks>Enterprise Library的DbCommand对象</remarks>
        ///// <param name="db">Database对象</param>
        ///// <param name="mainSql">除了Where条件和排序语句的主Sql语句</param>
        ///// <param name="orderSql">排序语句</param>
        ///// <returns>Enterprise Library的DbCommand对象</returns>
        //public DbCommand BuildDbCommand(Database db, string mainSql, string orderSql)
        //{
        //    string sql = " Where (1=1) ";
        //    string fieldName = string.Empty;
        //    SearchInfo searchInfo = null;
        //    StringBuilder sb = new StringBuilder();

        //    foreach (DictionaryEntry de in this.ConditionTable)
        //    {
        //        searchInfo = (SearchInfo)de.Value;

        //        //如果选择ExcludeIfEmpty为True,并且该字段为空值的话,跳过
        //        if (searchInfo.ExcludeIfEmpty &&
        //            (searchInfo.FieldValue == null || string.IsNullOrEmpty(searchInfo.FieldValue.ToString())))
        //        {
        //            continue;
        //        }

        //        sb.AppendFormat(" AND {0} {1} @{0} ", searchInfo.FieldName, this.ConvertSqlOperator(searchInfo.SqlOperator));
        //    }

        //    sql += sb.ToString();
        //    sql = mainSql + sql + orderSql;

        //    DbCommand dbCommand = db.GetSqlStringCommand(sql);
        //    foreach (DictionaryEntry de in this.ConditionTable)
        //    {
        //        searchInfo = (SearchInfo)de.Value;

        //        //如果选择ExcludeIfEmpty为True,并且该字段为空值的话,跳过
        //        if (searchInfo.ExcludeIfEmpty && string.IsNullOrEmpty((string)searchInfo.FieldValue))
        //        {
        //            continue;
        //        }

        //        if (searchInfo.SqlOperator == SqlOperator.Like)
        //        {
        //            if ( !string.IsNullOrEmpty(searchInfo.FieldValue.ToString()) )
        //                db.AddInParameter(dbCommand, searchInfo.FieldName,
        //                    this.GetFieldDbType(searchInfo.FieldValue), string.Format("%{0}%", searchInfo.FieldValue));
        //        }
        //        else
        //        {
        //            db.AddInParameter(dbCommand, searchInfo.FieldName,
        //                this.GetFieldDbType(searchInfo.FieldValue), searchInfo.FieldValue);
        //        }
        //    }

        //    return dbCommand;
        //}


        #region 辅助函数

        /// <summary>
        /// 转换枚举类型为对应的Sql语句操作符号
        /// </summary>
        /// <param name="sqlOperator">SqlOperator枚举对象</param>
        /// <returns><![CDATA[对应的Sql语句操作符号（如 ">" "<>" ">=")]]></returns>
        private string ConvertSqlOperator(SqlOperator sqlOperator)
        {
            string stringOperator = " = ";
            switch (sqlOperator)
            {
                case SqlOperator.Equal:
                    stringOperator = " = ";
                    break;
                case SqlOperator.LessThan:
                    stringOperator = " < ";
                    break;
                case SqlOperator.LessThanOrEqual:
                    stringOperator = " <= ";
                    break;
                case SqlOperator.Like:
                    stringOperator = " Like ";
                    break;
                case SqlOperator.NotLike:
                    stringOperator = " NOT Like ";
                    break;
                case SqlOperator.LikeStartAt:
                    stringOperator = " Like ";
                    break;
                case SqlOperator.MoreThan:
                    stringOperator = " > ";
                    break;
                case SqlOperator.MoreThanOrEqual:
                    stringOperator = " >= ";
                    break;
                case SqlOperator.NotEqual:
                    stringOperator = " <> ";
                    break;
                case SqlOperator.In:
                    stringOperator = " in ";
                    break;
                default:
                    break;
            }

            return stringOperator;
        }

        /// <summary>
        /// 根据传入对象的值类型获取其对应的DbType类型
        /// </summary>
        /// <param name="fieldValue">对象的值</param>
        /// <returns>DbType类型</returns>
        private DbType GetFieldDbType(object fieldValue)
        {
            DbType type = DbType.String;

            switch (fieldValue.GetType().ToString())
            {
                case "System.Int16":
                    type = DbType.Int16;
                    break;
                case "System.UInt16":
                    type = DbType.UInt16;
                    break;
                case "System.Single":
                    type = DbType.Single;
                    break;
                case "System.UInt32":
                    type = DbType.UInt32;
                    break;
                case "System.Int32":
                    type = DbType.Int32;
                    break;
                case "System.UInt64":
                    type = DbType.UInt64;
                    break;
                case "System.Int64":
                    type = DbType.Int64;
                    break;
                case "System.String":
                    type = DbType.String;
                    break;
                case "System.Double":
                    type = DbType.Double;
                    break;
                case "System.Decimal":
                    type = DbType.Decimal;
                    break;
                case "System.Byte":
                    type = DbType.Byte;
                    break;
                case "System.Boolean":
                    type = DbType.Boolean;
                    break;
                case "System.DateTime":
                    type = DbType.DateTime;
                    break;
                case "System.Guid":
                    type = DbType.Guid;
                    break;
                default:
                    break;
            }
            return type;
        }

        /// <summary>
        /// 判断输入的字符是否为日期
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        internal bool IsDate(string strValue)
        {
            return Regex.IsMatch(strValue, @"^((\d{2}(([02468][048])|([13579][26]))[\-\/\s]?((((0?[13578])|(1[02]))[\-\/\s]?((0?[1-9])|([1-2][0-9])|(3[01])))|(((0?[469])|(11))[\-\/\s]?((0?[1-9])|([1-2][0-9])|(30)))|(0?2[\-\/\s]?((0?[1-9])|([1-2][0-9])))))|(\d{2}(([02468][1235679])|([13579][01345789]))[\-\/\s]?((((0?[13578])|(1[02]))[\-\/\s]?((0?[1-9])|([1-2][0-9])|(3[01])))|(((0?[469])|(11))[\-\/\s]?((0?[1-9])|([1-2][0-9])|(30)))|(0?2[\-\/\s]?((0?[1-9])|(1[0-9])|(2[0-8]))))))");
        }

        /// <summary>
        /// 判断输入的字符是否为日期,如2004-07-12 14:25|||1900-01-01 00:00|||9999-12-31 23:59
        /// </summary>
        /// <param name="strValue"></param>
        /// <returns></returns>
        internal bool IsDateHourMinute(string strValue)
        {
            return Regex.IsMatch(strValue, @"^(19[0-9]{2}|[2-9][0-9]{3})-((0(1|3|5|7|8)|10|12)-(0[1-9]|1[0-9]|2[0-9]|3[0-1])|(0(4|6|9)|11)-(0[1-9]|1[0-9]|2[0-9]|30)|(02)-(0[1-9]|1[0-9]|2[0-9]))\x20(0[0-9]|1[0-9]|2[0-3])(:[0-5][0-9]){1}$");
        }

        #endregion
    }
}
