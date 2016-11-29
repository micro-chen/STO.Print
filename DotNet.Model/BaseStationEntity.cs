//-----------------------------------------------------------------------
// <copyright file="BaseComputerEntity.cs" company="Hairihan">
//     Copyright (C) 2016 , All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;

namespace DotNet.Model
{
    using DotNet.Utilities;

    /// <summary>
    /// BaseStationEntity
    /// 计算机表
    /// 
    /// 修改记录
    /// 
    /// 2015-02-24 版本：1.0 JiRiGaLa 创建文件。
    /// 
    /// <author>
    ///     <name>JiRiGaLa</name>
    ///     <date>2015-02-24</date>
    /// </author>
    /// </summary>
    public partial class BaseStationEntity : BaseEntity
    {
        private int id;
        /// <summary>
        /// 主键
        /// </summary>
        public int Id
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

        private int? organizeId = null;
        /// <summary>
        /// 组织机构主键
        /// </summary>
        public int? OrganizeId
        {
            get
            {
                return organizeId;
            }
            set
            {
                organizeId = value;
            }
        }

        private string code = string.Empty;
        /// <summary>
        /// 编号
        /// </summary>
        public string Code
        {
            get
            {
                return code;
            }
            set
            {
                code = value;
            }
        }

        private string realName = string.Empty;
        /// <summary>
        /// 名称
        /// </summary>
        public string RealName
        {
            get
            {
                return realName;
            }
            set
            {
                realName = value;
            }
        }

        private string categoryCode = string.Empty;
        /// <summary>
        /// 角色分类
        /// </summary>
        public string CategoryCode
        {
            get
            {
                return categoryCode;
            }
            set
            {
                categoryCode = value;
            }
        }

        private string macAddress = string.Empty;
        /// <summary>
        /// MAC地址
        /// </summary>
        public string MacAddress
        {
            get
            {
                return macAddress;
            }
            set
            {
                macAddress = value;
            }
        }

        private DateTime? uploadTime = null;
        /// <summary>
        /// 最后上传时间
        /// </summary>
        public DateTime? UploadTime
        {
            get
            {
                return uploadTime;
            }
            set
            {
                uploadTime = value;
            }
        }

        private DateTime? downloadTime = null;
        /// <summary>
        /// 最后下载时间
        /// </summary>
        public DateTime? DownloadTime
        {
            get
            {
                return downloadTime;
            }
            set
            {
                downloadTime = value;
            }
        }

        private int? sortCode = 0;
        /// <summary>
        /// 排序码
        /// </summary>
        public int? SortCode
        {
            get
            {
                return sortCode;
            }
            set
            {
                sortCode = value;
            }
        }

        private int deletionStateCode = 0;
        /// <summary>
        /// 删除标记
        /// </summary>
        public int DeletionStateCode
        {
            get
            {
                return deletionStateCode;
            }
            set
            {
                deletionStateCode = value;
            }
        }

        private int enabled = 1;
        /// <summary>
        /// 有效标志
        /// </summary>
        public int Enabled
        {
            get
            {
                return enabled;
            }
            set
            {
                enabled = value;
            }
        }

        private string description = string.Empty;
        /// <summary>
        /// 描述
        /// </summary>
        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                description = value;
            }
        }

        private DateTime? createOn = DateTime.Now;
        /// <summary>
        /// 创建日期
        /// </summary>
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

        private string createBy = string.Empty;
        /// <summary>
        /// 创建用户
        /// </summary>
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

        private DateTime? modifiedOn = DateTime.Now;
        /// <summary>
        /// 修改日期
        /// </summary>
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

        private string modifiedBy = string.Empty;
        /// <summary>
        /// 修改用户
        /// </summary>
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

        /// <summary>
        /// 从数据行读取
        /// </summary>
        /// <param name="dr">数据行</param>
        protected override BaseEntity GetFrom(IDataRow dr)
        {
            GetFromExpand(dr);
            Id = BaseBusinessLogic.ConvertToInt(dr[BaseStationEntity.FieldId]);
            OrganizeId = BaseBusinessLogic.ConvertToNullableInt(dr[BaseStationEntity.FieldOrganizeId]);
            Code = BaseBusinessLogic.ConvertToString(dr[BaseStationEntity.FieldCode]);
            RealName = BaseBusinessLogic.ConvertToString(dr[BaseStationEntity.FieldRealName]);
            CategoryCode = BaseBusinessLogic.ConvertToString(dr[BaseStationEntity.FieldCategoryCode]);
            MacAddress = BaseBusinessLogic.ConvertToString(dr[BaseStationEntity.FieldMacAddress]);
            UploadTime = BaseBusinessLogic.ConvertToNullableDateTime(dr[BaseStationEntity.FieldUploadTime]);
            DownloadTime = BaseBusinessLogic.ConvertToNullableDateTime(dr[BaseStationEntity.FieldDownloadTime]);
            SortCode = BaseBusinessLogic.ConvertToNullableInt(dr[BaseStationEntity.FieldSortCode]);
            DeletionStateCode = BaseBusinessLogic.ConvertToInt(dr[BaseStationEntity.FieldDeletionStateCode]);
            Enabled = BaseBusinessLogic.ConvertToInt(dr[BaseStationEntity.FieldEnabled]);
            Description = BaseBusinessLogic.ConvertToString(dr[BaseStationEntity.FieldDescription]);
            CreateOn = BaseBusinessLogic.ConvertToNullableDateTime(dr[BaseStationEntity.FieldCreateOn]);
            CreateUserId = BaseBusinessLogic.ConvertToString(dr[BaseStationEntity.FieldCreateUserId]);
            CreateBy = BaseBusinessLogic.ConvertToString(dr[BaseStationEntity.FieldCreateBy]);
            ModifiedOn = BaseBusinessLogic.ConvertToNullableDateTime(dr[BaseStationEntity.FieldModifiedOn]);
            ModifiedUserId = BaseBusinessLogic.ConvertToString(dr[BaseStationEntity.FieldModifiedUserId]);
            ModifiedBy = BaseBusinessLogic.ConvertToString(dr[BaseStationEntity.FieldModifiedBy]);
            return this;
        }

        ///<summary>
        /// 计算机表
        ///</summary>
        public static string TableName = "BaseComputer";

        ///<summary>
        /// 主键
        ///</summary>
        public static string FieldId = "Id";

        ///<summary>
        /// 组织机构主键
        ///</summary>
        public static string FieldOrganizeId = "OrganizeId";

        ///<summary>
        /// 编号
        ///</summary>
        public static string FieldCode = "Code";

        ///<summary>
        /// 名称
        ///</summary>
        public static string FieldRealName = "RealName";

        ///<summary>
        /// 角色分类
        ///</summary>
        public static string FieldCategoryCode = "CategoryCode";

        ///<summary>
        /// MAC地址
        ///</summary>
        public static string FieldMacAddress = "MacAddress";

        ///<summary>
        /// 最后上传时间
        ///</summary>
        public static string FieldUploadTime = "UploadTime";

        ///<summary>
        /// 最后下载时间
        ///</summary>
        public static string FieldDownloadTime = "DownloadTime";

        ///<summary>
        /// 排序码
        ///</summary>
        public static string FieldSortCode = "SortCode";

        ///<summary>
        /// 删除标记
        ///</summary>
        public static string FieldDeletionStateCode = "DeletionStateCode";

        ///<summary>
        /// 有效标志
        ///</summary>
        public static string FieldEnabled = "Enabled";

        ///<summary>
        /// 描述
        ///</summary>
        public static string FieldDescription = "Description";

        ///<summary>
        /// 创建日期
        ///</summary>
        public static string FieldCreateOn = "CreateOn";

        ///<summary>
        /// 创建用户主键
        ///</summary>
        public static string FieldCreateUserId = "CreateUserId";

        ///<summary>
        /// 创建用户
        ///</summary>
        public static string FieldCreateBy = "CreateBy";

        ///<summary>
        /// 修改日期
        ///</summary>
        public static string FieldModifiedOn = "ModifiedOn";

        ///<summary>
        /// 修改用户主键
        ///</summary>
        public static string FieldModifiedUserId = "ModifiedUserId";

        ///<summary>
        /// 修改用户
        ///</summary>
        public static string FieldModifiedBy = "ModifiedBy";
    }
}
