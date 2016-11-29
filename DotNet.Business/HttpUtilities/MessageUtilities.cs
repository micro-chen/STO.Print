//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd.  
//-----------------------------------------------------------------

using System;
using System.Collections.Specialized;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;

namespace DotNet.Business
{
    using DotNet.Utilities;

    /// <summary>
    /// MessageUtilities 
    /// 消息服务，远程调用接口
    ///
    /// 修改记录
    ///
    ///		2016.01.13 版本：1.0 JiRiGaLa  远程调用服务。
    ///
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2016.01.13</date>
    /// </author>
    /// </summary>
    public class MessageUtilities
    {
        /// <summary>
        /// 发送系统提示消息
        /// </summary>
        /// <param name="userInfo">用户</param>
        /// <param name="systemCode">系统编号</param>
        /// <param name="userId">接收者主键</param>
        /// <param name="contents">内容</param>
        /// <returns>主键</returns>
        public static BaseResult Remind(BaseUserInfo userInfo, string systemCode, string userId, string contents)
        {
            BaseResult result = new BaseResult();

            string url = BaseSystemInfo.UserCenterHost + "/UserCenterV42/MessageService.ashx";
            WebClient webClient = new WebClient();
            NameValueCollection postValues = new NameValueCollection();
            postValues.Add("system", BaseSystemInfo.SoftFullName);
            postValues.Add("systemCode", systemCode);
            postValues.Add("securityKey", BaseSystemInfo.SecurityKey);
            // 2015-11-25 吉日嘎拉，这个要看看，有没有必要设置缓存？
            postValues.Add("function", "Remind");
            postValues.Add("userInfo", userInfo.Serialize());
            postValues.Add("encrypted", false.ToString());
            postValues.Add("userId", userId);
            postValues.Add("contents", contents);
            // 向服务器发送POST数据
            byte[] responseArray = webClient.UploadValues(url, postValues);
            string response = Encoding.UTF8.GetString(responseArray);

            if (!string.IsNullOrEmpty(response))
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                result = javaScriptSerializer.Deserialize<BaseResult>(response);
            }

            return result;
        }

        /// <summary>
        /// 调用消息广播接口
        /// </summary>
        /// <returns></returns>
        public static BaseResult Broadcast(BaseUserInfo userInfo, string systemCode, bool allcompany, string[] roleIds
                        , string[] areaIds, string[] companyIds, bool subCompany
                        , string[] departmentIds,
                        bool subDepartment, string[] userIds, string message, bool onlineOnly, MessageFunction functionCode = MessageFunction.Remind, DateTime? expireAt = null)
        {

            BaseResult result = null;

            string url = BaseSystemInfo.UserCenterHost + "/UserCenterV42/MessageService.ashx";
            WebClient webClient = new WebClient();
            NameValueCollection postValues = new NameValueCollection();
            postValues.Add("system", BaseSystemInfo.SoftFullName);
            postValues.Add("systemCode", systemCode);
            postValues.Add("securityKey", BaseSystemInfo.SecurityKey);
            postValues.Add("function", "Broadcast");
            postValues.Add("userInfo", userInfo.Serialize());
            postValues.Add("allcompany", allcompany.ToString());
            if (roleIds != null)
            {
                postValues.Add("roleIds", string.Join(",", roleIds));
            }
            if (areaIds != null)
            {
                postValues.Add("areaIds", string.Join(",", areaIds));
            }
            if (companyIds != null)
            {
                postValues.Add("companyIds", string.Join(",", companyIds));
            }
            postValues.Add("subCompany", subCompany.ToString());
            if (departmentIds != null)
            {
                postValues.Add("departmentIds", string.Join(",", departmentIds));
            }
            postValues.Add("subDepartment", subDepartment.ToString());
            if (userIds != null)
            {
                postValues.Add("userIds", string.Join(",", userIds));
            }
            postValues.Add("message", HttpUtility.HtmlEncode(message));
            postValues.Add("onlineOnly", onlineOnly.ToString());
            // 2016-04-06 吉日嘎拉 提高弹出消息的位置
            postValues.Add("functionCode", functionCode.ToString());
            if (expireAt.HasValue)
            {
                postValues.Add("expireAt", expireAt.Value.ToString(BaseSystemInfo.DateTimeFormat));
            }

            // 向服务器发送POST数据
            byte[] responseArray = webClient.UploadValues(url, postValues);
            string response = Encoding.UTF8.GetString(responseArray);
            if (!string.IsNullOrEmpty(response))
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                result = javaScriptSerializer.Deserialize<BaseResult>(response);
            }

            return result;
        }


        public static BaseResult GetUserByOrganize(BaseUserInfo userInfo, string companyId, string departmentId)
        {
            BaseResult result = new BaseResult();

            string url = BaseSystemInfo.UserCenterHost + "/UserCenterV42/MessageService.ashx";
            WebClient webClient = new WebClient();
            NameValueCollection postValues = new NameValueCollection();
            postValues.Add("system", BaseSystemInfo.SoftFullName);
            postValues.Add("securityKey", BaseSystemInfo.SecurityKey);
            // 2015-11-25 吉日嘎拉，这个要看看，有没有必要设置缓存？
            postValues.Add("userInfo", userInfo.Serialize());
            postValues.Add("function", "GetUserByOrganize");
            postValues.Add("companyId", companyId);
            postValues.Add("departmentId", departmentId);
            // 向服务器发送POST数据
            byte[] responseArray = webClient.UploadValues(url, postValues);
            string response = Encoding.UTF8.GetString(responseArray);

            if (!string.IsNullOrEmpty(response))
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                result = javaScriptSerializer.Deserialize<BaseResult>(response);
            }

            return result;
        }
    }
}
