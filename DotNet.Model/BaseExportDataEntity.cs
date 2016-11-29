//-----------------------------------------------------------------------
// <copyright file="BaseExportDataEntity.cs" company="Hairihan">
//     Copyright (C) 2016 , All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Runtime.Serialization;

namespace DotNet.Model
{
    using DotNet.Utilities;

    /// <summary>
    /// BaseExportDataEntity
    /// 大数据导出任务表
    /// 
    /// 修改记录
    /// 
    /// 2013-12-27 版本：1.0 JiRiGaLa 创建文件。
    /// 
    /// <author>
    ///     <name>JiRiGaLa</name>
    ///     <date>2013-12-27</date>
    /// </author>
    /// </summary>
    public partial class BaseExportDataEntity : BaseEntity
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

        private string companyId = string.Empty;
        /// <summary>
        /// 公司主键
        /// </summary>
        [DataMember]
        public string CompanyId
        {
            get
            {
                return companyId;
            }
            set
            {
                companyId = value;
            }
        }

        private string companyCode = string.Empty;
        /// <summary>
        /// 公司编号
        /// </summary>
        [DataMember]
        public string CompanyCode
        {
            get
            {
                return companyCode;
            }
            set
            {
                companyCode = value;
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
                return companyName;
            }
            set
            {
                companyName = value;
            }
        }

        private string departmentName = string.Empty;
        /// <summary>
        /// 部门名称
        /// </summary>
        [DataMember]
        public string DepartmentName
        {
            get
            {
                return departmentName;
            }
            set
            {
                departmentName = value;
            }
        }

        private string dbCode = string.Empty;
        /// <summary>
        /// 目标数据库编号
        /// </summary>
        [DataMember]
        public string DbCode
        {
            get
            {
                return dbCode;
            }
            set
            {
                dbCode = value;
            }
        }

        private string dataCategory = string.Empty;
        /// <summary>
        /// 数据类型
        /// </summary>
        [DataMember]
        public string DataCategory
        {
            get
            {
                return dataCategory;
            }
            set
            {
                dataCategory = value;
            }
        }

        private string fullName = string.Empty;
        /// <summary>
        /// 项目名称
        /// </summary>
        [DataMember]
        public string FullName
        {
            get
            {
                return fullName;
            }
            set
            {
                fullName = value;
            }
        }

        private string filePath = string.Empty;
        /// <summary>
        /// 文件路径
        /// </summary>
        [DataMember]
        public string FilePath
        {
            get
            {
                return filePath;
            }
            set
            {
                filePath = value;
            }
        }

        private string fileName = string.Empty;
        /// <summary>
        /// 文件名称
        /// </summary>
        [DataMember]
        public string FileName
        {
            get
            {
                return fileName;
            }
            set
            {
                fileName = value;
            }
        }

        private DateTime? processingStart = null;
        /// <summary>
        /// 导出开始处理时间
        /// </summary>
        [DataMember]
        public DateTime? ProcessingStart
        {
            get
            {
                return processingStart;
            }
            set
            {
                processingStart = value;
            }
        }

        private DateTime? processingEnd = null;
        /// <summary>
        /// 导出结束处理时间
        /// </summary>
        [DataMember]
        public DateTime? ProcessingEnd
        {
            get
            {
                return processingEnd;
            }
            set
            {
                processingEnd = value;
            }
        }

        private string exportSql = string.Empty;
        /// <summary>
        /// 导出的SQL语句
        /// </summary>
        [DataMember]
        public string ExportSql
        {
            get
            {
                return exportSql;
            }
            set
            {
                exportSql = value;
            }
        }

        private int permission = 0;
        /// <summary>
        /// 下载权限(0:自己,1:部门,2:网点)
        /// </summary>
        [DataMember]
        public int Permission
        {
            get
            {
                return permission;
            }
            set
            {
                permission = value;
            }
        }

        private int exportState = 0;
        /// <summary>
        /// 导出状态,(-1失败，0未开始,1执行中,2执行完成,3已下载)
        /// </summary>
        [DataMember]
        public int ExportState
        {
            get
            {
                return exportState;
            }
            set
            {
                exportState = value;
            }
        }

        private string severAddress = string.Empty;
        /// <summary>
        /// 服务器地址
        /// </summary>
        [DataMember]
        public string SeverAddress
        {
            get
            {
                return severAddress;
            }
            set
            {
                severAddress = value;
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
                return description;
            }
            set
            {
                description = value;
            }
        }

        private DateTime createOn = DateTime.Now;
        /// <summary>
        /// 创建日期
        /// </summary>
        [DataMember]
        public DateTime CreateOn
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
        [DataMember]
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
        [DataMember]
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

        /// <summary>
        /// 从数据行读取
        /// </summary>
        /// <param name="dr">数据行</param>
        protected override BaseEntity GetFrom(IDataRow dr)
        {
            Id = BaseBusinessLogic.ConvertToString(dr[BaseExportDataEntity.FieldId]);
            CompanyId = BaseBusinessLogic.ConvertToString(dr[BaseExportDataEntity.FieldCompanyId]);
            CompanyCode = BaseBusinessLogic.ConvertToString(dr[BaseExportDataEntity.FieldCompanyCode]);
            CompanyName = BaseBusinessLogic.ConvertToString(dr[BaseExportDataEntity.FieldCompanyName]);
            DepartmentName = BaseBusinessLogic.ConvertToString(dr[BaseExportDataEntity.FieldDepartmentName]);
            DbCode = BaseBusinessLogic.ConvertToString(dr[BaseExportDataEntity.FieldDbCode]);
            DataCategory = BaseBusinessLogic.ConvertToString(dr[BaseExportDataEntity.FieldDataCategory]);
            FullName = BaseBusinessLogic.ConvertToString(dr[BaseExportDataEntity.FieldFullName]);
            FilePath = BaseBusinessLogic.ConvertToString(dr[BaseExportDataEntity.FieldFilePath]);
            FileName = BaseBusinessLogic.ConvertToString(dr[BaseExportDataEntity.FieldFileName]);
            ProcessingStart = BaseBusinessLogic.ConvertToNullableDateTime(dr[BaseExportDataEntity.FieldProcessingStart]);
            ProcessingEnd = BaseBusinessLogic.ConvertToNullableDateTime(dr[BaseExportDataEntity.FieldProcessingEnd]);
            ExportSql = BaseBusinessLogic.ConvertToString(dr[BaseExportDataEntity.FieldExportSql]);
            Permission = BaseBusinessLogic.ConvertToInt(dr[BaseExportDataEntity.FieldPermission]);
            ExportState = BaseBusinessLogic.ConvertToInt(dr[BaseExportDataEntity.FieldExportState]);
            SeverAddress = BaseBusinessLogic.ConvertToString(dr[BaseExportDataEntity.FieldSeverAddress]);
            Description = BaseBusinessLogic.ConvertToString(dr[BaseExportDataEntity.FieldDescription]);
            CreateOn = BaseBusinessLogic.ConvertToDateTime(dr[BaseExportDataEntity.FieldCreateOn]);
            CreateUserId = BaseBusinessLogic.ConvertToString(dr[BaseExportDataEntity.FieldCreateUserId]);
            CreateBy = BaseBusinessLogic.ConvertToString(dr[BaseExportDataEntity.FieldCreateBy]);
            // 获取扩展属性
            GetFromExpand(dr);
            return this;
        }

        ///<summary>
        /// 大数据导出任务表
        ///</summary>
        [NonSerialized]
        public static string TableName = "BaseExportData";

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
        /// 部门名称
        ///</summary>
        [NonSerialized]
        public static string FieldDepartmentName = "DepartmentName";

        ///<summary>
        /// 目标数据库编号
        ///</summary>
        [NonSerialized]
        public static string FieldDbCode = "DbCode";

        ///<summary>
        /// 数据类型
        ///</summary>
        [NonSerialized]
        public static string FieldDataCategory = "DataCategory";

        ///<summary>
        /// 项目名称
        ///</summary>
        [NonSerialized]
        public static string FieldFullName = "FullName";

        ///<summary>
        /// 文件路径
        ///</summary>
        [NonSerialized]
        public static string FieldFilePath = "FilePath";

        ///<summary>
        /// 文件名称
        ///</summary>
        [NonSerialized]
        public static string FieldFileName = "FileName";

        ///<summary>
        /// 导出开始处理时间
        ///</summary>
        [NonSerialized]
        public static string FieldProcessingStart = "ProcessingStart";

        ///<summary>
        /// 导出结束处理时间
        ///</summary>
        [NonSerialized]
        public static string FieldProcessingEnd = "ProcessingEnd";

        ///<summary>
        /// 导出的SQL语句
        ///</summary>
        [NonSerialized]
        public static string FieldExportSql = "ExportSql";

        ///<summary>
        /// 下载权限(0:自己,1:部门,2:网点)
        ///</summary>
        [NonSerialized]
        public static string FieldPermission = "Permission";

        ///<summary>
        /// 导出状态,(-1失败，0未开始,1执行中,2执行完成,3已下载)
        ///</summary>
        [NonSerialized]
        public static string FieldExportState = "ExportState";

        ///<summary>
        /// 服务器地址
        ///</summary>
        [NonSerialized]
        public static string FieldSeverAddress = "SeverAddress";

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
    }
}
