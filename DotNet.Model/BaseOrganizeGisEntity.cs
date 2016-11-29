//-----------------------------------------------------------------------
// <copyright file="BaseOrganizeGisEntity.cs" company="Hairihan">
//     Copyright (C) 2016 , All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations;

namespace DotNet.Model
{
    using DotNet.Utilities;

    /// <summary>
    /// BaseOrganizeGisEntity
    /// 网点GIS表，存储网点经纬度，网点是否对外显示区域
    /// 
    /// 修改记录
    /// 
    /// 2014-11-08 版本：1.0 JiRiGaLa 创建文件。
    /// 
    /// <author>
    ///     <name>JiRiGaLa</name>
    ///     <date>2014-11-08</date>
    /// </author>
    /// </summary>
    public partial class BaseOrganizeGisEntity : BaseEntity
    {
        private Decimal id;
        /// <summary>
        /// 对应组织机构表的ID
        /// </summary>
        [FieldDescription("对应组织机构表的ID", false)]
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

        private string longitude = string.Empty;
        /// <summary>
        /// 百度经度
        /// </summary>
        [FieldDescription("百度经度")]
        public string Longitude
        {
            get
            {
                return longitude;
            }
            set
            {
                longitude = value;
            }
        }

        private DateTime? modifiedOn = null;
        /// <summary>
        /// 修改日期
        /// </summary>
        [FieldDescription("修改日期", false)]
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

        private string modifiedUserId = string.Empty;
        /// <summary>
        /// 修改用户主键
        /// </summary>
        [FieldDescription("修改用户主键", false)]
        public string ModifiedUserId
        {
            get
            {
                return modifiedUserId;
            }
            set
            {
                modifiedUserId = value;
            }
        }

        private Decimal? regionLabelEnable = null;
        /// <summary>
        /// 对外标注取派件区域
        /// </summary>
        [FieldDescription("对外标注取派件区域")]
        public Decimal? RegionLabelEnable
        {
            get
            {
                return regionLabelEnable;
            }
            set
            {
                regionLabelEnable = value;
            }
        }

        private string address = string.Empty;
        /// <summary>
        /// 客户在OA中标注网点时输入的地址
        /// </summary>
        [FieldDescription("客户在OA中标注网点时输入的地址")]
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

        private Decimal? webShowEnable = null;
        /// <summary>
        /// 在外网GIS地图上显示此网点
        /// </summary>
        [FieldDescription("在外网GIS地图上显示此网点")]
        public Decimal? WebShowEnable
        {
            get
            {
                return webShowEnable;
            }
            set
            {
                webShowEnable = value;
            }
        }

        private string latitude = string.Empty;
        /// <summary>
        /// 百度纬度
        /// </summary>
        [FieldDescription("百度纬度")]
        public string Latitude
        {
            get
            {
                return latitude;
            }
            set
            {
                latitude = value;
            }
        }

        private string modifiedBy = string.Empty;
        /// <summary>
        /// 修改用户
        /// </summary>
        [FieldDescription("修改用户", false)]
        public string ModifiedBy
        {
            get
            {
                return modifiedBy;
            }
            set
            {
                modifiedBy = value;
            }
        }

        private string createBy = string.Empty;
        /// <summary>
        /// 创建用户
        /// </summary>
        [FieldDescription("创建用户", false)]
        public string CreateBy
        {
            get
            {
                return createBy;
            }
            set
            {
                createBy = value;
            }
        }

        private DateTime? createOn = null;
        /// <summary>
        /// 创建日期
        /// </summary>
        [FieldDescription("创建日期", false)]
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

        private string createUserId = string.Empty;
        /// <summary>
        /// 创建用户主键
        /// </summary>
        [FieldDescription("创建用户主键", false)]
        public string CreateUserId
        {
            get
            {
                return createUserId;
            }
            set
            {
                createUserId = value;
            }
        }

        /// <summary>
        /// 从数据行读取
        /// </summary>
        /// <param name="dr">数据行</param>
        protected override BaseEntity GetFrom(IDataRow dr)
        {
            Id = BaseBusinessLogic.ConvertToDecimal(dr[BaseOrganizeGisEntity.FieldId]);
            Latitude = BaseBusinessLogic.ConvertToString(dr[BaseOrganizeGisEntity.FieldLatitude]);
            ModifiedOn = BaseBusinessLogic.ConvertToNullableDateTime(dr[BaseOrganizeGisEntity.FieldModifiedOn]);
            ModifiedUserId = BaseBusinessLogic.ConvertToString(dr[BaseOrganizeGisEntity.FieldModifiedUserId]);
            RegionLabelEnable = BaseBusinessLogic.ConvertToNullableDecimal(dr[BaseOrganizeGisEntity.FieldRegionLabelEnable]);
            Address = BaseBusinessLogic.ConvertToString(dr[BaseOrganizeGisEntity.FieldAddress]);
            WebShowEnable = BaseBusinessLogic.ConvertToNullableDecimal(dr[BaseOrganizeGisEntity.FieldWebShowEnable]);
            Longitude = BaseBusinessLogic.ConvertToString(dr[BaseOrganizeGisEntity.FieldLongitude]);
            ModifiedBy = BaseBusinessLogic.ConvertToString(dr[BaseOrganizeGisEntity.FieldModifiedBy]);
            CreateBy = BaseBusinessLogic.ConvertToString(dr[BaseOrganizeGisEntity.FieldCreateBy]);
            CreateOn = BaseBusinessLogic.ConvertToNullableDateTime(dr[BaseOrganizeGisEntity.FieldCreateOn]);
            CreateUserId = BaseBusinessLogic.ConvertToString(dr[BaseOrganizeGisEntity.FieldCreateUserId]);
            // 获取扩展属性
            GetFromExpand(dr);
            return this;
        }

        ///<summary>
        /// 网点GIS表，存储网点经纬度，网点是否对外显示区域
        ///</summary>
        [FieldDescription("网点GIS表")]
        public static string TableName = "BaseOrganizeGis";

        ///<summary>
        /// 对应组织机构表的ID
        ///</summary>
        public static string FieldId = "Id";

        ///<summary>
        /// 百度经度
        ///</summary>
        public static string FieldLongitude = "Longitude";

        ///<summary>
        /// 修改日期
        ///</summary>
        public static string FieldModifiedOn = "ModifiedOn";

        ///<summary>
        /// 修改用户主键
        ///</summary>
        public static string FieldModifiedUserId = "ModifiedUserId";

        ///<summary>
        /// 对外标注取派件区域
        ///</summary>
        public static string FieldRegionLabelEnable = "RegionLabelEnable";

        ///<summary>
        /// 客户在OA中标注网点时输入的地址
        ///</summary>
        public static string FieldAddress = "Address";

        ///<summary>
        /// 在外网GIS地图上显示此网点
        ///</summary>
        public static string FieldWebShowEnable = "WebShowEnable";

        ///<summary>
        /// 百度纬度
        ///</summary>
        public static string FieldLatitude = "Latitude";

        ///<summary>
        /// 修改用户
        ///</summary>
        public static string FieldModifiedBy = "ModifiedBy";

        ///<summary>
        /// 创建用户
        ///</summary>
        public static string FieldCreateBy = "CreateBy";

        ///<summary>
        /// 创建日期
        ///</summary>
        public static string FieldCreateOn = "CreateOn";

        ///<summary>
        /// 创建用户主键
        ///</summary>
        public static string FieldCreateUserId = "CreateUserId";
    }
}
