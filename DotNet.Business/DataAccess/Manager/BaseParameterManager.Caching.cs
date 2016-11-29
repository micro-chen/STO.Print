//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd.  
//-----------------------------------------------------------------

using System;
using System.Web;
using System.Web.Caching;

namespace DotNet.Business
{
    using DotNet.Model;
    using DotNet.Utilities;

    /// <remarks>
    /// BaseParameterManager
    /// 参数管理
    /// 
    /// 修改记录
    /// 
    ///	版本：1.1 2015.07.21    JiRiGaLa    优化从缓存读取参数的代码。
    ///	版本：1.0 2014.12.31    JiRiGaLa    选项管理从缓存读取，通过编号显示名称的函数完善。
    ///	
    /// <author>  
    ///		<name>JiRiGaLa</name>
    ///		<date>2015.07.21</date>
    /// </author> 
    /// </remarks>
    public partial class BaseParameterManager
    {
        #region public static void ClearCache() 清除缓存
        /// <summary>
        /// 清除缓存
        /// </summary>
        public static void ClearCache()
        {
            System.Web.Caching.Cache cache = HttpRuntime.Cache;
            if (cache[BaseParameterEntity.TableName] != null)
            {
                cache.Remove(BaseParameterEntity.TableName);
            }
        }
        #endregion

        /// <summary>
        /// 从缓存获取获取实体
        /// </summary>
        /// <param name="id">主键</param>
        public static string GetParameterByCache(string tableName, string categoryCode, string parameterId, string parameterCode)
        {
            string result = null;

            System.Web.Caching.Cache cache = HttpRuntime.Cache;

            string cacheKey = "Parameter";
            if (!string.IsNullOrEmpty(categoryCode) && !string.IsNullOrEmpty(parameterId) && !string.IsNullOrEmpty(parameterCode))
            {
                cacheKey = "Parameter:" + tableName + ":" + categoryCode + ":" + parameterId + ":" + parameterCode;
            }
            if (cache != null && cache[cacheKey] == null)
            {
                BaseParameterManager parameterManager = new Business.BaseParameterManager();
                result = parameterManager.GetParameter(tableName, categoryCode, parameterId, parameterCode);

                // 若是空的不用缓存，继续读取实体
                if (result != null)
                {
                    cache.Add(cacheKey, result, null, DateTime.Now.AddMinutes(10), TimeSpan.Zero, CacheItemPriority.Normal, null);
                    // System.Console.WriteLine(System.DateTime.Now.ToString(BaseSystemInfo.DateTimeFormat) + " cache Parameter " + parameterCode);
                }
            }
            result = cache[cacheKey] as string;

            return result;
        }
    }
}