//-----------------------------------------------------------------------
// <copyright file="BaseCommentEntity.cs" company="Hairihan">
//     Copyright (C) 2016 , All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Runtime.Serialization;

namespace DotNet.Model
{
    using DotNet.Utilities;

    /// <summary>
    /// BaseCommentEntity
    /// 评论表
    /// 
    /// 修改记录
    /// 
    /// 2012-05-14 版本：1.0 JiRiGaLa 创建主键。
    /// Important，PriorityId。
    /// 
    /// <author>
    ///     <name>JiRiGaLa</name>
    ///     <date>2012-05-14</date>
    /// </author>
    /// </summary>
    [Serializable, DataContract]
	public partial class BaseCommentEntity : BaseEntity
    {
        /// <summary>
        /// 主键
        /// </summary>
        [DataMember]
        public String Id { get; set; }

        /// <summary>
        /// 部门主键
        /// </summary>
        [DataMember]
        public string DepartmentId { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        [DataMember]
        public String DepartmentName { get; set; }

        /// <summary>
        /// 父亲节点主键
        /// </summary>
        [DataMember]
        public string ParentId { get; set; }

        /// <summary>
        /// 分类编号
        /// </summary>
        [DataMember]
        public String CategoryCode { get; set; }

        /// <summary>
        /// 唯一识别主键
        /// </summary>
        [DataMember]
        public String ObjectId { get; set; }

        /// <summary>
        /// 消息的指向网页页面
        /// </summary>
        [DataMember]
        public String TargetURL { get; set; }

        /// <summary>
        /// 主题
        /// </summary>
        [DataMember]
        public String Title { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        [DataMember]
        public String Contents { get; set; }

        /// <summary>
        /// IP地址
        /// </summary>
        [DataMember]
        public String IPAddress { get; set; }

        // <summary>
        /// 已被处理标志
        /// </summary>
        [DataMember]
        public int? Worked { get; set; }

        /// <summary>
        /// 删除标志
        /// </summary>
        [DataMember]
        public int? DeletionStateCode { get; set; }

        /// <summary>
        /// 有效
        /// </summary>
        [DataMember]
        public int? Enabled { get; set; }

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
            Id = BaseBusinessLogic.ConvertToString(dr[BaseCommentEntity.FieldId]);
            DepartmentId = BaseBusinessLogic.ConvertToString(dr[BaseCommentEntity.FieldDepartmentId]);
            DepartmentName = BaseBusinessLogic.ConvertToString(dr[BaseCommentEntity.FieldDepartmentName]);
            ParentId = BaseBusinessLogic.ConvertToString(dr[BaseCommentEntity.FieldParentId]);
            CategoryCode = BaseBusinessLogic.ConvertToString(dr[BaseCommentEntity.FieldCategoryCode]);
            ObjectId = BaseBusinessLogic.ConvertToString(dr[BaseCommentEntity.FieldObjectId]);
            TargetURL = BaseBusinessLogic.ConvertToString(dr[BaseCommentEntity.FieldTargetURL]);
            Title = BaseBusinessLogic.ConvertToString(dr[BaseCommentEntity.FieldTitle]);
            Contents = BaseBusinessLogic.ConvertToString(dr[BaseCommentEntity.FieldContents]);
            IPAddress = BaseBusinessLogic.ConvertToString(dr[BaseCommentEntity.FieldIPAddress]);
            Worked = BaseBusinessLogic.ConvertToInt(dr[BaseCommentEntity.FieldWorked]);
            DeletionStateCode = BaseBusinessLogic.ConvertToInt(dr[BaseCommentEntity.FieldDeletionStateCode]);
            Enabled = BaseBusinessLogic.ConvertToInt(dr[BaseCommentEntity.FieldEnabled]);
            Description = BaseBusinessLogic.ConvertToString(dr[BaseCommentEntity.FieldDescription]);
            CreateOn = BaseBusinessLogic.ConvertToNullableDateTime(dr[BaseCommentEntity.FieldCreateOn]);
            CreateUserId = BaseBusinessLogic.ConvertToString(dr[BaseCommentEntity.FieldCreateUserId]);
            CreateBy = BaseBusinessLogic.ConvertToString(dr[BaseCommentEntity.FieldCreateBy]);
            ModifiedOn = BaseBusinessLogic.ConvertToNullableDateTime(dr[BaseCommentEntity.FieldModifiedOn]);
            ModifiedUserId = BaseBusinessLogic.ConvertToString(dr[BaseCommentEntity.FieldModifiedUserId]);
            ModifiedBy = BaseBusinessLogic.ConvertToString(dr[BaseCommentEntity.FieldModifiedBy]);
            // 获取扩展属性
            GetFromExpand(dr);
            return this;
        }

        ///<summary>
        /// 评论表
        ///</summary>
        [NonSerialized]
        public static string TableName = "BaseComment";

        ///<summary>
        /// 主键
        ///</summary>
        [NonSerialized]
        public static string FieldId = "Id";

        ///<summary>
        /// 部门代码
        ///</summary>
        [NonSerialized]
        public static string FieldDepartmentId = "DepartmentId";

        ///<summary>
        /// 部门名称
        ///</summary>
        [NonSerialized]
        public static string FieldDepartmentName = "DepartmentName";

        ///<summary>
        /// 父亲节点主键
        ///</summary>
        [NonSerialized]
        public static string FieldParentId = "ParentId";

        ///<summary>
        /// 分类编号
        ///</summary>
        [NonSerialized]
        public static string FieldCategoryCode = "CategoryCode";

        ///<summary>
        /// 唯一识别主键
        ///</summary>
        [NonSerialized]
        public static string FieldObjectId = "ObjectId";

        ///<summary>
        /// 消息的指向网页页面
        ///</summary>
        [NonSerialized]
        public static string FieldTargetURL = "TargetURL";

        ///<summary>
        /// 主题
        ///</summary>
        [NonSerialized]
        public static string FieldTitle = "Title";

        ///<summary>
        /// 内容
        ///</summary>
        [NonSerialized]
        public static string FieldContents = "Contents";

        ///<summary>
        /// IP地址
        ///</summary>
        [NonSerialized]
        public static string FieldIPAddress = "IPAddress";

        ///<summary>
        /// 已被处理标
        ///</summary>
        [NonSerialized]
        public static string FieldWorked = "Worked";

        ///<summary>
        /// 删除标志
        ///</summary>
        [NonSerialized]
        public static string FieldDeletionStateCode = "DeletionStateCode";

        ///<summary>
        /// 有效
        ///</summary>
        [NonSerialized]
        public static string FieldEnabled = "Enabled";

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
