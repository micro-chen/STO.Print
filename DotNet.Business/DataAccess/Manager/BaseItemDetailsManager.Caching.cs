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
    using DotNet.Model;

    /// <remarks>
    /// BaseItemDetailsManager
    /// 选项管理
    /// 
    /// 修改记录
    /// 
    ///	版本：1.0 2012.12.13    JiRiGaLa    选项管理从缓存读取，通过编号显示名称的函数完善。
    ///	
    /// <author>  
    ///		<name>JiRiGaLa</name>
    ///		<date>2012.12.13</date>
    /// </author> 
    /// </remarks>
    public partial class BaseItemDetailsManager
    {
        #region public static List<BaseItemDetailsEntity> GetEntities(string tableName) 获取选项表明细，从缓存读取
        /// <summary>
        /// 获取选项表明细，从缓存读取
        /// </summary>
        public static List<BaseItemDetailsEntity> GetEntities(string tableName)
        {
            if (HttpContext.Current.Session == null || HttpContext.Current.Cache[tableName] == null)
            {
                // 读取目标表中的数据
                List<BaseItemDetailsEntity> entityList = null;
                BaseItemDetailsManager manager = new DotNet.Business.BaseItemDetailsManager(tableName);
                entityList = manager.GetList<BaseItemDetailsEntity>();
                // 这个是没写过期时间的方法
                // HttpContext.Current.Cache[tableName] = result;
                // 设置过期时间为8个小时，第2天若有不正常的自动就可以正常了
                HttpContext.Current.Cache.Add(tableName, entityList, null, DateTime.Now.AddMinutes(10), TimeSpan.Zero, CacheItemPriority.Normal, null);
            }
            return HttpContext.Current.Cache[tableName] as List<BaseItemDetailsEntity>;
        }
        #endregion

        #region public static string GetItemNameByCode(string tableName, string itemCode) 通过编号获取选项的显示内容
        /// <summary>
        /// 通过编号获取选项的显示内容
        /// 这里是进行了内存缓存处理，减少数据库的I/O处理，提高程序的运行性能，
        /// 若有数据修改过，重新启动一下程序就可以了，这些基础数据也不是天天修改来修改去的，
        /// 所以没必要过度担忧，当然有需要时也可以写个刷新缓存的程序
        /// </summary>
        /// <param name="tableName">选项表名</param>
        /// <param name="itemCode">选项编号</param>
        /// <returns>显示值</returns>
        public static string GetItemNameByCode(string tableName, string itemCode)
        {
            string result = itemCode;
            if (!string.IsNullOrEmpty(itemCode))
            {
                List<BaseItemDetailsEntity> list = GetEntities(tableName);
                BaseItemDetailsEntity itemDetailsEntity = list.FirstOrDefault(entity => !string.IsNullOrEmpty(entity.ItemCode) && entity.ItemCode.Equals(itemCode));
                if (itemDetailsEntity != null)
                {
                    result = itemDetailsEntity.ItemName;
                }
            }
            return result;
        }
        #endregion

        #region public static string GetItemName(string tableName, string itemValue) 通过选项值获取选项的显示内容
        /// <summary>
        /// 通过选项值获取选项的显示内容
        /// 这里是进行了内存缓存处理，减少数据库的I/O处理，提高程序的运行性能，
        /// 若有数据修改过，重新启动一下程序就可以了，这些基础数据也不是天天修改来修改去的，
        /// 所以没必要过度担忧，当然有需要时也可以写个刷新缓存的程序
        /// </summary>
        /// <param name="tableName">选项表名</param>
        /// <param name="itemValue">选项值</param>
        /// <returns>显示值</returns>
        public static string GetItemName(string tableName, string itemValue)
        {
            string result = itemValue;
            if (!string.IsNullOrEmpty(itemValue))
            {
                List<BaseItemDetailsEntity> list = GetEntities(tableName);
                BaseItemDetailsEntity itemDetailsEntity = list.FirstOrDefault(entity => !string.IsNullOrEmpty(entity.ItemValue) && entity.ItemValue.Equals(itemValue));
                if (itemDetailsEntity != null)
                {
                    result = itemDetailsEntity.ItemName;
                }
            }
            return result;
        }
        #endregion
    }
}