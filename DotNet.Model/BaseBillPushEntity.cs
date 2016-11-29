//-----------------------------------------------------------------------
// <copyright file="BaseBillPushEntity.cs" company="Hairihan">
//     Copyright (C) 2016 , All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Runtime.Serialization;

namespace DotNet.Model
{
    using DotNet.Utilities;

    /// <summary>
    /// BaseBillPushEntity
    /// 订单信息订阅表
    /// 
    /// 修改记录
    /// 
    /// 2014-02-24 版本：1.0 JiRiGaLa 创建文件。
    /// 
    /// <author>
    ///     <name>JiRiGaLa</name>
    ///     <date>2014-02-24</date>
    /// </author>
    /// </summary>
    [Serializable, DataContract]
    public partial class BaseBillPushEntity : BaseEntity
    {
        private string id = string.Empty;
        /// <summary>
        /// 主键
        /// </summary>
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

        private string billCode = string.Empty;
        /// <summary>
        /// 运单编号
        /// </summary>
        [DataMember]
        public string BillCode
        {
            get
            {
                return billCode;
            }
            set
            {
                billCode = value;
            }
        }

        private string pushCategory = string.Empty;
        /// <summary>
        /// 推送类型（短信、微信、易信、Email)
        /// </summary>
        [DataMember]
        public string PushCategory
        {
            get
            {
                return pushCategory;
            }
            set
            {
                pushCategory = value;
            }
        }

        private string pushTarget = string.Empty;
        /// <summary>
        /// 推送对象(手机号、OpenId、邮箱地址)
        /// </summary>
        [DataMember]
        public string PushTarget
        {
            get
            {
                return pushTarget;
            }
            set
            {
                pushTarget = value;
            }
        }

        private string pushTime = string.Empty;
        /// <summary>
        /// 推送时间段
        /// </summary>
        [DataMember]
        public string PushTime
        {
            get
            {
                return pushTime;
            }
            set
            {
                pushTime = value;
            }
        }

        private Decimal? subscriptionCategory = null;
        /// <summary>
        /// 订阅类型(1-收件、2-发件、4-到件、8-派送、16-签收)
        /// </summary>
        [DataMember]
        public Decimal? SubscriptionCategory
        {
            get
            {
                return subscriptionCategory;
            }
            set
            {
                subscriptionCategory = value;
            }
        }

        private string subscriptionSource = string.Empty;
        /// <summary>
        /// 订阅来源(官网、微信、易信)
        /// </summary>
        [DataMember]
        public string SubscriptionSource
        {
            get
            {
                return subscriptionSource;
            }
            set
            {
                subscriptionSource = value;
            }
        }

        private Decimal? subscriptionStatus = null;
        /// <summary>
        /// 订阅状态(0-订阅开始、1-订阅完成)
        /// </summary>
        [DataMember]
        public Decimal? SubscriptionStatus
        {
            get
            {
                return subscriptionStatus;
            }
            set
            {
                subscriptionStatus = value;
            }
        }

        private DateTime? createOn = DateTime.Now;
        /// <summary>
        /// 订阅时间
        /// </summary>
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

        private string createBy = string.Empty;
        /// <summary>
        /// 订阅人（网点）
        /// </summary>
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

        /// <summary>
        /// 从数据行读取
        /// </summary>
        /// <param name="dr">数据行</param>
        protected override BaseEntity GetFrom(IDataRow dr)
        {
            Id = BaseBusinessLogic.ConvertToString(dr[BaseBillPushEntity.FieldId]);
            BillCode = BaseBusinessLogic.ConvertToString(dr[BaseBillPushEntity.FieldBillCode]);
            PushCategory = BaseBusinessLogic.ConvertToString(dr[BaseBillPushEntity.FieldPushCategory]);
            PushTarget = BaseBusinessLogic.ConvertToString(dr[BaseBillPushEntity.FieldPushTarget]);
            PushTime = BaseBusinessLogic.ConvertToString(dr[BaseBillPushEntity.FieldPushTime]);
            SubscriptionCategory = BaseBusinessLogic.ConvertToNullableDecimal(dr[BaseBillPushEntity.FieldSubscriptionCategory]);
            SubscriptionSource = BaseBusinessLogic.ConvertToString(dr[BaseBillPushEntity.FieldSubscriptionSource]);
            SubscriptionStatus = BaseBusinessLogic.ConvertToNullableDecimal(dr[BaseBillPushEntity.FieldSubscriptionStatus]);
            CreateOn = BaseBusinessLogic.ConvertToNullableDateTime(dr[BaseBillPushEntity.FieldCreateOn]);
            CreateBy = BaseBusinessLogic.ConvertToString(dr[BaseBillPushEntity.FieldCreateBy]);
            // 获取扩展属性
            GetFromExpand(dr);
            return this;
        }

        ///<summary>
        /// 订单信息订阅表
        ///</summary>
        [NonSerialized]
        public static string TableName = "BaseBillPush";

        ///<summary>
        /// 主键
        ///</summary>
        [NonSerialized]
        public static string FieldId = "Id";

        ///<summary>
        /// 运单编号
        ///</summary>
        [NonSerialized]
        public static string FieldBillCode = "BillCode";

        ///<summary>
        /// 推送类型（短信、微信、易信、Email)
        ///</summary>
        [NonSerialized]
        public static string FieldPushCategory = "PushCategory";

        ///<summary>
        /// 推送对象(手机号、OpenId、邮箱地址)
        ///</summary>
        [NonSerialized]
        public static string FieldPushTarget = "PushTarget";

        ///<summary>
        /// 推送时间段
        ///</summary>
        [NonSerialized]
        public static string FieldPushTime = "PushTime";

        ///<summary>
        /// 订阅类型(1-收件、2-发件、4-到件、8-派送、16-签收)
        ///</summary>
        [NonSerialized]
        public static string FieldSubscriptionCategory = "SubscriptionCategory";

        ///<summary>
        /// 订阅来源(官网、微信、易信)
        ///</summary>
        [NonSerialized]
        public static string FieldSubscriptionSource = "SubscriptionSource";

        ///<summary>
        /// 订阅状态(0-订阅开始、1-订阅完成)
        ///</summary>
        [NonSerialized]
        public static string FieldSubscriptionStatus = "SubscriptionStatus";

        ///<summary>
        /// 订阅时间
        ///</summary>
        [NonSerialized]
        public static string FieldCreateOn = "CreateOn";

        ///<summary>
        /// 订阅人（网点）
        ///</summary>
        [NonSerialized]
        public static string FieldCreateBy = "CreateBy";
    }
}
