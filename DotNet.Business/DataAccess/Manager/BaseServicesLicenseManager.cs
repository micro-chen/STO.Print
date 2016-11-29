//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd.  
//-----------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data;
using ServiceStack.Redis;

namespace DotNet.Business
{
    using DotNet.Utilities;
    using DotNet.Model;

    /// <summary>
    /// BaseServicesLicenseManager
    /// 参数类
    /// 
    /// 修改记录
    ///		2015.12.25 版本：1.0 JiRiGaLa	创建。
    ///
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2015.12.25</date>
    /// </author> 
    /// </summary>
    public partial class BaseServicesLicenseManager : BaseManager
    {
        /// <summary>
        /// 检查用户的 服务访问限制
        /// </summary>
        /// <param name="userId">用户主键</param>
        /// <param name="privateKey">私钥</param>
        /// <returns>正确</returns>
        public static bool CheckServiceByCache(string userId, string privateKey)
        {
            // 默认是不成功的，防止出错误
            bool result = false;

            // 检查参数的有效性
            if (string.IsNullOrEmpty(userId))
            {
                return result;
            }
            if (string.IsNullOrEmpty(privateKey))
            {
                return result;
            }

            using (var redisClient = PooledRedisHelper.GetClient())
            {
                string key = "User:" + userId + ":Service";

                // 若是缓存里过期了？
                if (!redisClient.ContainsKey(key))
                {
                    // 重新缓存用户的限制数据
                    if (CachePreheatingServiceByUser(redisClient, userId) == 0)
                    {
                        result = false;
                    }
                }

                // 若还是没有？表示是新增的
                if (redisClient.ContainsKey(key))
                {
                    // 若已经存在，就需要进行缓存里的判断？
                    // 这里要提高效率，不能反复打开缓存
                    result = redisClient.SetContainsItem(key, privateKey);
                    if (!result)
                    {
                        // 若没有验证成功、应该记录日志。
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 缓存预热,强制重新缓存
        /// </summary>
        /// <returns>影响行数</returns>
        public static int CachePreheatingService()
        {
            int result = 0;

            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>(BaseServicesLicenseEntity.FieldEnabled, 1));
            parameters.Add(new KeyValuePair<string, object>(BaseServicesLicenseEntity.FieldDeletionStateCode, 0));

            // 把所有的数据都缓存起来的代码
            BaseServicesLicenseManager manager = new BaseServicesLicenseManager();

            using (var redisClient = PooledRedisHelper.GetClient())
            {
                using (IDataReader dataReader = manager.ExecuteReader(parameters))
                {
                    while (dataReader.Read())
                    {
                        string key = "User:" + dataReader[BaseServicesLicenseEntity.FieldUserId].ToString() + ":Service";

                        string privateKey = dataReader[BaseServicesLicenseEntity.FieldPrivateKey].ToString();
                        redisClient.AddItemToSet(key, privateKey);

                        redisClient.ExpireEntryAt(key, DateTime.Now.AddMonths(3));

                        result++;
                        System.Console.WriteLine(result.ToString() + " : " + privateKey);
                    }
                    dataReader.Close();
                }
            }

            return result;
        }

        /// <summary>
        /// 从缓存中去掉
        /// </summary>
        /// <param name="userId">用户主键</param>
        public static bool ResetServiceByCache(string userId)
        {
            bool result = false;

            if (!string.IsNullOrEmpty(userId))
            {
                string key = "User:" + userId + ":Service";
                using (var redisClient = PooledRedisHelper.GetClient())
                {
                    result = redisClient.Remove(key);
                }
            }

            return result;
        }

        public static int CachePreheatingServiceByUser(IRedisClient redisClient, string userId)
        {
            int result = 0;

            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>(BaseServicesLicenseEntity.FieldUserId, userId));
            parameters.Add(new KeyValuePair<string, object>(BaseServicesLicenseEntity.FieldEnabled, 1));
            parameters.Add(new KeyValuePair<string, object>(BaseServicesLicenseEntity.FieldDeletionStateCode, 0));

            string key = "User:" + userId + ":Service";

            BaseServicesLicenseManager manager = new BaseServicesLicenseManager();
            manager.SelectFields = BaseServicesLicenseEntity.FieldPrivateKey;
            using (IDataReader dataReader = manager.ExecuteReader(parameters))
            {
                while (dataReader.Read())
                {
                    string privateKey = dataReader[BaseServicesLicenseEntity.FieldPrivateKey].ToString();
                    redisClient.AddItemToSet(key, privateKey);
                    result++;
                }
                dataReader.Close();

                redisClient.ExpireEntryAt(key, DateTime.Now.AddMonths(3));
            }

            return result;
        }

        /// <summary>
        /// 缓存中加载
        /// </summary>
        /// <param name="userId">用户主键</param>
        /// <returns>影响行数</returns>
        public static int CachePreheatingServiceByUser(string userId)
        {
            int result = 0;

            using (var redisClient = PooledRedisHelper.GetClient())
            {
                CachePreheatingServiceByUser(redisClient, userId);
            }

            return result;
        }

        /// <summary>
        /// 重设 Service 限制，
        /// </summary>
        /// <param name="userId">用户、接口主键</param>
        /// <returns>影响行数</returns>
        public int ResetService(string userId)
        {
            int result = 0;

            // 把缓存里的先清理掉
            ResetServiceByCache(userId);

            // todo 吉日嘎拉 这个操作应该增加个操作日志、谁什么时间，把什么数据删除了？ 把登录日志按操作日志、系统日志来看待？
            string commandText = string.Empty;
            commandText = "UPDATE " + BaseServicesLicenseEntity.TableName
                        + "   SET " + BaseServicesLicenseEntity.FieldDeletionStateCode + " = 1 "
                        + "     , " + BaseServicesLicenseEntity.FieldEnabled + " = 0 "
                        + " WHERE " + BaseServicesLicenseEntity.FieldUserId + " = " + DbHelper.GetParameter(BaseServicesLicenseEntity.FieldUserId);

            List<IDbDataParameter> dbParameters = new List<IDbDataParameter>();
            dbParameters.Add(DbHelper.MakeParameter(BaseServicesLicenseEntity.FieldUserId, userId));
            result = this.DbHelper.ExecuteNonQuery(commandText, dbParameters.ToArray());

            return result;
        }
    }
}