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
    /// 组织机构用独立的库。
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
        public static int InitialDbOrganize = 40;

        private static PooledRedisClientManager instanceOrganize = null;
       
        public static PooledRedisClientManager InstanceOrganize
        {
            get
            {
                if (instanceOrganize == null)
                {
                    instanceOrganize = new PooledRedisClientManager(new string[] { BaseSystemInfo.RedisHosts }, new string[] { BaseSystemInfo.RedisReadOnlyHosts }, InitialDbOrganize);
                }

                return instanceOrganize;
            }
        }

        public static IRedisClient GetOrganizeClient()
        {
            return InstanceOrganize.GetClient();
        }

        public static IRedisClient GetOrganizeReadOnlyClient()
        {
            return InstanceOrganize.GetReadOnlyClient();
        }
    }
}