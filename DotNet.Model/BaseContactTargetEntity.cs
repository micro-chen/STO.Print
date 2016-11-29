//-----------------------------------------------------------------------
// <copyright file="BaseContactDetailsEntity.cs" company="Hairihan">
//     Copyright (C) 2016 , All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Data;

namespace DotNet.Model
{
    using DotNet.Utilities;

    /// <summary>
    /// BaseContactTargetEntity
    /// 联络单按目标发送的细表
    ///
    /// 修改记录
    ///
    ///		2015-09-01 版本：1.0 JiRiGaLa 创建主键。
    ///
    /// 版本：1.0
    ///
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2015-09-01</date>
    /// </author>
    /// </summary>
    [Serializable]
    public partial class BaseContactTargetEntity : BaseEntity
    {
        private string id = null;
        /// <summary>
        /// 主键
        /// </summary>
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

        private string contactId = null;
        /// <summary>
        /// 联络单主主键
        /// </summary>
        public string ContactId
        {
            get
            {
                return this.contactId;
            }
            set
            {
                this.contactId = value;
            }
        }

        private string category = null;
        /// <summary>
        /// 接收者分类
        /// </summary>
        public string Category
        {
            get
            {
                return this.category;
            }
            set
            {
                this.category = value;
            }
        }

        private string receiverId = null;
        /// <summary>
        /// 接收者主键
        /// </summary>
        public string ReceiverId
        {
            get
            {
                return this.receiverId;
            }
            set
            {
                this.receiverId = value;
            }
        }

        private string receiverRealName = null;
        /// <summary>
        /// 接收者名称
        /// </summary>
        public string ReceiverRealName
        {
            get
            {
                return this.receiverRealName;
            }
            set
            {
                this.receiverRealName = value;
            }
        }

        private DateTime? createOn = null;
        /// <summary>
        /// 创建日期
        /// </summary>
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

        /// <summary>
        /// 构造函数
        /// </summary>
        public BaseContactTargetEntity()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dataRow">数据行</param>
        public BaseContactTargetEntity(DataRow dataRow)
        {
            this.GetFrom(dataRow);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dataReader">数据流</param>
        public BaseContactTargetEntity(IDataReader dataReader)
        {
            this.GetFrom(dataReader);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dataTable">数据表</param>
        public BaseContactTargetEntity(DataTable dataTable)
        {
            this.GetSingle(dataTable);
        }

        /// <summary>
        /// 从数据表读取
        /// </summary>
        /// <param name="dataTable">数据表</param>
        public BaseContactTargetEntity GetSingle(DataTable dataTable)
        {
            if ((dataTable == null) || (dataTable.Rows.Count == 0))
            {
                return null;
            }
            foreach (DataRow dataRow in dataTable.Rows)
            {
                this.GetFrom(dataRow);
                break;
            }
            return this;
        }

        /// <summary>
        /// 从数据流读取
        /// </summary>
        /// <param name="dr">数据流</param>
        protected override BaseEntity GetFrom(IDataRow dr)
        {
            this.GetFromExpand(dr);
            this.Id = BaseBusinessLogic.ConvertToString(dr[BaseContactTargetEntity.FieldId]);
            this.ContactId = BaseBusinessLogic.ConvertToString(dr[BaseContactTargetEntity.FieldContactId]);
            this.Category = BaseBusinessLogic.ConvertToString(dr[BaseContactTargetEntity.FieldCategory]);
            this.ReceiverId = BaseBusinessLogic.ConvertToString(dr[BaseContactTargetEntity.FieldReceiverId]);
            this.ReceiverRealName = BaseBusinessLogic.ConvertToString(dr[BaseContactTargetEntity.FieldReceiverRealName]);
            this.CreateOn = BaseBusinessLogic.ConvertToDateTime(dr[BaseContactTargetEntity.FieldCreateOn]);
            this.CreateUserId = BaseBusinessLogic.ConvertToString(dr[BaseContactTargetEntity.FieldCreateUserId]);
            this.CreateBy = BaseBusinessLogic.ConvertToString(dr[BaseContactTargetEntity.FieldCreateBy]);
            return this;
        }

        ///<summary>
        /// 联络单明细表
        ///</summary>
        [NonSerialized]
        public static string TableName = "BaseContactTarget";

        ///<summary>
        /// 主键
        ///</summary>
        [NonSerialized]
        public static string FieldId = "Id";

        ///<summary>
        /// 联络单主主键
        ///</summary>
        [NonSerialized]
        public static string FieldContactId = "ContactId";

        ///<summary>
        /// 接收者分类
        ///</summary>
        [NonSerialized]
        public static string FieldCategory = "Category";

        ///<summary>
        /// 接收区域主键
        ///</summary>
        [NonSerialized]
        public static string FieldReceiverId = "ReceiverId";

        ///<summary>
        /// 接收区域名称
        ///</summary>
        [NonSerialized]
        public static string FieldReceiverRealName = "ReceiverRealName";

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