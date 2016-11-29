//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd.  
//-----------------------------------------------------------------

using System.Collections.Specialized;
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace DotNet.Business
{
    using DotNet.Utilities;

    /// <summary>
    /// LogOnUtilities 
    /// 登录服务，远程调用接口
    ///
    /// 修改记录
    /// 
    ///		2016.03.14 版本：1.0 JiRiGaLa  远程调用服务。
    ///
    /// </summary>
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2016.03.14</date>
    /// </author>
    /// </summary>
    public class LogOnUtilities
    {
        /// <summary>
        /// 验证 OpenId 是否正确
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        /// <param name="systemCode">系统编号</param>
        /// <param name="userId">用户主键</param>
        /// <param name="openId">OpenId</param>
        /// <returns>在角色里</returns>
        public static bool ValidateOpenId(BaseUserInfo userInfo, string systemCode, string userId, string openId)
        {
            bool result = false;

            string url = BaseSystemInfo.UserCenterHost + "/UserCenterV42/LogOnService.ashx";
            WebClient webClient = new WebClient();
            NameValueCollection postValues = new NameValueCollection();
            postValues.Add("system", BaseSystemInfo.SoftFullName);
            postValues.Add("systemCode", systemCode);
            postValues.Add("securityKey", BaseSystemInfo.SecurityKey);
            postValues.Add("function", "ValidateOpenId");
            if (userInfo != null)
            {
                postValues.Add("userInfo", userInfo.Serialize());
            }
            postValues.Add("userId", userId);
            postValues.Add("openId", openId);
            // 向服务器发送POST数据
            byte[] responseArray = webClient.UploadValues(url, postValues);
            string response = Encoding.UTF8.GetString(responseArray);
            if (!string.IsNullOrEmpty(response))
            {
                result = response.Equals(true.ToString());
            }

            return result;
        }

        /// <summary>
        /// 获取 OpenId
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        /// <param name="cachingSystemCode">系统编号</param>
        /// <returns>OpenId</returns>
        public static string GetUserOpenId(BaseUserInfo userInfo, string cachingSystemCode = null)
        {
            string result = string.Empty;

            string url = BaseSystemInfo.UserCenterHost + "/UserCenterV42/LogOnService.ashx";
            WebClient webClient = new WebClient();
            NameValueCollection postValues = new NameValueCollection();
            postValues.Add("system", BaseSystemInfo.SoftFullName);
            postValues.Add("cachingSystemCode", cachingSystemCode);
            postValues.Add("securityKey", BaseSystemInfo.SecurityKey);
            postValues.Add("function", "GetUserOpenId");
            if (userInfo != null)
            {
                postValues.Add("userInfo", userInfo.Serialize());
            }
            // 向服务器发送POST数据
            byte[] responseArray = webClient.UploadValues(url, postValues);
            string response = Encoding.UTF8.GetString(responseArray);

            BaseResult baseResult = new BaseResult();
            if (!string.IsNullOrEmpty(response))
            {
                baseResult = JsonConvert.DeserializeObject<BaseResult>(response);
                result = baseResult.StatusCode; 
            }

            return result;
        }
    }
}
