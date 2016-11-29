//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd.  
//-----------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data;
using ServiceStack.Redis;

namespace DotNet.Business
{
    using DotNet.Model;
    using DotNet.Utilities;

    /// <summary>
    /// BaseServicesLicenseManager
    /// 服务管理
    /// 
    /// 修改记录
    /// 
    ///		2015.12.25 版本：1.0 JiRiGaLa	主键整理。
    /// 
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2015.12.25</date>
    /// </author> 
    /// </summary>
    public partial class BaseServicesLicenseManager : BaseManager
    {
        /// <summary>
        /// 检查一个服务调用是否是允许调用的？
        /// 1：是否要记录日志？
        /// 2：是否需要埋点？检查性能？访问频率等？调用次数？
        /// 3：非合法的调用？是否日志记录？
        /// 4：异常的要进行处理？
        /// </summary>
        /// <param name="appKey">应用唯一标识</param>
        /// <param name="appSecret">应用的签名密钥</param>
        /// <param name="callLimit">是否进行限制</param>
        /// <param name="systemCode">访问子系统</param>
        /// <param name="permissionCode">判断的权限编号</param>
        /// <returns>验证情况</returns>
        public static BaseResult CheckService(string appKey, string appSecret, bool callLimit = false, string systemCode = "Base", string permissionCode = null)
        {
            BaseResult result = new DotNet.Utilities.BaseResult();
            result.Status = false;

            // AppKey： 23286115
            // AppSecret： c8d1f06f599d7370467993c72a34c701
            // permissionCode： "User.Add" 

            string ipAddress = Utilities.GetIPAddress(true);

            // 1: 判断参数是否合理？目标服务，总不可以为空，否则怎么区别谁在调用这个服务了？
            if (string.IsNullOrEmpty(appKey))
            {
                result.StatusCode = "AccessDeny";
                result.StatusMessage = "appKey为空、访问被拒绝";
                return result;
            }

            // 2: 判断是否在接口角色里, 只有在接口角色里的，才可以进行远程调用，这样也方便把接口随时踢出来。
            string roleCode = "Interface";
            if (!BaseUserManager.IsInRoleByCache(systemCode, appKey, roleCode))
            {
                result.StatusCode = "AccessDeny";
                result.StatusMessage = "非接口用户、访问被拒绝";
                return result;
            }

            // 3: 判断调用的频率是否？这里需要高速判断，不能总走数据库？调用的效率要高，不能被远程接口给拖死了、自己的服务都不正常了。
            if (callLimit && PooledRedisHelper.CallLimit(appKey, 10, 10000))
            {
                result.StatusCode = "AccessDeny";
                result.StatusMessage = "访问频率过高、访问被拒绝";
                return result;
            }

            // 4: 判断签名是否有效？是否过期？可以支持多个签名，容易升级、容易兼容、容易有个过度的缓冲期。为了提高安全性，必须要有签名才对。
            if (!BaseServicesLicenseManager.CheckServiceByCache(appKey, appSecret))
            {
                result.StatusCode = "AccessDeny";
                result.StatusMessage = "不合法签名、访问被拒绝";
                return result;
            }

            // 5: 判断对方的ip是否合法的？1个服务程序，可以有多个ip。可以把服务当一个用户看待，一个目标用户可能也配置了多个服务，一般是远程连接。
            BaseUserLogOnManager userLogOnManager = new BaseUserLogOnManager();
            BaseUserLogOnEntity userLogOnEntity = userLogOnManager.GetObject(appKey);
            if (BaseUserManager.CheckIPAddressByCache(userLogOnEntity, ipAddress, true))
            {
                result.StatusCode = "AccessDeny";
                result.StatusMessage = "不合法IP、访问被拒绝";
                return result;
            }

            // 6: 判断是否有权限？防止被过渡调用，拖死数据库，可以用缓存的方式进行判断，这样不容易被客户端、合作伙伴拖垮。
            if (!string.IsNullOrEmpty(permissionCode) && !BasePermissionManager.IsAuthorizedByCache(systemCode, appKey, permissionCode))
            {
                result.StatusCode = "AccessDeny";
                result.StatusMessage = "无权限 " + permissionCode + "、访问被拒绝";
                return result;
            }

            // 7: 判断是否有效？判断时间是否对？
            BaseUserManager userManager = new BaseUserManager();
            BaseUserEntity userEntity = userManager.GetObject(appKey);
            UserLogOnResult userLogOnResult = userManager.CheckUser(userEntity, userLogOnEntity);
            if (!string.IsNullOrEmpty(userLogOnResult.StatusCode))
            {
                BaseLoginLogManager.AddLog(systemCode, userEntity, ipAddress, string.Empty, string.Empty, userLogOnResult.StatusMessage);
                result.StatusCode = userLogOnResult.StatusCode;
                result.StatusMessage = userLogOnResult.StatusMessage;
                return result;
            }

            // 8：目前需要判断的，都加上了。
            result.Status = true;
            return result;
        }
    }
}