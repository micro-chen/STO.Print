//-----------------------------------------------------------------------
// <copyright file="BaseLoginLogEntity.cs" company="Hairihan">
//     Copyright (C) 2016 , All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Runtime.Serialization;

namespace DotNet.Model
{
    using DotNet.Utilities;

    /// <summary>
    /// BaseLoginLogEntity
    /// 系统登录日志表
    /// 
    /// 修改记录
    /// 
    /// 2014-03-18 版本：1.0 JiRiGaLa 创建文件。
    /// 
    /// <author>
    ///     <name>JiRiGaLa</name>
    ///     <date>2014-03-18</date>
    /// </author>
    /// </summary>
    [Serializable, DataContract]
    public partial class BaseLoginLogEntity : BaseEntity
    {
        private string id = string.Empty;
        /// <summary>
        /// 主键
        /// </summary>
        [DataMember]
        public string Id
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

        private string systemCode = string.Empty;
        /// <summary>
        /// 子系统编号
        /// </summary>
        [DataMember]
        public string SystemCode
        {
            get
            {
                return systemCode;
            }
            set
            {
                systemCode = value;
            }
        }

        private string userId = string.Empty;
        /// <summary>
        /// 用户主键
        /// </summary>
        [DataMember]
        public string UserId
        {
            get
            {
                return userId;
            }
            set
            {
                userId = value;
            }
        }

        private string userName = string.Empty;
        /// <summary>
        /// 用户名
        /// </summary>
        [DataMember]
        public string UserName
        {
            get
            {
                return userName;
            }
            set
            {
                userName = value;
            }
        }

        private string realname = string.Empty;
        /// <summary>
        /// 姓名
        /// </summary>
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

        private string companyCode = null;
        /// <summary>
        /// 公司编号
        /// </summary>
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

        private string service = string.Empty;
        /// <summary>
        /// 服务
        /// </summary>
        [DataMember]
        public string Service
        {
            get
            {
                return this.service;
            }
            set
            {
                this.service = value;
            }
        }

        private long elapsedTicks = 0;
        /// <summary>
        /// 耗时
        /// </summary>
        [DataMember]
        public long ElapsedTicks
        {
            get
            {
                return this.elapsedTicks;
            }
            set
            {
                this.elapsedTicks = value;
            }
        }

        private string loginStatus = string.Empty;
        /// <summary>
        /// 登录状态
        /// </summary>
        [DataMember]
        public string LoginStatus
        {
            get
            {
                return loginStatus;
            }
            set
            {
                loginStatus = value;
            }
        }

        private int logLevel = 0;
        /// <summary>
        /// 登录级别（0，正常；1、注意；2，危险；3、攻击）
        /// </summary>
        [DataMember]
        public int LogLevel
        {
            get
            {
                return logLevel;
            }
            set
            {
                logLevel = value;
            }
        }

        private string iPAddress = string.Empty;
        /// <summary>
        /// IP地址
        /// </summary>
        [DataMember]
        public string IPAddress
        {
            get
            {
                return iPAddress;
            }
            set
            {
                iPAddress = value;
            }
        }

        private string mACAddress = string.Empty;
        /// <summary>
        /// MAC地址
        /// </summary>
        [DataMember]
        public string MACAddress
        {
            get
            {
                return mACAddress;
            }
            set
            {
                mACAddress = value;
            }
        }

        private string ipAddressName = string.Empty;
        /// <summary>
        /// IP地址名称
        /// </summary>
        [DataMember]
        public string IPAddressName
        {
            get
            {
                return ipAddressName;
            }
            set
            {
                ipAddressName = value;
                if (string.IsNullOrWhiteSpace(ipAddressName))
                {
                    ipAddressName = "未知";
                }
            }
        }

        /// <summary>
        /// 从数据行读取
        /// </summary>
        /// <param name="dr">数据行</param>
        protected override BaseEntity GetFrom(IDataRow dr)
        {
            Id = BaseBusinessLogic.ConvertToString(dr[BaseLoginLogEntity.FieldId]);
            CreateOn = BaseBusinessLogic.ConvertToNullableDateTime(dr[BaseLoginLogEntity.FieldCreateOn]);
            SystemCode = BaseBusinessLogic.ConvertToString(dr[BaseLoginLogEntity.FieldSystemCode]);
            UserId = BaseBusinessLogic.ConvertToString(dr[BaseLoginLogEntity.FieldUserId]);
            UserName = BaseBusinessLogic.ConvertToString(dr[BaseLoginLogEntity.FieldUserName]);
            RealName = BaseBusinessLogic.ConvertToString(dr[BaseLoginLogEntity.FieldRealName]);
            CompanyId = BaseBusinessLogic.ConvertToString(dr[BaseLoginLogEntity.FieldCompanyId]);
            CompanyCode = BaseBusinessLogic.ConvertToString(dr[BaseLoginLogEntity.FieldCompanyCode]);
            CompanyName = BaseBusinessLogic.ConvertToString(dr[BaseLoginLogEntity.FieldCompanyName]);
            // Service = BaseBusinessLogic.ConvertToString(dr[BaseLoginLogEntity.FieldService]);
            // ElapsedTicks = BaseBusinessLogic.ConvertToInt(dr[BaseLoginLogEntity.FieldElapsedTicks]);
            LoginStatus = BaseBusinessLogic.ConvertToString(dr[BaseLoginLogEntity.FieldLoginStatus]);
            MACAddress = BaseBusinessLogic.ConvertToString(dr[BaseLoginLogEntity.FieldMACAddress]);
            IPAddress = BaseBusinessLogic.ConvertToString(dr[BaseLoginLogEntity.FieldIPAddress]);
            IPAddressName = BaseBusinessLogic.ConvertToString(dr[BaseLoginLogEntity.FieldIPAddressName]);
            LogLevel = BaseBusinessLogic.ConvertToInt(dr[BaseLoginLogEntity.FieldLogLevel]);
            // 获取扩展属性
            GetFromExpand(dr);
            return this;
        }

        ///<summary>
        /// 系统登录日志表
        ///</summary>
        [NonSerialized]
        public static string TableName = "Base_LoginLog";

        ///<summary>
        /// 主键
        ///</summary>
        [NonSerialized]
        public static string FieldId = "Id";

        /// <summary>
        /// 哪个服务器上运行的？
        /// machine
        /// </summary>
        public static string FieldService = "Service";

        /// <summary>
        /// 耗时
        /// </summary>
        [NonSerialized]
        public static string FieldElapsedTicks = "ElapsedTicks";

        ///<summary>
        /// 系统编号
        ///</summary>
        [NonSerialized]
        public static string FieldSystemCode = "SystemCode";

        ///<summary>
        /// 创建日期
        ///</summary>
        [NonSerialized]
        public static string FieldCreateOn = "CreateOn";

        ///<summary>
        /// 用户主键
        ///</summary>
        [NonSerialized]
        public static string FieldUserId = "UserId";

        ///<summary>
        /// 用户名
        ///</summary>
        [NonSerialized]
        public static string FieldUserName = "UserName";

        ///<summary>
        /// 姓名
        ///</summary>
        [NonSerialized]
        public static string FieldRealName = "RealName";

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
        /// 登录状态
        ///</summary>
        [NonSerialized]
        public static string FieldLoginStatus = "LoginStatus";

        ///<summary>
        /// 登录级别（0，正常；1、注意；2，危险；3、攻击）
        ///</summary>
        [NonSerialized]
        public static string FieldLogLevel = "LogLevel";

        ///<summary>
        /// MAC地址
        ///</summary>
        [NonSerialized]
        public static string FieldMACAddress = "MACAddress";

        ///<summary>
        /// IP地址
        ///</summary>
        [NonSerialized]
        public static string FieldIPAddress = "IPAddress";

        ///<summary>
        /// IP地址名称
        ///</summary>
        [NonSerialized]
        public static string FieldIPAddressName = "IPAddressName";
    }
}
