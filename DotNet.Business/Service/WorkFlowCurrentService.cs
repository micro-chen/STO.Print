//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd.  
//-----------------------------------------------------------------

using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.ServiceModel;

namespace DotNet.Business
{
    using DotNet.IService;
    using DotNet.Model;
    using DotNet.Utilities;

    /// <summary>
    /// WorkFlowCurrentService
    /// 当前工作流服务
    /// 
    /// 修改记录
    /// 
    ///		2007.08.15 版本：2.2 JiRiGaLa 改进运行速度采用 WebService 变量定义 方式处理数据。
    ///		2007.08.14 版本：2.1 JiRiGaLa 改进运行速度采用 Instance 方式处理数据。
    ///		2007.07.19 版本：1.0 JiRiGaLa 实现控件功能。
    ///		
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2007.08.15</date>
    /// </author> 
    /// </summary>
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    [System.ServiceModel.Activation.AspNetCompatibilityRequirements(RequirementsMode = System.ServiceModel.Activation.AspNetCompatibilityRequirementsMode.Allowed)]
    public class WorkFlowCurrentService : IWorkFlowCurrentService
    {
        /// <summary>
        /// 工作流数据库连接
        /// </summary>
        private readonly string WorkFlowDbConnection = BaseSystemInfo.WorkFlowDbConnection;

        #region public string GetCurrentId(BaseUserInfo userInfo, string categoryId, string objectId) 获取工作流主键
        /// <summary>
        /// 获取工作流主键
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        /// <param name="categoryCode">类型编号</param>
        /// <param name="objectId">单据主键</param>
        /// <returns></returns>
        public string GetCurrentId(BaseUserInfo userInfo, string categoryCode, string objectId)
        {
			string currentId = string.Empty;

            var parameter = ServiceInfo.Create(userInfo, MethodBase.GetCurrentMethod());
            ServiceUtil.ProcessWorkFlowDb(userInfo, parameter, (dbHelper) =>
			{
				var workFlowCurrentManager = new BaseWorkFlowCurrentManager(dbHelper, userInfo);
				currentId = workFlowCurrentManager.GetCurrentId(categoryCode, objectId);
				// BaseWorkFlowCurrentEntity workFlowCurrentEntity = workFlowCurrentManager.GetObject(currentId);
				// workFlowCurrentEntity.ActivityId;
				// workFlowCurrentEntity.ActivityFullName;
			});
			return currentId;
        }
        #endregion

        #region public string AuditStatr(BaseUserInfo userInfo, string categoryCode, string categoryFullName, string[] objectIds, string objectFullName, string workFlowCode, string auditIdea, out string statusCode, DataTable dtWorkFlowActivity = null) 提交审批(步骤流)
        /// <summary>
        /// 提交审批(步骤流)
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        /// <param name="categoryCode">单据类型代码</param>
        /// <param name="categoryFullName">单据类型名称</param>
        /// <param name="objectIds">单据主键组</param>
        /// <param name="objectFullName">单据名称</param>
        /// <param name="workFlowCode">工作流编号</param>
        /// <param name="auditIdea">审批意见</param>
        /// <param name="statusCode">返回码</param>
        /// <param name="dtWorkFlowActivity">步骤列表</param>
        /// <returns></returns>
        public string AuditStatr(BaseUserInfo userInfo, string categoryCode, string categoryFullName, string[] objectIds, string objectFullName, string workFlowCode, string auditIdea, out string statusCode, DataTable dtWorkFlowActivity = null)
        {
			string result = string.Empty;
			
            string returnCode = string.Empty;

            var parameter = ServiceInfo.Create(userInfo, MethodBase.GetCurrentMethod());
            ServiceUtil.ProcessWorkFlowDbWithTransaction(userInfo, parameter, (dbHelper) =>
			{
				// 默认的都按报表来处理，特殊的直接调用，明确指定
				IWorkFlowManager userReportManager = new UserReportManager(userInfo);
				var workFlowCurrentManager = new BaseWorkFlowCurrentManager(dbHelper, userInfo);
				for(int i = 0; i < objectIds.Length; i++)
				{
					result = workFlowCurrentManager.AutoStatr(userReportManager, objectIds[i], objectFullName, categoryCode, categoryFullName, workFlowCode, auditIdea, dtWorkFlowActivity);
				}
				if(!string.IsNullOrEmpty(result))
				{
					returnCode = Status.OK.ToString();
				}
                // BaseLogManager.Instance.Add(result, this.serviceName, MethodBase.GetCurrentMethod());
            });
			statusCode = returnCode;
			return result;
        }
        #endregion

        #region public int AuditPass(BaseUserInfo userInfo, string[] flowIds, string auditIdea) 自动工作流审核通过
        /// <summary>
        /// 自动工作流审核通过
        /// </summary>
        /// <param name="flowIds">当前流程主键组</param>
        /// <param name="auditIdea">提交意见</param>
        /// <returns>影响行数</returns>
        public int AuditPass(BaseUserInfo userInfo, string[] flowIds, string auditIdea)
        {
			int result = 0;

            var parameter = ServiceInfo.Create(userInfo, MethodBase.GetCurrentMethod());
            ServiceUtil.ProcessWorkFlowDbWithTransaction(userInfo, parameter, (dbHelper) =>
			{
				IWorkFlowManager userReportManager = new UserReportManager(userInfo);
				var workFlowCurrentManager = new BaseWorkFlowCurrentManager(dbHelper, userInfo);
				result = workFlowCurrentManager.AutoAuditPass(flowIds, auditIdea);
			});
			return result;
        }
        #endregion

        #region public int AuditTransmit(BaseUserInfo userInfo, string[] ids,  string workFlowCategory, string sendToUserId) 下个流程发送给谁
        /// <summary>
        /// 下个流程发送给谁
        /// </summary>
        /// <param name="userInfo">用户</param>
        /// <param name="id">当前主键</param>
        /// <param name="sendToUserId">用户主键</param>
        /// <param name="auditIdea">审核意见</param>
        /// <returns>数据权限</returns>
        public int AuditTransmit(BaseUserInfo userInfo, string[] ids, string workFlowCategory, string sendToUserId, string auditIdea)
        {
			int result = 0;

            var parameter = ServiceInfo.Create(userInfo, MethodBase.GetCurrentMethod());
            ServiceUtil.ProcessWorkFlowDbWithTransaction(userInfo, parameter, (dbHelper) =>
			{
				var workFlowCurrentManager = new BaseWorkFlowCurrentManager(dbHelper, userInfo);
				result = workFlowCurrentManager.AuditTransmit(ids, workFlowCategory, sendToUserId, auditIdea);
			});
			return result;
        }
        #endregion

        #region public int AuditReject(BaseUserInfo userInfo, string[] ids, string auditIdea)
        /// <summary>
        /// 审核退回
        /// </summary>
        /// <param name="userInfo">用户</param>
        /// <param name="id">当前主键</param>
        /// <param name="auditIdea">审核建议</param>
        /// <returns>数据权限</returns>
        public int AuditReject(BaseUserInfo userInfo, string[] ids, string auditIdea)
        {
			int result = 0;

            var parameter = ServiceInfo.Create(userInfo, MethodBase.GetCurrentMethod()); 
            ServiceUtil.ProcessWorkFlowDbWithTransaction(userInfo, parameter, (dbHelper) =>
			{
				var workFlowCurrentManager = new BaseWorkFlowCurrentManager(dbHelper, userInfo);
				result = workFlowCurrentManager.AuditReject(ids, auditIdea);
			});
			return result;
        }
        #endregion

        #region public int AuditQuash(BaseUserInfo userInfo, string string[] currentWorkFlowIds, string auditIdea) 撤消审批流程中的单据
        /// <summary>
        /// 撤消审批流程中的单据
        /// </summary>
        /// <param name="userInfo">用户</param>
        /// <param name="currentWorkFlowId">当前工作流主键</param>
        /// <param name="auditIdea">撤销意见</param>
        /// <returns>影响行数</returns>
        public int AuditQuash(BaseUserInfo userInfo, string[] currentWorkFlowIds, string auditIdea)
        {
			int result = 0;

            var parameter = ServiceInfo.Create(userInfo, MethodBase.GetCurrentMethod());
            ServiceUtil.ProcessWorkFlowDbWithTransaction(userInfo, parameter, (dbHelper) =>
			{
				var workFlowCurrentManager = new BaseWorkFlowCurrentManager(dbHelper, userInfo);
				result = workFlowCurrentManager.AuditQuash(currentWorkFlowIds, auditIdea);
			});
			return result;
        }
        #endregion

        #region public int AuditComplete(BaseUserInfo userInfo, string[] ids, string auditIdea) 最终审核通过
        /// <summary>
        /// 最终审核通过
        /// </summary>
        /// <param name="userInfo">当前用户</param>
        /// <param name="ids">主键数组</param>
        /// <param name="auditIdea">审核意见</param>
        /// <returns>影响行数</returns>
        public int AuditComplete(BaseUserInfo userInfo, string[] ids, string auditIdea)
        {
			int result = 0;

            var parameter = ServiceInfo.Create(userInfo, MethodBase.GetCurrentMethod());
            ServiceUtil.ProcessWorkFlowDbWithTransaction(userInfo, parameter, (dbHelper) =>
			{
				//IWorkFlowManager userReportManager = new UserReportManager(result);
				var workFlowCurrentManager = new BaseWorkFlowCurrentManager(dbHelper, userInfo);
				result += workFlowCurrentManager.AuditComplete(ids, auditIdea);
			});
			return result;
        }
        #endregion

		#region public string AutoStatr(BaseUserInfo userInfo, IWorkFlowManager workFlowManager, string objectId, string objectFullName, string categoryCode, string categoryFullName = null, string workFlowCode = null, string auditIdea = null, DataTable dtWorkFlowActivity = null)启动工作流（步骤流转）
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
		public string AutoStatr(BaseUserInfo userInfo, IWorkFlowManager workFlowManager, string objectId, string objectFullName, string categoryCode, string categoryFullName = null, string workFlowCode = null, string auditIdea = null, DataTable dtWorkFlowActivity = null)
		{
			string result = string.Empty;

            var parameter = ServiceInfo.Create(userInfo, MethodBase.GetCurrentMethod());
            ServiceUtil.ProcessWorkFlowDb(userInfo, parameter, (dbHelper) =>
			{
				var workFlowCurrentManager = new BaseWorkFlowCurrentManager(dbHelper, userInfo);
				result = workFlowCurrentManager.AutoStatr(workFlowManager, objectId, objectFullName, categoryCode, categoryFullName, workFlowCode, auditIdea, dtWorkFlowActivity);
			});
			return result;
		}
		#endregion

		#region public DataTable GetDataTable(BaseUserInfo BaseUserInfo) 获取流程当前步骤列表
		/// <summary>
        /// 获取流程当前步骤列表
        /// </summary>
        /// <param name="userInfo">用户</param>
        /// <returns>数据表</returns>
        public DataTable GetDataTable(BaseUserInfo userInfo)
        {
			var dt = new DataTable(BaseWorkFlowCurrentEntity.TableName);

            var parameter = ServiceInfo.Create(userInfo, MethodBase.GetCurrentMethod());
            ServiceUtil.ProcessWorkFlowDb(userInfo, parameter, (dbHelper) =>
			{
				var workFlowCurrentManager = new BaseWorkFlowCurrentManager(dbHelper, userInfo);
				List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
				parameters.Add(new KeyValuePair<string, object>(BaseWorkFlowCurrentEntity.FieldEnabled, 1));
				parameters.Add(new KeyValuePair<string, object>(BaseWorkFlowCurrentEntity.FieldDeletionStateCode, 0));
				dt = workFlowCurrentManager.GetDataTable(parameters, BaseWorkFlowCurrentEntity.FieldSendDate);
				dt.TableName = BaseWorkFlowCurrentEntity.TableName;
			});

			return dt;
        }
        #endregion

        #region public DataTable GetMonitorDTByPage(BaseUserInfo userInfo, int pageSize, int pageIndex, out int recordCount, string categoryCode = null, string searchValue = null) 按分页获取监控列表
        /// <summary>
        /// 按分页获取监控列表
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageIndex">第几页</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="categoryCode">类型</param>
        /// <param name="searchValue">查找内容</param>
        /// <returns></returns>
        public DataTable GetMonitorDTByPage(BaseUserInfo userInfo, int pageSize, int pageIndex, out int recordCount, string categoryCode = null, string searchValue = null, bool unfinishedOnly = true)
        {
			DataTable dt = null;
			int myrecordCount = 0;

            var parameter = ServiceInfo.Create(userInfo, MethodBase.GetCurrentMethod());
            ServiceUtil.ProcessWorkFlowDb(userInfo, parameter, (dbHelper) =>
			{
				var workFlowCurrentManager = new BaseWorkFlowCurrentManager(dbHelper, userInfo);
				dt = workFlowCurrentManager.GetMonitorDTByPage(pageSize, pageIndex, out myrecordCount, categoryCode, searchValue, unfinishedOnly);
				dt.TableName = BaseWorkFlowCurrentEntity.TableName;
			});
			recordCount = myrecordCount;
			return dt;
        }
        #endregion

        #region public DataTable GetMonitorDT(BaseUserInfo userInfo) 获取监控列表
        /// <summary>
        /// 获取监控列表
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        /// <returns></returns>
        public DataTable GetMonitorDT(BaseUserInfo userInfo)
        {
			DataTable dt = null;

            var parameter = ServiceInfo.Create(userInfo, MethodBase.GetCurrentMethod());
            ServiceUtil.ProcessWorkFlowDb(userInfo, parameter, (dbHelper) =>
			{
				var workFlowCurrentManager = new BaseWorkFlowCurrentManager(dbHelper, userInfo);
				if(userInfo.IsAdministrator)
				{
					dt = workFlowCurrentManager.GetDataTable(new KeyValuePair<string, object>(BaseWorkFlowCurrentEntity.FieldDeletionStateCode, 0), BaseWorkFlowCurrentEntity.FieldSendDate);
				}
				else
				{
					dt = workFlowCurrentManager.GetMonitorDT();
				}
				dt.TableName = BaseWorkFlowCurrentEntity.TableName;
			});
			return dt;
        }
        #endregion

        #region public DataTable GetWaitForAudit(BaseUserInfo userInfo,string userId = null, string categoryCode = null, string categorybillFullName = null, string searchValue = null) 获取待审批
        /// <summary>
        /// 获取待审批
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        /// <param name="userId">用户主键</param>
        /// <param name="categoryCode">分类代码</param>      
        /// <param name="categorybillFullName">单据分类名称</param>
        /// <param name="searchValue">查询字符串</param>
        /// <returns></returns>
        public DataTable GetWaitForAudit(BaseUserInfo userInfo, string userId = null, string categoryCode = null, string categorybillFullName = null, string searchValue = null)
        {
			DataTable dt = null;

            var parameter = ServiceInfo.Create(userInfo, MethodBase.GetCurrentMethod());
            ServiceUtil.ProcessUserCenterReadDb(userInfo, parameter, (dbHelper) =>
			{
				//BaseUserManager userManager = new BaseUserManager(dbHelper);
				var userManager = new BaseUserManager(dbHelper, userInfo);
				string[] roleIds = userManager.GetRoleIds(userInfo.Id);
				dbHelper.Close();
				// 这里是获取待审核信息
				dbHelper.Open(WorkFlowDbConnection);
				var workFlowCurrentManager = new BaseWorkFlowCurrentManager(dbHelper, userInfo);
				dt = workFlowCurrentManager.GetWaitForAudit(userId, categoryCode, categorybillFullName, searchValue);
				dt.TableName = BaseWorkFlowCurrentEntity.TableName;
			});

			return dt;
        }
        #endregion

        #region public DataTable GetAuditDetailDT(BaseUserInfo userInfo, string categoryId, string objectId) 获取审核历史明细
        /// <summary>
        /// 获取审核历史明细 
        /// </summary>
        /// <param name="userInfo">用户</param>
        /// <param name="categoryId">单据分类主键</param>
        /// <param name="objectId">单据主键</param>
        /// <returns>数据权限</returns>
        public DataTable GetAuditDetailDT(BaseUserInfo userInfo, string categoryId, string objectId)
        {
			DataTable dt = null;

            var parameter = ServiceInfo.Create(userInfo, MethodBase.GetCurrentMethod());
            ServiceUtil.ProcessWorkFlowDb(userInfo, parameter, (dbHelper) =>
			{
				var workFlowCurrentManager = new BaseWorkFlowCurrentManager(dbHelper, userInfo);
				string[] ids = workFlowCurrentManager.GetIds(new KeyValuePair<string, object>(BaseWorkFlowCurrentEntity.FieldCategoryCode, categoryId), new KeyValuePair<string, object>(BaseWorkFlowCurrentEntity.FieldObjectId, objectId));
				var workFlowHistoryManager = new BaseWorkFlowHistoryManager(dbHelper, userInfo);
				dt = workFlowHistoryManager.GetDataTable(BaseWorkFlowHistoryEntity.FieldCurrentFlowId, ids, BaseWorkFlowHistoryEntity.FieldCreateOn);
				dt.TableName = BaseWorkFlowCurrentEntity.TableName;
			});
			return dt;
        }
        #endregion

        #region public int Replace(BaseUserInfo userInfo, string oldCode, string newCode) 替换工作审核者
        /// <summary>
        /// 替换工作审核者
        /// </summary>
        /// <param name="userInfo">用户</param>
        /// <param name="oldCode">原来的工号</param>
        /// <param name="newCode">新的工号</param>
        /// <returns>影响行数</returns>
        public int Replace(BaseUserInfo userInfo, string oldCode, string newCode)
        {
			int result = 0;

            var parameter = ServiceInfo.Create(userInfo, MethodBase.GetCurrentMethod());
            ServiceUtil.ProcessWorkFlowDb(userInfo, parameter, (dbHelper) =>
			{
				var workFlowCurrentManager = new BaseWorkFlowCurrentManager(dbHelper, userInfo);
				result = workFlowCurrentManager.ReplaceUser(oldCode, newCode);
			});
			return result;
        }
        #endregion

        /// <summary>
        /// 获取已审核流程列表
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        /// <param name="categoryCode">单据类型</param>
        /// <param name="categorybillFullName">流程</param>
        /// <param name="searchValue">关键字</param>
        /// <returns></returns>
        public DataTable GetAuditRecord(BaseUserInfo userInfo, string categoryCode, string categorybillFullName = null, string searchValue = null)
        {
			DataTable dt = null;

            var parameter = ServiceInfo.Create(userInfo, MethodBase.GetCurrentMethod());
            ServiceUtil.ProcessWorkFlowDb(userInfo, parameter, (dbHelper) =>
			{
				var workFlowCurrentManager = new BaseWorkFlowCurrentManager(dbHelper, userInfo);
				dt = workFlowCurrentManager.GetAuditRecord(categoryCode, categorybillFullName, searchValue);
				dt.TableName = BaseWorkFlowCurrentEntity.TableName;
			});
			return dt;
        }

        //----------------------------------------------------------------------------
        //                                  自由流 
        //----------------------------------------------------------------------------

        #region public string FreeAudit(BaseUserInfo userInfo, BaseWorkFlowAuditInfo workFlowAuditInfo)
        /// <summary>
        /// 提交审批（自由流）
        /// </summary>
        /// <param name="result"></param>
        /// <param name="workFlowAuditInfo"></param>
        /// <returns></returns>
        public string FreeAudit(BaseUserInfo userInfo, BaseWorkFlowAuditInfo workFlowAuditInfo)
        {
			string result = string.Empty;

            var parameter = ServiceInfo.Create(userInfo, MethodBase.GetCurrentMethod());
            ServiceUtil.ProcessWorkFlowDbWithTransaction(userInfo, parameter, (dbHelper) =>
			{
				// 默认的都按单据来处理，特殊的直接调用，明确指定
				IWorkFlowManager workFlowManager = new BaseUserBillManager(userInfo);
				var workFlowCurrentManager = new BaseWorkFlowCurrentManager(dbHelper, userInfo);
				result = workFlowCurrentManager.FreeAudit(workFlowManager, workFlowAuditInfo);
			});
			return result;
        }
        #endregion

        #region public int FreeAuditPass(BaseUserInfo userInfo, BaseWorkFlowAuditInfo workFlowAuditInfo) 自由工作流审核通过
        /// <summary>
        /// 自由工作流审核通过
        /// </summary>
        /// <param name="result"></param>
        /// <param name="workFlowAuditInfo"></param>
        /// <returns></returns>
        public int FreeAuditPass(BaseUserInfo userInfo, BaseWorkFlowAuditInfo workFlowAuditInfo)
        {
			int result = 0;

            var parameter = ServiceInfo.Create(userInfo, MethodBase.GetCurrentMethod());
            ServiceUtil.ProcessWorkFlowDbWithTransaction(userInfo, parameter, (dbHelper) =>
			{
				var workFlowCurrentManager = new BaseWorkFlowCurrentManager(dbHelper, userInfo);
				IWorkFlowManager workFlowManager = workFlowCurrentManager.GetWorkFlowManager(workFlowAuditInfo.Id);
				result = workFlowCurrentManager.FreeAuditPass(workFlowManager, workFlowAuditInfo);
			});
			return result;
        }
        #endregion

		#region public BaseWorkFlowCurrentEntity GetObject(BaseUserInfo userInfo, string id)获取实体
		/// <summary>
		/// 获取实体
		/// </summary>
		/// <param name="userInfo">用户信息</param>
		/// <param name="id">主键</param>
		/// <returns>实体</returns>
		public BaseWorkFlowCurrentEntity GetObject(BaseUserInfo userInfo, string id)
		{
			BaseWorkFlowCurrentEntity result = null;

            var parameter = ServiceInfo.Create(userInfo, MethodBase.GetCurrentMethod());
            ServiceUtil.ProcessWorkFlowDb(userInfo, parameter, (dbHelper) =>
			{
				var workFlowCurrentManager = new BaseWorkFlowCurrentManager(dbHelper, userInfo);
				result = workFlowCurrentManager.GetObject(id);
			});
			return result;
		}
		#endregion
	}
}