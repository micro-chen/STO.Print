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
    /// BaseUserOrganizeEntity
    /// 用户组织关系（兼任）表
    /// 
    /// 修改记录
    /// 
    /// 2012-07-27 版本：1.0 JiRiGaLa 创建主键。
    /// 
    /// <author>
    ///     <name>JiRiGaLa</name>
    ///     <date>2012-07-27</date>
    /// </author>
    /// </summary>
    [Serializable, DataContract]
	public partial class BaseUserOrganizeEntity : BaseEntity
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

        private string userId = null;
        /// <summary>
        /// 用户账户主键
        /// </summary>
        [DataMember]
        public string UserId
        {
            get
            {
                return this.userId;
            }
            set
            {
                this.userId = value;
            }
        }

        private String companyId = string.Empty;
        /// <summary>
        /// 公司主键，数据库中可以设置为int
        /// </summary>
        [DataMember]
        public String CompanyId
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

        private String subCompanyId = string.Empty;
        /// <summary>
        /// 分支机构主键，数据库中可以设置为int
        /// </summary>
        [DataMember]
        public String SubCompanyId
        {
            get
            {
                return this.subCompanyId;
            }
            set
            {
                this.subCompanyId = value;
            }
        }

        private String departmentId = string.Empty;
        /// <summary>
        /// 部门主键，数据库中可以设置为int
        /// </summary>
        [DataMember]
        public String DepartmentId
        {
            get
            {
                return this.departmentId;
            }
            set
            {
                this.departmentId = value;
            }
        }

        private String workgroupId = string.Empty;
        /// <summary>
        /// 工作组主键，数据库中可以设置为int
        /// </summary>
        [DataMember]
        public String WorkgroupId
        {
            get
            {
                return this.workgroupId;
            }
            set
            {
                this.workgroupId = value;
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
            Id = BaseBusinessLogic.ConvertToInt(dr[BaseUserOrganizeEntity.FieldId]);
            UserId = BaseBusinessLogic.ConvertToString(dr[BaseUserOrganizeEntity.FieldUserId]);
            CompanyId = BaseBusinessLogic.ConvertToString(dr[BaseUserOrganizeEntity.FieldCompanyId]);
            SubCompanyId = BaseBusinessLogic.ConvertToString(dr[BaseUserOrganizeEntity.FieldSubCompanyId]);
            DepartmentId = BaseBusinessLogic.ConvertToString(dr[BaseUserOrganizeEntity.FieldDepartmentId]);
            WorkgroupId = BaseBusinessLogic.ConvertToString(dr[BaseUserOrganizeEntity.FieldWorkgroupId]);
            Enabled = BaseBusinessLogic.ConvertToInt(dr[BaseUserOrganizeEntity.FieldEnabled]);
            Description = BaseBusinessLogic.ConvertToString(dr[BaseUserOrganizeEntity.FieldDescription]);
            DeletionStateCode = BaseBusinessLogic.ConvertToInt(dr[BaseUserOrganizeEntity.FieldDeletionStateCode]);
            CreateOn = BaseBusinessLogic.ConvertToNullableDateTime(dr[BaseUserOrganizeEntity.FieldCreateOn]);
            CreateUserId = BaseBusinessLogic.ConvertToString(dr[BaseUserOrganizeEntity.FieldCreateUserId]);
            CreateBy = BaseBusinessLogic.ConvertToString(dr[BaseUserOrganizeEntity.FieldCreateBy]);
            ModifiedOn = BaseBusinessLogic.ConvertToNullableDateTime(dr[BaseUserOrganizeEntity.FieldModifiedOn]);
            ModifiedUserId = BaseBusinessLogic.ConvertToString(dr[BaseUserOrganizeEntity.FieldModifiedUserId]);
            ModifiedBy = BaseBusinessLogic.ConvertToString(dr[BaseUserOrganizeEntity.FieldModifiedBy]);
            // 获取扩展属性
            GetFromExpand(dr);
            return this;
        }

        ///<summary>
        /// 用户组织关系表
        ///</summary>
        [NonSerialized]
        public static string TableName = "BaseUserOrganize";

        ///<summary>
        /// 主键
        ///</summary>
        [NonSerialized]
        public static string FieldId = "Id";

        ///<summary>
        /// 用户账户主键
        ///</summary>
        [NonSerialized]
        public static string FieldUserId = "UserId";

        ///<summary>
        /// 公司主键，数据库中可以设置为int
        ///</summary>
        [NonSerialized]
        public static string FieldCompanyId = "CompanyId";

        ///<summary>
        /// 分支机构主键，数据库中可以设置为int
        ///</summary>
        [NonSerialized]
        public static string FieldSubCompanyId = "SubCompanyId";

        ///<summary>
        /// 部门代码，数据库中可以设置为int
        ///</summary>
        [NonSerialized]
        public static string FieldDepartmentId = "DepartmentId";

        ///<summary>
        /// 工作组主键，数据库中可以设置为int
        ///</summary>
        [NonSerialized]
        public static string FieldWorkgroupId = "WorkgroupId";

        ///<summary>
        /// 有效
        ///</summary>
        [NonSerialized]
        public static string FieldEnabled = "Enabled";

        ///<summary>
        /// 备注
        ///</summary>
        [NonSerialized]
        public static string FieldDescription = "Description";

        ///<summary>
        /// 删除标记
        ///</summary>
        [NonSerialized]
        public static string FieldDeletionStateCode = "DeletionStateCode";

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
