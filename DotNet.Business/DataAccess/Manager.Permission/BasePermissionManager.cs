//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd.  
//-----------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace DotNet.Business
{
    using DotNet.Model;
    using DotNet.Utilities;

    /// <summary>
    /// BasePermissionManager
    /// 资源权限管理，操作权限管理（这里实现了用户操作权限，角色的操作权限）
    /// 
    /// 修改记录
    ///
    ///     2015.07.10 版本：2.1 JiRiGaLa 把删除标志补上来。
    ///     2010.09.21 版本：2.0 JiRiGaLa 智能权限判断、后台自动增加权限，增加并发锁PermissionLock。
    ///     2009.09.22 版本：1.1 JiRiGaLa 前台判断的权限，后台都需要记录起来，防止后台缺失前台的判断权限。
    ///     2008.03.28 版本：1.0 JiRiGaLa 创建主键。
    ///     
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2015.07.10</date>
    /// </author>
    /// </summary>
    public partial class BasePermissionManager : BaseManager, IBaseManager
    {
        #region public bool PermissionExists(string permissionId, string resourceCategory, string resourceId) 检查是否存在
        /// <summary>
        /// 检查是否存在
        /// </summary>
        /// <param name="permissionId">权限主键</param>
        /// <param name="resourceCategory">资源分类</param>
        /// <param name="resourceId">资源主键</param>
        /// <returns>是否存在</returns>      
        public bool PermissionExists(string permissionId, string resourceCategory, string resourceId)
        {
            bool result = false;

            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>(BasePermissionEntity.FieldResourceId, resourceId));
            parameters.Add(new KeyValuePair<string, object>(BasePermissionEntity.FieldResourceCategory, resourceCategory));
            parameters.Add(new KeyValuePair<string, object>(BasePermissionEntity.FieldPermissionId, permissionId));
            // 检查是否存在
            result = this.Exists(parameters);

            return result;
        }
        #endregion

        #region public string AddPermission(BasePermissionEntity permissionEntity) 添加
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="permissionEntity">实体</param>
        /// <returns>主键</returns>
        public string AddPermission(BasePermissionEntity permissionEntity)
        {
            string result = string.Empty;
            // 检查记录是否重复
            if (!this.PermissionExists(permissionEntity.PermissionId.ToString(), permissionEntity.ResourceCategory, permissionEntity.ResourceId))
            {
                result = this.AddObject(permissionEntity);
            }
            return result;
        }
        #endregion


        //
        // ResourcePermission 权限判断
        // 

        #region public bool IsAuthorized(string systemCode, string userId, string permissionCode, string permissionName = null, bool ignoreAdministrator = false, bool useBaseRole = true) 是否有相应的权限
        /// <summary>
        /// 是否有相应的权限
        /// </summary>
        /// <param name="systemCode">系统编号</param>
        /// <param name="userId">用户主键</param>
        /// <param name="permissionCode">权限编号</param>
        /// <param name="permissionName">权限名称</param>
        /// <param name="ignoreAdministrator">忽略管理员默认权限</param>
        /// <param name="useBaseRole">通用基础角色的权限是否计算</param>
        /// <returns>是否有权限</returns>
        public bool IsAuthorized(string systemCode, string userId, string permissionCode, string permissionName = null, bool ignoreAdministrator = false, bool useBaseRole = true)
        {
            bool result = false;

            // 忽略超级管理员，这里判断拒绝权限用的，因为超级管理员也被拒绝了，若所有的权限都有了
            if (!ignoreAdministrator)
            {
                // 先判断用户类别
                if (UserInfo != null && UserInfo.IsAdministrator)
                {
                    return true;
                }
            }

            // string permissionId = moduleManager.GetIdByAdd(permissionCode, permissionName);
            // 没有找到相应的权限
            // if (String.IsNullOrEmpty(permissionId))
            //{
            //    return false;
            //}

            BaseModuleEntity permissionEntity = BaseModuleManager.GetObjectByCacheByCode(systemCode, permissionCode);
            // 没有找到这个权限
            if (permissionEntity == null)
            {
                return false;
            }
            // 若是公开的权限，就不用进行判断了
            if (permissionEntity.IsPublic == 1)
            {
                return true;
            }
            if (permissionEntity.Enabled == 0)
            {
                return false;
            }
            if (!ignoreAdministrator)
            {
                // 这里需要判断,是系统权限？（系统管理员？）
                /*
                BaseUserManager userManager = new BaseUserManager(this.DbHelper, this.UserInfo);
                if (!string.IsNullOrEmpty(permissionEntity.CategoryCode) && permissionEntity.CategoryCode.Equals("System"))
                {
                    // 用户管理员
                    result = userManager.IsInRoleByCode(userId, "System");
                    if (result)
                    {
                        return result;
                    }
                }
                // 这里需要判断,是业务权限？(业务管理员？)
                if (!string.IsNullOrEmpty(permissionEntity.CategoryCode) && permissionEntity.CategoryCode.Equals("Application"))
                {
                    result = userManager.IsInRoleByCode(userId, "Application");
                    if (result)
                    {
                        return result;
                    }
                }
                */
            }

            // 判断用户权限
            if (this.CheckResourcePermission(systemCode, BaseUserEntity.TableName, userId, permissionEntity.Id))
            {
                return true;
            }

            // 判断用户角色权限
            if (this.CheckUserRolePermission(systemCode, userId, permissionEntity.Id, useBaseRole))
            {
                return true;
            }

            // 判断用户组织机构权限，这里有开关是为了提高性能用的，
            // 下面的函数接着还可以提高性能，可以进行一次判断就可以了，其实不用执行4次判断，浪费I/O，浪费性能。
            if (BaseSystemInfo.UseOrganizePermission)
            {
                // 2016-02-26 吉日嘎拉 进行简化权限判断，登录时应该选系统，选公司比较好，登录到哪个公司应该先确定？
                string companyId = BaseUserManager.GetCompanyIdByCache(userId);
                if (!string.IsNullOrEmpty(companyId))
                {
                    if (this.CheckResourcePermission(systemCode, BaseOrganizeEntity.TableName, companyId, permissionEntity.Id))
                    {
                        return true;
                    }
                }

                // 这里获取用户的所有所在的部门，包括兼职的部门
                /*
                BaseUserManager userManager = new BaseUserManager(this.DbHelper, this.UserInfo);
                string[] organizeIds = userManager.GetAllOrganizeIds(userId);
                if (organizeIds != null
                    && organizeIds.Length > 0
                    && this.CheckUserOrganizePermission(systemCode, userId, permissionEntity.Id, organizeIds))
                {
                    return true;
                }
                */
            }

            return false;
        }
        #endregion

        private bool CheckUserOrganizePermission(string systemCode, string userId, string permissionId, string[] organizeIds)
        {
            bool result = false;

            int errorMark = 0;
            if (string.IsNullOrEmpty(userId))
            {
                return false;
            }
            if (string.IsNullOrEmpty(permissionId))
            {
                return false;
            }
            if (organizeIds == null || organizeIds.Length == 0)
            {
                return false;
            }
            if (string.IsNullOrWhiteSpace(systemCode))
            {
                if (UserInfo != null && !string.IsNullOrWhiteSpace(UserInfo.SystemCode))
                {
                    systemCode = UserInfo.SystemCode;
                }
            }

            string tableName = systemCode + "Permission";
            string ids = "(" + string.Join(",", organizeIds) + ")";
            string sqlQuery = "SELECT COUNT(1) "
                             + "   FROM " + tableName
                             + "  WHERE " + BasePermissionEntity.FieldResourceCategory + " = " + DbHelper.GetParameter(BasePermissionEntity.FieldResourceCategory)
                             + "        AND (" + BasePermissionEntity.FieldResourceId + " IN " + ids + ") "
                             + "        AND " + BasePermissionEntity.FieldPermissionId + " = " + DbHelper.GetParameter(BasePermissionEntity.FieldPermissionId)
                             + "        AND " + BasePermissionEntity.FieldEnabled + " = " + DbHelper.GetParameter(BasePermissionEntity.FieldEnabled)
                             + "        AND " + BasePermissionEntity.FieldDeletionStateCode + " = " + DbHelper.GetParameter(BasePermissionEntity.FieldDeletionStateCode);

            int rowCount = 0;
            List<IDbDataParameter> dbParameters = new List<IDbDataParameter>();
            dbParameters.Add(DbHelper.MakeParameter(BasePermissionEntity.FieldResourceCategory, BaseOrganizeEntity.TableName));
            dbParameters.Add(DbHelper.MakeParameter(BasePermissionEntity.FieldPermissionId, permissionId));
            dbParameters.Add(DbHelper.MakeParameter(BasePermissionEntity.FieldEnabled, 1));
            dbParameters.Add(DbHelper.MakeParameter(BasePermissionEntity.FieldDeletionStateCode, 0));

            try
            {
                errorMark = 1;
                object returnObject = DbHelper.ExecuteScalar(sqlQuery, dbParameters.ToArray());

                if (returnObject != null)
                {
                    rowCount = int.Parse(returnObject.ToString());
                }
                result = rowCount > 0;
            }
            catch (System.Exception ex)
            {
                string writeMessage = "BasePermissionManager.CheckUserOrganizePermission:发生时间:" + DateTime.Now
                    + System.Environment.NewLine + "errorMark = " + errorMark.ToString()
                    + System.Environment.NewLine + "Message:" + ex.Message
                    + System.Environment.NewLine + "Source:" + ex.Source
                    + System.Environment.NewLine + "StackTrace:" + ex.StackTrace
                    + System.Environment.NewLine + "TargetSite:" + ex.TargetSite
                    + System.Environment.NewLine;

                FileUtil.WriteMessage(writeMessage, BaseSystemInfo.StartupPath + "//Exception//Exception" + DateTime.Now.ToString(BaseSystemInfo.DateFormat) + ".txt");
            }

            return result;
        }

        #region public bool CheckPermissionByUser(string systemCode, string userId, string permissionCode)
        /// <summary>
        /// 直接看用户本身是否有这个权限（不管角色是否有权限）
        /// </summary>
        /// <param name="systemCode">系统</param>
        /// <param name="userId">用户主键</param>
        /// <param name="permissionCode">权限主键</param>
        /// <returns>是否有权限</returns>
        public bool CheckPermissionByUser(string systemCode, string userId, string permissionCode)
        {
            string permissionId = BaseModuleManager.GetIdByCodeByCache(systemCode, permissionCode);
            // 没有找到相应的权限
            if (String.IsNullOrEmpty(permissionId))
            {
                return false;
            }
            return CheckResourcePermission(systemCode, BaseUserEntity.TableName, userId, permissionId);
        }
        #endregion

        private bool CheckResourcePermission(string systemCode, string resourceCategory, string resourceId, string permissionId)
        {
            bool result = false;

            int errorMark = 0;

            /*
            string tableName = systemCode + "Permission";
            string sqlQuery = "SELECT COUNT(1) "
                             + "   FROM " + tableName
                             + "  WHERE " + BasePermissionEntity.FieldResourceCategory + " = " + DbHelper.GetParameter(BasePermissionEntity.FieldResourceCategory)
                             + "        AND " + BasePermissionEntity.FieldResourceId + " = " + DbHelper.GetParameter(BasePermissionEntity.FieldResourceId)
                             + "        AND " + BasePermissionEntity.FieldPermissionId + " = " + DbHelper.GetParameter(BasePermissionEntity.FieldPermissionId)
                             + "        AND " + BasePermissionEntity.FieldEnabled + " = " + DbHelper.GetParameter(BasePermissionEntity.FieldEnabled)
                             + "        AND " + BasePermissionEntity.FieldDeletionStateCode + " = " + DbHelper.GetParameter(BasePermissionEntity.FieldDeletionStateCode);

            // 2016-02-26 吉日嘎拉 若是 mysql 等数据库，可以限制只获取一条就可以了，这个语句有优化的潜力，不需要获取所有，只要存在就可以了，判断是否存在就可以了

            int rowCount = 0;
            List<IDbDataParameter> dbParameters = new List<IDbDataParameter>();
            dbParameters.Add(DbHelper.MakeParameter(BasePermissionEntity.FieldResourceCategory, resourceCategory));
            dbParameters.Add(DbHelper.MakeParameter(BasePermissionEntity.FieldResourceId, resourceId));
            dbParameters.Add(DbHelper.MakeParameter(BasePermissionEntity.FieldPermissionId, permissionId));
            dbParameters.Add(DbHelper.MakeParameter(BasePermissionEntity.FieldEnabled, 1));
            dbParameters.Add(DbHelper.MakeParameter(BasePermissionEntity.FieldDeletionStateCode, 0));
            errorMark = 1;
            */

            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>(BasePermissionEntity.FieldResourceCategory, resourceCategory));
            parameters.Add(new KeyValuePair<string, object>(BasePermissionEntity.FieldResourceId, resourceId));
            parameters.Add(new KeyValuePair<string, object>(BasePermissionEntity.FieldPermissionId, permissionId));
            parameters.Add(new KeyValuePair<string, object>(BasePermissionEntity.FieldEnabled, 1));
            parameters.Add(new KeyValuePair<string, object>(BasePermissionEntity.FieldDeletionStateCode, 0));

            try
            {
                // 2016-02-27 吉日嘎拉 提高数据库查询效率，不需要全表扫描，提高判断权限的效率
                this.CurrentTableName = systemCode + "Permission";
                string id = this.GetProperty(parameters, BasePermissionEntity.FieldId);
                result = !string.IsNullOrEmpty(id);

                //object returnObject = this.DbHelper.ExecuteScalar(sqlQuery, dbParameters.ToArray());
                //if (returnObject != null && returnObject != DBNull.Value)
                //{
                //    rowCount = int.Parse(returnObject.ToString());
                //}
                //result = rowCount > 0;
            }
            catch (System.Exception ex)
            {
                string writeMessage = "BasePermissionManager.CheckResourcePermission:发生时间:" + DateTime.Now
                    + System.Environment.NewLine + "errorMark = " + errorMark.ToString()
                    + System.Environment.NewLine + "Message:" + ex.Message
                    + System.Environment.NewLine + "Source:" + ex.Source
                    + System.Environment.NewLine + "StackTrace:" + ex.StackTrace
                    + System.Environment.NewLine + "TargetSite:" + ex.TargetSite
                    + System.Environment.NewLine;

                FileUtil.WriteMessage(writeMessage, BaseSystemInfo.StartupPath + "//Exception//Exception" + DateTime.Now.ToString(BaseSystemInfo.DateFormat) + ".txt");
            }

            return result;
        }

        #region private bool CheckUserRolePermission(string systemCode, string userId, string permissionId, bool useBaseRole = true)
        /// <summary>
        /// 用户角色关系是否有模块权限
        /// 2015-11-29 吉日嘎拉 进行参数化改进。
        /// 2016-02-26 吉日嘎拉 优化索引的顺序。
        /// </summary>
        /// <param name="systemCode">数据库连接</param>
        /// <param name="userId">用户主键</param>
        /// <param name="permissionId">权限主键</param>
        /// <param name="useBaseRole">基础角色是否包含</param>
        /// <returns>有角色权限</returns>
        private bool CheckUserRolePermission(string systemCode, string userId, string permissionId, bool useBaseRole = true)
        {
            bool result = false;

            int errorMark = 0;
            string permissionTableName = "BasePermission";
            string userRoleTableName = "BaseUserRole";
            string roleTableName = "BaseRole";

            if (string.IsNullOrWhiteSpace(systemCode))
            {
                if (UserInfo != null && !string.IsNullOrWhiteSpace(UserInfo.SystemCode))
                {
                    systemCode = UserInfo.SystemCode;
                }
            }

            permissionTableName = systemCode + "Permission";
            userRoleTableName = systemCode + "UserRole";
            roleTableName = systemCode + "Role";

            List<IDbDataParameter> dbParameters = new List<IDbDataParameter>();

            string sqlQuery = "SELECT COUNT(1) "
                            + "   FROM " + permissionTableName
                            + "  WHERE " + BasePermissionEntity.FieldResourceCategory + " = '" + roleTableName + "'"
                            + "        AND " + BasePermissionEntity.FieldResourceId + " IN ( "
                                                + " SELECT " + BaseUserRoleEntity.FieldRoleId
                                                + "   FROM " + userRoleTableName
                                                + "  WHERE " + BaseUserRoleEntity.FieldUserId + " = " + DbHelper.GetParameter(userRoleTableName + "_" + BaseUserRoleEntity.FieldUserId)
                                                + "        AND " + BaseUserRoleEntity.FieldEnabled + " = 1 "
                                                + "        AND " + BaseUserRoleEntity.FieldDeletionStateCode + " = 0 ";

            dbParameters.Add(DbHelper.MakeParameter(userRoleTableName + "_" + BaseUserRoleEntity.FieldUserId, userId));
            if (useBaseRole && !systemCode.Equals("Base", StringComparison.OrdinalIgnoreCase))
            {
                sqlQuery += " UNION SELECT " + BaseUserRoleEntity.FieldRoleId
                                + "   FROM " + BaseUserRoleEntity.TableName
                                + "  WHERE " + BaseUserRoleEntity.FieldUserId + " = " + DbHelper.GetParameter(BaseUserRoleEntity.TableName + "_" + BaseUserRoleEntity.FieldUserId)
                                + "        AND " + BaseUserRoleEntity.FieldEnabled + " = 1 "
                                + "        AND " + BaseUserRoleEntity.FieldDeletionStateCode + " = 0 ";

                dbParameters.Add(DbHelper.MakeParameter(BaseUserRoleEntity.TableName + "_" + BaseUserRoleEntity.FieldUserId, userId));
            }
            sqlQuery += " ) "
                + "        AND " + BasePermissionEntity.FieldPermissionId + " = " + DbHelper.GetParameter(BasePermissionEntity.FieldPermissionId)
                + "        AND " + BasePermissionEntity.FieldEnabled + " = 1 "
                + "        AND " + BasePermissionEntity.FieldDeletionStateCode + " = 0 ";
            dbParameters.Add(DbHelper.MakeParameter(BasePermissionEntity.FieldPermissionId, permissionId));

            int rowCount = 0;

            // FileUtil.WriteMessage("sqlQuery:" + sqlQuery, System.Web.HttpContext.Current.Server.MapPath("~/Log/") + "Log" + DateTime.Now.ToString(BaseSystemInfo.DateFormat) + ".txt");

            try
            {
                errorMark = 1;
                object returnObject = this.DbHelper.ExecuteScalar(sqlQuery, dbParameters.ToArray());
                if (returnObject != null)
                {
                    rowCount = int.Parse(returnObject.ToString());
                }
                result = rowCount > 0;
            }
            catch (System.Exception ex)
            {
                string writeMessage = "BasePermissionManager.CheckUserRolePermission:发生时间:" + DateTime.Now
                    + System.Environment.NewLine + "errorMark = " + errorMark.ToString()
                    + System.Environment.NewLine + "Message:" + ex.Message
                    + System.Environment.NewLine + "Source:" + ex.Source
                    + System.Environment.NewLine + "StackTrace:" + ex.StackTrace
                    + System.Environment.NewLine + "TargetSite:" + ex.TargetSite
                    + System.Environment.NewLine + "sql:" + sqlQuery
                    + System.Environment.NewLine + "参数:" + JsonConvert.SerializeObject(dbParameters)
                    + System.Environment.NewLine;

                FileUtil.WriteMessage(writeMessage, BaseSystemInfo.StartupPath + "//Exception//Exception" + DateTime.Now.ToString(BaseSystemInfo.DateFormat) + ".txt");
            }

            return result;
        }
        #endregion

        //
        // 从数据库获取权限
        //

        public List<BaseModuleEntity> GetPermissionListByUser(string systemCode, string userId, string companyId = null, bool fromCache = false)
        {
            List<BaseModuleEntity> result = new List<BaseModuleEntity>();

            bool useBaseRole = false;

            string key = "BaseModule";
            string tableName = "BaseModule";
            if (!string.IsNullOrWhiteSpace(systemCode))
            {
                key = systemCode + "Module";
                tableName = systemCode + "Module";

                // 2015-11-19 所有的系统都继承基础角色的权限
                useBaseRole = true;
                // 2015-01-21 吉日嘎拉，实现判断别人的权限，是否超级管理员
                bool isAdministrator = false;

                if (UserInfo != null && UserInfo.IsAdministrator)
                {
                    if (UserInfo.Id.Equals(userId, StringComparison.CurrentCulture))
                    {
                        isAdministrator = true;
                    }
                    else
                    {
                        BaseUserManager userManager = new BaseUserManager(UserInfo);
                        isAdministrator = userManager.IsAdministrator(userId);
                    }
                }
                if (isAdministrator)
                {
                    result = BaseModuleManager.GetEntitiesByCache(systemCode);
                }
                else
                {
                    string[] permissionIds = null;
                    // 2016-02-26 吉日嘎拉进行优化，用缓存与不用缓存感觉区别不是很大。
                    if (fromCache)
                    {
                        // permissionIds = GetPermissionIdsByUserByCache(systemCode, userId, companyId, useBaseRole);
                        permissionIds = GetPermissionIdsByUser(systemCode, userId, companyId, false, useBaseRole);
                    }
                    else
                    {
                        permissionIds = GetPermissionIdsByUser(systemCode, userId, companyId, false, useBaseRole);
                    }

                    // 2016-03-02 吉日嘎拉，少读一次缓存服务器，减少缓存服务器读写压力
                    List<BaseModuleEntity> entities = BaseModuleManager.GetEntitiesByCache(systemCode);
                    // 若是以前赋予的权限，后来有些权限设置为无效了，那就不应该再获取哪些无效的权限才对。
                    if (permissionIds != null && permissionIds.Length > 0)
                    {   
                        result = (entities as List<BaseModuleEntity>).Where(t => (t.IsPublic == 1 && t.Enabled == 1 && t.DeletionStateCode == 0) || permissionIds.Contains(t.Id)).ToList();
                    }
                    else
                    {
                        result = (entities as List<BaseModuleEntity>).Where(t => t.IsPublic == 1 && t.Enabled == 1 && t.DeletionStateCode == 0).ToList();
                    }
                }
            }

            return result;
        }

        #region public string[] GetPermissionIds(BaseUserInfo userInfo)
        /// <summary>
        /// 获得一个员工的某一模块的权限
        /// </summary>
        /// <param name="dbHelper">数据库连接</param>
        /// <param name=result>用户</param>
        /// <returns>数据表</returns>
        public string[] GetPermissionIds(BaseUserInfo userInfo)
        {
            return this.GetPermissionIdsByUser(userInfo.Id, userInfo.CompanyId);
        }
        #endregion

        /// <summary>
        /// 获取用户的权限主键数组
        /// </summary>
        /// <param name="systemCode">系统编号</param>
        /// <param name="userId">用户主键</param>
        /// <param name="companyId">公司主键</param>
        /// <param name="containPublic">公开的也包含</param>
        /// <param name="useBaseRole">使用基础角色权限</param>
        /// <returns>拥有权限数组</returns>
        public string[] GetPermissionIdsByUser(string systemCode, string userId, string companyId = null, bool containPublic = true, bool useBaseRole = false)
        {
            // 公开的操作权限需要计算
            string[] result = null;

            int errorMark = 0;
            string tableName = BaseModuleEntity.TableName;
            if (string.IsNullOrWhiteSpace(systemCode))
            {
                systemCode = "Base";
            }
            // 就不需要参合基础的角色了
            if (systemCode.Equals("Base"))
            {
                useBaseRole = false;
            }
            tableName = systemCode + "Module";

            try
            {
                errorMark = 1;

                if (containPublic)
                {
                    // 把公开的部分获取出来（把公开的主键数组从缓存里获取出来，减少数据库的读取次数）
                    List<BaseModuleEntity> moduleEntities = BaseModuleManager.GetEntitiesByCache(systemCode);
                    if (moduleEntities != null)
                    {
                        result = moduleEntities.Where((t => t.IsPublic == 1 && t.Enabled == 1 && t.DeletionStateCode == 0)).Select(t => t.Id.ToString()).ToArray();
                    }
                }

                tableName = systemCode + "UserRole";
                string roleTableName = systemCode + "Role";
                this.CurrentTableName = systemCode + "Permission";
                List<IDbDataParameter> dbParameters = new List<IDbDataParameter>();

                StringBuilder sqlQuery = new StringBuilder();
                // 用户的操作权限
                sqlQuery.Append(" SELECT " + BasePermissionEntity.FieldPermissionId);
                sqlQuery.Append("   FROM " + this.CurrentTableName);
                sqlQuery.Append("  WHERE (" + BasePermissionEntity.FieldResourceCategory + " = " + DbHelper.GetParameter(BaseUserEntity.TableName + "_" + BasePermissionEntity.FieldResourceCategory));
                sqlQuery.Append("        AND " + BasePermissionEntity.FieldResourceId + " = " + DbHelper.GetParameter(BaseUserEntity.TableName + "_" + BaseUserEntity.FieldId));
                sqlQuery.Append("        AND " + BasePermissionEntity.FieldEnabled + " = 1 ");
                sqlQuery.Append("        AND " + BasePermissionEntity.FieldDeletionStateCode + " = 0)");

                dbParameters.Add(DbHelper.MakeParameter(BaseUserEntity.TableName + "_" + BasePermissionEntity.FieldResourceCategory, BaseUserEntity.TableName));
                dbParameters.Add(DbHelper.MakeParameter(BaseUserEntity.TableName + "_" + BaseUserEntity.FieldId, userId));

                // 角色的操作权限                            
                sqlQuery.Append("  UNION ");

                sqlQuery.Append(" SELECT " + BasePermissionEntity.FieldPermissionId);
                sqlQuery.Append("   FROM " + this.CurrentTableName);
                sqlQuery.Append("        , ( SELECT " + BaseUserRoleEntity.FieldRoleId);
                sqlQuery.Append("   FROM " + tableName);
                sqlQuery.Append("  WHERE (" + BaseUserRoleEntity.FieldUserId + " = " + DbHelper.GetParameter(BaseUserRoleEntity.TableName + "_" + BaseUserRoleEntity.FieldUserId));
                sqlQuery.Append("        AND " + BaseUserRoleEntity.FieldEnabled + " = 1 ");
                sqlQuery.Append("        AND " + BaseUserRoleEntity.FieldDeletionStateCode + " = 0 ) ");

                dbParameters.Add(DbHelper.MakeParameter(BaseUserRoleEntity.TableName + "_" + BaseUserRoleEntity.FieldUserId, userId));

                // 2015-12-02 吉日嘎拉 简化SQL语句，提高效率
                if (useBaseRole && !systemCode.Equals("Base", StringComparison.OrdinalIgnoreCase))
                {
                    // 是否使用基础角色的权限 
                    sqlQuery.Append(" UNION SELECT " + BaseUserRoleEntity.FieldRoleId);
                    sqlQuery.Append("   FROM " + BaseUserRoleEntity.TableName);
                    sqlQuery.Append("  WHERE ( " + BaseUserRoleEntity.FieldUserId + " = " + DbHelper.GetParameter(BaseUserRoleEntity.TableName + "_USEBASE_" + BaseUserRoleEntity.FieldUserId));
                    sqlQuery.Append("        AND " + BaseUserRoleEntity.FieldEnabled + " = 1 ");
                    sqlQuery.Append("        AND " + BaseUserRoleEntity.FieldDeletionStateCode + " = 0 ) ");

                    dbParameters.Add(DbHelper.MakeParameter(BaseUserRoleEntity.TableName + "_USEBASE_" + BaseUserRoleEntity.FieldUserId, userId));
                }

                /*
                // 角色与部门是否进行关联？
                // 2015-12-02 吉日嘎拉 这里基本上没在用的，心里有个数。
                if (BaseSystemInfo.UseRoleOrganize && !string.IsNullOrEmpty(companyId))
                {
                    string roleOrganizeTableName = systemCode + "RoleOrganize";
                    sqlQuery.Append(" UNION SELECT " + BaseRoleOrganizeEntity.FieldRoleId);
                    sqlQuery.Append("   FROM " + roleOrganizeTableName);
                    sqlQuery.Append("  WHERE ( " + BaseRoleOrganizeEntity.FieldOrganizeId + " = " + DbHelper.GetParameter(BaseRoleOrganizeEntity.FieldOrganizeId));
                    sqlQuery.Append("        AND " + BaseRoleOrganizeEntity.FieldEnabled + " = 1 ");
                    sqlQuery.Append("        AND " + BaseRoleOrganizeEntity.FieldDeletionStateCode + " = 0 )");
                    dbParameters.Add(DbHelper.MakeParameter(BaseRoleOrganizeEntity.FieldOrganizeId, companyId));
                }
                */

                sqlQuery.Append(") B ");
                sqlQuery.Append("   WHERE " + BasePermissionEntity.FieldResourceCategory + " = " + DbHelper.GetParameter(BaseRoleEntity.TableName + "_" + BasePermissionEntity.FieldResourceCategory));
                sqlQuery.Append("        AND " + this.CurrentTableName + "." + BasePermissionEntity.FieldResourceId + " = B." + BaseUserRoleEntity.FieldRoleId);
                sqlQuery.Append("        AND " + this.CurrentTableName + "." + BasePermissionEntity.FieldEnabled + " = 1 ");
                sqlQuery.Append("        AND " + this.CurrentTableName + "." + BasePermissionEntity.FieldDeletionStateCode + " = 0 ");

                dbParameters.Add(DbHelper.MakeParameter(BaseRoleEntity.TableName + "_" + BasePermissionEntity.FieldResourceCategory, roleTableName));

                List<string> ids = new List<string>();
                errorMark = 3;
                using (IDataReader dataReader = DbHelper.ExecuteReader(sqlQuery.ToString(), dbParameters.ToArray()))
                {
                    while (dataReader.Read())
                    {
                        ids.Add(dataReader[BasePermissionEntity.FieldPermissionId].ToString());
                    }
                }

                // string[] userRolePermissionIds = ids.ToArray();
                result = StringUtil.Concat(result, ids.ToArray());

                // 按部门(组织机构)获取权限项
                if (BaseSystemInfo.UseOrganizePermission)
                {
                    if (!string.IsNullOrEmpty(companyId))
                    {
                        sqlQuery = new StringBuilder();
                        sqlQuery.Append(" SELECT " + BasePermissionEntity.FieldPermissionId);
                        sqlQuery.Append("   FROM " + this.CurrentTableName);
                        sqlQuery.Append("  WHERE " + BasePermissionEntity.FieldResourceCategory + " = " + DbHelper.GetParameter(BasePermissionEntity.FieldResourceCategory));
                        sqlQuery.Append("        AND " + BasePermissionEntity.FieldPermissionId + " = " + DbHelper.GetParameter(BasePermissionEntity.FieldPermissionId));
                        sqlQuery.Append("        AND " + BasePermissionEntity.FieldEnabled + " = " + DbHelper.GetParameter(BasePermissionEntity.FieldEnabled));
                        sqlQuery.Append("        AND " + BasePermissionEntity.FieldDeletionStateCode + " = " + DbHelper.GetParameter(BasePermissionEntity.FieldDeletionStateCode));
                        // dt = DbHelper.Fill(sqlQuery);
                        // string[] organizePermission = BaseBusinessLogic.FieldToArray(dt, BasePermissionEntity.FieldPermissionId).Distinct<string>().Where(t => !string.IsNullOrEmpty(t)).ToArray();
                        // 2015-12-02 吉日嘎拉 优化参数，用ExecuteReader，提高效率节约内存。
                        dbParameters = new List<IDbDataParameter>();
                        dbParameters.Add(DbHelper.MakeParameter(BasePermissionEntity.FieldResourceCategory, BaseOrganizeEntity.TableName));
                        dbParameters.Add(DbHelper.MakeParameter(BasePermissionEntity.FieldPermissionId, companyId));
                        dbParameters.Add(DbHelper.MakeParameter(BasePermissionEntity.FieldEnabled, 1));
                        dbParameters.Add(DbHelper.MakeParameter(BasePermissionEntity.FieldDeletionStateCode, 0));
                        ids = new List<string>();
                        errorMark = 4;
                        using (IDataReader dataReader = DbHelper.ExecuteReader(sqlQuery.ToString(), dbParameters.ToArray()))
                        {
                            while (dataReader.Read())
                            {
                                ids.Add(dataReader[BasePermissionEntity.FieldPermissionId].ToString());
                            }
                        }
                        // string[] organizePermission = ids.ToArray();
                        result = StringUtil.Concat(result, ids.ToArray());
                    }
                }
            }
            catch (System.Exception ex)
            {
                string writeMessage = "BasePermissionManager.GetPermissionIdsByUser:发生时间:" + DateTime.Now
                    + System.Environment.NewLine + "errorMark = " + errorMark.ToString()
                    + System.Environment.NewLine + "Message:" + ex.Message
                    + System.Environment.NewLine + "Source:" + ex.Source
                    + System.Environment.NewLine + "StackTrace:" + ex.StackTrace
                    + System.Environment.NewLine + "TargetSite:" + ex.TargetSite
                    + System.Environment.NewLine;

                FileUtil.WriteMessage(writeMessage, BaseSystemInfo.StartupPath + "//Exception//Exception" + DateTime.Now.ToString(BaseSystemInfo.DateFormat) + ".txt");
            }

            return result;
        }

        #region public bool CheckPermissionByRole(string systemCode, string roleId, string permissionCode)
        /// <summary>
        /// 用户角色关系是否有模块权限
        /// 2015-12-15 吉日嘎拉 优化参数化
        /// </summary>
        /// <param name="systemCode">系统编号</param>
        /// <param name="roleId">角色主键</param>
        /// <param name="permissionCode">权限编号</param>
        /// <returns>有角色权限</returns>
        public bool CheckPermissionByRole(string systemCode, string roleId, string permissionCode)
        {
            // 判断当前判断的权限是否存在，否则很容易出现前台设置了权限，后台没此项权限
            // 需要自动的能把前台判断过的权限，都记录到后台来
            string permissionId = string.Empty;
#if (DEBUG)
                if (String.IsNullOrEmpty(permissionId))
                {
                    BaseModuleEntity permissionEntity = new BaseModuleEntity();
                    permissionEntity.Code = permissionCode;
                    permissionEntity.FullName = permissionCode;
                    permissionEntity.IsScope = 0;
                    permissionEntity.IsPublic = 0;
                    permissionEntity.IsMenu = 0;
                    permissionEntity.IsVisible = 1;
                    permissionEntity.AllowDelete = 1;
                    permissionEntity.AllowEdit = 1;
                    permissionEntity.DeletionStateCode = 0;
                    permissionEntity.Enabled = 1;
                    // 这里是防止主键重复？
                    // permissionEntity.ID = BaseBusinessLogic.NewGuid();
                    BaseModuleManager moduleManager = new Business.BaseModuleManager();
                    moduleManager.AddObject(permissionEntity);
                }
                else
                {
                    // 更新最后一次访问日期，设置为当前服务器日期
                    SQLBuilder sqlBuilder = new SQLBuilder(DbHelper);
                    sqlBuilder.BeginUpdate(this.CurrentTableName);
                    sqlBuilder.SetDBNow(BaseModuleEntity.FieldLastCall);
                    sqlBuilder.SetWhere(BaseModuleEntity.FieldId, permissionId);
                    sqlBuilder.EndUpdate();
                }
#endif

            permissionId = BaseModuleManager.GetIdByCodeByCache(systemCode, permissionCode);
            // 没有找到相应的权限
            if (String.IsNullOrEmpty(permissionId))
            {
                return false;
            }

            string resourceCategory = systemCode + "Role";
            return CheckResourcePermission(systemCode, resourceCategory, roleId, permissionId);
        }
        #endregion
    }
}