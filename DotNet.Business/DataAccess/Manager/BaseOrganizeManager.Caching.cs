//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2015 , Hairihan TECH, Ltd. 
//-----------------------------------------------------------------

using System;
using System.Web;
using System.Web.Caching;

namespace DotNet.Business
{
    using DotNet.Model;

    /// <remarks>
    /// BaseOrganizeManager
    /// 组织机构管理
    /// 
    /// 修改纪录
    /// 
    ///	版本：2.0 2015.12.11  JiRiGaLa 缓存优化。
    ///	版本：1.0 2012.12.17  JiRiGaLa 选项管理从缓存读取，通过编号显示名称的函数完善。
    ///	
    /// <author>  
    ///		<name>JiRiGaLa</name>
    ///		<date>2015.12.11</date>
    /// </author> 
    /// </remarks>
    public partial class BaseOrganizeManager
    {
        public static bool RemoveCache(string key)
        {
            bool result = false;

            if (!string.IsNullOrWhiteSpace(key))
            {
                System.Web.Caching.Cache cache = HttpRuntime.Cache;

                string cacheObject = "Organize" + key;
                cache.Remove(cacheObject);

                cacheObject = "OrganizeByCode" + key;
                cache.Remove(cacheObject);

                cacheObject = "OrganizeByName" + key;
                cache.Remove(cacheObject);
            }

            return result;
        }

        public static BaseOrganizeEntity GetCache(string key)
        {
            BaseOrganizeEntity result = null;

            if (!string.IsNullOrWhiteSpace(key))
            {
                System.Web.Caching.Cache cache = HttpRuntime.Cache;
                if (cache != null && cache[key] != null)
                {
                    result = cache[key] as BaseOrganizeEntity;
                }
            }

            return result;
        }

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="entity">实体</param>
        public static void SetCache(BaseOrganizeEntity entity)
        {
            if (entity != null && entity.Id != null)
            {
                string key = string.Empty;
                System.Web.Caching.Cache cache = HttpRuntime.Cache;

                key = "Organize" + entity.Id;
                cache.Add(key, entity, null, DateTime.Now.AddHours(16), TimeSpan.Zero, CacheItemPriority.Normal, null);

                key = "OrganizeByCode" + entity.Code;
                cache.Add(key, entity, null, DateTime.Now.AddHours(16), TimeSpan.Zero, CacheItemPriority.Normal, null);

                key = "OrganizeByName" + entity.FullName;
                cache.Add(key, entity, null, DateTime.Now.AddHours(16), TimeSpan.Zero, CacheItemPriority.Normal, null);
            }
        }

        public static BaseOrganizeEntity GetObjectByCodeByCache(string code)
        {
            BaseOrganizeEntity result = null;

            if (string.IsNullOrEmpty(code))
            {
                return result;
            }

            string key = "OrganizeByCode" + code;
            result = GetCache(key);

            return result;
        }

        public static BaseOrganizeEntity GetObjectByNameByCache(string fullName)
        {
            BaseOrganizeEntity result = null;

            string key = "OrganizeByName" + fullName;
            result = GetCache(key);

            return result;
        }

        /// <summary>
        /// 获取省份
        /// 2015-11-25 吉日嘎拉 采用缓存方式，效率应该会更高
        /// </summary>
        /// <param name="area">区域</param>
        /// <returns>省份数组</returns>
        public static string[] GetProvinceByCache(string area = null)
        {
            string[] result = null;

            if (string.IsNullOrWhiteSpace(area))
            {
                area = string.Empty;
            }
            string key = "OrganizeProvince_" + area;
            string province = string.Empty;
            using (var redisClient = PooledRedisHelper.GetOrganizeClient())
            {
                province = redisClient.Get<string>(key);
            }
            if (!string.IsNullOrWhiteSpace(province))
            {
                result = province.Split('.');
            }
            else
            {
                // 从数据库读取数据
                BaseOrganizeManager organizeManager = new BaseOrganizeManager();
                result = organizeManager.GetProvince(area);
                // 设置缓存
                if (result != null && result.Length > 0)
                {
                    province = string.Join(".", result);
                    using (var redisClient = PooledRedisHelper.GetOrganizeClient())
                    {
                        redisClient.Set<string>(key, province, DateTime.Now.AddHours(4));
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 获取城市
        /// 2015-11-25 吉日嘎拉 采用缓存方式，效率应该会更高
        /// </summary>
        /// <param name="province">省份</param>
        /// <returns>城市数组</returns>
        public static string[] GetCityByCache(string province = null)
        {
            string[] result = null;

            string city = string.Empty;
            if (string.IsNullOrWhiteSpace(province))
            {
                province = string.Empty;
            }
            string key = "OrganizeCity_" + province;
            using (var redisClient = PooledRedisHelper.GetOrganizeClient())
            {
                city = redisClient.Get<string>(key);
            }
            if (!string.IsNullOrWhiteSpace(city))
            {
                result = city.Split('.');
            }
            else
            {
                // 从数据库读取数据
                BaseOrganizeManager organizeManager = new BaseOrganizeManager();
                result = organizeManager.GetCity(province);
                // 设置缓存
                if (result != null && result.Length > 0)
                {
                    city = string.Join(".", result);
                    using (var redisClient = PooledRedisHelper.GetOrganizeClient())
                    {
                        redisClient.Set<string>(key, city, DateTime.Now.AddHours(4));
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 获取县区
        /// 2015-11-25 吉日嘎拉 采用缓存方式，效率应该会更高
        /// </summary>
        /// <param name="province">省份</param>
        /// <param name="city">城市</param>
        /// <returns>县区数组</returns>
        public static string[] GetDistrictByCache(string province, string city)
        {
            string[] result = null;

            string district = string.Empty;
            if (string.IsNullOrWhiteSpace(province))
            {
                province = string.Empty;
            }
            if (string.IsNullOrWhiteSpace(city))
            {
                city = string.Empty;
            }
            string key = "OrganizeDistrict_" + province + "_" + city;
            using (var redisClient = PooledRedisHelper.GetOrganizeClient())
            {
                district = redisClient.Get<string>(key);
            }
            if (!string.IsNullOrWhiteSpace(district))
            {
                result = district.Split('.');
            }
            else
            {
                // 从数据库读取数据
                BaseOrganizeManager organizeManager = new BaseOrganizeManager();
                result = organizeManager.GetDistrict(province, city);
                // 设置缓存
                if (result != null && result.Length > 0)
                {
                    district = string.Join(".", result);
                    using (var redisClient = PooledRedisHelper.GetOrganizeClient())
                    {
                        redisClient.Set<string>(key, district, DateTime.Now.AddHours(4));
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 获得公司列表
        /// 2015-11-25 吉日嘎拉 进行改进
        /// </summary>
        /// <param name="province">省</param>
        /// <param name="city">城市</param>
        /// <param name="district">县区</param>
        /// <returns>数据表</returns>
        public static string[] GetOrganizeByDistrictByCache(string province, string city, string district)
        {
            string[] result = null;

            string organize = string.Empty;
            if (string.IsNullOrWhiteSpace(province))
            {
                province = string.Empty;
            }
            if (string.IsNullOrWhiteSpace(city))
            {
                city = string.Empty;
            }
            if (string.IsNullOrWhiteSpace(district))
            {
                district = string.Empty;
            }
            string key = "OrganizeByDistrict_" + province + "_" + city + "_" + district;
            using (var redisClient = PooledRedisHelper.GetOrganizeClient())
            {
                organize = redisClient.Get<string>(key);
            }
            if (!string.IsNullOrWhiteSpace(organize))
            {
                result = organize.Split('.');
            }
            else
            {
                // 从数据库读取数据
                BaseOrganizeManager organizeManager = new BaseOrganizeManager();
                result = organizeManager.GetOrganizeByDistrict(province, city, district);
                // 设置缓存
                if (result != null && result.Length > 0)
                {
                    organize = string.Join(".", result);
                    using (var redisClient = PooledRedisHelper.GetOrganizeClient())
                    {
                        redisClient.Set<string>(key, organize, DateTime.Now.AddHours(4));
                    }
                }
            }

            return result;
        }
    }
}