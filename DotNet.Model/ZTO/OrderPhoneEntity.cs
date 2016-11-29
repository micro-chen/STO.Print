//--------------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd.  
//--------------------------------------------------------------------

using System;

namespace DotNet.Model
{
    public class OrderPhoneEntity
    {
        private bool mobileEncrypted = false;
        /// <summary>
        /// 手机号码是否被加密
        /// </summary>
        public bool MobileEncrypted
        {
            get
            {
                return this.mobileEncrypted;
            }
            set
            {
                this.mobileEncrypted = value;
            }
        }

        private string bill_code = null;
        /// <summary>
        /// 单号
        /// </summary>
        public string BILL_CODE
        {
            get
            {
                return this.bill_code;
            }
            set
            {
                this.bill_code = value;
            }
        }

        private string send_name = null;
        /// <summary>
        /// 收件人
        /// </summary>
        public string SEND_NAME
        {
            get
            {
                return this.send_name;
            }
            set
            {
                this.send_name = value;
            }
        }

        private string send_mobile = null;
        /// <summary>
        /// 收件人手机
        /// </summary>
        public string SEND_MOBILE
        {
            get
            {
                return this.send_mobile;
            }
            set
            {
                this.send_mobile = value;
            }
        }

        private string name = null;
        /// <summary>
        /// 发件人
        /// </summary>
        public string NAME
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
            }
        }

        private string phone = null;
        /// <summary>
        /// 发件人手机
        /// </summary>
        public string PHONE
        {
            get
            {
                return this.phone;
            }
            set
            {
                this.phone = value;
            }
        }
    }
}