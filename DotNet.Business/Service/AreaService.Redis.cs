//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2014 , Hairihan TECH, Ltd. 
//-----------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Reflection;
using System.ServiceModel;
using System.Web;
using System.Web.Caching;
using System.Web.Script.Serialization;

namespace DotNet.Business
{
    using DotNet.Utilities;
    using ServiceStack.Redis;

    /// <summary>
    /// AreaService.Redis
    /// 区域服务
    /// 
    /// 修改纪录
    /// 
    ///		2014.07.23 版本：2.0 JiRiGaLa Redis 改进。
    ///		2014.03.07 版本：1.0 JiRiGaLa 创建。
    ///		
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2014.07.23</date>
    /// </author> 
    /// </summary>
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    [System.ServiceModel.Activation.AspNetCompatibilityRequirements(RequirementsMode = System.ServiceModel.Activation.AspNetCompatibilityRequirementsMode.Allowed)]
    public class AreaService : IAreaService
    {
        private string serviceName = AppMessage.OrganizeService;

        // 当前的锁
        private static object locker = new Object();

        // 建立连接池
        public static PooledRedisClientManager redisPool = new PooledRedisClientManager(1, new string[] { ConfigurationManager.AppSettings["Redis"] });
       
        #region public bool Exists(BaseUserInfo userInfo, List<KeyValuePair<string, object>> parameters, string id)
        /// <summary>
        /// 判断字段是否重复
        /// </summary>
        /// <param name="userInfo">用户</param>
        /// <param name="parameters">字段名,字段值</param>
        /// <param name="id">主键</param>
        /// <returns>已存在</returns>
        public bool Exists(BaseUserInfo userInfo, List<KeyValuePair<string, object>> parameters, string id)
        {
            var parameter = ServiceParameter.CreateWithLog(userInfo
                , MethodBase.GetCurrentMethod());
            bool result = false;
            ServiceUtil.ProcessUserCenterReadDb(userInfo, parameter, (dbHelper) =>
            {
                var manager = new BaseManager(dbHelper, userInfo, BaseAreaEntity.TableName);
                result = manager.Exists(parameters, id);
            });
            return result;
        }
        #endregion

        #region public BaseAreaEntity GetObject(BaseUserInfo userInfo, string id)
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="userInfo">用户</param>
        /// <param name="id">主键</param>
        /// <returns>实体</returns>
        public BaseAreaEntity GetObject(BaseUserInfo userInfo, string id)
        {
            var parameter = ServiceParameter.CreateWithMessage(userInfo
                , MethodBase.GetCurrentMethod()
                , this.serviceName
                , AppMessage.OrganizeService_GetObject);
            BaseAreaEntity entity = null;
            ServiceUtil.ProcessUserCenterReadDb(userInfo, parameter, (dbHelper) =>
            {
                var manager = new BaseAreaManager(dbHelper, userInfo);
                entity = manager.GetObject(id);
            });
            return entity;
        }
        #endregion

        #region public int Update(BaseUserInfo userInfo, BaseAreaEntity entity, out string statusCode, out string statusMessage)
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="userInfo">用户</param>
        /// <param name="entity">实体</param>
        /// <param name="statusCode">状态码</param>
        /// <param name="statusMessage">状态信息</param>
        /// <returns>影响行数</returns>
        public int Update(BaseUserInfo userInfo, BaseAreaEntity entity, out string statusCode, out string statusMessage)
        {
            var parameter = ServiceParameter.CreateWithMessage(userInfo
                , MethodBase.GetCurrentMethod()
                , this.serviceName
                , AppMessage.OrganizeService_Update);

            string returnCode = string.Empty;
            string returnMessage = string.Empty;
            int result = 0;
            ServiceUtil.ProcessUserCenterWriteDb(userInfo, parameter, (dbHelper) =>
            {
                var manager = new BaseAreaManager(dbHelper, userInfo);
                result = manager.Update(entity, out returnCode);
                returnMessage = manager.GetStateMessage(returnCode);
            });
            statusCode = returnCode;
            statusMessage = returnMessage;
            
            // 处理缓存优化性能
            System.Web.Caching.Cache cache = HttpRuntime.Cache;
            string cacheObject = string.Empty;
            if (string.IsNullOrEmpty(entity.ParentId))
            {
                cacheObject = "AreaProvince";
            }
            else
            {
                cacheObject = "Area" + entity.ParentId;
            }
            cache.Remove(cacheObject);

            return result;
        }
        #endregion

        #region public DataTable GetDataTableByIds(BaseUserInfo userInfo, string[] ids)
        /// <summary>
        /// 按主键数组获取列表
        /// </summary>
        /// <param name="userInfo">用户</param>
        /// <param name="ids">组织机构主键</param>
        /// <returns>数据表</returns>
        public DataTable GetDataTableByIds(BaseUserInfo userInfo, string[] ids)
        {
            var parameter = ServiceParameter.CreateWithMessage(userInfo
                , MethodBase.GetCurrentMethod()
                , this.serviceName
                , AppMessage.OrganizeService_GetDataTable);
            var dt = new DataTable(BaseAreaEntity.TableName);
            ServiceUtil.ProcessUserCenterReadDb(userInfo, parameter, (dbHelper) =>
            {
                var manager = new BaseAreaManager(dbHelper, userInfo);
                dt = manager.GetDataTable(BaseAreaEntity.FieldId, ids, BaseAreaEntity.FieldSortCode);
                dt.TableName = BaseAreaEntity.TableName;
            });
            return dt;
        }
        #endregion

        /// <summary>
        /// 刷新列表
        /// </summary>
        /// <param name="userInfo">用户</param>
        /// <param name="parentId">父亲节点主键</param>
        /// <returns>数据表</returns>
        public void Refresh(BaseUserInfo userInfo, string parentId)
        {
            string cacheObject = "Area";
            if (!string.IsNullOrEmpty(parentId))
            {
                cacheObject = "Area" + parentId;
            }
            using (var client = redisPool.GetClient())
            {
                client.Remove(cacheObject);
            }
        }

        #region public DataTable GetDataTableByParent(BaseUserInfo userInfo, string parentId)
        /// <summary>
        /// 按父节点获取列表
        /// </summary>
        /// <param name="userInfo">用户</param>
        /// <param name="parentId">父节点</param>
        /// <returns>数据表</returns>
        public DataTable GetDataTableByParent(BaseUserInfo userInfo, string parentId)
        {
            DataTable result = null;
            var parameter = ServiceParameter.CreateWithMessage(userInfo
                , MethodBase.GetCurrentMethod()
                , this.serviceName
                , AppMessage.OrganizeService_GetProvince);

            System.Web.Caching.Cache cache = HttpRuntime.Cache;
            string cacheObject = "Area";
            if (!string.IsNullOrEmpty(parentId))
            {
                cacheObject = "Area" + parentId;
            }
            if (cache == null || cache[cacheObject] == null)
            {
                lock (locker)
                {
                    if (cache == null || cache[cacheObject] == null)
                    {
                        ServiceUtil.ProcessUserCenterReadDb(userInfo, parameter, (dbHelper) =>
                        {
                            // 这里是条件字段
                            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
                            parameters.Add(new KeyValuePair<string, object>(BaseAreaEntity.FieldParentId, parentId));
                            parameters.Add(new KeyValuePair<string, object>(BaseAreaEntity.FieldEnabled, 1));
                            parameters.Add(new KeyValuePair<string, object>(BaseAreaEntity.FieldDeletionStateCode, 0));
                            // 用静态方法获取数据，提高效率，获取列表，指定排序字段
                            result = DbLogic.GetDataTable(dbHelper, BaseAreaEntity.TableName, parameters, 0, BaseAreaEntity.FieldSortCode, null);
                            // var manager = new BaseAreaManager(dbHelper, userInfo);
                            // result = manager.GetDataTable(parameters, BaseAreaEntity.FieldSortCode);
                            result.DefaultView.Sort = BaseAreaEntity.FieldSortCode;
                            result.TableName = BaseAreaEntity.TableName;
                            // 这里可以缓存起来，提高效率
                            cache.Add(cacheObject, result, null, DateTime.Now.AddMinutes(30), TimeSpan.Zero, CacheItemPriority.Normal, null);
                            System.Console.WriteLine(System.DateTime.Now.ToString(BaseSystemInfo.DateTimeFormat) + " cache Area " + parentId);
                        });
                    }
                }
            }
            result = cache[cacheObject] as DataTable;
            return result;
        }
        #endregion

        #region public int SetDeleted(BaseUserInfo userInfo, string[] ids)
        /// <summary>
        /// 批量打删除标志
        /// </summary>
        /// <param name="userInfo">用户</param>
        /// <param name="ids">主键数组</param>
        /// <returns>影响行数</returns>
        public int SetDeleted(BaseUserInfo userInfo, string[] ids)
        {
            var parameter = ServiceParameter.CreateWithMessage(userInfo
                , MethodBase.GetCurrentMethod()
                , this.serviceName
                , AppMessage.OrganizeService_SetDeleted);
            int result = 0;
            ServiceUtil.ProcessUserCenterWriteDb(userInfo, parameter, (dbHelper) =>
            {
                var manager = new BaseAreaManager(dbHelper, userInfo);
                for (int i = 0; i < ids.Length; i++)
                {
                    // 设置部门为删除状态
                    result += manager.SetDeleted(ids[i]);
                    // 相应的用户也需要处理
                    var userManager = new BaseUserManager(dbHelper, userInfo);
                    List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
                    parameters.Add(new KeyValuePair<string, object>(BaseUserEntity.FieldCompanyId, null));
                    parameters.Add(new KeyValuePair<string, object>(BaseUserEntity.FieldCompanyName, null));
                    userManager.SetProperty(new KeyValuePair<string, object>(BaseUserEntity.FieldCompanyId, ids[i]), parameters);
                    parameters = new List<KeyValuePair<string, object>>();
                    parameters.Add(new KeyValuePair<string, object>(BaseUserEntity.FieldSubCompanyId, null));
                    parameters.Add(new KeyValuePair<string, object>(BaseUserEntity.FieldSubCompanyName, null));
                    userManager.SetProperty(new KeyValuePair<string, object>(BaseUserEntity.FieldSubCompanyId, ids[i]), parameters);
                    parameters = new List<KeyValuePair<string, object>>();
                    parameters.Add(new KeyValuePair<string, object>(BaseUserEntity.FieldDepartmentId, null));
                    parameters.Add(new KeyValuePair<string, object>(BaseUserEntity.FieldDepartmentName, null));
                    userManager.SetProperty(new KeyValuePair<string, object>(BaseUserEntity.FieldDepartmentId, ids[i]), parameters);
                    parameters = new List<KeyValuePair<string, object>>();
                    parameters.Add(new KeyValuePair<string, object>(BaseUserEntity.FieldWorkgroupId, null));
                    parameters.Add(new KeyValuePair<string, object>(BaseUserEntity.FieldWorkgroupName, null));
                    userManager.SetProperty(new KeyValuePair<string, object>(BaseUserEntity.FieldWorkgroupId, ids[i]), parameters);
                    // 相应的员工也需要处理
                    var staffManager = new BaseStaffManager(dbHelper, userInfo);
                    staffManager.SetProperty(new KeyValuePair<string, object>(BaseStaffEntity.FieldCompanyId, ids[i]), new KeyValuePair<string, object>(BaseStaffEntity.FieldCompanyId, null));
                    staffManager.SetProperty(new KeyValuePair<string, object>(BaseStaffEntity.FieldSubCompanyId, ids[i]), new KeyValuePair<string, object>(BaseStaffEntity.FieldSubCompanyId, null));
                    staffManager.SetProperty(new KeyValuePair<string, object>(BaseStaffEntity.FieldDepartmentId, ids[i]), new KeyValuePair<string, object>(BaseStaffEntity.FieldDepartmentId, null));
                    staffManager.SetProperty(new KeyValuePair<string, object>(BaseStaffEntity.FieldWorkgroupId, ids[i]), new KeyValuePair<string, object>(BaseStaffEntity.FieldWorkgroupId, null));
                }
                var folderManager = new BaseFolderManager(dbHelper, userInfo);
                folderManager.SetDeleted(ids);
            });
            return result;
        }
        #endregion

        #region public int BatchSave(BaseUserInfo userInfo, DataTable result)
        /// <summary>
        /// 批量保存数据
        /// </summary>
        /// <param name="userInfo">用户</param>
        /// <param name="result">数据表</param>
        /// <returns>影响行数</returns>
        public int BatchSave(BaseUserInfo userInfo, DataTable dt)
        {
            var parameter = ServiceParameter.CreateWithMessage(userInfo
                , MethodBase.GetCurrentMethod()
                , this.serviceName
                , AppMessage.OrganizeService_BatchSave);
            int result = 0;
            ServiceUtil.ProcessUserCenterWriteDb(userInfo, parameter, (dbHelper) =>
            {
                var manager = new BaseAreaManager(dbHelper, userInfo);
                result = manager.BatchSave(dt);
            });
            return result;
        }
        #endregion

        #region public int MoveTo(BaseUserInfo userInfo, string id, string parentId)
        /// <summary>
        /// 移动数据
        /// </summary>
        /// <param name="userInfo">用户</param>
        /// <param name="id">主键</param>
        /// <param name="parentId">父主键</param>
        /// <returns>影响行数</returns>
        public int MoveTo(BaseUserInfo userInfo, string id, string parentId)
        {
            var parameter = ServiceParameter.CreateWithMessage(userInfo
                , MethodBase.GetCurrentMethod()
                , this.serviceName
                , AppMessage.OrganizeService_MoveTo);
            int result = 0;
            ServiceUtil.ProcessUserCenterWriteDb(userInfo, parameter, (dbHelper) =>
            {
                var manager = new BaseAreaManager(dbHelper, userInfo);
                result = manager.MoveTo(id, parentId);
            });
            return result;
        }
        #endregion

        #region public int BatchMoveTo(BaseUserInfo userInfo, string[] ids, string parentId)
        /// <summary>
        /// 批量移动数据
        /// </summary>
        /// <param name="userInfo">用户</param>
        /// <param name="ids">主键数组</param>
        /// <param name="parentId">父节点主键</param>
        /// <returns>影响行数</returns>
        public int BatchMoveTo(BaseUserInfo userInfo, string[] ids, string parentId)
        {
            var parameter = ServiceParameter.CreateWithMessage(userInfo
                , MethodBase.GetCurrentMethod()
                , this.serviceName
                , AppMessage.OrganizeService_BatchMoveTo);
            int result = 0;
            ServiceUtil.ProcessUserCenterWriteDb(userInfo, parameter, (dbHelper) =>
            {
                var manager = new BaseAreaManager(dbHelper, userInfo);
                for (int i = 0; i < ids.Length; i++)
                {
                    result += manager.MoveTo(ids[i], parentId);
                }
            });
            return result;
        }
        #endregion

        #region public int BatchSetCode(BaseUserInfo userInfo, string[] ids, string[] codes)
        /// <summary>
        /// 批量重新生成编号
        /// </summary>
        /// <param name="userInfo">用户</param>
        /// <param name="ids">主键</param>
        /// <param name="codes">编号</param>
        /// <returns>影响行数</returns>
        public int BatchSetCode(BaseUserInfo userInfo, string[] ids, string[] codes)
        {
            var parameter = ServiceParameter.CreateWithMessage(userInfo
                , MethodBase.GetCurrentMethod()
                , this.serviceName
                , AppMessage.OrganizeService_BatchSetCode);
            int result = 0;
            ServiceUtil.ProcessUserCenterWriteDb(userInfo, parameter, (dbHelper) =>
            {
                var manager = new BaseAreaManager(dbHelper, userInfo);
                result = manager.BatchSetCode(ids, codes);
            });
            return result;
        }
        #endregion

        #region public int BatchSetSortCode(BaseUserInfo userInfo, string[] ids)
        /// <summary>
        /// 批量重新生成排序码
        /// </summary>
        /// <param name="userInfo">用户</param>
        /// <param name="ids">主键数组</param>
        /// <returns>影响行数</returns>
        public int BatchSetSortCode(BaseUserInfo userInfo, string[] ids)
        {
            var parameter = ServiceParameter.CreateWithMessage(userInfo
                , MethodBase.GetCurrentMethod()
                , this.serviceName
                , AppMessage.OrganizeService_BatchSetSortCode);
            int result = 0;
            ServiceUtil.ProcessUserCenterWriteDb(userInfo, parameter, (dbHelper) =>
            {
                var manager = new BaseAreaManager(dbHelper, userInfo);
                result = manager.BatchSetSortCode(ids);
            });
            return result;
        }
        #endregion

        #region public string Add(BaseUserInfo userInfo, BaseAreaEntity entity, out string statusCode, out string statusMessage)
        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="userInfo">用户</param>
        /// <param name="entity">实体</param>
        /// <param name="statusCode">状态码</param>
        /// <param name="statusMessage">状态信息</param>
        /// <returns>主键</returns>
        public string Add(BaseUserInfo userInfo, BaseAreaEntity entity, out string statusCode, out string statusMessage)
        {
            var parameter = ServiceParameter.CreateWithMessage(userInfo
                , MethodBase.GetCurrentMethod()
                , this.serviceName
                , AppMessage.OrganizeService_Add);
            string returnCode = string.Empty;
            string returnMessage = string.Empty;
            string result = string.Empty;
            ServiceUtil.ProcessUserCenterWriteDb(userInfo, parameter, (dbHelper) =>
            {
                var manager = new BaseAreaManager(dbHelper, userInfo);
                result = manager.Add(entity, out returnCode);
                returnMessage = manager.GetStateMessage(returnCode);
            });
            statusCode = returnCode;
            statusMessage = returnMessage;
            return result;
        }
        #endregion

        /// <summary>
        /// 获取省份
        /// </summary>
        /// <param name="userInfo">用户</param>
        /// <returns>省份列表</returns>
        public List<BaseAreaEntity> GetProvinceList(BaseUserInfo userInfo)
        {
            List<BaseAreaEntity> result = null;
            var parameter = ServiceParameter.CreateWithMessage(userInfo
                , MethodBase.GetCurrentMethod()
                , this.serviceName
                , AppMessage.OrganizeService_GetProvince);

            // System.Web.Caching.Cache cache = HttpRuntime.Cache;
            string cacheObject = "AreaProvince";
            string cacheValue = string.Empty;
            using (var client = redisPool.GetClient())
            {
                cacheValue = client.GetValue(cacheObject);
            }
            if (string.IsNullOrEmpty(cacheValue))
            {
                lock (locker)
                {
                    if (string.IsNullOrEmpty(cacheValue))
                    {
                        ServiceUtil.ProcessUserCenterReadDb(userInfo, parameter, (dbHelper) =>
                        {
                            BaseAreaManager areaManager = new BaseAreaManager(dbHelper, userInfo);
                            result = areaManager.GetProvince();
                            using (var client = redisPool.GetClient())
                            {
                                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                                cacheValue = javaScriptSerializer.Serialize(result);
                                client.SetEntry(cacheObject, cacheValue, new TimeSpan(0, 30, 0));
                            }
                            // cache.Add(cacheObject, result, null, DateTime.Now.AddMinutes(30), TimeSpan.Zero, CacheItemPriority.Normal, null);
                            System.Console.WriteLine(System.DateTime.Now.ToString(BaseSystemInfo.DateTimeFormat) + " cache Province");
                        });
                    }
                }
            }
            if (string.IsNullOrEmpty(cacheValue))
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                result = javaScriptSerializer.Deserialize<List<BaseAreaEntity>>(cacheValue);
            }
            // result = cache[cacheObject] as List<BaseAreaEntity>;
            return result;
        }

        /// <summary>
        /// 获取城市
        /// </summary>
        /// <param name="userInfo">用户</param>
        /// <param name="provinceId">省份主键</param>
        /// <returns>城市列表</returns>
        public List<BaseAreaEntity> GetCityList(BaseUserInfo userInfo, string provinceId)
        {
            List<BaseAreaEntity> result = null;
            var parameter = ServiceParameter.CreateWithMessage(userInfo
                , MethodBase.GetCurrentMethod()
                , this.serviceName
                , AppMessage.OrganizeService_GetCity);
            System.Web.Caching.Cache cache = HttpRuntime.Cache;
            string cacheObject = "AreaCity" + provinceId;
            if (cache == null || cache[cacheObject] == null)
            {
                lock (locker)
                {
                    if (cache == null || cache[cacheObject] == null)
                    {
                        ServiceUtil.ProcessUserCenterReadDb(userInfo, parameter, (dbHelper) =>
                        {
                            BaseAreaManager areaManager = new BaseAreaManager(dbHelper, userInfo);
                            result = areaManager.GetCity(provinceId);
                            cache.Add(cacheObject, result, null, DateTime.Now.AddMinutes(30), TimeSpan.Zero, CacheItemPriority.Normal, null);
                            System.Console.WriteLine(System.DateTime.Now.ToString(BaseSystemInfo.DateTimeFormat) + " cache AreaCity provinceId " + provinceId);

                        });
                    }
                }
            }
            result = cache[cacheObject] as List<BaseAreaEntity>;
            return result;
        }

        /// <summary>
        /// 获取县区
        /// </summary>
        /// <param name="userInfo">用户</param>
        /// <param name="cityId">城市主键</param>
        /// <returns>县区列表</returns>
        public List<BaseAreaEntity> GetDistrictList(BaseUserInfo userInfo, string cityId)
        {
            List<BaseAreaEntity> result = null;
            var parameter = ServiceParameter.CreateWithMessage(userInfo
                , MethodBase.GetCurrentMethod()
                , this.serviceName
                , AppMessage.OrganizeService_GetDistrict);

            System.Web.Caching.Cache cache = HttpRuntime.Cache;
            string cacheObject = "AreaDistrict" + cityId;
            if (cache == null || cache[cacheObject] == null)
            {
                lock (locker)
                {
                    if (cache == null || cache[cacheObject] == null)
                    {
                        ServiceUtil.ProcessUserCenterReadDb(userInfo, parameter, (dbHelper) =>
                        {
                            BaseAreaManager areaManager = new BaseAreaManager(dbHelper, userInfo);
                            result = areaManager.GetDistrict(cityId);
                            cache.Add(cacheObject, result, null, DateTime.Now.AddMinutes(30), TimeSpan.Zero, CacheItemPriority.Normal, null);
                            System.Console.WriteLine(System.DateTime.Now.ToString(BaseSystemInfo.DateTimeFormat) + " cache AreaDistrict cityId " + cityId);
                        });
                    }
                }
            }
            result = cache[cacheObject] as List<BaseAreaEntity>;
            return result;
        }

        /// <summary>
        /// 获取街道
        /// </summary>
        /// <param name="userInfo">用户</param>
        /// <param name="districtId">县区主键</param>
        /// <returns>街道列表</returns>
        public List<BaseAreaEntity> GetStreetList(BaseUserInfo userInfo, string districtId)
        {
            List<BaseAreaEntity> result = null;
            var parameter = ServiceParameter.CreateWithMessage(userInfo
                , MethodBase.GetCurrentMethod()
                , this.serviceName
                , AppMessage.OrganizeService_GetDistrict);

            System.Web.Caching.Cache cache = HttpRuntime.Cache;
            string cacheObject = "AreaStreet" + districtId;
            if (cache == null || cache[cacheObject] == null)
            {
                lock (locker)
                {
                    if (cache == null || cache[cacheObject] == null)
                    {
                        ServiceUtil.ProcessUserCenterReadDb(userInfo, parameter, (dbHelper) =>
                        {
                            BaseAreaManager areaManager = new BaseAreaManager(dbHelper, userInfo);
                            result = areaManager.GetStreet(districtId);
                            cache.Add(cacheObject, result, null, DateTime.Now.AddMinutes(30), TimeSpan.Zero, CacheItemPriority.Normal, null);
                            System.Console.WriteLine(System.DateTime.Now.ToString(BaseSystemInfo.DateTimeFormat) + " cache AreaStreet districtId " + districtId);
                        });
                    }
                }
            }
            result = cache[cacheObject] as List<BaseAreaEntity>;
            return result;
        }
    }
}