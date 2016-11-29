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
    /// 角色操作权限域
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

        #region public string[] GetPermissionIds(string roleId, string permissionCode) 获取员工的权限主键数组
        /// <summary>
        /// 获取员工的权限主键数组
        /// </summary>
        /// <param name="roleId">员工主键</param>
        /// <param name="permissionCode">权限编号</param>
        /// <returns>主键数组</returns>
        public string[] GetPermissionIds(string roleId, string permissionCode)
        {
            string[] result = null;

            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldResourceCategory, BaseRoleEntity.TableName));
            parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldResourceId, roleId));
            parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldTargetCategory, BaseModuleEntity.TableName));
            parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldPermissionId, this.GetPermissionIdByCode(permissionCode)));

            var dt = this.GetDataTable(parameters);
            result = BaseBusinessLogic.FieldToArray(dt, BasePermissionScopeEntity.FieldTargetId).Distinct<string>().Where(t => !string.IsNullOrEmpty(t)).ToArray();
            return result;
        }
        #endregion

        //
        // 授予授权范围的实现部分
        //

        #region private string GrantPermission(BasePermissionScopeManager manager, string id, string roleId, string grantPermissionId) 为了提高授权的运行速度
        /// <summary>
        /// 为了提高授权的运行速度
        /// </summary>
        /// <param name="manager">权限域读写器</param>
        /// <param name="roleId">角色主键</param>
        /// <param name="grantPermissionId">权限主键</param>
        /// <param name="permissionCode">权限编号</param>
        /// <returns>主键</returns>
        private string GrantPermission(BasePermissionScopeManager permissionScopeManager, string roleId, string grantPermissionId, string permissionCode)
        {
            string result = string.Empty;
            BasePermissionScopeEntity resourcePermissionScopeEntity = new BasePermissionScopeEntity();
            resourcePermissionScopeEntity.PermissionId = this.GetPermissionIdByCode(permissionCode);
            resourcePermissionScopeEntity.ResourceCategory = BaseRoleEntity.TableName;
            resourcePermissionScopeEntity.ResourceId = roleId;
            resourcePermissionScopeEntity.TargetCategory = BaseModuleEntity.TableName;
            resourcePermissionScopeEntity.TargetId = grantPermissionId;
            resourcePermissionScopeEntity.Enabled = 1;
            resourcePermissionScopeEntity.DeletionStateCode = 0;
            return permissionScopeManager.Add(resourcePermissionScopeEntity);
        }
        #endregion

        #region public string GrantPermission(string roleId, string result) 员工授予权限
        /// <summary>
        /// 员工授予权限
        /// </summary>
        /// <param name="roleId">员工主键</param>
        /// <param name="organizeId">权组织机构限主键</param>
        /// <param name="permissionCode">权限编号</param>
        /// <returns>主键</returns>
        public string GrantPermission(string roleId, string grantPermissionId, string permissionCode)
        {
            BasePermissionScopeManager permissionScopeManager = new BasePermissionScopeManager(DbHelper, UserInfo);
            return this.GrantPermission(permissionScopeManager, roleId, grantPermissionId, permissionCode);
        }
        #endregion

        public int GrantPermissiones(string roleId, string[] grantPermissionIds, string permissionCode)
        {
            int result = 0;
            BasePermissionScopeManager permissionScopeManager = new BasePermissionScopeManager(DbHelper, UserInfo);
            for (int i = 0; i < grantPermissionIds.Length; i++)
            {
                this.GrantPermission(permissionScopeManager, roleId, grantPermissionIds[i], permissionCode);
                result++;
            }
            return result;
        }

        public int GrantPermissiones(string[] roleIds, string grantPermissionId, string permissionCode)
        {
            int result = 0;
            BasePermissionScopeManager permissionScopeManager = new BasePermissionScopeManager(DbHelper, UserInfo);
            for (int i = 0; i < roleIds.Length; i++)
            {
                this.GrantPermission(permissionScopeManager, roleIds[i], grantPermissionId, permissionCode);
                result++;
            }
            return result;
        }

        public int GrantPermissions(string[] roleIds, string[] grantPermissionIds, string permissionCode)
        {
            int result = 0;
            BasePermissionScopeManager permissionScopeManager = new BasePermissionScopeManager(DbHelper, UserInfo);
            for (int i = 0; i < roleIds.Length; i++)
            {
                for (int j = 0; j < grantPermissionIds.Length; j++)
                {
                    this.GrantPermission(permissionScopeManager, roleIds[i], grantPermissionIds[j], permissionCode);
                    result++;
                }
            }
            return result;
        }


        //
        //  撤销授权范围的实现部分
        //

        #region private int RevokePermission(BasePermissionScopeManager manager, string roleId, string permissionCode, string revokePermissionId) 为了提高授权的运行速度
        /// <summary>
        /// 为了提高授权的运行速度
        /// </summary>
        /// <param name="manager">权限域读写器</param>
        /// <param name="roleId">角色主键</param>
        /// <param name="revokePermissionId">权限主键</param>
        /// <param name="permissionCode">权限编号</param>
        /// <returns>主键</returns>
        private int RevokePermission(BasePermissionScopeManager permissionScopeManager, string roleId, string revokePermissionId, string permissionCode)
        {
            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldResourceCategory, BaseRoleEntity.TableName));
            parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldResourceId, roleId));
            parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldTargetCategory, BaseModuleEntity.TableName));
            parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldTargetId, revokePermissionId));
            parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldPermissionId, this.GetPermissionIdByCode(permissionCode)));
            return permissionScopeManager.Delete(parameters);
        }
        #endregion

        #region public int RevokePermission(string roleId, string result) 员工撤销授权
        /// <summary>
        /// 员工撤销授权
        /// </summary>
        /// <param name="roleId">角色主键</param>
        /// <param name="permissionCode">权限编号</param>
        /// <returns>主键</returns>
        public int RevokePermission(string roleId, string revokePermissionId, string permissionCode)
        {
            BasePermissionScopeManager permissionScopeManager = new BasePermissionScopeManager(DbHelper, UserInfo);
            return this.RevokePermission(permissionScopeManager, roleId, revokePermissionId, permissionCode);
        }
        #endregion

        public int RevokePermissions(string roleId, string[] revokePermissionIds, string permissionCode)
        {
            int result = 0;
            BasePermissionScopeManager permissionScopeManager = new BasePermissionScopeManager(DbHelper, UserInfo);
            for (int i = 0; i < revokePermissionIds.Length; i++)
            {
                this.RevokePermission(permissionScopeManager, roleId, revokePermissionIds[i], permissionCode);
                result++;
            }
            return result;
        }

        public int RevokePermissions(string[] roleIds, string revokePermissionId, string permissionCode)
        {
            int result = 0;
            BasePermissionScopeManager permissionScopeManager = new BasePermissionScopeManager(DbHelper, UserInfo);
            for (int i = 0; i < roleIds.Length; i++)
            {
                this.RevokePermission(permissionScopeManager, roleIds[i], revokePermissionId, permissionCode);
                result++;
            }
            return result;
        }

        public int RevokePermissions(string[] roleIds, string[] revokePermissionIds, string permissionCode)
        {
            int result = 0;
            BasePermissionScopeManager permissionScopeManager = new BasePermissionScopeManager(DbHelper, UserInfo);
            for (int i = 0; i < roleIds.Length; i++)
            {
                for (int j = 0; j < revokePermissionIds.Length; j++)
                {
                    this.RevokePermission(permissionScopeManager, roleIds[i], revokePermissionIds[j], permissionCode);
                    result++;
                }
            }
            return result;
        }
    }
}