//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd.  
//-----------------------------------------------------------------

using System;

namespace DotNet.Business
{
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
        /// 流程转发送给谁
        /// </summary>
        /// <param name="currentIds">当前工作流主键</param>
        /// <param name="auditIdea">审核意见</param>
        /// <param name="historyId">退回到第几步</param>
        /// <returns>影响行数</returns>
        public int AuditTransmit(string[] currentIds, string workFlowCategory, string sendToUserId, string auditIdea)
        {
            int result = 0;
            for (int i = 0; i < currentIds.Length; i++)
            {
                result += AuditTransmit(currentIds[i], workFlowCategory, sendToUserId, auditIdea);
            }
            return result;
        }

        #region public int AuditTransmit(string currentId, string workFlowCategory, string sendToUserId, string auditIdea) 获取信息
        /// <summary>
        /// 流程转发送给谁
        /// </summary>
        /// <param name="workFlowId">当前主键</param>
        /// <returns>影响行数</returns>
        public int AuditTransmit(string currentId, string workFlowCategory, string sendToUserId, string auditIdea)
        {
            int result = 0;
            // 进行更新操作
            result = this.StepAuditTransmit(currentId, workFlowCategory, sendToUserId, auditIdea);
            if (result == 0)
            {
                // 数据可能被删除
                this.StatusCode = Status.ErrorDeleted.ToString();
            }
            BaseWorkFlowCurrentEntity workFlowCurrentEntity = this.GetObject(currentId);
            // 发送提醒信息
            // this.SendRemindMessage(workFlowCurrentEntity.ObjectId, AuditStatus.Transmit, auditIdea, sendToUserId, null);
            this.StatusMessage = this.GetStateMessage(this.StatusCode);
            return result;
        }
        #endregion

        #region private int StepAuditTransmit(string currentId, string workFlowCategory, string sendToUserId, string auditIdea) 获取信息
        /// <summary>
        /// 流程转发送给谁
        /// </summary>
        /// <param name="id">当前主键</param>
        /// <returns>影响行数</returns>
        private int StepAuditTransmit(string currentId, string workFlowCategory, string sendToId, string auditIdea)
        {
            BaseWorkFlowCurrentEntity workFlowCurrentEntity = this.GetObject(currentId);

            // 1.记录当前的审核时间、审核人信息
            workFlowCurrentEntity.ToDepartmentId = this.UserInfo.DepartmentId;
            workFlowCurrentEntity.ToDepartmentName = this.UserInfo.DepartmentName;
            workFlowCurrentEntity.ToUserId = this.UserInfo.Id;
            workFlowCurrentEntity.ToUserRealName = this.UserInfo.RealName;
            workFlowCurrentEntity.AuditStatus = AuditStatus.Transmit.ToString();
            workFlowCurrentEntity.AuditStatusName = AuditStatus.Transmit.ToDescription();

            // 2.记录审核日志
            this.AddHistory(workFlowCurrentEntity);

            // 3.上一个审核结束了，新的审核又开始了，更新待审核情况
            workFlowCurrentEntity.AuditUserId = this.UserInfo.Id;
            workFlowCurrentEntity.AuditUserRealName = this.UserInfo.RealName;
            workFlowCurrentEntity.AuditDate = DateTime.Now;
            workFlowCurrentEntity.AuditIdea = auditIdea;

            // 是否提交给部门审批
            if (workFlowCategory.Equals("ByOrganize"))
            {
                BaseOrganizeManager organizeManager = new BaseOrganizeManager(UserInfo);
                BaseOrganizeEntity entity = organizeManager.GetObject(sendToId);
                // 设置审批部门主键
                workFlowCurrentEntity.ToDepartmentId = sendToId;
                // 设置审批部门名称
                workFlowCurrentEntity.ToDepartmentName = entity.FullName;
            }
            // 是否提交给角色审批
            if (workFlowCategory.Equals("ByRole"))
            {
                BaseRoleManager roleManger = new BaseRoleManager(this.UserInfo);
                BaseRoleEntity roleEntity = roleManger.GetObject(sendToId);
                // 设置审批角色主键
                workFlowCurrentEntity.ToRoleId = sendToId;
                // 设置审批角色名称
                workFlowCurrentEntity.ToRoleRealName = roleEntity.RealName;
            }
            // 是否提交给用户审批
            if (workFlowCategory.Equals("ByUser"))
            {
                BaseUserManager userManager = new BaseUserManager(UserInfo);
                BaseUserEntity userEntity = userManager.GetObject(sendToId);
                // 设置审批用户主键
                workFlowCurrentEntity.ToUserId = sendToId;
                // 设置审批用户名称
                workFlowCurrentEntity.ToUserRealName = userEntity.RealName;
                // TODO 用户的部门信息需要处理
                if (!string.IsNullOrEmpty(userEntity.DepartmentId))
                {
                    BaseOrganizeManager organizeManager = new BaseOrganizeManager(UserInfo);
                    BaseOrganizeEntity entity = organizeManager.GetObject(userEntity.DepartmentId);
                    workFlowCurrentEntity.ToDepartmentId = userEntity.DepartmentId;
                    workFlowCurrentEntity.ToDepartmentName = entity.FullName;
                }
            }
            workFlowCurrentEntity.AuditStatus = AuditStatus.WaitForAudit.ToString();
            workFlowCurrentEntity.AuditStatusName = AuditStatus.WaitForAudit.ToDescription();
            // 当前审核人的信息写入当前工作流
            workFlowCurrentEntity.Enabled = 0;
            workFlowCurrentEntity.DeletionStateCode = 0;
            return this.UpdateObject(workFlowCurrentEntity);
        }
        #endregion
    }
}