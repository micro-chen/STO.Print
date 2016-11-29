//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd.  
//-----------------------------------------------------------------

using System.Collections.Generic;
using System.Data;

namespace DotNet.Business
{
    /// <summary>
    ///	BaseManager
    /// 通用基类部分（分页）
    /// 
    /// 总觉得自己写的程序不上档次，这些新技术也玩玩，也许做出来的东西更专业了。
    /// 修改记录
    /// 
    ///		2012.02.04 版本：1.0 JiRiGaLa 进行提炼，把代码进行分组。
    ///
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2012.02.04</date>
    /// </author> 
    /// </summary>
    public partial class BaseManager : IBaseManager
    {
        /// <summary>
        /// 获取分页DataTable
        /// </summary>
        /// <param name="recordCount">记录总数</param>
        /// <param name="pageIndex">当前页数</param>
        /// <param name="pageSize">每页显示多少条</param>
        /// <param name="whereClause">条件</param>
        /// <param name="order">排序字段</param>
        /// <returns>数据表</returns>
        public virtual DataTable GetDataTableByPage(out int recordCount, int pageIndex, int pageSize, string whereClause, IDbDataParameter[] dbParameters, string order)
        {
            recordCount = DbLogic.GetCount(DbHelper, this.CurrentTableName, whereClause, dbParameters, this.CurrentIndex);
            return DbLogic.GetDataTableByPage(DbHelper, this.CurrentTableName, this.SelectFields, pageIndex, pageSize, whereClause, dbParameters, order, this.CurrentIndex);
        }

        /// <summary>
        /// 分页读取数据
        /// </summary>
        /// <param name="recordCount">条数</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页显示</param>
        /// <param name="sortExpression">排序字段</param>
        /// <param name="sortDire">排序顺序</param>
        /// <param name="tableName">表名</param>
        /// <param name="conditional">查询条件</param>
        /// <param name="dbParameters">数据参数</param>
        /// <param name="selectField">选择哪些字段</param>
        /// <returns>数据表</returns>
        public virtual DataTable GetDataTableByPage(out int recordCount, int pageIndex = 0, int pageSize = 20, string sortExpression = null, string sortDire = null, string tableName = null, string conditional = null, IDbDataParameter[] dbParameters = null, string selectField = null)
        {
            if (string.IsNullOrEmpty(tableName))
            {
                tableName = this.CurrentTableName;
            }
            if (tableName.ToUpper().IndexOf("SELECT") >= 0 || DbHelper.CurrentDbType == DotNet.Utilities.CurrentDbType.MySql)
            {
                // 统计总条数
                string commandText = string.Empty;
                if (string.IsNullOrEmpty(tableName))
                {
                    tableName = this.CurrentTableName;
                }
                string whereClause = string.Empty;
                if (!string.IsNullOrEmpty(conditional))
                {
                    whereClause = string.Format(" WHERE {0} ", conditional);
                }
                commandText = tableName;
                if (tableName.ToUpper().IndexOf("SELECT") >= 0)
                {
                    commandText = "(" + tableName + ") T ";
                    // commandText = "(" + tableName + ") AS T ";
                }
                commandText = string.Format("SELECT COUNT(1) AS recordCount FROM {0} {1}", commandText, whereClause);
                object returnObject = DbHelper.ExecuteScalar(commandText, dbParameters);
                if (returnObject != null)
                {
                    recordCount = int.Parse(returnObject.ToString());
                }
                else
                {
                    recordCount = 0;
                }
                return DbLogic.GetDataTableByPage(DbHelper, recordCount, pageIndex, pageSize, tableName, dbParameters, sortExpression, sortDire);
            }
            // 这个是调用存储过程的方法
            return DbLogic.GetDataTableByPage(DbHelper, out recordCount, pageIndex, pageSize, sortExpression, sortDire, tableName, conditional, selectField);
        }
    }
}