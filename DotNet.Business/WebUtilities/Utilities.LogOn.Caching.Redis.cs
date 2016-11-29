//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd. 
//-----------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Text;
using System.Web;

namespace DotNet.Business
{
    using DotNet.Model;
    using DotNet.Utilities;
    using ServiceStack.Redis;

    /// <summary>
    /// 登录功能相关部分
    /// 
    /// 修改记录
    /// 
    ///		2015.11.20 版本：1.0 JiRiGaLa 进行改进。
    ///		
    /// </summary>
    public partial class Utilities
    {
        /// <summary>
        /// 验证用户信息（防止篡改用户信息、增强安全性）
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        /// <param name="systemCode">缓存系统</param>
        /// <param name="useCaching">采用缓存</param>
        /// <param name="useDataBase">采用数据库</param>
        /// <param name="useUserCenterHost">采用用户中心接口</param>
        /// <returns>验证通过</returns>
        public static bool ValidateUserInfo(BaseUserInfo userInfo, string cachingSystemCode = null, bool useCaching = true, bool useDataBase = false, bool useUserCenterHost = false)
        {
            // 用户是否为空的?
            if (userInfo == null)
            {
                return false;
            }
            // 用户的主键不是数字类型的?
            if (!ValidateUtil.IsInt(userInfo.Id))
            {
                return false;
            }
            // 没有OpenId的
            if (string.IsNullOrEmpty(userInfo.OpenId))
            {
                return false;
            }
            // 从缓存里进行校验?
            BaseUserInfo resultUserInfo = null;
            resultUserInfo = GetUserInfoCaching(userInfo.OpenId, cachingSystemCode);
            // 获取不到用户
            if (resultUserInfo == null)
            {
                return false;
            }
            // 进行比对
            if (resultUserInfo.IsAdministrator != userInfo.IsAdministrator)
            {
                return false;
            }
            if (resultUserInfo.IdentityAuthentication != userInfo.IdentityAuthentication)
            {
                return false;
            }
            if (resultUserInfo.Permission != userInfo.Permission)
            {
                return false;
            }
            if (resultUserInfo.Id != userInfo.Id)
            {
                return false;
            }
            if (resultUserInfo.CompanyId != userInfo.CompanyId)
            {
                return false;
            }
            if (resultUserInfo.Code != userInfo.Code)
            {
                return false;
            }
            if (resultUserInfo.NickName != userInfo.NickName)
            {
                return false;
            }
            // 若都没问题，就OK了
            return true;
        }

        /// <summary>
        /// 验证用户的令牌openId
        /// </summary>
        /// <param name="userId">用户主键</param>
        /// <param name="openId">用户的令牌</param>
        /// <param name="useCaching">采用缓存</param>
        /// <param name="useDataBase">采用数据库</param>
        /// <param name="useUserCenterHost">采用用户中心接口</param>
        /// <returns>验证通过</returns>
        public static bool ValidateOpenId(string userId, string openId, string cachingSystemCode = null, bool useCaching = true, bool useDataBase = false, bool useUserCenterHost = false)
        {
            bool result = false;

            if (string.IsNullOrEmpty(cachingSystemCode))
            {
                cachingSystemCode = string.Empty;
            }

            // 2016-03-14 吉日嘎拉、PDA系统的单独处理、其他的都认为是一样的。
            if (!cachingSystemCode.Equals("PDA"))
            {
                cachingSystemCode = string.Empty;
            }

            if (!string.IsNullOrWhiteSpace(openId))
            {
                // 使用缓存进行验证、效率高，减少数据库的I/O压力。
                if (useCaching)
                {
                    string key = string.Empty;
                    // 2015-11-20 吉日嘎拉 为了编译通过进行改进
                    using (var redisClient = PooledRedisHelper.GetReadOnlyClient())
                    {
                        if (string.IsNullOrEmpty(cachingSystemCode))
                        {
                            key = "openId:" + openId;
                        }
                        else
                        {
                            key = "openId:" + cachingSystemCode + ":" + openId;
                        }
                        result = redisClient.ContainsKey(key);
                    }
                }

                // 用数据库的方式进行验证
                if (!result && useDataBase)
                {
                    BaseUserLogOnManager userLogOnManager = new BaseUserLogOnManager();
                    result = userLogOnManager.ValidateOpenId(userId, openId, cachingSystemCode);
                    if (result)
                    {
                        // 提高缓存效率、若读取到了，写入到缓存里去
                        if (!string.IsNullOrWhiteSpace(userId) && useCaching)
                        {
                            SetUserOpenId(userId, openId, cachingSystemCode);
                        }
                        result = true;
                    }
                }

                // 不能访问数据库时、通过远程用户中心服务进行验证OpenId、通过服务方式进行验证
                if (!result && useUserCenterHost)
                {
                    string url = BaseSystemInfo.UserCenterHost + "/UserCenterV42/LogOnService.ashx";
                    WebClient webClient = new WebClient();
                    NameValueCollection postValues = new NameValueCollection();
                    if (!string.IsNullOrEmpty(cachingSystemCode))
                    {
                        postValues.Add("systemCode", cachingSystemCode);
                    }
                    postValues.Add("ipAddress", Utilities.GetIPAddress());
                    postValues.Add("securityKey", BaseSystemInfo.SecurityKey);
                    postValues.Add("function", "ValidateOpenId");
                    postValues.Add("userId", userId);
                    postValues.Add("openId", openId);
                    // 向服务器发送POST数据
                    byte[] responseArray = webClient.UploadValues(url, postValues);
                    string response = Encoding.UTF8.GetString(responseArray);
                    if (!string.IsNullOrEmpty(response))
                    {
                        result = response.Equals(true.ToString(), StringComparison.InvariantCultureIgnoreCase);
                    }
                }
            }

            return result;
        }

        //
        // 获取用户的最新 OpenId 的方法
        //

        public static string GetUserOpenId(BaseUserInfo userInfo, string cachingSystemCode = null, bool useCaching = true, bool useDataBase = false, bool useUserCenterHost = true)
        {
            string result = string.Empty;

            if (useCaching && userInfo != null && !string.IsNullOrWhiteSpace(userInfo.Id))
            {
                // 2015-11-20 吉日嘎拉 为了编译通过进行改进
                using (var redisClient = PooledRedisHelper.GetClient())
                {
                    string key = string.Empty;
                    if (string.IsNullOrEmpty(cachingSystemCode))
                    {
                        key = "userId:" + userInfo.Id;
                    }
                    else
                    {
                        key = "userId:" + cachingSystemCode + ":" + userInfo.Id;
                    }
                    result = redisClient.Get<string>(key);
                    // 2016-04-05 吉日嘎拉 这里代码有错误，不为空时进行处理
                    if (!string.IsNullOrWhiteSpace(result))
                    {
                        userInfo.OpenId = result;
                        HttpContext.Current.Session[DotNet.Business.Utilities.SessionName] = userInfo;
                        HttpContext.Current.Session["openId"] = userInfo.OpenId;
                        // 2016-04-05 吉日嘎拉，这里可以提前结束了，提高效率
                        return result;
                    }
                }

                if (useDataBase)
                {
                    BaseUserLogOnManager userLogOnManager = new Business.BaseUserLogOnManager(userInfo);
                    result = userLogOnManager.GetUserOpenId(userInfo, cachingSystemCode);
                }

                if (string.IsNullOrEmpty(result) && useUserCenterHost)
                {
                    result = LogOnUtilities.GetUserOpenId(userInfo, cachingSystemCode);
                }

                // 从数据库获取,这里要考虑读写分离的做法
                if (!string.IsNullOrWhiteSpace(result))
                {
                    userInfo.OpenId = result;
                    HttpContext.Current.Session[DotNet.Business.Utilities.SessionName] = userInfo;
                    HttpContext.Current.Session["openId"] = userInfo.OpenId;
                    // 这里这样处理一下，提高下次处理的效率
                    if (useCaching)
                    {
                        SetUserOpenId(userInfo.Id, userInfo.OpenId, cachingSystemCode);
                    }
                }
            }

            return result;
        }

        public static void SetUserOpenId(string userId, string openId, string cachingSystemCode = null)
        {
            if (!string.IsNullOrWhiteSpace(userId) && !string.IsNullOrWhiteSpace(openId))
            {
                // 2015-11-20 吉日嘎拉 为了编译通过进行改进
                using (var redisClient = PooledRedisHelper.GetClient())
                {
                    SetUserOpenId(redisClient, userId, openId, cachingSystemCode);
                }
            }
        }

        public static void SetUserOpenId(IRedisClient redisClient, string userId, string openId, string cachingSystemCode = null)
        {
            string key = string.Empty;

            if (!string.IsNullOrWhiteSpace(userId) && !string.IsNullOrWhiteSpace(openId))
            {
                // 2016-03-03 吉日嘎拉 让缓存早点儿失效
                DateTime expiresAt = DateTime.Now.AddHours(16);
                // 2016-03-23 吉日嘎拉 出现异常
                if (string.IsNullOrEmpty(cachingSystemCode))
                {
                    key = "openId:" + openId;
                }
                else
                {
                    key = "openId:" + cachingSystemCode + ":" + openId;
                }
                redisClient.Set(key, userId, expiresAt);

                if (string.IsNullOrEmpty(cachingSystemCode))
                {
                    key = "userId:" + userId;
                }
                else
                {
                    key = "userId:" + cachingSystemCode + ":" + userId;
                }
                redisClient.Set(key, openId, expiresAt);
            }
        }

        /// <summary>
        /// 把当前登录的用户信息缓存起来
        /// </summary>
        /// <param name="userInfo">当前登录用户信息</param>
        /// <param name="cachingSystemCode">系统编号</param>
        public static bool SetUserInfoCaching(BaseUserInfo userInfo, string cachingSystemCode = null)
        {
            bool result = false;

            if (userInfo != null && !string.IsNullOrEmpty(userInfo.Id))
            {
                // 2015-11-20 吉日嘎拉 为了编译通过进行改进
                using (var redisClient = PooledRedisHelper.GetClient())
                {
                    SetUserOpenId(redisClient, userInfo.Id, userInfo.OpenId, cachingSystemCode);
                    // 2016-03-24 吉日嘎拉、默认保存8个小时，时间太长了
                    if (string.IsNullOrEmpty(cachingSystemCode))
                    {
                        DateTime expiresAt = DateTime.Now.AddHours(16);
                        string key = "userInfo:" + userInfo.Id;
                        result = redisClient.Set<BaseUserInfo>(key, userInfo, expiresAt);
                    }
                }
            }

            return result;
        }

        public static BaseUserInfo GetUserInfoCaching(string openId, string cachingSystemCode = null)
        {
            BaseUserInfo result = null;

            // 2015-11-20 吉日嘎拉 为了编译通过进行改进
            using (var redisClient = PooledRedisHelper.GetClient())
            {
                string key = string.Empty;
                if (string.IsNullOrEmpty(cachingSystemCode))
                {
                    key = "openId:" + openId;
                }
                else
                {
                    key = "openId:" + cachingSystemCode + ":" + openId;
                }
                string userId = redisClient.Get<string>(key);
                if (!string.IsNullOrEmpty(userId))
                {
                    key = "userInfo:" + userId;
                    result = redisClient.Get<BaseUserInfo>(key);
                }
            }

            return result;
        }
    }
}