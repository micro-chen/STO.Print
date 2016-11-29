//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd. 
//-----------------------------------------------------------------

using System;
using System.Runtime.Serialization;

namespace DotNet.Model
{
    using DotNet.Utilities;

    /// <summary>
    /// BaseNewsEntity
    /// 新闻表
    ///
    /// 修改记录
    ///
    ///		2015-06-07 版本：2.0 JiRiGaLa 公司主键。
    ///		2010-07-28 版本：1.0 JiRiGaLa 创建主键。
    ///
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2015-06-07</date>
    /// </author>
    /// </summary>
    [Serializable, DataContract]
	public partial class BaseNewsEntity : BaseEntity
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

        /// <summary>
        /// 公司主键
        /// </summary>
        [DataMember]
        public String CompanyId { get; set; }

        /// <summary>
        /// 公司名称
        /// </summary>
        [DataMember]
        public String CompanyName { get; set; }

        /// <summary>
        /// 部门主键
        /// </summary>
        [DataMember]
        public String DepartmentId { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        [DataMember]
        public String DepartmentName { get; set; }

        private string categoryCode = null;
        /// <summary>
        /// 分类编号
        /// </summary>
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

        private string code = null;
        /// <summary>
        /// 编号
        /// </summary>
        [DataMember]
        public string Code
        {
            get
            {
                return this.code;
            }
            set
            {
                this.code = value;
            }
        }

        private string title = null;
        /// <summary>
        /// 标题
        /// </summary>
        [DataMember]
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

        private string introduction = null;
        /// <summary>
        /// 内容简介
        /// </summary>
        [DataMember]
        public string Introduction
        {
            get
            {
                return this.introduction;
            }
            set
            {
                this.introduction = value;
            }
        }

        private string contents = null;
        /// <summary>
        /// 内容
        /// </summary>
        [DataMember]
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

        private string source = null;
        /// <summary>
        /// 新闻来源
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

        private string keywords = null;
        /// <summary>
        /// 关键字
        /// </summary>
        [DataMember]
        public string Keywords
        {
            get
            {
                return this.keywords;
            }
            set
            {
                this.keywords = value;
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

        private string imageUrl = null;
        /// <summary>
        /// 图片文件位置(图片新闻)
        /// </summary>
        [DataMember]
        public string ImageUrl
        {
            get
            {
                return this.imageUrl;
            }
            set
            {
                this.imageUrl = value;
            }
        }

        private int? homePage = 0;
        /// <summary>
        /// 置首页
        /// </summary>
        [DataMember]
        public int? HomePage
        {
            get
            {
                return this.homePage;
            }
            set
            {
                this.homePage = value;
            }
        }

        private int? subPage = 0;
        /// <summary>
        /// 置二级页
        /// </summary>
        [DataMember]
        public int? SubPage
        {
            get
            {
                return this.subPage;
            }
            set
            {
                this.subPage = value;
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
            Id = BaseBusinessLogic.ConvertToString(dr[BaseNewsEntity.FieldId]);
            CompanyId = BaseBusinessLogic.ConvertToString(dr[BaseNewsEntity.FieldCompanyId]);
            CompanyName = BaseBusinessLogic.ConvertToString(dr[BaseNewsEntity.FieldCompanyName]);
            DepartmentId = BaseBusinessLogic.ConvertToString(dr[BaseNewsEntity.FieldDepartmentId]);
            DepartmentName = BaseBusinessLogic.ConvertToString(dr[BaseNewsEntity.FieldDepartmentName]);
            FolderId = BaseBusinessLogic.ConvertToString(dr[BaseNewsEntity.FieldFolderId]);
            CategoryCode = BaseBusinessLogic.ConvertToString(dr[BaseNewsEntity.FieldCategoryCode]);
            Code = BaseBusinessLogic.ConvertToString(dr[BaseNewsEntity.FieldCode]);
            Title = BaseBusinessLogic.ConvertToString(dr[BaseNewsEntity.FieldTitle]);
            FilePath = BaseBusinessLogic.ConvertToString(dr[BaseNewsEntity.FieldFilePath]);
            Introduction = BaseBusinessLogic.ConvertToString(dr[BaseNewsEntity.FieldIntroduction]);
            Contents = BaseBusinessLogic.ConvertToString(dr[BaseNewsEntity.FieldContents]);
            Source = BaseBusinessLogic.ConvertToString(dr[BaseNewsEntity.FieldSource]);
            Keywords = BaseBusinessLogic.ConvertToString(dr[BaseNewsEntity.FieldKeywords]);
            FileSize = BaseBusinessLogic.ConvertToInt(dr[BaseNewsEntity.FieldFileSize]);
            ImageUrl = BaseBusinessLogic.ConvertToString(dr[BaseNewsEntity.FieldImageUrl]);
            SubPage = BaseBusinessLogic.ConvertToInt(dr[BaseNewsEntity.FieldSubPage]);
            HomePage = BaseBusinessLogic.ConvertToInt(dr[BaseNewsEntity.FieldHomePage]);
            AuditStatus = BaseBusinessLogic.ConvertToString(dr[BaseNewsEntity.FieldAuditStatus]);
            ReadCount = BaseBusinessLogic.ConvertToInt(dr[BaseNewsEntity.FieldReadCount]);
            DeletionStateCode = BaseBusinessLogic.ConvertToInt(dr[BaseNewsEntity.FieldDeletionStateCode]);
            Description = BaseBusinessLogic.ConvertToString(dr[BaseNewsEntity.FieldDescription]);
            Enabled = BaseBusinessLogic.ConvertToInt(dr[BaseNewsEntity.FieldEnabled]);
            SortCode = BaseBusinessLogic.ConvertToInt(dr[BaseNewsEntity.FieldSortCode]);
            CreateOn = BaseBusinessLogic.ConvertToNullableDateTime(dr[BaseNewsEntity.FieldCreateOn]);
            CreateBy = BaseBusinessLogic.ConvertToString(dr[BaseNewsEntity.FieldCreateBy]);
            CreateUserId = BaseBusinessLogic.ConvertToString(dr[BaseNewsEntity.FieldCreateUserId]);
            ModifiedOn = BaseBusinessLogic.ConvertToNullableDateTime(dr[BaseNewsEntity.FieldModifiedOn]);
            ModifiedBy = BaseBusinessLogic.ConvertToString(dr[BaseNewsEntity.FieldModifiedBy]);
            ModifiedUserId = BaseBusinessLogic.ConvertToString(dr[BaseNewsEntity.FieldModifiedUserId]);
            // 获取扩展属性
            GetFromExpand(dr);
            return this;
        }

        ///<summary>
        /// 新闻表
        ///</summary>
        [NonSerialized]
        public static string TableName = "BaseNews";

        ///<summary>
        /// 代码
        ///</summary>
        [NonSerialized]
        public static string FieldId = "Id";

        ///<summary>
        /// 文件夹节点代码
        ///</summary>
        [NonSerialized]
        public static string FieldFolderId = "FolderId";

        ///<summary>
        /// 公司主键
        ///</summary>
        [NonSerialized]
        public static string FieldCompanyId = "CompanyId";

        ///<summary>
        /// 公司名称
        ///</summary>
        [NonSerialized]
        public static string FieldCompanyName = "CompanyName";

        ///<summary>
        /// 部门主键
        ///</summary>
        [NonSerialized]
        public static string FieldDepartmentId = "DepartmentId";

        ///<summary>
        /// 部门名称
        ///</summary>
        [NonSerialized]
        public static string FieldDepartmentName = "DepartmentName";

        ///<summary>
        /// 文件类别码
        ///</summary>
        [NonSerialized]
        public static string FieldCategoryCode = "CategoryCode";

        ///<summary>
        /// 文件编号
        ///</summary>
        [NonSerialized]
        public static string FieldCode = "Code";

        ///<summary>
        /// 文件名
        ///</summary>
        [NonSerialized]
        public static string FieldTitle = "Title";

        ///<summary>
        /// 文件路径
        ///</summary>
        [NonSerialized]
        public static string FieldFilePath = "FilePath";

        ///<summary>
        /// 内容简介
        ///</summary>
        [NonSerialized]
        public static string FieldIntroduction = "Introduction";

        ///<summary>
        /// 文件内容
        ///</summary>
        [NonSerialized]
        public static string FieldContents = "Contents";

        ///<summary>
        /// 新闻来源
        ///</summary>
        [NonSerialized]
        public static string FieldSource = "Source";

        ///<summary>
        /// 关键字
        ///</summary>
        [NonSerialized]
        public static string FieldKeywords = "Keywords";

        ///<summary>
        /// 文件大小
        ///</summary>
        [NonSerialized]
        public static string FieldFileSize = "FileSize";

        ///<summary>
        /// 图片文件位置(图片新闻)
        ///</summary>
        [NonSerialized]
        public static string FieldImageUrl = "ImageUrl";

        ///<summary>
        /// 置首页
        ///</summary>
        [NonSerialized]
        public static string FieldHomePage = "HomePage";

        ///<summary>
        /// 置二级页
        ///</summary>
        [NonSerialized]
        public static string FieldSubPage = "SubPage";

        ///<summary>
        /// 审核状态
        ///</summary>
        [NonSerialized]
        public static string FieldAuditStatus = "AuditStatus";

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
