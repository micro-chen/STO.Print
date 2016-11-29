//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd.  
//-----------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace DotNet.Business
{
    using DotNet.Model;
    using DotNet.Utilities;

    /// <summary>
    /// BaseUserManager
    /// 用户管理
    /// 
    /// 修改记录
    /// 
    ///		2016.01.18 版本：1.0 JiRiGaLa	主键整理。
    /// 
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2016.01.18</date>
    /// </author> 
    /// </summary>
    public partial class BaseUserManager : BaseManager
    {
        /// <summary>
        /// 获取最近一个月登录系统的用户，从缓存服务器，登录接口获取，这样僵尸账户就会可以过滤了。
        /// </summary>
        /// <returns>影响行数</returns>
        public int GetActiveUsers()
        {
            int result = 0;

            // 先把用户来源都修改为 空。
            string commandText = "UPDATE " + BaseUserEntity.TableName + " SET " + BaseUserEntity.FieldUserFrom + " = NULL WHERE " + BaseUserEntity.FieldUserFrom + " = 'Base'";
            result = this.DbHelper.ExecuteNonQuery(commandText);
            // 通过中天登录的，都设置为 "Base";
            string key = string.Empty;
            result = 0;
            int i = 0;
            using (var redisClient = PooledRedisHelper.GetClient())
            {
                key = "LogOn:UserCount:" + DateTime.Now.ToString("yyyy-MM");
                HashSet<string> list = redisClient.GetAllItemsFromSet(key);
                foreach (string id in list)
                {
                    i++;
                    commandText = "UPDATE " + BaseUserEntity.TableName + " SET " + BaseUserEntity.FieldUserFrom + " = 'Base' WHERE " + BaseUserEntity.FieldId + " = " + id;
                    result += this.DbHelper.ExecuteNonQuery(commandText);
                    System.Console.WriteLine("Count:" + i.ToString() + "/" + list.Count +  " Id:" + id);
                }
            }

            return result;
        }

        /// <summary>
        /// 登录次数统计功能（通过缓存高速实时统计、统计键值还是有些问题、进行了改进）
        /// 2016-03-28 吉日嘎拉
        /// </summary>
        /// <param name="userInfo">用户</param>
        public static void LogOnStatistics(BaseUserInfo userInfo)
        {
            if (!BaseSystemInfo.LogOnStatistics)
            {
                return;
            }
            string key = string.Empty;
            using (var redisClient = PooledRedisHelper.GetCallLimitClient())
            {
                // 1: 登录次数统计
                // 1-1：当月的登录人数增加一，
                key = "LogOn:Count:" + DateTime.Now.ToString("yyyy-MM");
                redisClient.IncrementValue(key);
                redisClient.ExpireEntryAt(key, DateTime.Now.AddYears(1));
                // 1-2：当天的登录人数增加一，
                key = "LogOn:Count:" + DateTime.Now.ToString("yyyy-MM-dd");
                redisClient.IncrementValue(key);
                redisClient.ExpireEntryAt(key, DateTime.Now.AddYears(1));
                // 1-3：当前小时的登录人数加一。
                key = "LogOn:Count:" + DateTime.Now.ToString("yyyy-MM-dd:HH");
                redisClient.IncrementValue(key);
                redisClient.ExpireEntryAt(key, DateTime.Now.AddYears(1));
                
                // 2：不重复的登录统计
                // 2-1：当月的不重复登录人数增加一。
                key = "LogOn:UserCount:" + DateTime.Now.ToString("yyyy-MM");
                redisClient.AddItemToSet(key, userInfo.Id);
                redisClient.ExpireEntryAt(key, DateTime.Now.AddYears(1));
                // 2-2：当天的不重复登录人数增加一。
                key = "LogOn:UserCount:" + DateTime.Now.ToString("yyyy-MM-dd");
                redisClient.AddItemToSet(key, userInfo.Id);
                redisClient.ExpireEntryAt(key, DateTime.Now.AddYears(1));
                // 2-3：当前小时的不重复登录人数增加一。
                key = "LogOn:UserCount:" + DateTime.Now.ToString("yyyy-MM-dd:HH");
                redisClient.AddItemToSet(key, userInfo.Id);
                redisClient.ExpireEntryAt(key, DateTime.Now.AddYears(1));

                // 3：不重复的网点登录统计
                // 3-1：每天有多少个公司在登录系统
                key = "LogOn:CompanyCount:" + DateTime.Now.ToString("yyyy-MM-dd");
                redisClient.AddItemToSet(key, userInfo.CompanyId);
                redisClient.ExpireEntryAt(key, DateTime.Now.AddYears(1));
            }
        }
    }
}