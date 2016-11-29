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
    /// BaseUserManager
    /// 选项管理
    /// 
    /// 修改纪录
    /// 
    ///	版本：1.0 2012.12.15    JiRiGaLa    选项管理从缓存读取，通过编号显示名称的函数完善。
    ///	
    /// <author>  
    ///		<name>JiRiGaLa</name>
    ///		<date>2012.12.15</date>
    /// </author> 
    /// </remarks>
    public partial class BaseUserManager
    {
        public static BaseUserEntity GetObjectByOpenIdByCache(string openId)
        {
            BaseUserEntity result = null;

            string userId = string.Empty;
            if (!string.IsNullOrWhiteSpace(openId))
            {
                if (result == null)
                {
                    // 若没获取到用户？到数据库里查一次
                    BaseUserLogOnManager userLogOnManager = new BaseUserLogOnManager();
                    userId = userLogOnManager.GetIdByOpenId(openId);
                    if (!string.IsNullOrWhiteSpace(userId))
                    {
                        result = GetObjectByCache(userId);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 从缓存获取获取实体
        /// </summary>
        /// <param name="id">主键</param>
        public static BaseUserEntity GetObjectByCache(string id)
        {
            BaseUserEntity result = null;

            System.Web.Caching.Cache cache = HttpRuntime.Cache;
            string cacheObject = "User";
            if (!string.IsNullOrEmpty(id))
            {
                cacheObject = "User" + id;
            }
            if (cache == null || cache[cacheObject] == null)
            {
                BaseUserManager manager = new DotNet.Business.BaseUserManager(BaseUserEntity.TableName);
                result = manager.GetObject(id);
                // 若是空的不用缓存，继续读取实体
                if (result != null)
                {
                    cache.Add(cacheObject, result, null, DateTime.Now.AddMinutes(30), TimeSpan.Zero, CacheItemPriority.Normal, null);
                    // System.Console.WriteLine(System.DateTime.Now.ToString(BaseSystemInfo.DateTimeFormat) + " cache User " + id);
                }
            }
            result = cache[cacheObject] as BaseUserEntity;

            return result;
        }

        #region public static void ClearCache() 清除缓存
        /// <summary>
        /// 清除缓存
        /// </summary>
        public static void ClearCache()
        {
            lock (BaseSystemInfo.UserLock)
            {
                if (HttpContext.Current.Cache[BaseUserEntity.TableName] != null)
                {
                    HttpContext.Current.Cache.Remove(BaseUserEntity.TableName);
                }
            }
        }
        #endregion

        #region public static List<BaseUserEntity> GetEntities() 获取用户表，从缓存读取
        /// <summary>
        /// 获取用户表，从缓存读取
        /// </summary>
        public static List<BaseUserEntity> GetEntities()
        {
            if (HttpContext.Current.Session == null || HttpContext.Current.Cache[BaseUserEntity.TableName] == null)
            {
                lock (BaseSystemInfo.UserLock)
                {
                    if (HttpContext.Current.Session == null || HttpContext.Current.Cache[BaseUserEntity.TableName] == null)
                    {
                        // 读取目标表中的数据
                        List<BaseUserEntity> entityList = null;
                        BaseUserManager manager = new DotNet.Business.BaseUserManager(BaseUserEntity.TableName);
                        entityList = manager.GetList<BaseUserEntity>();
                        // 这个是没写过期时间的方法
                        // HttpContext.Current.Cache[tableName] = entityList;
                        // 设置过期时间为8个小时，第2天若有不正常的自动就可以正常了
                        HttpContext.Current.Cache.Add(BaseUserEntity.TableName, entityList, null, DateTime.Now.AddHours(8), TimeSpan.Zero, CacheItemPriority.Normal, null);
                    }
                }
            }
            return HttpContext.Current.Cache[BaseUserEntity.TableName] as List<BaseUserEntity>;
        }
        #endregion

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="entity">用户实体</param>
        public static void SetCache(BaseUserEntity entity)
        {
            string key = string.Empty;
            if (entity != null && !string.IsNullOrWhiteSpace(entity.Id))
            {
                System.Web.Caching.Cache cache = HttpRuntime.Cache;

                key = "user" + entity.Id;
                cache.Add(key, entity, null, DateTime.Now.AddMinutes(30), TimeSpan.Zero, CacheItemPriority.Normal, null);
                
                key = "userByCode" + entity.Code;
                cache.Add(key, entity.Id.ToString(), null, DateTime.Now.AddMinutes(30), TimeSpan.Zero, CacheItemPriority.Normal, null);
                
                key = "userByCompanyIdByCode" + entity.CompanyId + "_" + entity.Code;
                cache.Add(key, entity.Id.ToString(), null, DateTime.Now.AddMinutes(30), TimeSpan.Zero, CacheItemPriority.Normal, null);
                
                string companyCode = BaseOrganizeManager.GetCodeByCache(entity.CompanyId);

                if (!string.IsNullOrEmpty(companyCode))
                {
                    key = "userByCompanyCodeByCode" + companyCode + "_" + entity.Code;
                    cache.Add(key, entity.Id.ToString(), null, DateTime.Now.AddMinutes(30), TimeSpan.Zero, CacheItemPriority.Normal, null);
                }
            }
        }
    }
}