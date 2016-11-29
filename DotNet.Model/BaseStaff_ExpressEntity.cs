//-----------------------------------------------------------------------
// <copyright file="BaseStaff_ExpressEntity.cs" company="Hairihan">
//     Copyright (c) 2015 , All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace DotNet.Business
{
    using DotNet.Utilities;
    using DotNet.Model;

    /// <summary>
    /// BaseStaff_ExpressEntity
    /// 
    /// 
    /// 修改纪录
    /// 
    /// 2014-11-08 版本：1.0 JiRiGaLa 创建文件。
    /// 
    /// <author>
    ///     <name>JiRiGaLa</name>
    ///     <date>2014-11-08</date>
    /// </author>
    /// </summary>
    public partial class BaseStaff_ExpressEntity : BaseEntity
    {
        private Decimal iD;
        /// <summary>
        /// 主键
        /// </summary>
        public Decimal ID
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

        private string oWNER_RANGE = string.Empty;
        /// <summary>
        /// 所属承包区
        /// </summary>
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

        /// <summary>
        /// 从数据行读取
        /// </summary>
        /// <param name="dr">数据行</param>
        protected override BaseEntity GetFrom(IDataRow dr)
        {
            GetFromExpand(dr);
            ID = BaseBusinessLogic.ConvertToDecimal(dr[BaseStaff_ExpressEntity.FieldID]);
            ModifiedOn = BaseBusinessLogic.ConvertToNullableDateTime(dr[BaseStaff_ExpressEntity.FieldModifiedOn]);
            TRANSFER_ADD_FEE = BaseBusinessLogic.ConvertToNullableDecimal(dr[BaseStaff_ExpressEntity.FieldTRANSFER_ADD_FEE]);
            OWNER_RANGE = BaseBusinessLogic.ConvertToString(dr[BaseStaff_ExpressEntity.FieldOWNER_RANGE]);
            ModifiedUserId = BaseBusinessLogic.ConvertToString(dr[BaseStaff_ExpressEntity.FieldModifiedUserId]);
            DISPATCH_ADD_FEE = BaseBusinessLogic.ConvertToNullableDecimal(dr[BaseStaff_ExpressEntity.FieldDISPATCH_ADD_FEE]);
            ModifiedBy = BaseBusinessLogic.ConvertToString(dr[BaseStaff_ExpressEntity.FieldModifiedBy]);
            CreateBy = BaseBusinessLogic.ConvertToString(dr[BaseStaff_ExpressEntity.FieldCreateBy]);
            CreateOn = BaseBusinessLogic.ConvertToNullableDateTime(dr[BaseStaff_ExpressEntity.FieldCreateOn]);
            CreateUserId = BaseBusinessLogic.ConvertToString(dr[BaseStaff_ExpressEntity.FieldCreateUserId]);
            return this;
        }

        ///<summary>
        /// 
        ///</summary>
        public static string TableName = "BASESTAFF_EXPRESS";

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
