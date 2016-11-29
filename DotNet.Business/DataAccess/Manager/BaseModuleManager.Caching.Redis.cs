//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd.  
//-----------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace DotNet.Business
{
    using DotNet.Model;
    using DotNet.Utilities;

    /// <remarks>
    /// BaseModuleManager
    /// 模块表缓存
    /// 
    /// 修改记录
    /// 
    ///		2016.02.26 版本：1.4 JiRiGaLa 独立缓存库。
    ///		2015.12.03 版本：1.3 JiRiGaLa List<BaseModuleEntity> 读写方法改进。
    ///     2015.07.30 版本：1.0 JiRiGaLa 创建。
    ///	
    /// <author>  
    ///		<name>JiRiGaLa</name>
    ///		<date>2016.02.26</date>
    /// </author> 
    /// </remarks>
    public partial class BaseModuleManager
    {
        public static bool RemoveCache(string key)
        {
            bool result = false;

            if (!string.IsNullOrWhiteSpace(key))
            {
                using (var redisClient = PooledRedisHelper.GetPermissionClient())
                {
                    result = redisClient.Remove(key);
                }
            }

            return result;
        }

        public static BaseModuleEntity GetCacheByKey(string key)
        {
            BaseModuleEntity result = null;

            if (!string.IsNullOrWhiteSpace(key))
            {
                using (var redisClient = PooledRedisHelper.GetPermissionReadOnlyClient())
                {
                    result = redisClient.Get<BaseModuleEntity>(key);
                }
            }

            return result;
        }

        private static void SetCache(string systemCode, BaseModuleEntity entity)
        {
            if (string.IsNullOrWhiteSpace(systemCode))
            {
                systemCode = "Base";
            }

            if (entity != null && !string.IsNullOrEmpty(entity.Id))
            {
                string key = string.Empty;
                using (var redisClient = PooledRedisHelper.GetPermissionClient())
                {
                    key = systemCode + ".Module." + entity.Id.ToString();
                    redisClient.Set<BaseModuleEntity>(key, entity, DateTime.Now.AddMinutes(10));

                    key = systemCode + ".Module." + entity.Code;
                    redisClient.Set<BaseModuleEntity>(key, entity, DateTime.Now.AddMinutes(10));
                }
            }
        }

        private static void SetListCache(string key, List<BaseModuleEntity> list)
        {
            if (!string.IsNullOrWhiteSpace(key) && list != null)
            {
                using (var redisClient = PooledRedisHelper.GetPermissionClient())
                {
                    // 时间太长了，不太好，能缩短就再缩短一些，而且经常是会被呼叫的，所以时间设置少一些。
                    redisClient.Set<List<BaseModuleEntity>>(key, list, DateTime.Now.AddMinutes(10));
                }
            }
        }

        private static List<BaseModuleEntity> GetListCache(string key)
        {
            List<BaseModuleEntity> result = null;

            if (!string.IsNullOrWhiteSpace(key))
            {
                using (var redisClient = PooledRedisHelper.GetPermissionReadOnlyClient())
                {
                    result = redisClient.Get<List<BaseModuleEntity>>(key);
                }
            }

            return result;
        }
    }
}