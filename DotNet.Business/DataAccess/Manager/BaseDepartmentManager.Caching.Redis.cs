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

    /// <remarks>
    /// BaseDepartmentManager
    /// 部门管理缓存类
    /// 
    /// 修改记录
    /// 
    ///	版本：1.0 2016.01.18  SongBiao 增加承包区缓存预热。
    ///	版本：1.1 2016.01.04  JiRiGaLa 写入主键的方式进行改进。
    ///	版本：1.0 2015.10.23  SongBiao GetCache 方法实现。
    ///	
    /// <author>  
    ///		<name>SongBiao</name>
    ///		<date>2016.01.04</date>
    /// </author> 
    /// </remarks>
    public partial class BaseDepartmentManager
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

        public static BaseDepartmentEntity GetCacheByKey(string key)
        {
            BaseDepartmentEntity result = null;

            if (!string.IsNullOrWhiteSpace(key))
            {
                using (var redisClient = PooledRedisHelper.GetReadOnlyClient())
                {
                    result = redisClient.Get<BaseDepartmentEntity>(key);
                }
            }

            return result;
        }

        /// <summary>
        /// 设置缓存
        /// 这里缓存的数据容量控制了一下，只保存Id，会节约内存空间
        /// </summary>
        /// <param name="entity">实体</param>
        public static void SetCache(BaseDepartmentEntity entity)
        {
            if (entity != null && entity.Id != null)
            {
                string key = string.Empty;
                using (var redisClient = PooledRedisHelper.GetClient())
                {
                    // key = "Department:" + entity.Id;
                    key = "D:" + entity.Id;
                    redisClient.Set<BaseDepartmentEntity>(key, entity, DateTime.Now.AddHours(16));

                    // key = "DepartmentByName:"+entity.CompanyId +":"+ entity.FullName;
                    key = "DBN:" + entity.CompanyId + ":" + entity.FullName;
                    redisClient.Set<string>(key, entity.Id.ToString(), DateTime.Now.AddHours(16));

                    // key = "DepartmentByCode:" + entity.Code;
                    // 2016-01-22 吉日嘎拉，若不同的公司编号重复，有待改进
                    key = "DBC:" + entity.Code;
                    redisClient.Set<string>(key, entity.Id.ToString(), DateTime.Now.AddHours(16));
                }
            }
        }

        public static BaseDepartmentEntity GetObjectByCodeByCache(string code)
        {
            BaseDepartmentEntity result = null;

            if (string.IsNullOrEmpty(code))
            {
                return result;
            }

            // string key = "DepartmentByCode:" + code;
            string key = "DBC:" + code;
            string id = string.Empty;
            using (var redisClient = PooledRedisHelper.GetReadOnlyClient())
            {
                id = redisClient.Get<string>(key);
            }
            if (!string.IsNullOrWhiteSpace(id))
            {
                result = GetObjectByCache(id);
            }
            else
            {
                // 从数据库读取数据
                BaseDepartmentManager departmentManager = new BaseDepartmentManager();
                result = departmentManager.GetObjectByCode(code);
                // 设置缓存，没必要来个空操作
                if (result != null)
                {
                    SetCache(result);
                }
            }

            return result;
        }

        /// <summary>
        /// 从缓存获取获取实体
        /// </summary>
        /// <param name="companyId">公司主键</param>
        /// <param name="fullName">部门名称</param>
        /// <returns>实体</returns>
        public static BaseDepartmentEntity GetObjectByNameByCache(string companyId, string fullName)
        {
            BaseDepartmentEntity result = null;

            if (!string.IsNullOrEmpty(companyId) && !string.IsNullOrEmpty(fullName))
            {
                string id = string.Empty;
                string key = "DBN:" + companyId + ":" + fullName;
                using (var redisClient = PooledRedisHelper.GetReadOnlyClient())
                {
                    id = redisClient.Get<string>(key);
                    if (!string.IsNullOrWhiteSpace(id))
                    {
                        result = GetObjectByCache(id);
                    }
                    if (result == null)
                    {
                        BaseDepartmentManager departmentManager = new BaseDepartmentManager();
                        result = departmentManager.GetObjectByName(companyId, fullName);
                        // 若是空的不用缓存，继续读取实体
                        if (result != null)
                        {
                            SetCache(result);
                        }
                    }
                }
            }

            return result;
        }


        /// <summary>
        /// 快速模糊检索承包区（高速缓存） 宋彪
        /// 
        /// 01：网点承包区缓存数据方法 prefix = "ContractArea:" + companyId + ":"
        /// 
        /// </summary>
        /// <param name="prefix">搜索前缀</param>
        /// <param name="key">搜搜关键字</param>
        /// <param name="returnId">返回主键</param>
        /// <param name="showCode">返回编号</param>
        /// <param name="topLimit">返回数量</param>
        /// <returns>查询结果数组</returns>
        public static List<KeyValuePair<string, string>> GetContractAreasByKey(string prefix, string key, bool returnId = true, bool showCode = false, int topLimit = 20)
        {
            List<KeyValuePair<string, string>> result = new List<KeyValuePair<string, string>>();

            if (string.IsNullOrEmpty(key))
            {
                return result;
            }

            // 全部转小写进行比对
            key = prefix + key.ToLower();
            using (var redisClient = PooledRedisHelper.GetClient())
            {
                // 每次从有序集合中的消息取出20条信息，返回给客户端，在客户端展示阅读成功后才从内存里去掉。
                List<string> list = redisClient.GetRangeFromSortedSet(key, 0, topLimit);

                if (list != null)
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        // 从内存里直接获取这个对应的消息
                        string[] contractArea = list[i].Split(';');
                        string id = contractArea[0];
                        string code = contractArea[1];
                        string fullName = contractArea[2];
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

        /// <summary>
        /// 承包区缓存预热 宋彪
        /// 网点下的承包区查询条件：CATEGORYCODE = 7 AND  and COMPANYID=‘网点id’
        /// 找到所有承包区的条件
        /// CATEGORYCODE = 7 AND COMPANYID IN(SELECT ID FROM BASEORGANIZE)
        /// </summary>
        /// <param name="flushDb"></param>
        public static void CacheContractAreaPreheatingSpelling(bool flushDb = false)
        {
            // 组织机构数据缓存预热实现
            BaseDepartmentManager departmentManager = new Business.BaseDepartmentManager();
            // 减少数据库连接、减少内存站用、一边度取、一边设置缓存，只读取需要的数据
            departmentManager.SelectFields = BaseDepartmentEntity.FieldCompanyId
                                             + " , " + BaseDepartmentEntity.FieldFullName
                                             + " , " + BaseDepartmentEntity.FieldManager
                                             + " , " + BaseDepartmentEntity.FieldManagerId
                                             + " , " + BaseDepartmentEntity.FieldEnabled
                                             + " , " + BaseDepartmentEntity.FieldDeletionStateCode;

            using (var redisClient = PooledRedisHelper.GetClient())
            {
                if (flushDb)
                {
                    redisClient.FlushDb();
                }

                double score = 0;

                // 获取某个网点下的承包区 循环进行缓存
                using (IDataReader dataReader = departmentManager.ExecuteReaderByWhere(" CATEGORYCODE = 7 AND COMPANYID IN (SELECT ID FROM BASEORGANIZE) AND DeletionStateCode=0 ", null, 0, BaseDepartmentEntity.FieldFullName))
                {
                    while (dataReader.Read())
                    {

                        // 具体使用时 使用承包区主管的ID和承包区的名字，不要使用部门的ID，因为费用计算时是扣到具体的承包区主管的 承包区是属于具体网点
                        BaseDepartmentEntity entity = new BaseDepartmentEntity();
                        entity.ManagerId = dataReader[BaseDepartmentEntity.FieldManagerId].ToString();
                        entity.Code = dataReader[BaseDepartmentEntity.FieldCode].ToString();
                        entity.FullName = dataReader[BaseDepartmentEntity.FieldFullName].ToString();
                        entity.CompanyId = dataReader[BaseDepartmentEntity.FieldCompanyId].ToString();

                        score++;
                        CacheContractAreaPreheatingSpelling(redisClient, entity, score);

                        // 2016-02-02 吉日嘎拉 设置一下排序属性
                        departmentManager.SetProperty(entity.Id, new KeyValuePair<string, object>(BaseDepartmentEntity.FieldSortCode, score));
                    }
                }
            }
        }

        /// <summary>
        /// 按实体缓存承包区 宋彪
        /// </summary>
        /// <param name="redisClient"></param>
        /// <param name="score"></param>
        /// <param name="departmentEntity"></param>
        public static void CacheContractAreaPreheatingSpelling(IRedisClient redisClient, BaseDepartmentEntity departmentEntity, double score = 0)
        {
            // 承包区主管id字段不可为空，对应的是用户的Id
            if (!string.IsNullOrWhiteSpace(departmentEntity.ManagerId))
            {
                string contractArea = departmentEntity.ManagerId + ";" + departmentEntity.Code + ";" + departmentEntity.FullName;
                string key = string.Empty;
                // 01：所有承包区查询的缓存数据方法  编号是按网点code生成 至少输入4个编号才返回查询结果
                for (int i = 4; i <= departmentEntity.Code.Length; i++)
                {
                    key = "ContractArea:" + departmentEntity.CompanyId + ":" + departmentEntity.Code.Substring(0, i).ToLower();
                    redisClient.AddItemToSortedSet(key, contractArea, score);
                    redisClient.ExpireEntryAt(key, DateTime.Now.AddDays(15));
                }

                // 02：按承包区名字查询的缓存
                for (int i = 1; i <= departmentEntity.FullName.Length; i++)
                {
                    key = "ContractArea:" + departmentEntity.CompanyId + ":" + departmentEntity.FullName.Substring(0, i).ToLower();
                    redisClient.AddItemToSortedSet(key, contractArea, score);
                    redisClient.ExpireEntryAt(key, DateTime.Now.AddDays(15));
                }
            }
        }
    }
}