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
    using DotNet.Utilities;

    /// <remarks>
    /// BaseRoleManager
    /// 角色管理
    /// 
    /// 修改纪录
    /// 
    ///     版本：3.0 2015.12.11 JiRiGaLa   整顿代码。
    ///     版本：2.0 2015.04.20 JiRiGaLa   用缓存方式获取系统角色。
    ///	    版本：1.0 2012.12.17 JiRiGaLa   选项管理从缓存读取，通过编号显示名称的函数完善。
    ///	
    /// <author>  
    ///		<name>JiRiGaLa</name>
    ///		<date>2015.12.11</date>
    /// </author> 
    /// </remarks>
    public partial class BaseRoleManager
    {
        public static BaseRoleEntity GetCache(string key)
        {
            BaseRoleEntity result = null;

            if (!string.IsNullOrWhiteSpace(key))
            {
                System.Web.Caching.Cache cache = HttpRuntime.Cache;
                if (cache != null && cache[key] == null)
                {
                    result = cache[key] as BaseRoleEntity;
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
                System.Web.Caching.Cache cache = HttpRuntime.Cache;

                key = systemCode + ".Role." + entity.Id.ToString();
                cache.Add(key, entity, null, DateTime.Now.AddMinutes(10), TimeSpan.Zero, CacheItemPriority.Normal, null);

                key = systemCode + ".Role." + entity.Code;
                cache.Add(key, entity, null, DateTime.Now.AddMinutes(10), TimeSpan.Zero, CacheItemPriority.Normal, null);

                key = systemCode + ".Role." + entity.RealName;
                cache.Add(key, entity, null, DateTime.Now.AddMinutes(10), TimeSpan.Zero, CacheItemPriority.Normal, null);
            }
        }

        private static void SetListCache(string key, List<BaseRoleEntity> list)
        {
            if (!string.IsNullOrWhiteSpace(key) && list != null)
            {
                System.Web.Caching.Cache cache = HttpRuntime.Cache;
                cache.Add(key, list, null, DateTime.Now.AddMinutes(10), TimeSpan.Zero, CacheItemPriority.Normal, null);
            }
        }

        private static List<BaseRoleEntity> GetListCache(string key)
        {
            List<BaseRoleEntity> result = null;

            if (!string.IsNullOrWhiteSpace(key))
            {
                System.Web.Caching.Cache cache = HttpRuntime.Cache;
                if (cache != null && cache[key] == null)
                {
                    result = cache[key] as List<BaseRoleEntity>;
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