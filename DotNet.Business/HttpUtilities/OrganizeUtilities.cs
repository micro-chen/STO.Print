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
    /// OrganizeUtilities 
    /// 组织机构服务，远程调用接口
    ///
    /// 修改记录
    ///
    ///		2016.01.08 版本：1.1 JiRiGaLa  GetObjectByCache 功能定位明确。
    ///		2015.07.31 版本：1.0 JiRiGaLa  远程调用服务。
    ///
    /// </summary>
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2015.07.31</date>
    /// </author>
    /// </summary>
    public class OrganizeUtilities
    {
        public static BaseOrganizeEntity GetObject(BaseUserInfo userInfo)
        {
            return GetObject(userInfo, userInfo.CompanyId);
        }

        public static BaseOrganizeEntity GetObject(BaseUserInfo userInfo, string id)
        {
            BaseOrganizeEntity result = null;

            string url = BaseSystemInfo.UserCenterHost + "/UserCenterV42/OrganizeService.ashx";
            WebClient webClient = new WebClient();
            NameValueCollection postValues = new NameValueCollection();
            postValues.Add("system", BaseSystemInfo.SoftFullName);
            postValues.Add("systemCode", BaseSystemInfo.SystemCode);
            postValues.Add("securityKey", BaseSystemInfo.SecurityKey);
            // 2015-11-25 吉日嘎拉，这里还是从缓存里获取就可以了，提高登录的效率。
            // postValues.Add("function", "GetObject");
            postValues.Add("function", "GetObjectByCache");
            postValues.Add("userInfo", userInfo.Serialize());
            postValues.Add("encrypted", true.ToString());
            postValues.Add("id", SecretUtil.Encrypt(id));
            // 向服务器发送POST数据
            byte[] responseArray = webClient.UploadValues(url, postValues);
            string response = Encoding.UTF8.GetString(responseArray);
            if (!string.IsNullOrEmpty(response))
            {
                result = JsonConvert.DeserializeObject<BaseOrganizeEntity>(response);
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
        public static BaseOrganizeEntity GetObjectByCache(BaseUserInfo userInfo, string id)
        {
            BaseOrganizeEntity result = null;

            string key = "O:" + id;
            result = BaseOrganizeManager.GetCacheByKey(key);

            // 远程通过接口获取数据
            if (result == null)
            {
                result = GetObject(userInfo, id);
            }

            return result;
        }

        public static BaseOrganizeEntity GetObjectByName(BaseUserInfo userInfo, string fullName)
        {
            BaseOrganizeEntity result = null;

            string url = BaseSystemInfo.UserCenterHost + "/UserCenterV42/OrganizeService.ashx";
            WebClient webClient = new WebClient();
            NameValueCollection postValues = new NameValueCollection();
            postValues.Add("system", BaseSystemInfo.SoftFullName);
            postValues.Add("systemCode", BaseSystemInfo.SystemCode);
            postValues.Add("securityKey", BaseSystemInfo.SecurityKey);
            postValues.Add("function", "GetObjectByName");
            postValues.Add("userInfo", userInfo.Serialize());
            postValues.Add("encrypted", true.ToString());
            postValues.Add("fullName", SecretUtil.Encrypt(fullName));
            // 向服务器发送POST数据
            byte[] responseArray = webClient.UploadValues(url, postValues);
            string response = Encoding.UTF8.GetString(responseArray);
            if (!string.IsNullOrEmpty(response))
            {
                result = JsonConvert.DeserializeObject<BaseOrganizeEntity>(response);
            }

            return result;
        }

        public static BaseOrganizeEntity GetObjectByNameByCache(BaseUserInfo userInfo, string fullName)
        {
            BaseOrganizeEntity result = null;

            string key = "OBN:" + fullName;
            string id = string.Empty;
            using (var redisClient = PooledRedisHelper.GetReadOnlyClient())
            {
                id = redisClient.Get<string>(key);
            }
            if (!string.IsNullOrEmpty(id))
            {
                key = "O:" + id;
                result = BaseOrganizeManager.GetCacheByKey(key);
            }

            // 远程通过接口获取数据
            if (result == null)
            {
                result = GetObjectByName(userInfo, fullName);
            }

            return result;
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
            string url = BaseSystemInfo.UserCenterHost + "/WebAPIV42/API/Organize/GetDataTableByPage";
            WebClient webClient = new WebClient();
            NameValueCollection postValues = new NameValueCollection();
            postValues.Add("userInfo", userInfo.Serialize());
            // postValues.Add("recordCount", recordCount.ToString());
            postValues.Add("tableName", BaseOrganizeEntity.TableName);
            postValues.Add("selectField", selectField);
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
