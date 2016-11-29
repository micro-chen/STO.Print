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
    /// DbHelperUtilities 
    /// 数据库服务，内网调用接口
    ///
    /// 修改记录
    ///
    ///		2016.01.19 版本：1.0 JiRiGaLa  远程调用服务。
    ///
    /// </summary>
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2016.01.19</date>
    /// </author>
    /// </summary>
    public class DbHelperUtilities
    {
        public static int ExecuteNonQuery(BaseUserInfo userInfo, string commandText, string commandType = null, List<KeyValuePair<string, object>> dbParameters = null)
        {
            int result = 0;

            string url = BaseSystemInfo.UserCenterHost + "/WebAPIV42/API/DbHelper/ExecuteNonQuery";
            WebClient webClient = new WebClient();
            NameValueCollection postValues = new NameValueCollection();
            postValues.Add("userInfo", userInfo.Serialize());
            postValues.Add("commandText", commandText);
            if (!string.IsNullOrEmpty(commandType))
            {
                postValues.Add("commandType", commandType);
            }
            if (dbParameters != null)
            {
                string dbParametersSerializer = JsonConvert.SerializeObject(dbParameters);
                postValues.Add("dbParameters", dbParametersSerializer);
            }
            // 向服务器发送POST数据
            byte[] responseArray = webClient.UploadValues(url, postValues);
            string responseBody = Encoding.UTF8.GetString(responseArray);
            if (!string.IsNullOrEmpty(responseBody) && !responseBody.Equals("null"))
            {
                BaseResult jsonResult = new BaseResult();
                jsonResult = (BaseResult)JsonConvert.DeserializeObject(responseBody, typeof(BaseResult));

                if (jsonResult != null)
                {
                    result = jsonResult.RecordCount;
                }
            }

            return result;
        }

        public static string ExecuteScalar(BaseUserInfo userInfo, string commandText, string commandType = null, List<KeyValuePair<string, object>> dbParameters = null)
        {
            string result = string.Empty;

            string url = BaseSystemInfo.UserCenterHost + "/WebAPIV42/API/DbHelper/ExecuteScalar";
            WebClient webClient = new WebClient();
            NameValueCollection postValues = new NameValueCollection();
            postValues.Add("userInfo", userInfo.Serialize());
            postValues.Add("commandText", commandText);
            if (!string.IsNullOrEmpty(commandType))
            {
                postValues.Add("commandType", commandType);
            }
            if (dbParameters != null)
            {
                string dbParametersSerializer = JsonConvert.SerializeObject(dbParameters);
                postValues.Add("dbParameters", dbParametersSerializer);
            }
            // 向服务器发送POST数据
            byte[] responseArray = webClient.UploadValues(url, postValues);
            string responseBody = Encoding.UTF8.GetString(responseArray);
            if (!string.IsNullOrEmpty(responseBody) && !responseBody.Equals("null"))
            {
                BaseResult jsonResult = new BaseResult();
                jsonResult = (BaseResult)JsonConvert.DeserializeObject(responseBody, typeof(BaseResult));

                if (jsonResult != null)
                {
                    result = jsonResult.StatusMessage;
                }
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

        public static DataTable GetDataTableByPage(BaseUserInfo userInfo, string tableName, string selectField, out int recordCount, int pageIndex = 1, int pageSize = 100, string whereClause = null, List<KeyValuePair<string, object>> dbParameters = null, string order = null)
        {
            DataTable result = new DataTable(BaseOrganizeEntity.TableName);

            recordCount = 0;
            string url = BaseSystemInfo.UserCenterHost + "/WebAPIV42/API/DbHelper/GetDataTableByPage";
            WebClient webClient = new WebClient();
            NameValueCollection postValues = new NameValueCollection();
            postValues.Add("userInfo", userInfo.Serialize());
            // postValues.Add("recordCount", recordCount.ToString());
            postValues.Add("tableName", tableName);
            postValues.Add("selectField", selectField);
            postValues.Add("pageIndex", pageIndex.ToString());
            postValues.Add("pageSize", pageSize.ToString());
            if (!string.IsNullOrEmpty(whereClause))
            {
                postValues.Add("conditions", whereClause);
            }
            if (dbParameters != null)
            {
                string dbParametersSerializer = JsonConvert.SerializeObject(dbParameters);
                postValues.Add("dbParameters", dbParametersSerializer);
            }
            if (!string.IsNullOrEmpty(order))
            {
                postValues.Add("orderBy", order);
            }

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
