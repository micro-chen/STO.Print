//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd.  
//-----------------------------------------------------------------

using System;
using System.Configuration;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;

namespace DotNet.Utilities
{
    /// <summary>
    /// BaseUserInfo
    /// 用户核心基础信息
    /// 
    /// 修改记录
    /// 
    ///     2015.07.01 JiRiGaLa 版本：4.0 增加数字签名 Signature。
    ///     2012.03.16 zhangyi  版本：3.7 修改注释方式，可以在其他类调用的时候显示其参数中文名称。
    ///		2011.09.12 JiRiGaLa 版本：2.1 公司名称、部门名称、工作组名称进行重构。
    ///		2011.05.11 JiRiGaLa 版本：2.0 增加安全通讯用户名、密码。
    ///		2008.08.26 JiRiGaLa 版本：1.2 整理主键。
    ///		2006.05.03 JiRiGaLa 版本：1.1 添加到工程项目中。
    ///		2006.01.21 JiRiGaLa 版本：1.0 远程传递参数用属性才可以。
    ///		
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2015.07.01</date>
    /// </author> 
    /// </summary>
    [Serializable, DataContract]
    public partial class BaseUserInfo
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public BaseUserInfo()
        {
            this.ServiceUserName = BaseSystemInfo.ServiceUserName;
            this.ServicePassword = BaseSystemInfo.ServicePassword;
            // this.CurrentLanguage = BaseSystemInfo.CurrentLanguage;
			// 张祈璟20130619添加，为wcf取消延迟绑定
			GetSystemCode();
        }

        public string GetUserParameter(string url)
        {
            if (!string.IsNullOrEmpty(url))
            {
                url = url.Replace("{Ticks}", DateTime.Now.ToString("yyyyMMddHHmmss"));
                url = url.Replace("{UserCode}", this.Code);
                url = url.Replace("{UserName}", this.UserName);
                url = url.Replace("{NickName}", this.NickName);
                url = url.Replace("{Password}", this.Password);
                url = url.Replace("{UserId}", this.Id);
                url = url.Replace("{OpenId}", this.OpenId);
                url = url.Replace("{CompanyId}", this.CompanyId);
                url = url.Replace("{CompanyCode}", this.CompanyCode);
            }
            return url;
        }

        /// <summary>
        /// 获取当点登录的网址
        /// </summary>
        /// <param name="url">当前网址</param>
        /// <returns>单点登录网址</returns>
        public string GetUrl(string url, bool isURL = true)
        {
            if (!string.IsNullOrEmpty(url))
            {
                url = this.GetUserParameter(url);
                if (isURL && url.ToUpper().IndexOf("HTTP://") < 0)
                {
                    url = BaseSystemInfo.WebHost + url;
                }
            }
            return url;
        }

        private string id = string.Empty;
        /// <summary>
        /// 用户主键
        /// </summary>
        [DataMember]
        public virtual string Id
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

        private string serviceUserName = "Hairihan";
        /// <summary>
        /// 远程调用Service用户名（为了提高软件的安全性）
        /// </summary>
        [DataMember]
        public virtual string ServiceUserName
        {
            get
            {
                return this.serviceUserName;
            }
            set
            {
                this.serviceUserName = value;
            }
        }

        private string servicePassword = "Hairihan";
        /// <summary>
        /// 远程调用Service密码（为了提高软件的安全性）
        /// </summary>
        [DataMember]
        public virtual string ServicePassword
        {
            get
            {
                return this.servicePassword;
            }
            set
            {
                this.servicePassword = value;
            }
        }

        private string openId = string.Empty;
        /// <summary>
        /// 单点登录唯一识别标识
        /// </summary>
        [DataMember]
        public virtual string OpenId
        {
            get
            {
                return this.openId;
            }
            set
            {
                this.openId = value;
            }
        }

        private string userName = string.Empty;
        /// <summary>
        /// 用户用户名
        /// </summary>
        [DataMember]
        public virtual string UserName
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

        private string realname = string.Empty;
        /// <summary>
        /// 用户姓名
        /// </summary>
        [DataMember]
        public virtual string RealName
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

        private string nickName = string.Empty;
        /// <summary>
        /// 唯一用户名
        /// </summary>
        [DataMember]
        public virtual string NickName
        {
            get
            {
                return this.nickName;
            }
            set
            {
                this.nickName = value;
            }
        }

        private string code = string.Empty;
        /// <summary>
        /// 编号
        /// </summary>
        [DataMember]
        public virtual string Code
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

        private string companyId = null;
        /// <summary>
        /// 当前的组织结构公司主键
        /// </summary>
        [DataMember]
        public virtual string CompanyId
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
        /// 当前的组织结构公司编号
        /// </summary>
        [DataMember]
        public virtual string CompanyCode
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
        /// 当前的组织结构公司名称
        /// </summary>
        [DataMember]
        public virtual string CompanyName
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

        private string departmentId = null;
        /// <summary>
        /// 当前的组织结构部门主键
        /// </summary>
        [DataMember]
        public virtual string DepartmentId
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

        private string departmentCode = string.Empty;
        /// <summary>
        /// 当前的组织结构部门编号
        /// </summary>
        [DataMember]
        public virtual string DepartmentCode
        {
            get
            {
                return this.departmentCode;
            }
            set
            {
                this.departmentCode = value;
            }
        }

        private string departmentName = string.Empty;
        /// <summary>
        /// 当前的组织结构部门名称
        /// </summary>
        [DataMember]
        public virtual string DepartmentName
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

        private bool isAdministrator = false;
        /// <summary>
        /// 是否超级管理员
        /// </summary>
        [DataMember]
        public virtual bool IsAdministrator
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

        private bool identityAuthentication = false;
        /// <summary>
        /// 身份认证通过
        /// </summary>
        [DataMember]
        public virtual bool IdentityAuthentication
        {
            get
            {
                return this.identityAuthentication;
            }
            set
            {
                this.identityAuthentication = value;
            }
        }

        private string password = string.Empty;
        /// <summary>
        /// 密码
        /// </summary>
        [DataMember]
        public virtual string Password
        {
            get
            {
                return this.password;
            }
            set
            {
                this.password = value;
            }
        }

        private string ipAddress = string.Empty;
        /// <summary>
        /// IP地址
        /// </summary>
        [DataMember]
        public virtual string IPAddress
        {
            get
            {
                return this.ipAddress;
            }
            set
            {
                this.ipAddress = value;
            }
        }

        private string macAddress = string.Empty;
        /// <summary>
        /// MAC地址
        /// </summary>
        [DataMember]
        public virtual string MACAddress
        {
            get
            {
                return this.macAddress;
            }
            set
            {
                this.macAddress = value;
            }
        }

        private string systemCode = string.Empty;
        /// <summary>
        /// 这里是设置，读取哪个系统的菜单
        /// </summary>
        [DataMember]
        public virtual string SystemCode
        {
            get
            {
                return GetSystemCode();
            }
            set
            {
                systemCode = value;
            }
        }

        private string signature = string.Empty;
        /// <summary>
        /// 数字签名(防止篡改用户信息用)
        /// </summary>
        [DataMember]
        public virtual string Signature
        {
            get
            {
                return signature;
            }
            set
            {
                signature = value;
            }
        }

        private string GetSystemCode()
        {
            if (string.IsNullOrEmpty(systemCode))
            {
                if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["SystemCode"]))
                {
                    systemCode = ConfigurationManager.AppSettings["SystemCode"];
                }
                if (string.IsNullOrEmpty(systemCode))
                {
                    systemCode = BaseSystemInfo.SystemCode;
                }
                if (string.IsNullOrEmpty(systemCode))
                {
                    systemCode = "Base";
                }
            }
            return systemCode;
        }

        public void CloneData(BaseUserInfo userInfo)
        {
            this.systemCode = userInfo.SystemCode;
        }

        public string Serialize()
        {
            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            return javaScriptSerializer.Serialize(this);
        }

        public static BaseUserInfo Deserialize(string response)
        {
            BaseUserInfo userInfo = null;
            if (!string.IsNullOrEmpty(response))
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                userInfo = javaScriptSerializer.Deserialize<BaseUserInfo>(response);
            }
            return userInfo;
        }
    }
}