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
    /// BaseStaffEntity
    /// 员工（职员）表
    /// 
    /// 修改记录
    /// 
    /// 2012-07-07 版本：1.0 JiRiGaLa 创建主键。
    /// 
    /// <author>
    ///     <name>JiRiGaLa</name>
    ///     <date>2012-07-07</date>
    /// </author>
    /// </summary>
    [Serializable, DataContract]
    public partial class BaseStaffEntity : BaseEntity
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

        private int? userId = null;
        /// <summary>
        /// 用户主键
        /// </summary>
        [DataMember]
        public int? UserId
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

        private String userName = string.Empty;
        /// <summary>
        /// 用户名(登录用)
        /// </summary>
        [DataMember]
        public String UserName
        {
            get
            {
                return this.userName;
            }
            set
            {
                this.userName = value;
            }
        }

        private String realname = string.Empty;
        /// <summary>
        /// 姓名
        /// </summary>
        [DataMember]
        public String RealName
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

        private String code = string.Empty;
        /// <summary>
        /// 编号,工号
        /// </summary>
        [DataMember]
        public String Code
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

        private String gender = string.Empty;
        /// <summary>
        /// 性别
        /// </summary>
        [DataMember]
        public String Gender
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

        private String companyName = string.Empty;
        /// <summary>
        /// 公司名称
        /// </summary>
        [DataMember]
        public String CompanyName
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

        private String departmentName = string.Empty;
        /// <summary>
        /// 部门名称
        /// </summary>
        [DataMember]
        public String DepartmentName
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

        private String workgroupName = string.Empty;
        /// <summary>
        /// 工作名称
        /// </summary>
        [DataMember]
        public String WorkgroupName
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

        private String quickQuery = string.Empty;
        /// <summary>
        /// 快速查找，记忆符
        /// </summary>
        [DataMember]
        public String QuickQuery
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

        private String dutyId = string.Empty;
        /// <summary>
        /// 职位主键
        /// </summary>
        [DataMember]
        public String DutyId
        {
            get
            {
                return this.dutyId;
            }
            set
            {
                this.dutyId = value;
            }
        }

        private String identificationCode = string.Empty;
        /// <summary>
        /// 唯一身份Id
        /// </summary>
        [DataMember]
        public String IdentificationCode
        {
            get
            {
                return this.identificationCode;
            }
            set
            {
                this.identificationCode = value;
            }
        }

        private String iDCard = string.Empty;
        /// <summary>
        /// 身份证号码
        /// </summary>
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

        private String bankCode = string.Empty;
        /// <summary>
        /// 银行卡号
        /// </summary>
        [DataMember]
        public String BankCode
        {
            get
            {
                return this.bankCode;
            }
            set
            {
                this.bankCode = value;
            }
        }

        private string bankName = null;
        /// <summary>
        /// 开户行
        /// </summary>
        [DataMember]
        public string BankName
        {
            get
            {
                return this.bankName;
            }
            set
            {
                this.bankName = value;
            }
        }

        private string bankUserName = null;
        /// <summary>
        /// 开户行姓名
        /// </summary>
        [DataMember]
        public string BankUserName
        {
            get
            {
                return this.bankUserName;
            }
            set
            {
                this.bankUserName = value;
            }
        }

        private String rewarCard = string.Empty;
        /// <summary>
        /// 奖金卡号
        /// </summary>
        [DataMember]
        public String RewarCard
        {
            get
            {
                return this.rewarCard;
            }
            set
            {
                this.rewarCard = value;
            }
        }

        private String medicalCard = string.Empty;
        /// <summary>
        /// 医疗卡号
        /// </summary>
        [DataMember]
        public String MedicalCard
        {
            get
            {
                return this.medicalCard;
            }
            set
            {
                this.medicalCard = value;
            }
        }

        private String unionMember = string.Empty;
        /// <summary>
        /// 工会证号
        /// </summary>
        [DataMember]
        public String UnionMember
        {
            get
            {
                return this.unionMember;
            }
            set
            {
                this.unionMember = value;
            }
        }

        private String email = string.Empty;
        /// <summary>
        /// 电子邮件
        /// </summary>
        [DataMember]
        public String Email
        {
            get
            {
                return this.email;
            }
            set
            {
                this.email = value;
            }
        }

        private String mobile = string.Empty;
        /// <summary>
        /// 手机
        /// </summary>
        [DataMember]
        public String Mobile
        {
            get
            {
                return this.mobile;
            }
            set
            {
                this.mobile = value;
            }
        }

        private String shortNumber = string.Empty;
        /// <summary>
        /// 短号
        /// </summary>
        [DataMember]
        public String ShortNumber
        {
            get
            {
                return this.shortNumber;
            }
            set
            {
                this.shortNumber = value;
            }
        }

        private String telephone = string.Empty;
        /// <summary>
        /// 电话
        /// </summary>
        [DataMember]
        public String Telephone
        {
            get
            {
                return this.telephone;
            }
            set
            {
                this.telephone = value;
            }
        }

        private String qq = string.Empty;
        /// <summary>
        /// QQ号码
        /// </summary>
        [DataMember]
        public String QQ
        {
            get
            {
                return this.qq;
            }
            set
            {
                this.qq = value;
            }
        }

        private String officePhone = string.Empty;
        /// <summary>
        /// 办公电话
        /// </summary>
        [DataMember]
        public String OfficePhone
        {
            get
            {
                return this.officePhone;
            }
            set
            {
                this.officePhone = value;
            }
        }

        private String extension = string.Empty;
        /// <summary>
        /// 分机号码
        /// </summary>
        [DataMember]
        public String Extension
        {
            get
            {
                return this.extension;
            }
            set
            {
                this.extension = value;
            }
        }

        private String officePostCode = string.Empty;
        /// <summary>
        /// 办公邮编
        /// </summary>
        [DataMember]
        public String OfficePostCode
        {
            get
            {
                return this.officePostCode;
            }
            set
            {
                this.officePostCode = value;
            }
        }

        private String officeAddress = string.Empty;
        /// <summary>
        /// 办公地址
        /// </summary>
        [DataMember]
        public String OfficeAddress
        {
            get
            {
                return this.officeAddress;
            }
            set
            {
                this.officeAddress = value;
            }
        }

        private String officeFax = string.Empty;
        /// <summary>
        /// 办公传真
        /// </summary>
        [DataMember]
        public String OfficeFax
        {
            get
            {
                return this.officeFax;
            }
            set
            {
                this.officeFax = value;
            }
        }

        private String age = string.Empty;
        /// <summary>
        /// 年龄
        /// </summary>
        [DataMember]
        public String Age
        {
            get
            {
                return this.age;
            }
            set
            {
                this.age = value;
            }
        }

        private String birthday = string.Empty;
        /// <summary>
        /// 出生日期
        /// </summary>
        [DataMember]
        public String Birthday
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

        private String height = string.Empty;
        /// <summary>
        /// 身高
        /// </summary>
        [DataMember]
        public String Height
        {
            get
            {
                return this.height;
            }
            set
            {
                this.height = value;
            }
        }

        private String weight = string.Empty;
        /// <summary>
        /// 体重
        /// </summary>
        [DataMember]
        public String Weight
        {
            get
            {
                return this.weight;
            }
            set
            {
                this.weight = value;
            }
        }

        private String marriage = string.Empty;
        /// <summary>
        /// 婚姻情况
        /// </summary>
        [DataMember]
        public String Marriage
        {
            get
            {
                return this.marriage;
            }
            set
            {
                this.marriage = value;
            }
        }

        private String education = string.Empty;
        /// <summary>
        /// 最高学历
        /// </summary>
        [DataMember]
        public String Education
        {
            get
            {
                return this.education;
            }
            set
            {
                this.education = value;
            }
        }

        private String school = string.Empty;
        /// <summary>
        /// 毕业院校
        /// </summary>
        [DataMember]
        public String School
        {
            get
            {
                return this.school;
            }
            set
            {
                this.school = value;
            }
        }

        private String graduationDate = string.Empty;
        /// <summary>
        /// 毕业时间
        /// </summary>
        [DataMember]
        public String GraduationDate
        {
            get
            {
                return this.graduationDate;
            }
            set
            {
                this.graduationDate = value;
            }
        }

        private String major = string.Empty;
        /// <summary>
        /// 专业
        /// </summary>
        [DataMember]
        public String Major
        {
            get
            {
                return this.major;
            }
            set
            {
                this.major = value;
            }
        }

        private String degree = string.Empty;
        /// <summary>
        /// 最高学位
        /// </summary>
        [DataMember]
        public String Degree
        {
            get
            {
                return this.degree;
            }
            set
            {
                this.degree = value;
            }
        }

        private String titleId = string.Empty;
        /// <summary>
        /// 职称主键
        /// </summary>
        [DataMember]
        public String TitleId
        {
            get
            {
                return this.titleId;
            }
            set
            {
                this.titleId = value;
            }
        }

        private String titleDate = string.Empty;
        /// <summary>
        /// 职称评定日期
        /// </summary>
        [DataMember]
        public String TitleDate
        {
            get
            {
                return this.titleDate;
            }
            set
            {
                this.titleDate = value;
            }
        }

        private String titleLevel = string.Empty;
        /// <summary>
        /// 职称等级
        /// </summary>
        [DataMember]
        public String TitleLevel
        {
            get
            {
                return this.titleLevel;
            }
            set
            {
                this.titleLevel = value;
            }
        }

        private String workingDate = string.Empty;
        /// <summary>
        /// 工作时间
        /// </summary>
        [DataMember]
        public String WorkingDate
        {
            get
            {
                return this.workingDate;
            }
            set
            {
                this.workingDate = value;
            }
        }

        private String joinInDate = string.Empty;
        /// <summary>
        /// 加入本单位时间
        /// </summary>
        [DataMember]
        public String JoinInDate
        {
            get
            {
                return this.joinInDate;
            }
            set
            {
                this.joinInDate = value;
            }
        }

        private String homePostCode = string.Empty;
        /// <summary>
        /// 家庭住址邮编
        /// </summary>
        [DataMember]
        public String HomePostCode
        {
            get
            {
                return this.homePostCode;
            }
            set
            {
                this.homePostCode = value;
            }
        }

        private String homeAddress = string.Empty;
        /// <summary>
        /// 家庭住址
        /// </summary>
        [DataMember]
        public String HomeAddress
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

        private String homePhone = string.Empty;
        /// <summary>
        /// 住宅电话
        /// </summary>
        [DataMember]
        public String HomePhone
        {
            get
            {
                return this.homePhone;
            }
            set
            {
                this.homePhone = value;
            }
        }

        private String homeFax = string.Empty;
        /// <summary>
        /// 家庭传真
        /// </summary>
        [DataMember]
        public String HomeFax
        {
            get
            {
                return this.homeFax;
            }
            set
            {
                this.homeFax = value;
            }
        }

        private String carCode = string.Empty;
        /// <summary>
        /// 车牌号
        /// </summary>
        [DataMember]
        public String CarCode
        {
            get
            {
                return this.carCode;
            }
            set
            {
                this.carCode = value;
            }
        }

        private String province = string.Empty;
        /// <summary>
        /// 籍贯省
        /// </summary>
        [DataMember]
        public String Province
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

        private String city = string.Empty;
        /// <summary>
        /// 籍贯市
        /// </summary>
        [DataMember]
        public String City
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

        private String district = string.Empty;
        /// <summary>
        /// 籍贯区
        /// </summary>
        [DataMember]
        public String District
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

        private String currentProvince = string.Empty;
        /// <summary>
        /// 当前省
        /// </summary>
        [DataMember]
        public String CurrentProvince
        {
            get
            {
                return this.currentProvince;
            }
            set
            {
                this.currentProvince = value;
            }
        }

        private String currentCity = string.Empty;
        /// <summary>
        /// 当前市
        /// </summary>
        [DataMember]
        public String CurrentCity
        {
            get
            {
                return this.currentCity;
            }
            set
            {
                this.currentCity = value;
            }
        }

        private String currentDistrict = string.Empty;
        /// <summary>
        /// 当前区
        /// </summary>
        [DataMember]
        public String CurrentDistrict
        {
            get
            {
                return this.currentDistrict;
            }
            set
            {
                this.currentDistrict = value;
            }
        }

        private String nativePlace = string.Empty;
        /// <summary>
        /// 籍贯
        /// </summary>
        [DataMember]
        public String NativePlace
        {
            get
            {
                return this.nativePlace;
            }
            set
            {
                this.nativePlace = value;
            }
        }

        private String party = string.Empty;
        /// <summary>
        /// 政治面貌
        /// </summary>
        [DataMember]
        public String Party
        {
            get
            {
                return this.party;
            }
            set
            {
                this.party = value;
            }
        }

        private String nation = string.Empty;
        /// <summary>
        /// 国籍
        /// </summary>
        [DataMember]
        public String Nation
        {
            get
            {
                return this.nation;
            }
            set
            {
                this.nation = value;
            }
        }

        private String nationality = string.Empty;
        /// <summary>
        /// 民族
        /// </summary>
        [DataMember]
        public String Nationality
        {
            get
            {
                return this.nationality;
            }
            set
            {
                this.nationality = value;
            }
        }

        private String workingProperty = string.Empty;
        /// <summary>
        /// 工作性质
        /// </summary>
        [DataMember]
        public String WorkingProperty
        {
            get
            {
                return this.workingProperty;
            }
            set
            {
                this.workingProperty = value;
            }
        }

        private String competency = string.Empty;
        /// <summary>
        /// 职业资格
        /// </summary>
        [DataMember]
        public String Competency
        {
            get
            {
                return this.competency;
            }
            set
            {
                this.competency = value;
            }
        }

        private String emergencyContact = string.Empty;
        /// <summary>
        /// 紧急联系
        /// </summary>
        [DataMember]
        public String EmergencyContact
        {
            get
            {
                return this.emergencyContact;
            }
            set
            {
                this.emergencyContact = value;
            }
        }

        private DateTime? weddingday = null;
        /// <summary>
        /// 结婚日期
        /// </summary>
        [DataMember]
        public DateTime? Weddingday
        {
            get
            {
                return this.weddingday;
            }
            set
            {
                this.weddingday = value;
            }
        }

        private DateTime? divorce = null;
        /// <summary>
        /// 离婚日期
        /// </summary>
        [DataMember]
        public DateTime? Divorce
        {
            get
            {
                return this.divorce;
            }
            set
            {
                this.divorce = value;
            }
        }

        private DateTime? childbirthday1 = null;
        /// <summary>
        /// 第一个孩子出生时间
        /// </summary>
        [DataMember]
        public DateTime? Childbirthday1
        {
            get
            {
                return this.childbirthday1;
            }
            set
            {
                this.childbirthday1 = value;
            }
        }

        private DateTime? childbirthday2 = null;
        /// <summary>
        /// 第二个孩子出生时间
        /// </summary>
        [DataMember]
        public DateTime? Childbirthday2
        {
            get
            {
                return this.childbirthday2;
            }
            set
            {
                this.childbirthday2 = value;
            }
        }

        private int? isDimission = 0;
        /// <summary>
        /// 离职
        /// </summary>
        [DataMember]
        public int? IsDimission
        {
            get
            {
                return this.isDimission;
            }
            set
            {
                this.isDimission = value;
            }
        }

        private String dimissionDate = string.Empty;
        /// <summary>
        /// 离职日期
        /// </summary>
        [DataMember]
        public String DimissionDate
        {
            get
            {
                return this.dimissionDate;
            }
            set
            {
                this.dimissionDate = value;
            }
        }

        private String dimissionCause = string.Empty;
        /// <summary>
        /// 离职原因
        /// </summary>
        [DataMember]
        public String DimissionCause
        {
            get
            {
                return this.dimissionCause;
            }
            set
            {
                this.dimissionCause = value;
            }
        }

        private String dimissionWhither = string.Empty;
        /// <summary>
        /// 离职去向
        /// </summary>
        [DataMember]
        public String DimissionWhither
        {
            get
            {
                return this.dimissionWhither;
            }
            set
            {
                this.dimissionWhither = value;
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


        private String duty = string.Empty;
        /// <summary>
        /// 岗位名称
        /// </summary>
        [DataMember]
        public String Duty
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


        private String dutyCode = string.Empty;
        /// <summary>
        /// 岗位编号
        /// </summary>
        [DataMember]
        public String DutyCode
        {
            get
            {
                return this.dutyCode;
            }
            set
            {
                this.dutyCode = value;
            }
        }

        private String dutyType = string.Empty;
        /// <summary>
        /// 岗位类型
        /// </summary>
        [DataMember]
        public String DutyType
        {
            get
            {
                return this.dutyType;
            }
            set
            {
                this.dutyType = value;
            }
        }

        private String dutyLevel = string.Empty;
        /// <summary>
        /// 岗位等级
        /// </summary>
        [DataMember]
        public String DutyLevel
        {
            get
            {
                return this.dutyLevel;
            }
            set
            {
                this.dutyLevel = value;
            }
        }


        /// <summary>
        /// 从数据行读取
        /// </summary>
        /// <param name="dr">数据行</param>
		protected override BaseEntity GetFrom(IDataRow dr)
        {
            Id = BaseBusinessLogic.ConvertToInt(dr[BaseStaffEntity.FieldId]);
            UserId = BaseBusinessLogic.ConvertToInt(dr[BaseStaffEntity.FieldUserId]);
            UserName = BaseBusinessLogic.ConvertToString(dr[BaseStaffEntity.FieldUserName]);
            RealName = BaseBusinessLogic.ConvertToString(dr[BaseStaffEntity.FieldRealName]);
            Code = BaseBusinessLogic.ConvertToString(dr[BaseStaffEntity.FieldCode]);
            Gender = BaseBusinessLogic.ConvertToString(dr[BaseStaffEntity.FieldGender]);
            SubCompanyId = BaseBusinessLogic.ConvertToString(dr[BaseStaffEntity.FieldSubCompanyId]);
            CompanyId = BaseBusinessLogic.ConvertToString(dr[BaseStaffEntity.FieldCompanyId]);
            CompanyName = BaseBusinessLogic.ConvertToString(dr[BaseStaffEntity.FieldCompanyName]);
            DepartmentId = BaseBusinessLogic.ConvertToString(dr[BaseStaffEntity.FieldDepartmentId]);
            DepartmentName = BaseBusinessLogic.ConvertToString(dr[BaseStaffEntity.FieldDepartmentName]);
            WorkgroupId = BaseBusinessLogic.ConvertToString(dr[BaseStaffEntity.FieldWorkgroupId]);
            WorkgroupName = BaseBusinessLogic.ConvertToString(dr[BaseStaffEntity.FieldWorkgroupName]);
            QuickQuery = BaseBusinessLogic.ConvertToString(dr[BaseStaffEntity.FieldQuickQuery]);
            DutyId = BaseBusinessLogic.ConvertToString(dr[BaseStaffEntity.FieldDutyId]);
            IdentificationCode = BaseBusinessLogic.ConvertToString(dr[BaseStaffEntity.FieldIdentificationCode]);
            IDCard = BaseBusinessLogic.ConvertToString(dr[BaseStaffEntity.FieldIDCard]);
            BankCode = BaseBusinessLogic.ConvertToString(dr[BaseStaffEntity.FieldBankCode]);
            BankName = BaseBusinessLogic.ConvertToString(dr[BaseStaffEntity.FieldBankName]);
            RewarCard = BaseBusinessLogic.ConvertToString(dr[BaseStaffEntity.FieldRewarCard]);
            MedicalCard = BaseBusinessLogic.ConvertToString(dr[BaseStaffEntity.FieldMedicalCard]);
            UnionMember = BaseBusinessLogic.ConvertToString(dr[BaseStaffEntity.FieldUnionMember]);
            Email = BaseBusinessLogic.ConvertToString(dr[BaseStaffEntity.FieldEmail]);
            Mobile = BaseBusinessLogic.ConvertToString(dr[BaseStaffEntity.FieldMobile]);
            ShortNumber = BaseBusinessLogic.ConvertToString(dr[BaseStaffEntity.FieldShortNumber]);
            Telephone = BaseBusinessLogic.ConvertToString(dr[BaseStaffEntity.FieldTelephone]);
            QQ = BaseBusinessLogic.ConvertToString(dr[BaseStaffEntity.FieldQQ]);
            OfficePhone = BaseBusinessLogic.ConvertToString(dr[BaseStaffEntity.FieldOfficePhone]);
            Extension = BaseBusinessLogic.ConvertToString(dr[BaseStaffEntity.FieldExtension]);
            OfficePostCode = BaseBusinessLogic.ConvertToString(dr[BaseStaffEntity.FieldOfficePostCode]);
            OfficeAddress = BaseBusinessLogic.ConvertToString(dr[BaseStaffEntity.FieldOfficeAddress]);
            OfficeFax = BaseBusinessLogic.ConvertToString(dr[BaseStaffEntity.FieldOfficeFax]);
            Age = BaseBusinessLogic.ConvertToString(dr[BaseStaffEntity.FieldAge]);
            Birthday = BaseBusinessLogic.ConvertToString(dr[BaseStaffEntity.FieldBirthday]);
            Height = BaseBusinessLogic.ConvertToString(dr[BaseStaffEntity.FieldHeight]);
            Weight = BaseBusinessLogic.ConvertToString(dr[BaseStaffEntity.FieldWeight]);
            Marriage = BaseBusinessLogic.ConvertToString(dr[BaseStaffEntity.FieldMarriage]);
            Education = BaseBusinessLogic.ConvertToString(dr[BaseStaffEntity.FieldEducation]);
            School = BaseBusinessLogic.ConvertToString(dr[BaseStaffEntity.FieldSchool]);
            GraduationDate = BaseBusinessLogic.ConvertToString(dr[BaseStaffEntity.FieldGraduationDate]);
            Major = BaseBusinessLogic.ConvertToString(dr[BaseStaffEntity.FieldMajor]);
            Degree = BaseBusinessLogic.ConvertToString(dr[BaseStaffEntity.FieldDegree]);
            TitleId = BaseBusinessLogic.ConvertToString(dr[BaseStaffEntity.FieldTitleId]);
            TitleDate = BaseBusinessLogic.ConvertToString(dr[BaseStaffEntity.FieldTitleDate]);
            TitleLevel = BaseBusinessLogic.ConvertToString(dr[BaseStaffEntity.FieldTitleLevel]);
            WorkingDate = BaseBusinessLogic.ConvertToString(dr[BaseStaffEntity.FieldWorkingDate]);
            JoinInDate = BaseBusinessLogic.ConvertToString(dr[BaseStaffEntity.FieldJoinInDate]);
            HomePostCode = BaseBusinessLogic.ConvertToString(dr[BaseStaffEntity.FieldHomePostCode]);
            HomeAddress = BaseBusinessLogic.ConvertToString(dr[BaseStaffEntity.FieldHomeAddress]);
            HomePhone = BaseBusinessLogic.ConvertToString(dr[BaseStaffEntity.FieldHomePhone]);
            HomeFax = BaseBusinessLogic.ConvertToString(dr[BaseStaffEntity.FieldHomeFax]);
            CarCode = BaseBusinessLogic.ConvertToString(dr[BaseStaffEntity.FieldCarCode]);
            Province = BaseBusinessLogic.ConvertToString(dr[BaseStaffEntity.FieldProvince]);
            City = BaseBusinessLogic.ConvertToString(dr[BaseStaffEntity.FieldCity]);
            District = BaseBusinessLogic.ConvertToString(dr[BaseStaffEntity.FieldDistrict]);
            NativePlace = BaseBusinessLogic.ConvertToString(dr[BaseStaffEntity.FieldNativePlace]);
            CurrentProvince = BaseBusinessLogic.ConvertToString(dr[BaseStaffEntity.FieldCurrentProvince]);
            CurrentCity = BaseBusinessLogic.ConvertToString(dr[BaseStaffEntity.FieldCurrentCity]);
            CurrentDistrict = BaseBusinessLogic.ConvertToString(dr[BaseStaffEntity.FieldCurrentDistrict]);
            Party = BaseBusinessLogic.ConvertToString(dr[BaseStaffEntity.FieldParty]);
            Nation = BaseBusinessLogic.ConvertToString(dr[BaseStaffEntity.FieldNation]);
            Nationality = BaseBusinessLogic.ConvertToString(dr[BaseStaffEntity.FieldNationality]);
            WorkingProperty = BaseBusinessLogic.ConvertToString(dr[BaseStaffEntity.FieldWorkingProperty]);
            Competency = BaseBusinessLogic.ConvertToString(dr[BaseStaffEntity.FieldCompetency]);
            EmergencyContact = BaseBusinessLogic.ConvertToString(dr[BaseStaffEntity.FieldEmergencyContact]);
            Weddingday = BaseBusinessLogic.ConvertToNullableDateTime(dr[BaseStaffEntity.FieldWeddingday]);
            Divorce = BaseBusinessLogic.ConvertToNullableDateTime(dr[BaseStaffEntity.FieldDivorce]);
            Childbirthday1 = BaseBusinessLogic.ConvertToNullableDateTime(dr[BaseStaffEntity.FieldChildbirthday1]);
            Childbirthday2 = BaseBusinessLogic.ConvertToNullableDateTime(dr[BaseStaffEntity.FieldChildbirthday2]);
            IsDimission = BaseBusinessLogic.ConvertToInt(dr[BaseStaffEntity.FieldIsDimission]);
            DimissionDate = BaseBusinessLogic.ConvertToString(dr[BaseStaffEntity.FieldDimissionDate]);
            DimissionCause = BaseBusinessLogic.ConvertToString(dr[BaseStaffEntity.FieldDimissionCause]);
            DimissionWhither = BaseBusinessLogic.ConvertToString(dr[BaseStaffEntity.FieldDimissionWhither]);
            Enabled = BaseBusinessLogic.ConvertToInt(dr[BaseStaffEntity.FieldEnabled]);
            DeletionStateCode = BaseBusinessLogic.ConvertToInt(dr[BaseStaffEntity.FieldDeletionStateCode]);
            SortCode = BaseBusinessLogic.ConvertToInt(dr[BaseStaffEntity.FieldSortCode]);
            Description = BaseBusinessLogic.ConvertToString(dr[BaseStaffEntity.FieldDescription]);
            CreateOn = BaseBusinessLogic.ConvertToNullableDateTime(dr[BaseStaffEntity.FieldCreateOn]);
            CreateUserId = BaseBusinessLogic.ConvertToString(dr[BaseStaffEntity.FieldCreateUserId]);
            CreateBy = BaseBusinessLogic.ConvertToString(dr[BaseStaffEntity.FieldCreateBy]);
            ModifiedOn = BaseBusinessLogic.ConvertToNullableDateTime(dr[BaseStaffEntity.FieldModifiedOn]);
            ModifiedUserId = BaseBusinessLogic.ConvertToString(dr[BaseStaffEntity.FieldModifiedUserId]);
            ModifiedBy = BaseBusinessLogic.ConvertToString(dr[BaseStaffEntity.FieldModifiedBy]);

            Duty = BaseBusinessLogic.ConvertToString(dr[BaseStaffEntity.FieldDuty]);
            DutyCode = BaseBusinessLogic.ConvertToString(dr[BaseStaffEntity.FieldDutyCode]);
            DutyType = BaseBusinessLogic.ConvertToString(dr[BaseStaffEntity.FieldDutyType]);
            DutyLevel = BaseBusinessLogic.ConvertToString(dr[BaseStaffEntity.FieldDutyLevel]);
            // 获取扩展属性
            GetFromExpand(dr);
            return this;
        }

        ///<summary>
        /// 员工（职员）表
        ///</summary>
        [NonSerialized]
        public static string TableName = "BaseStaff";

        ///<summary>
        /// 主键
        ///</summary>
        [NonSerialized]
        public static string FieldId = "Id";

        ///<summary>
        /// 用户主键
        ///</summary>
        [NonSerialized]
        public static string FieldUserId = "UserId";

        ///<summary>
        /// 用户名(登录用)
        ///</summary>
        [NonSerialized]
        public static string FieldUserName = "UserName";

        ///<summary>
        /// 姓名
        ///</summary>
        [NonSerialized]
        public static string FieldRealName = "RealName";

        ///<summary>
        /// 编号,工号
        ///</summary>
        [NonSerialized]
        public static string FieldCode = "Code";

        ///<summary>
        /// 性别
        ///</summary>
        [NonSerialized]
        public static string FieldGender = "Gender";

        ///<summary>
        /// 分支机构主键，数据库中可以设置为int
        ///</summary>
        [NonSerialized]
        public static string FieldSubCompanyId = "SubCompanyId";

        ///<summary>
        /// 公司主键，数据库中可以设置为int
        ///</summary>
        [NonSerialized]
        public static string FieldCompanyId = "CompanyId";

        ///<summary>
        /// 公司名称
        ///</summary>
        [NonSerialized]
        public static string FieldCompanyName = "CompanyName";

        ///<summary>
        /// 部门主键，数据库中可以设置为int
        ///</summary>
        [NonSerialized]
        public static string FieldDepartmentId = "DepartmentId";

        ///<summary>
        /// 部门名称
        ///</summary>
        [NonSerialized]
        public static string FieldDepartmentName = "DepartmentName";

        ///<summary>
        /// 工作组主键，数据库中可以设置为int
        ///</summary>
        [NonSerialized]
        public static string FieldWorkgroupId = "WorkgroupId";

        ///<summary>
        /// 工作组名称
        ///</summary>
        [NonSerialized]
        public static string FieldWorkgroupName = "WorkgroupName";

        ///<summary>
        /// 快速查找，记忆符
        ///</summary>
        [NonSerialized]
        public static string FieldQuickQuery = "QuickQuery";

        ///<summary>
        /// 职位代码
        ///</summary>
        [NonSerialized]
        public static string FieldDutyId = "DutyId";

        ///<summary>
        /// 唯一身份Id
        ///</summary>
        [NonSerialized]
        public static string FieldIdentificationCode = "IdentificationCode";

        ///<summary>
        /// 身份证号码
        ///</summary>
        [NonSerialized]
        public static string FieldIDCard = "IDCard";

        ///<summary>
        /// 银行卡号
        ///</summary>
        [NonSerialized]
        public static string FieldBankCode = "BankCode";

        ///<summary>
        /// 开户行
        ///</summary>
        [NonSerialized]
        public static string FieldBankName = "BankName";

        ///<summary>
        /// 开户行姓名
        ///</summary>
        [NonSerialized]
        public static string FieldBankUserName = "BankUserName";

        ///<summary>
        /// 奖金卡号
        ///</summary>
        [NonSerialized]
        public static string FieldRewarCard = "RewarCard";

        ///<summary>
        /// 医疗卡号
        ///</summary>
        [NonSerialized]
        public static string FieldMedicalCard = "MedicalCard";

        ///<summary>
        /// 工会证号
        ///</summary>
        [NonSerialized]
        public static string FieldUnionMember = "UnionMember";

        ///<summary>
        /// 电子邮件
        ///</summary>
        [NonSerialized]
        public static string FieldEmail = "Email";

        ///<summary>
        /// 手机
        ///</summary>
        [NonSerialized]
        public static string FieldMobile = "Mobile";

        ///<summary>
        /// 短号
        ///</summary>
        [NonSerialized]
        public static string FieldShortNumber = "ShortNumber";

        ///<summary>
        /// 电话
        ///</summary>
        [NonSerialized]
        public static string FieldTelephone = "Telephone";

        ///<summary>
        /// QQ号码
        ///</summary>
        [NonSerialized]
        public static string FieldQQ = "QQ";

        ///<summary>
        /// 办公电话
        ///</summary>
        [NonSerialized]
        public static string FieldOfficePhone = "OfficePhone";

        ///<summary>
        /// 分机号码
        ///</summary>
        [NonSerialized]
        public static string FieldExtension = "Extension";

        ///<summary>
        /// 办公邮编
        ///</summary>
        [NonSerialized]
        public static string FieldOfficePostCode = "OfficePostCode";

        ///<summary>
        /// 办公地址
        ///</summary>
        [NonSerialized]
        public static string FieldOfficeAddress = "OfficeAddress";

        ///<summary>
        /// 办公传真
        ///</summary>
        [NonSerialized]
        public static string FieldOfficeFax = "OfficeFax";

        ///<summary>
        /// 年龄
        ///</summary>
        [NonSerialized]
        public static string FieldAge = "Age";

        ///<summary>
        /// 出生日期
        ///</summary>
        [NonSerialized]
        public static string FieldBirthday = "Birthday";

        ///<summary>
        /// 身高
        ///</summary>
        [NonSerialized]
        public static string FieldWeight = "Weight";

        ///<summary>
        /// 体重
        ///</summary>
        [NonSerialized]
        public static string FieldHeight = "Height";

        ///<summary>
        /// 婚姻情况
        ///</summary>
        [NonSerialized]
        public static string FieldMarriage = "Marriage";

        ///<summary>
        /// 最高学历
        ///</summary>
        [NonSerialized]
        public static string FieldEducation = "Education";

        ///<summary>
        /// 毕业院校
        ///</summary>
        [NonSerialized]
        public static string FieldSchool = "School";

        ///<summary>
        /// 毕业时间
        ///</summary>
        [NonSerialized]
        public static string FieldGraduationDate = "GraduationDate";

        ///<summary>
        /// 专业
        ///</summary>
        [NonSerialized]
        public static string FieldMajor = "Major";

        ///<summary>
        /// 最高学位
        ///</summary>
        [NonSerialized]
        public static string FieldDegree = "Degree";

        ///<summary>
        /// 职称主键
        ///</summary>
        [NonSerialized]
        public static string FieldTitleId = "TitleId";

        ///<summary>
        /// 职称评定日期
        ///</summary>
        [NonSerialized]
        public static string FieldTitleDate = "TitleDate";

        ///<summary>
        /// 职称等级
        ///</summary>
        [NonSerialized]
        public static string FieldTitleLevel = "TitleLevel";

        ///<summary>
        /// 工作时间
        ///</summary>
        [NonSerialized]
        public static string FieldWorkingDate = "WorkingDate";

        ///<summary>
        /// 加入本单位时间
        ///</summary>
        [NonSerialized]
        public static string FieldJoinInDate = "JoinInDate";

        ///<summary>
        /// 家庭住址邮编
        ///</summary>
        [NonSerialized]
        public static string FieldHomePostCode = "HomePostCode";

        ///<summary>
        /// 家庭住址
        ///</summary>
        [NonSerialized]
        public static string FieldHomeAddress = "HomeAddress";

        ///<summary>
        /// 住宅电话
        ///</summary>
        [NonSerialized]
        public static string FieldHomePhone = "HomePhone";

        ///<summary>
        /// 家庭传真
        ///</summary>
        [NonSerialized]
        public static string FieldHomeFax = "HomeFax";

        ///<summary>
        /// 车牌号
        ///</summary>
        [NonSerialized]
        public static string FieldCarCode = "CarCode";

        ///<summary>
        /// 籍贯省
        ///</summary>
        [NonSerialized]
        public static string FieldProvince = "Province";

        ///<summary>
        /// 籍贯市
        ///</summary>
        [NonSerialized]
        public static string FieldCity = "City";

        ///<summary>
        /// 籍贯区
        ///</summary>
        [NonSerialized]
        public static string FieldDistrict = "District";

        ///<summary>
        /// 籍贯
        ///</summary>
        [NonSerialized]
        public static string FieldNativePlace = "NativePlace";

        ///<summary>
        /// 当前省
        ///</summary>
        [NonSerialized]
        public static string FieldCurrentProvince = "CurrentProvince";

        ///<summary>
        /// 当前市
        ///</summary>
        [NonSerialized]
        public static string FieldCurrentCity = "CurrentCity";

        ///<summary>
        /// 当前区
        ///</summary>
        [NonSerialized]
        public static string FieldCurrentDistrict = "CurrentDistrict";

        ///<summary>
        /// 政治面貌
        ///</summary>
        [NonSerialized]
        public static string FieldParty = "Party";

        ///<summary>
        /// 国籍
        ///</summary>
        [NonSerialized]
        public static string FieldNation = "Nation";

        ///<summary>
        /// 民族
        ///</summary>
        [NonSerialized]
        public static string FieldNationality = "Nationality";

        ///<summary>
        /// 工作性质
        ///</summary>
        [NonSerialized]
        public static string FieldWorkingProperty = "WorkingProperty";

        ///<summary>
        /// 职业资格
        ///</summary>
        [NonSerialized]
        public static string FieldCompetency = "Competency";

        ///<summary>
        /// 紧急联系
        ///</summary>
        [NonSerialized]
        public static string FieldEmergencyContact = "EmergencyContact";

        ///<summary>
        /// 结婚日期
        ///</summary>
        [NonSerialized]
        public static string FieldWeddingday = "Weddingday";

        ///<summary>
        /// 离婚日期
        ///</summary>
        [NonSerialized]
        public static string FieldDivorce = "Divorce";

        ///<summary>
        /// 第一个孩子出生时间
        ///</summary>
        [NonSerialized]
        public static string FieldChildbirthday1 = "Childbirthday1";

        ///<summary>
        /// 第二个孩子出生时间
        ///</summary>
        [NonSerialized]
        public static string FieldChildbirthday2 = "Childbirthday2";

        ///<summary>
        /// 离职
        ///</summary>
        [NonSerialized]
        public static string FieldIsDimission = "IsDimission";

        ///<summary>
        /// 离职日期
        ///</summary>
        [NonSerialized]
        public static string FieldDimissionDate = "DimissionDate";

        ///<summary>
        /// 离职原因
        ///</summary>
        [NonSerialized]
        public static string FieldDimissionCause = "DimissionCause";

        ///<summary>
        /// 离职去向
        ///</summary>
        [NonSerialized]
        public static string FieldDimissionWhither = "DimissionWhither";

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

        ///<summary>
        /// 岗位名称
        ///</summary>
        [NonSerialized]
        public static string FieldDuty = "Duty";

        ///<summary>
        /// 岗位编号
        ///</summary>
        [NonSerialized]
        public static string FieldDutyCode = "DutyCode";

        ///<summary>
        /// 岗位序列
        ///</summary>
        [NonSerialized]
        public static string FieldDutyType = "DutyType";

        ///<summary>
        /// 岗位等级
        ///</summary>
        [NonSerialized]
        public static string FieldDutyLevel = "DutyLevel";
    }
}
