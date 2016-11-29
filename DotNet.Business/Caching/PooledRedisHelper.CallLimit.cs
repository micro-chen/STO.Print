//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2015 , Hairihan TECH, Ltd. 
//-----------------------------------------------------------------

using System;
using System.Configuration;

namespace DotNet.Business
{
    using DotNet.Utilities;
    using ServiceStack.Redis;

    /// <summary>
    /// 调用频率限制独立的库。
    ///
    /// 修改记录
    ///
    ///		2015-09-25 版本：1.0 JiRiGaLa 创建主键。
    ///
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2015-09-25</date>
    /// </author>
    /// </summary>
    public sealed partial class PooledRedisHelper
    {
        // 数据库
        public static int InitialDbCallLimit = 78;
        
        private static PooledRedisClientManager instanceCallLimit = null;

        public static PooledRedisClientManager InstanceCallLimit
        {
            get
            {
                if (instanceCallLimit == null)
                {
                    instanceCallLimit = new PooledRedisClientManager(BaseSystemInfo.RedisHosts, BaseSystemInfo.RedisReadOnlyHosts, InitialDbCallLimit);
                }

                return instanceCallLimit;
            }
        }

        public static IRedisClient GetCallLimitClient()
        {
            return InstanceCallLimit.GetClient();
        }

        /// <summary>
        /// 是否在指定的时间内，已经到了呼叫限制次数
        /// 什么键名，什么键值，在多少时间内，限制调用几次
        /// 2015-09-25 吉日嘎拉
        /// </summary>
        /// <param name="keyName">键名</param>
        /// <param name="minutes">过期时间，多少时间里</param>
        /// <param name="limit">限制次数</param>
        /// <returns>是否超过限制</returns>
        public static bool CallLimit(string keyName, int minutes, int limit)
        {
            return CallLimit(keyName, DateTime.Now.AddMinutes(minutes), limit);
        }

        public static bool CallLimit(string keyName, DateTime expireAt, int limit)
        {
            bool result = false;

            using (var redisClient = PooledRedisHelper.GetCallLimitClient())
            {
                if (redisClient.ContainsKey(keyName))
                {
                    result = redisClient.IncrementValue(keyName) > limit;
                }
                else
                {
                    redisClient.IncrementValue(keyName);
                    // 设置过期时间
                    redisClient.ExpireEntryAt(keyName, expireAt);
                }
            }

            return result;
        }
    }
}