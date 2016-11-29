//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd.  
//-----------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DotNet.Business
{
    using DotNet.Model;
    using DotNet.Utilities;

    /// <summary>
    /// BasePermissionScopeManager
    /// 资源权限范围
    ///
    ///	修改记录
    /// 
    ///     组织
    ///      ↓
    ///     角色 → 组织
    ///      ↓
    ///     用户
    ///     
    /// 
    ///     用户能有某种权限的所有员工      public string[] GetUserIds(string managerUserId, string permissionCode)
    ///                                     public string GetUserIdsSql(string managerUserId, string permissionCode)
    ///     
    ///     用户能有某种权限所有组织机构    public string[] GetOrganizeIds(string managerUserId, string permissionCode)
    ///                                     public string GetOrganizeIdsSql(string managerUserId, string permissionCode)
    ///     
    ///     用户能有某种权限的所有角色      public string[] GetAllRoleIds(string managerUserId, string permissionCode)
    ///                                     public string GetAllRoleIdsSql(string managerUserId, string permissionCode)
    ///     
    ///     2011.10.27 版本：4.3 张广梁 增加获得有效的委托列表的方法GetAuthoriedList。
    ///     2011.09.21 版本：4.2 张广梁 增加 public bool HasAuthorized(string[] names, object[] values,string startDate,string endDate)
    ///		2010.07.06 版本：4.1 JiRiGaLa permissionCode，result 修改为 permissionCode，result。
    ///		2007.03.03 版本：4.0 JiRiGaLa 核心的外部调用程序进行整理。
    ///		2007.03.03 版本：3.0 JiRiGaLa 调整主键的规范化。
    ///		2007.02.15 版本：2.0 JiRiGaLa 调整主键的规范化。
    ///	    2006.02.12 版本：1.2 JiRiGaLa 调整主键的规范化。
    ///     2005.08.14 版本：1.1 JiRiGaLa 添加了批量添加和批量删除。
    ///     2004.11.19 版本：1.0 JiRiGaLa 主键进行了绝对的优化，这是个好东西啊，平时要多用，用得要灵活些。
    ///     
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2007.03.03</date>
    /// </author>
    /// </summary>
    public partial class BasePermissionScopeManager : BaseManager, IBaseManager
    {
        /// <summary>
        /// 是否按编号获得子节点，SQL2005以上或者Oracle数据库按ParentId,Id进行关联 
        /// </summary>
        public static bool UseGetChildrensByCode = false;

        #region public bool PermissionScopeExists(string result, string resourceCategory, string resourceId, string targetCategory, string targetId) 检查是否存在
        /// <summary>
        /// 检查是否存在
        /// </summary>
        /// <param name="result">权限主键</param>
        /// <param name="resourceCategory">资源分类</param>
        /// <param name="resourceId">资源主键</param>
        /// <param name="targetCategory">目标分类</param>
        /// <param name="targetId">目标主键</param>
        /// <returns>是否存在</returns>
        public bool PermissionScopeExists(string permissionId, string resourceCategory, string resourceId, string targetCategory, string targetId)
        {
            bool result = true;

            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldPermissionId, permissionId));
            parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldResourceCategory, resourceCategory));
            parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldResourceId, resourceId));
            parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldTargetCategory, targetCategory));
            parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldTargetId, targetId));

            // 检查是否存在
            if (!this.Exists(parameters))
            {
                result = false;
            }
            return result;
        }
        #endregion

        public int PermissionScopeDelete(string permissionId, string resourceCategory, string resourceId, string targetCategory, string targetId)
        {
            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldResourceCategory, resourceCategory));
            parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldResourceId, resourceId));
            parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldTargetCategory, targetCategory));
            parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldTargetId, targetId));
            parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldPermissionId, permissionId));
            return this.Delete(parameters);
        }

        public string AddPermission(string resourceCategory, string resourceId, string targetCategory, string targetId)
        {
            BasePermissionScopeEntity resourcePermissionScope = new BasePermissionScopeEntity();
            resourcePermissionScope.ResourceCategory = resourceCategory;
            resourcePermissionScope.ResourceId = resourceId;
            resourcePermissionScope.TargetCategory = targetCategory;
            resourcePermissionScope.TargetId = targetId;
            resourcePermissionScope.Enabled = 1;
            resourcePermissionScope.DeletionStateCode = 0;
            return this.AddPermission(resourcePermissionScope);
        }

        #region public string AddPermission(BasePermissionScopeEntity resourcePermissionScope)
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="paramObject">对象</param>
        /// <returns>主键</returns>
        public string AddPermission(BasePermissionScopeEntity resourcePermissionScope)
        {
            string result = string.Empty;
            // 检查记录是否重复
            if (!this.PermissionScopeExists(resourcePermissionScope.PermissionId.ToString(), resourcePermissionScope.ResourceCategory, resourcePermissionScope.ResourceId, resourcePermissionScope.TargetCategory, resourcePermissionScope.TargetId))
            {
                result = this.AddObject(resourcePermissionScope);
            }
            return result;
        }
        #endregion

        /// <summary>
        /// 获得用户的权限范围设置
        /// </summary>
        /// <param name="managerUserId">用户主键</param>
        /// <param name="permissionCode">权限范围编号</param>
        /// <returns>用户的权限范围</returns>
        public PermissionOrganizeScope GetUserPermissionScope(string systemCode, string managerUserId, string permissionCode)
        {
            string sqlQuery = this.GetOrganizeIdsSql(systemCode, managerUserId, permissionCode);
            var dt = DbHelper.Fill(sqlQuery);
            string[] organizeIds = BaseBusinessLogic.FieldToArray(dt, BasePermissionScopeEntity.FieldTargetId).Distinct<string>().Where(t => !string.IsNullOrEmpty(t)).ToArray();
            return BaseBusinessLogic.GetPermissionScope(organizeIds);
        }

        // 权限范围的判断

        //
        // 获得被某个权限管理范围内 组织机构的 Id、SQL、List
        //

        #region public string GetOrganizeIdsSql(string managerUserId, string permissionCode) 按某个权限获取组织机构 Sql
        /// <summary>
        /// 按某个权限获取组织机构 Sql
        /// </summary>
        /// <param name="managerUserId">管理用户主键</param>
        /// <param name="permissionCode">权限编号</param>
        /// <returns>Sql</returns>
        public string GetOrganizeIdsSql(string systemCode, string managerUserId, string permissionCode)
        {
            string permissionId = BaseModuleManager.GetIdByCodeByCache(systemCode, permissionCode);

            string sqlQuery = string.Empty;
            sqlQuery = "SELECT " + BasePermissionScopeEntity.FieldTargetId
                     + "   FROM " + BasePermissionScopeEntity.TableName
                // 有效的，并且不为空的组织机构主键
                     + "  WHERE (" + BasePermissionScopeEntity.FieldTargetCategory + " = '" + BaseOrganizeEntity.TableName + "') "
                     + "        AND ( " + BasePermissionScopeEntity.TableName + "." + BasePermissionScopeEntity.FieldDeletionStateCode + " = 0) "
                     + "        AND ( " + BasePermissionScopeEntity.TableName + "." + BasePermissionScopeEntity.FieldEnabled + " = 1) "
                     + "        AND ( " + BasePermissionScopeEntity.TableName + "." + BasePermissionScopeEntity.FieldTargetId + " IS NOT NULL) "
                // 自己直接由相应权限的组织机构
                     + "        AND ((" + BasePermissionScopeEntity.FieldResourceCategory + " = '" + BaseUserEntity.TableName + "' "
                     + "        AND " + BasePermissionScopeEntity.FieldResourceId + " = '" + managerUserId + "')"
                     + " OR (" + BasePermissionScopeEntity.FieldResourceCategory + " = '" + BaseRoleEntity.TableName + "' "
                     + "       AND " + BasePermissionScopeEntity.FieldResourceId + " IN ( "
                // 获得属于那些角色有相应权限的组织机构
                     + "SELECT " + BaseUserRoleEntity.FieldRoleId
                     + "   FROM " + BaseUserRoleEntity.TableName
                     + "  WHERE " + BaseUserRoleEntity.FieldUserId + " = '" + managerUserId + "'"
                     + "        AND " + BaseUserRoleEntity.FieldDeletionStateCode + " = 0 "
                     + "        AND " + BaseUserRoleEntity.FieldEnabled + " = 1"
                // 修正不会读取用户默认角色权限域范围BUG
                     + "))) "
                // 并且是指定的本权限
                     + " AND (" + BasePermissionScopeEntity.FieldPermissionId + " = '" + permissionId + "') ";
            return sqlQuery;
        }
        #endregion

        #region public string GetOrganizeIdsSqlByParentId(string managerUserId, string permissionCode) 按某个权限获取组织机构 Sql
        /// <summary>
        /// 按某个权限获取组织机构 Sql (按ParentId树形结构计算)
        /// </summary>
        /// <param name="managerUserId">管理用户主键</param>
        /// <param name="permissionCode">权限编号</param>
        /// <returns>Sql</returns>
        public string GetOrganizeIdsSqlByParentId(string systemCode, string managerUserId, string permissionCode)
        {
            string sqlQuery = string.Empty;
            sqlQuery = "SELECT Id "
                     + "   FROM " + BaseOrganizeEntity.TableName
                     + "  WHERE " + BaseOrganizeEntity.TableName + "." + BaseOrganizeEntity.FieldEnabled + " = 1 "
                     + "        AND " + BaseOrganizeEntity.TableName + "." + BaseOrganizeEntity.FieldDeletionStateCode + " = 0 "
                     + "  START WITH Id IN (" + this.GetOrganizeIdsSql(systemCode, managerUserId, permissionCode) + ") "
                     + " CONNECT BY PRIOR " + BaseOrganizeEntity.FieldId + " = " + BaseOrganizeEntity.FieldParentId;
            return sqlQuery;
        }
        #endregion

        #region public string GetOrganizeIdsSqlByCode(string managerUserId, string permissionCode) 按某个权限获取组织机构 Sql
        /// <summary>
        /// 按某个权限获取组织机构 Sql (按Code编号进行计算)
        /// </summary>
        /// <param name="managerUserId">管理用户主键</param>
        /// <param name="permissionCode">权限编号</param>
        /// <returns>Sql</returns>
        public string GetOrganizeIdsSqlByCode(string systemCode, string managerUserId, string permissionCode)
        {
            string sqlQuery = string.Empty;
            sqlQuery = "SELECT " + BaseOrganizeEntity.FieldId + " AS " + BaseBusinessLogic.FieldId
                     + "   FROM " + BaseOrganizeEntity.TableName
                     + "         , ( SELECT " + DbHelper.PlusSign(BaseOrganizeEntity.FieldCode, "'%'") + " AS " + BaseOrganizeEntity.FieldCode
                     + "      FROM " + BaseOrganizeEntity.TableName
                     + "     WHERE " + BaseOrganizeEntity.FieldId + " IN (" + this.GetOrganizeIdsSql(systemCode, managerUserId, permissionCode) + ")) ManageOrganize "
                     + " WHERE (" + BaseOrganizeEntity.TableName + "." + BaseOrganizeEntity.FieldEnabled + " = 1 "
                // 编号相似的所有组织机构获取出来
                     + "       AND " + BaseOrganizeEntity.TableName + "." + BaseOrganizeEntity.FieldCode + " LIKE ManageOrganize." + BaseOrganizeEntity.FieldCode + ")";
            return sqlQuery;
        }
        #endregion


        #region public string[] GetOrganizeIds(string managerUserId, string permissionCode = "Resource.ManagePermission", bool organizeIdOnly = true) 按某个权限获取组织机构 主键数组
        /// <summary>
        /// 按某个权限获取组织机构 主键数组
        /// </summary>
        /// <param name="managerUserId">管理用户主键</param>
        /// <param name="permissionCode">权限编号</param>
        /// <param name="organizeIdOnly">只返回组织机构主键</param>
        /// <returns>主键数组</returns>
        public string[] GetOrganizeIds(string systemCode, string managerUserId, string permissionCode = "Resource.ManagePermission", bool organizeIdOnly = true)
        {
            // 这里应该考虑，当前用户的管理权限是，所在公司？所在部门？所以在工作组等情况
            string sqlQuery = string.Empty;
            if (BasePermissionScopeManager.UseGetChildrensByCode)
            {
                sqlQuery = this.GetOrganizeIdsSqlByCode(systemCode, managerUserId, permissionCode);
            }
            else
            {
                if (this.DbHelper.CurrentDbType == CurrentDbType.Oracle)
                {
                    sqlQuery = this.GetOrganizeIdsSqlByParentId(systemCode, managerUserId, permissionCode);
                }
                else
                {
                    // edit by zgl 不默认获取子部门
                    // string[] ids = this.GetTreeResourceScopeIds(managerUserId, BaseOrganizeEntity.TableName, permissionCode, true);
                    string[] ids = this.GetTreeResourceScopeIds(systemCode, managerUserId, BaseOrganizeEntity.TableName, permissionCode, false);
                    if (ids != null && ids.Length > 0 && organizeIdOnly)
                    {
                        TransformPermissionScope(managerUserId, ref ids);
                    }
                    // 这里是否应该整理，自己的公司、部门、工作组的事情？
                    if (organizeIdOnly)
                    {
                        // 这里列出只是有效地，没被删除的组织机构主键
                        if (ids != null && ids.Length > 0)
                        {
                            BaseOrganizeManager organizeManager = new BaseOrganizeManager(this.DbHelper, this.UserInfo);
                            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
                            parameters.Add(new KeyValuePair<string, object>(BaseOrganizeEntity.FieldId, ids));
                            parameters.Add(new KeyValuePair<string, object>(BaseOrganizeEntity.FieldEnabled, 1));
                            parameters.Add(new KeyValuePair<string, object>(BaseOrganizeEntity.FieldDeletionStateCode, 0));
                            ids = organizeManager.GetIds(parameters);
                        }
                    }
                    return ids;
                }
            }
            var dt = DbHelper.Fill(sqlQuery);
            return BaseBusinessLogic.FieldToArray(dt, BaseOrganizeEntity.FieldId).Distinct<string>().Where(t => !string.IsNullOrEmpty(t)).ToArray();
        }
        #endregion

        #region public DataTable GetOrganizeDT(string managerUserId, string permissionCode = "Resource.ManagePermission", bool childrens = true) 按某个权限获取组织机构 数据表
        /// <summary>
        /// 按某个权限获取组织机构 数据表
        /// </summary>
        /// <param name="managerUserId">管理用户主键</param>
        /// <param name="permissionCode">权限编号</param>
        /// <param name="childrens">获取子节点</param>
        /// <returns>数据表</returns>
        public DataTable GetOrganizeDT(string systemCode, string managerUserId, string permissionCode = "Resource.ManagePermission", bool childrens = true)
        {
            string whereQuery = string.Empty;
            PermissionOrganizeScope permissionScope = PermissionOrganizeScope.NotAllowed;
            if (BasePermissionScopeManager.UseGetChildrensByCode)
            {
                whereQuery = this.GetOrganizeIdsSqlByCode(systemCode, managerUserId, permissionCode);
            }
            else
            {
                if (this.DbHelper.CurrentDbType == CurrentDbType.Oracle)
                {
                    whereQuery = this.GetOrganizeIdsSqlByParentId(systemCode, managerUserId, permissionCode);
                }
                else
                {
                    // edit by zgl on 2011.12.15, 不自动获取子部门
                    string[] ids = this.GetTreeResourceScopeIds(systemCode, managerUserId, BaseOrganizeEntity.TableName, permissionCode, childrens);
                    permissionScope = TransformPermissionScope(managerUserId, ref ids);
                    // 需要进行适当的翻译，所在部门，所在公司，全部啥啥的。
                    whereQuery = StringUtil.ArrayToList(ids);
                }
            }
            if (string.IsNullOrEmpty(whereQuery))
            {
                whereQuery = " NULL ";
            }
            string sqlQuery = string.Empty;
            sqlQuery = "SELECT * FROM " + BaseOrganizeEntity.TableName
                     + " WHERE " + BaseOrganizeEntity.FieldDeletionStateCode + " = 0 "
                     + "   AND " + BaseOrganizeEntity.FieldEnabled + " = 1 "
                     + "   AND " + BaseOrganizeEntity.FieldIsInnerOrganize + " = 1 ";
            if (permissionScope != PermissionOrganizeScope.AllData)
            {
                sqlQuery += " AND " + BaseOrganizeEntity.TableName + "." + BaseOrganizeEntity.FieldId + " IN (" + whereQuery + ") ";
            }
            sqlQuery += " ORDER BY " + BaseOrganizeEntity.FieldSortCode;
            return DbHelper.Fill(sqlQuery);
        }
        #endregion


        //
        // 获得被某个权限管理范围内 角色的 Id、SQL、List
        // 

        #region public string GetRoleIdsSql(string systemCode, string managerUserId, string permissionCode, bool useBaseRole = false) 按某个权限获取角色 Sql
        /// <summary>
        /// 按某个权限获取角色 Sql
        /// </summary>
        /// <param name="systemCode">系统编号</param>
        /// <param name="managerUserId">管理用户主键</param>
        /// <param name="permissionCode">权限编号</param>
        /// <param name="useBaseRole">基础系统的权限是否采用</param>
        /// <returns>Sql</returns>
        public string GetRoleIdsSql(string systemCode, string managerUserId, string permissionCode, bool useBaseRole = false)
        {
            string permissionId = BaseModuleManager.GetIdByCodeByCache(systemCode, permissionCode);
            string roleTableName = systemCode + "Role";
            string userRoleTableName = systemCode + "UserRole";
            string permissionScopeTableName = systemCode + "PermissionScope";
            string sqlQuery = string.Empty;
            // 被管理的角色 
            sqlQuery += "SELECT " + permissionScopeTableName + ".TargetId AS " + BaseBusinessLogic.FieldId
                      + "   FROM " + permissionScopeTableName
                      + "  WHERE " + permissionScopeTableName + ".TargetId IS NOT NULL "
                      + "        AND " + permissionScopeTableName + ".TargetCategory = '" + roleTableName + "' "
                      + "        AND ((" + permissionScopeTableName + ".ResourceCategory = '" + BaseUserEntity.TableName + "' "
                      + "             AND " + permissionScopeTableName + ".ResourceId = '" + managerUserId + "')"
                // 以及 他所在的角色在管理的角色
                      + "        OR (" + permissionScopeTableName + ".ResourceCategory = '" + roleTableName + "'"
                      + "            AND " + permissionScopeTableName + ".ResourceId IN ( "
                      + " SELECT RoleId "
                                                 + "   FROM " + userRoleTableName
                      + "  WHERE (" + BaseUserRoleEntity.FieldUserId + " = '" + managerUserId + "' "
                      + "        AND " + BaseUserRoleEntity.FieldEnabled + " = 1) ";

            if (useBaseRole)
            {
                sqlQuery += " UNION SELECT RoleId FROM BaseUserRole WHERE (UserId = '" + managerUserId + "' AND Enabled = 1  ) ";
            }

            // 并且是指定的本权限
            sqlQuery += ")) " + " AND " + BasePermissionScopeEntity.FieldPermissionId + " = '" + permissionId + "')";

            // 被管理部门的列表
            string[] organizeIds = this.GetOrganizeIds(systemCode, managerUserId, permissionCode);
            if (organizeIds.Length > 0)
            {
                string organizes = string.Join(",", organizeIds);
                if (!String.IsNullOrEmpty(organizes))
                {
                    // 被管理的组织机构包含的角色
                    sqlQuery += "  UNION "
                              + " SELECT " + roleTableName + "." + BaseRoleEntity.FieldId + " AS " + BaseBusinessLogic.FieldId
                              + "   FROM " + roleTableName
                              + "  WHERE " + roleTableName + "." + BaseRoleEntity.FieldEnabled + " = 1 "
                              + "    AND " + roleTableName + "." + BaseRoleEntity.FieldDeletionStateCode + " = 0 "
                              + "    AND " + roleTableName + "." + BaseRoleEntity.FieldOrganizeId + " IN (" + organizes + ") ";
                }
            }
            return sqlQuery;
        }
        #endregion

        #region public string[] GetRoleIds(string managerUserId, string permissionCode) 按某个权限获取角色 主键数组
        /// <summary>
        /// 按某个权限获取角色 主键数组
        /// </summary>
        /// <param name="managerUserId">管理用户主键</param>
        /// <param name="permissionCode">权限编号</param>
        /// <returns>主键数组</returns>
        public string[] GetRoleIds(string systemCode, string managerUserId, string permissionCode)
        {
            string sqlQuery = this.GetRoleIdsSql(systemCode, managerUserId, permissionCode);
            var dt = DbHelper.Fill(sqlQuery);
            string[] ids = BaseBusinessLogic.FieldToArray(dt, BaseBusinessLogic.FieldId).Distinct<string>().Where(t => !string.IsNullOrEmpty(t)).ToArray();
            // 这里列出只是有效地，没被删除的角色主键
            if (ids != null && ids.Length > 0)
            {
                BaseRoleManager roleManager = new BaseRoleManager(this.DbHelper, this.UserInfo);

                List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
                parameters.Add(new KeyValuePair<string, object>(BaseRoleEntity.FieldId, ids));
                parameters.Add(new KeyValuePair<string, object>(BaseRoleEntity.FieldEnabled, 1));
                parameters.Add(new KeyValuePair<string, object>(BaseRoleEntity.FieldDeletionStateCode, 0));

                ids = roleManager.GetIds(parameters);
            }
            return ids;
        }
        #endregion

        public List<BaseRoleEntity> GetRoleList(string systemCode, string userId, string permissionCode = "Resource.ManagePermission", bool useBaseRole = false)
        {
            List<BaseRoleEntity> result = new List<BaseRoleEntity>();
            DataTable dt = GetRoleDT(systemCode, userId, permissionCode, useBaseRole);
            result = BaseRoleEntity.GetList<BaseRoleEntity>(dt);
            return result;
        }

        #region public DataTable GetRoleDT(string systemCode, string userId, string permissionCode, bool useBaseRole = false) 按某个权限获取角色 数据表
        /// <summary>
        /// 按某个权限获取角色 数据表
        /// </summary>
        /// <param name="systemCode">系统编号</param>
        /// <param name="userId">管理用户主键</param>
        /// <param name="permissionCode">权限编号</param>
        /// <param name="useBaseRole">使用基础角色</param>
        /// <returns>数据表</returns>
        public DataTable GetRoleDT(string systemCode, string userId, string permissionCode = "Resource.ManagePermission", bool useBaseRole = false)
        {
            DataTable result = new DataTable(BaseRoleEntity.TableName);

            // 这里需要判断,是系统权限？
            bool isAdmin = false;

            BaseUserEntity userEntity = BaseUserManager.GetObjectByCache(userId);

            BaseUserManager userManager = new BaseUserManager(this.DbHelper, this.UserInfo);
            // 用户管理员,这里需要判断,是业务权限？
            isAdmin = userManager.IsAdministrator(userEntity);
            /*
                || userManager.IsInRoleByCode(systemCode, userId, "UserAdmin", useBaseRole)
                || userManager.IsInRoleByCode(systemCode, userId, "Admin", useBaseRole);
            */
            string tableName = BaseRoleEntity.TableName;
            if (!string.IsNullOrEmpty(systemCode))
            {
                tableName = systemCode + "Role";
            }
            if (isAdmin)
            {
                BaseRoleManager manager = new BaseRoleManager(this.DbHelper, this.UserInfo, tableName);
                List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
                parameters.Add(new KeyValuePair<string, object>(BaseRoleEntity.FieldIsVisible, 1));
                parameters.Add(new KeyValuePair<string, object>(BaseRoleEntity.FieldEnabled, 1));
                parameters.Add(new KeyValuePair<string, object>(BaseRoleEntity.FieldDeletionStateCode, 0));
                result = manager.GetDataTable(parameters, BaseModuleEntity.FieldSortCode);
                result.TableName = this.CurrentTableName;
                return result;
            }

            string sqlQuery = string.Empty;
            sqlQuery = "SELECT * "
                      + "  FROM " + tableName
                      + " WHERE " + BaseRoleEntity.FieldCreateUserId + " = '" + userId + "'"
                      + "    OR " + tableName + "." + BaseRoleEntity.FieldId + " IN ("
                                + this.GetRoleIdsSql(systemCode, userId, permissionCode, useBaseRole)
                                + " ) AND (" + BaseRoleEntity.FieldDeletionStateCode + " = 0) "
                                + " AND (" + BaseRoleEntity.FieldIsVisible + " = 1) "
                   + " ORDER BY " + BaseRoleEntity.FieldSortCode;

            return DbHelper.Fill(sqlQuery);
        }
        #endregion

        //
        // 获得被某个权限管理范围内 用户的 Id、SQL、List
        // 

        #region public string GetUserIdsSql(string managerUserId, string permissionCode) 按某个权限获取员工 Sql
        /// <summary>
        /// 按某个权限获取用户主键 Sql
        /// </summary>
        /// <param name="managerUserId">管理用户主键</param>
        /// <param name="permissionCode">权限编号</param>
        /// <returns>Sql</returns>
        public string GetUserIdsSql(string systemCode, string managerUserId, string permissionCode)
        {
            string permissionId = BaseModuleManager.GetIdByCodeByCache(systemCode, permissionCode);

            string sqlQuery = string.Empty;

            // 直接管理的用户
            sqlQuery = "SELECT BasePermissionScope.TargetId AS " + BaseBusinessLogic.FieldId
                     + "   FROM BasePermissionScope "
                     + "  WHERE (BasePermissionScope.TargetCategory = '" + BaseUserEntity.TableName + "'"
                     + "        AND BasePermissionScope.ResourceId = '" + managerUserId + "'"
                     + "        AND BasePermissionScope.ResourceCategory = '" + BaseUserEntity.TableName + "'"
                     + "        AND BasePermissionScope.PermissionId = '" + permissionId + "'"
                     + "        AND BasePermissionScope.TargetId IS NOT NULL) ";

            // 被管理部门的列表
            string[] organizeIds = this.GetOrganizeIds(systemCode, managerUserId, permissionCode, false);
            if (organizeIds != null && organizeIds.Length > 0)
            {
                // 是否仅仅是自己的还有点儿问题
                if (StringUtil.Exists(organizeIds, ((int)PermissionOrganizeScope.OnlyOwnData).ToString()))
                {
                    sqlQuery += " UNION SELECT '" + this.UserInfo.Id + "' AS Id ";
                }
                else
                {
                    string organizes = string.Join(",", organizeIds);
                    if (!String.IsNullOrEmpty(organizes))
                    {
                        // 被管理的组织机构包含的用户，公司、部门、工作组
                        // sqlQuery += " UNION "
                        //         + "SELECT " + BaseStaffEntity.TableName + "." + BaseStaffEntity.FieldUserId + " AS " + BaseBusinessLogic.FieldId
                        //         + "   FROM " + BaseStaffEntity.TableName
                        //         + "  WHERE (" + BaseStaffEntity.TableName + "." + BaseStaffEntity.FieldCompanyId + " IN (" + organizes + ") "
                        //         + "     OR " + BaseStaffEntity.TableName + "." + BaseStaffEntity.FieldDepartmentId + " IN (" + organizes + ") "
                        //         + "     OR " + BaseStaffEntity.TableName + "." + BaseStaffEntity.FieldWorkgroupId + " IN (" + organizes + ")) ";

                        sqlQuery += " UNION "
                                 + "SELECT " + BaseUserEntity.TableName + "." + BaseUserEntity.FieldId + " AS " + BaseBusinessLogic.FieldId
                                 + "   FROM " + BaseUserEntity.TableName
                                 + "  WHERE (" + BaseUserEntity.TableName + "." + BaseUserEntity.FieldDeletionStateCode + " = 0 ) "
                                 + "        AND (" + BaseUserEntity.TableName + "." + BaseUserEntity.FieldEnabled + " = 1 ) "
                                 + "        AND (" + BaseUserEntity.TableName + "." + BaseUserEntity.FieldCompanyId + " IN (" + organizes + ") "
                                  + "            OR " + BaseUserEntity.TableName + "." + BaseUserEntity.FieldSubCompanyId + " IN (" + organizes + ") "
                                 + "            OR " + BaseUserEntity.TableName + "." + BaseUserEntity.FieldDepartmentId + " IN (" + organizes + ") "
                                 + "            OR " + BaseUserEntity.TableName + "." + BaseUserEntity.FieldWorkgroupId + " IN (" + organizes + ")) ";
                    }
                }
            }

            // 被管理角色列表
            string[] roleIds = this.GetRoleIds(systemCode, managerUserId, permissionCode);
            if (roleIds.Length > 0)
            {
                string roles = string.Join(",", roleIds);
                if (!String.IsNullOrEmpty(roles))
                {
                    // 被管理的角色包含的员工
                    sqlQuery += " UNION "
                             + "SELECT " + BaseUserRoleEntity.TableName + "." + BaseUserRoleEntity.FieldUserId + " AS " + BaseBusinessLogic.FieldId
                             + "   FROM " + BaseUserRoleEntity.TableName
                             + "  WHERE (" + BaseUserRoleEntity.TableName + "." + BaseUserRoleEntity.FieldEnabled + " = 1 "
                             + "        AND " + BaseUserRoleEntity.TableName + "." + BaseUserRoleEntity.FieldDeletionStateCode + " = 0 "
                             + "        AND " + BaseUserRoleEntity.TableName + "." + BaseUserRoleEntity.FieldRoleId + " IN (" + roles + ")) ";
                }
            }

            return sqlQuery;
        }
        #endregion

        #region public string[] GetUserIds(string systemCode, string managerUserId, string permissionCode) 按某个权限获取员工 主键数组
        /// <summary>
        /// 按某个权限获取员工 主键数组
        /// </summary>
        /// <param name="managerUserId">管理用户主键</param>
        /// <param name="permissionCode">权限编号</param>
        /// <returns>主键数组</returns>
        public string[] GetUserIds(string systemCode, string managerUserId, string permissionCode)
        {
            string[] ids = this.GetTreeResourceScopeIds(systemCode, managerUserId, BaseOrganizeEntity.TableName, permissionCode, true);
            // 是否为仅本人
            if (StringUtil.Exists(ids, ((int)PermissionOrganizeScope.OnlyOwnData).ToString()))
            {
                return new string[] { managerUserId };
            }

            string sqlQuery = this.GetUserIdsSql(systemCode, managerUserId, permissionCode);
            var dt = DbHelper.Fill(sqlQuery);

            // 这里应该考虑，当前用户的管理权限是，所在公司？所在部门？所以在工作组等情况
            if (ids != null && ids.Length > 0)
            {
                BaseUserEntity userEntity = BaseUserManager.GetObjectByCache(managerUserId);
                for (int i = 0; i < ids.Length; i++)
                {
                    if (ids[i].Equals(((int)PermissionOrganizeScope.OnlyOwnData).ToString()))
                    {
                        ids[i] = userEntity.Id;
                        break;
                    }
                }
            }

            // 这里列出只是有效地，没被删除的角色主键
            if (ids != null && ids.Length > 0)
            {
                BaseUserManager userManager = new BaseUserManager(this.DbHelper, this.UserInfo);

                List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
                parameters.Add(new KeyValuePair<string, object>(BaseUserEntity.FieldId, ids));
                parameters.Add(new KeyValuePair<string, object>(BaseUserEntity.FieldEnabled, 1));
                parameters.Add(new KeyValuePair<string, object>(BaseUserEntity.FieldDeletionStateCode, 0));

                string[] names = new string[] { BaseUserEntity.FieldId, BaseUserEntity.FieldEnabled, BaseUserEntity.FieldDeletionStateCode };
                Object[] values = new Object[] { ids, 1, 0 };
                ids = userManager.GetIds(parameters);
            }

            return ids;
        }
        #endregion

        #region public List<BaseUserEntity> GetUserList(string userId, string permissionCode) 按某个权限获取员工 数据表
        /// <summary>
        /// 按某个权限获取员工 数据表
        /// </summary>
        /// <param name="userId">管理用户主键</param>
        /// <param name="permissionCode">权限编号</param>
        /// <returns>数据表</returns>
        public List<BaseUserEntity> GetUserList(string systemCode, string userId, string permissionCode)
        {
            List<BaseUserEntity> result = new List<BaseUserEntity>();
            //string[] names = null;
            //object[] values = null;
            // 这里需要判断,是系统权限？
            bool isRole = false;
            BaseUserManager userManager = new BaseUserManager(this.DbHelper, this.UserInfo);
            // 用户管理员,这里需要判断,是业务权限？
            isRole = userManager.IsInRoleByCode(userId, "UserAdmin") || userManager.IsInRoleByCode(userId, "Admin");
            if (isRole)
            {
                List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
                parameters.Add(new KeyValuePair<string, object>(BaseUserEntity.FieldIsVisible, 1));
                parameters.Add(new KeyValuePair<string, object>(BaseUserEntity.FieldEnabled, 1));
                parameters.Add(new KeyValuePair<string, object>(BaseUserEntity.FieldDeletionStateCode, 0));
                return userManager.GetList<BaseUserEntity>(parameters, BaseModuleEntity.FieldSortCode);
            }

            string sqlQuery = string.Empty;
            sqlQuery = "SELECT * FROM " + BaseUserEntity.TableName;
            sqlQuery += " WHERE " + BaseUserEntity.TableName + "." + BaseUserEntity.FieldDeletionStateCode + " = 0 "
                     + " AND " + BaseUserEntity.TableName + "." + BaseUserEntity.FieldIsVisible + " = 1 "
                     + " AND " + BaseUserEntity.TableName + "." + BaseUserEntity.FieldEnabled + " = 1 "
                     + " AND " + BaseUserEntity.TableName + "." + BaseUserEntity.FieldId + " IN ("
                     + this.GetUserIdsSql(systemCode, userId, permissionCode)
                     + " ) "
                     + " ORDER BY " + BaseUserEntity.FieldSortCode;
            using (IDataReader dr = userManager.DbHelper.ExecuteReader(sqlQuery))
            {
                result = userManager.GetList<BaseUserEntity>(dr);
            }
            return result;
        }
        #endregion

        #region public DataTable GetUserDataTable(string userId, string permissionCode) 按某个权限获取员工 数据表
        /// <summary>
        /// 按某个权限获取员工 数据表
        /// </summary>
        /// <param name="userId">管理用户主键</param>
        /// <param name="permissionCode">权限编号</param>
        /// <returns>数据表</returns>
        public DataTable GetUserDataTable(string systemCode, string userId, string permissionCode)
        {
            //string[] names = null;
            //object[] values = null;
            // 这里需要判断,是系统权限？
            bool isRole = false;
            BaseUserManager userManager = new BaseUserManager(this.DbHelper, this.UserInfo);
            // 用户管理员,这里需要判断,是业务权限？
            isRole = userManager.IsInRoleByCode(userId, "UserAdmin") || userManager.IsInRoleByCode(userId, "Admin");
            if (isRole)
            {
                List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
                parameters.Add(new KeyValuePair<string, object>(BaseUserEntity.FieldIsVisible, 1));
                parameters.Add(new KeyValuePair<string, object>(BaseUserEntity.FieldEnabled, 1));
                parameters.Add(new KeyValuePair<string, object>(BaseUserEntity.FieldDeletionStateCode, 0));
                return userManager.GetDataTable(parameters, BaseModuleEntity.FieldSortCode);
            }

            string sqlQuery = string.Empty;
            sqlQuery = "SELECT * FROM " + BaseUserEntity.TableName;
            sqlQuery += " WHERE " + BaseUserEntity.TableName + "." + BaseUserEntity.FieldDeletionStateCode + " = 0 "
                     + " AND " + BaseUserEntity.TableName + "." + BaseUserEntity.FieldIsVisible + " = 1 "
                     + " AND " + BaseUserEntity.TableName + "." + BaseUserEntity.FieldEnabled + " = 1 "
                     + " AND " + BaseUserEntity.TableName + "." + BaseUserEntity.FieldId + " IN ("
                     + this.GetUserIdsSql(systemCode, userId, permissionCode)
                     + " ) "
                     + " ORDER BY " + BaseUserEntity.FieldSortCode;

            return userManager.Fill(sqlQuery);
        }
        #endregion

        #region public string[] GetTargetIds(string userId, string targetCategory, string result)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="result"></param>
        /// <returns>主键数组</returns>
        public string[] GetTargetIds(string userId, string targetCategory, string permissionId)
        {
            string[] result = new string[0];

            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldResourceCategory, BaseUserEntity.TableName));
            parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldResourceId, userId));
            parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldPermissionId, permissionId));
            parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldTargetCategory, targetCategory));

            var dt = this.GetDataTable(parameters);
            result = BaseBusinessLogic.FieldToArray(dt, BasePermissionScopeEntity.FieldTargetId).Distinct<string>().Where(t => !string.IsNullOrEmpty(t)).ToArray();
            return result;
        }
        #endregion

        //
        // ResourcePermissionScope 权限判断
        //


        /// <summary>
        /// 转换用户的权限范围
        /// </summary>
        /// <param name="userId">目标用户</param>
        /// <param name="resourceIds">权限范围</param>
        /// <param name="userManager"></param>
        /// <returns></returns>
        public PermissionOrganizeScope TransformPermissionScope(string userId, ref string[] resourceIds, BaseUserManager userManager = null)
        {
            PermissionOrganizeScope permissionScope = DotNet.Utilities.PermissionOrganizeScope.NotAllowed;
            if (resourceIds != null && resourceIds.Length > 0)
            {
                if (userManager == null)
                {
                    userManager = new BaseUserManager(DbHelper, UserInfo);
                }
                BaseUserEntity userEntity = userManager.GetObject(userId);

                for (int i = 0; i < resourceIds.Length; i++)
                {
                    if (resourceIds[i].Equals(((int)PermissionOrganizeScope.AllData).ToString()))
                    {
                        permissionScope = PermissionOrganizeScope.AllData;
                        continue;
                    }
                    if (resourceIds[i].Equals(((int)PermissionOrganizeScope.UserCompany).ToString()))
                    {
                        resourceIds[i] = userEntity.CompanyId;
                        permissionScope = PermissionOrganizeScope.UserCompany;
                        continue;
                    }
                    if (resourceIds[i].Equals(((int)PermissionOrganizeScope.UserSubCompany).ToString()))
                    {
                        resourceIds[i] = userEntity.SubCompanyId;
                        permissionScope = PermissionOrganizeScope.UserSubCompany;
                        continue;
                    }
                    if (resourceIds[i].Equals(((int)PermissionOrganizeScope.UserDepartment).ToString()))
                    {
                        resourceIds[i] = userEntity.DepartmentId;
                        permissionScope = PermissionOrganizeScope.UserDepartment;
                        continue;
                    }
                    if (resourceIds[i].Equals(((int)PermissionOrganizeScope.UserWorkgroup).ToString()))
                    {
                        resourceIds[i] = userEntity.WorkgroupId;
                        permissionScope = PermissionOrganizeScope.UserWorkgroup;
                        continue;
                    }
                }
            }
            return permissionScope;
        }

        /// <summary>
        /// 获得用户的某个权限范围资源主键数组
        /// </summary>
        /// <param name="userId">用户</param>
        /// <param name="targetCategory">资源分类</param>
        /// <param name="permissionCode">权限编号</param>
        /// <returns>主键数组</returns>
        public string[] GetResourceScopeIds(string systemCode, string userId, string targetCategory, string permissionCode)
        {
            string permissionId = BaseModuleManager.GetIdByCodeByCache(systemCode, permissionCode);

            string tableName = systemCode + "UserRole";

            this.CurrentTableName = systemCode + "PermissionScope";

            string sqlQuery = string.Empty;
            sqlQuery =
                // 用户的权限
                          "SELECT TargetId "
                        + "   FROM " + this.CurrentTableName
                        + "  WHERE (" + this.CurrentTableName + ".ResourceCategory = '" + BaseUserEntity.TableName + "') "
                        + "        AND (ResourceId = '" + userId + "') "
                        + "        AND (TargetCategory = '" + targetCategory + "') "
                        + "        AND (PermissionId = '" + permissionId + "') "
                        + "        AND (Enabled = 1) "
                        + "        AND (DeletionStateCode = 0)"

                        + " UNION "

                        // 用户归属的角色的权限                            
                        + "SELECT TargetId "
                        + "   FROM " + this.CurrentTableName
                        + "  WHERE (ResourceCategory  = '" + BaseRoleEntity.TableName + "') "
                        + "        AND (TargetCategory  = '" + targetCategory + "') "
                        + "        AND (PermissionId = '" + permissionId + "') "
                        + "        AND (DeletionStateCode = 0)"
                        + "        AND (Enabled = 1) "
                        + "        AND ((ResourceId IN ( "
                        + "             SELECT RoleId "
                        + "               FROM " + tableName
                        + "              WHERE (UserId  = '" + userId + "') "
                        + "                  AND (Enabled = 1) "
                        + "                  AND (DeletionStateCode = 0) ) ";
            sqlQuery += " ) "
            + " ) ";

            var dt = DbHelper.Fill(sqlQuery);
            string[] resourceIds = BaseBusinessLogic.FieldToArray(dt, BasePermissionScopeEntity.FieldTargetId).Distinct<string>().Where(t => !string.IsNullOrEmpty(t)).ToArray();

            // 按部门获取权限
            if (BaseSystemInfo.UseOrganizePermission)
            {
                sqlQuery = string.Empty;
                BaseUserEntity userEntity = new BaseUserManager(this.DbHelper).GetObject(userId);
                sqlQuery = "SELECT TargetId "
                           + "   FROM " + this.CurrentTableName
                           + "  WHERE (" + this.CurrentTableName + ".ResourceCategory = '" +
                           BaseOrganizeEntity.TableName + "') "
                           + "        AND (ResourceId = '" + userEntity.CompanyId + "' OR "
                           + "              ResourceId = '" + userEntity.DepartmentId + "' OR "
                           + "              ResourceId = '" + userEntity.SubCompanyId + "' OR"
                           + "              ResourceId = '" + userEntity.WorkgroupId + "') "
                           + "        AND (TargetCategory = '" + targetCategory + "') "
                           + "        AND (PermissionId = '" + permissionId + "') "
                           + "        AND (Enabled = 1) "
                           + "        AND (DeletionStateCode = 0)";
                dt = DbHelper.Fill(sqlQuery);
                string[] resourceIdsByOrganize = BaseBusinessLogic.FieldToArray(dt, BasePermissionScopeEntity.FieldTargetId).Distinct<string>().Where(t => !string.IsNullOrEmpty(t)).ToArray();
                resourceIds = StringUtil.Concat(resourceIds, resourceIdsByOrganize);
            }

            if (targetCategory.Equals(BaseOrganizeEntity.TableName))
            {
                TransformPermissionScope(userId, ref resourceIds);
            }
            return resourceIds;
        }

        /// <summary>
        /// 树型资源的权限范围
        /// 2011-03-17 吉日嘎拉
        /// 这个是嫩牛X的方法，值得收藏，值得分析
        /// </summary>
        /// <param name="userId">用户</param>
        /// <param name="targetCategory">资源分类</param>
        /// <param name="permissionCode">权限编号</param>
        /// <param name="childrens">是否含子节点</param>
        /// <returns>主键数组</returns>
        public string[] GetTreeResourceScopeIds(string systemCode, string userId, string tableName, string permissionCode, bool childrens)
        {
            string[] resourceScopeIds = null;
            resourceScopeIds = GetResourceScopeIds(systemCode, userId, tableName, permissionCode);
            if (!childrens)
            {
                return resourceScopeIds;
            }
            string idList = StringUtil.ArrayToList(resourceScopeIds);
            // 若本来就没管理部门啥的，那就没必要进行递归操作了
            if (!string.IsNullOrEmpty(idList))
            {
                string sqlQuery = string.Empty;

                if (DbHelper.CurrentDbType == CurrentDbType.SqlServer)
                {
                    sqlQuery = @" WITH PermissionScopeTree AS (SELECT ID 
                                                             FROM " + tableName + @"
                                                            WHERE (Id IN (" + idList + @") )
                                                            UNION ALL
                                                           SELECT ResourceTree.Id
                                                             FROM " + tableName + @" AS ResourceTree INNER JOIN
                                                                  PermissionScopeTree AS A ON A.Id = ResourceTree.ParentId)
                                                   SELECT Id
                                                     FROM PermissionScopeTree ";
                }
                else if (DbHelper.CurrentDbType == CurrentDbType.Oracle)
                {
                    sqlQuery = "     SELECT Id "
                             + "       FROM " + tableName
                             + " START WITH Id = ParentId "
                             + " CONNECT BY PRIOR Id Id IN (" + idList + ")";
                }

                var dt = DbHelper.Fill(sqlQuery);
                string[] resourceIds = BaseBusinessLogic.FieldToArray(dt, "Id").Distinct<string>().Where(t => !string.IsNullOrEmpty(t)).ToArray();
                return StringUtil.Concat(resourceScopeIds, resourceIds);
            }
            return resourceScopeIds;
        }

        #region public bool IsModuleAuthorized(BaseUserInfo userInfo, string moduleCode, string permissionCode) 是否有相应的权限
        /// <summary>
        /// 是否有相应的权限
        /// </summary>
        /// <param name=result>用户</param>
        /// <param name="permissionCode">权限编号</param>
        /// <returns>是否有权限</returns>
        public bool IsModuleAuthorized(BaseUserInfo userInfo, string moduleCode, string permissionCode)
        {
            // 先判断用户类别
            if (userInfo.IsAdministrator)
            {
                return true;
            }
            return this.IsModuleAuthorized(UserInfo.SystemCode, UserInfo.Id, moduleCode, permissionCode);
        }
        #endregion

        #region public bool IsModuleAuthorized(string userId, string moduleCode, string permissionCode) 是否有相应的权限
        /// <summary>
        /// 是否有相应的权限
        /// </summary>
        /// <param name="userId">用户主键</param>
        /// <param name="permissionCode">权限编号</param>
        /// <returns>是否有权限</returns>
        public bool IsModuleAuthorized(string systemCode, string userId, string moduleCode, string permissionCode)
        {
            string moduleId = BaseModuleManager.GetIdByCodeByCache(systemCode, moduleCode);
            string permissionId = BaseModuleManager.GetIdByCodeByCache(systemCode, permissionCode);
            // 判断员工权限
            if (this.CheckUserModulePermission(userId, moduleId, permissionId))
            {
                return true;
            }
            // 判断员工角色权限
            if (this.CheckRoleModulePermission(userId, moduleId, permissionId))
            {
                return true;
            }
            return false;
        }
        #endregion

        #region private bool CheckUserModulePermission(string userId, string moduleId, string result)
        /// <summary>
        /// 员工是否有模块权限
        /// </summary>
        /// <param name="userId">用户主键</param>
        /// <param name="result">权限主键</param>
        /// <returns>是否有权限</returns>
        private bool CheckUserModulePermission(string userId, string moduleId, string permissionId)
        {
            return CheckResourcePermissionScope(BaseModuleEntity.TableName, userId, BaseModuleEntity.TableName, moduleId, permissionId);
        }
        #endregion

        private bool CheckResourcePermissionScope(string resourceCategory, string resourceId, string targetCategory, string targetId, string permissionId)
        {
            string sqlQuery = "SELECT COUNT(1) "
                             + "  FROM BasePermissionScope "
                             + " WHERE (BasePermissionScope.ResourceCategory = '" + resourceCategory + "')"
                             + "       AND (BasePermissionScope.Enabled = 1) "
                             + "       AND (BasePermissionScope.DeletionStateCode = 0 )"
                             + "       AND (BasePermissionScope.ResourceId = '" + resourceId + "')"
                             + "       AND (BasePermissionScope.TargetCategory = '" + targetCategory + "')"
                             + "       AND (BasePermissionScope.TargetId = '" + targetId + "')"
                             + "       AND (BasePermissionScope.PermissionId = " + permissionId + "))";
            int rowCount = 0;

            object returnObject = DbHelper.ExecuteScalar(sqlQuery);
            if (returnObject != null)
            {
                rowCount = int.Parse(returnObject.ToString());
            }
            return rowCount > 0;
        }

        #region private bool CheckRoleModulePermission(string userId, string moduleId, string result)
        /// <summary>
        /// 员工角色关系是否有模块权限
        /// </summary>
        /// <param name="userId">用户主键</param>
        /// <param name="result">权限主键</param>
        /// <returns>有角色权限</returns>
        private bool CheckRoleModulePermission(string userId, string moduleId, string permissionId)
        {
            return CheckRolePermissionScope(userId, BaseModuleEntity.TableName, moduleId, permissionId);
        }
        #endregion

        private bool CheckRolePermissionScope(string userId, string targetCategory, string targetId, string permissionId)
        {
            string sqlQuery = "SELECT COUNT(1) "
                            + "   FROM BasePermissionScope "
                            + "  WHERE (BasePermissionScope.ResourceCategory = '" + BaseRoleEntity.TableName + "') "
                            + "        AND (BasePermissionScope.Enabled = 1 "
                            + "        AND (BasePermissionScope.DeletionStateCode = 0 "
                            + "        AND (BasePermissionScope.ResourceId IN ( "
                                             + "SELECT BaseUserRole.RoleId "
                                             + "   FROM BaseUserRole "
                                             + "  WHERE BaseUserRole.UserId = '" + userId + "'"
                                             + "        AND BaseUserRole.Enabled = 1 "
                                             + " )) "
                            + " AND (BasePermissionScope.TargetCategory = '" + targetCategory + "') "
                            + " AND (BasePermissionScope.TargetId = '" + targetId + "') "
                            + " AND (BasePermissionScope.PermissionId = " + permissionId + ")) ";
            int rowCount = 0;

            object returnObject = DbHelper.ExecuteScalar(sqlQuery);
            if (returnObject != null)
            {
                rowCount = int.Parse(returnObject.ToString());
            }
            return rowCount > 0;
        }

        public int GrantResourcePermissionScopeTarget(string resourceCategory, string resourceId, string targetCategory, string[] grantTargetIds, string permissionId, DateTime? startDate = null, DateTime? endDate = null)
        {
            int result = 0;

            BasePermissionScopeEntity resourcePermissionScope = new BasePermissionScopeEntity();
            resourcePermissionScope.ResourceCategory = resourceCategory;
            resourcePermissionScope.ResourceId = resourceId;
            resourcePermissionScope.TargetCategory = targetCategory;
            resourcePermissionScope.PermissionId = permissionId;
            resourcePermissionScope.StartDate = startDate;
            resourcePermissionScope.EndDate = endDate;
            resourcePermissionScope.Enabled = 1;
            resourcePermissionScope.DeletionStateCode = 0;
            for (int i = 0; i < grantTargetIds.Length; i++)
            {
                List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
                parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldResourceCategory, resourceCategory));
                parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldResourceId, resourceId));
                parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldTargetCategory, targetCategory));
                parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldTargetId, grantTargetIds[i]));
                parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldPermissionId, permissionId));
                parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldEnabled, 1));
                parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldDeletionStateCode, 0));

                resourcePermissionScope.TargetId = grantTargetIds[i];
                if (!this.Exists(parameters))
                {
                    this.Add(resourcePermissionScope);
                    result++;
                }
            }
            return result;
        }

        public int GrantResourcePermissionScopeTarget(string resourceCategory, string[] resourceIds, string targetCategory, string grantTargetId, string permissionId)
        {
            int result = 0;

            List<KeyValuePair<string, object>> parameters = null;
            BasePermissionScopeEntity resourcePermissionScope = new BasePermissionScopeEntity();
            resourcePermissionScope.ResourceCategory = resourceCategory;
            // resourcePermissionScope.ResourceId = resourceId;
            resourcePermissionScope.TargetCategory = targetCategory;
            resourcePermissionScope.PermissionId = permissionId;
            resourcePermissionScope.TargetId = grantTargetId;

            resourcePermissionScope.Enabled = 1;
            resourcePermissionScope.DeletionStateCode = 0;
            for (int i = 0; i < resourceIds.Length; i++)
            {
                parameters = new List<KeyValuePair<string, object>>();
                parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldResourceCategory, resourceCategory));
                parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldResourceId, resourceIds[i]));
                parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldTargetCategory, grantTargetId));
                parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldTargetId, targetCategory));
                parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldPermissionId, permissionId));
                parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldEnabled, 1));
                parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldDeletionStateCode, 0));

                resourcePermissionScope.ResourceId = resourceIds[i];
                if (!this.Exists(parameters))
                {
                    resourcePermissionScope.Id = null;
                    this.Add(resourcePermissionScope);
                    result++;
                }
            }
            return result;
        }

        /// <summary>
        /// 52.撤消资源的权限范围
        /// </summary>
        /// <param name="userInfo">用户</param>
        /// <param name="resourceCategory">资源分类</param>
        /// <param name="resourceId">资源主键</param>
        /// <param name="targetCategory">目标类别</param>
        /// <param name="revokeTargetIds">目标主键数组</param>
        /// <param name="result">权限主键</param>
        /// <returns>影响的行数</returns>
        public int RevokeResourcePermissionScopeTarget(string resourceCategory, string resourceId, string targetCategory, string[] revokeTargetIds, string permissionId)
        {
            int result = 0;
            for (int i = 0; i < revokeTargetIds.Length; i++)
            {
                List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
                parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldResourceCategory, resourceCategory));
                parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldResourceId, resourceId));
                parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldTargetCategory, targetCategory));
                parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldTargetId, revokeTargetIds[i]));
                parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldPermissionId, permissionId));
                parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldEnabled, 1));
                // 逻辑删除
                // result += this.SetDeleted(parameters);
                // 物理删除
                result += this.Delete(parameters);
            }
            return result;
        }

        /// <summary>
        /// 52.撤消资源的权限范围
        /// </summary>
        /// <param name="userInfo">用户</param>
        /// <param name="resourceCategory">资源分类</param>
        /// <param name="resourceId">资源主键</param>
        /// <param name="targetCategory">目标类别</param>
        /// <param name="revokeTargetIds">目标主键数组</param>
        /// <param name="result">权限主键</param>
        /// <returns>影响的行数</returns>
        public int RevokeResourcePermissionScopeTarget(string resourceCategory, string resourceId, string targetCategory, string permissionId)
        {
            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldResourceCategory, resourceCategory));
            parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldResourceId, resourceId));
            parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldTargetCategory, targetCategory));
            parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldPermissionId, permissionId));
            parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldEnabled, 1));
            // 逻辑删除
            return this.SetDeleted(parameters);
            // 物理删除
            // return this.Delete(parameters);
        }

        public int RevokeResourcePermissionScopeTarget(string resourceCategory, string[] resourceIds, string targetCategory, string revokeTargetId, string permissionId)
        {
            int result = 0;
            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            for (int i = 0; i < resourceIds.Length; i++)
            {
                parameters = new List<KeyValuePair<string, object>>();
                parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldResourceCategory, resourceCategory));
                parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldResourceId, resourceIds[i]));
                parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldTargetCategory, targetCategory));
                parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldTargetId, revokeTargetId));
                parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldPermissionId, permissionId));
                parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldEnabled, 1));
                // 逻辑删除
                result += this.SetDeleted(parameters);
                // 物理删除
                //result += this.Delete(parameters);
            }
            return result;
        }

        #region public bool HasAuthorized(string[] names, object[] values, string startDate, string endDate) 判断某个时间范围内是否存在
        /// <summary>
        /// 判断某个时间范围内是否存在
        /// </summary>
        /// <param name="names"></param>
        /// <param name="values"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public bool HasAuthorized(List<KeyValuePair<string, object>> parameters, string startDate, string endDate)
        {
            bool result = false;
            BasePermissionScopeManager manager = new BasePermissionScopeManager(this.DbHelper, this.UserInfo);
            result = manager.Exists(parameters);
            /*
            if (result)
            {
                // 再去判断时间
                string minDate = "1900-01-01 00:00:00";
                string maxDate = "3000-12-31 00:00:00";
                // 不用设置的太大
                string srcStartDate = manager.GetProperty(names, values, BasePermissionScopeEntity.FieldStartDate);
                string srcEndDate = manager.GetProperty(names, values, BasePermissionScopeEntity.FieldEndDate);
                if (string.IsNullOrEmpty(srcStartDate))
                {
                    srcStartDate = minDate;
                }
                if (string.IsNullOrEmpty(startDate))
                {
                    startDate = minDate;
                }
                if (string.IsNullOrEmpty(srcEndDate))
                {
                    srcEndDate = maxDate;
                }
                if (string.IsNullOrEmpty(endDate))
                {
                    endDate = maxDate;
                }
                // 满足这样的条件
                // 时间A(Start1-End1)，   时间B(Start2-End2)
                // Start1 <Start2   &&   Start2 <End1
                // Start1 <End2   &&   End2 <End1
                // Start2 <Start1   &&   End1 <End2
                if ((CheckDateScope(srcStartDate, startDate) && CheckDateScope(startDate, srcEndDate)) 
                    || (CheckDateScope(srcStartDate, endDate) && CheckDateScope(endDate, srcEndDate)) 
                    || (CheckDateScope(startDate, srcStartDate) && CheckDateScope(srcEndDate, endDate)))
                {
                    result = true;
                }
                else 
                {
                    result = false;
                }                   
            }
            */
            return result;
        }
        #endregion

        #region  public int CheckDateScope(string smallDate,string bigDate) 检查日期大小
        /// <summary>
        /// 检查日期大小
        /// </summary>
        /// <param name="smallDate">开始日期</param>
        /// <param name="bigDate">结束日期</param>
        /// <returns>0：小于等于 1：大于等于</returns>
        public bool CheckDateScope(string smallDate, string bigDate)
        {
            // 开始日期是无限大
            // 结束日期是无限大
            return DateTime.Parse(smallDate) < DateTime.Parse(bigDate);
        }
        #endregion

        public DataTable Search(string resourceId, string resourceCategory, string targetCategory)
        {
            string sqlQuery = string.Empty;
            sqlQuery = "SELECT * FROM " + this.CurrentTableName
                            + " WHERE " + BasePermissionScopeEntity.FieldDeletionStateCode + " =0 "
                            + " AND " + BasePermissionScopeEntity.FieldEnabled + " =1 ";
            List<IDbDataParameter> dbParameters = new List<IDbDataParameter>();
            if (!String.IsNullOrEmpty(resourceId))
            {
                sqlQuery += " AND " + BasePermissionScopeEntity.FieldResourceId + " = '" + resourceId + "'";
            }
            if (!String.IsNullOrEmpty(resourceCategory))
            {
                sqlQuery += " AND " + BasePermissionScopeEntity.FieldResourceCategory + " = '" + resourceCategory + "'";
            }
            if (!String.IsNullOrEmpty(targetCategory))
            {
                sqlQuery += " AND " + BasePermissionScopeEntity.FieldTargetCategory + " = '" + targetCategory + "'";
            }
            sqlQuery += " ORDER BY " + BasePermissionScopeEntity.FieldCreateOn + " DESC ";
            return DbHelper.Fill(sqlQuery);

            //for (int i = 0; i < result.Rows.Count; i++)
            //{
            //    if (!string.IsNullOrEmpty(result.Rows[i][BasePermissionScopeEntity.FieldEndDate].ToString()))
            //    {
            //        // 过期的不显示
            //        if (DateTime.Parse(result.Rows[i][BasePermissionScopeEntity.FieldEndDate].ToString()).Date < DateTime.Now.Date)
            //        {
            //            result.Rows.RemoveAt(i);
            //        }
            //    }
            //}
        }

        #region public DataTable GetAuthoriedList(string resourceCategory, string result, string targetCategory, string targetId) 获得有效的委托列表
        public DataTable GetAuthoriedList(string resourceCategory, string permissionId, string targetCategory, string targetId)
        {
            string sqlQuery = string.Empty;
            sqlQuery = "SELECT * FROM " + this.CurrentTableName
                            + " WHERE " + BasePermissionScopeEntity.FieldDeletionStateCode + " =0 "
                            + " AND " + BasePermissionScopeEntity.FieldEnabled + " =1 ";
            if (!String.IsNullOrEmpty(resourceCategory))
            {
                sqlQuery += " AND " + BasePermissionScopeEntity.FieldResourceCategory + " = '" + resourceCategory + "'";
            }
            if (!String.IsNullOrEmpty(permissionId))
            {
                sqlQuery += " AND " + BasePermissionScopeEntity.FieldPermissionId + " = '" + permissionId + "'";
            }
            if (!String.IsNullOrEmpty(targetCategory))
            {
                sqlQuery += " AND " + BasePermissionScopeEntity.FieldTargetCategory + " = '" + targetCategory + "'";
            }
            if (!String.IsNullOrEmpty(targetId))
            {
                sqlQuery += " AND " + BasePermissionScopeEntity.FieldTargetId + " = '" + targetId + "'";
            }
            // 时间在startDate或者endDate之间为有效
            if (BaseSystemInfo.UserCenterDbType.Equals(CurrentDbType.SqlServer))
            {
                sqlQuery += " AND ((SELECT DATEDIFF(day, " + BasePermissionScopeEntity.FieldStartDate + ", GETDATE()))>=0"
                         + " OR (SELECT DATEDIFF(day, " + BasePermissionScopeEntity.FieldStartDate + ", GETDATE())) IS NULL)";
                sqlQuery += " AND ((SELECT DATEDIFF(day, " + BasePermissionScopeEntity.FieldEndDate + ", GETDATE()))<=0"
                         + " OR (SELECT DATEDIFF(day, " + BasePermissionScopeEntity.FieldEndDate + ", GETDATE())) IS NULL)";
            }
            // TODO:其他数据库的兼容
            sqlQuery += " ORDER BY " + BasePermissionScopeEntity.FieldCreateOn + " DESC ";
            return DbHelper.Fill(sqlQuery);
        }
        #endregion
    }
}