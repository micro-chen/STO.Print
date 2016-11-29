//-----------------------------------------------------------------------
// <copyright file="ZtoPrintCancelEntity.cs" company="STO">
//     Copyright (c) 2016 , All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using DotNet.Model;
using DotNet.Utilities;

namespace STO.Print.Model
{
    /// <summary>
    /// ZtoPrintCancelEntity
    /// 
    /// 
    /// 修改纪录
    /// 
    /// 2016-07-05 版本：1.0 YangHengLian 创建文件。
    /// 
    /// <author>
    ///     <name>YangHengLian</name>
    ///     <date>2016-07-05</date>
    /// </author>
    /// </summary>
    public partial class ZtoPrintCancelEntity : BaseEntity
    {
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

        private string orderNumber = string.Empty;
        /// <summary>
        /// ORDER_NUMBER
        /// </summary>
        [FieldDescription("ORDER_NUMBER")]
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

        private string id = string.Empty;
        /// <summary>
        /// ID
        /// </summary>
        [FieldDescription("ID")]
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

        /// <summary>
        /// 从数据行读取
        /// </summary>
        /// <param name="dr">数据行</param>
        protected override BaseEntity GetFrom(IDataRow dr)
        {
            GetFromExpand(dr);
            if (dr.ContainsColumn(ZtoPrintCancelEntity.FieldSendPhone))
            {
                SendPhone = BaseBusinessLogic.ConvertToString(dr[ZtoPrintCancelEntity.FieldSendPhone]);
            }
            if (dr.ContainsColumn(ZtoPrintCancelEntity.FieldReceiveMan))
            {
                ReceiveMan = BaseBusinessLogic.ConvertToString(dr[ZtoPrintCancelEntity.FieldReceiveMan]);
            }
            if (dr.ContainsColumn(ZtoPrintCancelEntity.FieldCreateUserName))
            {
                CreateUserName = BaseBusinessLogic.ConvertToString(dr[ZtoPrintCancelEntity.FieldCreateUserName]);
            }
            if (dr.ContainsColumn(ZtoPrintCancelEntity.FieldModifiedSite))
            {
                ModifiedSite = BaseBusinessLogic.ConvertToString(dr[ZtoPrintCancelEntity.FieldModifiedSite]);
            }
            if (dr.ContainsColumn(ZtoPrintCancelEntity.FieldModifiedUserName))
            {
                ModifiedUserName = BaseBusinessLogic.ConvertToString(dr[ZtoPrintCancelEntity.FieldModifiedUserName]);
            }
            if (dr.ContainsColumn(ZtoPrintCancelEntity.FieldCreateOn))
            {
                CreateOn = BaseBusinessLogic.ConvertToNullableDateTime(dr[ZtoPrintCancelEntity.FieldCreateOn]);
            }
            if (dr.ContainsColumn(ZtoPrintCancelEntity.FieldOrderNumber))
            {
                OrderNumber = BaseBusinessLogic.ConvertToString(dr[ZtoPrintCancelEntity.FieldOrderNumber]);
            }
            if (dr.ContainsColumn(ZtoPrintCancelEntity.FieldBillCode))
            {
                BillCode = BaseBusinessLogic.ConvertToString(dr[ZtoPrintCancelEntity.FieldBillCode]);
            }
            if (dr.ContainsColumn(ZtoPrintCancelEntity.FieldSendMan))
            {
                SendMan = BaseBusinessLogic.ConvertToString(dr[ZtoPrintCancelEntity.FieldSendMan]);
            }
            if (dr.ContainsColumn(ZtoPrintCancelEntity.FieldRemark))
            {
                Remark = BaseBusinessLogic.ConvertToString(dr[ZtoPrintCancelEntity.FieldRemark]);
            }
            if (dr.ContainsColumn(ZtoPrintCancelEntity.FieldModifiedOn))
            {
                ModifiedOn = BaseBusinessLogic.ConvertToNullableDateTime(dr[ZtoPrintCancelEntity.FieldModifiedOn]);
            }
            if (dr.ContainsColumn(ZtoPrintCancelEntity.FieldCreateSite))
            {
                CreateSite = BaseBusinessLogic.ConvertToString(dr[ZtoPrintCancelEntity.FieldCreateSite]);
            }
            if (dr.ContainsColumn(ZtoPrintCancelEntity.FieldReceiveAddress))
            {
                ReceiveAddress = BaseBusinessLogic.ConvertToString(dr[ZtoPrintCancelEntity.FieldReceiveAddress]);
            }
            if (dr.ContainsColumn(ZtoPrintCancelEntity.FieldId))
            {
                Id = BaseBusinessLogic.ConvertToString(dr[ZtoPrintCancelEntity.FieldId]);
            }
            if (dr.ContainsColumn(ZtoPrintCancelEntity.FieldReceivePhone))
            {
                ReceivePhone = BaseBusinessLogic.ConvertToString(dr[ZtoPrintCancelEntity.FieldReceivePhone]);
            }
            if (dr.ContainsColumn(ZtoPrintCancelEntity.FieldSendAddress))
            {
                SendAddress = BaseBusinessLogic.ConvertToString(dr[ZtoPrintCancelEntity.FieldSendAddress]);
            }
            if (dr.ContainsColumn(ZtoPrintCancelEntity.FieldSendProvince))
            {
                SendProvince = BaseBusinessLogic.ConvertToString(dr[ZtoPrintCancelEntity.FieldSendProvince]);
            }
            return this;
        }

        ///<summary>
        /// 
        ///</summary>
        [FieldDescription("")]
        public static string TableName = "ZTO_PRINT_CANCEL";

        ///<summary>
        /// SEND_PHONE
        ///</summary>
        public static string FieldSendPhone = "SEND_PHONE";

        ///<summary>
        /// RECEIVE_MAN
        ///</summary>
        public static string FieldReceiveMan = "RECEIVE_MAN";

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
        /// CREATEON
        ///</summary>
        public static string FieldCreateOn = "CREATEON";

        ///<summary>
        /// ORDER_NUMBER
        ///</summary>
        public static string FieldOrderNumber = "ORDER_NUMBER";

        ///<summary>
        /// BILL_CODE
        ///</summary>
        public static string FieldBillCode = "BILL_CODE";

        ///<summary>
        /// SEND_MAN
        ///</summary>
        public static string FieldSendMan = "SEND_MAN";

        ///<summary>
        /// REMARK
        ///</summary>
        public static string FieldRemark = "REMARK";

        ///<summary>
        /// MODIFIEDON
        ///</summary>
        public static string FieldModifiedOn = "MODIFIEDON";

        ///<summary>
        /// CREATESITE
        ///</summary>
        public static string FieldCreateSite = "CREATESITE";

        ///<summary>
        /// RECEIVE_ADDRESS
        ///</summary>
        public static string FieldReceiveAddress = "RECEIVE_ADDRESS";

        ///<summary>
        /// ID
        ///</summary>
        public static string FieldId = "ID";

        ///<summary>
        /// RECEIVE_PHONE
        ///</summary>
        public static string FieldReceivePhone = "RECEIVE_PHONE";

        ///<summary>
        /// SEND_ADDRESS
        ///</summary>
        public static string FieldSendAddress = "SEND_ADDRESS";

        ///<summary>
        /// SEND_PROVINCE
        ///</summary>
        public static string FieldSendProvince = "SEND_PROVINCE";
    }
}
