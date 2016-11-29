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
    /// BaseUserAddressEntity
    /// 用户送货地址表
    ///
    /// 修改记录
    ///
    ///		2010-07-15 版本：1.0 JiRiGaLa 创建主键。
    ///
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2010-07-15</date>
    /// </author>
    /// </summary>
    [Serializable, DataContract]
	public partial class BaseUserAddressEntity : BaseEntity
    {
        private string id = null;
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

        private string userId = null;
        /// <summary>
        /// 用户主键
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

        private string realname = null;
        /// <summary>
        /// 联系人（收货人）
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

        private string provinceId = null;
        /// <summary>
        /// 省主键
        /// </summary>
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

        private string areaId = null;
        /// <summary>
        /// 区/县主键
        /// </summary>
        [DataMember]
        public string AreaId
        {
            get
            {
                return this.areaId;
            }
            set
            {
                this.areaId = value;
            }
        }

        private string area = null;
        /// <summary>
        /// 区/县
        /// </summary>
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

        private string address = null;
        /// <summary>
        /// 街道地址
        /// </summary>
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

        private string postCode = null;
        /// <summary>
        /// 邮政编码
        /// </summary>
        [DataMember]
        public string PostCode
        {
            get
            {
                return this.postCode;
            }
            set
            {
                this.postCode = value;
            }
        }

        private string phone = null;
        /// <summary>
        /// 联系电话
        /// </summary>
        [DataMember]
        public string Phone
        {
            get
            {
                return this.phone;
            }
            set
            {
                this.phone = value;
            }
        }

        private string fax = null;
        /// <summary>
        /// 传真
        /// </summary>
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

        private string mobile = null;
        /// <summary>
        /// 手机
        /// </summary>
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

        private string email = null;
        /// <summary>
        /// 电子邮件
        /// </summary>
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

        private string deliverCategory = null;
        /// <summary>
        /// 送货方式
        /// </summary>
        [DataMember]
        public string DeliverCategory
        {
            get
            {
                return this.deliverCategory;
            }
            set
            {
                this.deliverCategory = value;
            }
        }

        private int? sortCode = 0;
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

        private string description = null;
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

        private string createUserId = null;
        /// <summary>
        /// 创建用户主键
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

        private string modifiedUserId = null;
        /// <summary>
        /// 修改用户主键
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

        /// <summary>
        /// 从数据行读取
        /// </summary>
        /// <param name="dr">数据行</param>
		protected override BaseEntity GetFrom(IDataRow dr)
        {
            Id = BaseBusinessLogic.ConvertToString(dr[BaseUserAddressEntity.FieldId]);
            UserId = BaseBusinessLogic.ConvertToString(dr[BaseUserAddressEntity.FieldUserId]);
            RealName = BaseBusinessLogic.ConvertToString(dr[BaseUserAddressEntity.FieldRealName]);
            ProvinceId = BaseBusinessLogic.ConvertToString(dr[BaseUserAddressEntity.FieldProvinceId]);
            Province = BaseBusinessLogic.ConvertToString(dr[BaseUserAddressEntity.FieldProvince]);
            CityId = BaseBusinessLogic.ConvertToString(dr[BaseUserAddressEntity.FieldCityId]);
            City = BaseBusinessLogic.ConvertToString(dr[BaseUserAddressEntity.FieldCity]);
            AreaId = BaseBusinessLogic.ConvertToString(dr[BaseUserAddressEntity.FieldAreaId]);
            Area = BaseBusinessLogic.ConvertToString(dr[BaseUserAddressEntity.FieldArea]);
            Address = BaseBusinessLogic.ConvertToString(dr[BaseUserAddressEntity.FieldAddress]);
            PostCode = BaseBusinessLogic.ConvertToString(dr[BaseUserAddressEntity.FieldPostCode]);
            Phone = BaseBusinessLogic.ConvertToString(dr[BaseUserAddressEntity.FieldPhone]);
            Fax = BaseBusinessLogic.ConvertToString(dr[BaseUserAddressEntity.FieldFax]);
            Mobile = BaseBusinessLogic.ConvertToString(dr[BaseUserAddressEntity.FieldMobile]);
            Email = BaseBusinessLogic.ConvertToString(dr[BaseUserAddressEntity.FieldEmail]);
            DeliverCategory = BaseBusinessLogic.ConvertToString(dr[BaseUserAddressEntity.FieldDeliverCategory]);
            SortCode = BaseBusinessLogic.ConvertToInt(dr[BaseUserAddressEntity.FieldSortCode]);
            DeletionStateCode = BaseBusinessLogic.ConvertToInt(dr[BaseUserAddressEntity.FieldDeletionStateCode]);
            Enabled = BaseBusinessLogic.ConvertToInt(dr[BaseUserAddressEntity.FieldEnabled]);
            Description = BaseBusinessLogic.ConvertToString(dr[BaseUserAddressEntity.FieldDescription]);
            CreateOn = BaseBusinessLogic.ConvertToNullableDateTime(dr[BaseUserAddressEntity.FieldCreateOn]);
            CreateUserId = BaseBusinessLogic.ConvertToString(dr[BaseUserAddressEntity.FieldCreateUserId]);
            CreateBy = BaseBusinessLogic.ConvertToString(dr[BaseUserAddressEntity.FieldCreateBy]);
            ModifiedOn = BaseBusinessLogic.ConvertToNullableDateTime(dr[BaseUserAddressEntity.FieldModifiedOn]);
            ModifiedUserId = BaseBusinessLogic.ConvertToString(dr[BaseUserAddressEntity.FieldModifiedUserId]);
            ModifiedBy = BaseBusinessLogic.ConvertToString(dr[BaseUserAddressEntity.FieldModifiedBy]);
            // 获取扩展属性
            GetFromExpand(dr);
            return this;
        }

        ///<summary>
        /// 用户送货地址表
        ///</summary>
        [NonSerialized]
        public static string TableName = "BaseUserAddress";

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
        /// 联系人（收货人）
        ///</summary>
        [NonSerialized]
        public static string FieldRealName = "RealName";

        ///<summary>
        /// 省主键
        ///</summary>
        [NonSerialized]
        public static string FieldProvinceId = "ProvinceId";

        ///<summary>
        /// 省
        ///</summary>
        [NonSerialized]
        public static string FieldProvince = "Province";

        ///<summary>
        /// 市主键
        ///</summary>
        [NonSerialized]
        public static string FieldCityId = "CityId";

        ///<summary>
        /// 市
        ///</summary>
        [NonSerialized]
        public static string FieldCity = "City";

        ///<summary>
        /// 区/县主键
        ///</summary>
        [NonSerialized]
        public static string FieldAreaId = "AreaId";

        ///<summary>
        /// 区/县
        ///</summary>
        [NonSerialized]
        public static string FieldArea = "Area";

        ///<summary>
        /// 街道地址
        ///</summary>
        [NonSerialized]
        public static string FieldAddress = "Address";

        ///<summary>
        /// 邮政编码
        ///</summary>
        [NonSerialized]
        public static string FieldPostCode = "PostCode";

        ///<summary>
        /// 联系电话
        ///</summary>
        [NonSerialized]
        public static string FieldPhone = "Phone";

        ///<summary>
        /// 传真
        ///</summary>
        [NonSerialized]
        public static string FieldFax = "Fax";

        ///<summary>
        /// 手机
        ///</summary>
        [NonSerialized]
        public static string FieldMobile = "Mobile";

        ///<summary>
        /// 电子邮件
        ///</summary>
        [NonSerialized]
        public static string FieldEmail = "Email";

        ///<summary>
        /// 送货方式
        ///</summary>
        [NonSerialized]
        public static string FieldDeliverCategory = "DeliverCategory";

        ///<summary>
        /// 排序码
        ///</summary>
        [NonSerialized]
        public static string FieldSortCode = "SortCode";

        ///<summary>
        /// 删除标记
        ///</summary>
        [NonSerialized]
        public static string FieldDeletionStateCode = "DeletionStateCode";

        ///<summary>
        /// 有效标志
        ///</summary>
        [NonSerialized]
        public static string FieldEnabled = "Enabled";

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
