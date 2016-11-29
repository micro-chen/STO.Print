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
    /// BaseUserManager
    /// 用户缓存管理
    /// 
    /// 修改记录
    /// 
    ///	版本：1.3 2016.01.18    JiRiGaLa    读写分离，配置文件优化。
    ///	版本：1.2 2016.01.07    JiRiGaLa    缓存服务器，读写分离。
    ///	版本：1.1 2015.06.15    JiRiGaLa    增加强制刷新缓存的功能。
    ///	版本：1.0 2015.01.06    JiRiGaLa    缓存优化。
    ///	
    /// <author>  
    ///		<name>JiRiGaLa</name>
    ///		<date>2016.01.18</date>
    /// </author> 
    /// </remarks>
    public partial class BaseUserManager
    {
        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="key">缓存主键</param>
        /// <returns>用户信息</returns>
        public static BaseUserEntity GetCacheByKey(string key)
        {
            BaseUserEntity result = null;

            if (!string.IsNullOrWhiteSpace(key))
            {
                // 2016-01-08 吉日嘎拉 实现只读连接，读写分离
                using (var redisClient = PooledRedisHelper.GetReadOnlyClient())
                {
                    result = GetCacheByKey(redisClient, key);
                }
            }

            return result;
        }

        /// <summary>
        /// 获取缓存
        /// 20151007 吉日嘎拉，需要在一个连接上进行大量的操作
        /// </summary>
        /// <param name="key">缓存主键</param>
        /// <returns>用户信息</returns>
        private static BaseUserEntity GetCacheByKey(IRedisClient redisClient, string key)
        {
            BaseUserEntity result = null;

            if (!string.IsNullOrWhiteSpace(key))
            {
                result = redisClient.Get<BaseUserEntity>(key);
            }

            return result;
        }

        /// <summary>
        /// 通过唯一用户名从Reids中获取
        /// </summary>
        /// <param name="nickName"></param>
        /// <returns></returns>
        public static BaseUserEntity GetObjectByNickNameByCache(string nickName)
        {
            // 2016-01-25 黄斌 添加, 从缓存中 通过唯一用户名获取
            BaseUserEntity result = null;
            if (string.IsNullOrEmpty(nickName))
            {
                return result;
            }

            //存取Redis中的U:BNN:  
            string key = "User:ByNickName:" + nickName.ToLower();
            string id = string.Empty;
            using (var redisClient = PooledRedisHelper.GetReadOnlyClient())
            {
                id = redisClient.Get<string>(key);
                if (!string.IsNullOrWhiteSpace(id))
                {
                    result = GetObjectByCache(redisClient, id);
                }
            }

            if (result == null)
            {
                BaseUserManager manager = new BaseUserManager();
                result = manager.GetObjectByNickName(nickName);
                if (result != null)
                {
                    result.NickName = result.NickName.ToLower();
                    SetCache(result);
                }
            }

            return result;
        }

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="entity">用户实体</param>
        public static void SetCache(BaseUserEntity entity)
        {
            using (var redisClient = PooledRedisHelper.GetClient())
            {
                SetCache(redisClient, entity);
            }
        }

        /// <summary>
        /// 设置缓存
        /// 20151007 吉日嘎拉，需要在一个连接上进行大量的操作
        /// 20160128 吉日嘎拉，一些空调间的判断。
        /// </summary>
        /// <param name="entity">用户实体</param>
        public static void SetCache(IRedisClient redisClient, BaseUserEntity entity)
        {
            string key = string.Empty;

            if (entity != null && !string.IsNullOrWhiteSpace(entity.Id))
            {
                key = "User:" + entity.Id;
                redisClient.Set<BaseUserEntity>(key, entity, DateTime.Now.AddDays(7));

                if (!string.IsNullOrEmpty(entity.NickName))
                {
                    key = "User:ByNickName:" + entity.NickName.ToLower();
                    redisClient.Set<string>(key, entity.Id, DateTime.Now.AddDays(7));
                }

                if (!string.IsNullOrEmpty(entity.Code))
                {
                    key = "User:ByCode:" + entity.Code;
                    redisClient.Set<string>(key, entity.Id, DateTime.Now.AddHours(8));

                    key = "User:ByCompanyId:ByCode" + entity.CompanyId + ":" + entity.Code;
                    redisClient.Set<string>(key, entity.Id, DateTime.Now.AddHours(8));
                }

                string companyCode = BaseOrganizeManager.GetCodeByCache(entity.CompanyId);
                if (!string.IsNullOrEmpty(companyCode))
                {
                    key = "User:ByCompanyCode:ByCode" + companyCode + ":" + entity.Code;
                    redisClient.Set<string>(key, entity.Id, DateTime.Now.AddHours(8));
                }

                System.Console.WriteLine(entity.Id + " : " + entity.RealName);
            }
        }

        public static BaseUserEntity GetObjectByCache(IRedisClient redisClient, string id)
        {
            BaseUserEntity result = null;

            if (string.IsNullOrEmpty(id))
            {
                return result;
            }

            string key = "User:" + id;
            result = GetCacheByKey(redisClient, key);

            // 若没有读取到重新设置缓存
            if (result == null)
            {
                result = SetCache(id);
            }

            return result;
        }

        /// <summary>
        /// 从缓存获取获取实体
        /// 20151007 吉日嘎拉，需要在一个连接上进行大量的操作
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>实体</returns>
        public static BaseUserEntity GetObjectByCache(string id, bool fefreshCache = false)
        {
            BaseUserEntity result = null;

            if (string.IsNullOrEmpty(id))
            {
                return result;
            }

            string key = "User:" + id;

            if (!fefreshCache)
            {
                result = GetCacheByKey(key);
            }

            // 若没有读取到重新设置缓存
            if (result == null)
            {
                result = SetCache(id);
            }

            return result;
        }

        /// <summary>
        /// 重新设置缓存（重新强制设置缓存）可以提供外部调用的
        /// 20151007 吉日嘎拉，需要在一个连接上进行大量的操作
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>用户信息</returns>
        private static BaseUserEntity SetCache(IRedisClient redisClient, string id)
        {
            BaseUserEntity result = null;

            BaseUserManager manager = new BaseUserManager();
            result = manager.GetObject(id);
            if (result != null)
            {
                SetCache(redisClient, result);
            }

            return result;
        }

        public static BaseUserEntity GetObjectByCodeByCache(string userCode)
        {
            BaseUserEntity result = null;

            string key = "User:ByCode:" + userCode;

            string id = string.Empty;
            using (var redisClient = PooledRedisHelper.GetReadOnlyClient())
            {
                id = redisClient.Get<string>(key);
                if (!string.IsNullOrWhiteSpace(id))
                {
                    result = GetObjectByCache(redisClient, id);
                }
            }

            if (result == null)
            {
                BaseUserManager manager = new BaseUserManager();
                result = manager.GetObjectByCode(userCode);

                SetCache(result);
            }

            return result;
        }

        public static string GetIdByCodeByCache(string userCode)
        {
            string result = null;

            BaseUserEntity userEntity = GetObjectByCodeByCache(userCode);
            if (userEntity != null)
            {
                result = userEntity.Id;
            }

            return result;
        }

        public static BaseUserEntity GetObjectByCompanyIdByCodeByCache(string companyId, string userCode)
        {
            BaseUserEntity result = null;
            // 检查参数有效性
            if (string.IsNullOrWhiteSpace(companyId) || string.IsNullOrWhiteSpace(userCode))
            {
                return result;
            }

            string key = "User:ByCompanyId:ByCode" + companyId + ":" + userCode;
            string id = string.Empty;
            using (var redisClient = PooledRedisHelper.GetReadOnlyClient())
            {
                id = redisClient.Get<string>(key);

                if (!string.IsNullOrWhiteSpace(id))
                {
                    result = GetObjectByCache(redisClient, id);
                }
            }

            if (result == null)
            {
                BaseUserManager manager = new BaseUserManager();
                result = manager.GetObjectByCompanyIdByCode(companyId, userCode);

                SetCache(result);
            }

            return result;
        }

        /// <summary>
        /// 用户是否在公司里
        /// </summary>
        /// <param name="companyCode">公司编号</param>
        /// <param name="userCode">用户编号</param>
        /// <returns></returns>
        public static bool IsInOrganizeByCode(string companyCode, string userCode)
        {
            // 返回值
            bool result = false;

            // 检查参数有效性
            if (string.IsNullOrWhiteSpace(companyCode) || string.IsNullOrWhiteSpace(userCode))
            {
                return result;
            }

            // 先判断缓存，减少数据库查询
            string key = "User:ByCompanyCode:ByCode" + companyCode + ":" + userCode;
            using (var redisClient = PooledRedisHelper.GetReadOnlyClient())
            {
                result = redisClient.ContainsKey(key);
                if (result)
                {
                    return result;
                }
            }

            if (!result)
            {
                BaseUserManager manager = new BaseUserManager();
                BaseUserEntity entity = manager.GetObjectByCompanyCodeByCode(companyCode, userCode);
                SetCache(entity);
                result = true;
            }

            return result;
        }

        /// <summary>
        /// 通过 openId 获取用户信息
        /// </summary>
        /// <param name="openId">唯一键</param>
        /// <returns>用户实体</returns>
        public static BaseUserEntity GetObjectByOpenIdByCache(string openId)
        {
            BaseUserEntity result = null;

            string key = "OpenId";
            string userId = string.Empty;
            if (!string.IsNullOrWhiteSpace(openId))
            {
                
                key = key + openId;
# if Redis
                // 2015-12-14 吉日嘎拉 这里可以支持不走缓存的方式
                using (var redisClient = PooledRedisHelper.GetReadOnlyClient())
                {
                    userId = redisClient.Get<string>(key);

                    if (!string.IsNullOrWhiteSpace(userId))
                    {
                        result = GetObjectByCache(redisClient, userId);
                    }

                    if (result == null)
                    {
                        // 若没获取到用户？到数据库里查一次
                        BaseUserLogOnManager userLogOnManager = new BaseUserLogOnManager();
                        userId = userLogOnManager.GetIdByOpenId(openId);
                        if (!string.IsNullOrWhiteSpace(userId))
                        {   
                            result = GetObjectByCache(redisClient, userId);
                        }
                    }
                }
#endif
            }

            return result;
        }

        /// <summary>
        /// 快速模糊检索用户（高速缓存）
        /// 
        /// 01: 按编号或者真实姓名快速搜索全网业务员 prefix = "User:CodeOrRealName:"
        /// 02: 按网点快速搜索业务员 prefix = "User:CompanyId:" + companyId + ":"
        /// 
        /// </summary>
        /// <param name="prefix">搜索前缀</param>
        /// <param name="key">搜搜关键字</param>
        /// <param name="returnId">返回主键</param>
        /// <param name="returnCode">返回编号</param>
        /// <returns>查询结果数组</returns>
        public static List<KeyValuePair<string, string>> GetUserByKey(string prefix, string key, bool returnId = true, bool showCode = false, int topLimit = 20)
        {
            List<KeyValuePair<string, string>> result = new List<KeyValuePair<string, string>>();

            if (string.IsNullOrEmpty(key))
            {
                return result;
            }

            // 2016-01-06 吉日嘎拉 优化代码，提高检索效率，全部转小写进行比对
            key = prefix + key.ToLower();

            // 2016-02-19 宋彪 PooledRedisHelper.GetClient() 改为 PooledRedisHelper.GetSpellingReadOnlyClient()
            using (var redisClient = PooledRedisHelper.GetSpellingReadOnlyClient())
            {
                // 每次从有序集合中的消息取出20条信息，返回给客户端，在客户端展示阅读成功后才从内存里去掉。
                List<string> list = redisClient.GetRangeFromSortedSet(key, 0, topLimit);

                if (list != null)
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        // 从内存里直接获取这个对应的消息
                        string[] user = list[i].Split(';');
                        string id = user[0];
                        string code = user[1];
                        string realName = user[2];
                        if (returnId)
                        {
                            if (showCode)
                            {
                                result.Add(new KeyValuePair<string, string>(id, realName + " " + code));
                            }
                            else
                            {
                                result.Add(new KeyValuePair<string, string>(id, realName));
                            }
                        }
                        else
                        {
                            if (showCode)
                            {
                                result.Add(new KeyValuePair<string, string>(code, realName + " " + code));
                            }
                            else
                            {
                                result.Add(new KeyValuePair<string, string>(code, realName));
                            }
                        }
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="entity">用户实体</param>
        public static void CachePreheatingSpelling(BaseUserEntity entity)
        {
            // 2016-02-19 宋彪 PooledRedisHelper.GetClient() 改为 PooledRedisHelper.GetSpellingClient()
            using (var redisClient = PooledRedisHelper.GetSpellingClient())
            {
                double score = entity.SortCode.Value;
                CachePreheatingSpelling(redisClient, entity, score);
            }
        }

        public static void CachePreheatingSpelling(IRedisClient redisClient, BaseUserEntity userEntity, double score = 0)
        {
            // 读取到的数据直接强制设置到缓存里
            string id = userEntity.Id;
            // 2016-01-06 吉日嘎拉 网点编号不能大小写转换，否则查询就乱套了，不能改变原样
            string code = userEntity.Code;
            string realName = userEntity.RealName;
            string simpleSpelling = userEntity.SimpleSpelling.ToLower();

            string user = id + ";" + code + ";" + realName;

            if (userEntity.Enabled.HasValue && userEntity.Enabled.Value == 0)
            {
                // user += " 失效";
            }
            if (userEntity.DeletionStateCode.HasValue && userEntity.DeletionStateCode.Value == 1)
            {
                // user += " 已删除";
            }

            string key = string.Empty;

            code = code.Replace(" ", "");

            // 01：按网点进行缓存
            string companyId = userEntity.CompanyId;
            if (!string.IsNullOrEmpty(companyId))
            {
                for (int i = 1; i <= code.Length; i++)
                {
                    // 2016-01-06 吉日嘎拉 这里需要小写，提高效率，提高有善度
                    key = "User:CompanyId:" + companyId + ":" + code.Substring(0, i).ToLower();
                    redisClient.AddItemToSortedSet(key, user, score);
                    redisClient.ExpireEntryAt(key, DateTime.Now.AddDays(15));
                }
                for (int i = 1; i <= realName.Length; i++)
                {
                    key = "User:CompanyId:" + companyId + ":" + realName.Substring(0, i).ToLower();
                    redisClient.AddItemToSortedSet(key, user, score);
                    redisClient.ExpireEntryAt(key, DateTime.Now.AddDays(15));
                }
                for (int i = 1; i <= simpleSpelling.Length; i++)
                {
                    key = "User:CompanyId:" + companyId + ":" + simpleSpelling.Substring(0, i);
                    redisClient.AddItemToSortedSet(key, user, score);
                    redisClient.ExpireEntryAt(key, DateTime.Now.AddDays(15));
                }
            }

            // 02：按用户编号进行缓存
            if (!string.IsNullOrEmpty(code.Trim()))
            {
                for (int i = 6; i <= code.Length; i++)
                {
                    // 2016-01-06 吉日嘎拉 这里需要小写，提高效率，提高有善度
                    key = "User:CodeOrRealName:" + code.Substring(0, i).ToLower();
                    redisClient.AddItemToSortedSet(key, user, score);
                    redisClient.ExpireEntryAt(key, DateTime.Now.AddDays(15));
                }
            }
            if (!string.IsNullOrEmpty(realName.Trim()))
            {
                key = "User:CodeOrRealName:" + realName.ToLower();
                redisClient.AddItemToSortedSet(key, user, score);
                redisClient.ExpireEntryAt(key, DateTime.Now.AddDays(15));
            }

            // 输出到屏幕看看运行效果如何？心里有个数
            System.Console.WriteLine(score.ToString() + " " + user);
        }

        public static void CachePreheatingSpelling(bool flushDb = false)
        {
            // 组织机构数据缓存预热实现
            BaseUserManager userManager = new Business.BaseUserManager();
            // 减少数据库连接、减少内存站用、一边度取、一边设置缓存，只读取需要的数据
            userManager.SelectFields = BaseUserEntity.FieldId
                + ", " + BaseUserEntity.FieldCode
                + ", " + BaseUserEntity.FieldRealName
                + ", " + BaseUserEntity.FieldSimpleSpelling
                + ", " + BaseUserEntity.FieldQuickQuery
                + ", " + BaseUserEntity.FieldCompanyId
                + ", " + BaseUserEntity.FieldEnabled
                + ", " + BaseUserEntity.FieldDeletionStateCode
                + ", " + BaseUserEntity.FieldSortCode;

            // 读取有效的，没有被删除的网点数据
            // List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            // parameters.Add(new KeyValuePair<string, object>(BaseOrganizeEntity.FieldEnabled, 1));
            // parameters.Add(new KeyValuePair<string, object>(BaseOrganizeEntity.FieldDeletionStateCode, 0));

            // 2016-02-19 宋彪 PooledRedisHelper.GetClient() 改为 PooledRedisHelper.GetSpellingClient()
            using (var redisClient = PooledRedisHelper.GetSpellingClient())
            {
                if (flushDb)
                {
                    redisClient.FlushDb();
                }

                double score = 0;

                using (IDataReader dataReader = userManager.ExecuteReader(0, BaseUserEntity.FieldRealName))
                {
                    while (dataReader.Read())
                    {
                        score ++;
                        BaseUserEntity entity = new BaseUserEntity();
                        // 读取到的数据直接强制设置到缓存里
                        entity.Id = dataReader[BaseUserEntity.FieldId].ToString();
                        entity.CompanyId = dataReader[BaseUserEntity.FieldCompanyId].ToString();
                        // 2016-01-06 吉日嘎拉 网点编号不能大小写转换，否则查询就乱套了，不能改变原样
                        entity.Code = dataReader[BaseUserEntity.FieldCode].ToString();
                        entity.RealName = dataReader[BaseUserEntity.FieldRealName].ToString();
                        entity.SimpleSpelling = dataReader[BaseUserEntity.FieldSimpleSpelling].ToString().ToLower();
                        entity.SortCode = BaseBusinessLogic.ConvertToInt(dataReader[BaseUserEntity.FieldSortCode]);
                        entity.Enabled = BaseBusinessLogic.ConvertToInt(dataReader[BaseUserEntity.FieldEnabled]);
                        entity.DeletionStateCode = BaseBusinessLogic.ConvertToInt(dataReader[BaseUserEntity.FieldDeletionStateCode]);
                        
                        if (!flushDb)
                        {
                            score = entity.SortCode.Value;
                        }

                        CachePreheatingSpelling(redisClient, entity, score);

                        if (flushDb)
                        {
                            // 2016-02-02 吉日嘎拉 设置一下排序属性
                            new Business.BaseManager(BaseUserEntity.TableName).SetProperty(entity.Id, new KeyValuePair<string, object>(BaseUserEntity.FieldSortCode, score));
                        }
                    }
                }
            }
        }
    }
}