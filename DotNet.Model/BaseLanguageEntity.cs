//-----------------------------------------------------------------------
// <copyright file="BaseLanguageEntity.cs" company="Hairihan">
//     Copyright (C) 2016 , All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace DotNet.Model
{
    using DotNet.Utilities;

    /// <summary>
    /// BaseLanguageEntity
    /// 多语言
    /// 
    /// 修改记录
    /// 
    /// 2015-02-25 版本：1.0 JiRiGaLa 创建文件。
    /// 
    /// <author>
    ///     <name>JiRiGaLa</name>
    ///     <date>2015-02-25</date>
    /// </author>
    /// </summary>
    public partial class BaseLanguageEntity : BaseEntity
    {
        private int id;
        /// <summary>
        /// 主键
        /// </summary>
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

        private string languageCode = string.Empty;
        /// <summary>
        /// 语言编号
        /// </summary>
        public string LanguageCode
        {
            get
            {
                return languageCode;
            }
            set
            {
                languageCode = value;
            }
        }

        private string messageCode = string.Empty;
        /// <summary>
        /// 消息编号
        /// </summary>
        public string MessageCode
        {
            get
            {
                return messageCode;
            }
            set
            {
                messageCode = value;
            }
        }

        private string caption = string.Empty;
        /// <summary>
        /// 解说词
        /// </summary>
        public string Caption
        {
            get
            {
                return caption;
            }
            set
            {
                caption = value;
            }
        }

        private int deletionStateCode = 0;
        /// <summary>
        /// 删除标记
        /// </summary>
        public int DeletionStateCode
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

        private DateTime? createOn = DateTime.Now;
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

        private string createUserId = string.Empty;
        /// <summary>
        /// 创建用户主键
        /// </summary>
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

        private DateTime? modifiedOn = DateTime.Now;
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

        private string modifiedUserId = string.Empty;
        /// <summary>
        /// 修改用户主键
        /// </summary>
        public string ModifiedUserId
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

        private string modifiedBy = string.Empty;
        /// <summary>
        /// 修改用户
        /// </summary>
        public string ModifiedBy
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
            Id = BaseBusinessLogic.ConvertToInt(dr[BaseLanguageEntity.FieldId]);
            LanguageCode = BaseBusinessLogic.ConvertToString(dr[BaseLanguageEntity.FieldLanguageCode]);
            MessageCode = BaseBusinessLogic.ConvertToString(dr[BaseLanguageEntity.FieldMessageCode]);
            Caption = BaseBusinessLogic.ConvertToString(dr[BaseLanguageEntity.FieldCaption]);
            DeletionStateCode = BaseBusinessLogic.ConvertToInt(dr[BaseLanguageEntity.FieldDeletionStateCode]);
            CreateOn = BaseBusinessLogic.ConvertToNullableDateTime(dr[BaseLanguageEntity.FieldCreateOn]);
            CreateUserId = BaseBusinessLogic.ConvertToString(dr[BaseLanguageEntity.FieldCreateUserId]);
            CreateBy = BaseBusinessLogic.ConvertToString(dr[BaseLanguageEntity.FieldCreateBy]);
            ModifiedOn = BaseBusinessLogic.ConvertToNullableDateTime(dr[BaseLanguageEntity.FieldModifiedOn]);
            ModifiedUserId = BaseBusinessLogic.ConvertToString(dr[BaseLanguageEntity.FieldModifiedUserId]);
            ModifiedBy = BaseBusinessLogic.ConvertToString(dr[BaseLanguageEntity.FieldModifiedBy]);
            return this;
        }

        ///<summary>
        /// 多语言
        ///</summary>
        public static string TableName = "BaseLanguage";

        ///<summary>
        /// 主键
        ///</summary>
        public static string FieldId = "Id";

        ///<summary>
        /// 语言编号
        ///</summary>
        public static string FieldLanguageCode = "LanguageCode";

        ///<summary>
        /// 消息编号
        ///</summary>
        public static string FieldMessageCode = "MessageCode";

        ///<summary>
        /// 解说词
        ///</summary>
        public static string FieldCaption = "Caption";

        ///<summary>
        /// 删除标记
        ///</summary>
        public static string FieldDeletionStateCode = "DeletionStateCode";

        ///<summary>
        /// 创建日期
        ///</summary>
        public static string FieldCreateOn = "CreateOn";

        ///<summary>
        /// 创建用户主键
        ///</summary>
        public static string FieldCreateUserId = "CreateUserId";

        ///<summary>
        /// 创建用户
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
        /// 修改用户
        ///</summary>
        public static string FieldModifiedBy = "ModifiedBy";
    }
}
