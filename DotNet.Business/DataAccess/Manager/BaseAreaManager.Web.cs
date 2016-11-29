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
    /// BaseAreaManager
    /// 地区表(省、市、县)
    /// 
    /// 修改纪录
    /// 
    ///	版本：1.0 2014.10.15    JiRiGaLa    从缓存读取。
    ///	
    /// <author>  
    ///		<name>JiRiGaLa</name>
    ///		<date>2014.10.15</date>
    /// </author> 
    /// </remarks>
    public partial class BaseAreaManager
    {
        /// <summary>
        /// 从缓存获取获取实体
        /// </summary>
        /// <param name="id">主键</param>
        public static BaseAreaEntity GetObjectByCache(string id)
        {
            BaseAreaEntity result = null;
            System.Web.Caching.Cache cache = HttpRuntime.Cache;
            string cacheObject = "Area";
            if (!string.IsNullOrEmpty(id))
            {
                cacheObject = "Area" + id;
            }
            if (cache != null && cache[cacheObject] == null)
            {
                lock (locker)
                {
                    if (cache != null && cache[cacheObject] == null)
                    {
                        BaseAreaManager manager = new DotNet.Business.BaseAreaManager(BaseAreaEntity.TableName);
                        result = manager.GetObject(id);
                        // 若是空的不用缓存，继续读取实体
                        if (result != null)
                        {
                            cache.Add(cacheObject, result, null, DateTime.Now.AddMinutes(5), TimeSpan.Zero, CacheItemPriority.Normal, null);
                            System.Console.WriteLine(System.DateTime.Now.ToString(BaseSystemInfo.DateTimeFormat) + " cache Area " + id);
                        }
                    }
                }
            }
            result = cache[cacheObject] as BaseAreaEntity;
            return result;
        }

        public List<BaseAreaEntity> GetListByParentByCache(string parentId)
        {
            List<BaseAreaEntity> result = null;
            System.Web.Caching.Cache cache = HttpRuntime.Cache;
            string cacheObject = "AreaList";
            if (!string.IsNullOrWhiteSpace(parentId))
            {
                cacheObject = "AreaList" + parentId;
            }
            if (cache != null && cache[cacheObject] == null)
            {
                lock (locker)
                {
                    if (cache != null && cache[cacheObject] == null)
                    {
                        result = this.GetListByParent(parentId);
                        cache.Add(cacheObject, result, null, DateTime.Now.AddMinutes(15), TimeSpan.Zero, CacheItemPriority.Normal, null);
                        System.Console.WriteLine(System.DateTime.Now.ToString(BaseSystemInfo.DateTimeFormat) + " cache AreaList");
                    }
                }
            }
            result = cache[cacheObject] as List<BaseAreaEntity>;
            return result;
        }
    }
}