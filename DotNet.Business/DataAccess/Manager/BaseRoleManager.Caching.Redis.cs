//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd.  
//-----------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace DotNet.Business
{
    using DotNet.Model;

    /// <remarks>
    /// BaseRoleManager
    /// 角色表缓存
    /// 
    /// 修改记录
    /// 
    ///     2015.12.10 版本：1.0 JiRiGaLa  创建。
    ///	
    /// <author>  
    ///		<name>JiRiGaLa</name>
    ///		<date>2015.12.10</date>
    /// </author> 
    /// </remarks>
    public partial class BaseRoleManager
    {
        public static BaseRoleEntity GetCacheByKey(string key)
        {
            BaseRoleEntity result = null;

            if (!string.IsNullOrWhiteSpace(key))
            {
                using (var redisClient = PooledRedisHelper.GetClient())
                {
                    result = redisClient.Get<BaseRoleEntity>(key);
                }
            }

            return result;
        }

        private static void SetCache(string systemCode, BaseRoleEntity entity)
        {
            if (string.IsNullOrWhiteSpace(systemCode))
            {
                systemCode = "Base";
            }

            if (entity != null && !string.IsNullOrEmpty(entity.Id))
            {
                string key = string.Empty;
                using (var redisClient = PooledRedisHelper.GetClient())
                {
                    key = systemCode + ".Role." + entity.Id;
                    redisClient.Set<BaseRoleEntity>(key, entity, DateTime.Now.AddMinutes(20));

                    key = systemCode + ".Role." + entity.Code;
                    redisClient.Set<BaseRoleEntity>(key, entity, DateTime.Now.AddMinutes(20));
                }
            }
        }

        private static void SetListCacheByKey(string key, List<BaseRoleEntity> list)
        {
            if (!string.IsNullOrWhiteSpace(key) && list != null)
            {
                using (var redisClient = PooledRedisHelper.GetClient())
                {
                    redisClient.Set<List<BaseRoleEntity>>(key, list, DateTime.Now.AddMinutes(20));
                }
            }
        }

        private static List<BaseRoleEntity> GetListCacheByKey(string key)
        {
            List<BaseRoleEntity> result = null;

            if (!string.IsNullOrWhiteSpace(key))
            {
                using (var redisClient = PooledRedisHelper.GetClient())
                {
                    result = redisClient.Get<List<BaseRoleEntity>>(key);
                }
            }

            return result;
        }

        #region public static void ClearCache() 清除缓存
        /// <summary>
        /// 清除缓存
        /// </summary>
        public static void ClearCache()
        {
        }
        #endregion
    }
}