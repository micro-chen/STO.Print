//--------------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd. 
//--------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data;
using System.ServiceModel;
using System.Runtime.Serialization;

namespace DotNet.Model
{
    using DotNet.Utilities;

    /// <summary>
    /// BaseWorkFlowCurrentEntity
    /// 工作流当前审核状态
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
	public partial class BaseWorkFlowCurrentEntity : BaseEntity
    {
        private String id = string.Empty;
        /// <summary>
        /// 代码
        /// </summary>
        [DataMember]
        public String Id
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

        private String categoryCode = string.Empty;
        /// <summary>
        /// 实体分类主键
        /// </summary>
        [DataMember]
        public String CategoryCode
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

        private String categoryFullName = string.Empty;
        /// <summary>
        /// 实体分类名称
        /// </summary>
        [DataMember]
        public String CategoryFullName
        {
            get
            {
                return this.categoryFullName;
            }
            set
            {
                this.categoryFullName = value;
            }
        }

        private String objectId = string.Empty;
        /// <summary>
        /// 实体主键
        /// </summary>
        [DataMember]
        public String ObjectId
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

        private String objectFullName = string.Empty;
        /// <summary>
        /// 实体名称
        /// </summary>
        [DataMember]
        public String ObjectFullName
        {
            get
            {
                return this.objectFullName;
            }
            set
            {
                this.objectFullName = value;
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

        private string activityCode = null;
        /// <summary>
        /// 审核步骤编号
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
            Id = BaseBusinessLogic.ConvertToString(dr[BaseWorkFlowCurrentEntity.FieldId]);
            CategoryCode = BaseBusinessLogic.ConvertToString(dr[BaseWorkFlowCurrentEntity.FieldCategoryCode]);
            CategoryFullName = BaseBusinessLogic.ConvertToString(dr[BaseWorkFlowCurrentEntity.FieldCategoryFullName]);
            ObjectId = BaseBusinessLogic.ConvertToString(dr[BaseWorkFlowCurrentEntity.FieldObjectId]);
            ObjectFullName = BaseBusinessLogic.ConvertToString(dr[BaseWorkFlowCurrentEntity.FieldObjectFullName]);
            ProcessId = BaseBusinessLogic.ConvertToInt(dr[BaseWorkFlowCurrentEntity.FieldProcessId]);
            ActivityId = BaseBusinessLogic.ConvertToInt(dr[BaseWorkFlowCurrentEntity.FieldActivityId]);
            ActivityCode = BaseBusinessLogic.ConvertToString(dr[BaseWorkFlowCurrentEntity.FieldActivityCode]);
            ActivityFullName = BaseBusinessLogic.ConvertToString(dr[BaseWorkFlowCurrentEntity.FieldActivityFullName]);
            ActivityType = BaseBusinessLogic.ConvertToString(dr[BaseWorkFlowActivityEntity.FieldActivityType]);
            ToDepartmentId = BaseBusinessLogic.ConvertToString(dr[BaseWorkFlowCurrentEntity.FieldToDepartmentId]);
            ToDepartmentName = BaseBusinessLogic.ConvertToString(dr[BaseWorkFlowCurrentEntity.FieldToDepartmentName]);
            ToUserId = BaseBusinessLogic.ConvertToString(dr[BaseWorkFlowCurrentEntity.FieldToUserId]);
            ToUserRealName = BaseBusinessLogic.ConvertToString(dr[BaseWorkFlowCurrentEntity.FieldToUserRealName]);
            ToRoleId = BaseBusinessLogic.ConvertToString(dr[BaseWorkFlowCurrentEntity.FieldToRoleId]);
            ToRoleRealName = BaseBusinessLogic.ConvertToString(dr[BaseWorkFlowCurrentEntity.FieldToRoleRealName]);
            AuditUserId = BaseBusinessLogic.ConvertToString(dr[BaseWorkFlowCurrentEntity.FieldAuditUserId]);
            AuditUserCode = BaseBusinessLogic.ConvertToString(dr[BaseWorkFlowCurrentEntity.FieldAuditUserCode]);
            AuditUserRealName = BaseBusinessLogic.ConvertToString(dr[BaseWorkFlowCurrentEntity.FieldAuditUserRealName]);
			SendDate = BaseBusinessLogic.ConvertToNullableDateTime(dr[BaseWorkFlowCurrentEntity.FieldSendDate]);
			AuditDate = BaseBusinessLogic.ConvertToNullableDateTime(dr[BaseWorkFlowCurrentEntity.FieldAuditDate]);
            AuditIdea = BaseBusinessLogic.ConvertToString(dr[BaseWorkFlowCurrentEntity.FieldAuditIdea]);
            AuditStatus = BaseBusinessLogic.ConvertToString(dr[BaseWorkFlowCurrentEntity.FieldAuditStatus]);
            AuditStatusName = BaseBusinessLogic.ConvertToString(dr[BaseWorkFlowCurrentEntity.FieldAuditStatusName]);
            SortCode = BaseBusinessLogic.ConvertToInt(dr[BaseWorkFlowCurrentEntity.FieldSortCode]);
            Enabled = BaseBusinessLogic.ConvertToInt(dr[BaseWorkFlowCurrentEntity.FieldEnabled]);
            DeletionStateCode = BaseBusinessLogic.ConvertToInt(dr[BaseWorkFlowCurrentEntity.FieldDeletionStateCode]);
            Description = BaseBusinessLogic.ConvertToString(dr[BaseWorkFlowCurrentEntity.FieldDescription]);
            CreateOn = BaseBusinessLogic.ConvertToNullableDateTime(dr[BaseWorkFlowCurrentEntity.FieldCreateOn]);
            CreateUserId = BaseBusinessLogic.ConvertToString(dr[BaseWorkFlowCurrentEntity.FieldCreateUserId]);
            CreateBy = BaseBusinessLogic.ConvertToString(dr[BaseWorkFlowCurrentEntity.FieldCreateBy]);
            ModifiedOn = BaseBusinessLogic.ConvertToNullableDateTime(dr[BaseWorkFlowCurrentEntity.FieldModifiedOn]);
            ModifiedUserId = BaseBusinessLogic.ConvertToString(dr[BaseWorkFlowCurrentEntity.FieldModifiedUserId]);
            ModifiedBy = BaseBusinessLogic.ConvertToString(dr[BaseWorkFlowCurrentEntity.FieldModifiedBy]);
            // 获取扩展属性
            GetFromExpand(dr);
            return this;
        }

        ///<summary>
        /// 工作流当前审核状态
        ///</summary>
        [NonSerialized]
        public static string TableName = "BaseWorkFlowCurrent";

        ///<summary>
        /// 代码
        ///</summary>
        [NonSerialized]
        public static string FieldId = "Id";

        ///<summary>
        /// 实体分类主键
        ///</summary>
        [NonSerialized]
        public static string FieldCategoryCode = "CategoryCode";

        ///<summary>
        /// 实体分类名称
        ///</summary>
        [NonSerialized]
        public static string FieldCategoryFullName = "CategoryFullName";

        ///<summary>
        /// 实体主键
        ///</summary>
        [NonSerialized]
        public static string FieldObjectId = "ObjectId";

        ///<summary>
        /// 实体名称
        ///</summary>
        [NonSerialized]
        public static string FieldObjectFullName = "ObjectFullName";

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
        /// 节点审核类型
        ///</summary>
        [NonSerialized]
        public static string FieldActivityType = "ActivityType";

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
