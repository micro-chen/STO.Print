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
    /// BasePermissionManager
    /// 资源权限管理，操作权限管理（这里实现了用户操作权限，角色的操作权限）
    /// 
    /// 修改记录
    ///
    ///     2015.07.10 版本：2.1 JiRiGaLa 把删除标志补上来。
    ///     2010.09.21 版本：2.0 JiRiGaLa 智能权限判断、后台自动增加权限，增加并发锁PermissionLock。
    ///     2009.09.22 版本：1.1 JiRiGaLa 前台判断的权限，后台都需要记录起来，防止后台缺失前台的判断权限。
    ///     2008.03.28 版本：1.0 JiRiGaLa 创建主键。
    ///     
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2015.07.10</date>
    /// </author>
    /// </summary>
    public partial class BasePermissionManager : BaseManager, IBaseManager
    {
        public static bool IsAuthorizedByCache(string systemCode, string userId, string permissionCode)
        {
            bool result = false;

            using (var redisReadOnlyClient = PooledRedisHelper.GetPermissionReadOnlyClient())
            {
                // 2016-02-18 吉日嘎拉 这样可以刷新用户权限时，可以把一个用户的权限全去掉。
                string hashId = "User:IsAuthorized:" + userId;
                string key = systemCode + ":" + permissionCode;
                // 若是缓存里过期了？
                if (redisReadOnlyClient.HashContainsEntry(hashId, key))
                {
                    string isAuthorized = redisReadOnlyClient.GetValueFromHash(hashId, key);
                    result = isAuthorized.Equals(true.ToString());
                }
                else
                {
                    BasePermissionManager permissionManager = new BasePermissionManager();
                    result = permissionManager.IsAuthorized(systemCode, userId, permissionCode);
#if ReadOnlyRedis
                    using (var redisClient = PooledRedisHelper.GetPermissionClient())
                    {
                        redisClient.SetEntryInHash(hashId, key, result.ToString());
                        redisClient.ExpireEntryAt(hashId, DateTime.Now.AddMinutes(20));
                    }
#else
                    redisReadOnlyClient.SetEntryInHash(hashId, key, result.ToString());
                    redisReadOnlyClient.ExpireEntryAt(hashId, DateTime.Now.AddMinutes(20));
#endif
                }
            }

            return result;
        }


        /// <summary>
        /// 获取用户的权限主键数组
        /// </summary>
        /// <param name="systemCode">系统编号</param>
        /// <param name="userId">用户主键</param>
        /// <param name="companyId">公司主键</param>
        /// <param name="useBaseRole">使用基础角色权限</param>
        /// <returns>拥有权限数组</returns>
        public static string[] GetPermissionIdsByUserByCache(string systemCode, string userId, string companyId = null, bool containPublic = true, bool useBaseRole = false)
        {
            // 公开的操作权限需要计算
            string[] result = null;

            int errorMark = 0;
            string tableName = BaseModuleEntity.TableName;
            if (string.IsNullOrWhiteSpace(systemCode))
            {
                systemCode = "Base";
            }
            // 就不需要参合基础的角色了
            if (systemCode.Equals("Base"))
            {
                useBaseRole = false;
            }
            tableName = systemCode + "Module";

            try
            {
                errorMark = 1;

                // 01: 把公开的部分获取出来（把公开的主键数组从缓存里获取出来，减少数据库的读取次数）
                if (containPublic)
                {
                    List<BaseModuleEntity> moduleEntities = BaseModuleManager.GetEntitiesByCache(systemCode);
                    if (moduleEntities != null)
                    {
                        result = moduleEntities.Where((t => t.IsPublic == 1 && t.Enabled == 1 && t.DeletionStateCode == 0)).Select(t => t.Id.ToString()).ToArray();
                    }
                }

                // 02: 获取用户本身拥有的权限 
                string[] userPermissionIds = BaseUserPermissionManager.GetPermissionIdsByCache(systemCode, userId);
                result = StringUtil.Concat(result, userPermissionIds);

                // 03: 用户角色的操作权限

                // 用户都在哪些角色里？通过缓存读取？没有角色的，没必要进行运算了
                string[] roleIds = BaseUserManager.GetRoleIdsByCache(systemCode, userId, companyId);
                if (useBaseRole && !systemCode.Equals("Base", StringComparison.OrdinalIgnoreCase))
                {
                    string[] baseRoleIds = BaseUserManager.GetRoleIdsByCache("Base", userId, companyId);
                    if (baseRoleIds != null && baseRoleIds.Length > 0)
                    {
                        roleIds = StringUtil.Concat(roleIds, baseRoleIds);
                    }
                }
                if (roleIds != null && roleIds.Length > 0)
                {
                    string[] userRolePermissionIds = BaseRolePermissionManager.GetPermissionIdsByCache(systemCode, roleIds);
                    result = StringUtil.Concat(result, userRolePermissionIds);
                }

                // 04: 按部门(组织机构)获取权限项
                if (BaseSystemInfo.UseOrganizePermission && !string.IsNullOrEmpty(companyId))
                {
                    // 2016-02-26 吉日嘎拉，公司权限进行优化简化
                    string[] organizePermission = BaseOrganizePermissionManager.GetPermissionIdsByCache(systemCode, companyId);
                    result = StringUtil.Concat(result, organizePermission);
                }
            }
            catch (System.Exception ex)
            {
                string writeMessage = "BasePermissionManager.GetPermissionIdsByUser:发生时间:" + DateTime.Now
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

        public static bool CheckPermissionByRoleByCache(string systemCode, string roleId, string permissionCode)
        {
            string permissionId = string.Empty;
            permissionId = BaseModuleManager.GetIdByCodeByCache(systemCode, permissionCode);
            // 没有找到相应的权限
            if (String.IsNullOrEmpty(permissionId))
            {
                return false;
            }

            string[] permissionIds = BaseRolePermissionManager.GetPermissionIdsByCache(systemCode, new string[] { roleId });
            return Array.IndexOf(permissionIds, permissionId) >= 0;
        }
    }
}