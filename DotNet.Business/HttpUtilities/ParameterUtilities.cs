//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd.  
//-----------------------------------------------------------------

using System.Collections.Specialized;
using System.Net;
using System.Text;

namespace DotNet.Business
{
    using DotNet.Utilities;

    /// <summary>
    /// ParameterUtilities 
    /// 参数服务，远程调用接口
    ///
    /// 修改记录
    ///
    ///		2015.07.29 版本：1.0 JiRiGaLa  远程调用服务。
    ///
    /// </summary>
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2015.07.29</date>
    /// </author>
    /// </summary>
    public class ParameterUtilities
    {
        public static void SetParameter(BaseUserInfo userInfo, string tableName, string categoryCode, string parameterId, string parameterCode, string parameterContent)
        {
            string url = BaseSystemInfo.UserCenterHost + "/UserCenterV42/ParameterService.ashx";
            WebClient webClient = new WebClient();
            NameValueCollection postValues = new NameValueCollection();
            postValues.Add("system", BaseSystemInfo.SoftFullName);
            postValues.Add("systemCode", BaseSystemInfo.SystemCode);
            postValues.Add("securityKey", BaseSystemInfo.SecurityKey);
            postValues.Add("function", "SetParameter");
            postValues.Add("userInfo", userInfo.Serialize());
            postValues.Add("tableName", tableName);
            postValues.Add("categoryCode", categoryCode);
            postValues.Add("parameterId", parameterId);
            postValues.Add("parameterCode", parameterCode);
            postValues.Add("parameterContent", parameterContent);
            // 向服务器发送POST数据
            webClient.UploadValues(url, postValues);
        }

        public static string GetParameter(BaseUserInfo userInfo, string tableName, string categoryCode, string parameterId, string parameterCode)
        {
            string result = string.Empty;

            string url = BaseSystemInfo.UserCenterHost + "/UserCenterV42/ParameterService.ashx";
            WebClient webClient = new WebClient();
            NameValueCollection postValues = new NameValueCollection();
            postValues.Add("system", BaseSystemInfo.SoftFullName);
            postValues.Add("systemCode", BaseSystemInfo.SystemCode);
            postValues.Add("securityKey", BaseSystemInfo.SecurityKey);
            // 2015-11-25 吉日嘎拉，这里还是从缓存里获取就可以了，提高登录的效率。
            postValues.Add("function", "GetParameter");
            // postValues.Add("function", "GetParameterByCache");
            postValues.Add("userInfo", userInfo.Serialize());
            postValues.Add("tableName", tableName);
            postValues.Add("categoryCode", categoryCode);
            postValues.Add("parameterId", parameterId);
            postValues.Add("parameterCode", parameterCode);
            // 向服务器发送POST数据
            byte[] responseArray = webClient.UploadValues(url, postValues);
            result = Encoding.UTF8.GetString(responseArray);

            return result;
        }

        public static string GetParameterByCache(BaseUserInfo userInfo, string tableName, string categoryCode, string parameterId, string parameterCode)
        {
            string result = string.Empty;

            string url = BaseSystemInfo.UserCenterHost + "/UserCenterV42/ParameterService.ashx";
            WebClient webClient = new WebClient();
            NameValueCollection postValues = new NameValueCollection();
            postValues.Add("system", BaseSystemInfo.SoftFullName);
            postValues.Add("systemCode", BaseSystemInfo.SystemCode);
            postValues.Add("securityKey", BaseSystemInfo.SecurityKey);
            postValues.Add("function", "GetParameterByCache");
            postValues.Add("userInfo", userInfo.Serialize());
            postValues.Add("tableName", tableName);
            postValues.Add("categoryCode", categoryCode);
            postValues.Add("parameterId", parameterId);
            postValues.Add("parameterCode", parameterCode);
            // 向服务器发送POST数据
            byte[] responseArray = webClient.UploadValues(url, postValues);
            result = Encoding.UTF8.GetString(responseArray);

            return result;
        }
    }
}
