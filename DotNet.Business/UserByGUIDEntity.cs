//-----------------------------------------------------------------------
// <copyright file="UserByGUIDEntity.cs" company="Hairihan">
//     Copyright (c) 2013 , All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace DotNet.Business
{
    using DotNet.Utilities;

    /// <summary>
    /// UserByGUIDEntity
    /// 
    /// 
    /// 修改纪录
    /// 
    /// 2013-07-14 版本：1.0 JiRiGaLa 创建主键。
    /// 
    /// <author>
    ///     <name>JiRiGaLa</name>
    ///     <date>2013-07-14</date>
    /// </author>
    /// </summary>
    public partial class UserByGUIDEntity : BaseEntity
    {
        private String userId = "newid()";
        /// <summary>
        /// 
        /// </summary>
        [StringLength(50, ErrorMessage = "不能超过50个字符")] 
        public String UserId
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
        /// 
        /// </summary>
        [StringLength(50, ErrorMessage = "不能超过50个字符")] 
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
        /// 
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
        /// 
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
        /// 
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
        /// 
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
        /// 
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
        /// 
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
        /// 
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
        /// 
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
        /// 
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
        /// 
        /// </summary>
        [StringLength(800, ErrorMessage = "不能超过800个字符")] 
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
        /// 
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
        /// 
        /// </summary>
        [StringLength(50, ErrorMessage = "不能超过50个字符")] 
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
        /// 
        /// </summary>
        [StringLength(50, ErrorMessage = "不能超过50个字符")] 
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
        /// 
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
        /// 
        /// </summary>
        [StringLength(50, ErrorMessage = "不能超过50个字符")] 
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
        /// 
        /// </summary>
        [StringLength(50, ErrorMessage = "不能超过50个字符")] 
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
        /// 创建新实例
        /// </summary>
        /// <returns>新实例</returns>
        protected override BaseEntity CreateNew()
        {
            return new UserByGUIDEntity();
        }


        /// <summary>
        /// 从数据行读取
        /// </summary>
        /// <param name="dataRow">数据行</param>
        protected override BaseEntity GetFrom(IDataRow dr)
        {
            GetFromExpand(dr);
            UserId = BaseBusinessLogic.ConvertToString(dr[UserByGUIDEntity.FieldUserId]);
            FullName = BaseBusinessLogic.ConvertToString(dr[UserByGUIDEntity.FieldFullName]);
            Salary = BaseBusinessLogic.ConvertToNullableDecimal(dr[UserByGUIDEntity.FieldSalary]);
            Age = BaseBusinessLogic.ConvertToNullableInt(dr[UserByGUIDEntity.FieldAge]);
            Birthday = BaseBusinessLogic.ConvertToNullableDateTime(dr[UserByGUIDEntity.FieldBirthday]);
            Photo = BaseBusinessLogic.ConvertToByte(dr[UserByGUIDEntity.FieldPhoto]);
            AllowEdit = BaseBusinessLogic.ConvertToNullableInt(dr[UserByGUIDEntity.FieldAllowEdit]);
            AllowDelete = BaseBusinessLogic.ConvertToNullableInt(dr[UserByGUIDEntity.FieldAllowDelete]);
            Enabled = BaseBusinessLogic.ConvertToInt(dr[UserByGUIDEntity.FieldEnabled]);
            DeletionStateCode = BaseBusinessLogic.ConvertToNullableInt(dr[UserByGUIDEntity.FieldDeletionStateCode]);
            SortCode = BaseBusinessLogic.ConvertToInt(dr[UserByGUIDEntity.FieldSortCode]);
            Description = BaseBusinessLogic.ConvertToString(dr[UserByGUIDEntity.FieldDescription]);
            CreateOn = BaseBusinessLogic.ConvertToNullableDateTime(dr[UserByGUIDEntity.FieldCreateOn]);
            CreateUserId = BaseBusinessLogic.ConvertToString(dr[UserByGUIDEntity.FieldCreateUserId]);
            CreateBy = BaseBusinessLogic.ConvertToString(dr[UserByGUIDEntity.FieldCreateBy]);
            ModifiedOn = BaseBusinessLogic.ConvertToNullableDateTime(dr[UserByGUIDEntity.FieldModifiedOn]);
            ModifiedUserId = BaseBusinessLogic.ConvertToString(dr[UserByGUIDEntity.FieldModifiedUserId]);
            ModifiedBy = BaseBusinessLogic.ConvertToString(dr[UserByGUIDEntity.FieldModifiedBy]);
            return this;
        }

        ///<summary>
        /// 
        ///</summary>
        public static string TableName = "UserByGUID";

        ///<summary>
        /// 
        ///</summary>
        public static string FieldUserId = "UserId";

        ///<summary>
        /// 
        ///</summary>
        public static string FieldFullName = "FullName";

        ///<summary>
        /// 
        ///</summary>
        public static string FieldSalary = "Salary";

        ///<summary>
        /// 
        ///</summary>
        public static string FieldAge = "Age";

        ///<summary>
        /// 
        ///</summary>
        public static string FieldBirthday = "Birthday";

        ///<summary>
        /// 
        ///</summary>
        public static string FieldPhoto = "Photo";

        ///<summary>
        /// 
        ///</summary>
        public static string FieldAllowEdit = "AllowEdit";

        ///<summary>
        /// 
        ///</summary>
        public static string FieldAllowDelete = "AllowDelete";

        ///<summary>
        /// 
        ///</summary>
        public static string FieldEnabled = "Enabled";

        ///<summary>
        /// 
        ///</summary>
        public static string FieldDeletionStateCode = "DeletionStateCode";

        ///<summary>
        /// 
        ///</summary>
        public static string FieldSortCode = "SortCode";

        ///<summary>
        /// 
        ///</summary>
        public static string FieldDescription = "Description";

        ///<summary>
        /// 
        ///</summary>
        public static string FieldCreateOn = "CreateOn";

        ///<summary>
        /// 
        ///</summary>
        public static string FieldCreateUserId = "CreateUserId";

        ///<summary>
        /// 
        ///</summary>
        public static string FieldCreateBy = "CreateBy";

        ///<summary>
        /// 
        ///</summary>
        public static string FieldModifiedOn = "ModifiedOn";

        ///<summary>
        /// 
        ///</summary>
        public static string FieldModifiedUserId = "ModifiedUserId";

        ///<summary>
        /// 
        ///</summary>
        public static string FieldModifiedBy = "ModifiedBy";
    }
}
