//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd.  
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
    using DotNet.IService;
    using DotNet.Model;
    using DotNet.Utilities;

    /// <summary>
    /// AreaService
    /// 区域服务
    /// 
    /// 修改记录
    /// 
    ///		2015.07.17 版本：2.1 JiRiGaLa 刷新缓存功能优化。
    ///		2015.07.03 版本：2.0 JiRiGaLa 修改大头笔增加日志功能。
    ///		2014.03.07 版本：1.0 JiRiGaLa 创建。
    ///		
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2015.07.17</date>
    /// </author> 
    /// </summary>
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    [System.ServiceModel.Activation.AspNetCompatibilityRequirements(RequirementsMode = System.ServiceModel.Activation.AspNetCompatibilityRequirementsMode.Allowed)]
    public class AreaService : IAreaService
    {
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
            bool result = false;

            var parameter = ServiceInfo.Create(userInfo, MethodBase.GetCurrentMethod());
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
            BaseAreaEntity entity = null;

            var parameter = ServiceInfo.Create(userInfo, MethodBase.GetCurrentMethod());
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
            int result = 0;
            
            string returnCode = string.Empty;
            string returnMessage = string.Empty;
            
            var parameter = ServiceInfo.Create(userInfo, MethodBase.GetCurrentMethod());
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
            var dt = new DataTable(BaseAreaEntity.TableName);

            var parameter = ServiceInfo.Create(userInfo, MethodBase.GetCurrentMethod());
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
        /// 2015-07-17 吉日嘎拉 刷新缓存功能优化
        /// </summary>
        /// <param name="userInfo">用户</param>
        /// <param name="id">父亲节点主键</param>
        /// <returns>数据表</returns>
        public void Refresh(BaseUserInfo userInfo, string id)
        {
            string cacheKey = string.Empty;
            // 把列表的缓存更新掉
            cacheKey = "Area" + id;
            BaseAreaManager.RemoveCache(cacheKey);

            // 重新获取本身的缓存
            // cacheKey = "AreaList" + id;
            BaseAreaManager.GetCache(cacheKey);

            /*
            System.Web.Caching.Cache cache = HttpRuntime.Cache;
            string cacheKey = "Area";
            if (!string.IsNullOrEmpty(parentId))
            {
                cacheKey = "Area" + parentId;
            }
            cache.Remove(cacheKey);
            */
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

            var parameter = ServiceInfo.Create(userInfo, MethodBase.GetCurrentMethod());
            System.Web.Caching.Cache cache = HttpRuntime.Cache;
            string cacheObject = "Area";
            if (!string.IsNullOrEmpty(parentId))
            {
                cacheObject = "Area" + parentId;
            }
            if (cache != null && cache[cacheObject] == null)
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
                    // var manager = new BaseAreaManager(dbHelper, result);
                    // result = manager.GetDataTable(parameters, BaseAreaEntity.FieldSortCode);
                    result.DefaultView.Sort = BaseAreaEntity.FieldSortCode;
                    result.TableName = BaseAreaEntity.TableName;
                    // 这里可以缓存起来，提高效率
                    cache.Add(cacheObject, result, null, DateTime.Now.AddMinutes(10), TimeSpan.Zero, CacheItemPriority.Normal, null);
                    System.Console.WriteLine(System.DateTime.Now.ToString(BaseSystemInfo.DateTimeFormat) + " cache Area " + parentId);
                });
            }
            result = cache[cacheObject] as DataTable;

            return result;
        }
        #endregion

        #region public DataTable GetAreaRouteMarkEdit(BaseUserInfo userInfo, string parentId)
        /// <summary>
        /// 获取按省路由大头笔信息（输入）
        /// </summary>
        /// <param name="userInfo">用户</param>
        /// <param name="parentId">父节点</param>
        /// <returns>数据表</returns>
        public DataTable GetAreaRouteMarkEdit(BaseUserInfo userInfo, string parentId)
        {
            DataTable result = null;

            var parameter = ServiceInfo.Create(userInfo, MethodBase.GetCurrentMethod());
            ServiceUtil.ProcessUserCenterReadDb(userInfo, parameter, (dbHelper) =>
            {
                BaseAreaProvinceMarkManager areaProvinceMarkManager = new BaseAreaProvinceMarkManager(dbHelper, userInfo);
                result = areaProvinceMarkManager.GetAreaRouteMarkEdit(parentId);
                result.TableName = BaseAreaProvinceMarkEntity.TableName;
            });

            return result;
        }
        #endregion

        #region public DataTable GetAreaRouteMarkByCache(BaseUserInfo userInfo, string parentId)
        /// <summary>
        /// 获取大头笔信息
        /// </summary>
        /// <param name="userInfo">用户</param>
        /// <param name="parentId">父节点</param>
        /// <returns>数据表</returns>
        public DataTable GetAreaRouteMarkByCache(BaseUserInfo userInfo, string parentId)
        {
            DataTable result = null;
            
            var parameter = ServiceInfo.Create(userInfo, MethodBase.GetCurrentMethod());
            System.Web.Caching.Cache cache = HttpRuntime.Cache;
            string cacheObject = "ProvinceMark";
            if (!string.IsNullOrEmpty(parentId))
            {
                cacheObject = "ProvinceMark" + parentId;
            }
            if (cache != null && cache[cacheObject] == null)
            {
                ServiceUtil.ProcessUserCenterReadDb(userInfo, parameter, (dbHelper) =>
                {
                    string commandText = @"SELECT PROVINCE, MARK, DESCRIPTION, CREATEON, CREATEBY, MODIFIEDON, MODIFIEDBY FROM basearea_provincemark WHERE ENABLED = 1 AND areaid = " + parentId;
                    result = dbHelper.Fill(commandText);
                    result.TableName = BaseAreaEntity.TableName;
                    // 这里可以缓存起来，提高效率
                    cache.Add(cacheObject, result, null, DateTime.Now.AddMinutes(10), TimeSpan.Zero, CacheItemPriority.Normal, null);
                    System.Console.WriteLine(System.DateTime.Now.ToString(BaseSystemInfo.DateTimeFormat) + " cache Area " + parentId);
                });
            }
            result = cache[cacheObject] as DataTable;
            return result;
        }
        #endregion

        /// <summary>
        /// 设置按省路由大头笔
        /// </summary>
        /// <param name="userInfo">用户</param>
        /// <param name="dtAreaRouteMark">路由设置</param>
        /// <returns>影响行数</returns>
        public int SetAreaRouteMark(BaseUserInfo userInfo, string areaId, DataTable dtAreaRouteMark)
        {
            int result = 0;

            var parameter = ServiceInfo.Create(userInfo, MethodBase.GetCurrentMethod());
            ServiceUtil.ProcessUserCenterWriteDb(userInfo, parameter, (dbHelper) =>
            {
                BaseAreaProvinceMarkManager areaProvinceMarkManager = new BaseAreaProvinceMarkManager(dbHelper, userInfo);
                result = areaProvinceMarkManager.SetAreaRouteMark(areaId, dtAreaRouteMark);
            });

            return result;
        }

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="userInfo">用户</param>
        /// <param name="searchValue">查询</param>
        /// <returns>数据表</returns>
        public DataTable Search(BaseUserInfo userInfo, string searchValue)
        {
            DataTable result = null;
            
            var parameter = ServiceInfo.Create(userInfo, MethodBase.GetCurrentMethod());
            ServiceUtil.ProcessUserCenterReadDb(userInfo, parameter, (dbHelper) =>
            {
                var manager = new BaseAreaManager(dbHelper, userInfo);
                result = manager.Search(searchValue);
            });

            return result;
        }

        #region public int SetDeleted(BaseUserInfo userInfo, string[] ids)
        /// <summary>
        /// 批量打删除标志
        /// </summary>
        /// <param name="userInfo">用户</param>
        /// <param name="ids">主键数组</param>
        /// <returns>影响行数</returns>
        public int SetDeleted(BaseUserInfo userInfo, string[] ids)
        {
            int result = 0;

            var parameter = ServiceInfo.Create(userInfo, MethodBase.GetCurrentMethod());
            ServiceUtil.ProcessUserCenterWriteDb(userInfo, parameter, (dbHelper) =>
            {
                var manager = new BaseAreaManager(dbHelper, userInfo);
                for (int i = 0; i < ids.Length; i++)
                {
                    // 设置为删除状态
                    result += manager.SetDeleted(ids[i]);
                }
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
            int result = 0;

            var parameter = ServiceInfo.Create(userInfo, MethodBase.GetCurrentMethod());
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
            int result = 0;

            var parameter = ServiceInfo.Create(userInfo, MethodBase.GetCurrentMethod());
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
            int result = 0;

            var parameter = ServiceInfo.Create(userInfo, MethodBase.GetCurrentMethod());
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
            int result = 0;

            var parameter = ServiceInfo.Create(userInfo, MethodBase.GetCurrentMethod());
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
            int result = 0;

            var parameter = ServiceInfo.Create(userInfo, MethodBase.GetCurrentMethod());
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
            string result = string.Empty;
            
            string returnCode = string.Empty;
            string returnMessage = string.Empty;
            var parameter = ServiceInfo.Create(userInfo, MethodBase.GetCurrentMethod());
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
        public List<BaseAreaEntity> GetListByParent(BaseUserInfo userInfo, string parentId)
        {
            List<BaseAreaEntity> result = null;
            
            var parameter = ServiceInfo.Create(userInfo, MethodBase.GetCurrentMethod());
            ServiceUtil.ProcessUserCenterReadDb(userInfo, parameter, (dbHelper) =>
            {
                result = BaseAreaManager.GetListByParentByCache(parentId);
            });

            return result;
        }


        /// <summary>
        /// 获取省份
        /// </summary>
        /// <param name="userInfo">用户</param>
        /// <returns>省份列表</returns>
        public List<BaseAreaEntity> GetProvinceList(BaseUserInfo userInfo)
        {
            List<BaseAreaEntity> result = null;

            var parameter = ServiceInfo.Create(userInfo, MethodBase.GetCurrentMethod());
            ServiceUtil.ProcessUserCenterReadDb(userInfo, parameter, (dbHelper) =>
            {
                result = BaseAreaManager.GetProvinceByCache();
            });

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

            var parameter = ServiceInfo.Create(userInfo, MethodBase.GetCurrentMethod());
            ServiceUtil.ProcessUserCenterReadDb(userInfo, parameter, (dbHelper) =>
            {
                result = BaseAreaManager.GetListByParentByCache(provinceId);
            });

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

            var parameter = ServiceInfo.Create(userInfo, MethodBase.GetCurrentMethod());
            ServiceUtil.ProcessUserCenterReadDb(userInfo, parameter, (dbHelper) =>
            {
                result = BaseAreaManager.GetListByParentByCache(cityId);
            });

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
            
            var parameter = ServiceInfo.Create(userInfo, MethodBase.GetCurrentMethod());
            ServiceUtil.ProcessUserCenterReadDb(userInfo, parameter, (dbHelper) =>
            {
                result = BaseAreaManager.GetListByParentByCache(districtId);
            });

            return result;
        }
    }
}