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
        //-----------------------------------------------------
        //                  启动工作流 自由流
        //-----------------------------------------------------

        public string FreeAudit(BaseWorkFlowAuditInfo workFlowAuditInfo)
        {
            IWorkFlowManager workFlowManager = null;
            return FreeAudit(workFlowManager, workFlowAuditInfo);
        }

        /// <summary>
        /// 启动工作流(自由流转)
        /// </summary>
        /// <param name="workFlowManager"></param>
        /// <param name="activityId">丢到第几部审核哪里去了</param>
        /// <param name="objectId"></param>
        /// <param name="objectFullName"></param>
        /// <param name="categoryCode"></param>
        /// <param name="categoryFullName"></param>
        /// <param name="toUserId"></param>
        /// <param name="toDepartmentId"></param>
        /// <param name="toRoleId"></param>
        /// <param name="auditIdea"></param>
        /// <returns></returns>
        public string FreeAudit(IWorkFlowManager workFlowManager, BaseWorkFlowAuditInfo workFlowAuditInfo)
        {
            // 是否提交给用户审批
            if (!string.IsNullOrEmpty(workFlowAuditInfo.ToUserId))
            {
                workFlowAuditInfo.ToUserRealName = BaseUserManager.GetRealNameByCache(workFlowAuditInfo.ToUserId);
            }
            // 是否提交给部门审批
            if (!string.IsNullOrEmpty(workFlowAuditInfo.ToDepartmentId))
            {
                workFlowAuditInfo.ToDepartmentName = BaseOrganizeManager.GetNameByCache(workFlowAuditInfo.ToDepartmentId);
            }
            // 是否提交给角色审批
            if (!string.IsNullOrEmpty(workFlowAuditInfo.ToRoleId))
            {
                workFlowAuditInfo.ToRoleRealName = BaseRoleManager.GetRealNameByCache(this.UserInfo.SystemCode, workFlowAuditInfo.ToRoleId);
            }
            // 计算第几步审核
            if (!string.IsNullOrEmpty(workFlowAuditInfo.ActivityId))
            {
                BaseWorkFlowActivityEntity workFlowActivityEntity = new BaseWorkFlowActivityManager().GetObject(workFlowAuditInfo.ActivityId);
                workFlowAuditInfo.ProcessId = workFlowActivityEntity.ProcessId;
                workFlowAuditInfo.ActivityCode = workFlowActivityEntity.Code;
                workFlowAuditInfo.ActivityType = workFlowActivityEntity.ActivityType;
                workFlowAuditInfo.ActivityFullName = workFlowActivityEntity.FullName;
                BaseWorkFlowProcessEntity workFlowProcessEntity = new BaseWorkFlowProcessManager().GetObject(workFlowActivityEntity.ProcessId.ToString());
                workFlowAuditInfo.ProcessCode = workFlowProcessEntity.Code;
                workFlowAuditInfo.CallBackClass = workFlowProcessEntity.CallBackClass;
                workFlowAuditInfo.CallBackTable = workFlowProcessEntity.CallBackTable;
                // 若没有分类信息，分类信息可以按流程的信息保存
                if (string.IsNullOrEmpty(workFlowAuditInfo.CategoryCode))
                {
                    workFlowAuditInfo.CategoryCode = workFlowProcessEntity.Code;
                    workFlowAuditInfo.CategoryFullName = workFlowProcessEntity.FullName;
                }

                // 不需要反复从数据库读取的方法
                workFlowManager = GetWorkFlowManager(workFlowAuditInfo);
            }

            // 获取当前工作流步骤
            BaseWorkFlowCurrentEntity workFlowCurrentEntity = this.GetObjectBy(workFlowAuditInfo.CategoryCode, workFlowAuditInfo.ObjectId);
            // 已经在审批里了，那不需要重新审批，直接进入另外一个审批流程
            if (workFlowCurrentEntity != null && !string.IsNullOrEmpty(workFlowCurrentEntity.Id))
            {
                workFlowAuditInfo.Id = workFlowCurrentEntity.Id;

                // 判断现在是在什么流程节点上？
                int? sortCode = BaseWorkFlowActivityManager.GetEntity(workFlowCurrentEntity.ActivityId.ToString()).SortCode;

                // 与现在发送的流程节点比，是前进了，还是倒退了？
                // 这里是判断是退回还是前进了？排序码小的，就是在前面的步骤里，是退回了，否则是继续通过前进了
                if (BaseWorkFlowActivityManager.GetEntity(workFlowAuditInfo.ActivityId).SortCode <= sortCode)
                {
                    // 自由审批退回
                    this.FreeAuditReject(workFlowManager, workFlowAuditInfo);
                }
                else
                {
                    // 自由审批通过
                    this.FreeAuditPass(workFlowManager, workFlowAuditInfo);
                }
                return workFlowCurrentEntity.Id;
            }

            // 提交自由审批
            this.FreeAuditStatr(workFlowAuditInfo);

            return workFlowAuditInfo.Id;
        }
    }
}