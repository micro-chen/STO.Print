//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd.  
//-----------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DotNet.Business
{
    using DotNet.Model;
    using DotNet.Utilities;

    /// <summary>
    /// BaseUserManager
    /// 用户管理-用户角色关系管理
    /// 
    /// 修改记录
    /// 
    ///		2016.03.02 版本：2.1 JiRiGaLa	方法简化、能把去掉的方法全部去掉、这样调用的来源就好控制了。
    ///		2016.02.26 版本：2.0 JiRiGaLa	用户角色关系进行缓存优化。
    ///		2015.12.06 版本：1.1 JiRiGaLa	改进参数化、未绑定变量硬解析。
    ///		2015.11.17 版本：1.1 JiRiGaLa	谁操作的？哪个系统的？哪个用户是否在哪个角色里？进行改进。
    ///		2012.04.12 版本：1.0 JiRiGaLa	主键整理。
    /// 
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2016.03.02</date>
    /// </author> 
    /// </summary>
    public partial class BaseUserManager : BaseManager
    {
        public static string[] GetRoleIdsByCache(string systemCode, string userId, string companyId = null)
        {
            // 返回值
            string[] result = null;

            if (!string.IsNullOrEmpty(userId))
            {
                string key = "User:" + userId + ":" + systemCode + ":Role";

                using (var readOnlyRedisClient = PooledRedisHelper.GetPermissionReadOnlyClient())
                {
                    HashSet<string> setItems = readOnlyRedisClient.GetAllItemsFromSet(key);
                    if (setItems.Count == 0)
                    {
                        // 找不到数据进行数据库查询
                        BaseUserManager userManager = new BaseUserManager();
                        string[] roleIds = userManager.GetRoleIds(systemCode, userId, companyId);

                        List<string> items = null;
                        if (roleIds.Length > 0)
                        {
                            items = new List<string>(roleIds);
                        }
                        else
                        {
                            // 2016-02-26 吉日嘎拉 这里是为了防止重复读取，数据库被拖、效率低
                            items = new List<string>(new string[] { string.Empty });
                        }
#if ReadOnlyRedis
                        using (var redisClient = PooledRedisHelper.GetPermissionClient())
                        {
                            redisClient.AddRangeToSet(key, items);
                            redisClient.ExpireEntryAt(key, DateTime.Now.AddMinutes(20));
                        }
#else
                        readOnlyRedisClient.AddRangeToSet(key, items);
                        readOnlyRedisClient.ExpireEntryAt(key, DateTime.Now.AddMinutes(20));
#endif
                    }
                    else
                    {
                        result = setItems.ToArray();
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 用户是否在角色里
        /// 2015-12-24 吉日嘎拉 提供高速缓存使用的方法
        /// </summary>
        /// <param name="systemCode">系统编号</param>
        /// <param name="userId">用户主键</param>
        /// <param name="roleCode">角色编号</param>
        /// <param name="companyId">公司主键</param>
        /// <param name="useBaseRole">使用基础角色</param>
        /// <returns>在角色中</returns>
        public static bool IsInRoleByCache(string systemCode, string userId, string roleCode, string companyId = null)
        {
            // 返回值
            bool result = false;

            if (!string.IsNullOrEmpty(userId))
            {
                // 从缓存里快速得到角色对应的主键盘
                string roleId = BaseRoleManager.GetIdByCodeByCache(systemCode, roleCode);
                if (!string.IsNullOrEmpty(roleId))
                {
                    string[] roleIds = GetRoleIdsByCache(systemCode, userId, companyId);
                    if (roleIds != null && roleIds.Length > 0)
                    {
                        result = (Array.IndexOf(roleIds, roleId) >= 0);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 用户是否在某个角色里
        /// 包括所在公司的角色也进行判断
        /// </summary>
        /// <param name="userInfo">当前用户</param>
        /// <param name="roleName">角色名称</param>
        /// <returns>是否在角色中</returns>
        public bool IsInRole(BaseUserInfo userInfo, string roleName)
        {
            return IsInRole(userInfo.SystemCode, userInfo.Id, roleName);
        }

        /// <summary>
        /// 用户是否在某个角色中
        /// </summary>
        /// <param name="userId">用户主键</param>
        /// <param name="roleName">角色</param>
        /// <returns>存在</returns>
        public bool IsInRole(string userId, string roleName)
        {
            string systemCode = "Base";
            if (UserInfo != null && !string.IsNullOrWhiteSpace(UserInfo.SystemCode))
            {
                systemCode = UserInfo.SystemCode;
            }

            return IsInRole(systemCode, userId, roleName);
        }

        /// <summary>
        /// 2015-11-17 吉日嘎拉 改进判断函数，方便别人调用，弱化当前操作者、可以灵活控制哪个子系统的数据
        /// </summary>
        /// <param name="systemCode">系统编号</param>
        /// <param name="userId">用户主键</param>
        /// <param name="roleName">角色名称</param>
        /// <returns>是否在角色中</returns>
        public bool IsInRole(string systemCode, string userId, string roleName)
        {
            bool result = false;

            // 用户参数不合法
            if (string.IsNullOrEmpty(userId))
            {
                return result;
            }
            // 角色名称不合法
            if (string.IsNullOrEmpty(roleName))
            {
                return result;
            }
            // 传入的系统编号不合法，自动认为是基础系统
            if (string.IsNullOrEmpty(systemCode))
            {
                systemCode = "Base";
            }

            string roleId = BaseRoleManager.GetIdByNameByCache(systemCode, roleName);
            
            // 无法获取角色主键
            if (string.IsNullOrEmpty(roleId))
            {
                return false;
            }

            // 获取用户的所有角色主键列表
            string[] roleIds = GetRoleIds(systemCode, userId);
            result = StringUtil.Exists(roleIds, roleId);

            return result;
        }

        /// <summary>
        /// 用户是否在某个角色里
        /// 包括所在公司的角色也进行判断
        /// </summary>
        /// <param name="userInfo">当前用户</param>
        /// <param name="code">编号</param>
        /// <returns>是否在角色里</returns>
        public bool IsInRoleByCode(BaseUserInfo userInfo, string code)
        {
            return IsInRoleByCode(userInfo.SystemCode, userInfo.Id, code);
        }

        /// <summary>
        /// 用户是否在某个角色中
        /// </summary>
        /// <param name="userId">用户主键</param>
        /// <param name="roleCode">角色编号</param>
        /// <returns>存在</returns>
        public bool IsInRoleByCode(string userId, string code)
        {
            string systemCode = "Base";
            if (UserInfo != null && !string.IsNullOrWhiteSpace(UserInfo.SystemCode))
            {
                systemCode = UserInfo.SystemCode;
            }

            return IsInRoleByCode(systemCode, userId, code);
        }

        /// <summary>
        /// 用户是否在某个角色中
        /// </summary>
        /// <param name="systemCode">系统编号</param>
        /// <param name="userId">用户主键</param>
        /// <param name="roleCode">角色编号</param>
        /// <returns>存在</returns>
        public bool IsInRoleByCode(string systemCode, string userId, string roleCode, bool useBaseRole = true)
        {
            bool result = false;

            if (string.IsNullOrEmpty(systemCode))
            {
                return false;
            }

            if (string.IsNullOrEmpty(userId))
            {
                return false;
            }

            if (string.IsNullOrEmpty(roleCode))
            {
                return false;
            }

            // 2016-01-07 吉日嘎拉 这里用缓存、效率会高
            string roleId = BaseRoleManager.GetIdByCodeByCache(systemCode, roleCode);
            if (string.IsNullOrEmpty(roleId))
            {
                // 2016-01-08 吉日嘎拉 看基础共用的角色里，是否在
                if (useBaseRole && !systemCode.Equals("Base"))
                {
                    roleId = BaseRoleManager.GetIdByCodeByCache("Base", roleCode);
                }
                if (string.IsNullOrEmpty(roleId))
                {
                    // 表明2个系统里都没了，就真没了
                    return false;
                }
            }

            string tableName = systemCode + "UserRole";
            BaseUserRoleManager userRoleManager = new BaseUserRoleManager(this.DbHelper, this.UserInfo, tableName);
            result = userRoleManager.Exists(new KeyValuePair<string, object>(BaseUserRoleEntity.FieldDeletionStateCode, 0)
                , new KeyValuePair<string, object>(BaseUserRoleEntity.FieldEnabled, 1)
                , new KeyValuePair<string, object>(BaseUserRoleEntity.FieldUserId, userId)
                , new KeyValuePair<string, object>(BaseUserRoleEntity.FieldRoleId, roleId));

            return result;
        }

        /// <summary>
        /// 获取用户的角色主键数组
        /// </summary>
        /// <param name="userId">用户主键</param>
        /// <returns>主键数组</returns>
        public string[] GetRoleIds(string userId)
        {
            return GetRoleIds(UserInfo.SystemCode, userId);
        }

        #region public string[] GetRoleIds(string systemCode, string userId, string companyId = null) 获取用户的角色主键数组
        /// <summary>
        /// 获取用户的角色主键数组
        /// </summary>
        /// <param name="systemCode">系统编号</param>
        /// <param name="userId">用户主键</param>
        /// <param name="companyId">公司主键</param>
        /// <returns>角色主键数组</returns>
        public string[] GetRoleIds(string systemCode, string userId, string companyId = null)
        {
            List<string> result = new List<string>();

            string userRoleTable = systemCode + "UserRole";

            // 被删除的角色不应该显示出来
            StringBuilder sqlQuery = new StringBuilder();
            sqlQuery.Append(" SELECT " + BaseUserRoleEntity.FieldRoleId);
            sqlQuery.Append("   FROM " + userRoleTable);
            sqlQuery.Append("  WHERE " + BaseUserRoleEntity.FieldUserId + " = " + DbHelper.GetParameter(BaseUserRoleEntity.FieldUserId));
            sqlQuery.Append("        AND " + BaseUserRoleEntity.FieldEnabled + " = " + DbHelper.GetParameter(BaseUserRoleEntity.FieldEnabled));
            sqlQuery.Append("        AND " + BaseUserRoleEntity.FieldDeletionStateCode + " = " + DbHelper.GetParameter(BaseUserRoleEntity.FieldDeletionStateCode));

            List<IDbDataParameter> dbParameters = new List<IDbDataParameter>();
            dbParameters.Add(DbHelper.MakeParameter(BaseUserRoleEntity.FieldUserId, userId));
            dbParameters.Add(DbHelper.MakeParameter(BaseUserRoleEntity.FieldEnabled, 1));
            dbParameters.Add(DbHelper.MakeParameter(BaseUserRoleEntity.FieldDeletionStateCode, 0));

            /*
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

            if (BaseSystemInfo.UseRoleOrganize && !string.IsNullOrEmpty(companyId))
            {
                string roleOrganizeTableName = systemCode + "RoleOrganize";
                sqlQuery += " UNION SELECT " + BaseRoleOrganizeEntity.FieldRoleId
                                  + " FROM " + roleOrganizeTableName
                                + "  WHERE " + BaseRoleOrganizeEntity.FieldOrganizeId + " = " + DbHelper.GetParameter(BaseRoleOrganizeEntity.FieldOrganizeId)
                                  + "        AND " + BaseRoleOrganizeEntity.FieldEnabled + " = 1 "
                                  + "        AND " + BaseRoleOrganizeEntity.FieldDeletionStateCode + " = 0 ";

                dbParameters.Add(DbHelper.MakeParameter(BaseRoleOrganizeEntity.FieldOrganizeId, companyId));
            }
            */

            using (IDataReader dataReader = DbHelper.ExecuteReader(sqlQuery.ToString(), dbParameters.ToArray()))
            {
                while (dataReader.Read())
                {
                    result.Add(dataReader[BaseUserRoleEntity.FieldRoleId].ToString());
                }
                dataReader.Close();
            }

            return result.ToArray();
            // return BaseBusinessLogic.FieldToArray(result, BaseUserRoleEntity.FieldRoleId).Distinct<string>().Where(t => !string.IsNullOrEmpty(t)).ToArray();
        }
        #endregion

        #region public List<BaseRoleEntity> GetRoleList(string systemCode, string userId, string companyId = null)
        /// <summary>
        /// 一个用户的所有的角色列表
        /// </summary>
        /// <param name="systemCode">系统编号</param>
        /// <param name="userId">用户主键</param>
        /// <param name="companyId">公司主键</param>
        /// <returns>角色列表</returns>
        public List<BaseRoleEntity> GetRoleList(string systemCode, string userId, string companyId = null, bool useBaseRole = true)
        {
            List<BaseRoleEntity> result = new List<BaseRoleEntity>();

            string[] roleIds = GetRoleIds(systemCode, userId, companyId);
            if (roleIds != null && roleIds.Length > 0)
            {
                List<BaseRoleEntity> entities = BaseRoleManager.GetEntitiesByCache(systemCode);
                result = (entities as List<BaseRoleEntity>).Where(t => roleIds.Contains(t.Id) && t.Enabled == 1 && t.DeletionStateCode == 0).ToList();
            }

            return result;

            /*
            string userRoleTable = systemCode + "UserRole";
            string roleTable = systemCode + "Role";
            // 被删除的角色不应该显示出来
            string sqlQuery = @"SELECT * FROM " + roleTable + " WHERE Enabled = 1 AND DeletionStateCode = 0 AND Id "
                + " IN (SELECT RoleId FROM " + userRoleTable
                + " WHERE UserId = " + DbHelper.GetParameter(BaseUserRoleEntity.FieldUserId) + " AND Enabled = 1 AND DeletionStateCode = 0 ";

            List<IDbDataParameter> dbParameters = new List<IDbDataParameter>();
            dbParameters.Add(DbHelper.MakeParameter(BaseUserRoleEntity.FieldUserId, userId));

            if (BaseSystemInfo.UseRoleOrganize && !string.IsNullOrEmpty(companyId))
            {
                string roleOrganizeTableName = systemCode + "RoleOrganize";
                sqlQuery += " UNION SELECT " + BaseRoleOrganizeEntity.FieldRoleId
                                  + " FROM " + roleOrganizeTableName
                                + "  WHERE " + BaseRoleOrganizeEntity.FieldOrganizeId + " = " + DbHelper.GetParameter(BaseRoleOrganizeEntity.FieldOrganizeId)
                                  + "        AND " + BaseRoleOrganizeEntity.FieldEnabled + " = 1 "
                                  + "        AND " + BaseRoleOrganizeEntity.FieldDeletionStateCode + " = 0 ";

                dbParameters.Add(DbHelper.MakeParameter(BaseRoleOrganizeEntity.FieldOrganizeId, companyId));
            }

            sqlQuery += ")";

            using (IDataReader dataReader = DbHelper.ExecuteReader(sqlQuery, dbParameters.ToArray()))
            {
                result = BaseEntity.GetList<BaseRoleEntity>(dataReader);
            }
            */
        }
        #endregion

        #region public List<BaseUserEntity> GetListByRole(string systemCode, string roleCode, string companyId = null) 按角色编号获得用户列表
        /// <summary>
        /// 按角色编号获得用户列表
        /// </summary>
        /// <param name="systemCode">系统编号</param>
        /// <param name="roleCode">角色编号</param>
        /// <returns>主键数组</returns>
        public List<BaseUserEntity> GetListByRole(string systemCode, string roleCode, string companyId = null)
        {
            List<BaseUserEntity> result = null;

            if (string.IsNullOrEmpty(systemCode))
            {
                systemCode = "Base";
            }
            string roleId = BaseRoleManager.GetIdByCodeByCache(systemCode, roleCode);
            if (!string.IsNullOrEmpty(roleId))
            {
                result = GetListByRole(systemCode, new string[] { roleId }, companyId);
            }

            return result;
        }
        #endregion

        public List<BaseUserEntity> GetListByRole(string systemCode, string[] roleIds, string companyId = null)
        {
            List<BaseUserEntity> result = new List<BaseUserEntity>();
            List<IDbDataParameter> dbParameters = new List<IDbDataParameter>();

            string tableNameUserRole = systemCode + "UserRole";

            string sqlQuery = string.Empty;

            /*
            sqlQuery = " SELECT * "
                     + "   FROM " + BaseUserEntity.TableName
                     + "  WHERE " + BaseUserEntity.FieldEnabled + " = 1 "
                     + "        AND " + BaseUserEntity.FieldDeletionStateCode + "= 0 ";

            if (!string.IsNullOrWhiteSpace(companyId))
            {
                sqlQuery += " AND " + BaseUserEntity.FieldCompanyId + " = " + DbHelper.GetParameter(BaseUserEntity.FieldCompanyId);
                dbParameters.Add(DbHelper.MakeParameter(BaseUserEntity.FieldCompanyId, companyId));
            }

            sqlQuery +=     "       AND ( " + BaseUserEntity.FieldId + " IN "
                          + "           (SELECT  " + BaseUserRoleEntity.FieldUserId
                          + "              FROM " + tableNameUserRole
                          + "             WHERE " + BaseUserRoleEntity.FieldRoleId + " IN (" + string.Join(",", roleIds) + ")"
                          + "               AND " + BaseUserRoleEntity.FieldEnabled + " = 1"
                          + "               AND " + BaseUserRoleEntity.FieldDeletionStateCode + " = 0) ) ";
                          // 排序的意义不大，在前台自己排序，尽量不要浪费数据库的宝贵资源
                          // + " ORDER BY " + BaseUserEntity.FieldSortCode;
            */

            sqlQuery = "     SELECT " + this.SelectFields
                     + "       FROM " + BaseUserEntity.TableName
                     + "          , (SELECT " + BaseUserRoleEntity.FieldUserId
                          + "         FROM " + tableNameUserRole
                          + "        WHERE " + BaseUserRoleEntity.FieldRoleId + " IN (" + string.Join(",", roleIds) + ")"
                          + "               AND " + BaseUserRoleEntity.FieldEnabled + " = 1 "
                          + "               AND " + BaseUserRoleEntity.FieldDeletionStateCode + " = 0) B "
                          + " WHERE " + BaseUserEntity.TableName + "." + BaseUserEntity.FieldId + " = B." + BaseUserRoleEntity.FieldUserId
                          + "       AND " + BaseUserEntity.TableName + "." + BaseUserEntity.FieldEnabled + " = 1 "
                          + "       AND " + BaseUserEntity.TableName + "." + BaseUserEntity.FieldDeletionStateCode + "= 0";

            if (!string.IsNullOrWhiteSpace(companyId))
            {
                sqlQuery += " AND " + BaseUserEntity.TableName + "." + BaseUserEntity.FieldCompanyId + " = " + DbHelper.GetParameter(BaseUserEntity.FieldCompanyId);
                dbParameters.Add(DbHelper.MakeParameter(BaseUserEntity.FieldCompanyId, companyId));
            }

            using (IDataReader dr = DbHelper.ExecuteReader(sqlQuery, dbParameters.ToArray()))
            {
                result = this.GetList<BaseUserEntity>(dr);
            }

            return result;
        }

        public DataTable GetDataTableByRole(string systemCode, string[] roleIds, string companyId = null)
        {
            if (string.IsNullOrEmpty(systemCode))
            {
                systemCode = "Base";
            }
            string tableNameUserRole = systemCode + "UserRole";

            string sqlQuery = "SELECT " + this.SelectFields + " FROM " + BaseUserEntity.TableName
                            + " WHERE " + BaseUserEntity.FieldEnabled + " = 1 "
                            + "       AND " + BaseUserEntity.FieldDeletionStateCode + "= 0 "
                            + "       AND ( " + BaseUserEntity.FieldId + " IN "
                            + "           (SELECT  " + BaseUserRoleEntity.FieldUserId
                            + "              FROM " + tableNameUserRole
                            + "             WHERE " + BaseUserRoleEntity.FieldRoleId + " IN (" + string.Join(",", roleIds) + ")"
                            + "               AND " + BaseUserRoleEntity.FieldEnabled + " = 1"
                            + "                AND " + BaseUserRoleEntity.FieldDeletionStateCode + " = 0)) "
                            + " ORDER BY  " + BaseUserEntity.FieldSortCode;

            return this.DbHelper.Fill(sqlQuery);
        }

        public int ClearUser(string systemCode, string roleId)
        {
            int result = 0;

            if (string.IsNullOrEmpty(systemCode))
            {
                systemCode = "Base";
            }
            string tableName = systemCode + "UserRole";
            BaseUserRoleManager manager = new BaseUserRoleManager(this.DbHelper, this.UserInfo, tableName);
            result += manager.Delete(new KeyValuePair<string, object>(BaseUserRoleEntity.FieldRoleId, roleId));

            return result;
        }

        public int ClearRole(string systemCode, string userId)
        {
            int result = 0;

            if (string.IsNullOrEmpty(systemCode))
            {
                systemCode = "Base";
            }
            string tableName = systemCode + "UserRole";
            BaseUserRoleManager manager = new BaseUserRoleManager(this.DbHelper, this.UserInfo, tableName);
            result += manager.Delete(new KeyValuePair<string, object>(BaseUserRoleEntity.FieldUserId, userId));

            return result;
        }

        /// <summary>
        /// 获取用户的角色列表
        /// </summary>
        /// <param name="systemCode">系统编号</param>
        /// <param name="userId">用户主键</param>
        /// <returns>角色数据表</returns>
        public DataTable GetUserRoleDataTable(string systemCode, string userId)
        {
            DataTable result = new DataTable(BaseRoleEntity.TableName);

            string tableUserRoleName = BaseUserRoleEntity.TableName;
            if (!string.IsNullOrWhiteSpace(systemCode))
            {
                tableUserRoleName = systemCode + "UserRole";
            }
            string tableRoleName = BaseRoleEntity.TableName;
            if (!string.IsNullOrWhiteSpace(systemCode))
            {
                tableRoleName = systemCode + "Role";
            }

            string commandText = @"SELECT BaseRole.Id
                                    , BaseRole.Code 
                                    , BaseRole.RealName 
                                    , BaseRole.Description 
                                    , UserRole.Enabled
                                    , UserRole.CreateOn
                                    , UserRole.CreateBy
                                    , UserRole.ModifiedOn
                                    , UserRole.ModifiedBy
                        FROM BaseRole RIGHT OUTER JOIN
                          (SELECT RoleId, Enabled, CreateOn, CreateBy, ModifiedOn, ModifiedBy
                             FROM BaseUserRole
                            WHERE UserId = " + DbHelper.GetParameter(BaseUserRoleEntity.FieldUserId)
                                  + " AND Enabled = 1 AND DeletionStateCode = 0 " + @") UserRole 
                            ON BaseRole.Id = UserRole.RoleId
                         WHERE BaseRole.Enabled = 1 AND BaseRole.DeletionStateCode = 0 
                      ORDER BY UserRole.CreateOn DESC ";

            commandText = commandText.Replace("BaseUserRole", tableUserRoleName);
            commandText = commandText.Replace("BaseRole", tableRoleName);

            List<IDbDataParameter> dbParameters = new List<IDbDataParameter>();
            dbParameters.Add(DbHelper.MakeParameter(BaseUserRoleEntity.FieldUserId, userId));

            result = this.Fill(commandText, dbParameters.ToArray());

            return result;
        }

        /// <summary>
        /// 获取某个单位某个角色里的成员
        /// 2015-12-05 吉日嘎拉 增加参数化优化。
        /// </summary>
        /// <param name="systemCode">系统编号</param>
        /// <param name="companyId">公司主键</param>
        /// <param name="roleId">角色主键</param>
        /// <returns>用户数据表</returns>
        public DataTable GetDataTableByCompanyByRole(string systemCode, string companyId, string roleId)
        {
            DataTable result = new DataTable(BaseRoleEntity.TableName);

            string commandText = @"SELECT BaseUser.Id
                                    , BaseUser.UserName
                                    , BaseUser.Code
                                    , BaseUser.Companyname
                                    , BaseUser.Departmentname
                                    , BaseUser.Realname 
                                    , BaseUser.Description 
                                    , UserRole.Enabled
                                    , UserRole.CreateOn
                                    , UserRole.CreateBy
                                    , UserRole.ModifiedOn
                                    , UserRole.ModifiedBy
                        FROM BaseUser RIGHT OUTER JOIN
                          (SELECT UserId, Enabled, CreateOn, CreateBy, ModifiedOn, ModifiedBy
                             FROM BaseUserRole
                            WHERE RoleId = " + DbHelper.GetParameter(BaseUserRoleEntity.FieldRoleId)
                             + " AND DeletionStateCode = " + DbHelper.GetParameter(BaseUserRoleEntity.FieldDeletionStateCode) + @") UserRole 
                            ON BaseUser.Id = UserRole.UserId
                         WHERE BaseUser.CompanyId = " + DbHelper.GetParameter(BaseUserEntity.FieldCompanyId) + @"
                      ORDER BY UserRole.CreateOn";

            List<IDbDataParameter> dbParameters = new List<IDbDataParameter>();
            dbParameters.Add(DbHelper.MakeParameter(BaseUserRoleEntity.FieldRoleId, roleId));
            dbParameters.Add(DbHelper.MakeParameter(BaseUserRoleEntity.FieldDeletionStateCode, 0));
            dbParameters.Add(DbHelper.MakeParameter(BaseUserEntity.FieldCompanyId, companyId));
            result = this.Fill(commandText, dbParameters.ToArray());

            return result;
        }

        public bool IsAdministrator(string userId)
        {
            /*
            bool result = false;
            string userRoleTable = UserInfo.SystemCode + "UserRole";
            string roleTable = UserInfo.SystemCode + "Role";
            // 被删除的角色不应该显示出来
            string sqlQuery = @"SELECT COUNT(1) FROM " + roleTable + " WHERE Id IN (SELECT RoleId FROM " + userRoleTable + " WHERE UserId = " + userId + " AND Enabled = 1 AND DeletionStateCode = 0) AND Enabled = 1 AND DeletionStateCode = 0 AND (Code = 'Administrators' OR RealName = 'Administrators')";
            object isIsAdministrator = DbHelper.ExecuteScalar(sqlQuery);
            result = int.Parse(isIsAdministrator.ToString()) > 0;
            return result;
            */

            bool result = false;

            string commandText = @"SELECT COUNT(1) "
                                 + " FROM " + BaseUserEntity.TableName
                                + " WHERE Id = " + DbHelper.GetParameter(BaseUserEntity.FieldId)
                                          + " AND " + BaseUserEntity.FieldEnabled + " = " + DbHelper.GetParameter(BaseUserEntity.FieldEnabled)
                                          + " AND " + BaseUserEntity.FieldDeletionStateCode + " = " + DbHelper.GetParameter(BaseUserEntity.FieldDeletionStateCode)
                                          + " AND IsAdministrator = 1";
            List<IDbDataParameter> dbParameters = new List<IDbDataParameter>();
            dbParameters.Add(DbHelper.MakeParameter(BaseUserEntity.FieldId, userId));
            dbParameters.Add(DbHelper.MakeParameter(BaseUserEntity.FieldEnabled, 1));
            dbParameters.Add(DbHelper.MakeParameter(BaseUserEntity.FieldDeletionStateCode, 0));
            object isIsAdministrator = DbHelper.ExecuteScalar(commandText, dbParameters.ToArray());
            result = int.Parse(isIsAdministrator.ToString()) > 0;

            return result;
        }

        public string[] GetUserIdsInRoleId(string roleId)
        {
            return GetUserIdsInRoleId(UserInfo.SystemCode, roleId);
        }

        /// <summary>
        /// 获取用户主键数组
        /// </summary>
        /// <param name="roleIds">角色主键数组</param>
        /// <returns>用户主键数组</returns>
        public string[] GetUserIds(string[] roleIds)
        {
            return GetUserIds("Base", roleIds);
        }

        #region public string[] GetUserIdsByRole(string systemCode, string roleCode, string companyId = null) 按角色编号获得用户主键数组
        /// <summary>
        /// 按角色编号获得用户主键数组
        /// </summary>
        /// <param name="systemCode">系统编号</param>
        /// <param name="roleCode">角色编号</param>
        /// <param name="companyId">公司主键</param>
        /// <returns>主键数组</returns>
        public string[] GetUserIdsByRole(string systemCode, string roleCode, string companyId = null)
        {
            string[] result = null;
            string roleId = BaseRoleManager.GetIdByCodeByCache(systemCode, roleCode);
            if (!string.IsNullOrEmpty(roleId))
            {
                result = GetUserIdsInRoleId(systemCode, roleId, companyId);
            }

            return result;
        }
        #endregion

        #region public string[] GetUserIdsInRoleId(string systemCode, string roleId, string companyId = null) 按角色主键获得用户主键数组
        /// <summary>
        /// 按角色主键获得用户主键数组
        /// </summary>
        /// <param name="systemCode">系统编号</param>
        /// <param name="roleId">角色主键</param>
        /// <param name="companyId">公司主键</param>
        /// <returns>主键数组</returns>
        public string[] GetUserIdsInRoleId(string systemCode, string roleId, string companyId = null)
        {
            string[] result = null;

            string tableName = "BaseUserRole";
            if (!string.IsNullOrEmpty(systemCode))
            {
                tableName = systemCode + "UserRole";
            }

            // 需要显示未被删除的用户
            string sqlQuery = "SELECT UserId FROM " + tableName + " WHERE RoleId = " + DbHelper.GetParameter(BaseUserRoleEntity.FieldRoleId) + " AND DeletionStateCode = 0 "
                              + " AND ( UserId IN (  SELECT " + BaseUserEntity.FieldId
                                                 + "   FROM " + BaseUserEntity.TableName
                                                 + "  WHERE " + BaseUserEntity.FieldEnabled + " = 1 " + BaseUserEntity.FieldDeletionStateCode + " = 0 ";

            List<IDbDataParameter> dbParameters = new List<IDbDataParameter>();
            dbParameters.Add(DbHelper.MakeParameter(BaseUserRoleEntity.FieldRoleId, roleId));

            if (!string.IsNullOrWhiteSpace(companyId))
            {
                sqlQuery += " AND " + BaseUserEntity.FieldCompanyId + " = " + DbHelper.GetParameter(BaseUserEntity.FieldCompanyId);
                dbParameters.Add(DbHelper.MakeParameter(BaseUserEntity.FieldCompanyId, companyId));
            }
            sqlQuery += " ) )";

            // var dt = DbHelper.Fill(sqlQuery);
            // return BaseBusinessLogic.FieldToArray(dt, BaseUserRoleEntity.FieldUserId).Distinct<string>().Where(t => !string.IsNullOrEmpty(t)).ToArray();

            List<string> userIds = new List<string>();
            using (IDataReader dataReader = DbHelper.ExecuteReader(sqlQuery, dbParameters.ToArray()))
            {
                while (dataReader.Read())
                {
                    userIds.Add(dataReader["UserId"].ToString());
                }
            }
            result = userIds.ToArray();

            return result;
        }
        #endregion

        /// <summary>
        /// 获取用户主键数组
        /// 2015-11-03 吉日嘎拉 优化程序、用ExecuteReader提高性能
        /// </summary>
        /// <param name="systemCode">系统编号</param>
        /// <param name="roleIds">角色主键数组</param>
        /// <returns>用户主键数组</returns>
        public string[] GetUserIds(string systemCode, string[] roleIds)
        {
            string[] result = null;

            if (roleIds != null && roleIds.Length > 0)
            {
                // 需要显示未被删除的用户
                string tableName = systemCode + "UserRole";
                string commandText = "SELECT UserId FROM " + tableName + " WHERE RoleId IN (" + StringUtil.ArrayToList(roleIds) + ") "
                                + "  AND (UserId IN (SELECT Id FROM " + BaseUserEntity.TableName + " WHERE DeletionStateCode = 0)) AND (DeletionStateCode = 0)";

                List<string> ids = new List<string>();
                using (IDataReader dr = DbHelper.ExecuteReader(commandText))
                {
                    while (dr.Read())
                    {
                        ids.Add(dr["UserId"].ToString());
                    }
                }
                // 这里不需要有重复数据、用程序的方式把重复的数据去掉
                // result = BaseBusinessLogic.FieldToArray(dt, BaseUserRoleEntity.FieldUserId).Distinct<string>().Where(t => !string.IsNullOrEmpty(t)).ToArray();
                result = ids.ToArray();
            }

            return result;
        }

        //
        // 加入到角色
        //

        /*
        public string AddToRole(string systemCode, string userId, string roleName, bool enabled = true)
        {
            string result = string.Empty;

            string roleId = BaseRoleManager.GetIdByNameByCache(systemCode, roleName);
            if (!string.IsNullOrEmpty(roleId))
            {
                result = AddToRoleById(systemCode, userId, roleId, enabled);
            }
            
            return result;
        }

        public int AddToRole(string systemCode, string userId, string[] roleIds, bool enabled = true)
        {
            int result = 0;

            for (int i = 0; i < roleIds.Length; i++)
            {
                this.AddToRoleById(systemCode, userId, roleIds[i], enabled);
                result++;
            }

            return result;
        }

        public int AddToRole(string systemCode, string[] userIds, string roleId)
        {
            int result = 0;

            for (int i = 0; i < userIds.Length; i++)
            {
                this.AddToRoleById(systemCode, userIds[i], roleId);
                result++;
            }

            return result;
        }
        */

        public int AddToRole(string systemCode, string[] userIds, string[] roleIds, bool enabled = true)
        {
            int result = 0;
            for (int i = 0; i < userIds.Length; i++)
            {
                for (int j = 0; j < roleIds.Length; j++)
                {
                    this.AddToRoleById(systemCode, userIds[i], roleIds[j], enabled);
                    result++;
                }
            }
            return result;
        }

        #region public string AddToRole(string systemCode, string userId, string roleId, bool enabled = true) 为了提高授权的运行速度
        /// <summary>
        /// 为了提高授权的运行速度
        /// </summary>
        /// <param name="systemCode">系统编号</param>
        /// <param name="userId">用户主键</param>
        /// <param name="roleId">角色主键</param>
        /// <param name="enabled">有效状态</param>
        /// <returns>主键</returns>
        public string AddToRoleById(string systemCode, string userId, string roleId, bool enabled = true)
        {
            string result = string.Empty;

            if (string.IsNullOrEmpty(systemCode))
            {
                systemCode = "Base";
            }
            string tableName = string.Empty;
            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(roleId))
            {
                BaseUserRoleEntity entity = new BaseUserRoleEntity();
                entity.UserId = userId;
                entity.RoleId = roleId;
                entity.Enabled = enabled ? 1 : 0;
                entity.DeletionStateCode = 0;
                // 2016-03-02 吉日嘎拉 增加按公司可以区别数据的功能。
                if (this.DbHelper.CurrentDbType == CurrentDbType.MySql)
                {
                    entity.CompanyId = BaseUserManager.GetCompanyIdByCache(userId);
                }
                // 2015-12-05 吉日嘎拉 把修改人记录起来，若是新增加的
                if (this.UserInfo != null)
                {
                    entity.CreateUserId = this.UserInfo.Id;
                    entity.CreateBy = this.UserInfo.RealName;
                    entity.CreateOn = System.DateTime.Now;
                    entity.ModifiedUserId = this.UserInfo.Id;
                    entity.ModifiedBy = this.UserInfo.RealName;
                    entity.ModifiedOn = System.DateTime.Now;
                }
                tableName = systemCode + "UserRole";
                BaseUserRoleManager manager = new BaseUserRoleManager(this.DbHelper, this.UserInfo, tableName);
                result = manager.Add(entity);
            }

            return result;
        }
        #endregion


        //
        //  从角色中移除用户
        //

        #region public int RemoveFormRole(string systemCode, string userId, string roleId) 将用户从角色移除
        /// <summary>
        /// 将用户从角色移除
        /// </summary>
        /// <param name="systemCode">系统编号</param>
        /// <param name="userId">用户主键</param>
        /// <param name="roleId">角色主键</param>
        /// <returns>影响行数</returns>
        public int RemoveFormRole(string systemCode, string userId, string roleId)
        {
            string tableName = BaseUserRoleEntity.TableName;
            if (!string.IsNullOrWhiteSpace(systemCode))
            {
                tableName = systemCode + "UserRole";
            }

            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>(BaseUserRoleEntity.FieldUserId, userId));
            parameters.Add(new KeyValuePair<string, object>(BaseUserRoleEntity.FieldRoleId, roleId));
            BaseUserRoleManager manager = new BaseUserRoleManager(this.DbHelper, this.UserInfo, tableName);
            return manager.Delete(parameters);
        }
        #endregion

        /*
        public int RemoveFormRole(string systemCode, string userId, string[] roleIds)
        {
            int result = 0;
            for (int i = 0; i < roleIds.Length; i++)
            {
                result += this.RemoveFormRole(systemCode, userId, roleIds[i]);
            }
            return result;
        }

        public int RemoveFormRole(string systemCode, string[] userIds, string roleId)
        {
            int result = 0;
            for (int i = 0; i < userIds.Length; i++)
            {
                // 移除用户角色
                result += this.RemoveFormRole(systemCode, userIds[i], roleId);
            }
            return result;
        }
        */

        public int RemoveFormRole(string systemCode, string[] userIds, string[] roleIds)
        {
            int result = 0;

            for (int i = 0; i < userIds.Length; i++)
            {
                for (int j = 0; j < roleIds.Length; j++)
                {
                    result += this.RemoveFormRole(systemCode, userIds[i], roleIds[j]);
                }
            }

            return result;
        }

        /// <summary>
        /// 轮循计数器
        /// </summary>
        public static int userIndex = 0;

        /// <summary>
        /// 从某个角色列表中轮循获取一个用户，在线的用户优先。
        /// </summary>
        /// <param name="roleCode">角色编号</param>
        /// <returns>用户主键</returns>
        public string GetRandomUserId(string systemCode, string roleCode)
        {
            string result = string.Empty;

            // 先不管是否在线，总需要能发一个人再说
            string[] userIds = GetUserIdsByRole(systemCode, roleCode);
            if (userIds != null && userIds.Length > 0)
            {
                int index = userIndex % userIds.Length;
                result = userIds[index];

                // 接着再判断是否有人在线，若有在线的，发给在线的用户
                BaseUserLogOnManager userLogOnManager = new BaseUserLogOnManager(this.DbHelper);
                userIds = userLogOnManager.GetOnLineUserIds(userIds);
                if (userIds != null && userIds.Length > 0)
                {
                    index = userIndex % userIds.Length;
                    result = userIds[index];
                }
            }
            userIndex++;

            return result;
        }
    }
}