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
    /// BasePermissionScopeEntity
    /// 数据权限表
    ///
    /// 修改记录
    ///
    ///		2011-03-07 版本：1.1 JiRiGaLa 改名。
    ///		2010-07-15 版本：1.0 JiRiGaLa 创建主键。
    ///
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2010-07-15</date>
    /// </author>
    /// </summary>
    [Serializable, DataContract]
	public partial class BasePermissionScopeEntity : BaseEntity
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

        private string resourceCategory = null;
        /// <summary>
        /// 什么类型的
        /// </summary>
        [DataMember]
        public string ResourceCategory
        {
            get
            {
                return this.resourceCategory;
            }
            set
            {
                this.resourceCategory = value;
            }
        }

        private string resourceId = null;
        /// <summary>
        /// 什么资源主键
        /// </summary>
        [DataMember]
        public string ResourceId
        {
            get
            {
                return this.resourceId;
            }
            set
            {
                this.resourceId = value;
            }
        }

        private string targetCategory = null;
        /// <summary>
        /// 对什么类型的
        /// </summary>
        [DataMember]
        public string TargetCategory
        {
            get
            {
                return this.targetCategory;
            }
            set
            {
                this.targetCategory = value;
            }
        }

        private string targetId = null;
        /// <summary>
        /// 对什么资源主键
        /// </summary>
        [DataMember]
        public string TargetId
        {
            get
            {
                return this.targetId;
            }
            set
            {
                this.targetId = value;
            }
        }

        private string permissionId = null;
        /// <summary>
        /// 有什么权限（模块菜单）主键
        /// </summary>
        [DataMember]
        public string PermissionId
        {
            get
            {
                return this.permissionId;
            }
            set
            {
                this.permissionId = value;
            }
        }

        private int containChild = 0;
        /// <summary>
        /// 包含子节点
        /// </summary>
        [DataMember]
        public int ContainChild
        {
            get
            {
                return this.containChild;
            }
            set
            {
                this.containChild = value;
            }
        }

        private string permissionConstraint = null;
        /// <summary>
        /// 有什么权限约束表达式
        /// </summary>
        [DataMember]
        public string PermissionConstraint
        {
            get
            {
                return this.permissionConstraint;
            }
            set
            {
                this.permissionConstraint = value;
            }
        }


        private DateTime? startDate = null;
        /// <summary>
        /// 开始生效日期
        /// </summary>
        [DataMember]
        public DateTime? StartDate
        {
            get
            {
                return this.startDate;
            }
            set
            {
                this.startDate = value;
            }
        }

        private DateTime? endDate = null;
        /// <summary>
        /// 结束生效日期
        /// </summary>
        [DataMember]
        public DateTime? EndDate
        {
            get
            {
                return this.endDate;
            }
            set
            {
                this.endDate = value;
            }
        }


        private int? enabled = 1;
        /// <summary>
        /// 有效标志
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
        /// 删除标记
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

        /// <summary>
        /// 从数据行读取
        /// </summary>
        /// <param name="dr">数据行</param>
		protected override BaseEntity GetFrom(IDataRow dr)
        {
            Id = BaseBusinessLogic.ConvertToInt(dr[BasePermissionScopeEntity.FieldId]);
            ResourceCategory = BaseBusinessLogic.ConvertToString(dr[BasePermissionScopeEntity.FieldResourceCategory]);
            ResourceId = BaseBusinessLogic.ConvertToString(dr[BasePermissionScopeEntity.FieldResourceId]);
            TargetCategory = BaseBusinessLogic.ConvertToString(dr[BasePermissionScopeEntity.FieldTargetCategory]);
            TargetId = BaseBusinessLogic.ConvertToString(dr[BasePermissionScopeEntity.FieldTargetId]);
            PermissionId = BaseBusinessLogic.ConvertToString(dr[BasePermissionScopeEntity.FieldPermissionId]);
            ContainChild = BaseBusinessLogic.ConvertToInt(dr[BasePermissionScopeEntity.FieldContainChild]);
            PermissionConstraint = BaseBusinessLogic.ConvertToString(dr[BasePermissionScopeEntity.FieldPermissionConstraint]);
            Enabled = BaseBusinessLogic.ConvertToInt(dr[BasePermissionScopeEntity.FieldEnabled]);
            DeletionStateCode = BaseBusinessLogic.ConvertToInt(dr[BasePermissionScopeEntity.FieldDeletionStateCode]);
            Description = BaseBusinessLogic.ConvertToString(dr[BasePermissionScopeEntity.FieldDescription]);
            CreateOn = BaseBusinessLogic.ConvertToNullableDateTime(dr[BasePermissionScopeEntity.FieldCreateOn]);
            CreateBy = BaseBusinessLogic.ConvertToString(dr[BasePermissionScopeEntity.FieldCreateBy]);
            CreateUserId = BaseBusinessLogic.ConvertToString(dr[BasePermissionScopeEntity.FieldCreateUserId]);
            ModifiedOn = BaseBusinessLogic.ConvertToNullableDateTime(dr[BasePermissionScopeEntity.FieldModifiedOn]);
            ModifiedBy = BaseBusinessLogic.ConvertToString(dr[BasePermissionScopeEntity.FieldModifiedBy]);
            ModifiedUserId = BaseBusinessLogic.ConvertToString(dr[BasePermissionScopeEntity.FieldModifiedUserId]);
            // 获取扩展属性
            GetFromExpand(dr);
            return this;
        }

        ///<summary>
        /// 数据权限表
        ///</summary>
        [NonSerialized]
        public static string TableName = "BasePermissionScope";

        ///<summary>
        /// 主键
        ///</summary>
        [NonSerialized]
        public static string FieldId = "Id";

        ///<summary>
        /// 什么类型的
        ///</summary>
        [NonSerialized]
        public static string FieldResourceCategory = "ResourceCategory";

        ///<summary>
        /// 什么资源
        ///</summary>
        [NonSerialized]
        public static string FieldResourceId = "ResourceId";

        ///<summary>
        /// 对什么类型的
        ///</summary>
        [NonSerialized]
        public static string FieldTargetCategory = "TargetCategory";

        ///<summary>
        /// 对什么资源
        ///</summary>
        [NonSerialized]
        public static string FieldTargetId = "TargetId";

        ///<summary>
        /// 有什么权限
        ///</summary>
        [NonSerialized]
        public static string FieldPermissionId = "PermissionId";

        /// <summary>
        /// 包含子节点
        /// </summary>
        public static string FieldContainChild = "ContainChild";
       
        ///<summary>
        /// 权限约束
        ///</summary>
        [NonSerialized]
        public static string FieldPermissionConstraint = "PermissionConstraint";

        ///<summary>
        /// 开始生效日期
        ///</summary>
        [NonSerialized]
        public static string FieldStartDate = "StartDate";

        ///<summary>
        /// 结束生效日期
        ///</summary>
        [NonSerialized]
        public static string FieldEndDate = "EndDate";

        ///<summary>
        /// 有效
        ///</summary>
        [NonSerialized]
        public static string FieldEnabled = "Enabled";

        ///<summary>
        /// 删除标记
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
        /// 创建用户
        ///</summary>
        [NonSerialized]
        public static string FieldCreateBy = "CreateBy";

        ///<summary>
        /// 创建用户主键
        ///</summary>
        [NonSerialized]
        public static string FieldCreateUserId = "CreateUserId";

        ///<summary>
        /// 修改日期
        ///</summary>
        [NonSerialized]
        public static string FieldModifiedOn = "ModifiedOn";

        ///<summary>
        /// 修改用户
        ///</summary>
        [NonSerialized]
        public static string FieldModifiedBy = "ModifiedBy";

        ///<summary>
        /// 修改用户主键
        ///</summary>
        [NonSerialized]
        public static string FieldModifiedUserId = "ModifiedUserId";
    }
}
