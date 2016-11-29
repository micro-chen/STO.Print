//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2015 , Hairihan TECH, Ltd. 
//-----------------------------------------------------------------

using System.Configuration;

namespace DotNet.Business
{
    using DotNet.Utilities;
    using ServiceStack.Redis;

    /// <summary>
    /// 联络单用独立的库。
    ///
    /// 修改记录
    ///
    ///		2015-09-14 版本：1.0 JiRiGaLa 创建主键。
    ///
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2015-09-14</date>
    /// </author>
    /// </summary>
    public sealed partial class PooledRedisHelper
    {
        // 数据库
        public static int InitialDbContact = 35;
        
        private static PooledRedisClientManager instanceContact = null;

        public static PooledRedisClientManager InstanceContact
        {
            get
            {
                if (instanceContact == null)
                {
                    instanceContact = new PooledRedisClientManager(new string[] { BaseSystemInfo.RedisHosts }, new string[] { BaseSystemInfo.RedisReadOnlyHosts }, InitialDbContact);
                }

                return instanceContact;
            }
        }

        public static IRedisClient GetContactClient()
        {
            return InstanceContact.GetClient();
        }
    }
}