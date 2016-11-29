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
    /// DepartmentUtilities 
    /// 部门服务，远程调用接口
    ///
    /// 修改记录
    ///
    ///		2015.09.25 版本：1.0 JiRiGaLa  远程调用服务。
    ///
    /// </summary>
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2015.09.25</date>
    /// </author>
    /// </summary>
    public class DepartmentUtilities
    {
        public static BaseDepartmentEntity GetObject(BaseUserInfo userInfo)
        {
            return GetObject(userInfo, userInfo.Id);
        }

        public static BaseDepartmentEntity GetObject(BaseUserInfo userInfo, string id)
        {
            BaseDepartmentEntity result = null;

            string url = BaseSystemInfo.UserCenterHost + "/UserCenterV42/DepartmentService.ashx";
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
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                result = javaScriptSerializer.Deserialize<BaseDepartmentEntity>(response);
            }

            return result;
        }

        public static BaseDepartmentEntity GetObjectByName(BaseUserInfo userInfo, string companyId, string fullName)
        {
            BaseDepartmentEntity result = null;

            string url = BaseSystemInfo.UserCenterHost + "/UserCenterV42/DepartmentService.ashx";
            WebClient webClient = new WebClient();
            NameValueCollection postValues = new NameValueCollection();
            postValues.Add("system", BaseSystemInfo.SoftFullName);
            postValues.Add("systemCode", BaseSystemInfo.SystemCode);
            postValues.Add("securityKey", BaseSystemInfo.SecurityKey);
            postValues.Add("function", "GetObjectByName");
            postValues.Add("userInfo", userInfo.Serialize());
            postValues.Add("encrypted", true.ToString());
            postValues.Add("companyId", SecretUtil.Encrypt(companyId));
            postValues.Add("fullName", SecretUtil.Encrypt(fullName));
            // 向服务器发送POST数据
            byte[] responseArray = webClient.UploadValues(url, postValues);
            string response = Encoding.UTF8.GetString(responseArray);
            if (!string.IsNullOrEmpty(response))
            {
                result = JsonConvert.DeserializeObject<BaseDepartmentEntity>(response);
            }

            return result;
        }

        public static BaseDepartmentEntity GetObjectByNameByCache(BaseUserInfo userInfo, string companyId, string fullName)
        {
            BaseDepartmentEntity result = null;

            string key = "DBN:" + companyId + ":" + fullName;
            string id = string.Empty;
            using (var redisClient = PooledRedisHelper.GetReadOnlyClient())
            {
                id = redisClient.Get<string>(key);
                if (!string.IsNullOrWhiteSpace(id))
                {
                    key = "D:" + id;
                    result = redisClient.Get<BaseDepartmentEntity>(key);
                }
            }
            
            // 远程通过接口获取数据
            if (result == null)
            {
                result = GetObjectByName(userInfo, companyId, fullName);
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
            DataTable result = new DataTable(BaseDepartmentEntity.TableName);

            recordCount = 0;
            string url = BaseSystemInfo.UserCenterHost + "/WebAPIV42/API/Department/GetDataTableByPage";
            WebClient webClient = new WebClient();
            NameValueCollection postValues = new NameValueCollection();
            postValues.Add("userInfo", userInfo.Serialize());
            // postValues.Add("recordCount", recordCount.ToString());
            postValues.Add("tableName", BaseDepartmentEntity.TableName);
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
