//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd.  
//-----------------------------------------------------------------

using System;
using System.Data;
using System.Collections.Generic;
using System.Reflection;
using System.Transactions;

namespace DotNet.Business
{
    using DotNet.Utilities;
    using DotNet.Model;
    using DotNet.IService;

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
        public int FreeAuditReject(BaseWorkFlowAuditInfo workFlowAuditInfo)
        {
            IWorkFlowManager workFlowManager = this.GetWorkFlowManager(workFlowAuditInfo.Id);
            workFlowManager.SetUserInfo(this.UserInfo);
            return FreeAuditReject(workFlowManager, workFlowAuditInfo);
        }

        #region public int FreeAuditReject(IWorkFlowManager workFlowManager, BaseWorkFlowAuditInfo workFlowAuditInfo) 自由审批退回
        /// <summary>
        /// 自由审批退回
        /// </summary>
        /// <param name="workFlowManager">流程控制管理器</param>
        /// <param name="workFlowAuditInfo">流程信息</param>
        /// <returns>影响行数</returns>
        public int FreeAuditReject(IWorkFlowManager workFlowManager, BaseWorkFlowAuditInfo workFlowAuditInfo)
        {
            // 返回值
            int result = 0;
            // 这里用锁的机制，提高并发控制能力
            lock (WorkFlowCurrentLock)
            {
                try
                {
                    // 开始事务
                    this.DbHelper.BeginTransaction();
                    BaseWorkFlowStepEntity workFlowStepEntity = new BaseWorkFlowStepEntity();
                    if (!string.IsNullOrEmpty(workFlowAuditInfo.ActivityId))
                    {
                        workFlowStepEntity.ActivityId = int.Parse(workFlowAuditInfo.ActivityId);
                        workFlowStepEntity.ActivityCode = workFlowAuditInfo.ActivityCode;
                        workFlowStepEntity.ActivityFullName = workFlowAuditInfo.ActivityFullName;
                    }
                    // 是否提交给用户审批
                    if (!string.IsNullOrEmpty(workFlowAuditInfo.ToUserId))
                    {
                        workFlowStepEntity.AuditUserId = workFlowAuditInfo.ToUserId;
                        workFlowStepEntity.AuditUserRealName = BaseUserManager.GetRealNameByCache(workFlowAuditInfo.ToUserId);
                    }
                    // 是否提交给部门审批
                    if (!string.IsNullOrEmpty(workFlowAuditInfo.ToDepartmentId))
                    {
                        workFlowStepEntity.AuditDepartmentId = workFlowAuditInfo.ToDepartmentId;
                        workFlowStepEntity.AuditDepartmentName = BaseOrganizeManager.GetNameByCache(workFlowAuditInfo.ToDepartmentId);
                    }
                    // 是否提交给角色审批
                    if (!string.IsNullOrEmpty(workFlowAuditInfo.ToRoleId))
                    {
                        workFlowStepEntity.AuditRoleId = workFlowAuditInfo.ToRoleId;
                        workFlowStepEntity.AuditRoleRealName = BaseRoleManager.GetRealNameByCache(this.UserInfo.SystemCode, workFlowAuditInfo.ToRoleId);
                    }
                    // 获取排序码
                    workFlowStepEntity.SortCode = int.Parse(new BaseSequenceManager().Increment("WorkFlow", 10000000));
                    // 进行更新操作
                    result = this.StepAuditReject(workFlowAuditInfo.Id, workFlowAuditInfo.AuditIdea, workFlowStepEntity);
                    if (result == 0)
                    {
                        // 数据可能被删除
                        this.StatusCode = Status.ErrorDeleted.ToString();
                    }
                    BaseWorkFlowCurrentEntity workFlowCurrentEntity = this.GetObject(workFlowAuditInfo.Id);
                    // 发送提醒信息
                    if (workFlowManager != null)
                    {
                        if (!string.IsNullOrEmpty(workFlowStepEntity.AuditUserId))
                        {
                            workFlowStepEntity.AuditDepartmentId = null;
                            workFlowStepEntity.AuditRoleId = null;
                        }
                        workFlowManager.OnAutoAuditReject(workFlowCurrentEntity);
                        workFlowManager.SendRemindMessage(workFlowCurrentEntity, AuditStatus.AuditReject, new string[] { workFlowCurrentEntity.CreateUserId, workFlowStepEntity.AuditUserId }, workFlowStepEntity.AuditDepartmentId, workFlowStepEntity.AuditRoleId);
                    }
                    this.StatusMessage = this.GetStateMessage(this.StatusCode);
                    this.DbHelper.CommitTransaction();
                }
                catch (Exception ex)
                {
                    this.DbHelper.RollbackTransaction();
                    BaseExceptionManager.LogException(dbHelper, this.UserInfo, ex);
                    this.StatusCode = Status.Error.ToString();
                    // throw;
                }
                finally
                {
                    this.DbHelper.Close();
                }
                return result;
            }
        }
        #endregion
    }
}