//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd.  
//-----------------------------------------------------------------

using System;
using System.Runtime.Serialization;

namespace DotNet.Model
{
    using DotNet.Utilities;

    /// <summary>
    /// BaseServicesLicenseEntity
    /// 接口调用设置定义
    ///
    /// 修改记录
    /// 
    ///		2015.12.24 版本：1.0 JiRiGaLa	添加。
    /// 
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2015.12.24</date>
    /// </author> 
    /// </summary>
    [Serializable, DataContract]
    public partial class BaseServicesLicenseEntity : BaseEntity
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
                return this.id;
            }
            set
            {
                this.id = value;
            }
        }
        
        private string userId = string.Empty;
        /// <summary>
        /// 参数主键
        /// </summary>
        [DataMember]
        public string UserId
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

        private string privateKey = null;
		/// <summary>
        /// 私钥
		/// </summary>
		public string PrivateKey
		{
			get
			{
                return this.privateKey;
			}
			set
			{
                this.privateKey = value;
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

        private DateTime? startDate = null;
        /// <summary>
        /// 开始生效日期
        /// </summary>
        [DataMember]
        public DateTime? StartDate
        {
            get
            {
                return this.startDate;
            }
            set
            {
                this.startDate = value;
            }
        }

        private DateTime? endDate = null;
        /// <summary>
        /// 结束生效日期
        /// </summary>
        [DataMember]
        public DateTime? EndDate
        {
            get
            {
                return this.endDate;
            }
            set
            {
                this.endDate = value;
            }
        }

        private bool enabled = true;
        /// <summary>
        /// 有效
        /// </summary>
        [DataMember]
        public bool Enabled
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
        /// 删除标志
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

        private string description = string.Empty;
        /// <summary>
        /// 备注
        /// </summary>
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

        private string createUserId = string.Empty;
        /// <summary>
        /// 创建者主键
        /// </summary>
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

        private string modifiedUserId = string.Empty;
        /// <summary>
        /// 最后修改者主键
        /// </summary>
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

        #region public BaseParameterEntity GetFrom(DataRow dr)
        /// <summary>
        /// 从数据行读取
        /// </summary>
        /// <param name="dr">数据行</param>
        /// <returns>BaseParameterEntity</returns>
		protected override BaseEntity GetFrom(IDataRow dr)
        {
            Id = BaseBusinessLogic.ConvertToString(dr[BaseServicesLicenseEntity.FieldId]);
            UserId = BaseBusinessLogic.ConvertToString(dr[BaseServicesLicenseEntity.FieldUserId]);
            PrivateKey = BaseBusinessLogic.ConvertToString(dr[BaseServicesLicenseEntity.FieldPrivateKey]);
            PublicKey = BaseBusinessLogic.ConvertToString(dr[BaseServicesLicenseEntity.FieldPublicKey]);
            StartDate = BaseBusinessLogic.ConvertToNullableDateTime(dr[BaseServicesLicenseEntity.FieldStartDate]);
            EndDate = BaseBusinessLogic.ConvertToNullableDateTime(dr[BaseServicesLicenseEntity.FieldEndDate]);
			Enabled = BaseBusinessLogic.ConvertIntToBoolean(dr[BaseServicesLicenseEntity.FieldEnabled]);
            Description = BaseBusinessLogic.ConvertToString(dr[BaseServicesLicenseEntity.FieldDescription]);
            CreateUserId = BaseBusinessLogic.ConvertToString(dr[BaseServicesLicenseEntity.FieldCreateUserId]);
            CreateOn = BaseBusinessLogic.ConvertToNullableDateTime(dr[BaseServicesLicenseEntity.FieldCreateOn]);
            CreateBy = BaseBusinessLogic.ConvertToString(dr[BaseServicesLicenseEntity.FieldCreateBy]);
            ModifiedUserId = BaseBusinessLogic.ConvertToString(dr[BaseServicesLicenseEntity.FieldModifiedUserId]);
            ModifiedOn = BaseBusinessLogic.ConvertToNullableDateTime(dr[BaseServicesLicenseEntity.FieldModifiedOn]);
            ModifiedBy = BaseBusinessLogic.ConvertToString(dr[BaseServicesLicenseEntity.FieldModifiedBy]);
            // 获取扩展属性
            GetFromExpand(dr);
            return this;
        }
        #endregion

        /// <summary>
        /// 表名
        /// </summary>
        [NonSerialized]
        public static string TableName = "BaseServicesLicense";

        /// <summary>
        /// 主键
        /// </summary>
        [NonSerialized]
        public static string FieldId = "Id";

        /// <summary>
        /// 参数主键
        /// </summary>
        [NonSerialized]
        public static string FieldUserId = "UserId";

        /// <summary>
        /// 私钥
        /// </summary>
        [NonSerialized]
        public static string FieldPrivateKey = "PrivateKey";

        /// <summary>
        /// 公钥
        /// </summary>
        [NonSerialized]
        public static string FieldPublicKey = "PublicKey";

        ///<summary>
        /// 开始生效日期
        ///</summary>
        [NonSerialized]
        public static string FieldStartDate = "StartDate";

        ///<summary>
        /// 结束生效日期
        ///</summary>
        [NonSerialized]
        public static string FieldEndDate = "EndDate";

        /// <summary>
        /// 有效性
        /// </summary>
        [NonSerialized]
        public static string FieldEnabled = "Enabled";

        /// <summary>
        /// 备注
        /// </summary>
        [NonSerialized]
        public static string FieldDescription = "Description";

        ///<summary>
        /// 删除标志
        ///</summary>
        [NonSerialized]
        public static string FieldDeletionStateCode = "DeletionStateCode";

        /// <summary>
        /// 创建者
        /// </summary>
        [NonSerialized]
        public static string FieldCreateBy = "CreateBy";

        /// <summary>
        /// 创建者主键
        /// </summary>
        [NonSerialized]
        public static string FieldCreateUserId = "CreateUserId";

        /// <summary>
        /// 创建时间
        /// </summary>
        [NonSerialized]
        public static string FieldCreateOn = "CreateOn";

        /// <summary>
        /// 最后修改者主键
        /// </summary>
        [NonSerialized]
        public static string FieldModifiedUserId = "ModifiedUserId";

        /// <summary>
        /// 最后修改者
        /// </summary>
        [NonSerialized]
        public static string FieldModifiedBy = "ModifiedBy";

        /// <summary>
        /// 最后修改时间
        /// </summary>
        [NonSerialized]
        public static string FieldModifiedOn = "ModifiedOn";
    }
}