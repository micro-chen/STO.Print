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
        // 当前的锁
        private static object locker = new Object();

        /*
        
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
                lock (locker)
                {
                    if (cache != null && cache[cacheObject] == null)
                    {
                        BaseUserContactManager manager = new DotNet.Business.BaseUserContactManager(BaseUserContactEntity.TableName);
                        result = manager.GetObject(id);
                        // 若是空的不用缓存，继续读取实体
                        if (result != null)
                        {
                            cache.Add(cacheObject, result, null, DateTime.Now.AddMinutes(10), TimeSpan.Zero, CacheItemPriority.Normal, null);
                            System.Console.WriteLine(System.DateTime.Now.ToString(BaseSystemInfo.DateTimeFormat) + " cache UserContact " + id);
                        }
                    }
                }
            }
            result = cache[cacheObject] as BaseUserContactEntity;
            return result;
        }
        
        #region public static string GetEmailByCache(string id) 通过主键获取电子邮件
        /// <summary>
        /// 通过主键获取姓名
        /// 这里是进行了内存缓存处理，减少数据库的I/O处理，提高程序的运行性能，
        /// 若有数据修改过，重新启动一下程序就可以了，这些基础数据也不是天天修改来修改去的，
        /// 所以没必要过度担忧，当然有需要时也可以写个刷新缓存的程序
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>显示值</returns>
        public static string GetEmailByCache(string id)
        {
            string result = id;
            if (!string.IsNullOrEmpty(id))
            {
                BaseUserContactEntity userContactEntity = GetEntities().FirstOrDefault(entity => !string.IsNullOrEmpty(entity.Id) && entity.Id.Equals(id));
                if (userContactEntity != null)
                {
                    result = userContactEntity.Email;
                }
            }
            return result;
        }
        #endregion

        */

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