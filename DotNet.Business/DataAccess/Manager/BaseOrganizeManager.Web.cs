//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2015 , Hairihan TECH, Ltd. 
//-----------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;

namespace DotNet.Business
{
    using DotNet.Utilities;
    using DotNet.Model;

    /// <remarks>
    /// BaseOrganizeManager
    /// 组织机构管理
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
    public partial class BaseOrganizeManager
    {
        // 当前的锁
        private static object locker = new Object();

        #region public static void ClearCache() 清除缓存
        /// <summary>
        /// 清除缓存
        /// </summary>
        public static void ClearCache()
        {
            lock (BaseSystemInfo.UserLock)
            {
                System.Web.Caching.Cache cache = HttpRuntime.Cache;
                if (cache[BaseOrganizeEntity.TableName] != null)
                {
                    cache.Remove(BaseOrganizeEntity.TableName);
                }
            }
        }
        #endregion

        /*
        /// <summary>
        /// 从缓存获取获取实体
        /// </summary>
        /// <param name="id">主键</param>
        public static BaseOrganizeEntity GetObjectByCache(string id)
        {
            BaseOrganizeEntity result = null;
            System.Web.Caching.Cache cache = HttpRuntime.Cache;
            string cacheObject = "Organize";
            if (!string.IsNullOrEmpty(id))
            {
                cacheObject = "Organize" + id;
            }
            if (cache != null && cache[cacheObject] == null)
            {
                lock (locker)
                {
                    if (cache != null && cache[cacheObject] == null)
                    {
                        BaseOrganizeManager manager = new DotNet.Business.BaseOrganizeManager(BaseOrganizeEntity.TableName);
                        result = manager.GetObject(id);
                        // 若是空的不用缓存，继续读取实体
                        if (result != null)
                        {
                            cache.Add(cacheObject, result, null, DateTime.Now.AddMinutes(10), TimeSpan.Zero, CacheItemPriority.Normal, null);
                            System.Console.WriteLine(System.DateTime.Now.ToString(BaseSystemInfo.DateTimeFormat) + " cache Organize " + id);
                        }
                    }
                }
            }
            result = cache[cacheObject] as BaseOrganizeEntity;
            return result;
        }
        
        public static BaseOrganizeEntity GetObjectByCodeByCache(string code)
        {
            BaseOrganizeEntity result = null;
            System.Web.Caching.Cache cache = HttpRuntime.Cache;
            string cacheObject = "OrganizeByCode" + code;
            if (cache != null && cache[cacheObject] == null)
            {
                lock (locker)
                {
                    if (cache != null && cache[cacheObject] == null)
                    {
                        BaseOrganizeManager organizeManager = new BaseOrganizeManager();
                        result = organizeManager.GetObjectByCode(code);
                        cache.Add(cacheObject, result, null, DateTime.Now.AddMinutes(10), TimeSpan.Zero, CacheItemPriority.Normal, null);
                        System.Console.WriteLine(System.DateTime.Now.ToString(BaseSystemInfo.DateTimeFormat) + " cache Organize");
                    }
                }
            }
            result = cache[cacheObject] as BaseOrganizeEntity;
            return result;
        }

        public static BaseOrganizeEntity GetObjectByNameByCache(string fullName)
        {
            BaseOrganizeEntity result = null;
            System.Web.Caching.Cache cache = HttpRuntime.Cache;
            string cacheObject = "OrganizeByName" + fullName;
            if (cache != null && cache[cacheObject] == null)
            {
                lock (locker)
                {
                    if (cache != null && cache[cacheObject] == null)
                    {
                        BaseOrganizeManager organizeManager = new BaseOrganizeManager();
                        result = organizeManager.GetObjectByName(fullName);
                        cache.Add(cacheObject, result, null, DateTime.Now.AddMinutes(10), TimeSpan.Zero, CacheItemPriority.Normal, null);
                        System.Console.WriteLine(System.DateTime.Now.ToString(BaseSystemInfo.DateTimeFormat) + " cache Organize");
                    }
                }
            }
            result = cache[cacheObject] as BaseOrganizeEntity;
            return result;
        }
        */

        #region public static List<BaseOrganizeEntity> GetEntities() 获取角色表，从缓存读取
        /// <summary>
        /// 获取角色表，从缓存读取
        /// </summary>
        public static List<BaseOrganizeEntity> GetEntities()
        {
            System.Web.Caching.Cache cache = HttpRuntime.Cache;
            if (cache != null && cache[BaseOrganizeEntity.TableName] == null)
            {
                lock (BaseSystemInfo.UserLock)
                {
                    if (cache != null && cache[BaseOrganizeEntity.TableName] == null)
                    {
                        // 读取目标表中的数据
                        List<BaseOrganizeEntity> entityList = null;
                        BaseOrganizeManager manager = new DotNet.Business.BaseOrganizeManager(BaseOrganizeEntity.TableName);
                        entityList = manager.GetList<BaseOrganizeEntity>();
                        // 这个是没写过期时间的方法
                        // HttpContext.Current.Cache[tableName] = entityList;
                        // 设置过期时间为8个小时，第2天若有不正常的自动就可以正常了
                        cache.Add(BaseOrganizeEntity.TableName, entityList, null, DateTime.Now.AddMinutes(10), TimeSpan.Zero, CacheItemPriority.Normal, null);
                    }
                }
            }
            return cache[BaseOrganizeEntity.TableName] as List<BaseOrganizeEntity>;
        }
        #endregion
    }
}