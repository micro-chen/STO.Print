//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2015 , Hairihan TECH, Ltd. 
//-----------------------------------------------------------------

using System;

namespace DotNet.Model
{
    using DotNet.Utilities;

    /// <summary>
    /// ItemssystemEntity
    /// 
    /// 
    /// 修改纪录
    /// 
    /// 2015-01-23 版本：1.0 SongBiao 创建文件。
    /// 
    /// <author>
    ///     <name>SongBiao</name>
    ///     <date>2015-01-23</date>
    /// </author>
    /// </summary>
    public partial class ItemsSystemEntity : BaseEntity
    {
        private Decimal id;
        /// <summary>
        /// 主键
        /// </summary>
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

        private Decimal? parentId = null;
        /// <summary>
        /// PARENTID
        /// </summary>
        public Decimal? ParentId
        {
            get
            {
                return parentId;
            }
            set
            {
                parentId = value;
            }
        }

        private string description = string.Empty;
        /// <summary>
        /// DESCRIPTION
        /// </summary>
        public string Description
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

        private Decimal enabled;
        /// <summary>
        /// ENABLED
        /// </summary>
        public Decimal Enabled
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

        private string modifiedUserId = string.Empty;
        /// <summary>
        /// ModifiedUserId
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

        private Decimal? sortCode = null;
        /// <summary>
        /// SORTCODE
        /// </summary>
        public Decimal? SortCode
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

        private Decimal ispublic;
        /// <summary>
        /// ISPUBLIC
        /// </summary>
        public Decimal Ispublic
        {
            get
            {
                return ispublic;
            }
            set
            {
                ispublic = value;
            }
        }

        private string createBy = string.Empty;
        /// <summary>
        /// CREATEBY
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

        private DateTime? createOn = null;
        /// <summary>
        /// CREATEON
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

        private string itemValue = string.Empty;
        /// <summary>
        /// ItemValue
        /// </summary>
        public string ItemValue
        {
            get
            {
                return itemValue;
            }
            set
            {
                itemValue = value;
            }
        }

        private string createUserId = string.Empty;
        /// <summary>
        /// CREATEUSERID
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

        private string logonurl = string.Empty;
        /// <summary>
        /// 登录url
        /// </summary>
        public string Logonurl
        {
            get
            {
                return logonurl;
            }
            set
            {
                logonurl = value;
            }
        }

        private Decimal allowDelete;
        /// <summary>
        /// ALLOWDELETE
        /// </summary>
        public Decimal AllowDelete
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

        private DateTime? modifiedOn = null;
        /// <summary>
        /// MODIFIEDON
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

        private string itemName = string.Empty;
        /// <summary>
        /// itemName
        /// </summary>
        public string ItemName
        {
            get
            {
                return itemName;
            }
            set
            {
                itemName = value;
            }
        }

        private Decimal allowEdit;
        /// <summary>
        /// AllowEdit
        /// </summary>
        public Decimal AllowEdit
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

        private string modifiedBy = string.Empty;
        /// <summary>
        /// MODIFIEDBY
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

        private string itemCode = string.Empty;
        /// <summary>
        /// itemCode
        /// </summary>
        public string ItemCode
        {
            get
            {
                return itemCode;
            }
            set
            {
                itemCode = value;
            }
        }

        private Decimal? deletionStateCode = null;
        /// <summary>
        /// DELETIONSTATECODE
        /// </summary>
        public Decimal? DeletionStateCode
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

        /// <summary>
        /// 从数据行读取
        /// </summary>
        /// <param name="dr">数据行</param>
        protected override BaseEntity GetFrom(IDataRow dr)
        {
            GetFromExpand(dr);
            Id = BaseBusinessLogic.ConvertToDecimal(dr[ItemsSystemEntity.FieldId]);
            ParentId = BaseBusinessLogic.ConvertToNullableDecimal(dr[ItemsSystemEntity.FieldParentId]);
            Description = BaseBusinessLogic.ConvertToString(dr[ItemsSystemEntity.FieldDescription]);
            Enabled = BaseBusinessLogic.ConvertToDecimal(dr[ItemsSystemEntity.FieldEnabled]);
            ModifiedUserId = BaseBusinessLogic.ConvertToString(dr[ItemsSystemEntity.FieldModifiedUserId]);
            SortCode = BaseBusinessLogic.ConvertToNullableDecimal(dr[ItemsSystemEntity.FieldSortCode]);
            Ispublic = BaseBusinessLogic.ConvertToDecimal(dr[ItemsSystemEntity.FieldIspublic]);
            CreateBy = BaseBusinessLogic.ConvertToString(dr[ItemsSystemEntity.FieldCreateby]);
            CreateOn = BaseBusinessLogic.ConvertToNullableDateTime(dr[ItemsSystemEntity.FieldCreateon]);
            ItemValue = BaseBusinessLogic.ConvertToString(dr[ItemsSystemEntity.FieldItemValue]);
            CreateUserId = BaseBusinessLogic.ConvertToString(dr[ItemsSystemEntity.FieldCreateUserId]);
            Logonurl = BaseBusinessLogic.ConvertToString(dr[ItemsSystemEntity.FieldLogonUrl]);
            AllowDelete = BaseBusinessLogic.ConvertToDecimal(dr[ItemsSystemEntity.FieldAllowDelete]);
            ModifiedOn = BaseBusinessLogic.ConvertToNullableDateTime(dr[ItemsSystemEntity.FieldModifiedOn]);
            itemName = BaseBusinessLogic.ConvertToString(dr[ItemsSystemEntity.FieldItemName]);
            AllowEdit = BaseBusinessLogic.ConvertToDecimal(dr[ItemsSystemEntity.FieldAllowEdit]);
            ModifiedBy = BaseBusinessLogic.ConvertToString(dr[ItemsSystemEntity.FieldModifiedBy]);
            itemCode = BaseBusinessLogic.ConvertToString(dr[ItemsSystemEntity.FieldItemCode]);
            DeletionStateCode = BaseBusinessLogic.ConvertToNullableDecimal(dr[ItemsSystemEntity.FieldDeletionStateCode]);
            return this;
        }

        ///<summary>
        /// 
        ///</summary>
        public static string TableName = "ITEMSSYSTEM";

        ///<summary>
        /// 主键
        ///</summary>
        public static string FieldId = "ID";

        ///<summary>
        /// PARENTID
        ///</summary>
        public static string FieldParentId = "PARENTID";

        ///<summary>
        /// DESCRIPTION
        ///</summary>
        public static string FieldDescription = "DESCRIPTION";

        ///<summary>
        /// ENABLED
        ///</summary>
        public static string FieldEnabled = "ENABLED";

        ///<summary>
        /// ModifiedUserId
        ///</summary>
        public static string FieldModifiedUserId = "MODIFIEDUSERID";

        ///<summary>
        /// SORTCODE
        ///</summary>
        public static string FieldSortCode = "SORTCODE";

        ///<summary>
        /// ISPUBLIC
        ///</summary>
        public static string FieldIspublic = "ISPUBLIC";

        ///<summary>
        /// CREATEBY
        ///</summary>
        public static string FieldCreateby = "CREATEBY";

        ///<summary>
        /// CREATEON
        ///</summary>
        public static string FieldCreateon = "CREATEON";

        ///<summary>
        /// ITEMVALUE
        ///</summary>
        public static string FieldItemValue = "ITEMVALUE";

        ///<summary>
        /// CREATEUSERID
        ///</summary>
        public static string FieldCreateUserId = "CREATEUSERID";

        ///<summary>
        /// 登录url
        ///</summary>
        public static string FieldLogonUrl = "LOGONURL";

        ///<summary>
        /// ALLOWDELETE
        ///</summary>
        public static string FieldAllowDelete = "ALLOWDELETE";

        ///<summary>
        /// MODIFIEDON
        ///</summary>
        public static string FieldModifiedOn = "MODIFIEDON";

        ///<summary>
        /// itemName
        ///</summary>
        public static string FieldItemName = "ITEMNAME";

        ///<summary>
        /// ALLOWEDIT
        ///</summary>
        public static string FieldAllowEdit = "ALLOWEDIT";

        ///<summary>
        /// MODIFIEDBY
        ///</summary>
        public static string FieldModifiedBy = "MODIFIEDBY";

        ///<summary>
        /// itemCode
        ///</summary>
        public static string FieldItemCode = "ITEMCODE";

        ///<summary>
        /// DELETIONSTATECODE
        ///</summary>
        public static string FieldDeletionStateCode = "DELETIONSTATECODE";
    }
}