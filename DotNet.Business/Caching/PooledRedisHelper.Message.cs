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
        public static int InitialDbMessage = 66;
        
        private static PooledRedisClientManager instanceMessage = null;

        public static PooledRedisClientManager InstanceMessage
        {
            get
            {
                if (instanceMessage == null)
                {
                    instanceMessage = new PooledRedisClientManager(BaseSystemInfo.RedisHosts, BaseSystemInfo.RedisReadOnlyHosts, InitialDbMessage);
                }

                return instanceMessage;
            }
        }

        public static IRedisClient GetMessageClient()
        {
            return InstanceMessage.GetClient();
        }

        public static IRedisClient GetMessageReadOnlyClient()
        {
            return InstanceMessage.GetReadOnlyClient();
        }
    }
}