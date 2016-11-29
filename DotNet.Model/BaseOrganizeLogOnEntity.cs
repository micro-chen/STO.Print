//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd. 
//-----------------------------------------------------------------

using System;
using System.Runtime.Serialization;

namespace DotNet.Model
{
	using DotNet.Utilities;

	/// <summary>
    /// BaseOrganizeLogOnEntity
	/// 登录信息表
	///
	/// 修改记录
	///
    ///		2016-03-24 版本：1.0 JiRiGaLa 创建主键。
	///
	/// <author>
	///		<name>JiRiGaLa</name>
    ///		<date>2016-03-24</date>
	/// </author>
	/// </summary>
	[Serializable, DataContract]
    public partial class BaseOrganizeLogOnEntity : BaseEntity
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

        private int? agree = 0;
		/// <summary>
		/// 赞成数
		/// </summary>
        [FieldDescription("赞成", false)]
		[DataMember]
		public int? Agree
		{
			get
			{
				return this.agree;
			}
			set
			{
				this.agree = value;
			}
		}

        private int? oppose = 0;
		/// <summary>
        /// 反对数
		/// </summary>
        [FieldDescription("反对", false)]
		[DataMember]
        public int? Oppose
		{
			get
			{
                return this.oppose;
			}
			set
			{
                this.oppose = value;
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

		/// <summary>
		/// 从数据行读取
		/// </summary>
		/// <param name="dr">数据行</param>
		protected override BaseEntity GetFrom(IDataRow dr)
		{
			Id = BaseBusinessLogic.ConvertToString(dr[BaseOrganizeLogOnEntity.FieldId]);
            Agree = BaseBusinessLogic.ConvertToInt(dr[BaseOrganizeLogOnEntity.FieldAgree]);
            Oppose = BaseBusinessLogic.ConvertToInt(dr[BaseOrganizeLogOnEntity.FieldOppose]);
            FirstVisit = BaseBusinessLogic.ConvertToNullableDateTime(dr[BaseOrganizeLogOnEntity.FieldFirstVisit]);
			LastVisit = BaseBusinessLogic.ConvertToNullableDateTime(dr[BaseOrganizeLogOnEntity.FieldLastVisit]);
			LogOnCount = BaseBusinessLogic.ConvertToInt(dr[BaseOrganizeLogOnEntity.FieldLogOnCount]);
			ShowCount = BaseBusinessLogic.ConvertToInt(dr[BaseOrganizeLogOnEntity.FieldShowCount]);
			UserOnLine = BaseBusinessLogic.ConvertToInt(dr[BaseOrganizeLogOnEntity.FieldUserOnLine]);
			IPAddress = BaseBusinessLogic.ConvertToString(dr[BaseOrganizeLogOnEntity.FieldIPAddress]);
			ModifiedOn = BaseBusinessLogic.ConvertToNullableDateTime(dr[BaseOrganizeEntity.FieldModifiedOn]);
            // 获取扩展属性
            GetFromExpand(dr);
			return this;
		}

		///<summary>
        /// 系统用户账户表
		///</summary>
		[NonSerialized]
        [FieldDescription("网点登录信息表")]
        public static string TableName = "BaseOrganizeLogOn";

		///<summary>
		/// 主键
		///</summary>
		[NonSerialized]
		public static string FieldId = "Id";

        ///<summary>
        /// 赞成数
        ///</summary>
        [NonSerialized]
        public static string FieldAgree = "Agree";

        ///<summary>
        /// 反对数
        ///</summary>
        [NonSerialized]
        public static string FieldOppose = "Oppose";

        ///<summary>
		/// 第一次访问时间
		///</summary>
		[NonSerialized]
		public static string FieldFirstVisit = "FirstVisit";

		///<summary>
		/// 最后访问时间
		///</summary>
		[NonSerialized]
		public static string FieldLastVisit = "LastVisit";

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
		/// 在线状态
		///</summary>
		[NonSerialized]
		public static string FieldUserOnLine = "UserOnLine";

		///<summary>
		/// 登录IP地址
		///</summary>
		[NonSerialized]
		public static string FieldIPAddress = "IPAddress";

        ///<summary>
        /// 修改日期
        ///</summary>
        [NonSerialized]
        [FieldDescription("修改日期")]
        public static string FieldModifiedOn = "ModifiedOn";
	}
}
