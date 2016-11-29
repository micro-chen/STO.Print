//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2015 , Hairihan TECH, Ltd. 
//-----------------------------------------------------------------

using System.Configuration;

namespace DotNet.Business
{
    using DotNet.Utilities;
    using ServiceStack.Redis;

    /// <summary>
    /// 菜单模块用独立的库。
    ///
    /// 修改记录
    ///
    ///		2015-07-30 版本：1.0 JiRiGaLa 创建主键。
    ///
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2015-07-30</date>
    /// </author>
    /// </summary>
    public sealed partial class PooledRedisHelper
    {
        // 数据库
        public static int InitialDbModule = 38;
        
        private static PooledRedisClientManager instanceModule = null;

        public static PooledRedisClientManager InstanceModule
        {
            get
            {
                if (instanceModule == null)
                {
                    instanceModule = new PooledRedisClientManager(new string[] { BaseSystemInfo.RedisHosts }, new string[] { BaseSystemInfo.RedisReadOnlyHosts }, InitialDbModule);
                }

                return instanceModule;
            }
        }

        public static IRedisClient GetModuleClient()
        {
            return InstanceModule.GetClient();
        }

        public static IRedisClient GetModuleReadOnlyClient()
        {
            return InstanceModule.GetReadOnlyClient();
        }
    }
}