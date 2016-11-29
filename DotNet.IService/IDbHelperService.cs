//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2015 , Hairihan TECH, Ltd. 
//-----------------------------------------------------------------

using System.Collections.Generic;
using System.Data;
using System.ServiceModel;

namespace DotNet.IService
{
    using DotNet.Utilities;
    using DotNet.Model;

    /// <summary>
    /// IDbHelperService
    /// 数据库访问通用类标准接口。
    /// 
    /// 修改纪录
    /// 
    ///		2013.09.07 版本：3.0 JiRiGaLa 支持调用存储过程的扩展。
    ///		2008.08.26 版本：2.0 JiRiGaLa 将主键进行精简整理。
    ///		2008.08.25 版本：1.3 JiRiGaLa 将标准数据库接口方法进行分离、分离为标准接口方法与扩展接口方法部分。
    ///		2008.06.03 版本：1.2 JiRiGaLa 增加 DbParameter[] dbParameters 方法。
    ///		2008.05.07 版本：1.1 JiRiGaLa 增加GetWhereString定义。
    ///		2008.03.20 版本：1.0 JiRiGaLa 创建标准接口，这次应该算是一次飞跃。
    /// 
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2008.08.26</date>
    /// </author> 
    /// </summary>
    
    // [ServiceKnownType(typeof(System.DBNull))]
    // [ServiceKnownType(typeof(System.Data.SqlClient.SqlParameter))]
    // [ServiceKnownType(typeof(Oracle.DataAccess.Client.OracleParameter))]
    
    [ServiceContract]
    public interface IDbHelperService
    {
        /// <summary>
        /// 执行查询
        /// </summary>
        /// <param name="commandText">查询</param>
        /// <returns>影响行数</returns>
        [OperationContract(Name = "ExecuteNonQuery")]
        int ExecuteNonQuery(BaseUserInfo userInfo, string commandText, string commandType);
        
        /// <summary>
        /// 执行查询
        /// </summary>
        /// <param name="commandText">查询</param>
        /// <param name="dbParameters">参数集</param>
        /// <returns>影响行数</returns>
        [OperationContract(Name = "ExecuteNonQuery2")]
        int ExecuteNonQuery(BaseUserInfo userInfo, string commandText, List<KeyValuePair<string, object>> dbParameters, string commandType);

		/// <summary>
		/// 执行查询
		/// </summary>
		/// <param name="commandText">查询</param>
		/// <returns>影响行数</returns>
		[OperationContract(Name = "ExecuteNonQuery3")]
		int ExecuteNonQuery(BaseUserInfo userInfo, ref SqlExecute sqlExecute);

        /// <summary>
        /// 执行查询
        /// </summary>
        /// <param name="commandText">sql查询</param>
        /// <returns>Object</returns>
        [OperationContract(Name = "ExecuteScalar")]
        object ExecuteScalar(BaseUserInfo userInfo, string commandText, string commandType);
        
        /// <summary>
        /// 执行查询
        /// </summary>
        /// <param name="commandText">sql查询</param>
        /// <param name="dbParameters">参数集</param>
        /// <returns>Object</returns>
        [OperationContract(Name = "ExecuteScalar2")]
        object ExecuteScalar(BaseUserInfo userInfo, string commandText, List<KeyValuePair<string, object>> dbParameters, string commandType);

		/// <summary>
		/// 执行查询
		/// </summary>
		/// <param name="commandText">sql查询</param>
		/// <returns>Object</returns>
		[OperationContract(Name = "ExecuteScalar3")]
		object ExecuteScalar(BaseUserInfo userInfo, ref SqlExecute sqlExecute);
        
        /// <summary>
        /// 填充数据表
        /// </summary>
        /// <param name="commandText">查询</param>
        /// <returns>数据表</returns>
        [OperationContract(Name = "Fill")]
        DataTable Fill(BaseUserInfo userInfo, string commandText, string commandType);

        /// <summary>
        /// 填充数据表
        /// </summary>
        /// <param name="commandText">sql查询</param>
        /// <param name="dbParameters">参数集</param>
        /// <returns>数据表</returns>
        [OperationContract(Name = "Fill2")]
        DataTable Fill(BaseUserInfo userInfo, string commandText, List<KeyValuePair<string, object>> dbParameters, string commandType);

		/// <summary>
		/// 填充数据表
		/// </summary>
		/// <param name="commandText">sql查询</param>
		/// <param name="dbParameters">参数集</param>
		/// <returns>数据表</returns>
		[OperationContract(Name = "Fill3")]
		DataTable Fill(BaseUserInfo userInfo, ref SqlExecute sqlExecute);

        /// <summary>
        /// 获取分页数据（防注入功能的）
        /// </summary>
        /// <param name="recordCount">记录条数</param>
        /// <param name="tableName">数据来源表名</param>
        /// <param name="selectField">选择字段</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页显示多少条</param>
        /// <param name="conditions">查询条件</param>
        /// <param name="dbParameters">查询参数</param>
        /// <param name="orderBy">排序字段</param>
        /// <returns>数据表</returns>
        [OperationContract]
        DataTable GetDataTableByPage(BaseUserInfo userInfo, out int recordCount, string tableName, string selectField, int pageIndex, int pageSize, string conditions, List<KeyValuePair<string, object>> dbParameters, string orderBy);        
    }
}