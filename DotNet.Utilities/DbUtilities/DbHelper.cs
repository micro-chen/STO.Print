//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd.  
//-----------------------------------------------------------------

using System;
using System.Data;

namespace DotNet.Utilities
{
    /// <summary>
    /// DbHelper
    /// 有关数据库连接的方法。
    /// 
    /// 修改记录
    /// 
    ///		2011.09.18 版本：2.0 JiRiGaLa 采用默认参数技术,把一些方法进行简化。
    ///		2008.09.03 版本：1.0 JiRiGaLa 创建。
    /// 
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2011.09.18</date>
    /// </author> 
    /// </summary>
    public partial class DbHelper
    {
        #region public static CurrentDbType GetDbType(string dbType, CurrentDbType defaultDbType = CurrentDbType.SqlServer) 数据库连接的类型判断
        /// <summary>
        /// 数据库连接的类型判断
        /// 2016-02-24 吉日嘎拉 忽略大小写，只要拼写正确就可以了，提高兼容性
        /// </summary>
        /// <param name="dbType">数据库类型</param>
        /// <param name="defaultDbType">默认数据库，防止设置有错误</param>
        /// <returns>数据库类型</returns>
        public static CurrentDbType GetDbType(string dbType, CurrentDbType defaultDbType = CurrentDbType.SqlServer)
        {
            CurrentDbType result = defaultDbType;

            if (!string.IsNullOrEmpty(dbType))
            {
                foreach (CurrentDbType currentDbType in Enum.GetValues(typeof(CurrentDbType)))
                {
                    if (currentDbType.ToString().Equals(dbType, StringComparison.OrdinalIgnoreCase))
                    {
                        result = currentDbType;
                        break;
                    }
                }
            }

            return result;
        }
        #endregion

        #region public static CommandType GetCommandType(string commandType) 数据库连接的类型判断
        /// <summary>
        /// 数据库连接的类型判断
        /// </summary>
        /// <param name="commandType">命令类型</param>
        /// <returns>命令类型</returns>
        public static CommandType GetCommandType(string commandType)
        {
            CommandType result = CommandType.Text;
            foreach (CommandType currentCommandType in Enum.GetValues(typeof(CommandType)))
            {
                if (currentCommandType.ToString().Equals(commandType))
                {
                    result = currentCommandType;
                    break;
                }
            }
            return result;
        }
        #endregion

        /// <summary>
        /// 按数据类型获取数据库访问实现类
        /// </summary>
        /// <param name="dbType">数据库类型</param>
        /// <returns>数据库访问实现类</returns>
        public static string GetDbHelperClass(CurrentDbType dbType)
        {
            string result = "DotNet.Utilities.SqlHelper";
            switch (dbType)
            {
                case CurrentDbType.SqlServer:
                    result = "DotNet.Utilities.SqlHelper";
                    break;
                case CurrentDbType.Oracle:
                    result = "DotNet.Utilities.OracleHelper";
                    // result = "DotNet.Utilities.MSOracleHelper";
                    break;
                case CurrentDbType.Access:
                    result = "DotNet.Utilities.OleDbHelper";
                    break;
                case CurrentDbType.MySql:
                    result = "DotNet.Utilities.MySqlHelper";
                    break;
                case CurrentDbType.DB2:
                    result = "DotNet.Utilities.DB2Helper";
                    break;
                case CurrentDbType.SQLite:
                    result = "DotNet.Utilities.SqLiteHelper";
                    break;
                case CurrentDbType.Ase:
                    result = "DotNet.Utilities.AseHelper";
                    break;
                case CurrentDbType.PostgreSql:
                    result = "DotNet.Utilities.PostgreSqlHelper";
                    break;
            }
            return result;
        }

        /// <summary>
        /// 数据库连接串，这里是为了简化思路
        /// </summary>
        public static string DbConnection = BaseSystemInfo.BusinessDbConnection;

        /// <summary>
        /// 数据库类型，这里也是为了简化思路
        /// </summary>
        public static CurrentDbType DbType = BaseSystemInfo.BusinessDbType;
    }
}