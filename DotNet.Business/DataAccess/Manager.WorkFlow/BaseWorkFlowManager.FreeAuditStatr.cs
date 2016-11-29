//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd.  
//-----------------------------------------------------------------

using System;

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
    ///		2012.05.31 版本：1.0 JiRiGaLa	创建。
    /// 
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2012.05.31</date>
    /// </author>
    /// </summary>
    public partial class BaseWorkFlowCurrentManager : BaseManager, IBaseManager
    {
        public string FreeAuditStatr(BaseWorkFlowAuditInfo workFlowAuditInfo)
        {
            IWorkFlowManager workFlowManager = this.GetWorkFlowManager(workFlowAuditInfo);
            workFlowManager.SetUserInfo(this.UserInfo);
            return FreeAuditStatr(workFlowManager, workFlowAuditInfo);
        }

        #region public string FreeAuditStatr(IWorkFlowManager workFlowManager, BaseWorkFlowAuditInfo workFlowAuditInfo) 提交自由审批
        /// <summary>
        /// 提交自由审批
        /// </summary>
        /// <param name="workFlowManager">流程控制管理器</param>
        /// <param name="workFlowAuditInfo">流程信息</param>
        /// <returns>主键</returns>
        public string FreeAuditStatr(IWorkFlowManager workFlowManager, BaseWorkFlowAuditInfo workFlowAuditInfo)
        {
            // 当工作流开始启动，进入流程审批
            try
            {
                // 开始事务
                this.DbHelper.BeginTransaction();

                if (workFlowManager != null)
                {
                    workFlowManager.BeforeAutoStatr(workFlowAuditInfo);
                }

                workFlowAuditInfo.AuditStatus = AuditStatus.StartAudit.ToString();
                workFlowAuditInfo.AuditStatusName = AuditStatus.StartAudit.ToDescription();
                workFlowAuditInfo.Description = "申请登记";
                workFlowAuditInfo.Id = this.StepAuditStatr(workFlowAuditInfo);

                BaseWorkFlowCurrentEntity workFlowCurrentEntity = this.GetObject(workFlowAuditInfo.Id);

                // 获取当前处于什么状态
                // 如果还是开始审批状态的话，允许他再次提交把原来的覆盖掉
                // if (workFlowCurrentEntity.AuditStatus == AuditStatus.AuditReject.ToString())
                // {
                // 更新
                // this.UpdataAuditStatr(workFlowCurrentEntity.Id, categoryCode, categoryFullName, objectId, objectFullName, auditIdea, workFlowStepEntity);

                // 发送提醒信息，若发给指定的某个人了，就不发给部门的提示信息了
                if (workFlowManager != null)
                {
                    if (!string.IsNullOrEmpty(workFlowAuditInfo.ToUserId))
                    {
                        workFlowAuditInfo.ToDepartmentId = null;
                        workFlowAuditInfo.ToRoleId = null;
                    }
                    workFlowManager.SendRemindMessage(workFlowCurrentEntity, AuditStatus.StartAudit, new string[] { workFlowCurrentEntity.CreateUserId, workFlowAuditInfo.ToUserId }, workFlowAuditInfo.ToDepartmentId, workFlowAuditInfo.ToRoleId);
                }
                // 成功工作流后的处理
                if (workFlowManager != null && !string.IsNullOrEmpty(workFlowAuditInfo.ObjectId))
                {
                    workFlowManager.AfterAutoStatr(workFlowAuditInfo);
                }

                this.DbHelper.CommitTransaction();
                // 运行成功
                this.StatusCode = Status.OK.ToString();
                this.StatusMessage = this.GetStateMessage(this.StatusCode);
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

            return workFlowAuditInfo.Id;
        }
        #endregion
    }
}