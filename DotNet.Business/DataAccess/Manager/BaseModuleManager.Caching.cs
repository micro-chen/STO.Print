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
    /// BaseModuleManager
    /// 模块菜单管理
    /// 
    /// 修改纪录
    /// 
    ///	版本：1.0 2012.12.17    JiRiGaLa    选项管理从缓存读取，通过编号显示名称的函数完善。
    ///	
    /// <author>  
    ///		<name>JiRiGaLa</name>
    ///		<date>2012.12.17</date>
    /// </author> 
    /// </remarks>
    public partial class BaseModuleManager
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

        public static BaseModuleEntity GetCache(string key)
        {
            BaseModuleEntity result = null;

            if (!string.IsNullOrWhiteSpace(key))
            {
                System.Web.Caching.Cache cache = HttpRuntime.Cache;
                if (cache != null && cache[key] == null)
                {
                    result = cache[key] as BaseModuleEntity;
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
                key = systemCode + "Module" + entity.Id;

                System.Web.Caching.Cache cache = HttpRuntime.Cache;
                cache.Add(key, entity, null, DateTime.Now.AddMinutes(20), TimeSpan.Zero, CacheItemPriority.Normal, null);

                key = systemCode + ".Module." + entity.Code;
                cache.Add(key, entity, null, DateTime.Now.AddMinutes(20), TimeSpan.Zero, CacheItemPriority.Normal, null);
            }
        }

        private static void SetListCache(string key, List<BaseModuleEntity> list)
        {
            if (!string.IsNullOrWhiteSpace(key) && list != null && list.Count > 0)
            {
                System.Web.Caching.Cache cache = HttpRuntime.Cache;
                cache.Add(key, list, null, DateTime.Now.AddMinutes(10), TimeSpan.Zero, CacheItemPriority.Normal, null);

            }
        }

        private static List<BaseModuleEntity> GetListCache(string key)
        {
            List<BaseModuleEntity> result = null;

            if (!string.IsNullOrWhiteSpace(key))
            {
                System.Web.Caching.Cache cache = HttpRuntime.Cache;
                if (cache != null && cache[key] == null)
                {
                    result = cache[key] as List<BaseModuleEntity>;
                }
            }

            return result;
        }
    }
}