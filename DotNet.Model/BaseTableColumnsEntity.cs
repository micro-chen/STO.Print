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
    /// BaseTableColumnsEntity
    /// 表字段结构定义说明
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
	public partial class BaseTableColumnsEntity : BaseEntity
    {
        private int? id = 0;
        /// <summary>
        /// 主键
        /// </summary>
        [DataMember]
        public int? Id
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

        private string tableCode = null;
        /// <summary>
        /// 表
        /// </summary>
        [DataMember]
        public string TableCode
        {
            get
            {
                return this.tableCode;
            }
            set
            {
                this.tableCode = value;
            }
        }

        private string columnCode = null;
        /// <summary>
        /// 表
        /// </summary>
        [DataMember]
        public string ColumnCode
        {
            get
            {
                return this.columnCode;
            }
            set
            {
                this.columnCode = value;
            }
        }

        private string columnName = null;
        /// <summary>
        /// 表名
        /// </summary>
        [DataMember]
        public string ColumnName
        {
            get
            {
                return this.columnName;
            }
            set
            {
                this.columnName = value;
            }
        }

        private int? isPublic = 0;
        /// <summary>
        /// 是公开
        /// </summary>
        [DataMember]
        public int? IsPublic
        {
            get
            {
                return this.isPublic;
            }
            set
            {
                this.isPublic = value;
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

        private int? allowEdit = 0;
        /// <summary>
        /// 允许编辑
        /// </summary>
        [DataMember]
        public int? AllowEdit
        {
            get
            {
                return this.allowEdit;
            }
            set
            {
                this.allowEdit = value;
            }
        }

        private int? allowDelete = 0;
        /// <summary>
        /// 允许删除
        /// </summary>
        [DataMember]
        public int? AllowDelete
        {
            get
            {
                return this.allowDelete;
            }
            set
            {
                this.allowDelete = value;
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
            Id = BaseBusinessLogic.ConvertToInt(dr[BaseTableColumnsEntity.FieldId]);
            TableCode = BaseBusinessLogic.ConvertToString(dr[BaseTableColumnsEntity.FieldTableCode]);
            ColumnCode = BaseBusinessLogic.ConvertToString(dr[BaseTableColumnsEntity.FieldColumnCode]);
            ColumnName = BaseBusinessLogic.ConvertToString(dr[BaseTableColumnsEntity.FieldColumnName]);
            IsPublic = BaseBusinessLogic.ConvertToInt(dr[BaseTableColumnsEntity.FieldIsPublic]);
            Enabled = BaseBusinessLogic.ConvertToInt(dr[BaseTableColumnsEntity.FieldEnabled]);
            AllowEdit = BaseBusinessLogic.ConvertToInt(dr[BaseTableColumnsEntity.FieldAllowEdit]);
            AllowDelete = BaseBusinessLogic.ConvertToInt(dr[BaseTableColumnsEntity.FieldAllowDelete]);
            DeletionStateCode = BaseBusinessLogic.ConvertToInt(dr[BaseTableColumnsEntity.FieldDeletionStateCode]);
            SortCode = BaseBusinessLogic.ConvertToInt(dr[BaseTableColumnsEntity.FieldSortCode]);
            Description = BaseBusinessLogic.ConvertToString(dr[BaseTableColumnsEntity.FieldDescription]);
            CreateOn = BaseBusinessLogic.ConvertToNullableDateTime(dr[BaseTableColumnsEntity.FieldCreateOn]);
            CreateUserId = BaseBusinessLogic.ConvertToString(dr[BaseTableColumnsEntity.FieldCreateUserId]);
            CreateBy = BaseBusinessLogic.ConvertToString(dr[BaseTableColumnsEntity.FieldCreateBy]);
            ModifiedOn = BaseBusinessLogic.ConvertToNullableDateTime(dr[BaseTableColumnsEntity.FieldModifiedOn]);
            ModifiedUserId = BaseBusinessLogic.ConvertToString(dr[BaseTableColumnsEntity.FieldModifiedUserId]);
            ModifiedBy = BaseBusinessLogic.ConvertToString(dr[BaseTableColumnsEntity.FieldModifiedBy]);
            // 获取扩展属性
            GetFromExpand(dr);
            return this;
        }

        ///<summary>
        /// 表字段结构定义说明
        ///</summary>
        [NonSerialized]
        public static string TableName = "BaseTableColumns";

        ///<summary>
        /// 主键
        ///</summary>
        [NonSerialized]
        public static string FieldId = "Id";

        ///<summary>
        /// 表
        ///</summary>
        [NonSerialized]
        public static string FieldTableCode = "TableCode";

        ///<summary>
        /// 字段编号
        ///</summary>
        [NonSerialized]
        public static string FieldColumnCode = "ColumnCode";

        ///<summary>
        /// 字段名
        ///</summary>
        [NonSerialized]
        public static string FieldColumnName = "ColumnName";

        /// <summary>
        /// 约束条件
        /// </summary>
        [NonSerialized]
        public static string FieldUseConstraint = "UseConstraint";

        ///<summary>
        /// 是公开
        ///</summary>
        [NonSerialized]
        public static string FieldIsPublic = "IsPublic";

        ///<summary>
        /// 数据类型
        ///</summary>
        [NonSerialized]
        public static string FieldDataType = "DataType";

        ///<summary>
        /// 数据字典来源
        ///</summary>
        [NonSerialized]
        public static string FieldTargetTable = "TargetTable";

        ///<summary>
        /// 有效
        ///</summary>
        [NonSerialized]
        public static string FieldEnabled = "Enabled";

        ///<summary>
        /// 允许编辑
        ///</summary>
        [NonSerialized]
        public static string FieldAllowEdit = "AllowEdit";

        ///<summary>
        /// 允许删除
        ///</summary>
        [NonSerialized]
        public static string FieldAllowDelete = "AllowDelete";

        ///<summary>
        /// 删除标记
        ///</summary>
        [NonSerialized]
        public static string FieldDeletionStateCode = "DeletionStateCode";

        ///<summary>
        /// 排序码
        ///</summary>
        [NonSerialized]
        public static string FieldSortCode = "SortCode";

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
