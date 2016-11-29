//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd. 
//-----------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace DotNet.Model
{
    using DotNet.Utilities;

    /// <summary>
    /// BaseAreaEntity
    /// 地区表(省、市、县)
    ///
    /// 修改记录
    /// 
    ///		2015-11-24 版本：2.1 JiRiGaLa Statistics int, 1 区域表， 是否纳入统计 增加。
    ///		2015-03-09 版本：2.0 JiRiGaLa DelayDay 增加。
    ///		2014-02-11 版本：1.0 JiRiGaLa 创建主键。
    ///
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2015-03-09</date>
    /// </author>
    /// </summary>
    [Serializable, DataContract]
    public partial class BaseAreaEntity : BaseEntity
    {
        private string id = null;
        /// <summary>
        /// 主键
        /// </summary>
        [FieldDescription("主键", false)]
        [DataMember]
        public string Id
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

        private string parentId = null;
        /// <summary>
        /// 父节点主键
        /// </summary>
        [FieldDescription("父节点主键")]
        [DataMember]
        public string ParentId
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

        private string code = null;
        /// <summary>
        /// 编号
        /// </summary>
        [FieldDescription("编号")]
        [DataMember]
        public string Code
        {
            get
            {
                return code;
            }
            set
            {
                code = value;
            }
        }

        private string fullName = null;
        /// <summary>
        /// 名称
        /// </summary>
        [FieldDescription("名称")]
        [DataMember]
        public string FullName
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

        private string shortName = null;
        /// <summary>
        /// 简称
        /// </summary>
        [FieldDescription("简称")]
        [DataMember]
        public string ShortName
        {
            get
            {
                return shortName;
            }
            set
            {
                shortName = value;
            }
        }

        private string postalcode = null;
        /// <summary>
        /// 邮编
        /// </summary>
        [FieldDescription("邮编")]
        [DataMember]
        public string Postalcode
        {
            get
            {
                return postalcode;
            }
            set
            {
                postalcode = value;
            }
        }

        private string quickQuery = string.Empty;
        /// <summary>
        /// 快速查询，全拼
        /// </summary>
        [FieldDescription("快速查询，全拼")]
        [DataMember]
        public string QuickQuery
        {
            get
            {
                return quickQuery;
            }
            set
            {
                quickQuery = value;
            }
        }

        private string simpleSpelling = string.Empty;
        /// <summary>
        /// 快速查询，简拼
        /// </summary>
        [FieldDescription("快速查询，简拼")]
        [DataMember]
        public string SimpleSpelling
        {
            get
            {
                return simpleSpelling;
            }
            set
            {
                simpleSpelling = value;
            }
        }

        private string province = null;
        /// <summary>
        /// 省
        /// </summary>
        [FieldDescription("省")]
        [DataMember]
        public string Province
        {
            get
            {
                return province;
            }
            set
            {
                province = value;
            }
        }

        private string city = null;
        /// <summary>
        /// 市
        /// </summary>
        [FieldDescription("市")]
        [DataMember]
        public string City
        {
            get
            {
                return city;
            }
            set
            {
                city = value;
            }
        }

        private string district = null;
        /// <summary>
        /// 县
        /// </summary>
        [FieldDescription("县")]
        [DataMember]
        public string District
        {
            get
            {
                return district;
            }
            set
            {
                district = value;
            }
        }

        private int delayDay = 0;
        /// <summary>
        /// 延迟天数
        /// </summary>
        [FieldDescription("延迟天数")]
        [DataMember]
        public int DelayDay
        {
            get
            {
                return delayDay;
            }
            set
            {
                delayDay = value;
            }
        }

        private string longitude = null;
        /// <summary>
        /// 经度
        /// </summary>
        [FieldDescription("经度")]
        [DataMember]
        public string Longitude
        {
            get
            {
                return longitude;
            }
            set
            {
                longitude = value;
            }
        }

        private string latitude = null;
        /// <summary>
        /// 维度
        /// </summary>
        [FieldDescription("维度")]
        [DataMember]
        public string Latitude
        {
            get
            {
                return latitude;
            }
            set
            {
                latitude = value;
            }
        }

        private string manageCompanyId = null;
        /// <summary>
        /// 管理网点主键
        /// </summary>
        [FieldDescription("管理网点主键")]
        [DataMember]
        public string ManageCompanyId
        {
            get
            {
                return manageCompanyId;
            }
            set
            {
                manageCompanyId = value;
            }
        }

        private string manageCompanyCode = null;
        /// <summary>
        /// 管理网点编号
        /// </summary>
        [FieldDescription("管理网点编号")]
        [DataMember]
        public string ManageCompanyCode
        {
            get
            {
                return manageCompanyCode;
            }
            set
            {
                manageCompanyCode = value;
            }
        }

        private string manageCompany = null;
        /// <summary>
        /// 管理网点
        /// </summary>
        [FieldDescription("管理网点")]
        [DataMember]
        public string ManageCompany
        {
            get
            {
                return manageCompany;
            }
            set
            {
                manageCompany = value;
            }
        }

        private int? opening = 0;
        /// <summary>
        /// 开通业务
        /// </summary>
        [FieldDescription("开通业务")]
        [DataMember]
        public int? Opening
        {
            get
            {
                return opening;
            }
            set
            {
                opening = value;
            }
        }

        private int? whole = 0;
        /// <summary>
        /// 全境派送
        /// </summary>
        [FieldDescription("全境派送")]
        [DataMember]
        public int? Whole
        {
            get
            {
                return whole;
            }
            set
            {
                whole = value;
            }
        }

        private int? receive = 0;
        /// <summary>
        /// 揽收
        /// </summary>
        [FieldDescription("揽收")]
        [DataMember]
        public int? Receive
        {
            get
            {
                return receive;
            }
            set
            {
                receive = value;
            }
        }

        private int? send = 0;
        /// <summary>
        /// 发件
        /// </summary>
        [FieldDescription("发件")]
        [DataMember]
        public int? Send
        {
            get
            {
                return send;
            }
            set
            {
                send = value;
            }
        }

        private int? layer = 0;
        /// <summary>
        /// 层级
        ///    0：虚拟的？
        ///    1：哪个州的？
        ///    2：哪个国家？
        ///    3：哪个大区？
        ///    4：哪个省？
        ///    5：哪个市？
        ///    6：哪个县、区？
        ///    7：哪街道？
        /// </summary>
        [FieldDescription("层级")]
        [DataMember]
        public int? Layer
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

        private int? allowToPay = 0;
        /// <summary>
        /// 允许到付
        /// </summary>
        [FieldDescription("允许到付")]
        [DataMember]
        public int? AllowToPay
        {
            get
            {
                return allowToPay;
            }
            set
            {
                allowToPay = value;
            }
        }

        private int? maxToPayment = 0;
        /// <summary>
        /// 允许的最大到付款
        /// </summary>
        [FieldDescription("允许的最大到付款")]
        [DataMember]
        public int? MaxToPayment
        {
            get
            {
                return maxToPayment;
            }
            set
            {
                maxToPayment = value;
            }
        }

        private int? allowGoodsPay = 0;
        /// <summary>
        /// 允许代收
        /// </summary>
        [FieldDescription("允许代收")]
        [DataMember]
        public int? AllowGoodsPay
        {
            get
            {
                return allowGoodsPay;
            }
            set
            {
                allowGoodsPay = value;
            }
        }

        private int? maxGoodsPayment = 0;
        /// <summary>
        /// 允许的最大代收款
        /// </summary>
        [FieldDescription("允许的最大代收款")]
        [DataMember]
        public int? MaxGoodsPayment
        {
            get
            {
                return maxGoodsPayment;
            }
            set
            {
                maxGoodsPayment = value;
            }
        }

        private string mark = null;
        /// <summary>
        /// 手写大头笔
        /// </summary>
        [FieldDescription("手写大头笔")]
        [DataMember]
        public string Mark
        {
            get
            {
                return mark;
            }
            set
            {
                mark = value;
            }
        }

        private string printMark = null;
        /// <summary>
        /// 机打大头笔
        /// </summary>
        [FieldDescription("机打大头笔")]
        [DataMember]
        public string PrintMark
        {
            get
            {
                return printMark;
            }
            set
            {
                printMark = value;
            }
        }

        private int? outOfRange = 0;
        /// <summary>
        /// 超区、超出业务范围
        /// </summary>
        [FieldDescription("超区、超出业务范围")]
        [DataMember]
        public int? OutOfRange
        {
            get
            {
                return outOfRange;
            }
            set
            {
                outOfRange = value;
            }
        }

        private int? deletionStateCode = 0;
        /// <summary>
        /// 删除标记
        /// </summary>
        [FieldDescription("删除标记")]
        [DataMember]
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

        private string description = null;
        /// <summary>
        /// 备注
        /// </summary>
        [FieldDescription("备注")]
        [DataMember]
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

        private int networkOrders = 0;
        /// <summary>
        /// 开通网络订单
        /// </summary>
        [FieldDescription("开通网络订单")]
        [DataMember]
        public int NetworkOrders
        {
            get
            {
                return networkOrders;
            }
            set
            {
                networkOrders = value;
            }
        }

        private int? enabled = 1;
        /// <summary>
        /// 有效
        /// </summary>
        [FieldDescription("有效")]
        [DataMember]
        public int? Enabled
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

        private int? statistics = 1;
        /// <summary>
        /// 纳入统计
        /// </summary>
        [FieldDescription("纳入统计")]
        [DataMember]
        public int? Statistics
        {
            get
            {
                return statistics;
            }
            set
            {
                statistics = value;
            }
        }

        private int? sortCode = 0;
        /// <summary>
        /// 排序码
        /// </summary>
        [FieldDescription("排序码")]
        [DataMember]
        public int? SortCode
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

        private string createUserId = null;
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

        private string createBy = null;
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

        private string modifiedUserId = null;
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

        private string modifiedBy = null;
        /// <summary>
        /// 修改用户
        /// </summary>
        [FieldDescription("修改用户", false)]
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

        /// <summary>
        /// 从数据行读取
        /// </summary>
        /// <param name="dr">数据行</param>
        protected override BaseEntity GetFrom(IDataRow dr)
        {
            Id = BaseBusinessLogic.ConvertToString(dr[BaseAreaEntity.FieldId]);
            ParentId = BaseBusinessLogic.ConvertToString(dr[BaseAreaEntity.FieldParentId]);
            Code = BaseBusinessLogic.ConvertToString(dr[BaseAreaEntity.FieldCode]);
            QuickQuery = BaseBusinessLogic.ConvertToString(dr[BaseAreaEntity.FieldQuickQuery]);
            SimpleSpelling = BaseBusinessLogic.ConvertToString(dr[BaseAreaEntity.FieldSimpleSpelling]);
            Province = BaseBusinessLogic.ConvertToString(dr[BaseAreaEntity.FieldProvince]);
            City = BaseBusinessLogic.ConvertToString(dr[BaseAreaEntity.FieldCity]);
            District = BaseBusinessLogic.ConvertToString(dr[BaseAreaEntity.FieldDistrict]);
            FullName = BaseBusinessLogic.ConvertToString(dr[BaseAreaEntity.FieldFullName]);
            ShortName = BaseBusinessLogic.ConvertToString(dr[BaseAreaEntity.FieldShortName]);
            Postalcode = BaseBusinessLogic.ConvertToString(dr[BaseAreaEntity.FieldPostalcode]);
            DelayDay = BaseBusinessLogic.ConvertToInt(dr[BaseAreaEntity.FieldDelayDay]);
            Longitude = BaseBusinessLogic.ConvertToString(dr[BaseAreaEntity.FieldLongitude]);
            Latitude = BaseBusinessLogic.ConvertToString(dr[BaseAreaEntity.FieldLatitude]);
            NetworkOrders = BaseBusinessLogic.ConvertToInt(dr[BaseAreaEntity.FieldNetworkOrders]);
            ManageCompanyId = BaseBusinessLogic.ConvertToString(dr[BaseAreaEntity.FieldManageCompanyId]);
            ManageCompanyCode = BaseBusinessLogic.ConvertToString(dr[BaseAreaEntity.FieldManageCompanyCode]);
            ManageCompany = BaseBusinessLogic.ConvertToString(dr[BaseAreaEntity.FieldManageCompany]);
            Whole = BaseBusinessLogic.ConvertToInt(dr[BaseAreaEntity.FieldWhole]);
            Receive = BaseBusinessLogic.ConvertToInt(dr[BaseAreaEntity.FieldReceive]);
            Send = BaseBusinessLogic.ConvertToInt(dr[BaseAreaEntity.FieldSend]);
            Layer = BaseBusinessLogic.ConvertToInt(dr[BaseAreaEntity.FieldLayer]);
            Opening = BaseBusinessLogic.ConvertToInt(dr[BaseAreaEntity.FieldOpening]);
            AllowToPay = BaseBusinessLogic.ConvertToInt(dr[BaseAreaEntity.FieldAllowToPay]);
            MaxToPayment = BaseBusinessLogic.ConvertToInt(dr[BaseAreaEntity.FieldMaxToPayment]);
            AllowGoodsPay = BaseBusinessLogic.ConvertToInt(dr[BaseAreaEntity.FieldAllowGoodsPay]);
            MaxGoodsPayment = BaseBusinessLogic.ConvertToInt(dr[BaseAreaEntity.FieldMaxGoodsPayment]);
            DeletionStateCode = BaseBusinessLogic.ConvertToInt(dr[BaseAreaEntity.FieldDeletionStateCode]);
            Mark = BaseBusinessLogic.ConvertToString(dr[BaseAreaEntity.FieldMark]);
            PrintMark = BaseBusinessLogic.ConvertToString(dr[BaseAreaEntity.FieldPrintMark]);
            OutOfRange = BaseBusinessLogic.ConvertToInt(dr[BaseAreaEntity.FieldOutOfRange]);
            Description = BaseBusinessLogic.ConvertToString(dr[BaseAreaEntity.FieldDescription]);
            Enabled = BaseBusinessLogic.ConvertToInt(dr[BaseAreaEntity.FieldEnabled]);
            Statistics = BaseBusinessLogic.ConvertToInt(dr[BaseAreaEntity.FieldStatistics]);
            SortCode = BaseBusinessLogic.ConvertToInt(dr[BaseAreaEntity.FieldSortCode]);
            CreateOn = BaseBusinessLogic.ConvertToNullableDateTime(dr[BaseAreaEntity.FieldCreateOn]);
            CreateUserId = BaseBusinessLogic.ConvertToString(dr[BaseAreaEntity.FieldCreateUserId]);
            CreateBy = BaseBusinessLogic.ConvertToString(dr[BaseAreaEntity.FieldCreateBy]);
            ModifiedOn = BaseBusinessLogic.ConvertToNullableDateTime(dr[BaseAreaEntity.FieldModifiedOn]);
            ModifiedUserId = BaseBusinessLogic.ConvertToString(dr[BaseAreaEntity.FieldModifiedUserId]);
            ModifiedBy = BaseBusinessLogic.ConvertToString(dr[BaseAreaEntity.FieldModifiedBy]);
            // 获取扩展属性
            GetFromExpand(dr);
            return this;
        }

        ///<summary>
        /// 区域表
        ///</summary>
        [NonSerialized]
        [FieldDescription("区域表")]
        public static string TableName = "BaseArea";

        ///<summary>
        /// 主键
        ///</summary>
        [NonSerialized]
        [FieldDescription("主键")]
        public static string FieldId = "Id";

        ///<summary>
        /// 父节点主键
        ///</summary>
        [NonSerialized]
        [FieldDescription("父节点主键")]
        public static string FieldParentId = "ParentId";

        ///<summary>
        /// 编号
        ///</summary>
        [NonSerialized]
        [FieldDescription("编号")]
        public static string FieldCode = "Code";

        ///<summary>
        /// 省
        ///</summary>
        [NonSerialized]
        [FieldDescription("省")]
        public static string FieldProvince = "Province";

        ///<summary>
        /// 市
        ///</summary>
        [NonSerialized]
        [FieldDescription("市")]
        public static string FieldCity = "City";

        ///<summary>
        /// 县
        ///</summary>
        [NonSerialized]
        [FieldDescription("县")]
        public static string FieldDistrict = "District";

        ///<summary>
        /// 名称
        ///</summary>
        [NonSerialized]
        [FieldDescription("名称")]
        public static string FieldFullName = "FullName";

        ///<summary>
        /// 延迟天数
        ///</summary>
        [NonSerialized]
        [FieldDescription("延迟天数")]
        public static string FieldDelayDay = "DelayDay";

        ///<summary>
        /// 邮编
        ///</summary>
        [NonSerialized]
        [FieldDescription("邮编")]
        public static string FieldPostalcode = "Postalcode";

        ///<summary>
        /// 快速查询，全拼
        ///</summary>
        [NonSerialized]
        [FieldDescription("全拼")]
        public static string FieldQuickQuery = "QuickQuery";

        ///<summary>
        /// 快速查询，简拼
        ///</summary>
        [NonSerialized]
        [FieldDescription("简拼")]
        public static string FieldSimpleSpelling = "SimpleSpelling";

        ///<summary>
        /// 简称
        ///</summary>
        [NonSerialized]
        [FieldDescription("简称")]
        public static string FieldShortName = "ShortName";

        /// <summary>
        /// 百度经度
        /// </summary>
        [NonSerialized]
        [FieldDescription("百度经度")]
        public static string FieldLongitude = "Longitude";

        /// <summary>
        /// 百度纬度
        /// </summary>
        [NonSerialized]
        [FieldDescription("百度纬度")]
        public static string FieldLatitude = "Latitude";

        /// <summary>
        /// 开通业务
        /// </summary>
        [NonSerialized]
        [FieldDescription("开通业务")]
        public static string FieldOpening = "Opening";

        ///<summary>
        /// 网络订单
        ///</summary>
        [NonSerialized]
        [FieldDescription("网络订单")]
        public static string FieldNetworkOrders = "NetworkOrders";

        ///<summary>
        /// 管理网点主键
        ///</summary>
        [NonSerialized]
        [FieldDescription("管理网点主键")]
        public static string FieldManageCompanyId = "ManageCompanyId";

        ///<summary>
        /// 管理网点编号
        ///</summary>
        [NonSerialized]
        [FieldDescription("管理网点编号")]
        public static string FieldManageCompanyCode = "ManageCompanyCode";

        ///<summary>
        /// 管理网点
        ///</summary>
        [NonSerialized]
        [FieldDescription("管理网点")]
        public static string FieldManageCompany = "ManageCompany";

        /// <summary>
        /// 全境派送
        /// </summary>
        [NonSerialized]
        [FieldDescription("全境派送")]
        public static string FieldWhole = "Whole";

        /// <summary>
        /// 揽收
        /// </summary>
        [NonSerialized]
        [FieldDescription("揽收")]
        public static string FieldReceive = "Receive";

        /// <summary>
        /// 发件
        /// </summary>
        [NonSerialized]
        [FieldDescription("发件")]
        public static string FieldSend = "Send";

        ///<summary>
        /// 层级
        ///</summary>
        [NonSerialized]
        [FieldDescription("层级")]
        public static string FieldLayer = "Layer";

        /// <summary>
        /// 允许到付
        /// </summary>
        [NonSerialized]
        [FieldDescription("允许到付")]
        public static string FieldAllowToPay = "AllowToPay";

        /// <summary>
        /// 允许的最大到付款
        /// </summary>
        [NonSerialized]
        [FieldDescription("最大到付款")]
        public static string FieldMaxToPayment = "MaxToPayment";

        /// <summary>
        /// 允许代收
        /// </summary>
        [NonSerialized]
        [FieldDescription("允许代收")]
        public static string FieldAllowGoodsPay = "AllowGoodsPay";

        /// <summary>
        /// 允许的最大代收款
        /// </summary>
        [NonSerialized]
        [FieldDescription("最大代收款")]
        public static string FieldMaxGoodsPayment = "MaxGoodsPayment";

        /// <summary>
        /// 手写大头笔
        /// </summary>
        [NonSerialized]
        [FieldDescription("手写大头笔")]
        public static string FieldMark = "Mark";

        /// <summary>
        /// 机打大头笔
        /// </summary>
        [NonSerialized]
        [FieldDescription("机打大头笔")]
        public static string FieldPrintMark = "PrintMark";

        /// <summary>
        /// 超区、超出业务范围
        /// </summary>
        [NonSerialized]
        [FieldDescription("超区")]
        public static string FieldOutOfRange = "OutOfRange";

        ///<summary>
        /// 删除标志
        ///</summary>
        [NonSerialized]
        [FieldDescription("删除标志")]
        public static string FieldDeletionStateCode = "DeletionStateCode";

        ///<summary>
        /// 纳入统计
        ///</summary>
        [NonSerialized]
        [FieldDescription("纳入统计")]
        public static string FieldStatistics = "Statistics";

        ///<summary>
        /// 备注
        ///</summary>
        [NonSerialized]
        [FieldDescription("备注")]
        public static string FieldDescription = "Description";

        ///<summary>
        /// 有效
        ///</summary>
        [NonSerialized]
        [FieldDescription("有效")]
        public static string FieldEnabled = "Enabled";

        ///<summary>
        /// 排序码
        ///</summary>
        [NonSerialized]
        [FieldDescription("排序码")]
        public static string FieldSortCode = "SortCode";

        ///<summary>
        /// 创建日期
        ///</summary>
        [NonSerialized]
        [FieldDescription("创建日期")]
        public static string FieldCreateOn = "CreateOn";

        ///<summary>
        /// 创建用户主键
        ///</summary>
        [NonSerialized]
        [FieldDescription("创建用户主键")]
        public static string FieldCreateUserId = "CreateUserId";

        ///<summary>
        /// 创建用户
        ///</summary>
        [NonSerialized]
        [FieldDescription("创建用户")]
        public static string FieldCreateBy = "CreateBy";

        ///<summary>
        /// 修改日期
        ///</summary>
        [NonSerialized]
        [FieldDescription("修改日期")]
        public static string FieldModifiedOn = "ModifiedOn";

        ///<summary>
        /// 修改用户主键
        ///</summary>
        [NonSerialized]
        [FieldDescription("修改用户主键")]
        public static string FieldModifiedUserId = "ModifiedUserId";

        ///<summary>
        /// 修改用户
        ///</summary>
        [NonSerialized]
        [FieldDescription("修改用户")]
        public static string FieldModifiedBy = "ModifiedBy";
    }
}
