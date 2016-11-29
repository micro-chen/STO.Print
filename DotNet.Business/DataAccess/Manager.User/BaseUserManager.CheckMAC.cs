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

    /// <summary>
    /// BaseUserManager
    /// 用户管理
    /// 
    /// 修改记录
    /// 
    ///		2015.12.21 版本：1.0 JiRiGaLa	进行代码分离、缓存优化、这个可以降低I/O的。
    /// 
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2015.12.21</date>
    /// </author> 
    /// </summary>
    public partial class BaseUserManager : BaseManager
    {
        /// <summary>
        /// 检查用户的 macAddress 绑定是否正常
        ///
        /// 防止重复多读数据？ 
        /// 是否判断正确？
        /// 可以按每个用户缓存？
        /// 若没有就自动化增加？
        /// mac 限制完善？
        /// mac 限制缓存预热？
        /// </summary>
        /// <param name="userId">用户主键</param>
        /// <param name="macAddress">硬件地址</param>
        /// <returns>正确</returns>
        public static bool CheckMACAddressByCache(string userId, string macAddress)
        {
            // 默认是不成功的，防止出错误
            bool result = false;

            // 检查参数的有效性
            if (string.IsNullOrEmpty(userId))
            {
                return result;
            }
            if (string.IsNullOrEmpty(macAddress))
            {
                return result;
            }

            // 提高效率，全小写转换
            macAddress = macAddress.ToLower();

            // 这里是处理，多个mac的问题
            string[] mac = macAddress.Split(';');

            using (var redisClient = PooledRedisHelper.GetClient())
            {
                string key = "MAC:" + userId;

                // 若是缓存里过期了？
                if (!redisClient.ContainsKey(key))
                {
                    // 重新缓存用户的限制数据
                    if (CachePreheatingMACAddressByUser(redisClient, userId) == 0)
                    {
                        // 若没有设置mac限制，需要把限制都自动加上来。
                        // 没有加到数据的，就是表明是新增加的用户、第一次登录的用户
                        BaseParameterManager parameterManager = new BaseParameterManager();
                        for (int i = 0; i < mac.Length; i++)
                        {
                            if (!string.IsNullOrEmpty(mac[i]))
                            {
                                // 把收集过来的mac地址需要保存起来
                                BaseParameterEntity parameterEntity = new BaseParameterEntity();
                                parameterEntity.Id = Guid.NewGuid().ToString("N");
                                parameterEntity.CategoryCode = "MacAddress";
                                parameterEntity.ParameterCode = "Single";
                                parameterEntity.ParameterId = userId;
                                // 这里之际保存小写、就效率也高，省事了
                                parameterEntity.ParameterContent = mac[i].Trim();
                                parameterManager.Add(parameterEntity);
                            }
                        }
                        result = true;
                    }
                }

                // 若还是没有？表示是新增的
                if (redisClient.ContainsKey(key))
                {
                    // 若已经存在，就需要进行缓存里的判断？
                    // 这里要提高效率，不能反复打开缓存
                    for (int i = 0; i < mac.Length; i++)
                    {
                        // 这里对数据还不放心，进行优化处理
                        if (!string.IsNullOrEmpty(mac[i]))
                        {
                            mac[i] = mac[i].Trim();
                            result = redisClient.SetContainsItem(key, mac[i]);
                            if (result)
                            {
                                // 这里要提高判断的效率
                                break;
                            }
                        }
                    }
                    // 若没有验证成功、把当前的 macAddress 保存起来, 方便后台管理的人加上去。
                    if (!result)
                    {
                        List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
                        parameters.Add(new KeyValuePair<string, object>(BaseUserLogOnEntity.FieldMACAddress, macAddress));
                        BaseUserLogOnManager userLogOnManager = new BaseUserLogOnManager();
                        userLogOnManager.SetProperty(userId, parameters);
                    }
                }
            }

            return result;
        }


        /// <summary>
        /// 缓存预热,强制重新缓存
        /// </summary>
        /// <returns>影响行数</returns>
        public static int CachePreheatingMACAddress()
        {
            int result = 0;

            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>(BaseParameterEntity.FieldCategoryCode, "MacAddress"));
            parameters.Add(new KeyValuePair<string, object>(BaseParameterEntity.FieldEnabled, 1));
            parameters.Add(new KeyValuePair<string, object>(BaseParameterEntity.FieldDeletionStateCode, 0));

            // 把所有的数据都缓存起来的代码
            BaseParameterManager manager = new BaseParameterManager();

            using (var redisClient = PooledRedisHelper.GetClient())
            {
                using (IDataReader dataReader = manager.ExecuteReader(parameters))
                {
                    while (dataReader.Read())
                    {
                        string key = "MAC:" + dataReader[BaseParameterEntity.FieldParameterId].ToString();

                        string macAddress = dataReader[BaseParameterEntity.FieldParameterContent].ToString().ToLower();
                        redisClient.AddItemToSet(key, macAddress);

                        redisClient.ExpireEntryAt(key, DateTime.Now.AddMonths(3));

                        result++;
                        System.Console.WriteLine(result.ToString() + " : " + macAddress);
                    }
                    dataReader.Close();
                }
            }

            return result;
        }

        /// <summary>
        /// 从缓存中去掉MAC限制
        /// 防止重复查询数据库？
        /// </summary>
        /// <param name="userId">用户主键</param>
        public static bool ResetMACAddressByCache(string userId)
        {
            bool result = false;

            if (!string.IsNullOrEmpty(userId))
            {
                string key = "MAC:" + userId;
                using (var redisClient = PooledRedisHelper.GetClient())
                {
                    result = redisClient.Remove(key);
                }
            }

            return result;
        }

        public static int CachePreheatingMACAddressByUser(IRedisClient redisClient, string userId)
        {
            int result = 0;

            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>(BaseParameterEntity.FieldParameterId, userId));
            parameters.Add(new KeyValuePair<string, object>(BaseParameterEntity.FieldCategoryCode, "MacAddress"));
            parameters.Add(new KeyValuePair<string, object>(BaseParameterEntity.FieldEnabled, 1));
            parameters.Add(new KeyValuePair<string, object>(BaseParameterEntity.FieldDeletionStateCode, 0));

            string key = "MAC:" + userId;

            BaseParameterManager parameterManager = new BaseParameterManager();
            parameterManager.SelectFields = BaseParameterEntity.FieldParameterContent;
            using (IDataReader dataReader = parameterManager.ExecuteReader(parameters))
            {
                while (dataReader.Read())
                {
                    string macAddress = dataReader[BaseParameterEntity.FieldParameterContent].ToString().ToLower();
                    redisClient.AddItemToSet(key, macAddress);
                    result++;
                }
                dataReader.Close();

                redisClient.ExpireEntryAt(key, DateTime.Now.AddMonths(3));
            }

            return result;
        }

        /// <summary>
        /// 缓存中加载用户的MAC
        /// </summary>
        /// <param name="userId">用户主键</param>
        /// <returns>影响行数</returns>
        public static int CachePreheatingMACAddressByUser(string userId)
        {
            int result = 0;

            using (var redisClient = PooledRedisHelper.GetClient())
            {
                result = CachePreheatingMACAddressByUser(redisClient, userId);
            }

            return result;
        }

        /// <summary>
        /// 重设 MacAddress 限制，
        /// 2015-12-21 吉日嘎拉 历史数据不应该被丢失才对
        /// </summary>
        /// <param name="userId">用户、接口主键</param>
        /// <returns>影响行数</returns>
        public int ResetMACAddress(string userId)
        {
            int result = 0;

            // 把缓存里的先清理掉
            ResetMACAddressByCache(userId);

            // todo 吉日嘎拉 这个操作应该增加个操作日志、谁什么时间，把什么数据删除了？ 把登录日志按操作日志、系统日志来看待？
            string commandText = string.Empty;
            commandText = "UPDATE " + BaseParameterEntity.TableName
                        + "   SET " + BaseParameterEntity.FieldDeletionStateCode + " = 1 "
                        + "     , " + BaseParameterEntity.FieldEnabled + " = 0 "
                        + " WHERE " + BaseParameterEntity.FieldCategoryCode + " =  " + DbHelper.GetParameter(BaseParameterEntity.FieldCategoryCode)
                        + "       AND " + BaseParameterEntity.FieldParameterId + " = " + DbHelper.GetParameter(BaseParameterEntity.FieldParameterId);

            List<IDbDataParameter> dbParameters = new List<IDbDataParameter>();
            dbParameters.Add(DbHelper.MakeParameter(BaseParameterEntity.FieldCategoryCode, "MacAddress"));
            dbParameters.Add(DbHelper.MakeParameter(BaseParameterEntity.FieldParameterId, userId));
            result = this.DbHelper.ExecuteNonQuery(commandText, dbParameters.ToArray());

            return result;
        }


        /*

        public bool CheckMACAddress(string userId, string macAddress)
        {
            bool result = false;

            BaseParameterManager parameterManager = new BaseParameterManager(this.DbHelper, this.UserInfo);
            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>(BaseParameterEntity.FieldParameterId, userId));
            parameters.Add(new KeyValuePair<string, object>(BaseParameterEntity.FieldCategoryCode, "MacAddress"));
            if (parameterManager.Exists(parameters))
            {
                if (this.CheckMacAddress(userId, macAddress))
                {
                    result = true;
                }
                else
                {
                    parameters = new List<KeyValuePair<string, object>>();
                    parameters.Add(new KeyValuePair<string, object>(BaseUserLogOnEntity.FieldMACAddress, macAddress));

                    BaseUserLogOnManager userLogOnManager = new BaseUserLogOnManager(this.DbHelper, this.UserInfo)
                    userLogOnManager.SetProperty(userId, parameters);

                    return result;
                }
            }
            else
            {
                // 若没有设置mac限制，需要把限制都自动加上来。
                string[] mac = macAddress.Split(';');
                if (mac != null && mac.Length > 0)
                {
                    for (int i = 0; i < mac.Length; i++)
                    {
                        if (!string.IsNullOrEmpty(mac[i]))
                        {
                            // 把收集过来的mac地址需要保存起来
                            BaseParameterEntity parameterEntity = new BaseParameterEntity();
                            parameterEntity.Id = Guid.NewGuid().ToString("N");
                            parameterEntity.CategoryCode = "MacAddress";
                            parameterEntity.ParameterCode = "Single";
                            parameterEntity.ParameterId = userId;
                            // 这里之际保存小写、就效率也高，省事了
                            parameterEntity.ParameterContent = mac[i].Trim().ToLower();
                            parameterManager.Add(parameterEntity);
                        }
                    }
                }
                result = true;
            }
            return result;
        }

        #region private bool CheckMacAddress(string userId, string macAddress) 检查用户的网卡Mac地址
        /// <summary>
        /// 检查用户的网卡Mac地址
        /// </summary>
        /// <param name="macAddress">Mac地址</param>
        /// <returns>符合限制</returns>
        private bool CheckMacAddress(string userId, string macAddress)
        {
            bool result = false;

            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>(BaseParameterEntity.FieldParameterId, userId));
            parameters.Add(new KeyValuePair<string, object>(BaseParameterEntity.FieldCategoryCode, "MacAddress"));
            parameters.Add(new KeyValuePair<string, object>(BaseParameterEntity.FieldEnabled, 1));

            BaseParameterManager parameterManager = new BaseParameterManager(this.UserInfo);
            var dt = parameterManager.GetDataTable(parameters);
            if (dt.Rows.Count > 0)
            {
                string[] mac = macAddress.Split(';');
                if (mac != null)
                {
                    for (int i = 0; i < mac.Length; i++)
                    {
                        string parameterCode = string.Empty;
                        string parameterCotent = string.Empty;
                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            // parameterCode = dt.Rows[j][BaseParameterEntity.FieldParameterCode].ToString();
                            parameterCotent = dt.Rows[j][BaseParameterEntity.FieldParameterContent].ToString();
                            // 简单格式化一下
                            result = (mac[i].ToLower()).Equals(parameterCotent.ToLower());
                            if (result)
                            {
                                break;
                            }
                        }
                    }
                }
            }

            return result;
        }
        #endregion

        private bool CheckMacAddress(string userId, string[] macAddress)
        {
            bool result = false;

            for (int i = 0; i < macAddress.Length; i++)
            {
                if (this.CheckMacAddress(userId, macAddress[i]))
                {
                    result = true;
                    break;
                }
            }

            return result;
        }
        
        */
    }
}