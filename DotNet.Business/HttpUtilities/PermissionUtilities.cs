//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd.  
//-----------------------------------------------------------------

using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Text;
using System.Web.Script.Serialization;

namespace DotNet.Business
{
    using DotNet.Model;
    using DotNet.Utilities;

    /// <summary>
    /// PermissionUtilities 
    /// 权限服务，远程调用接口
    ///
    /// 修改记录
    ///
    ///		2015.12.15 版本：1.0 JiRiGaLa  远程调用服务。
    ///
    /// </summary>
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2015.12.15</date>
    /// </author>
    /// </summary>
    public class PermissionUtilities
    {
        // 用户的最终权限判断
        public static bool IsAuthorized(BaseUserInfo userInfo, string systemCode, string userId, string permissionCode, string permissionName = null)
        {
            bool result = false;

            string url = BaseSystemInfo.UserCenterHost + "/UserCenterV42/PermissionService.ashx";
            WebClient webClient = new WebClient();
            NameValueCollection postValues = new NameValueCollection();
            postValues.Add("system", BaseSystemInfo.SoftFullName);
            postValues.Add("systemCode", systemCode);
            postValues.Add("securityKey", BaseSystemInfo.SecurityKey);
            postValues.Add("function", "IsAuthorized");
            postValues.Add("userInfo", userInfo.Serialize());
            postValues.Add("userId", userId);
            postValues.Add("permissionCode", permissionCode);
            if (!string.IsNullOrEmpty(permissionName))
            {
                postValues.Add("permissionName", permissionName);
            }
            // 向服务器发送POST数据
            byte[] responseArray = webClient.UploadValues(url, postValues);
            string response = Encoding.UTF8.GetString(responseArray);
            if (!string.IsNullOrEmpty(response))
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                result = javaScriptSerializer.Deserialize<bool>(response);
            }

            return result;
        }

        // 用户的最终权限获取
        public static List<BaseModuleEntity> GetPermissionList(BaseUserInfo userInfo, string systemCode, string userId)
        {
            List<BaseModuleEntity> result = null;

            string url = BaseSystemInfo.UserCenterHost + "/UserCenterV42/PermissionService.ashx";
            WebClient webClient = new WebClient();
            NameValueCollection postValues = new NameValueCollection();
            postValues.Add("system", BaseSystemInfo.SoftFullName);
            postValues.Add("systemCode", systemCode);
            postValues.Add("securityKey", BaseSystemInfo.SecurityKey);
            postValues.Add("function", "GetPermissionList");
            postValues.Add("userInfo", userInfo.Serialize());
            postValues.Add("userId", userId);
            
            // 向服务器发送POST数据
            byte[] responseArray = webClient.UploadValues(url, postValues);
            string response = Encoding.UTF8.GetString(responseArray);
            if (!string.IsNullOrEmpty(response))
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                result = javaScriptSerializer.Deserialize<List<BaseModuleEntity>>(response);
            }

            return result;
        }

        // 只判断角色权限
        public static bool CheckPermissionByRole(BaseUserInfo userInfo, string systemCode, string roleId, string permissionCode)
        {
            bool result = false;

            string url = BaseSystemInfo.UserCenterHost + "/UserCenterV42/PermissionService.ashx";
            WebClient webClient = new WebClient();
            NameValueCollection postValues = new NameValueCollection();
            postValues.Add("system", BaseSystemInfo.SoftFullName);
            postValues.Add("systemCode", systemCode);
            postValues.Add("securityKey", BaseSystemInfo.SecurityKey);
            postValues.Add("function", "CheckPermissionByRole");
            postValues.Add("userInfo", userInfo.Serialize());
            postValues.Add("roleId", roleId);
            postValues.Add("permissionCode", permissionCode);
            // 向服务器发送POST数据
            byte[] responseArray = webClient.UploadValues(url, postValues);
            string response = Encoding.UTF8.GetString(responseArray);
            if (!string.IsNullOrEmpty(response))
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                result = javaScriptSerializer.Deserialize<bool>(response);
            }

            return result;
        }

        // 只判断用户权限
        public static bool CheckPermissionByUser(BaseUserInfo userInfo, string systemCode, string userId, string permissionCode)
        {
            bool result = false;

            string url = BaseSystemInfo.UserCenterHost + "/UserCenterV42/PermissionService.ashx";
            WebClient webClient = new WebClient();
            NameValueCollection postValues = new NameValueCollection();
            postValues.Add("system", BaseSystemInfo.SoftFullName);
            postValues.Add("systemCode", systemCode);
            postValues.Add("securityKey", BaseSystemInfo.SecurityKey);
            postValues.Add("function", "CheckPermissionByUser");
            postValues.Add("userInfo", userInfo.Serialize());
            postValues.Add("userId", userId);
            postValues.Add("permissionCode", permissionCode);
            // 向服务器发送POST数据
            byte[] responseArray = webClient.UploadValues(url, postValues);
            string response = Encoding.UTF8.GetString(responseArray);
            if (!string.IsNullOrEmpty(response))
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                result = javaScriptSerializer.Deserialize<bool>(response);
            }

            return result;
        }

        /// <summary>
        /// 获取用户权限主键数组
        /// </summary>
        /// <param name="userInfo">用户</param>
        /// <param name="systemCode">系统编号</param>
        /// <param name="userId">用户主键</param>
        /// <returns>主键数组</returns>
        public static string[] GetUserPermissionIds(BaseUserInfo userInfo, string systemCode, string userId)
        {
            string[] result = null;

            string url = BaseSystemInfo.UserCenterHost + "/UserCenterV42/PermissionService.ashx";
            WebClient webClient = new WebClient();
            NameValueCollection postValues = new NameValueCollection();
            postValues.Add("system", BaseSystemInfo.SoftFullName);
            postValues.Add("systemCode", systemCode);
            postValues.Add("securityKey", BaseSystemInfo.SecurityKey);
            // 2015-11-25 吉日嘎拉，这个要看看，有没有必要设置缓存？
            postValues.Add("function", "GetUserPermissionIds");
            postValues.Add("userInfo", userInfo.Serialize());
            postValues.Add("encrypted", false.ToString());
            postValues.Add("userId", userId);
            // 向服务器发送POST数据
            byte[] responseArray = webClient.UploadValues(url, postValues);
            string response = Encoding.UTF8.GetString(responseArray);
            if (!string.IsNullOrEmpty(response))
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                result = javaScriptSerializer.Deserialize<string[]>(response);
            }

            return result;
        }

        public static int GrantUserPermissions(BaseUserInfo userInfo, string systemCode, string[] userIds, string[] permissionIds)
        {
            int result = 0;

            string url = BaseSystemInfo.UserCenterHost + "/UserCenterV42/PermissionService.ashx";
            WebClient webClient = new WebClient();
            NameValueCollection postValues = new NameValueCollection();
            postValues.Add("system", BaseSystemInfo.SoftFullName);
            postValues.Add("systemCode", systemCode);
            postValues.Add("securityKey", BaseSystemInfo.SecurityKey);
            // 2015-11-25 吉日嘎拉，这个要看看，有没有必要设置缓存？
            postValues.Add("function", "GrantUserPermissions");
            postValues.Add("userInfo", userInfo.Serialize());
            postValues.Add("encrypted", false.ToString());
            postValues.Add("userId", string.Join(",", userIds));
            postValues.Add("permissionId", string.Join(",", permissionIds));
            // 向服务器发送POST数据
            byte[] responseArray = webClient.UploadValues(url, postValues);
            string response = Encoding.UTF8.GetString(responseArray);
            if (!string.IsNullOrEmpty(response))
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                result = javaScriptSerializer.Deserialize<int>(response);
            }

            return result;
        }

        public static int RevokeUserPermissions(BaseUserInfo userInfo, string systemCode, string[] userIds, string[] permissionIds)
        {
            int result = 0;

            string url = BaseSystemInfo.UserCenterHost + "/UserCenterV42/PermissionService.ashx";
            WebClient webClient = new WebClient();
            NameValueCollection postValues = new NameValueCollection();
            postValues.Add("system", BaseSystemInfo.SoftFullName);
            postValues.Add("systemCode", systemCode);
            postValues.Add("securityKey", BaseSystemInfo.SecurityKey);
            // 2015-11-25 吉日嘎拉，这个要看看，有没有必要设置缓存？
            postValues.Add("function", "RevokeUserPermissions");
            postValues.Add("userInfo", userInfo.Serialize());
            postValues.Add("encrypted", false.ToString());
            postValues.Add("userId", string.Join(",", userIds));
            postValues.Add("permissionId", string.Join(",", permissionIds));
            // 向服务器发送POST数据
            byte[] responseArray = webClient.UploadValues(url, postValues);
            string response = Encoding.UTF8.GetString(responseArray);
            if (!string.IsNullOrEmpty(response))
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                result = javaScriptSerializer.Deserialize<int>(response);
            }

            return result;
        }


        /// <summary>
        /// 获取角色权限主键数组
        /// </summary>
        /// <param name="userInfo">用户</param>
        /// <param name="systemCode">系统编号</param>
        /// <param name="roleId">角色主键</param>
        /// <returns>主键数组</returns>
        public static string[] GetRolePermissionIds(BaseUserInfo userInfo, string systemCode, string roleId)
        {
            string[] result = null;

            string url = BaseSystemInfo.UserCenterHost + "/UserCenterV42/PermissionService.ashx";
            WebClient webClient = new WebClient();
            NameValueCollection postValues = new NameValueCollection();
            postValues.Add("system", BaseSystemInfo.SoftFullName);
            postValues.Add("systemCode", systemCode);
            postValues.Add("securityKey", BaseSystemInfo.SecurityKey);
            // 2015-11-25 吉日嘎拉，这个要看看，有没有必要设置缓存？
            postValues.Add("function", "GetRolePermissionIds");
            postValues.Add("userInfo", userInfo.Serialize());
            postValues.Add("encrypted", false.ToString());
            postValues.Add("roleId", roleId);
            // 向服务器发送POST数据
            byte[] responseArray = webClient.UploadValues(url, postValues);
            string response = Encoding.UTF8.GetString(responseArray);
            if (!string.IsNullOrEmpty(response))
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                result = javaScriptSerializer.Deserialize<string[]>(response);
            }

            return result;
        }

        public static int GrantRolePermissions(BaseUserInfo userInfo, string systemCode, string[] roleIds, string[] permissionIds)
        {
            int result = 0;

            string url = BaseSystemInfo.UserCenterHost + "/UserCenterV42/PermissionService.ashx";
            WebClient webClient = new WebClient();
            NameValueCollection postValues = new NameValueCollection();
            postValues.Add("system", BaseSystemInfo.SoftFullName);
            postValues.Add("systemCode", systemCode);
            postValues.Add("securityKey", BaseSystemInfo.SecurityKey);
            // 2015-11-25 吉日嘎拉，这个要看看，有没有必要设置缓存？
            postValues.Add("function", "GrantRolePermissions");
            postValues.Add("userInfo", userInfo.Serialize());
            postValues.Add("encrypted", false.ToString());
            postValues.Add("roleId", string.Join(",", roleIds));
            postValues.Add("permissionId", string.Join(",", permissionIds));
            // 向服务器发送POST数据
            byte[] responseArray = webClient.UploadValues(url, postValues);
            string response = Encoding.UTF8.GetString(responseArray);
            if (!string.IsNullOrEmpty(response))
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                result = javaScriptSerializer.Deserialize<int>(response);
            }

            return result;
        }

        public static int RevokeRolePermissions(BaseUserInfo userInfo, string systemCode, string[] roleIds, string[] permissionIds)
        {
            int result = 0;

            string url = BaseSystemInfo.UserCenterHost + "/UserCenterV42/PermissionService.ashx";
            WebClient webClient = new WebClient();
            NameValueCollection postValues = new NameValueCollection();
            postValues.Add("system", BaseSystemInfo.SoftFullName);
            postValues.Add("systemCode", systemCode);
            postValues.Add("securityKey", BaseSystemInfo.SecurityKey);
            // 2015-11-25 吉日嘎拉，这个要看看，有没有必要设置缓存？
            postValues.Add("function", "RevokeRolePermissions");
            postValues.Add("userInfo", userInfo.Serialize());
            postValues.Add("encrypted", false.ToString());
            postValues.Add("roleId", string.Join(",", roleIds));
            postValues.Add("permissionId", string.Join(",", permissionIds));
            // 向服务器发送POST数据
            byte[] responseArray = webClient.UploadValues(url, postValues);
            string response = Encoding.UTF8.GetString(responseArray);
            if (!string.IsNullOrEmpty(response))
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                result = javaScriptSerializer.Deserialize<int>(response);
            }

            return result;
        }

        public static List<BaseRoleEntity> GetRoleListByPermission(BaseUserInfo userInfo, string systemCode, string userId, string permissionCode)
        {
            List<BaseRoleEntity> result = new List<BaseRoleEntity>();

            string url = BaseSystemInfo.UserCenterHost + "/UserCenterV42/PermissionService.ashx";
            WebClient webClient = new WebClient();
            NameValueCollection postValues = new NameValueCollection();
            postValues.Add("system", BaseSystemInfo.SoftFullName);
            postValues.Add("systemCode", systemCode);
            postValues.Add("securityKey", BaseSystemInfo.SecurityKey);
            // 2015-11-25 吉日嘎拉，这个要看看，有没有必要设置缓存？
            postValues.Add("function", "GetRoleListByPermission");
            postValues.Add("userInfo", userInfo.Serialize());
            postValues.Add("encrypted", false.ToString());
            postValues.Add("userId", userId);
            postValues.Add("permissionCode", permissionCode);
            // 向服务器发送POST数据
            byte[] responseArray = webClient.UploadValues(url, postValues);
            string response = Encoding.UTF8.GetString(responseArray);
            if (!string.IsNullOrEmpty(response))
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                result = javaScriptSerializer.Deserialize<List<BaseRoleEntity>>(response);
            }

            return result;
        }
    }
}
