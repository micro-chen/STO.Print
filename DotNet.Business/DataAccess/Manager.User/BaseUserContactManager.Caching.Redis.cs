//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd.  
//-----------------------------------------------------------------

using System;

namespace DotNet.Business
{
    using DotNet.Model;

    /// <remarks>
    /// BaseUserContactContact
    /// 用户联系方式管理
    /// 
    /// 修改记录
    /// 
    ///	版本：1.1 2015.06.05    JiRiGaLa    强制重新设置缓存的功能。
    ///	版本：1.0 2015.01.06    JiRiGaLa    选项管理从缓存读取，通过编号显示名称的函数完善。
    ///	
    /// <author>  
    ///		<name>JiRiGaLa</name>
    ///		<date>2015.06.05</date>
    /// </author> 
    /// </remarks>
    public partial class BaseUserContactManager
    {
        public static BaseUserContactEntity GetCacheByKey(string key)
        {
            BaseUserContactEntity result = null;

            if (!string.IsNullOrWhiteSpace(key))
            {
                using (var redisClient = PooledRedisHelper.GetClient())
                {
                    result = redisClient.Get<BaseUserContactEntity>(key);
                }
            }
            return result;
        }

        private static void SetCache(BaseUserContactEntity entity)
        {
            string key = string.Empty;
            if (entity != null && !string.IsNullOrWhiteSpace(entity.Id))
            {
                using (var redisClient = PooledRedisHelper.GetClient())
                {
                    key = "UserContact:" + entity.Id;
                    redisClient.Set<BaseUserContactEntity>(key, entity, DateTime.Now.AddMinutes(10));
                }
            }
        }

        /// <summary>
        /// 从缓存获取获取实体
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>实体</returns>
        public static BaseUserContactEntity GetObjectByCache(string id)
        {
            BaseUserContactEntity result = null;

            string key = "UserContact:";
            if (!string.IsNullOrEmpty(id))
            {
                key = key + id;
            }
            result = GetCacheByKey(key);
            if (result == null)
            {
                result = SetCache(id);
            }

            return result;
        }
    }
}