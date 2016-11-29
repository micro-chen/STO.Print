//-----------------------------------------------------------------------
// <copyright file="organizeScopeEntity.cs" company="Hairihan">
//     Copyright (C) 2016 , All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Runtime.Serialization;

namespace DotNet.Model
{
    using DotNet.Utilities;

    /// <summary>
    /// organizeScopeEntity
    /// 基于组织机构的权限范围
    /// 
    /// 修改记录
    /// 
    /// 2013-12-24 版本：1.0 JiRiGaLa 创建文件。
    /// 
    /// <author>
    ///     <name>JiRiGaLa</name>
    ///     <date>2013-12-24</date>
    /// </author>
    /// </summary>
    [Serializable, DataContract]
    public partial class BaseOrganizeScopeEntity : BaseEntity
    {
        private int id;
        /// <summary>
        /// 主键
        /// </summary>
        [DataMember]
        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }

        private string resourceCategory = string.Empty;
        /// <summary>
        /// 什么类型的
        /// </summary>
        [DataMember]
        public string ResourceCategory
        {
            get
            {
                return resourceCategory;
            }
            set
            {
                resourceCategory = value;
            }
        }

        private string resourceId = string.Empty;
        /// <summary>
        /// 什么资源主键
        /// </summary>
        [DataMember]
        public string ResourceId
        {
            get
            {
                return resourceId;
            }
            set
            {
                resourceId = value;
            }
        }

        private int? permissionId = null;
        /// <summary>
        /// 有什么权限（模块菜单）主键
        /// </summary>
        [DataMember]
        public int? PermissionId
        {
            get
            {
                return permissionId;
            }
            set
            {
                permissionId = value;
            }
        }

        private int? allData = null;
        /// <summary>
        /// 全部数据
        /// </summary>
        [DataMember]
        public int? AllData
        {
            get
            {
                return allData;
            }
            set
            {
                allData = value;
            }
        }

        private int? province = 0;
        /// <summary>
        /// 所在省
        /// </summary>
        [DataMember]
        public int? Province
        {
            get
            {
                return province;
            }
            set
            {
                province = value;
            }
        }

        private int? city = 0;
        /// <summary>
        /// 所在市
        /// </summary>
        [DataMember]
        public int? City
        {
            get
            {
                return city;
            }
            set
            {
                city = value;
            }
        }

        private int? district = 0;
        /// <summary>
        /// 所在区/县
        /// </summary>
        [DataMember]
        public int? District
        {
            get
            {
                return district;
            }
            set
            {
                district = value;
            }
        }

        private int? street = 0;
        /// <summary>
        /// 所在街道
        /// </summary>
        [DataMember]
        public int? Street
        {
            get
            {
                return street;
            }
            set
            {
                street = value;
            }
        }

        private int? userCompany = 0;
        /// <summary>
        /// 用户所在公司的数据
        /// </summary>
        [DataMember]
        public int? UserCompany
        {
            get
            {
                return userCompany;
            }
            set
            {
                userCompany = value;
            }
        }

        private int? userSubCompany = 0;
        /// <summary>
        /// 用户所在分公司的数据
        /// </summary>
        [DataMember]
        public int? UserSubCompany
        {
            get
            {
                return userSubCompany;
            }
            set
            {
                userSubCompany = value;
            }
        }

        private int? userDepartment = 0;
        /// <summary>
        /// 用户所在部门的数据
        /// </summary>
        [DataMember]
        public int? UserDepartment
        {
            get
            {
                return userDepartment;
            }
            set
            {
                userDepartment = value;
            }
        }

        private int? userSubDepartment = 0;
        /// <summary>
        /// 用户所在子部门的数据
        /// </summary>
        [DataMember]
        public int? UserSubDepartment
        {
            get
            {
                return userSubDepartment;
            }
            set
            {
                userSubDepartment = value;
            }
        }

        private int? userWorkgroup = 0;
        /// <summary>
        /// 用户所在工作组的数据
        /// </summary>
        [DataMember]
        public int? UserWorkgroup
        {
            get
            {
                return userWorkgroup;
            }
            set
            {
                userWorkgroup = value;
            }
        }

        private int onlyOwnData = 1;
        /// <summary>
        /// 仅仅用户自己的数据
        /// </summary>
        [DataMember]
        public int OnlyOwnData
        {
            get
            {
                return onlyOwnData;
            }
            set
            {
                onlyOwnData = value;
            }
        }

        private int? notAllowed = 0;
        /// <summary>
        /// 不允许查看数据
        /// </summary>
        [DataMember]
        public int? NotAllowed
        {
            get
            {
                return notAllowed;
            }
            set
            {
                notAllowed = value;
            }
        }

        private int? byDetails = 0;
        /// <summary>
        /// 按明细设置
        /// </summary>
        [DataMember]
        public int? ByDetails
        {
            get
            {
                return byDetails;
            }
            set
            {
                byDetails = value;
            }
        }

        private int containChild = 0;
        /// <summary>
        /// 包含子节点的数据
        /// </summary>
        [DataMember]
        public int ContainChild
        {
            get
            {
                return containChild;
            }
            set
            {
                containChild = value;
            }
        }

        private int enabled = 1;
        /// <summary>
        /// 有效标示
        /// </summary>
        [DataMember]
        public int Enabled
        {
            get
            {
                return enabled;
            }
            set
            {
                enabled = value;
            }
        }

        private int deletionStateCode = 0;
        /// <summary>
        /// 删除标记
        /// </summary>
        [DataMember]
        public int DeletionStateCode
        {
            get
            {
                return deletionStateCode;
            }
            set
            {
                deletionStateCode = value;
            }
        }

        private string description = string.Empty;
        /// <summary>
        /// 备注
        /// </summary>
        [DataMember]
        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                description = value;
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
                return createOn;
            }
            set
            {
                createOn = value;
            }
        }

        private string createUserId = string.Empty;
        /// <summary>
        /// 创建用户主键
        /// </summary>
        [DataMember]
        public string CreateUserId
        {
            get
            {
                return createUserId;
            }
            set
            {
                createUserId = value;
            }
        }

        private string createBy = string.Empty;
        /// <summary>
        /// 创建用户
        /// </summary>
        [DataMember]
        public string CreateBy
        {
            get
            {
                return createBy;
            }
            set
            {
                createBy = value;
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
                return modifiedOn;
            }
            set
            {
                modifiedOn = value;
            }
        }

        private string modifiedUserId = string.Empty;
        /// <summary>
        /// 修改用户主键
        /// </summary>
        [DataMember]
        public string ModifiedUserId
        {
            get
            {
                return modifiedUserId;
            }
            set
            {
                modifiedUserId = value;
            }
        }

        private string modifiedBy = string.Empty;
        /// <summary>
        /// 修改用户
        /// </summary>
        [DataMember]
        public string ModifiedBy
        {
            get
            {
                return modifiedBy;
            }
            set
            {
                modifiedBy = value;
            }
        }

        /// <summary>
        /// 从数据行读取
        /// </summary>
        /// <param name="dr">数据行</param>
        protected override BaseEntity GetFrom(IDataRow dr)
        {
            GetFromExpand(dr);
            Id = BaseBusinessLogic.ConvertToInt(dr[BaseOrganizeScopeEntity.FieldId]);
            ResourceCategory = BaseBusinessLogic.ConvertToString(dr[BaseOrganizeScopeEntity.FieldResourceCategory]);
            ResourceId = BaseBusinessLogic.ConvertToString(dr[BaseOrganizeScopeEntity.FieldResourceId]);
            PermissionId = BaseBusinessLogic.ConvertToNullableInt(dr[BaseOrganizeScopeEntity.FieldPermissionId]);
            AllData = BaseBusinessLogic.ConvertToNullableInt(dr[BaseOrganizeScopeEntity.FieldAllData]);
            Province = BaseBusinessLogic.ConvertToNullableInt(dr[BaseOrganizeScopeEntity.FieldProvince]);
            City = BaseBusinessLogic.ConvertToNullableInt(dr[BaseOrganizeScopeEntity.FieldCity]);
            District = BaseBusinessLogic.ConvertToNullableInt(dr[BaseOrganizeScopeEntity.FieldDistrict]);
            Street = BaseBusinessLogic.ConvertToNullableInt(dr[BaseOrganizeScopeEntity.FieldStreet]);
            UserCompany = BaseBusinessLogic.ConvertToNullableInt(dr[BaseOrganizeScopeEntity.FieldUserCompany]);
            UserSubCompany = BaseBusinessLogic.ConvertToNullableInt(dr[BaseOrganizeScopeEntity.FieldUserSubCompany]);
            UserDepartment = BaseBusinessLogic.ConvertToNullableInt(dr[BaseOrganizeScopeEntity.FieldUserDepartment]);
            UserSubDepartment = BaseBusinessLogic.ConvertToNullableInt(dr[BaseOrganizeScopeEntity.FieldUserSubDepartment]);
            UserWorkgroup = BaseBusinessLogic.ConvertToNullableInt(dr[BaseOrganizeScopeEntity.FieldUserWorkgroup]);
            OnlyOwnData = BaseBusinessLogic.ConvertToInt(dr[BaseOrganizeScopeEntity.FieldOnlyOwnData]);
            NotAllowed = BaseBusinessLogic.ConvertToNullableInt(dr[BaseOrganizeScopeEntity.FieldNotAllowed]);
            ByDetails = BaseBusinessLogic.ConvertToNullableInt(dr[BaseOrganizeScopeEntity.FieldByDetails]);
            ContainChild = BaseBusinessLogic.ConvertToInt(dr[BaseOrganizeScopeEntity.FieldContainChild]);
            Enabled = BaseBusinessLogic.ConvertToInt(dr[BaseOrganizeScopeEntity.FieldEnabled]);
            DeletionStateCode = BaseBusinessLogic.ConvertToInt(dr[BaseOrganizeScopeEntity.FieldDeletionStateCode]);
            Description = BaseBusinessLogic.ConvertToString(dr[BaseOrganizeScopeEntity.FieldDescription]);
            CreateOn = BaseBusinessLogic.ConvertToNullableDateTime(dr[BaseOrganizeScopeEntity.FieldCreateOn]);
            CreateUserId = BaseBusinessLogic.ConvertToString(dr[BaseOrganizeScopeEntity.FieldCreateUserId]);
            CreateBy = BaseBusinessLogic.ConvertToString(dr[BaseOrganizeScopeEntity.FieldCreateBy]);
            ModifiedOn = BaseBusinessLogic.ConvertToNullableDateTime(dr[BaseOrganizeScopeEntity.FieldModifiedOn]);
            ModifiedUserId = BaseBusinessLogic.ConvertToString(dr[BaseOrganizeScopeEntity.FieldModifiedUserId]);
            ModifiedBy = BaseBusinessLogic.ConvertToString(dr[BaseOrganizeScopeEntity.FieldModifiedBy]);
            return this;
        }

        ///<summary>
        /// 基于组织机构的权限范围
        ///</summary>
        [NonSerialized]
        public static string TableName = "BaseOrganizeScope";

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
        /// 什么资源主键
        ///</summary>
        [NonSerialized]
        public static string FieldResourceId = "ResourceId";

        ///<summary>
        /// 有什么权限（模块菜单）主键
        ///</summary>
        [NonSerialized]
        public static string FieldPermissionId = "PermissionId";

        ///<summary>
        /// 全部数据
        ///</summary>
        [NonSerialized]
        public static string FieldAllData = "AllData";

        /// <summary>
        /// 所在省
        /// </summary>
        [NonSerialized]
        public static string FieldProvince = "Province";
        
        /// <summary>
        /// 所在市
        /// </summary>
        [NonSerialized]
        public static string FieldCity = "City";
        
        /// <summary>
        /// 所在区/县
        /// </summary>
        [NonSerialized]
        public static string FieldDistrict = "District";

        /// <summary>
        /// 所在街道
        /// </summary>
        [NonSerialized]
        public static string FieldStreet = "Street";
        
        ///<summary>
        /// 用户所在公司的数据
        ///</summary>
        [NonSerialized]
        public static string FieldUserCompany = "UserCompany";

        ///<summary>
        /// 用户所在分公司的数据
        ///</summary>
        [NonSerialized]
        public static string FieldUserSubCompany = "UserSubCompany";

        ///<summary>
        /// 用户所在部门的数据
        ///</summary>
        [NonSerialized]
        public static string FieldUserDepartment = "UserDepartment";

        ///<summary>
        /// 用户所在子部门的数据
        ///</summary>
        [NonSerialized]
        public static string FieldUserSubDepartment = "UserSubDepartment";

        ///<summary>
        /// 用户所在工作组的数据
        ///</summary>
        [NonSerialized]
        public static string FieldUserWorkgroup = "UserWorkgroup";

        ///<summary>
        /// 仅仅用户自己的数据
        ///</summary>
        [NonSerialized]
        public static string FieldOnlyOwnData = "OnlyOwnData";

        ///<summary>
        /// 不允许查看数据
        ///</summary>
        [NonSerialized]
        public static string FieldNotAllowed = "NotAllowed";

        ///<summary>
        /// 
        ///</summary>
        [NonSerialized]
        public static string FieldByDetails = "ByDetails";

        ///<summary>
        /// 包含子节点的数据
        ///</summary>
        [NonSerialized]
        public static string FieldContainChild = "ContainChild";

        ///<summary>
        /// 有效标示
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
