//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd.  
//-----------------------------------------------------------------

using System.Collections.Generic;

namespace DotNet.Business
{
    using DotNet.Model;

    /// <summary>
    /// BaseUserScopeManager
    /// 用户对用户的权限域
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

        #region public string[] GetUserIds(string systemCode, string userId, string permissionCode) 获取员工的权限主键数组
        /// <summary>
        /// 获取员工的权限主键数组
        /// </summary>
        /// <param name="systemCode">系统编号</param>
        /// <param name="userId">用户主键</param>
        /// <param name="permissionCode">权限编号</param>
        /// <returns>主键数组</returns>
        public string[] GetUserIds(string systemCode, string userId, string permissionCode)
        {
            string[] result = null;

            string permissionId = BaseModuleManager.GetIdByCodeByCache(systemCode, permissionCode);
            if (!string.IsNullOrEmpty(permissionId))
            {
                List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
                parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldResourceCategory, BaseUserEntity.TableName));
                parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldResourceId, userId));
                parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldTargetCategory, BaseUserEntity.TableName));
                parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldPermissionId, permissionId));

                // 20130605 JiRiGaLa 这个运行效率更高一些
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

        #region private string GrantUser(BasePermissionScopeManager manager, string userId, string grantUserId, string permissionCode) 为了提高授权的运行速度
        /// <summary>
        /// 为了提高授权的运行速度
        /// </summary>
        /// <param name="manager">权限域读写器</param>
        /// <param name="userId">用户主键</param>
        /// <param name="grantUserId">权限主键</param>
        /// <param name="permissionCode">权限编号</param>
        /// <returns>主键</returns>
        private string GrantUser(BasePermissionScopeManager permissionScopeManager, string systemCode, string userId, string grantUserId, string permissionCode)
        {
            string result = string.Empty;

            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldResourceCategory, BaseUserEntity.TableName));
            parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldResourceId, userId));
            parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldTargetCategory, BaseUserEntity.TableName));
            parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldTargetId, grantUserId));
            parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldPermissionId, BaseModuleManager.GetIdByCodeByCache(systemCode, permissionCode)));

            if (!this.Exists(parameters))
            {
                BasePermissionScopeEntity resourcePermissionScopeEntity = new BasePermissionScopeEntity();
                resourcePermissionScopeEntity.PermissionId = BaseModuleManager.GetIdByCodeByCache(systemCode, permissionCode);
                resourcePermissionScopeEntity.ResourceCategory = BaseUserEntity.TableName;
                resourcePermissionScopeEntity.ResourceId = userId;
                resourcePermissionScopeEntity.TargetCategory = BaseUserEntity.TableName;
                resourcePermissionScopeEntity.TargetId = grantUserId;
                resourcePermissionScopeEntity.Enabled = 1;
                resourcePermissionScopeEntity.DeletionStateCode = 0;
                return permissionScopeManager.Add(resourcePermissionScopeEntity);
            }

            return result;
        }
        #endregion

        #region public string GrantUser(string userId, string grantUserId, string permissionCode) 用户授予权限
        /// <summary>
        /// 用户授予权限
        /// </summary>
        /// <param name="userId">用户主键</param>
        /// <param name="grantUserId">权组织机构限主键</param>
        /// <param name="result">权限主键</param>
        /// <returns>主键</returns>
        public string GrantUser(string systemCode, string userId, string grantUserId, string permissionCode)
        {
            BasePermissionScopeManager permissionScopeManager = new BasePermissionScopeManager(DbHelper, UserInfo);
            return this.GrantUser(permissionScopeManager, systemCode, userId, grantUserId, permissionCode);
        }
        #endregion

        public int GrantUsers(string systemCode, string userId, string[] grantUserIds, string permissionCode)
        {
            int result = 0;
            BasePermissionScopeManager permissionScopeManager = new BasePermissionScopeManager(DbHelper, UserInfo, this.CurrentTableName);
            for (int i = 0; i < grantUserIds.Length; i++)
            {
                this.GrantUser(permissionScopeManager, systemCode, userId, grantUserIds[i], permissionCode);
                result++;
            }
            return result;
        }

        public int GrantUsers(string systemCode, string[] userIds, string grantUserId, string permissionCode)
        {
            int result = 0;
            BasePermissionScopeManager permissionScopeManager = new BasePermissionScopeManager(DbHelper, UserInfo, this.CurrentTableName);
            for (int i = 0; i < userIds.Length; i++)
            {
                this.GrantUser(permissionScopeManager, systemCode, userIds[i], grantUserId, permissionCode);
                result++;
            }
            return result;
        }

        public int GrantUsers(string systemCode, string[] userIds, string[] grantUserIds, string permissionCode)
        {
            int result = 0;
            BasePermissionScopeManager permissionScopeManager = new BasePermissionScopeManager(DbHelper, UserInfo, this.CurrentTableName);
            for (int i = 0; i < userIds.Length; i++)
            {
                for (int j = 0; j < grantUserIds.Length; j++)
                {
                    this.GrantUser(permissionScopeManager, systemCode, userIds[i], grantUserIds[j], permissionCode);
                    result++;
                }
            }
            return result;
        }


        //
        //  撤销授权范围的实现部分
        //

        #region private int RevokeUser(BasePermissionScopeManager manager, string userId, string revokeUserId, string permissionCode) 为了提高授权的运行速度
        /// <summary>
        /// 为了提高授权的运行速度
        /// </summary>
        /// <param name="manager">权限域读写器</param>
        /// <param name="userId">用户主键</param>
        /// <param name="revokeUserId">权限主键</param>
        /// <returns>主键</returns>
        private int RevokeUser(BasePermissionScopeManager permissionScopeManager, string systemCode, string userId, string revokeUserId, string permissionCode)
        {
            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldResourceCategory, BaseUserEntity.TableName));
            parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldResourceId, userId));
            parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldTargetCategory, BaseUserEntity.TableName));
            parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldTargetId, revokeUserId));
            parameters.Add(new KeyValuePair<string, object>(BasePermissionScopeEntity.FieldPermissionId, BaseModuleManager.GetIdByCodeByCache(systemCode, permissionCode)));
            return permissionScopeManager.Delete(parameters);
        }
        #endregion

        #region public int RevokeUser(string userId, string revokeUserId, string permissionCode) 员工撤销授权
        /// <summary>
        /// 员工撤销授权
        /// </summary>
        /// <param name="userId">用户主键</param>
        /// <param name="permissionCode">权限编号</param>
        /// <returns>主键</returns>
        public int RevokeUser(string systemCode, string userId, string revokeUserId, string permissionCode)
        {
            BasePermissionScopeManager permissionScopeManager = new BasePermissionScopeManager(DbHelper, UserInfo, this.CurrentTableName);
            return this.RevokeUser(permissionScopeManager, systemCode, userId, revokeUserId, permissionCode);
        }
        #endregion

        public int RevokeUsers(string systemCode, string userId, string[] revokeUserIds, string permissionCode)
        {
            int result = 0;
            BasePermissionScopeManager permissionScopeManager = new BasePermissionScopeManager(DbHelper, UserInfo, this.CurrentTableName);
            for (int i = 0; i < revokeUserIds.Length; i++)
            {
                this.RevokeUser(permissionScopeManager, systemCode, userId, revokeUserIds[i], permissionCode);
                result++;
            }
            return result;
        }

        public int RevokeUsers(string systemCode, string[] userIds, string revokeUserId, string permissionCode)
        {
            int result = 0;
            BasePermissionScopeManager permissionScopeManager = new BasePermissionScopeManager(DbHelper, UserInfo, this.CurrentTableName);
            for (int i = 0; i < userIds.Length; i++)
            {
                this.RevokeUser(permissionScopeManager, systemCode, userIds[i], revokeUserId, permissionCode);
                result++;
            }
            return result;
        }

        public int RevokeUsers(string systemCode, string[] userIds, string[] revokeUserIds, string permissionCode)
        {
            int result = 0;
            BasePermissionScopeManager permissionScopeManager = new BasePermissionScopeManager(DbHelper, UserInfo, this.CurrentTableName);
            for (int i = 0; i < userIds.Length; i++)
            {
                for (int j = 0; j < revokeUserIds.Length; j++)
                {
                    this.RevokeUser(permissionScopeManager, systemCode, userIds[i], revokeUserIds[j], permissionCode);
                    result++;
                }
            }
            return result;
        }
    }
}