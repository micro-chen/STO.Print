//-----------------------------------------------------------------------
// <copyright file="BaseModifyRecordEntity.cs" company="Hairihan">
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
    /// BaseModifyRecordEntity
    /// 修改记录表
    /// 
    /// 修改记录
    /// 
    /// 2013-12-16 版本：1.0 JiRiGaLa 创建文件。
    /// 
    /// <author>
    ///     <name>JiRiGaLa</name>
    ///     <date>2013-12-16</date>
    /// </author>
    /// </summary>
    [Serializable, DataContract]
    public partial class BaseModifyRecordEntity : BaseEntity
    {
        private int id;
        /// <summary>
        /// 主键
        /// </summary>
        [DataMember]
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

        private string tableCode = string.Empty;
        /// <summary>
        /// 表名
        /// </summary>
        [DataMember]
        public string TableCode
        {
            get
            {
                return tableCode;
            }
            set
            {
                tableCode = value;
            }
        }

        private string tableDescription = string.Empty;
        /// <summary>
        /// 表备注
        /// </summary>
        [DataMember]
        public string TableDescription
        {
            get
            {
                return tableDescription;
            }
            set
            {
                tableDescription = value;
            }
        }

        private string columnCode = string.Empty;
        /// <summary>
        /// 列名
        /// </summary>
        [DataMember]
        public string ColumnCode
        {
            get
            {
                return columnCode;
            }
            set
            {
                columnCode = value;
            }
        }

        private string columnDescription = string.Empty;
        /// <summary>
        /// 列备注
        /// </summary>
        [DataMember]
        public string ColumnDescription
        {
            get
            {
                return columnDescription;
            }
            set
            {
                columnDescription = value;
            }
        }

        private string recordKey = string.Empty;
        /// <summary>
        /// 记录主键
        /// </summary>
        [DataMember]
        public string RecordKey
        {
            get
            {
                return recordKey;
            }
            set
            {
                recordKey = value;
            }
        }

        private string oldKey = string.Empty;
        /// <summary>
        /// 原值主键
        /// </summary>
        [DataMember]
        public string OldKey
        {
            get
            {
                return oldKey;
            }
            set
            {
                oldKey = value;
            }
        }

        private string oldValue = string.Empty;
        /// <summary>
        /// 原值
        /// </summary>
        [DataMember]
        public string OldValue
        {
            get
            {
                return oldValue;
            }
            set
            {
                oldValue = value;
            }
        }

        private string newKey = string.Empty;
        /// <summary>
        /// 现值主键
        /// </summary>
        [DataMember]
        public string NewKey
        {
            get
            {
                return newKey;
            }
            set
            {
                newKey = value;
            }
        }

        private string newValue = string.Empty;
        /// <summary>
        /// 现值
        /// </summary>
        [DataMember]
        public string NewValue
        {
            get
            {
                return newValue;
            }
            set
            {
                newValue = value;
            }
        }

        private string ipAddress = string.Empty;
        /// <summary>
        /// IPAddress 地址
        /// </summary>
        [DataMember]
        public string IPAddress
        {
            get
            {
                return ipAddress;
            }
            set
            {
                ipAddress = value;
            }
        }

        private DateTime? createOn = DateTime.Now;
        /// <summary>
        /// 创建日期
        /// </summary>
        [DataMember]
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
            Id = BaseBusinessLogic.ConvertToInt(dr[BaseModifyRecordEntity.FieldId]);
            TableCode = BaseBusinessLogic.ConvertToString(dr[BaseModifyRecordEntity.FieldTableCode]);
            TableDescription = BaseBusinessLogic.ConvertToString(dr[BaseModifyRecordEntity.FieldTableDescription]);
            ColumnCode = BaseBusinessLogic.ConvertToString(dr[BaseModifyRecordEntity.FieldColumnCode]);
            ColumnDescription = BaseBusinessLogic.ConvertToString(dr[BaseModifyRecordEntity.FieldColumnDescription]);
            RecordKey = BaseBusinessLogic.ConvertToString(dr[BaseModifyRecordEntity.FieldRecordKey]);
            OldKey = BaseBusinessLogic.ConvertToString(dr[BaseModifyRecordEntity.FieldOldKey]);
            OldValue = BaseBusinessLogic.ConvertToString(dr[BaseModifyRecordEntity.FieldOldValue]);
            NewKey = BaseBusinessLogic.ConvertToString(dr[BaseModifyRecordEntity.FieldNewKey]);
            NewValue = BaseBusinessLogic.ConvertToString(dr[BaseModifyRecordEntity.FieldNewValue]);
            IPAddress = BaseBusinessLogic.ConvertToString(dr[BaseModifyRecordEntity.FieldIPAddress]);
            CreateOn = BaseBusinessLogic.ConvertToNullableDateTime(dr[BaseModifyRecordEntity.FieldCreateOn]);
            CreateUserId = BaseBusinessLogic.ConvertToString(dr[BaseModifyRecordEntity.FieldCreateUserId]);
            CreateBy = BaseBusinessLogic.ConvertToString(dr[BaseModifyRecordEntity.FieldCreateBy]);
            // 获取扩展属性
            GetFromExpand(dr);
            return this;
        }

        ///<summary>
        /// 修改记录表
        ///</summary>
        [NonSerialized]
        public static string TableName = "BaseModifyRecord";

        ///<summary>
        /// 主键
        ///</summary>
        [NonSerialized]
        public static string FieldId = "Id";

        ///<summary>
        /// 表名
        ///</summary>
        [NonSerialized]
        public static string FieldTableCode = "TableCode";

        ///<summary>
        /// 表备注
        ///</summary>
        [NonSerialized]
        public static string FieldTableDescription = "TableDescription";

        ///<summary>
        /// 列名
        ///</summary>
        [NonSerialized]
        public static string FieldColumnCode = "ColumnCode";

        ///<summary>
        /// 列备注
        ///</summary>
        [NonSerialized]
        public static string FieldColumnDescription = "ColumnDescription";

        ///<summary>
        /// 记录主键
        /// </summary>
        [NonSerialized]
        public static string FieldRecordKey = "RecordKey";

        ///<summary>
        /// 原值主键
        ///</summary>
        [NonSerialized]
        public static string FieldOldKey = "OldKey";

        ///<summary>
        /// 原值
        ///</summary>
        [NonSerialized]
        public static string FieldOldValue = "OldValue";

        ///<summary>
        /// 现值主键
        ///</summary>
        [NonSerialized]
        public static string FieldNewKey = "NewKey";

        ///<summary>
        /// 现值
        ///</summary>
        [NonSerialized]
        public static string FieldNewValue = "NewValue";

        ///<summary>
        /// IPAddress 地址
        ///</summary>
        [NonSerialized]
        public static string FieldIPAddress = "IPAddress";

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
