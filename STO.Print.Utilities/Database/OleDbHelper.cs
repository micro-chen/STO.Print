using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;

namespace STO.Print.Utilities
{
    /// <summary>
    /// 常用的Access数据库Sql操作辅助类库
    /// </summary>
    public class OleDbHelper
    {
        private string connectionString = "";
        private const string accessPrefix = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};User ID=Admin;Jet OLEDB:Database Password=;";

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="accessFilePath"></param>
        public OleDbHelper(string accessFilePath)
        {
            connectionString = string.Format(accessPrefix, accessFilePath);
        }

        /// <summary>
        /// 测试数据库是否正常连接
        /// </summary>
        /// <returns></returns>
        public bool TestConnection()
        {
            bool result = false;

            using (DbConnection connection = new OleDbConnection(connectionString))
            {
                connection.Open();
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    result = true;
                }
            }

            return result;
        }

        /// <summary>
        /// 执行Sql，并返回成功的数量
        /// </summary>
        /// <param name="sqlList">待执行的Sql列表</param>
        /// <returns></returns>
        public int ExecuteNonQuery(List<string> sqlList)
        {
            int count = 0;
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;

                foreach (string sql in sqlList)
                {
                    command.CommandText = sql;
                    command.CommandType = CommandType.Text;

                    try
                    {
                        if (command.ExecuteNonQuery() > 0)
                        {
                            count++;
                        }
                    }
                    catch (Exception ex)
                    {
                        continue;
                    }
                }
            }

            return count;
        }

        /// <summary>
        /// 执行无返回值的语句，成功返回True，否则False
        /// </summary>
        /// <param name="sql">待执行的Sql</param>
        /// <returns></returns>
        public bool ExecuteNoQuery(string sql)
        {
            bool result = false;
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                command.CommandText = sql;
                command.CommandType = CommandType.Text;

                if (command.ExecuteNonQuery() > 0)
                {
                    result = true;
                }
            }
            return result;
        }

        /// <summary>
        /// 执行单返回值的语句
        /// </summary>
        /// <param name="sql">待执行的Sql</param>
        /// <returns></returns>
        public object ExecuteScalar(string sql)
        {
            object result = null;
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                command.CommandText = sql;
                command.CommandType = CommandType.Text;
                result = command.ExecuteScalar();
            }
            return result;
        }

        /// <summary>
        /// 执行Sql，并返回IDataReader对象。
        /// </summary>
        /// <param name="sql">待执行的Sql</param>
        /// <returns></returns>
        public IDataReader ExecuteReader(string sql)
        {
            IDataReader reader = null;
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                command.CommandText = sql;
                command.CommandType = CommandType.Text;

                reader = command.ExecuteReader(CommandBehavior.CloseConnection);
            }

            return reader;
        }

        /// <summary>
        /// 执行Sql并返回DataSet集合
        /// </summary>
        /// <param name="sql">待执行的Sql</param>
        /// <returns></returns>
        public DataSet ExecuteDataSet(string sql)
        {
            DataSet ds = new DataSet();
            OleDbDataAdapter adapter = new OleDbDataAdapter(sql, connectionString);
            adapter.Fill(ds);
            return ds;
        }

    }
}
