//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd.  
//-----------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data;

namespace DotNet.Business
{
    using DotNet.IService;
    using DotNet.Model;
    using DotNet.Utilities;

    /// <summary>
    /// BaseWorkFlowCurrentManager
    /// 流程管理.
    /// 
    /// 修改记录
    ///		
    ///		2012.04.04 版本：1.0 JiRiGaLa	脱离。
    /// 
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2012.04.04</date>
    /// </author>
    /// </summary>
    public partial class BaseWorkFlowCurrentManager : BaseManager, IBaseManager
    {
        /// <summary>
        /// 退回到第几步审核
        /// </summary>
        /// <param name="currentIds">当前工作流主键</param>
        /// <param name="auditIdea">审核意见</param>
        /// <param name="historyId">退回到第几步</param>
        /// <returns>影响行数</returns>
        public int AuditReject(string[] currentIds, string auditIdea, string historyId = null)
        {
            int result = 0;
            for (int i = 0; i < currentIds.Length; i++)
            {
                result += AuditReject(currentIds[i], auditIdea, historyId);
            }
            return result;
        }

        /// <summary>
        /// 退回到第几步审核
        /// </summary>
        /// <param name="currentId">当前工作流主键</param>
        /// <param name="auditIdea">审核意见</param>
        /// <param name="historyId">退回到第几步,这里不应该是activityId</param>
        /// <returns>影响行数</returns>
        public int AuditReject(string currentId, string auditIdea, string historyId = null)
        {
            IWorkFlowManager workFlowManager = this.GetWorkFlowManager(currentId);
            workFlowManager.SetUserInfo(this.UserInfo);
            return AuditReject(workFlowManager, currentId, auditIdea, historyId);
        }

        #region public int AuditReject(IWorkFlowManager workFlowManager, string currentId, string auditIdea, string historyId = null)
        /// <summary>
        /// 审核退回
        /// </summary>
        /// <param name="currentId">当前主键</param>
        /// <param name="auditIdea">批示</param>
        /// <param name="historyId">退回到第几步</param>
        /// <returns>影响行数</returns>
        public int AuditReject(IWorkFlowManager workFlowManager, string currentId, string auditIdea, string historyId = null)
        {
            int result = 0;

            lock (WorkFlowCurrentLock)
            {
                // using (TransactionScope transactionScope = new TransactionScope())
                //{
                try
                {
                    BaseWorkFlowCurrentEntity workFlowCurrentEntity = this.GetObject(currentId);
                    // 判断是是否是自由流（自由流这个为空）
                    if (workFlowCurrentEntity.ProcessId != null && workFlowCurrentEntity.ProcessId != 0)
                    {
                        // 若都已经被退回到彻底了，不能再被退回了，意思是已经退回到创建人了
                        if (workFlowCurrentEntity.ActivityId == null)
                        {
                            return result;
                        }
                    }

                    // 只有待审核状态的，才可以退回，还需要能持续退回
                    if ( //workFlowCurrentEntity.AuditStatus.Equals(AuditStatus.AuditComplete.ToString()) || 
                        workFlowCurrentEntity.AuditStatus.Equals(AuditStatus.AuditQuash.ToString()))
                    {
                        return result;
                    }

                    /* 彻底退回的处理
                    if (activityId == null)
                    {
                        // 发送给当初发起这个工作流的创建者
                        sendToUserId = workFlowCurrentEntity.CreateUserId;
                    }
                    else
                    {
                        BaseWorkFlowActivityManager workFlowActivityManager = new BaseWorkFlowActivityManager(this.DbHelper, this.UserInfo);
                        BaseWorkFlowActivityEntity workFlowActivityEntity = workFlowActivityManager.GetObject(activityId.ToString());
                        sendToUserId = workFlowActivityEntity.AuditUserId.ToString();
                    }
                    */

                    if (!string.IsNullOrEmpty(workFlowCurrentEntity.ToUserId))
                    {
                        // 若不是自己应该审核的,不应该能退回,在审核历史里需要控制一下
                        if (!this.UserInfo.IsAdministrator
                            && (!(workFlowCurrentEntity.AuditUserId.ToString().Equals(this.UserInfo.Id) || workFlowCurrentEntity.ToUserId.ToString().Equals(this.UserInfo.Id))))
                        {
                            return result;
                        }

                        // 一个审核者不能持续退回,但是发给自己的，还可以持续退回
                        if (workFlowCurrentEntity.AuditUserId.ToString().Equals(this.UserInfo.Id)
                            && workFlowCurrentEntity.AuditStatus.Equals(AuditStatus.AuditReject.ToString())
                            && (!workFlowCurrentEntity.ToUserId.ToString().Equals(this.UserInfo.Id)))
                        {
                            return result;
                        }
                    }

                    // 默认是自由工作流
                    string workFlowId = "0";
                    List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
                    DataTable dt = null;
                    BaseWorkFlowHistoryManager workFlowHistoryManager = new BaseWorkFlowHistoryManager(this.DbHelper, this.UserInfo);
                    string rejectToActivityId = string.Empty;
                    // 工作流主键
                    if (workFlowCurrentEntity.ProcessId != null && workFlowCurrentEntity.ProcessId != 0)
                    {
                        workFlowId = workFlowCurrentEntity.ProcessId.ToString();
                        BaseWorkFlowStepManager workFlowStepManager = new BaseWorkFlowStepManager(this.DbHelper, this.UserInfo);
                        parameters.Add(new KeyValuePair<string, object>(BaseWorkFlowStepEntity.FieldCategoryCode, workFlowCurrentEntity.CategoryCode));
                        parameters.Add(new KeyValuePair<string, object>(BaseWorkFlowStepEntity.FieldObjectId, workFlowCurrentEntity.ObjectId));
                        parameters.Add(new KeyValuePair<string, object>(BaseWorkFlowStepEntity.FieldProcessId, workFlowId));
                        parameters.Add(new KeyValuePair<string, object>(BaseWorkFlowStepEntity.FieldEnabled, 1));
                        parameters.Add(new KeyValuePair<string, object>(BaseWorkFlowStepEntity.FieldDeletionStateCode, 0));
                        dt = workFlowStepManager.GetDataTable(parameters, BaseWorkFlowStepEntity.FieldSortCode);
                        if (dt.Rows.Count > 0)
                        {
                            dt.Columns.Remove(BaseWorkFlowStepEntity.FieldId);
                            dt.Columns[BaseWorkFlowStepEntity.FieldActivityId].ColumnName = BaseWorkFlowStepEntity.FieldId;
                        }
                        else
                        {
                            // 判断是是否是自由流（自由流这个为空）
                            if (workFlowCurrentEntity.ProcessId != null && workFlowCurrentEntity.ProcessId != 0)
                            {
                                return result;
                            }
                        }

                        if (!string.IsNullOrEmpty(historyId))
                        {
                            BaseWorkFlowHistoryEntity workFlowHistoryEntity = workFlowHistoryManager.GetObject(historyId);
                            rejectToActivityId = workFlowHistoryEntity.ActivityId.ToString();
                        }
                        else
                        {
                            // 2. 从工作流审核模板里选取审核步骤 下一步是多少？按工作流进行查找审核步骤
                            // 3. 下一步是多少？按工作流进行查找审核步骤
                            BaseWorkFlowActivityManager workFlowActivityManager = new BaseWorkFlowActivityManager(this.DbHelper, this.UserInfo);
                            parameters = new List<KeyValuePair<string, object>>();
                            parameters.Add(new KeyValuePair<string, object>(BaseWorkFlowActivityEntity.FieldProcessId, workFlowId));
                            parameters.Add(new KeyValuePair<string, object>(BaseWorkFlowActivityEntity.FieldEnabled, 1));
                            parameters.Add(new KeyValuePair<string, object>(BaseWorkFlowActivityEntity.FieldDeletionStateCode, 0));
                            DataTable dataTableActivity = workFlowActivityManager.GetDataTable(parameters, BaseWorkFlowActivityEntity.FieldSortCode);
                            if (dataTableActivity.Rows.Count > 0)
                            {
                                dt = dataTableActivity;
                            }
                            string activityId = string.Empty;
                            if (workFlowCurrentEntity.ActivityId != null)
                            {
                                activityId = workFlowCurrentEntity.ActivityId.ToString();
                            }

                            if (!string.IsNullOrEmpty(activityId))
                            {
                                rejectToActivityId = BaseSortLogic.GetPreviousId(dt, activityId.ToString());
                            }
                            else
                            {
                                if (dt.Rows.Count > 0)
                                {
                                    rejectToActivityId = dt.Rows[0][BaseWorkFlowActivityEntity.FieldId].ToString();
                                }
                            }
                            if (string.IsNullOrEmpty(rejectToActivityId))
                            {
                                // 这里已经是最后一步，发出者这里了
                                // 已经到自己手里的，没必要再继续退回了
                                if (this.UserInfo.Id.Equals(workFlowCurrentEntity.CreateUserId))
                                {
                                    return result;
                                }
                            }
                        }
                    }

                    // 这里不应该是发给所有的人，审核过的人，才可以看到才对。
                    string[] userIds = workFlowHistoryManager.GetProperties(new KeyValuePair<string, object>(BaseWorkFlowCurrentEntity.FieldProcessId, workFlowCurrentEntity.ProcessId)
                        , BaseWorkFlowCurrentEntity.FieldAuditUserId);
                    userIds = StringUtil.Concat(userIds, workFlowCurrentEntity.CreateUserId);

                    // 进行更新操作
                    workFlowCurrentEntity = this.StepAuditReject(currentId, auditIdea, workFlowCurrentEntity.CreateUserId, rejectToActivityId);
                    if (workFlowCurrentEntity.Id != null)
                    {
                        // 5.发送提示信息
                        if (workFlowManager != null)
                        {
                            workFlowManager.OnAuditReject(workFlowCurrentEntity);
                            /*
                            // 这个是表明已经彻底退回了，不是退回给指定的一个人了
                            if (activityId == null)
                            {
                                // 已经审核过的人，都需要得到退回的信息
                                BaseWorkFlowActivityManager workFlowActivityManager = new BaseWorkFlowActivityManager(this.UserInfo);
                                var result = workFlowActivityManager.GetBackToDT(workFlowCurrentEntity);
                                userIds = BaseBusinessLogic.FieldToArray(result, BaseWorkFlowActivityEntity.FieldAuditUserId).Distinct<string>().Where(t => !string.IsNullOrEmpty(t)).ToArray();
                            }
                            */
                            // 都给谁发送退回的消息
                            workFlowManager.SendRemindMessage(workFlowCurrentEntity, AuditStatus.AuditReject, userIds, workFlowCurrentEntity.ToDepartmentId, workFlowCurrentEntity.ToRoleId);
                        }
                        result = 1;
                    }
                    else
                    {
                        // 数据可能被删除
                        this.StatusCode = Status.ErrorDeleted.ToString();
                    }
                    // 应该给创建者一个提醒消息
                    this.StatusMessage = this.GetStateMessage(this.StatusCode);
                }
                catch (System.Exception ex)
                {
                    // 在本地记录异常
                    FileUtil.WriteException(UserInfo, ex);
                }
                finally
                {
                }
                // transactionScope.Complete();
                // }
            }
            return result;
        }
        #endregion

        #region private int StepAuditReject(string currentId, string auditIdea, BaseWorkFlowStepEntity workFlowStepEntity) 审核通过(不需要再发给别人了)
        /// <summary>
        /// 审核退回(不需要再发给别人了)
        /// </summary>
        /// <param name="currentId">当前主键</param>
        /// <param name="auditIdea">批示</param>
        /// <returns>影响行数</returns>
        private int StepAuditReject(string currentId, string auditIdea, BaseWorkFlowStepEntity workFlowStepEntity)
        {
            BaseWorkFlowCurrentEntity workFlowCurrentEntity = this.GetObject(currentId);
            // 1.记录当前的审核时间、审核人信息
            workFlowCurrentEntity.AuditDate = DateTime.Now;
            workFlowCurrentEntity.AuditIdea = auditIdea;
            workFlowCurrentEntity.AuditUserId = this.UserInfo.Id;
            workFlowCurrentEntity.AuditUserRealName = this.UserInfo.RealName;
            workFlowCurrentEntity.AuditStatus = AuditStatus.AuditReject.ToString();
            workFlowCurrentEntity.AuditStatusName = AuditStatus.AuditReject.ToDescription();
            if (!string.IsNullOrEmpty(workFlowCurrentEntity.ActivityFullName))
            {
                workFlowCurrentEntity.Description = string.Format("从{0}退回到{1}{2}", workFlowCurrentEntity.ActivityFullName, workFlowStepEntity.ActivityFullName, !string.IsNullOrEmpty(workFlowStepEntity.Description) ? "，" + workFlowStepEntity.Description : string.Empty);
            }
            // 2.记录审核日志
            this.AddHistory(workFlowCurrentEntity);
            // 3.上一个审核结束了，新的审核又开始了，更新待审核情况
            workFlowCurrentEntity.ActivityId = workFlowStepEntity.ActivityId;
            workFlowCurrentEntity.ActivityCode = workFlowStepEntity.ActivityCode;
            workFlowCurrentEntity.ActivityFullName = workFlowStepEntity.ActivityFullName;
            workFlowCurrentEntity.ActivityType = workFlowStepEntity.ActivityType;
            workFlowCurrentEntity.ToRoleId = workFlowStepEntity.AuditRoleId;
            workFlowCurrentEntity.ToRoleRealName = workFlowStepEntity.AuditRoleRealName;
            workFlowCurrentEntity.ToDepartmentId = workFlowStepEntity.AuditDepartmentId;
            workFlowCurrentEntity.ToDepartmentName = workFlowStepEntity.AuditDepartmentName;
            workFlowCurrentEntity.ToUserId = workFlowStepEntity.AuditUserId;
            workFlowCurrentEntity.ToUserRealName = workFlowStepEntity.AuditUserRealName;
            workFlowCurrentEntity.SortCode = workFlowStepEntity.SortCode;
            return this.UpdateObject(workFlowCurrentEntity);
        }
        #endregion

        #region private BaseWorkFlowCurrentEntity StepAuditReject(string currentId, string auditIdea, string toUserId, string activityId = null)
        /// <summary>
        /// 审核退回详细步骤
        /// </summary>
        /// <param name="currentId">当前主键</param>
        /// <param name="auditIdea">批示</param>
        /// <param name="toUserId">发送给</param>
        /// <param name="activityId">退回到指定步骤</param>
        /// <returns>影响行数</returns>
        private BaseWorkFlowCurrentEntity StepAuditReject(string currentId, string auditIdea, string toUserId = null, string activityId = null)
        {
            BaseWorkFlowCurrentEntity workFlowCurrentEntity = this.GetObject(currentId);
            // 1.记录审核时间、审核人
            workFlowCurrentEntity.AuditUserId = this.UserInfo.Id;
            workFlowCurrentEntity.AuditUserCode = this.UserInfo.Code;
            workFlowCurrentEntity.AuditUserRealName = this.UserInfo.RealName;
            workFlowCurrentEntity.AuditStatus = AuditStatus.AuditReject.ToString();
            workFlowCurrentEntity.AuditStatusName = AuditStatus.AuditReject.ToDescription();
            workFlowCurrentEntity.AuditDate = DateTime.Now;
            workFlowCurrentEntity.AuditIdea = auditIdea;
            // 2.记录日志
            this.AddHistory(workFlowCurrentEntity);

            // 3.更新待审核情况，流程已经结束了
            workFlowCurrentEntity.ActivityId = null;
            if (!string.IsNullOrEmpty(activityId))
            {
                workFlowCurrentEntity.ActivityId = int.Parse(activityId);
                // 1:先看流程审核步骤里是否有这个记录
                BaseWorkFlowStepManager workFlowStepManager = new BaseWorkFlowStepManager(this.DbHelper, this.UserInfo);
				BaseWorkFlowStepEntity workFlowStepEntity = BaseEntity.Create<BaseWorkFlowStepEntity>(
                    workFlowStepManager.GetDataTable(
                    new KeyValuePair<string, object>(BaseWorkFlowStepEntity.FieldActivityId, activityId)
                    , new KeyValuePair<string, object>(BaseWorkFlowStepEntity.FieldObjectId, workFlowCurrentEntity.ObjectId)));
                if (workFlowStepEntity != null && workFlowStepEntity.Id != null)
                {
                    workFlowCurrentEntity.SortCode = workFlowStepEntity.SortCode;
                    workFlowCurrentEntity.ToUserId = workFlowStepEntity.AuditUserId;
                    workFlowCurrentEntity.ToUserRealName = workFlowStepEntity.AuditUserRealName;
                    workFlowCurrentEntity.ToDepartmentId = workFlowStepEntity.AuditDepartmentId;
                    workFlowCurrentEntity.ToDepartmentName = workFlowStepEntity.AuditDepartmentName;
                    workFlowCurrentEntity.ToRoleId = workFlowStepEntity.AuditRoleId;
                    workFlowCurrentEntity.ToRoleRealName = workFlowStepEntity.AuditRoleRealName;
                    workFlowCurrentEntity.Description = string.Format("从{0}退回到{1}", workFlowCurrentEntity.ActivityFullName, workFlowStepEntity.FullName);
                    workFlowCurrentEntity.ActivityCode = workFlowStepEntity.Code;
                    workFlowCurrentEntity.ActivityFullName = workFlowStepEntity.FullName;
                }
                else
                {
                    // 2:若没在流程审核步骤里那就从定义里找
                    BaseWorkFlowActivityManager workFlowActivityManager = new BaseWorkFlowActivityManager(this.DbHelper, this.UserInfo);
                    BaseWorkFlowActivityEntity workFlowActivityEntity = workFlowActivityManager.GetObject(activityId);
                    workFlowCurrentEntity.SortCode = workFlowActivityEntity.SortCode;
                    workFlowCurrentEntity.ToUserId = workFlowActivityEntity.AuditUserId;
                    workFlowCurrentEntity.ToUserRealName = workFlowActivityEntity.AuditUserRealName;
                    workFlowCurrentEntity.ToDepartmentId = workFlowActivityEntity.AuditDepartmentId;
                    workFlowCurrentEntity.ToDepartmentName = workFlowActivityEntity.AuditDepartmentName;
                    workFlowCurrentEntity.ToRoleId = workFlowActivityEntity.AuditRoleId;
                    workFlowCurrentEntity.ToRoleRealName = workFlowActivityEntity.AuditRoleRealName;
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(toUserId))
                {
                    BaseUserManager userManager = new BaseUserManager(UserInfo);
                    BaseUserEntity userEntity = userManager.GetObject(toUserId);
                    workFlowCurrentEntity.ToUserId = userEntity.Id;
                    workFlowCurrentEntity.ToUserRealName = userEntity.RealName;
                    workFlowCurrentEntity.ToDepartmentId = userEntity.DepartmentId;
                    workFlowCurrentEntity.ToDepartmentName = userEntity.DepartmentName;
                    workFlowCurrentEntity.ToRoleId = null;
                    workFlowCurrentEntity.ToRoleRealName = null;
                }
                //workFlowCurrentEntity.SortCode = null;
            }
            workFlowCurrentEntity.SendDate = DateTime.Now;
            workFlowCurrentEntity.Enabled = 0;
            // 4.生成审核结束的记录
            this.UpdateObject(workFlowCurrentEntity);
            return workFlowCurrentEntity;
        }
        #endregion
    }
}