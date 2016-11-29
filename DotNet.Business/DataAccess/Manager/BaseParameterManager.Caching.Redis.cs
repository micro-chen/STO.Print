//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd.  
//-----------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace DotNet.Business
{
    using DotNet.Model;
    using DotNet.Utilities;

    /// <remarks>
    /// BaseParameterManager
    /// 参数表缓存
    /// 
    /// 修改记录
    /// 
    ///     2016.03.01 版本：1.0 JiRiGaLa 创建。
    ///	
    /// <author>  
    ///		<name>JiRiGaLa</name>
    ///		<date>2016.03.01</date>
    /// </author> 
    /// </remarks>
    public partial class BaseParameterManager
    {
        public static void SetParameterByCache(string tableName, BaseParameterEntity entity)
        {
            string key = "Parameter:" + tableName + ":" + entity.CategoryCode + ":" + entity.ParameterId + ":" + entity.ParameterCode;
            using (var redisClient = PooledRedisHelper.GetClient())
            {
                if (!string.IsNullOrEmpty(entity.ParameterContent))
                {
                    redisClient.Set<string>(key, entity.ParameterContent, DateTime.Now.AddMinutes(10));
                }
            }
        }

        public static string GetParameterByCache(string tableName, string categoryCode, string parameterId, string parameterCode, bool refreshCache = false)
        {
            string result = string.Empty;

            if (!string.IsNullOrEmpty(categoryCode) && !string.IsNullOrEmpty(parameterId) && !string.IsNullOrEmpty(parameterCode))
            {
                string key = "Parameter:" + tableName + ":" + categoryCode + ":" + parameterId + ":" + parameterCode;
                using (var redisClient = PooledRedisHelper.GetClient())
                {
                    if (!refreshCache)
                    {
                        result = redisClient.Get<string>(key);
                    }

                    if (string.IsNullOrEmpty(result))
                    {
                        BaseParameterManager parameterManager = new Business.BaseParameterManager(tableName);
                        result = parameterManager.GetParameter(tableName, categoryCode, parameterId, parameterCode);
                        if (!string.IsNullOrEmpty(result))
                        {
                            redisClient.Set<string>(key, result, DateTime.Now.AddMinutes(10));
                        }
                    }
                }
            }

            return result;
        }
    }
}