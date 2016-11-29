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
    ///		2012.04.03 版本：4.0 JiRiGaLa	整理优化审批流程组件。
    ///		2010.10.11 版本：3.1 JiRiGaLa	发送提醒信息功能完善。
    ///		2010.10.11 版本：3.0 JiRiGaLa	流转历史、自动流转进行改进、审核流程进行彻底的测试完善。
    ///		2008.03.17 版本：2.0 JiRiGaLa	流转的单子到底到哪里了信息显示不够 进行改进。
    ///		2007.07.18 版本：1.0 JiRiGaLa	编写主键。
    /// 
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2012.04.03</date>
    /// </author>
    /// </summary>
    public partial class BaseWorkFlowCurrentManager : BaseManager, IBaseManager
    {
        public string GetCurrentId(string categoryCode, string objectId)
        {
            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            if (!string.IsNullOrEmpty(categoryCode))
            {
                parameters.Add(new KeyValuePair<string, object>(BaseWorkFlowCurrentEntity.FieldCategoryCode, categoryCode));
            }
            parameters.Add(new KeyValuePair<string, object>(BaseWorkFlowCurrentEntity.FieldObjectId, objectId));
            parameters.Add(new KeyValuePair<string, object>(BaseWorkFlowCurrentEntity.FieldDeletionStateCode, 0));
            return this.GetId(parameters);
        }

        public BaseWorkFlowCurrentEntity GetObjectBy(string categoryCode, string objectId)
        {
            BaseWorkFlowCurrentEntity workFlowCurrentEntity = null;
            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            if (!string.IsNullOrEmpty(categoryCode))
            {
                parameters.Add(new KeyValuePair<string, object>(BaseWorkFlowCurrentEntity.FieldCategoryCode, categoryCode));
            }
            parameters.Add(new KeyValuePair<string, object>(BaseWorkFlowCurrentEntity.FieldObjectId, objectId));
            parameters.Add(new KeyValuePair<string, object>(BaseWorkFlowCurrentEntity.FieldDeletionStateCode, 0));
            workFlowCurrentEntity = BaseEntity.Create<BaseWorkFlowCurrentEntity>(this.GetDataTable(parameters));
            return workFlowCurrentEntity;
        }

        public IWorkFlowManager GetWorkFlowManager(BaseWorkFlowAuditInfo workFlowAuditInfo)
        {
            IWorkFlowManager workFlowManager = null;
            // Type objType = Type.GetType(workFlowAuditInfo.CallBackClass, true);
            // workFlowManager = (IWorkFlowManager)Activator.CreateInstance(objType);
            string assemblyString = workFlowAuditInfo.CallBackClass.Substring(0, workFlowAuditInfo.CallBackClass.LastIndexOf('.'));
            workFlowManager = (IWorkFlowManager)Assembly.Load(assemblyString).CreateInstance(workFlowAuditInfo.CallBackClass);
            workFlowManager.SetUserInfo(this.UserInfo);
            if (!string.IsNullOrEmpty(workFlowAuditInfo.CallBackTable))
            {
                // 这里本来是想动态创建类库 编码外包[100]
                workFlowManager.CurrentTableName = workFlowAuditInfo.CallBackTable;
            }
            return workFlowManager;
        }


        /// <summary>
        /// 获取反射调用的类
        /// 回写状态时用
        /// </summary>
        /// <param name="currentId">当前工作流主键</param>
        /// <returns></returns>
        public IWorkFlowManager GetWorkFlowManager(string currentId)
        {
            IWorkFlowManager workFlowManager = null;
            BaseWorkFlowCurrentEntity workFlowCurrentEntity = this.GetObject(currentId);
            string processId = this.GetObject(currentId).ProcessId.ToString();
            if (!string.IsNullOrEmpty(processId))
            {
                BaseWorkFlowProcessManager workFlowProcessManager = new BaseWorkFlowProcessManager(this.DbHelper, this.UserInfo);
                BaseWorkFlowProcessEntity workFlowProcessEntity = new BaseWorkFlowProcessEntity();
                if (!string.IsNullOrEmpty(processId))
                {
                    workFlowProcessEntity = workFlowProcessManager.GetObject(processId);
                }
                // 这里本来是想动态创建类库 编码外包[100]
                // System.Reflection.Assembly assembly = Assembly.Load("__code");  
                // System.Reflection.Assembly assembly = Assembly.Load("app_code"); 
                string assemblyString = workFlowProcessEntity.CallBackAssembly;
                workFlowManager = (IWorkFlowManager)Assembly.Load(assemblyString).CreateInstance(workFlowProcessEntity.CallBackClass, true);
                // Type objType = assembly.GetType(workFlowProcessEntity.CallBackClass, true);
                // workFlowManager = (IWorkFlowManager)Activator.CreateInstance(objType);
                workFlowManager.SetUserInfo(this.UserInfo);
                workFlowManager.CurrentTableName = workFlowCurrentEntity.CategoryCode;
                if (!string.IsNullOrEmpty(workFlowProcessEntity.CallBackTable))
                {
                    // 这里本来是想动态创建类库 编码外包[100]
                    workFlowManager.CurrentTableName = workFlowProcessEntity.CallBackTable;
                }
            }
            // workFlowManager = new BaseUserBillManager(this.DbHelper, this.UserInfo);
            return workFlowManager;
        }

        /// <summary>
        /// 获取第一步审核的
        /// </summary>
        /// <param name="categoryCode">单据分类</param>
        /// <param name="workFlowCode">审批流程编号</param>
        /// <returns>审核步骤</returns>
        public BaseWorkFlowActivityEntity GetFirstActivityEntity(string workFlowCode, string categoryCode = null)
        {
            BaseWorkFlowActivityEntity workFlowActivityEntity = null;

            string workFlowId = string.Empty;
            DataTable dt = null;
            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            // 这里是获取用户的工作流, 按用户主键，按模板编号
            if (string.IsNullOrEmpty(workFlowCode))
            {
                parameters.Add(new KeyValuePair<string, object>(BaseWorkFlowBillTemplateEntity.FieldCategoryCode, categoryCode));
                parameters.Add(new KeyValuePair<string, object>(BaseWorkFlowBillTemplateEntity.FieldDeletionStateCode, 0));
                BaseWorkFlowBillTemplateManager templateManager = new BaseWorkFlowBillTemplateManager(this.DbHelper, this.UserInfo);
                dt = templateManager.GetDataTable(parameters);
				BaseWorkFlowBillTemplateEntity templateEntity = BaseEntity.Create<BaseWorkFlowBillTemplateEntity>(dt);
                if (!string.IsNullOrEmpty(templateEntity.Id))
                {
                    workFlowCode = this.UserInfo.Id + "_" + templateEntity.Id;
                }
            }
            if (string.IsNullOrEmpty(workFlowCode))
            {
                return null;
            }
            // 1. 先检查工作流是否存在？
            BaseWorkFlowProcessManager workFlowProcessManager = new BaseWorkFlowProcessManager(this.DbHelper, this.UserInfo);

            parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>(BaseWorkFlowProcessEntity.FieldCode, workFlowCode));
            parameters.Add(new KeyValuePair<string, object>(BaseWorkFlowProcessEntity.FieldEnabled, 1));
            parameters.Add(new KeyValuePair<string, object>(BaseWorkFlowProcessEntity.FieldDeletionStateCode, 0));

            string[] names = new string[] { BaseWorkFlowProcessEntity.FieldCode, BaseWorkFlowProcessEntity.FieldEnabled, BaseWorkFlowProcessEntity.FieldDeletionStateCode };  // 2010.01.25 LiangMingMing 将 BaseWorkFlowProcessEntity.FieldCode 改 BaseWorkFlowProcessEntity.FieldId
            object[] values = new object[] { workFlowCode, 1, 0 };
            workFlowId = workFlowProcessManager.GetId(parameters);
            if (string.IsNullOrEmpty(workFlowId))
            {
                return null;
            }
            // 2. 查找第一步是按帐户审核？还是按角色审核？
            BaseWorkFlowActivityManager workFlowActivityManager = new BaseWorkFlowActivityManager(this.DbHelper, this.UserInfo);
            // 2010.01.25 LiangMingMing 新加了两个参数new string[] { BaseWorkFlowActivityEntity.FieldProcessId }, new string[] { Convert.ToString(workFlowId) },（具体获取哪个流程的步骤）

            parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>(BaseWorkFlowActivityEntity.FieldProcessId, workFlowId.ToString()));
            parameters.Add(new KeyValuePair<string, object>(BaseWorkFlowActivityEntity.FieldEnabled, 1));
            parameters.Add(new KeyValuePair<string, object>(BaseWorkFlowActivityEntity.FieldDeletionStateCode, 0));

            dt = workFlowActivityManager.GetDataTable(parameters);
            // 3. 取第一个排序的数据
            if (dt.Rows.Count == 0)
            {
                return null;
            }
			workFlowActivityEntity = BaseEntity.Create<BaseWorkFlowActivityEntity>(dt.Rows[0]);
            if ((workFlowActivityEntity.AuditUserId == null) && (workFlowActivityEntity.AuditRoleId == null))
            {
                return null;
            }
            return workFlowActivityEntity;
        }

        /// <summary>
        /// 获取下一步是谁审核的功能
        /// </summary>
        /// <param name="workFlowCurrentEntity">当前审核情况</param>
        /// <returns>下一步审核</returns>
        public BaseWorkFlowStepEntity GetNextWorkFlowStep(BaseWorkFlowCurrentEntity workFlowCurrentEntity)
        {
            BaseWorkFlowStepEntity workFlowStepEntity = null;
            DataTable dt = null;
            string nextActivityId = string.Empty;
            // 1. 从工作流审核步骤里选取审核步骤
            BaseWorkFlowStepManager workFlowStepManager = new BaseWorkFlowStepManager(this.DbHelper, this.UserInfo);

            // 若是会签
            bool allPersons = false;
            if (!string.IsNullOrEmpty(workFlowCurrentEntity.ActivityType) && workFlowCurrentEntity.ActivityType.Equals("AllPersons"))
            {
                if (!string.IsNullOrEmpty(workFlowCurrentEntity.ToUserId) && workFlowCurrentEntity.ToUserId.IndexOf(",") >= 0)
                {
                    // 这里是识别是会签
                    allPersons = true;

                    List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
                    parameters.Add(new KeyValuePair<string, object>(BaseWorkFlowStepEntity.FieldObjectId, workFlowCurrentEntity.ObjectId));
                    parameters.Add(new KeyValuePair<string, object>(BaseWorkFlowStepEntity.FieldProcessId, workFlowCurrentEntity.ProcessId));
                    parameters.Add(new KeyValuePair<string, object>(BaseWorkFlowStepEntity.FieldActivityId, workFlowCurrentEntity.ActivityId));

					workFlowStepEntity = BaseEntity.Create<BaseWorkFlowStepEntity>(workFlowStepManager.GetDataTable(parameters));
                    // 这里是替换已经审核的人员
                    workFlowCurrentEntity.ToUserId = workFlowCurrentEntity.ToUserId.Replace(this.UserInfo.Id, "").Trim(',');
                    workFlowStepEntity.AuditUserId = workFlowCurrentEntity.ToUserId;
                    workFlowStepEntity.AuditUserRealName = new BaseUserManager().GetUsersName(workFlowCurrentEntity.ToUserId);
                }
            }

            // 若不是会签
            if (!allPersons)
            {
                // 工作流主键
                string workFlowId = workFlowCurrentEntity.ProcessId.ToString();

                List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
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
                    // 2. 从工作流审核模板里选取审核步骤 下一步是多少？按工作流进行查找审核步骤
                    BaseWorkFlowActivityManager workFlowActivityManager = new BaseWorkFlowActivityManager(this.DbHelper, this.UserInfo);

                    parameters = new List<KeyValuePair<string, object>>();
                    parameters.Add(new KeyValuePair<string, object>(BaseWorkFlowActivityEntity.FieldProcessId, workFlowId));
                    parameters.Add(new KeyValuePair<string, object>(BaseWorkFlowActivityEntity.FieldEnabled, 1));
                    parameters.Add(new KeyValuePair<string, object>(BaseWorkFlowActivityEntity.FieldDeletionStateCode, 0));

                    dt = workFlowActivityManager.GetDataTable(parameters, BaseWorkFlowActivityEntity.FieldSortCode);
                }

                // 审核步骤主键
                string activityId = string.Empty;
                if (workFlowCurrentEntity.ActivityId != null)
                {
                    activityId = workFlowCurrentEntity.ActivityId.ToString();
                }
                if (dt.Rows.Count == 0)
                {
                    return workFlowStepEntity;
                }
                
                if (!string.IsNullOrEmpty(activityId))
                {
                    nextActivityId = BaseSortLogic.GetNextId(dt, activityId.ToString());
                }
                else
                {
                    nextActivityId = dt.Rows[0][BaseWorkFlowActivityEntity.FieldId].ToString();
                }
            }

            if (!string.IsNullOrEmpty(nextActivityId) && dt != null)
            {
                // workFlowActivityEntity = workFlowActivityManager.GetObject(nextActivityId);
                DataRow dr = BaseBusinessLogic.GetDataRow(dt, nextActivityId);
				workFlowStepEntity = BaseEntity.Create<BaseWorkFlowStepEntity>(dr);
                workFlowStepEntity.ActivityId = int.Parse(nextActivityId);
            }
            return workFlowStepEntity;
        }

        #region private string AddHistory(BaseWorkFlowCurrentEntity workFlowCurrentEntity) 添加到工作流审批流程历史
        private string AddHistory(BaseWorkFlowCurrentEntity workFlowCurrentEntity)
        {
            BaseWorkFlowHistoryEntity entity = new BaseWorkFlowHistoryEntity();

            // 这些是待审核信息
            entity.CurrentFlowId = workFlowCurrentEntity.Id;
            entity.ProcessId = workFlowCurrentEntity.ProcessId;
            entity.ActivityId = workFlowCurrentEntity.ActivityId;
            entity.ActivityCode = workFlowCurrentEntity.ActivityCode;
            entity.ActivityFullName = workFlowCurrentEntity.ActivityFullName;

            entity.ToUserId = workFlowCurrentEntity.ToUserId;
            entity.ToUserRealName = workFlowCurrentEntity.ToUserRealName;
            entity.ToRoleId = workFlowCurrentEntity.ToRoleId;
            entity.ToRoleRealName = workFlowCurrentEntity.ToRoleRealName;
            if (string.IsNullOrEmpty(workFlowCurrentEntity.ToDepartmentId))
            {
                entity.ToDepartmentId = this.UserInfo.DepartmentId;
                entity.ToDepartmentName = this.UserInfo.DepartmentName;
            }
            else
            {
                entity.ToDepartmentId = workFlowCurrentEntity.ToDepartmentId;
                entity.ToDepartmentName = workFlowCurrentEntity.ToDepartmentName;
            }

            entity.AuditUserId = workFlowCurrentEntity.AuditUserId;
            entity.AuditUserRealName = workFlowCurrentEntity.AuditUserRealName;

            entity.AuditIdea = workFlowCurrentEntity.AuditIdea;
            entity.AuditStatus = workFlowCurrentEntity.AuditStatus;
            entity.AuditStatusName = workFlowCurrentEntity.AuditStatusName;

            // 这里需要模拟产生的功能
            // entity.SendDate = workFlowCurrentEntity.AuditDate;
            // entity.AuditDate = DateTime.Now;
            entity.SendDate = workFlowCurrentEntity.CreateOn;
            entity.AuditDate = workFlowCurrentEntity.AuditDate;
            entity.Description = workFlowCurrentEntity.Description;
            entity.SortCode = workFlowCurrentEntity.SortCode;
            entity.DeletionStateCode = workFlowCurrentEntity.DeletionStateCode;
            entity.Enabled = workFlowCurrentEntity.Enabled;

            BaseWorkFlowHistoryManager workFlowHistoryManager = new BaseWorkFlowHistoryManager(DbHelper, UserInfo);
            return workFlowHistoryManager.AddObject(entity);
        }

        /// <summary>
        /// 添加到工作流审批流程历史
        /// </summary>
        /// <param name="currentId">当前工作流主键</param>
        /// <returns>主键</returns>
        private string AddHistory(string currentId)
        {
            // 读取一下，然后添加到历史记录表里
            BaseWorkFlowCurrentEntity workFlowCurrentEntity = this.GetObject(currentId);
            return this.AddHistory(workFlowCurrentEntity);
        }
        #endregion

        #region public DataTable GetDataTableByPage(string userId, string searchValue, out int recordCount, int pageIndex = 0, int pageSize = 20, string sortExpression = null, string sortDire = null)
        /// <summary>
        /// 按条件分页查询
        /// </summary>
        /// <param name="userId">查看用户</param>
        /// <param name="searchValue">查询字段</param>
        /// <param name="recordCount">记录数</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页显示</param>
        /// <param name="sortExpression">排序字段</param>
        /// <param name="sortDire">排序方向</param>
        /// <returns>数据表</returns>
        public DataTable GetDataTableByPage(string userId, string searchValue, string categoryCode, out int recordCount, int pageIndex = 0, int pageSize = 20, string sortExpression = "CreateOn", string sortDire = "DESC")
        {
            string whereClause = BaseWorkFlowCurrentEntity.FieldDeletionStateCode + " = 0 ";
            if (!string.IsNullOrEmpty(categoryCode))
                whereClause += " AND " + BaseWorkFlowCurrentEntity.FieldCategoryCode + " = '" + categoryCode + "'";
            searchValue = searchValue.Trim();
            if (!string.IsNullOrEmpty(searchValue))
            {
                searchValue = StringUtil.GetSearchString(searchValue);
                whereClause += " AND (" + BaseWorkFlowCurrentEntity.FieldCreateBy + " LIKE " + searchValue;

                whereClause += " OR " + BaseWorkFlowCurrentEntity.FieldObjectFullName + " LIKE " + searchValue;
                whereClause += " OR " + BaseWorkFlowCurrentEntity.FieldActivityFullName + " LIKE " + searchValue;
                whereClause += " OR " + BaseWorkFlowCurrentEntity.FieldToDepartmentName + " LIKE " + searchValue;
                whereClause += " OR " + BaseWorkFlowCurrentEntity.FieldToUserRealName + " LIKE " + searchValue;
                whereClause += " OR " + BaseWorkFlowCurrentEntity.FieldToRoleRealName + " LIKE " + searchValue;
                whereClause += " OR " + BaseWorkFlowCurrentEntity.FieldAuditUserRealName + " LIKE " + searchValue;

                whereClause += " OR " + BaseWorkFlowCurrentEntity.FieldModifiedBy + " LIKE " + searchValue + ")";
            }
            return GetDataTableByPage(out recordCount, pageIndex, pageSize, sortExpression, sortDire, this.CurrentTableName, whereClause, null, "*");
        }
        #endregion

    }
}