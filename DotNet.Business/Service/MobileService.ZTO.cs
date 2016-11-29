//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd.  
//-----------------------------------------------------------------

using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace DotNet.Business
{
    using DotNet.IService;
    using DotNet.Utilities;

    /// <summary>
    /// MobileService
    /// 消息服务
    /// 
    /// 修改记录
    /// 
    ///		2014.03.20 版本：1.0 JiRiGaLa 创建。
    ///		
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2014.03.20</date>
    /// </author> 
    /// </summary>
    public partial class MobileService : IMobileService
    {
        public class PhoneLitEntity
        {
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

        /// <summary>
        /// 发送手机消息
        /// </summary>
        /// <param name="userInfo">用户</param>
        /// <param name="billCode">运单编号</param>
        /// <param name="message">内容</param>
        /// <returns>主键</returns>
        public int SendBillMessage(BaseUserInfo userInfo, string billCode, string message, string channel = "3")
        {
            int result = 0;
            string returnMsg = string.Empty;
            string cellPhone = string.Empty;
            string user = string.Empty;

            string request = string.Empty;
            request = "Function=GetPhoneByBill&billcode=" + billCode;
            string key = DotNet.Utilities.SecretUtil.BuildSecurityRequest(request);
            string url = "http://122.225.108.5:9876/getphonebybill.ashx?Key=" + key;
            request = DotNet.Business.Utilities.GetResponse(url);
            if (!string.IsNullOrEmpty(request))
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                List<PhoneLitEntity> entites = javaScriptSerializer.Deserialize<List<PhoneLitEntity>>(request);
                if (entites != null && entites.Count > 0)
                {
                    cellPhone = entites[0].SEND_MOBILE;
                    user = entites[0].SEND_NAME;
                    if (!string.IsNullOrEmpty(cellPhone))
                    {
                        message = message.Replace("{User}", user).Replace("{BillCode}", billCode);
                        if (SendMobile(userInfo, "Base", userInfo.Id, cellPhone, message, string.Empty, true, false, channel,out returnMsg) > 0)
                        {
                            // 发送成功
                            result = 1;
                        }
                        else
                        {
                            // 发送失败
                            result = 0;
                        }
                    }
                    else
                    {
                        // 表示没有填写联系人手机
                        result = -1;
                    }
                }
            }
            else
            {
                // 表示没有联系人信息
                result = -2;
            }

            return result;
        }

        /// <summary>
        /// 获取手机号码
        /// </summary>
        /// <param name="userInfo">用户</param>
        /// <param name="billCode">运单编号</param>
        /// <returns>手机号码</returns>
        public string GetMobileByBill(BaseUserInfo userInfo, string billCode)
        {
            string result = string.Empty;

            try
            {
                string request = string.Empty;
                request = "Function=GetPhoneByBill&billcode=" + billCode;
                string key = DotNet.Utilities.SecretUtil.BuildSecurityRequest(request);
                string url = "http://122.225.108.5:9876/getphonebybill.ashx?Key=" + key;
                request = DotNet.Business.Utilities.GetResponse(url);
                if (!string.IsNullOrEmpty(request))
                {
                    JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                    List<PhoneLitEntity> entites = javaScriptSerializer.Deserialize<List<PhoneLitEntity>>(request);
                    if (entites != null && entites.Count > 0)
                    {
                        result = entites[0].SEND_MOBILE;
                    }
                }
            }
            catch
            {

            }

            return result;
        }
    }
}