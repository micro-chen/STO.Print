//-----------------------------------------------------------------------
// <copyright file="BaseContactEntity.cs" company="Hairihan">
//     Copyright (C) 2016 , All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Data;
using System.Runtime.Serialization;

namespace DotNet.Model
{
    using DotNet.Utilities;

    /// <summary>
    /// BaseContactEntity
    /// 联络单主表
    ///
    /// 修改记录
    ///
    ///		2015-12-29 版本：2.1 JiRiGaLa 允许下级可以看？
    ///		2015-10-30 版本：2.0 JiRiGaLa 必读、必回。
    ///                颜色、（标题样式 加粗、斜体、粗斜体）、来源部门、来源公司？
    ///                省、审核人、
    ///		2010-07-15 版本：1.0 JiRiGaLa 创建主键。
    ///
    /// 版本：2.1
    ///
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2015-12-29</date>
    /// </author>
    /// </summary>
    [Serializable]
    public partial class BaseContactEntity : BaseEntity
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

        private string parentId = null;
        /// <summary>
        /// 父主键
        /// </summary>
        public string ParentId
        {
            get
            {
                return this.parentId;
            }
            set
            {
                this.parentId = value;
            }
        }

        private string title = null;
        /// <summary>
        /// 主题
        /// </summary>
        public string Title
        {
            get
            {
                return this.title;
            }
            set
            {
                this.title = value;
            }
        }

        private string color = null;
        /// <summary>
        /// 颜色
        /// </summary>
        public string Color
        {
            get
            {
                return this.color;
            }
            set
            {
                this.color = value;
            }
        }

        private string style = null;
        /// <summary>
        /// 样式
        /// </summary>
        public string Style
        {
            get
            {
                return this.style;
            }
            set
            {
                this.style = value;
            }
        }

        private string contents = null;
        /// <summary>
        /// 内容
        /// </summary>
        public string Contents
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

        private string priority = null;
        /// <summary>
        /// 等级(置顶, 1,2,3,4)
        /// </summary>
        public string Priority
        {
            get
            {
                return this.priority;
            }
            set
            {
                this.priority = value;
            }
        }

        private int? cancelTopDay = 0;
        /// <summary>
        /// 取消置顶期限
        /// </summary>
        [DataMember]
        public int? CancelTopDay
        {
            get
            {
                return this.cancelTopDay;
            }
            set
            {
                this.cancelTopDay = value;
            }
        }

        private string source = null;
        /// <summary>
        /// 新闻来源(供稿人)
        /// </summary>
        [DataMember]
        public string Source
        {
            get
            {
                return this.source;
            }
            set
            {
                this.source = value;
            }
        }

        private string categoryCode = null;
        /// <summary>
        /// 分类
        /// </summary>
        [FieldDescription("分类")]
        [DataMember]
        public string CategoryCode
        {
            get
            {
                return this.categoryCode;
            }
            set
            {
                this.categoryCode = value;
            }
        }

        private string labelMark = null;
        /// <summary>
        /// 标签
        /// </summary>
        [FieldDescription("标签")]
        [DataMember]
        public string LabelMark
        {
            get
            {
                return this.labelMark;
            }
            set
            {
                this.labelMark = value;
            }
        }

        private int? sendCount = 0;
        /// <summary>
        /// 发送邮件总数
        /// </summary>
        public int? SendCount
        {
            get
            {
                return this.sendCount;
            }
            set
            {
                this.sendCount = value;
            }
        }

        private int? readCount = 0;
        /// <summary>
        /// 已阅读人数
        /// </summary>
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

        private int? replyCount = 0;
        /// <summary>
        /// 回复数
        /// </summary>
        public int? ReplyCount
        {
            get
            {
                return this.replyCount;
            }
            set
            {
                this.replyCount = value;
            }
        }

        private int? mustRead = 0;
        /// <summary>
        /// 必读
        /// </summary>
        public int? MustRead
        {
            get
            {
                return this.mustRead;
            }
            set
            {
                this.mustRead = value;
            }
        }

        private int? mustReply = 0;
        /// <summary>
        /// 必须回复
        /// </summary>
        public int? MustReply
        {
            get
            {
                return this.mustReply;
            }
            set
            {
                this.mustReply = value;
            }
        }

        private int? isOpen = 0;
        /// <summary>
        /// 是否公开（允许下属网点看）
        /// </summary>
        public int? IsOpen
        {
            get
            {
                return this.isOpen;
            }
            set
            {
                this.isOpen = value;
            }
        }

        private string ipAddress = null;
        /// <summary>
        /// IP地址
        /// </summary>
        public string IPAddress
        {
            get
            {
                return this.ipAddress;
            }
            set
            {
                this.ipAddress = value;
            }
        }

        private int? allowComments = 0;
        /// <summary>
        /// 允许评论
        /// </summary>
        public int? AllowComments
        {
            get
            {
                return this.allowComments;
            }
            set
            {
                this.allowComments = value;
            }
        }

        private string commentUserId = null;
        /// <summary>
        /// 最后评论人主键
        /// </summary>
        public string CommentUserId
        {
            get
            {
                return this.commentUserId;
            }
            set
            {
                this.commentUserId = value;
            }
        }

        private string commentUserRealName = null;
        /// <summary>
        /// 最后评论人姓名
        /// </summary>
        public string CommentUserRealName
        {
            get
            {
                return this.commentUserRealName;
            }
            set
            {
                this.commentUserRealName = value;
            }
        }

        private DateTime? commentDate = null;
        /// <summary>
        /// 最后评论时间
        /// </summary>
        public DateTime? CommentDate
        {
            get
            {
                return this.commentDate;
            }
            set
            {
                this.commentDate = value;
            }
        }

        private int? deletionStateCode = 0;
        /// <summary>
        /// 是否删除
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

        private string auditStatus = null;
        /// <summary>
        /// 审核状态
        /// </summary>
        [DataMember]
        public string AuditStatus
        {
            get
            {
                return this.auditStatus;
            }
            set
            {
                this.auditStatus = value;
            }
        }

        private string auditUserId = null;
        /// <summary>
        /// 审核员主键
        /// </summary>
        [DataMember]
        public string AuditUserId
        {
            get
            {
                return this.auditUserId;
            }
            set
            {
                this.auditUserId = value;
            }
        }

        private string auditUserRealName = null;
        /// <summary>
        /// 审核员
        /// </summary>
        [DataMember]
        public string AuditUserRealName
        {
            get
            {
                return this.auditUserRealName;
            }
            set
            {
                this.auditUserRealName = value;
            }
        }

        private int? enabled = 0;
        /// <summary>
        /// 有效
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

        private string createDepartment = null;
        /// <summary>
        /// 来源部门
        /// </summary>
        public string CreateDepartment
        {
            get
            {
                return this.createDepartment;
            }
            set
            {
                this.createDepartment = value;
            }
        }

        private string createCompanyId = null;
        /// <summary>
        /// 创建公司主键
        /// </summary>
        public string CreateCompanyId
        {
            get
            {
                return this.createCompanyId;
            }
            set
            {
                this.createCompanyId = value;
            }
        }

        private string createCompany = null;
        /// <summary>
        /// 来源公司
        /// </summary>
        public string CreateCompany
        {
            get
            {
                return this.createCompany;
            }
            set
            {
                this.createCompany = value;
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
        public BaseContactEntity()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dataRow">数据行</param>
        public BaseContactEntity(DataRow dataRow)
        {
            this.GetFrom(dataRow);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dataReader">数据流</param>
        public BaseContactEntity(IDataReader dataReader)
        {
            this.GetFrom(dataReader);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dataTable">数据表</param>
        public BaseContactEntity(DataTable dataTable)
        {
            this.GetSingle(dataTable);
        }

        /// <summary>
        /// 从数据行读取
        /// </summary>
        /// <param name="dataRow">数据行</param>
        protected override BaseEntity GetFrom(IDataRow dr)
        {
            this.GetFromExpand(dr);
            this.Id = BaseBusinessLogic.ConvertToString(dr[BaseContactEntity.FieldId]);
            this.ParentId = BaseBusinessLogic.ConvertToString(dr[BaseContactEntity.FieldParentId]);
            this.Title = BaseBusinessLogic.ConvertToString(dr[BaseContactEntity.FieldTitle]);
            this.Color = BaseBusinessLogic.ConvertToString(dr[BaseContactEntity.FieldColor]);
            this.Style = BaseBusinessLogic.ConvertToString(dr[BaseContactEntity.FieldStyle]);
            this.Contents = BaseBusinessLogic.ConvertToString(dr[BaseContactEntity.FieldContents]);
            this.CategoryCode = BaseBusinessLogic.ConvertToString(dr[BaseContactEntity.FieldCategoryCode]);
            this.LabelMark = BaseBusinessLogic.ConvertToString(dr[BaseContactEntity.FieldLabelMark]);
            this.Priority = BaseBusinessLogic.ConvertToString(dr[BaseContactEntity.FieldPriority]);
            this.SendCount = BaseBusinessLogic.ConvertToInt(dr[BaseContactEntity.FieldSendCount]);
            this.ReadCount = BaseBusinessLogic.ConvertToInt(dr[BaseContactEntity.FieldReadCount]);
            this.Source = BaseBusinessLogic.ConvertToString(dr[BaseContactEntity.FieldSource]);
            this.IsOpen = BaseBusinessLogic.ConvertToInt(dr[BaseContactEntity.FieldIsOpen]);
            this.IPAddress = BaseBusinessLogic.ConvertToString(dr[BaseContactEntity.FieldIPAddress]);
            this.AllowComments = BaseBusinessLogic.ConvertToInt(dr[BaseContactEntity.FieldAllowComments]);
            this.ReplyCount = BaseBusinessLogic.ConvertToInt(dr[BaseContactEntity.FieldReplyCount]);
            this.MustRead = BaseBusinessLogic.ConvertToInt(dr[BaseContactEntity.FieldMustRead]);
            this.MustReply = BaseBusinessLogic.ConvertToInt(dr[BaseContactEntity.FieldMustReply]);
            this.CommentUserId = BaseBusinessLogic.ConvertToString(dr[BaseContactEntity.FieldCommentUserId]);
            this.CommentUserRealName = BaseBusinessLogic.ConvertToString(dr[BaseContactEntity.FieldCommentUserRealName]);
            this.CancelTopDay = BaseBusinessLogic.ConvertToInt(dr[BaseContactEntity.FieldCancelTopDay]);
            this.CommentDate = BaseBusinessLogic.ConvertToDateTime(dr[BaseContactEntity.FieldCommentDate]);
            this.DeletionStateCode = BaseBusinessLogic.ConvertToInt(dr[BaseContactEntity.FieldDeletionStateCode]);
            this.Enabled = BaseBusinessLogic.ConvertToInt(dr[BaseContactEntity.FieldEnabled]);
            this.AuditUserId = BaseBusinessLogic.ConvertToString(dr[BaseContactEntity.FieldAuditUserId]);
            this.AuditUserRealName = BaseBusinessLogic.ConvertToString(dr[BaseContactEntity.FieldAuditUserRealName]);
            this.AuditStatus = BaseBusinessLogic.ConvertToString(dr[BaseContactEntity.FieldAuditStatus]);
            this.SortCode = BaseBusinessLogic.ConvertToInt(dr[BaseContactEntity.FieldSortCode]);
            this.Description = BaseBusinessLogic.ConvertToString(dr[BaseContactEntity.FieldDescription]);
            this.CreateOn = BaseBusinessLogic.ConvertToDateTime(dr[BaseContactEntity.FieldCreateOn]);
            this.CreateUserId = BaseBusinessLogic.ConvertToString(dr[BaseContactEntity.FieldCreateUserId]);
            this.CreateBy = BaseBusinessLogic.ConvertToString(dr[BaseContactEntity.FieldCreateBy]);
            this.CreateDepartment = BaseBusinessLogic.ConvertToString(dr[BaseContactEntity.FieldCreateDepartment]);
            this.CreateCompany = BaseBusinessLogic.ConvertToString(dr[BaseContactEntity.FieldCreateCompany]);
            this.CreateCompanyId = BaseBusinessLogic.ConvertToString(dr[BaseContactEntity.FieldCreateCompanyId]);
            this.ModifiedOn = BaseBusinessLogic.ConvertToDateTime(dr[BaseContactEntity.FieldModifiedOn]);
            this.ModifiedUserId = BaseBusinessLogic.ConvertToString(dr[BaseContactEntity.FieldModifiedUserId]);
            this.ModifiedBy = BaseBusinessLogic.ConvertToString(dr[BaseContactEntity.FieldModifiedBy]);
            return this;
        }

        ///<summary>
        /// 联络单主表
        ///</summary>
        [NonSerialized]
        public static string TableName = "BaseContact";

        ///<summary>
        /// 主键
        ///</summary>
        [NonSerialized]
        public static string FieldId = "Id";

        ///<summary>
        /// 父主键
        ///</summary>
        [NonSerialized]
        public static string FieldParentId = "ParentId";

        ///<summary>
        /// 主题
        ///</summary>
        [NonSerialized]
        public static string FieldTitle = "Title";

        ///<summary>
        /// 颜色
        ///</summary>
        [NonSerialized]
        public static string FieldColor = "Color";

        ///<summary>
        /// 样式
        ///</summary>
        [NonSerialized]
        public static string FieldStyle = "Style";

        ///<summary>
        /// 内容
        ///</summary>
        [NonSerialized]
        public static string FieldContents = "Contents";

        ///<summary>
        /// 邮件等级
        ///</summary>
        [NonSerialized]
        public static string FieldPriority = "Priority";

        ///<summary>
        /// 取消置顶期限
        ///</summary>
        [NonSerialized]
        public static string FieldCancelTopDay = "CancelTopDay";

        ///<summary>
        /// 发送邮件总数
        ///</summary>
        [NonSerialized]
        public static string FieldSendCount = "SendCount";

        ///<summary>
        /// 已阅读人数
        ///</summary>
        [NonSerialized]
        public static string FieldReadCount = "ReadCount";

        /// <summary>
        /// 回复数量
        /// </summary>
        [NonSerialized]
        public static string FieldReplyCount = "ReplyCount";

        ///<summary>
        /// 是否公开
        ///</summary>
        [NonSerialized]
        public static string FieldIsOpen = "IsOpen";

        ///<summary>
        /// IP地址
        ///</summary>
        [NonSerialized]
        public static string FieldIPAddress = "IPAddress";

        ///<summary>
        /// 允许评论
        ///</summary>
        [NonSerialized]
        public static string FieldAllowComments = "AllowComments";

        ///<summary>
        /// 必读
        ///</summary>
        [NonSerialized]
        public static string FieldMustRead = "MustRead";

        ///<summary>
        /// 必须回复
        ///</summary>
        [NonSerialized]
        public static string FieldMustReply = "MustReply";

        ///<summary>
        /// 最后评论人主键
        ///</summary>
        [NonSerialized]
        public static string FieldCommentUserId = "CommentUserId";

        ///<summary>
        /// 最后评论人姓名
        ///</summary>
        [NonSerialized]
        public static string FieldCommentUserRealName = "CommentUserRealName";

        ///<summary>
        /// 最后评论时间
        ///</summary>
        [NonSerialized]
        public static string FieldCommentDate = "CommentDate";

        ///<summary>
        /// 是否删除
        ///</summary>
        [NonSerialized]
        public static string FieldDeletionStateCode = "DeletionStateCode";

        ///<summary>
        /// 有效
        ///</summary>
        [NonSerialized]
        public static string FieldEnabled = "Enabled";

        ///<summary>
        /// 审核状态
        ///</summary>
        [NonSerialized]
        public static string FieldAuditStatus = "AuditStatus";

        ///<summary>
        /// 审核员主键
        ///</summary>
        [NonSerialized]
        public static string FieldAuditUserId = "AuditUserId";

        ///<summary>
        /// 审核员
        ///</summary>
        [NonSerialized]
        public static string FieldAuditUserRealName = "AuditUserRealName";

        ///<summary>
        /// 排序码
        ///</summary>
        [NonSerialized]
        public static string FieldSortCode = "SortCode";

        ///<summary>
        /// 新闻来源
        ///</summary>
        [NonSerialized]
        public static string FieldSource = "Source";

        ///<summary>
        /// 分类
        ///</summary>
        [NonSerialized]
        public static string FieldCategoryCode = "CategoryCode";

        ///<summary>
        /// 标签
        ///</summary>
        [NonSerialized]
        public static string FieldLabelMark = "LabelMark";

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
        /// 来源部门
        ///</summary>
        [NonSerialized]
        public static string FieldCreateDepartment = "CreateDepartment";

        ///<summary>
        /// 来源公司
        ///</summary>
        [NonSerialized]
        public static string FieldCreateCompany = "CreateCompany";

        ///<summary>
        /// 来源公司主键
        ///</summary>
        [NonSerialized]
        public static string FieldCreateCompanyId = "CreateCompanyId";

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
