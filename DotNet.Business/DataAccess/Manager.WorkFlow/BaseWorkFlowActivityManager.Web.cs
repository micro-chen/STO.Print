//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd.  
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
    /// BaseWorkFlowActivityManager
    /// 流程步骤定义
    /// 
    /// 修改记录
    /// 
    ///	版本：1.0 2012.12.15    JiRiGaLa    选项管理从缓存读取，通过编号显示名称的函数完善。
    ///	
    /// <author>  
    ///		<name>JiRiGaLa</name>
    ///		<date>2012.12.15</date>
    /// </author> 
    /// </remarks>
    public partial class BaseWorkFlowActivityManager
    {
        #region public static void ClearCache() 清除缓存
        /// <summary>
        /// 清除缓存
        /// </summary>
        public static void ClearCache()
        {
            lock (BaseSystemInfo.UserLock)
            {
                if (HttpContext.Current.Cache[BaseWorkFlowActivityEntity.TableName] != null)
                {
                    HttpContext.Current.Cache.Remove(BaseWorkFlowActivityEntity.TableName);
                }
            }
        }
        #endregion

        #region public static List<BaseWorkFlowActivityEntity> GetEntities() 获取表，从缓存读取
        /// <summary>
        /// 获取表，从缓存读取
        /// </summary>
        public static List<BaseWorkFlowActivityEntity> GetEntities()
        {
            if (HttpContext.Current.Session == null || HttpContext.Current.Cache[BaseWorkFlowActivityEntity.TableName] == null)
            {
                lock (BaseSystemInfo.UserLock)
                {
                    if (HttpContext.Current.Session == null || HttpContext.Current.Cache[BaseWorkFlowActivityEntity.TableName] == null)
                    {
                        // 读取目标表中的数据
                        List<BaseWorkFlowActivityEntity> entityList = null;
                        BaseWorkFlowActivityManager manager = new DotNet.Business.BaseWorkFlowActivityManager(BaseWorkFlowActivityEntity.TableName);
                        entityList = manager.GetList<BaseWorkFlowActivityEntity>();
                        // 这个是没写过期时间的方法
                        // HttpContext.Current.Cache[tableName] = result;
                        // 设置过期时间为8个小时，第2天若有不正常的自动就可以正常了
                        HttpContext.Current.Cache.Add(BaseWorkFlowActivityEntity.TableName, entityList, null, DateTime.Now.AddHours(1), TimeSpan.Zero, CacheItemPriority.Normal, null);
                    }
                }
            }
            return HttpContext.Current.Cache[BaseWorkFlowActivityEntity.TableName] as List<BaseWorkFlowActivityEntity>;
        }
        #endregion

        #region public static BaseWorkFlowActivityEntity GetEntity(string id) 从缓存读取
        /// <summary>
        /// 从缓存读取
        /// </summary>
        public static BaseWorkFlowActivityEntity GetEntity(string id)
        {
            return GetEntities().FirstOrDefault(entity => entity.Id.HasValue && entity.Id.ToString().Equals(id));
        }
        #endregion

        #region public static BaseWorkFlowActivityEntity GetEntity(int? id) 从缓存读取
        /// <summary>
        /// 从缓存读取
        /// </summary>
        public static BaseWorkFlowActivityEntity GetEntity(int? id)
        {
            return GetEntities().FirstOrDefault(entity => entity.Id.HasValue && entity.Id == id);
        }
        #endregion

        #region public static string GetFullName(string id) 通过编号获取选项的显示内容
        /// <summary>
        /// 通过编号获取选项的显示内容
        /// 这里是进行了内存缓存处理，减少数据库的I/O处理，提高程序的运行性能，
        /// 若有数据修改过，重新启动一下程序就可以了，这些基础数据也不是天天修改来修改去的，
        /// 所以没必要过度担忧，当然有需要时也可以写个刷新缓存的程序
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>显示值</returns>
        public static string GetFullName(string id)
        {
            string result = id;
            if (!string.IsNullOrEmpty(id))
            {
                BaseWorkFlowActivityEntity workFlowActivityEntity = GetEntities().FirstOrDefault(entity => entity.Id.HasValue && entity.Id.ToString().Equals(id));
                if (workFlowActivityEntity != null)
                {
                    result = workFlowActivityEntity.FullName;
                }
            }
            return result;
        }
        #endregion

        #region public static string GetFullNameByCode(string code) 通过编号获取选项的显示内容
        /// <summary>
        /// 通过编号获取选项的显示内容
        /// 这里是进行了内存缓存处理，减少数据库的I/O处理，提高程序的运行性能，
        /// 若有数据修改过，重新启动一下程序就可以了，这些基础数据也不是天天修改来修改去的，
        /// 所以没必要过度担忧，当然有需要时也可以写个刷新缓存的程序
        /// </summary>
        /// <param name="code">编号</param>
        /// <returns>显示值</returns>
        public new static string GetFullNameByCode(string code)
        {
            string result = code;
            if (!string.IsNullOrEmpty(code))
            {
                BaseWorkFlowActivityEntity workFlowActivityEntity = GetEntities().FirstOrDefault(entity => !string.IsNullOrEmpty(entity.Code) && entity.Code.Equals(code));
                if (workFlowActivityEntity != null)
                {
                    result = workFlowActivityEntity.FullName;
                }
            }
            return result;
        }
        #endregion
    }
}