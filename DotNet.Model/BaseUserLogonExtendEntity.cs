//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd. 
//-----------------------------------------------------------------

using System;
using System.Runtime.Serialization;

namespace DotNet.Model
{
    using DotNet.Utilities;

    /// <summary>
    /// BaseAreaEntity
    /// 登录提醒表
    ///
    /// 修改记录
    ///
    ///		2015-01-23 版本：1.0 SongBiao 创建主键。
    ///
    /// <author>
    ///		<name>SongBiao</name>
    ///		<date>2015-01-23</date>
    /// </author>
    /// </summary>
    /// <summary>
    /// BaseUserLogonExtendEntity
    /// 用户登录的扩展表，账号登录方式，登录提醒方式
    /// 
    /// 修改记录
    /// 
    /// 2015-01-23 版本：1.0 JiRiGaLa 创建文件。
    /// 
    /// <author>
    ///     <name>JiRiGaLa</name>
    ///     <date>2015-01-23</date>
    /// </author>
    /// </summary>
    public partial class BaseUserLogonExtendEntity : BaseEntity
    {
        private Decimal id;
        /// <summary>
        /// 主键 用户ID
        /// </summary>
        [FieldDescription("主键", false)]
        [DataMember]
        public Decimal Id
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

        private Decimal? emailRemind = null;
        /// <summary>
        /// 登录邮件提醒
        /// </summary>
        [FieldDescription("登录邮件提醒")]
        [DataMember]
        public Decimal? EmailRemind
        {
            get
            {
                return emailRemind;
            }
            set
            {
                emailRemind = value;
            }
        }

        private Decimal? qrCodeLogon = null;
        /// <summary>
        /// 二维码登录
        /// </summary>
        [FieldDescription("二维码登录")]
        [DataMember]
        public Decimal? QrCodeLogon
        {
            get
            {
                return qrCodeLogon;
            }
            set
            {
                qrCodeLogon = value;
            }
        }

        private Decimal? jixinRemind = null;
        /// <summary>
        /// 登录吉信提醒
        /// </summary>
        [FieldDescription("登录吉信提醒")]
        [DataMember]
        public Decimal? JixinRemind
        {
            get
            {
                return jixinRemind;
            }
            set
            {
                jixinRemind = value;
            }
        }

        private Decimal? wechatRemind = null;
        /// <summary>
        /// 登录微信提醒
        /// </summary>
        [FieldDescription("登录微信提醒")]
        [DataMember]
        public Decimal? WechatRemind
        {
            get
            {
                return wechatRemind;
            }
            set
            {
                wechatRemind = value;
            }
        }

        private Decimal? dynamicCodeLogon = null;
        /// <summary>
        /// 动态码登录
        /// </summary>
        [FieldDescription("动态码登录")]
        [DataMember]
        public Decimal? DynamicCodeLogon
        {
            get
            {
                return dynamicCodeLogon;
            }
            set
            {
                dynamicCodeLogon = value;
            }
        }

        private Decimal? mobileRemind = null;
        /// <summary>
        /// 登录手机短信提醒
        /// </summary>
        [FieldDescription("登录手机短信提醒")]
        [DataMember]
        public Decimal? MobileRemind
        {
            get
            {
                return mobileRemind;
            }
            set
            {
                mobileRemind = value;
            }
        }

        private Decimal? usernamePasswordLogon = null;
        /// <summary>
        /// 用户名密码方式登录
        /// </summary>
        [FieldDescription("用户名密码方式登录")]
        [DataMember]
        public Decimal? UsernamePasswordLogon
        {
            get
            {
                return usernamePasswordLogon;
            }
            set
            {
                usernamePasswordLogon = value;
            }
        }

        /// <summary>
        /// 从数据行读取
        /// </summary>
        /// <param name="dr">数据行</param>
        protected override BaseEntity GetFrom(IDataRow dr)
        {
            Id = BaseBusinessLogic.ConvertToDecimal(dr[BaseUserLogonExtendEntity.FieldId]);
            EmailRemind = BaseBusinessLogic.ConvertToNullableDecimal(dr[BaseUserLogonExtendEntity.FieldEmailRemind]);
            QrCodeLogon = BaseBusinessLogic.ConvertToNullableDecimal(dr[BaseUserLogonExtendEntity.FieldQrCodeLogon]);
            JixinRemind = BaseBusinessLogic.ConvertToNullableDecimal(dr[BaseUserLogonExtendEntity.FieldJixinRemind]);
            WechatRemind = BaseBusinessLogic.ConvertToNullableDecimal(dr[BaseUserLogonExtendEntity.FieldWechatRemind]);
            DynamicCodeLogon = BaseBusinessLogic.ConvertToNullableDecimal(dr[BaseUserLogonExtendEntity.FieldDynamicCodeLogon]);
            MobileRemind = BaseBusinessLogic.ConvertToNullableDecimal(dr[BaseUserLogonExtendEntity.FieldMobileRemind]);
            UsernamePasswordLogon = BaseBusinessLogic.ConvertToNullableDecimal(dr[BaseUserLogonExtendEntity.FieldUsernamePasswordLogon]);
            // 获取扩展属性
            GetFromExpand(dr);
            return this;
        }

        ///<summary>
        /// 用户登录的扩展表，账号登录方式，登录提醒方式
        ///</summary>
        [FieldDescription("用户登录扩展表")]
        public static string TableName = "BASE_USER_LOGON_EXTEND";

        ///<summary>
        /// 主键 用户ID
        ///</summary>
        public static string FieldId = "Id";

        ///<summary>
        /// 登录邮件提醒
        ///</summary>
        public static string FieldEmailRemind = "EMAIL_REMIND";

        ///<summary>
        /// 二维码登录
        ///</summary>
        public static string FieldQrCodeLogon = "QR_CODE_LOGON";

        ///<summary>
        /// 登录吉信提醒
        ///</summary>
        public static string FieldJixinRemind = "JIXIN_REMIND";

        ///<summary>
        /// 登录微信提醒
        ///</summary>
        public static string FieldWechatRemind = "WECHAT_REMIND";

        ///<summary>
        /// 动态码登录
        ///</summary>
        public static string FieldDynamicCodeLogon = "DYNAMIC_CODE_LOGON";

        ///<summary>
        /// 登录手机短信提醒
        ///</summary>
        public static string FieldMobileRemind = "MOBILE_REMIND";

        ///<summary>
        /// 用户名密码方式登录
        ///</summary>
        public static string FieldUsernamePasswordLogon = "USERNAME_PASSWORD_LOGON";
    }
}