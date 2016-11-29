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
    /// BaseWorkFlowActivityEntity
    /// 工作流步骤定义
    ///
    /// 修改记录
    ///
    ///		2010-08-04 版本：1.0 JiRiGaLa 创建主键。
    ///
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2010-08-04</date>
    /// </author>
    /// </summary>
    [Serializable, DataContract]
	public partial class BaseWorkFlowActivityEntity : BaseEntity
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

        private int? processId = null;
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

        private int? commitmentDays = 0;
        /// <summary>
        /// 承诺天数
        /// </summary>
        [DataMember]
        public int? CommitmentDays
        {
            get
            {
                return this.commitmentDays;
            }
            set
            {
                this.commitmentDays = value;
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

        private string editFields = null;
        /// <summary>
        /// 可编辑字段
        /// </summary>
        [DataMember]
        public string EditFields
        {
            get
            {
                return this.editFields;
            }
            set
            {
                this.editFields = value;
            }
        }

        private string activityType = null;
        /// <summary>
        /// 节点审核类型
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

        private string defaultAuditUserId = null;
        /// <summary>
        /// 默认审核人主键
        /// </summary>
        [DataMember]
        public string DefaultAuditUserId
        {
            get
            {
                return this.defaultAuditUserId;
            }
            set
            {
                this.defaultAuditUserId = value;
            }
        }

        private string defaultAuditUserRealName = null;
        /// <summary>
        /// 默认审核人姓名
        /// </summary>
        [DataMember]
        public string DefaultAuditUserRealName
        {
            get
            {
                return this.defaultAuditUserRealName;
            }
            set
            {
                this.defaultAuditUserRealName = value;
            }
        }

        private int? allowSkip = 0;
        /// <summary>
        /// 允许跳过此审核步骤
        /// </summary>
        [DataMember]
        public int? AllowSkip
        {
            get
            {
                return this.allowSkip;
            }
            set
            {
                this.allowSkip = value;
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

        private int? allowAuditQuash = 0;
        /// <summary>
        /// 允许废弃单据
        /// </summary>
        [DataMember]
        public int? AllowAuditQuash
        {
            get
            {
                return this.allowAuditQuash;
            }
            set
            {
                this.allowAuditQuash = value;
            }
        }

        private int? allowAuditPass = 0;
        /// <summary>
        /// 允许审核通过单据
        /// </summary>
        [DataMember]
        public int? AllowAuditPass
        {
            get
            {
                return this.allowAuditPass;
            }
            set
            {
                this.allowAuditPass = value;
            }
        }

        private string enterConstraint = string.Empty;
        /// <summary>
        /// 入口条件
        /// </summary>
        [DataMember]
        public string EnterConstraint
        {
            get
            {
                return this.enterConstraint;
            }
            set
            {
                this.enterConstraint = value;
            }
        } 

        private string endConstraint = string.Empty;
        /// <summary>
        /// 结束条件
        /// </summary>
        [DataMember]
        public string EndConstraint
        {
            get
            {
                return this.endConstraint;
            }
            set
            {
                this.endConstraint = value;
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
            Id = BaseBusinessLogic.ConvertToInt(dr[BaseWorkFlowActivityEntity.FieldId]);
            ProcessId = BaseBusinessLogic.ConvertToInt(dr[BaseWorkFlowActivityEntity.FieldProcessId]);
            Code = BaseBusinessLogic.ConvertToString(dr[BaseWorkFlowActivityEntity.FieldCode]);
            CommitmentDays = BaseBusinessLogic.ConvertToInt(dr[BaseWorkFlowActivityEntity.FieldCommitmentDays]);
            FullName = BaseBusinessLogic.ConvertToString(dr[BaseWorkFlowActivityEntity.FieldFullName]);
            AuditDepartmentId = BaseBusinessLogic.ConvertToString(dr[BaseWorkFlowActivityEntity.FieldAuditDepartmentId]);
            AuditDepartmentName = BaseBusinessLogic.ConvertToString(dr[BaseWorkFlowActivityEntity.FieldAuditDepartmentName]);
            AuditUserId = BaseBusinessLogic.ConvertToString(dr[BaseWorkFlowActivityEntity.FieldAuditUserId]);
            AuditUserCode = BaseBusinessLogic.ConvertToString(dr[BaseWorkFlowActivityEntity.FieldAuditUserCode]);
            AuditUserRealName = BaseBusinessLogic.ConvertToString(dr[BaseWorkFlowActivityEntity.FieldAuditUserRealName]);
            AuditRoleId = BaseBusinessLogic.ConvertToString(dr[BaseWorkFlowActivityEntity.FieldAuditRoleId]);
            AuditRoleRealName = BaseBusinessLogic.ConvertToString(dr[BaseWorkFlowActivityEntity.FieldAuditRoleRealName]);
            ActivityType = BaseBusinessLogic.ConvertToString(dr[BaseWorkFlowActivityEntity.FieldActivityType]);
            DefaultAuditUserId = BaseBusinessLogic.ConvertToString(dr[BaseWorkFlowActivityEntity.FieldDefaultAuditUserId]);
            DefaultAuditUserRealName = BaseBusinessLogic.ConvertToString(dr[BaseWorkFlowActivityEntity.FieldDefaultAuditUserRealName]);
            EditFields = BaseBusinessLogic.ConvertToString(dr[BaseWorkFlowActivityEntity.FieldEditFields]);
            AllowSkip = BaseBusinessLogic.ConvertToInt(dr[BaseWorkFlowActivityEntity.FieldAllowSkip]);
            AllowPrint = BaseBusinessLogic.ConvertToInt(dr[BaseWorkFlowActivityEntity.FieldAllowPrint]);
            AllowEditDocuments = BaseBusinessLogic.ConvertToInt(dr[BaseWorkFlowActivityEntity.FieldAllowEditDocuments]);
            AllowAuditQuash = BaseBusinessLogic.ConvertToInt(dr[BaseWorkFlowActivityEntity.FieldAllowAuditQuash]);
            AllowAuditPass = BaseBusinessLogic.ConvertToInt(dr[BaseWorkFlowActivityEntity.FieldAllowAuditPass]);
            EnterConstraint = BaseBusinessLogic.ConvertToString(dr[BaseWorkFlowActivityEntity.FieldEnterConstraint]);
            EndConstraint = BaseBusinessLogic.ConvertToString(dr[BaseWorkFlowActivityEntity.FieldEndConstraint]);
            SortCode = BaseBusinessLogic.ConvertToInt(dr[BaseWorkFlowActivityEntity.FieldSortCode]);
            Enabled = BaseBusinessLogic.ConvertToInt(dr[BaseWorkFlowActivityEntity.FieldEnabled]);
            DeletionStateCode = BaseBusinessLogic.ConvertToInt(dr[BaseWorkFlowActivityEntity.FieldDeletionStateCode]);
            Description = BaseBusinessLogic.ConvertToString(dr[BaseWorkFlowActivityEntity.FieldDescription]);
            CreateOn = BaseBusinessLogic.ConvertToNullableDateTime(dr[BaseWorkFlowActivityEntity.FieldCreateOn]);
            CreateUserId = BaseBusinessLogic.ConvertToString(dr[BaseWorkFlowActivityEntity.FieldCreateUserId]);
            CreateBy = BaseBusinessLogic.ConvertToString(dr[BaseWorkFlowActivityEntity.FieldCreateBy]);
            ModifiedOn = BaseBusinessLogic.ConvertToNullableDateTime(dr[BaseWorkFlowActivityEntity.FieldModifiedOn]);
            ModifiedUserId = BaseBusinessLogic.ConvertToString(dr[BaseWorkFlowActivityEntity.FieldModifiedUserId]);
            ModifiedBy = BaseBusinessLogic.ConvertToString(dr[BaseWorkFlowActivityEntity.FieldModifiedBy]);
            // 获取扩展属性
            GetFromExpand(dr);
            return this;
        }

        ///<summary>
        /// 工作流步骤定义
        ///</summary>
        [NonSerialized]
        public static string TableName = "BaseWorkFlowActivity";

        ///<summary>
        /// 主键
        ///</summary>
        [NonSerialized]
        public static string FieldId = "Id";

        ///<summary>
        /// 工作流主键
        ///</summary>
        [NonSerialized]
        public static string FieldProcessId = "ProcessId";

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
        /// 承诺天数
        ///</summary>
        [NonSerialized]
        public static string FieldCommitmentDays = "CommitmentDays";

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
        /// 节点审核类型
        ///</summary>
        [NonSerialized]
        public static string FieldActivityType = "ActivityType";

        ///<summary>
        /// 默认审核人主键
        ///</summary>
        [NonSerialized]
        public static string FieldDefaultAuditUserId = "DefaultAuditUserId";

        ///<summary>
        /// 默认审核人姓名
        ///</summary>
        [NonSerialized]
        public static string FieldDefaultAuditUserRealName = "DefaultAuditUserRealName";

        ///<summary>
        /// 可编辑字段
        ///</summary>
        [NonSerialized]
        public static string FieldEditFields = "EditFields";

        ///<summary>
        /// 允许跳过此审核步骤
        ///</summary>
        [NonSerialized]
        public static string FieldAllowSkip = "AllowSkip";

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
        /// 允许废弃单据
        ///</summary>
        [NonSerialized]
        public static string FieldAllowAuditQuash = "AllowAuditQuash";

        ///<summary>
        /// 允许审核通过单据
        ///</summary>
        [NonSerialized]
        public static string FieldAllowAuditPass = "AllowAuditPass";

        ///<summary>
        /// 入口条件
        ///</summary>
        [NonSerialized]
        public static string FieldEnterConstraint = "EnterConstraint";

        ///<summary>
        /// 结束条件
        ///</summary>
        [NonSerialized]
        public static string FieldEndConstraint = "EndConstraint";

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
