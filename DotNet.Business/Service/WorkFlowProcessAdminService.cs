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
    /// WorkFlowProcessAdminService
    /// 工作流处理过程服务
    /// 
    /// 修改记录
    /// 
    ///		2007.06.26 版本：1.0 JiRiGaLa 窗体与数据库连接的分离。
    ///		
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2007.06.26</date>
    /// </author> 
    /// </summary>
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    [System.ServiceModel.Activation.AspNetCompatibilityRequirements(RequirementsMode = System.ServiceModel.Activation.AspNetCompatibilityRequirementsMode.Allowed)]
    public class WorkFlowProcessAdminService : IWorkFlowProcessAdminService
    {
        /// <summary>
        /// 用户中心数据库连接
        /// </summary>
        private readonly string WorkFlowDbConnection = BaseSystemInfo.WorkFlowDbConnection;

        /// <summary>
        /// 添加工作流
        /// </summary>
        /// <param name="userInfo">当前用户</param>
        /// <param name="entity">工作流定义实体</param>
        /// <param name="statusCode">状态码</param>
        /// <param name="statusMessage">状态信息</param>
        /// <returns>主键</returns>
        public string Add(BaseUserInfo userInfo, BaseWorkFlowProcessEntity entity, out string statusCode, out string statusMessage)
        {			
			
			string returnCode = string.Empty;
			string returnMessage = string.Empty;
			string result = string.Empty;

            var parameter = ServiceInfo.Create(userInfo, MethodBase.GetCurrentMethod());
            ServiceUtil.ProcessWorkFlowDb(userInfo, parameter, (dbHelper) =>
			{
				var workFlowManager = new BaseWorkFlowProcessManager(dbHelper, userInfo);
				result = workFlowManager.Add(entity, out returnCode);
				// 获得状态消息
				returnMessage = workFlowManager.GetStateMessage(returnCode);
			});
			statusCode = returnCode;
			statusMessage = returnMessage;
			return result;
        }

        public BaseWorkFlowProcessEntity GetObject(BaseUserInfo userInfo, string id)
        {
			BaseWorkFlowProcessEntity entity = new BaseWorkFlowProcessEntity();

            var parameter = ServiceInfo.Create(userInfo, MethodBase.GetCurrentMethod());
            ServiceUtil.ProcessWorkFlowDb(userInfo, parameter, (dbHelper) =>
			{
				// 创建实现类
				var workFlowManager = new BaseWorkFlowProcessManager(dbHelper, userInfo);
				entity = workFlowManager.GetObject(id);
			});
			return entity;
        }

        public int Update(BaseUserInfo userInfo, BaseWorkFlowProcessEntity entity, out string statusCode, out string statusMessage)
        {
            int result = 0;
            
            string returnCode = string.Empty;
			string returnMessage = string.Empty;
			
            var parameter = ServiceInfo.Create(userInfo, MethodBase.GetCurrentMethod());
            ServiceUtil.ProcessWorkFlowDb(userInfo, parameter, (dbHelper) =>
			{
				var workFlowManager = new BaseWorkFlowProcessManager(dbHelper, userInfo);
				result = workFlowManager.Update(entity, out returnCode);
				// 获得状态消息
				returnMessage = workFlowManager.GetStateMessage(returnCode);
			});
			statusCode = returnCode;
			statusMessage = returnMessage;
			return result; 
        }


        #region public DataTable GetDataTable(BaseUserInfo BaseUserInfo,string id = null) 获取数据
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="userInfo">用户</param>
        /// <param name="id">主键</param>
        /// <returns>数据表</returns>
        public DataTable GetDataTable(BaseUserInfo userInfo,string id = null)
        {
			var dt = new DataTable(BaseWorkFlowProcessEntity.TableName);

            var parameter = ServiceInfo.Create(userInfo, MethodBase.GetCurrentMethod());
            ServiceUtil.ProcessWorkFlowDb(userInfo, parameter, (dbHelper) =>
			{
				var workFlowManager = new BaseWorkFlowProcessManager(dbHelper, userInfo);
				if(string.IsNullOrEmpty(id))
				{
					dt = workFlowManager.GetDataTable(new KeyValuePair<string, object>(BaseWorkFlowProcessEntity.FieldDeletionStateCode, 0), BaseWorkFlowProcessEntity.FieldSortCode);
				}
				else
				{
					//dt = workFlowManager.GetDataTableById(id);
                    List<KeyValuePair<string, object>> parametersList = new List<KeyValuePair<string, object>>();
                    parametersList.Add(new KeyValuePair<string, object>(BaseWorkFlowProcessEntity.FieldDeletionStateCode, 0));
                    parametersList.Add(new KeyValuePair<string, object>(BaseWorkFlowProcessEntity.FieldId, id));
                    dt = workFlowManager.GetDataTable(parametersList, BaseWorkFlowProcessEntity.FieldSortCode);
				}
				//result = workFlowManager.GetDataTable();
				dt.TableName = BaseWorkFlowProcessEntity.TableName;
			});
			return dt;
        }
        #endregion

        #region public int Delete(BaseUserInfo userInfo, string id) 单个删除工作流
        /// <summary>
        /// 单个删除工作流
        /// </summary>
        /// <param name="userInfo">用户</param>
        /// <param name="id">主键</param>
        /// <param name="organizeId">组织主键</param> 
        /// <returns>影响行数</returns>
        public int Delete(BaseUserInfo userInfo, string id)
        {
			int result = 0;

            var parameter = ServiceInfo.Create(userInfo, MethodBase.GetCurrentMethod());
            ServiceUtil.ProcessWorkFlowDb(userInfo, parameter, (dbHelper) =>
			{
				var workFlowProcess = new BaseWorkFlowProcessManager(dbHelper, userInfo);
				result = workFlowProcess.Delete(id);
			});
			return result;
        }
        #endregion

        #region public int BatchDelete(BaseUserInfo userInfo, string[] ids) 批量删除组织机构
        /// <summary>
        /// 批量删除组织机构
        /// </summary>
        /// <param name="userInfo">用户</param>
        /// <param name="ids">主键数组</param>
        /// <returns>影响行数</returns>
        public int BatchDelete(BaseUserInfo userInfo, string[] ids)
        {
			int result = 0;

            var parameter = ServiceInfo.Create(userInfo, MethodBase.GetCurrentMethod());
            ServiceUtil.ProcessWorkFlowDb(userInfo, parameter, (dbHelper) =>
			{
				var workFlowProcess = new BaseWorkFlowProcessManager(dbHelper, userInfo);
				result = workFlowProcess.Delete(ids);
			});
			return result;
        }
        #endregion

        #region public int BatchSave(BaseUserInfo userInfo, DataTable result) 批量保存权限
        /// <summary>
        /// 批量保存权限
        /// </summary>
        /// <param name="userInfo">用户</param>
        /// <param name="result">数据表</param>
        /// <returns>数据表</returns>
        public int BatchSave(BaseUserInfo userInfo, DataTable dt)
        {
			int result = 0;

            var parameter = ServiceInfo.Create(userInfo, MethodBase.GetCurrentMethod());
            ServiceUtil.ProcessWorkFlowDb(userInfo, parameter, (dbHelper) =>
			{
				var workFlowManager = new BaseWorkFlowProcessManager(dbHelper, userInfo);
				result = workFlowManager.BatchSave(dt);
				// this.StatusMessage = workFlowManager.StatusMessage;
				// this.StatusCode = workFlowManager.StatusCode;
				// ReturnDataTable = workFlowManager.GetDataTable();
			});
			return result;
        }
        #endregion

        #region public int SetDeleted(BaseUserInfo userInfo, string[] ids) 批量打删除标志
        /// <summary>
        /// 批量打删除标志
        /// </summary>
        /// <param name="userInfo">用户</param>
        /// <param name="ids">主键数组</param>
        /// <returns>影响行数</returns>
        public int SetDeleted(BaseUserInfo userInfo, string[] ids)
        {
			int result = 0;

            var parameter = ServiceInfo.Create(userInfo, MethodBase.GetCurrentMethod());
            ServiceUtil.ProcessWorkFlowDb(userInfo, parameter, (dbHelper) =>
			{
				var workFlowManager = new BaseWorkFlowProcessManager(dbHelper, userInfo);
				result = workFlowManager.SetDeleted(ids);
			});
			return result;
        }
        #endregion

        #region public DataTable GetBillTemplateDT(BaseUserInfo userInfo) 获取表单模板类型
        /// <summary>
        /// 获取表单模板类型
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public DataTable GetBillTemplateDT(BaseUserInfo userInfo)
        {
			var dt = new DataTable(BaseWorkFlowBillTemplateEntity.TableName);

            var parameter = ServiceInfo.Create(userInfo, MethodBase.GetCurrentMethod());
            ServiceUtil.ProcessWorkFlowDb(userInfo, parameter, (dbHelper) =>
			{
				var templateManager = new BaseWorkFlowBillTemplateManager(dbHelper, userInfo);
				dt = templateManager.GetDataTable(new KeyValuePair<string, object>(BaseWorkFlowBillTemplateEntity.FieldDeletionStateCode, 0), BaseWorkFlowBillTemplateEntity.FieldSortCode);
			});
			return dt;
        }
        #endregion

        #region public string GetProcessId(BaseUserInfo userInfo, string workFlowCode) 获取具体的审批流程
        /// <summary>
        /// 获取具体的审批流程
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        /// <param name="workFlowCode">工作流程编号</param>
        /// <returns>流程</returns>
        public string GetProcessId(BaseUserInfo userInfo, string workFlowCode)
        {
			string result = string.Empty;

            var parameter = ServiceInfo.Create(userInfo, MethodBase.GetCurrentMethod());
            ServiceUtil.ProcessWorkFlowDb(userInfo, parameter, (dbHelper) =>
			{
				var workFlowManager = new BaseWorkFlowProcessManager(dbHelper, userInfo);
				result = workFlowManager.GetProcessId(userInfo, workFlowCode);
			});
			return result;
        }
        #endregion

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="result"></param>
        /// <param name="userId"></param>
        /// <param name="categoryCode"></param>
        /// <param name="searchValue"></param>
        /// <param name="enabled"></param>
        /// <param name="deletionStateCode"></param>
        /// <returns></returns>
        public DataTable Search(BaseUserInfo userInfo, string userId, string categoryCode, string searchValue, bool? enabled, bool? deletionStateCode)
        {
			var dt = new DataTable(BaseWorkFlowBillTemplateEntity.TableName);

            var parameter = ServiceInfo.Create(userInfo, MethodBase.GetCurrentMethod());
            ServiceUtil.ProcessWorkFlowDb(userInfo, parameter, (dbHelper) =>
			{
				var templateManager = new BaseWorkFlowBillTemplateManager(dbHelper, userInfo);
				dt = templateManager.Search(userId, categoryCode, searchValue, enabled, deletionStateCode);
			});
			return dt;
        }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="result"></param>
		/// <param name="parameters"></param>
		/// <param name="id"></param>
		/// <returns></returns>
		public bool Exists(BaseUserInfo userInfo, List<KeyValuePair<string, object>> parameters, object id = null)
		{
			bool result = false;

            var parameter = ServiceInfo.Create(userInfo, MethodBase.GetCurrentMethod());
            ServiceUtil.ProcessWorkFlowDb(userInfo, parameter, (dbHelper) =>
			{
				var workFlowProcessManager = new BaseWorkFlowProcessManager(dbHelper, userInfo);
				result = workFlowProcessManager.Exists(parameters, id);
			});

			return result;
		}

		public List<BaseHolidaysEntity> GetHolidaysList(BaseUserInfo userInfo, string where)
		{
			List<BaseHolidaysEntity> result = null;

            var parameter = ServiceInfo.Create(userInfo, MethodBase.GetCurrentMethod());
            ServiceUtil.ProcessUserCenterDb(userInfo, parameter, (dbHelper) =>
			{
				var manager = new BaseHolidaysManager(dbHelper, userInfo);
				result = manager.GetList<BaseHolidaysEntity>(where);
			});
			return result;
		}

		public void AddHoliday(BaseUserInfo userInfo, string holiday, bool checkExists = true)
		{
            var parameter = ServiceInfo.Create(userInfo, MethodBase.GetCurrentMethod());
            ServiceUtil.ProcessUserCenterDb(userInfo, parameter, (dbHelper) =>
			{
				var manager = new BaseHolidaysManager(dbHelper, userInfo);
				manager.AddHoliday(holiday, checkExists);
			});
		}

		public int DeleteHoliday(BaseUserInfo userInfo, string holiday)
		{
			int result = 0;

            var parameter = ServiceInfo.Create(userInfo, MethodBase.GetCurrentMethod());
            ServiceUtil.ProcessUserCenterDb(userInfo, parameter, (dbHelper) =>
			{
				var manager = new BaseHolidaysManager(dbHelper, userInfo);
				result = manager.Delete(new KeyValuePair<string, object>(BaseHolidaysEntity.FieldHoliday, holiday));
			});

			return result;
		}
    }
}