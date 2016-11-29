//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd.  
//-----------------------------------------------------------------

using System;
using System.Configuration;

namespace DotNet.Business
{
    using DotNet.Utilities;
    using ServiceStack.Redis;

    /// <summary>
    /// 消息提醒独立的库。
    ///
    /// 修改记录
    ///
    ///		2016-01-18 版本：1.1 JiRiGaLa 读写进行分离。
    ///		2015-09-26 版本：1.0 JiRiGaLa 创建主键。
    ///
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2016-01-18</date>
    /// </author>
    /// </summary>
    public sealed partial class PooledRedisHelper
    {
        // 数据库
        public static int InitialDbSpelling = 1;

        private static PooledRedisClientManager instanceSpelling = null;

        public static PooledRedisClientManager InstanceSpelling
        {
            get
            {
                if (instanceSpelling == null)
                {
                    instanceSpelling = new PooledRedisClientManager(BaseSystemInfo.RedisHosts, BaseSystemInfo.RedisReadOnlyHosts, InitialDbSpelling);
                }

                return instanceSpelling;
            }
        }

        public static IRedisClient GetSpellingClient()
        {
            return InstanceSpelling.GetClient();
        }

        public static IRedisClient GetSpellingReadOnlyClient()
        {
            return InstanceSpelling.GetReadOnlyClient();
        }
    }
}