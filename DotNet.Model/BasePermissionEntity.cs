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
    /// BasePermissionEntity
    /// 操作权限表
    ///
    /// 修改记录
    ///
    ///		2011-03-07 版本：1.1 JiRiGaLa 表名修正。
    ///		2010-07-15 版本：1.0 JiRiGaLa 创建主键。
    ///
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2010-07-15</date>
    /// </author>
    /// </summary>
    [Serializable, DataContract]
	public partial class BasePermissionEntity : BaseEntity
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

        private string resourceId = null;
        /// <summary>
        /// 资源主键
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

        private string resourceCategory = null;
        /// <summary>
        /// 资料类别
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

        private string permissionId = null;
        /// <summary>
        /// 权限（菜单模块）主键
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

        private string companyId = null;
        /// <summary>
        /// 公司主键
        /// </summary>
        [DataMember]
        public string CompanyId
        {
            get
            {
                return this.companyId;
            }
            set
            {
                this.companyId = value;
            }
        }

        private string companyName = string.Empty;
        /// <summary>
        /// 公司名称
        /// </summary>
        [DataMember]
        public string CompanyName
        {
            get
            {
                return this.companyName;
            }
            set
            {
                this.companyName = value;
            }
        }

        private string permissionConstraint = null;
        /// <summary>
        /// 权限条件限制
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
            Id = BaseBusinessLogic.ConvertToInt(dr[BasePermissionEntity.FieldId]);
            ResourceId = BaseBusinessLogic.ConvertToString(dr[BasePermissionEntity.FieldResourceId]);
            ResourceCategory = BaseBusinessLogic.ConvertToString(dr[BasePermissionEntity.FieldResourceCategory]);
            PermissionId = BaseBusinessLogic.ConvertToString(dr[BasePermissionEntity.FieldPermissionId]);
            PermissionConstraint = BaseBusinessLogic.ConvertToString(dr[BasePermissionEntity.FieldPermissionConstraint]);
            CompanyId = BaseBusinessLogic.ConvertToString(dr[BasePermissionEntity.FieldCompanyId]);
            CompanyName = BaseBusinessLogic.ConvertToString(dr[BasePermissionEntity.FieldCompanyName]);
            Enabled = BaseBusinessLogic.ConvertToInt(dr[BasePermissionEntity.FieldEnabled]);
            DeletionStateCode = BaseBusinessLogic.ConvertToInt(dr[BasePermissionEntity.FieldDeletionStateCode]);
            Description = BaseBusinessLogic.ConvertToString(dr[BasePermissionEntity.FieldDescription]);
            CreateOn = BaseBusinessLogic.ConvertToNullableDateTime(dr[BasePermissionEntity.FieldCreateOn]);
            CreateBy = BaseBusinessLogic.ConvertToString(dr[BasePermissionEntity.FieldCreateBy]);
            CreateUserId = BaseBusinessLogic.ConvertToString(dr[BasePermissionEntity.FieldCreateUserId]);
            ModifiedOn = BaseBusinessLogic.ConvertToNullableDateTime(dr[BasePermissionEntity.FieldModifiedOn]);
            ModifiedBy = BaseBusinessLogic.ConvertToString(dr[BasePermissionEntity.FieldModifiedBy]);
            ModifiedUserId = BaseBusinessLogic.ConvertToString(dr[BasePermissionEntity.FieldModifiedUserId]);
            // 获取扩展属性
            GetFromExpand(dr);
            return this;
        }

        ///<summary>
        /// 操作权限表
        ///</summary>
        [NonSerialized]
        public static string TableName = "BasePermission";

        ///<summary>
        /// 主键
        ///</summary>
        [NonSerialized]
        public static string FieldId = "Id";

        ///<summary>
        /// 资源主键
        ///</summary>
        [NonSerialized]
        public static string FieldResourceId = "ResourceId";

        ///<summary>
        /// 资料类别
        ///</summary>
        [NonSerialized]
        public static string FieldResourceCategory = "ResourceCategory";

        ///<summary>
        /// 权限主键
        ///</summary>
        [NonSerialized]
        public static string FieldPermissionId = "PermissionId";

        ///<summary>
        /// 权限条件限制
        ///</summary>
        [NonSerialized]
        public static string FieldPermissionConstraint = "PermissionConstraint";

        ///<summary>
        /// 公司主键
        ///</summary>
        [NonSerialized]
        public static string FieldCompanyId = "CompanyId";

        ///<summary>
        /// 公司名称
        ///</summary>
        [NonSerialized]
        public static string FieldCompanyName = "CompanyName";

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
