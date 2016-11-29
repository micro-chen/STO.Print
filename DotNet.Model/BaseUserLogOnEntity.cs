//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd. 
//-----------------------------------------------------------------

using System;
using System.Runtime.Serialization;

namespace DotNet.Model
{
	using DotNet.Utilities;

	/// <summary>
	/// BaseUserLogOnEntity
	/// 系统用户登录信息表
	///
	/// 修改记录
	///
    ///		2015-07-06 版本：2.1 JiRiGaLa 需要修改密码 NeedModifyPassword 的功能实现。
    ///		2014-06-26 版本：2.0 JiRiGaLa 密码盐、OpenId过期时间设置。
	///		2013-04-21 版本：1.0 JiRiGaLa 创建主键。
	///
	/// <author>
	///		<name>JiRiGaLa</name>
    ///		<date>2015-07-06</date>
	/// </author>
	/// </summary>
	[Serializable, DataContract]
	public partial class BaseUserLogOnEntity : BaseEntity
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

		private string userPassword = null;
		/// <summary>
		/// 用户密码
		/// </summary>
        [FieldDescription("用户密码", false)]
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

        private string salt = string.Empty;
        /// <summary>
        /// 密码盐
        /// </summary>
        [FieldDescription("密码盐", false)]
        [DataMember]
        public string Salt
        {
            get
            {
                return this.salt;
            }
            set
            {
                this.salt = value;
            }
        }

		private string openId = Guid.NewGuid().ToString("N");
		/// <summary>
		/// 当点登录标示
		/// </summary>
        [FieldDescription("当点登录标示", false)]
		[DataMember]
		public string OpenId
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

		private DateTime? openIdTimeout = null;
		/// <summary>
		/// OpenId过期时间
		/// </summary>
        [FieldDescription("OpenId过期时间", false)]
		[DataMember]
		public DateTime? OpenIdTimeout
		{
			get
			{
				return this.openIdTimeout;
			}
			set
			{
				this.openIdTimeout = value;
			}
		}

		private string systemCode = string.Empty;
		/// <summary>
		/// 系统编号
		/// </summary>
        [FieldDescription("系统编号", false)]
		[DataMember]
		public string SystemCode
		{
			get
			{
				return this.systemCode;
			}
			set
			{
				this.systemCode = value;
			}
		}

		private string verificationCode = null;
		/// <summary>
		/// 验证码
		/// </summary>
        [FieldDescription("验证码", false)]
		[DataMember]
		public string VerificationCode
		{
			get
			{
				return this.verificationCode;
			}
			set
			{
				this.verificationCode = value;
			}
		}

		private DateTime? changePasswordDate = null;
		/// <summary>
		/// 最后修改密码日期
		/// </summary>
        [FieldDescription("最后修改密码日期", false)]
		[DataMember]
		public DateTime? ChangePasswordDate
		{
			get
			{
				return this.changePasswordDate;
			}
			set
			{
				this.changePasswordDate = value;
			}
		}

		private DateTime? allowStartTime = null;
		/// <summary>
		/// 允许登录时间开始
		/// </summary>
        [FieldDescription("允许登录时间开始", false)]
		[DataMember]
		public DateTime? AllowStartTime
		{
			get
			{
				return this.allowStartTime;
			}
			set
			{
				this.allowStartTime = value;
			}
		}

		private DateTime? allowEndTime = null;
		/// <summary>
		/// 允许登录时间结束
		/// </summary>
        [FieldDescription("允许登录时间结束", false)]
		[DataMember]
		public DateTime? AllowEndTime
		{
			get
			{
				return this.allowEndTime;
			}
			set
			{
				this.allowEndTime = value;
			}
		}

		private DateTime? lockStartDate = null;
		/// <summary>
		/// 暂停用户开始日期
		/// </summary>
        [FieldDescription("暂停用户开始日期", false)]
		[DataMember]
		public DateTime? LockStartDate
		{
			get
			{
				return this.lockStartDate;
			}
			set
			{
				this.lockStartDate = value;
			}
		}

		private DateTime? lockEndDate = null;
		/// <summary>
		/// 暂停用户结束日期
		/// </summary>
        [FieldDescription("暂停用户结束日期", false)]
		[DataMember]
		public DateTime? LockEndDate
		{
			get
			{
				return this.lockEndDate;
			}
			set
			{
				this.lockEndDate = value;
			}
		}

		private DateTime? firstVisit = null;
		/// <summary>
		/// 第一次访问时间
		/// </summary>
        [FieldDescription("第一次访问时间", false)]
		[DataMember]
		public DateTime? FirstVisit
		{
			get
			{
				return this.firstVisit;
			}
			set
			{
				this.firstVisit = value;
			}
		}

		private DateTime? previousVisit = null;
		/// <summary>
		/// 上一次访问时间
		/// </summary>
        [FieldDescription("上一次访问时间", false)]
		[DataMember]
		public DateTime? PreviousVisit
		{
			get
			{
				return this.previousVisit;
			}
			set
			{
				this.previousVisit = value;
			}
		}

		private DateTime? lastVisit = null;
		/// <summary>
		/// 最后访问时间
		/// </summary>
        [FieldDescription("最后访问时间", false)]
		[DataMember]
		public DateTime? LastVisit
		{
			get
			{
				return this.lastVisit;
			}
			set
			{
				this.lastVisit = value;
			}
		}

		private int? multiUserLogin = 1;
		/// <summary>
        /// 允许有多用户同时登录
		/// </summary>
        [FieldDescription("允许有多用户同时登录")]
		[DataMember]
		public int? MultiUserLogin
		{
			get
			{
				return this.multiUserLogin;
			}
			set
			{
				this.multiUserLogin = value;
			}
		}

		private int? checkIPAddress = 0;
		/// <summary>
		/// 访问限制
		/// </summary>
        [FieldDescription("访问限制")]
		[DataMember]
		public int? CheckIPAddress
		{
			get
			{
				return this.checkIPAddress;
			}
			set
			{
				this.checkIPAddress = value;
			}
		}

		private int? logOnCount = 0;
		/// <summary>
		/// 登录次数
		/// </summary>
        [FieldDescription("登录次数", false)]
		[DataMember]
		public int? LogOnCount
		{
			get
			{
				return this.logOnCount;
			}
			set
			{
				this.logOnCount = value;
			}
		}

		private int? showCount = 0;
		/// <summary>
		/// 展示次数
		/// </summary>
        [FieldDescription("展示次数", false)]
		[DataMember]
		public int? ShowCount
		{
			get
			{
				return this.showCount;
			}
			set
			{
				this.showCount = value;
			}
		}

		private int? passwordErrorCount = 0;
		/// <summary>
		/// 密码连续错误次数
		/// </summary>
        [FieldDescription("密码连续错误次数", false)]
		[DataMember]
		public int? PasswordErrorCount
		{
			get
			{
				return this.passwordErrorCount;
			}
			set
			{
				this.passwordErrorCount = value;
			}
		}

		private int? userOnLine = 0;
		/// <summary>
		/// 在线状态
		/// </summary>
        [FieldDescription("在线状态", false)]
		[DataMember]
		public int? UserOnLine
		{
			get
			{
				return this.userOnLine;
			}
			set
			{
				this.userOnLine = value;
			}
		}

		private string iPAddress = null;
		/// <summary>
		/// IP地址
		/// </summary>
        [FieldDescription("IP地址")]
		[DataMember]
		public string IPAddress
		{
			get
			{
				return this.iPAddress;
			}
			set
			{
				this.iPAddress = value;
			}
		}

		private string iPAddressName = string.Empty;
		/// <summary>
		/// IP地址名称
		/// </summary>
        [FieldDescription("IP地址名称")]
		[DataMember]
		public string IPAddressName
		{
			get
			{
				return iPAddressName;
			}
			set
			{
				iPAddressName = value;
			}
		}

        private string computerName = null;
        /// <summary>
        /// 计算机名称
        /// </summary>
        [FieldDescription("计算机名称")]
        [DataMember]
        public string ComputerName
        {
            get
            {
                return this.computerName;
            }
            set
            {
                this.computerName = value;
            }
        }

		private string mACAddress = null;
		/// <summary>
		/// MAC地址
		/// </summary>
        [FieldDescription("MAC地址")]
		[DataMember]
		public string MACAddress
		{
			get
			{
				return this.mACAddress;
			}
			set
			{
				this.mACAddress = value;
			}
		}

		private string question = null;
		/// <summary>
		/// 密码提示问题
		/// </summary>
        [FieldDescription("密码提示问题")]
		[DataMember]
		public string Question
		{
			get
			{
				return this.question;
			}
			set
			{
				this.question = value;
			}
		}

		private string answerQuestion = null;
		/// <summary>
		/// 密码提示答案
		/// </summary>
        [FieldDescription("密码提示答案")]
		[DataMember]
		public string AnswerQuestion
		{
			get
			{
				return this.answerQuestion;
			}
			set
			{
				this.answerQuestion = value;
			}
		}

        private decimal? passwordStrength = -1;
        /// <summary>
        /// 密码强度
        /// </summary>
        [FieldDescription("密码强度", false)]
        [DataMember]
        public decimal? PasswordStrength
        {
            get
            {
                return this.passwordStrength;
            }
            set
            {
                this.passwordStrength = value;
            }
        }

		/*

		private string communicationPassword = null;
		/// <summary>
		/// 通讯密码
		/// </summary>
		public string CommunicationPassword
		{
			get
			{
				return this.communicationPassword;
			}
			set
			{
				this.communicationPassword = value;
			}
		}

		private string signedPassword = null;
		/// <summary>
		/// 数字签名密码
		/// </summary>
		public string SignedPassword
		{
			get
			{
				return this.signedPassword;
			}
			set
			{
				this.signedPassword = value;
			}
		}

		private string publicKey = null;
		/// <summary>
		/// 公钥
		/// </summary>
		public string PublicKey
		{
			get
			{
				return this.publicKey;
			}
			set
			{
				this.publicKey = value;
			}
		}
		
		*/

        private int  needModifyPassword = 0;
        /// <summary>
        /// 有效
        /// </summary>
        [FieldDescription("需修改密码")]
        [DataMember]
        public int NeedModifyPassword
        {
            get
            {
                return this.needModifyPassword;
            }
            set
            {
                this.needModifyPassword = value;
            }
        }
        

        private int enabled = 1;
        /// <summary>
        /// 有效
        /// </summary>
        [FieldDescription("有效")]
        [DataMember]
        public int Enabled
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
			Id = BaseBusinessLogic.ConvertToString(dr[BaseUserLogOnEntity.FieldId]);
            // 2016-03-02 吉日嘎拉 防止程序出错，没有这个字段也可以正常运行
            if (dr.ContainsColumn(BaseUserLogOnEntity.FieldCompanyId))
            {
                CompanyId = BaseBusinessLogic.ConvertToString(dr[BaseUserLogOnEntity.FieldCompanyId]);
            }
			ChangePasswordDate = BaseBusinessLogic.ConvertToNullableDateTime(dr[BaseUserLogOnEntity.FieldChangePasswordDate]);
			UserPassword = BaseBusinessLogic.ConvertToString(dr[BaseUserLogOnEntity.FieldUserPassword]);
            Enabled = BaseBusinessLogic.ConvertToInt(dr[BaseUserLogOnEntity.FieldEnabled]);
			OpenId = BaseBusinessLogic.ConvertToString(dr[BaseUserLogOnEntity.FieldOpenId]);
			OpenIdTimeout = BaseBusinessLogic.ConvertToNullableDateTime(dr[BaseUserLogOnEntity.FieldOpenIdTimeout]);
			Salt = BaseBusinessLogic.ConvertToString(dr[BaseUserLogOnEntity.FieldSalt]);
			/*
			CommunicationPassword = BaseBusinessLogic.ConvertToString(dr[BaseUserLogOnEntity.FieldCommunicationPassword]);
			SignedPassword = BaseBusinessLogic.ConvertToString(dr[BaseUserLogOnEntity.FieldSignedPassword]);
			PublicKey = BaseBusinessLogic.ConvertToString(dr[BaseUserLogOnEntity.FieldPublicKey]);
			*/
			AllowStartTime = BaseBusinessLogic.ConvertToNullableDateTime(dr[BaseUserLogOnEntity.FieldAllowStartTime]);
			AllowEndTime = BaseBusinessLogic.ConvertToNullableDateTime(dr[BaseUserLogOnEntity.FieldAllowEndTime]);
			SystemCode = BaseBusinessLogic.ConvertToString(dr[BaseUserLogOnEntity.FieldSystemCode]);
			LockStartDate = BaseBusinessLogic.ConvertToNullableDateTime(dr[BaseUserLogOnEntity.FieldLockStartDate]);
			LockEndDate = BaseBusinessLogic.ConvertToNullableDateTime(dr[BaseUserLogOnEntity.FieldLockEndDate]);
			FirstVisit = BaseBusinessLogic.ConvertToNullableDateTime(dr[BaseUserLogOnEntity.FieldFirstVisit]);
			PreviousVisit = BaseBusinessLogic.ConvertToNullableDateTime(dr[BaseUserLogOnEntity.FieldPreviousVisit]);
			LastVisit = BaseBusinessLogic.ConvertToNullableDateTime(dr[BaseUserLogOnEntity.FieldLastVisit]);
			MultiUserLogin = BaseBusinessLogic.ConvertToInt(dr[BaseUserLogOnEntity.FieldMultiUserLogin]);
			CheckIPAddress = BaseBusinessLogic.ConvertToInt(dr[BaseUserLogOnEntity.FieldCheckIPAddress]);
			LogOnCount = BaseBusinessLogic.ConvertToInt(dr[BaseUserLogOnEntity.FieldLogOnCount]);
			ShowCount = BaseBusinessLogic.ConvertToInt(dr[BaseUserLogOnEntity.FieldShowCount]);
			PasswordErrorCount = BaseBusinessLogic.ConvertToInt(dr[BaseUserLogOnEntity.FieldPasswordErrorCount]);
			UserOnLine = BaseBusinessLogic.ConvertToInt(dr[BaseUserLogOnEntity.FieldUserOnLine]);
			IPAddress = BaseBusinessLogic.ConvertToString(dr[BaseUserLogOnEntity.FieldIPAddress]);
			IPAddressName = BaseBusinessLogic.ConvertToString(dr[BaseUserLogOnEntity.FieldIPAddressName]);
			MACAddress = BaseBusinessLogic.ConvertToString(dr[BaseUserLogOnEntity.FieldMACAddress]);
            ComputerName = BaseBusinessLogic.ConvertToString(dr[BaseUserLogOnEntity.FieldComputerName]);
            Question = BaseBusinessLogic.ConvertToString(dr[BaseUserLogOnEntity.FieldQuestion]);
			AnswerQuestion = BaseBusinessLogic.ConvertToString(dr[BaseUserLogOnEntity.FieldAnswerQuestion]);
            PasswordStrength = BaseBusinessLogic.ConvertToDecimal(dr[BaseUserLogOnEntity.FieldPasswordStrength]);
            NeedModifyPassword = BaseBusinessLogic.ConvertToInt(dr[BaseUserLogOnEntity.FieldNeedModifyPassword]);
            CreateOn = BaseBusinessLogic.ConvertToNullableDateTime(dr[BaseUserLogOnEntity.FieldCreateOn]);
			CreateUserId = BaseBusinessLogic.ConvertToString(dr[BaseUserLogOnEntity.FieldCreateUserId]);
			CreateBy = BaseBusinessLogic.ConvertToString(dr[BaseUserLogOnEntity.FieldCreateBy]);
			ModifiedOn = BaseBusinessLogic.ConvertToNullableDateTime(dr[BaseUserLogOnEntity.FieldModifiedOn]);
			ModifiedUserId = BaseBusinessLogic.ConvertToString(dr[BaseUserLogOnEntity.FieldModifiedUserId]);
			ModifiedBy = BaseBusinessLogic.ConvertToString(dr[BaseUserLogOnEntity.FieldModifiedBy]);
			// 获取扩展属性
            GetFromExpand(dr);
			return this;
		}

		///<summary>
        /// 系统用户账户表
		///</summary>
		[NonSerialized]
        [FieldDescription("用户登录信息表")]
        public static string TableName = "BaseUserLogOn";

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
        /// 启用状态
        ///</summary>
        [NonSerialized]
        public static string FieldEnabled = "Enabled";

		///<summary>
		/// 用户密码
		///</summary>
		[NonSerialized]
		public static string FieldUserPassword = "UserPassword";

		///<summary>
		/// 验证码
		///</summary>
		[NonSerialized]
		public static string FieldVerificationCode = "VerificationCode";

		///<summary>
		/// 最后修改密码日期
		///</summary>
		[NonSerialized]
		public static string FieldChangePasswordDate = "ChangePasswordDate";

		///<summary>
		/// 允许登录时间开始
		///</summary>
		[NonSerialized]
		public static string FieldAllowStartTime = "AllowStartTime";

		///<summary>
		/// 允许登录时间结束
		///</summary>
		[NonSerialized]
		public static string FieldAllowEndTime = "AllowEndTime";

		///<summary>
		/// 暂停用户开始日期
		///</summary>
		[NonSerialized]
		public static string FieldLockStartDate = "LockStartDate";

		///<summary>
		/// 暂停用户结束日期
		///</summary>
		[NonSerialized]
		public static string FieldLockEndDate = "LockEndDate";

		///<summary>
		/// 系统编号
		///</summary>
		[NonSerialized]
		public static string FieldSystemCode = "SystemCode";

		///<summary>
		/// 第一次访问时间
		///</summary>
		[NonSerialized]
		public static string FieldFirstVisit = "FirstVisit";

		///<summary>
		/// 上一次访问时间
		///</summary>
		[NonSerialized]
		public static string FieldPreviousVisit = "PreviousVisit";

		///<summary>
		/// 最后访问时间
		///</summary>
		[NonSerialized]
		public static string FieldLastVisit = "LastVisit";

		///<summary>
		/// 允许同时有多用户登录
		///</summary>
		[NonSerialized]
		public static string FieldMultiUserLogin = "MultiUserLogin";

		///<summary>
		/// 登录次数
		///</summary>
		[NonSerialized]
		public static string FieldLogOnCount = "LogOnCount";

		///<summary>
		/// 展示次数
		///</summary>
		[NonSerialized]
		public static string FieldShowCount = "ShowCount";

		///<summary>
		/// 密码连续错误次数
		///</summary>
		[NonSerialized]
		public static string FieldPasswordErrorCount = "PasswordErrorCount";

		///<summary>
		/// 在线状态
		///</summary>
		[NonSerialized]
		public static string FieldUserOnLine = "UserOnLine";

		///<summary>
		/// 当点登录标示
		///</summary>
		[NonSerialized]
		public static string FieldOpenId = "OpenId";

		///<summary>
		/// OpenId超时时间
		///</summary>
		[NonSerialized]
		public static string FieldOpenIdTimeout = "OpenIdTimeout";

		///<summary>
		/// 密码盐
		///</summary>
		[NonSerialized]
		public static string FieldSalt = "Salt";

		///<summary>
		/// IP访问限制
		///</summary>
		[NonSerialized]
		public static string FieldCheckIPAddress = "CheckIPAddress";

		///<summary>
		/// 登录IP地址
		///</summary>
		[NonSerialized]
		public static string FieldIPAddress = "IPAddress";

		///<summary>
		/// IP地址名称
		///</summary>
		[NonSerialized]
		public static string FieldIPAddressName = "IPAddressName";

		///<summary>
		/// 登录MAC地址
		///</summary>
		[NonSerialized]
		public static string FieldMACAddress = "MACAddress";

        /// <summary>
        /// 计算机名称
        /// </summary>
        [NonSerialized]
        public static string FieldComputerName = "ComputerName";

		///<summary>
		/// 密码提示问题代码
		///</summary>
		[NonSerialized]
		public static string FieldQuestion = "Question";

		///<summary>
		/// 密码提示答案
		///</summary>
		[NonSerialized]
		public static string FieldAnswerQuestion = "AnswerQuestion";

		/*

		///<summary>
		/// 通讯密码
		///</summary>
		[NonSerialized]
		public static string FieldCommunicationPassword = "CommunicationPassword";

		///<summary>
		/// 数字签名密码
		///</summary>
		[NonSerialized]
		public static string FieldSignedPassword = "SignedPassword";

		///<summary>
		/// 公钥
		///</summary>
		[NonSerialized]
		public static string FieldPublicKey = "PublicKey";
		
		*/

        ///<summary>
        /// 需要修改密码
        ///</summary>
        [NonSerialized]
        public static string FieldNeedModifyPassword = "NeedModifyPassword";

        ///<summary>
        /// 密码强度级别
        ///</summary>
        [NonSerialized]
        public static string FieldPasswordStrength = "PasswordStrength";

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
