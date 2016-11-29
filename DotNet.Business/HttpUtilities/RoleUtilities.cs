//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd.  
//-----------------------------------------------------------------

using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Net;
using System.Text;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

namespace DotNet.Business
{
    using DotNet.Model;
    using DotNet.Utilities;

    /// <summary>
    /// RoleUtilities 
    /// 用户服务，远程调用接口
    ///
    /// 修改记录
    ///
    ///		2016.01.06 版本：1.0 JiRiGaLa  远程调用服务。
    ///
    /// </summary>
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2016.01.06</date>
    /// </author>
    /// </summary>
    public class RoleUtilities
    {
        public static List<BaseRoleEntity> GetList(BaseUserInfo userInfo, string systemCode)
        {
            List<BaseRoleEntity> result = new List<BaseRoleEntity>();

            string url = BaseSystemInfo.UserCenterHost + "/UserCenterV42/RoleService.ashx";
            WebClient webClient = new WebClient();
            NameValueCollection postValues = new NameValueCollection();
            postValues.Add("system", BaseSystemInfo.SoftFullName);
            postValues.Add("systemCode", systemCode);
            postValues.Add("securityKey", BaseSystemInfo.SecurityKey);
            // 2015-11-25 吉日嘎拉，这个要看看，有没有必要设置缓存？
            postValues.Add("function", "GetList");
            postValues.Add("userInfo", userInfo.Serialize());
            postValues.Add("encrypted", false.ToString());
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

        public static BaseRoleEntity GetObject(BaseUserInfo userInfo, string systemCode, string id)
        {
            BaseRoleEntity result = null;

            string url = BaseSystemInfo.UserCenterHost + "/UserCenterV42/RoleService.ashx";
            WebClient webClient = new WebClient();
            NameValueCollection postValues = new NameValueCollection();
            postValues.Add("system", BaseSystemInfo.SoftFullName);
            postValues.Add("systemCode", systemCode);
            postValues.Add("securityKey", BaseSystemInfo.SecurityKey);
            postValues.Add("function", "GetObject");
            postValues.Add("userInfo", userInfo.Serialize());
            postValues.Add("encrypted", true.ToString());
            postValues.Add("id", SecretUtil.Encrypt(id));
            // 向服务器发送POST数据
            byte[] responseArray = webClient.UploadValues(url, postValues);
            string response = Encoding.UTF8.GetString(responseArray);
            if (!string.IsNullOrEmpty(response))
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                result = javaScriptSerializer.Deserialize<BaseRoleEntity>(response);
            }

            return result;
        }

        /// <summary>
        /// 1：默认从只读的缓存服务器获取数据
        /// 2：若读取不到，就会到接口上获取，接口会把数据缓存到只读服务器上，为下次阅读提高性能
        /// </summary>
        /// <param name="userInfo"></param>
        /// <param name="systemCode">系统编号</param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static BaseRoleEntity GetObjectByCache(BaseUserInfo userInfo, string systemCode, string id)
        {
            BaseRoleEntity result = null;

            string key = string.Empty;
            if (!string.IsNullOrEmpty(id))
            {
                key = systemCode + ".Role." + id;
            }

            result = BaseRoleManager.GetCacheByKey(key);

            // 远程通过接口获取数据
            if (result == null)
            {
                result = GetObject(userInfo, systemCode, id);
            }

            return result;
        }

        public static int AddUserToRole(BaseUserInfo userInfo, string systemCode, string roleId, string[] userIds)
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
            postValues.Add("roleId", roleId);
            postValues.Add("userId", string.Join(",", userIds));
            // 向服务器发送POST数据
            byte[] responseArray = webClient.UploadValues(url, postValues);
            string response = Encoding.UTF8.GetString(responseArray);
            if (!string.IsNullOrEmpty(response))
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                baseResult = javaScriptSerializer.Deserialize<BaseResult>(response);
            }

            return baseResult.RecordCount;
        }

        public static int RemoveUserFromRole(BaseUserInfo userInfo, string systemCode, string roleId, string[] userIds)
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
            postValues.Add("roleId", roleId);
            postValues.Add("userId", string.Join(",", userIds));
            // 向服务器发送POST数据
            byte[] responseArray = webClient.UploadValues(url, postValues);
            string response = Encoding.UTF8.GetString(responseArray);
            if (!string.IsNullOrEmpty(response))
            {   
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                baseResult = javaScriptSerializer.Deserialize<BaseResult>(response);
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

        public static DataTable GetRoleUserDataTable(BaseUserInfo userInfo, string systemCode, string roleId, string companyId, string userId, string searchValue, out int recordCount, int pageIndex, int pageSize, string orderBy)
        {
            DataTable result = new DataTable(BaseUserEntity.TableName);

            recordCount = 0;

            string url = BaseSystemInfo.UserCenterHost + "/WebAPIV42/API/Role/GetRoleUserDataTable";
            WebClient webClient = new WebClient();
            NameValueCollection postValues = new NameValueCollection();
            postValues.Add("system", BaseSystemInfo.SoftFullName);
            postValues.Add("systemCode", systemCode);
            postValues.Add("securityKey", BaseSystemInfo.SecurityKey);
            // 2015-11-25 吉日嘎拉，这个要看看，有没有必要设置缓存？
            postValues.Add("function", "GetRoleUserList");
            postValues.Add("userInfo", userInfo.Serialize());
            postValues.Add("roleId", roleId);
            postValues.Add("companyId", companyId);
            postValues.Add("userId", userId);
            postValues.Add("searchValue", searchValue);
            postValues.Add("pageIndex", pageIndex.ToString());
            postValues.Add("pageSize", pageSize.ToString());
            postValues.Add("orderBy", orderBy);
            postValues.Add("encrypted", false.ToString());
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

        public static List<BaseUserEntity> GetRoleUserList(BaseUserInfo userInfo, string systemCode, string roleCode, string companyId)
        {
            List<BaseUserEntity> result = new List<BaseUserEntity>();

            string url = BaseSystemInfo.UserCenterHost + "/UserCenterV42/RoleService.ashx";
            WebClient webClient = new WebClient();
            NameValueCollection postValues = new NameValueCollection();
            postValues.Add("system", BaseSystemInfo.SoftFullName);
            postValues.Add("systemCode", systemCode);
            postValues.Add("securityKey", BaseSystemInfo.SecurityKey);
            // 2015-11-25 吉日嘎拉，这个要看看，有没有必要设置缓存？
            postValues.Add("function", "GetRoleUserList");
            postValues.Add("userInfo", userInfo.Serialize());
            postValues.Add("roleCode", roleCode);
            postValues.Add("companyId", companyId);
            postValues.Add("encrypted", false.ToString());
            // 向服务器发送POST数据
            byte[] responseArray = webClient.UploadValues(url, postValues);
            string response = Encoding.UTF8.GetString(responseArray);
            if (!string.IsNullOrEmpty(response))
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                result = javaScriptSerializer.Deserialize<List<BaseUserEntity>>(response);
            }

            return result;
        }
    }
}
