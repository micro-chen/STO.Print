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
    /// BaseContactDetailsEntity
    /// 联络单明细表
    ///
    /// 修改记录
    ///
    ///     给人的？阅读状态
    /// 
    ///		2015-10-30 版本：2.0 JiRiGaLa 已回复。
    ///		2010-07-15 版本：1.0 JiRiGaLa 创建主键。
    ///
    /// 版本：1.0
    ///
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2015-10-30</date>
    /// </author>
    /// </summary>
    [Serializable]
    public partial class BaseContactDetailsEntity : BaseEntity
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
        /// 接收者姓名
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

        private int? isNew = 0;
        /// <summary>
        /// 是否新邮件
        /// </summary>
        public int? IsNew
        {
            get
            {
                return this.isNew;
            }
            set
            {
                this.isNew = value;
            }
        }

        private int? newComment = 0;
        /// <summary>
        /// 是否有新的评论
        /// </summary>
        public int? NewComment
        {
            get
            {
                return this.newComment;
            }
            set
            {
                this.newComment = value;
            }
        }

        private int? replied = 0;
        /// <summary>
        /// 已回复
        /// </summary>
        public int? Replied
        {
            get
            {
                return this.replied;
            }
            set
            {
                this.replied = value;
            }
        }

        private string lastViewIP = null;
        /// <summary>
        /// 最后阅读IP
        /// </summary>
        public string LastViewIP
        {
            get
            {
                return this.lastViewIP;
            }
            set
            {
                this.lastViewIP = value;
            }
        }

        private string lastViewDate = null;
        /// <summary>
        /// 最后阅读时间
        /// </summary>
        public string LastViewDate
        {
            get
            {
                return this.lastViewDate;
            }
            set
            {
                this.lastViewDate = value;
            }
        }

        private int? enabled = 0;
        /// <summary>
        /// 有新评论是否提示
        /// </summary>
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

        private int? deletionStateCode = 0;
        /// <summary>
        /// 删除标志
        /// </summary>
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
        /// 描述
        /// </summary>
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

        private DateTime? modifiedOn = null;
        /// <summary>
        /// 修改日期
        /// </summary>
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
        /// 构造函数
        /// </summary>
        public BaseContactDetailsEntity()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dataRow">数据行</param>
        public BaseContactDetailsEntity(DataRow dataRow)
        {
            this.GetFrom(dataRow);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dataReader">数据流</param>
        public BaseContactDetailsEntity(IDataReader dataReader)
        {
            this.GetFrom(dataReader);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dataTable">数据表</param>
        public BaseContactDetailsEntity(DataTable dataTable)
        {
            this.GetSingle(dataTable);
        }

        /// <summary>
        /// 从数据表读取
        /// </summary>
        /// <param name="dataTable">数据表</param>
        public BaseContactDetailsEntity GetSingle(DataTable dataTable)
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
            this.Id = BaseBusinessLogic.ConvertToString(dr[BaseContactDetailsEntity.FieldId]);
            this.ContactId = BaseBusinessLogic.ConvertToString(dr[BaseContactDetailsEntity.FieldContactId]);
            this.Category = BaseBusinessLogic.ConvertToString(dr[BaseContactDetailsEntity.FieldCategory]);
            this.ReceiverId = BaseBusinessLogic.ConvertToString(dr[BaseContactDetailsEntity.FieldReceiverId]);
            this.ReceiverRealName = BaseBusinessLogic.ConvertToString(dr[BaseContactDetailsEntity.FieldReceiverRealName]);
            this.IsNew = BaseBusinessLogic.ConvertToInt(dr[BaseContactDetailsEntity.FieldIsNew]);
            this.NewComment = BaseBusinessLogic.ConvertToInt(dr[BaseContactDetailsEntity.FieldNewComment]);
            this.Replied = BaseBusinessLogic.ConvertToInt(dr[BaseContactDetailsEntity.FieldReplied]);
            this.LastViewIP = BaseBusinessLogic.ConvertToString(dr[BaseContactDetailsEntity.FieldLastViewIP]);
            this.LastViewDate = BaseBusinessLogic.ConvertToString(dr[BaseContactDetailsEntity.FieldLastViewDate]);
            this.Enabled = BaseBusinessLogic.ConvertToInt(dr[BaseContactDetailsEntity.FieldEnabled]);
            this.DeletionStateCode = BaseBusinessLogic.ConvertToInt(dr[BaseContactDetailsEntity.FieldDeletionStateCode]);
            this.SortCode = BaseBusinessLogic.ConvertToInt(dr[BaseContactDetailsEntity.FieldSortCode]);
            this.Description = BaseBusinessLogic.ConvertToString(dr[BaseContactDetailsEntity.FieldDescription]);
            this.CreateOn = BaseBusinessLogic.ConvertToDateTime(dr[BaseContactDetailsEntity.FieldCreateOn]);
            this.CreateUserId = BaseBusinessLogic.ConvertToString(dr[BaseContactDetailsEntity.FieldCreateUserId]);
            this.CreateBy = BaseBusinessLogic.ConvertToString(dr[BaseContactDetailsEntity.FieldCreateBy]);
            this.ModifiedOn = BaseBusinessLogic.ConvertToDateTime(dr[BaseContactDetailsEntity.FieldModifiedOn]);
            this.ModifiedUserId = BaseBusinessLogic.ConvertToString(dr[BaseContactDetailsEntity.FieldModifiedUserId]);
            this.ModifiedBy = BaseBusinessLogic.ConvertToString(dr[BaseContactDetailsEntity.FieldModifiedBy]);
            return this;
        }

        ///<summary>
        /// 联络单明细表
        ///</summary>
        [NonSerialized]
        public static string TableName = "BaseContactDetails";

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
        /// 接收者主键
        ///</summary>
        [NonSerialized]
        public static string FieldReceiverId = "ReceiverId";

        ///<summary>
        /// 接收者姓名
        ///</summary>
        [NonSerialized]
        public static string FieldReceiverRealName = "ReceiverRealName";

        ///<summary>
        /// 是否新邮件
        ///</summary>
        [NonSerialized]
        public static string FieldIsNew = "IsNew";

        ///<summary>
        /// 是否有新的评论
        ///</summary>
        [NonSerialized]
        public static string FieldNewComment = "NewComment";

        ///<summary>
        /// 已回复
        ///</summary>
        [NonSerialized]
        public static string FieldReplied = "Replied";

        ///<summary>
        /// 最后阅读IP
        ///</summary>
        [NonSerialized]
        public static string FieldLastViewIP = "LastViewIP";

        ///<summary>
        /// 最后阅读时间
        ///</summary>
        [NonSerialized]
        public static string FieldLastViewDate = "LastViewDate";

        ///<summary>
        /// 有新评论是否提示
        ///</summary>
        [NonSerialized]
        public static string FieldEnabled = "Enabled";

        ///<summary>
        /// 删除标志
        ///</summary>
        [NonSerialized]
        public static string FieldDeletionStateCode = "DeletionStateCode";

        ///<summary>
        /// 排序码
        ///</summary>
        [NonSerialized]
        public static string FieldSortCode = "SortCode";

        ///<summary>
        /// 描述
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