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
    /// WorkFlowActivityAdminService
    /// 工作流定义服务
    /// 
    /// 修改记录
    /// 
    ///         2007.07.05 版本：1.0 完成了窗体加载、批量删除、关闭、写入日志功能
    ///		
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2007.07.05</date>
    /// </author> 
    /// </summary>
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    [System.ServiceModel.Activation.AspNetCompatibilityRequirements(RequirementsMode = System.ServiceModel.Activation.AspNetCompatibilityRequirementsMode.Allowed)]
    public class WorkFlowActivityAdminService : System.MarshalByRefObject,IWorkFlowActivityAdminService
    {
        /// <summary>
        /// 用户中心数据库连接
        /// </summary>
        private readonly string WorkFlowDbConnection = BaseSystemInfo.WorkFlowDbConnection;

        #region public DataTable GetDataTable(BaseUserInfo BaseUserInfo, string processId) 获取工作流步骤定义列表
        /// <summary>
        /// 获取工作流步骤定义列表
        /// </summary>
        /// <param name="userInfo">用户</param>
        /// <param name="processId">工作流主键</param> 
        /// <returns>数据表</returns>
        public DataTable GetDataTable(BaseUserInfo userInfo, string processId)
        {
			var dt = new DataTable(BaseWorkFlowProcessEntity.TableName);

            var parameter = ServiceInfo.Create(userInfo, MethodBase.GetCurrentMethod());
            ServiceUtil.ProcessWorkFlowDb(userInfo, parameter, (dbHelper) =>
			{
				var workFlowActivityManager = new BaseWorkFlowActivityManager(dbHelper, userInfo);

				List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
				parameters.Add(new KeyValuePair<string, object>(BaseWorkFlowActivityEntity.FieldProcessId, processId));
				parameters.Add(new KeyValuePair<string, object>(BaseWorkFlowActivityEntity.FieldEnabled, 1));
				parameters.Add(new KeyValuePair<string, object>(BaseWorkFlowActivityEntity.FieldDeletionStateCode, 0));

				dt = workFlowActivityManager.GetDataTable(parameters, BaseWorkFlowActivityEntity.FieldSortCode);
				dt.TableName = BaseWorkFlowActivityEntity.TableName;
			});

			return dt;
        }
        #endregion

        /// <summary>
        /// 添加工作流
        /// </summary>
        /// <param name="userInfo">当前用户</param>
        /// <param name="entity">工作流定义实体</param>
        /// <returns>主键</returns>
        public string Add(BaseUserInfo userInfo, BaseWorkFlowActivityEntity entity)
        {
			string result = string.Empty;

            var parameter = ServiceInfo.Create(userInfo, MethodBase.GetCurrentMethod());
            ServiceUtil.ProcessWorkFlowDb(userInfo, parameter, (dbHelper) =>
			{
				var workFlowActivityManager = new BaseWorkFlowActivityManager(dbHelper, userInfo);
				result = workFlowActivityManager.Add(entity);
			});
			return result;
        }

        #region public int BatchDelete(BaseUserInfo userInfo, string[] ids) 批量删除
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="userInfo">用户</param>
        /// <param name="ids">选种的数组</param>
        /// <returns>数据权限</returns>
        public int BatchDelete(BaseUserInfo userInfo, string[] ids)
        {
			int result = 0;

            var parameter = ServiceInfo.Create(userInfo, MethodBase.GetCurrentMethod());
            ServiceUtil.ProcessWorkFlowDb(userInfo, parameter, (dbHelper) =>
			{
				var WorkFlowActivity = new BaseWorkFlowActivityManager(dbHelper, userInfo);
				result = WorkFlowActivity.Delete(ids);
			});
			return result;
        }
        #endregion

        /// <summary>
        /// 批量保存
        /// </summary>
        /// <param name="userInfo">用户</param>
        /// <param name="result">数据表</param>
        /// <returns>影响行数</returns>
        public int BatchSave(BaseUserInfo userInfo, DataTable dt)
        {
			int result = 0;

            var parameter = ServiceInfo.Create(userInfo, MethodBase.GetCurrentMethod()); 
            ServiceUtil.ProcessWorkFlowDb(userInfo, parameter, (dbHelper) =>
			{
				var workFlowActivityManager = new BaseWorkFlowActivityManager(dbHelper, userInfo);
				result = workFlowActivityManager.BatchSave(dt);
				// result = Role.GetDataTableByOrganize(organizeId);
			});
			return result;
        }
    }
}