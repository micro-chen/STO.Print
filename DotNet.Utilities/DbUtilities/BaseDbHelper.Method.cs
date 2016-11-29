//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd.  
//-----------------------------------------------------------------

using System;
using System.Data;
using System.Data.Common;

namespace DotNet.Utilities
{
    /// <summary>
    /// BaseDbHelper
    /// 数据库访问层基础类。
    /// 
    /// 修改记录
    ///     
    ///		2013.02.04 版本：1.0 JiRiGaLa 分离改进。
    /// 
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2013.02.04</date>
    /// </author> 
    /// </summary>
    public abstract partial class BaseDbHelper : IDisposable // IDbHelper
    {
        #region public virtual IDataReader ExecuteReader(string commandText) 执行查询
        /// <summary>
        /// 执行查询
        /// </summary>
        /// <param name="commandText">sql查询</param>
        /// <returns>结果集流</returns>
        public virtual IDataReader ExecuteReader(string commandText)
        {
            return ExecuteReader(commandText, (IDbDataParameter[])null, CommandType.Text);
        }
        #endregion

        #region public virtual IDataReader ExecuteReader(string commandText, IDbDataParameter[] dbParameters); 执行查询
        /// <summary>
        /// 执行查询
        /// </summary>
        /// <param name="commandText">sql查询</param>
        /// <param name="dbParameters">参数集</param>
        /// <returns>结果集流</returns>
        public virtual IDataReader ExecuteReader(string commandText, IDbDataParameter[] dbParameters)
        {
            return this.ExecuteReader(commandText, dbParameters, CommandType.Text);
        }
        #endregion

        #region public virtual IDataReader ExecuteReader(string commandText, IDbDataParameter[] dbParameters, CommandType commandType) 执行查询
        /// <summary>
        /// 执行查询
        /// </summary>
        /// <param name="commandText">sql查询</param>
        /// <param name="dbParameters">参数集</param>
        /// <param name="commandType">命令分类</param>
        /// <returns>结果集流</returns>
        public virtual IDataReader ExecuteReader(string commandText, IDbDataParameter[] dbParameters, CommandType commandType)
        {
            // 自动打开
            if (this.DbConnection == null)
            {
                this.Open();
                this.MustCloseConnection = true;
            }
            else if (this.DbConnection.State == ConnectionState.Closed)
            {
                this.Open();
                this.MustCloseConnection = true;
            }
            this.dbCommand = this.DbConnection.CreateCommand();
            this.dbCommand.CommandTimeout = this.DbConnection.ConnectionTimeout;
            this.dbCommand.CommandText = commandText;
            this.dbCommand.CommandType = commandType;
            if (this.dbTransaction != null)
            {
                this.dbCommand.Transaction = this.dbTransaction;
            }
            if (dbParameters != null)
            {
                this.dbCommand.Parameters.Clear();
                for (int i = 0; i < dbParameters.Length; i++)
                {
                    if (dbParameters[i] != null)
                    {
                        this.dbCommand.Parameters.Add(((ICloneable)dbParameters[i]).Clone());
                    }
                }
            }

            // 这里要关闭数据库才可以的
            DbDataReader dbDataReader = null;
            if (!this.MustCloseConnection)
            {
                dbDataReader = this.dbCommand.ExecuteReader();
            }
            else
            {
                dbDataReader = this.dbCommand.ExecuteReader(CommandBehavior.CloseConnection);
            }

            // 写入日志
            // this.WriteLog(commandText);
            SQLTrace.WriteLog(commandText, dbParameters);

            return dbDataReader;
        }
        #endregion


        #region public virtual int ExecuteNonQuery(string commandText) 执行查询, SQL BUILDER 用了这个东西？参数需要保存, 不能丢失.
        /// <summary>
        /// 执行查询, SQL BUILDER 用了这个东西？参数需要保存, 不能丢失.
        /// </summary>
        /// <param name="commandText">sql查询</param>
        /// <returns>影响行数</returns>
        public virtual int ExecuteNonQuery(string commandText)
        {
            return this.ExecuteNonQuery(this.dbTransaction, commandText, (IDbDataParameter[])null, CommandType.Text);
        }
        #endregion

        #region public virtual int ExecuteNonQuery(string commandText, IDbDataParameter[] dbParameters) 执行查询
        /// <summary>
        /// 执行查询
        /// </summary>
        /// <param name="commandText">sql查询</param>
        /// <param name="dbParameters">参数集</param>
        /// <returns>影响行数</returns>
        public virtual int ExecuteNonQuery(string commandText, IDbDataParameter[] dbParameters)
        {
            return this.ExecuteNonQuery(this.dbTransaction, commandText, dbParameters, CommandType.Text);
        }
        #endregion

        #region public virtual int ExecuteNonQuery(string commandText, CommandType commandType) 执行查询
        /// <summary>
        /// 执行查询
        /// </summary>
        /// <param name="commandText">sql查询</param>
        /// <param name="commandType">命令分类</param>
        /// <returns>影响行数</returns>
        public virtual int ExecuteNonQuery(string commandText, CommandType commandType)
        {
            return this.ExecuteNonQuery(this.dbTransaction, commandText, (IDbDataParameter[])null, commandType);
        }
        #endregion

        #region public virtual int ExecuteNonQuery(string commandText, IDbDataParameter[] dbParameters, CommandType commandType) 执行查询
        /// <summary>
        /// 执行查询
        /// </summary>
        /// <param name="commandText">sql查询</param>
        /// <param name="dbParameters">参数集</param>
        /// <param name="commandType">命令分类</param>
        /// <returns>影响行数</returns>
        public virtual int ExecuteNonQuery(string commandText, IDbDataParameter[] dbParameters, CommandType commandType)
        {
            return this.ExecuteNonQuery(this.dbTransaction, commandText, dbParameters, commandType);
        }
        #endregion

        #region public virtual int ExecuteNonQuery(IDbTransaction transaction, string commandText, IDbDataParameter[] dbParameters, CommandType commandType) 执行查询
        /// <summary>
        /// 执行查询
        /// </summary>
        /// <param name="transaction">数据库事务</param>
        /// <param name="commandText">sql查询</param>
        /// <param name="dbParameters">参数集</param>
        /// <param name="commandType">命令分类</param>
        /// <returns>影响行数</returns>
        public virtual int ExecuteNonQuery(IDbTransaction transaction, string commandText, IDbDataParameter[] dbParameters, CommandType commandType)
        {
            // 自动打开
            if (this.DbConnection == null)
            {
                this.Open();
                this.MustCloseConnection = true;
            }
            else if (this.DbConnection.State == ConnectionState.Closed)
            {
                this.Open();
                this.MustCloseConnection = true;
            }

            this.dbCommand = this.DbConnection.CreateCommand();
            this.dbCommand.CommandTimeout = this.DbConnection.ConnectionTimeout;
            this.dbCommand.CommandText = commandText;
            this.dbCommand.CommandType = commandType;
            if (this.dbTransaction != null)
            {
                this.dbCommand.Transaction = this.dbTransaction;
            }
            if (dbParameters != null)
            {
                this.dbCommand.Parameters.Clear();
                for (int i = 0; i < dbParameters.Length; i++)
                {
                    // if (dbParameters[i] != null)
                    //{
                    this.dbCommand.Parameters.Add(((ICloneable)dbParameters[i]).Clone());
                    //}
                }
            }
            int result = this.dbCommand.ExecuteNonQuery();

			SetBackParamValue(dbParameters);

			// 自动关闭
			if(this.MustCloseConnection)
			{
				this.Close();
			}
			else
			{
				this.dbCommand.Parameters.Clear();
			}

            // 写入日志
            // this.WriteLog(commandText);

            return result;
        }
		#endregion

		#region private void SetBackParamValue(IDbDataParameter[] dbParameters)
		/// <summary>
		/// 设置返回值
		/// </summary>
		/// <param name="dbParameters"></param>
		private void SetBackParamValue(IDbDataParameter[] dbParameters)
		{
			for(int i = 0; dbParameters != null && i <= dbParameters.Length - 1; i++)
			{
                if (dbParameters[i].Direction != ParameterDirection.Input)
                {
                    dbParameters[i].Value = dbCommand.Parameters[i].Value;
                }
			}
		}
		#endregion

		#region public virtual object ExecuteScalar(string commandText) 执行查询
		/// <summary>
        /// 执行查询
        /// </summary>
        /// <param name="commandText">sql查询</param>
        /// <returns>object</returns>
        public virtual object ExecuteScalar(string commandText)
        {
            return this.ExecuteScalar(commandText, null, CommandType.Text);
        }
        #endregion

        #region public virtual object ExecuteScalar(string commandText, IDbDataParameter[] dbParameters) 执行查询
        /// <summary>
        /// 执行查询
        /// </summary>
        /// <param name="commandText">sql查询</param>
        /// <param name="dbParameters">参数集</param>
        /// <returns>Object</returns>
        public virtual object ExecuteScalar(string commandText, IDbDataParameter[] dbParameters)
        {
            return this.ExecuteScalar(commandText, dbParameters, CommandType.Text);
        }
        #endregion

        #region public virtual object ExecuteScalar(string commandText, IDbDataParameter[] dbParameters, CommandType commandType) 执行查询
        /// <summary>
        /// 执行查询
        /// </summary>
        /// <param name="commandText">sql查询</param>
        /// <param name="dbParameters">参数集</param>
        /// <param name="commandType">命令分类</param>
        /// <returns>Object</returns>
        public virtual object ExecuteScalar(string commandText, IDbDataParameter[] dbParameters, CommandType commandType)
        {
            // 自动打开
            if (this.DbConnection == null)
            {
                this.Open();
                this.MustCloseConnection = true;
            }
            else if (this.DbConnection.State == ConnectionState.Closed)
            {
                this.Open();
                this.MustCloseConnection = true;
            }

            this.dbCommand = this.DbConnection.CreateCommand();
            this.dbCommand.CommandTimeout = this.DbConnection.ConnectionTimeout;
            this.dbCommand.CommandText = commandText;
            this.dbCommand.CommandType = commandType;

            if (this.dbTransaction != null)
            {
                this.dbCommand.Transaction = this.dbTransaction;
            }

            if (dbParameters != null)
            {
                this.dbCommand.Parameters.Clear();
                for (int i = 0; i < dbParameters.Length; i++)
                {
                    if (dbParameters[i] != null)
                    {
                        this.dbCommand.Parameters.Add(((ICloneable)dbParameters[i]).Clone());
                        // this.dbCommand.Parameters.Add(dbParameters[i]);
                    }
                }
            }
            object result = this.dbCommand.ExecuteScalar();

			SetBackParamValue(dbParameters);

			// 自动关闭
            if (this.MustCloseConnection)
            {
                this.Close();
            }
            else
            {
                this.dbCommand.Parameters.Clear();
            }

            // 写入日志
            // this.WriteLog(commandText);
            return result;
        }
        #endregion


        #region public virtual DataTable Fill(string commandText) 填充数据表
        /// <summary>
        /// 填充数据表
        /// </summary>
        /// <param name="commandText">查询</param>
        /// <returns>数据表</returns>
        public virtual DataTable Fill(string commandText)
        {
            var dt = new DataTable("DotNet");
            return this.Fill(dt, commandText, (IDbDataParameter[])null, CommandType.Text);
        }
        #endregion

        #region public virtual DataTable Fill(DataTable dt, string commandText) 填充数据表
        /// <summary>
        /// 填充数据表
        /// </summary>
        /// <param name="dt">目标数据表</param>
        /// <param name="commandText">查询</param>
        /// <returns>数据表</returns>
        public virtual DataTable Fill(DataTable dt, string commandText)
        {
            return this.Fill(dt, commandText, (IDbDataParameter[])null, CommandType.Text);
        }
        #endregion

        #region public virtual DataTable Fill(string commandText, IDbDataParameter[] dbParameters) 填充数据表
        /// <summary>
        /// 填充数据表
        /// </summary>
        /// <param name="commandText">sql查询</param>
        /// <param name="dbParameters">参数集</param>
        /// <returns>数据表</returns>
        public virtual DataTable Fill(string commandText, IDbDataParameter[] dbParameters)
        {
            var dt = new DataTable("DotNet");
            return this.Fill(dt, commandText, dbParameters, CommandType.Text);
        }
        #endregion

        #region public virtual DataTable Fill(DataTable dt, string commandText, IDbDataParameter[] dbParameters) 填充数据表
        /// <summary>
        /// 填充数据表
        /// </summary>
        /// <param name="dt">目标数据表</param>
        /// <param name="commandText">sql查询</param>
        /// <param name="dbParameters">参数集</param>
        /// <returns>数据表</returns>
        public virtual DataTable Fill(DataTable dt, string commandText, IDbDataParameter[] dbParameters)
        {
            return this.Fill(dt, commandText, dbParameters, CommandType.Text);
        }
        #endregion

        #region public virtual DataTable Fill(string commandText, IDbDataParameter[] dbParameters, CommandType commandType) 填充数据表
        /// <summary>
        /// 填充数据表
        /// </summary>
        /// <param name="commandText">sql查询</param>
        /// <param name="commandType">命令分类</param>
        /// <param name="dbParameters">参数集</param>
        /// <returns>数据表</returns>
        public virtual DataTable Fill(string commandText, IDbDataParameter[] dbParameters, CommandType commandType)
        {
            var dt = new DataTable("DotNet");
            return this.Fill(dt, commandText, dbParameters, commandType);
        }
        #endregion

        #region public virtual DataTable Fill(DataTable dt, string commandText, IDbDataParameter[] dbParameters, CommandType commandType) 填充数据表
        /// <summary>
        /// 填充数据表
        /// </summary>
        /// <param name="dt">目标数据表</param>
        /// <param name="commandText">sql查询</param>
        /// <param name="dbParameters">参数集</param>
        /// <param name="commandType">命令分类</param>
        /// <returns>数据表</returns>
        public virtual DataTable Fill(DataTable dt, string commandText, IDbDataParameter[] dbParameters, CommandType commandType)
        {
            // 自动打开
            if (this.DbConnection == null)
            {
                this.Open();
                this.MustCloseConnection = true;
            }
            else if (this.DbConnection.State == ConnectionState.Closed)
            {
                this.Open();
                this.MustCloseConnection = true;
            }

            using (this.dbCommand = this.DbConnection.CreateCommand())
            {
                this.dbCommand.CommandTimeout = this.DbConnection.ConnectionTimeout;
                this.dbCommand.CommandText = commandText;
                this.dbCommand.CommandType = commandType;
                if (this.dbTransaction != null)
                {
                    this.dbCommand.Transaction = this.dbTransaction;
                }
                this.dbDataAdapter = this.GetInstance().CreateDataAdapter();
                this.dbDataAdapter.SelectCommand = this.dbCommand;
                if ((dbParameters != null) && (dbParameters.Length > 0))
                {
                    for (int i = 0; i < dbParameters.Length; i++)
                    {
                        if (dbParameters[i] != null)
                        {
                            this.dbCommand.Parameters.Add(((ICloneable)dbParameters[i]).Clone());
                        }
                    }
                    // this.dbCommand.Parameters.AddRange(dbParameters);
                }
                this.dbDataAdapter.Fill(dt);
				SetBackParamValue(dbParameters);
				this.dbDataAdapter.SelectCommand.Parameters.Clear();
            }

            // 自动关闭
            if (this.MustCloseConnection)
            {
                this.Close();
            }

            // 写入日志
            //this.WriteLog(commandText);
            // 记录日志
            SQLTrace.WriteLog(commandText, dbParameters);

            return dt;
        }
        #endregion

        #region public virtual DataSet Fill(DataSet dataSet, string commandText, string tableName) 填充数据权限
        /// <summary>
        /// 填充数据权限
        /// </summary>
        /// <param name="dataSet">目标数据权限</param>
        /// <param name="commandText">查询</param>
        /// <param name="tableName">填充表</param>
        /// <returns>数据权限</returns>
        public virtual DataSet Fill(DataSet dataSet, string commandText, string tableName)
        {
            return this.Fill(dataSet, commandText, tableName, (IDbDataParameter[])null, CommandType.Text);
        }
        #endregion

        #region public virtual DataSet Fill(DataSet dataSet, string commandText, string tableName, IDbDataParameter[] dbParameters) 填充数据权限
        /// <summary>
        /// 填充数据权限
        /// </summary>
        /// <param name="dataSet">数据权限</param>
        /// <param name="commandText">sql查询</param>
        /// <param name="tableName">填充表</param>
        /// <param name="dbParameters">参数集</param>
        /// <returns>数据权限</returns>
        public virtual DataSet Fill(DataSet dataSet, string commandText, string tableName, IDbDataParameter[] dbParameters)
        {
            return this.Fill(dataSet, commandText, tableName, dbParameters, CommandType.Text);
        }
        #endregion

        #region public virtual DataSet Fill(DataSet dataSet, string commandText, string tableName, IDbDataParameter[] dbParameters, CommandType commandType) 填充数据权限
        /// <summary>
        /// 填充数据权限
        /// </summary>
        /// <param name="dataSet">数据权限</param>
        /// <param name="commandType">命令分类</param>
        /// <param name="commandText">sql查询</param>
        /// <param name="tableName">填充表</param>
        /// <param name="dbParameters">参数集</param>
        /// <returns>数据权限</returns>
        public virtual DataSet Fill(DataSet dataSet, string commandText, string tableName, IDbDataParameter[] dbParameters, CommandType commandType)
        {
            // 自动打开
            if (this.DbConnection == null)
            {
                this.Open();
                this.MustCloseConnection = true;
            }
            else if (this.DbConnection.State == ConnectionState.Closed)
            {
                this.Open();
                this.MustCloseConnection = true;
            }

            using (this.dbCommand = this.DbConnection.CreateCommand())
            {
                //this.dbCommand.Parameters.Clear();
                //if ((dbParameters != null) && (dbParameters.Length > 0))
                //{
                //    for (int i = 0; i < dbParameters.Length; i++)
                //    {
                //        if (dbParameters[i] != null)
                //        {
                //            this.dbDataAdapter.SelectCommand.Parameters.Add(dbParameters[i]);
                //        }
                //    }
                //}
                this.dbCommand.CommandTimeout = this.DbConnection.ConnectionTimeout;
                this.dbCommand.CommandText = commandText;
                this.dbCommand.CommandType = commandType;
                if (this.dbTransaction != null)
                {
                    this.dbCommand.Transaction = this.dbTransaction;
                }

                if ((dbParameters != null) && (dbParameters.Length > 0))
                {
                    for (int i = 0; i < dbParameters.Length; i++)
                    {
                        if (dbParameters[i] != null)
                        {
                            this.dbCommand.Parameters.Add(((ICloneable)dbParameters[i]).Clone());
                        }
                    }
                    // this.dbCommand.Parameters.AddRange(dbParameters);
                }

                this.dbDataAdapter = this.GetInstance().CreateDataAdapter();
                this.dbDataAdapter.SelectCommand = this.dbCommand;
                this.dbDataAdapter.Fill(dataSet, tableName);

				SetBackParamValue(dbParameters);

                if (this.MustCloseConnection)
                {
                    this.Close();
                }
                else
                {
                    this.dbDataAdapter.SelectCommand.Parameters.Clear();
                }
            }

            // 写入日志
            // this.WriteLog(commandText);
            // 记录日志
            SQLTrace.WriteLog(commandText, dbParameters);

            return dataSet;
        }
        #endregion
    }
}