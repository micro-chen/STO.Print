//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd.  
//-----------------------------------------------------------------

using System;
using System.Collections.Specialized;
using System.Net;

namespace DotNet.Business
{
    using DotNet.Model;
    using DotNet.Utilities;

    /// <summary>
    /// LogUtilities 
    /// 日志服务，远程调用接口
    ///
    /// 修改记录
    /// 
    ///		2016.02.16 版本：1.0 JiRiGaLa  远程调用服务。
    ///
    /// </summary>
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2016.02.16</date>
    /// </author>
    /// </summary>
    public class LogUtilities
    {
        public static void AddLog(BaseUserInfo userInfo, BaseLogEntity entity)
        {
            // 2016-02-17 吉日嘎拉 是否允许记录日志的判断
            if (!BaseSystemInfo.RecordLogOnLog)
            {
                return;
            }

            // string url = BaseSystemInfo.UserCenterHost + "/UserCenterV42/LogService.ashx";
            string url = "http://139.196.91.4/UserCenterV42/LogService.ashx";
            WebClient webClient = new WebClient();
            NameValueCollection postValues = new NameValueCollection();
            postValues.Add("system", BaseSystemInfo.SoftFullName);
            postValues.Add("systemCode", BaseSystemInfo.SystemCode);
            postValues.Add("securityKey", BaseSystemInfo.SecurityKey);
            // 2015-11-25 吉日嘎拉，这里还是从缓存里获取就可以了，提高登录的效率。
            postValues.Add("function", "AddLog");
            if (userInfo != null)
            {
                postValues.Add("userInfo", userInfo.Serialize());
            }
            postValues.Add("service", entity.Service);
            postValues.Add("startTime", entity.StartTime.ToString(BaseSystemInfo.DateTimeFormat));
            postValues.Add("TaskId", entity.TaskId);
            postValues.Add("ClientIP", entity.ClientIP);
            postValues.Add("ElapsedTicks", entity.ElapsedTicks.ToString());
            postValues.Add("UserId", entity.UserId);
            postValues.Add("CompanyId", entity.CompanyId);
            postValues.Add("UserRealName", entity.UserRealName);
            postValues.Add("WebUrl", entity.WebUrl);
            // 向服务器发送POST数据、异步提交日志服务器
            Uri address = new Uri(url);
            webClient.UploadValuesAsync(address, postValues);
            
            /*
            BaseResult result = null;
            byte[] responseArray = webClient.UploadValues(url, postValues);
            string response = Encoding.UTF8.GetString(responseArray);
            if (!string.IsNullOrEmpty(response))
            {
                result = JsonConvert.DeserializeObject<BaseResult>(response);
            }
            */
        }
    }
}
