//-----------------------------------------------------------------------
// <copyright file="BaseUserIdentityAuditEntity.cs" company="ZTO , Ltd .">
//     Copyright (C) 2016 , All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Runtime.Serialization;

namespace DotNet.Model
{
    using DotNet.Utilities;

    /// <summary>
    /// BaseUserIdentityAuditEntity
    /// 用户身份需要人工审核的：目前有身份证的审核，淘宝每日请求次数要求加入到此表中，后期用户其它身份的人工审核也加入到这里，如银行卡等
    /// 
    /// 修改记录
    /// 
    /// 2015-02-10 版本：1.0 SongBiao 创建文件。
    /// 
    /// <author>
    ///     <name>SongBiao</name>
    ///     <date>2015-02-10</date>
    /// </author>
    /// </summary>
    [Serializable]
    public partial class BaseUserIdentityAuditEntity : BaseEntity
    {
        private DateTime? modifiedOn = null;
        /// <summary>
        /// 身份证照片修改时间,被驳回后的重新上传保存时间
        /// </summary>
        [FieldDescription("身份证照片修改时间", false)]
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

        private string idcardPhotoHand = string.Empty;
        /// <summary>
        /// 手持身份证上传后的地址
        /// </summary>
        [FieldDescription("持身份证上传后的地址", false)]
        [DataMember]
        public string IdcardPhotoHand
        {
            get
            {
                return idcardPhotoHand;
            }
            set
            {
                idcardPhotoHand = value;
            }
        }

        private string organizeFullname = string.Empty;
        /// <summary>
        /// 申请人所属网点名称
        /// </summary>
        [FieldDescription("申请人所属网点名称")]
        [DataMember]
        public string OrganizeFullname
        {
            get
            {
                return organizeFullname;
            }
            set
            {
                organizeFullname = value;
            }
        }

        private DateTime createOn;
        /// <summary>
        /// 身份证照片上传、申请创建时间
        /// </summary>
        [FieldDescription("身份证照片上传、申请创建时间", false)]
        [DataMember]
        public DateTime CreateOn
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

        private DateTime? auditDate = null;
        /// <summary>
        /// 身份证审核者审核时间
        /// </summary>
        [FieldDescription("身份证审核者审核时间", false)]
        [DataMember]
        public DateTime? AuditDate
        {
            get
            {
                return auditDate;
            }
            set
            {
                auditDate = value;
            }
        }

        private string auditStatus = string.Empty;
        /// <summary>
        /// 审核状态：未审核、已通过、已驳回
        /// </summary>
        [FieldDescription("审核状态")]
        [DataMember]
        public string AuditStatus
        {
            get
            {
                return auditStatus;
            }
            set
            {
                auditStatus = value;
            }
        }

        private Decimal? interfaceDayLimit = null;
        /// <summary>
        /// 接口每日调用次数
        /// </summary>
        [FieldDescription("接口每日调用次数")]
        [DataMember]
        public Decimal? InterfaceDayLimit
        {
            get
            {
                return interfaceDayLimit;
            }
            set
            {
                interfaceDayLimit = value;
            }
        }

        private string nickName = string.Empty;
        /// <summary>
        /// 用户唯一名
        /// </summary>
        [FieldDescription("用户唯一名")]
        [DataMember]
        public string NickName
        {
            get
            {
                return nickName;
            }
            set
            {
                nickName = value;
            }
        }

        private string userRealName = string.Empty;
        /// <summary>
        /// 用户真实姓名
        /// </summary>
        [FieldDescription("用户真实姓名")]
        [DataMember]
        public string UserRealName
        {
            get
            {
                return userRealName;
            }
            set
            {
                userRealName = value;
            }
        }

        private string idcard = string.Empty;
        /// <summary>
        /// 身份证号码
        /// </summary>
        [FieldDescription("身份证号码")]
        [DataMember]
        public string Idcard
        {
            get
            {
                return idcard;
            }
            set
            {
                idcard = value;
            }
        }

        private Decimal? auditUserId = null;
        /// <summary>
        /// 身份证审核者的ID，关联用户表BASEUSER主键
        /// </summary>
        [FieldDescription("身份证审核者的ID")]
        [DataMember]
        public Decimal? AuditUserId
        {
            get
            {
                return auditUserId;
            }
            set
            {
                auditUserId = value;
            }
        }

        private Decimal id;
        /// <summary>
        /// 关联用户表BASEUSER主键
        /// </summary>
        [FieldDescription("关联用户表主键")]
        [DataMember]
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

        private string auditIdea = string.Empty;
        /// <summary>
        /// 身份证审核意见
        /// </summary>
        [FieldDescription("身份证审核意见")]
        [DataMember]
        public string AuditIdea
        {
            get
            {
                return auditIdea;
            }
            set
            {
                auditIdea = value;
            }
        }

        private string auditUserRealName = string.Empty;
        /// <summary>
        /// 审核者真实姓名
        /// </summary>
        [FieldDescription("审核者真实姓名")]
        [DataMember]
        public string AuditUserRealName
        {
            get
            {
                return auditUserRealName;
            }
            set
            {
                auditUserRealName = value;
            }
        }

        private Decimal organizeId;
        /// <summary>
        /// 申请人所属网点ID
        /// </summary>
        [FieldDescription("申请人所属网点ID")]
        [DataMember]
        public Decimal OrganizeId
        {
            get
            {
                return organizeId;
            }
            set
            {
                organizeId = value;
            }
        }

        private string auditUserNickName = string.Empty;
        /// <summary>
        /// 审核者唯一用户名
        /// </summary>
        [FieldDescription("审核者唯一用户名")]
        [DataMember]
        public string AuditUserNickName
        {
            get
            {
                return auditUserNickName;
            }
            set
            {
                auditUserNickName = value;
            }
        }

        private string idcardPhotoFront = string.Empty;
        /// <summary>
        /// 身份证正面图片上传后的地址
        /// </summary>
        [FieldDescription("身份证正面图片上传后的地址")]
        [DataMember]
        public string IdcardPhotoFront
        {
            get
            {
                return idcardPhotoFront;
            }
            set
            {
                idcardPhotoFront = value;
            }
        }

        /// <summary>
        /// 从数据行读取
        /// </summary>
        /// <param name="dr">数据行</param>
        protected override BaseEntity GetFrom(IDataRow dr)
        {
            GetFromExpand(dr);
            ModifiedOn = BaseBusinessLogic.ConvertToNullableDateTime(dr[BaseUserIdentityAuditEntity.FieldModifiedOn]);
            IdcardPhotoHand = BaseBusinessLogic.ConvertToString(dr[BaseUserIdentityAuditEntity.FieldIdcardPhotoHand]);
            OrganizeFullname = BaseBusinessLogic.ConvertToString(dr[BaseUserIdentityAuditEntity.FieldOrganizeFullname]);
            CreateOn = BaseBusinessLogic.ConvertToDateTime(dr[BaseUserIdentityAuditEntity.FieldCreateOn]);
            AuditDate = BaseBusinessLogic.ConvertToNullableDateTime(dr[BaseUserIdentityAuditEntity.FieldAuditDate]);
            AuditStatus = BaseBusinessLogic.ConvertToString(dr[BaseUserIdentityAuditEntity.FieldAuditStatus]);
            InterfaceDayLimit = BaseBusinessLogic.ConvertToNullableDecimal(dr[BaseUserIdentityAuditEntity.FieldInterfaceDayLimit]);
            NickName = BaseBusinessLogic.ConvertToString(dr[BaseUserIdentityAuditEntity.FieldNickName]);
            UserRealName = BaseBusinessLogic.ConvertToString(dr[BaseUserIdentityAuditEntity.FieldUserRealName]);
            Idcard = BaseBusinessLogic.ConvertToString(dr[BaseUserIdentityAuditEntity.FieldIdcard]);
            AuditUserId = BaseBusinessLogic.ConvertToNullableDecimal(dr[BaseUserIdentityAuditEntity.FieldAuditUserId]);
            Id = BaseBusinessLogic.ConvertToDecimal(dr[BaseUserIdentityAuditEntity.FieldId]);
            AuditIdea = BaseBusinessLogic.ConvertToString(dr[BaseUserIdentityAuditEntity.FieldAuditIdea]);
            AuditUserRealName = BaseBusinessLogic.ConvertToString(dr[BaseUserIdentityAuditEntity.FieldAuditUserRealName]);
            OrganizeId = BaseBusinessLogic.ConvertToDecimal(dr[BaseUserIdentityAuditEntity.FieldOrganizeId]);
            AuditUserNickName = BaseBusinessLogic.ConvertToString(dr[BaseUserIdentityAuditEntity.FieldAuditUserNickName]);
            IdcardPhotoFront = BaseBusinessLogic.ConvertToString(dr[BaseUserIdentityAuditEntity.FieldIdcardPhotoFront]);
            return this;
        }

        ///<summary>
        /// 用户身份需要人工审核的：目前有身份证的审核，淘宝每日请求次数要求加入到此表中，后期用户其它身份的人工审核也加入到这里，如银行卡等
        ///</summary>
        [NonSerialized]
        [FieldDescription("用户身份审核")]
        public static string TableName = "BASE_USER_IDENTITY_AUDIT";

        ///<summary>
        /// 身份证照片修改时间,被驳回后的重新上传保存时间
        ///</summary>
        [NonSerialized]
        public static string FieldModifiedOn = "MODIFIED_ON";

        ///<summary>
        /// 手持身份证上传后的地址
        ///</summary>
        [NonSerialized]
        public static string FieldIdcardPhotoHand = "IDCARD_PHOTO_HAND";

        ///<summary>
        /// 申请人所属网点名称
        ///</summary>
        [NonSerialized]
        public static string FieldOrganizeFullname = "ORGANIZE_FULLNAME";

        ///<summary>
        /// 身份证照片上传、申请创建时间
        ///</summary>
        [NonSerialized]
        public static string FieldCreateOn = "CREATE_ON";

        ///<summary>
        /// 身份证审核者审核时间
        ///</summary>
        [NonSerialized]
        public static string FieldAuditDate = "AUDIT_DATE";

        ///<summary>
        /// 审核状态：未审核、已通过、已驳回
        ///</summary>
        [NonSerialized]
        public static string FieldAuditStatus = "AUDIT_STATUS";

        ///<summary>
        /// 接口每日调用次数
        ///</summary>
        [NonSerialized]
        public static string FieldInterfaceDayLimit = "INTERFACE_DAY_LIMIT";

        ///<summary>
        /// 用户唯一名
        ///</summary>
        [NonSerialized]
        public static string FieldNickName = "NICK_NAME";

        ///<summary>
        /// 用户真实姓名
        ///</summary>
        [NonSerialized]
        public static string FieldUserRealName = "USER_REAL_NAME";

        ///<summary>
        /// 身份证号码
        ///</summary>
        [NonSerialized]
        public static string FieldIdcard = "IDCARD";

        ///<summary>
        /// 身份证审核者的ID，关联用户表BASEUSER主键
        ///</summary>
        [NonSerialized]
        public static string FieldAuditUserId = "AUDIT_USER_ID";

        ///<summary>
        /// 关联用户表BASEUSER主键
        ///</summary>
        [NonSerialized]
        public static string FieldId = "Id";

        ///<summary>
        /// 身份证审核意见
        ///</summary>
        [NonSerialized]
        public static string FieldAuditIdea = "AUDIT_IDEA";

        ///<summary>
        /// 审核者真实姓名
        ///</summary>
        [NonSerialized]
        public static string FieldAuditUserRealName = "AUDIT_USER_REAL_NAME";

        ///<summary>
        /// 申请人所属网点ID
        ///</summary>
        [NonSerialized]
        public static string FieldOrganizeId = "ORGANIZE_ID";

        ///<summary>
        /// 审核者唯一用户名
        ///</summary>
        [NonSerialized]
        public static string FieldAuditUserNickName = "AUDIT_USER_NICK_NAME";

        ///<summary>
        /// 身份证正面图片上传后的地址
        ///</summary>
        [NonSerialized]
        public static string FieldIdcardPhotoFront = "IDCARD_PHOTO_FRONT";
    }
}