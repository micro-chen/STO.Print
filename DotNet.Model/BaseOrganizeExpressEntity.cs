//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd. 
//-----------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations;

namespace DotNet.Model
{
    using DotNet.Utilities;

    /// <summary>
    /// BaseOrganize_ExpressEntity
    /// 网点基础资料扩展表
    /// 
    /// 修改记录
    /// 
    /// 2015-02-01 版本：1.2 潘齐民   添加对内派送范围字段。
    /// 2015-01-31 版本：1.1 潘齐民   添加外网启用字段。
    /// 2014-11-08 版本：1.0 JiRiGaLa 创建文件。
    /// 
    /// <author>
    ///     <name>JiRiGaLa</name>
    ///     <date>2014-11-08</date>
    /// </author>
    /// </summary>
    public partial class BaseOrganizeExpressEntity : BaseEntity
    {
        private Decimal id;
        /// <summary>
        /// 组织机构ID
        /// </summary>
        [FieldDescription("组织机构ID", false)]
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

        private string notDispatchRange = string.Empty;
        /// <summary>
        /// 不派送范围
        /// </summary>
        [FieldDescription("不派送范围")]
        public string NotDispatchRange
        {
            get
            {
                return notDispatchRange;
            }
            set
            {
                notDispatchRange = value;
            }
        }

        private Decimal? scanSelect = null;
        /// <summary>
        /// 标识此网点只在某些扫描(到件)时能选择
        /// </summary>
        [FieldDescription("到件扫描使用")]
        public Decimal? ScanSelect
        {
            get
            {
                return scanSelect;
            }
            set
            {
                scanSelect = value;
            }
        }

        private Decimal? sitePrior = null;
        /// <summary>
        /// 站点优先级(统计运单状态时使用，数值越大级别越高)
        /// </summary>
        [FieldDescription("站点优先级")]
        public Decimal? SitePrior
        {
            get
            {
                return sitePrior;
            }
            set
            {
                sitePrior = value;
            }
        }

        private string modifiedUserId = string.Empty;
        /// <summary>
        /// 修改用户主键
        /// </summary>
        [FieldDescription("修改用户主键", false)]
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

        private string privateRemark = string.Empty;
        /// <summary>
        /// 内部备注
        /// </summary>
        [FieldDescription("内部备注")]
        public string PrivateRemark
        {
            get
            {
                return privateRemark;
            }
            set
            {
                privateRemark = value;
            }
        }

        private string publicRemark = string.Empty;
        /// <summary>
        /// 对外备注
        /// </summary>
        [FieldDescription("对外备注")]
        public string PublicRemark
        {
            get
            {
                return publicRemark;
            }
            set
            {
                publicRemark = value;
            }
        }

        private string dispatchTimeLimit = string.Empty;
        /// <summary>
        /// 派送时效限制
        /// </summary>
        [FieldDescription("派送时效限制")]
        public string DispatchTimeLimit
        {
            get
            {
                return dispatchTimeLimit;
            }
            set
            {
                dispatchTimeLimit = value;
            }
        }

        private Decimal? agentMoneyLimited = null;
        /// <summary>
        /// 代收货款限制金额
        /// </summary>
        [FieldDescription("代收货款限制金额")]
        public Decimal? AgentMoneyLimited
        {
            get
            {
                return agentMoneyLimited;
            }
            set
            {
                agentMoneyLimited = value;
            }
        }

        private string createBy = string.Empty;
        /// <summary>
        /// 创建用户
        /// </summary>
        [FieldDescription("创建用户", false)]
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

        private string defaultSendPlace = string.Empty;
        /// <summary>
        /// 默认发件地
        /// </summary>
        [FieldDescription("默认发件地")]
        public string DefaultSendPlace
        {
            get
            {
                return defaultSendPlace;
            }
            set
            {
                defaultSendPlace = value;
            }
        }

        private DateTime? createOn = null;
        /// <summary>
        /// 创建日期
        /// </summary>
        [FieldDescription("创建日期", false)]
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

        private string dispatchRange = string.Empty;
        /// <summary>
        /// 派送范围
        /// </summary>
        [FieldDescription("派送范围")]
        public string DispatchRange
        {
            get
            {
                return dispatchRange;
            }
            set
            {
                dispatchRange = value;
            }
        }

        private Decimal? allowToPayment = null;
        /// <summary>
        /// 允许到付
        /// </summary>
        [FieldDescription("允许到付")]
        public Decimal? AllowToPayment
        {
            get
            {
                return allowToPayment;
            }
            set
            {
                allowToPayment = value;
            }
        }

        private string createUserId = string.Empty;
        /// <summary>
        /// 创建用户主键
        /// </summary>
        [FieldDescription("创建用户主键", false)]
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

        private Decimal? allowAgentMoney = null;
        /// <summary>
        /// 允许代收货款
        /// </summary>
        [FieldDescription("允许代收货款")]
        public Decimal? AllowAgentMoney
        {
            get
            {
                return allowAgentMoney;
            }
            set
            {
                allowAgentMoney = value;
            }
        }

        private DateTime? modifiedOn = null;
        /// <summary>
        /// 修改日期
        /// </summary>
        [FieldDescription("修改日期", false)]
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

        private Decimal? dispatchOutRangeFee = null;
        /// <summary>
        /// 区域外派费
        /// </summary>
        [FieldDescription("区域外派费")]
        public Decimal? DispatchOutRangeFee
        {
            get
            {
                return dispatchOutRangeFee;
            }
            set
            {
                dispatchOutRangeFee = value;
            }
        }

        private string currency = string.Empty;
        /// <summary>
        /// 本币币别
        /// </summary>
        [FieldDescription("本币币别")]
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

        private Decimal? dispatchRangeFee = null;
        /// <summary>
        /// 区域内派费
        /// </summary>
        [FieldDescription("区域内派费")]
        public Decimal? DispatchRangeFee
        {
            get
            {
                return dispatchRangeFee;
            }
            set
            {
                dispatchRangeFee = value;
            }
        }

        private string modifiedBy = string.Empty;
        /// <summary>
        /// 修改用户
        /// </summary>
        [FieldDescription("修改用户", false)]
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

        private string dispatchMoneyDesc = string.Empty;
        /// <summary>
        /// 派送费用说明
        /// </summary>
        [FieldDescription("派送费用说明")]
        public string DispatchMoneyDesc
        {
            get
            {
                return dispatchMoneyDesc;
            }
            set
            {
                dispatchMoneyDesc = value;
            }
        }

        private string webSiteName = string.Empty;
        /// <summary>
        /// 外网展示名称
        /// </summary>
        [FieldDescription("外网展示名称")]
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

        private Decimal? webEnabled = null;
        /// <summary>
        /// 外网启用
        /// </summary>
        [FieldDescription("外网启用")]
        public Decimal? WebEnabled
        {
            get
            {
                return webEnabled;
            }
            set
            {
                webEnabled = value;
            }
        }

        private string internalDispatch = string.Empty;
        /// <summary>
        /// 对内派送范围
        /// </summary>
        [FieldDescription("对内派送范围", false)]
        public string InternalDispatch
        {
            get
            {
                return internalDispatch;
            }
            set
            {
                internalDispatch = value;
            }
        }

        private Decimal? isTransferCenter;
        /// <summary>
        /// 转运中心标识
        /// </summary>
        [FieldDescription("转运中心标识")]
        public Decimal? IsTransferCenter
        {
            get
            {
                return isTransferCenter;
            }
            set
            {
                isTransferCenter = value;
            }
        }

        private int? isErpOpen = 0;
        /// <summary>
        /// 开通ERP(0未使用ERP，1已使用ERP)
        /// </summary>
        [FieldDescription("开通ERP")]
        public int? IsErpOpen
        {
            get
            {
                return this.isErpOpen;
            }
            set
            {
                this.isErpOpen = value;
            }
        }

        private int? isReceiveOrder = 1;
        /// <summary>
        /// 接收订单(0不接收，1接收)
        /// </summary>
        [FieldDescription("接收订单(0不接收，1接收)")]
        public int? IsReceiveOrder
        {
            get
            {
                return this.isReceiveOrder;
            }
            set
            {
                this.isReceiveOrder = value;
            }
        }

        private int? isReceiveComplain = 1;
        /// <summary>
        /// 接收投诉工单(0不接收，1接收)
        /// </summary>
        [FieldDescription("接收投诉工单(0不接收，1接收)")]
        public int? IsReceiveComplain
        {
            get
            {
                return this.isReceiveComplain;
            }
            set
            {
                this.isReceiveComplain = value;
            }
        }
        /// <summary>
        /// 从数据行读取
        /// </summary>
        /// <param name="dr">数据行</param>
        protected override BaseEntity GetFrom(IDataRow dr)
        {
            Id = BaseBusinessLogic.ConvertToDecimal(dr[BaseOrganizeExpressEntity.FieldID]);
            NotDispatchRange = BaseBusinessLogic.ConvertToString(dr[BaseOrganizeExpressEntity.FieldNotDispatchRange]);
            ScanSelect = BaseBusinessLogic.ConvertToNullableDecimal(dr[BaseOrganizeExpressEntity.FieldScanSelect]);
            SitePrior = BaseBusinessLogic.ConvertToNullableDecimal(dr[BaseOrganizeExpressEntity.FieldSitePrior]);
            ModifiedUserId = BaseBusinessLogic.ConvertToString(dr[BaseOrganizeExpressEntity.FieldModifiedUserId]);
            PrivateRemark = BaseBusinessLogic.ConvertToString(dr[BaseOrganizeExpressEntity.FieldPrivateRemark]);
            PublicRemark = BaseBusinessLogic.ConvertToString(dr[BaseOrganizeExpressEntity.FieldPublicRemark]);
            DispatchTimeLimit = BaseBusinessLogic.ConvertToString(dr[BaseOrganizeExpressEntity.FieldDispatchTimeLimit]);
            AgentMoneyLimited = BaseBusinessLogic.ConvertToNullableDecimal(dr[BaseOrganizeExpressEntity.FieldAgentMoneyLimited]);
            CreateBy = BaseBusinessLogic.ConvertToString(dr[BaseOrganizeExpressEntity.FieldCreateBy]);
            DefaultSendPlace = BaseBusinessLogic.ConvertToString(dr[BaseOrganizeExpressEntity.FieldDefaultSendPlace]);
            CreateOn = BaseBusinessLogic.ConvertToNullableDateTime(dr[BaseOrganizeExpressEntity.FieldCreateOn]);
            DispatchRange = BaseBusinessLogic.ConvertToString(dr[BaseOrganizeExpressEntity.FieldDispatchRange]);
            AllowToPayment = BaseBusinessLogic.ConvertToNullableDecimal(dr[BaseOrganizeExpressEntity.FieldAllowToPayment]);
            CreateUserId = BaseBusinessLogic.ConvertToString(dr[BaseOrganizeExpressEntity.FieldCreateUserId]);
            AllowAgentMoney = BaseBusinessLogic.ConvertToNullableDecimal(dr[BaseOrganizeExpressEntity.FieldAllowAgentMoney]);
            ModifiedOn = BaseBusinessLogic.ConvertToNullableDateTime(dr[BaseOrganizeExpressEntity.FieldModifiedOn]);
            DispatchOutRangeFee = BaseBusinessLogic.ConvertToNullableDecimal(dr[BaseOrganizeExpressEntity.FieldDispatchOutRangeFee]);
            Currency = BaseBusinessLogic.ConvertToString(dr[BaseOrganizeExpressEntity.FieldCurrency]);
            DispatchRangeFee = BaseBusinessLogic.ConvertToNullableDecimal(dr[BaseOrganizeExpressEntity.FieldDispatchRangeFee]);
            ModifiedBy = BaseBusinessLogic.ConvertToString(dr[BaseOrganizeExpressEntity.FieldModifiedBy]);
            DispatchMoneyDesc = BaseBusinessLogic.ConvertToString(dr[BaseOrganizeExpressEntity.FieldDispatchMoneyDesc]);
            WebSiteName = BaseBusinessLogic.ConvertToString(dr[BaseOrganizeExpressEntity.FieldWebSiteName]);
            WebEnabled = BaseBusinessLogic.ConvertToNullableDecimal(dr[BaseOrganizeExpressEntity.FieldWebEnabled]);
            InternalDispatch = BaseBusinessLogic.ConvertToString(dr[BaseOrganizeExpressEntity.FieldInternalDispatch]);
            IsTransferCenter = BaseBusinessLogic.ConvertToNullableDecimal(dr[BaseOrganizeExpressEntity.FieldIsTransferCenter]);
            IsErpOpen = BaseBusinessLogic.ConvertToInt(dr[BaseOrganizeExpressEntity.FieldIsErpOpen]);
            IsReceiveOrder = BaseBusinessLogic.ConvertToInt(dr[BaseOrganizeExpressEntity.FieldIsReceiveOrder]);
            IsReceiveComplain = BaseBusinessLogic.ConvertToInt(dr[BaseOrganizeExpressEntity.FieldIsReceiveComplain]);
            // 获取扩展属性
            GetFromExpand(dr);
            return this;
        }

        ///<summary>
        /// 网点基础资料扩展表
        ///</summary>
        [FieldDescription("网点基础资料扩展表")]
        public static string TableName = "BaseOrganize_Express";

        ///<summary>
        /// 组织机构ID
        ///</summary>
        public static string FieldID = "ID";

        ///<summary>
        /// 不派送范围
        ///</summary>
        public static string FieldNotDispatchRange = "Not_Dispatch_Range";

        ///<summary>
        /// 标识此网点只在某些扫描(到件)时能选择
        ///</summary>
        public static string FieldScanSelect = "Scan_Select";

        ///<summary>
        /// 站点优先级(统计运单状态时使用，数值越大级别越高)
        ///</summary>
        public static string FieldSitePrior = "Site_Prior";

        ///<summary>
        /// 修改用户主键
        ///</summary>
        public static string FieldModifiedUserId = "ModifiedUserId";

        ///<summary>
        /// 内部备注
        ///</summary>
        public static string FieldPrivateRemark = "Private_Remark";

        ///<summary>
        /// 对外备注
        ///</summary>
        public static string FieldPublicRemark = "Public_Remark";

        ///<summary>
        /// 派送时效限制
        ///</summary>
        public static string FieldDispatchTimeLimit = "Dispatch_Time_Limit";

        ///<summary>
        /// 代收货款限制金额
        ///</summary>
        public static string FieldAgentMoneyLimited = "Agent_Money_Limited";

        ///<summary>
        /// 创建用户
        ///</summary>
        public static string FieldCreateBy = "CreateBy";

        ///<summary>
        /// 默认发件地
        ///</summary>
        public static string FieldDefaultSendPlace = "Default_Send_Place";

        ///<summary>
        /// 创建日期
        ///</summary>
        public static string FieldCreateOn = "CreateOn";

        ///<summary>
        /// 派送范围
        ///</summary>
        public static string FieldDispatchRange = "Dispatch_Range";

        ///<summary>
        /// 允许到付
        ///</summary>
        public static string FieldAllowToPayment = "Allow_ToPayment";

        ///<summary>
        /// 创建用户主键
        ///</summary>
        public static string FieldCreateUserId = "CreateUserId";

        ///<summary>
        /// 允许代收货款
        ///</summary>
        public static string FieldAllowAgentMoney = "Allow_Agent_Money";

        ///<summary>
        /// 修改日期
        ///</summary>
        public static string FieldModifiedOn = "ModifiedOn";

        ///<summary>
        /// 区域外派费
        ///</summary>
        public static string FieldDispatchOutRangeFee = "Dispatch_OutRange_Fee";

        ///<summary>
        /// 本币币别
        ///</summary>
        public static string FieldCurrency = "Currency";

        ///<summary>
        /// 区域内派费
        ///</summary>
        public static string FieldDispatchRangeFee = "Dispatch_Range_Fee";

        ///<summary>
        /// 修改用户
        ///</summary>
        public static string FieldModifiedBy = "ModifiedBy";

        ///<summary>
        /// 派送费用说明
        ///</summary>
        public static string FieldDispatchMoneyDesc = "Dispatch_Money_Desc";

        ///<summary>
        /// 派送费用说明
        ///</summary>
        public static string FieldWebSiteName = "WebSiteName";

        ///<summary>
        /// 外网启用
        ///</summary>
        public static string FieldWebEnabled = "WebEnabled";

        ///<summary>
        /// 对内派送范围
        ///</summary>
        public static string FieldInternalDispatch = "Internal_Dispatch";

        ///<summary>
        /// 转运中心
        ///</summary>
        public static string FieldIsTransferCenter = "Is_Transfer_Center";

        ///<summary>
        /// 开通ERP(0未使用ERP，1已使用ERP)
        ///</summary>
        public static string FieldIsErpOpen = "Is_Erp_Open";

        ///<summary>
        /// 接收订单(0不接收，1接收)
        ///</summary>
        public static string FieldIsReceiveOrder = "Is_Receive_Order";

        ///<summary>
        /// 接收投诉工单(0不接收，1接收)
        ///</summary>
        public static string FieldIsReceiveComplain = "Is_Receive_Complain";
    }
}
