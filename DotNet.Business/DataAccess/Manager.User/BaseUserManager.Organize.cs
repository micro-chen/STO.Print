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
    /// BaseUserManager
    /// 用户管理
    /// 
    /// 修改记录
    /// 
    ///		2012.04.12 版本：1.0 JiRiGaLa	主键整理。
    /// 
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2012.04.12</date>
    /// </author> 
    /// </summary>
    public partial class BaseUserManager : BaseManager
    {
        /// <summary>
        /// 用户是否在某部门
        /// </summary>
        /// <param name="organizeName">部门名称</param>
        /// <returns>存在</returns>
        public bool IsInOrganize(string organizeName)
        {
            return IsInOrganize(this.UserInfo.Id, organizeName);
        }

        /// <summary>
        /// 用户是否在某部门
        /// </summary>
        /// <param name="userId">用户主键</param>
        /// <param name="organizeName">部门名称</param>
        /// <returns>存在</returns>
        public bool IsInOrganize(string userId, string organizeName)
        {
            bool result = false;
            // 把部门的主键找出来
            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>(BaseOrganizeEntity.FieldFullName, organizeName));
            parameters.Add(new KeyValuePair<string, object>(BaseOrganizeEntity.FieldEnabled, 1));
            parameters.Add(new KeyValuePair<string, object>(BaseOrganizeEntity.FieldDeletionStateCode, 0));
            BaseOrganizeManager organizeManager = new BaseOrganizeManager(this.UserInfo);
            string organizeId = organizeManager.GetId(parameters);
            if (string.IsNullOrEmpty(organizeId))
            {
                return result;
            }
            // 用户组织机构关联关系
            string[] organizeIds = this.GetAllOrganizeIds(userId);
            if (organizeIds == null || organizeIds.Length == 0)
            {
                return result;
            }
            // 用户的部门是否存在这些部门里
            result = StringUtil.Exists(organizeIds, organizeId);
            return result;
        }

        #region public string[] GetOrganizeIds(string userId) 获取用户的所有所在部门主键数组
        /// <summary>
        /// 获取用户的所有所在部门主键数组
        /// 2015-12-02 吉日嘎拉，优化方法。
        /// </summary>
        /// <param name="userId">用户主键</param>
        /// <returns>主键数组</returns>
        public string[] GetAllOrganizeIds(string userId)
        {
            string[] result = null;

            int errorMark = 0;
            /*
            // 被删除的不应该显示出来
            string sqlQuery = string.Format(
                             @"SELECT CompanyId AS Id
                                  FROM BaseUser
                                 WHERE DeletionStateCode = 0 AND Enabled =1 AND CompanyId IS NOT NULL  AND (Id = {0})
                                 UNION
                                SELECT SubCompanyId AS Id
                                  FROM BaseUser
                                 WHERE DeletionStateCode = 0 AND Enabled =1 AND CompanyId IS NOT NULL  AND (Id = {0})
                                 UNION
                                SELECT DepartmentId AS Id
                                  FROM BaseUser
                                 WHERE DeletionStateCode = 0 AND Enabled =1  AND DepartmentId IS NOT NULL AND (Id = {0})
                                 UNION
                                SELECT SubDepartmentId AS Id
                                  FROM BaseUser
                                 WHERE DeletionStateCode = 0 AND Enabled =1  AND SubDepartmentId IS NOT NULL AND (Id = {0})
                                 UNION
                                SELECT WorkgroupId AS Id
                                  FROM BaseUser
                                 WHERE DeletionStateCode = 0 AND Enabled =1  AND WorkgroupId IS NOT NULL AND (Id = {0})
                                 UNION

                                SELECT CompanyId AS Id
                                  FROM BaseUserOrganize
                                 WHERE DeletionStateCode = 0 AND Enabled =1  AND CompanyId IS NOT NULL AND (UserId = {0})
                                 UNION
                                SELECT SubCompanyId AS Id
                                  FROM BaseUserOrganize
                                 WHERE DeletionStateCode = 0 AND Enabled =1  AND CompanyId IS NOT NULL AND (UserId = {0})
                                 UNION
                                SELECT DepartmentId AS Id
                                  FROM BaseUserOrganize
                                 WHERE DeletionStateCode = 0 AND Enabled =1  AND DepartmentId IS NOT NULL AND (UserId = {0})
                                 UNION
                                SELECT SubDepartmentId AS Id
                                  FROM BaseUserOrganize
                                 WHERE DeletionStateCode = 0 AND Enabled =1  AND SubDepartmentId IS NOT NULL AND (UserId = {0})
                                 UNION
                                SELECT WorkgroupId AS Id
                                  FROM BaseUserOrganize
                                 WHERE DeletionStateCode = 0 AND Enabled =1  AND WorkgroupId IS NOT NULL AND (UserId = {0}) ", userId);
            */

            string commandText = @"SELECT CompanyId AS Id
                                  FROM BaseUser 
                                 WHERE DeletionStateCode = 0 AND Enabled =1 AND CompanyId IS NOT NULL AND Id = " + DbHelper.GetParameter(BaseUserEntity.FieldId) + @"
                                 UNION
                                SELECT CompanyId AS Id
                                  FROM BaseUserOrganize
                                 WHERE DeletionStateCode = 0 AND Enabled =1  AND CompanyId IS NOT NULL AND UserId = " + DbHelper.GetParameter(BaseUserOrganizeEntity.FieldUserId);

            /*
            var dt = DbHelper.Fill(sqlQuery);
            if (dt.Rows.Count > 0)
            {
                result = BaseBusinessLogic.FieldToArray(dt, BaseUserEntity.FieldId).Distinct<string>().Where(t => !string.IsNullOrEmpty(t)).ToArray();
            }
            */
            // 2015-12-02 吉日嘎拉 方法优化，采用 ExecuteReader 提高效率，减少内存使用。
            List<IDbDataParameter> dbParameters = new List<IDbDataParameter>();
            dbParameters.Add(DbHelper.MakeParameter(BaseUserEntity.FieldId, userId));
            dbParameters.Add(DbHelper.MakeParameter(BaseUserOrganizeEntity.FieldUserId, userId));
            List<string> ids = new List<string>();
            try
            {
                errorMark = 1;
                using (IDataReader dataReader = DbHelper.ExecuteReader(commandText, dbParameters.ToArray()))
                {
                    while (dataReader.Read())
                    {
                        ids.Add(dataReader[BaseOrganizeEntity.FieldId].ToString());
                    }
                }
                result = ids.ToArray();
            }
            catch (System.Exception ex)
            {
                string writeMessage = "BasePermissionManager.CheckUserRolePermission:发生时间:" + DateTime.Now
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
        #endregion

        public DataTable GetDataTableByDepartment(string departmentId)
        {
            string sqlQuery = "SELECT " + this.SelectFields
                + " FROM " + BaseUserEntity.TableName;

            sqlQuery += " WHERE (" + BaseUserEntity.TableName + "." + BaseUserEntity.FieldDeletionStateCode + " = 0 ";
            sqlQuery += " AND " + BaseUserEntity.TableName + "." + BaseUserEntity.FieldEnabled + " = 1 ) ";

            if (!String.IsNullOrEmpty(departmentId))
            {
                // 从用户表
                sqlQuery += " AND (" + BaseUserEntity.TableName + "." + BaseUserEntity.FieldDepartmentId + " = '" + departmentId + "') ";
                // 从兼职表读取用户 
                /*
                sqlQuery += " OR " + BaseUserEntity.FieldId + " IN ("
                        + "SELECT " + BaseUserOrganizeEntity.FieldUserId
                        + "   FROM " + BaseUserOrganizeEntity.TableName
                        + "  WHERE (" + BaseUserOrganizeEntity.TableName + "." + BaseUserOrganizeEntity.FieldDeletionStateCode + " = 0 ) "
                        + "       AND (" + BaseUserOrganizeEntity.TableName + "." + BaseUserOrganizeEntity.FieldDepartmentId + " = '" + departmentId + "')) ";
                 */


            }
            sqlQuery += " ORDER BY " + BaseUserEntity.TableName + "." + BaseUserEntity.FieldSortCode;
            return this.DbHelper.Fill(sqlQuery);
        }

        public DataTable GetDataTableByCompany(string companyId)
        {
            string sqlQuery = "SELECT " + this.SelectFields
                + " FROM " + BaseUserEntity.TableName;

            sqlQuery += " WHERE (" + BaseUserEntity.TableName + "." + BaseUserEntity.FieldDeletionStateCode + " = 0 ";
            sqlQuery += " AND " + BaseUserEntity.TableName + "." + BaseUserEntity.FieldEnabled + " = 1 ) ";

            if (!String.IsNullOrEmpty(companyId))
            {
                // 从用户表
                sqlQuery += " AND (" + BaseUserEntity.TableName + "." + BaseUserEntity.FieldCompanyId + " = '" + companyId + "') ";
                // 从兼职表读取用户
                /*
                sqlQuery += " OR " + BaseUserEntity.FieldId + " IN ("
                        + "SELECT " + BaseUserOrganizeEntity.FieldUserId
                        + "   FROM " + BaseUserOrganizeEntity.TableName
                        + "  WHERE (" + BaseUserOrganizeEntity.TableName + "." + BaseUserOrganizeEntity.FieldDeletionStateCode + " = 0 ) "
                        + "       AND (" + BaseUserOrganizeEntity.TableName + "." + BaseUserOrganizeEntity.FieldCompanyId + " = '" + companyId + "')) ";
                */
            }
            sqlQuery += " ORDER BY " + BaseUserEntity.TableName + "." + BaseUserEntity.FieldSortCode;
            return this.DbHelper.Fill(sqlQuery);
        }

        #region public List<BaseUserEntity> GetListByDepartment(string departmentId)
        /// <summary>
        /// 按部门获取用户
        /// </summary>
        /// <param name="departmentId">部门主键</param>
        /// <returns>数据表</returns>
        public List<BaseUserEntity> GetListByDepartment(string departmentId)
        {
            string sqlQuery = "SELECT " + BaseUserEntity.TableName + ".* "
                + " FROM " + BaseUserEntity.TableName;

            sqlQuery += " WHERE (" + BaseUserEntity.TableName + "." + BaseUserEntity.FieldDeletionStateCode + " = 0 ";
            sqlQuery += " AND " + BaseUserEntity.TableName + "." + BaseUserEntity.FieldEnabled + " = 1 ) ";

            if (!String.IsNullOrEmpty(departmentId))
            {
                // 从用户表
                sqlQuery += " AND (" + BaseUserEntity.TableName + "." + BaseUserEntity.FieldDepartmentId + " = '" + departmentId + "') ";
                // 从兼职表读取用户 
                /*
                sqlQuery += " OR " + BaseUserEntity.FieldId + " IN ("
                        + "SELECT " + BaseUserOrganizeEntity.FieldUserId
                        + "   FROM " + BaseUserOrganizeEntity.TableName
                        + "  WHERE (" + BaseUserOrganizeEntity.TableName + "." + BaseUserOrganizeEntity.FieldDeletionStateCode + " = 0 ) "
                        + "       AND (" + BaseUserOrganizeEntity.TableName + "." + BaseUserOrganizeEntity.FieldDepartmentId + " = '" + departmentId + "')) ";
                */
            }
            sqlQuery += " ORDER BY " + BaseUserEntity.TableName + "." + BaseUserEntity.FieldSortCode;
            IDataReader dr = DbHelper.ExecuteReader(sqlQuery);
            return this.GetList<BaseUserEntity>(dr);
        }
        #endregion

        #region public List<BaseUserEntity> GetListByCompany(string companyId)
        /// <summary>
        /// 按公司获取用户
        /// </summary>
        /// <param name="companyId">公司主键</param>
        /// <returns>数据表</returns>
        public List<BaseUserEntity> GetListByCompany(string companyId)
        {
            string sqlQuery = "SELECT " + BaseUserEntity.TableName + ".* "
                + " FROM " + BaseUserEntity.TableName;

            sqlQuery += " WHERE (" + BaseUserEntity.TableName + "." + BaseUserEntity.FieldDeletionStateCode + " = 0 ";
            sqlQuery += " AND " + BaseUserEntity.TableName + "." + BaseUserEntity.FieldEnabled + " = 1 ) ";

            if (!String.IsNullOrEmpty(companyId))
            {
                // 从用户表
                sqlQuery += " AND (" + BaseUserEntity.TableName + "." + BaseUserEntity.FieldCompanyId + " = '" + companyId + "') ";
                // 从兼职表读取用户 
                /*
                sqlQuery += " OR " + BaseUserEntity.FieldId + " IN ("
                        + "SELECT " + BaseUserOrganizeEntity.FieldUserId
                        + "   FROM " + BaseUserOrganizeEntity.TableName
                        + "  WHERE (" + BaseUserOrganizeEntity.TableName + "." + BaseUserOrganizeEntity.FieldDeletionStateCode + " = 0 ) "
                        + "       AND (" + BaseUserOrganizeEntity.TableName + "." + BaseUserOrganizeEntity.FieldCompanyId + " = '" + companyId + "')) ";
                */
            }
            sqlQuery += " ORDER BY " + BaseUserEntity.TableName + "." + BaseUserEntity.FieldSortCode;
            IDataReader dr = DbHelper.ExecuteReader(sqlQuery);
            return this.GetList<BaseUserEntity>(dr);
        }
        #endregion

        private string GetUserSQL(string[] organizeIds, bool idOnly = false)
        {
            string field = idOnly ? BaseUserEntity.FieldId : "*";
            string organizeList = string.Join(",", organizeIds);
            string sqlQuery = "SELECT " + field
                + " FROM " + BaseUserEntity.TableName
                // 从用户表里去找
                + " WHERE " + BaseUserEntity.TableName + "." + BaseUserEntity.FieldDeletionStateCode + " = 0 "
                + "       AND " + BaseUserEntity.TableName + "." + BaseUserEntity.FieldEnabled + " = 1 "
                + "       AND (" + BaseUserEntity.TableName + "." + BaseUserEntity.FieldWorkgroupId + " IN ( " + organizeList + ") "
                + "             OR " + BaseUserEntity.TableName + "." + BaseUserEntity.FieldDepartmentId + " IN (" + organizeList + ") "
                + "             OR " + BaseUserEntity.TableName + "." + BaseUserEntity.FieldSubCompanyId + " IN (" + organizeList + ") "
                + "             OR " + BaseUserEntity.TableName + "." + BaseUserEntity.FieldCompanyId + " IN (" + organizeList + ")) "
                // 从用户兼职表里去取用户
                /*
                + " OR " + BaseUserEntity.FieldId + " IN ("
                        + "SELECT " + BaseUserOrganizeEntity.FieldUserId
                        + "   FROM " + BaseUserOrganizeEntity.TableName
                        + "  WHERE (" + BaseUserOrganizeEntity.TableName + "." + BaseUserOrganizeEntity.FieldDeletionStateCode + " = 0 ) "
                        + "       AND (" + BaseUserOrganizeEntity.TableName + "." + BaseUserOrganizeEntity.FieldWorkgroupId + " IN ( " + organizeList + ") "
                        + "             OR " + BaseUserOrganizeEntity.TableName + "." + BaseUserOrganizeEntity.FieldDepartmentId + " IN (" + organizeList + ") "
                        + "             OR " + BaseUserOrganizeEntity.TableName + "." + BaseUserOrganizeEntity.FieldSubCompanyId + " IN (" + organizeList + ") "
                        + "             OR " + BaseUserOrganizeEntity.TableName + "." + BaseUserOrganizeEntity.FieldCompanyId + " IN (" + organizeList + "))) "
                */
                + " ORDER BY " + BaseUserEntity.TableName + "." + BaseUserEntity.FieldSortCode;
            return sqlQuery;
        }

        #region public List<BaseUserEntity> GetDataTableByOrganizes(string[] ids) 按工作组、部门、公司获用户列表
        /// <summary>
        /// 按工作组、部门、公司获用户列表
        /// </summary>
        /// <param name="ids">主键数组</param>
        /// <returns>数据表</returns>
        public List<BaseUserEntity> GetListByOrganizes(string[] organizeIds)
        {
            string sqlQuery = this.GetUserSQL(organizeIds, false);
            IDataReader dr = DbHelper.ExecuteReader(sqlQuery);
            return this.GetList<BaseUserEntity>(dr);
        }
        #endregion

        public DataTable GetDataTableByOrganizes(string[] organizeIds)
        {
            string sqlQuery = this.GetUserSQL(organizeIds, false);
            return this.DbHelper.Fill(sqlQuery);
        }

        public string[] GetUserIds(string[] userIds, string[] organizeIds, string[] roleIds)
        {
            /*
            // 要注意不能重复发信息，只能发一次。
            // 按公司查找用户
            string[] companyUsers = null;
            // 按部门查找用户
            string[] departmentUsers = null; 
            // 按工作组查找用户
            string[] workgroupUsers = null; 
            if (ids != null && ids.Length > 0)
            {
                // 这里获得的是用户主键，不是员工主键
                companyUsers = this.GetIds(new KeyValuePair<string, object>(BaseUserEntity.FieldDeletionStateCode, 0)
                    , new KeyValuePair<string, object>(BaseUserEntity.FieldEnabled, 1)
                    , new KeyValuePair<string, object>(BaseUserEntity.FieldCompanyId, ids));
                subCompanyUsers = this.GetIds(new KeyValuePair<string, object>(BaseUserEntity.FieldDeletionStateCode, 0)
                    , new KeyValuePair<string, object>(BaseUserEntity.FieldEnabled, 1)
                    , new KeyValuePair<string, object>(BaseUserEntity.FieldSubCompanyId, ids));
                departmentUsers = this.GetIds(new KeyValuePair<string, object>(BaseUserEntity.FieldDeletionStateCode, 0)
                    , new KeyValuePair<string, object>(BaseUserEntity.FieldEnabled, 1)
                    , new KeyValuePair<string, object>(BaseUserEntity.FieldDepartmentId, ids));
                workgroupUsers = this.GetIds(new KeyValuePair<string, object>(BaseUserEntity.FieldDeletionStateCode, 0)
                    , new KeyValuePair<string, object>(BaseUserEntity.FieldEnabled, 1)
                    , new KeyValuePair<string, object>(BaseUserEntity.FieldWorkgroupId, ids));
            }
            */

            string[] companyUsers = null;

            if (organizeIds != null && organizeIds.Length > 0)
            {
                string sqlQuery = this.GetUserSQL(organizeIds, true);
                var dt = DbHelper.Fill(sqlQuery);
                companyUsers = BaseBusinessLogic.FieldToArray(dt, BaseUserEntity.FieldId).Distinct<string>().Where(t => !string.IsNullOrEmpty(t)).ToArray();
            }

            string[] roleUsers = null;
            if (roleIds != null && roleIds.Length > 0)
            {
                roleUsers = this.GetUserIds(roleIds);
            }
            // userIds = StringUtil.Concat(userIds, companyUsers, departmentUsers, workgroupUsers, roleUsers);
            userIds = StringUtil.Concat(userIds, companyUsers, roleUsers);
            return userIds;
        }

        #region public DataTable SearchByDepartment(string departmentId, string searchValue)  按部门获取部门用户,包括子部门的用户
        /// <summary>
        /// 按部门获取部门用户,包括子部门的用户
        /// </summary>
        /// <param name="departmentId">部门主键</param>
        /// <returns>数据表</returns>
        public DataTable SearchByDepartment(string departmentId, string searchValue)
        {
            string sqlQuery = "SELECT " + BaseUserEntity.TableName + ".* "
                + " FROM " + BaseUserEntity.TableName;
            sqlQuery += " WHERE (" + BaseUserEntity.TableName + "." + BaseUserEntity.FieldDeletionStateCode + " = 0 ";
            sqlQuery += " AND " + BaseUserEntity.TableName + "." + BaseUserEntity.FieldEnabled + " = 1 ) ";
            if (!String.IsNullOrEmpty(departmentId))
            {
                /*
                用非递归调用的建议方法
                sqlQuery += " AND " + BaseUserEntity.TableName + "." + BaseUserEntity.FieldDepartmentId 
                    + " IN ( SELECT " + BaseOrganizeEntity.FieldId 
                    + " FROM " + BaseOrganizeEntity.TableName 
                    + " WHERE " + BaseOrganizeEntity.FieldId + " = " + departmentId + " OR " + BaseOrganizeEntity.FieldParentId + " = " + departmentId + ")";
                */
                BaseOrganizeManager organizeManager = new BaseOrganizeManager(this.DbHelper, this.UserInfo);
                string[] organizeIds = organizeManager.GetChildrensId(BaseOrganizeEntity.FieldId, departmentId, BaseOrganizeEntity.FieldParentId);
                if (organizeIds != null && organizeIds.Length > 0)
                {
                    sqlQuery += " AND (" + BaseUserEntity.TableName + "." + BaseUserEntity.FieldCompanyId + " IN (" + StringUtil.ArrayToList(organizeIds) + ")"
                     + " OR " + BaseUserEntity.TableName + "." + BaseUserEntity.FieldDepartmentId + " IN (" + StringUtil.ArrayToList(organizeIds) + ")"
                     + " OR " + BaseUserEntity.TableName + "." + BaseUserEntity.FieldWorkgroupId + " IN (" + StringUtil.ArrayToList(organizeIds) + "))";
                }
            }
            List<IDbDataParameter> dbParameters = new List<IDbDataParameter>();
            searchValue = searchValue.Trim();
            if (!String.IsNullOrEmpty(searchValue))
            {
                sqlQuery += " AND (" + BaseUserEntity.FieldUserName + " LIKE " + DbHelper.GetParameter(BaseUserEntity.FieldUserName);
                sqlQuery += " OR " + BaseUserEntity.FieldCode + " LIKE " + DbHelper.GetParameter(BaseUserEntity.FieldCode);
                sqlQuery += " OR " + BaseUserEntity.FieldRealName + " LIKE " + DbHelper.GetParameter(BaseUserEntity.FieldRealName);
                sqlQuery += " OR " + BaseUserEntity.FieldDepartmentName + " LIKE " + DbHelper.GetParameter(BaseUserEntity.FieldDepartmentName) + ")";
                if (searchValue.IndexOf("%") < 0)
                {
                    searchValue = "%" + searchValue + "%";
                }
                dbParameters.Add(DbHelper.MakeParameter(BaseUserEntity.FieldUserName, searchValue));
                dbParameters.Add(DbHelper.MakeParameter(BaseUserEntity.FieldCode, searchValue));
                dbParameters.Add(DbHelper.MakeParameter(BaseUserEntity.FieldRealName, searchValue));
                dbParameters.Add(DbHelper.MakeParameter(BaseUserEntity.FieldDepartmentName, searchValue));
            }
            sqlQuery += " ORDER BY " + BaseUserEntity.TableName + "." + BaseUserEntity.FieldSortCode;
            return DbHelper.Fill(sqlQuery, dbParameters.ToArray());
        }
        #endregion

        public List<BaseUserEntity> GetChildrenUserList(string organizeId)
        {
            string[] organizeIds = null;
            BaseOrganizeManager organizeManager = new BaseOrganizeManager(this.DbHelper, this.UserInfo);
            switch (DbHelper.CurrentDbType)
            {
                case CurrentDbType.Access:
                case CurrentDbType.SqlServer:
                    string organizeCode = organizeManager.GetCodeById(organizeId);
                    organizeIds = organizeManager.GetChildrensIdByCode(BaseOrganizeEntity.FieldCode, organizeCode);
                    break;
                case CurrentDbType.Oracle:
                    organizeIds = organizeManager.GetChildrensId(BaseOrganizeEntity.FieldId, organizeId, BaseOrganizeEntity.FieldParentId);
                    break;
            }
            return this.GetListByOrganizes(organizeIds);
        }

        public DataTable GetChildrenUserDataTable(string organizeId)
        {
            string[] organizeIds = null;
            BaseOrganizeManager organizeManager = new BaseOrganizeManager(this.DbHelper, this.UserInfo);
            switch (DbHelper.CurrentDbType)
            {
                case CurrentDbType.Access:
                case CurrentDbType.SqlServer:
                    string organizeCode = organizeManager.GetCodeById(organizeId);
                    organizeIds = organizeManager.GetChildrensIdByCode(BaseOrganizeEntity.FieldCode, organizeCode);
                    break;
                case CurrentDbType.Oracle:
                    organizeIds = organizeManager.GetChildrensId(BaseOrganizeEntity.FieldId, organizeId, BaseOrganizeEntity.FieldParentId);
                    break;
            }
            return this.GetDataTableByOrganizes(organizeIds);
        }
    }
}