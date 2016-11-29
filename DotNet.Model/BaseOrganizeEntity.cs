//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd. 
//-----------------------------------------------------------------

using System;
using System.Runtime.Serialization;

namespace DotNet.Model
{
    using DotNet.Utilities;

    /// <summary>
    /// BaseOrganizeEntity
    /// 组织机构、部门表
    ///
    /// 修改记录
    ///
    ///     2015-01-22 版本：2.0 panqimin 添加FieldDescriptionName，用于修改记录显示字段名称
    ///		2010-07-15 版本：1.0 JiRiGaLa 创建主键。
    ///
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2010-07-15</date>
    /// </author>
    /// </summary>
    [Serializable, DataContract]
    public partial class BaseOrganizeEntity : BaseEntity
    {
        private string id = null;
        /// <summary>
        /// 主键
        /// </summary>
        [FieldDescription("主键", false)]
        [DataMember]
        public string Id
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

        private int? layer = 0;
        /// <summary>
        /// 层
        /// </summary>
        [FieldDescription("层")]
        [DataMember]
        public int? Layer
        {
            get
            {
                return this.layer;
            }
            set
            {
                this.layer = value;
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

        private string standardName = null;
        /// <summary>
        /// 标准名称
        /// </summary>
        [FieldDescription("标准名称")]
        [DataMember]
        public string StandardName
        {
            get
            {
                return this.standardName;
            }
            set
            {
                this.standardName = value;
            }
        }

        private string standardCode = null;
        /// <summary>
        /// 标准编号
        /// </summary>
        [FieldDescription("标准编号")]
        [DataMember]
        public string StandardCode
        {
            get
            {
                return this.standardCode;
            }
            set
            {
                this.standardCode = value;
            }
        }

        private string quickQuery = string.Empty;
        /// <summary>
        /// 快速查询，全拼
        /// </summary>
        [FieldDescription("快速查询，全拼")]
        [DataMember]
        public string QuickQuery
        {
            get
            {
                return this.quickQuery;
            }
            set
            {
                this.quickQuery = value;
            }
        }

        private string simpleSpelling = string.Empty;
        /// <summary>
        /// 快速查询，简拼
        /// </summary>
        [FieldDescription("快速查询，简拼")]
        [DataMember]
        public string SimpleSpelling
        {
            get
            {
                return this.simpleSpelling;
            }
            set
            {
                this.simpleSpelling = value;
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

        private string postalcode = null;
        /// <summary>
        /// 邮编
        /// </summary>
        [FieldDescription("邮编")]
        [DataMember]
        public string Postalcode
        {
            get
            {
                return this.postalcode;
            }
            set
            {
                this.postalcode = value;
            }
        }

        private string address = null;
        /// <summary>
        /// 地址
        /// </summary>
        [FieldDescription("地址")]
        [DataMember]
        public string Address
        {
            get
            {
                return this.address;
            }
            set
            {
                this.address = value;
            }
        }

        private string provinceId = null;
        /// <summary>
        /// 省主键
        /// </summary>
        [FieldDescription("省主键")]
        [DataMember]
        public string ProvinceId
        {
            get
            {
                return this.provinceId;
            }
            set
            {
                this.provinceId = value;
            }
        }

        private string province = null;
        /// <summary>
        /// 省
        /// </summary>
        [FieldDescription("省")]
        [DataMember]
        public string Province
        {
            get
            {
                return this.province;
            }
            set
            {
                this.province = value;
            }
        }

        private string cityId = null;
        /// <summary>
        /// 市主键
        /// </summary>
        [FieldDescription("市主键")]
        [DataMember]
        public string CityId
        {
            get
            {
                return this.cityId;
            }
            set
            {
                this.cityId = value;
            }
        }

        private string city = null;
        /// <summary>
        /// 市
        /// </summary>
        [FieldDescription("市")]
        [DataMember]
        public string City
        {
            get
            {
                return this.city;
            }
            set
            {
                this.city = value;
            }
        }

        private string districtId = null;
        /// <summary>
        /// 县主键
        /// </summary>
        [FieldDescription("县主键")]
        [DataMember]
        public string DistrictId
        {
            get
            {
                return this.districtId;
            }
            set
            {
                this.districtId = value;
            }
        }

        private string district = null;
        /// <summary>
        /// 县
        /// </summary>
        [FieldDescription("县")]
        [DataMember]
        public string District
        {
            get
            {
                return this.district;
            }
            set
            {
                this.district = value;
            }
        }

        private string streetId = null;
        /// <summary>
        /// 街道主键
        /// </summary>
        [FieldDescription("街道主键")]
        [DataMember]
        public string StreetId
        {
            get
            {
                return this.streetId;
            }
            set
            {
                this.streetId = value;
            }
        }

        private string street = null;
        /// <summary>
        /// 街道
        /// </summary>
        [FieldDescription("街道")]
        [DataMember]
        public string Street
        {
            get
            {
                return this.street;
            }
            set
            {
                this.street = value;
            }
        }

        private string web = null;
        /// <summary>
        /// 网址
        /// </summary>
        [FieldDescription("网址")]
        [DataMember]
        public string Web
        {
            get
            {
                return this.web;
            }
            set
            {
                this.web = value;
            }
        }

        private int? isInnerOrganize = 1;
        /// <summary>
        /// 内部组织机构
        /// </summary>
        [FieldDescription("内部组织机构")]
        [DataMember]
        public int? IsInnerOrganize
        {
            get
            {
                return this.isInnerOrganize;
            }
            set
            {
                this.isInnerOrganize = value;
            }
        }

        private string bank = null;
        /// <summary>
        /// 开户行
        /// </summary>
        [FieldDescription("开户行")]
        [DataMember]
        public string Bank
        {
            get
            {
                return this.bank;
            }
            set
            {
                this.bank = value;
            }
        }

        private string bankAccount = null;
        /// <summary>
        /// 银行帐号
        /// </summary>
        [FieldDescription("银行帐号")]
        [DataMember]
        public string BankAccount
        {
            get
            {
                return this.bankAccount;
            }
            set
            {
                this.bankAccount = value;
            }
        }

        private string companyId = null;
        /// <summary>
        /// 所属公司（各地的分公司，一级网点）主键
        /// </summary>
        [FieldDescription("所属公司（一级网点）主键")]
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

        private string companyCode = null;
        /// <summary>
        /// 所属公司（各地的分公司，一级网点）编号
        /// </summary>
        [FieldDescription("所属公司（一级网点）编号")]
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

        private string companyName = null;
        /// <summary>
        /// 所属公司（各地的分公司，一级网点）名称
        /// </summary>
        [FieldDescription("所属公司（一级网点）名称")]
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

        private string costCenterId = null;
        /// <summary>
        /// 结算中心主键
        /// </summary>
        [FieldDescription("结算中心主键")]
        [DataMember]
        public string CostCenterId
        {
            get
            {
                return this.costCenterId;
            }
            set
            {
                this.costCenterId = value;
            }
        }

        private string costCenter = null;
        /// <summary>
        /// 结算中心
        /// </summary>
        [FieldDescription("结算中心")]
        [DataMember]
        public string CostCenter
        {
            get
            {
                return this.costCenter;
            }
            set
            {
                this.costCenter = value;
            }
        }

        private string financialCenterId = null;
        /// <summary>
        /// 财务中心主键
        /// </summary>
        [FieldDescription("财务中心主键")]
        [DataMember]
        public string FinancialCenterId
        {
            get
            {
                return this.financialCenterId;
            }
            set
            {
                this.financialCenterId = value;
            }
        }

        private string financialCenter = null;
        /// <summary>
        /// 财务中心
        /// </summary>
        [FieldDescription("财务中心")]
        [DataMember]
        public string FinancialCenter
        {
            get
            {
                return this.financialCenter;
            }
            set
            {
                this.financialCenter = value;
            }
        }

        private string area = null;
        /// <summary>
        /// 所属区域（片区）
        /// </summary>
        [FieldDescription("所属区域（片区）")]
        [DataMember]
        public string Area
        {
            get
            {
                return this.area;
            }
            set
            {
                this.area = value;
            }
        }

        private string joiningMethods = null;
        /// <summary>
        /// 加盟方式
        /// </summary>
        [FieldDescription("加盟方式")]
        [DataMember]
        public string JoiningMethods
        {
            get
            {
                return this.joiningMethods;
            }
            set
            {
                this.joiningMethods = value;
            }
        }

        private int? deletionStateCode = 0;
        /// <summary>
        /// 删除标记
        /// </summary>
        [FieldDescription("删除标记", false)]
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
        [FieldDescription("排序码", false)]
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

        private string manageName = null;
        /// <summary>
        /// 组织架构名称
        /// </summary>
        [FieldDescription("组织架构名称")]
        [DataMember]
        public string ManageName
        {
            get
            {
                return this.manageName;
            }
            set
            {
                this.manageName = value;
            }
        }

        private string manageId = null;
        /// <summary>
        /// 组织架构主键
        /// </summary>
        [FieldDescription("组织架构主键")]
        [DataMember]
        public string ManageId
        {
            get
            {
                return this.manageId;
            }
            set
            {
                this.manageId = value;
            }
        }

        /// <summary>
        /// 从数据行读取
        /// </summary>
        /// <param name="dr">数据行</param>
        protected override BaseEntity GetFrom(IDataRow dr)
        {
            Id = BaseBusinessLogic.ConvertToString(dr[BaseOrganizeEntity.FieldId]);
            ParentId = BaseBusinessLogic.ConvertToString(dr[BaseOrganizeEntity.FieldParentId]);
            ParentName = BaseBusinessLogic.ConvertToString(dr[BaseOrganizeEntity.FieldParentName]);
            Layer = BaseBusinessLogic.ConvertToInt(dr[BaseOrganizeEntity.FieldLayer]);
            Code = BaseBusinessLogic.ConvertToString(dr[BaseOrganizeEntity.FieldCode]);
            FullName = BaseBusinessLogic.ConvertToString(dr[BaseOrganizeEntity.FieldFullName]);
            ShortName = BaseBusinessLogic.ConvertToString(dr[BaseOrganizeEntity.FieldShortName]);
            StandardName = BaseBusinessLogic.ConvertToString(dr[BaseOrganizeEntity.FieldStandardName]);
            StandardCode = BaseBusinessLogic.ConvertToString(dr[BaseOrganizeEntity.FieldStandardCode]);

            QuickQuery = BaseBusinessLogic.ConvertToString(dr[BaseOrganizeEntity.FieldQuickQuery]);
            SimpleSpelling = BaseBusinessLogic.ConvertToString(dr[BaseOrganizeEntity.FieldSimpleSpelling]);
            CategoryCode = BaseBusinessLogic.ConvertToString(dr[BaseOrganizeEntity.FieldCategoryCode]);
            OuterPhone = BaseBusinessLogic.ConvertToString(dr[BaseOrganizeEntity.FieldOuterPhone]);
            InnerPhone = BaseBusinessLogic.ConvertToString(dr[BaseOrganizeEntity.FieldInnerPhone]);
            Fax = BaseBusinessLogic.ConvertToString(dr[BaseOrganizeEntity.FieldFax]);
            Postalcode = BaseBusinessLogic.ConvertToString(dr[BaseOrganizeEntity.FieldPostalcode]);

            ProvinceId = BaseBusinessLogic.ConvertToString(dr[BaseOrganizeEntity.FieldProvinceId]);
            Province = BaseBusinessLogic.ConvertToString(dr[BaseOrganizeEntity.FieldProvince]);
            CityId = BaseBusinessLogic.ConvertToString(dr[BaseOrganizeEntity.FieldCityId]);
            City = BaseBusinessLogic.ConvertToString(dr[BaseOrganizeEntity.FieldCity]);
            DistrictId = BaseBusinessLogic.ConvertToString(dr[BaseOrganizeEntity.FieldDistrictId]);
            District = BaseBusinessLogic.ConvertToString(dr[BaseOrganizeEntity.FieldDistrict]);
            StreetId = BaseBusinessLogic.ConvertToString(dr[BaseOrganizeEntity.FieldStreetId]);
            Street = BaseBusinessLogic.ConvertToString(dr[BaseOrganizeEntity.FieldStreet]);

            Address = BaseBusinessLogic.ConvertToString(dr[BaseOrganizeEntity.FieldAddress]);
            Web = BaseBusinessLogic.ConvertToString(dr[BaseOrganizeEntity.FieldWeb]);
            IsInnerOrganize = BaseBusinessLogic.ConvertToInt(dr[BaseOrganizeEntity.FieldIsInnerOrganize]);
            Bank = BaseBusinessLogic.ConvertToString(dr[BaseOrganizeEntity.FieldBank]);
            BankAccount = BaseBusinessLogic.ConvertToString(dr[BaseOrganizeEntity.FieldBankAccount]);

            CompanyId = BaseBusinessLogic.ConvertToString(dr[BaseOrganizeEntity.FieldCompanyId]);
            CompanyCode = BaseBusinessLogic.ConvertToString(dr[BaseOrganizeEntity.FieldCompanyCode]);
            CompanyName = BaseBusinessLogic.ConvertToString(dr[BaseOrganizeEntity.FieldCompanyName]);

            if (dr.ContainsColumn(BaseOrganizeEntity.FieldCostCenterId))
            {
                CostCenterId = BaseBusinessLogic.ConvertToString(dr[BaseOrganizeEntity.FieldCostCenterId]);
            }
            CostCenter = BaseBusinessLogic.ConvertToString(dr[BaseOrganizeEntity.FieldCostCenter]);
            if (dr.ContainsColumn(BaseOrganizeEntity.FieldFinancialCenterId))
            {
                FinancialCenterId = BaseBusinessLogic.ConvertToString(dr[BaseOrganizeEntity.FieldFinancialCenterId]);
            }
            FinancialCenter = BaseBusinessLogic.ConvertToString(dr[BaseOrganizeEntity.FieldFinancialCenter]);
            if (dr.ContainsColumn(BaseOrganizeEntity.FieldManageId))
            {
                ManageId = BaseBusinessLogic.ConvertToString(dr[BaseOrganizeEntity.FieldManageId]);
            }
            if (dr.ContainsColumn(BaseOrganizeEntity.FieldManageName))
            {
                ManageName = BaseBusinessLogic.ConvertToString(dr[BaseOrganizeEntity.FieldManageName]);
            }
            Area = BaseBusinessLogic.ConvertToString(dr[BaseOrganizeEntity.FieldArea]);
            JoiningMethods = BaseBusinessLogic.ConvertToString(dr[BaseOrganizeEntity.FieldJoiningMethods]);

            DeletionStateCode = BaseBusinessLogic.ConvertToInt(dr[BaseOrganizeEntity.FieldDeletionStateCode]);
            Enabled = BaseBusinessLogic.ConvertToInt(dr[BaseOrganizeEntity.FieldEnabled]);
            SortCode = BaseBusinessLogic.ConvertToInt(dr[BaseOrganizeEntity.FieldSortCode]);
            Description = BaseBusinessLogic.ConvertToString(dr[BaseOrganizeEntity.FieldDescription]);
            CreateOn = BaseBusinessLogic.ConvertToNullableDateTime(dr[BaseOrganizeEntity.FieldCreateOn]);
            CreateUserId = BaseBusinessLogic.ConvertToString(dr[BaseOrganizeEntity.FieldCreateUserId]);
            CreateBy = BaseBusinessLogic.ConvertToString(dr[BaseOrganizeEntity.FieldCreateBy]);
            ModifiedOn = BaseBusinessLogic.ConvertToNullableDateTime(dr[BaseOrganizeEntity.FieldModifiedOn]);
            ModifiedUserId = BaseBusinessLogic.ConvertToString(dr[BaseOrganizeEntity.FieldModifiedUserId]);
            ModifiedBy = BaseBusinessLogic.ConvertToString(dr[BaseOrganizeEntity.FieldModifiedBy]);
            // 获取扩展属性
            GetFromExpand(dr);
            return this;
        }

        ///<summary>
        /// 组织机构、部门表
        ///</summary>
        [NonSerialized]
        [FieldDescription("组织机构、部门表")]
        public static string TableName = "BaseOrganize";

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
        /// 父级名称
        ///</summary>
        [NonSerialized]
        [FieldDescription("父级名称")]
        public static string FieldParentName = "ParentName";

        ///<summary>
        /// 层
        ///</summary>
        [NonSerialized]
        [FieldDescription("层")]
        public static string FieldLayer = "Layer";

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

        /// <summary>
        /// 标准名称
        /// </summary>
        [NonSerialized]
        [FieldDescription("标准名称")]
        public static string FieldStandardName = "StandardName";

        /// <summary>
        /// 标准编号
        /// </summary>
        [NonSerialized]
        [FieldDescription("标准编号")]
        public static string FieldStandardCode = "StandardCode";

        ///<summary>
        /// 快速查询，全拼
        ///</summary>
        [NonSerialized]
        [FieldDescription("快速查询，全拼")]
        public static string FieldQuickQuery = "QuickQuery";

        ///<summary>
        /// 快速查询，简拼
        ///</summary>
        [NonSerialized]
        [FieldDescription("快速查询，简拼")]
        public static string FieldSimpleSpelling = "SimpleSpelling";

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
        /// 邮编
        ///</summary>
        [NonSerialized]
        [FieldDescription("邮编")]
        public static string FieldPostalcode = "Postalcode";

        ///<summary>
        /// 省主键
        ///</summary>
        [NonSerialized]
        [FieldDescription("省主键")]
        public static string FieldProvinceId = "ProvinceId";

        ///<summary>
        /// 省
        ///</summary>
        [NonSerialized]
        [FieldDescription("省")]
        public static string FieldProvince = "Province";

        ///<summary>
        /// 市主键
        ///</summary>
        [NonSerialized]
        [FieldDescription("市主键")]
        public static string FieldCityId = "CityId";

        ///<summary>
        /// 市
        ///</summary>
        [NonSerialized]
        [FieldDescription("市")]
        public static string FieldCity = "City";

        ///<summary>
        /// 县主键
        ///</summary>
        [NonSerialized]
        [FieldDescription("县主键")]
        public static string FieldDistrictId = "DistrictId";

        ///<summary>
        /// 县
        ///</summary>
        [NonSerialized]
        [FieldDescription("县")]
        public static string FieldDistrict = "District";

        ///<summary>
        /// 街道键
        ///</summary>
        [NonSerialized]
        [FieldDescription("街道主键")]
        public static string FieldStreetId = "StreetId";

        ///<summary>
        /// 街道
        ///</summary>
        [NonSerialized]
        [FieldDescription("街道")]
        public static string FieldStreet = "Street";

        ///<summary>
        /// 地址
        ///</summary>
        [NonSerialized]
        [FieldDescription("地址")]
        public static string FieldAddress = "Address";

        ///<summary>
        /// 网址
        ///</summary>
        [NonSerialized]
        [FieldDescription("网址")]
        public static string FieldWeb = "Web";

        ///<summary>
        /// 内部组织机构
        ///</summary>
        [NonSerialized]
        [FieldDescription("内部组织机构")]
        public static string FieldIsInnerOrganize = "IsInnerOrganize";

        ///<summary>
        /// 开户行
        ///</summary>
        [NonSerialized]
        [FieldDescription("开户行")]
        public static string FieldBank = "Bank";

        ///<summary>
        /// 银行帐号
        ///</summary>
        [NonSerialized]
        [FieldDescription("银行帐号")]
        public static string FieldBankAccount = "BankAccount";

        /// <summary>
        /// 所属公司（各地的分公司，一级网点）主键
        /// </summary>
        [NonSerialized]
        [FieldDescription("一级网点主键")]
        public static string FieldCompanyId = "CompanyId";

        /// <summary>
        /// 所属公司（各地的分公司，一级网点）编号
        /// </summary>
        [NonSerialized]
        [FieldDescription("一级网点编号")]
        public static string FieldCompanyCode = "CompanyCode";

        /// <summary>
        /// 所属公司（各地的分公司，一级网点）名称
        /// </summary>
        [NonSerialized]
        [FieldDescription("一级网点名称")]
        public static string FieldCompanyName = "CompanyName";

        /// <summary>
        /// 结算中心
        /// </summary>
        [NonSerialized]
        [FieldDescription("结算中心主键")]
        public static string FieldCostCenterId = "CostCenterId";

        /// <summary>
        /// 结算中心
        /// </summary>
        [NonSerialized]
        [FieldDescription("结算中心")]
        public static string FieldCostCenter = "CostCenter";

        /// <summary>
        /// 财务中心主键
        /// </summary>
        [NonSerialized]
        [FieldDescription("财务中心主键")]
        public static string FieldFinancialCenterId = "FinancialCenterId";

        /// <summary>
        /// 财务中心
        /// </summary>
        [NonSerialized]
        [FieldDescription("财务中心")]
        public static string FieldFinancialCenter = "FinancialCenter";

        /// <summary>
        /// 所属区域（片区）
        /// </summary>
        [NonSerialized]
        [FieldDescription("片区")]
        public static string FieldArea = "Area";

        /// <summary>
        /// 加盟方式
        /// </summary>
        [NonSerialized]
        [FieldDescription("加盟方式")]
        public static string FieldJoiningMethods = "JoiningMethods";

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
        /// 组织架构主键
        ///</summary>
        [NonSerialized]
        [FieldDescription("组织架构主键")]
        public static string FieldManageId = "ManageId";

        ///<summary>
        /// 组织架构名称
        ///</summary>
        [NonSerialized]
        [FieldDescription("组织架构名称")]
        public static string FieldManageName = "ManageName";
    }
}
