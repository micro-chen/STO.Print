//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd.  
//-----------------------------------------------------------------

using System.Data;
using System.ServiceModel;

namespace DotNet.IService
{
    using DotNet.Model;
    using DotNet.Utilities;

    /// <summary>
    /// IWorkFlowCurrentService
    /// 当前审核工作流
    /// 
    /// <author>
    ///		<name>韩峰</name>
    ///		<date>2011.03.05</date>
    /// </author> 
    /// </summary>
    [ServiceContract]
    public interface IWorkFlowCurrentService
    {
        /// <summary>
        /// 替换工作审核者
        /// </summary>
        /// <param name="userInfo">用户</param>
        /// <param name="oldCode">原来的工号</param>
        /// <param name="newCode">新的工号</param>
        /// <returns>影响行数</returns>
        [OperationContract]
        int Replace(BaseUserInfo userInfo, string oldCode, string newCode);

        /// <summary>
        /// 获取待审核主键
        /// </summary>
        /// <param name="userInfo">用户</param>
        /// <param name="categoryCode">分类编号</param>
        /// <param name="objectId">主键</param>
        /// <returns>主键</returns>
        [OperationContract]
        string GetCurrentId(BaseUserInfo userInfo, string categoryCode, string objectId);

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="userInfo">用户</param>
        /// <returns>数据表</returns>
        [OperationContract]
        DataTable GetDataTable(BaseUserInfo userInfo);

        /// <summary>
        /// 获取监控列表
        /// </summary>
        /// <param name="userInfo">用户</param>
        /// <returns>数据表</returns>
        [OperationContract]
        DataTable GetMonitorDT(BaseUserInfo userInfo);

        /// <summary>
        /// 获取分页监控列表
        /// </summary>
        /// <param name="result"></param>
        /// <param name="categoryCode"></param>
        /// <param name="searchValue"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        [OperationContract]
        DataTable GetMonitorDTByPage(BaseUserInfo userInfo, int pageSize, int pageIndex, out int recordCount, string categoryCode = null, string searchValue = null, bool unfinishedOnly = true);

        /// <summary>
        /// 获取待审批
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        /// <param name="userId">用户主键</param>
        /// <param name="categoryCode">分类代码</param>      
        /// <param name="searchValue">查询字符串</param>
        /// <returns></returns>
        [OperationContract]
        DataTable GetWaitForAudit(BaseUserInfo userInfo, string userId = null, string categoryCode = null, string categorybillFullName = null, string searchValue = null);

        /// <summary>
        /// 获取审核历史明细 
        /// </summary>
        /// <param name="userInfo">用户</param>
        /// <param name="categoryId">单据分类主键</param>
        /// <param name="objectId">单据主键</param>
        /// <returns>数据权限</returns>
        [OperationContract]
        DataTable GetAuditDetailDT(BaseUserInfo userInfo, string categoryId, string objectId);

        /// <summary>
        /// 最终审核通过
        /// </summary>
        /// <param name="result">当前用户</param>
        /// <param name="id">主键</param>
        /// <param name="auditIdea">审核意见</param>
        /// <returns>影响行数</returns>
        [OperationContract]
        int AuditComplete(BaseUserInfo userInfo, string[] ids, string auditIdea);

        /// <summary>
        /// 撤消审批流程中的单据
        /// </summary>
        /// <param name="userInfo">用户</param>
        /// <param name="currentWorkFlowId">当前工作流主键</param>
        /// <param name="auditIdea">撤销意见</param>
        /// <returns>影响行数</returns>
        [OperationContract]
        int AuditQuash(BaseUserInfo userInfo, string[] currentWorkFlowIds, string auditIdea);

        /// <summary>
        /// 审核退回
        /// </summary>
        /// <param name="userInfo">用户</param>
        /// <param name="id">当前主键</param>
        /// <param name="auditIdea">审核建议</param>
        /// <returns>数据权限</returns>
        [OperationContract]
        int AuditReject(BaseUserInfo userInfo, string[] ids, string auditIdea);

        /// <summary>
        /// 自动工作流审核通过
        /// </summary>
        /// <param name="flowIds">当前流程主键组</param>
        /// <param name="auditIdea">提交意见</param>
        /// <returns>影响行数</returns>
        [OperationContract]
        int AuditPass(BaseUserInfo userInfo, string[] flowIds, string auditIdea);

        /// <summary>
        /// 开始审核
        /// </summary>
        /// <param name="userInfo">用户</param>
        /// <param name="categoryCode">分类编号</param>
        /// <param name="categoryFullName">分类名称</param>
        /// <param name="objectIds">实体主键数组</param>
        /// <param name="objectFullName">实体名称</param>
        /// <param name="workFlowCode">工作流编号</param>
        /// <param name="auditIdea">审核意见</param>
        /// <param name="statusCode">审核状态</param>
        /// <returns>主键</returns>
        [OperationContract]
        string AuditStatr(BaseUserInfo userInfo, string categoryCode, string categoryFullName, string[] objectIds, string objectFullName, string workFlowCode, string auditIdea, out string statusCode, DataTable dtWorkFlowActivity = null);

        /// <summary>
        /// 下个流程发送给谁
        /// </summary>
        /// <param name="userInfo">用户</param>
        /// <param name="id">当前主键</param>
        /// <param name="sendToUserId">用户主键</param>
        /// <param name="auditIdea">审核意见</param>
        /// <returns>数据权限</returns>
        [OperationContract]
        int AuditTransmit(BaseUserInfo userInfo, string[] ids, string workFlowCategory, string sendToUserId, string auditIdea);

        /// <summary>
        /// 获取已审核流程列表
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        /// <param name="categoryCode">单据类型</param>
        /// <param name="categorybillFullName">流程</param>
        /// <param name="searchValue">关键字</param>
        /// <returns></returns>
        [OperationContract]
        DataTable GetAuditRecord(BaseUserInfo userInfo, string categoryCode, string categorybillFullName = null, string searchValue = null);

        /// <summary>
        /// 提交审批（自由流）
        /// </summary>
        /// <param name="userInfo">用户</param>
        /// <param name="entity">实体</param>
        /// <returns>主键</returns>
        [OperationContract]
        string FreeAudit(BaseUserInfo userInfo, BaseWorkFlowAuditInfo entity);
        
        /// <summary>
        /// 自由工作流审核通过
        /// </summary>
        /// <param name="userInfo">用户</param>
        /// <param name="entity">实体</param>
        /// <returns>影响行数</returns>
        [OperationContract]
        int FreeAuditPass(BaseUserInfo userInfo, BaseWorkFlowAuditInfo entity);

		/// <summary>
		/// 启动工作流（步骤流转）
		/// </summary>
		/// <param name="userInfo">用户信息</param>
		/// <param name="workFlowManager">审批流程管理器</param>
		/// <param name="objectId">单据主键</param>
		/// <param name="objectFullName">单据名称</param>
		/// <param name="categoryCode">单据分类</param>
		/// <param name="categoryFullName">单据分类名称</param>
		/// <param name="workFlowCode">工作流程</param>
		/// <param name="auditIdea">审批意见</param>
		/// <param name="dtWorkFlowActivity">需要走的流程</param>
		/// <returns>主键</returns>
		[OperationContract]
		string AutoStatr(BaseUserInfo userInfo, IWorkFlowManager workFlowManager, string objectId, string objectFullName, string categoryCode, string categoryFullName = null, string workFlowCode = null, string auditIdea = null, DataTable dtWorkFlowActivity = null);

		/// <summary>
		/// 获取实体
		/// </summary>
		/// <param name="userInfo">用户信息</param>
		/// <param name="id">主键</param>
		/// <returns>实体</returns>
		[OperationContract]
		BaseWorkFlowCurrentEntity GetObject(BaseUserInfo userInfo, string id);
    }
}
