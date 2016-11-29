//-----------------------------------------------------------------------
// <copyright file="BaseOrganize_ExpressEntity.cs" company="Hairihan">
//     Copyright (c) 2015 , All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations;

namespace DotNet.Model
{
    using DotNet.Model;
    using DotNet.Utilities;

    /// <summary>
    /// BaseOrganize_ExpressEntity
    /// 网点基础资料扩展表
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
    public partial class BaseOrganize_ExpressEntity : BaseEntity
    {
        private Decimal id;
        /// <summary>
        /// 组织机构ID
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

        private string not_Dispatch_Range = string.Empty;
        /// <summary>
        /// 不派送范围
        /// </summary>
        [Display(Name = "不派送范围")]
        [Required(ErrorMessage = "需要输入不派送范围")]
        [DataType(DataType.Text)]
        public string Not_Dispatch_Range
        {
            get
            {
                return not_Dispatch_Range;
            }
            set
            {
                not_Dispatch_Range = value;
            }
        }

        private Decimal? scan_Select = null;
        /// <summary>
        /// 标识此网点只在某些扫描(到件)时能选择
        /// </summary>
        [Display(Name = "到件扫描使用")]
        [Required(ErrorMessage = "需要选择到件扫描使用")]
        [DataType(DataType.Currency)]
        public Decimal? Scan_Select
        {
            get
            {
                return scan_Select;
            }
            set
            {
                scan_Select = value;
            }
        }

        private Decimal? site_Prior = null;
        /// <summary>
        /// 站点优先级(统计运单状态时使用，数值越大级别越高)
        /// </summary>
        [Display(Name = "站点优先级")]
        [Required(ErrorMessage = "需要选择站点优先级")]
        [DataType(DataType.Currency)]
        public Decimal? Site_Prior
        {
            get
            {
                return site_Prior;
            }
            set
            {
                site_Prior = value;
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

        private string private_Remark = string.Empty;
        /// <summary>
        /// 内部备注
        /// </summary>
        [Display(Name = "内部备注")]
        [Required(ErrorMessage = "需要输入内部备注")]
        [DataType(DataType.Text)]
        public string Private_Remark
        {
            get
            {
                return private_Remark;
            }
            set
            {
                private_Remark = value;
            }
        }

        private string public_Remark = string.Empty;
        /// <summary>
        /// 对外备注
        /// </summary>
        [Display(Name = "对外备注")]
        [Required(ErrorMessage = "需要输入对外备注")]
        [DataType(DataType.Text)]
        public string Public_Remark
        {
            get
            {
                return public_Remark;
            }
            set
            {
                public_Remark = value;
            }
        }

        private string dispatch_Time_Limit = string.Empty;
        /// <summary>
        /// 派送时效限制
        /// </summary>
        [Display(Name = "派送时效限制")]
        [Required(ErrorMessage = "需要输入派送时效限制")]
        [DataType(DataType.Text)]
        public string Dispatch_Time_Limit
        {
            get
            {
                return dispatch_Time_Limit;
            }
            set
            {
                dispatch_Time_Limit = value;
            }
        }

        private Decimal? agent_Money_Limited = null;
        /// <summary>
        /// 代收货款限制金额
        /// </summary>
        [Display(Name = "代收货款限制金额")]
        [Required(ErrorMessage = "需要输入代收货款限制金额")]
        [DataType(DataType.Currency)]
        public Decimal? Agent_Money_Limited
        {
            get
            {
                return agent_Money_Limited;
            }
            set
            {
                agent_Money_Limited = value;
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

        private string default_Send_Place = string.Empty;
        /// <summary>
        /// 默认发件地
        /// </summary>
        [Display(Name = "默认发件地")]
        [Required(ErrorMessage = "需要输入默认发件地")]
        [DataType(DataType.Text)]
        public string Default_Send_Place
        {
            get
            {
                return default_Send_Place;
            }
            set
            {
                default_Send_Place = value;
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

        private string dispatch_Range = string.Empty;
        /// <summary>
        /// 派送范围
        /// </summary>
        [Display(Name = "派送范围")]
        [Required(ErrorMessage = "需要输入派送范围")]
        [DataType(DataType.Text)]
        public string Dispatch_Range
        {
            get
            {
                return dispatch_Range;
            }
            set
            {
                dispatch_Range = value;
            }
        }

        private Decimal? allow_ToPayment = null;
        /// <summary>
        /// 允许到付
        /// </summary>
        [Display(Name = "允许到付")]
        [Required(ErrorMessage = "需要选择允许到付")]
        [DataType(DataType.Currency)]
        public Decimal? Allow_ToPayment
        {
            get
            {
                return allow_ToPayment;
            }
            set
            {
                allow_ToPayment = value;
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

        private Decimal? allow_Agent_Money = null;
        /// <summary>
        /// 允许代收货款
        /// </summary>
        [Display(Name = "允许代收货款")]
        [Required(ErrorMessage = "需要选择允许代收货款")]
        [DataType(DataType.Currency)]
        public Decimal? Allow_Agent_Money
        {
            get
            {
                return allow_Agent_Money;
            }
            set
            {
                allow_Agent_Money = value;
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

        private Decimal? dispatch_OutRange_Fee = null;
        /// <summary>
        /// 区域外派费
        /// </summary>
        [Display(Name = "区域外派费")]
        [Required(ErrorMessage = "需要输入区域外派费")]
        [DataType(DataType.Currency)]
        public Decimal? Dispatch_OutRange_Fee
        {
            get
            {
                return dispatch_OutRange_Fee;
            }
            set
            {
                dispatch_OutRange_Fee = value;
            }
        }

        private string currency = string.Empty;
        /// <summary>
        /// 本币币别
        /// </summary>
        [Display(Name = "本币币别")]
        [Required(ErrorMessage = "需要选择本币币别")]
        [DataType(DataType.Text)]
        public string Currency
        {
            get
            {
                return currency;
            }
            set
            {
                currency = value;
            }
        }

        private Decimal? dispatch_Range_Fee = null;
        /// <summary>
        /// 区域内派费
        /// </summary>
        [Display(Name = "区域内派费")]
        [Required(ErrorMessage = "需要输入区域内派费")]
        [DataType(DataType.Currency)]
        public Decimal? Dispatch_Range_Fee
        {
            get
            {
                return dispatch_Range_Fee;
            }
            set
            {
                dispatch_Range_Fee = value;
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

        private string dispatch_Money_Desc = string.Empty;
        /// <summary>
        /// 派送费用说明
        /// </summary>
        [Display(Name = "派送费用说明")]
        [Required(ErrorMessage = "需要输入派送费用说明")]
        [DataType(DataType.Text)]
        public string Dispatch_Money_Desc
        {
            get
            {
                return dispatch_Money_Desc;
            }
            set
            {
                dispatch_Money_Desc = value;
            }
        }

        private string webSiteName = string.Empty;
        /// <summary>
        /// 外网展示名称
        /// </summary>
        [Display(Name = "外网展示名称")]
        [Required(ErrorMessage = "需要输入外网展示名称")]
        [DataType(DataType.Text)]
        public string WebSiteName
        {
            get
            {
                return webSiteName;
            }
            set
            {
                webSiteName = value;
            }
        }

        /// <summary>
        /// 从数据行读取
        /// </summary>
        /// <param name="dr">数据行</param>
        protected override BaseEntity GetFrom(IDataRow dr)
        {
            GetFromExpand(dr);
            Id = BaseBusinessLogic.ConvertToDecimal(dr[BaseOrganize_ExpressEntity.FieldID]);
            Not_Dispatch_Range = BaseBusinessLogic.ConvertToString(dr[BaseOrganize_ExpressEntity.FieldNot_Dispatch_Range]);
            Scan_Select = BaseBusinessLogic.ConvertToNullableDecimal(dr[BaseOrganize_ExpressEntity.FieldScan_Select]);
            Site_Prior = BaseBusinessLogic.ConvertToNullableDecimal(dr[BaseOrganize_ExpressEntity.FieldSite_Prior]);
            ModifiedUserId = BaseBusinessLogic.ConvertToString(dr[BaseOrganize_ExpressEntity.FieldModifiedUserId]);
            Private_Remark = BaseBusinessLogic.ConvertToString(dr[BaseOrganize_ExpressEntity.FieldPrivate_Remark]);
            Public_Remark = BaseBusinessLogic.ConvertToString(dr[BaseOrganize_ExpressEntity.FieldPublic_Remark]);
            Dispatch_Time_Limit = BaseBusinessLogic.ConvertToString(dr[BaseOrganize_ExpressEntity.FieldDispatch_Time_Limit]);
            Agent_Money_Limited = BaseBusinessLogic.ConvertToNullableDecimal(dr[BaseOrganize_ExpressEntity.FieldAgent_Money_Limited]);
            CreateBy = BaseBusinessLogic.ConvertToString(dr[BaseOrganize_ExpressEntity.FieldCreateBy]);
            Default_Send_Place = BaseBusinessLogic.ConvertToString(dr[BaseOrganize_ExpressEntity.FieldDefault_Send_Place]);
            CreateOn = BaseBusinessLogic.ConvertToNullableDateTime(dr[BaseOrganize_ExpressEntity.FieldCreateOn]);
            Dispatch_Range = BaseBusinessLogic.ConvertToString(dr[BaseOrganize_ExpressEntity.FieldDispatch_Range]);
            Allow_ToPayment = BaseBusinessLogic.ConvertToNullableDecimal(dr[BaseOrganize_ExpressEntity.FieldAllow_ToPayment]);
            CreateUserId = BaseBusinessLogic.ConvertToString(dr[BaseOrganize_ExpressEntity.FieldCreateUserId]);
            Allow_Agent_Money = BaseBusinessLogic.ConvertToNullableDecimal(dr[BaseOrganize_ExpressEntity.FieldAllow_Agent_Money]);
            ModifiedOn = BaseBusinessLogic.ConvertToNullableDateTime(dr[BaseOrganize_ExpressEntity.FieldModifiedOn]);
            Dispatch_OutRange_Fee = BaseBusinessLogic.ConvertToNullableDecimal(dr[BaseOrganize_ExpressEntity.FieldDispatch_OutRange_Fee]);
            Currency = BaseBusinessLogic.ConvertToString(dr[BaseOrganize_ExpressEntity.FieldCurrency]);
            Dispatch_Range_Fee = BaseBusinessLogic.ConvertToNullableDecimal(dr[BaseOrganize_ExpressEntity.FieldDispatch_Range_Fee]);
            ModifiedBy = BaseBusinessLogic.ConvertToString(dr[BaseOrganize_ExpressEntity.FieldModifiedBy]);
            Dispatch_Money_Desc = BaseBusinessLogic.ConvertToString(dr[BaseOrganize_ExpressEntity.FieldDispatch_Money_Desc]);
            WebSiteName = BaseBusinessLogic.ConvertToString(dr[BaseOrganize_ExpressEntity.FieldWebSiteName]);
            return this;
        }

        ///<summary>
        /// 网点基础资料扩展表
        ///</summary>
        public static string TableName = "BaseOrganize_Express";

        ///<summary>
        /// 组织机构ID
        ///</summary>
        public static string FieldID = "ID";

        ///<summary>
        /// 不派送范围
        ///</summary>
        public static string FieldNot_Dispatch_Range = "Not_Dispatch_Range";

        ///<summary>
        /// 标识此网点只在某些扫描(到件)时能选择
        ///</summary>
        public static string FieldScan_Select = "Scan_Select";

        ///<summary>
        /// 站点优先级(统计运单状态时使用，数值越大级别越高)
        ///</summary>
        public static string FieldSite_Prior = "Site_Prior";

        ///<summary>
        /// 修改用户主键
        ///</summary>
        public static string FieldModifiedUserId = "ModifiedUserId";

        ///<summary>
        /// 内部备注
        ///</summary>
        public static string FieldPrivate_Remark = "Private_Remark";

        ///<summary>
        /// 对外备注
        ///</summary>
        public static string FieldPublic_Remark = "Public_Remark";

        ///<summary>
        /// 派送时效限制
        ///</summary>
        public static string FieldDispatch_Time_Limit = "Dispatch_Time_Limit";

        ///<summary>
        /// 代收货款限制金额
        ///</summary>
        public static string FieldAgent_Money_Limited = "Agent_Money_Limited";

        ///<summary>
        /// 创建用户
        ///</summary>
        public static string FieldCreateBy = "CreateBy";

        ///<summary>
        /// 默认发件地
        ///</summary>
        public static string FieldDefault_Send_Place = "Default_Send_Place";

        ///<summary>
        /// 创建日期
        ///</summary>
        public static string FieldCreateOn = "CreateOn";

        ///<summary>
        /// 派送范围
        ///</summary>
        public static string FieldDispatch_Range = "Dispatch_Range";

        ///<summary>
        /// 允许到付
        ///</summary>
        public static string FieldAllow_ToPayment = "Allow_ToPayment";

        ///<summary>
        /// 创建用户主键
        ///</summary>
        public static string FieldCreateUserId = "CreateUserId";

        ///<summary>
        /// 允许代收货款
        ///</summary>
        public static string FieldAllow_Agent_Money = "Allow_Agent_Money";

        ///<summary>
        /// 修改日期
        ///</summary>
        public static string FieldModifiedOn = "ModifiedOn";

        ///<summary>
        /// 区域外派费
        ///</summary>
        public static string FieldDispatch_OutRange_Fee = "Dispatch_OutRange_Fee";

        ///<summary>
        /// 本币币别
        ///</summary>
        public static string FieldCurrency = "Currency";

        ///<summary>
        /// 区域内派费
        ///</summary>
        public static string FieldDispatch_Range_Fee = "Dispatch_Range_Fee";

        ///<summary>
        /// 修改用户
        ///</summary>
        public static string FieldModifiedBy = "ModifiedBy";

        ///<summary>
        /// 派送费用说明
        ///</summary>
        public static string FieldDispatch_Money_Desc = "Dispatch_Money_Desc";

        ///<summary>
        /// 派送费用说明
        ///</summary>
        public static string FieldWebSiteName = "WebSiteName";
    }
}
