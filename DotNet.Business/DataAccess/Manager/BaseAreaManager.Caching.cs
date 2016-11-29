//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2015 , Hairihan TECH, Ltd. 
//-----------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Caching;

namespace DotNet.Business
{
    using DotNet.Model;

    /// <remarks>
    /// BaseAreaManager
    /// 地区表(省、市、县)
    /// 
    /// 修改纪录
    /// 
    ///	    2015.12.10 版本：1.0 JiRiGaLa 重新构造。
    ///	
    /// <author>  
    ///		<name>JiRiGaLa</name>
    ///		<date>2015.12.10</date>
    /// </author> 
    /// </remarks>
    public partial class BaseAreaManager
    {
        public static bool RemoveCache(string key)
        {
            bool result = false;

            if (!string.IsNullOrWhiteSpace(key))
            {
                System.Web.Caching.Cache cache = HttpRuntime.Cache;
                cache.Remove(key);
            }

            return result;
        }

        public static BaseAreaEntity GetCache(string key)
        {
            BaseAreaEntity result = null;

            if (!string.IsNullOrWhiteSpace(key))
            {
                System.Web.Caching.Cache cache = HttpRuntime.Cache;
                if (cache != null && cache[key] == null)
                {
                    result = cache[key] as BaseAreaEntity;
                }
            }

            return result;
        }

        private static void SetCache(BaseAreaEntity entity)
        {
            if (entity != null && !string.IsNullOrWhiteSpace(entity.Id))
            {
                string key = string.Empty;
                key = "Area" + entity.Id;

                System.Web.Caching.Cache cache = HttpRuntime.Cache;
                cache.Add(key, entity, null, DateTime.Now.AddMinutes(10), TimeSpan.Zero, CacheItemPriority.Normal, null);
            }
        }

        private static void SetListCache(string key, List<BaseAreaEntity> list)
        {
            if (!string.IsNullOrWhiteSpace(key) && list != null && list.Count > 0)
            {
                System.Web.Caching.Cache cache = HttpRuntime.Cache;
                cache.Add(key, list, null, DateTime.Now.AddMinutes(10), TimeSpan.Zero, CacheItemPriority.Normal, null);
            }
        }

        private static List<BaseAreaEntity> GetListCache(string key)
        {
            List<BaseAreaEntity> result = null;

            if (!string.IsNullOrWhiteSpace(key))
            {
                System.Web.Caching.Cache cache = HttpRuntime.Cache;
                if (cache != null && cache[key] == null)
                {
                    result = cache[key] as List<BaseAreaEntity>;
                }
            }

            return result;
        }
    }
}