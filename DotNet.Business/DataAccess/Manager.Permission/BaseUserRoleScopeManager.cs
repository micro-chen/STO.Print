//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd.  
//-----------------------------------------------------------------

using System.Collections.Generic;

namespace DotNet.Business
{
    using DotNet.Model;

    /// <summary>
    /// BaseUserScopeManager
    /// 用户对角色权限域
    /// 
    /// 修改记录
    ///
    ///     2011.03.13 版本：2.0 JiRiGaLa 重新整理代码。
    ///     2008.05.24 版本：1.0 JiRiGaLa 创建主键。
    ///
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2011.03.13</date>
    /// </author>
    /// </summary>
    public partial class BaseUserScopeManager : BaseManager, IBaseManager
    {
        ////
        ////
        //// 授权范围管理部分
        ////
        ////

        #region public string[] GetRoleIds(string systemCode, string userId, string permissionCode) 获取角色的权限主键数组
        /// <summary>
        /// 获取角色的权限主键数组
        /// </summary>
        /// <param name="systemCode">系统编号</param>
        /// <param name="userId">用户主键</param>
        /// <param name="permissionCode">权限编号</param>
        /// <returns>主键数组</returns>
        public string[] GetRoleIds(string systemCode, string userId, string permissionCode)
        {
            string[] result = null;

            string permissionId = BaseModuleManager.GetIdByCodeByCache(systemCode, permissionCode);
            if (!string.IsNullOrEmpty(permissionId))
            {
                string roleTableName = systemCode + "Role";
                List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
                parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldResourceCategory, BaseUserEntity.TableName));
                parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldResourceId, userId));
                parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldTargetCategory, roleTableName));
                parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldPermissionId, permissionId));

                // 20130605 JiRiGaLa 这个运行效率更高一些
                // this.CurrentTableName = systemCode + "PermissionScope";
                result = this.GetProperties(parameters, BasePermissionScopeEntity.FieldTargetId);
                // var result = this.GetDataTable(parameters);
                // result = BaseBusinessLogic.FieldToArray(result, BasePermissionScopeEntity.FieldTargetId).Distinct<string>().Where(t => !string.IsNullOrEmpty(t)).ToArray();
            }

            return result;
        }
        #endregion

        //
        // 授予授权范围的实现部分
        //

        #region private string GrantRole(BasePermissionScopeManager manager, string id, string userId, string grantRoleId) 为了提高授权的运行速度
        /// <summary>
        /// 为了提高授权的运行速度
        /// </summary>
        /// <param name="manager">权限范围管理器</param>
        /// <param name="systemCode">系统编号</param>
        /// <param name="userId">用户主键</param>
        /// <param name="grantRoleId">权限主键</param>
        /// <param name="permissionCode">权限编号</param>
        /// <returns>主键</returns>
        private string GrantRole(BasePermissionScopeManager manager, string systemCode, string userId, string grantRoleId, string permissionCode)
        {
            string result = string.Empty;

            string permissionId = BaseModuleManager.GetIdByCodeByCache(systemCode, permissionCode);
            if (!string.IsNullOrEmpty(permissionId))
            {
                string roleTableName = systemCode + "Role";
                BasePermissionScopeEntity entity = new BasePermissionScopeEntity();
                entity.PermissionId = permissionId;
                entity.ResourceCategory = BaseUserEntity.TableName;
                entity.ResourceId = userId;
                entity.TargetCategory = roleTableName;
                entity.TargetId = grantRoleId;
                entity.Enabled = 1;
                entity.DeletionStateCode = 0;
                result = manager.Add(entity);
            }

            return result;
        }
        #endregion

        #region public string GrantRole(string userId, string result) 员工授予权限
        /// <summary>
        /// 员工授予权限
        /// </summary>
        /// <param name="userId">用户主键</param>
        /// <param name="result">权限主键</param>
        /// <param name="organizeId">权组织机构限主键</param>
        /// <returns>主键</returns>
        public string GrantRole(string systemCode, string userId, string grantRoleId, string permissionCode)
        {
            BasePermissionScopeManager permissionScopeManager = new BasePermissionScopeManager(DbHelper, UserInfo);
            return this.GrantRole(permissionScopeManager, systemCode, userId, grantRoleId, permissionCode);
        }
        #endregion

        public int GrantRoles(string systemCode, string userId, string[] grantRoleIds, string permissionCode)
        {
            int result = 0;
            BasePermissionScopeManager manager = new BasePermissionScopeManager(DbHelper, UserInfo, this.CurrentTableName);
            for (int i = 0; i < grantRoleIds.Length; i++)
            {
                this.GrantRole(manager, systemCode, userId, grantRoleIds[i], permissionCode);
                result++;
            }
            return result;
        }

        public int GrantRoles(string systemCode, string[] userIds, string grantRoleId, string permissionCode)
        {
            int result = 0;
            BasePermissionScopeManager manager = new BasePermissionScopeManager(DbHelper, UserInfo, this.CurrentTableName);
            for (int i = 0; i < userIds.Length; i++)
            {
                this.GrantRole(manager, systemCode, userIds[i], grantRoleId, permissionCode);
                result++;
            }
            return result;
        }

        public int GrantRoles(string systemCode, string[] userIds, string[] grantRoleIds, string permissionCode)
        {
            int result = 0;
            BasePermissionScopeManager manager = new BasePermissionScopeManager(DbHelper, UserInfo, this.CurrentTableName);
            for (int i = 0; i < userIds.Length; i++)
            {
                for (int j = 0; j < grantRoleIds.Length; j++)
                {
                    this.GrantRole(manager, systemCode, userIds[i], grantRoleIds[j], permissionCode);
                    result++;
                }
            }
            return result;
        }


        //
        //  撤销授权范围的实现部分
        //

        #region private int RevokeRole(BasePermissionScopeManager manager, string userId, string revokeRoleId, string permissionCode) 为了提高授权的运行速度
        /// <summary>
        /// 为了提高授权的运行速度
        /// </summary>
        /// <param name="manager">权限域读写器</param>
        /// <param name="userId">用户主键</param>
        /// <param name="revokeRoleId">权限主键</param>
        /// <param name="permissionCode">权限编号</param>
        /// <returns>主键</returns>
        private int RevokeRole(BasePermissionScopeManager manager, string systemCode, string userId, string revokeRoleId, string permissionCode)
        {
            string permissionId = BaseModuleManager.GetIdByCodeByCache(systemCode, permissionCode);
            string roleTableName = UserInfo.SystemCode + "Role";
            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldResourceCategory, BaseUserEntity.TableName));
            parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldResourceId, userId));
            parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldTargetCategory, roleTableName));
            parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldTargetId, revokeRoleId));
            parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldPermissionId, permissionId));            
            return manager.Delete(parameters);
        }
        #endregion

        #region public int RevokeRole(string userId, string revokeRoleId, string permissionCode) 员工撤销授权
        /// <summary>
        /// 员工撤销授权
        /// </summary>
        /// <param name="userId">用户主键</param>
        /// <param name="revokeRoleId">权限主键</param>
        /// <param name="permissionCode">权限编号</param>
        /// <returns>主键</returns>
        public int RevokeRole(string systemCode, string userId, string revokeRoleId, string permissionCode)
        {
            BasePermissionScopeManager permissionScopeManager = new BasePermissionScopeManager(DbHelper, UserInfo, this.CurrentTableName);
            return this.RevokeRole(permissionScopeManager, systemCode, userId, revokeRoleId, permissionCode);
        }
        #endregion

        public int RevokeRoles(string systemCode, string userId, string[] revokeRoleIds, string permissionCode)
        {
            int result = 0;
            BasePermissionScopeManager manager = new BasePermissionScopeManager(DbHelper, UserInfo, this.CurrentTableName);
            for (int i = 0; i < revokeRoleIds.Length; i++)
            {
                this.RevokeRole(manager, systemCode, userId, revokeRoleIds[i], permissionCode);
                result++;
            }
            return result;
        }

        public int RevokeRoles(string systemCode, string[] userIds, string revokeRoleId, string permissionCode)
        {
            int result = 0;
            BasePermissionScopeManager manager = new BasePermissionScopeManager(DbHelper, UserInfo, this.CurrentTableName);
            for (int i = 0; i < userIds.Length; i++)
            {
                this.RevokeRole(manager, systemCode, userIds[i], revokeRoleId, permissionCode);
                result++;
            }
            return result;
        }

        public int RevokeRoles(string systemCode, string[] userIds, string[] revokeRoleIds, string permissionCode)
        {
            int result = 0;
            BasePermissionScopeManager manager = new BasePermissionScopeManager(DbHelper, UserInfo, this.CurrentTableName);
            for (int i = 0; i < userIds.Length; i++)
            {
                for (int j = 0; j < revokeRoleIds.Length; j++)
                {
                    this.RevokeRole(manager, systemCode, userIds[i], revokeRoleIds[j], permissionCode);
                    result++;
                }
            }
            return result;
        }
    }
}