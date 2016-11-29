//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2015 , STO TECH, Ltd. 
//-----------------------------------------------------------------

using System;

namespace STO.Print.Model
{
    using DotNet.Model;
    using DotNet.Utilities;

    /// <summary>
    /// ZtoUserEntity
    /// 
    /// 
    /// 修改纪录
    /// 
    /// 2015-08-11 版本：1.0 YangHengLian 创建文件。
    /// 
    /// <author>
    ///     <name>YangHengLian</name>
    ///     <date>2015-08-11</date>
    /// </author>
    /// </summary>
    public partial class ZtoUserEntity : BaseEntity
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

        private string isDefault = string.Empty;
        /// <summary>
        /// IsDefault
        /// </summary>
        [FieldDescription("IsDefault")]
        public string IsDefault
        {
            get
            {
                return isDefault;
            }
            set
            {
                isDefault = value;
            }
        }

        private string postcode = string.Empty;
        /// <summary>
        /// POSTCODE
        /// </summary>
        [FieldDescription("POSTCODE")]
        public string Postcode
        {
            get
            {
                return postcode;
            }
            set
            {
                postcode = value;
            }
        }

        private string realname = string.Empty;
        /// <summary>
        /// REALNAME
        /// </summary>
        [FieldDescription("REALNAME")]
        public string Realname
        {
            get
            {
                return realname;
            }
            set
            {
                realname = value;
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

        private string department = string.Empty;
        /// <summary>
        /// DEPARTMENT
        /// </summary>
        [FieldDescription("DEPARTMENT")]
        public string Department
        {
            get
            {
                return department;
            }
            set
            {
                department = value;
            }
        }

        private string province = string.Empty;
        /// <summary>
        /// PROVINCE
        /// </summary>
        [FieldDescription("PROVINCE")]
        public string Province
        {
            get
            {
                return province;
            }
            set
            {
                province = value;
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

        private string issendorreceive = string.Empty;
        /// <summary>
        /// 是否发件人 1：发件人 0：收件人
        /// </summary>
        [FieldDescription("ISSENDORRECEIVE")]
        public string Issendorreceive
        {
            get
            {
                return issendorreceive;
            }
            set
            {
                issendorreceive = value;
            }
        }

        private string telePhone = string.Empty;
        /// <summary>
        /// TELEPHONE
        /// </summary>
        [FieldDescription("TELEPHONE")]
        public string TelePhone
        {
            get
            {
                return telePhone;
            }
            set
            {
                telePhone = value;
            }
        }

        private string mobile = string.Empty;
        /// <summary>
        /// Mobile
        /// </summary>
        [FieldDescription("MOBILE")]
        public string Mobile
        {
            get
            {
                return mobile;
            }
            set
            {
                mobile = value;
            }
        }

        private string company = string.Empty;
        /// <summary>
        /// COMPANY
        /// </summary>
        [FieldDescription("COMPANY")]
        public string Company
        {
            get
            {
                return company;
            }
            set
            {
                company = value;
            }
        }

        /// <summary>
        /// 从数据行读取
        /// </summary>
        /// <param name="dr">数据行</param>
        protected override BaseEntity GetFrom(IDataRow dr)
        {
            GetFromExpand(dr);
            if (dr.ContainsColumn(ZtoUserEntity.FieldId))
            {
                Id = BaseBusinessLogic.ConvertToDecimal(dr[ZtoUserEntity.FieldId]);
            }
            if (dr.ContainsColumn(ZtoUserEntity.FieldPostcode))
            {
                Postcode = BaseBusinessLogic.ConvertToString(dr[ZtoUserEntity.FieldPostcode]);
            }
            if (dr.ContainsColumn(ZtoUserEntity.FieldRealname))
            {
                Realname = BaseBusinessLogic.ConvertToString(dr[ZtoUserEntity.FieldRealname]);
            }
            if (dr.ContainsColumn(ZtoUserEntity.FieldCreateUserName))
            {
                CreateUserName = BaseBusinessLogic.ConvertToString(dr[ZtoUserEntity.FieldCreateUserName]);
            }
            if (dr.ContainsColumn(ZtoUserEntity.FieldCityId))
            {
                CityId = BaseBusinessLogic.ConvertToString(dr[ZtoUserEntity.FieldCityId]);
            }
            if (dr.ContainsColumn(ZtoUserEntity.FieldModifiedUserName))
            {
                ModifiedUserName = BaseBusinessLogic.ConvertToString(dr[ZtoUserEntity.FieldModifiedUserName]);
            }
            if (dr.ContainsColumn(ZtoUserEntity.FieldCreateOn))
            {
                CreateOn = BaseBusinessLogic.ConvertToNullableDateTime(dr[ZtoUserEntity.FieldCreateOn]);
            }
            if (dr.ContainsColumn(ZtoUserEntity.FieldDepartment))
            {
                Department = BaseBusinessLogic.ConvertToString(dr[ZtoUserEntity.FieldDepartment]);
            }
            if (dr.ContainsColumn(ZtoUserEntity.FieldProvince))
            {
                Province = BaseBusinessLogic.ConvertToString(dr[ZtoUserEntity.FieldProvince]);
            }
            if (dr.ContainsColumn(ZtoUserEntity.FieldCounty))
            {
                County = BaseBusinessLogic.ConvertToString(dr[ZtoUserEntity.FieldCounty]);
            }
            if (dr.ContainsColumn(ZtoUserEntity.FieldCountyId))
            {
                CountyId = BaseBusinessLogic.ConvertToString(dr[ZtoUserEntity.FieldCountyId]);
            }
            if (dr.ContainsColumn(ZtoUserEntity.FieldRemark))
            {
                Remark = BaseBusinessLogic.ConvertToString(dr[ZtoUserEntity.FieldRemark]);
            }
            if (dr.ContainsColumn(ZtoUserEntity.FieldModifiedOn))
            {
                ModifiedOn = BaseBusinessLogic.ConvertToNullableDateTime(dr[ZtoUserEntity.FieldModifiedOn]);
            }
            if (dr.ContainsColumn(ZtoUserEntity.FieldProvinceId))
            {
                ProvinceId = BaseBusinessLogic.ConvertToString(dr[ZtoUserEntity.FieldProvinceId]);
            }
            if (dr.ContainsColumn(ZtoUserEntity.FieldCity))
            {
                City = BaseBusinessLogic.ConvertToString(dr[ZtoUserEntity.FieldCity]);
            }
            if (dr.ContainsColumn(ZtoUserEntity.FieldAddress))
            {
                Address = BaseBusinessLogic.ConvertToString(dr[ZtoUserEntity.FieldAddress]);
            }
            if (dr.ContainsColumn(ZtoUserEntity.FieldIssendorreceive))
            {
                Issendorreceive = BaseBusinessLogic.ConvertToString(dr[ZtoUserEntity.FieldIssendorreceive]);
            }
            if (dr.ContainsColumn(ZtoUserEntity.FieldTelePhone))
            {
                TelePhone = BaseBusinessLogic.ConvertToString(dr[ZtoUserEntity.FieldTelePhone]);
            }
            if (dr.ContainsColumn(ZtoUserEntity.FieldMobile))
            {
                Mobile = BaseBusinessLogic.ConvertToString(dr[ZtoUserEntity.FieldMobile]);
            }
            if (dr.ContainsColumn(ZtoUserEntity.FieldCompany))
            {
                Company = BaseBusinessLogic.ConvertToString(dr[ZtoUserEntity.FieldCompany]);
            }
            if (dr.ContainsColumn(ZtoUserEntity.FieldIsDefault))
            {
                IsDefault = BaseBusinessLogic.ConvertToString(dr[ZtoUserEntity.FieldIsDefault]);
            }
            return this;
        }

        ///<summary>
        /// 
        ///</summary>
        public static string TableName = "ZTO_USER";

        ///<summary>
        /// ID
        ///</summary>
        public static string FieldId = "ID";

        ///<summary>
        /// POSTCODE
        ///</summary>
        public static string FieldPostcode = "POSTCODE";

        ///<summary>
        /// REALNAME
        ///</summary>
        public static string FieldRealname = "REALNAME";

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
        /// DEPARTMENT
        ///</summary>
        public static string FieldDepartment = "DEPARTMENT";

        ///<summary>
        /// PROVINCE
        ///</summary>
        public static string FieldProvince = "PROVINCE";

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
        /// ISSENDORRECEIVE
        ///</summary>
        public static string FieldIssendorreceive = "ISSENDORRECEIVE";

        ///<summary>
        /// TELEPHONE
        ///</summary>
        public static string FieldTelePhone = "TELEPHONE";

        ///<summary>
        /// COMPANY
        ///</summary>
        public static string FieldCompany = "COMPANY";

        /// <summary>
        /// MOBILE
        /// </summary>
        public static string FieldMobile = "MOBILE";

        /// <summary>
        /// IsDefault
        /// </summary>
        public static string FieldIsDefault = "IsDefault";
    }
}
