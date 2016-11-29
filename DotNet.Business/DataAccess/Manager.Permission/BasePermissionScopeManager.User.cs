//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd.  
//-----------------------------------------------------------------

using System.Data;
using System.Linq;

namespace DotNet.Business
{
    using DotNet.Model;
    using DotNet.Utilities;

    /// <summary>
    /// BasePermissionScopeManager
    /// 资源权限范围
    ///
    ///     2012.08.06 版本：1.0 JiRiGaLa 主键进行了绝对的优化，这是个好东西啊，平时要多用，用得要灵活些。
    ///     
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2012.08.06</date>
    /// </author>
    /// </summary>
    public partial class BasePermissionScopeManager : BaseManager, IBaseManager
    {
        /// <summary>
        /// 获取用户权限树
        /// </summary>
        /// <param name="userInfo">用户</param>
        /// <param name="userId">用户主键</param>
        /// <param name="permissionName">权限名称</param>
        /// <param name="permissionCode">权限编号</param>
        /// <returns>用户主键</returns>
        public string[] GetPermissionTreeUserIds(string systemCode, string userId, string permissionCode, string permissionName = null)
        {
            string[] result = null;
            string tableName = string.Empty;
            string permissionId = BaseModuleManager.GetIdByCodeByCache(systemCode, permissionCode);
            if (!string.IsNullOrEmpty(permissionId))
            {
                tableName = " (SELECT ResourceId, TargetId FROM " + UserInfo.SystemCode + "PermissionScope WHERE Enabled = 1 AND DeletionStateCode = 0 AND ResourceCategory = '" + BaseUserEntity.TableName + "' AND TargetCategory = '" + BaseUserEntity.TableName + "' AND PermissionId = " + permissionId + ") T ";
                // tableName = UserInfo.SystemCode + "UserUserScope";
                string fieldParentId = "ResourceId"; //"ManagerUserId";
                string fieldId = "TargetId"; // "UserId";
                string order = null; 
                bool idOnly = true;
                DataTable dt = DbLogic.GetChildrens(this.DbHelper, tableName, fieldId, userId, fieldParentId, order, idOnly);
                result = BaseBusinessLogic.FieldToArray(dt, "TargetId");
            }
            return result;
        }


        /// <summary>
        /// 获得有某个权限的所有用户主键
        /// </summary>
        /// <param name="organizeId">组织机构主键</param>
        /// <param name="permissionCode">操作权限编号</param>
        /// <param name="permissionItemName">操作权限名称</param>
        /// <returns>用户主键数组</returns>
        public string[] GetUserIds(string systemCode, string organizeId, string permissionCode, string permissionName = null)
        {
            string permissionId = string.Empty;
            // 若不存在就需要自动能增加一个操作权限项
            permissionId = BaseModuleManager.GetIdByCodeByCache(systemCode, permissionCode);
            return GetUserIdsByPermissionId(organizeId, permissionId);
        }

        public string[] GetUserIdsByPermissionId(string organizeId, string permissionId)
        {
            DataTable dt = null;
            string[] result = null;
            if (!string.IsNullOrEmpty(permissionId))
            {
                string tableName = UserInfo.SystemCode + "PermissionScope";
                string sqlQuery = string.Empty;

                // 1.本人直接就有某个操作权限的。
                sqlQuery = "SELECT ResourceId FROM " + tableName + " WHERE (ResourceCategory = 'BaseUser') AND (PermissionId = " + permissionId + ") AND TargetCategory='BaseOrganize' AND TargetId = " + organizeId + " AND (DeletionStateCode = 0) AND (Enabled = 1) ";
                dt = this.Fill(sqlQuery);
                string[] userIds = BaseBusinessLogic.FieldToArray(dt, BasePermissionEntity.FieldResourceId).Distinct<string>().Where(t => !string.IsNullOrEmpty(t)).ToArray();

                // 2.角色本身就有某个操作权限的。
                sqlQuery = "SELECT ResourceId FROM " + tableName + " WHERE (ResourceCategory = 'BaseRole') AND (PermissionId = " + permissionId + ") AND TargetCategory='BaseOrganize' AND TargetId = " + organizeId + " AND (DeletionStateCode = 0) AND (Enabled = 1) ";
                dt = this.Fill(sqlQuery);
                string[] roleIds = StringUtil.Concat(result, BaseBusinessLogic.FieldToArray(dt, BasePermissionEntity.FieldResourceId)).Distinct<string>().Where(t => !string.IsNullOrEmpty(t)).ToArray();

                // 3.组织机构有某个操作权限。
                // sqlQuery = "SELECT ResourceId FROM " + tableName + " WHERE (ResourceCategory = 'BaseOrganize') AND (PermissionId = " + result + ") AND (DeletionStateCode = 0) AND (Enabled = 1) ";
                // result = this.Fill(sqlQuery);
                // string[] ids = StringUtil.Concat(result, BaseBusinessLogic.FieldToArray(result, BasePermissionEntity.FieldResourceId)).Distinct<string>().Where(t => !string.IsNullOrEmpty(t)).ToArray();

                // 4.获取所有有这个操作权限的用户Id，而且这些用户是有效的。
                BaseUserManager userManager = new BaseUserManager(this.DbHelper, this.UserInfo);
                result = userManager.GetUserIds(userIds, null, roleIds);
            }
            return result;
        }
    }
}