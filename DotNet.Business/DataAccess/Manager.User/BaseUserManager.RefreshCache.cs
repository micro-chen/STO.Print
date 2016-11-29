//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd. 
//-----------------------------------------------------------------

using System;
using System.Data;

namespace DotNet.Business
{
    using DotNet.Model;
    using DotNet.Utilities;

    /// <summary>
    /// BaseUserManager
    /// 用户管理
    /// 
    /// 修改纪录
    /// 
    ///		2016.02.29 版本：1.0 JiRiGaLa	主键整理。
    /// 
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2016.02.29</date>
    /// </author> 
    /// </summary>
    public partial class BaseUserManager : BaseManager
    {
        public static int RefreshCache(string userId)
        {
            int result = 0;

            // 刷新用户的缓存
            BaseUserEntity userEntity = BaseUserManager.GetObjectByCache(userId, true);
            if (userEntity != null)
            {
                // 刷新用户的登录限制
                BaseUserManager.ResetIPAddressByCache(userId);
                BaseUserManager.ResetMACAddressByCache(userId);
                // 刷新组织机构缓存
                BaseOrganizeManager.GetObjectByCache(userEntity.CompanyId, true);
                // 刷新部门缓存
                BaseDepartmentManager.GetObjectByCache(userEntity.DepartmentId, true);
                // 2016-02-18 吉日嘎拉 刷新拒绝权限(把用户的权限放在一起方便直接移除、刷新)
                string key = "User:IsAuthorized:" + userId;
                using (var redisClient = PooledRedisHelper.GetPermissionClient())
                {
                    redisClient.Remove(key);
                }
                // 每个子系统都可以循环一次
                string[] systemCodes = BaseSystemManager.GetSystemCodes();
                for (int i = 0; i < systemCodes.Length; i++)
                {
                    BaseUserPermissionManager.ResetPermissionByCache(systemCodes[i], userId);
                }
            }

            return result;
        }
    }
}