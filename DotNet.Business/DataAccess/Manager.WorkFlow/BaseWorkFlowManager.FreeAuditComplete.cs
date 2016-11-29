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
        public string FreeAuditComplete(BaseWorkFlowAuditInfo workFlowAuditInfo)
        {
            workFlowAuditInfo.Id = this.GetCurrentId(workFlowAuditInfo.CategoryCode, workFlowAuditInfo.ObjectId);
            IWorkFlowManager workFlowManager = this.GetWorkFlowManager(workFlowAuditInfo.Id);
            workFlowManager.SetUserInfo(this.UserInfo);
            return FreeAuditComplete(workFlowManager, workFlowAuditInfo);
        }

        #region public int FreeAuditComplete(IWorkFlowManager workFlowManager, BaseWorkFlowAuditInfo workFlowAuditInfo) 自由审批
        /// <summary>
        /// 自由审批完成
        /// </summary>
        /// <param name="workFlowManager">流程控制管理器</param>
        /// <param name="workFlowAuditInfo">流程信息</param>
        /// <returns>影响行数</returns>
        public string FreeAuditComplete(IWorkFlowManager workFlowManager, BaseWorkFlowAuditInfo workFlowAuditInfo)
        {
            // 返回值
            string result = string.Empty;
            // 这里用锁的机制，提高并发控制能力
            lock (WorkFlowCurrentLock)
            {
                try
                {
                    // 开始事务
                    this.DbHelper.BeginTransaction();
                    // 进行更新操作
                    this.StepAuditComplete(workFlowAuditInfo.Id, workFlowAuditInfo.AuditIdea);
                    BaseWorkFlowCurrentEntity workFlowCurrentEntity = this.GetObject(workFlowAuditInfo.Id);
                    // 发送提醒信息
                    if (workFlowManager != null)
                    {
                        workFlowManager.OnAutoAuditComplete(workFlowCurrentEntity);
                        workFlowManager.SendRemindMessage(workFlowCurrentEntity, AuditStatus.AuditComplete, new string[] { workFlowCurrentEntity.CreateUserId, workFlowAuditInfo.ToUserId }, workFlowAuditInfo.ToDepartmentId, workFlowAuditInfo.ToRoleId);
                    }
                    this.StatusMessage = this.GetStateMessage(this.StatusCode);
                    result = workFlowAuditInfo.Id;
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