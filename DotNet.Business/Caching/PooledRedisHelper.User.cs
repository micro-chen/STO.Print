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
    /// 用户数据的缓存独立库。
    ///
    /// 修改记录
    ///
    ///		2016-01-07 版本：1.1 JiRiGaLa 实现读写分离。
    ///		2015-04-11 版本：1.0 JiRiGaLa 创建主键。
    ///
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2016-01-07</date>
    /// </author>
    /// </summary>
    public sealed partial class PooledRedisHelper
    {
        // 数据库
        public static int InitialDbUser = 6;

        private static PooledRedisClientManager instanceUser = null;

        public static PooledRedisClientManager InstanceUser
        {
            get
            {
                if (instanceUser == null)
                {
                    instanceUser = new PooledRedisClientManager(new string[] { BaseSystemInfo.RedisHosts }, new string[] { BaseSystemInfo.RedisReadOnlyHosts }, InitialDbUser);
                }

                return instanceUser;
            }
        }

        public static IRedisClient GetUserClient()
        {
            return InstanceUser.GetClient();
        }

        public static IRedisClient GetUserReadOnlyClient()
        {
            return InstanceUser.GetReadOnlyClient();
        }
    }
}