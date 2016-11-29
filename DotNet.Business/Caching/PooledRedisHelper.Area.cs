//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2015 , Hairihan TECH, Ltd. 
//-----------------------------------------------------------------

using System.Configuration;

namespace DotNet.Business
{
    using DotNet.Utilities;
    using ServiceStack.Redis;

    /// <summary>
    /// 区域用独立的库。
    ///
    /// 修改记录
    ///
    ///		2015-06-19 版本：1.0 JiRiGaLa 创建主键。
    ///
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2015-06-19</date>
    /// </author>
    /// </summary>
    public sealed partial class PooledRedisHelper
    {
        // 数据库
        public static int InitialDbArea = 30;
        
        private static PooledRedisClientManager instanceArea = null;

        public static PooledRedisClientManager InstanceArea
        {
            get
            {
                if (instanceArea == null)
                {
                    instanceArea = new PooledRedisClientManager(new string[] { BaseSystemInfo.RedisHosts }, new string[] { BaseSystemInfo.RedisReadOnlyHosts }, InitialDbArea);
                }

                return instanceArea;
            }
        }

        public static IRedisClient GetAreaClient()
        {
            return InstanceArea.GetClient();
        }

        public static IRedisClient GetAreaReadOnlyClient()
        {
            return InstanceArea.GetReadOnlyClient();
        }
    }
}