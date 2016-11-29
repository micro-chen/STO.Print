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
    using Jurassic;

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
        /// 计算表达式
        /// 20130513 吉日嘎拉，有了这个方法程序牛X多了，很多需求都可以满足了
        /// </summary>
        /// <param name="table">哪个表</param>
        /// <param name="id">什么主键</param>
        /// <param name="expression">什么表达式</param>
        /// <returns>成立</returns>
        public bool Evaluate(string table, string id, string expression)
        {
            string commandText = string.Format("SELECT * FROM {0} WHERE id = {1}", table, id);
            var dt = this.Fill(commandText);
            if (dt.Rows.Count == 1)
            {
                // expression = expression.ToUpper();
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    string columnName = dt.Columns[i].ColumnName;
                    string columnValue = dt.Rows[0][dt.Columns[i].ColumnName].ToString();
                    if (dt.Columns[i].DataType == typeof(string))
                    {
                        expression = expression.Replace(columnName, "'" + columnValue + "'");
                    }
                    else
                    {
                        expression = expression.Replace(columnName, columnValue);
                    }

                    // expression = expression.Replace(result.Columns[i].ColumnName.ToUpper(), result.Rows[0][result.Columns[i].ColumnName].ToString());
                }
                var engine = new ScriptEngine();
                return engine.Evaluate<bool>(expression);
            }
            // 这里先返回 false, 按这里这里返回异常才对。
            return false;
        }


        //-----------------------------------------------------
        //                  启动工作流 步骤流
        //-----------------------------------------------------

        /// <summary>
        /// 启动工作流（步骤流转）
        /// </summary>
        /// <param name="workFlowManager">审批流程管理器</param>
        /// <param name="objectId">单据主键</param>
        /// <param name="objectFullName">单据名称</param>
        /// <param name="categoryCode">单据分类</param>
        /// <param name="categoryFullName">单据分类名称</param>
        /// <param name="workFlowCode">工作流程</param>
        /// <param name="auditIdea">审批意见</param>
        /// <param name="dtWorkFlowActivity">需要走的流程</param>
        /// <returns>主键</returns>
        public string AutoStatr(IWorkFlowManager workFlowManager, string objectId, string objectFullName, string categoryCode, string categoryFullName = null, string workFlowCode = null, string auditIdea = null, DataTable dtWorkFlowActivity = null)
        {
            if (workFlowManager == null && !string.IsNullOrEmpty(categoryCode))
            {
                if (string.IsNullOrEmpty(workFlowCode))
                {
                    workFlowCode = categoryCode;
                }
                workFlowManager = new BaseWorkFlowProcessManager(this.DbHelper, this.UserInfo).GetWorkFlowManagerByCode(workFlowCode);
                // workFlowManager = new BaseUserBillManager(this.DbHelper, this.UserInfo, categoryCode);
            }

            BaseWorkFlowAuditInfo workFlowAuditInfo = new BaseWorkFlowAuditInfo();
            workFlowAuditInfo.CategoryCode = categoryCode;
            workFlowAuditInfo.ObjectId = objectId;
            workFlowAuditInfo.CallBackTable = workFlowManager.CurrentTableName;
            workFlowAuditInfo.ProcessCode = workFlowCode;

            string currentId = string.Empty;
            // 看审批流程是否被定义
            if (dtWorkFlowActivity == null || dtWorkFlowActivity.Rows.Count == 0)
            {
                BaseWorkFlowProcessManager workFlowProcessManager = new BaseWorkFlowProcessManager(this.DbHelper, this.UserInfo);
                var dt = workFlowProcessManager.GetDataTable(
                    new KeyValuePair<string, object>(BaseWorkFlowProcessEntity.FieldCode, workFlowCode)
                    , new KeyValuePair<string, object>(BaseWorkFlowProcessEntity.FieldDeletionStateCode, 0), BaseWorkFlowProcessEntity.FieldSortCode);
                BaseWorkFlowProcessEntity workFlowProcessEntity = null;
                foreach (DataRow dr in dt.Rows)
                {
                    workFlowProcessEntity = BaseEntity.Create<BaseWorkFlowProcessEntity>(dr);
                    // 这里是进入条件，结束条件进行筛选
                    // 进入条件是否满足
                    if (!string.IsNullOrEmpty(workFlowProcessEntity.EnterConstraint))
                    {
                        if (!Evaluate(workFlowAuditInfo.CallBackTable, objectId, workFlowProcessEntity.EnterConstraint))
                        {
                            // 没有满足入口条件
                            dr.Delete();
                        }
                    }
                }
                dt.AcceptChanges();
                if (dt.Rows.Count == 0)
                {
                    return currentId;
                }
				workFlowProcessEntity = BaseEntity.Create<BaseWorkFlowProcessEntity>(dt);
                // 这里是获取用户的走的流程
                dtWorkFlowActivity = new BaseWorkFlowActivityManager(this.UserInfo).GetActivityDTById(workFlowProcessEntity.Id.ToString());
                // dtWorkFlowActivity = new BaseWorkFlowActivityManager(this.UserInfo).GetActivityDTByCode(workFlowCode);
                if (dtWorkFlowActivity.Rows.Count == 0)
                {
                    return currentId;
                }
            }
            lock (WorkFlowCurrentLock)
            {
                BaseWorkFlowStepEntity workFlowStepEntity = null;
                // 这里需要读取一下
                if (dtWorkFlowActivity == null)
                {

                }
				workFlowStepEntity = BaseEntity.Create<BaseWorkFlowStepEntity>(dtWorkFlowActivity.Rows[0]);
                if (!string.IsNullOrEmpty(workFlowStepEntity.AuditUserId))
                {
                    // 若是任意人可以审核的,需要进行一次人工选任的工作
                    if (workFlowStepEntity.AuditUserId.Equals("Anyone"))
                    {
                        return null;
                    }
                }
                // 1. 先把已有的流程设置功能都删除掉
                BaseWorkFlowStepManager workFlowStepManager = new BaseWorkFlowStepManager(this.DbHelper, this.UserInfo);
                workFlowStepManager.Delete(new KeyValuePair<string, object>(BaseWorkFlowStepEntity.FieldCategoryCode, categoryCode)
                    , new KeyValuePair<string, object>(BaseWorkFlowStepEntity.FieldObjectId, objectId));
                // 2. 把当前的流程设置保存好
                BaseWorkFlowActivityEntity activityEntity = null;
                // 是否已经满足了条件了
                bool endConstraint = false;
                foreach (DataRow dr in dtWorkFlowActivity.Rows)
                {
                    // 是否已经结束流程了
                    if (endConstraint)
                    {
                        dr.Delete();
                    }
                    else
                    {
						activityEntity = BaseEntity.Create<BaseWorkFlowActivityEntity>(dr);
                        // 这里是进入条件，结束条件进行筛选
                        // 进入条件是否满足
                        if (!string.IsNullOrEmpty(activityEntity.EnterConstraint))
                        {
                            if (!Evaluate(workFlowAuditInfo.CallBackTable, objectId, activityEntity.EnterConstraint))
                            {
                                // 没有满足入口条件
                                dr.Delete();
                            }
                        }
                        // 结束条件是否满足
                        if (!string.IsNullOrEmpty(activityEntity.EndConstraint))
                        {
                            if (Evaluate(workFlowAuditInfo.CallBackTable, objectId, activityEntity.EndConstraint))
                            {
                                // 已经满足了结束条件了
                                dr.Delete();
                                endConstraint = true;
                            }
                        }
                    }
                }
                dtWorkFlowActivity.AcceptChanges();
                // 没有任何审核流程步骤了
                if (dtWorkFlowActivity.Rows.Count == 0)
                {
                    return null;
                }

                // 建立审核步骤表，需要走哪些审核步骤的具体步骤表
                foreach (DataRow dr in dtWorkFlowActivity.Rows)
                {
					workFlowStepEntity = BaseEntity.Create<BaseWorkFlowStepEntity>(dr);
                    // workFlowStepEntity.ActivityId = workFlowActivityEntity.Id;
                    // workFlowStepEntity.ActivityType = workFlowActivityEntity.ActivityType;
                    workFlowStepEntity.CategoryCode = categoryCode;
                    workFlowStepEntity.ObjectId = objectId;
                    workFlowStepEntity.Id = null;
                    workFlowStepManager.Add(workFlowStepEntity);
                }
				workFlowStepEntity = BaseEntity.Create<BaseWorkFlowStepEntity>(dtWorkFlowActivity.Rows[0]);

                // 3. 启动审核流程
                currentId = this.GetCurrentId(categoryCode, objectId);
                BaseWorkFlowCurrentEntity workFlowCurrentEntity = null;
                if (currentId.Length > 0)
                {
                    // 获取当前处于什么状态
                    string auditstatus = this.GetProperty(currentId, BaseWorkFlowCurrentEntity.FieldAuditStatus);
                    // 如果还是开始审批状态的话，允许他再次提交把原来的覆盖掉
                    if (auditstatus == AuditStatus.StartAudit.ToString()
                        || auditstatus == AuditStatus.AuditReject.ToString())
                    {
                        this.UpdataAuditStatr(currentId, categoryCode, categoryFullName, objectId, objectFullName, auditIdea, workFlowStepEntity);
                        if (workFlowManager != null)
                        {
                            workFlowManager.AfterAutoStatr(workFlowAuditInfo);
                        }
                    }
                    // 不是的话则返回
                    else
                    {
                        // 该单据已进入审核状态不能在次提交
                        this.StatusCode = Status.ErrorChanged.ToString();
                        // 返回为空可以判断
                        currentId = null;
                    }
                }
                else
                {
                    if (workFlowManager != null)
                    {
                        workFlowManager.BeforeAutoStatr(workFlowAuditInfo);
                    }
                    currentId = this.StepAuditStatr(categoryCode, categoryFullName, objectId, objectFullName, auditIdea, workFlowStepEntity);
                    workFlowCurrentEntity = this.GetObject(currentId);
                    // 发送提醒信息，若发给指定的某个人了，就不发给部门的提示信息了
                    if (workFlowManager != null)
                    {
                        if (!string.IsNullOrEmpty(workFlowStepEntity.AuditUserId))
                        {
                            workFlowStepEntity.AuditDepartmentId = null;
                            workFlowStepEntity.AuditRoleId = null;
                        }
                        workFlowManager.SendRemindMessage(workFlowCurrentEntity, AuditStatus.StartAudit, new string[] { workFlowCurrentEntity.CreateUserId, workFlowStepEntity.AuditUserId }, workFlowStepEntity.AuditDepartmentId, workFlowStepEntity.AuditRoleId);
                    }
                    // 成功工作流后的处理
                    if (!string.IsNullOrEmpty(objectId) && workFlowManager != null)
                    {
                        workFlowManager.AfterAutoStatr(workFlowAuditInfo);
                    }
                    // 运行成功
                    this.StatusCode = Status.OK.ToString();
                    this.StatusMessage = this.GetStateMessage(this.StatusCode);
                }
            }
            return currentId;
        }

        private int UpdataAuditStatr(string id, string categoryCode, string categoryFullName, string objectId, string objectFullName, string auditIdea, BaseWorkFlowStepEntity workFlowStepEntity)
        {
            BaseWorkFlowCurrentEntity workFlowCurrentEntity = this.GetObject(id);
            workFlowCurrentEntity.CategoryCode = categoryCode;
            workFlowCurrentEntity.CategoryFullName = categoryFullName;
            workFlowCurrentEntity.ObjectId = objectId;
            workFlowCurrentEntity.ObjectFullName = objectFullName;
            workFlowCurrentEntity.ProcessId = workFlowStepEntity.ProcessId;
            workFlowCurrentEntity.ActivityId = workFlowStepEntity.Id;
            workFlowCurrentEntity.ActivityType = workFlowStepEntity.ActivityType;
            workFlowCurrentEntity.SendDate = DateTime.Now;
            workFlowCurrentEntity.AuditDate = DateTime.Now;
            workFlowCurrentEntity.AuditStatus = AuditStatus.StartAudit.ToString();
            workFlowCurrentEntity.AuditStatusName = AuditStatus.StartAudit.ToDescription();

            // 是否提交给组织机构审批
            workFlowCurrentEntity.ToDepartmentId = workFlowStepEntity.AuditDepartmentId;
            workFlowCurrentEntity.ToDepartmentName = workFlowStepEntity.AuditDepartmentName;
            // 是否提交给角色审批
            workFlowCurrentEntity.ToRoleId = workFlowStepEntity.AuditRoleId;
            workFlowCurrentEntity.ToRoleRealName = workFlowStepEntity.AuditRoleRealName;
            // 是否提交给用户审批
            workFlowCurrentEntity.ToUserId = workFlowStepEntity.AuditUserId;
            workFlowCurrentEntity.ToUserRealName = workFlowStepEntity.AuditUserRealName;

            if (!string.IsNullOrEmpty(workFlowStepEntity.AuditUserId))
            {
                BaseUserManager userManager = new BaseUserManager(UserInfo);
                BaseUserEntity userEntity = userManager.GetObject(workFlowStepEntity.AuditUserId);
                workFlowCurrentEntity.ToUserRealName = userEntity.RealName;
                // 用户的部门信息需要处理
                if (!string.IsNullOrEmpty(userEntity.DepartmentId))
                {
                    BaseOrganizeManager organizeManager = new BaseOrganizeManager(UserInfo);
                    BaseOrganizeEntity entity = organizeManager.GetObject(userEntity.DepartmentId);
                    workFlowCurrentEntity.ToDepartmentName = entity.FullName;
                }
            }
            // 当前审核人的信息写入当前工作流
            workFlowCurrentEntity.AuditUserId = string.Empty;
            workFlowCurrentEntity.AuditUserCode = string.Empty;
            workFlowCurrentEntity.AuditUserRealName = string.Empty;
            workFlowCurrentEntity.AuditIdea = auditIdea;
            workFlowCurrentEntity.AuditDate = DateTime.Now;

            int result = this.UpdateObject(workFlowCurrentEntity);

            this.AddHistory(workFlowCurrentEntity);
            return result;
        }

        private string StepAuditStatr(string categoryCode, string categoryFullName, string objectId, string objectFullName, string auditIdea, BaseWorkFlowStepEntity workFlowStepEntity)
        {
            BaseWorkFlowAuditInfo workFlowAuditInfo = new BaseWorkFlowAuditInfo();
            workFlowAuditInfo.CategoryCode = categoryCode;
            workFlowAuditInfo.CategoryFullName = categoryFullName;
            workFlowAuditInfo.ObjectId = objectId;
            workFlowAuditInfo.ObjectFullName = objectFullName;
            workFlowAuditInfo.AuditIdea = auditIdea;
            workFlowAuditInfo.ProcessId = workFlowStepEntity.ProcessId;
            workFlowAuditInfo.ActivityId = workFlowStepEntity.ActivityId.ToString();
            workFlowAuditInfo.ActivityCode = workFlowStepEntity.ActivityCode;
            workFlowAuditInfo.ActivityType = workFlowStepEntity.ActivityType;
            workFlowAuditInfo.ActivityFullName = workFlowStepEntity.FullName;
            workFlowAuditInfo.ToUserId = workFlowStepEntity.AuditUserId;
            workFlowAuditInfo.ToUserRealName = workFlowStepEntity.AuditUserRealName;
            workFlowAuditInfo.ToDepartmentId = workFlowStepEntity.AuditDepartmentId;
            workFlowAuditInfo.ToDepartmentName = workFlowStepEntity.AuditDepartmentName;
            workFlowAuditInfo.ToRoleId = workFlowStepEntity.AuditRoleId;
            workFlowAuditInfo.ToRoleRealName = workFlowStepEntity.AuditRoleRealName;
            workFlowAuditInfo.AuditStatus = AuditStatus.WaitForAudit.ToString();
            workFlowAuditInfo.AuditStatusName = AuditStatus.WaitForAudit.ToDescription();
            return StepAuditStatr(workFlowAuditInfo);
        }

        private string StepAuditStatr(BaseWorkFlowAuditInfo workFlowAuditInfo)
        {
            string result = string.Empty;

            BaseWorkFlowCurrentEntity workFlowCurrentEntity = new BaseWorkFlowCurrentEntity();
            // 1.这个是工作流的第一个发出日期，需要全局保留，其实也是创建日期了，但是有重新审核的这个说法，若有2次重新审核的事项，的确需要保留这个字段
            // workFlowCurrentEntity.CallBack = workFlowManager.GetType().ToString();
            workFlowCurrentEntity.ProcessId = workFlowAuditInfo.ProcessId;
            workFlowCurrentEntity.ActivityId = int.Parse(workFlowAuditInfo.ActivityId);
            workFlowCurrentEntity.ActivityCode = workFlowAuditInfo.ActivityCode;
            workFlowCurrentEntity.ActivityFullName = workFlowAuditInfo.ActivityFullName;
            workFlowCurrentEntity.ActivityType = workFlowAuditInfo.ActivityType;
            // 这里是为了优化越级审核用的，按排序嘛进行先后顺序排序
            workFlowCurrentEntity.CategoryCode = workFlowAuditInfo.CategoryCode;
            workFlowCurrentEntity.CategoryFullName = workFlowAuditInfo.CategoryFullName;
            workFlowCurrentEntity.ObjectId = workFlowAuditInfo.ObjectId;
            workFlowCurrentEntity.ObjectFullName = workFlowAuditInfo.ObjectFullName;
            // 2.当前审核人的信息写入当前工作流
            // workFlowCurrentEntity.AuditUserId = this.UserInfo.Id;
            // workFlowCurrentEntity.AuditUserCode = this.UserInfo.Code;
            // workFlowCurrentEntity.AuditUserRealName = this.UserInfo.RealName;
            workFlowCurrentEntity.AuditIdea = workFlowAuditInfo.AuditIdea;
            workFlowCurrentEntity.AuditStatus = workFlowAuditInfo.AuditStatus;
            workFlowCurrentEntity.AuditStatusName = workFlowAuditInfo.AuditStatusName;
            workFlowCurrentEntity.SendDate = DateTime.Now;
            // 3.接下来需要待审核的对象的信息
            workFlowCurrentEntity.ToUserId = workFlowAuditInfo.ToUserId;
            workFlowCurrentEntity.ToUserRealName = workFlowAuditInfo.ToUserRealName;
            workFlowCurrentEntity.ToDepartmentId = workFlowAuditInfo.ToDepartmentId;
            workFlowCurrentEntity.ToDepartmentName = workFlowAuditInfo.ToDepartmentName;
            workFlowCurrentEntity.ToRoleId = workFlowAuditInfo.ToRoleId;
            workFlowCurrentEntity.ToRoleRealName = workFlowAuditInfo.ToRoleRealName;
            workFlowCurrentEntity.Description = workFlowAuditInfo.Description;
            // 4.这些标志信息需要处理好，这里表示工作流还没完成生效，还在审批中的意思。
            workFlowCurrentEntity.SortCode = int.Parse(new BaseSequenceManager(this.DbHelper).Increment("WorkFlow", 10000000));
            workFlowCurrentEntity.Enabled = 0;
            workFlowCurrentEntity.DeletionStateCode = 0;

            // 添加当前待审核流程表
            result = this.AddObject(workFlowCurrentEntity);

            // 4.记录审核日志
            workFlowCurrentEntity.Id = result;
            this.AddHistory(workFlowCurrentEntity);

            return result;
        }
    }
}