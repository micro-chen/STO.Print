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
    /// 角色用户权限域
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

        #region public string[] GetUserIds(string systemCode, string roleId, string permissionCode) 获取员工的权限主键数组
        /// <summary>
        /// 获取用户的权限主键数组
        /// </summary>
        /// <param name="systemCode">系统编号</param>
        /// <param name="roleId">角色主键</param>
        /// <param name="permissionCode">权限编号</param>
        /// <returns>主键数组</returns>
        public string[] GetUserIds(string systemCode, string roleId, string permissionCode)
        {
            string[] result = null;
            string roleTableName = systemCode + "Role";
            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldResourceCategory, roleTableName));
            parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldResourceId, roleId));
            parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldTargetCategory, BaseUserEntity.TableName));
            parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldPermissionId, this.GetPermissionIdByCode(permissionCode)));

            var dt = this.GetDataTable(parameters);
            result = BaseBusinessLogic.FieldToArray(dt, BasePermissionScopeEntity.FieldTargetId).Distinct<string>().Where(t => !string.IsNullOrEmpty(t)).ToArray();
            return result;
        }
        #endregion

        //
        // 授予授权范围的实现部分
        //

        #region private string GrantUser(BasePermissionScopeManager manager, string id, string roleId, string grantUserId) 为了提高授权的运行速度
        /// <summary>
        /// 为了提高授权的运行速度
        /// </summary>
        /// <param name="manager">权限域读写器</param>
        /// <param name="roleId">员工主键</param>
        /// <param name="grantUserId">权限主键</param>
        /// <param name="permissionCode">权限编号</param>
        /// <returns>主键</returns>
        private string GrantUser(BasePermissionScopeManager permissionScopeManager, string roleId, string grantUserId, string permissionCode)
        {
            string result = string.Empty;
            string roleTableName = this.UserInfo.SystemCode + "Role";
            BasePermissionScopeEntity resourcePermissionScopeEntity = new BasePermissionScopeEntity();
            resourcePermissionScopeEntity.PermissionId = this.GetPermissionIdByCode(permissionCode);
            resourcePermissionScopeEntity.ResourceCategory = roleTableName;
            resourcePermissionScopeEntity.ResourceId = roleId;
            resourcePermissionScopeEntity.TargetCategory = BaseUserEntity.TableName;
            resourcePermissionScopeEntity.TargetId = grantUserId;
            resourcePermissionScopeEntity.Enabled = 1;
            resourcePermissionScopeEntity.DeletionStateCode = 0;
            return permissionScopeManager.Add(resourcePermissionScopeEntity);
        }
        #endregion

        #region public string GrantUser(string roleId, string result) 员工授予权限
        /// <summary>
        /// 员工授予权限
        /// </summary>
        /// <param name="roleId">员工主键</param>
        /// <param name="result">权限主键</param>
        /// <param name="organizeId">权组织机构限主键</param>
        /// <returns>主键</returns>
        public string GrantUser(string roleId, string grantUserId, string permissionCode)
        {
            BasePermissionScopeManager permissionScopeManager = new BasePermissionScopeManager(DbHelper, UserInfo);
            return this.GrantUser(permissionScopeManager, roleId, grantUserId, permissionCode);
        }
        #endregion

        public int GrantUsers(string roleId, string[] grantUserIds, string permissionCode)
        {
            int result = 0;
            BasePermissionScopeManager permissionScopeManager = new BasePermissionScopeManager(DbHelper, UserInfo);
            for (int i = 0; i < grantUserIds.Length; i++)
            {
                this.GrantUser(permissionScopeManager, roleId, grantUserIds[i], permissionCode);
                result++;
            }
            return result;
        }

        public int GrantUsers(string[] roleIds, string grantUserId, string permissionCode)
        {
            int result = 0;
            BasePermissionScopeManager permissionScopeManager = new BasePermissionScopeManager(DbHelper, UserInfo);
            for (int i = 0; i < roleIds.Length; i++)
            {
                this.GrantUser(permissionScopeManager, roleIds[i], grantUserId, permissionCode);
                result++;
            }
            return result;
        }

        public int GrantUsers(string[] roleIds, string[] grantUserIds, string permissionCode)
        {
            int result = 0;
            BasePermissionScopeManager permissionScopeManager = new BasePermissionScopeManager(DbHelper, UserInfo);
            for (int i = 0; i < roleIds.Length; i++)
            {
                for (int j = 0; j < grantUserIds.Length; j++)
                {
                    this.GrantUser(permissionScopeManager, roleIds[i], grantUserIds[j], permissionCode);
                    result++;
                }
            }
            return result;
        }


        //
        //  撤销授权范围的实现部分
        //

        #region private int RevokeUser(BasePermissionScopeManager manager, string roleId, string revokeUserId, string permissionCode) 为了提高授权的运行速度
        /// <summary>
        /// 为了提高授权的运行速度
        /// </summary>
        /// <param name="manager">权限域读写器</param>
        /// <param name="roleId">员工主键</param>
        /// <param name="revokeUserId">权限主键</param>
        /// <param name="permissionCode">权限编号</param>
        /// <returns>主键</returns>
        private int RevokeUser(BasePermissionScopeManager permissionScopeManager, string roleId, string revokeUserId, string permissionCode)
        {
            string roleTableName = this.UserInfo.SystemCode + "Role";
            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldResourceCategory, roleTableName));
            parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldResourceId, roleId));
            parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldTargetCategory, BaseUserEntity.TableName));
            parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldTargetId, revokeUserId));
            parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldPermissionId, this.GetPermissionIdByCode(permissionCode)));
            return permissionScopeManager.Delete(parameters);
        }
        #endregion

        #region public int RevokeUser(string roleId, string revokeUserId, string permissionCode) 角色撤销授权
        /// <summary>
        /// 角色撤销授权
        /// </summary>
        /// <param name="roleId">角色主键</param>
        /// <param name="permissionCode">权限编号</param>
        /// <returns>主键</returns>
        public int RevokeUser(string roleId, string revokeUserId, string permissionCode)
        {
            BasePermissionScopeManager permissionScopeManager = new BasePermissionScopeManager(DbHelper, UserInfo);
            return this.RevokeUser(permissionScopeManager, roleId, revokeUserId, permissionCode);
        }
        #endregion

        public int RevokeUsers(string roleId, string[] revokeUserIds, string permissionCode)
        {
            int result = 0;
            BasePermissionScopeManager permissionScopeManager = new BasePermissionScopeManager(DbHelper, UserInfo);
            for (int i = 0; i < revokeUserIds.Length; i++)
            {
                this.RevokeUser(permissionScopeManager, roleId, revokeUserIds[i], permissionCode);
                result++;
            }
            return result;
        }

        public int RevokeUsers(string[] roleIds, string revokeUserId, string permissionCode)
        {
            int result = 0;
            BasePermissionScopeManager permissionScopeManager = new BasePermissionScopeManager(DbHelper, UserInfo);
            for (int i = 0; i < roleIds.Length; i++)
            {
                this.RevokeUser(permissionScopeManager, roleIds[i], revokeUserId, permissionCode);
                result++;
            }
            return result;
        }

        public int RevokeUsers(string[] roleIds, string[] revokeUserIds, string permissionCode)
        {
            int result = 0;
            BasePermissionScopeManager permissionScopeManager = new BasePermissionScopeManager(DbHelper, UserInfo);
            for (int i = 0; i < roleIds.Length; i++)
            {
                for (int j = 0; j < revokeUserIds.Length; j++)
                {
                    this.RevokeUser(permissionScopeManager, roleIds[i], revokeUserIds[j], permissionCode);
                    result++;
                }
            }
            return result;
        }
    }
}