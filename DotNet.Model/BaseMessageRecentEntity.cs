//-----------------------------------------------------------------------
// <copyright file="BaseMessageRecentEntity.cs" company="Hairihan">
//     Copyright (C) 2016 , All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Runtime.Serialization;

namespace DotNet.Model
{
    using DotNet.Utilities;

    /// <summary>
    /// BaseMessageRecentEntity
    /// 最近联系人
    /// 
    /// 修改记录
    /// 
    /// 2014-04-12 版本：1.0 JiRiGaLa 创建文件。
    /// 
    /// <author>
    ///     <name>JiRiGaLa</name>
    ///     <date>2014-04-12</date>
    /// </author>
    /// </summary>
    [Serializable, DataContract]
    public partial class BaseMessageRecentEntity : BaseEntity
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

        private int userId;
        /// <summary>
        /// 用户主键
        /// </summary>
        [DataMember]
        public int UserId
        {
            get
            {
                return userId;
            }
            set
            {
                userId = value;
            }
        }

        private string targetCategory = string.Empty;
        /// <summary>
        /// 什么类型资源
        /// </summary>
        [DataMember]
        public string TargetCategory
        {
            get
            {
                return targetCategory;
            }
            set
            {
                targetCategory = value;
            }
        }

        private string targetId = string.Empty;
        /// <summary>
        /// 资源主键
        /// </summary>
        [DataMember]
        public string TargetId
        {
            get
            {
                return targetId;
            }
            set
            {
                targetId = value;
            }
        }

        private string realName = string.Empty;
        /// <summary>
        /// 资源名称
        /// </summary>
        [DataMember]
        public string RealName
        {
            get
            {
                return realName;
            }
            set
            {
                realName = value;
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
                return this.companyName;
            }
            set
            {
                this.companyName = value;
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
                return this.departmentName;
            }
            set
            {
                this.departmentName = value;
            }
        }

        private DateTime? modifiedOn = DateTime.Now;
        /// <summary>
        /// 修改日期
        /// </summary>
        [DataMember]
        public DateTime? ModifiedOn
        {
            get
            {
                return modifiedOn;
            }
            set
            {
                modifiedOn = value;
            }
        }

        /// <summary>
        /// 从数据行读取
        /// </summary>
        /// <param name="dr">数据行</param>
        protected override BaseEntity GetFrom(IDataRow dr)
        {
            Id = BaseBusinessLogic.ConvertToInt(dr[BaseMessageRecentEntity.FieldId]);
            UserId = BaseBusinessLogic.ConvertToInt(dr[BaseMessageRecentEntity.FieldUserId]);
            TargetCategory = BaseBusinessLogic.ConvertToString(dr[BaseMessageRecentEntity.FieldTargetCategory]);
            TargetId = BaseBusinessLogic.ConvertToString(dr[BaseMessageRecentEntity.FieldTargetId]);
            RealName = BaseBusinessLogic.ConvertToString(dr[BaseMessageRecentEntity.FieldRealName]);
            CompanyName = BaseBusinessLogic.ConvertToString(dr[BaseMessageRecentEntity.FieldCompanyName]);
            DepartmentName = BaseBusinessLogic.ConvertToString(dr[BaseMessageRecentEntity.FieldDepartmentName]);
            ModifiedOn = BaseBusinessLogic.ConvertToNullableDateTime(dr[BaseMessageRecentEntity.FieldModifiedOn]);
            // 获取扩展属性
            GetFromExpand(dr);
            return this;
        }

        ///<summary>
        /// 最近联系人
        ///</summary>
        [NonSerialized]
        public static string TableName = "BaseMessageRecent";

        ///<summary>
        /// 主键
        ///</summary>
        [NonSerialized]
        public static string FieldId = "Id";

        ///<summary>
        /// 用户主键
        ///</summary>
        [NonSerialized]
        public static string FieldUserId = "UserId";

        ///<summary>
        /// 什么类型资源
        ///</summary>
        [NonSerialized]
        public static string FieldTargetCategory = "TargetCategory";

        ///<summary>
        /// 资源主键
        ///</summary>
        [NonSerialized]
        public static string FieldTargetId = "TargetId";

        ///<summary>
        /// 资源名称
        ///</summary>
        [NonSerialized]
        public static string FieldRealName = "RealName";

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
        /// 修改日期
        ///</summary>
        [NonSerialized]
        public static string FieldModifiedOn = "ModifiedOn";
    }
}
