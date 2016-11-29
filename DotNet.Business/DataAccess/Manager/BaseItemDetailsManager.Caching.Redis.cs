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
    public partial class BaseItemDetailsManager
    {
        public static BaseItemDetailsEntity GetCache(string key)
        {
            BaseItemDetailsEntity result = null;

            if (!string.IsNullOrWhiteSpace(key))
            {

                using (var redisClient = PooledRedisHelper.GetClient())
                {
                    result = redisClient.Get<BaseItemDetailsEntity>(key);
                }
            }

            return result;
        }

        private static void SetCache(string tableName, BaseItemDetailsEntity entity)
        {
            if (entity != null)
            {
                string key = string.Empty;
                using (var redisClient = PooledRedisHelper.GetClient())
                {
                    key = "ItemDetails:" + tableName + ":" + entity.Id;
                    redisClient.Set<BaseItemDetailsEntity>(key, entity, DateTime.Now.AddMinutes(10));
                }
            }
        }

        private static void SetListCache(string key, List<BaseItemDetailsEntity> list)
        {
            if (!string.IsNullOrWhiteSpace(key) && list != null)
            {
                using (var redisClient = PooledRedisHelper.GetClient())
                {
                    // 2016-03-14 其实缓存10分钟也已经非常好了，就是缓存5分钟其实也足够好了
                    redisClient.Set<List<BaseItemDetailsEntity>>(key, list, DateTime.Now.AddMinutes(10));
                }
            }
        }

        public static List<BaseItemDetailsEntity> GetListCache(string key)
        {
            List<BaseItemDetailsEntity> result = null;

            if (!string.IsNullOrWhiteSpace(key))
            {
                using (var redisClient = PooledRedisHelper.GetClient())
                {
                    result = redisClient.Get<List<BaseItemDetailsEntity>>(key);
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