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

    /// <summary>
    /// BaseUserManager
    /// 用户管理
    /// 
    /// 修改记录
    /// 
    ///		2015.12.24 版本：1.0 JiRiGaLa	主键整理。
    /// 
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2015.12.24</date>
    /// </author> 
    /// </summary>
    public partial class BaseUserManager : BaseManager
    {
        public static bool CheckIPAddressByCache(string userId, string ipAddress, bool autoAdd = false)
        {
            // 判断用户是否限制ip访问，有的是不限制访问的
            BaseUserLogOnManager userLogOnManager = new BaseUserLogOnManager();
            BaseUserLogOnEntity userLogOnEntity = userLogOnManager.GetObject(userId);
            return CheckIPAddressByCache(userLogOnEntity, ipAddress, autoAdd);
        }

        /// <summary>
        /// 检查用户的 IPAddress 绑定是否正常
        ///
        /// 防止重复多读数据？ 
        /// 是否判断正确？
        /// 可以按每个用户缓存？
        /// 若没有就自动化增加？
        /// IP 限制完善？
        /// IP 限制缓存预热？
        /// </summary>
        /// <param name="userId">用户主键</param>
        /// <param name="IPAddress">ip地址</param>
        /// <param name="autoAdd">没有在列表的IP是否自动增加</param>
        /// <returns>正确</returns>
        public static bool CheckIPAddressByCache(BaseUserLogOnEntity userLogOnEntity, string ipAddress, bool autoAdd = false)
        {
            // 默认是不成功的，防止出错误
            bool result = false;
            
            if (userLogOnEntity == null)
            {
                return result;
            }

            string userId = userLogOnEntity.Id;
            // 检查参数的有效性
            if (string.IsNullOrEmpty(userId))
            {
                return result;
            }
            if (string.IsNullOrEmpty(ipAddress))
            {
                return result;
            }

            // 若用户是不限制登录的、那就可以返回真的
            if (!userLogOnEntity.CheckIPAddress.HasValue || userLogOnEntity.CheckIPAddress.Value == 0)
            {
                return true;
            }

            // 提高效率，全小写转换
            ipAddress = ipAddress.ToLower();

            // 这里是处理，多个IP的问题
            string[] ip = ipAddress.Split(';');

            using (var redisClient = PooledRedisHelper.GetClient())
            {
                string key = "IP:" + userId;

                // 若是缓存里过期了？
                if (!redisClient.ContainsKey(key))
                {
                    // 重新缓存用户的限制数据
                    if (CachePreheatingIPAddressByUser(redisClient, userId) == 0)
                    {
                        // 若没有设置IP限制，需要把限制都自动加上来。
                        // 没有加到数据的，就是表明是新增加的用户、第一次登录的用户
                        BaseParameterManager parameterManager = new BaseParameterManager();
                        for (int i = 0; i < ip.Length; i++)
                        {
                            if (!string.IsNullOrEmpty(ip[i]))
                            {
                                // 把收集过来的IP地址需要保存起来
                                BaseParameterEntity parameterEntity = new BaseParameterEntity();
                                parameterEntity.Id = Guid.NewGuid().ToString("N");
                                parameterEntity.CategoryCode = "IPAddress";
                                parameterEntity.ParameterCode = "Single";
                                parameterEntity.ParameterId = userId;
                                // 这里之际保存小写、就效率也高，省事了
                                parameterEntity.ParameterContent = ip[i].Trim();
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
                    for (int i = 0; i < ip.Length; i++)
                    {
                        // 这里对数据还不放心，进行优化处理
                        if (!string.IsNullOrEmpty(ip[i]))
                        {
                            ip[i] = ip[i].Trim();
                            result = redisClient.SetContainsItem(key, ip[i]);
                            if (result)
                            {
                                // 这里要提高判断的效率
                                break;
                            }
                        }
                    }
                    // 若没有验证成功、把当前的 IPAddress 保存起来, 方便后台管理的人加上去。
                    if (!result)
                    {
                        List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
                        parameters.Add(new KeyValuePair<string, object>(BaseUserLogOnEntity.FieldIPAddress, ipAddress));
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
        public static int CachePreheatingIPAddress()
        {
            int result = 0;

            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>(BaseParameterEntity.FieldCategoryCode, "IPAddress"));
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
                        string key = "IP:" + dataReader[BaseParameterEntity.FieldParameterId].ToString();

                        string ipAddress = dataReader[BaseParameterEntity.FieldParameterContent].ToString().ToLower();
                        redisClient.AddItemToSet(key, ipAddress);

                        redisClient.ExpireEntryAt(key, DateTime.Now.AddMonths(3));

                        result++;
                        System.Console.WriteLine(result.ToString() + " : " + ipAddress);
                    }
                    dataReader.Close();
                }
            }

            return result;
        }

        /// <summary>
        /// 从缓存中去掉IP限制
        /// 防止重复查询数据库？
        /// </summary>
        /// <param name="userId">用户主键</param>
        public static bool ResetIPAddressByCache(string userId)
        {
            bool result = false;

            if (!string.IsNullOrEmpty(userId))
            {
                string key = "IP:" + userId;
                using (var redisClient = PooledRedisHelper.GetClient())
                {
                    result = redisClient.Remove(key);
                }
            }

            return result;
        }

        public static int CachePreheatingIPAddressByUser(IRedisClient redisClient, string userId)
        {
            int result = 0;

            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>(BaseParameterEntity.FieldParameterId, userId));
            parameters.Add(new KeyValuePair<string, object>(BaseParameterEntity.FieldCategoryCode, "IPAddress"));
            parameters.Add(new KeyValuePair<string, object>(BaseParameterEntity.FieldEnabled, 1));
            parameters.Add(new KeyValuePair<string, object>(BaseParameterEntity.FieldDeletionStateCode, 0));

            string key = "IP:" + userId;

            BaseParameterManager parameterManager = new BaseParameterManager();
            parameterManager.SelectFields = BaseParameterEntity.FieldParameterContent;
            using (IDataReader dataReader = parameterManager.ExecuteReader(parameters))
            {
                while (dataReader.Read())
                {
                    string ipAddress = dataReader[BaseParameterEntity.FieldParameterContent].ToString().ToLower();
                    redisClient.AddItemToSet(key, ipAddress);
                    result++;
                }
                dataReader.Close();

                redisClient.ExpireEntryAt(key, DateTime.Now.AddMonths(3));
            }

            return result;
        }

        /// <summary>
        /// 缓存中加载用户的IP
        /// </summary>
        /// <param name="userId">用户主键</param>
        /// <returns>影响行数</returns>
        public static int CachePreheatingIPAddressByUser(string userId)
        {
            int result = 0;

            using (var redisClient = PooledRedisHelper.GetClient())
            {
                CachePreheatingIPAddressByUser(redisClient, userId);
            }

            return result;
        }

        /// <summary>
        /// 重设 IPAddress 限制，
        /// 2015-12-21 吉日嘎拉 历史数据不应该被丢失才对
        /// </summary>
        /// <param name="userId">用户、接口主键</param>
        /// <returns>影响行数</returns>
        public int ResetIPAddress(string userId)
        {
            int result = 0;

            // 把缓存里的先清理掉
            ResetIPAddressByCache(userId);

            // todo 吉日嘎拉 这个操作应该增加个操作日志、谁什么时间，把什么数据删除了？ 把登录日志按操作日志、系统日志来看待？
            string commandText = string.Empty;
            commandText = "UPDATE " + BaseParameterEntity.TableName
                        + "   SET " + BaseParameterEntity.FieldDeletionStateCode + " = 1 "
                        + "     , " + BaseParameterEntity.FieldEnabled + " = 0 "
                        + " WHERE " + BaseParameterEntity.FieldCategoryCode + " =  " + DbHelper.GetParameter(BaseParameterEntity.FieldCategoryCode)
                        + "       AND " + BaseParameterEntity.FieldParameterId + " = " + DbHelper.GetParameter(BaseParameterEntity.FieldParameterId);

            List<IDbDataParameter> dbParameters = new List<IDbDataParameter>();
            dbParameters.Add(DbHelper.MakeParameter(BaseParameterEntity.FieldCategoryCode, "IPAddress"));
            dbParameters.Add(DbHelper.MakeParameter(BaseParameterEntity.FieldParameterId, userId));
            result = this.DbHelper.ExecuteNonQuery(commandText, dbParameters.ToArray());

            return result;
        }

        /*

        #region private bool CheckIPAddress(string ipAddress, string userId) 检查用户IP地址
        /// <summary>
        /// 检查用户IP地址
        /// </summary>
        /// <param name="ipAddress">IP地址</param>
        /// <returns>是否符合限制</returns>
        private bool CheckIPAddress(string ipAddress, string userId)
        {
            bool result = false;

            BaseParameterManager manager = new BaseParameterManager(this.UserInfo);
            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>(BaseParameterEntity.FieldParameterId, userId));
            parameters.Add(new KeyValuePair<string, object>(BaseParameterEntity.FieldCategoryCode, "IPAddress"));
            parameters.Add(new KeyValuePair<string, object>(BaseParameterEntity.FieldEnabled, 1));
            // var result = parameterManager.GetDataTable(parameters);
            string where = string.Format("((CategoryCode = 'IPAddress' AND ParameterId = 'TrustedIP') OR (CategoryCode = 'IPAddress' AND ParameterId = '{0}' )) AND Enabled = 1 AND DeletionStateCode = 0", userId);
            var dt = manager.GetDataTable(where);

            if (dt.Rows.Count > 0)
            {
                string parameterCode = string.Empty;
                string parameterCotent = string.Empty;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    parameterCode = dt.Rows[i][BaseParameterEntity.FieldParameterCode].ToString();
                    parameterCotent = dt.Rows[i][BaseParameterEntity.FieldParameterContent].ToString();
                    switch (parameterCode)
                    {
                        // 匹配单个IP
                        case "Single":
                            result = CheckSingleIPAddress(ipAddress, parameterCotent);
                            break;
                        // 匹配ip地址段
                        case "Range":
                            result = CheckIPAddressWithRange(ipAddress, parameterCotent);
                            break;
                        // 匹配带掩码的地址段
                        case "Mask":
                            result = CheckIPAddressWithMask(ipAddress, parameterCotent);
                            break;
                    }
                    if (result)
                    {
                        break;
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 检查是否匹配单个IP
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <param name="sourceIp"></param>
        /// <returns></returns>
        private bool CheckSingleIPAddress(string ipAddress, string sourceIp)
        {
            return ipAddress.Equals(sourceIp);
        }

        /// <summary>
        /// 检查是否匹配地址段
        /// </summary>
        /// <param name="ipAddress">192.168.0.8</param>
        /// <param name="ipRange">192.168.0.1-192.168.0.10</param>
        /// <returns></returns>
        private bool CheckIPAddressWithRange(string ipAddress, string ipRange)
        {
            //先判断符合192.168.0.1-192.168.0.10 的正则表达式

            //在判断ipAddress是否有效
            string startIp = ipRange.Split('-')[0];
            string endIp = ipRange.Split('-')[1];
            //如果大于等于 startip 或者 小于等于endip
            if (CompareIp(ipAddress, startIp) == 2 && CompareIp(ipAddress, endIp) == 0 || CompareIp(ipAddress, startIp) == 1 || CompareIp(ipAddress, endIp) == 1)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 比较两个IP地址，比较前可以先判断是否是IP地址
        /// </summary>
        /// <param name="ip1"></param>
        /// <param name="ip2"></param>
        /// <returns>1：相等;  0：ip1小于ip2 ; 2：ip1大于ip2；-1 不符合ip正则表达式 </returns>
        public int CompareIp(string ip1, string ip2)
        {
            //if (!IsIP(ip1) || !IsIP(ip2))
            //{
            //    return -1;

            //}

            String[] arr1 = ip1.Split('.');
            String[] arr2 = ip2.Split('.');
            for (int i = 0; i < arr1.Length; i++)
            {
                int a1 = int.Parse(arr1[i]);
                int a2 = int.Parse(arr2[i]);
                if (a1 > a2)
                {
                    return 2;
                }
                else if (a1 < a2)
                {
                    return 0;
                }
            }
            return 1;
        }

        /// <summary>
        /// 检查是否匹配带通配符的IP地址
        /// </summary>
        /// <param name="ipAddress">192.168.1.1</param>
        /// <param name="ipWithMask">192.168.1.*</param>
        /// <returns></returns>
        private bool CheckIPAddressWithMask(string ipAddress, string ipWithMask)
        {
            //先判断是否符合192.168.1.*

            //然后判断
            string[] arr1 = ipAddress.Split('.');
            string[] arr2 = ipWithMask.Split('.');
            for (int i = 0; i < arr1.Length; i++)
            {
                if (!(arr2[i].Equals("*") || arr1[i].Equals(arr2[i])))
                {
                    return false;
                }
            }
            return true;
        }
        #endregion

        private bool CheckIPAddress(string[] ipAddress, string userId)
        {
            bool result = false;
            for (int i = 0; i < ipAddress.Length; i++)
            {
                if (this.CheckIPAddress(ipAddress[i], userId))
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