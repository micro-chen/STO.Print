//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd.  
//-----------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Web;
using System.Web.Caching;

namespace DotNet.Business
{
    using DotNet.IService;
    using DotNet.Model;
    using DotNet.Utilities;

    /// <summary>
    /// MessageService
    /// 消息服务
    /// 
    /// 修改记录
    /// 
    ///		2015.11.25 版本：2.0 JiRiGaLa 进行缓存优化，ExecuteReader 进行优化。
    ///		2013.11.12 版本：1.0 JiRiGaLa 创建。
    ///		
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2015.11.25</date>
    /// </author> 
    /// </summary>
    public partial class MessageService : IMessageService
    {
        /// <summary>
        /// 获取省份
        /// </summary>
        /// <param name="userInfo">用户</param>
        /// <returns>省份数组</returns>
        public string[] GetProvince(BaseUserInfo userInfo)
        {
            string[] result = null;

#if Redis
            result = BaseOrganizeManager.GetProvinceByCache();
#else

            var parameter = ServiceInfo.Create(userInfo, MethodBase.GetCurrentMethod()); 
            ServiceUtil.ProcessUserCenterReadDb(userInfo, parameter, (dbHelper) =>
            {
                System.Web.Caching.Cache cache = HttpRuntime.Cache;
                string cacheObject = "AreaOrganizeProvince";
                //if (cache != null && cache[cacheObject] == null)
                //{

                if (cache != null && cache[cacheObject] == null)
                {
                    // BaseAreaManager areaManager = new BaseAreaManager(dbHelper, result);
                    // result = areaManager.GetProvinceList();
                    var manager = new BaseOrganizeManager(dbHelper, userInfo);
                    result = manager.GetProvince();
                    cache.Add(cacheObject, result, null, DateTime.Now.AddHours(8), TimeSpan.Zero, CacheItemPriority.Normal, null);
                }

                result = cache[cacheObject] as string[];
            });
#endif

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
            string[] result = null;

#if Redis
    result = BaseOrganizeManager.GetCityByCache(province);
#else

            var parameter = ServiceInfo.Create(userInfo, MethodBase.GetCurrentMethod()); 
            ServiceUtil.ProcessUserCenterReadDb(userInfo, parameter, (dbHelper) =>
            {
                System.Web.Caching.Cache cache = HttpRuntime.Cache;
                string cacheObject = "AreaOrganizeCity" + province;

                if (cache != null && cache[cacheObject] == null)
                {
                    // BaseAreaManager areaManager = new BaseAreaManager(dbHelper, result);
                    // result = areaManager.GetCityList(provinceId);
                    var manager = new BaseOrganizeManager(dbHelper, userInfo);
                    result = manager.GetCity(province);
                    cache.Add(cacheObject, result, null, DateTime.Now.AddHours(8), TimeSpan.Zero, CacheItemPriority.Normal, null);
                }

                result = cache[cacheObject] as string[];
            });
#endif

            return result;
        }

        /// <summary>
        /// 获取县区
        /// </summary>
        /// <param name="userInfo">用户</param>
        /// <param name="province">省份</param>
        /// <param name="city">城市</param>
        /// <returns>县区数组</returns>
        public string[] GetDistrict(BaseUserInfo userInfo, string province, string city)
        {
            string[] result = null;

#if Redis
            result = BaseOrganizeManager.GetDistrictByCache(province, city);
#else

            var parameter = ServiceInfo.Create(userInfo, MethodBase.GetCurrentMethod()); 
            ServiceUtil.ProcessUserCenterReadDb(userInfo, parameter, (dbHelper) =>
            {
                System.Web.Caching.Cache cache = HttpRuntime.Cache;
                string cacheObject = "AreaOrganizeDistrict" + city;

                if (cache != null && cache[cacheObject] == null)
                {
                    // BaseAreaManager areaManager = new BaseAreaManager(dbHelper, result);
                    // result = areaManager.GetDistrictList(cityId);
                    var manager = new BaseOrganizeManager(dbHelper, userInfo);
                    result = manager.GetDistrict(province, city);
                    cache.Add(cacheObject, result, null, DateTime.Now.AddHours(8), TimeSpan.Zero, CacheItemPriority.Normal, null);
                }

                result = cache[cacheObject] as string[];
            });
#endif

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
            string[] result = null;

            /*
            var parameter = ServiceParameter.CreateWithMessage(userInfo
                , MethodBase.GetCurrentMethod()
                , this.serviceName
                , AppMessage.OrganizeService_GetOrganizeByProvince);
            
            ServiceUtil.ProcessUserCenterReadDb(userInfo, parameter, (dbHelper) =>
            {
                System.Web.Caching.Cache cache = HttpRuntime.Cache;
                string cacheObject = "OrganizeByProvince" + province;
                if (cache != null && cache[cacheObject] == null)
                {
                    // lock (BaseSystemInfo.UserLock)
                    //{
                        //if (cache != null && cache[cacheObject] == null)
                        //{
                            var manager = new BaseOrganizeManager(dbHelper, userInfo);
                            manager.SelectFields = BaseOrganizeEntity.FieldId + "," + BaseOrganizeEntity.FieldFullName;
                            DataTable dt = manager.GetOrganizeByProvince(province);
                            List<string> list = new List<string>();
                            foreach (DataRow dr in dt.Rows)
                            {
                                list.Add(dr[BaseOrganizeEntity.FieldId].ToString() + "=" + dr[BaseOrganizeEntity.FieldFullName].ToString());
                            }
                            result = list.ToArray();

                            cache.Add(cacheObject, result, null, DateTime.Now.AddHours(8), TimeSpan.Zero, CacheItemPriority.Normal, null);
                        //}
                    //}
                }
                result = cache[cacheObject] as string[];
            });
            */

            return result;
        }

        /// <summary>
        /// 获得内部部门（公司的组织机构）
        /// </summary>
        /// <param name="userInfo">用户</param>
        /// <param name="province">省</param>
        /// <param name="city">城市</param>
        /// <returns>数据表</returns>
        public string[] GetOrganizeByCity(BaseUserInfo userInfo, string province, string city)
        {
            string[] result = null;

            /*
            var parameter = ServiceParameter.CreateWithMessage(userInfo
                , MethodBase.GetCurrentMethod()
                , this.serviceName
                , AppMessage.OrganizeService_GetOrganizeByCity);
            
            ServiceUtil.ProcessUserCenterReadDb(userInfo, parameter, (dbHelper) =>
            {
                System.Web.Caching.Cache cache = HttpRuntime.Cache;
                string cacheObject = "OrganizeByCity" + city;
                if (cache != null && cache[cacheObject] == null)
                {
                    // lock (BaseSystemInfo.UserLock)
                    //{
                        //if (cache != null && cache[cacheObject] == null)
                        //{
                            var manager = new BaseOrganizeManager(dbHelper, userInfo);
                            manager.SelectFields = BaseOrganizeEntity.FieldId + "," + BaseOrganizeEntity.FieldFullName;
                            DataTable dt = manager.GetOrganizeByCity(province, city);
                            List<string> list = new List<string>();
                            foreach (DataRow dr in dt.Rows)
                            {
                                list.Add(dr[BaseOrganizeEntity.FieldId].ToString() + "=" + dr[BaseOrganizeEntity.FieldFullName].ToString());
                            }
                            result = list.ToArray();
                            cache.Add(cacheObject, result, null, DateTime.Now.AddHours(8), TimeSpan.Zero, CacheItemPriority.Normal, null);
                        //}
                    //}
                }
                result = cache[cacheObject] as string[];
            });
            */

            return result;
        }

        /// <summary>
        /// 获得公司列表
        /// </summary>
        /// <param name="userInfo">用户</param>
        /// <param name="province">省</param>
        /// <param name="city">城市</param>
        /// <param name="district">县区</param>
        /// <returns>数据表</returns>
        public string[] GetOrganizeByDistrict(BaseUserInfo userInfo, string province, string city, string district)
        {
            string[] result = null;

#if Redis
            result = BaseOrganizeManager.GetOrganizeByDistrictByCache(province, city, district);
#else

            var parameter = ServiceInfo.Create(userInfo, MethodBase.GetCurrentMethod()); 
            ServiceUtil.ProcessUserCenterReadDb(userInfo, parameter, (dbHelper) =>
            {
                System.Web.Caching.Cache cache = HttpRuntime.Cache;
                string cacheObject = "OrganizeByDistrict" + district;
                if (cache != null && cache[cacheObject] == null)
                {
                    var manager = new BaseOrganizeManager(dbHelper, userInfo);
                    result = manager.GetOrganizeByDistrict(province, city, district);

                    cache.Add(cacheObject, result, null, DateTime.Now.AddHours(8), TimeSpan.Zero, CacheItemPriority.Normal, null);
                }
                result = cache[cacheObject] as string[];
            });
#endif

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
            var parameter = ServiceInfo.Create(userInfo, MethodBase.GetCurrentMethod()); 
            ServiceUtil.ProcessUserCenterWriteDb(userInfo, parameter, (dbHelper) =>
            {
                var manager = new BaseOrganizeManager(dbHelper, userInfo);
                if (MessageService.LaseInnerOrganizeCheck == DateTime.MinValue)
                {
                }
                else
                {
                    // 2008.01.23 JiRiGaLa 修正错误
                    TimeSpan timeSpan = DateTime.Now - MessageService.LaseInnerOrganizeCheck;
                    if ((timeSpan.Minutes * 60 + timeSpan.Seconds) >= BaseSystemInfo.OnLineCheck * 10)
                    {
                    }
                }
                if (OnLineStateDT == null)
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
                // BaseLogManager.Instance.Add(result, this.serviceName, MethodBase.GetCurrentMethod());
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
            var result = new DataTable(BaseOrganizeEntity.TableName);

            var parameter = ServiceInfo.Create(userInfo, MethodBase.GetCurrentMethod()); 
            ServiceUtil.ProcessUserCenterReadDb(userInfo, parameter, (dbHelper) =>
            {
                // 获得组织机构列表
                var manager = new BaseOrganizeManager(dbHelper, userInfo);

                List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
                parameters.Add(new KeyValuePair<string, object>(BaseOrganizeEntity.FieldParentId, parentId));
                parameters.Add(new KeyValuePair<string, object>(BaseOrganizeEntity.FieldEnabled, 1));
                parameters.Add(new KeyValuePair<string, object>(BaseOrganizeEntity.FieldDeletionStateCode, 0));

                result = manager.GetDataTable(parameters, BaseOrganizeEntity.FieldSortCode);
                result.DefaultView.Sort = BaseOrganizeEntity.FieldSortCode;
                result.TableName = BaseOrganizeEntity.TableName;
            });

            return result;
        }
    }
}