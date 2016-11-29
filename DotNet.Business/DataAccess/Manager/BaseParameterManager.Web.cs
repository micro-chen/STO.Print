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
    /// BaseParameterManager
    /// 参数管理
    /// 
    /// 修改纪录
    /// 
    ///	版本：1.0 2014.12.31    JiRiGaLa    选项管理从缓存读取，通过编号显示名称的函数完善。
    ///	
    /// <author>  
    ///		<name>JiRiGaLa</name>
    ///		<date>2014.12.31</date>
    /// </author> 
    /// </remarks>
    public partial class BaseParameterManager
    {
        // 当前的锁
        private static object locker = new Object();

        #region public static void ClearCache() 清除缓存
        /// <summary>
        /// 清除缓存
        /// </summary>
        public static void ClearCache()
        {
            lock (BaseSystemInfo.UserLock)
            {
                System.Web.Caching.Cache cache = HttpRuntime.Cache;
                if (cache[BaseParameterEntity.TableName] != null)
                {
                    cache.Remove(BaseParameterEntity.TableName);
                }
            }
        }
        #endregion

        /// <summary>
        /// 从缓存获取获取实体
        /// </summary>
        /// <param name="id">主键</param>
        public static string GetParameterByCache(string categoryCode, string parameterId, string parameterCode)
        {
            string result = null;
            System.Web.Caching.Cache cache = HttpRuntime.Cache;
            string cacheObject = "Parameter";
            if (!string.IsNullOrEmpty(categoryCode) && !string.IsNullOrEmpty(parameterId) && !string.IsNullOrEmpty(parameterCode))
            {
                cacheObject = "Parameter" + categoryCode + parameterId + parameterCode;
            }
            if (cache != null && cache[cacheObject] == null)
            {
                lock (locker)
                {
                    if (cache != null && cache[cacheObject] == null)
                    {
                        List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
                        parameters.Add(new KeyValuePair<string, object>(BaseParameterEntity.FieldCategoryCode, categoryCode));
                        parameters.Add(new KeyValuePair<string, object>(BaseParameterEntity.FieldParameterId, parameterId));
                        parameters.Add(new KeyValuePair<string, object>(BaseParameterEntity.FieldParameterCode, parameterCode));
                        parameters.Add(new KeyValuePair<string, object>(BaseParameterEntity.FieldDeletionStateCode, 0));

                        BaseParameterManager parameterManager = new Business.BaseParameterManager();
                        result = parameterManager.GetProperty(parameters, BaseParameterEntity.FieldParameterContent);

                        // 若是空的不用缓存，继续读取实体
                        if (result != null)
                        {
                            cache.Add(cacheObject, result, null, DateTime.Now.AddMinutes(10), TimeSpan.Zero, CacheItemPriority.Normal, null);
                            // System.Console.WriteLine(System.DateTime.Now.ToString(BaseSystemInfo.DateTimeFormat) + " cache Parameter " + parameterCode);
                        }
                    }
                }
            }
            result = cache[cacheObject] as string;
            return result;
        }
    }
}