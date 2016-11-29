//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd.  
//-----------------------------------------------------------------

using System;
using System.Linq;
using System.Collections.Generic;
using System.Data;

namespace DotNet.Business
{
    using DotNet.Utilities;
    using DotNet.Model;

    /// <summary>
    /// BaseWorkFlowActivityManager
    /// 流程定义.
    /// 
    /// 修改记录
    ///		
    ///		2007.08.02 版本：1.0 JiRiGaLa	编写主键。
    /// 
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2007.08.02</date>
    /// </author>
    /// </summary>
    public partial class BaseWorkFlowActivityManager : BaseManager, IBaseManager
    {
        public DataTable GetActivityDTByCode(string workFlowCode)
        {
            BaseWorkFlowProcessManager workFlowProcessManager = new BaseWorkFlowProcessManager(this.DbHelper, this.UserInfo);
            string workFlowId = workFlowProcessManager.GetId(
                new KeyValuePair<string, object>(BaseWorkFlowProcessEntity.FieldCode, workFlowCode)
                , new KeyValuePair<string, object>(BaseWorkFlowProcessEntity.FieldDeletionStateCode, 0)
                , new KeyValuePair<string, object>(BaseWorkFlowProcessEntity.FieldEnabled, 1));
            return GetActivityDTById(workFlowId);
        }

        public DataTable GetActivityDTById(string processId)
        {
            DataTable dtWorkFlowActivity = null;
            if (!string.IsNullOrEmpty(processId))
            {
                List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
                parameters.Add(new KeyValuePair<string, object>(BaseWorkFlowActivityEntity.FieldProcessId, processId));
                parameters.Add(new KeyValuePair<string, object>(BaseWorkFlowActivityEntity.FieldEnabled, 1));
                parameters.Add(new KeyValuePair<string, object>(BaseWorkFlowActivityEntity.FieldDeletionStateCode, 0));
                dtWorkFlowActivity = this.GetDataTable(parameters, BaseWorkFlowActivityEntity.FieldSortCode);
            }
            return dtWorkFlowActivity;
        }

        public string GetWorkFlowActivityByUser(string userId, string billTemplateId)
        {
            BaseWorkFlowProcessManager workFlowProcessManager = new BaseWorkFlowProcessManager(this.DbHelper, this.UserInfo);
            string workFlowId = workFlowProcessManager.GetId(
                new KeyValuePair<string, object>(BaseWorkFlowProcessEntity.FieldDeletionStateCode, 0)
                , new KeyValuePair<string, object>(BaseWorkFlowProcessEntity.FieldUserId, userId)
                , new KeyValuePair<string, object>(BaseWorkFlowProcessEntity.FieldBillTemplateId, billTemplateId));
            return GetWorkFlowActivity(workFlowId);
        }

        public string GetWorkFlowActivityByOrganize(string organizeId, string billTemplateId)
        {
            BaseWorkFlowProcessManager workFlowProcessManager = new BaseWorkFlowProcessManager(this.DbHelper, this.UserInfo);
            string workFlowId = workFlowProcessManager.GetId(
                new KeyValuePair<string, object>(BaseWorkFlowProcessEntity.FieldDeletionStateCode, 0)
                , new KeyValuePair<string, object>(BaseWorkFlowProcessEntity.FieldOrganizeId, organizeId)
                , new KeyValuePair<string, object>(BaseWorkFlowProcessEntity.FieldBillTemplateId, billTemplateId));
            return GetWorkFlowActivity(workFlowId);
        }

        public string GetWorkFlowActivityByCode(string workFlowCode)
        {
            BaseWorkFlowProcessManager workFlowProcessManager = new BaseWorkFlowProcessManager(this.DbHelper, this.UserInfo);
            string workFlowId = workFlowProcessManager.GetId(new KeyValuePair<string, object>(BaseWorkFlowProcessEntity.FieldDeletionStateCode, 0), new KeyValuePair<string, object>(BaseWorkFlowProcessEntity.FieldCode, workFlowCode));
            return GetWorkFlowActivity(workFlowId);
        }

        /// <summary>
        /// 获取审批流程步骤
        /// </summary>
        /// <returns>审批流程</returns>
        public string GetWorkFlowActivity(string processId)
        {
            string retuanValue = string.Empty;
            // 需要审核的人列表
            string auditList = string.Empty;
            // 需要共享的列表
            string shareList = string.Empty;
            if (!string.IsNullOrEmpty(processId))
            {
                DataTable dtWorkFlowActivity = this.GetActivityDTById(processId);
                foreach (DataRow dr in dtWorkFlowActivity.Rows)
                {
                    string userRealName = dr[BaseWorkFlowActivityEntity.FieldAuditUserRealName].ToString();
                    string departmentName = dr[BaseWorkFlowActivityEntity.FieldAuditDepartmentName].ToString();
                    string role = dr[BaseWorkFlowActivityEntity.FieldAuditRoleRealName].ToString();

                    if (dr[BaseWorkFlowActivityEntity.FieldEnabled].ToString().Equals("1"))
                    {
                        if (string.IsNullOrEmpty(role))
                        {
                            auditList += departmentName
                            + "[" + userRealName + "."
                            + dr[BaseWorkFlowActivityEntity.FieldAuditUserCode].ToString() + "]" + " → ";
                        }
                        else
                        {
                            auditList += "[" + role + "]" + " → ";
                        }
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(role))
                        {
                            shareList += departmentName
                            + "[" + userRealName + "."
                            + dr[BaseWorkFlowActivityEntity.FieldAuditUserCode].ToString() + "]" + " → ";
                        }
                        else
                        {
                            shareList += "[" + role + "]" + " → ";
                        }
                    }
                }
                auditList = auditList.TrimEnd().TrimEnd('→');
                shareList = shareList.TrimEnd().TrimEnd('→');
            }
            if (!string.IsNullOrEmpty(auditList))
            {
                auditList = "审核：" + auditList;
            }
            if (!string.IsNullOrEmpty(shareList))
            {
                auditList += "<br>共享：" + shareList;
            }
            return auditList;
        }

        public string GetBackToUserId(BaseWorkFlowCurrentEntity entity)
        {
            string userId = string.Empty;
            var dt = GetBackToDT(entity);
            if (dt.Rows.Count > 0)
            {
                userId = dt.Rows[0][BaseWorkFlowHistoryEntity.FieldAuditUserId].ToString();
            }
            return userId;
        }

        /// <summary>
        /// 获取退回人员列表
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public DataTable GetBackToDT(string currentFlowId, string processId)
        {
            string sqlQuery = @"  SELECT MAX(Id) AS Id, ActivityId, AuditUserId, AuditUserRealName
									FROM BaseWorkFlowHistory
									WHERE ( AuditStatus != 'AuditReject'
											AND CurrentFlowId = '" + currentFlowId + @"'
											AND ProcessId = " + processId + @"
											-- AND AuditUserId = ToUserId
										  )
								 GROUP BY ActivityId, AuditUserId, AuditUserRealName
								 ORDER BY MAX(SortCode) ";
            return DbHelper.Fill(sqlQuery);
        }

        /// <summary>
        /// 获取退回人员列表
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public DataTable GetBackToDT(BaseWorkFlowCurrentEntity entity)
        {
            string processId = entity.ProcessId.ToString();
            string sortCode = entity.SortCode.ToString();
            string sqlQuery = @"  SELECT MAX(Id) AS Id, ActivityId, AuditUserId, AuditUserRealName
									FROM BaseWorkFlowHistory
									WHERE ( AuditStatus != 'AuditReject'
											AND CurrentFlowId = '" + entity.Id + @"'
											AND ProcessId = " + processId + @"
											-- AND AuditUserId = ToUserId
											AND ActivityId IN
												(
												   SELECT Id
													 FROM BaseWorkFlowActivity
													WHERE (DeletionStateCode = 0) 
														  AND (Enabled = 1) 
														  AND (ProcessId = " + processId + @") 
														  AND (SortCode < " + sortCode + @")                         
												)
										  )
								 GROUP BY ActivityId, AuditUserId, AuditUserRealName
								 ORDER BY MAX(SortCode) ";
            // 发出人是否在单据里？
            var dt = DbHelper.Fill(sqlQuery);
            dt.TableName = "BaseWorkFlowHistory";
            if (!BaseBusinessLogic.Exists(dt, "AuditUserId", entity.CreateUserId))
            {
                DataRow dr = dt.NewRow();
                dr["Id"] = DBNull.Value;
                dr["ActivityId"] = DBNull.Value;
                dr["AuditUserId"] = entity.CreateUserId;
                dr["AuditUserRealName"] = entity.CreateBy;
                dt.Rows.InsertAt(dr, 0);
                dt.AcceptChanges();
            }
            return dt;
        }

        #region public override int BatchSave(DataTable result) 批量保存
        /// <summary>
        /// 批量保存
        /// </summary>
        /// <param name="result">数据表</param>
        /// <returns>影响行数</returns>
        public override int BatchSave(DataTable dt)
        {
            int result = 0;
            BaseWorkFlowActivityEntity workFlowActivityEntity = new BaseWorkFlowActivityEntity();
            foreach (DataRow dr in dt.Rows)
            {
                // 删除状态
                if (dr.RowState == DataRowState.Deleted)
                {
                    string id = dr[BaseRoleEntity.FieldId, DataRowVersion.Original].ToString();
                    if (id.Length > 0)
                    {
                        result += this.Delete(id);
                    }
                }
                // 被修改过
                if (dr.RowState == DataRowState.Modified)
                {
                    string id = dr[BaseRoleEntity.FieldId, DataRowVersion.Original].ToString();
                    if (!String.IsNullOrEmpty(id))
                    {
                        workFlowActivityEntity.GetFrom(dr);
                        result += this.UpdateObject(workFlowActivityEntity);
                    }
                }
                // 添加状态
                if (dr.RowState == DataRowState.Added)
                {
                    workFlowActivityEntity.GetFrom(dr);
                    result += this.AddObject(workFlowActivityEntity).Length > 0 ? 1 : 0;
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

        #region public int Replace(string oldUserId, string newUserId)替换流程定义中的用户
        /// <summary>
        /// 替换流程定义中的用户
        /// </summary>
        /// <param name="oldUserId">原来的userId</param>
        /// <param name="newUserId">新的userId</param>
        /// <returns>影响行数</returns>
        public int ReplaceUser(string oldUserId, string newUserId)
        {
            BaseUserManager userManager = new BaseUserManager(this.UserInfo);
            BaseUserEntity newUserEntity = userManager.GetObject(newUserId);
            SQLBuilder sqlBuilder = new SQLBuilder(this.DbHelper);
            sqlBuilder.BeginUpdate(this.CurrentTableName);
            sqlBuilder.SetValue(BaseWorkFlowActivityEntity.FieldAuditUserId, newUserEntity.Id);
            sqlBuilder.SetValue(BaseWorkFlowActivityEntity.FieldAuditUserCode, newUserEntity.Code);
            sqlBuilder.SetValue(BaseWorkFlowActivityEntity.FieldAuditUserRealName, newUserEntity.RealName);
            sqlBuilder.SetValue(BaseWorkFlowActivityEntity.FieldAuditDepartmentId, newUserEntity.DepartmentId);
            sqlBuilder.SetValue(BaseWorkFlowActivityEntity.FieldAuditDepartmentName, newUserEntity.DepartmentName);
            sqlBuilder.SetWhere(BaseWorkFlowActivityEntity.FieldAuditUserId, oldUserId, "OldUserId");
            return sqlBuilder.EndUpdate();
        }
        #endregion

        /// <summary>
        /// 获取委托列表
        /// </summary>
        /// <param name="permissionCode">操作权限编号</param>
        /// <param name="userId">用户主键</param>
        /// <returns>数据表</returns>
        public DataTable GetAuthorizeDT(string systemCode, string permissionCode, string userId = null)
        {
            if (userId == null)
            {
                userId = this.UserInfo.Id;
            }
            // 获取别人委托我的列表
            string permissionId = string.Empty;
            permissionId = BaseModuleManager.GetIdByCodeByCache(systemCode, permissionCode);
            string tableName = systemCode + "PermissionScope";
            BasePermissionScopeManager permissionScopeManager = new BasePermissionScopeManager(this.UserInfo, tableName);
            string[] names = new string[]{
				BasePermissionScopeEntity.FieldDeletionStateCode
				, BasePermissionScopeEntity.FieldEnabled
				, BasePermissionScopeEntity.FieldResourceCategory
				, BasePermissionScopeEntity.FieldPermissionId
				, BasePermissionScopeEntity.FieldTargetCategory
				, BasePermissionScopeEntity.FieldTargetId};
            Object[] values = new Object[] { 0, 1, BaseUserEntity.TableName, permissionId, BaseUserEntity.TableName, userId };
            // 排除过期的，此方法有性能问题，已经放到后台的Sql中处理。 comment by zgl on 2011-10-27
            //var result = manager.GetDataTable(names, values);
            //for (int i = 0; i < result.Rows.Count; i++)
            //{
            //    if (!string.IsNullOrEmpty(result.Rows[i][BasePermissionScopeEntity.FieldEndDate].ToString()))
            //    {
            //        // 过期的不显示
            //        if (DateTime.Parse(result.Rows[i][BasePermissionScopeEntity.FieldEndDate].ToString()).Date < DateTime.Now.Date)
            //        {
            //            result.Rows.RemoveAt(i);
            //            // result 行数会减少
            //            i--;
            //        }
            //    }
            //}

            //排除过期的，已经放到后台的Sql中处理。
            var dt = permissionScopeManager.GetAuthoriedList(BaseUserEntity.TableName, permissionId, BaseUserEntity.TableName, userId);
            string[] userIds = BaseBusinessLogic.FieldToArray(dt, BasePermissionScopeEntity.FieldResourceId).Distinct<string>().Where(t => !string.IsNullOrEmpty(t)).ToArray();
            BaseUserManager userManager = new BaseUserManager(this.UserInfo);
            return userManager.GetDataTable(userIds);
        }
    }
}