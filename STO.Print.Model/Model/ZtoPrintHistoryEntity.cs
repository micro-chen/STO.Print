//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2015 , STO TECH, Ltd. 
//-----------------------------------------------------------------

using System;

namespace STO.Print.Model
{
    using DotNet.Model;
    using DotNet.Utilities;

    /// <summary>
    /// ZtoPrintHistoryEntity
    /// 打印历史记录实体
    /// 
    /// 修改纪录
    /// 
    /// 2015-09-12 版本：1.0 YangHengLian 创建文件。
    /// 
    /// <author>
    ///     <name>YangHengLian</name>
    ///     <date>2015-09-12</date>
    /// </author>
    /// </summary>
    public partial class ZtoPrintHistoryEntity : BaseEntity
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

        private DateTime? expressType = null;
        /// <summary>
        /// EXPRESS_TYPE
        /// </summary>
        [FieldDescription("EXPRESS_TYPE")]
        public DateTime? ExpressType
        {
            get
            {
                return expressType;
            }
            set
            {
                expressType = value;
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

        /// <summary>
        /// 从数据行读取
        /// </summary>
        /// <param name="dr">数据行</param>
        protected override BaseEntity GetFrom(IDataRow dr)
        {
            GetFromExpand(dr);
            if (dr.ContainsColumn(ZtoPrintHistoryEntity.FieldId))
            {
                Id = BaseBusinessLogic.ConvertToDecimal(dr[ZtoPrintHistoryEntity.FieldId]);
            }
            if (dr.ContainsColumn(ZtoPrintHistoryEntity.FieldReceiveCompany))
            {
                ReceiveCompany = BaseBusinessLogic.ConvertToString(dr[ZtoPrintHistoryEntity.FieldReceiveCompany]);
            }
            if (dr.ContainsColumn(ZtoPrintHistoryEntity.FieldReceiveMan))
            {
                ReceiveMan = BaseBusinessLogic.ConvertToString(dr[ZtoPrintHistoryEntity.FieldReceiveMan]);
            }
            if (dr.ContainsColumn(ZtoPrintHistoryEntity.FieldExpressType))
            {
                ExpressType = BaseBusinessLogic.ConvertToNullableDateTime(dr[ZtoPrintHistoryEntity.FieldExpressType]);
            }
            if (dr.ContainsColumn(ZtoPrintHistoryEntity.FieldCreateOn))
            {
                CreateOn = BaseBusinessLogic.ConvertToNullableDateTime(dr[ZtoPrintHistoryEntity.FieldCreateOn]);
            }
            if (dr.ContainsColumn(ZtoPrintHistoryEntity.FieldBillCode))
            {
                BillCode = BaseBusinessLogic.ConvertToString(dr[ZtoPrintHistoryEntity.FieldBillCode]);
            }
            return this;
        }

        ///<summary>
        /// 
        ///</summary>
        public static string TableName = "ZTO_PRINT_HISTORY";

        ///<summary>
        /// ID
        ///</summary>
        public static string FieldId = "ID";

        ///<summary>
        /// RECEIVE_COMPANY
        ///</summary>
        public static string FieldReceiveCompany = "RECEIVE_COMPANY";

        ///<summary>
        /// RECEIVE_MAN
        ///</summary>
        public static string FieldReceiveMan = "RECEIVE_MAN";

        ///<summary>
        /// EXPRESS_TYPE
        ///</summary>
        public static string FieldExpressType = "EXPRESS_TYPE";

        ///<summary>
        /// CREATEON
        ///</summary>
        public static string FieldCreateOn = "CREATEON";

        ///<summary>
        /// BILL_CODE
        ///</summary>
        public static string FieldBillCode = "BILL_CODE";
    }
}
