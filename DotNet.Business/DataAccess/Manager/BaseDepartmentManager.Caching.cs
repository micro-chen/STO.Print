//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2015 , Hairihan TECH, Ltd. 
//-----------------------------------------------------------------

using System;
using System.Web;
using System.Web.Caching;

namespace DotNet.Business
{
    using DotNet.Model;
    using DotNet.Utilities;

    /// <remarks>
    /// BaseDepartmentManager
    /// 部门管理
    /// 
    /// 修改纪录
    /// 
    ///	
    ///	版本：1.0 2015.04.23 潘齐民 创建文件，用于缓存。
    ///	
    /// <author>  
    ///		<name>潘齐民</name>
    ///		<date>2015.04.23</date>
    /// </author> 
    /// </remarks>
    public partial class BaseDepartmentManager
    {
        public static bool RemoveCache(string key)
        {
            bool result = false;

            if (!string.IsNullOrWhiteSpace(key))
            {
                System.Web.Caching.Cache cache = HttpRuntime.Cache;

                string cacheObject = "Department" + key;
                cache.Remove(cacheObject);
            }

            return result;
        }

        public static BaseDepartmentEntity GetCache(string key)
        {
            BaseDepartmentEntity result = null;

            if (!string.IsNullOrWhiteSpace(key))
            {
                System.Web.Caching.Cache cache = HttpRuntime.Cache;
                if (cache != null && cache[key] != null)
                {
                    result = cache[key] as BaseDepartmentEntity;
                }
            }

            return result;
        }

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="entity">实体</param>
        public static void SetCache(BaseDepartmentEntity entity)
        {
            if (entity != null && entity.Id != null)
            {
                string key = string.Empty;
                System.Web.Caching.Cache cache = HttpRuntime.Cache;

                key = "Department" + entity.Id;
                cache.Add(key, entity, null, DateTime.Now.AddHours(16), TimeSpan.Zero, CacheItemPriority.Normal, null);

                key = "DepartmentByCode" + entity.Code;
                cache.Add(key, entity, null, DateTime.Now.AddHours(16), TimeSpan.Zero, CacheItemPriority.Normal, null);

                key = "DepartmentByName" + entity.FullName;
                cache.Add(key, entity, null, DateTime.Now.AddHours(16), TimeSpan.Zero, CacheItemPriority.Normal, null);
            }
        }
    }
}