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
    /// 这里是个坑、需要用静态的唯一的。
    /// 系统用户联系方式表
    ///
    /// 修改记录
    ///
    ///		2015-04-03 版本：1.0 JiRiGaLa 创建主键。
    ///
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2015-04-03</date>
    /// </author>
    /// </summary>
    public sealed partial class PooledRedisHelper
    {
        // 数据库
        public static int InitialDb = 6;

        // 2.6.15 最早的连接方式
        // Url = "ztredis6488(*)134&^%xswed@redis.ztosys.com:6488";

        // 2.6.17 有密码,有端口的,有数据同步（会发生 Zero length response）
        // Url = "ztredis(*)134&^%xswed@redis-Read.ztosys.com:6479";

        // [可用]
        // 2.6.17,无数据同步(有密码，建议用这个，3.9链接过来会有错误发生、4.0链接过来也会发生错误，Zero length response)
        // Url = "ztredis6480(*)134&^%xswed@redis-Read.ztosys.com:6480";

        // [可用、3.9]
        // 2.6.15,无数据同步(有密码)
        // Url = "ztredis6482(*)134&^%xswed@redis-Read.ztosys.com:6482";

        // [可用、3.9]
        // 2.6.15,无数据同步(有密码)
        // Url = "ztredis6488(*)134&^%xswed@redis-Read.ztosys.com:6488";

        // 192.168.0.134：6488  -》 192.168.0.123：6488

        private static PooledRedisClientManager instance = null;

        public static PooledRedisClientManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PooledRedisClientManager(BaseSystemInfo.RedisHosts, BaseSystemInfo.RedisReadOnlyHosts, InitialDb);
                }

                return instance;
            }
        }

        static PooledRedisHelper()
        {
        }

        public static IRedisClient GetClient()
        {
            return Instance.GetClient();
        }

        public static IRedisClient GetReadOnlyClient()
        {
            return Instance.GetReadOnlyClient();
        }
    }
}