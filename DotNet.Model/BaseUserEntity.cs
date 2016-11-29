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
    /// BaseUserEntity
    /// 系统用户表
    ///
    /// 修改记录
    ///
    ///		2010-08-07 版本：1.0 JiRiGaLa 创建主键。
    ///
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2010-08-07</date>
    /// </author>
    /// </summary>
    [Serializable, DataContract]
    public partial class BaseUserEntity : BaseEntity
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

        private string userFrom = string.Empty;
        /// <summary>
        /// 用户来源
        /// </summary>
        [FieldDescription("用户来源")]
        [DataMember]
        public string UserFrom
        {
            get
            {
                return this.userFrom;
            }
            set
            {
                this.userFrom = value;
            }
        }

        private string userPassword = null;
        /// <summary>
        /// 用户密码
        /// </summary>
        [FieldDescription("用户密码")]
        [DataMember]
        public string UserPassword
        {
            get
            {
                return this.userPassword;
            }
            set
            {
                this.userPassword = value;
            }
        }

        private string code = string.Empty;
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

        private string userName = string.Empty;
        /// <summary>
        /// 登录名
        /// </summary>
        [FieldDescription("登录名")]
        [DataMember]
        public string UserName
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(userName))
                {
                    return userName.ToLower();
                }
                return this.userName;
            }
            set
            {
                this.userName = value;
            }
        }

        private string realname = string.Empty;
        /// <summary>
        /// 姓名
        /// </summary>
        [FieldDescription("姓名")]
        [DataMember]
        public string RealName
        {
            get
            {
                return this.realname;
            }
            set
            {
                this.realname = value;
            }
        }

        private string nickname = string.Empty;
        /// <summary>
        /// 唯一用户名
        /// </summary>
        [FieldDescription("唯一用户名")]
        [DataMember]
        public string NickName
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(nickname))
                {
                    return nickname.ToLower();
                }
                return this.nickname;
            }
            set
            {
                this.nickname = value;
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
                if (!string.IsNullOrWhiteSpace(quickQuery))
                {
                    return quickQuery.ToLower();
                }
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
                if (!string.IsNullOrWhiteSpace(simpleSpelling))
                {
                    return simpleSpelling.ToLower();
                }
                return this.simpleSpelling;
            }
            set
            {
                this.simpleSpelling = value;
            }
        }

        private int? securityLevel = null;
        /// <summary>
        /// 安全级别
        /// </summary>
        [FieldDescription("安全级别")]
        [DataMember]
        public int? SecurityLevel
        {
            get
            {
                return this.securityLevel;
            }
            set
            {
                this.securityLevel = value;
            }
        }

        private string workCategory = string.Empty;
        /// <summary>
        /// 工作行业
        /// </summary>
        [FieldDescription("工作行业")]
        [DataMember]
        public string WorkCategory
        {
            get
            {
                return this.workCategory;
            }
            set
            {
                this.workCategory = value;
            }
        }

        private string companyId = null;
        /// <summary>
        /// 公司主键
        /// </summary>
        [FieldDescription("公司主键")]
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

        private string companyCode = string.Empty;
        /// <summary>
        /// 公司编号
        /// </summary>
        [FieldDescription("公司编号")]
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

        private string companyName = string.Empty;
        /// <summary>
        /// 公司名称
        /// </summary>
        [FieldDescription("公司名称")]
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

        private string subCompanyId = null;
        /// <summary>
        /// 分支机构主键
        /// </summary>
        [FieldDescription("分支机构主键")]
        [DataMember]
        public string SubCompanyId
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

        private string subCompanyName = string.Empty;
        /// <summary>
        /// 分支机构名称
        /// </summary>
        [FieldDescription("分支机构名称")]
        [DataMember]
        public string SubCompanyName
        {
            get
            {
                return this.subCompanyName;
            }
            set
            {
                this.subCompanyName = value;
            }
        }

        private string departmentId = null;
        /// <summary>
        /// 部门主键
        /// </summary>
        [FieldDescription("部门主键")]
        [DataMember]
        public string DepartmentId
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

        private string departmentName = string.Empty;
        /// <summary>
        /// 部门名称
        /// </summary>
        [FieldDescription("部门名称")]
        [DataMember]
        public string DepartmentName
        {
            get
            {
                return this.departmentName;
            }
            set
            {
                this.departmentName = value;
            }
        }

        private string subDepartmentId = null;
        /// <summary>
        /// 子部门主键
        /// </summary>
        [FieldDescription("子部门主键")]
        [DataMember]
        public string SubDepartmentId
        {
            get
            {
                return this.subDepartmentId;
            }
            set
            {
                this.subDepartmentId = value;
            }
        }

        private string subDepartmentName = string.Empty;
        /// <summary>
        /// 子部门名称
        /// </summary>
        [FieldDescription("子部门名称")]
        [DataMember]
        public string SubDepartmentName
        {
            get
            {
                return this.subDepartmentName;
            }
            set
            {
                this.subDepartmentName = value;
            }
        }

        private string workgroupId = null;
        /// <summary>
        /// 工作组代码
        /// </summary>
        [FieldDescription("工作组代码")]
        [DataMember]
        public string WorkgroupId
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

        private string workgroupName = string.Empty;
        /// <summary>
        /// 工作组名称
        /// </summary>
        [FieldDescription("工作组名称")]
        [DataMember]
        public string WorkgroupName
        {
            get
            {
                return this.workgroupName;
            }
            set
            {
                this.workgroupName = value;
            }
        }

        private string gender = string.Empty;
        /// <summary>
        /// 性别
        /// </summary>
        [FieldDescription("性别")]
        [DataMember]
        public string Gender
        {
            get
            {
                return this.gender;
            }
            set
            {
                this.gender = value;
            }
        }

        private string birthday = string.Empty;
        /// <summary>
        /// 出生日期
        /// </summary>
        [FieldDescription("出生日期")]
        [DataMember]
        public string Birthday
        {
            get
            {
                return this.birthday;
            }
            set
            {
                this.birthday = value;
            }
        }

        private String iDCard = string.Empty;
        /// <summary>
        /// 身份证号码
        /// </summary>
        [FieldDescription("身份证号码")]
        [DataMember]
        public String IDCard
        {
            get
            {
                return this.iDCard;
            }
            set
            {
                this.iDCard = value;
            }
        }

        private string duty = null;
        /// <summary>
        /// 岗位
        /// </summary>
        [FieldDescription("岗位")]
        [DataMember]
        public string Duty
        {
            get
            {
                return this.duty;
            }
            set
            {
                this.duty = value;
            }
        }

        private string title = null;
        /// <summary>
        /// 职称
        /// </summary>
        [FieldDescription("职称")]
        [DataMember]
        public string Title
        {
            get
            {
                return this.title;
            }
            set
            {
                this.title = value;
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

        private string homeAddress = null;
        /// <summary>
        /// 家庭住址（单位）
        /// </summary>
        [FieldDescription("家庭住址（单位）")]
        [DataMember]
        public string HomeAddress
        {
            get
            {
                return this.homeAddress;
            }
            set
            {
                this.homeAddress = value;
            }
        }

        private int? score = 0;
        /// <summary>
        /// 用户积分
        /// </summary>
        [FieldDescription("用户积分")]
        [DataMember]
        public int? Score
        {
            get
            {
                return this.score;
            }
            set
            {
                this.score = value;
            }
        }

        private string lang = null;
        /// <summary>
        /// 系统语言选择
        /// </summary>
        [FieldDescription("系统语言选择")]
        [DataMember]
        public string Lang
        {
            get
            {
                return this.lang;
            }
            set
            {
                this.lang = value;
            }
        }

        private string theme = null;
        /// <summary>
        /// 系统样式选择
        /// </summary>
        [FieldDescription("系统样式选择")]
        [DataMember]
        public string Theme
        {
            get
            {
                return this.theme;
            }
            set
            {
                this.theme = value;
            }
        }

        private int? isStaff = 0;
        /// <summary>
        /// 是否员工
        /// </summary>
        [FieldDescription("是否员工")]
        [DataMember]
        public int? IsStaff
        {
            get
            {
                return this.isStaff;
            }
            set
            {
                this.isStaff = value;
            }
        }

        private string auditStatus = null;
        /// <summary>
        /// 审核状态
        /// </summary>
        [FieldDescription("审核状态")]
        [DataMember]
        public string AuditStatus
        {
            get
            {
                //不小心影响登录,无法正常登录了
                //if (this.Enabled == 0 && auditStatus == null)
                //{
                //    return DotNet.Utilities.AuditStatus.WaitForAudit.ToString();
                //}
                return this.auditStatus;
            }
            set
            {
                this.auditStatus = value;
            }
        }

        private string managerId = null;
        /// <summary>
        /// 上级主管审核主键
        /// </summary>
        [FieldDescription("上级主管审核主键")]
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

        private string managerAuditStatus = null;
        /// <summary>
        /// 上级主管审核状态
        /// </summary>
        [FieldDescription("上级主管审核状态")]
        [DataMember]
        public string ManagerAuditStatus
        {
            get
            {
                return this.managerAuditStatus;
            }
            set
            {
                this.managerAuditStatus = value;
            }
        }

        private DateTime? managerAuditDate = null;
        /// <summary>
        /// 上级主管审核日期
        /// </summary>
        [FieldDescription("上级主管审核日期")]
        [DataMember]
        public DateTime? ManagerAuditDate
        {
            get
            {
                return this.managerAuditDate;
            }
            set
            {
                this.managerAuditDate = value;
            }
        }

        private int? isVisible = 1;
        /// <summary>
        /// 是否显示
        /// </summary>
        [FieldDescription("是否显示")]
        [DataMember]
        public int? IsVisible
        {
            get
            {
                return this.isVisible;
            }
            set
            {
                this.isVisible = value;
            }
        }

        private int? deletionStateCode = 0;
        /// <summary>
        /// 删除标志
        /// </summary>
        [FieldDescription("删除标志")]
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

        private bool isAdministrator = false;
        /// <summary>
        /// 管理员
        /// </summary>
        [FieldDescription("管理员")]
        [DataMember]
        public bool IsAdministrator
        {
            get
            {
                return this.isAdministrator;
            }
            set
            {
                this.isAdministrator = value;
            }
        }

        private int? sortCode = 0;
        /// <summary>
        /// 排序码
        /// </summary>
        [FieldDescription("排序码",false)]
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

        private string signature = null;
        /// <summary>
        /// 个性签名
        /// </summary>
        [FieldDescription("个性签名")]
        [DataMember]
        public string Signature
        {
            get
            {
                return this.signature;
            }
            set
            {
                this.signature = value;
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

        private int? isCheckBalance = 0;
        ///<summary>
        /// 扫描检测余额
        ///</summary>
        [FieldDescription("扫描检测余额")]
        [DataMember]
        public int? IsCheckBalance
        {
            get
            {
                return this.isCheckBalance;
            }
            set
            {
                this.isCheckBalance = value;
            }
        }

        /// <summary>
        /// 从数据行读取
        /// </summary>
        /// <param name="dr">数据行</param>
        protected override BaseEntity GetFrom(IDataRow dr)
        {
            Id = BaseBusinessLogic.ConvertToString(dr[BaseUserEntity.FieldId]);
            UserFrom = BaseBusinessLogic.ConvertToString(dr[BaseUserEntity.FieldUserFrom]);
            Code = BaseBusinessLogic.ConvertToString(dr[BaseUserEntity.FieldCode]);
            UserName = BaseBusinessLogic.ConvertToString(dr[BaseUserEntity.FieldUserName]);
            NickName = BaseBusinessLogic.ConvertToString(dr[BaseUserEntity.FieldNickName]);
            RealName = BaseBusinessLogic.ConvertToString(dr[BaseUserEntity.FieldRealName]);
            QuickQuery = BaseBusinessLogic.ConvertToString(dr[BaseUserEntity.FieldQuickQuery]);
            SimpleSpelling = BaseBusinessLogic.ConvertToString(dr[BaseUserEntity.FieldSimpleSpelling]);
            SecurityLevel = BaseBusinessLogic.ConvertToInt(dr[BaseUserEntity.FieldSecurityLevel]);
            WorkCategory = BaseBusinessLogic.ConvertToString(dr[BaseUserEntity.FieldWorkCategory]);
            CompanyId = BaseBusinessLogic.ConvertToString(dr[BaseUserEntity.FieldCompanyId]);
            // 2015-12-29 吉日嘎拉 防止程序出错，没有这个字段也可以正常运行
            if (dr.ContainsColumn(BaseUserEntity.FieldCompanyCode))
            {
                CompanyCode = BaseBusinessLogic.ConvertToString(dr[BaseUserEntity.FieldCompanyCode]);
            }
            CompanyName = BaseBusinessLogic.ConvertToString(dr[BaseUserEntity.FieldCompanyName]);
            SubCompanyId = BaseBusinessLogic.ConvertToString(dr[BaseUserEntity.FieldSubCompanyId]);
            SubCompanyName = BaseBusinessLogic.ConvertToString(dr[BaseUserEntity.FieldSubCompanyName]);
            DepartmentId = BaseBusinessLogic.ConvertToString(dr[BaseUserEntity.FieldDepartmentId]);
            DepartmentName = BaseBusinessLogic.ConvertToString(dr[BaseUserEntity.FieldDepartmentName]);
            SubDepartmentId = BaseBusinessLogic.ConvertToString(dr[BaseUserEntity.FieldSubDepartmentId]);
            SubDepartmentName = BaseBusinessLogic.ConvertToString(dr[BaseUserEntity.FieldSubDepartmentName]);
            WorkgroupId = BaseBusinessLogic.ConvertToString(dr[BaseUserEntity.FieldWorkgroupId]);
            WorkgroupName = BaseBusinessLogic.ConvertToString(dr[BaseUserEntity.FieldWorkgroupName]);
            IDCard = BaseBusinessLogic.ConvertToString(dr[BaseUserEntity.FieldIDCard]);
            Gender = BaseBusinessLogic.ConvertToString(dr[BaseUserEntity.FieldGender]);
            Birthday = BaseBusinessLogic.ConvertToString(dr[BaseUserEntity.FieldBirthday]);
            Duty = BaseBusinessLogic.ConvertToString(dr[BaseUserEntity.FieldDuty]);
            Title = BaseBusinessLogic.ConvertToString(dr[BaseUserEntity.FieldTitle]);
            Province = BaseBusinessLogic.ConvertToString(dr[BaseUserEntity.FieldProvince]);
            City = BaseBusinessLogic.ConvertToString(dr[BaseUserEntity.FieldCity]);
            District = BaseBusinessLogic.ConvertToString(dr[BaseUserEntity.FieldDistrict]);
            HomeAddress = BaseBusinessLogic.ConvertToString(dr[BaseUserEntity.FieldHomeAddress]);
            Score = BaseBusinessLogic.ConvertToInt(dr[BaseUserEntity.FieldScore]);
            IsAdministrator = BaseBusinessLogic.ConvertToBoolean(dr[BaseUserEntity.FieldIsAdministrator]);
            Lang = BaseBusinessLogic.ConvertToString(dr[BaseUserEntity.FieldLang]);
            Theme = BaseBusinessLogic.ConvertToString(dr[BaseUserEntity.FieldTheme]);
            Signature = BaseBusinessLogic.ConvertToString(dr[BaseUserEntity.FieldSignature]);
            IsStaff = BaseBusinessLogic.ConvertToInt(dr[BaseUserEntity.FieldIsStaff]);
            IsCheckBalance = BaseBusinessLogic.ConvertToInt(dr[BaseUserEntity.FieldIsCheckBalance]);
            AuditStatus = BaseBusinessLogic.ConvertToString(dr[BaseUserEntity.FieldAuditStatus]);
            ManagerId = BaseBusinessLogic.ConvertToString(dr[BaseUserEntity.FieldManagerId]);
            ManagerAuditStatus = BaseBusinessLogic.ConvertToString(dr[BaseUserEntity.FieldManagerAuditStatus]);
            ManagerAuditDate = BaseBusinessLogic.ConvertToNullableDateTime(dr[BaseUserEntity.FieldManagerAuditDate]);
            IsVisible = BaseBusinessLogic.ConvertToInt(dr[BaseUserEntity.FieldIsVisible]);
            DeletionStateCode = BaseBusinessLogic.ConvertToInt(dr[BaseUserEntity.FieldDeletionStateCode]);
            Enabled = BaseBusinessLogic.ConvertToInt(dr[BaseUserEntity.FieldEnabled]);
            SortCode = BaseBusinessLogic.ConvertToInt(dr[BaseUserEntity.FieldSortCode]);
            Description = BaseBusinessLogic.ConvertToString(dr[BaseUserEntity.FieldDescription]);
            CreateOn = BaseBusinessLogic.ConvertToNullableDateTime(dr[BaseUserEntity.FieldCreateOn]);
            CreateUserId = BaseBusinessLogic.ConvertToString(dr[BaseUserEntity.FieldCreateUserId]);
            CreateBy = BaseBusinessLogic.ConvertToString(dr[BaseUserEntity.FieldCreateBy]);
            ModifiedOn = BaseBusinessLogic.ConvertToNullableDateTime(dr[BaseUserEntity.FieldModifiedOn]);
            ModifiedUserId = BaseBusinessLogic.ConvertToString(dr[BaseUserEntity.FieldModifiedUserId]);
            ModifiedBy = BaseBusinessLogic.ConvertToString(dr[BaseUserEntity.FieldModifiedBy]);
            // 获取扩展属性
            GetFromExpand(dr);
            return this;
        }

        ///<summary>
        /// 系统用户表
        ///</summary>
        [NonSerialized]
        [FieldDescription("系统用户表")]
        public static string TableName = "BaseUser";

        ///<summary>
        /// 主键
        ///</summary>
        [NonSerialized]
        public static string FieldId = "Id";

        ///<summary>
        /// 用户来源
        ///</summary>
        [NonSerialized]
        public static string FieldUserFrom = "UserFrom";

        /// <summary>
        /// 主管主键
        /// </summary>
        [NonSerialized]
        public static string FieldManagerId = "ManagerId";

        /// <summary>
        /// 上级主管审核状态
        /// </summary>
        [NonSerialized]
        public static string FieldManagerAuditStatus = "ManagerAuditStatus";

        /// <summary>
        /// 上级主管审核日期
        /// </summary>
        [NonSerialized]
        public static string FieldManagerAuditDate = "ManagerAuditDate";

        ///<summary>
        /// 编号
        ///</summary>
        [NonSerialized]
        public static string FieldCode = "Code";

        ///<summary>
        /// 登录名
        ///</summary>
        [NonSerialized]
        public static string FieldUserName = "UserName";

        ///<summary>
        /// 姓名
        ///</summary>
        [NonSerialized]
        public static string FieldRealName = "RealName";

        ///<summary>
        /// 呢称
        ///</summary>
        [NonSerialized]
        public static string FieldNickName = "Nickname";

        ///<summary>
        /// 身份证号码
        ///</summary>
        [NonSerialized]
        public static string FieldIDCard = "IDCard";

        ///<summary>
        /// 快速查询，全拼
        ///</summary>
        [NonSerialized]
        public static string FieldQuickQuery = "QuickQuery";

        ///<summary>
        /// 快速查询，简拼
        ///</summary>
        [NonSerialized]
        public static string FieldSimpleSpelling = "SimpleSpelling";

        ///<summary>
        /// 个性签名
        ///</summary>
        [NonSerialized]
        public static string FieldSignature = "Signature";

        ///<summary>
        /// 安全级别
        ///</summary>
        [NonSerialized]
        public static string FieldSecurityLevel = "SecurityLevel";

        ///<summary>
        /// 工作行业
        ///</summary>
        [NonSerialized]
        public static string FieldWorkCategory = "WorkCategory";

        ///<summary>
        /// 公司主键
        ///</summary>
        [NonSerialized]
        public static string FieldCompanyId = "CompanyId";

        ///<summary>
        /// 公司编号
        ///</summary>
        [NonSerialized]
        public static string FieldCompanyCode = "CompanyCode";

        ///<summary>
        /// 公司名称
        ///</summary>
        [NonSerialized]
        public static string FieldCompanyName = "CompanyName";

        ///<summary>
        /// 分支机构主键
        ///</summary>
        [NonSerialized]
        public static string FieldSubCompanyId = "SubCompanyId";

        ///<summary>
        /// 分支机构名称
        ///</summary>
        [NonSerialized]
        public static string FieldSubCompanyName = "SubCompanyName";

        ///<summary>
        /// 部门主键
        ///</summary>
        [NonSerialized]
        public static string FieldDepartmentId = "DepartmentId";

        ///<summary>
        /// 部门名称
        ///</summary>
        [NonSerialized]
        public static string FieldDepartmentName = "DepartmentName";

        ///<summary>
        /// 子部门主键
        ///</summary>
        [NonSerialized]
        public static string FieldSubDepartmentId = "SubDepartmentId";

        ///<summary>
        /// 子部门名称
        ///</summary>
        [NonSerialized]
        public static string FieldSubDepartmentName = "SubDepartmentName";

        ///<summary>
        /// 工作组主键
        ///</summary>
        [NonSerialized]
        public static string FieldWorkgroupId = "WorkgroupId";

        ///<summary>
        /// 工作组名称
        ///</summary>
        [NonSerialized]
        public static string FieldWorkgroupName = "WorkgroupName";

        ///<summary>
        /// 性别
        ///</summary>
        [NonSerialized]
        public static string FieldGender = "Gender";

        ///<summary>
        /// 出生日期
        ///</summary>
        [NonSerialized]
        public static string FieldBirthday = "Birthday";

        ///<summary>
        /// 岗位
        ///</summary>
        [NonSerialized]
        public static string FieldDuty = "Duty";

        ///<summary>
        /// 职称
        ///</summary>
        [NonSerialized]
        public static string FieldTitle = "Title";

        ///<summary>
        /// 省
        ///</summary>
        [NonSerialized]
        public static string FieldProvince = "Province";

        ///<summary>
        /// 市
        ///</summary>
        [NonSerialized]
        public static string FieldCity = "City";

        ///<summary>
        /// 县
        ///</summary>
        [NonSerialized]
        public static string FieldDistrict = "District";

        ///<summary>
        /// 家庭住址
        ///</summary>
        [NonSerialized]
        public static string FieldHomeAddress = "HomeAddress";

        ///<summary>
        /// 用户积分
        ///</summary>
        [NonSerialized]
        public static string FieldScore = "Score";

        ///<summary>
        /// 系统语言选择
        ///</summary>
        [NonSerialized]
        public static string FieldLang = "Lang";

        ///<summary>
        /// 系统样式选择
        ///</summary>
        [NonSerialized]
        public static string FieldTheme = "Theme";

        ///<summary>
        /// 是否员工
        ///</summary>
        [NonSerialized]
        public static string FieldIsStaff = "IsStaff";

        ///<summary>
        /// 扫描检测余额
        ///</summary>
        [NonSerialized]
        public static string FieldIsCheckBalance = "IsCheckBalance";

        ///<summary>
        /// 审核状态
        ///</summary>
        [NonSerialized]
        public static string FieldAuditStatus = "AuditStatus";

        ///<summary>
        /// 是否显示
        ///</summary>
        [NonSerialized]
        public static string FieldIsVisible = "IsVisible";

        ///<summary>
        /// 删除标志
        ///</summary>
        [NonSerialized]
        public static string FieldDeletionStateCode = "DeletionStateCode";

        ///<summary>
        /// 有效
        ///</summary>
        [NonSerialized]
        public static string FieldEnabled = "Enabled";

        ///<summary>
        /// 是否超级管理员
        ///</summary>
        [NonSerialized]
        public static string FieldIsAdministrator = "IsAdministrator";

        ///<summary>
        /// 排序码
        ///</summary>
        [NonSerialized]
        public static string FieldSortCode = "SortCode";

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