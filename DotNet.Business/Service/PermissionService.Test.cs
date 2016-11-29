//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd.  
//-----------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Reflection;

namespace DotNet.Business
{
    using DotNet.IService;
    using DotNet.Model;
    using DotNet.Utilities;

    /// <summary>
    /// PermissionService
    /// 权限判断服务
    /// 
    /// 修改记录
    /// 
    ///		2015.02.10 版本：3.2 JiRiGaLa 授权函数代码整理，直接可以指定哪个子系统。
    ///		2011.06.04 版本：3.1 JiRiGaLa 整理日志功能。
    ///		2009.09.25 版本：3.0 JiRiGaLa Resource.ManagePermission 自动判断增加。
    ///		2008.12.12 版本：2.0 JiRiGaLa 进行了彻底的改进。
    ///		2008.05.30 版本：1.0 JiRiGaLa 创建权限判断服务。
    ///		
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2015.02.10</date>
    /// </author> 
    /// </summary>
    public partial class PermissionService : IPermissionService
    {
        /////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// 测试权限用的
        /////////////////////////////////////////////////////////////////////////////////////////////////////////


        #region public string AddPermission(BaseUserInfo userInfo, string permissionCode)
        /// <summary>
        /// 添加操作权限
        /// </summary>
        /// <param name="userInfo">用户</param>
        /// <param name="permissionCode">权限编号</param>
        /// <returns>主键</returns>
        public string AddPermission(BaseUserInfo userInfo, string permissionCode)
        {
            string result = string.Empty;

            var parameter = ServiceInfo.Create(userInfo, MethodBase.GetCurrentMethod());
            ServiceUtil.ProcessUserCenterWriteDb(userInfo, parameter, (dbHelper) =>
            {
                var manager = new BaseModuleManager(dbHelper, userInfo);
                string statusCode = string.Empty;
                BaseModuleEntity permissionEntity = new BaseModuleEntity();
                permissionEntity.Code = permissionCode;
                permissionEntity.Enabled = 1;
                permissionEntity.AllowDelete = 1;
                permissionEntity.AllowEdit = 1;
                permissionEntity.IsScope = 0;
                result = manager.Add(permissionEntity, out statusCode);
            });

            return result;
        }
        #endregion

        #region public int DeletePermission(BaseUserInfo userInfo, string permissionCode)
        /// <summary>
        /// 删除权限
        /// </summary>
        /// <param name="userInfo">用户</param>
        /// <param name="permissionCode">权限编号</param>
        /// <returns>影响行数</returns>
        public int DeletePermission(BaseUserInfo userInfo, string permissionCode)
        {
            int result = 0;

            var parameter = ServiceInfo.Create(userInfo, MethodBase.GetCurrentMethod());
            ServiceUtil.ProcessUserCenterWriteDb(userInfo, parameter, (dbHelper) =>
            {
                var manager = new BaseModuleManager(dbHelper, userInfo);
                string id = manager.GetId(new KeyValuePair<string, object>(BaseModuleEntity.FieldCode, permissionCode));
                if (!String.IsNullOrEmpty(id))
                {
                    // 在删除时，可能会把相关的其他配置权限会删除掉，所以需要调用这个方法。
                    result = manager.Delete(id);
                }
            });
            return result;
        }
        #endregion

        #region public string GrantUserPermission(BaseUserInfo userInfo, string userName, string permissionCode)
        /// <summary>
        /// 给用户权限
        /// </summary>
        /// <param name="userInfo">用户</param>
        /// <param name="userName">用户名</param>
        /// <param name="permissionCode">权限编号</param>
        /// <returns>主键</returns>
        public string GrantUserPermission(BaseUserInfo userInfo, string userName, string permissionCode)
        {
            string result = string.Empty;

            var parameter = ServiceInfo.Create(userInfo, MethodBase.GetCurrentMethod());
            ServiceUtil.ProcessUserCenterWriteDb(userInfo, parameter, (dbHelper) =>
            {
                var userManager = new BaseUserManager(dbHelper, userInfo);
                string userId = userManager.GetId(new KeyValuePair<string, object>(BaseUserEntity.FieldUserName, userName));

                if (!String.IsNullOrEmpty(userId))
                {
                    var userPermissionManager = new BaseUserPermissionManager(dbHelper, userInfo);
                    result = userPermissionManager.GrantByPermissionCode(userInfo.SystemCode, userId, permissionCode);
                }
            });

            return result;
        }
        #endregion

        #region public string GrantUserPermission(BaseUserInfo userInfo, string systemCode, string userId, string permissionCode)
        /// <summary>
        /// 给用户权限（分子系统）
        /// </summary>
        /// <param name="userInfo">用户</param>
        /// <param name="systemCode">系统编号</param>
        /// <param name="userId">用户主键</param>
        /// <param name="permissionCode">权限编号</param>
        /// <returns>主键</returns>
        public string GrantUserPermission(BaseUserInfo userInfo, string systemCode, string userId, string permissionCode)
        {
            string result = string.Empty;

            var parameter = ServiceInfo.Create(userInfo, MethodBase.GetCurrentMethod());
            ServiceUtil.ProcessUserCenterWriteDb(userInfo, parameter, (dbHelper) =>
            {
                var userPermissionManager = new BaseUserPermissionManager(dbHelper, userInfo);
                result = userPermissionManager.GrantByPermissionCode(systemCode, userId, permissionCode);

            });
            return result;
        }
        #endregion

        #region public int RevokeUserPermission(BaseUserInfo userInfo, string userName, string permissionCode)
        /// <summary>
        /// 撤销用户权限
        /// </summary>
        /// <param name="userInfo">用户</param>
        /// <param name="userName">用户名</param>
        /// <param name="permissionCode">权限编号</param>
        /// <returns>主键</returns>
        public int RevokeUserPermission(BaseUserInfo userInfo, string userName, string permissionCode)
        {
            int result = 0;

            var parameter = ServiceInfo.Create(userInfo, MethodBase.GetCurrentMethod());
            ServiceUtil.ProcessUserCenterWriteDb(userInfo, parameter, (dbHelper) =>
            {
                var userManager = new BaseUserManager(dbHelper, userInfo);
                string userId = userManager.GetId(new KeyValuePair<string, object>(BaseUserEntity.FieldUserName, userName));
                if (!String.IsNullOrEmpty(userId))
                {
                    var userPermissionManager = new BaseUserPermissionManager(dbHelper, userInfo);
                    result = userPermissionManager.RevokeByPermissionCode(userInfo.SystemCode, userId, permissionCode);
                }
            });

            return result;
        }
        #endregion

        #region public string AddRole(BaseUserInfo userInfo, string role)
        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="userInfo">用户</param>
        /// <param name="role">角色</param>
        /// <returns>主键</returns>
        public string AddRole(BaseUserInfo userInfo, string role)
        {
            string result = string.Empty;

            var parameter = ServiceInfo.Create(userInfo, MethodBase.GetCurrentMethod());
            ServiceUtil.ProcessUserCenterWriteDb(userInfo, parameter, (dbHelper) =>
            {
                var roleManager = new BaseRoleManager(dbHelper, userInfo);
                string statusCode = string.Empty;
                BaseRoleEntity roleEntity = new BaseRoleEntity();
                roleEntity.RealName = role;
                roleEntity.Enabled = 1;
                result = roleManager.Add(roleEntity, out statusCode);
            });
            return result;
        }
        #endregion

        #region public int DeleteRole(BaseUserInfo userInfo, string role)
        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="userInfo">用户</param>
        /// <param name="role">角色</param>
        /// <returns>影响行数</returns>
        public int DeleteRole(BaseUserInfo userInfo, string role)
        {
            int result = 0;

            var parameter = ServiceInfo.Create(userInfo, MethodBase.GetCurrentMethod());
            ServiceUtil.ProcessUserCenterWriteDb(userInfo, parameter, (dbHelper) =>
            {
                var roleManager = new BaseRoleManager(dbHelper, userInfo);
                string id = roleManager.GetId(new KeyValuePair<string, object>(BaseRoleEntity.FieldRealName, role));
                if (!String.IsNullOrEmpty(id))
                {
                    // 在删除时，可能会把相关的其他配置角色会删除掉，所以需要调用这个方法。
                    result = roleManager.Delete(id);
                }
            });
            return result;
        }
        #endregion

        #region public string AddUserToRole(BaseUserInfo userInfo, string userName, string roleName)
        /// <summary>
        /// 用户添加到角色
        /// </summary>
        /// <param name="userInfo">用户</param>
        /// <param name="roleName">角色名</param>
        /// <param name="permissionCode">权限编号</param>
        /// <returns>主键</returns>
        public string AddUserToRole(BaseUserInfo userInfo, string userName, string roleName)
        {
            string result = string.Empty;

            var parameter = ServiceInfo.Create(userInfo, MethodBase.GetCurrentMethod());
            ServiceUtil.ProcessUserCenterWriteDb(userInfo, parameter, (dbHelper) =>
            {
                var userManager = new BaseUserManager(dbHelper, userInfo);
                string userId = userManager.GetId(new KeyValuePair<string, object>(BaseUserEntity.FieldUserName, userName));
                var roleManager = new BaseRoleManager(dbHelper, userInfo);
                string roleId = roleManager.GetId(new KeyValuePair<string, object>(BaseRoleEntity.FieldRealName, roleName));
                if (!String.IsNullOrEmpty(userId) && !String.IsNullOrEmpty(roleId))
                {
                    userManager.AddToRole(userInfo.SystemCode, new string[] { userId }, new string[] { roleId });
                }
            });
            return result;
        }
        #endregion

        #region public int RemoveUserFromRole(BaseUserInfo userInfo, string userName, string roleName)
        /// <summary>
        /// 用户从角色移除
        /// </summary>
        /// <param name="userInfo">用户</param>
        /// <param name="roleName">角色名</param>
        /// <param name="permissionCode">权限编号</param>
        /// <returns>主键</returns>
        public int RemoveUserFromRole(BaseUserInfo userInfo, string userName, string roleName)
        {
            int result = 0;

            var parameter = ServiceInfo.Create(userInfo, MethodBase.GetCurrentMethod());
            ServiceUtil.ProcessUserCenterWriteDb(userInfo, parameter, (dbHelper) =>
            {
                var userManager = new BaseUserManager(dbHelper, userInfo);
                string userId = userManager.GetId(new KeyValuePair<string, object>(BaseUserEntity.FieldUserName, userName));
                var roleManager = new BaseRoleManager(dbHelper, userInfo);
                string roleId = roleManager.GetId(new KeyValuePair<string, object>(BaseRoleEntity.FieldRealName, roleName));
                if (!String.IsNullOrEmpty(userId) && !String.IsNullOrEmpty(roleId))
                {
                    result = userManager.RemoveFormRole(userInfo.SystemCode, userId, roleId);
                }
            });
            return result;
        }
        #endregion

        #region public string GrantRolePermission(BaseUserInfo userInfo, string roleName, string permissionCode)
        /// <summary>
        /// 给角色权限
        /// </summary>
        /// <param name="userInfo">用户</param>
        /// <param name="roleName">角色名</param>
        /// <param name="permissionCode">权限编号</param>
        /// <returns>主键</returns>
        public string GrantRolePermission(BaseUserInfo userInfo, string roleName, string permissionCode)
        {
            string result = string.Empty;

            var parameter = ServiceInfo.Create(userInfo, MethodBase.GetCurrentMethod());
            ServiceUtil.ProcessUserCenterWriteDb(userInfo, parameter, (dbHelper) =>
            {
                string roleId = BaseRoleManager.GetIdByCodeByCache(userInfo.SystemCode, roleName);
                string permissionId = BaseModuleManager.GetIdByCodeByCache(userInfo.SystemCode, permissionCode);
                if (!String.IsNullOrEmpty(roleId) && !String.IsNullOrEmpty(permissionId))
                {
                    var rolePermissionManager = new BaseRolePermissionManager(dbHelper, userInfo);
                    result = rolePermissionManager.Grant(userInfo.SystemCode, roleId, permissionId);
                }
            });

            return result;
        }
        #endregion

        #region public int RevokeRolePermission(BaseUserInfo userInfo, string roleName, string permissionCode)
        /// <summary>
        /// 撤销角色权限
        /// </summary>
        /// <param name="userInfo">用户</param>
        /// <param name="roleName">角色名</param>
        /// <param name="permissionCode">权限编号</param>
        /// <returns>主键</returns>
        public int RevokeRolePermission(BaseUserInfo userInfo, string roleName, string permissionCode)
        {
            int result = 0;

            var parameter = ServiceInfo.Create(userInfo, MethodBase.GetCurrentMethod());
            ServiceUtil.ProcessUserCenterWriteDb(userInfo, parameter, (dbHelper) =>
            {
                string roleId = BaseRoleManager.GetIdByNameByCache(userInfo.SystemCode, roleName);
                string permissionId = BaseModuleManager.GetIdByCodeByCache(userInfo.SystemCode, permissionCode);
                if (!String.IsNullOrEmpty(roleId) && !String.IsNullOrEmpty(permissionId))
                {
                    var rolePermissionManager = new BaseRolePermissionManager(dbHelper, userInfo);
                    result = rolePermissionManager.Revoke(userInfo.SystemCode, roleId, permissionId);
                }
            });

            return result;
        }
        #endregion
    }
}