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
	/// BaseItemDetailsManager 
    /// 主键管理表结构定义部分（程序OK）
    ///
	/// 注意事项;
	///		Id 为主键
	///		CreateOn不为空，默认值
	///		ParentId、FullName 需要建立唯一索引
	///		CategoryId 是为了解决多重数据库兼容的问题
	///		ParentId 是为了解决形成树行结构的问题
	///
	/// 修改记录
    ///
    ///     2011.03.29 版本：4.5 JiRiGaLa  允许重复的名称，但是不允许编号和名称都重复。
    ///     2009.07.01 版本：4.4 JiRiGaLa  按某种权限获取主键列表。
    ///     2007.12.03 版本：4.3 JiRiGaLa  进行规范化整理。
    ///     2007.06.03 版本：4.2 JiRiGaLa  进行改进整理。
    ///     2007.05.31 版本：4.1 JiRiGaLa  进行改进整理。
    ///		2007.01.15 版本：4.0 JiRiGaLa  重新整理主键。
    ///		2007.01.03 版本：3.0 JiRiGaLa  进行大批量主键整理。
    ///		2006.12.05 版本：2.0 JiRiGaLa  GetFullName 方法更新。
	///		2006.01.23 版本：1.0 JiRiGaLa  获取ItemDetails方法的改进。
	///		2004.11.12 版本：1.0 JiRiGaLa  主键进行了绝对的优化，基本上看上去还过得去了。
    ///     2007.12.03 版本：2.2 JiRiGaLa  进行规范化整理。
    ///     2007.05.30 版本：2.1 JiRiGaLa  整理主键，调整GetFrom()方法,增加AddObject(),UpdateObject(),DeleteObject()
    ///		2007.01.15 版本：2.0 JiRiGaLa  重新整理主键。
	///		2006.02.06 版本：1.1 JiRiGaLa  重新调整主键的规范化。
	///		2005.10.03 版本：1.0 JiRiGaLa  表中添加是否可删除，可修改字段。
	///
	/// </summary>
	/// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2007.12.03</date>
	/// </author>
	/// </summary>
    public partial class BaseItemDetailsManager : BaseManager //, IBaseItemDetailsManager
    {
        public BaseItemDetailsManager(IDbHelper dbHelper, string tableName)
            : this(dbHelper)
        {
            CurrentTableName = tableName;
        }

        public List<BaseItemDetailsEntity> GetListByTable(string tableName)
        {
            List<BaseItemDetailsEntity> result = null;
            string sqlQuery = "   SELECT * "
                            + "     FROM " + tableName
                            + "    WHERE " + BaseItemDetailsEntity.FieldDeletionStateCode + " = 0 "
                            + "          AND " + BaseItemDetailsEntity.FieldEnabled + " = 1 "
                            + " ORDER BY " + BaseItemDetailsEntity.FieldSortCode;
            using (IDataReader dataReader = DbHelper.ExecuteReader(sqlQuery))
            {
                result = this.GetList<BaseItemDetailsEntity>(dataReader);
            }
            return result;
        }

        #region public string Add(BaseItemDetailsEntity entity, out string statusCode) 添加
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="statusCode">返回状态码</param>
        /// <returns>主键</returns>
        public string Add(BaseItemDetailsEntity entity, out string statusCode)
        {
            string result = string.Empty;
            // 检查编号是否重复
            if (!String.IsNullOrEmpty(entity.ItemCode))
            {
                List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
                parameters.Add(new KeyValuePair<string, object>(BaseItemDetailsEntity.FieldItemCode, entity.ItemCode));
                parameters.Add(new KeyValuePair<string, object>(BaseItemDetailsEntity.FieldDeletionStateCode, 0));
                if (this.Exists(parameters))
                {
                    // 编号已重复
                    statusCode = Status.ErrorCodeExist.ToString();
                    return result;
                }
            }

            /*

            if (!String.IsNullOrEmpty(entity.ItemName))
            {
                if (this.Exists(new KeyValuePair<string, object>(BaseItemDetailsEntity.FieldItemName, entity.ItemName)))
                {
                    // 名称已重复
                    statusCode = Status.ErrorNameExist.ToString();
                    return result;
                }
            }

            if (!String.IsNullOrEmpty(entity.ItemValue))
            {
                if (this.Exists(new KeyValuePair<string, object>(BaseItemDetailsEntity.FieldItemValue, entity.ItemValue)))
                {
                    // 值已重复
                    statusCode = Status.ErrorValueExist.ToString();
                    return result;
                }
            }

            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            if (entity.ParentId.HasValue)
            {
                parameters.Add(new KeyValuePair<string, object>(BaseItemDetailsEntity.FieldParentId, entity.ParentId));
            }
            parameters.Add(new KeyValuePair<string, object>(BaseItemDetailsEntity.FieldItemCode, entity.ItemCode));
            parameters.Add(new KeyValuePair<string, object>(BaseItemDetailsEntity.FieldItemName, entity.ItemName));
            parameters.Add(new KeyValuePair<string, object>(BaseItemDetailsEntity.FieldDeletionStateCode, 0));

            if (this.Exists(parameters))
            {
                // 名称已重复
                statusCode = Status.Exist.ToString();
                return result;
            }
            */

            // 运行成功
            result = this.AddObject(entity);
            statusCode = Status.OKAdd.ToString();
            return result;
        }
        #endregion

        #region public int Update(BaseItemDetailsEntity entity, out string statusCode) 更新
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="statusCode">返回状态码</param>
        /// <returns>影响行数</returns>
        public int Update(BaseItemDetailsEntity entity, out string statusCode)
        {
            int result = 0;
            // 检查是否已被其他人修改            
            //if (DbLogic.IsModifed(DbHelper, this.CurrentTableName, itemDetailsEntity.Id, itemDetailsEntity.ModifiedUserId, itemDetailsEntity.ModifiedOn))
            //{
            //    // 数据已经被修改
            //    statusCode = StatusCode.ErrorChanged.ToString();
            //}
            // 检查编号是否重复
            // if (this.Exists(BaseItemDetailsEntity.FieldItemCode, itemDetailsEntity.ItemCode, itemDetailsEntity.Id))
            // if (this.Exists(BaseItemDetailsEntity.FieldItemValue, itemDetailsEntity.ItemValue, itemDetailsEntity.Id))
            // 检查名称是否重复

            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            if (entity.ParentId.HasValue)
            {
                parameters.Add(new KeyValuePair<string, object>(BaseItemDetailsEntity.FieldParentId, entity.ParentId));
            }
            parameters.Add(new KeyValuePair<string, object>(BaseItemDetailsEntity.FieldItemCode, entity.ItemCode));
            // parameters.Add(new KeyValuePair<string, object>(BaseItemDetailsEntity.FieldItemName, entity.ItemName));
            parameters.Add(new KeyValuePair<string, object>(BaseItemDetailsEntity.FieldDeletionStateCode, 0));

            if (this.Exists(parameters, entity.Id))
            {
                // 名称已重复
                statusCode = Status.Exist.ToString();
                return result;
            }
            result = this.UpdateObject(entity);
            if (result == 1)
            {
                statusCode = Status.OKUpdate.ToString();
            }
            else
            {
                statusCode = Status.ErrorDeleted.ToString();
            }
            return result;
        }
        #endregion

        #region public int BatchSave(DataTable result) 批量进行保存
        /// <summary>
        /// 批量进行保存
        /// </summary>
        /// <param name="result">数据表</param>
        /// <returns>影响行数</returns>
        public override int BatchSave(DataTable dt)
        {
            int result = 0;
            BaseItemDetailsEntity entity = new BaseItemDetailsEntity();
            foreach (DataRow dr in dt.Rows)
            {
                // 删除状态
                if (dr.RowState == DataRowState.Deleted)
                {
                    string id = dr[BaseItemDetailsEntity.FieldId, DataRowVersion.Original].ToString();
                    if (id.Length > 0)
                    {
                        if (entity.AllowDelete == 1)
                        {
                            result += this.Delete(id);
                        }
                    }
                }
                // 被修改过
                if (dr.RowState == DataRowState.Modified)
                {
                    string id = dr[BaseItemDetailsEntity.FieldId, DataRowVersion.Original].ToString();
                    if (id.Length > 0)
                    {
                        entity.GetFrom(dr);
                        if (!entity.IsPublic.HasValue)
                        {
                            entity.IsPublic = 1;
                        }
                        if (!entity.Enabled.HasValue)
                        {
                            entity.Enabled = 1;
                        }
                        if (!entity.DeletionStateCode.HasValue)
                        {
                            entity.DeletionStateCode = 0;
                        }
                        // 判断是否允许编辑
                        if (entity.AllowEdit == 1)
                        {
                            result += this.UpdateObject(entity);
                        }
                        else
                        {
                            // 不允许编辑，但是排序还是允许的
                            result += this.SetProperty(id, new KeyValuePair<string, object>(BaseItemDetailsEntity.FieldSortCode, entity.SortCode));
                        }
                    }
                }
                // 添加状态
                if (dr.RowState == DataRowState.Added)
                {
                    entity.GetFrom(dr);
                    if (!entity.IsPublic.HasValue)
                    {
                        entity.IsPublic = 1;
                    }
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
        #endregion

        public int Save(List<BaseItemDetailsEntity> list, string tableName)
        {
            int result = 0;
            if (list != null)
            {
                this.CurrentTableName = tableName;
                foreach (var entity in list)
                {
                    result = this.UpdateObject(entity);
                    if (result == 0)
                    {
                        this.AddObject(entity);
                    }
                }
                /*
                this.Delete();
                foreach (var entity in list)
                {
                    result += this.AddObject(entity).Length > 0 ? 1 : 0;
                }
                */
            }
            return result;
        }

        #region public int Save(DataTable result) 批量进行保存
        /// <summary>
        /// 批量进行保存
        /// </summary>
        /// <param name="result">数据表</param>
        /// <returns>影响行数</returns>
        public int Save(DataTable dt)
        {
            int result = 0;
            if (dt != null)
            {
                this.CurrentTableName = dt.TableName;
                this.Delete();
                BaseItemDetailsEntity entity = new BaseItemDetailsEntity();
                foreach (DataRow dr in dt.Rows)
                {
                    entity.GetFrom(dr);
                    result += this.AddObject(entity).Length > 0 ? 1 : 0;
                }
            }
            return result;
        }
        #endregion

        #region public DataTable GetDataTableByPermission(string userId, string resourceCategory, string permissionCode) 按某种权限获取主键列表
        /// <summary>
        /// 按某种权限获取主键列表
        /// </summary>
        /// <param name="userId">用户主键</param>
        /// <param name="resourceCategory">资源分类</param>
        /// <param name="permissionCode">权限编号</param>
        /// <returns>数据表</returns>
        public DataTable GetDataTableByPermission(string userId, string resourceCategory, string permissionCode = "Resource.ManagePermission")
        {
            BasePermissionScopeManager permissionScopeManager = new BasePermissionScopeManager(DbHelper, UserInfo);
            string[] ids = permissionScopeManager.GetResourceScopeIds(UserInfo.SystemCode, userId, resourceCategory, permissionCode);
            var dt = this.GetDataTable(ids);
            dt.DefaultView.Sort = BaseItemDetailsEntity.FieldSortCode;
            return dt;
        }
        #endregion

        /// <summary>
        /// 移动
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="parentId">父级主键</param>
        /// <returns>影响行数</returns>
        public int MoveTo(string id, string parentId)
        {
            return this.SetProperty(id, new KeyValuePair<string, object>(BaseItemDetailsEntity.FieldParentId, parentId));
        }

        #region public static List<BaseItemDetailsEntity> GetEntitiesByCache(string tableName, bool refresh = false) 获取模块菜单表，从缓存读取
        /// <summary>
        /// 获取模块菜单表，从缓存读取
        /// 2016-03-14 吉日嘎拉 更新有缓存功能
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="refresh">刷新</param>
        /// <returns>选项数据列表</returns>
        public static List<BaseItemDetailsEntity> GetEntitiesByCache(string tableName, bool refresh = false)
        {
            List<BaseItemDetailsEntity> result = null;

            string key = "ItemDetails:" + tableName;
            if (!refresh)
            {
                result = GetListCache(key);
            }

            if (result == null)
            {
                // 这里只要有效的，没被删除的
                BaseItemDetailsManager itemDetailsManager = new BaseItemDetailsManager(tableName);
                List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
                // 管理的时候无效的也需要被管理
                parameters.Add(new KeyValuePair<string, object>(BaseItemDetailsEntity.FieldEnabled, 1));
                parameters.Add(new KeyValuePair<string, object>(BaseItemDetailsEntity.FieldDeletionStateCode, 0));
                result = itemDetailsManager.GetList<BaseItemDetailsEntity>(parameters, BaseItemDetailsEntity.FieldSortCode);
                
                // 若是空的不用缓存，继续读取实体
                if (result != null)
                {
                    SetListCache(key, result);
                }
            }

            return result;
        }
        #endregion

        public static BaseItemDetailsEntity GetObjectByCache(string tableName, string id)
        {
            BaseItemDetailsEntity result = null;

            string key = "ItemDetails:" + tableName;
            if (!string.IsNullOrEmpty(id))
            {
                key = "ItemDetails:" + tableName + ":" + id;
            }
            result = GetCache(key);

            if (result == null)
            {
                // 动态读取表中的数据
                BaseItemDetailsManager manager = new BaseItemDetailsManager(tableName);
                result = manager.GetObject(id);
                // 若是空的不用缓存，继续读取实体
                if (result != null)
                {
                    SetCache(tableName, result);
                }
            }

            return result;
        }
    }
}