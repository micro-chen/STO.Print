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
    /// BaseDepartmentEntity
    /// 部门表
    ///
    /// 修改记录
    ///
    ///		2014-12-08 版本：1.0 JiRiGaLa 创建主键。
    ///
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2014-12-08</date>
    /// </author>
    /// </summary>
    [Serializable, DataContract]
    public partial class BaseDepartmentEntity : BaseEntity
    {
        private int? id = null;
        /// <summary>
        /// 主键
        /// </summary>
        [FieldDescription("主键", false)]
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

        private string parentId = null;
        /// <summary>
        /// 父级主键
        /// </summary>
        [FieldDescription("父级主键")]
        [DataMember]
        public string ParentId
        {
            get
            {
                return this.parentId;
            }
            set
            {
                this.parentId = value;
            }
        }

        private string code = null;
        /// <summary>
        /// 编号
        /// </summary>
        [FieldDescription("编号")]
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
        /// 名称
        /// </summary>
        [FieldDescription("名称")]
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

        private string shortName = null;
        /// <summary>
        /// 简称
        /// </summary>
        [FieldDescription("简称")]
        [DataMember]
        public string ShortName
        {
            get
            {
                return this.shortName;
            }
            set
            {
                this.shortName = value;
            }
        }

        private string categoryCode = null;
        /// <summary>
        /// 分类编码
        /// </summary>
        [FieldDescription("分类编码")]
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

        private string outerPhone = null;
        /// <summary>
        /// 外线电话
        /// </summary>
        [FieldDescription("外线电话")]
        [DataMember]
        public string OuterPhone
        {
            get
            {
                return this.outerPhone;
            }
            set
            {
                this.outerPhone = value;
            }
        }

        private string innerPhone = null;
        /// <summary>
        /// 内线电话
        /// </summary>
        [FieldDescription("内线电话")]
        [DataMember]
        public string InnerPhone
        {
            get
            {
                return this.innerPhone;
            }
            set
            {
                this.innerPhone = value;
            }
        }

        private string fax = null;
        /// <summary>
        /// 传真
        /// </summary>
        [FieldDescription("传真")]
        [DataMember]
        public string Fax
        {
            get
            {
                return this.fax;
            }
            set
            {
                this.fax = value;
            }
        }

        private string manager = string.Empty;
        ///<summary>
        /// 业务负责人
        ///</summary>
        [FieldDescription("业务负责人")]
        [DataMember]
        public string Manager
        {
            get
            {
                return this.manager;
            }
            set
            {
                this.manager = value;
            }
        }

        private string managerMobile = string.Empty;
        ///<summary>
        /// 业务负责人手机
        ///</summary>
        [FieldDescription("业务负责人手机")]
        [DataMember]
        public string ManagerMobile
        {
            get
            {
                return this.managerMobile;
            }
            set
            {
                this.managerMobile = value;
            }
        }

        private string managerQQ = string.Empty;
        ///<summary>
        /// 业务负责人QQ
        ///</summary>
        [FieldDescription("业务负责人QQ")]
        [DataMember]
        public string ManagerQQ
        {
            get
            {
                return this.managerQQ;
            }
            set
            {
                this.managerQQ = value;
            }
        }

        private int? deletionStateCode = 0;
        /// <summary>
        /// 删除标记
        /// </summary>
        [FieldDescription("删除标记")]
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

        private int? enabled = 1;
        /// <summary>
        /// 有效
        /// </summary>
        [FieldDescription("有效")]
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

        private int? sortCode = 0;
        /// <summary>
        /// 排序码
        /// </summary>
        [FieldDescription("排序码")]
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

        private string description = null;
        /// <summary>
        /// 备注
        /// </summary>
        [FieldDescription("备注")]
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
        [FieldDescription("创建日期", false)]
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
        [FieldDescription("创建用户主键", false)]
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
        [FieldDescription("创建用户", false)]
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
        [FieldDescription("修改日期", false)]
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
        [FieldDescription("修改用户主键", false)]
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
        [FieldDescription("修改用户", false)]
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

        private string parentName = null;
        /// <summary>
        /// 父级名称
        /// </summary>
        [FieldDescription("父级名称")]
        [DataMember]
        public string ParentName
        {
            get
            {
                return this.parentName;
            }
            set
            {
                this.parentName = value;
            }
        }

        private string companyId = null;
        /// <summary>
        /// 网点主键
        /// </summary>
        [FieldDescription("网点主键")]
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

        private string companyName = null;
        /// <summary>
        /// 网点名称
        /// </summary>
        [FieldDescription("网点名称")]
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

        private string companyCode = null;
        /// <summary>
        /// 网点编号
        /// </summary>
        [FieldDescription("网点编号")]
        [DataMember]
        public string CompanyCode
        {
            get
            {
                return this.companyCode;
            }
            set
            {
                this.companyCode = value;
            }
        }

        private string categoryName = null;
        /// <summary>
        /// 分类名称
        /// </summary>
        [FieldDescription("分类名称")]
        [DataMember]
        public string CategoryName
        {
            get
            {
                return this.categoryName;
            }
            set
            {
                this.categoryName = value;
            }
        }

        private string parentCode = null;
        /// <summary>
        /// 父级编号
        /// </summary>
        [FieldDescription("父级编号")]
        [DataMember]
        public string ParentCode
        {
            get
            {
                return this.parentCode;
            }
            set
            {
                this.parentCode = value;
            }
        }

        private string managerId = null;
        /// <summary>
        /// 主管主键
        /// </summary>
        [FieldDescription("主管主键")]
        [DataMember]
        public string ManagerId
        {
            get
            {
                return this.managerId;
            }
            set
            {
                this.managerId = value;
            }
        }

        /// <summary>
        /// 从数据行读取
        /// </summary>
        /// <param name="dr">数据行</param>
        protected override BaseEntity GetFrom(IDataRow dr)
        {
            Id = BaseBusinessLogic.ConvertToInt(dr[BaseDepartmentEntity.FieldId]);
            ParentId = BaseBusinessLogic.ConvertToString(dr[BaseDepartmentEntity.FieldParentId]);
            Code = BaseBusinessLogic.ConvertToString(dr[BaseDepartmentEntity.FieldCode]);
            FullName = BaseBusinessLogic.ConvertToString(dr[BaseDepartmentEntity.FieldFullName]);
            ShortName = BaseBusinessLogic.ConvertToString(dr[BaseDepartmentEntity.FieldShortName]);
            CategoryCode = BaseBusinessLogic.ConvertToString(dr[BaseDepartmentEntity.FieldCategoryCode]);
            OuterPhone = BaseBusinessLogic.ConvertToString(dr[BaseDepartmentEntity.FieldOuterPhone]);
            InnerPhone = BaseBusinessLogic.ConvertToString(dr[BaseDepartmentEntity.FieldInnerPhone]);
            Fax = BaseBusinessLogic.ConvertToString(dr[BaseDepartmentEntity.FieldFax]);
            Manager = BaseBusinessLogic.ConvertToString(dr[BaseDepartmentEntity.FieldManager]);
            ManagerMobile = BaseBusinessLogic.ConvertToString(dr[BaseDepartmentEntity.FieldManagerMobile]);
            ManagerQQ = BaseBusinessLogic.ConvertToString(dr[BaseDepartmentEntity.FieldManagerQQ]);
            DeletionStateCode = BaseBusinessLogic.ConvertToInt(dr[BaseDepartmentEntity.FieldDeletionStateCode]);
            Enabled = BaseBusinessLogic.ConvertToInt(dr[BaseDepartmentEntity.FieldEnabled]);
            SortCode = BaseBusinessLogic.ConvertToInt(dr[BaseDepartmentEntity.FieldSortCode]);
            Description = BaseBusinessLogic.ConvertToString(dr[BaseDepartmentEntity.FieldDescription]);
            CreateOn = BaseBusinessLogic.ConvertToNullableDateTime(dr[BaseDepartmentEntity.FieldCreateOn]);
            CreateUserId = BaseBusinessLogic.ConvertToString(dr[BaseDepartmentEntity.FieldCreateUserId]);
            CreateBy = BaseBusinessLogic.ConvertToString(dr[BaseDepartmentEntity.FieldCreateBy]);
            ModifiedOn = BaseBusinessLogic.ConvertToNullableDateTime(dr[BaseDepartmentEntity.FieldModifiedOn]);
            ModifiedUserId = BaseBusinessLogic.ConvertToString(dr[BaseDepartmentEntity.FieldModifiedUserId]);
            ModifiedBy = BaseBusinessLogic.ConvertToString(dr[BaseDepartmentEntity.FieldModifiedBy]);
            ParentName = BaseBusinessLogic.ConvertToString(dr[BaseDepartmentEntity.FieldParentName]);
            CompanyId = BaseBusinessLogic.ConvertToString(dr[BaseDepartmentEntity.FieldCompanyId]);
            CompanyName = BaseBusinessLogic.ConvertToString(dr[BaseDepartmentEntity.FieldCompanyName]);
            CompanyCode = BaseBusinessLogic.ConvertToString(dr[BaseDepartmentEntity.FieldCompanyCode]);
            CategoryName = BaseBusinessLogic.ConvertToString(dr[BaseDepartmentEntity.FieldCategoryName]);
            ParentCode = BaseBusinessLogic.ConvertToString(dr[BaseDepartmentEntity.FieldParentCode]);
            ManagerId = BaseBusinessLogic.ConvertToString(dr[BaseDepartmentEntity.FieldManagerId]);
            // 获取扩展属性
            GetFromExpand(dr);
            return this;
        }

        ///<summary>
        /// 部门表
        ///</summary>
        [NonSerialized]
        [FieldDescription("部门表")]
        public static string TableName = "BaseDepartment";

        ///<summary>
        /// 主键
        ///</summary>
        [NonSerialized]
        [FieldDescription("主键")]
        public static string FieldId = "Id";

        ///<summary>
        /// 父级主键
        ///</summary>
        [NonSerialized]
        [FieldDescription("父级主键")]
        public static string FieldParentId = "ParentId";

        ///<summary>
        /// 编号
        ///</summary>
        [NonSerialized]
        [FieldDescription("编号")]
        public static string FieldCode = "Code";

        ///<summary>
        /// 简称
        ///</summary>
        [NonSerialized]
        [FieldDescription("简称")]
        public static string FieldShortName = "ShortName";

        ///<summary>
        /// 名称
        ///</summary>
        [NonSerialized]
        [FieldDescription("名称")]
        public static string FieldFullName = "FullName";

        ///<summary>
        /// 分类编号
        ///</summary>
        [NonSerialized]
        [FieldDescription("分类编码")]
        public static string FieldCategoryCode = "CategoryCode";

        ///<summary>
        /// 外线电话
        ///</summary>
        [NonSerialized]
        [FieldDescription("外线电话")]
        public static string FieldOuterPhone = "OuterPhone";

        ///<summary>
        /// 内线电话
        ///</summary>
        [NonSerialized]
        [FieldDescription("内线电话")]
        public static string FieldInnerPhone = "InnerPhone";

        ///<summary>
        /// 传真
        ///</summary>
        [NonSerialized]
        [FieldDescription("传真")]
        public static string FieldFax = "Fax";

        ///<summary>
        /// 业务负责人
        ///</summary>
        [NonSerialized]
        [FieldDescription("业务负责人")]
        public static string FieldManager = "Manager";

        ///<summary>
        /// 业务负责人手机
        ///</summary>
        [NonSerialized]
        [FieldDescription("业务负责人手")]
        public static string FieldManagerMobile = "ManagerMobile";

        ///<summary>
        /// 业务负责人QQ
        ///</summary>
        [NonSerialized]
        [FieldDescription("业务负责人QQ")]
        public static string FieldManagerQQ = "ManagerQQ";

        ///<summary>
        /// 删除标记
        ///</summary>
        [NonSerialized]
        [FieldDescription("删除标记")]
        public static string FieldDeletionStateCode = "DeletionStateCode";

        ///<summary>
        /// 有效
        ///</summary>
        [NonSerialized]
        [FieldDescription("有效")]
        public static string FieldEnabled = "Enabled";

        ///<summary>
        /// 排序码
        ///</summary>
        [NonSerialized]
        [FieldDescription("排序码")]
        public static string FieldSortCode = "SortCode";

        ///<summary>
        /// 备注
        ///</summary>
        [NonSerialized]
        [FieldDescription("备注")]
        public static string FieldDescription = "Description";

        ///<summary>
        /// 创建日期
        ///</summary>
        [NonSerialized]
        [FieldDescription("创建日期")]
        public static string FieldCreateOn = "CreateOn";

        ///<summary>
        /// 创建用户主键
        ///</summary>
        [NonSerialized]
        [FieldDescription("创建用户主键")]
        public static string FieldCreateUserId = "CreateUserId";

        ///<summary>
        /// 创建用户
        ///</summary>
        [NonSerialized]
        [FieldDescription("创建用户")]
        public static string FieldCreateBy = "CreateBy";

        ///<summary>
        /// 修改日期
        ///</summary>
        [NonSerialized]
        [FieldDescription("修改日期")]
        public static string FieldModifiedOn = "ModifiedOn";

        ///<summary>
        /// 修改用户主键
        ///</summary>
        [NonSerialized]
        [FieldDescription("修改用户主键")]
        public static string FieldModifiedUserId = "ModifiedUserId";

        ///<summary>
        /// 修改用户
        ///</summary>
        [NonSerialized]
        [FieldDescription("修改用户")]
        public static string FieldModifiedBy = "ModifiedBy";

        ///<summary>
        /// 父级名称
        ///</summary>
        [NonSerialized]
        [FieldDescription("父级名称")]
        public static string FieldParentName = "PARENTNAME";

        ///<summary>
        /// 网点主键
        ///</summary>
        [NonSerialized]
        [FieldDescription("网点主键")]
        public static string FieldCompanyId = "COMPANYID";

        ///<summary>
        /// 网点名称
        ///</summary>
        [NonSerialized]
        [FieldDescription("网点名称")]
        public static string FieldCompanyName = "COMPANYNAME";

        ///<summary>
        /// 网点编号
        ///</summary>
        [NonSerialized]
        [FieldDescription("网点编号")]
        public static string FieldCompanyCode = "COMPANYCODE";

        ///<summary>
        /// 分类名称
        ///</summary>
        [NonSerialized]
        [FieldDescription("分类名称")]
        public static string FieldCategoryName = "CATEGORYNAME";

        ///<summary>
        /// 父级编号
        ///</summary>
        [NonSerialized]
        [FieldDescription("父级编号")]
        public static string FieldParentCode = "PARENTCODE";

        ///<summary>
        /// 主管主键
        ///</summary>
        [NonSerialized]
        [FieldDescription("主管主键")]
        public static string FieldManagerId = "MANAGERID";

    }
}
