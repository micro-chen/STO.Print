//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2015 , STO TECH, Ltd. 
//-----------------------------------------------------------------

using System;

namespace STO.Print.Model
{
    using DotNet.Model;
    using DotNet.Utilities;

    /// <summary>
    /// ZtoPrintBillEntity
    /// 
    /// 
    /// 修改纪录
    /// 
    /// 2015-07-16 版本：1.0 YangHengLian 创建文件。
    /// 
    /// <author>
    ///     <name>YangHengLian</name>
    ///     <date>2015-07-16</date>
    /// </author>
    /// </summary>
    public class ZtoPrintBillEntity : BaseEntity
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

        private string sendPostcode = string.Empty;
        /// <summary>
        /// SEND_POSTCODE
        /// </summary>
        [FieldDescription("SEND_POSTCODE")]
        public string SendPostcode
        {
            get
            {
                return sendPostcode;
            }
            set
            {
                sendPostcode = value;
            }
        }

        private string receiveMan = string.Empty;
        /// <summary>
        /// RECEIVE_MAN
        /// </summary>
        [FieldDescription("RECEIVE_MAN")]
        public string ReceiveMan
        {
            get
            {
                return receiveMan;
            }
            set
            {
                receiveMan = value;
            }
        }

        private string receiveProvince = string.Empty;
        /// <summary>
        /// RECEIVE_PROVINCE
        /// </summary>
        [FieldDescription("RECEIVE_PROVINCE")]
        public string ReceiveProvince
        {
            get
            {
                return receiveProvince;
            }
            set
            {
                receiveProvince = value;
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

        private string modifiedSite = string.Empty;
        /// <summary>
        /// MODIFIEDSITE
        /// </summary>
        [FieldDescription("MODIFIEDSITE")]
        public string ModifiedSite
        {
            get
            {
                return modifiedSite;
            }
            set
            {
                modifiedSite = value;
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

        private string paymentType = string.Empty;
        /// <summary>
        /// PAYMENT_TYPE
        /// </summary>
        [FieldDescription("PAYMENT_TYPE")]
        public string PaymentType
        {
            get
            {
                return paymentType;
            }
            set
            {
                paymentType = value;
            }
        }

        private string length = null;
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

        private string sendMan = string.Empty;
        /// <summary>
        /// SEND_MAN
        /// </summary>
        [FieldDescription("SEND_MAN")]
        public string SendMan
        {
            get
            {
                return sendMan;
            }
            set
            {
                sendMan = value;
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

        private string receiveAddress = string.Empty;
        /// <summary>
        /// RECEIVE_ADDRESS
        /// </summary>
        [FieldDescription("RECEIVE_ADDRESS")]
        public string ReceiveAddress
        {
            get
            {
                return receiveAddress;
            }
            set
            {
                receiveAddress = value;
            }
        }

        private string receivePostcode = string.Empty;
        /// <summary>
        /// RECEIVE_POSTCODE
        /// </summary>
        [FieldDescription("RECEIVE_POSTCODE")]
        public string ReceivePostcode
        {
            get
            {
                return receivePostcode;
            }
            set
            {
                receivePostcode = value;
            }
        }

        private string receivePhone = string.Empty;
        /// <summary>
        /// RECEIVE_PHONE
        /// </summary>
        [FieldDescription("RECEIVE_PHONE")]
        public string ReceivePhone
        {
            get
            {
                return receivePhone;
            }
            set
            {
                receivePhone = value;
            }
        }

        private string sendDepartment = string.Empty;
        /// <summary>
        /// SEND_DEPARTMENT
        /// </summary>
        [FieldDescription("SEND_DEPARTMENT")]
        public string SendDepartment
        {
            get
            {
                return sendDepartment;
            }
            set
            {
                sendDepartment = value;
            }
        }

        private string sendDate;
        /// <summary>
        /// SEND_DATE
        /// </summary>
        [FieldDescription("SEND_DATE")]
        public string SendDate
        {
            get
            {
                return sendDate;
            }
            set
            {
                sendDate = value;
            }
        }

        private string sendProvince = string.Empty;
        /// <summary>
        /// SEND_PROVINCE
        /// </summary>
        [FieldDescription("SEND_PROVINCE")]
        public string SendProvince
        {
            get
            {
                return sendProvince;
            }
            set
            {
                sendProvince = value;
            }
        }

        private string tranFee = null;
        /// <summary>
        /// TRAN_FEE
        /// </summary>
        [FieldDescription("TRAN_FEE")]
        public string TranFee
        {
            get
            {
                return tranFee;
            }
            set
            {
                tranFee = value;
            }
        }

        private string sendDeparture = string.Empty;
        /// <summary>
        /// 始发地
        /// </summary>
        [FieldDescription("SEND_DEPARTURE")]
        public string SendDeparture
        {
            get
            {
                return sendDeparture;
            }
            set
            {
                sendDeparture = value;
            }
        }

        private string sendPhone = string.Empty;
        /// <summary>
        /// SEND_PHONE
        /// </summary>
        [FieldDescription("SEND_PHONE")]
        public string SendPhone
        {
            get
            {
                return sendPhone;
            }
            set
            {
                sendPhone = value;
            }
        }

        private string sendCity = string.Empty;
        /// <summary>
        /// SEND_CITY
        /// </summary>
        [FieldDescription("SEND_CITY")]
        public string SendCity
        {
            get
            {
                return sendCity;
            }
            set
            {
                sendCity = value;
            }
        }

        private string goodsName = string.Empty;
        /// <summary>
        /// GOODS_NAME
        /// </summary>
        [FieldDescription("GOODS_NAME")]
        public string GoodsName
        {
            get
            {
                return goodsName;
            }
            set
            {
                goodsName = value;
            }
        }

        private string weight = null;
        /// <summary>
        /// WEIGHT
        /// </summary>
        [FieldDescription("WEIGHT")]
        public string Weight
        {
            get
            {
                return weight;
            }
            set
            {
                weight = value;
            }
        }

        private string height = null;
        /// <summary>
        /// HEIGHT
        /// </summary>
        [FieldDescription("HEIGHT")]
        public string Height
        {
            get
            {
                return height;
            }
            set
            {
                height = value;
            }
        }

        private string receiveCounty = string.Empty;
        /// <summary>
        /// RECEIVE_COUNTY
        /// </summary>
        [FieldDescription("RECEIVE_COUNTY")]
        public string ReceiveCounty
        {
            get
            {
                return receiveCounty;
            }
            set
            {
                receiveCounty = value;
            }
        }

        private string receiveDestination = string.Empty;
        /// <summary>
        /// 目的地
        /// </summary>
        [FieldDescription("RECEIVE_DESTINATION")]
        public string ReceiveDestination
        {
            get
            {
                return receiveDestination;
            }
            set
            {
                receiveDestination = value;
            }
        }

        private string sendCompany = string.Empty;
        /// <summary>
        /// SEND_COMPANY
        /// </summary>
        [FieldDescription("SEND_COMPANY")]
        public string SendCompany
        {
            get
            {
                return sendCompany;
            }
            set
            {
                sendCompany = value;
            }
        }

        private DateTime createOn;
        /// <summary>
        /// CREATEON
        /// </summary>
        [FieldDescription("CREATEON")]
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

        private string billCode = string.Empty;
        /// <summary>
        /// BILL_CODE
        /// </summary>
        [FieldDescription("BILL_CODE")]
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

        private string totalNumber = null;
        /// <summary>
        /// TOTAL_NUMBER
        /// </summary>
        [FieldDescription("TOTAL_NUMBER")]
        public string TotalNumber
        {
            get
            {
                return totalNumber;
            }
            set
            {
                totalNumber = value;
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

        private string createSite = string.Empty;
        /// <summary>
        /// CREATESITE
        /// </summary>
        [FieldDescription("CREATESITE")]
        public string CreateSite
        {
            get
            {
                return createSite;
            }
            set
            {
                createSite = value;
            }
        }

        private string sendSite = string.Empty;
        /// <summary>
        /// SEND_SITE
        /// </summary>
        [FieldDescription("SEND_SITE")]
        public string SendSite
        {
            get
            {
                return sendSite;
            }
            set
            {
                sendSite = value;
            }
        }

        private string width = null;
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

        private string receiveCity = string.Empty;
        /// <summary>
        /// RECEIVE_CITY
        /// </summary>
        [FieldDescription("RECEIVE_CITY")]
        public string ReceiveCity
        {
            get
            {
                return receiveCity;
            }
            set
            {
                receiveCity = value;
            }
        }

        private string sendAddress = string.Empty;
        /// <summary>
        /// SEND_ADDRESS
        /// </summary>
        [FieldDescription("SEND_ADDRESS")]
        public string SendAddress
        {
            get
            {
                return sendAddress;
            }
            set
            {
                sendAddress = value;
            }
        }

        private string sendCounty = string.Empty;
        /// <summary>
        /// SEND_COUNTY
        /// </summary>
        [FieldDescription("SEND_COUNTY")]
        public string SendCounty
        {
            get
            {
                return sendCounty;
            }
            set
            {
                sendCounty = value;
            }
        }

        private string receiveCompany = string.Empty;
        /// <summary>
        /// RECEIVE_COMPANY
        /// </summary>
        [FieldDescription("RECEIVE_COMPANY")]
        public string ReceiveCompany
        {
            get
            {
                return receiveCompany;
            }
            set
            {
                receiveCompany = value;
            }
        }

        private string bigPen = string.Empty;
        /// <summary>
        /// BIG_PEN
        /// </summary>
        [FieldDescription("BIG_PEN")]
        public string BigPen
        {
            get
            {
                return bigPen;
            }
            set
            {
                bigPen = value;
            }
        }

        private string orderNumber = string.Empty;
        /// <summary>
        /// 订单号
        /// </summary>
        [FieldDescription("OrderNumber")]
        public string OrderNumber
        {
            get
            {
                return orderNumber;
            }
            set
            {
                orderNumber = value;
            }
        }

        private string expressId = string.Empty;
        /// <summary>
        /// 快递ID
        /// </summary>
        [FieldDescription("EXPRESS_ID")]
        public string ExpressId
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

        private Decimal? tOPAYMENT = null;
        /// <summary>
        /// 到付款
        /// </summary>
        public Decimal? TOPAYMENT
        {
            get
            {
                return tOPAYMENT;
            }
            set
            {
                tOPAYMENT = value;
            }
        }

        private Decimal? gOODS_PAYMENT = null;
        /// <summary>
        /// 代收货款
        /// </summary>
        public Decimal? GOODS_PAYMENT
        {
            get
            {
                return gOODS_PAYMENT;
            }
            set
            {
                gOODS_PAYMENT = value;
            }
        }

        /// <summary>
        /// 从数据行读取
        /// </summary>
        /// <param name="dr">数据行</param>
        protected override BaseEntity GetFrom(IDataRow dr)
        {
            GetFromExpand(dr);
            if (dr.ContainsColumn(ZtoPrintBillEntity.FieldId))
            {
                Id = BaseBusinessLogic.ConvertToDecimal(dr[ZtoPrintBillEntity.FieldId]);
            }
            if (dr.ContainsColumn(ZtoPrintBillEntity.FieldSendPostcode))
            {
                SendPostcode = BaseBusinessLogic.ConvertToString(dr[ZtoPrintBillEntity.FieldSendPostcode]);
            }
            if (dr.ContainsColumn(ZtoPrintBillEntity.FieldReceiveMan))
            {
                ReceiveMan = BaseBusinessLogic.ConvertToString(dr[ZtoPrintBillEntity.FieldReceiveMan]);
            }
            if (dr.ContainsColumn(ZtoPrintBillEntity.FieldReceiveProvince))
            {
                ReceiveProvince = BaseBusinessLogic.ConvertToString(dr[ZtoPrintBillEntity.FieldReceiveProvince]);
            }
            if (dr.ContainsColumn(ZtoPrintBillEntity.FieldCreateUserName))
            {
                CreateUserName = BaseBusinessLogic.ConvertToString(dr[ZtoPrintBillEntity.FieldCreateUserName]);
            }
            if (dr.ContainsColumn(ZtoPrintBillEntity.FieldModifiedSite))
            {
                ModifiedSite = BaseBusinessLogic.ConvertToString(dr[ZtoPrintBillEntity.FieldModifiedSite]);
            }
            if (dr.ContainsColumn(ZtoPrintBillEntity.FieldModifiedUserName))
            {
                ModifiedUserName = BaseBusinessLogic.ConvertToString(dr[ZtoPrintBillEntity.FieldModifiedUserName]);
            }
            if (dr.ContainsColumn(ZtoPrintBillEntity.FieldPaymentType))
            {
                PaymentType = BaseBusinessLogic.ConvertToString(dr[ZtoPrintBillEntity.FieldPaymentType]);
            }
            if (dr.ContainsColumn(ZtoPrintBillEntity.FieldLength))
            {
                Length = BaseBusinessLogic.ConvertToString(dr[ZtoPrintBillEntity.FieldLength]);
            }
            if (dr.ContainsColumn(ZtoPrintBillEntity.FieldSendMan))
            {
                SendMan = BaseBusinessLogic.ConvertToString(dr[ZtoPrintBillEntity.FieldSendMan]);
            }
            if (dr.ContainsColumn(ZtoPrintBillEntity.FieldModifiedOn))
            {
                ModifiedOn = BaseBusinessLogic.ConvertToNullableDateTime(dr[ZtoPrintBillEntity.FieldModifiedOn]);
            }
            if (dr.ContainsColumn(ZtoPrintBillEntity.FieldReceiveAddress))
            {
                ReceiveAddress = BaseBusinessLogic.ConvertToString(dr[ZtoPrintBillEntity.FieldReceiveAddress]);
            }
            if (dr.ContainsColumn(ZtoPrintBillEntity.FieldReceivePostcode))
            {
                ReceivePostcode = BaseBusinessLogic.ConvertToString(dr[ZtoPrintBillEntity.FieldReceivePostcode]);
            }
            if (dr.ContainsColumn(ZtoPrintBillEntity.FieldReceivePhone))
            {
                ReceivePhone = BaseBusinessLogic.ConvertToString(dr[ZtoPrintBillEntity.FieldReceivePhone]);
            }
            if (dr.ContainsColumn(ZtoPrintBillEntity.FieldSendDepartment))
            {
                SendDepartment = BaseBusinessLogic.ConvertToString(dr[ZtoPrintBillEntity.FieldSendDepartment]);
            }
            if (dr.ContainsColumn(ZtoPrintBillEntity.FieldSendDate))
            {
                SendDate = BaseBusinessLogic.ConvertToString(dr[ZtoPrintBillEntity.FieldSendDate]);
            }
            if (dr.ContainsColumn(ZtoPrintBillEntity.FieldSendProvince))
            {
                SendProvince = BaseBusinessLogic.ConvertToString(dr[ZtoPrintBillEntity.FieldSendProvince]);
            }
            if (dr.ContainsColumn(ZtoPrintBillEntity.FieldTranFee))
            {
                TranFee = BaseBusinessLogic.ConvertToString(dr[ZtoPrintBillEntity.FieldTranFee]);
            }
            if (dr.ContainsColumn(ZtoPrintBillEntity.FieldSendDeparture))
            {
                SendDeparture = BaseBusinessLogic.ConvertToString(dr[ZtoPrintBillEntity.FieldSendDeparture]);
            }
            if (dr.ContainsColumn(ZtoPrintBillEntity.FieldSendPhone))
            {
                SendPhone = BaseBusinessLogic.ConvertToString(dr[ZtoPrintBillEntity.FieldSendPhone]);
            }
            if (dr.ContainsColumn(ZtoPrintBillEntity.FieldSendCity))
            {
                SendCity = BaseBusinessLogic.ConvertToString(dr[ZtoPrintBillEntity.FieldSendCity]);
            }
            if (dr.ContainsColumn(ZtoPrintBillEntity.FieldGoodsName))
            {
                GoodsName = BaseBusinessLogic.ConvertToString(dr[ZtoPrintBillEntity.FieldGoodsName]);
            }
            if (dr.ContainsColumn(ZtoPrintBillEntity.FieldWeight))
            {
                Weight = BaseBusinessLogic.ConvertToString(dr[ZtoPrintBillEntity.FieldWeight]);
            }
            if (dr.ContainsColumn(ZtoPrintBillEntity.FieldHeight))
            {
                Height = BaseBusinessLogic.ConvertToString(dr[ZtoPrintBillEntity.FieldHeight]);
            }
            if (dr.ContainsColumn(ZtoPrintBillEntity.FieldReceiveCounty))
            {
                ReceiveCounty = BaseBusinessLogic.ConvertToString(dr[ZtoPrintBillEntity.FieldReceiveCounty]);
            }
            if (dr.ContainsColumn(ZtoPrintBillEntity.FieldReceiveDestination))
            {
                ReceiveDestination = BaseBusinessLogic.ConvertToString(dr[ZtoPrintBillEntity.FieldReceiveDestination]);
            }
            if (dr.ContainsColumn(ZtoPrintBillEntity.FieldSendCompany))
            {
                SendCompany = BaseBusinessLogic.ConvertToString(dr[ZtoPrintBillEntity.FieldSendCompany]);
            }
            if (dr.ContainsColumn(ZtoPrintBillEntity.FieldCreateOn))
            {
                CreateOn = BaseBusinessLogic.ConvertToDateTime(dr[ZtoPrintBillEntity.FieldCreateOn]);
            }
            if (dr.ContainsColumn(ZtoPrintBillEntity.FieldBillCode))
            {
                BillCode = BaseBusinessLogic.ConvertToString(dr[ZtoPrintBillEntity.FieldBillCode]);
            }
            if (dr.ContainsColumn(ZtoPrintBillEntity.FieldTotalNumber))
            {
                TotalNumber = BaseBusinessLogic.ConvertToString(dr[ZtoPrintBillEntity.FieldTotalNumber]);
            }
            if (dr.ContainsColumn(ZtoPrintBillEntity.FieldRemark))
            {
                Remark = BaseBusinessLogic.ConvertToString(dr[ZtoPrintBillEntity.FieldRemark]);
            }
            if (dr.ContainsColumn(ZtoPrintBillEntity.FieldCreateSite))
            {
                CreateSite = BaseBusinessLogic.ConvertToString(dr[ZtoPrintBillEntity.FieldCreateSite]);
            }
            if (dr.ContainsColumn(ZtoPrintBillEntity.FieldSendSite))
            {
                SendSite = BaseBusinessLogic.ConvertToString(dr[ZtoPrintBillEntity.FieldSendSite]);
            }
            if (dr.ContainsColumn(ZtoPrintBillEntity.FieldWidth))
            {
                Width = BaseBusinessLogic.ConvertToString(dr[ZtoPrintBillEntity.FieldWidth]);
            }
            if (dr.ContainsColumn(ZtoPrintBillEntity.FieldReceiveCity))
            {
                ReceiveCity = BaseBusinessLogic.ConvertToString(dr[ZtoPrintBillEntity.FieldReceiveCity]);
            }
            if (dr.ContainsColumn(ZtoPrintBillEntity.FieldSendAddress))
            {
                SendAddress = BaseBusinessLogic.ConvertToString(dr[ZtoPrintBillEntity.FieldSendAddress]);
            }
            if (dr.ContainsColumn(ZtoPrintBillEntity.FieldSendCounty))
            {
                SendCounty = BaseBusinessLogic.ConvertToString(dr[ZtoPrintBillEntity.FieldSendCounty]);
            }
            if (dr.ContainsColumn(ZtoPrintBillEntity.FieldReceiveCompany))
            {
                ReceiveCompany = BaseBusinessLogic.ConvertToString(dr[ZtoPrintBillEntity.FieldReceiveCompany]);
            }
            if (dr.ContainsColumn(ZtoPrintBillEntity.FieldBigPen))
            {
                BigPen = BaseBusinessLogic.ConvertToString(dr[ZtoPrintBillEntity.FieldBigPen]);
            }
            if (dr.ContainsColumn(ZtoPrintBillEntity.FieldOrderNumber))
            {
                OrderNumber = BaseBusinessLogic.ConvertToString(dr[ZtoPrintBillEntity.FieldOrderNumber]);
            }
            if (dr.ContainsColumn(ZtoPrintBillEntity.FieldExpressId))
            {
                ExpressId = BaseBusinessLogic.ConvertToString(dr[ZtoPrintBillEntity.FieldExpressId]);
            }
            if (dr.ContainsColumn(ZtoPrintBillEntity.FieldToPayMent))
            {
                TOPAYMENT = BaseBusinessLogic.ConvertToDecimal(dr[ZtoPrintBillEntity.FieldToPayMent]);
            }
            if (dr.ContainsColumn(ZtoPrintBillEntity.FieldGoodsPayMent))
            {
                GOODS_PAYMENT = BaseBusinessLogic.ConvertToDecimal(dr[ZtoPrintBillEntity.FieldGoodsPayMent]);
            }
            return this;
        }

        /// <summary>
        /// BIG_PEN
        /// </summary>
        public static string FieldBigPen = "BIG_PEN";

        ///<summary>
        /// 
        ///</summary>
        public static string TableName = "ZTO_PRINT_BILL";

        ///<summary>
        /// ID
        ///</summary>
        public static string FieldId = "ID";

        ///<summary>
        /// SEND_POSTCODE
        ///</summary>
        public static string FieldSendPostcode = "SEND_POSTCODE";

        ///<summary>
        /// RECEIVE_MAN
        ///</summary>
        public static string FieldReceiveMan = "RECEIVE_MAN";

        ///<summary>
        /// RECEIVE_PROVINCE
        ///</summary>
        public static string FieldReceiveProvince = "RECEIVE_PROVINCE";

        ///<summary>
        /// CREATEUSERNAME
        ///</summary>
        public static string FieldCreateUserName = "CREATEUSERNAME";

        ///<summary>
        /// MODIFIEDSITE
        ///</summary>
        public static string FieldModifiedSite = "MODIFIEDSITE";

        ///<summary>
        /// MODIFIEDUSERNAME
        ///</summary>
        public static string FieldModifiedUserName = "MODIFIEDUSERNAME";

        ///<summary>
        /// PAYMENT_TYPE
        ///</summary>
        public static string FieldPaymentType = "PAYMENT_TYPE";

        ///<summary>
        /// LENGTH
        ///</summary>
        public static string FieldLength = "LENGTH";

        ///<summary>
        /// SEND_MAN
        ///</summary>
        public static string FieldSendMan = "SEND_MAN";

        ///<summary>
        /// MODIFIEDON
        ///</summary>
        public static string FieldModifiedOn = "MODIFIEDON";

        ///<summary>
        /// RECEIVE_ADDRESS
        ///</summary>
        public static string FieldReceiveAddress = "RECEIVE_ADDRESS";

        ///<summary>
        /// RECEIVE_POSTCODE
        ///</summary>
        public static string FieldReceivePostcode = "RECEIVE_POSTCODE";

        ///<summary>
        /// RECEIVE_PHONE
        ///</summary>
        public static string FieldReceivePhone = "RECEIVE_PHONE";

        ///<summary>
        /// SEND_DEPARTMENT
        ///</summary>
        public static string FieldSendDepartment = "SEND_DEPARTMENT";

        ///<summary>
        /// SEND_DATE
        ///</summary>
        public static string FieldSendDate = "SEND_DATE";

        ///<summary>
        /// SEND_PROVINCE
        ///</summary>
        public static string FieldSendProvince = "SEND_PROVINCE";

        ///<summary>
        /// TRAN_FEE
        ///</summary>
        public static string FieldTranFee = "TRAN_FEE";

        ///<summary>
        /// SEND_DEPARTURE
        ///</summary>
        public static string FieldSendDeparture = "SEND_DEPARTURE";

        ///<summary>
        /// SEND_PHONE
        ///</summary>
        public static string FieldSendPhone = "SEND_PHONE";

        ///<summary>
        /// SEND_CITY
        ///</summary>
        public static string FieldSendCity = "SEND_CITY";

        ///<summary>
        /// GOODS_NAME
        ///</summary>
        public static string FieldGoodsName = "GOODS_NAME";

        ///<summary>
        /// WEIGHT
        ///</summary>
        public static string FieldWeight = "WEIGHT";

        ///<summary>
        /// HEIGHT
        ///</summary>
        public static string FieldHeight = "HEIGHT";

        ///<summary>
        /// RECEIVE_COUNTY
        ///</summary>
        public static string FieldReceiveCounty = "RECEIVE_COUNTY";

        ///<summary>
        /// RECEIVE_DESTINATION
        ///</summary>
        public static string FieldReceiveDestination = "RECEIVE_DESTINATION";

        ///<summary>
        /// SEND_COMPANY
        ///</summary>
        public static string FieldSendCompany = "SEND_COMPANY";

        ///<summary>
        /// CREATEON
        ///</summary>
        public static string FieldCreateOn = "CREATEON";

        ///<summary>
        /// BILL_CODE
        ///</summary>
        public static string FieldBillCode = "BILL_CODE";

        ///<summary>
        /// TOTAL_NUMBER
        ///</summary>
        public static string FieldTotalNumber = "TOTAL_NUMBER";

        ///<summary>
        /// REMARK
        ///</summary>
        public static string FieldRemark = "REMARK";

        ///<summary>
        /// CREATESITE
        ///</summary>
        public static string FieldCreateSite = "CREATESITE";

        ///<summary>
        /// SEND_SITE
        ///</summary>
        public static string FieldSendSite = "SEND_SITE";

        ///<summary>
        /// WIDTH
        ///</summary>
        public static string FieldWidth = "WIDTH";

        ///<summary>
        /// RECEIVE_CITY
        ///</summary>
        public static string FieldReceiveCity = "RECEIVE_CITY";

        ///<summary>
        /// SEND_ADDRESS
        ///</summary>
        public static string FieldSendAddress = "SEND_ADDRESS";

        ///<summary>
        /// SEND_COUNTY
        ///</summary>
        public static string FieldSendCounty = "SEND_COUNTY";

        /// <summary>
        /// 收件人单位
        /// </summary>
        public static string FieldReceiveCompany = "RECEIVE_COMPANY";

        /// <summary>
        /// 订单编号
        /// </summary>
        public static string FieldOrderNumber = "ORDER_NUMBER";

        /// <summary>
        /// 快递ID
        /// </summary>
        public static string FieldExpressId = "EXPRESS_ID";

        /// <summary>
        /// 到付款
        /// </summary>
        public static string FieldToPayMent = "TOPAYMENT";

        /// <summary>
        /// 代收货款
        /// </summary>
        public static string FieldGoodsPayMent = "GOODS_PAYMENT";
    }

}
