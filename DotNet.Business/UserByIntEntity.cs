//-----------------------------------------------------------------------
// <copyright file="UserByIntEntity.cs" company="Hairihan">
//     Copyright (c) 2013 , All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace DotNet.Business
{
    using DotNet.Utilities;

    /// <summary>
    /// UserByIntEntity
    /// 用户表按数值主键测试
    /// 
    /// 修改纪录
    /// 
    /// 2013-10-02 版本：1.0 JiRiGaLa 创建主键。
    /// 
    /// <author>
    ///     <name>JiRiGaLa</name>
    ///     <date>2013-10-02</date>
    /// </author>
    /// </summary>
    public partial class UserByIntEntity : BaseEntity
    {
        private int userId;
        /// <summary>
        /// 主键
        /// </summary>
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

        private String fullName = string.Empty;
        /// <summary>
        /// 姓名
        /// </summary>
        public String FullName
        {
            get
            {
                return fullName;
            }
            set
            {
                fullName = value;
            }
        }

        private Decimal? salary = 0;
        /// <summary>
        /// 薪资
        /// </summary>
        public Decimal? Salary
        {
            get
            {
                return salary;
            }
            set
            {
                salary = value;
            }
        }

        private int? age = 0;
        /// <summary>
        /// 年龄
        /// </summary>
        public int? Age
        {
            get
            {
                return age;
            }
            set
            {
                age = value;
            }
        }

        private DateTime? birthday = DateTime.Now;
        /// <summary>
        /// 生日
        /// </summary>
        public DateTime? Birthday
        {
            get
            {
                return birthday;
            }
            set
            {
                birthday = value;
            }
        }

        private Byte[] photo = null;
        /// <summary>
        /// 照片
        /// </summary>
        public Byte[] Photo
        {
            get
            {
                return photo;
            }
            set
            {
                photo = value;
            }
        }

        private int? allowEdit = 1;
        /// <summary>
        /// 允许编辑
        /// </summary>
        public int? AllowEdit
        {
            get
            {
                return allowEdit;
            }
            set
            {
                allowEdit = value;
            }
        }

        private int? allowDelete = 1;
        /// <summary>
        /// 允许删除
        /// </summary>
        public int? AllowDelete
        {
            get
            {
                return allowDelete;
            }
            set
            {
                allowDelete = value;
            }
        }

        private int enabled = 1;
        /// <summary>
        /// 有效
        /// </summary>
        public int Enabled
        {
            get
            {
                return enabled;
            }
            set
            {
                enabled = value;
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
                return deletionStateCode;
            }
            set
            {
                deletionStateCode = value;
            }
        }

        private int sortCode;
        /// <summary>
        /// 排序码
        /// </summary>
        public int SortCode
        {
            get
            {
                return sortCode;
            }
            set
            {
                sortCode = value;
            }
        }

        private String description = string.Empty;
        /// <summary>
        /// 备注
        /// </summary>
        public String Description
        {
            get
            {
                return description;
            }
            set
            {
                description = value;
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
                return createOn;
            }
            set
            {
                createOn = value;
            }
        }

        private String createUserId = string.Empty;
        /// <summary>
        /// 创建用户主键
        /// </summary>
        public String CreateUserId
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

        private String createBy = string.Empty;
        /// <summary>
        /// 创建人
        /// </summary>
        public String CreateBy
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

        private DateTime? modifiedOn = null;
        /// <summary>
        /// 修改日期
        /// </summary>
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

        private String modifiedUserId = string.Empty;
        /// <summary>
        /// 修改用户主键
        /// </summary>
        public String ModifiedUserId
        {
            get
            {
                return modifiedUserId;
            }
            set
            {
                modifiedUserId = value;
            }
        }

        private String modifiedBy = string.Empty;
        /// <summary>
        /// 修改人
        /// </summary>
        public String ModifiedBy
        {
            get
            {
                return modifiedBy;
            }
            set
            {
                modifiedBy = value;
            }
        }

        /// <summary>
        /// 从数据行读取
        /// </summary>
        /// <param name="dr">数据行</param>
        protected override BaseEntity GetFrom(IDataRow dr)
        {
            GetFromExpand(dr);
            UserId = BaseBusinessLogic.ConvertToInt(dr[UserByIntEntity.FieldUserId]);
            FullName = BaseBusinessLogic.ConvertToString(dr[UserByIntEntity.FieldFullName]);
            Salary = BaseBusinessLogic.ConvertToNullableDecimal(dr[UserByIntEntity.FieldSalary]);
            Age = BaseBusinessLogic.ConvertToNullableInt(dr[UserByIntEntity.FieldAge]);
            Birthday = BaseBusinessLogic.ConvertToNullableDateTime(dr[UserByIntEntity.FieldBirthday]);
            Photo = BaseBusinessLogic.ConvertToByte(dr[UserByIntEntity.FieldPhoto]);
            AllowEdit = BaseBusinessLogic.ConvertToNullableInt(dr[UserByIntEntity.FieldAllowEdit]);
            AllowDelete = BaseBusinessLogic.ConvertToNullableInt(dr[UserByIntEntity.FieldAllowDelete]);
            Enabled = BaseBusinessLogic.ConvertToInt(dr[UserByIntEntity.FieldEnabled]);
            DeletionStateCode = BaseBusinessLogic.ConvertToNullableInt(dr[UserByIntEntity.FieldDeletionStateCode]);
            SortCode = BaseBusinessLogic.ConvertToInt(dr[UserByIntEntity.FieldSortCode]);
            Description = BaseBusinessLogic.ConvertToString(dr[UserByIntEntity.FieldDescription]);
            CreateOn = BaseBusinessLogic.ConvertToNullableDateTime(dr[UserByIntEntity.FieldCreateOn]);
            CreateUserId = BaseBusinessLogic.ConvertToString(dr[UserByIntEntity.FieldCreateUserId]);
            CreateBy = BaseBusinessLogic.ConvertToString(dr[UserByIntEntity.FieldCreateBy]);
            ModifiedOn = BaseBusinessLogic.ConvertToNullableDateTime(dr[UserByIntEntity.FieldModifiedOn]);
            ModifiedUserId = BaseBusinessLogic.ConvertToString(dr[UserByIntEntity.FieldModifiedUserId]);
            ModifiedBy = BaseBusinessLogic.ConvertToString(dr[UserByIntEntity.FieldModifiedBy]);
            return this;
        }

        ///<summary>
        /// 用户表按数值主键测试
        ///</summary>
        public static string TableName = "UserByInt";

        ///<summary>
        /// 主键
        ///</summary>
        public static string FieldUserId = "UserId";

        ///<summary>
        /// 姓名
        ///</summary>
        public static string FieldFullName = "FullName";

        ///<summary>
        /// 薪资
        ///</summary>
        public static string FieldSalary = "Salary";

        ///<summary>
        /// 年龄
        ///</summary>
        public static string FieldAge = "Age";

        ///<summary>
        /// 生日
        ///</summary>
        public static string FieldBirthday = "Birthday";

        ///<summary>
        /// 照片
        ///</summary>
        public static string FieldPhoto = "Photo";

        ///<summary>
        /// 允许编辑
        ///</summary>
        public static string FieldAllowEdit = "AllowEdit";

        ///<summary>
        /// 允许删除
        ///</summary>
        public static string FieldAllowDelete = "AllowDelete";

        ///<summary>
        /// 有效
        ///</summary>
        public static string FieldEnabled = "Enabled";

        ///<summary>
        /// 删除标志
        ///</summary>
        public static string FieldDeletionStateCode = "DeletionStateCode";

        ///<summary>
        /// 排序码
        ///</summary>
        public static string FieldSortCode = "SortCode";

        ///<summary>
        /// 备注
        ///</summary>
        public static string FieldDescription = "Description";

        ///<summary>
        /// 创建日期
        ///</summary>
        public static string FieldCreateOn = "CreateOn";

        ///<summary>
        /// 创建用户主键
        ///</summary>
        public static string FieldCreateUserId = "CreateUserId";

        ///<summary>
        /// 创建人
        ///</summary>
        public static string FieldCreateBy = "CreateBy";

        ///<summary>
        /// 修改日期
        ///</summary>
        public static string FieldModifiedOn = "ModifiedOn";

        ///<summary>
        /// 修改用户主键
        ///</summary>
        public static string FieldModifiedUserId = "ModifiedUserId";

        ///<summary>
        /// 修改人
        ///</summary>
        public static string FieldModifiedBy = "ModifiedBy";
    }
}
