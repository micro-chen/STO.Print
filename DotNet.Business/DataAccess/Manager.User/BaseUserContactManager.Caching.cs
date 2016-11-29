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
    /// BaseUserContactContact
    /// 用户联系方式管理
    /// 
    /// 修改纪录
    /// 
    ///	版本：1.0 2013.01.13    JiRiGaLa    选项管理从缓存读取，通过编号显示名称的函数完善。
    ///	
    /// <author>  
    ///		<name>JiRiGaLa</name>
    ///		<date>2013.01.13</date>
    /// </author> 
    /// </remarks>
    public partial class BaseUserContactManager
    {
        /// <summary>
        /// 从缓存获取获取实体
        /// </summary>
        /// <param name="id">主键</param>
        public static BaseUserContactEntity GetObjectByCache(string id)
        {
            BaseUserContactEntity result = null;

            System.Web.Caching.Cache cache = HttpRuntime.Cache;
            string cacheObject = "UserContact";
            if (!string.IsNullOrEmpty(id))
            {
                cacheObject = "UserContact" + id;
            }
            if (cache != null && cache[cacheObject] == null)
            {
                BaseUserContactManager manager = new DotNet.Business.BaseUserContactManager(BaseUserContactEntity.TableName);
                result = manager.GetObject(id);
                // 若是空的不用缓存，继续读取实体
                if (result != null)
                {
                    cache.Add(cacheObject, result, null, DateTime.Now.AddMinutes(10), TimeSpan.Zero, CacheItemPriority.Normal, null);
                    // System.Console.WriteLine(System.DateTime.Now.ToString(BaseSystemInfo.DateTimeFormat) + " cache UserContact " + id);
                }
            }
            result = cache[cacheObject] as BaseUserContactEntity;

            return result;
        }

        private static void SetCache(BaseUserContactEntity entity)
        {
            if (entity != null && !string.IsNullOrWhiteSpace(entity.Id))
            {
                System.Web.Caching.Cache cache = HttpRuntime.Cache;
                string key = "UserContact" + entity.Id;
                cache.Add(key, entity, null, DateTime.Now.AddMinutes(10), TimeSpan.Zero, CacheItemPriority.Normal, null);
            }
        }

        #region public static void ClearCache() 清除缓存
        /// <summary>
        /// 清除缓存
        /// </summary>
        public static void ClearCache()
        {
            lock (BaseSystemInfo.UserLock)
            {
                if (HttpContext.Current.Cache[BaseUserContactEntity.TableName] != null)
                {
                    HttpContext.Current.Cache.Remove(BaseUserContactEntity.TableName);
                }
            }
        }
        #endregion

        #region public static List<BaseUserEntity> GetEntities() 获取用户表，从缓存读取
        /// <summary>
        /// 获取用户表，从缓存读取
        /// </summary>
        public static List<BaseUserContactEntity> GetEntities()
        {
            if (HttpContext.Current.Session == null || HttpContext.Current.Cache[BaseUserContactEntity.TableName] == null)
            {
                lock (BaseSystemInfo.UserLock)
                {
                    if (HttpContext.Current.Session == null || HttpContext.Current.Cache[BaseUserContactEntity.TableName] == null)
                    {
                        // 读取目标表中的数据
                        List<BaseUserContactEntity> entityList = null;
                        BaseUserContactManager manager = new DotNet.Business.BaseUserContactManager(BaseUserContactEntity.TableName);
                        entityList = manager.GetList<BaseUserContactEntity>();
                        // 这个是没写过期时间的方法
                        // HttpContext.Current.Cache[tableName] = entityList;
                        // 设置过期时间为8个小时，第2天若有不正常的自动就可以正常了
                        HttpContext.Current.Cache.Add(BaseUserContactEntity.TableName, entityList, null, DateTime.Now.AddMinutes(10), TimeSpan.Zero, CacheItemPriority.Normal, null);
                    }
                }
            }
            return HttpContext.Current.Cache[BaseUserContactEntity.TableName] as List<BaseUserContactEntity>;
        }
        #endregion
    }
}