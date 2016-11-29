//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd.  
//-----------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data;

namespace DotNet.Business
{
    using DotNet.Model;
    using DotNet.Utilities;

    /// <summary>
    /// BaseDepartmentManager
    /// 部门表
    ///
    /// 修改记录
    ///     
    ///     2015-12-15 版本：1.1 宋彪   添加按编号获取实体 按名称获取实体
    ///     2015-04-23 版本：1.1 潘齐民   添加锁
    ///     2014.12.08 版本：1.0 JiRiGaLa 创建。
    ///
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2014.12.08</date>
    /// </author>
    /// </summary>
    public partial class BaseDepartmentManager : BaseManager //, IBaseOrganizeManager
    {
        // 当前的锁
        // private static object locker = new Object();

        public static string GetNames(List<BaseDepartmentEntity> list)
        {
            string result = string.Empty;

            foreach (BaseDepartmentEntity entity in list)
            {
                result += "," + entity.FullName;
            }
            if (!string.IsNullOrEmpty(result))
            {
                result = result.Substring(1);
            }

            return result;
        }

        /// <summary>
        /// 按编号获取实体
        /// </summary>
        /// <param name="code">编号</param>
        public BaseDepartmentEntity GetObjectByCode(string code)
        {
            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>(BaseOrganizeEntity.FieldCode, code));
            parameters.Add(new KeyValuePair<string, object>(BaseOrganizeEntity.FieldDeletionStateCode, 0));
            return BaseEntity.Create<BaseDepartmentEntity>(this.GetDataTable(parameters));
        }

        /// <summary>
        /// 按名称获取实体
        /// </summary>
        /// <param name="fullName">名称</param>
        public BaseDepartmentEntity GetObjectByName(string companyId, string fullName)
        {
            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>(BaseOrganizeEntity.FieldFullName, fullName));
            parameters.Add(new KeyValuePair<string, object>(BaseOrganizeEntity.FieldCompanyId, companyId));
            parameters.Add(new KeyValuePair<string, object>(BaseOrganizeEntity.FieldDeletionStateCode, 0));
            parameters.Add(new KeyValuePair<string, object>(BaseOrganizeEntity.FieldEnabled, 1));
            return BaseEntity.Create<BaseDepartmentEntity>(this.ExecuteReader(parameters));
        }

        public string Add(BaseDepartmentEntity entity, out string statusCode)
        {
            string result = string.Empty;
            // 检查是否重复
            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>(BaseDepartmentEntity.FieldParentId, entity.ParentId));
            parameters.Add(new KeyValuePair<string, object>(BaseDepartmentEntity.FieldFullName, entity.FullName));
            parameters.Add(new KeyValuePair<string, object>(BaseDepartmentEntity.FieldDeletionStateCode, 0));

            //注意Access 的时候，类型不匹配，会出错故此将 Id 传入
            if (BaseSystemInfo.UserCenterDbType == CurrentDbType.Access)
            {
                if (this.Exists(parameters, entity.Id))
                {
                    // 名称已重复
                    statusCode = Status.ErrorNameExist.ToString();
                }
                else
                {
                    parameters = new List<KeyValuePair<string, object>>();
                    parameters.Add(new KeyValuePair<string, object>(BaseDepartmentEntity.FieldCode, entity.Code));
                    parameters.Add(new KeyValuePair<string, object>(BaseDepartmentEntity.FieldDeletionStateCode, 0));
                    if (entity.Code.Length > 0 && this.Exists(parameters))
                    {
                        // 编号已重复
                        statusCode = Status.ErrorCodeExist.ToString();
                    }
                    else
                    {
                        result = this.AddObject(entity);
                        // 运行成功
                        statusCode = Status.OKAdd.ToString();

                        AfterAdd(entity);
                    }
                }
            }
            else if (this.Exists(parameters))
            {
                // 名称已重复
                statusCode = Status.ErrorNameExist.ToString();
            }
            else
            {
                parameters = new List<KeyValuePair<string, object>>();
                parameters.Add(new KeyValuePair<string, object>(BaseDepartmentEntity.FieldCode, entity.Code));
                parameters.Add(new KeyValuePair<string, object>(BaseDepartmentEntity.FieldDeletionStateCode, 0));
                if (entity.Code.Length > 0 && this.Exists(parameters))
                {
                    // 编号已重复
                    statusCode = Status.ErrorCodeExist.ToString();
                }
                else
                {
                    result = this.AddObject(entity);
                    // 运行成功
                    statusCode = Status.OKAdd.ToString();

                    AfterAdd(entity);
                }
            }


            return result;
        }

        /// <summary>
        /// 添加之后，需要重新刷新缓存，否则其他读取数据的地方会乱了，或者不及时了
        /// 宋彪
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public int AfterAdd(BaseDepartmentEntity entity)
        {
            int result = 0;

            // 2016-01-28 更新用户缓存
            using (var redisClient = PooledRedisHelper.GetClient())
            {
                BaseDepartmentManager.CacheContractAreaPreheatingSpelling(redisClient, entity);
            }

            return result;
        }


        public override int BatchSave(DataTable dt)
        {
            int result = 0;
            BaseDepartmentEntity entity = new BaseDepartmentEntity();
            foreach (DataRow dr in dt.Rows)
            {
                // 删除状态
                if (dr.RowState == DataRowState.Deleted)
                {
                    string id = dr[BaseDepartmentEntity.FieldId, DataRowVersion.Original].ToString();
                    if (id.Length > 0)
                    {
                        result += this.DeleteObject(id);
                    }
                }
                // 被修改过
                if (dr.RowState == DataRowState.Modified)
                {
                    string id = dr[BaseDepartmentEntity.FieldId, DataRowVersion.Original].ToString();
                    if (id.Length > 0)
                    {
                        entity.GetFrom(dr);
                        result += this.UpdateObject(entity);
                    }
                }
                // 添加状态
                if (dr.RowState == DataRowState.Added)
                {
                    entity.GetFrom(dr);
                    result += this.AddObject(entity).Length > 0 ? 1 : 0;
                }
                if (dr.RowState == DataRowState.Unchanged)
                {
                    continue;
                }
                if (dr.RowState == DataRowState.Detached)
                {
                    continue;
                }
            }
            return result;
        }

        /// <summary>
        /// 移动
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="parentId">父级主键</param>
        /// <returns>影响行数</returns>
        public int MoveTo(string id, string parentId)
        {
            return this.SetProperty(id, new KeyValuePair<string, object>(BaseDepartmentEntity.FieldParentId, parentId));
        }

        /// <summary>
        /// 从缓存获取获取实体
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>实体</returns>
        public static BaseDepartmentEntity GetObjectByCache(string id, bool fefreshCache = false)
        {
            BaseDepartmentEntity result = null;
            if (!string.IsNullOrEmpty(id))
            {
                string key = "D:" + id;

                if (!fefreshCache)
                {
                    result = GetCacheByKey(key);
                }

                if (result == null)
                {
                    BaseDepartmentManager manager = new BaseDepartmentManager();
                    result = manager.GetObject(id);
                    // 若是空的不用缓存，继续读取实体
                    if (result != null)
                    {
                        SetCache(result);
                    }
                }
            }

            return result;
        }
    }
}