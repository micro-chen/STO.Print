//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd.  
//-----------------------------------------------------------------

using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace DotNet.Business
{
    using DotNet.Model;
    using DotNet.Utilities;

    /// <summary>
    /// UserUtilities 
    /// 用户服务，远程调用接口
    ///
    /// 修改记录
    ///     2016.01.25 版本: 1.2 huangbin  添加通过唯一用户名从远程和缓存中获取用户的方法
    ///     2015.11.25 版本：1.1 JiRiGaLa  这里还是从缓存里获取就可以了，提高登录的效率。
    ///		2015.07.30 版本：1.0 JiRiGaLa  远程调用服务。
    ///
    /// </summary>
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2015.11.25</date>
    /// </author>
    /// </summary>
    public class UserUtilities
    {
        public static BaseUserEntity GetObject(BaseUserInfo userInfo)
        {
            return GetObject(userInfo, userInfo.Id);
        }

        public static BaseUserEntity GetObject(BaseUserInfo userInfo, string id)
        {
            BaseUserEntity result = null;


            string url = BaseSystemInfo.UserCenterHost + "/UserCenterV42/UserService.ashx";
            WebClient webClient = new WebClient();
            NameValueCollection postValues = new NameValueCollection();
            postValues.Add("system", BaseSystemInfo.SoftFullName);
            postValues.Add("systemCode", BaseSystemInfo.SystemCode);
            postValues.Add("securityKey", BaseSystemInfo.SecurityKey);
            // 2015-11-25 吉日嘎拉，这里还是从缓存里获取就可以了，提高登录的效率。
            postValues.Add("function", "GetObject");
            postValues.Add("userInfo", userInfo.Serialize());
            postValues.Add("encrypted", true.ToString());
            postValues.Add("id", SecretUtil.Encrypt(id));
            // 向服务器发送POST数据
            byte[] responseArray = webClient.UploadValues(url, postValues);
            string response = Encoding.UTF8.GetString(responseArray);
            if (!string.IsNullOrEmpty(response))
            {
                result = JsonConvert.DeserializeObject<BaseUserEntity>(response);
            }

            return result;
        }

        public static BaseUserEntity GetObjectByNickName(BaseUserInfo userInfo, string nickName)
        {
            BaseUserEntity result = null;

            string url = BaseSystemInfo.UserCenterHost + "/UserCenterV42/UserService.ashx";
            WebClient webClient = new WebClient();
            NameValueCollection postValues = new NameValueCollection();
            postValues.Add("system", BaseSystemInfo.SoftFullName);
            postValues.Add("systemCode", BaseSystemInfo.SystemCode);
            postValues.Add("securityKey", BaseSystemInfo.SecurityKey);
            // 2015-11-25 吉日嘎拉，这里还是从缓存里获取就可以了，提高登录的效率。
            postValues.Add("function", "GetObjectByNickName");
            postValues.Add("userInfo", userInfo.Serialize());
            postValues.Add("encrypted", true.ToString());
            postValues.Add("nickName", SecretUtil.Encrypt(nickName));
            // 向服务器发送POST数据
            byte[] responseArray = webClient.UploadValues(url, postValues);
            string response = Encoding.UTF8.GetString(responseArray);
            if (!string.IsNullOrEmpty(response))
            {
                result = JsonConvert.DeserializeObject<BaseUserEntity>(response);
            }

            return result;
        }

        public static BaseUserEntity GetObjectByNickNameByCache(BaseUserInfo userInfo, string nickName)
        {
            BaseUserEntity result = null;

            if (string.IsNullOrEmpty(nickName))
            {
                return result;
            }

            string key = "User:ByNickName:" + nickName.ToLower();
            result = BaseUserManager.GetObjectByNickNameByCache(key);

            // 远程通过接口获取数据
            if (result == null)
            {
                result = GetObjectByNickName(userInfo, nickName);
            }

            return result;
        }

        /// <summary>
        /// 1：默认从只读的缓存服务器获取数据
        /// 2：若读取不到，就会到接口上获取，接口会把数据缓存到只读服务器上，为下次阅读提高性能
        /// </summary>
        /// <param name="userInfo"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static BaseUserEntity GetObjectByCache(BaseUserInfo userInfo, string id)
        {
            BaseUserEntity result = null;

            string key = "User:" + id;
            result = BaseUserManager.GetCacheByKey(key);

            // 远程通过接口获取数据
            if (result == null)
            {
                result = GetObject(userInfo, id);
            }

            return result;
        }

        public static BaseUserContactEntity GetUserContactObject(BaseUserInfo userInfo)
        {
            return GetUserContactObject(userInfo, userInfo.Id);
        }

        public static BaseUserContactEntity GetUserContactObject(BaseUserInfo userInfo, string id)
        {
            BaseUserContactEntity result = null;

            string url = BaseSystemInfo.UserCenterHost + "/UserCenterV42/UserService.ashx";

            WebClient webClient = new WebClient();
            NameValueCollection postValues = new NameValueCollection();
            postValues.Add("system", BaseSystemInfo.SoftFullName);
            postValues.Add("systemCode", BaseSystemInfo.SystemCode);
            postValues.Add("securityKey", BaseSystemInfo.SecurityKey);
            // 2015-11-25 吉日嘎拉，这里还是从缓存里获取就可以了，提高登录的效率。
            postValues.Add("function", "GetUserContactObject");
            postValues.Add("userInfo", userInfo.Serialize());
            postValues.Add("encrypted", true.ToString());
            postValues.Add("id", SecretUtil.Encrypt(id));
            // 向服务器发送POST数据
            byte[] responseArray = webClient.UploadValues(url, postValues);
            string response = Encoding.UTF8.GetString(responseArray);
            if (!string.IsNullOrEmpty(response))
            {
                result = JsonConvert.DeserializeObject<BaseUserContactEntity>(response);
            }

            return result;
        }

        public static BaseUserContactEntity GetUserContactObjectByCache(BaseUserInfo userInfo, string id)
        {
            BaseUserContactEntity result = null;

            string key = "UserContact:" + id;
            result = BaseUserContactManager.GetCacheByKey(key);

            // 远程通过接口获取数据
            if (result == null)
            {
                result = GetUserContactObject(userInfo, id);
            }

            return result;
        }

        public static BaseUserLogOnEntity GetUserLogOnObject(BaseUserInfo userInfo)
        {
            return GetUserLogOnObject(userInfo, userInfo.Id);
        }

        public static BaseUserLogOnEntity GetUserLogOnObject(BaseUserInfo userInfo, string id)
        {
            BaseUserLogOnEntity result = null;

            string url = BaseSystemInfo.UserCenterHost + "/UserCenterV42/UserService.ashx";
            WebClient webClient = new WebClient();
            NameValueCollection postValues = new NameValueCollection();
            postValues.Add("system", BaseSystemInfo.SoftFullName);
            postValues.Add("systemCode", BaseSystemInfo.SystemCode);
            postValues.Add("securityKey", BaseSystemInfo.SecurityKey);
            // 2015-11-25 吉日嘎拉，这个要看看，有没有必要设置缓存？
            postValues.Add("function", "GetUserLogOnObject");
            postValues.Add("userInfo", userInfo.Serialize());
            postValues.Add("encrypted", true.ToString());
            postValues.Add("id", SecretUtil.Encrypt(id));
            // 向服务器发送POST数据
            byte[] responseArray = webClient.UploadValues(url, postValues);
            string response = Encoding.UTF8.GetString(responseArray);
            if (!string.IsNullOrEmpty(response))
            {
                result = JsonConvert.DeserializeObject<BaseUserLogOnEntity>(response);
            }

            return result;
        }

        /// <summary>
        /// 用户是否在某个角色里
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        /// <param name="systemCode">系统编号</param>
        /// <param name="userId">用户主键</param>
        /// <param name="roleCode">角色编号</param>
        /// <returns>在角色里</returns>
        public static bool IsInRoleByCode(BaseUserInfo userInfo, string systemCode, string userId, string roleCode)
        {
            bool result = false;

            string url = BaseSystemInfo.UserCenterHost + "/UserCenterV42/UserService.ashx";
            WebClient webClient = new WebClient();
            NameValueCollection postValues = new NameValueCollection();
            postValues.Add("system", BaseSystemInfo.SoftFullName);
            postValues.Add("systemCode", systemCode);
            postValues.Add("securityKey", BaseSystemInfo.SecurityKey);
            postValues.Add("function", "IsInRoleByCode");
            postValues.Add("userInfo", userInfo.Serialize());
            postValues.Add("userId", userId);
            postValues.Add("roleCode", roleCode);
            // 向服务器发送POST数据
            byte[] responseArray = webClient.UploadValues(url, postValues);
            string response = Encoding.UTF8.GetString(responseArray);
            if (!string.IsNullOrEmpty(response))
            {
                result = JsonConvert.DeserializeObject<bool>(response);
            }

            return result;
        }

        /// <summary>
        /// 获取用户的角色列表
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        /// <param name="systemCode">系统编号</param>
        /// <param name="userId">用户主键</param>
        /// <returns>角色列表</returns>
        public static List<BaseRoleEntity> GetUserRoleList(BaseUserInfo userInfo, string systemCode, string userId)
        {
            List<BaseRoleEntity> result = new List<BaseRoleEntity>();

            string url = BaseSystemInfo.UserCenterHost + "/UserCenterV42/UserService.ashx";
            WebClient webClient = new WebClient();
            NameValueCollection postValues = new NameValueCollection();
            postValues.Add("system", BaseSystemInfo.SoftFullName);
            postValues.Add("systemCode", systemCode);
            postValues.Add("securityKey", BaseSystemInfo.SecurityKey);
            postValues.Add("function", "GetUserRoleList");
            postValues.Add("userInfo", userInfo.Serialize());
            postValues.Add("userId", userId);
            // 向服务器发送POST数据
            byte[] responseArray = webClient.UploadValues(url, postValues);
            string response = Encoding.UTF8.GetString(responseArray);
            if (!string.IsNullOrEmpty(response))
            {
                result = JsonConvert.DeserializeObject<List<BaseRoleEntity>>(response);
            }

            return result;
        }


        /// <summary>
        /// 获取用户数据权限（区域Id）
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        /// <param name="systemCode">系统编号</param>
        /// <param name="permissionCode">操作菜单的code</param>
        /// <param name="userId">用户主键</param>
        /// <returns>角色列表</returns>
        public static string[] GetUserAreas(BaseUserInfo userInfo, string permissionCode, string userId, string systemCode)
        {
            string url = BaseSystemInfo.UserCenterHost + "/UserCenterV42/UserService.ashx";
            WebClient webClient = new WebClient();
            NameValueCollection postValues = new NameValueCollection();
            postValues.Add("system", BaseSystemInfo.SoftFullName);
            postValues.Add("systemCode", systemCode);
            postValues.Add("securityKey", BaseSystemInfo.SecurityKey);
            postValues.Add("function", "GetUserAreas");
            postValues.Add("userInfo", userInfo.Serialize());
            postValues.Add("userId", userId);
            postValues.Add("permissionCode", permissionCode);
            // 向服务器发送POST数据
            byte[] responseArray = webClient.UploadValues(url, postValues);
            string response = Encoding.UTF8.GetString(responseArray);
            string[] result=null;
            if (!string.IsNullOrEmpty(response))
            {
                result = response.Split(',');
            }
            return result;
        }


        /// <summary>
        /// 将用户添加到角色中
        /// </summary>
        /// <param name="userInfo"></param>
        /// <param name="systemCode"></param>
        /// <param name="userId"></param>
        /// <param name="roleIds"></param>
        /// <returns></returns>
        public static int AddUserToRole(BaseUserInfo userInfo, string systemCode, string userId, string[] roleIds)
        {
            BaseResult baseResult = new BaseResult();

            string url = BaseSystemInfo.UserCenterHost + "/UserCenterV42/RoleService.ashx";
            WebClient webClient = new WebClient();
            NameValueCollection postValues = new NameValueCollection();
            postValues.Add("system", BaseSystemInfo.SoftFullName);
            postValues.Add("systemCode", systemCode);
            postValues.Add("securityKey", BaseSystemInfo.SecurityKey);
            // 2015-11-25 吉日嘎拉，这个要看看，有没有必要设置缓存？
            postValues.Add("function", "AddUserToRole");
            postValues.Add("userInfo", userInfo.Serialize());
            postValues.Add("encrypted", false.ToString());
            postValues.Add("userId", userId);
            postValues.Add("roleId", string.Join(",", roleIds));
            // 向服务器发送POST数据
            byte[] responseArray = webClient.UploadValues(url, postValues);
            string response = Encoding.UTF8.GetString(responseArray);
            if (!string.IsNullOrEmpty(response))
            {
                baseResult = JsonConvert.DeserializeObject<BaseResult>(response);
            }
            
            return baseResult.RecordCount;
        }

        /// <summary>
        /// 从角色中移除用户
        /// </summary>
        /// <param name="userInfo"></param>
        /// <param name="systemCode"></param>
        /// <param name="userId"></param>
        /// <param name="roleIds"></param>
        /// <returns></returns>
        public static int RemoveUserFromRole(BaseUserInfo userInfo, string systemCode, string userId, string[] roleIds)
        {
            BaseResult baseResult = new BaseResult();

            string url = BaseSystemInfo.UserCenterHost + "/UserCenterV42/RoleService.ashx";
            WebClient webClient = new WebClient();
            NameValueCollection postValues = new NameValueCollection();
            postValues.Add("system", BaseSystemInfo.SoftFullName);
            postValues.Add("systemCode", systemCode);
            postValues.Add("securityKey", BaseSystemInfo.SecurityKey);
            // 2015-11-25 吉日嘎拉，这个要看看，有没有必要设置缓存？
            postValues.Add("function", "RemoveUserFromRole");
            postValues.Add("userInfo", userInfo.Serialize());
            postValues.Add("encrypted", false.ToString());
            postValues.Add("userId", userId);
            postValues.Add("roleId", string.Join(",", roleIds));
            // 向服务器发送POST数据
            byte[] responseArray = webClient.UploadValues(url, postValues);
            string response = Encoding.UTF8.GetString(responseArray);
            if (!string.IsNullOrEmpty(response))
            {
                baseResult = JsonConvert.DeserializeObject<BaseResult>(response);
            }

            return baseResult.RecordCount;
        }

        public class JsonResult
        {
            public DataTable Data { get; set; }

            public int RecordCount { get; set; }

            public bool Status { get; set; }

            public string StatusCode { get; set; }

            public string StatusMessage { get; set; }
        }

        public static DataTable GetDataTableByPage(BaseUserInfo userInfo, string selectField, out int recordCount, int pageIndex, int pageSize, string whereClause, List<KeyValuePair<string, object>> dbParameters, string order = null)
        {
            DataTable result = new DataTable(BaseOrganizeEntity.TableName);

            recordCount = 0;
            string url = BaseSystemInfo.UserCenterHost + "/WebAPIV42/API/User/GetDataTableByPage";
            WebClient webClient = new WebClient();
            NameValueCollection postValues = new NameValueCollection();
            postValues.Add("userInfo", userInfo.Serialize());
            // postValues.Add("recordCount", recordCount.ToString());
            postValues.Add("tableName", BaseUserEntity.TableName);
            if (!string.IsNullOrWhiteSpace(selectField))
            {
                postValues.Add("selectField", selectField);
            }
            postValues.Add("pageIndex", pageIndex.ToString());
            postValues.Add("pageSize", pageSize.ToString());
            postValues.Add("conditions", whereClause);
            string dbParametersSerializer = JsonConvert.SerializeObject(dbParameters);
            postValues.Add("dbParameters", dbParametersSerializer);
            postValues.Add("orderBy", order);
            // 向服务器发送POST数据
            byte[] responseArray = webClient.UploadValues(url, postValues);
            string responseBody = Encoding.UTF8.GetString(responseArray);
            if (!string.IsNullOrEmpty(responseBody) && !responseBody.Equals("null"))
            {
                JsonResult jsonResult = new JsonResult();
                jsonResult = (JsonResult)JsonConvert.DeserializeObject(responseBody, typeof(JsonResult));

                if (jsonResult != null)
                {
                    result = jsonResult.Data;
                    recordCount = jsonResult.RecordCount;
                }
            }

            return result;
        }
    }
}
