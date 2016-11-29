//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2015 , Hairihan TECH, Ltd .
//-----------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Text;
using System.Web;
using ServiceStack.Redis;

namespace DotNet.Business
{
    using DotNet.Model;
    using DotNet.Utilities;

    /// <summary>
    /// 登录功能相关部分
    /// </summary>
    public partial class Utilities
    {
        public static int Port = 88;
        public static string Url = "redis.ztosys.com:6379";

        public static bool ValidateOpenId(string openId)
        {
            bool result = false;
            if (!string.IsNullOrWhiteSpace(openId))
            {
                using (var redis = new PooledRedisClientManager(Port, new string[] { Url }).GetClient())
                {
                    string userId = redis.Get<string>("openId" + openId);
                    if (!string.IsNullOrWhiteSpace(userId))
                    {
                        result = true;
                    }
                }
            }

            if (!result)
            {
                string url = BaseSystemInfo.UserCenterHost + "UserCenterV42/LogOnService.ashx";
                WebClient webClient = new WebClient();
                NameValueCollection postValues = new NameValueCollection();
                postValues.Add("function", "ValidateOpenId");
                postValues.Add("systemCode", BaseSystemInfo.SystemCode);
                postValues.Add("ipAddress", Utilities.GetIPAddress());
                postValues.Add("securityKey", BaseSystemInfo.SecurityKey);
                postValues.Add("openId", openId);
                // 向服务器发送POST数据
                byte[] responseArray = webClient.UploadValues(url, postValues);
                string response = Encoding.UTF8.GetString(responseArray);
                if (!string.IsNullOrEmpty(response))
                {
                    result = response.Equals(true.ToString(), StringComparison.InvariantCultureIgnoreCase);
                }
            }
            return result;
        }

        //
        // 获取用户的最新 OpenId 的方法
        //

        public static string GetUserOpenId(BaseUserInfo userInfo)
        {
            string result = string.Empty;
            
            if (userInfo != null && !string.IsNullOrWhiteSpace(userInfo.Id))
            {
                using (var redis = new PooledRedisClientManager(Port, new string[] { Url }).GetClient())
                {
                    result = redis.Get<string>("userId" + userInfo.Id);
                    if (string.IsNullOrWhiteSpace(result))
                    {
                        userInfo.OpenId = result;
                        HttpContext.Current.Session[DotNet.Business.Utilities.SessionName] = userInfo;
                        HttpContext.Current.Session["openId"] = userInfo.OpenId;
                    }
                }
                // 从数据库获取,这里要考虑读写分离的做法
                if (string.IsNullOrWhiteSpace(result))
                {
                    using (IDbHelper dbHelper = DbHelperFactory.GetHelper(BaseSystemInfo.UserCenterDbType, BaseSystemInfo.UserCenterReadDbConnection))
                    {
                        List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
                        parameters.Add(new KeyValuePair<string, object>(BaseUserLogOnEntity.FieldId, userInfo.Id));
                        result = DbLogic.GetProperty(dbHelper, BaseUserLogOnEntity.TableName, parameters, BaseUserLogOnEntity.FieldOpenId);
                        if (string.IsNullOrWhiteSpace(result))
                        {
                            userInfo.OpenId = result;
                            HttpContext.Current.Session[DotNet.Business.Utilities.SessionName] = userInfo;
                            HttpContext.Current.Session["openId"] = userInfo.OpenId;
                            SetUserOpenId(userInfo.Id, userInfo.OpenId);
                        }
                    }
                }
            }
            return result;
        }

        public static void SetUserOpenId(string userId, string openId)
        {
            string key = string.Empty;
            if (!string.IsNullOrWhiteSpace(userId))
            {
                using (var redis = new PooledRedisClientManager(Port, new string[] { Url }).GetClient())
                {
                    key = "openId" + openId;
                    redis.Set(key, userId, DateTime.Now.AddHours(16));
                    key = "userId" + userId;
                    redis.Set(key, openId, DateTime.Now.AddHours(16));
                }
            }
        }
    }
}