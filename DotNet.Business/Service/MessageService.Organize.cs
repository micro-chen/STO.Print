//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2013 , Hairihan TECH, Ltd. 
//-----------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.ServiceModel;
using System.Web;
using System.Web.Caching;

namespace DotNet.Business
{
    using DotNet.Utilities;

    /// <summary>
    /// MessageService
    /// 消息服务
    /// 
    /// 修改纪录
    /// 
    ///		2013.11.12 版本：1.0 JiRiGaLa 创建。
    ///		
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2013.11.12</date>
    /// </author> 
    /// </summary>
    public partial class MessageService : IMessageService
    {
        // 当前的锁
        private static object locker = new Object();

        /// <summary>
        /// 获取区域
        /// </summary>
        /// <returns>区域数组</returns>
        public string[] GetArea(BaseUserInfo userInfo)
        {
            var parameter = ServiceParameter.CreateWithMessage(userInfo
                , MethodBase.GetCurrentMethod()
                , this.serviceName
                , AppMessage.OrganizeService_GetArea);
            string[] result = null;
            ServiceUtil.ProcessUserCenterReadDb(parameter, (dbHelper) =>
            {
                System.Web.Caching.Cache cache = HttpRuntime.Cache;
                string cacheObject = "OrganizeArea";
                if (cache == null || cache[cacheObject] == null)
                {
                    lock (locker)
                    {
                        if (cache == null || cache[cacheObject] == null)
                        {
                            var manager = new BaseOrganizeManager(dbHelper, userInfo);
                            result = manager.GetArea();
                            cache.Add(cacheObject, result, null, DateTime.Now.AddHours(8), TimeSpan.Zero, CacheItemPriority.Normal, null);
                        }
                    }
                }
                result = cache[cacheObject] as string[];
            });
            return result;
        }

        /// <summary>
        /// 获取省份
        /// </summary>
        /// <param name="userInfo">用户</param>
        /// <param name="area">区域</param>
        /// <returns>省份数组</returns>
        public string[] GetProvince(BaseUserInfo userInfo, string area)
        {
            var parameter = ServiceParameter.CreateWithMessage(userInfo
                , MethodBase.GetCurrentMethod()
                , this.serviceName
                , AppMessage.OrganizeService_GetProvince);
            string[] result = null;
            ServiceUtil.ProcessUserCenterReadDb(parameter, (dbHelper) =>
            {
                System.Web.Caching.Cache cache = HttpRuntime.Cache;
                string cacheObject = "OrganizeProvince" + area;
                if (cache == null || cache[cacheObject] == null)
                {
                    lock (locker)
                    {
                        if (cache == null || cache[cacheObject] == null)
                        {
                            var manager = new BaseOrganizeManager(dbHelper, userInfo);
                            result = manager.GetProvince(area);
                            cache.Add(cacheObject, result, null, DateTime.Now.AddHours(8), TimeSpan.Zero, CacheItemPriority.Normal, null);
                        }
                    }
                }
                result = cache[cacheObject] as string[];
            });
            return result;
        }

        /// <summary>
        /// 获取城市
        /// </summary>
        /// <param name="userInfo">用户</param>
        /// <param name="province">省份</param>
        /// <returns>城市数组</returns>
        public string[] GetCity(BaseUserInfo userInfo, string province)
        {
            var parameter = ServiceParameter.CreateWithMessage(userInfo
                , MethodBase.GetCurrentMethod()
                , this.serviceName
                , AppMessage.OrganizeService_GetCity);
            string[] result = null;
            ServiceUtil.ProcessUserCenterReadDb(parameter, (dbHelper) =>
            {
                System.Web.Caching.Cache cache = HttpRuntime.Cache;
                string cacheObject = "OrganizeCity" + province;
                if (cache == null || cache[cacheObject] == null)
                {
                    lock (locker)
                    {
                        if (cache == null || cache[cacheObject] == null)
                        {
                            var manager = new BaseOrganizeManager(dbHelper, userInfo);
                            result = manager.GetCity(province);
                            cache.Add(cacheObject, result, null, DateTime.Now.AddHours(8), TimeSpan.Zero, CacheItemPriority.Normal, null);
                        }
                    }
                }
                result = cache[cacheObject] as string[];
            });
            return result;
        }

        
        /// <summary>
        /// 获取县区
        /// </summary>
        /// <param name="userInfo">用户</param>
        /// <param name="city">城市</param>
        /// <returns>县区数组</returns>
        public string[] GetDistrict(BaseUserInfo userInfo, string city)
        {
            var parameter = ServiceParameter.CreateWithMessage(userInfo
                , MethodBase.GetCurrentMethod()
                , this.serviceName
                , AppMessage.OrganizeService_GetDistrict);
            string[] result = null;
            ServiceUtil.ProcessUserCenterReadDb(parameter, (dbHelper) =>
            {
                System.Web.Caching.Cache cache = HttpRuntime.Cache;
                string cacheObject = "OrganizeDistrict" + city;
                if (cache == null || cache[cacheObject] == null)
                {
                    lock (locker)
                    {
                        if (cache == null || cache[cacheObject] == null)
                        {
                            var manager = new BaseOrganizeManager(dbHelper, userInfo);
                            result = manager.GetDistrict(city);
                            cache.Add(cacheObject, result, null, DateTime.Now.AddHours(8), TimeSpan.Zero, CacheItemPriority.Normal, null);
                        }
                    }
                }
                result = cache[cacheObject] as string[];
            });
            return result;
        }




        /// <summary>
        /// 获得内部部门（公司的组织机构）
        /// </summary>
        /// <param name="userInfo">用户</param>
        /// <param name="province">省</param>
        /// <returns>数据表</returns>
        public string[] GetOrganizeByProvince(BaseUserInfo userInfo, string province)
        {
            var parameter = ServiceParameter.CreateWithMessage(userInfo
                , MethodBase.GetCurrentMethod()
                , this.serviceName
                , AppMessage.OrganizeService_GetOrganizeByProvince);
            string[] result = null;
            ServiceUtil.ProcessUserCenterReadDb(parameter, (dbHelper) =>
            {
                System.Web.Caching.Cache cache = HttpRuntime.Cache;
                string cacheObject = "OrganizeByProvince" + province;
                if (cache == null || cache[cacheObject] == null)
                {
                    lock (BaseSystemInfo.UserLock)
                    {
                        if (cache == null || cache[cacheObject] == null)
                        {
                            var manager = new BaseOrganizeManager(dbHelper, userInfo);
                            manager.SelectField = BaseOrganizeEntity.FieldId + "," + BaseOrganizeEntity.FieldFullName;
                            DataTable dt = manager.GetOrganizeByProvince(province);
                            List<string> list = new List<string>();
                            foreach (DataRow dr in dt.Rows)
                            {
                                list.Add(dr[BaseOrganizeEntity.FieldId].ToString() + "=" + dr[BaseOrganizeEntity.FieldFullName].ToString());
                            }
                            result = list.ToArray();

                            cache.Add(cacheObject, result, null, DateTime.Now.AddHours(8), TimeSpan.Zero, CacheItemPriority.Normal, null);
                        }
                    }
                }
                result = cache[cacheObject] as string[];
            });
            return result;
        }

        /// <summary>
        /// 获得内部部门（公司的组织机构）
        /// </summary>
        /// <param name="userInfo">用户</param>
        /// <param name="city">城市</param>
        /// <returns>数据表</returns>
        public string[] GetOrganizeByCity(BaseUserInfo userInfo, string city)
        {
            var parameter = ServiceParameter.CreateWithMessage(userInfo
                , MethodBase.GetCurrentMethod()
                , this.serviceName
                , AppMessage.OrganizeService_GetOrganizeByCity);
            string[] result = null;
            ServiceUtil.ProcessUserCenterReadDb(parameter, (dbHelper) =>
            {
                System.Web.Caching.Cache cache = HttpRuntime.Cache;
                string cacheObject = "OrganizeByCity" + city;
                if (cache == null || cache[cacheObject] == null)
                {
                    lock (BaseSystemInfo.UserLock)
                    {
                        if (cache == null || cache[cacheObject] == null)
                        {
                            var manager = new BaseOrganizeManager(dbHelper, userInfo);
                            manager.SelectField = BaseOrganizeEntity.FieldId + "," + BaseOrganizeEntity.FieldFullName;
                            DataTable dt = manager.GetOrganizeByCity(city);
                            List<string> list = new List<string>();
                            foreach (DataRow dr in dt.Rows)
                            {
                                list.Add(dr[BaseOrganizeEntity.FieldId].ToString() + "=" + dr[BaseOrganizeEntity.FieldFullName].ToString());
                            }
                            result = list.ToArray();
                            cache.Add(cacheObject, result, null, DateTime.Now.AddHours(8), TimeSpan.Zero, CacheItemPriority.Normal, null);
                        }
                    }
                }
                result = cache[cacheObject] as string[];
            });
            return result;
        }

        /// <summary>
        /// 获得内部部门（公司的组织机构）
        /// </summary>
        /// <param name="userInfo">用户</param>
        /// <param name="district">县区</param>
        /// <returns>数据表</returns>
        public string[] GetOrganizeByDistrict(BaseUserInfo userInfo, string district)
        {
            var parameter = ServiceParameter.CreateWithMessage(userInfo
                , MethodBase.GetCurrentMethod()
                , this.serviceName
                , AppMessage.OrganizeService_GetOrganizeByDistrict);
            string[] result = null;
            ServiceUtil.ProcessUserCenterReadDb(parameter, (dbHelper) =>
            {
                System.Web.Caching.Cache cache = HttpRuntime.Cache;
                string cacheObject = "OrganizeByDistrict" + district;
                if (cache == null || cache[cacheObject] == null)
                {
                    lock (BaseSystemInfo.UserLock)
                    {
                        if (cache == null || cache[cacheObject] == null)
                        {
                            var manager = new BaseOrganizeManager(dbHelper, userInfo);
                            manager.SelectField = BaseOrganizeEntity.FieldId + "," + BaseOrganizeEntity.FieldFullName;
                            DataTable dt = manager.GetOrganizeByDistrict(district);
                            List<string> list = new List<string>();
                            foreach (DataRow dr in dt.Rows)
                            {
                                list.Add(dr[BaseOrganizeEntity.FieldId].ToString() + "=" + dr[BaseOrganizeEntity.FieldFullName].ToString());
                            }
                            result = list.ToArray();
                            cache.Add(cacheObject, result, null, DateTime.Now.AddHours(8), TimeSpan.Zero, CacheItemPriority.Normal, null);
                        }
                    }
                }
                result = cache[cacheObject] as string[];
            });
            return result;
        }



        public static DataTable InnerOrganizeDT = null;

        // 最后检查组织机构时间
        public static DateTime LaseInnerOrganizeCheck = DateTime.MinValue;

        #region public DataTable GetInnerOrganizeDT(BaseUserInfo userInfo) 获取内部组织机构
        /// <summary>
        /// 获取内部组织机构
        /// </summary>
        /// <param name="userInfo">用户</param>
        /// <returns>数据表</returns>
        public DataTable GetInnerOrganizeDT(BaseUserInfo userInfo)
        {
            var parameter = ServiceParameter.CreateWithLog(userInfo
                , MethodBase.GetCurrentMethod());
            ServiceUtil.ProcessUserCenterWriteDbWithLock(parameter, locker, (dbHelper, getOnLine) =>
            {
                var manager = new BaseOrganizeManager(dbHelper, userInfo);
                if (MessageService.LaseInnerOrganizeCheck == DateTime.MinValue)
                {
                    getOnLine = true;
                }
                else
                {
                    // 2008.01.23 JiRiGaLa 修正错误
                    TimeSpan timeSpan = DateTime.Now - MessageService.LaseInnerOrganizeCheck;
                    if ((timeSpan.Minutes * 60 + timeSpan.Seconds) >= BaseSystemInfo.OnLineCheck * 10)
                    {
                        getOnLine = true;
                    }
                }
                if (OnLineStateDT == null || getOnLine)
                {
                    string commandText = string.Empty;

                    if (BaseSystemInfo.OrganizeDynamicLoading)
                    {
                        commandText = "    SELECT * "
                                    + "      FROM " + BaseOrganizeEntity.TableName
                                    + "     WHERE " + BaseOrganizeEntity.FieldDeletionStateCode + " = 0 "
                                    + "           AND " + BaseOrganizeEntity.FieldIsInnerOrganize + " = 1 "
                                    + "           AND " + BaseOrganizeEntity.FieldEnabled + " = 1 "
                                    + "           AND (" + BaseOrganizeEntity.FieldParentId + " IS NULL "
                                    + "                OR " + BaseOrganizeEntity.FieldParentId + " IN (SELECT " + BaseOrganizeEntity.FieldId + " FROM " + BaseOrganizeEntity.TableName + " WHERE " + BaseOrganizeEntity.FieldDeletionStateCode + " = 0 AND " + BaseOrganizeEntity.FieldIsInnerOrganize + " = 1 AND " + BaseOrganizeEntity.FieldEnabled + " = 1 AND " + BaseOrganizeEntity.FieldParentId + " IS NULL)) "
                                    + "  ORDER BY " + BaseOrganizeEntity.FieldSortCode;
                    }
                    else
                    {
                        commandText = "    SELECT * "
                                    + "      FROM " + BaseOrganizeEntity.TableName
                                    + "     WHERE " + BaseOrganizeEntity.FieldDeletionStateCode + " = 0 "
                                    + "           AND " + BaseOrganizeEntity.FieldIsInnerOrganize + " = 1 "
                                    + "           AND " + BaseOrganizeEntity.FieldEnabled + " = 1 "
                                    + "  ORDER BY " + BaseOrganizeEntity.FieldSortCode;
                    }

                    InnerOrganizeDT = manager.Fill(commandText);
                    InnerOrganizeDT.TableName = BaseOrganizeEntity.TableName;
                    MessageService.LaseInnerOrganizeCheck = DateTime.Now;
                }
                // BaseLogManager.Instance.Add(userInfo, this.serviceName, MethodBase.GetCurrentMethod());
                return getOnLine;
            });
            return InnerOrganizeDT;
        }
        #endregion

        /// <summary>
        /// 按父节点获取列表
        /// </summary>
        /// <param name="userInfo">用户</param>
        /// <param name="parentId">父节点</param>
        /// <returns>数据表</returns>
        public DataTable GetDataTableByParent(BaseUserInfo userInfo, string parentId)
        {
            var parameter = ServiceParameter.CreateWithMessage(userInfo
                , MethodBase.GetCurrentMethod()
                , this.serviceName
                , AppMessage.OrganizeService_GetDataTableByParent);
            var dt = new DataTable(BaseOrganizeEntity.TableName);
            ServiceUtil.ProcessUserCenterReadDb(parameter, (dbHelper) =>
            {
                // 获得组织机构列表
                var manager = new BaseOrganizeManager(dbHelper, userInfo);

                List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
                parameters.Add(new KeyValuePair<string, object>(BaseOrganizeEntity.FieldParentId, parentId));
                parameters.Add(new KeyValuePair<string, object>(BaseOrganizeEntity.FieldEnabled, 1));
                parameters.Add(new KeyValuePair<string, object>(BaseOrganizeEntity.FieldDeletionStateCode, 0));

                dt = manager.GetDataTable(parameters, BaseOrganizeEntity.FieldSortCode);
                dt.DefaultView.Sort = BaseOrganizeEntity.FieldSortCode;
                dt.TableName = BaseOrganizeEntity.TableName;
            });
            return dt;
        }
    }
}