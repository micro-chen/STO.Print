//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd.  
//-----------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data;
using ServiceStack.Redis;

namespace DotNet.Business
{
    using DotNet.Model;
    using DotNet.Utilities;

    /// <remarks>
    /// BaseMessageManager
    /// 消息的缓存服务
    /// 
    /// 修改记录
    /// 
    ///	版本：1.0 2015.09.26  JiRiGaLa    消息的缓存服务。
    ///	
    /// <author>  
    ///		<name>JiRiGaLa</name>
    ///		<date>2015.09.26</date>
    /// </author> 
    /// </remarks>
    public partial class BaseMessageManager
    {
        /// <summary>
        /// 缓存预热,强制重新缓存
        /// </summary>
        /// <returns>影响行数</returns>
        public static int CachePreheating()
        {
            int result = 0;

            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>(BaseMessageEntity.FieldCategoryCode, MessageCategory.Receiver.ToString()));
            parameters.Add(new KeyValuePair<string, object>(BaseMessageEntity.FieldIsNew, (int)MessageStateCode.New));
            parameters.Add(new KeyValuePair<string, object>(BaseMessageEntity.FieldDeletionStateCode, 0));

            // 2015-09-27 吉日嘎拉 把所有的未阅读的消息都缓存起来的代码。
            BaseMessageManager manager = new BaseMessageManager();
            using (IDataReader dataReader = manager.ExecuteReader(parameters, BaseMessageEntity.FieldCreateOn))
            {
                while (dataReader.Read())
                {
                    BaseMessageEntity entity = BaseEntity.Create<BaseMessageEntity>(dataReader, false);
                    // 2015-09-30 吉日嘎拉 两个月以上的信息，意义不大了，可以考虑不缓存了
                    if (entity != null)
                    {
                        manager.CacheProcessing(entity);
                        result++;
                        // System.Console.WriteLine(result.ToString() + " : " + entity.Contents);
                    }
                }
                dataReader.Close();
            }

            return result;
        }

        public static BaseMessageEntity GetCacheByKey(string key)
        {
            BaseMessageEntity result = null;

            if (!string.IsNullOrWhiteSpace(key))
            {
                using (var redisClient = PooledRedisHelper.GetMessageReadOnlyClient())
                {
                    result = redisClient.Get<BaseMessageEntity>(key);
                }
            }

            return result;
        }

        private static void SetCache(BaseMessageEntity entity)
        {
            if (entity != null && !string.IsNullOrWhiteSpace(entity.Id))
            {
                string key = string.Empty;
                using (var redisClient = PooledRedisHelper.GetMessageClient())
                {
                    SetCache(redisClient, entity);
                }
            }
        }

        private static void SetCache(IRedisClient redisClient, BaseMessageEntity entity)
        {
            if (entity != null && !string.IsNullOrWhiteSpace(entity.Id))
            {
                // string key = string.Empty;
                string key = "m";
                // 默认缓存三个月，三个月不看的消息，意义也不大了，也浪费内存空间了。
                // key = "Message" + entity.Id;
                // 2015-09-27 吉日嘎拉 用简短的Key，这样效率高一些，节约内存
                key = key + entity.Id;
                redisClient.Set<BaseMessageEntity>(key, entity, DateTime.Now.AddDays(15));
            }
        }

        /// <summary>
        /// 从缓存获取获取实体
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>实体</returns>
        public static BaseMessageEntity GetObjectByCache(string id)
        {
            BaseMessageEntity result = null;
            // string cacheKey = "Message";
            string cacheKey = "m";
            if (!string.IsNullOrEmpty(id))
            {
                cacheKey = cacheKey + id;
                result = GetCacheByKey(cacheKey);
                if (result == null || string.IsNullOrWhiteSpace(result.Id))
                {
                    BaseMessageManager manager = new DotNet.Business.BaseMessageManager();
                    result = manager.GetObject(id);
                    // 若是空的不用缓存，继续读取实体
                    if (result != null)
                    {
                        SetCache(result);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 把未阅读的消息，进行缓存处理
        /// 2015-09-27 吉日嘎拉 优化代码
        /// </summary>
        /// <param name="entity">消息</param>
        public void CacheProcessing(BaseMessageEntity entity, DateTime? expireAt = null)
        {
            using (var redisClient = PooledRedisHelper.GetMessageClient())
            {
                CacheProcessing(redisClient, entity, expireAt);
            }
        }

        /// <summary>
        /// 把未阅读的消息，进行缓存处理
        /// 2015-10-08 吉日嘎拉 优化代码
        /// </summary>
        /// <param name="entity">消息</param>
        public void CacheProcessing(IRedisClient redisClient, BaseMessageEntity entity, DateTime? expireAt = null)
        {
            // 需要把消息本身放一份在缓存服务器里
            if (entity != null)
            {
                SetCache(redisClient, entity);

                if (!string.IsNullOrEmpty(entity.ReceiverId))
                {
                    // 把消息的主键放在有序集合里, 尽量存放小数字，不要太长了
                    redisClient.AddItemToSortedSet(entity.ReceiverId, entity.Id, entity.CreateOn.Value.Ticks - (new DateTime(2015, 10, 1)).Ticks);
                }
                if (!string.IsNullOrEmpty(entity.CreateUserId))
                {
                    if (!expireAt.HasValue)
                    {
                        expireAt = DateTime.Now.AddDays(15);
                    }
                    // 设置一个需要阅读的标志(过期时间)
                    redisClient.ExpireEntryAt(entity.CreateUserId, expireAt.Value);
                }

                if (!string.IsNullOrEmpty(entity.CreateUserId))
                {
                    // 设置一个最近联络人标注，把最近联系人放在有序集合里(过期时间)
                    // 设置过期时间，防止长时间不能释放
                    redisClient.AddItemToSortedSet("r" + entity.CreateUserId, entity.ReceiverId, entity.CreateOn.Value.Ticks - (new DateTime(2015, 10, 1)).Ticks);
                    redisClient.ExpireEntryAt("r" + entity.CreateUserId, DateTime.Now.AddDays(15));
                }

                if (!string.IsNullOrEmpty(entity.ReceiverId))
                {
                    redisClient.AddItemToSortedSet("r" + entity.ReceiverId, entity.CreateUserId, entity.CreateOn.Value.Ticks - (new DateTime(2015, 10, 1)).Ticks);
                    redisClient.ExpireEntryAt("r" + entity.ReceiverId, DateTime.Now.AddDays(15));
                }

                // 把多余的数据删除掉，没必要放太多的历史数据，最近联系人列表里
            }
        }
    }
}