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
    /// BaseRoleManager 
    /// 角色表结构定义部分
    ///
    /// 修改记录
    ///
    ///     2008.04.09 版本：1.0 JiRiGaLa 创建主键。
    ///     
    /// </summary>
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2008.04.09</date>
    /// </author>
    /// </summary>
    public partial class BaseRoleManager : BaseManager //, IBaseRoleManager
    {
        #region public string Add(BaseRoleEntity entity, out string statusCode) 添加
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="statusCode">返回状态码</param>
        /// <returns>主键</returns>
        public string Add(BaseRoleEntity entity, out string statusCode)
        {
            string result = string.Empty;
            // 检查名称是否重复
            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>(BaseRoleEntity.FieldRealName, entity.RealName));
            parameters.Add(new KeyValuePair<string, object>(BaseRoleEntity.FieldDeletionStateCode, 0));
            if (!string.IsNullOrEmpty(entity.OrganizeId))
            {
                parameters.Add(new KeyValuePair<string, object>(BaseRoleEntity.FieldOrganizeId, entity.OrganizeId));
            }
            if (this.Exists(parameters))
            {
                // 名称是否重复
                statusCode = Status.ErrorNameExist.ToString();
            }
            else
            {
                result = this.AddObject(entity);
                // 运行成功
                statusCode = Status.OKAdd.ToString();
            }
            return result;
        }
        #endregion

        #region public string GetIdByRealName(string realName) 按名称获取主键
        /// <summary>
        /// 按名称获取主键
        /// </summary>
        /// <param name="realName">名称</param>
        /// <returns>主键</returns>
        public string GetIdByRealName(string realName)
        {
            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>(BaseRoleEntity.FieldRealName, realName));
            parameters.Add(new KeyValuePair<string, object>(BaseRoleEntity.FieldDeletionStateCode, 0));
            parameters.Add(new KeyValuePair<string, object>(BaseRoleEntity.FieldEnabled, 1));
            return DbLogic.GetProperty(DbHelper, this.CurrentTableName, parameters, BaseBusinessLogic.FieldId);
        }
        #endregion

        /// <summary>
        /// 通过主键获取名称
        /// 这里是进行了内存缓存处理，减少数据库的I/O处理，提高程序的运行性能，
        /// 若有数据修改过，重新启动一下程序就可以了，这些基础数据也不是天天修改来修改去的，
        /// 所以没必要过度担忧，当然有需要时也可以写个刷新缓存的程序
        /// </summary>
        /// <param name="systemCode">系统编号</param>
        /// <param name="id">主键</param>
        /// <returns>显示值</returns>
        public static string GetRealNameByCache(string systemCode, string id)
        {
            string result = string.Empty;

            BaseRoleEntity entity = GetObjectByCache("Base", id);
            if (entity != null)
            {
                result = entity.RealName;
            }

            return result;
        }

        /// <summary>
        /// 通过编号获取主键
        /// </summary>
        /// <param name="systemCode">系统编号</param>
        /// <param name="code">编号</param>
        /// <returns>显示值</returns>
        public static string GetIdByCodeByCache(string systemCode, string code)
        {
            string result = string.Empty;

            BaseRoleEntity entity = GetObjectByCacheByCode(systemCode, code);
            if (entity != null)
            {
                result = entity.Id;
            }

            return result;
        }

        /// <summary>
        /// 通过名称获取主键
        /// </summary>
        /// <param name="systemCode">系统编号</param>
        /// <param name="realName">名称</param>
        /// <returns>显示值</returns>
        public static string GetIdByNameByCache(string systemCode, string realName)
        {
            string result = string.Empty;

            BaseRoleEntity entity = GetObjectByCacheByName(systemCode, realName);
            if (entity != null)
            {
                result = entity.RealName;
            }

            return result;
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="code">编号</param>
        /// <returns>实体</returns>
        public BaseRoleEntity GetObjectByCode(string code)
        {
            BaseRoleEntity result = null;

            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>(BaseRoleEntity.FieldCode, code));
            parameters.Add(new KeyValuePair<string, object>(BaseRoleEntity.FieldDeletionStateCode, 0));
            result = BaseEntity.Create<BaseRoleEntity>(this.ExecuteReader(parameters));

            return result;
        }

        /// <summary>
        /// 按名称获取实体
        /// </summary>
        /// <param name="realName">名称</param>
        public BaseRoleEntity GetObjectByName(string realName)
        {
            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>(BaseRoleEntity.FieldRealName, realName));
            parameters.Add(new KeyValuePair<string, object>(BaseRoleEntity.FieldDeletionStateCode, 0));
            return BaseEntity.Create<BaseRoleEntity>(this.ExecuteReader(parameters));
        }

        /// <summary>
        /// 获取角色列表中的角色名称
        /// </summary>
        /// <param name="list">角色列表</param>
        /// <returns>角色名称</returns>
        public static string GetNames(List<BaseRoleEntity> list)
        {
            string result = string.Empty;

            foreach (BaseRoleEntity entity in list)
            {
                result += "," + entity.RealName;
            }
            if (!string.IsNullOrEmpty(result))
            {
                result = result.Substring(1);
            }

            return result;
        }

        #region public DataTable GetDataTableByOrganize(string organizeId) 获取列表
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="organizeId">组织机构主键</param>
        /// <returns>数据表</returns>
        public DataTable GetDataTableByOrganize(string organizeId)
        {
            List<KeyValuePair<string, object>> parametersList = new List<KeyValuePair<string, object>>();
            parametersList.Add(new KeyValuePair<string, object>(BaseRoleEntity.FieldOrganizeId, organizeId));
            parametersList.Add(new KeyValuePair<string, object>(BaseRoleEntity.FieldDeletionStateCode, 0));
            return this.GetDataTable(parametersList, BaseRoleEntity.FieldSortCode);
            /*
            string sqlQuery = "SELECT " + BaseRoleEntity.TableName + ".*,"
                            + " (SELECT COUNT(1) FROM " + BaseUserRoleEntity.TableName + " WHERE (Enabled = 1) AND (RoleId = " + BaseRoleEntity.TableName + ".Id)) AS UserCount "
                            + " FROM " + BaseRoleEntity.TableName
                            + " WHERE " + BaseRoleEntity.FieldSystemId + " = " + "'" + systemId + "'"
                            + " ORDER BY " + BaseRoleEntity.FieldSortCode;
            return DbHelper.Fill(sqlQuery);
            */
        }
        #endregion

        #region public DataTable GetDataTableByName(string roleName) 按角色名称获取列表
        /// <summary>
        /// 按角色名称获取列表
        /// </summary>
        /// <param name="roleName">名称</param>
        /// <returns>数据表</returns>
        public DataTable GetDataTableByName(string roleName)
        {
            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>(BaseRoleEntity.FieldRealName, roleName));
            parameters.Add(new KeyValuePair<string, object>(BaseRoleEntity.FieldDeletionStateCode, 0));
            return this.GetDataTable(parameters, BaseRoleEntity.FieldSortCode);
        }
        #endregion

        #region public DataTable Search(string organizeId, string searchValue,string categoryCode=null) 查询
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="organizeId">组织机构主键</param>
        /// <param name="search">查询字符串</param>
        /// <param name="categoryCode">分类编号</param>
        /// <returns>数据表</returns>
        public DataTable Search(string organizeId, string searchValue, string categoryCode = null)
        {
            string sqlQuery = null;
            sqlQuery += "SELECT * FROM " + this.CurrentTableName + " WHERE " + BaseRoleEntity.FieldDeletionStateCode + " = 0 ";

            if (!string.IsNullOrEmpty(searchValue))
            {
                searchValue = StringUtil.GetSearchString(searchValue);
                sqlQuery += string.Format("  AND ({0} LIKE '{1}' OR {2} LIKE '{3}')", BaseRoleEntity.FieldRealName, searchValue, BaseRoleEntity.FieldDescription, searchValue);
            }
            if (!String.IsNullOrEmpty(organizeId))
            {
                sqlQuery += string.Format(" AND {0} = '{1}'", BaseRoleEntity.FieldOrganizeId, organizeId);
            }
            if (!String.IsNullOrEmpty(categoryCode))
            {
                sqlQuery += string.Format(" AND {0} = '{1}'", BaseRoleEntity.FieldCategoryCode, categoryCode);
            }
            sqlQuery += " ORDER BY " + BaseRoleEntity.FieldSortCode;
            return DbHelper.Fill(sqlQuery);
        }
        #endregion

        #region public int MoveTo(string id, string targetOrganizeId) 移动
        /// <summary>
        /// 移动
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="targetSystemId">目标主键</param>
        /// <returns>影响行数</returns>
        public int MoveTo(string id, string targetSystemId)
        {
            //return this.SetProperty(id, new KeyValuePair<string, object>(BaseRoleEntity.FieldSystemId, targetSystemId));
            return this.SetProperty(id, new KeyValuePair<string, object>(BaseRoleEntity.FieldOrganizeId, targetSystemId));
        }
        #endregion

        #region public int BatchMoveTo(string[] ids, string targetOrganizeId) 批量移动
        /// <summary>
        /// 批量移动
        /// </summary>
        /// <param name="ids">主键数组</param>
        /// <param name="targetOrganizeId">目标主键</param>
        /// <returns>影响行数</returns>
        public int BatchMoveTo(string[] ids, string targetOrganizeId)
        {
            int result = 0;
            for (int i = 0; i < ids.Length; i++)
            {
                result += this.MoveTo(ids[i], targetOrganizeId);
            }
            return result;
        }
        #endregion

        #region public int BatchSave(List<BaseRoleEntity> entites) 批量保存
        /// <summary>
        /// 批量保存
        /// </summary>
        /// <param name="entites">实体列表</param>
        /// <returns>影响行数</returns>
        public int BatchSave(List<BaseRoleEntity> entites)
        {
            /*
            foreach (BaseRoleEntity roleEntity in roleEntites)
            {
                // 删除状态
                if (dr.RowState == DataRowState.Deleted)
                {
                    string id = dr[BaseRoleEntity.FieldId, DataRowVersion.Original].ToString();
                    if (id.Length > 0)
                    {
                        result += this.Delete(id);
                    }
                }
                // 被修改过
                if (dr.RowState == DataRowState.Modified)
                {
                    string id = dr[BaseRoleEntity.FieldId, DataRowVersion.Original].ToString();
                    if (!String.IsNullOrEmpty(id))
                    {
                        roleEntity.GetFrom(dr);
                        result += this.UpdateObject(roleEntity);
                    }
                }
                // 添加状态
                if (dr.RowState == DataRowState.Added)
                {
                    roleEntity.GetFrom(dr);
                    result += this.AddObject(roleEntity).Length > 0 ? 1 : 0;
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
            */

            int result = 0;
            foreach (BaseRoleEntity entity in entites)
            {
                result += this.UpdateObject(entity);
            }
            return result;
        }
        #endregion

        #region public int ResetSortCode(string organizeId) 重置排序码
        /// <summary>
        /// 重置排序码
        /// </summary>
        /// <param name="organizeId">组织机构主键</param>
        public int ResetSortCode(string organizeId)
        {
            int result = 0;
            var dt = this.GetDataTable();
            string id = string.Empty;
            BaseSequenceManager sequenceManager = new BaseSequenceManager(DbHelper);
            string[] sortCode = sequenceManager.GetBatchSequence(BaseRoleEntity.TableName, dt.Rows.Count);
            int i = 0;
            foreach (DataRow dr in dt.Rows)
            {
                id = dr[BaseRoleEntity.FieldId].ToString();
                result += this.SetProperty(id, new KeyValuePair<string, object>(BaseRoleEntity.FieldSortCode, sortCode[i]));
                i++;
            }
            return result;
        }
        #endregion

        #region public DataTable GetDataTableByPage(string userId, string categoryCode, string serviceState, string searchValue, out int recordCount, int pageIndex = 0, int pageSize = 20, string sortExpression = null, string sortDire = null)
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="result"></param>
        /// <param name="categoryCode">分类编码</param>
        /// <param name="searchValue">查询字段</param>
        /// <param name="recordCount">记录数</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页显示</param>
        /// <param name="sortExpression">排序字段</param>
        /// <param name="sortDire">排序方向</param>
        /// <returns>数据表</returns>
        public DataTable GetDataTableByPage(BaseUserInfo userInfo, string categoryCode, string searchValue, out int recordCount, int pageIndex = 0, int pageSize = 20, string sortExpression = null, string sortDire = null)
        {
            string whereClause = BaseRoleEntity.FieldDeletionStateCode + " = 0 ";

            if (!string.IsNullOrEmpty(categoryCode))
            {
                whereClause += string.Format(" AND {0} = '{1}'", BaseRoleEntity.FieldCategoryCode, categoryCode);
            }

            if (!string.IsNullOrEmpty(searchValue))
            {
                searchValue = string.Format("'{0}'", StringUtil.GetSearchString(searchValue));
                whereClause += string.Format(" AND ({0} LIKE {1}", BaseRoleEntity.FieldRealName, searchValue);
                whereClause += string.Format(" OR {0} LIKE {1}", BaseRoleEntity.FieldCategoryCode, searchValue);
                whereClause += string.Format(" OR {0} LIKE {1})", BaseRoleEntity.FieldCode, searchValue);
            }
            return GetDataTableByPage(out recordCount, pageIndex, pageSize, sortExpression, sortDire, this.CurrentTableName, whereClause, null, "*");
        }
        #endregion

        public DataTable GetUserDataTable(string systemCode, string roleId, string companyId, string userId, string searchValue, out int recordCount, int pageIndex, int pageSize, string orderBy)
        {
            DataTable result = new DataTable(BaseUserEntity.TableName);

            string tableName = BaseUserRoleEntity.TableName;
            if (!string.IsNullOrWhiteSpace(systemCode))
            {
                tableName = systemCode + "UserRole";
            }

            string commandText = @"SELECT BaseUser.Id
                                    , BaseUser.Code
                                    , BaseUser.UserName
                                    , BaseUser.CompanyName
                                    , BaseUser.DepartmentName
                                    , BaseUser.RealName 
                                    , BaseUser.Description 
                                    , UserRole.Enabled
                                    , UserRole.CreateOn
                                    , UserRole.CreateBy
                                    , UserRole.ModifiedOn
                                    , UserRole.ModifiedBy
                        FROM BaseUser,
                          (SELECT UserId, Enabled, CreateOn, CreateBy, ModifiedOn, ModifiedBy
                             FROM BaseUserRole
                            WHERE RoleId = " + DbHelper.GetParameter(BaseUserRoleEntity.FieldRoleId) + @" AND DeletionStateCode = 0) UserRole 
                         WHERE BaseUser.Id = UserRole.UserId 
                               AND BaseUser.DeletionStateCode = 0 ";
            if (!string.IsNullOrEmpty(searchValue))
            {
                // 2016-02-25 吉日嘎拉 增加搜索功能、方便管理
                if (searchValue.IndexOf("%") < 0)
                {
                    searchValue = string.Format("%{0}%", searchValue);
                }
                commandText += " AND (" + BaseUserEntity.FieldCode + " LIKE '" + searchValue + "'"
                         + " OR " + BaseUserEntity.FieldUserName + " LIKE '" + searchValue + "'"
                         + " OR " + BaseUserEntity.FieldDepartmentName + " LIKE '" + searchValue + "'"
                         + " OR " + BaseUserEntity.FieldRealName + " LIKE '" + searchValue + "')";
            }
            // ORDER BY UserRole.CreateOn DESC ";
            commandText = commandText.Replace("BaseUserRole", tableName);
            List<IDbDataParameter> dbParameters = new List<IDbDataParameter>();
            dbParameters.Add(DbHelper.MakeParameter(BaseUserRoleEntity.FieldRoleId, roleId));
            
            if (!string.IsNullOrEmpty(companyId))
            {
                commandText += " AND " + BaseUserEntity.TableName + "." + BaseUserEntity.FieldCompanyId + " = " + DbHelper.GetParameter(BaseUserEntity.FieldCompanyId);
                dbParameters.Add(DbHelper.MakeParameter(BaseUserEntity.FieldCompanyId, companyId));
            }
            if (!string.IsNullOrEmpty(userId))
            {
                commandText += " AND " + BaseUserEntity.TableName + "." + BaseUserEntity.FieldId + " = " + DbHelper.GetParameter(BaseUserEntity.FieldId);
                dbParameters.Add(DbHelper.MakeParameter(BaseUserEntity.FieldId, userId));
            }
            commandText = "(" + commandText + ") T ";
            // 2015-12-05 吉日嘎拉 增加参数化功能
            result = DbLogic.GetDataTableByPage(this.DbHelper, out recordCount, commandText, "*", pageIndex, pageSize, null, dbParameters.ToArray(), orderBy);

            return result;
        }

        /// <summary>
        /// 获取角色权限范围（组织机构）
        /// 2015-11-28 吉日嘎拉 整理参数化
        /// </summary>
        /// <param name="systemCode">系统编号</param>
        /// <param name="roleId">角色主键</param>
        /// <returns>组织机构表</returns>
        public DataTable GetOrganizeDataTable(string systemCode, string roleId)
        {
            DataTable result = new DataTable(BaseOrganizeEntity.TableName);

            string tableName = BaseRoleOrganizeEntity.TableName;
            if (!string.IsNullOrWhiteSpace(this.UserInfo.SystemCode))
            {
                tableName = this.UserInfo.SystemCode + "RoleOrganize";
            }

            string commandText = @"SELECT BaseOrganize.Id
                                    , BaseOrganize.Code
                                    , BaseOrganize.FullName 
                                    , BaseOrganize.Description 
                                    , RoleOrganize.Enabled
                                    , RoleOrganize.CreateOn
                                    , RoleOrganize.CreateBy
                                    , RoleOrganize.ModifiedOn
                                    , RoleOrganize.ModifiedBy
                        FROM BaseOrganize RIGHT OUTER JOIN
                          (SELECT OrganizeId, Enabled, CreateOn, CreateBy, ModifiedOn, ModifiedBy
                             FROM BaseRoleOrganize
                            WHERE RoleId = " + DbHelper.GetParameter(BaseRoleOrganizeEntity.FieldRoleId) + 
                                " AND DeletionStateCode = " + DbHelper.GetParameter(BaseRoleOrganizeEntity.FieldDeletionStateCode) + @") RoleOrganize 
                            ON BaseOrganize.Id = RoleOrganize.OrganizeId
                         WHERE BaseOrganize.Enabled = 1 AND BaseOrganize.DeletionStateCode = 0
                      ORDER BY RoleOrganize.CreateOn DESC ";

            commandText = commandText.Replace("BaseRoleOrganize", tableName);

            List<IDbDataParameter> dbParameters = new List<IDbDataParameter>();
            dbParameters.Add(DbHelper.MakeParameter(BaseRoleOrganizeEntity.FieldRoleId, roleId));
            dbParameters.Add(DbHelper.MakeParameter(BaseRoleOrganizeEntity.FieldDeletionStateCode, 0));
            
            result = this.Fill(commandText, dbParameters.ToArray());

            return result;
        }

        /// <summary>
        /// 获取角色权限范围（组织机构）
        /// 2015-12-10 吉日嘎拉 整理参数化
        /// </summary>
        /// <param name="systemCode">系统编号</param>
        /// <param name="roleId">角色主键</param>
        /// <returns>组织机构列表</returns>
        public List<BaseOrganizeEntity> GetOrganizeList(string systemCode, string roleId)
        {
            List<BaseOrganizeEntity> result = new List<BaseOrganizeEntity>();

            string tableName = BaseRoleOrganizeEntity.TableName;
            if (!string.IsNullOrWhiteSpace(this.UserInfo.SystemCode))
            {
                tableName = this.UserInfo.SystemCode + "RoleOrganize";
            }

            string commandText = @"SELECT *
                                     FROM BaseOrganize 
                                    WHERE BaseOrganize.Enabled = 1 
                                          AND BaseOrganize.DeletionStateCode = 0  Id IN 
                                              (SELECT OrganizeId
                                                 FROM BaseRoleOrganize
                                                WHERE RoleId = " + DbHelper.GetParameter(BaseRoleOrganizeEntity.FieldRoleId)
                                                  + " AND Enabled = " + DbHelper.GetParameter(BaseRoleOrganizeEntity.FieldEnabled)
                                                  + " AND DeletionStateCode = " + DbHelper.GetParameter(BaseRoleOrganizeEntity.FieldDeletionStateCode) + ")";

            commandText = commandText.Replace("BaseRoleOrganize", tableName);

            List<IDbDataParameter> dbParameters = new List<IDbDataParameter>();
            dbParameters.Add(DbHelper.MakeParameter(BaseRoleOrganizeEntity.FieldRoleId, roleId));
            dbParameters.Add(DbHelper.MakeParameter(BaseRoleOrganizeEntity.FieldEnabled, 1));
            dbParameters.Add(DbHelper.MakeParameter(BaseRoleOrganizeEntity.FieldDeletionStateCode, 0));

            result = this.GetList<BaseOrganizeEntity>(DbHelper.ExecuteReader(commandText, dbParameters.ToArray()));

            return result;
        }

        #region public int Delete(string id) 删除
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>影响行数</returns>
        public int Delete(string id)
        {
            int result = 0;

            // 删除角色权限结构定义
            // result = DbLogic.Delete(DbHelper, BaseRoleModuleOperationTable.TableName, BaseRoleModuleOperationTable.FieldRoleId, id);
            
            // 删除员工角色表结构定义部分
            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>(BaseUserRoleEntity.FieldRoleId, id));
            result += DbLogic.Delete(DbHelper, BaseUserRoleEntity.TableName, parameters);
            
            // 删除角色的表结构定义部分
            parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>(BaseRoleEntity.FieldId, id));
            parameters.Add(new KeyValuePair<string, object>(BaseRoleEntity.FieldAllowDelete, 1));
            result += DbLogic.Delete(DbHelper, this.CurrentTableName, parameters);

            return result;
        }
        #endregion

        #region public int BatchDelete(string id) 批量删除
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="id">主键数组</param>
        /// <returns>影响行数</returns>
        public int BatchDelete(string[] ids)
        {
            int result = 0;

            for (int i = 0; i < ids.Length; i++)
            {
                result += this.Delete(ids[i]);
            }

            return result;
        }
        #endregion

        /// <summary>
        /// 从缓存获取获取实体
        /// </summary>
        /// <param name="id">主键</param>
        public static BaseRoleEntity GetObjectByCache(string id)
        {
            return GetObjectByCache("Base", id);
        }

        /// <summary>
        /// 从缓存获取获取实体
        /// </summary>
        /// <param name="id">主键</param>
        public static BaseRoleEntity GetObjectByCache(BaseUserInfo userInfo, string id)
        {
            return GetObjectByCache(userInfo.SystemCode, id);
        }

        public static BaseRoleEntity GetObjectByCache(string systemCode, string id, bool fefreshCache = false)
        {
            BaseRoleEntity result = null;

            if (string.IsNullOrWhiteSpace(systemCode))
            {
                systemCode = "Base";
            }
            string key = systemCode + ".Role";
            if (!string.IsNullOrEmpty(id))
            {
                key = systemCode + ".Role." + id;
            }

            // 2016-02-29 吉日嘎拉，强制刷新缓存的方法改进。
            if (!fefreshCache)
            {
                result = GetCacheByKey(key);
            }

            if (result == null)
            {
                // 动态读取表中的数据
                string tableName = systemCode + "Role";
                BaseRoleManager manager = new BaseRoleManager(tableName);
                result = manager.GetObject(id);
                // 若是空的不用缓存，继续读取实体
                if (result != null)
                {
                    SetCache(systemCode, result);
                }
            }

            return result;
        }

        /// <summary>
        /// 从缓存获取获取实体
        /// </summary>
        /// <param name="systemCode">系统编号</param>
        /// <param name="code">编号</param>
        /// <returns>权限实体</returns>
        public static BaseRoleEntity GetObjectByCacheByCode(string systemCode, string code)
        {
            BaseRoleEntity result = null;

            if (string.IsNullOrWhiteSpace(systemCode))
            {
                systemCode = "Base";
            }
            string key = systemCode + ".Role";
            if (!string.IsNullOrEmpty(code))
            {
                key = systemCode + ".Role." + code;
            }
            result = GetCacheByKey(key);

            if (result == null)
            {
                // 动态读取表中的数据
                string tableName = systemCode + "Role";
                BaseRoleManager manager = new BaseRoleManager(tableName);
                result = manager.GetObjectByCode(code);
                // 若是空的不用缓存，继续读取实体
                if (result != null)
                {
                    SetCache(systemCode, result);
                }
            }

            return result;
        }

        /// <summary>
        /// 从缓存获取获取实体
        /// </summary>
        /// <param name="systemCode">系统编号</param>
        /// <param name="realName">名称</param>
        /// <returns>权限实体</returns>
        public static BaseRoleEntity GetObjectByCacheByName(string systemCode, string realName)
        {
            BaseRoleEntity result = null;

            if (string.IsNullOrWhiteSpace(systemCode))
            {
                systemCode = "Base";
            }
            string key = systemCode + ".Role";
            if (!string.IsNullOrEmpty(realName))
            {
                key = systemCode + ".Role." + realName;
            }
            result = GetCacheByKey(key);

            if (result == null)
            {
                // 动态读取表中的数据
                string tableName = systemCode + "Role";
                BaseRoleManager manager = new BaseRoleManager(tableName);
                result = manager.GetObjectByName(realName);
                // 若是空的不用缓存，继续读取实体
                if (result != null)
                {
                    SetCache(systemCode, result);
                }
            }

            return result;
        }

        #region public static List<BaseRoleEntity> GetEntitiesByCache(string systemCode = "Base") 获取模块菜单表，从缓存读取
        /// <summary>
        /// 获取模块菜单表，从缓存读取
        /// </summary>
        /// <param name="systemCode">系统编号</param>
        /// <returns>角色列表</returns>
        public static List<BaseRoleEntity> GetEntitiesByCache(string systemCode = "Base", bool fefreshCache = false)
        {
            List<BaseRoleEntity> result = new List<BaseRoleEntity>();

            string tableName = systemCode + "Role";
            if (!fefreshCache)
            {
                result = GetListCacheByKey(tableName);
            }
            if (result == null)
            {
                BaseRoleManager roleManager = new BaseRoleManager(tableName);
                // 读取目标表中的数据
                List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
                // 有效的菜单
                parameters.Add(new KeyValuePair<string, object>(BaseRoleEntity.FieldEnabled, 1));
                // 没被删除的菜单
                parameters.Add(new KeyValuePair<string, object>(BaseRoleEntity.FieldDeletionStateCode, 0));
                // parameters.Add(new KeyValuePair<string, object>(BaseRoleEntity.FieldIsVisible, 1));
                // 2015-11-30 吉日嘎拉，这里没必要进行排序，浪费时间，列表可以认为是无序的，在界面上进行排序就可以了
                result = roleManager.GetList<BaseRoleEntity>(parameters);
                // 若是空的不用缓存，继续读取实体
                if (result != null)
                {
                    SetListCacheByKey(tableName, result);
                }
            }

            return result;
        }
        #endregion
    }
}
