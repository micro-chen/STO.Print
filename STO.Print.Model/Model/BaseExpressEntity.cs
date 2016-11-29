//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2015 , STO TECH, Ltd. 
//-----------------------------------------------------------------

using System;

namespace STO.Print.Model
{
    using DotNet.Model;
    using DotNet.Utilities;

    /// <summary>
    /// BaseExpressEntity
    /// 
    /// 
    /// 修改纪录
    /// 
    /// 2015-08-23 版本：1.0 YanHengLian 创建文件。
    /// 
    /// <author>
    ///     <name>YanHengLian</name>
    ///     <date>2015-08-23</date>
    /// </author>
    /// </summary>
    public partial class BaseExpressEntity : BaseEntity
    {
        private Decimal id;
        /// <summary>
        /// ID
        /// </summary>
        [FieldDescription("ID")]
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

        private string createUserName = string.Empty;
        /// <summary>
        /// CREATEUSERNAME
        /// </summary>
        [FieldDescription("CREATEUSERNAME")]
        public string CreateUserName
        {
            get
            {
                return createUserName;
            }
            set
            {
                createUserName = value;
            }
        }

        private string cityId = string.Empty;
        /// <summary>
        /// CITY_ID
        /// </summary>
        [FieldDescription("CITY_ID")]
        public string CityId
        {
            get
            {
                return cityId;
            }
            set
            {
                cityId = value;
            }
        }

        private string modifiedUserName = string.Empty;
        /// <summary>
        /// MODIFIEDUSERNAME
        /// </summary>
        [FieldDescription("MODIFIEDUSERNAME")]
        public string ModifiedUserName
        {
            get
            {
                return modifiedUserName;
            }
            set
            {
                modifiedUserName = value;
            }
        }

        private DateTime? createOn = null;
        /// <summary>
        /// CREATEON
        /// </summary>
        [FieldDescription("CREATEON")]
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

        private string county = string.Empty;
        /// <summary>
        /// COUNTY
        /// </summary>
        [FieldDescription("COUNTY")]
        public string County
        {
            get
            {
                return county;
            }
            set
            {
                county = value;
            }
        }

        private string countyId = string.Empty;
        /// <summary>
        /// COUNTY_ID
        /// </summary>
        [FieldDescription("COUNTY_ID")]
        public string CountyId
        {
            get
            {
                return countyId;
            }
            set
            {
                countyId = value;
            }
        }

        private string remark = string.Empty;
        /// <summary>
        /// REMARK
        /// </summary>
        [FieldDescription("REMARK")]
        public string Remark
        {
            get
            {
                return remark;
            }
            set
            {
                remark = value;
            }
        }

        private string name = string.Empty;
        /// <summary>
        /// NAME
        /// </summary>
        [FieldDescription("NAME")]
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        private string shortName = string.Empty;
        /// <summary>
        /// SHORTNAME
        /// </summary>
        [FieldDescription("SHORTNAME")]
        public string ShortName
        {
            get
            {
                return shortName;
            }
            set
            {
                shortName = value;
            }
        }

        private DateTime? modifiedOn = null;
        /// <summary>
        /// MODIFIEDON
        /// </summary>
        [FieldDescription("MODIFIEDON")]
        public DateTime? ModifiedOn
        {
            get
            {
                return modifiedOn;
            }
            set
            {
                modifiedOn = value;
            }
        }

        private string provinceId = string.Empty;
        /// <summary>
        /// PROVINCE_ID
        /// </summary>
        [FieldDescription("PROVINCE_ID")]
        public string ProvinceId
        {
            get
            {
                return provinceId;
            }
            set
            {
                provinceId = value;
            }
        }

        private string city = string.Empty;
        /// <summary>
        /// CITY
        /// </summary>
        [FieldDescription("CITY")]
        public string City
        {
            get
            {
                return city;
            }
            set
            {
                city = value;
            }
        }

        private string address = string.Empty;
        /// <summary>
        /// ADDRESS
        /// </summary>
        [FieldDescription("ADDRESS")]
        public string Address
        {
            get
            {
                return address;
            }
            set
            {
                address = value;
            }
        }

        private string layer = string.Empty;
        /// <summary>
        /// LAYER
        /// </summary>
        [FieldDescription("LAYER")]
        public string Layer
        {
            get
            {
                return layer;
            }
            set
            {
                layer = value;
            }
        }

        /// <summary>
        /// 从数据行读取
        /// </summary>
        /// <param name="dr">数据行</param>
        protected override BaseEntity GetFrom(IDataRow dr)
        {
            GetFromExpand(dr);
            Id = BaseBusinessLogic.ConvertToDecimal(dr[BaseExpressEntity.FieldId]);
            CreateUserName = BaseBusinessLogic.ConvertToString(dr[BaseExpressEntity.FieldCreateUserName]);
            CityId = BaseBusinessLogic.ConvertToString(dr[BaseExpressEntity.FieldCityId]);
            ModifiedUserName = BaseBusinessLogic.ConvertToString(dr[BaseExpressEntity.FieldModifiedUserName]);
            CreateOn = BaseBusinessLogic.ConvertToNullableDateTime(dr[BaseExpressEntity.FieldCreateOn]);
            County = BaseBusinessLogic.ConvertToString(dr[BaseExpressEntity.FieldCounty]);
            CountyId = BaseBusinessLogic.ConvertToString(dr[BaseExpressEntity.FieldCountyId]);
            Remark = BaseBusinessLogic.ConvertToString(dr[BaseExpressEntity.FieldRemark]);
            Name = BaseBusinessLogic.ConvertToString(dr[BaseExpressEntity.FieldName]);
            ModifiedOn = BaseBusinessLogic.ConvertToNullableDateTime(dr[BaseExpressEntity.FieldModifiedOn]);
            ProvinceId = BaseBusinessLogic.ConvertToString(dr[BaseExpressEntity.FieldProvinceId]);
            City = BaseBusinessLogic.ConvertToString(dr[BaseExpressEntity.FieldCity]);
            Address = BaseBusinessLogic.ConvertToString(dr[BaseExpressEntity.FieldAddress]);
            Layer = BaseBusinessLogic.ConvertToString(dr[BaseExpressEntity.FieldLayer]);
            ShortName = BaseBusinessLogic.ConvertToString(dr[BaseExpressEntity.FiledShortName]);
            return this;
        }

        ///<summary>
        /// SHORTNAME
        ///</summary>
        public static string FiledShortName = "SHORTNAME";

        ///<summary>
        /// BASE_EXPRESS
        ///</summary>
        public static string TableName = "BASE_EXPRESS";

        ///<summary>
        /// ID
        ///</summary>
        public static string FieldId = "ID";

        ///<summary>
        /// CREATEUSERNAME
        ///</summary>
        public static string FieldCreateUserName = "CREATEUSERNAME";

        ///<summary>
        /// CITY_ID
        ///</summary>
        public static string FieldCityId = "CITY_ID";

        ///<summary>
        /// MODIFIEDUSERNAME
        ///</summary>
        public static string FieldModifiedUserName = "MODIFIEDUSERNAME";

        ///<summary>
        /// CREATEON
        ///</summary>
        public static string FieldCreateOn = "CREATEON";

        ///<summary>
        /// COUNTY
        ///</summary>
        public static string FieldCounty = "COUNTY";

        ///<summary>
        /// COUNTY_ID
        ///</summary>
        public static string FieldCountyId = "COUNTY_ID";

        ///<summary>
        /// REMARK
        ///</summary>
        public static string FieldRemark = "REMARK";

        ///<summary>
        /// NAME
        ///</summary>
        public static string FieldName = "NAME";

        ///<summary>
        /// MODIFIEDON
        ///</summary>
        public static string FieldModifiedOn = "MODIFIEDON";

        ///<summary>
        /// PROVINCE_ID
        ///</summary>
        public static string FieldProvinceId = "PROVINCE_ID";

        ///<summary>
        /// CITY
        ///</summary>
        public static string FieldCity = "CITY";

        ///<summary>
        /// ADDRESS
        ///</summary>
        public static string FieldAddress = "ADDRESS";

        ///<summary>
        /// LAYER
        ///</summary>
        public static string FieldLayer = "LAYER";
    }
}
