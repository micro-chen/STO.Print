//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd. 
//-----------------------------------------------------------------

using System;
using System.Runtime.Serialization;

namespace DotNet.Model
{
    using DotNet.Utilities;

    /// <summary>
    /// BaseUserContactEntity
    /// 系统用户联系方式表
    ///
    /// 修改记录
    ///
    ///		2014-01-13 版本：1.0 JiRiGaLa 创建主键。
    ///
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2014-01-13</date>
    /// </author>
    /// </summary>
    [Serializable, DataContract]
    public partial class BaseUserContactEntity : BaseEntity
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

        private string mobile = string.Empty;
        /// <summary>
        /// 手机
        /// </summary>
        [FieldDescription("手机")]
        [DataMember]
        public string Mobile
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

        private int mobileValiated = 0;
        /// <summary>
        /// 手机验证通过
        /// </summary>
        [FieldDescription("手机验证通过")]
        [DataMember]
        public int MobileValiated
        {
            get
            {
                return this.mobileValiated;
            }
            set
            {
                this.mobileValiated = value;
            }
        }

        private DateTime? mobileVerificationDate = null;
        /// <summary>
        /// 手机验证日期
        /// </summary>
        [FieldDescription("手机验证日期")]
        [DataMember]
        public DateTime? MobileVerificationDate
        {
            get
            {
                return this.mobileVerificationDate;
            }
            set
            {
                this.mobileVerificationDate = value;
            }
        }

        private int showMobile = 1;
        /// <summary>
        /// 显示手机号码
        /// </summary>
        [FieldDescription("显示手机号码")]
        [DataMember]
        public int ShowMobile
        {
            get
            {
                return this.showMobile;
            }
            set
            {
                this.showMobile = value;
            }
        }

        private string shortNumber = string.Empty;
        /// <summary>
        /// 短号
        /// </summary>
        [FieldDescription("短号")]
        [DataMember]
        public string ShortNumber
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

        private string ww = string.Empty;
        /// <summary>
        /// 汪汪号码
        /// </summary>
        [FieldDescription("汪汪号码")]
        [DataMember]
        public string WW
        {
            get
            {
                return this.ww;
            }
            set
            {
                this.ww = value;
            }
        }

        private string weChat = string.Empty;
        /// <summary>
        /// 微信号码
        /// </summary>
        [FieldDescription("微信号码")]
        [DataMember]
        public string WeChat
        {
            get
            {
                return this.weChat;
            }
            set
            {
                this.weChat = value;
            }
        }

        private string weChatOpenId = string.Empty;
        /// <summary>
        /// 微信识别码
        /// </summary>
        [FieldDescription("微信识别码")]
        [DataMember]
        public string WeChatOpenId
        {
            get
            {
                return this.weChatOpenId;
            }
            set
            {
                this.weChatOpenId = value;
            }
        }

        private int weChatValiated = 0;
        /// <summary>
        /// 微信号码验证通过
        /// </summary>
        [FieldDescription("微信号码验证通过")]
        [DataMember]
        public int WeChatValiated
        {
            get
            {
                return this.weChatValiated;
            }
            set
            {
                this.weChatValiated = value;
            }
        }

        private string yiXin = string.Empty;
        /// <summary>
        /// 易信号码
        /// </summary>
        [FieldDescription("易信号码")]
        [DataMember]
        public string YiXin
        {
            get
            {
                return this.yiXin;
            }
            set
            {
                this.yiXin = value;
            }
        }

        private int yiXinValiated = 0;
        /// <summary>
        /// 易信号码验证通过
        /// </summary>
        [FieldDescription("易信号码验证通过")]
        [DataMember]
        public int YiXinValiated
        {
            get
            {
                return this.yiXinValiated;
            }
            set
            {
                this.yiXinValiated = value;
            }
        }

        private string telephone = string.Empty;
        /// <summary>
        /// 电话号码
        /// </summary>
        [FieldDescription("电话号码")]
        [DataMember]
        public string Telephone
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

        private string extension = string.Empty;
        /// <summary>
        /// 分机号码
        /// </summary>
        [FieldDescription("分机号码")]
        [DataMember]
        public string Extension
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

        private string qq = null;
        /// <summary>
        /// QQ号码
        /// </summary>
        [FieldDescription("QQ号码")]
        [DataMember]
        public string QQ
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

        private string email = null;
        /// <summary>
        /// 电子邮件
        /// </summary>
        [FieldDescription("电子邮件")]
        [DataMember]
        public string Email
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

        private int emailValiated = 0;
        /// <summary>
        /// 电子邮箱验证通过
        /// </summary>
        [FieldDescription("电子邮箱验证通过")]
        [DataMember]
        public int EmailValiated
        {
            get
            {
                return this.emailValiated;
            }
            set
            {
                this.emailValiated = value;
            }
        }

        private string companyMail = null;
        /// <summary>
        /// 公司邮件
        /// </summary>
        [FieldDescription("电子邮件")]
        [DataMember]
        public string CompanyMail
        {
            get
            {
                return this.companyMail;
            }
            set
            {
                this.companyMail = value;
            }
        }

        private string yy = null;
        /// <summary>
        /// YY
        /// </summary>
        [FieldDescription("YY")]
        [DataMember]
        public string YY
        {
            get
            {
                return this.yy;
            }
            set
            {
                this.yy = value;
            }
        }

        private string emergencyContact = null;
        /// <summary>
        /// 紧急联系
        /// </summary>
        [FieldDescription("紧急联系")]
        [DataMember]
        public string EmergencyContact
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

        /// <summary>
        /// 从数据行读取
        /// </summary>
        /// <param name="dr">数据行</param>
        protected override BaseEntity GetFrom(IDataRow dr)
        {
            Id = BaseBusinessLogic.ConvertToString(dr[BaseUserEntity.FieldId]);
            // 2016-03-02 吉日嘎拉 防止程序出错，没有这个字段也可以正常运行
            if (dr.ContainsColumn(BaseUserLogOnEntity.FieldCompanyId))
            {
                CompanyId = BaseBusinessLogic.ConvertToString(dr[BaseUserLogOnEntity.FieldCompanyId]);
            }
            Mobile = BaseBusinessLogic.ConvertToString(dr[BaseUserContactEntity.FieldMobile]);
            MobileValiated = BaseBusinessLogic.ConvertToInt(dr[BaseUserContactEntity.FieldMobileValiated]);
            MobileVerificationDate = BaseBusinessLogic.ConvertToNullableDateTime(dr[BaseUserContactEntity.FieldMobileVerificationDate]);
            ShowMobile = BaseBusinessLogic.ConvertToInt(dr[BaseUserContactEntity.FieldShowMobile]);
            Telephone = BaseBusinessLogic.ConvertToString(dr[BaseUserContactEntity.FieldTelephone]);
            Extension = BaseBusinessLogic.ConvertToString(dr[BaseUserContactEntity.FieldExtension]);
            ShortNumber = BaseBusinessLogic.ConvertToString(dr[BaseUserContactEntity.FieldShortNumber]);
            WW = BaseBusinessLogic.ConvertToString(dr[BaseUserContactEntity.FieldWW]);
            QQ = BaseBusinessLogic.ConvertToString(dr[BaseUserContactEntity.FieldQQ]);
            WeChat = BaseBusinessLogic.ConvertToString(dr[BaseUserContactEntity.FieldWeChat]);
            WeChatOpenId = BaseBusinessLogic.ConvertToString(dr[BaseUserContactEntity.FieldWeChatOpenId]);
            WeChatValiated = BaseBusinessLogic.ConvertToInt(dr[BaseUserContactEntity.FieldWeChatValiated]);
            YiXin = BaseBusinessLogic.ConvertToString(dr[BaseUserContactEntity.FieldYiXin]);
            YiXinValiated = BaseBusinessLogic.ConvertToInt(dr[BaseUserContactEntity.FieldYiXinValiated]);
            Email = BaseBusinessLogic.ConvertToString(dr[BaseUserContactEntity.FieldEmail]);
            EmailValiated = BaseBusinessLogic.ConvertToInt(dr[BaseUserContactEntity.FieldEmailValiated]);
            CompanyMail = BaseBusinessLogic.ConvertToString(dr[BaseUserContactEntity.FieldCompanyMail]);
            EmergencyContact = BaseBusinessLogic.ConvertToString(dr[BaseUserContactEntity.FieldEmergencyContact]);
            YY = BaseBusinessLogic.ConvertToString(dr[BaseUserContactEntity.FieldYY]);
            CreateOn = BaseBusinessLogic.ConvertToNullableDateTime(dr[BaseUserContactEntity.FieldCreateOn]);
            CreateUserId = BaseBusinessLogic.ConvertToString(dr[BaseUserContactEntity.FieldCreateUserId]);
            CreateBy = BaseBusinessLogic.ConvertToString(dr[BaseUserContactEntity.FieldCreateBy]);
            ModifiedOn = BaseBusinessLogic.ConvertToNullableDateTime(dr[BaseUserContactEntity.FieldModifiedOn]);
            ModifiedUserId = BaseBusinessLogic.ConvertToString(dr[BaseUserContactEntity.FieldModifiedUserId]);
            ModifiedBy = BaseBusinessLogic.ConvertToString(dr[BaseUserContactEntity.FieldModifiedBy]);
            // 获取扩展属性
            GetFromExpand(dr);
            return this;
        }

        ///<summary>
        /// 用户联系方式表
        ///</summary>
        [NonSerialized]
        [FieldDescription("用户联系方式表")]
        public static string TableName = "BaseUserContact";

        ///<summary>
        /// 主键
        ///</summary>
        [NonSerialized]
        public static string FieldId = "Id";

        ///<summary>
        /// 公司主键
        ///</summary>
        [NonSerialized]
        public static string FieldCompanyId = "CompanyId";

        ///<summary>
        /// 手机
        ///</summary>
        [NonSerialized]
        public static string FieldMobile = "Mobile";

        ///<summary>
        /// 分机号码
        ///</summary>
        [NonSerialized]
        public static string FieldExtension = "Extension";

        /// <summary>
        /// 手机是否验证通过
        /// </summary>
        [NonSerialized]
        public static string FieldMobileValiated = "MobileValiated";

        /// <summary>
        /// 手机验证日期
        /// </summary>
        [NonSerialized]
        public static string FieldMobileVerificationDate = "MobileVerificationDate";

        /// <summary>
        /// 显示手机号码
        /// </summary>
        [NonSerialized]
        public static string FieldShowMobile = "ShowMobile";

        ///<summary>
        /// 电话号码
        ///</summary>
        [NonSerialized]
        public static string FieldTelephone = "Telephone";

        ///<summary>
        /// 短号
        ///</summary>
        [NonSerialized]
        public static string FieldShortNumber = "ShortNumber";

        ///<summary>
        /// 汪汪号码
        ///</summary>
        [NonSerialized]
        public static string FieldWW = "WW";

        ///<summary>
        /// QQ号码
        ///</summary>
        [NonSerialized]
        public static string FieldQQ = "QQ";

        /// <summary>
        /// 微信号码
        /// </summary>
        [NonSerialized]
        public static string FieldWeChat = "WeChat";

        /// <summary>
        /// 微信识别码
        /// </summary>
        [NonSerialized]
        public static string FieldWeChatOpenId = "WeChat_OpenId";
        
        /// <summary>
        /// 微信是否验证通过
        /// </summary>
        [NonSerialized]
        public static string FieldWeChatValiated = "WeChatValiated";

        /// <summary>
        /// YY号码
        /// </summary>
        [NonSerialized]
        public static string FieldYY = "YY";

        /// <summary>
        /// 易信号码
        /// </summary>
        [NonSerialized]
        public static string FieldYiXin = "YiXin";

        /// <summary>
        /// 易信是否验证通过
        /// </summary>
        [NonSerialized]
        public static string FieldYiXinValiated = "YiXinValiated";

        ///<summary>
        /// 公司邮件
        ///</summary>
        [NonSerialized]
        public static string FieldCompanyMail = "CompanyMail";

        ///<summary>
        /// 紧急联系
        ///</summary>
        [NonSerialized]
        public static string FieldEmergencyContact = "EmergencyContact";
        
        ///<summary>
        /// 电子邮件
        ///</summary>
        [NonSerialized]
        public static string FieldEmail = "Email";

        /// <summary>
        /// 电子邮箱是否验证通过
        /// </summary>
        [NonSerialized]
        public static string FieldEmailValiated = "EmailValiated";

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
