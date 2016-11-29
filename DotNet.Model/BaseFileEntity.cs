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

    /// <remarks>
    ///	BaseFileEntity
    /// 文件信息
    ///
    /// 注意事项
    ///     1.主键与编号一定要一致否则以后比较难扩展。
    ///     2.或者模块权限添加时，能自动添加到基本权限表里，这样也能解决问题。
    ///
    /// 修改记录
    ///
    ///     2008.04.29 版本：2.4 JiRiGaLa 整理 Entity 主键部分。
    ///     2007.05.30 版本：2.3 JiRiGaLa 整理 Entity 主键部分。
    ///     2007.01.20 版本：2.2 JiRiGaLa 添加AllowEdit,AllowDelete两个字段。
    ///     2007.01.19 版本：2.1 JiRiGaLa SQLBuild修改为SQLBuild。
    ///     2007.01.04 版本：2.0 JiRiGaLa 重新整理主键。
    ///		2006.03.16 版本：1.0 JiRiGaLa 规范化主键。
    /// 
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2008.04.29</date>
    /// </author>
    /// </remarks>
    [Serializable, DataContract]
	public partial class BaseFileEntity : BaseEntity
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

        private string folderId = null;
        /// <summary>
        /// 文件夹节点主键
        /// </summary>
        [DataMember]
        public string FolderId
        {
            get
            {
                return this.folderId;
            }
            set
            {
                this.folderId = value;
            }
        }

        private string fileName = null;
        /// <summary>
        /// 文件名
        /// </summary>
        [DataMember]
        public string FileName
        {
            get
            {
                return this.fileName;
            }
            set
            {
                this.fileName = value;
            }
        }

        private string filePath = null;
        /// <summary>
        /// 文件路径
        /// </summary>
        [DataMember]
        public string FilePath
        {
            get
            {
                return this.filePath;
            }
            set
            {
                this.filePath = value;
            }
        }

        private byte[] contents = null;
        /// <summary>
        /// 文件内容
        /// </summary>
        [DataMember]
        public byte[] Contents
        {
            get
            {
                return this.contents;
            }
            set
            {
                this.contents = value;
            }
        }
        
        private int? fileSize = 0;
        /// <summary>
        /// 文件大小
        /// </summary>
        [DataMember]
        public int? FileSize
        {
            get
            {
                return this.fileSize;
            }
            set
            {
                this.fileSize = value;
            }
        }
        
        private int? readCount = 0;
        /// <summary>
        /// 被阅读次数
        /// </summary>
        [DataMember]
        public int? ReadCount
        {
            get
            {
                return this.readCount;
            }
            set
            {
                this.readCount = value;
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

        /// <summary>
        /// 从数据行读取
        /// </summary>
        /// <param name="dr">数据行</param>
		protected override BaseEntity GetFrom(IDataRow dr)
        {
            Id = BaseBusinessLogic.ConvertToString(dr[BaseFileEntity.FieldId]);
            FolderId = BaseBusinessLogic.ConvertToString(dr[BaseFileEntity.FieldFolderId]);
            FileName = BaseBusinessLogic.ConvertToString(dr[BaseFileEntity.FieldFileName]);
            FilePath = BaseBusinessLogic.ConvertToString(dr[BaseFileEntity.FieldFilePath]);
            FileSize = BaseBusinessLogic.ConvertToInt(dr[BaseFileEntity.FieldFileSize]);
            ReadCount = BaseBusinessLogic.ConvertToInt(dr[BaseFileEntity.FieldReadCount]);
            DeletionStateCode = BaseBusinessLogic.ConvertToInt(dr[BaseFileEntity.FieldDeletionStateCode]);
            Description = BaseBusinessLogic.ConvertToString(dr[BaseFileEntity.FieldDescription]);
            Enabled = BaseBusinessLogic.ConvertToInt(dr[BaseFileEntity.FieldEnabled]);
            SortCode = BaseBusinessLogic.ConvertToInt(dr[BaseFileEntity.FieldSortCode]);
            CreateOn = BaseBusinessLogic.ConvertToNullableDateTime(dr[BaseFileEntity.FieldCreateOn]);
            CreateBy = BaseBusinessLogic.ConvertToString(dr[BaseFileEntity.FieldCreateBy]);
            CreateUserId = BaseBusinessLogic.ConvertToString(dr[BaseFileEntity.FieldCreateUserId]);
            ModifiedOn = BaseBusinessLogic.ConvertToNullableDateTime(dr[BaseFileEntity.FieldModifiedOn]);
            ModifiedBy = BaseBusinessLogic.ConvertToString(dr[BaseFileEntity.FieldModifiedBy]);
            ModifiedUserId = BaseBusinessLogic.ConvertToString(dr[BaseFileEntity.FieldModifiedUserId]);
            // 获取扩展属性
            GetFromExpand(dr);
            return this;
        }

        ///<summary>
        /// 文件新闻表
        ///</summary>
        [NonSerialized]
        public static string TableName = "BaseFile";

        ///<summary>
        /// 主键
        ///</summary>
        [NonSerialized]
        public static string FieldId = "Id";

        ///<summary>
        /// 文件夹节点主键
        ///</summary>
        [NonSerialized]
        public static string FieldFolderId = "FolderId";

        ///<summary>
        /// 文件名
        ///</summary>
        [NonSerialized]
        public static string FieldFileName = "FileName";

        ///<summary>
        /// 文件路径
        ///</summary>
        [NonSerialized]
        public static string FieldFilePath = "FilePath";

        ///<summary>
        /// 文件内容
        ///</summary>
        [NonSerialized]
        public static string FieldContents = "Contents";

        ///<summary>
        /// 文件大小
        ///</summary>
        [NonSerialized]
        public static string FieldFileSize = "FileSize";

        ///<summary>
        /// 被阅读次数
        ///</summary>
        [NonSerialized]
        public static string FieldReadCount = "ReadCount";

        ///<summary>
        /// 删除标志
        ///</summary>
        [NonSerialized]
        public static string FieldDeletionStateCode = "DeletionStateCode";

        ///<summary>
        /// 备注
        ///</summary>
        [NonSerialized]
        public static string FieldDescription = "Description";

        ///<summary>
        /// 有效
        ///</summary>
        [NonSerialized]
        public static string FieldEnabled = "Enabled";

        ///<summary>
        /// 排序码
        ///</summary>
        [NonSerialized]
        public static string FieldSortCode = "SortCode";

        ///<summary>
        /// 创建日期
        ///</summary>
        [NonSerialized]
        public static string FieldCreateOn = "CreateOn";

        ///<summary>
        /// 创建用户
        ///</summary>
        [NonSerialized]
        public static string FieldCreateBy = "CreateBy";

        ///<summary>
        /// 创建用户主键
        ///</summary>
        [NonSerialized]
        public static string FieldCreateUserId = "CreateUserId";

        ///<summary>
        /// 修改日期
        ///</summary>
        [NonSerialized]
        public static string FieldModifiedOn = "ModifiedOn";

        ///<summary>
        /// 修改用户
        ///</summary>
        [NonSerialized]
        public static string FieldModifiedBy = "ModifiedBy";

        ///<summary>
        /// 修改用户主键
        ///</summary>
        [NonSerialized]
        public static string FieldModifiedUserId = "ModifiedUserId";
    }
}