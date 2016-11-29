//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd. 
//-----------------------------------------------------------------

using System.Configuration;

namespace DotNet.Business
{
    using DotNet.Utilities;
    using ServiceStack.Redis;

    /// <summary>
    /// 判断权限用的独立的库。
    ///
    /// 修改记录
    ///
    ///		2016-02-26 版本：1.0 JiRiGaLa 创建主键。
    ///
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2016-02-26</date>
    /// </author>
    /// </summary>
    public sealed partial class PooledRedisHelper
    {
        // 数据库
        public static int InitialDbPermission = 8;

        private static PooledRedisClientManager instancePermission = null;

        public static PooledRedisClientManager InstancePermission
        {
            get
            {
                if (instancePermission == null)
                {
                    instancePermission = new PooledRedisClientManager(BaseSystemInfo.RedisHosts, BaseSystemInfo.RedisReadOnlyHosts, InitialDbPermission);
                }

                return instancePermission;
            }
        }

        public static IRedisClient GetPermissionClient()
        {
            return InstancePermission.GetClient();
        }

        public static IRedisClient GetPermissionReadOnlyClient()
        {
#if ReadOnlyRedis
            return InstancePermission.GetReadOnlyClient();
#else
            return InstancePermission.GetClient();             
#endif
        }
    }
}