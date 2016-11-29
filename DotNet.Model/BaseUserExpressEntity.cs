//-----------------------------------------------------------------------
// <copyright file="BaseUserExpressEntity.cs" company="Hairihan">
//     Copyright (C) 2016 , All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Runtime.Serialization;

namespace DotNet.Business
{
    using DotNet.Utilities;
    using DotNet.Model;

    /// <summary>
    /// BaseUserExpressEntity
    /// 
    /// 
    /// 修改记录
    /// 
    /// 2014-11-08 版本：1.0 JiRiGaLa 创建文件。
    /// 
    /// <author>
    ///     <name>JiRiGaLa</name>
    ///     <date>2014-11-08</date>
    /// </author>
    /// </summary>
    public partial class BaseUserExpressEntity : BaseEntity
    {
        private Decimal iD;
        /// <summary>
        /// 主键
        /// </summary>
        [FieldDescription("主键", false)]
        [DataMember]
        public Decimal Id
        {
            get
            {
                return iD;
            }
            set
            {
                iD = value;
            }
        }

        private DateTime? modifiedOn = null;
        /// <summary>
        /// 修改日期
        /// </summary>
        [FieldDescription("修改日期", false)]
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

        private Decimal? tRANSFER_ADD_FEE = null;
        /// <summary>
        /// 中转附加费
        /// </summary>
        [FieldDescription("中转附加费")]
        [DataMember]
        public Decimal? TRANSFER_ADD_FEE
        {
            get
            {
                return tRANSFER_ADD_FEE;
            }
            set
            {
                tRANSFER_ADD_FEE = value;
            }
        }

        private Decimal? oWNER_ID = null;
        /// <summary>
        /// 所属承包区ID
        /// </summary>
        [FieldDescription("所属承包区ID")]
        [DataMember]
        public Decimal? OWNER_ID
        {
            get
            {
                return oWNER_ID;
            }
            set
            {
                oWNER_ID = value;
            }
        }



        private string oWNER_RANGE = string.Empty;
        /// <summary>
        /// 所属承包区
        /// </summary>
        [FieldDescription("所属承包区")]
        [DataMember]
        public string OWNER_RANGE
        {
            get
            {
                return oWNER_RANGE;
            }
            set
            {
                oWNER_RANGE = value;
            }
        }

        private string modifiedUserId = string.Empty;
        /// <summary>
        /// 修改用户主键
        /// </summary>
        [FieldDescription("修改用户主键", false)]
        [DataMember]
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

        private Decimal? dISPATCH_ADD_FEE = null;
        /// <summary>
        /// 派件附加费
        /// </summary>
        [FieldDescription("派件附加费")]
        [DataMember]
        public Decimal? DISPATCH_ADD_FEE
        {
            get
            {
                return dISPATCH_ADD_FEE;
            }
            set
            {
                dISPATCH_ADD_FEE = value;
            }
        }

        private string modifiedBy = string.Empty;
        /// <summary>
        /// 修改用户
        /// </summary>
        [FieldDescription("修改用户",false)]
        [DataMember]
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

        private string createBy = string.Empty;
        /// <summary>
        /// 创建用户
        /// </summary>
        [FieldDescription("创建用户", false)]
        [DataMember]
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
        /// 创建日期
        /// </summary>
        [FieldDescription("创建日期", false)]
        [DataMember]
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
        [FieldDescription("创建用户主键", false)]
        [DataMember]
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

        /// <summary>
        /// 从数据行读取
        /// </summary>
        /// <param name="dr">数据行</param>
        protected override BaseEntity GetFrom(IDataRow dr)
        {
            Id = BaseBusinessLogic.ConvertToDecimal(dr[BaseUserExpressEntity.FieldID]);
            ModifiedOn = BaseBusinessLogic.ConvertToNullableDateTime(dr[BaseUserExpressEntity.FieldModifiedOn]);
            TRANSFER_ADD_FEE = BaseBusinessLogic.ConvertToNullableDecimal(dr[BaseUserExpressEntity.FieldTRANSFER_ADD_FEE]);
            OWNER_ID = BaseBusinessLogic.ConvertToDecimal(dr[BaseUserExpressEntity.FieldOWNER_ID]);
            OWNER_RANGE = BaseBusinessLogic.ConvertToString(dr[BaseUserExpressEntity.FieldOWNER_RANGE]);
            ModifiedUserId = BaseBusinessLogic.ConvertToString(dr[BaseUserExpressEntity.FieldModifiedUserId]);
            DISPATCH_ADD_FEE = BaseBusinessLogic.ConvertToNullableDecimal(dr[BaseUserExpressEntity.FieldDISPATCH_ADD_FEE]);
            ModifiedBy = BaseBusinessLogic.ConvertToString(dr[BaseUserExpressEntity.FieldModifiedBy]);
            CreateBy = BaseBusinessLogic.ConvertToString(dr[BaseUserExpressEntity.FieldCreateBy]);
            CreateOn = BaseBusinessLogic.ConvertToNullableDateTime(dr[BaseUserExpressEntity.FieldCreateOn]);
            CreateUserId = BaseBusinessLogic.ConvertToString(dr[BaseUserExpressEntity.FieldCreateUserId]);
            // 获取扩展属性
            GetFromExpand(dr);
            return this;
        }

        ///<summary>
        /// 用户扩展表
        ///</summary>
        [FieldDescription("用户扩展表")]
        public static string TableName = "BaseUser_EXPRESS";

        ///<summary>
        /// 主键
        ///</summary>
        public static string FieldID = "ID";

        ///<summary>
        /// 修改日期
        ///</summary>
        public static string FieldModifiedOn = "ModifiedOn";

        ///<summary>
        /// 中转附加费
        ///</summary>
        public static string FieldTRANSFER_ADD_FEE = "TRANSFER_ADD_FEE";

        ///<summary>
        /// 所属承包区ID
        ///</summary>
        public static string FieldOWNER_ID = "OWNER_ID";

        ///<summary>
        /// 所属承包区
        ///</summary>
        public static string FieldOWNER_RANGE = "OWNER_RANGE";

        ///<summary>
        /// 修改用户主键
        ///</summary>
        public static string FieldModifiedUserId = "ModifiedUserId";

        ///<summary>
        /// 派件附加费
        ///</summary>
        public static string FieldDISPATCH_ADD_FEE = "DISPATCH_ADD_FEE";

        ///<summary>
        /// 修改用户
        ///</summary>
        public static string FieldModifiedBy = "ModifiedBy";

        ///<summary>
        /// 创建用户
        ///</summary>
        public static string FieldCreateBy = "CreateBy";

        ///<summary>
        /// 创建日期
        ///</summary>
        public static string FieldCreateOn = "CreateOn";

        ///<summary>
        /// 创建用户主键
        ///</summary>
        public static string FieldCreateUserId = "CreateUserId";
    }
}
