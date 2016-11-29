//-----------------------------------------------------------------------
// <copyright file="BaseContactManager.cs" company="Hairihan">
//     Copyright (C) 2016 , All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace DotNet.Business
{
    using DotNet.Model;

    /// <summary>
    /// BaseContactManager
    /// 联络单
    ///
    /// 修改记录
    ///
    ///		2015-12-03 版本：1.2 JiRiGaLa List<BaseContactEntity> 读写方法改进。
    ///		2015-11-19 版本：1.1 JiRiGaLa 增加移除缓存的功能。
    ///		2015-11-18 版本：1.0 JiRiGaLa 创建分离方法。
    ///		
    ///
    /// 版本：1.2
    ///
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2015-12-03</date>
    /// </author>
    /// </summary>
    public partial class BaseContactManager : BaseManager, IBaseManager
    {
        /// <summary>
        /// 移除缓存
        /// 2015-11-19 吉日嘎拉 增加移除缓存的功能
        /// </summary>
        /// <param name="parentId">分类主键</param>
        /// <param name="topLimit">获取前几个</param>
        public static void RemoveTopListCache(string parentId, int topLimit)
        {
            string key = "BaseContact";
            if (!string.IsNullOrEmpty(parentId))
            {
                key = key + "_" + parentId + "_" + topLimit.ToString();
            }
            using (var redisClient = PooledRedisHelper.GetClient())
            {
                redisClient.Remove(key);
            }
        }

        /// <summary>
        /// 移除缓存
        /// 2015-12-14 吉日嘎拉 增加移除缓存的功能
        /// </summary>
        /// <param name="companyId">公司主键</param>
        /// <param name="topLimit">获取前几个</param>
        public static void RemoveTopListByCompanyIdCache(string companyId, int topLimit)
        {
            string key = "BaseContact";
            if (!string.IsNullOrEmpty(companyId))
            {
                key = key + "_" + companyId + "_" + topLimit.ToString();
            }
            using (var redisClient = PooledRedisHelper.GetClient())
            {
                redisClient.Remove(key);
            }
        }

        private static void SetListCache(string key, List<BaseContactEntity> list)
        {
            if (!string.IsNullOrWhiteSpace(key) && list != null && list.Count > 0)
            {
                using (var redisClient = PooledRedisHelper.GetClient())
                {
                    redisClient.Set<List<BaseContactEntity>>(key, list, DateTime.Now.AddMinutes(10));
                }
            }
        }

        private static List<BaseContactEntity> GetListCache(string key)
        {
            List<BaseContactEntity> result = null;

            if (!string.IsNullOrWhiteSpace(key))
            {
                using (var redisClient = PooledRedisHelper.GetClient())
                {
                    result = redisClient.Get<List<BaseContactEntity>>(key);
                }
                if (result != null)
                {
                    foreach (var entity in result)
                    {
                        entity.CreateOn = entity.CreateOn.Value.ToLocalTime();
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 从缓存高速获取新闻列表
        /// </summary>
        /// <param name="parentId">分类主键</param>
        /// <param name="topLimit">获取前几个</param>
        /// <param name="containContents">是否要内容</param>
        /// <returns>新闻列表</returns>
        public static List<BaseContactEntity> GetTopListByCache(string parentId, int topLimit, bool containContents = false)
        {
            List<BaseContactEntity> result = null;

            string key = "BaseContact";
            if (!string.IsNullOrEmpty(parentId))
            {
                key = key + "_" + parentId + "_" + topLimit.ToString();
            }

            // 读取缓存
            result = GetListCache(key);

            if (result == null)
            {
                BaseContactManager manager = new DotNet.Business.BaseContactManager();
                result = manager.GetTopList(parentId, topLimit, containContents);

                // 若是空的不用缓存，继续读取实体
                if (result != null)
                {
                    // 设置缓存
                    SetListCache(key, result);
                }
            }

            return result;
        }

        public static List<BaseContactEntity> GetTopListByCompanyIdByCache(string companyId, string parentId, int topLimit, bool containContents = false)
        {
            List<BaseContactEntity> result = null;

            string key = "BaseContact";
            if (!string.IsNullOrEmpty(companyId))
            {
                key = key + "_" + companyId + "_" + topLimit.ToString();
            }

            // 读取缓存
            result = GetListCache(key);

            if (result == null)
            {
                BaseContactManager manager = new DotNet.Business.BaseContactManager();
                result = manager.GetTopListByCompanyId(companyId, parentId, topLimit, containContents);

                // 若是空的不用缓存，继续读取实体
                if (result != null)
                {
                    // 设置缓存
                    SetListCache(key, result);
                }
            }

            return result;
        }
    }
}
