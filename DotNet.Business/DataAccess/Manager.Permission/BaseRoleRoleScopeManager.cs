//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd.  
//-----------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;

namespace DotNet.Business
{
    using DotNet.Model;
    using DotNet.Utilities;

    /// <summary>
    /// BaseRoleScopeManager
    /// 角色权限域
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
    public partial class BaseRoleScopeManager : BaseManager, IBaseManager
    {
        ////
        ////
        //// 授权范围管理部分
        ////
        ////

        #region public string[] GetRoleIds(string roleId, string permissionCode) 获取员工的权限主键数组
        /// <summary>
        /// 获取员工的权限主键数组
        /// </summary>
        /// <param name="roleId">员工主键</param>
        /// <param name="permissionCode">权限编号</param>
        /// <returns>主键数组</returns>
        public string[] GetRoleIds(string roleId, string permissionCode)
        {
            string[] result = null;
            string roleTableName = this.UserInfo.SystemCode + "Role";
            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldResourceCategory, roleTableName));
            parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldResourceId, roleId));
            parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldTargetCategory, roleTableName));
            parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldPermissionId, this.GetPermissionIdByCode(permissionCode)));
            var dt = this.GetDataTable(parameters);
            result = BaseBusinessLogic.FieldToArray(dt, BasePermissionScopeEntity.FieldTargetId).Distinct<string>().Where(t => !string.IsNullOrEmpty(t)).ToArray();
            return result;
        }
        #endregion

        //
        // 授予授权范围的实现部分
        //

        #region private string GrantRole(BasePermissionScopeManager manager, string id, string roleId, string grantRoleId) 为了提高授权的运行速度
        /// <summary>
        /// 为了提高授权的运行速度
        /// </summary>
        /// <param name="manager">权限域读写器</param>
        /// <param name="roleId">员工主键</param>
        /// <param name="grantRoleId">权限主键</param>
        /// <param name="permissionCode">权限编号</param>
        /// <returns>主键</returns>
        private string GrantRole(BasePermissionScopeManager permissionScopeManager, string roleId, string grantRoleId, string permissionCode)
        {
            string roleTableName = this.UserInfo.SystemCode + "Role";

            string result = string.Empty;
            BasePermissionScopeEntity entity = new BasePermissionScopeEntity();
            entity.PermissionId = this.GetPermissionIdByCode(permissionCode);
            entity.ResourceCategory = roleTableName;
            entity.ResourceId = roleId;
            entity.TargetCategory = roleTableName;
            entity.TargetId = grantRoleId;
            entity.Enabled = 1;
            entity.DeletionStateCode = 0;
            return permissionScopeManager.Add(entity);
        }
        #endregion

        #region public string GrantRole(string roleId, string grantRoleId, string permissionCode) 角色授予权限
        /// <summary>
        /// 角色授予权限
        /// </summary>
        /// <param name="roleId">员工主键</param>
        /// <param name="organizeId">权组织机构限主键</param>
        /// <param name="permissionCode">权限编号</param>
        /// <returns>主键</returns>
        public string GrantRole(string roleId, string grantRoleId, string permissionCode)
        {
            BasePermissionScopeManager permissionScopeManager = new BasePermissionScopeManager(DbHelper, UserInfo);
            return this.GrantRole(permissionScopeManager, roleId, grantRoleId, permissionCode);
        }
        #endregion

        public int GrantRoles(string roleId, string[] grantRoleIds, string permissionCode)
        {
            int result = 0;

            BasePermissionScopeManager permissionScopeManager = new BasePermissionScopeManager(DbHelper, UserInfo, this.CurrentTableName);
            for (int i = 0; i < grantRoleIds.Length; i++)
            {
                this.GrantRole(permissionScopeManager, roleId, grantRoleIds[i], permissionCode);
                result++;
            }

            return result;
        }

        public int GrantRoles(string[] roleIds, string grantRoleId, string permissionCode)
        {
            int result = 0;
            BasePermissionScopeManager permissionScopeManager = new BasePermissionScopeManager(DbHelper, UserInfo, this.CurrentTableName);
            for (int i = 0; i < roleIds.Length; i++)
            {
                this.GrantRole(permissionScopeManager, roleIds[i], grantRoleId, permissionCode);
                result++;
            }
            return result;
        }

        public int GrantRoles(string[] roleIds, string[] grantRoleIds, string permissionCode)
        {
            int result = 0;
            BasePermissionScopeManager permissionScopeManager = new BasePermissionScopeManager(DbHelper, UserInfo, this.CurrentTableName);
            for (int i = 0; i < roleIds.Length; i++)
            {
                for (int j = 0; j < grantRoleIds.Length; j++)
                {
                    this.GrantRole(permissionScopeManager, roleIds[i], grantRoleIds[j], permissionCode);
                    result++;
                }
            }
            return result;
        }


        //
        //  撤销授权范围的实现部分
        //

        #region private int RevokeRole(BasePermissionScopeManager manager, string roleId, string revokeRoleId, string permissionCode) 为了提高授权的运行速度
        /// <summary>
        /// 为了提高授权的运行速度
        /// </summary>
        /// <param name="manager">权限域读写器</param>
        /// <param name="roleId">员工主键</param>
        /// <param name="revokeRoleId">权限主键</param>
        /// <param name="permissionCode">权限编号</param>
        /// <returns>主键</returns>
        private int RevokeRole(BasePermissionScopeManager permissionScopeManager, string roleId, string revokeRoleId, string permissionCode)
        {
            string roleTableName = this.UserInfo.SystemCode + "Role";
            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldResourceCategory, roleTableName));
            parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldResourceId, roleId));
            parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldTargetCategory, roleTableName));
            parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldTargetId, revokeRoleId));
            parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldPermissionId, this.GetPermissionIdByCode(permissionCode)));
            return permissionScopeManager.Delete(parameters);
        }
        #endregion

        #region public int RevokeRole(string roleId, string revokeRoleId, string permissionCode) 角色撤销授权
        /// <summary>
        /// 角色撤销授权
        /// </summary>
        /// <param name="roleId">员工主键</param>
        /// <param name="permissionCode">权限编号</param>
        /// <returns>主键</returns>
        public int RevokeRole(string roleId, string revokeRoleId, string permissionCode)
        {
            BasePermissionScopeManager permissionScopeManager = new BasePermissionScopeManager(DbHelper, UserInfo, this.CurrentTableName);
            return this.RevokeRole(permissionScopeManager, roleId, revokeRoleId, permissionCode);
        }
        #endregion

        public int RevokeRoles(string roleId, string[] revokeRoleIds, string permissionCode)
        {
            int result = 0;
            BasePermissionScopeManager permissionScopeManager = new BasePermissionScopeManager(DbHelper, UserInfo, this.CurrentTableName);
            for (int i = 0; i < revokeRoleIds.Length; i++)
            {
                this.RevokeRole(permissionScopeManager, roleId, revokeRoleIds[i], permissionCode);
                result++;
            }
            return result;
        }

        public int RevokeRoles(string[] roleIds, string revokeRoleId, string permissionCode)
        {
            int result = 0;
            BasePermissionScopeManager permissionScopeManager = new BasePermissionScopeManager(DbHelper, UserInfo, this.CurrentTableName);
            for (int i = 0; i < roleIds.Length; i++)
            {
                this.RevokeRole(permissionScopeManager, roleIds[i], revokeRoleId, permissionCode);
                result++;
            }
            return result;
        }

        public int RevokeRoles(string[] roleIds, string[] revokeRoleIds, string permissionCode)
        {
            int result = 0;
            BasePermissionScopeManager permissionScopeManager = new BasePermissionScopeManager(DbHelper, UserInfo, this.CurrentTableName);
            for (int i = 0; i < roleIds.Length; i++)
            {
                for (int j = 0; j < revokeRoleIds.Length; j++)
                {
                    this.RevokeRole(permissionScopeManager, roleIds[i], revokeRoleIds[j], permissionCode);
                    result++;
                }
            }
            return result;
        }
    }
}