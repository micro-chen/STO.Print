//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd. 
//-----------------------------------------------------------------

using System;

namespace DotNet.Business
{
    using DotNet.Model;

    /// <summary>
    /// BaseUserLogonExtendManager
    /// 用户登录提醒 缓存
    /// 
    /// 修改记录
    /// 
    /// 2015-01-23 版本：1.0 SongBiao 创建文件。
    /// 
    /// <author>
    ///     <name>SongBiao</name>
    ///     <date>2015-01-23</date>
    /// </author>
    /// </summary>
    public partial class BaseUserLogonExtendManager : BaseManager, IBaseManager
    {
        /// <summary>
        /// 缓存键前缀
        /// </summary>
        private static string cacheKeyPrefix = "BaseUserLogonRemind";

        /// <summary>
        /// 根据key值获取缓存对象
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private static BaseUserLogonExtendEntity GetCache(string key)
        {
            BaseUserLogonExtendEntity result = null;
            if (!string.IsNullOrWhiteSpace(key))
            {
                using (var redisClient = PooledRedisHelper.GetClient())
                {
                    result = redisClient.Get<BaseUserLogonExtendEntity>(key);
                }
            }
            return result;
        }

        /// <summary>
        /// 将对象设置到缓存中
        /// </summary>
        /// <param name="entity"></param>
        public static void SetCache(BaseUserLogonExtendEntity entity)
        {
            string key = string.Empty;
            if (entity != null && !string.IsNullOrWhiteSpace(entity.Id.ToString()))
            {
                using (var redisClient = PooledRedisHelper.GetClient())
                {
                    key = cacheKeyPrefix + entity.Id;
                    redisClient.Set<BaseUserLogonExtendEntity>(key, entity, DateTime.Now.AddMinutes(10));
                }
            }
        }

        /// <summary>
        /// 根据主键从缓存中获取获取实体
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>实体</returns>
        public static BaseUserLogonExtendEntity GetObjectByCache(string id)
        {
            BaseUserLogonExtendEntity result = null;
            string cacheObject = cacheKeyPrefix;
            if (!string.IsNullOrEmpty(id))
            {
                cacheObject = cacheObject + id;
            }

            result = GetCache(cacheObject);
            if (result == null)
            {
                BaseUserLogonExtendManager manager = new BaseUserLogonExtendManager();
                result = manager.GetObject(id);
                if (result != null)
                {
                    SetCache(result);
                }
            }
            return result;
        }
    }
}
