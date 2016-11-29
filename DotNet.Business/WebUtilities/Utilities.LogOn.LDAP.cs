//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2013 , Hairihan TECH, Ltd .
//-----------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Text;
using System.Web;
using System.Web.Caching;
using System.Web.Security;
using System.DirectoryServices;
using DotNet.Utilities;

namespace DotNet.Business
{
    /// <summary>
    /// LDAP登录功能相关部分
    /// </summary>
    public partial class Utilities
    {
        // LDAP域用户登录部分：包括Windows AD域用户登录
        #region public static BaseUserInfo LogOnByLDAP(string domain, string lDAP, string userName, string password, string permissionCode, bool persistCookie, bool formsAuthentication, out string statusCode, out string statusMessage)
        /// <summary>
        /// 验证LDAP用户
        /// </summary>
        /// <param name="domain">域</param>
        /// <param name="lDAP">LDAP</param>
        /// <param name="userName">域用户名</param>
        /// <param name="password">域密码</param>
        /// <param name="permissionCode">权限编号</param>
        /// <param name="persistCookie">是否保存密码</param>
        /// <param name="formsAuthentication">表单验证，是否需要重定位</param>
        /// <param name="statusCode"></param>
        /// <param name="statusMessage"></param>
        /// <returns></returns>
        public static BaseUserInfo LogOnByLDAP(string domain, string lDAP, string userName, string password, string openId, string permissionCode, bool persistCookie, bool formsAuthentication, out string statusCode, out string statusMessage)
        {
            DirectoryEntry dirEntry = new DirectoryEntry();
            dirEntry.Path = lDAP;
            dirEntry.Username = domain + "\\" + userName;
            dirEntry.Password = password;
            dirEntry.AuthenticationType = AuthenticationTypes.Secure;

            try
            {
                DirectorySearcher dirSearcher = new DirectorySearcher(dirEntry);
                dirSearcher.Filter = String.Format("(&(objectClass=user)(samAccountName={0}))", userName);
                System.DirectoryServices.SearchResult result = dirSearcher.FindOne();
                if (result != null)
                {
                    // 统一的登录服务
                    DotNetService dotNetService = new DotNetService();
                    BaseUserInfo userInfo = dotNetService.LogOnService.LogOnByUserName(Utilities.GetUserInfo(), userName, out statusCode, out statusMessage);
                    //BaseUserInfo userInfo = dotNetService.LogOnService.UserLogOn(Utilities.GetUserInfo(), userName, password, openId, false, out statusCode, out statusMessage);
                    // 检查身份
                    if (statusCode.Equals(Status.OK.ToString()))
                    {
                        userInfo.IPAddress = GetIPAddress();

                        bool isAuthorized = true;
                        // 用户是否有哪个相应的权限
                        if (!string.IsNullOrEmpty(permissionCode))
                        {
                            isAuthorized = dotNetService.PermissionService.IsAuthorized(userInfo, permissionCode, null);
                        }
                        // 有相应的权限才可以登录
                        if (isAuthorized)
                        {
                            if (persistCookie)
                            {
                                // 相对安全的方式保存登录状态
                                // SaveCookie(userName, password);
                                // 内部单点登录方式
                                SaveCookie(userInfo);
                            }
                            else
                            {
                                RemoveUserCookie();
                            }
                            LogOn(userInfo, formsAuthentication);
                        }
                        else
                        {
                            statusCode = Status.LogOnDeny.ToString();
                            statusMessage = "访问被拒绝、您的账户没有后台管理访问权限。";
                        }
                    }

                    return userInfo;
                }
                else
                {
                    statusCode = Status.LogOnDeny.ToString();
                    statusMessage = "应用系统用户不存在，请联系管理员。";
                    return null;
                }
            }
            catch (Exception e)
            {
                //Logon failure: unknown user name or bad password.
                statusCode = Status.LogOnDeny.ToString();
                statusMessage = "域服务器返回信息" + e.Message.Replace("\r\n", "");
                return null;
            }

            
        }
        #endregion

    }
}