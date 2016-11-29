//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd.  
//-----------------------------------------------------------------

using System;
using System.Linq;

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
        public int FreeAuditPass(BaseWorkFlowAuditInfo workFlowAuditInfo)
        {
            IWorkFlowManager workFlowManager = this.GetWorkFlowManager(workFlowAuditInfo.Id);
            workFlowManager.SetUserInfo(this.UserInfo);
            return FreeAuditPass(workFlowManager, workFlowAuditInfo);
        }

        #region public int FreeAuditPass(IWorkFlowManager workFlowManager, BaseWorkFlowAuditInfo workFlowAuditInfo) 自由审批
        /// <summary>
        /// 自由审批通过
        /// </summary>
        /// <param name="workFlowManager">流程控制管理器</param>
        /// <param name="workFlowAuditInfo">流程信息</param>
        /// <returns>影响行数</returns>
        public int FreeAuditPass(IWorkFlowManager workFlowManager, BaseWorkFlowAuditInfo workFlowAuditInfo)
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

                    // 这里需要循环产生前面的没有审批的人的记录，默认都要让他们审批通过才可以？
                    // 在这里处理连续跳过好几个审批的，自动能生成审批记录的功能？
                    BaseWorkFlowCurrentEntity workFlowCurrentEntity = null;
                    workFlowCurrentEntity = this.GetObject(workFlowAuditInfo.Id);
                    // 当前是什么步骤主键？
                    int? startSortCode = BaseWorkFlowActivityManager.GetEntity(workFlowCurrentEntity.ActivityId).SortCode;
                    // 现在要到什么步骤主键？
                    int? endSortCode = BaseWorkFlowActivityManager.GetEntity(workFlowAuditInfo.ActivityId).SortCode;
                    // 中间还有几个步骤被跳过了？
                    var listEntity = BaseWorkFlowActivityManager.GetEntities().Where(entity => entity.SortCode > startSortCode && entity.SortCode <= endSortCode && entity.Enabled == 1 && entity.DeletionStateCode == 0).OrderBy(entity => entity.SortCode);
                    int i = 0;
                    foreach (var entity in listEntity)
                    {
                        // 这里是需要自动模拟产生的审批用的
                        BaseWorkFlowAuditInfo workFlowInfo = new BaseWorkFlowAuditInfo(); 
                        // 是最后一步了
                        if (entity.SortCode == endSortCode)
                        {
                            workFlowInfo = workFlowAuditInfo;
                            workFlowInfo.AuditDate = DateTime.Now.AddMinutes(i * 10);
                        }
                        else
                        {
                            workFlowInfo.Id = workFlowAuditInfo.Id;
                            workFlowInfo.ActivityId = entity.Id.ToString();
                            workFlowInfo.ActivityCode = entity.Code;
                            workFlowInfo.ActivityFullName = entity.FullName;
                            // 这个是默认写入的审核意见
                            workFlowInfo.AuditIdea = "通过";
                            workFlowInfo.AuditUserId = entity.DefaultAuditUserId;
                            workFlowInfo.AuditUserCode = BaseUserManager.GetUserCodeByCache(entity.DefaultAuditUserId);
                            workFlowInfo.AuditUserRealName = BaseUserManager.GetRealNameByCache(entity.DefaultAuditUserId);
                            i++;
                            // 这里是生成不一样的审核人日期的方法
                            workFlowInfo.AuditDate = DateTime.Now.AddMinutes(i * 10);
                            workFlowInfo.AuditStatus = AuditStatus.AuditPass.ToString();
                            workFlowInfo.AuditStatusName = AuditStatus.AuditPass.ToDescription();
                            // workFlowInfo.ToUserId = entity.DefaultAuditUserId;
                            // workFlowInfo.ToRoleId = string.Empty;
                            // workFlowInfo.ToDepartmentId = string.Empty;
                        }

                        BaseWorkFlowStepEntity workFlowStepEntity = new BaseWorkFlowStepEntity();
                        if (!string.IsNullOrEmpty(workFlowInfo.ActivityId))
                        {
                            workFlowStepEntity.ActivityId = int.Parse(workFlowInfo.ActivityId);
                            workFlowStepEntity.ActivityCode = workFlowInfo.ActivityCode;
                            workFlowStepEntity.ActivityFullName = workFlowInfo.ActivityFullName;
                        }
                        // 是否提交给用户审批
                        if (!string.IsNullOrEmpty(workFlowInfo.ToUserId))
                        {
                            workFlowStepEntity.AuditUserId = workFlowInfo.ToUserId;
                            workFlowStepEntity.AuditUserCode = BaseUserManager.GetUserCodeByCache(workFlowInfo.ToUserId);
                            workFlowStepEntity.AuditUserRealName = BaseUserManager.GetRealNameByCache(workFlowInfo.ToUserId);
                        }
                        // 是否提交给部门审批
                        if (!string.IsNullOrEmpty(workFlowInfo.ToDepartmentId))
                        {
                            workFlowStepEntity.AuditDepartmentId = workFlowInfo.ToDepartmentId;
                            workFlowStepEntity.AuditDepartmentName = BaseOrganizeManager.GetNameByCache(workFlowInfo.ToDepartmentId);
                        }
                        // 是否提交给角色审批
                        if (!string.IsNullOrEmpty(workFlowInfo.ToRoleId))
                        {
                            workFlowStepEntity.AuditRoleId = workFlowInfo.ToRoleId;
                            workFlowStepEntity.AuditRoleRealName = BaseRoleManager.GetRealNameByCache(this.UserInfo.SystemCode, workFlowInfo.ToRoleId);
                        }
                        // 获取排序码
                        workFlowStepEntity.SortCode = int.Parse(new BaseSequenceManager().Increment("WorkFlow", 10000000));
                        // 进行更新操作，这里有时候需要模拟别人登录
                        result = this.StepAuditPass(workFlowInfo.Id, workFlowInfo.AuditIdea, workFlowStepEntity, workFlowInfo);
                        if (result == 0)
                        {
                            // 数据可能被删除
                            this.StatusCode = Status.ErrorDeleted.ToString();
                        }
                        workFlowCurrentEntity = this.GetObject(workFlowInfo.Id);
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