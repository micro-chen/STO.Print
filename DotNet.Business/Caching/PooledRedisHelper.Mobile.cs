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
    /// 手机短信。
    ///
    /// 修改记录
    ///
    ///		2015-04-11 版本：1.0 JiRiGaLa 创建主键。
    ///
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2015-04-11</date>
    /// </author>
    /// </summary>
    public sealed partial class PooledRedisHelper
    {
        // 数据库
        public static int InitialDbMobile = 130;

        private static PooledRedisClientManager instanceMobile = null;

        public static PooledRedisClientManager InstanceMobile
        {
            get
            {
                if (instanceMobile == null)
                {
                    /*
                    RedisClientManagerConfig redisClientManagerConfig = new RedisClientManagerConfig();
                    redisClientManagerConfig.AutoStart = true;
                    redisClientManagerConfig.MaxReadPoolSize = 5;
                    redisClientManagerConfig.MaxWritePoolSize = 5;
                    redisClientManagerConfig.DefaultDb = InitialDbMobile;
                    */

                    instanceMobile = new PooledRedisClientManager(BaseSystemInfo.RedisHosts, BaseSystemInfo.RedisReadOnlyHosts, InitialDbMobile);
                }

                return instanceMobile;
            }
        }

        public static IRedisClient GetMobileClient()
        {
            return InstanceMobile.GetClient();
        }
    }
}