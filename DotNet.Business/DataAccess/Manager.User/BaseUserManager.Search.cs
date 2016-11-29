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
    /// BaseUserManager
    /// 用户管理
    /// 
    /// 修改记录
    /// 
    ///		2014.03.21 版本：1.0 JiRiGaLa	主键整理。
    /// 
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2014.03.21</date>
    /// </author> 
    /// </summary>
    public partial class BaseUserManager : BaseManager
    {
        #region public DataTable GetDataTable() 获取列表
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns>数据表</returns>
        public DataTable GetDataTable()
        {
            string sqlQuery = "SELECT " + BaseUserEntity.TableName + ".* "
                            + "   FROM " + BaseUserEntity.TableName
                            + " WHERE " + BaseUserEntity.FieldDeletionStateCode + "= 0 "
                            + " AND " + BaseUserEntity.FieldIsVisible + "= 1 "
                //+ "       AND " + BaseUserEntity.FieldEnabled + "= 1 "  //如果Enabled = 1，只显示有效用户
                            + " ORDER BY " + BaseUserEntity.TableName + "." + BaseUserEntity.FieldSortCode;
            return DbHelper.Fill(sqlQuery);
        }
        #endregion

        private string GetSearchConditional(string systemCode, string permissionCode, string where, string[] roleIds, bool? enabled, string auditStates, string companyId = null, string departmentId = null, bool onlyOnLine = false)
        {
            string whereClause = BaseUserEntity.TableName + "." + BaseUserEntity.FieldDeletionStateCode + " = 0 "
                            + " AND " + BaseUserEntity.TableName + "." + BaseUserEntity.FieldIsVisible + " = 1 ";

            if (enabled.HasValue)
            {
                if (enabled == true)
                {
                    whereClause += " AND " + BaseUserEntity.TableName + "." + BaseUserEntity.FieldEnabled + " = 1 ";
                }
                else
                {
                    whereClause += " AND " + BaseUserEntity.TableName + "." + BaseUserEntity.FieldEnabled + " = 0 ";
                }
            }
            if (onlyOnLine)
            {
                whereClause += " AND " + BaseUserEntity.TableName + ".Id IN (SELECT Id FROM " + BaseUserLogOnEntity.TableName + " WHERE UserOnLine = 1) ";
            }
            if (!String.IsNullOrEmpty(where))
            {
                // 传递过来的表达式，还是搜索值？
                if (where.IndexOf("AND") < 0 && where.IndexOf("=") < 0)
                {
                    where = StringUtil.GetSearchString(where);
                    whereClause += " AND ("
                                + BaseUserEntity.TableName + "." + BaseUserEntity.FieldUserName + " LIKE '" + where + "'"
                                + " OR " + BaseUserEntity.TableName + "." + BaseUserEntity.FieldSimpleSpelling + " LIKE '" + where + "'"
                                + " OR " + BaseUserEntity.TableName + "." + BaseUserEntity.FieldCode + " LIKE '" + where + "'"
                                + " OR " + BaseUserEntity.TableName + "." + BaseUserEntity.FieldRealName + " LIKE '" + where + "'"
                                + " OR " + BaseUserEntity.TableName + "." + BaseUserEntity.FieldQuickQuery + " LIKE '" + where + "'"
                                + " OR " + BaseUserEntity.TableName + "." + BaseUserEntity.FieldCompanyName + " LIKE '" + where + "'"
                                + " OR " + BaseUserEntity.TableName + "." + BaseUserEntity.FieldDepartmentName + " LIKE '" + where + "'"
                        // + " OR " + BaseUserEntity.TableName + "." + BaseUserEntity.FieldDescription + " LIKE '" + search + "'"
                                + ")";
                }
                else
                {
                    whereClause += " AND (" + where + ")";
                }
            }
            if (!string.IsNullOrEmpty(departmentId))
            {
                /*
                BaseOrganizeManager organizeManager = new BaseOrganizeManager(this.DbHelper, this.UserInfo);
                string[] ids = organizeManager.GetChildrensId(BaseOrganizeEntity.FieldId, departmentId, BaseOrganizeEntity.FieldParentId);
                if (ids != null && ids.Length > 0)
                {
                    whereClause += " AND (" + BaseUserEntity.TableName + "." + BaseUserEntity.FieldCompanyId + " IN (" + StringUtil.ArrayToList(ids) + ")"
                     + " OR " + BaseUserEntity.TableName + "." + BaseUserEntity.FieldDepartmentId + " IN (" + StringUtil.ArrayToList(ids) + ")"
                     + " OR " + BaseUserEntity.TableName + "." + BaseUserEntity.FieldWorkgroupId + " IN (" + StringUtil.ArrayToList(ids) + "))";
                }
                */
                whereClause += " AND (" + BaseUserEntity.TableName + "." + BaseUserEntity.FieldDepartmentId + " = '" + departmentId + "')";
            }
            if (!string.IsNullOrEmpty(companyId))
            {
                whereClause += " AND (" + BaseUserEntity.TableName + "." + BaseUserEntity.FieldCompanyId + " = '" + companyId + "')";
            }
            if (!String.IsNullOrEmpty(auditStates))
            {
                whereClause += " AND (" + BaseUserEntity.TableName + "." + BaseUserEntity.FieldAuditStatus + " = '" + auditStates + "'";
                // 待审核
                if (auditStates.Equals(AuditStatus.WaitForAudit.ToString()))
                {
                    whereClause += " OR " + BaseUserEntity.TableName + ".Id IN ( SELECT Id FROM " + BaseUserLogOnEntity.TableName + " WHERE LockEndDate > " + dbHelper.GetDbNow() + ") ";
                }
                whereClause += ")";
            }
            if (enabled != null)
            {
                whereClause += " AND (" + BaseUserEntity.TableName + "." + BaseUserEntity.FieldEnabled + " = " + ((bool)enabled ? 1 : 0) + ")";
            }
            if ((roleIds != null) && (roleIds.Length > 0))
            {
                string roles = StringUtil.ArrayToList(roleIds, "'");
                whereClause += " AND (" + BaseUserEntity.TableName + "." + BaseUserEntity.FieldId + " IN (" + "SELECT " + BaseUserRoleEntity.FieldUserId + " FROM " + BaseUserRoleEntity.TableName + " WHERE " + BaseUserRoleEntity.FieldRoleId + " IN (" + roles + ")" + "))";
            }

            // 是否过滤用户， 获得组织机构列表， 这里需要一个按用户过滤得功能
            if (!string.IsNullOrEmpty(permissionCode) 
                && (!UserInfo.IsAdministrator) 
                && (BaseSystemInfo.UsePermissionScope))
            {
                // string permissionCode = "Resource.ManagePermission";
                string permissionId = BaseModuleManager.GetIdByCodeByCache(systemCode, permissionCode);
                if (!string.IsNullOrEmpty(permissionId))
                {
                    // 从小到大的顺序进行显示，防止错误发生
                    BaseUserScopeManager userPermissionScopeManager = new BaseUserScopeManager(this.DbHelper, this.UserInfo);
                    string[] organizeIds = userPermissionScopeManager.GetOrganizeIds(UserInfo.SystemCode, this.UserInfo.Id, permissionId);

                    // 没有任何数据权限
                    if (StringUtil.Exists(organizeIds, ((int)PermissionOrganizeScope.NotAllowed).ToString()))
                    {
                        whereClause += " AND (" + BaseUserEntity.TableName + "." + BaseUserEntity.FieldId + " = NULL ) ";
                    }
                    // 按详细设定的数据
                    if (StringUtil.Exists(organizeIds, ((int)PermissionOrganizeScope.ByDetails).ToString()))
                    {
                        BasePermissionScopeManager permissionScopeManager = new BasePermissionScopeManager(DbHelper, UserInfo);
                        string[] userIds = permissionScopeManager.GetUserIds(UserInfo.SystemCode, UserInfo.Id, permissionCode);
                        whereClause += " AND (" + BaseUserEntity.TableName + "." + BaseUserEntity.FieldId + " IN (" + string.Join(",", userIds) + ")) ";
                    }
                    // 自己的数据，仅本人
                    if (StringUtil.Exists(organizeIds, ((int)PermissionOrganizeScope.OnlyOwnData).ToString()))
                    {
                        whereClause += " AND (" + BaseUserEntity.TableName + "." + BaseUserEntity.FieldId + " = " + this.UserInfo.Id + ") ";
                    }
                    // 用户所在工作组数据
                    if (StringUtil.Exists(organizeIds, ((int)PermissionOrganizeScope.UserWorkgroup).ToString()))
                    {
                        // whereClause += " AND (" + BaseUserEntity.TableName + "." + BaseUserEntity.FieldWorkgroupId + " = " + this.UserInfo.WorkgroupId + ") ";
                    }
                    // 用户所在部门数据
                    if (StringUtil.Exists(organizeIds, ((int)PermissionOrganizeScope.UserDepartment).ToString()))
                    {
                        whereClause += " AND (" + BaseUserEntity.TableName + "." + BaseUserEntity.FieldDepartmentId + " = " + this.UserInfo.DepartmentId + ") ";
                    }
                    // 用户所在分支机构数据
                    if (StringUtil.Exists(organizeIds, ((int)PermissionOrganizeScope.UserSubCompany).ToString()))
                    {
                        // whereClause += " AND (" + BaseUserEntity.TableName + "." + BaseUserEntity.FieldSubCompanyId + " = '" + this.UserInfo.SubCompanyId + "') ";
                    }
                    // 用户所在公司数据
                    if (StringUtil.Exists(organizeIds, ((int)PermissionOrganizeScope.UserCompany).ToString()))
                    {
                        whereClause += " AND (" + BaseUserEntity.TableName + "." + BaseUserEntity.FieldCompanyId + " = '" + this.UserInfo.CompanyId + "') ";
                    }
                    // 全部数据，这里就不用设置过滤条件了
                    if (StringUtil.Exists(organizeIds, ((int)PermissionOrganizeScope.AllData).ToString()))
                    {
                    }
                }
            }

            return whereClause;
        }

        public DataTable Search(string systemCode, string permissionCode, string search, string[] roleIds, bool? enabled, string auditStates, string departmentId, string companyId = null)
        {
            string sqlQuery = "SELECT " + BaseUserEntity.TableName + ".* ";
            if (ShowUserLogOnInfo)
            {
                sqlQuery += "," + BaseUserLogOnEntity.TableName + "." + BaseUserLogOnEntity.FieldFirstVisit
                + "," + BaseUserLogOnEntity.TableName + "." + BaseUserLogOnEntity.FieldPreviousVisit
                + "," + BaseUserLogOnEntity.TableName + "." + BaseUserLogOnEntity.FieldLastVisit
                + "," + BaseUserLogOnEntity.TableName + "." + BaseUserLogOnEntity.FieldIPAddress
                + "," + BaseUserLogOnEntity.TableName + "." + BaseUserLogOnEntity.FieldMACAddress
                + "," + BaseUserLogOnEntity.TableName + "." + BaseUserLogOnEntity.FieldLogOnCount
                + "," + BaseUserLogOnEntity.TableName + "." + BaseUserLogOnEntity.FieldUserOnLine
                + "," + BaseUserLogOnEntity.TableName + "." + BaseUserLogOnEntity.FieldCheckIPAddress
                + "," + BaseUserLogOnEntity.TableName + "." + BaseUserLogOnEntity.FieldMultiUserLogin
                + " FROM " + BaseUserEntity.TableName + " LEFT OUTER JOIN " + BaseUserLogOnEntity.TableName + " ON " + BaseUserEntity.TableName + ".Id = " + BaseUserLogOnEntity.TableName + ".Id ";
            }
            else
            {
                sqlQuery += "," + BaseUserContactEntity.TableName + "." + BaseUserContactEntity.FieldEmail
                + "," + BaseUserContactEntity.TableName + "." + BaseUserContactEntity.FieldMobile
                + "," + BaseUserContactEntity.TableName + "." + BaseUserContactEntity.FieldQQ
                + "," + BaseUserContactEntity.TableName + "." + BaseUserContactEntity.FieldTelephone
                + " FROM " + BaseUserEntity.TableName + " LEFT OUTER JOIN BaseUserContact ON " + BaseUserEntity.TableName + ".Id = BaseUserContact.Id ";
            }
            string whereClause = GetSearchConditional(systemCode, permissionCode, search, roleIds, enabled, auditStates, companyId, departmentId);
            sqlQuery += " WHERE " + whereClause;
            sqlQuery += " ORDER BY " + BaseUserEntity.TableName + "." + BaseUserEntity.FieldSortCode;
            return DbHelper.Fill(sqlQuery);
        }

        public DataTable SearchByPage(string systemCode, string permissionCode, string whereClause, string[] roleIds, bool? enabled, string auditStates, string companyId, string departmentId, bool showRole, bool userAllInformation, bool onlyOnLine, out int recordCount, int pageIndex = 0, int pageSize = 20, string order = null)
        {
            whereClause = GetSearchConditional(systemCode, permissionCode, whereClause, roleIds, enabled, auditStates, companyId, departmentId, onlyOnLine);

            switch (DbHelper.CurrentDbType)
            {
                case CurrentDbType.SqlServer:
                case CurrentDbType.Access:
                    this.CurrentTableName = BaseUserEntity.TableName + " LEFT OUTER JOIN BaseUserContact ON " + BaseUserEntity.TableName + ".Id = BaseUserContact.Id";
                    if (this.ShowUserLogOnInfo)
                    {
                        this.CurrentTableName += " LEFT OUTER JOIN " + BaseUserLogOnEntity.TableName + " ON " + BaseUserEntity.TableName + ".Id = " + BaseUserLogOnEntity.TableName + ".Id";
                    }
                    this.SelectFields = BaseUserEntity.TableName + ".* "
                                                + "," + BaseUserContactEntity.TableName + "." + BaseUserContactEntity.FieldEmail
                                                + "," + BaseUserContactEntity.TableName + "." + BaseUserContactEntity.FieldMobile
                                                + "," + BaseUserContactEntity.TableName + "." + BaseUserContactEntity.FieldTelephone
                                                + "," + BaseUserContactEntity.TableName + "." + BaseUserContactEntity.FieldQQ;

                    if (this.ShowUserLogOnInfo)
                    {
                        this.SelectFields += "," + BaseUserLogOnEntity.TableName + "." + BaseUserLogOnEntity.FieldFirstVisit
                                                + "," + BaseUserLogOnEntity.TableName + "." + BaseUserLogOnEntity.FieldPreviousVisit
                                                + "," + BaseUserLogOnEntity.TableName + "." + BaseUserLogOnEntity.FieldLastVisit
                                                + "," + BaseUserLogOnEntity.TableName + "." + BaseUserLogOnEntity.FieldIPAddress
                                                + "," + BaseUserLogOnEntity.TableName + "." + BaseUserLogOnEntity.FieldMACAddress
                                                + "," + BaseUserLogOnEntity.TableName + "." + BaseUserLogOnEntity.FieldLogOnCount
                                                + "," + BaseUserLogOnEntity.TableName + "." + BaseUserLogOnEntity.FieldUserOnLine
                                                + "," + BaseUserLogOnEntity.TableName + "." + BaseUserLogOnEntity.FieldCheckIPAddress
                                                + "," + BaseUserLogOnEntity.TableName + "." + BaseUserLogOnEntity.FieldMultiUserLogin;
                    }
                    break;
                case CurrentDbType.Oracle:
                    this.SelectFields = "*";
                    this.CurrentTableName = @"(SELECT " + BaseUserEntity.TableName + ".*";
                    if (this.ShowUserLogOnInfo)
                    {
                        this.CurrentTableName += " ," + BaseUserLogOnEntity.TableName + ".FirstVisit, " + BaseUserLogOnEntity.TableName + ".PreviousVisit, " + BaseUserLogOnEntity.TableName + ".LastVisit, " + BaseUserLogOnEntity.TableName + ".IPAddress, " + BaseUserLogOnEntity.TableName + ".MACAddress, " + BaseUserLogOnEntity.TableName + ".LogOnCount, " + BaseUserLogOnEntity.TableName + ".UserOnLine, " + BaseUserLogOnEntity.TableName + ".CheckIPAddress, " + BaseUserLogOnEntity.TableName + ".MultiUserLogin ";
                    }
                    this.CurrentTableName += @"       , BaseUserContact.Email
                                          , BaseUserContact.Mobile
                                          , BaseUserContact.Telephone
                                          , BaseUserContact.QQ
                                    FROM " + BaseUserEntity.TableName + " LEFT OUTER JOIN BaseUserContact ON " + BaseUserEntity.TableName + ".Id = BaseUserContact.Id ";

                    if (this.ShowUserLogOnInfo)
                    {
                        this.CurrentTableName += " LEFT OUTER JOIN " + BaseUserLogOnEntity.TableName + " ON " + BaseUserEntity.TableName + ".Id = " + BaseUserLogOnEntity.TableName + ".Id ";
                    }
                    if (enabled == null)
                    {
                        this.CurrentTableName += " WHERE " + BaseUserEntity.TableName + ".Id > 0 AND " + BaseUserEntity.TableName + ".DeletionStateCode = 0  AND " + BaseUserEntity.TableName + ".IsVisible = 1 ORDER BY " + BaseUserEntity.TableName + ".SortCode) " + BaseUserEntity.TableName;
                    }
                    else
                    {
                        string enabledState = enabled == true ? "1" : "0";
                        this.CurrentTableName += " WHERE " + BaseUserEntity.TableName + ".Id > 0 AND " + BaseUserEntity.TableName + ".DeletionStateCode = 0  AND " + BaseUserEntity.TableName + ".IsVisible = 1 AND (" + BaseUserEntity.TableName + ".Enabled = " + enabledState + ") ORDER BY " + BaseUserEntity.TableName + ".SortCode) " + BaseUserEntity.TableName;
                    }
                    break;
            }

            return GetDataTableByPage(out recordCount, pageIndex, pageSize, whereClause, null, order);
        }

        public DataTable SearchLogByPage(out int recordCount, int pageIndex, int pageSize, string permissionCode, string whereClause, string order = null)
        {
            switch (DbHelper.CurrentDbType)
            {
                case CurrentDbType.SqlServer:
                case CurrentDbType.Access:
                    this.CurrentTableName = BaseUserEntity.TableName + " LEFT OUTER JOIN " + BaseUserLogOnEntity.TableName + " ON " + BaseUserEntity.TableName + ".Id = " + BaseUserLogOnEntity.TableName + ".Id";
                    this.SelectFields = BaseUserEntity.TableName + ".* ";

                    this.SelectFields += "," + BaseUserLogOnEntity.TableName + "." + BaseUserLogOnEntity.FieldFirstVisit
                                            + "," + BaseUserLogOnEntity.TableName + "." + BaseUserLogOnEntity.FieldPreviousVisit
                                            + "," + BaseUserLogOnEntity.TableName + "." + BaseUserLogOnEntity.FieldLastVisit
                                            + "," + BaseUserLogOnEntity.TableName + "." + BaseUserLogOnEntity.FieldIPAddress
                                            + "," + BaseUserLogOnEntity.TableName + "." + BaseUserLogOnEntity.FieldMACAddress
                                            + "," + BaseUserLogOnEntity.TableName + "." + BaseUserLogOnEntity.FieldLogOnCount
                                            + "," + BaseUserLogOnEntity.TableName + "." + BaseUserLogOnEntity.FieldUserOnLine
                                            + "," + BaseUserLogOnEntity.TableName + "." + BaseUserLogOnEntity.FieldCheckIPAddress
                                            + "," + BaseUserLogOnEntity.TableName + "." + BaseUserLogOnEntity.FieldMultiUserLogin;
                    break;
                case CurrentDbType.Oracle:
                    this.SelectFields = "*";
                    this.CurrentTableName = @"(SELECT " + BaseUserEntity.TableName + ".*";
                    this.CurrentTableName += " ," + BaseUserLogOnEntity.TableName + ".FirstVisit, " + BaseUserLogOnEntity.TableName + ".PreviousVisit, " + BaseUserLogOnEntity.TableName + ".LastVisit, " + BaseUserLogOnEntity.TableName + ".IPAddress, " + BaseUserLogOnEntity.TableName + ".MACAddress, " + BaseUserLogOnEntity.TableName + ".LogOnCount, " + BaseUserLogOnEntity.TableName + ".UserOnLine, " + BaseUserLogOnEntity.TableName + ".CheckIPAddress, " + BaseUserLogOnEntity.TableName + ".MultiUserLogin ";
                    this.CurrentTableName += @" FROM " + BaseUserEntity.TableName + " LEFT OUTER JOIN " + BaseUserLogOnEntity.TableName + " ON " + BaseUserEntity.TableName + ".Id = " + BaseUserLogOnEntity.TableName + ".Id ";
                    this.CurrentTableName += " WHERE " + BaseUserEntity.TableName + ".DeletionStateCode = 0 AND " + BaseUserEntity.TableName + ".IsVisible = 1 AND (" + BaseUserEntity.TableName + ".Enabled = 1) ORDER BY " + BaseUserEntity.TableName + ".SortCode) " + BaseUserEntity.TableName;
                    break;
            }
            return GetDataTableByPage(out recordCount, pageIndex, pageSize, whereClause, null, order);
        }

        #region public DataTable GetDataTableByPage(string searchValue, string companyId, string departmentId, string roleId, out int recordCount, int pageIndex = 0, int pageSize = 20, string order = null) 分页查询
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="searchValue">查询字段</param>
        /// <param name="companyId">公司主键</param>
        /// <param name="departmentId">部门主键</param>
        /// <param name="roleId">角色主键</param>
        /// <param name="recordCount">记录数</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页显示</param>
        /// <param name="order">排序</param>
        /// <returns>数据表</returns>
        public DataTable GetDataTableByPage(string searchValue, string companyId, string departmentId, string roleId, out int recordCount, int pageIndex = 0, int pageSize = 20, string order = null)
        {
            string whereClause = BaseUserEntity.TableName + "." + BaseUserEntity.FieldDeletionStateCode + " = 0 "
                 + " AND " + BaseUserEntity.TableName + "." + BaseUserEntity.FieldEnabled + " = 1 "
                 + " AND " + BaseUserEntity.TableName + "." + BaseUserEntity.FieldIsVisible + " = 1 "
                 + " AND " + BaseUserEntity.TableName + "." + BaseUserEntity.FieldId + " > 0 ";

            if (!String.IsNullOrEmpty(companyId))
            {
                whereClause += " AND (" + BaseUserEntity.TableName + "." + BaseUserEntity.FieldCompanyId + " = '" + companyId + "')";
            }
            if (!String.IsNullOrEmpty(departmentId))
            {
                /*
                用非递归调用的建议方法
                sqlQuery += " AND " + BaseUserEntity.TableName + "." + BaseUserEntity.FieldDepartmentId 
                    + " IN ( SELECT " + BaseOrganizeEntity.FieldId 
                    + " FROM " + BaseOrganizeEntity.TableName 
                    + " WHERE " + BaseOrganizeEntity.FieldId + " = " + departmentId + " OR " + BaseOrganizeEntity.FieldParentId + " = " + departmentId + ")";
                */

                /*
                BaseOrganizeManager organizeManager = new BaseOrganizeManager(this.UserInfo);
                string[] ids = organizeManager.GetChildrensId(BaseOrganizeEntity.FieldId, departmentId, BaseOrganizeEntity.FieldParentId);
                if (ids != null && ids.Length > 0)
                {
                    whereClause += " AND (" + BaseUserEntity.TableName + "." + BaseUserEntity.FieldCompanyId + " IN (" + StringUtil.ArrayToList(ids) + ")"
                     + " OR " + BaseUserEntity.TableName + "." + BaseUserEntity.FieldSubCompanyId + " IN (" + StringUtil.ArrayToList(ids) + ")"
                     + " OR " + BaseUserEntity.TableName + "." + BaseUserEntity.FieldDepartmentId + " IN (" + StringUtil.ArrayToList(ids) + ")"
                     + " OR " + BaseUserEntity.TableName + "." + BaseUserEntity.FieldWorkgroupId + " IN (" + StringUtil.ArrayToList(ids) + "))";
                }
                */
                whereClause += " AND (" + BaseUserEntity.TableName + "." + BaseUserEntity.FieldDepartmentId + " = " + departmentId + ")";
            }
            if (!string.IsNullOrEmpty(roleId))
            {
                string tableNameUserRole = UserInfo.SystemCode + "UserRole";
                whereClause += " AND ( " + BaseUserEntity.TableName + "." + BaseUserEntity.FieldId + " IN "
                            + "           (SELECT " + BaseUserRoleEntity.FieldUserId
                            + "              FROM " + tableNameUserRole
                            + "             WHERE " + BaseUserRoleEntity.FieldRoleId + " = " + roleId + ""
                            + "               AND " + BaseUserRoleEntity.FieldEnabled + " = 1"
                            + "                AND " + BaseUserRoleEntity.FieldDeletionStateCode + " = 0)) ";
            }
            if (!string.IsNullOrEmpty(searchValue))
            {
                searchValue = "'" + StringUtil.GetSearchString(searchValue) + "'";
                whereClause += " AND (" + BaseUserEntity.FieldRealName + " LIKE " + searchValue;
                whereClause += " OR " + BaseUserEntity.FieldUserName + " LIKE " + searchValue;
                whereClause += " OR " + BaseUserEntity.FieldQuickQuery + " LIKE " + searchValue;
                whereClause += " OR " + BaseUserEntity.FieldSimpleSpelling + " LIKE " + searchValue + ")";
            }
            recordCount = DbLogic.GetCount(DbHelper, this.CurrentTableName, whereClause);
            this.CurrentTableName = "BaseUser";
            if (this.ShowUserLogOnInfo)
            {
                this.CurrentTableName = BaseUserEntity.TableName + " LEFT OUTER JOIN " + BaseUserLogOnEntity.TableName + " ON " + BaseUserEntity.TableName + ".Id = " + BaseUserLogOnEntity.TableName + ".Id ";
            }
            switch (DbHelper.CurrentDbType)
            {
                case CurrentDbType.SqlServer:
                case CurrentDbType.Access:
                    this.SelectFields = BaseUserEntity.TableName + ".* ";
                    if (this.ShowUserLogOnInfo)
                    {
                        this.SelectFields += "," + BaseUserLogOnEntity.TableName + "." + BaseUserLogOnEntity.FieldFirstVisit
                                    + "," + BaseUserLogOnEntity.TableName + "." + BaseUserLogOnEntity.FieldPreviousVisit
                                    + "," + BaseUserLogOnEntity.TableName + "." + BaseUserLogOnEntity.FieldLastVisit
                                    + "," + BaseUserLogOnEntity.TableName + "." + BaseUserLogOnEntity.FieldIPAddress
                                    + "," + BaseUserLogOnEntity.TableName + "." + BaseUserLogOnEntity.FieldMACAddress
                                    + "," + BaseUserLogOnEntity.TableName + "." + BaseUserLogOnEntity.FieldLogOnCount
                                    + "," + BaseUserLogOnEntity.TableName + "." + BaseUserLogOnEntity.FieldUserOnLine;
                    }
                    break;
                case CurrentDbType.Oracle:
                case CurrentDbType.MySql:
                case CurrentDbType.DB2:
                    break;
            }
            return DbLogic.GetDataTableByPage(DbHelper, this.CurrentTableName, this.SelectFields, pageIndex, pageSize, whereClause, order);
        }
        #endregion
    }
}