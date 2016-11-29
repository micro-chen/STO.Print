//-----------------------------------------------------------------------
// <copyright file="TAB_EMPLOYEEEntity.cs" company="Hairihan">
//     Copyright (c) 2013 , All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;

namespace DotNet.Model
{
    using DotNet.Utilities;
 
    /// <summary>
    /// TAB_EMPLOYEEEntity
    /// 员工
    /// 
    /// 修改纪录
    /// 
    /// 2013-11-23 版本：1.0 songliangliang 创建文件。
    /// 
    /// <author>
    ///     <name>songliangliang</name>
    ///     <date>2013-11-23</date>
    /// </author>
    /// </summary>
    public partial class TAB_EMPLOYEEEntity : BaseEntity
    {
        private Decimal id;
        /// <summary>
        /// 主键
        /// </summary>
        public Decimal ID
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

        private String owner_site = string.Empty;
        /// <summary>
        /// 所属网点
        /// </summary>
        public String OWNER_SITE
        {
            get
            {
                return owner_site;
            }
            set
            {
                owner_site = value;
            }
        }

        private Decimal transfer_add_fee;
        /// <summary>
        /// 中转附加费
        /// </summary>
        public Decimal TRANSFER_ADD_FEE
        {
            get
            {
                return transfer_add_fee;
            }
            set
            {
                transfer_add_fee = value;
            }
        }

        private String employee_code = string.Empty;
        /// <summary>
        /// 员工编号
        /// </summary>
        public String EMPLOYEE_CODE
        {
            get
            {
                return employee_code;
            }
            set
            {
                employee_code = value;
            }
        }

        private String owner_range = string.Empty;
        /// <summary>
        /// 所属承包区
        /// </summary>
        public String OWNER_RANGE
        {
            get
            {
                return owner_range;
            }
            set
            {
                owner_range = value;
            }
        }

        private String cardnum = string.Empty;
        /// <summary>
        /// 身份证号码
        /// </summary>
        public String CARDNUM
        {
            get
            {
                return cardnum;
            }
            set
            {
                cardnum = value;
            }
        }

        private Decimal bl_dispatch_gain;
        /// <summary>
        /// 派件提成
        /// </summary>
        public Decimal BL_DISPATCH_GAIN
        {
            get
            {
                return bl_dispatch_gain;
            }
            set
            {
                bl_dispatch_gain = value;
            }
        }

        private Decimal bl_send_gain;
        /// <summary>
        /// 寄件提成
        /// </summary>
        public Decimal BL_SEND_GAIN
        {
            get
            {
                return bl_send_gain;
            }
            set
            {
                bl_send_gain = value;
            }
        }

        private String id_card = string.Empty;
        /// <summary>
        /// 身份证号
        /// </summary>
        public String ID_CARD
        {
            get
            {
                return id_card;
            }
            set
            {
                id_card = value;
            }
        }

        private DateTime updatetime;
        /// <summary>
        /// 最后更新时间
        /// </summary>
        public DateTime UPDATETIME
        {
            get
            {
                return updatetime;
            }
            set
            {
                updatetime = value;
            }
        }

        private String bar_password = string.Empty;
        /// <summary>
        /// 巴枪操作密码
        /// </summary>
        public String BAR_PASSWORD
        {
            get
            {
                return bar_password;
            }
            set
            {
                bar_password = value;
            }
        }

        private String employee_type = string.Empty;
        /// <summary>
        /// 仓库操作员 、 取派员、双重身份
        /// </summary>
        public String EMPLOYEE_TYPE
        {
            get
            {
                return employee_type;
            }
            set
            {
                employee_type = value;
            }
        }

        private Decimal dispatch__add_fee;
        /// <summary>
        /// 派件附加费
        /// </summary>
        public Decimal DISPATCH__ADD_FEE
        {
            get
            {
                return dispatch__add_fee;
            }
            set
            {
                dispatch__add_fee = value;
            }
        }

        private String dept_name = string.Empty;
        /// <summary>
        /// 部门名称
        /// </summary>
        public String DEPT_NAME
        {
            get
            {
                return dept_name;
            }
            set
            {
                dept_name = value;
            }
        }

        private String address = string.Empty;
        /// <summary>
        /// 住址
        /// </summary>
        public String ADDRESS
        {
            get
            {
                return address;
            }
            set
            {
                address = value;
            }
        }

        private String phone = string.Empty;
        /// <summary>
        /// 联系电话
        /// </summary>
        public String PHONE
        {
            get
            {
                return phone;
            }
            set
            {
                phone = value;
            }
        }

        private String employee_name = string.Empty;
        /// <summary>
        /// 员工名称
        /// </summary>
        public String EMPLOYEE_NAME
        {
            get
            {
                return employee_name;
            }
            set
            {
                employee_name = value;
            }
        }

        private Decimal deletionstatecode;
        /// <summary>
        /// DELETIONSTATECODE
        /// </summary>
        public Decimal DELETIONSTATECODE
        {
            get
            {
                return deletionstatecode;
            }
            set
            {
                deletionstatecode = value;
            }
        }

        private String group_name = string.Empty;
        /// <summary>
        /// 组名
        /// </summary>
        public String GROUP_NAME
        {
            get
            {
                return group_name;
            }
            set
            {
                group_name = value;
            }
        }

        private String dispatch_add_fee_opera = string.Empty;
        /// <summary>
        /// 派件附加费操作符
        /// </summary>
        public String DISPATCH_ADD_FEE_OPERA
        {
            get
            {
                return dispatch_add_fee_opera;
            }
            set
            {
                dispatch_add_fee_opera = value;
            }
        }

        private String transfer_add_fee_opera = string.Empty;
        /// <summary>
        /// 中转附加费操作符
        /// </summary>
        public String TRANSFER_ADD_FEE_OPERA
        {
            get
            {
                return transfer_add_fee_opera;
            }
            set
            {
                transfer_add_fee_opera = value;
            }
        }

        /// <summary>
        /// 从数据行读取
        /// </summary>
        /// <param name="dr">数据行</param>
        protected override BaseEntity GetFrom(IDataRow dr)
        {
            GetFromExpand(dr);
            ID = BaseBusinessLogic.ConvertToDecimal(dr[TAB_EMPLOYEEEntity.Field_ID]);
            OWNER_SITE = BaseBusinessLogic.ConvertToString(dr[TAB_EMPLOYEEEntity.Field_OWNER_SITE]);
            TRANSFER_ADD_FEE = BaseBusinessLogic.ConvertToDecimal(dr[TAB_EMPLOYEEEntity.Field_TRANSFER_ADD_FEE]);
            EMPLOYEE_CODE = BaseBusinessLogic.ConvertToString(dr[TAB_EMPLOYEEEntity.Field_EMPLOYEE_CODE]);
            OWNER_RANGE = BaseBusinessLogic.ConvertToString(dr[TAB_EMPLOYEEEntity.Field_OWNER_RANGE]);
            CARDNUM = BaseBusinessLogic.ConvertToString(dr[TAB_EMPLOYEEEntity.Field_CARDNUM]);
            BL_DISPATCH_GAIN = BaseBusinessLogic.ConvertToDecimal(dr[TAB_EMPLOYEEEntity.Field_BL_DISPATCH_GAIN]);
            BL_SEND_GAIN = BaseBusinessLogic.ConvertToDecimal(dr[TAB_EMPLOYEEEntity.Field_BL_SEND_GAIN]);
            ID_CARD = BaseBusinessLogic.ConvertToString(dr[TAB_EMPLOYEEEntity.Field_ID_CARD]);
            UPDATETIME = BaseBusinessLogic.ConvertToDateTime(dr[TAB_EMPLOYEEEntity.Field_UPDATETIME]);
            BAR_PASSWORD = BaseBusinessLogic.ConvertToString(dr[TAB_EMPLOYEEEntity.Field_BAR_PASSWORD]);
            EMPLOYEE_TYPE = BaseBusinessLogic.ConvertToString(dr[TAB_EMPLOYEEEntity.Field_EMPLOYEE_TYPE]);
            DISPATCH__ADD_FEE = BaseBusinessLogic.ConvertToDecimal(dr[TAB_EMPLOYEEEntity.Field_DISPATCH__ADD_FEE]);
            DEPT_NAME = BaseBusinessLogic.ConvertToString(dr[TAB_EMPLOYEEEntity.Field_DEPT_NAME]);
            ADDRESS = BaseBusinessLogic.ConvertToString(dr[TAB_EMPLOYEEEntity.Field_ADDRESS]);
            PHONE = BaseBusinessLogic.ConvertToString(dr[TAB_EMPLOYEEEntity.Field_PHONE]);
            EMPLOYEE_NAME = BaseBusinessLogic.ConvertToString(dr[TAB_EMPLOYEEEntity.Field_EMPLOYEE_NAME]);
            DELETIONSTATECODE = BaseBusinessLogic.ConvertToDecimal(dr[TAB_EMPLOYEEEntity.Field_DELETIONSTATECODE]);
            GROUP_NAME = BaseBusinessLogic.ConvertToString(dr[TAB_EMPLOYEEEntity.Field_GROUP_NAME]);
            DISPATCH_ADD_FEE_OPERA = BaseBusinessLogic.ConvertToString(dr[TAB_EMPLOYEEEntity.Field_DISPATCH_ADD_FEE_OPERA]);
            TRANSFER_ADD_FEE_OPERA = BaseBusinessLogic.ConvertToString(dr[TAB_EMPLOYEEEntity.Field_TRANSFER_ADD_FEE_OPERA]);
            return this;
        }

        ///<summary>
        /// 员工
        ///</summary>
        public static string TableName = "TAB_EMPLOYEE";

        ///<summary>
        /// 主键
        ///</summary>
        public static string Field_ID = "ID";

        ///<summary>
        /// 所属网点
        ///</summary>
        public static string Field_OWNER_SITE = "OWNER_SITE";

        ///<summary>
        /// 中转附加费
        ///</summary>
        public static string Field_TRANSFER_ADD_FEE = "TRANSFER_ADD_FEE";

        ///<summary>
        /// 员工编号
        ///</summary>
        public static string Field_EMPLOYEE_CODE = "EMPLOYEE_CODE";

        ///<summary>
        /// 所属承包区
        ///</summary>
        public static string Field_OWNER_RANGE = "OWNER_RANGE";

        ///<summary>
        /// 身份证号码
        ///</summary>
        public static string Field_CARDNUM = "CARDNUM";

        ///<summary>
        /// 派件提成
        ///</summary>
        public static string Field_BL_DISPATCH_GAIN = "BL_DISPATCH_GAIN";

        ///<summary>
        /// 寄件提成
        ///</summary>
        public static string Field_BL_SEND_GAIN = "BL_SEND_GAIN";

        ///<summary>
        /// 身份证号
        ///</summary>
        public static string Field_ID_CARD = "ID_CARD";

        ///<summary>
        /// 最后更新时间
        ///</summary>
        public static string Field_UPDATETIME = "UPDATETIME";

        ///<summary>
        /// 巴枪操作密码
        ///</summary>
        public static string Field_BAR_PASSWORD = "BAR_PASSWORD";

        ///<summary>
        /// 仓库操作员 、 取派员、双重身份
        ///</summary>
        public static string Field_EMPLOYEE_TYPE = "EMPLOYEE_TYPE";

        ///<summary>
        /// 派件附加费
        ///</summary>
        public static string Field_DISPATCH__ADD_FEE = "DISPATCH__ADD_FEE";

        ///<summary>
        /// 部门名称
        ///</summary>
        public static string Field_DEPT_NAME = "DEPT_NAME";

        ///<summary>
        /// 住址
        ///</summary>
        public static string Field_ADDRESS = "ADDRESS";

        ///<summary>
        /// 联系电话
        ///</summary>
        public static string Field_PHONE = "PHONE";

        ///<summary>
        /// 员工名称
        ///</summary>
        public static string Field_EMPLOYEE_NAME = "EMPLOYEE_NAME";

        ///<summary>
        /// DELETIONSTATECODE
        ///</summary>
        public static string Field_DELETIONSTATECODE = "DELETIONSTATECODE";

        ///<summary>
        /// 组名
        ///</summary>
        public static string Field_GROUP_NAME = "GROUP_NAME";

        ///<summary>
        /// 派件附加费操作符
        ///</summary>
        public static string Field_DISPATCH_ADD_FEE_OPERA = "DISPATCH_ADD_FEE_OPERA";

        ///<summary>
        /// 中转附加费操作符
        ///</summary>
        public static string Field_TRANSFER_ADD_FEE_OPERA = "TRANSFER_ADD_FEE_OPERA";
    }
}
