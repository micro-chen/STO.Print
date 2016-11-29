//--------------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd. 
//--------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.Serialization;

namespace DotNet.Model
{
    using DotNet.Utilities;

    /// <summary>
    /// BaseFolderEntity
    /// 文件夹表
    /// 
    /// 修改记录
    /// 
    /// 2012-05-17 版本：1.1 Serwif 补充完整AllowEdit,AllowDelete
    /// 2012-05-17 版本：1.0 JiRiGaLa 创建主键。
    /// 
    /// <author>
    ///     <name>JiRiGaLa</name>
    ///     <date>2012-05-17</date>
    /// </author>
    /// </summary>
    [Serializable, DataContract]
	public partial class BaseFolderEntity : BaseEntity
    {
        /// <summary>
        /// 主键
        /// </summary>
        [DataMember]
        public String Id { get; set; }

        /// <summary>
        /// 父亲节点主键
        /// </summary>
        [DataMember]
        public String ParentId { get; set; }

        /// <summary>
        /// 文件夹名
        /// </summary>
        [DataMember]
        public String FolderName { get; set; }

        /// <summary>
        /// 排序码
        /// </summary>
        [DataMember]
        public int? SortCode { get; set; }

        public int? allowEdit = 1;
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

        public int? allowDelete = 1;
        /// <summary>
        /// 允许删除
        /// </summary>
        [DataMember]
        public int? AllowDelete
        {
            get {
                return this.allowDelete;
            }
            set { this.allowDelete = value; }
        }

        /// <summary>
        /// 是公开
        /// </summary>
        [DataMember]
        public int? IsPublic { get; set; }

        /// <summary>
        /// 有效
        /// </summary>
        [DataMember]
        public int? Enabled { get; set; }

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

        /// <summary>
        /// 备注
        /// </summary>
        [DataMember]
        public String Description { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        [DataMember]
        public DateTime? CreateOn { get; set; }

        /// <summary>
        /// 创建用户主键
        /// </summary>
        [DataMember]
        public String CreateUserId { get; set; }

        /// <summary>
        /// 创建用户
        /// </summary>
        [DataMember]
        public String CreateBy { get; set; }

        /// <summary>
        /// 修改日期
        /// </summary>
        [DataMember]
        public DateTime? ModifiedOn { get; set; }

        /// <summary>
        /// 修改用户主键
        /// </summary>
        [DataMember]
        public String ModifiedUserId { get; set; }

        /// <summary>
        /// 修改用户
        /// </summary>
        [DataMember]
        public String ModifiedBy { get; set; }

        /// <summary>
        /// 从数据行读取
        /// </summary>
        /// <param name="dr">数据行</param>
		protected override BaseEntity GetFrom(IDataRow dr)
        {
            Id = BaseBusinessLogic.ConvertToString(dr[BaseFolderEntity.FieldId]);
            ParentId = BaseBusinessLogic.ConvertToString(dr[BaseFolderEntity.FieldParentId]);
            FolderName = BaseBusinessLogic.ConvertToString(dr[BaseFolderEntity.FieldFolderName]);
            SortCode = BaseBusinessLogic.ConvertToInt(dr[BaseFolderEntity.FieldSortCode]);
            AllowEdit = BaseBusinessLogic.ConvertToInt(dr[BaseFolderEntity.FieldAllowEdit]);
            AllowDelete = BaseBusinessLogic.ConvertToInt(dr[BaseFolderEntity.FieldAllowDelete]);
            IsPublic = BaseBusinessLogic.ConvertToInt(dr[BaseFolderEntity.FieldIsPublic]);
            Enabled = BaseBusinessLogic.ConvertToInt(dr[BaseFolderEntity.FieldEnabled]);
            DeletionStateCode = BaseBusinessLogic.ConvertToInt(dr[BaseFolderEntity.FieldDeletionStateCode]);
            Description = BaseBusinessLogic.ConvertToString(dr[BaseFolderEntity.FieldDescription]);
            CreateOn = BaseBusinessLogic.ConvertToNullableDateTime(dr[BaseFolderEntity.FieldCreateOn]);
            CreateUserId = BaseBusinessLogic.ConvertToString(dr[BaseFolderEntity.FieldCreateUserId]);
            CreateBy = BaseBusinessLogic.ConvertToString(dr[BaseFolderEntity.FieldCreateBy]);
            ModifiedOn = BaseBusinessLogic.ConvertToNullableDateTime(dr[BaseFolderEntity.FieldModifiedOn]);
            ModifiedUserId = BaseBusinessLogic.ConvertToString(dr[BaseFolderEntity.FieldModifiedUserId]);
            ModifiedBy = BaseBusinessLogic.ConvertToString(dr[BaseFolderEntity.FieldModifiedBy]);
            // 获取扩展属性
            GetFromExpand(dr);
            return this;
        }

        ///<summary>
        /// 文件夹表
        ///</summary>
        [NonSerialized]
        public static string TableName = "BaseFolder";

        ///<summary>
        /// 主键
        ///</summary>
        [NonSerialized]
        public static string FieldId = "Id";

        ///<summary>
        /// 父亲节点主键
        ///</summary>
        [NonSerialized]
        public static string FieldParentId = "ParentId";

        ///<summary>
        /// 文件夹名
        ///</summary>
        [NonSerialized]
        public static string FieldFolderName = "FolderName";

        ///<summary>
        /// 排序码
        ///</summary>
        [NonSerialized]
        public static string FieldSortCode = "SortCode";

        ///<summary>
        /// 允许编辑
        ///</summary>
        [NonSerialized]
        public static string FieldAllowEdit = "AllowEdit";

        ///<summary>
        /// 备注
        ///</summary>
        [NonSerialized]
        public static string FieldAllowDelete = "AllowDelete";

        ///<summary>
        /// 是公开
        ///</summary>
        [NonSerialized]
        public static string FieldIsPublic = "IsPublic";

        ///<summary>
        /// 有效
        ///</summary>
        [NonSerialized]
        public static string FieldEnabled = "Enabled";

        ///<summary>
        /// 状态
        ///</summary>
        [NonSerialized]
        public static string FieldDeletionStateCode = "DeletionStateCode";

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
