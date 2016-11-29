//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd.  
//-----------------------------------------------------------------

using System;

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
        /// (点批量通过时)当批量审核通过时
        /// </summary>
        /// <param name="currentId">审批流当前主键数组</param>
        /// <param name="auditIdea">批示</param>
        /// <returns>成功失败</returns>
        public int AutoAuditPass(string[] currentIds, string auditIdea)
        {
            int result = 0;
            for (int i = 0; i < currentIds.Length; i++)
            {
                result += this.AutoAuditPass(currentIds[i], auditIdea);
            }
            return result;
        }

        public int AutoAuditPass(string currentId, string auditIdea)
        {
            IWorkFlowManager workFlowManager = this.GetWorkFlowManager(currentId);
            return AutoAuditPass(workFlowManager, currentId, auditIdea);
        }

        /// <summary>
        /// (点通过时)当审核通过时
        /// </summary>
        /// <param name="currentId">审批流当前主键</param>
        /// <param name="auditIdea">批示</param>
        /// <returns>成功失败</returns>
        public int AutoAuditPass(IWorkFlowManager workFlowManager, string currentId, string auditIdea)
        {
            int result = 0;
            // 这里要加锁，防止并发提交
            // 这里用锁的机制，提高并发控制能力
            lock (WorkFlowCurrentLock)
            {
                // using (TransactionScope transactionScope = new TransactionScope())
                //{
                //try
                //{
                // 1. 先获得现在的状态？当前的工作流主键、当前的审核步骤主键？
                BaseWorkFlowCurrentEntity workFlowCurrentEntity = this.GetObject(currentId);

                // 只有待审核状态的，才可以通过,被退回的也可以重新提交
                if (!(workFlowCurrentEntity.AuditStatus.Equals(AuditStatus.StartAudit.ToString())
                    || workFlowCurrentEntity.AuditStatus.Equals(AuditStatus.AuditPass.ToString())
                    || workFlowCurrentEntity.AuditStatus.Equals(AuditStatus.WaitForAudit.ToString())
                    || workFlowCurrentEntity.AuditStatus.Equals(AuditStatus.AuditReject.ToString())
                    ))
                {
                    return result;
                }

                // 是不是给当前人审核的,或者当前人在委托的人？
                if (!string.IsNullOrEmpty(workFlowCurrentEntity.ToUserId))
                {
                    if (!(workFlowCurrentEntity.ToUserId.ToString().Equals(this.UserInfo.Id)
                        || workFlowCurrentEntity.ToUserId.IndexOf(this.UserInfo.Id) >= 0
                        // || workFlowCurrentEntity.ToUserId.ToString().Equals(this.UserInfo.TargetUserId)
                        ))
                    {
                        return result;
                    }
                }

                // 获取下一步是谁审核。
                BaseWorkFlowStepEntity workFlowStepEntity = this.GetNextWorkFlowStep(workFlowCurrentEntity);
                // 3. 进行下一步流转？转给角色？还是传给用户？
                if (workFlowStepEntity == null || workFlowStepEntity.Id == null)
                {
                    // 4. 若没下一步了，那就得结束流程了？审核结束了
                    result = this.AuditComplete(workFlowManager, currentId, auditIdea);
                }
                else
                {
                    // 审核进入下一步
                    // 当前是哪个步骤？
                    // 4. 是否已经在工作流里了？
                    // 5. 若已经在工作流里了，那就进行更新操作？
                    if (!string.IsNullOrEmpty(workFlowStepEntity.AuditUserId))
                    {
                        // 若是任意人可以审核的,需要进行一次人工选任的工作
                        if (workFlowStepEntity.AuditUserId.Equals("Anyone"))
                        {
                            return result;
                        }
                    }
                    // 按用户审核,审核通过
                    result = AuditPass(workFlowManager, currentId, auditIdea, workFlowStepEntity);
                }
                //}
                //catch (System.Exception ex)
                //{
                // 在本地记录异常
                //    FileUtil.WriteException(UserInfo, ex);
                //}
                //finally
                //{
                //}
                // transactionScope.Complete();
                //}
            }
            return result;
        }

        #region public int AuditPass(IWorkFlowManager workFlowManager, string currentId, string auditIdea, BaseWorkFlowStepEntity workFlowStepEntity) 审核通过
        /// <summary>
        /// 审核通过
        /// </summary>
        /// <param name="id">当前主键</param>
        /// <param name="auditIdea">批示</param>
        /// <returns>影响行数</returns>
        public int AuditPass(IWorkFlowManager workFlowManager, string currentId, string auditIdea, BaseWorkFlowStepEntity workFlowStepEntity)
        {
            int result = 0;
            // 进行更新操作
            result = this.StepAuditPass(currentId, auditIdea, workFlowStepEntity);
            if (result == 0)
            {
                // 数据可能被删除
                this.StatusCode = Status.ErrorDeleted.ToString();
            }
            BaseWorkFlowCurrentEntity workFlowCurrentEntity = this.GetObject(currentId);
            // 发送提醒信息
            if (workFlowManager != null)
            {
                if (!string.IsNullOrEmpty(workFlowStepEntity.AuditUserId))
                {
                    workFlowStepEntity.AuditDepartmentId = null;
                    workFlowStepEntity.AuditRoleId = null;
                }
                workFlowManager.OnAutoAuditPass(workFlowCurrentEntity);
                workFlowManager.SendRemindMessage(workFlowCurrentEntity, AuditStatus.AuditPass, new string[] { workFlowCurrentEntity.CreateUserId, workFlowStepEntity.AuditUserId }, workFlowStepEntity.AuditDepartmentId, workFlowStepEntity.AuditRoleId);
            }
            this.StatusMessage = this.GetStateMessage(this.StatusCode);
            return result;
        }
        #endregion

        #region private int StepAuditPass(string currentId, string auditIdea, BaseWorkFlowStepEntity toStepEntity, BaseWorkFlowAuditInfo workFlowAuditInfo = null) 审核通过(不需要再发给别人了是完成审批了)
        /// <summary>
        /// 审核通过(不需要再发给别人了是完成审批了)
        /// </summary>
        /// <param name="currentId">当前主键</param>
        /// <param name="auditIdea">批示</param>
        /// <param name="toStepEntity">审核到第几步</param>
        /// <param name="workFlowAuditInfo">当前审核人信息</param>
        /// <returns>影响行数</returns>
        private int StepAuditPass(string currentId, string auditIdea, BaseWorkFlowStepEntity toStepEntity, BaseWorkFlowAuditInfo workFlowAuditInfo = null)
        {
            BaseWorkFlowCurrentEntity workFlowCurrentEntity = this.GetObject(currentId);

            // 初始化审核信息，这里是显示当前审核人，可以不是当前操作员的功能
            if (workFlowAuditInfo == null)
            {
                workFlowAuditInfo = new BaseWorkFlowAuditInfo(this.UserInfo);
                workFlowAuditInfo.AuditIdea = auditIdea;
                workFlowAuditInfo.AuditDate = DateTime.Now;
                workFlowAuditInfo.AuditUserId = this.UserInfo.Id;
                workFlowAuditInfo.AuditUserRealName = this.UserInfo.RealName;
                workFlowAuditInfo.AuditStatus = AuditStatus.AuditPass.ToString();
                workFlowAuditInfo.AuditStatusName = AuditStatus.AuditPass.ToDescription();
            }
            else
            {
                workFlowAuditInfo.AuditIdea = auditIdea;
            }

            // 审核意见是什么？
            workFlowCurrentEntity.AuditIdea = workFlowAuditInfo.AuditIdea;
            // 1.记录当前的审核时间、审核人信息
            // 什么时间审核的？
            workFlowCurrentEntity.AuditDate = workFlowAuditInfo.AuditDate;
            // 审核的用户是谁？
            workFlowCurrentEntity.AuditUserId = workFlowAuditInfo.AuditUserId;
            // 审核人的姓名是谁？
            workFlowCurrentEntity.AuditUserRealName = workFlowAuditInfo.AuditUserRealName;
            // 审核状态是什么？
            workFlowCurrentEntity.AuditStatus = workFlowAuditInfo.AuditStatus;
            // 审核状态备注是什么？
            workFlowCurrentEntity.AuditStatusName = workFlowAuditInfo.AuditStatusName;
            if (!string.IsNullOrEmpty(workFlowCurrentEntity.ActivityFullName))
            {
                workFlowCurrentEntity.Description = string.Format("从{0}提交到{1}{2}", workFlowCurrentEntity.ActivityFullName, toStepEntity.FullName, !string.IsNullOrEmpty(toStepEntity.Description) ? "，" + toStepEntity.Description : string.Empty);
            }

            workFlowCurrentEntity.ToUserId = toStepEntity.AuditUserId;
            workFlowCurrentEntity.ToUserRealName = toStepEntity.AuditUserRealName;
            workFlowCurrentEntity.ToRoleId = toStepEntity.AuditRoleId;
            workFlowCurrentEntity.ToRoleRealName = toStepEntity.AuditRoleRealName;
            workFlowCurrentEntity.ToDepartmentId = toStepEntity.AuditDepartmentId;
            workFlowCurrentEntity.ToDepartmentName = toStepEntity.AuditDepartmentName;

            // 2.记录审核日志
            this.AddHistory(workFlowCurrentEntity);
            // 3.上一个审核结束了，新的审核又开始了，更新待审核情况
            workFlowCurrentEntity.ActivityId = toStepEntity.ActivityId;
            workFlowCurrentEntity.ActivityCode = toStepEntity.Code;
            workFlowCurrentEntity.ActivityFullName = toStepEntity.FullName;
            workFlowCurrentEntity.ActivityType = toStepEntity.ActivityType;
            workFlowCurrentEntity.SortCode = toStepEntity.SortCode;
            return this.UpdateObject(workFlowCurrentEntity);
        }
        #endregion
    }
}