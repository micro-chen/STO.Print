//--------------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd. 
//--------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.Serialization;

namespace DotNet.Model
{
    using DotNet.Utilities;

    /// <summary>
    /// BaseWorkFlowHistoryEntity
    /// 工作流审核历史步骤记录
    /// 
    /// 修改记录
    /// 
    /// 2012-07-03 版本：1.0 JiRiGaLa 创建主键。
    /// 
    /// <author>
    ///     <name>JiRiGaLa</name>
    ///     <date>2012-07-03</date>
    /// </author>
    /// </summary>
    [Serializable, DataContract]
	public partial class BaseWorkFlowHistoryEntity : BaseEntity
    {
        private int? id = null;
        /// <summary>
        /// 代码
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

        private String currentFlowId = string.Empty;
        /// <summary>
        /// 当前工作流主键
        /// </summary>
        [DataMember]
        public String CurrentFlowId
        {
            get
            {
                return this.currentFlowId;
            }
            set
            {
                this.currentFlowId = value;
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

        private int? activityId = null;
        /// <summary>
        /// 审核步骤主键
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

        private String activityCode = string.Empty;
        /// <summary>
        /// 审核步骤编号
        /// </summary>
        [DataMember]
        public String ActivityCode
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

        private String activityFullName = string.Empty;
        /// <summary>
        /// 审核步骤名称
        /// </summary>
        [DataMember]
        public String ActivityFullName
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

        private String toDepartmentId = string.Empty;
        /// <summary>
        /// 待审核部门主键
        /// </summary>
        [DataMember]
        public String ToDepartmentId
        {
            get
            {
                return this.toDepartmentId;
            }
            set
            {
                this.toDepartmentId = value;
            }
        }

        private String toDepartmentName = string.Empty;
        /// <summary>
        /// 待审核部门名称
        /// </summary>
        [DataMember]
        public String ToDepartmentName
        {
            get
            {
                return this.toDepartmentName;
            }
            set
            {
                this.toDepartmentName = value;
            }
        }

        private String toUserId = string.Empty;
        /// <summary>
        /// 待审核用户主键
        /// </summary>
        [DataMember]
        public String ToUserId
        {
            get
            {
                return this.toUserId;
            }
            set
            {
                this.toUserId = value;
            }
        }

        private String toUserRealName = string.Empty;
        /// <summary>
        /// 待审核用户
        /// </summary>
        [DataMember]
        public String ToUserRealName
        {
            get
            {
                return this.toUserRealName;
            }
            set
            {
                this.toUserRealName = value;
            }
        }

        private String toRoleId = string.Empty;
        /// <summary>
        /// 待审核角色主键
        /// </summary>
        [DataMember]
        public String ToRoleId
        {
            get
            {
                return this.toRoleId;
            }
            set
            {
                this.toRoleId = value;
            }
        }

        private String toRoleRealName = string.Empty;
        /// <summary>
        /// 待审核角色
        /// </summary>
        [DataMember]
        public String ToRoleRealName
        {
            get
            {
                return this.toRoleRealName;
            }
            set
            {
                this.toRoleRealName = value;
            }
        }

        private String auditUserId = string.Empty;
        /// <summary>
        /// 审核用户主键
        /// </summary>
        [DataMember]
        public String AuditUserId
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

        private String auditUserCode = string.Empty;
        /// <summary>
        /// 审核用户主键
        /// </summary>
        [DataMember]
        public String AuditUserCode
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

        private String auditUserRealName = string.Empty;
        /// <summary>
        /// 审核用户
        /// </summary>
        [DataMember]
        public String AuditUserRealName
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

        private DateTime? sendDate = null;
        /// <summary>
        /// 发出日期
        /// </summary>
        [DataMember]
        public DateTime? SendDate
        {
            get
            {
                return this.sendDate;
            }
            set
            {
                this.sendDate = value;
            }
        }

        private DateTime? auditDate = null;
        /// <summary>
        /// 审核日期
        /// </summary>
        [DataMember]
        public DateTime? AuditDate
        {
            get
            {
                return this.auditDate;
            }
            set
            {
                this.auditDate = value;
            }
        }

        private String auditIdea = string.Empty;
        /// <summary>
        /// 审核意见
        /// </summary>
        [DataMember]
        public String AuditIdea
        {
            get
            {
                return this.auditIdea;
            }
            set
            {
                this.auditIdea = value;
            }
        }

        private String auditStatus = string.Empty;
        /// <summary>
        /// 审核状态码
        /// </summary>
        [DataMember]
        public String AuditStatus
        {
            get
            {
                return this.auditStatus;
            }
            set
            {
                this.auditStatus = value;
            }
        }

        private String auditStatusName = string.Empty;
        /// <summary>
        /// 审核状态
        /// </summary>
        [DataMember]
        public String AuditStatusName
        {
            get
            {
                return this.auditStatusName;
            }
            set
            {
                this.auditStatusName = value;
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

        private String description = string.Empty;
        /// <summary>
        /// 备注
        /// </summary>
        [DataMember]
        public String Description
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

        private String createUserId = string.Empty;
        /// <summary>
        /// 创建用户主键
        /// </summary>
        [DataMember]
        public String CreateUserId
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

        private String createBy = string.Empty;
        /// <summary>
        /// 创建用户
        /// </summary>
        [DataMember]
        public String CreateBy
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

        private String modifiedUserId = string.Empty;
        /// <summary>
        /// 修改用户主键
        /// </summary>
        [DataMember]
        public String ModifiedUserId
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

        private String modifiedBy = string.Empty;
        /// <summary>
        /// 修改用户
        /// </summary>
        [DataMember]
        public String ModifiedBy
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
            Id = BaseBusinessLogic.ConvertToInt(dr[BaseWorkFlowHistoryEntity.FieldId]);
            CurrentFlowId = BaseBusinessLogic.ConvertToString(dr[BaseWorkFlowHistoryEntity.FieldCurrentFlowId]);
            ProcessId = BaseBusinessLogic.ConvertToInt(dr[BaseWorkFlowHistoryEntity.FieldProcessId]);
            ActivityId = BaseBusinessLogic.ConvertToInt(dr[BaseWorkFlowHistoryEntity.FieldActivityId]);
            ActivityCode = BaseBusinessLogic.ConvertToString(dr[BaseWorkFlowHistoryEntity.FieldActivityCode]);
            ActivityFullName = BaseBusinessLogic.ConvertToString(dr[BaseWorkFlowHistoryEntity.FieldActivityFullName]);
            ToDepartmentId = BaseBusinessLogic.ConvertToString(dr[BaseWorkFlowHistoryEntity.FieldToDepartmentId]);
            ToDepartmentName = BaseBusinessLogic.ConvertToString(dr[BaseWorkFlowHistoryEntity.FieldToDepartmentName]);
            ToUserId = BaseBusinessLogic.ConvertToString(dr[BaseWorkFlowHistoryEntity.FieldToUserId]);
            ToUserRealName = BaseBusinessLogic.ConvertToString(dr[BaseWorkFlowHistoryEntity.FieldToUserRealName]);
            ToRoleId = BaseBusinessLogic.ConvertToString(dr[BaseWorkFlowHistoryEntity.FieldToRoleId]);
            ToRoleRealName = BaseBusinessLogic.ConvertToString(dr[BaseWorkFlowHistoryEntity.FieldToRoleRealName]);
            AuditUserId = BaseBusinessLogic.ConvertToString(dr[BaseWorkFlowHistoryEntity.FieldAuditUserId]);
            AuditUserCode = BaseBusinessLogic.ConvertToString(dr[BaseWorkFlowHistoryEntity.FieldAuditUserCode]);
            AuditUserRealName = BaseBusinessLogic.ConvertToString(dr[BaseWorkFlowHistoryEntity.FieldAuditUserRealName]);
			SendDate = BaseBusinessLogic.ConvertToNullableDateTime(dr[BaseWorkFlowHistoryEntity.FieldSendDate]);
			AuditDate = BaseBusinessLogic.ConvertToNullableDateTime(dr[BaseWorkFlowHistoryEntity.FieldAuditDate]);
            AuditIdea = BaseBusinessLogic.ConvertToString(dr[BaseWorkFlowHistoryEntity.FieldAuditIdea]);
            AuditStatus = BaseBusinessLogic.ConvertToString(dr[BaseWorkFlowHistoryEntity.FieldAuditStatus]);
            AuditStatusName = BaseBusinessLogic.ConvertToString(dr[BaseWorkFlowHistoryEntity.FieldAuditStatusName]);
            SortCode = BaseBusinessLogic.ConvertToInt(dr[BaseWorkFlowHistoryEntity.FieldSortCode]);
            Enabled = BaseBusinessLogic.ConvertToInt(dr[BaseWorkFlowHistoryEntity.FieldEnabled]);
            DeletionStateCode = BaseBusinessLogic.ConvertToInt(dr[BaseWorkFlowHistoryEntity.FieldDeletionStateCode]);
            Description = BaseBusinessLogic.ConvertToString(dr[BaseWorkFlowHistoryEntity.FieldDescription]);
            CreateOn = BaseBusinessLogic.ConvertToNullableDateTime(dr[BaseWorkFlowHistoryEntity.FieldCreateOn]);
            CreateUserId = BaseBusinessLogic.ConvertToString(dr[BaseWorkFlowHistoryEntity.FieldCreateUserId]);
            CreateBy = BaseBusinessLogic.ConvertToString(dr[BaseWorkFlowHistoryEntity.FieldCreateBy]);
            ModifiedOn = BaseBusinessLogic.ConvertToNullableDateTime(dr[BaseWorkFlowHistoryEntity.FieldModifiedOn]);
            ModifiedUserId = BaseBusinessLogic.ConvertToString(dr[BaseWorkFlowHistoryEntity.FieldModifiedUserId]);
            ModifiedBy = BaseBusinessLogic.ConvertToString(dr[BaseWorkFlowHistoryEntity.FieldModifiedBy]);
            // 获取扩展属性
            GetFromExpand(dr);
            return this;
        }

        ///<summary>
        /// 工作流审核历史步骤记录
        ///</summary>
        [NonSerialized]
        public static string TableName = "BaseWorkFlowHistory";

        ///<summary>
        /// 代码
        ///</summary>
        [NonSerialized]
        public static string FieldId = "Id";

        ///<summary>
        /// 当前工作流主键
        ///</summary>
        [NonSerialized]
        public static string FieldCurrentFlowId = "CurrentFlowId";

        ///<summary>
        /// 工作流主键
        ///</summary>
        [NonSerialized]
        public static string FieldProcessId = "ProcessId";

        ///<summary>
        /// 审核步骤主键
        ///</summary>
        [NonSerialized]
        public static string FieldActivityId = "ActivityId";

        ///<summary>
        /// 审核步骤编号
        ///</summary>
        [NonSerialized]
        public static string FieldActivityCode = "ActivityCode";

        ///<summary>
        /// 审核步骤名称
        ///</summary>
        [NonSerialized]
        public static string FieldActivityFullName = "ActivityFullName";

        ///<summary>
        /// 待审核部门主键
        ///</summary>
        [NonSerialized]
        public static string FieldToDepartmentId = "ToDepartmentId";

        ///<summary>
        /// 待审核部门名称
        ///</summary>
        [NonSerialized]
        public static string FieldToDepartmentName = "ToDepartmentName";

        ///<summary>
        /// 待审核用户主键
        ///</summary>
        [NonSerialized]
        public static string FieldToUserId = "ToUserId";

        ///<summary>
        /// 待审核用户
        ///</summary>
        [NonSerialized]
        public static string FieldToUserRealName = "ToUserRealName";

        ///<summary>
        /// 待审核角色主键
        ///</summary>
        [NonSerialized]
        public static string FieldToRoleId = "ToRoleId";

        ///<summary>
        /// 待审核角色
        ///</summary>
        [NonSerialized]
        public static string FieldToRoleRealName = "ToRoleRealName";

        ///<summary>
        /// 审核用户主键
        ///</summary>
        [NonSerialized]
        public static string FieldAuditUserId = "AuditUserId";

        ///<summary>
        /// 审核用户主键
        ///</summary>
        [NonSerialized]
        public static string FieldAuditUserCode = "AuditUserCode";

        ///<summary>
        /// 审核用户
        ///</summary>
        [NonSerialized]
        public static string FieldAuditUserRealName = "AuditUserRealName";

        ///<summary>
        /// 发出日期
        ///</summary>
        [NonSerialized]
        public static string FieldSendDate = "SendDate";

        ///<summary>
        /// 审核日期
        ///</summary>
        [NonSerialized]
        public static string FieldAuditDate = "AuditDate";

        ///<summary>
        /// 审核意见
        ///</summary>
        [NonSerialized]
        public static string FieldAuditIdea = "AuditIdea";

        ///<summary>
        /// 审核状态码
        ///</summary>
        [NonSerialized]
        public static string FieldAuditStatus = "AuditStatus";

        ///<summary>
        /// 审核状态
        ///</summary>
        [NonSerialized]
        public static string FieldAuditStatusName = "AuditStatusName";

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
