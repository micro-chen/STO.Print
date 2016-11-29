//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd. 
//-----------------------------------------------------------------

using System;
using System.Data;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DotNet.Model
{
    using DotNet.Utilities;

    /// <summary>
    /// BaseWorkFlowStepEntity
    /// 工作流步骤定义
    ///
    /// 修改记录
    ///
    ///		2011-11-17 版本：1.0 JiRiGaLa 创建主键。
    ///
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2011-11-17</date>
    /// </author>
    /// </summary>
    [Serializable, DataContract]
	public partial class BaseWorkFlowStepEntity : BaseEntity
    {
        private int? id = null;
        /// <summary>
        /// 主键
        /// </summary>
        [DataMember]
        public int? Id
        {
            get
            {
                return this.id;
            }
            set
            {
                this.id = value;
            }
        }

        private int? activityId = null;
        /// <summary>
        /// 审批主键
        /// </summary>
        [DataMember]
        public int? ActivityId
        {
            get
            {
                return this.activityId;
            }
            set
            {
                this.activityId = value;
            }
        }

        private string activityCode = null;
        /// <summary>
        /// 审批编号
        /// </summary>
        [DataMember]
        public string ActivityCode
        {
            get
            {
                return this.activityCode;
            }
            set
            {
                this.activityCode = value;
            }
        }

        private string activityFullName = null;
        /// <summary>
        /// 审批名称
        /// </summary>
        [DataMember]
        public string ActivityFullName
        {
            get
            {
                return this.activityFullName;
            }
            set
            {
                this.activityFullName = value;
            }
        }


        private string categoryCode = null;
        /// <summary>
        /// 实体分类主键
        /// </summary>
        [DataMember]
        public string CategoryCode
        {
            get
            {
                return this.categoryCode;
            }
            set
            {
                this.categoryCode = value;
            }
        }

        private string objectId = null;
        /// <summary>
        /// 实体主键
        /// </summary>
        [DataMember]
        public string ObjectId
        {
            get
            {
                return this.objectId;
            }
            set
            {
                this.objectId = value;
            }
        }

        private int? processId = 0;
        /// <summary>
        /// 工作流主键
        /// </summary>
        [DataMember]
        public int? ProcessId
        {
            get
            {
                return this.processId;
            }
            set
            {
                this.processId = value;
            }
        }

        private string code = null;
        /// <summary>
        /// 流程编号
        /// </summary>
        [DataMember]
        public string Code
        {
            get
            {
                return this.code;
            }
            set
            {
                this.code = value;
            }
        }

        private string fullName = null;
        /// <summary>
        /// 流程名称
        /// </summary>
        [DataMember]
        public string FullName
        {
            get
            {
                return this.fullName;
            }
            set
            {
                this.fullName = value;
            }
        }

        private string auditDepartmentId = null;
        /// <summary>
        /// 审核部门主键
        /// </summary>
        [DataMember]
        public string AuditDepartmentId
        {
            get
            {
                return this.auditDepartmentId;
            }
            set
            {
                this.auditDepartmentId = value;
            }
        }

        private string auditDepartmentName = null;
        /// <summary>
        /// 审核部门名称
        /// </summary>
        [DataMember]
        public string AuditDepartmentName
        {
            get
            {
                return this.auditDepartmentName;
            }
            set
            {
                this.auditDepartmentName = value;
            }
        }

        private string auditUserId = null;
        /// <summary>
        /// 审核员主键
        /// </summary>
        [DataMember]
        public string AuditUserId
        {
            get
            {
                return this.auditUserId;
            }
            set
            {
                this.auditUserId = value;
            }
        }

        private string auditUserCode = null;
        /// <summary>
        /// 审核员编号
        /// </summary>
        [DataMember]
        public string AuditUserCode
        {
            get
            {
                return this.auditUserCode;
            }
            set
            {
                this.auditUserCode = value;
            }
        }

        private string auditUserRealName = null;
        /// <summary>
        /// 审核员
        /// </summary>
        [DataMember]
        public string AuditUserRealName
        {
            get
            {
                return this.auditUserRealName;
            }
            set
            {
                this.auditUserRealName = value;
            }
        }

        private string auditRoleId = null;
        /// <summary>
        /// 审核角色主键
        /// </summary>
        [DataMember]
        public string AuditRoleId
        {
            get
            {
                return this.auditRoleId;
            }
            set
            {
                this.auditRoleId = value;
            }
        }

        private string auditRoleRealName = null;
        /// <summary>
        /// 审核角色
        /// </summary>
        [DataMember]
        public string AuditRoleRealName
        {
            get
            {
                return this.auditRoleRealName;
            }
            set
            {
                this.auditRoleRealName = value;
            }
        }

        private string activityType = null;
        /// <summary>
        /// 审核类型
        /// </summary>
        [DataMember]
        public string ActivityType
        {
            get
            {
                return this.activityType;
            }
            set
            {
                this.activityType = value;
            }
        }

        private int? allowPrint = 0;
        /// <summary>
        /// 允许打印
        /// </summary>
        [DataMember]
        public int? AllowPrint
        {
            get
            {
                return this.allowPrint;
            }
            set
            {
                this.allowPrint = value;
            }
        }

        private int? allowEditDocuments = 0;
        /// <summary>
        /// 允许编辑单据
        /// </summary>
        [DataMember]
        public int? AllowEditDocuments
        {
            get
            {
                return this.allowEditDocuments;
            }
            set
            {
                this.allowEditDocuments = value;
            }
        }

        private int? sortCode = null;
        /// <summary>
        /// 排序码
        /// </summary>
        [DataMember]
        public int? SortCode
        {
            get
            {
                return this.sortCode;
            }
            set
            {
                this.sortCode = value;
            }
        }

        private int? enabled = 1;
        /// <summary>
        /// 有效
        /// </summary>
        [DataMember]
        public int? Enabled
        {
            get
            {
                return this.enabled;
            }
            set
            {
                this.enabled = value;
            }
        }

        private int? deletionStateCode = 0;
        /// <summary>
        /// 删除标志
        /// </summary>
        [DataMember]
        public int? DeletionStateCode
        {
            get
            {
                return this.deletionStateCode;
            }
            set
            {
                this.deletionStateCode = value;
            }
        }

        private string description = null;
        /// <summary>
        /// 备注
        /// </summary>
        [DataMember]
        public string Description
        {
            get
            {
                return this.description;
            }
            set
            {
                this.description = value;
            }
        }

        private DateTime? createOn = null;
        /// <summary>
        /// 创建日期
        /// </summary>
        [DataMember]
        public DateTime? CreateOn
        {
            get
            {
                return this.createOn;
            }
            set
            {
                this.createOn = value;
            }
        }

        private string createUserId = null;
        /// <summary>
        /// 创建用户主键
        /// </summary>
        [DataMember]
        public string CreateUserId
        {
            get
            {
                return this.createUserId;
            }
            set
            {
                this.createUserId = value;
            }
        }

        private string createBy = null;
        /// <summary>
        /// 创建用户
        /// </summary>
        [DataMember]
        public string CreateBy
        {
            get
            {
                return this.createBy;
            }
            set
            {
                this.createBy = value;
            }
        }

        private DateTime? modifiedOn = null;
        /// <summary>
        /// 修改日期
        /// </summary>
        [DataMember]
        public DateTime? ModifiedOn
        {
            get
            {
                return this.modifiedOn;
            }
            set
            {
                this.modifiedOn = value;
            }
        }

        private string modifiedUserId = null;
        /// <summary>
        /// 修改用户主键
        /// </summary>
        [DataMember]
        public string ModifiedUserId
        {
            get
            {
                return this.modifiedUserId;
            }
            set
            {
                this.modifiedUserId = value;
            }
        }

        private string modifiedBy = null;
        /// <summary>
        /// 修改用户
        /// </summary>
        [DataMember]
        public string ModifiedBy
        {
            get
            {
                return this.modifiedBy;
            }
            set
            {
                this.modifiedBy = value;
            }
        }

        /// <summary>
        /// 从数据行读取
        /// </summary>
        /// <param name="dr">数据行</param>
		protected override BaseEntity GetFrom(IDataRow dr)
        {
            Id = BaseBusinessLogic.ConvertToInt(dr[BaseWorkFlowStepEntity.FieldId]);
            ProcessId = BaseBusinessLogic.ConvertToInt(dr[BaseWorkFlowStepEntity.FieldProcessId]);
            Code = BaseBusinessLogic.ConvertToString(dr[BaseWorkFlowStepEntity.FieldCode]);
            FullName = BaseBusinessLogic.ConvertToString(dr[BaseWorkFlowStepEntity.FieldFullName]);
            AuditDepartmentId = BaseBusinessLogic.ConvertToString(dr[BaseWorkFlowStepEntity.FieldAuditDepartmentId]);
            AuditDepartmentName = BaseBusinessLogic.ConvertToString(dr[BaseWorkFlowStepEntity.FieldAuditDepartmentName]);
            AuditUserId = BaseBusinessLogic.ConvertToString(dr[BaseWorkFlowStepEntity.FieldAuditUserId]);
            AuditUserCode = BaseBusinessLogic.ConvertToString(dr[BaseWorkFlowStepEntity.FieldAuditUserCode]);
            AuditUserRealName = BaseBusinessLogic.ConvertToString(dr[BaseWorkFlowStepEntity.FieldAuditUserRealName]);
            AuditRoleId = BaseBusinessLogic.ConvertToString(dr[BaseWorkFlowStepEntity.FieldAuditRoleId]);
            AuditRoleRealName = BaseBusinessLogic.ConvertToString(dr[BaseWorkFlowStepEntity.FieldAuditRoleRealName]);
            if (dr.ContainsColumn(BaseWorkFlowStepEntity.FieldActivityId))
            {
                ActivityId = BaseBusinessLogic.ConvertToInt(dr[BaseWorkFlowStepEntity.FieldActivityId]);
            }
            else
            {
                ActivityId = BaseBusinessLogic.ConvertToInt(dr[BaseWorkFlowStepEntity.FieldId]);
            }
            ActivityType = BaseBusinessLogic.ConvertToString(dr[BaseWorkFlowStepEntity.FieldActivityType]);
            AllowPrint = BaseBusinessLogic.ConvertToInt(dr[BaseWorkFlowStepEntity.FieldAllowPrint]);
            AllowEditDocuments = BaseBusinessLogic.ConvertToInt(dr[BaseWorkFlowStepEntity.FieldAllowEditDocuments]);
            SortCode = BaseBusinessLogic.ConvertToInt(dr[BaseWorkFlowStepEntity.FieldSortCode]);
            Enabled = BaseBusinessLogic.ConvertToInt(dr[BaseWorkFlowStepEntity.FieldEnabled]);
            DeletionStateCode = BaseBusinessLogic.ConvertToInt(dr[BaseWorkFlowStepEntity.FieldDeletionStateCode]);
            Description = BaseBusinessLogic.ConvertToString(dr[BaseWorkFlowStepEntity.FieldDescription]);
            CreateOn = BaseBusinessLogic.ConvertToNullableDateTime(dr[BaseWorkFlowStepEntity.FieldCreateOn]);
            CreateUserId = BaseBusinessLogic.ConvertToString(dr[BaseWorkFlowStepEntity.FieldCreateUserId]);
            CreateBy = BaseBusinessLogic.ConvertToString(dr[BaseWorkFlowStepEntity.FieldCreateBy]);
            ModifiedOn = BaseBusinessLogic.ConvertToNullableDateTime(dr[BaseWorkFlowStepEntity.FieldModifiedOn]);
            ModifiedUserId = BaseBusinessLogic.ConvertToString(dr[BaseWorkFlowStepEntity.FieldModifiedUserId]);
            ModifiedBy = BaseBusinessLogic.ConvertToString(dr[BaseWorkFlowStepEntity.FieldModifiedBy]);
            return this;
        }

        ///<summary>
        /// 工作流步骤定义
        ///</summary>
        [NonSerialized]
        public static string TableName = "BaseWorkFlowStep";

        ///<summary>
        /// 主键
        ///</summary>
        [NonSerialized]
        public static string FieldId = "Id";

        ///<summary>
        /// 实体分类主键
        ///</summary>
        [NonSerialized]
        public static string FieldCategoryCode = "CategoryCode";

        ///<summary>
        /// 实体主键
        ///</summary>
        [NonSerialized]
        public static string FieldObjectId = "ObjectId";

        ///<summary>
        /// 工作流主键
        ///</summary>
        [NonSerialized]
        public static string FieldProcessId = "ProcessId";

        ///<summary>
        /// 审批主键
        ///</summary>
        [NonSerialized]
        public static string FieldActivityId = "ActivityId";

        ///<summary>
        /// 流程编号
        ///</summary>
        [NonSerialized]
        public static string FieldCode = "Code";

        ///<summary>
        /// 流程名称
        ///</summary>
        [NonSerialized]
        public static string FieldFullName = "FullName";

        ///<summary>
        /// 审核部门主键
        ///</summary>
        [NonSerialized]
        public static string FieldAuditDepartmentId = "AuditDepartmentId";

        ///<summary>
        /// 审核部门名称
        ///</summary>
        [NonSerialized]
        public static string FieldAuditDepartmentName = "AuditDepartmentName";

        ///<summary>
        /// 审核员主键
        ///</summary>
        [NonSerialized]
        public static string FieldAuditUserId = "AuditUserId";

        ///<summary>
        /// 审核员编号
        ///</summary>
        [NonSerialized]
        public static string FieldAuditUserCode = "AuditUserCode";

        ///<summary>
        /// 审核员
        ///</summary>
        [NonSerialized]
        public static string FieldAuditUserRealName = "AuditUserRealName";

        ///<summary>
        /// 审核角色主键
        ///</summary>
        [NonSerialized]
        public static string FieldAuditRoleId = "AuditRoleId";

        ///<summary>
        /// 审核角色
        ///</summary>
        [NonSerialized]
        public static string FieldAuditRoleRealName = "AuditRoleRealName";

        ///<summary>
        /// 节点类型
        ///</summary>
        [NonSerialized]
        public static string FieldActivityType = "ActivityType";

        ///<summary>
        /// 允许打印
        ///</summary>
        [NonSerialized]
        public static string FieldAllowPrint = "AllowPrint";

        ///<summary>
        /// 允许编辑单据
        ///</summary>
        [NonSerialized]
        public static string FieldAllowEditDocuments = "AllowEditDocuments";

        ///<summary>
        /// 排序码
        ///</summary>
        [NonSerialized]
        public static string FieldSortCode = "SortCode";

        ///<summary>
        /// 有效
        ///</summary>
        [NonSerialized]
        public static string FieldEnabled = "Enabled";

        ///<summary>
        /// 删除标志
        ///</summary>
        [NonSerialized]
        public static string FieldDeletionStateCode = "DeletionStateCode";

        ///<summary>
        /// 备注
        ///</summary>
        [NonSerialized]
        public static string FieldDescription = "Description";

        ///<summary>
        /// 创建日期
        ///</summary>
        [NonSerialized]
        public static string FieldCreateOn = "CreateOn";

        ///<summary>
        /// 创建用户主键
        ///</summary>
        [NonSerialized]
        public static string FieldCreateUserId = "CreateUserId";

        ///<summary>
        /// 创建用户
        ///</summary>
        [NonSerialized]
        public static string FieldCreateBy = "CreateBy";

        ///<summary>
        /// 修改日期
        ///</summary>
        [NonSerialized]
        public static string FieldModifiedOn = "ModifiedOn";

        ///<summary>
        /// 修改用户主键
        ///</summary>
        [NonSerialized]
        public static string FieldModifiedUserId = "ModifiedUserId";

        ///<summary>
        /// 修改用户
        ///</summary>
        [NonSerialized]
        public static string FieldModifiedBy = "ModifiedBy";
    }
}
