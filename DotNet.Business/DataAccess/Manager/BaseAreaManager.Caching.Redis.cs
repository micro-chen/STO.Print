//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd.  
//-----------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace DotNet.Business
{
    using DotNet.Model;

    /// <remarks>
    /// BaseAreaManager
    /// 地区表(省、市、县)
    /// 
    /// 修改记录
    /// 
    ///		2015.12.03 版本：1.3 JiRiGaLa List<BaseAreaEntity> 读写方法改进。
    ///	    2015.07.17 版本：1.2 JiRiGaLa 缓存清除功能实现（其实是重新获取了就可以了）。
    ///	    2015.03.15 版本：1.1 JiRiGaLa 缓存的时间不能太长了、不方便变更、10分钟读取一次就可以了。
    ///	    2015.01.06 版本：1.0 JiRiGaLa 选项管理从缓存读取，通过编号显示名称的函数完善。
    ///	
    /// <author>  
    ///		<name>JiRiGaLa</name>
    ///		<date>2015.12.03</date>
    /// </author> 
    /// </remarks>
    public partial class BaseAreaManager
    {
        public static bool RemoveCache(string key)
        {
            bool result = false;

            if (!string.IsNullOrWhiteSpace(key))
            {
                using (var redisClient = PooledRedisHelper.GetClient())
                {
                    result = redisClient.Remove(key);
                }
            }

            return result;
        }

        public static BaseAreaEntity GetCache(string key)
        {
            BaseAreaEntity result = null;

            if (!string.IsNullOrWhiteSpace(key))
            {
                using (var redisClient = PooledRedisHelper.GetClient())
                {
                    result = redisClient.Get<BaseAreaEntity>(key);
                }
            }

            return result;
        }

        private static void SetCache(BaseAreaEntity entity)
        {
            if (entity != null && !string.IsNullOrWhiteSpace(entity.Id))
            {
                string key = string.Empty;
                using (var redisClient = PooledRedisHelper.GetClient())
                {
                    key = "Area:" + entity.Id;
                    redisClient.Set<BaseAreaEntity>(key, entity, DateTime.Now.AddMinutes(10));
                }
            }
        }

        private static void SetListCache(string key, List<BaseAreaEntity> list)
        {
            if (!string.IsNullOrWhiteSpace(key) && list != null && list.Count > 0)
            {
                using (var redisClient = PooledRedisHelper.GetClient())
                {
                    redisClient.Set<List<BaseAreaEntity>>(key, list, DateTime.Now.AddMinutes(10));
                }
            }
        }

        private static List<BaseAreaEntity> GetListCache(string key)
        {
            List<BaseAreaEntity> result = null;

            if (!string.IsNullOrWhiteSpace(key))
            {
                using (var redisClient = PooledRedisHelper.GetClient())
                {
                    result = redisClient.Get<List<BaseAreaEntity>>(key);
                }
            }

            return result;
        }
    }
}