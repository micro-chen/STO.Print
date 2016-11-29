//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd.  
//-----------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace DotNet.Business
{
	using DotNet.Utilities;
    using DotNet.Model;
    using DotNet.IService;

	/// <summary>
	/// BaseWorkFlowManager
	/// 流程管理的表结构定义部分
	/// 
	///	修改记录
	///		2007.03.15 版本：2.0 JiRiGaLa	规范表结构。
	///		2006.05.11 版本：1.1 JiRiGaLa	重新调整主键的规范化。
	/// 
	/// <author>
	///		<name>JiRiGaLa</name>
	///		<date>2006.05.11</date>
	/// </author> 
	/// </summary>
	public partial class BaseWorkFlowProcessManager : BaseManager
    {
        #region public bool UserIsInWorkFlow(string userId,  string processCode)
        /// <summary>
        /// 用户是否在某个流程里
        /// </summary>
        /// <param name="userId">用户主键</param>
        /// <param name="processCode">流程编号</param>
        /// <returns>是否在某个流程里</returns>
        public bool UserIsInWorkFlow(string userId,  string processCode)
        {
            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>(BaseWorkFlowProcessEntity.FieldCode, processCode));
            parameters.Add(new KeyValuePair<string, object>(BaseWorkFlowProcessEntity.FieldEnabled, 1));
            parameters.Add(new KeyValuePair<string, object>(BaseWorkFlowProcessEntity.FieldDeletionStateCode, 0));
            string processId = this.GetProperty(parameters, BaseWorkFlowProcessEntity.FieldId);
            // 没有找到这个审批流程
            if (string.IsNullOrEmpty(processId))
            {
                return false;
            }
            // 看是否在审核步骤里
            BaseWorkFlowActivityManager workFlowActivityManager = new Business.BaseWorkFlowActivityManager(this.UserInfo);
            parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>(BaseWorkFlowActivityEntity.FieldProcessId, processId));
            parameters.Add(new KeyValuePair<string, object>(BaseWorkFlowActivityEntity.FieldEnabled, 1));
            parameters.Add(new KeyValuePair<string, object>(BaseWorkFlowActivityEntity.FieldDeletionStateCode, 0));
            List<BaseWorkFlowActivityEntity> entityList = workFlowActivityManager.GetList<BaseWorkFlowActivityEntity>(parameters, BaseWorkFlowActivityEntity.FieldSortCode);
            foreach (var entity in entityList)
            {
                // 默认审核人
                if (!string.IsNullOrEmpty(entity.DefaultAuditUserId) && entity.DefaultAuditUserId.Equals(userId))
                {
                    return true;
                }
                // 在审核人列表里
                if (!string.IsNullOrEmpty(entity.AuditUserId) && entity.AuditUserId.IndexOf(userId) >= 0)
                {
                    return true;
                }
            }
            // 没有找到就返回没在审核流程里
            return false;
        }
        #endregion


        /// <summary>
        /// 获取反射调用的类
        /// 回写状态时用
        /// </summary>
        /// <param name="processCode">当前工作流主键</param>
        /// <returns></returns>
        public IWorkFlowManager GetWorkFlowManagerByCode(string processCode)
        {
            string id = this.GetIdByCode(processCode);
            return GetWorkFlowManager(id);
        }

        public IWorkFlowManager GetWorkFlowManager(string id)
        {
            IWorkFlowManager workFlowManager = null;
            BaseWorkFlowProcessEntity  workFlowProcessEntity = this.GetObject(id);
            if (!string.IsNullOrEmpty(workFlowProcessEntity.CallBackClass))
            {
                // 这里本来是想动态创建类库 编码外包[100]
                Assembly assembly = Assembly.Load(workFlowProcessEntity.CallBackAssembly);
                Type objType = assembly.GetType(workFlowProcessEntity.CallBackClass, true);
                workFlowManager = (IWorkFlowManager)Activator.CreateInstance(objType);
                workFlowManager.SetUserInfo(this.UserInfo);
                if (!string.IsNullOrEmpty(workFlowProcessEntity.CallBackTable))
                {
                    workFlowManager.CurrentTableName = workFlowProcessEntity.CallBackTable;
                }
            }
            else
            {
                workFlowManager = new BaseUserBillManager(this.DbHelper, this.UserInfo);
            }
            return workFlowManager;
        }

        /// <summary>
        /// 获取具体的审批流程
        /// </summary>
        /// <param name="processCode">工作流程编号</param>
        /// <returns>流程</returns>
        public string GetProcessId(BaseUserInfo userInfo, string processCode)
        {
            // 这里处理单据编号
            string workFlowId = string.Empty;
            // 1：按用户的审核是否存在
            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>(BaseWorkFlowProcessEntity.FieldCode, processCode));
            parameters.Add(new KeyValuePair<string, object>(BaseWorkFlowProcessEntity.FieldUserId, userInfo.Id));
            parameters.Add(new KeyValuePair<string, object>(BaseWorkFlowProcessEntity.FieldAuditCategoryCode, "ByUser"));
            parameters.Add(new KeyValuePair<string, object>(BaseWorkFlowProcessEntity.FieldEnabled, 1));
            parameters.Add(new KeyValuePair<string, object>(BaseWorkFlowProcessEntity.FieldDeletionStateCode, 0));
            workFlowId = this.GetProperty(parameters, BaseWorkFlowProcessEntity.FieldId);
            if (string.IsNullOrEmpty(workFlowId))
            {
                // 2: 找部门的审核是否存在
                parameters = new List<KeyValuePair<string, object>>();
                parameters.Add(new KeyValuePair<string, object>(BaseWorkFlowProcessEntity.FieldCode, processCode));
                parameters.Add(new KeyValuePair<string, object>(BaseWorkFlowProcessEntity.FieldOrganizeId, userInfo.DepartmentId));
                parameters.Add(new KeyValuePair<string, object>(BaseWorkFlowProcessEntity.FieldAuditCategoryCode, "ByOrganize"));
                parameters.Add(new KeyValuePair<string, object>(BaseWorkFlowProcessEntity.FieldEnabled, 1));
                parameters.Add(new KeyValuePair<string, object>(BaseWorkFlowProcessEntity.FieldDeletionStateCode, 0));
                workFlowId = this.GetProperty(parameters, BaseWorkFlowProcessEntity.FieldId);
            }
            if (string.IsNullOrEmpty(workFlowId))
            {
                // 3：若找不到用户的接着找按单据的审核是否存在
                parameters = new List<KeyValuePair<string, object>>();
                parameters.Add(new KeyValuePair<string, object>(BaseWorkFlowProcessEntity.FieldCode, processCode));
                parameters.Add(new KeyValuePair<string, object>(BaseWorkFlowProcessEntity.FieldAuditCategoryCode, "ByTemplate"));
                parameters.Add(new KeyValuePair<string, object>(BaseWorkFlowProcessEntity.FieldEnabled, 1));
                parameters.Add(new KeyValuePair<string, object>(BaseWorkFlowProcessEntity.FieldDeletionStateCode, 0));
                workFlowId = this.GetProperty(parameters, BaseWorkFlowProcessEntity.FieldId);
            }
            return workFlowId;
        }
        
		#region public DataTable GetListByOrganize(string organizeId) 按部门获得列表
		/// <summary>
		/// 按部门获得列表
		/// </summary>
		/// <param name="organizeId">部门主键</param>
		/// <returns>记录集</returns>
		public DataTable GetListByOrganize(string organizeId)
		{
			return this.GetDataTable(new KeyValuePair<string, object>(BaseWorkFlowProcessEntity.FieldOrganizeId, organizeId));
		}
		#endregion

        #region public string Add(BaseWorkFlowProcessEntity entity, out string statusCode)
        /// <summary>
		/// 添加
		/// </summary>
        /// <param name="entity">实体</param>
		/// <param name="statusCode">返回状态码</param>
		/// <returns>主键</returns>
		public string Add(BaseWorkFlowProcessEntity entity, out string statusCode)
		{
			string result = string.Empty;
			// 检查编号是否重复
            //if (this.Exists(new KeyValuePair<string, object>(BaseWorkFlowProcessEntity.FieldDeletionStateCode, 0), new KeyValuePair<string, object>(BaseWorkFlowProcessEntity.FieldCode, workFlowProcessEntity.Code)))
            //{
            //    // 编号已重复
            //    statusCode = Status.ErrorCodeExist.ToString();
            //}
            //else
            //{
				result = this.AddObject(entity);
				// 运行成功
				statusCode = Status.OKAdd.ToString();
            //}
			return result;
		}
		#endregion

        #region public int Update(BaseWorkFlowProcessEntity entity, out string statusCode)
        /// <summary>
		/// 更新
		/// </summary>
        /// <param name="entity">实体</param>
		/// <param name="statusCode">返回状态码</param>
		/// <returns>影响行数</returns>
		public int Update(BaseWorkFlowProcessEntity entity, out string statusCode)
		{
			int result = 0;
			// 检查编号是否重复
            //if (this.Exists(new KeyValuePair<string, object>(BaseWorkFlowProcessEntity.FieldDeletionStateCode, 0), new KeyValuePair<string, object>(BaseWorkFlowProcessEntity.FieldCode, entity.Code), entity.Id))
            //{
            //    // 文件夹名已重复
            //    statusCode = Status.ErrorCodeExist.ToString();
            //}
            //else
            //{
				// 进行更新操作
				result = this.UpdateObject(entity);
				if (result == 1)
				{
					statusCode = Status.OKUpdate.ToString();
				}
				else
				{
					// 数据可能被删除
					statusCode = Status.ErrorDeleted.ToString();
				}
            //}
			return result;
		}
		#endregion

		#region public new int BatchSave(DataTable result) 批量进行保存
		/// <summary>
		/// 批量进行保存
		/// </summary>
		/// <param name="dataSet">数据权限</param>
		/// <returns>影响行数</returns>
		public new int BatchSave(DataTable dt)
		{
			int result = 0;
			foreach (DataRow dr in dt.Rows)
			{
				BaseWorkFlowProcessEntity workFlowProcessEntity = BaseEntity.Create<BaseWorkFlowProcessEntity>(dr);
				// 删除状态
				if (dr.RowState == DataRowState.Deleted)
				{
					string id = dr[BaseWorkFlowProcessEntity.FieldId, DataRowVersion.Original].ToString();
					if (id.Length > 0)
					{
						result += this.Delete(id);
					}
				}
				// 被修改过
				if (dr.RowState == DataRowState.Modified)
				{
					string id = dr[BaseWorkFlowProcessEntity.FieldId, DataRowVersion.Original].ToString();
					if (id.Length > 0)
					{
                        workFlowProcessEntity.GetFrom(dr);
						// 判断是否允许编辑
						result += this.Update(workFlowProcessEntity);
					}
				}
				// 添加状态
				if (dr.RowState == DataRowState.Added)
				{
                    workFlowProcessEntity.GetFrom(dr);
					result += this.Add(workFlowProcessEntity).Length > 0 ? 1 : 0;
				}
				if (dr.RowState == DataRowState.Unchanged)
				{
					continue;
				}
				if (dr.RowState == DataRowState.Detached)
				{
					continue;
				}
			}
			return result;
		}
		#endregion

        #region public override DataTable GetDataTableById(string id) 按主键获取记录
        /// <summary>
        /// 按主键获取记录
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public override DataTable GetDataTableById(string id)
        {
           string sqlQuery = @"SELECT * 
                                      ,( SELECT BaseWorkFlowBillTemplate.Title 
                                          FROM BaseWorkFlowBillTemplate
                                          WHERE BaseWorkFlowBillTemplate.Id 
                                                  = BaseWorkFlowProcess.BillTemplateId 
                                                    ) AS BillTemplateName
                                      ,( Case BaseWorkFlowProcess.AuditCategoryCode 
                                           WHEN 'ByTemplate' THEN '按模版流程' 
                                           WHEN 'ByOrganize' THEN '按组织机构流程'
                                           WHEN 'ByUser' THEN '按用户流程' 
                                           END 
                                             ) AS AuditCategoryName
                                  FROM BaseWorkFlowProcess 
                                  WHERE Id = '" + id + "'";
           return dbHelper.Fill(sqlQuery);
        }
        #endregion
    }
}