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
    /// BaseStaffManager
    /// 职员管理
    /// 
    /// 修改记录
    /// 
    ///		2014.08.10 版本：1.0 JiRiGaLa	主键整理。
    /// 
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2014.08.10</date>
    /// </author> 
    /// </summary>
    public partial class BaseStaffManager : BaseManager
    { 
        private string GetSearchConditional(string permissionCode, string where, bool? enabled, string auditStates, string companyId = null, string departmentId = null)
        {
            string whereClause = BaseStaffEntity.TableName + "." + BaseStaffEntity.FieldDeletionStateCode + " = 0 ";
            if (enabled.HasValue)
            {
                if (enabled == true)
                {
                    whereClause += " AND " + BaseStaffEntity.TableName + "." + BaseStaffEntity.FieldEnabled + " = 1 ";
                }
                else
                {
                    whereClause += " AND " + BaseStaffEntity.TableName + "." + BaseStaffEntity.FieldEnabled + " = 0 ";
                }
            }
            if (!String.IsNullOrEmpty(where))
            {
                // 传递过来的表达式，还是搜索值？
                if (where.IndexOf("AND") < 0 && where.IndexOf("=") < 0)
                {
                    where = StringUtil.GetSearchString(where);
                    whereClause += " AND ("
                                + BaseStaffEntity.TableName + "." + BaseStaffEntity.FieldUserName + " LIKE '" + where + "'"
                                // + " OR " + BaseStaffEntity.TableName + "." + BaseStaffEntity.FieldSimpleSpelling + " LIKE '" + where + "'"
                                + " OR " + BaseStaffEntity.TableName + "." + BaseStaffEntity.FieldCode + " LIKE '" + where + "'"
                                + " OR " + BaseStaffEntity.TableName + "." + BaseStaffEntity.FieldRealName + " LIKE '" + where + "'"
                                + " OR " + BaseStaffEntity.TableName + "." + BaseStaffEntity.FieldQuickQuery + " LIKE '" + where + "'"
                                + " OR " + BaseStaffEntity.TableName + "." + BaseStaffEntity.FieldCompanyName + " LIKE '" + where + "'"
                                + " OR " + BaseStaffEntity.TableName + "." + BaseStaffEntity.FieldDepartmentName + " LIKE '" + where + "'"
                        // + " OR " + BaseStaffEntity.TableName + "." + BaseStaffEntity.FieldDescription + " LIKE '" + search + "'"
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
                    whereClause += " AND (" + BaseStaffEntity.TableName + "." + BaseStaffEntity.FieldCompanyId + " IN (" + StringUtil.ArrayToList(ids) + ")"
                     + " OR " + BaseStaffEntity.TableName + "." + BaseStaffEntity.FieldDepartmentId + " IN (" + StringUtil.ArrayToList(ids) + ")"
                     + " OR " + BaseStaffEntity.TableName + "." + BaseStaffEntity.FieldWorkgroupId + " IN (" + StringUtil.ArrayToList(ids) + "))";
                }
                */
                whereClause += " AND (" + BaseStaffEntity.TableName + "." + BaseStaffEntity.FieldDepartmentId + " = " + departmentId + ")";
            }
            if (!string.IsNullOrEmpty(companyId))
            {
                whereClause += " AND (" + BaseStaffEntity.TableName + "." + BaseStaffEntity.FieldCompanyId + " = " + companyId + ")";
            }
            if (enabled != null)
            {
                whereClause += " AND (" + BaseStaffEntity.TableName + "." + BaseStaffEntity.FieldEnabled + " = " + ((bool)enabled ? 1 : 0) + ")";
            }
           
            // 是否过滤用户， 获得组织机构列表， 这里需要一个按用户过滤得功能
            if (!string.IsNullOrEmpty(permissionCode) && (!UserInfo.IsAdministrator) && (BaseSystemInfo.UsePermissionScope))
            {
                // string permissionCode = "Resource.ManagePermission";
                string permissionId = BaseModuleManager.GetIdByCodeByCache(UserInfo.SystemCode, permissionCode);
                if (!string.IsNullOrEmpty(permissionId))
                {
                    // 从小到大的顺序进行显示，防止错误发生
                    BaseUserScopeManager userPermissionScopeManager = new BaseUserScopeManager(this.DbHelper, this.UserInfo);
                    string[] organizeIds = userPermissionScopeManager.GetOrganizeIds(UserInfo.SystemCode, UserInfo.Id, permissionId);

                    // 没有任何数据权限
                    if (StringUtil.Exists(organizeIds, ((int)PermissionOrganizeScope.NotAllowed).ToString()))
                    {
                        whereClause += " AND (" + BaseStaffEntity.TableName + "." + BaseStaffEntity.FieldId + " = NULL ) ";
                    }
                    // 按详细设定的数据
                    if (StringUtil.Exists(organizeIds, ((int)PermissionOrganizeScope.ByDetails).ToString()))
                    {
                        BasePermissionScopeManager permissionScopeManager = new BasePermissionScopeManager(DbHelper, UserInfo);
                        string[] userIds = permissionScopeManager.GetUserIds(UserInfo.SystemCode, UserInfo.Id, permissionCode);
                        whereClause += " AND (" + BaseStaffEntity.TableName + "." + BaseStaffEntity.FieldId + " IN (" + string.Join(",", userIds) + ")) ";
                    }
                    // 自己的数据，仅本人
                    if (StringUtil.Exists(organizeIds, ((int)PermissionOrganizeScope.OnlyOwnData).ToString()))
                    {
                        whereClause += " AND (" + BaseStaffEntity.TableName + "." + BaseStaffEntity.FieldId + " = " + this.UserInfo.Id + ") ";
                    }
                    // 用户所在工作组数据
                    if (StringUtil.Exists(organizeIds, ((int)PermissionOrganizeScope.UserWorkgroup).ToString()))
                    {
                        // whereClause += " AND (" + BaseStaffEntity.TableName + "." + BaseStaffEntity.FieldWorkgroupId + " = " + this.UserInfo.WorkgroupId + ") ";
                    }
                    // 用户所在部门数据
                    if (StringUtil.Exists(organizeIds, ((int)PermissionOrganizeScope.UserDepartment).ToString()))
                    {
                        whereClause += " AND (" + BaseStaffEntity.TableName + "." + BaseStaffEntity.FieldDepartmentId + " = " + this.UserInfo.DepartmentId + ") ";
                    }
                    // 用户所在分支机构数据
                    if (StringUtil.Exists(organizeIds, ((int)PermissionOrganizeScope.UserSubCompany).ToString()))
                    {
                        // whereClause += " AND (" + BaseStaffEntity.TableName + "." + BaseStaffEntity.FieldSubCompanyId + " = " + this.UserInfo.SubCompanyId + ") ";
                    }
                    // 用户所在公司数据
                    if (StringUtil.Exists(organizeIds, ((int)PermissionOrganizeScope.UserCompany).ToString()))
                    {
                        whereClause += " AND (" + BaseStaffEntity.TableName + "." + BaseStaffEntity.FieldCompanyId + " = " + this.UserInfo.CompanyId + ") ";
                    }
                    // 全部数据，这里就不用设置过滤条件了
                    if (StringUtil.Exists(organizeIds, ((int)PermissionOrganizeScope.AllData).ToString()))
                    {
                    }
                }
            }
            return whereClause;
        }

      
        public DataTable SearchByPage(string permissionCode, string whereClause, bool? enabled, string auditStates, string companyId, string departmentId, out int recordCount, int pageIndex = 0, int pageSize = 20, string order = null)
        {
            whereClause = GetSearchConditional(permissionCode, whereClause, enabled, auditStates, companyId, departmentId);
            return GetDataTableByPage(out recordCount, pageIndex, pageSize, whereClause, null, order);
        }

        public DataTable SearchLogByPage(out int recordCount, int pageIndex, int pageSize, string permissionCode, string whereClause, string order = null)
        {
            switch (DbHelper.CurrentDbType)
            {
                case CurrentDbType.SqlServer:
                case CurrentDbType.Oracle:
                    this.SelectFields = "*";
                    this.CurrentTableName = "BaseStaff";
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
            string whereClause = BaseStaffEntity.TableName + "." + BaseStaffEntity.FieldDeletionStateCode + " = 0 "
                 + " AND " + BaseStaffEntity.TableName + "." + BaseStaffEntity.FieldEnabled + " = 1 ";

            if (!String.IsNullOrEmpty(companyId))
            {
                whereClause += " AND (" + BaseStaffEntity.TableName + "." + BaseStaffEntity.FieldCompanyId + " = " + companyId + ")";
            }
            if (!String.IsNullOrEmpty(departmentId))
            {
                whereClause += " AND (" + BaseStaffEntity.TableName + "." + BaseStaffEntity.FieldDepartmentId + " = " + departmentId + ")";
            }
            if (!string.IsNullOrEmpty(searchValue))
            {
                searchValue = "'" + StringUtil.GetSearchString(searchValue) + "'";
                whereClause += " AND (" + BaseStaffEntity.FieldRealName + " LIKE " + searchValue;
                whereClause += " OR " + BaseStaffEntity.FieldUserName + " LIKE " + searchValue;
                whereClause += " OR " + BaseStaffEntity.FieldQuickQuery + " LIKE " + searchValue +")";
                // whereClause += " OR " + BaseStaffEntity.FieldSimpleSpelling + " LIKE " + searchValue + ")";
            }
            recordCount = DbLogic.GetCount(DbHelper, this.CurrentTableName, whereClause);
            this.CurrentTableName = "BaseStaff";
            
            return DbLogic.GetDataTableByPage(DbHelper, this.CurrentTableName, this.SelectFields, pageIndex, pageSize, whereClause, order);
        }
        #endregion
    }
}