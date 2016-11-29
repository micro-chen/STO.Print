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

    /// <remarks>
    /// BaseOrganizeManager
    /// 组织机构管理
    /// 
    /// 修改记录
    /// 
    ///	版本：1.6 2016.01.08  JiRiGaLa 实现只读连接，读写分离。
    ///	版本：1.5 2016.01.04  JiRiGaLa 写入主键的方式进行改进。
    ///	版本：1.4 2015.10.09  JiRiGaLa GetCache方法实现。
    ///	版本：1.3 2015.07.17  JiRiGaLa 缓存清除功能实现（其实是重新获取了就可以了）。
    ///	版本：1.2 2015.06.15  JiRiGaLa 增加强制设置缓存的功能。
    ///	版本：1.1 2015.04.11  JiRiGaLa 取消锁、提高效率、从独立的小库里读取缓存数据、希望能提高性能。
    ///	版本：1.0 2015.01.06  JiRiGaLa 缓存优化。
    ///	
    /// <author>  
    ///		<name>JiRiGaLa</name>
    ///		<date>2016.01.08</date>
    /// </author> 
    /// </remarks>
    public partial class BaseOrganizeManager
    {
        public static bool RemoveCache(string key)
        {
            bool result = false;

            if (!string.IsNullOrWhiteSpace(key))
            {
                using (var redisClient = PooledRedisHelper.GetClient())
                {
                    result = redisClient.Remove(key);
                }
            }

            return result;
        }

        public static BaseOrganizeEntity GetCacheByKey(string key)
        {
            BaseOrganizeEntity result = null;

            if (!string.IsNullOrWhiteSpace(key))
            {
                // 2016-01-08 吉日嘎拉 实现只读连接，读写分离
                using (var redisClient = PooledRedisHelper.GetReadOnlyClient())
                {
                    result = redisClient.Get<BaseOrganizeEntity>(key);
                }
            }

            return result;
        }

        public static BaseOrganizeEntity GetCacheByKey(IRedisClient redisClient, string key)
        {
            BaseOrganizeEntity result = null;

            if (!string.IsNullOrWhiteSpace(key))
            {
                // 2016-01-08 吉日嘎拉 实现只读连接，读写分离
                result = redisClient.Get<BaseOrganizeEntity>(key);
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
                using (var redisClient = PooledRedisHelper.GetClient())
                {
                    // key = "Organize:" + entity.Id;
                    key = "O:" + entity.Id;
                    redisClient.Set<BaseOrganizeEntity>(key, entity, DateTime.Now.AddHours(16));

                    // key = "OrganizeByCode:" + entity.Code;
                    key = "OBC:" + entity.Code;
                    redisClient.Set<string>(key, entity.Id, DateTime.Now.AddHours(16));

                    //key = "OrganizeByName:" + entity.FullName;
                    key = "OBN:" + entity.FullName;
                    redisClient.Set<string>(key, entity.Id, DateTime.Now.AddHours(16));
                }
            }
        }

        public static BaseOrganizeEntity GetObjectByCodeByCache(string code)
        {
            BaseOrganizeEntity result = null;

            if (string.IsNullOrEmpty(code))
            {
                return result;
            }

            // string key = "OrganizeByCode:" + code;
            string key = "OBC:" + code;
            string id = string.Empty;
            using (var redisClient = PooledRedisHelper.GetReadOnlyClient())
            {
                id = redisClient.Get<string>(key);

                if (!string.IsNullOrWhiteSpace(id))
                {
                    result = GetObjectByCache(redisClient, id);
                }
                else
                {
                    // 从数据库读取数据
                    BaseOrganizeManager organizeManager = new BaseOrganizeManager();
                    result = organizeManager.GetObjectByCode(code);
                    // 设置缓存
                    if (result != null)
                    {
                        SetCache(result);
                    }
                }
            }

            return result;
        }

        public static BaseOrganizeEntity GetObjectByNameByCache(string fullName)
        {
            BaseOrganizeEntity result = null;

            // string key = "OrganizeByName:" + fullName;
            string key = "OBN:" + fullName;
            string id = string.Empty;
            using (var redisClient = PooledRedisHelper.GetReadOnlyClient())
            {
                id = redisClient.Get<string>(key);

                if (!string.IsNullOrWhiteSpace(id))
                {
                    result = GetObjectByCache(redisClient, id);
                }
                else
                {
                    // 从数据库读取数据
                    BaseOrganizeManager organizeManager = new BaseOrganizeManager();
                    result = organizeManager.GetObjectByName(fullName);
                    // 设置缓存
                    if (result != null)
                    {
                        SetCache(result);
                    }
                }
            }

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
            // string key = "OrganizeProvince:" + area;
            string key = "OP:" + area;
            string province = string.Empty;
            using (var redisClient = PooledRedisHelper.GetReadOnlyClient())
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
                    using (var redisClient = PooledRedisHelper.GetClient())
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
            // string key = "OrganizeCity:" + province;
            string key = "OC:" + province;
            using (var redisClient = PooledRedisHelper.GetReadOnlyClient())
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
                    using (var redisClient = PooledRedisHelper.GetClient())
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
            // string key = "OrganizeDistrict:" + province + ":" + city;
            string key = "OD:" + province + ":" + city;
            using (var redisClient = PooledRedisHelper.GetReadOnlyClient())
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
                    using (var redisClient = PooledRedisHelper.GetClient())
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
            // string key = "OrganizeByDistrict:" + province + ":" + city + ":" + district;
            string key = "OBD:" + province + ":" + city + ":" + district;
            using (var redisClient = PooledRedisHelper.GetReadOnlyClient())
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
                    using (var redisClient = PooledRedisHelper.GetClient())
                    {
                        redisClient.Set<string>(key, organize, DateTime.Now.AddHours(4));
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 快速模糊检索网点（高速缓存）
        /// 
        /// 01：所有网点查询的缓存数据方法 prefix = string.empty 
        /// 02: 结算中心主键缓存数据方法 prefix = "CostCenterId:" + costCenterId + ":"
        /// 03：按省缓存数据方法 prefix = "ProvinceId:" + provinceId + ":"
        /// 04：按市缓存数据方法 prefix = "CityId:" + cityId + ":"
        /// 05：父级主键缓存数据方法 prefix = "ParentId:" + parentId + ":"
        /// 06：所有下属递归的方式进行快速缓存检索（START WITH CONNECT BY PRIOR） prefix = "StartId:" + startId + ":"
        /// 07：发航空 prefix = "SendAir:"
        /// 08：接收订单 prefix = "IsReceiveOrder:"
        /// 09：接收投诉工单 prefix = "IsReceiveComplain:"
        /// 10: 一级网点主键缓存数据方法 prefix = "CompanyId:" + companyId + ":"
        /// 
        /// </summary>
        /// <param name="prefix">搜索前缀</param>
        /// <param name="key">搜搜关键字</param>
        /// <param name="returnId">返回主键</param>
        /// <param name="returnCode">返回编号</param>
        /// <returns>查询结果数组</returns>
        public static List<KeyValuePair<string, string>> GetOrganizesByKey(string prefix, string key, bool returnId = true, bool showCode = false, int topLimit = 20)
        {
            List<KeyValuePair<string, string>> result = new List<KeyValuePair<string, string>>();

            /*
            var where = "(" + BaseOrganizeEntity.FieldFullName + " LIKE'%" + q
                + "%' OR " + BaseOrganizeEntity.FieldCode + " LIKE '%" + q + "%' OR "
                + BaseOrganizeEntity.FieldSimpleSpelling + " LIKE '%" + q + "%' OR "
                + BaseOrganizeEntity.FieldQuickQuery + " LIKE '%" + q + "%') AND "
                + BaseOrganizeEntity.FieldEnabled + " = 1 "
                + BaseOrganizeEntity.FieldDeletionStateCode + " = 0 ";
            */

            if (string.IsNullOrEmpty(key))
            {
                key = string.Empty;
            }

            // 2016-01-06 吉日嘎拉 优化代码，提高检索效率，全部转小写进行比对
            key = prefix + key.ToLower();

            // 2016-02-19 宋彪 PooledRedisHelper.GetReadOnlyClient() 改为 PooledRedisHelper.GetSpellingClient()
            using (var redisClient = PooledRedisHelper.GetSpellingReadOnlyClient())
            {
                // 每次从有序集合中的消息取出20条信息，返回给客户端，在客户端展示阅读成功后才从内存里去掉。
                List<string> list = redisClient.GetRangeFromSortedSet(key, 0, topLimit);

                if (list != null)
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        // 从内存里直接获取这个对应的消息
                        string[] organize = list[i].Split(';');
                        string id = organize[0];
                        string code = organize[1];
                        string fullName = organize[2];
                        if (returnId)
                        {
                            if (showCode)
                            {
                                result.Add(new KeyValuePair<string, string>(id, fullName + " " + code));
                            }
                            else
                            {
                                result.Add(new KeyValuePair<string, string>(id, fullName));
                            }
                        }
                        else
                        {
                            if (showCode)
                            {
                                result.Add(new KeyValuePair<string, string>(code, fullName + " " + code));
                            }
                            else
                            {
                                result.Add(new KeyValuePair<string, string>(code, fullName));
                            }
                        }
                    }
                }
            }

            return result;
        }

        public static void CachePreheatingSpelling(IRedisClient redisClient, BaseOrganizeEntity organizeEntity, double score = 0)
        {
            // 读取到的数据直接强制设置到缓存里
            string id = organizeEntity.Id;
            // 2016-01-06 吉日嘎拉 网点编号不能大小写转换，否则查询就乱套了，不能改变原样
            string code = organizeEntity.Code;
            string fullName = organizeEntity.FullName;
            string simpleSpelling = organizeEntity.SimpleSpelling;

            string organize = id + ";" + code + ";" + fullName;
            if (organizeEntity.Enabled.HasValue && organizeEntity.Enabled.Value == 0)
            {
                // organize += " 失效"; 
            }
            if (organizeEntity.DeletionStateCode.HasValue && organizeEntity.DeletionStateCode.Value == 1)
            {
                // organize += " 已删除";
            }

            string key = string.Empty;

            // 01：所有网点查询的缓存数据方法
            for (int i = 2; i <= code.Length; i++)
            {
                // 2016-01-06 吉日嘎拉 这里需要小写，提高效率，提高有善度
                key = code.Substring(0, i).ToLower();
                redisClient.AddItemToSortedSet(key, organize, score);
                redisClient.ExpireEntryAt(key, DateTime.Now.AddDays(15));
            }
            for (int i = 1; i <= fullName.Length; i++)
            {
                key = fullName.Substring(0, i).ToLower();
                redisClient.AddItemToSortedSet(key, organize, score);
                redisClient.ExpireEntryAt(key, DateTime.Now.AddDays(15));
            }
            for (int i = 2; i <= simpleSpelling.Length; i++)
            {
                key = simpleSpelling.Substring(0, i);
                redisClient.AddItemToSortedSet(key, organize, score);
                redisClient.ExpireEntryAt(key, DateTime.Now.AddDays(15));
            }

            // 02：结算中心主键缓存数据方法
            string costCenterId = organizeEntity.CostCenterId;
            if (!string.IsNullOrEmpty(costCenterId))
            {
                for (int i = 2; i <= code.Length; i++)
                {
                    // 2016-01-06 吉日嘎拉 这里需要小写，提高效率，提高有善度
                    key = "CostCenterId:" + costCenterId + ":" + code.Substring(0, i).ToLower(); ;
                    redisClient.AddItemToSortedSet(key, organize, score);
                    redisClient.ExpireEntryAt(key, DateTime.Now.AddDays(15));
                }
                for (int i = 1; i <= fullName.Length; i++)
                {
                    key = "CostCenterId:" + costCenterId + ":" + fullName.Substring(0, i).ToLower(); ;
                    redisClient.AddItemToSortedSet(key, organize, score);
                    redisClient.ExpireEntryAt(key, DateTime.Now.AddDays(15));
                }
                for (int i = 2; i <= simpleSpelling.Length; i++)
                {
                    key = "CostCenterId:" + costCenterId + ":" + simpleSpelling.Substring(0, i);
                    redisClient.AddItemToSortedSet(key, organize, score);
                    redisClient.ExpireEntryAt(key, DateTime.Now.AddDays(15));
                }
            }

            // 10: 按一级网点缓存数据方法
            string companyId = organizeEntity.CompanyId;
            if (!string.IsNullOrEmpty(companyId))
            {
                for (int i = 0; i <= code.Length; i++)
                {
                    // 2016-01-18 吉日嘎拉 这里需要小写，提高效率，提高有善度
                    key = "CompanyId:" + companyId + ":" + code.Substring(0, i).ToLower();
                    redisClient.AddItemToSortedSet(key, organize, score);
                    redisClient.ExpireEntryAt(key, DateTime.Now.AddDays(15));
                }
                for (int i = 1; i <= fullName.Length; i++)
                {
                    key = "CompanyId:" + companyId + ":" + fullName.Substring(0, i).ToLower();
                    redisClient.AddItemToSortedSet(key, organize, score);
                    redisClient.ExpireEntryAt(key, DateTime.Now.AddDays(15));
                }
                for (int i = 2; i <= simpleSpelling.Length; i++)
                {
                    key = "CompanyId:" + companyId + ":" + simpleSpelling.Substring(0, i);
                    redisClient.AddItemToSortedSet(key, organize, score);
                    redisClient.ExpireEntryAt(key, DateTime.Now.AddDays(15));
                }
            }

            // 03：按省缓存数据方法
            string provinceId = organizeEntity.ProvinceId;
            if (!string.IsNullOrEmpty(provinceId))
            {
                for (int i = 2; i <= code.Length; i++)
                {
                    // 2016-01-06 吉日嘎拉 这里需要小写，提高效率，提高有善度
                    key = "ProvinceId:" + provinceId + ":" + code.Substring(0, i).ToLower(); ;
                    redisClient.AddItemToSortedSet(key, organize, score);
                    redisClient.ExpireEntryAt(key, DateTime.Now.AddDays(15));
                }
                for (int i = 1; i <= fullName.Length; i++)
                {
                    key = "ProvinceId:" + provinceId + ":" + fullName.Substring(0, i).ToLower(); ;
                    redisClient.AddItemToSortedSet(key, organize, score);
                    redisClient.ExpireEntryAt(key, DateTime.Now.AddDays(15));
                }
                for (int i = 2; i <= simpleSpelling.Length; i++)
                {
                    key = "ProvinceId:" + provinceId + ":" + simpleSpelling.Substring(0, i);
                    redisClient.AddItemToSortedSet(key, organize, score);
                    redisClient.ExpireEntryAt(key, DateTime.Now.AddDays(15));
                }
            }

            // 04：按市缓存数据方法
            string cityId = organizeEntity.CityId;
            if (!string.IsNullOrEmpty(cityId))
            {
                for (int i = 2; i <= code.Length; i++)
                {
                    // 2016-01-06 吉日嘎拉 这里需要小写，提高效率，提高有善度
                    key = "CityId:" + cityId + ":" + code.Substring(0, i).ToLower();
                    redisClient.AddItemToSortedSet(key, organize, score);
                    redisClient.ExpireEntryAt(key, DateTime.Now.AddDays(15));
                }
                for (int i = 1; i <= fullName.Length; i++)
                {
                    key = "CityId:" + cityId + ":" + fullName.Substring(0, i).ToLower();
                    redisClient.AddItemToSortedSet(key, organize, score);
                    redisClient.ExpireEntryAt(key, DateTime.Now.AddDays(15));
                }
                for (int i = 2; i <= simpleSpelling.Length; i++)
                {
                    key = "CityId:" + cityId + ":" + simpleSpelling.Substring(0, i);
                    redisClient.AddItemToSortedSet(key, organize, score);
                    redisClient.ExpireEntryAt(key, DateTime.Now.AddDays(15));
                }
            }

            // 05：父级主键缓存数据方法
            string parentId = organizeEntity.ParentId;
            if (!string.IsNullOrEmpty(parentId))
            {
                for (int i = 2; i <= code.Length; i++)
                {
                    // 2016-01-06 吉日嘎拉 这里需要小写，提高效率，提高有善度
                    key = "ParentId:" + parentId + ":" + code.Substring(0, i).ToLower(); ;
                    redisClient.AddItemToSortedSet(key, organize, score);
                    redisClient.ExpireEntryAt(key, DateTime.Now.AddDays(15));
                }
                for (int i = 1; i <= fullName.Length; i++)
                {
                    key = "ParentId:" + parentId + ":" + fullName.Substring(0, i).ToLower();
                    redisClient.AddItemToSortedSet(key, organize, score);
                    redisClient.ExpireEntryAt(key, DateTime.Now.AddDays(15));
                }
                for (int i = 2; i <= simpleSpelling.Length; i++)
                {
                    key = "ParentId:" + parentId + ":" + simpleSpelling.Substring(0, i).Trim();
                    redisClient.AddItemToSortedSet(key, organize, score);
                    redisClient.ExpireEntryAt(key, DateTime.Now.AddDays(15));
                }
            }

            // 06：所有下属递归的方式进行快速缓存检索（START WITH CONNECT BY PRIOR） 包括自己
            string startId = organizeEntity.Id;
            while (!string.IsNullOrEmpty(startId))
            {
                for (int i = 2; i <= code.Length; i++)
                {
                    // 2016-01-06 吉日嘎拉 这里需要小写，提高效率，提高有善度
                    key = "StartId:" + startId + ":" + code.Substring(0, i).ToLower();
                    redisClient.AddItemToSortedSet(key, organize, score);
                    redisClient.ExpireEntryAt(key, DateTime.Now.AddDays(15));
                }
                for (int i = 1; i <= fullName.Length; i++)
                {
                    key = "StartId:" + startId + ":" + fullName.Substring(0, i).ToLower(); ;
                    redisClient.AddItemToSortedSet(key, organize, score);
                    redisClient.ExpireEntryAt(key, DateTime.Now.AddDays(15));
                }
                for (int i = 2; i <= simpleSpelling.Length; i++)
                {
                    key = "StartId:" + startId + ":" + simpleSpelling.Substring(0, i);
                    redisClient.AddItemToSortedSet(key, organize, score);
                    redisClient.ExpireEntryAt(key, DateTime.Now.AddDays(15));
                }
                // 获取上级的上级，一直进行循环，在缓存里进行计算，提高效率
                startId = BaseOrganizeManager.GetParentIdByCache(startId);
            }

            // 07：发航空
            string sendAir = organizeEntity.SendAir.ToString();
            if (!string.IsNullOrEmpty(sendAir) && sendAir.Equals("1"))
            {
                for (int i = 2; i <= code.Length; i++)
                {
                    // 2016-01-06 吉日嘎拉 这里需要小写，提高效率，提高有善度
                    key = "SendAir:" + code.Substring(0, i).ToLower();
                    redisClient.AddItemToSortedSet(key, organize, score);
                    redisClient.ExpireEntryAt(key, DateTime.Now.AddDays(15));
                }
                for (int i = 1; i <= fullName.Length; i++)
                {
                    key = "SendAir:" + fullName.Substring(0, i).ToLower();
                    redisClient.AddItemToSortedSet(key, organize, score);
                    redisClient.ExpireEntryAt(key, DateTime.Now.AddDays(15));
                }
                for (int i = 2; i <= simpleSpelling.Length; i++)
                {
                    key = "SendAir:" + simpleSpelling.Substring(0, i);
                    redisClient.AddItemToSortedSet(key, organize, score);
                    redisClient.ExpireEntryAt(key, DateTime.Now.AddDays(15));
                }
            }

            // 输出到屏幕看看运行效果如何？心里有个数
            System.Console.WriteLine(score.ToString() + " " + organize);
        }

        public static void CachePreheatingSpelling(bool flushDb = false)
        {
            // 组织机构数据缓存预热实现
            BaseOrganizeManager organizeManager = new Business.BaseOrganizeManager();
            // 减少数据库连接、减少内存站用、一边度取、一边设置缓存，只读取需要的数据
            organizeManager.SelectFields = BaseOrganizeEntity.FieldId
                + ", " + BaseOrganizeEntity.FieldCode
                + ", " + BaseOrganizeEntity.FieldFullName
                + " , " + BaseOrganizeEntity.FieldSimpleSpelling
                + " , " + BaseOrganizeEntity.FieldCostCenterId
                + " , " + BaseOrganizeEntity.FieldProvinceId
                + " , " + BaseOrganizeEntity.FieldCompanyId
                + " , " + BaseOrganizeEntity.FieldCityId
                + " , " + BaseOrganizeEntity.FieldParentId
                + " , " + BaseOrganizeEntity.FieldSendAir
                + " , " + BaseOrganizeEntity.FieldEnabled
                + " , " + BaseOrganizeEntity.FieldDeletionStateCode
                + " , " + BaseOrganizeEntity.FieldSortCode;

            // 读取有效的，没有被删除的网点数据
            // List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            // parameters.Add(new KeyValuePair<string, object>(BaseOrganizeEntity.FieldEnabled, 1));
            // parameters.Add(new KeyValuePair<string, object>(BaseOrganizeEntity.FieldDeletionStateCode, 0));

            // 2016-02-19 宋彪 PooledRedisHelper.GetClient() 改为 PooledRedisHelper.GetSpellingClient()
            using (var redisClient = PooledRedisHelper.GetSpellingClient())
            {
                if (flushDb)
                {
                    redisClient.FlushDb();
                }

                double score = 0;

                using (IDataReader dataReader = organizeManager.ExecuteReader(0, BaseOrganizeEntity.FieldFullName))
                {
                    while (dataReader.Read())
                    {
                        score++;

                        // 读取到的数据直接强制设置到缓存里
                        BaseOrganizeEntity entity = new BaseOrganizeEntity();
                        entity.Id = dataReader[BaseOrganizeEntity.FieldId].ToString();
                        entity.Code = dataReader[BaseOrganizeEntity.FieldCode].ToString();
                        entity.FullName = dataReader[BaseOrganizeEntity.FieldFullName].ToString();
                        entity.SimpleSpelling = dataReader[BaseOrganizeEntity.FieldSimpleSpelling].ToString().ToLower();

                        entity.CostCenterId = dataReader[BaseOrganizeEntity.FieldCostCenterId].ToString();
                        entity.ProvinceId = dataReader[BaseOrganizeEntity.FieldProvinceId].ToString();
                        entity.CompanyId = dataReader[BaseOrganizeEntity.FieldCompanyId].ToString();
                        entity.CityId = dataReader[BaseOrganizeEntity.FieldCityId].ToString();
                        entity.ParentId = dataReader[BaseOrganizeEntity.FieldParentId].ToString();
                        entity.SendAir = BaseBusinessLogic.ConvertToInt(dataReader[BaseOrganizeEntity.FieldSendAir]);
                        entity.Enabled = BaseBusinessLogic.ConvertToInt(dataReader[BaseOrganizeEntity.FieldEnabled]);
                        entity.DeletionStateCode = BaseBusinessLogic.ConvertToInt(dataReader[BaseOrganizeEntity.FieldDeletionStateCode]);
                        entity.SortCode = BaseBusinessLogic.ConvertToInt(dataReader[BaseOrganizeEntity.FieldSortCode]);

                        if (!flushDb)
                        {
                            score = entity.SortCode.Value;
                        }

                        CachePreheatingSpelling(redisClient, entity, score);

                        if (flushDb)
                        {
                            // 2016-02-02 吉日嘎拉 设置一下排序属性
                            new Business.BaseManager(BaseOrganizeEntity.TableName).SetProperty(entity.Id, new KeyValuePair<string, object>(BaseOrganizeEntity.FieldSortCode, score));
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 某一个网点是否另外一个网点的父级
        /// </summary>
        /// <param name="parentId">父级别主键</param>
        /// <param name="id">主键盘</param>
        /// <returns>是父亲节点</returns>
        public static bool IsParentByCache(string parentId, string id)
        {
            bool result = false;

            // 打开缓存、进行查找
            using (var redisClient = PooledRedisHelper.GetReadOnlyClient())
            {
                // 若已经在缓存里保存了，是上级网点、就不用计算了，提高判断的效率
                string key = "IsParent:" + parentId + ":" + id;
                result = redisClient.Get<bool>(key);
                if (!result)
                {
                    // 从缓存读取数据、提高读取的效率，这里需要防止死循环，有时候会有脏数据
                    BaseOrganizeEntity organizeEntity = BaseOrganizeManager.GetObjectByCache(redisClient, id);
                    while (organizeEntity != null
                        && !string.IsNullOrEmpty(organizeEntity.ParentId)
                        && !id.Equals(organizeEntity.ParentId)
                        )
                    {
                        // 若已经找到了，就退出循环，提高效率
                        if (organizeEntity.ParentId.Equals(parentId))
                        {
                            result = true;
                            break;
                        }
                        organizeEntity = BaseOrganizeManager.GetObjectByCache(redisClient, organizeEntity.ParentId);
                    }
                    // 设置一个过期时间，提高效率，10分钟内不需要重复判断，把关系写入缓存数据库里，是否成立都写入缓存服务器里。
                    redisClient.Set<bool>(key, result, DateTime.Now.AddMinutes(10));
                }
            }

            return result;
        }
    }
}