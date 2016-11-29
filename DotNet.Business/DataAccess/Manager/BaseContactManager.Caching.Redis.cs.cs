//-----------------------------------------------------------------------
// <copyright file="BaseContactManager.cs" company="Hairihan">
//     Copyright (c) 2015 , All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Script.Serialization;

namespace DotNet.Business
{
    using DotNet.Model;
    using DotNet.Utilities;

    /// <summary>
    /// BaseContactManager
    /// 联络单
    ///
    /// 修改纪录
    ///
    ///		2015-11-19 版本：1.1 JiRiGaLa 增加移除缓存的功能。
    ///		2015-11-18 版本：1.0 JiRiGaLa 创建分离方法。
    ///		
    ///
    /// 版本：1.0
    ///
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2015-11-18</date>
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
            using (var redisClient = PooledRedisHelper.GetContactClient())
            {
                redisClient.Remove(key);
            }
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

        private static void SetListCache(string key, List<BaseContactEntity> list)
        {
            if (!string.IsNullOrWhiteSpace(key) && list != null && list.Count > 0)
            {
                using (var redisClient = PooledRedisHelper.GetContactClient())
                {
                    JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                    string keyValue = javaScriptSerializer.Serialize(list);
                    redisClient.Set(key, keyValue, DateTime.Now.AddMinutes(10));
                }
            }
        }

        private static List<BaseContactEntity> GetListCache(string key)
        {
            List<BaseContactEntity> result = null;

            if (!string.IsNullOrWhiteSpace(key))
            {
                using (var redisClient = PooledRedisHelper.GetContactClient())
                {
                    string keyValue = redisClient.Get<string>(key);
                    if (!string.IsNullOrWhiteSpace(keyValue))
                    {
                        JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                        result = javaScriptSerializer.Deserialize<List<BaseContactEntity>>(keyValue);
                    }
                }
            }

            return result;
        }
    }
}
