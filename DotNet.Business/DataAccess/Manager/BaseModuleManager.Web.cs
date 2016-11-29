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
        #region public static void ClearCache() 清除缓存
        /// <summary>
        /// 清除缓存
        /// </summary>
        public static void ClearCache()
        {
            lock (BaseSystemInfo.UserLock)
            {
                if (HttpContext.Current.Cache[BaseModuleEntity.TableName] != null)
                {
                    HttpContext.Current.Cache.Remove(BaseModuleEntity.TableName);
                }
            }
        }
        #endregion

        // 当前的锁
        private static object locker = new Object();


        /// <summary>
        /// 从缓存获取获取实体
        /// </summary>
        /// <param name="id">主键</param>
        public static BaseModuleEntity GetObjectByCache(string id)
        {
            return GetObjectByCache("Base", id);
        }

        /// <summary>
        /// 从缓存获取获取实体
        /// </summary>
        /// <param name="id">主键</param>
        public static BaseModuleEntity GetObjectByCache(BaseUserInfo userInfo, string id)
        {
            return GetObjectByCache(userInfo.SystemCode, id);
        }

        /// <summary>
        /// 从缓存获取获取实体
        /// </summary>
        /// <param name="id">主键</param>
        public static BaseModuleEntity GetObjectByCache(string systemCode, string id)
        {
            if (string.IsNullOrWhiteSpace(systemCode))
            {
                systemCode = "Base";
            }

            BaseModuleEntity result = null;
            System.Web.Caching.Cache cache = HttpRuntime.Cache;
            string cacheObject = systemCode + "Module";
            if (!string.IsNullOrEmpty(id))
            {
                cacheObject = systemCode + "Module" + id;
            }
            if (cache != null && cache[cacheObject] == null)
            {
                lock (locker)
                {
                    if (cache != null && cache[cacheObject] == null)
                    {
                        BaseModuleManager manager = new DotNet.Business.BaseModuleManager(BaseModuleEntity.TableName);
                        result = manager.GetObject(id);
                        // 若是空的不用缓存，继续读取实体
                        if (result != null)
                        {
                            cache.Add(cacheObject, result, null, DateTime.Now.AddMinutes(10), TimeSpan.Zero, CacheItemPriority.Normal, null);
                            System.Console.WriteLine(System.DateTime.Now.ToString(BaseSystemInfo.DateTimeFormat) + " cache " + systemCode + " Module " + id);
                        }
                    }
                }
            }
            result = cache[cacheObject] as BaseModuleEntity;
            return result;
        }

        #region public static List<BaseModuleEntity> GetEntities() 获取模块菜单表，从缓存读取
        /// <summary>
        /// 获取模块菜单表，从缓存读取
        /// </summary>
        public static List<BaseModuleEntity> GetEntities()
        {
            if (HttpContext.Current.Session == null || HttpContext.Current.Cache[BaseModuleEntity.TableName] == null)
            {
                lock (BaseSystemInfo.UserLock)
                {
                    if (HttpContext.Current.Session == null || HttpContext.Current.Cache[BaseModuleEntity.TableName] == null)
                    {
                        // 读取目标表中的数据
                        List<BaseModuleEntity> entityList = null;
                        string tableName = BaseSystemInfo.SystemCode + "Module";
                        BaseModuleManager manager = new DotNet.Business.BaseModuleManager(tableName);
                        entityList = manager.GetList<BaseModuleEntity>();
                        // 这个是没写过期时间的方法
                        // HttpContext.Current.Cache[tableName] = entityList;
                        // 设置过期时间为8个小时，第2天若有不正常的自动就可以正常了
                        HttpContext.Current.Cache.Add(BaseModuleEntity.TableName, entityList, null, DateTime.Now.AddMinutes(10), TimeSpan.Zero, CacheItemPriority.Normal, null);
                    }
                }
            }
            return HttpContext.Current.Cache[BaseModuleEntity.TableName] as List<BaseModuleEntity>;
        }
        #endregion

        #region public static string GetRealName(string id) 通过编号获取选项的显示内容
        /// <summary>
        /// 通过编号获取选项的显示内容
        /// 这里是进行了内存缓存处理，减少数据库的I/O处理，提高程序的运行性能，
        /// 若有数据修改过，重新启动一下程序就可以了，这些基础数据也不是天天修改来修改去的，
        /// 所以没必要过度担忧，当然有需要时也可以写个刷新缓存的程序
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>显示值</returns>
        public static string GetRealName(string id)
        {
            string result = id;
            if (!string.IsNullOrEmpty(id))
            {
                List<BaseModuleEntity> entityList = GetEntities();
                BaseModuleEntity moduleEntity = entityList.FirstOrDefault(entity => entity.Id.HasValue && entity.Id.ToString().Equals(id));
                if (moduleEntity != null)
                {
                    result = moduleEntity.FullName;
                }
            }
            return result;
        }
        #endregion
    }
}