//-----------------------------------------------------------------------
// <copyright file="BaseTemplateEntity.cs" company="ZTO">
//     Copyright (c) 2015 , All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using DotNet.Model;
using DotNet.Utilities;

namespace ZTO.Print.Manager
{
    /// <summary>
    /// BaseTemplateEntity
    /// 
    /// 
    /// 修改纪录
    /// 
    /// 2015-08-23 版本：1.0 PanQiMin 创建文件。
    /// 
    /// <author>
    ///     <name>PanQiMin</name>
    ///     <date>2015-08-23</date>
    /// </author>
    /// </summary>
    public partial class BaseTemplateEntity : BaseEntity
    {
        private Decimal id;
        /// <summary>
        /// ID
        /// </summary>
        [FieldDescription("ID")]
        public Decimal Id
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

        private Decimal? expressId = null;
        /// <summary>
        /// EXPRESS_ID
        /// </summary>
        [FieldDescription("EXPRESS_ID")]
        public Decimal? ExpressId
        {
            get
            {
                return expressId;
            }
            set
            {
                expressId = value;
            }
        }

        private string createUserName = string.Empty;
        /// <summary>
        /// CREATEUSERNAME
        /// </summary>
        [FieldDescription("CREATEUSERNAME")]
        public string CreateUserName
        {
            get
            {
                return createUserName;
            }
            set
            {
                createUserName = value;
            }
        }

        private string modifiedUserName = string.Empty;
        /// <summary>
        /// MODIFIEDUSERNAME
        /// </summary>
        [FieldDescription("MODIFIEDUSERNAME")]
        public string ModifiedUserName
        {
            get
            {
                return modifiedUserName;
            }
            set
            {
                modifiedUserName = value;
            }
        }

        private DateTime? createOn = null;
        /// <summary>
        /// CREATEON
        /// </summary>
        [FieldDescription("CREATEON")]
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

        private string length = string.Empty;
        /// <summary>
        /// LENGTH
        /// </summary>
        [FieldDescription("LENGTH")]
        public string Length
        {
            get
            {
                return length;
            }
            set
            {
                length = value;
            }
        }

        private string remark = string.Empty;
        /// <summary>
        /// REMARK
        /// </summary>
        [FieldDescription("REMARK")]
        public string Remark
        {
            get
            {
                return remark;
            }
            set
            {
                remark = value;
            }
        }

        private string name = string.Empty;
        /// <summary>
        /// NAME
        /// </summary>
        [FieldDescription("NAME")]
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        private DateTime? modifiedOn = null;
        /// <summary>
        /// MODIFIEDON
        /// </summary>
        [FieldDescription("MODIFIEDON")]
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

        private string backgroundImagePath = string.Empty;
        /// <summary>
        /// BACKGROUND_IMAGE_PATH
        /// </summary>
        [FieldDescription("BACKGROUND_IMAGE_PATH")]
        public string BackgroundImagePath
        {
            get
            {
                return backgroundImagePath;
            }
            set
            {
                backgroundImagePath = value;
            }
        }

        private string width = string.Empty;
        /// <summary>
        /// WIDTH
        /// </summary>
        [FieldDescription("WIDTH")]
        public string Width
        {
            get
            {
                return width;
            }
            set
            {
                width = value;
            }
        }

        private string layer = string.Empty;
        /// <summary>
        /// LAYER
        /// </summary>
        [FieldDescription("LAYER")]
        public string Layer
        {
            get
            {
                return layer;
            }
            set
            {
                layer = value;
            }
        }

        private string filePath = string.Empty;
        /// <summary>
        /// FILE_PATH
        /// </summary>
        [FieldDescription("FILE_PATH")]
        public string FilePath
        {
            get
            {
                return filePath;
            }
            set
            {
                filePath = value;
            }
        }

        /// <summary>
        /// 从数据行读取
        /// </summary>
        /// <param name="dr">数据行</param>
        protected override BaseEntity GetFrom(IDataRow dr)
        {
            GetFromExpand(dr);
            Id = BaseBusinessLogic.ConvertToDecimal(dr[BaseTemplateEntity.FieldId]);
            ExpressId = BaseBusinessLogic.ConvertToNullableDecimal(dr[BaseTemplateEntity.FieldExpressId]);
            CreateUserName = BaseBusinessLogic.ConvertToString(dr[BaseTemplateEntity.FieldCreateUserName]);
            ModifiedUserName = BaseBusinessLogic.ConvertToString(dr[BaseTemplateEntity.FieldModifiedUserName]);
            CreateOn = BaseBusinessLogic.ConvertToNullableDateTime(dr[BaseTemplateEntity.FieldCreateOn]);
            Length = BaseBusinessLogic.ConvertToString(dr[BaseTemplateEntity.FieldLength]);
            Remark = BaseBusinessLogic.ConvertToString(dr[BaseTemplateEntity.FieldRemark]);
            Name = BaseBusinessLogic.ConvertToString(dr[BaseTemplateEntity.FieldName]);
            ModifiedOn = BaseBusinessLogic.ConvertToNullableDateTime(dr[BaseTemplateEntity.FieldModifiedOn]);
            BackgroundImagePath = BaseBusinessLogic.ConvertToString(dr[BaseTemplateEntity.FieldBackgroundImagePath]);
            Width = BaseBusinessLogic.ConvertToString(dr[BaseTemplateEntity.FieldWidth]);
            Layer = BaseBusinessLogic.ConvertToString(dr[BaseTemplateEntity.FieldLayer]);
            FilePath = BaseBusinessLogic.ConvertToString(dr[BaseTemplateEntity.FieldFilePath]);
            return this;
        }

        ///<summary>
        /// 
        ///</summary>
        public static string TableName = "BASE_TEMPLATE";

        ///<summary>
        /// ID
        ///</summary>
        public static string FieldId = "ID";

        ///<summary>
        /// EXPRESS_ID
        ///</summary>
        public static string FieldExpressId = "EXPRESS_ID";

        ///<summary>
        /// CREATEUSERNAME
        ///</summary>
        public static string FieldCreateUserName = "CREATEUSERNAME";

        ///<summary>
        /// MODIFIEDUSERNAME
        ///</summary>
        public static string FieldModifiedUserName = "MODIFIEDUSERNAME";

        ///<summary>
        /// CREATEON
        ///</summary>
        public static string FieldCreateOn = "CREATEON";

        ///<summary>
        /// LENGTH
        ///</summary>
        public static string FieldLength = "LENGTH";

        ///<summary>
        /// REMARK
        ///</summary>
        public static string FieldRemark = "REMARK";

        ///<summary>
        /// NAME
        ///</summary>
        public static string FieldName = "NAME";

        ///<summary>
        /// MODIFIEDON
        ///</summary>
        public static string FieldModifiedOn = "MODIFIEDON";

        ///<summary>
        /// BACKGROUND_IMAGE_PATH
        ///</summary>
        public static string FieldBackgroundImagePath = "BACKGROUND_IMAGE_PATH";

        ///<summary>
        /// WIDTH
        ///</summary>
        public static string FieldWidth = "WIDTH";

        ///<summary>
        /// LAYER
        ///</summary>
        public static string FieldLayer = "LAYER";

        ///<summary>
        /// FILE_PATH
        ///</summary>
        public static string FieldFilePath = "FILE_PATH";
    }
}
