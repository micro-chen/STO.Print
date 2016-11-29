//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2015 , STO TECH, Ltd. 
//-----------------------------------------------------------------

using System;
using System.Collections.Specialized;
using System.Data;
using System.Net;
using System.Text;

namespace STO.Print.Synchronous
{
    using DotNet.Business;
    using DotNet.Utilities;
    using Newtonsoft.Json;

    /// <summary>
    /// Synchronous.cs
    /// 数据同步程序
    ///		
    /// 修改记录
    /// 
    ///     2015.05.19 版本：3.1 YangHengLian  删除客户端的同步数据、因为主数据表里需要加索引、唯一约束等。
    ///     2015.05.14 版本：3.0 YangHengLian  客户端不直接执行SQL语句了。
    ///     2015.05.04 版本：2.0 YangHengLian  按事务方式同步客户端数据。
    ///     2015.03.23 版本：1.0 YangHengLian  添加项目详细资料功能页面编写。
    ///		
    /// <author>
    ///		<name>YangHengLian</name>
    ///		<date>2015.05.19</date>
    /// </author> 
    /// </summary>
    public partial class Synchronous
    {
        /// <summary>
        /// 从服务器同步数据
        /// </summary>
        /// <param name="fromDataBase"></param>
        /// <param name="tableName"></param>
        /// <param name="primaryKeys"></param>
        /// <param name="modifiedOn"></param>
        /// <param name="toDataBase"></param>
        /// <param name="toTableName"></param>
        /// <param name="topLimit"></param>
        /// <param name="delete"></param>
        /// <returns></returns>
        public static int SynchronousTable(string fromDataBase, string tableName, string[] primaryKeys, DateTime? modifiedOn, string toDataBase, string toTableName, int topLimit, bool delete = true)
        {
            const int result = 0;
            string dbConnection = string.Empty;
            string sqLiteDb = string.Empty;
            if (toDataBase.Equals("Bill"))
            {
                sqLiteDb = System.Windows.Forms.Application.StartupPath + @"\DataBase\STO.Bill.db";
                dbConnection = "Data Source={StartupPath}/DataBase/STO.BILL.db;Pooling=true;FailIfMissing=false;Password=ZTO20149988";
                dbConnection = dbConnection.Replace("{StartupPath}", System.Windows.Forms.Application.StartupPath);
            }
            if (!System.IO.File.Exists(sqLiteDb))
            {
                return result;
            }
            return SynchronousTable(fromDataBase, tableName, primaryKeys, modifiedOn, CurrentDbType.SQLite, dbConnection, toTableName, topLimit, delete);
        }

        /// <summary>
        /// 远程数据同步的工具类
        /// </summary>
        /// <param name="fromDataBase">从服务器的哪个数据库获取数据</param>
        /// <param name="tableName">同步哪个表</param>
        /// <param name="primaryKeys">表的主键是什么</param>
        /// <param name="modifiedOn">同步的更新时间</param>
        /// <param name="toDataBaseDbType">同步到本地什么类型的数据库里？</param>
        /// <param name="dbConnection">同步的目标数据库连接方式？</param>
        /// <param name="toTableName">本地表</param>
        /// <param name="topLimit"></param>
        /// <param name="delete"></param>
        /// <returns>影响行数</returns>
        public static int SynchronousTable(string fromDataBase, string tableName, string[] primaryKeys, DateTime? modifiedOn, CurrentDbType toDataBaseDbType, string dbConnection, string toTableName, int topLimit, bool delete = true)
        {
            int result = 0;
            // 输入参数检查
            if (primaryKeys == null)
            {
                return result;
            }
            if (topLimit == 0)
            {
                topLimit = 200;
            }

            int synchronousCount = topLimit;

            DataTable dataTable = null;

            while (synchronousCount >= topLimit)
            {
                // 一批一批写入数据库，提高同步的性能
                var dbHelper = DbHelperFactory.GetHelper(toDataBaseDbType, dbConnection);
                SQLBuilder sqlBuilder = new SQLBuilder(dbHelper);
                dbHelper.BeginTransaction();

                // string url = BaseSystemInfo.WebHost + "WebAPIV42/API/Synchronous/GetTopLimitTable";
                const string url = "http://userCenter.zt-express.com/WebAPIV42/API/Synchronous/GetTopLimitTable";

                WebClient webClient = new WebClient();
                NameValueCollection postValues = new NameValueCollection
                {
                    {"userInfo", BaseSystemInfo.UserInfo.Serialize()},
                    {"systemCode", BaseSystemInfo.SystemCode},
                    {"securityKey", BaseSystemInfo.SecurityKey}, 
                    {"dataBase", fromDataBase}, 
                    {"tableName", tableName}, 
                    {"toTableName", toTableName}, 
                    {"topLimit", topLimit.ToString()}, 
                    {"modifiedOn", modifiedOn.Value.ToString(BaseSystemInfo.DateTimeFormat)}
                };
                // 向服务器发送POST数据
                byte[] responseArray = webClient.UploadValues(url, postValues);
                string response = Encoding.UTF8.GetString(responseArray);
                if (!string.IsNullOrEmpty(response))
                {
                    dataTable = (DataTable)JsonConvert.DeserializeObject(response, typeof(DataTable));
                }
                int r;
                // 出错的日志都需要能保存起来，这样有问题的可以找出原因来。
                for (r = 0; r < dataTable.Rows.Count; r++)
                {
                    // 先删除数据，修改的、新增的、都删除后添加来处理，问题就简单化了
                    // dbHelper.ExecuteNonQuery("DELETE FROM " + tableName + " WHERE " + primaryKey + " = '" + dataTable.Rows[r][primaryKey].ToString() + "'");
                    if (delete)
                    {
                        sqlBuilder.BeginDelete(toTableName);
                        for (int i = 0; i < primaryKeys.Length; i++)
                        {
                            string primaryKey = primaryKeys[i];
                            if (!string.IsNullOrWhiteSpace(primaryKey))
                            {
                                sqlBuilder.SetWhere(primaryKey, dataTable.Rows[r][primaryKey].ToString());
                            }
                        }
                        sqlBuilder.EndDelete();
                    }
                    // 然后插入数据
                    sqlBuilder.BeginInsert(toTableName);
                    for (int i = 0; i < dataTable.Columns.Count; i++)
                    {
                        // 这里能判断目标表里是否有这个字段存在就更完美了。
                        sqlBuilder.SetValue(dataTable.Columns[i].ColumnName, dataTable.Rows[r][dataTable.Columns[i].ColumnName]);
                    }
                    sqlBuilder.EndInsert();

                    if (DateTime.Parse(dataTable.Rows[r][BaseBusinessLogic.FieldModifiedOn].ToString()) > modifiedOn.Value)
                    {
                        modifiedOn = DateTime.Parse(dataTable.Rows[r][BaseBusinessLogic.FieldModifiedOn].ToString());
                    }
                    result++;
                }
                synchronousCount = dataTable.Rows.Count;

                // 批量提交
                try
                {
                    dbHelper.CommitTransaction();
                }
                catch (Exception ex)
                {
                    LogUtil.WriteException( ex);
                    dbHelper.RollbackTransaction();
                }
                finally
                {
                    dbHelper.Close();
                    dbHelper.Dispose();
                }
            }
            return result;
        }

        /// <summary>
        /// 删除同步数据
        /// </summary>
        /// <param name="fromDataBase">从服务器的哪个数据库获取数据</param>
        /// <param name="tableName">同步哪个表</param>
        /// <param name="primaryKeys">表的主键是什么</param>
        /// <param name="modifiedOn">同步的更新时间</param>
        /// <param name="toDataBaseDbType">同步到本地什么类型的数据库里？</param>
        /// <param name="dbConnection">同步的目标数据库连接方式？</param>
        /// <param name="toTableName">本地表</param>
        /// <param name="topLimit"></param>
        /// <param name="delete"></param>
        /// <returns>影响行数</returns>
        public static int DeleteTable(string fromDataBase, string tableName, string[] primaryKeys, DateTime? modifiedOn, CurrentDbType toDataBaseDbType, string dbConnection, string toTableName, int topLimit, bool delete = true)
        {
            int result = 0;
            // 输入参数检查
            if (primaryKeys == null)
            {
                return result;
            }
            if (topLimit == 0)
            {
                topLimit = 200;
            }

            int synchronousCount = topLimit;

            DataTable dataTable = null;

            while (synchronousCount >= topLimit)
            {
                // 一批一批写入数据库，提高同步的性能
                var dbHelper = DbHelperFactory.GetHelper(toDataBaseDbType, dbConnection);
                SQLBuilder sqlBuilder = new SQLBuilder(dbHelper);
                dbHelper.BeginTransaction();

                string url = BaseSystemInfo.WebHost + "WebAPIV42/API/Synchronous/GetTopLimitTable";
                // url = "http://localhost:8899/API/Synchronous/GetTopLimitTable";
                WebClient webClient = new WebClient();
                NameValueCollection postValues = new NameValueCollection();
                postValues.Add("userInfo", BaseSystemInfo.UserInfo.Serialize());
                postValues.Add("systemCode", BaseSystemInfo.SystemCode);
                postValues.Add("securityKey", BaseSystemInfo.SecurityKey);
                postValues.Add("dataBase", fromDataBase);
                postValues.Add("tableName", tableName);
                postValues.Add("toTableName", toTableName);
                postValues.Add("topLimit", topLimit.ToString());
                postValues.Add("modifiedOn", modifiedOn.Value.ToString(BaseSystemInfo.DateTimeFormat));
                // 向服务器发送POST数据
                byte[] responseArray = webClient.UploadValues(url, postValues);
                string response = Encoding.UTF8.GetString(responseArray);
                if (!string.IsNullOrEmpty(response))
                {
                    dataTable = (DataTable)JsonConvert.DeserializeObject(response, typeof(DataTable));
                }
                int r;
                // 出错的日志都需要能保存起来，这样有问题的可以找出原因来。
                for (r = 0; r < dataTable.Rows.Count; r++)
                {
                    // 先删除数据，修改的、新增的、都删除后添加来处理，问题就简单化了
                    // dbHelper.ExecuteNonQuery("DELETE FROM " + tableName + " WHERE " + primaryKey + " = '" + dataTable.Rows[r][primaryKey].ToString() + "'");

                    if (DateTime.Parse(dataTable.Rows[r][BaseBusinessLogic.FieldModifiedOn].ToString()) > modifiedOn.Value)
                    {
                        modifiedOn = DateTime.Parse(dataTable.Rows[r][BaseBusinessLogic.FieldModifiedOn].ToString());
                    }
                    if (delete)
                    {
                        sqlBuilder.BeginDelete(toTableName);
                        for (int i = 0; i < primaryKeys.Length; i++)
                        {
                            string primaryKey = primaryKeys[i];
                            if (!string.IsNullOrWhiteSpace(primaryKey))
                            {
                                sqlBuilder.SetWhere(primaryKey, dataTable.Rows[r][primaryKey].ToString());
                            }
                        }
                        sqlBuilder.EndDelete();
                    }
                    result++;
                }
                synchronousCount = dataTable.Rows.Count;

                // 批量提交
                try
                {
                    dbHelper.CommitTransaction();
                }
                catch (Exception ex)
                {
                    LogUtil.WriteException(ex);
                    dbHelper.RollbackTransaction();
                }
                finally
                {
                    dbHelper.Close();
                    dbHelper.Dispose();
                }
            }
            return result;
        }
    }
}