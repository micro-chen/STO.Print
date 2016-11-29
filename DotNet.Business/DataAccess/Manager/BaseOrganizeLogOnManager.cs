//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd. 
//-----------------------------------------------------------------

using System;
using System.Data;
using ServiceStack.Redis;

namespace DotNet.Business
{
    using DotNet.Model;
    using DotNet.Utilities;

    /// <summary>
    /// BaseOrganizeLogOnManager
    /// 系统网点登录信息
    ///
    /// 修改记录
    ///
    ///		2016-04-01 版本：1.1 JiRiGaLa LogOnStatistics 方法进行细化改进。
    ///		2016-03-29 版本：1.0 JiRiGaLa 创建主键。
    ///
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2016-03-29</date>
    /// </author>
    /// </summary>
    public partial class BaseOrganizeLogOnManager : BaseManager, IBaseManager
    {
        /// <summary>
        /// 从数据库加载上来？缓存预热的机制
        /// </summary>
        /// <returns></returns>
        public static int CachePreheating()
        {
            int result = 0;
            // 把所有的数据都缓存起来的代码
            var manager = new BaseOrganizeLogOnManager();
            using (IDataReader dataReader = manager.ExecuteReader(0, BaseOrganizeLogOnEntity.FieldId))
            {
                while (dataReader.Read())
                {
                    var entity = BaseEntity.Create<BaseOrganizeLogOnEntity>(dataReader, false);
                    SetCache(entity);
                    result++;
                }
                dataReader.Close();
            }

            return result;
        }

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="entity">登录信息</param>
        public static void SetCache(BaseOrganizeLogOnEntity entity)
        {
            using (var redisClient = PooledRedisHelper.GetCallLimitClient())
            {
                SetCache(redisClient, entity);
            }
        }

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="entity">登录信息</param>
        public static void SetCache(IRedisClient redisClient, BaseOrganizeLogOnEntity entity)
        {
            string key = string.Empty;

            if (entity != null && !string.IsNullOrWhiteSpace(entity.Id))
            {
                key = "OrganizeLogOn:" + entity.Id;
                // 最多存放两天就可以了
                redisClient.Set(key, entity, DateTime.Now.AddDays(2));
            }
        }

        /// <summary>
        /// 获取缓存的网点登录信息
        /// </summary>
        /// <param name="companyId">网点Id</param>
        public static BaseOrganizeLogOnEntity GetCache(string companyId)
        {
            using (var redisClient = PooledRedisHelper.GetCallLimitClient())
            {
                return redisClient.Get<BaseOrganizeLogOnEntity>("OrganizeLogOn:" + companyId);
            }
        }

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="entity">实体</param>
        public static int LogOnStatistics()
        {
            int result = 0;

            bool update = false;
            int i = 0;

            // 电脑登录系统、没有登记过的，全部登记上来。
            BaseOrganizeLogOnEntity organizeLogOnEntity = null;
            string sqlQuery = string.Empty;
            sqlQuery = @"SELECT u.companyid id
                                , MIN(l.firstvisit) firstvisit
                                , MAX(l.lastvisit) lastvisit 
                           FROM baseuserlogon l
                                , baseuser u 
                          WHERE l.id = u.id 
                          GROUP BY u.companyid";

            BaseOrganizeLogOnManager organizeLogOnManager = new BaseOrganizeLogOnManager();
            BaseOrganizeLogOnManager manager = new BaseOrganizeLogOnManager();
            using (IDataReader dataReader = manager.ExecuteReader(sqlQuery))
            {

                while (dataReader.Read())
                {
                    i++;
                    string id = BaseBusinessLogic.ConvertToString(dataReader[BaseOrganizeLogOnEntity.FieldId]);

                    DateTime? firstVisit = BaseBusinessLogic.ConvertToNullableDateTime(dataReader[BaseOrganizeLogOnEntity.FieldFirstVisit]);
                    DateTime? lastVisit = BaseBusinessLogic.ConvertToNullableDateTime(dataReader[BaseOrganizeLogOnEntity.FieldLastVisit]);
                    organizeLogOnEntity = organizeLogOnManager.GetObject(id);
                    if (organizeLogOnEntity == null)
                    {
                        organizeLogOnEntity = new BaseOrganizeLogOnEntity();
                        organizeLogOnEntity.Id = id;
                        organizeLogOnEntity.FirstVisit = firstVisit;
                        organizeLogOnEntity.LastVisit = lastVisit;
                        organizeLogOnManager.AddObject(organizeLogOnEntity);
                    }
                    else
                    {
                        update = false;
                        if (firstVisit.HasValue)
                        {
                            if (organizeLogOnEntity.FirstVisit == null || organizeLogOnEntity.FirstVisit > firstVisit)
                            {
                                organizeLogOnEntity.FirstVisit = firstVisit;
                                update = true;
                            }
                        }
                        if (lastVisit.HasValue)
                        {
                            if (organizeLogOnEntity.LastVisit == null || organizeLogOnEntity.LastVisit < lastVisit)
                            {
                                organizeLogOnEntity.LastVisit = lastVisit;
                                update = true;
                            }
                        }
                        if (update)
                        {
                            organizeLogOnManager.UpdateObject(organizeLogOnEntity);
                        }
                    }
                    if (update)
                    {
                        System.Console.WriteLine("第 " + i.ToString() + " companyid : " + id);
                    }
                    else
                    {
                        System.Console.WriteLine("第 " + i.ToString());
                    }
                }
                dataReader.Close();
            }

            // 巴枪登录系统、没有登记过的，全部登记上来。
            sqlQuery = @"SELECT u.companyid id
                                , MIN(l.firstvisit) firstvisit
                                , MAX(l.lastvisit) lastvisit 
                           FROM pdauserlogon l
                                , baseuser u 
                          WHERE l.id = u.id 
                          GROUP BY u.companyid";
            using (IDataReader dataReader = manager.ExecuteReader(sqlQuery))
            {
                i = 0;
                while (dataReader.Read())
                {
                    i++;
                    string id = BaseBusinessLogic.ConvertToString(dataReader[BaseOrganizeLogOnEntity.FieldId]);
                    DateTime? firstVisit = BaseBusinessLogic.ConvertToNullableDateTime(dataReader[BaseOrganizeLogOnEntity.FieldFirstVisit]);
                    DateTime? lastVisit = BaseBusinessLogic.ConvertToNullableDateTime(dataReader[BaseOrganizeLogOnEntity.FieldLastVisit]);
                    organizeLogOnEntity = organizeLogOnManager.GetObject(id);
                    if (organizeLogOnEntity == null)
                    {
                        organizeLogOnEntity = new BaseOrganizeLogOnEntity();
                        organizeLogOnEntity.Id = id;
                        organizeLogOnEntity.FirstVisit = firstVisit;
                        organizeLogOnEntity.LastVisit = lastVisit;
                        organizeLogOnManager.Add(organizeLogOnEntity);
                    }
                    else
                    {
                        update = false;
                        if (firstVisit.HasValue)
                        {
                            if (organizeLogOnEntity.FirstVisit == null || organizeLogOnEntity.FirstVisit > firstVisit)
                            {
                                organizeLogOnEntity.FirstVisit = firstVisit;
                                update = true;
                            }
                        }
                        if (lastVisit.HasValue)
                        {
                            if (organizeLogOnEntity.LastVisit == null || organizeLogOnEntity.LastVisit < lastVisit)
                            {
                                organizeLogOnEntity.LastVisit = lastVisit;
                                update = true;
                            }
                        }
                        organizeLogOnManager.UpdateObject(organizeLogOnEntity);
                    }

                    if (update)
                    {
                        System.Console.WriteLine("第 " + i.ToString() + " companyid : " + id);
                    }
                    else
                    {
                        System.Console.WriteLine("第 " + i.ToString());
                    }
                }
                dataReader.Close();
            }

            System.Console.WriteLine("公司整体登录状态已经更新完毕。");

            return result;
        }
    }
}
