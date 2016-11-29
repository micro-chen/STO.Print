//-------------------------------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2015 , STO , Ltd .
//-------------------------------------------------------------------------------------

using System;
using System.Runtime.Caching;

namespace STO.Print.Utilities
{
    /*
     * /// <summary>
        /// 根据用户的ID，获取用户的全名，并放到缓存里面
        /// </summary>
        /// <param name="userId">用户的ID</param>
        /// <returns></returns>
        public static string GetUserFullName(string userId)
        {
            string key = "Security_UserFullName" + userId;
            string fullName = MemoryCacheHelper.GetCacheItem<string>(key,
                delegate()
                {
                    return "杨恒连";
                },
                new TimeSpan(0, 30, 0));//30分钟过期
            return fullName;
        }
    */
    /// <summary>
    /// 基于MemoryCache的缓存辅助类
    /// http://www.cnblogs.com/wuhuacong/p/3526335.html 
    /// </summary>
    public static class MemoryCacheHelper
    {
        private static readonly Object Locker = new object();

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="cachePopulate"></param>
        /// <param name="slidingExpiration"></param>
        /// <param name="absoluteExpiration"></param>
        /// <returns></returns>
        public static T GetCacheItem<T>(String key, Func<T> cachePopulate, TimeSpan? slidingExpiration = null, DateTime? absoluteExpiration = null)
        {
            if (String.IsNullOrWhiteSpace(key)) throw new ArgumentException("Invalid cache key");
            if (cachePopulate == null) throw new ArgumentNullException("cachePopulate");
            if (slidingExpiration == null && absoluteExpiration == null) throw new ArgumentException("Either a sliding expiration or absolute must be provided");

            if (MemoryCache.Default[key] == null)
            {
                lock (Locker)
                {
                    if (MemoryCache.Default[key] == null)
                    {
                        var item = new CacheItem(key, cachePopulate());
                        var policy = CreatePolicy(slidingExpiration, absoluteExpiration);

                        MemoryCache.Default.Add(item, policy);
                    }
                }
            }

            return (T)MemoryCache.Default[key];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="slidingExpiration"></param>
        /// <param name="absoluteExpiration"></param>
        /// <returns></returns>
        private static CacheItemPolicy CreatePolicy(TimeSpan? slidingExpiration, DateTime? absoluteExpiration)
        {
            var policy = new CacheItemPolicy();

            if (absoluteExpiration.HasValue)
            {
                policy.AbsoluteExpiration = absoluteExpiration.Value;
            }
            else if (slidingExpiration.HasValue)
            {
                policy.SlidingExpiration = slidingExpiration.Value;
            }

            policy.Priority = CacheItemPriority.Default;

            return policy;
        }
    }
}
