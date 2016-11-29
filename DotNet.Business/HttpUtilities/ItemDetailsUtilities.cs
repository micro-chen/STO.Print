//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd.  
//-----------------------------------------------------------------

using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace DotNet.Business
{
    using DotNet.Model;
    using DotNet.Utilities;

    /// <summary>
    /// ItemDetailsUtilities 
    /// 选项服务，远程调用接口
    ///
    /// 修改记录
    ///
    ///		2016.01.12 版本：1.0 JiRiGaLa  远程调用服务。
    ///
    /// </summary>
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2016.01.12</date>
    /// </author>
    /// </summary>
    public class ItemDetailsUtilities
    {
        public static BaseItemDetailsEntity GetObject(BaseUserInfo userInfo, string tableName, string id)
        {
            BaseItemDetailsEntity result = null;

            string url = BaseSystemInfo.UserCenterHost + "/UserCenterV42/ItemDetailsService.ashx";
            WebClient webClient = new WebClient();
            NameValueCollection postValues = new NameValueCollection();
            postValues.Add("system", BaseSystemInfo.SoftFullName);
            postValues.Add("systemCode", BaseSystemInfo.SystemCode);
            postValues.Add("securityKey", BaseSystemInfo.SecurityKey);
            // 2015-11-25 吉日嘎拉，这里还是从缓存里获取就可以了，提高登录的效率。
            postValues.Add("function", "GetObject");
            postValues.Add("tableName", tableName);
            postValues.Add("userInfo", userInfo.Serialize());
            postValues.Add("encrypted", true.ToString());
            postValues.Add("id", SecretUtil.Encrypt(id));
            // 向服务器发送POST数据
            byte[] responseArray = webClient.UploadValues(url, postValues);
            string response = Encoding.UTF8.GetString(responseArray);
            if (!string.IsNullOrEmpty(response))
            {
                result = JsonConvert.DeserializeObject<BaseItemDetailsEntity>(response);
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
        public static BaseItemDetailsEntity GetObjectByCache(BaseUserInfo userInfo, string tableName, string id)
        {
            BaseItemDetailsEntity result = null;

            string key = "ItemDetails:" + tableName + ":" + id;
            result = BaseItemDetailsManager.GetCache(key);

            // 远程通过接口获取数据
            if (result == null)
            {
                result = GetObject(userInfo, tableName, id);
            }

            return result;
        }

        public static List<BaseItemDetailsEntity> GetListByTargetTable(BaseUserInfo userInfo, string systemCode, string tableName)
        {
            List<BaseItemDetailsEntity> result = null;

            string url = BaseSystemInfo.UserCenterHost + "/UserCenterV42/ItemDetailsService.ashx";
            WebClient webClient = new WebClient();
            NameValueCollection postValues = new NameValueCollection();
            postValues.Add("system", BaseSystemInfo.SoftFullName);
            postValues.Add("systemCode", systemCode);
            postValues.Add("securityKey", BaseSystemInfo.SecurityKey);
            // 2015-11-25 吉日嘎拉，这里还是从缓存里获取就可以了，提高登录的效率。
            postValues.Add("function", "GetList");
            postValues.Add("userInfo", userInfo.Serialize());
            postValues.Add("tableName", tableName);
            // 向服务器发送POST数据
            byte[] responseArray = webClient.UploadValues(url, postValues);
            string response = Encoding.UTF8.GetString(responseArray);
            if (!string.IsNullOrEmpty(response))
            {
                result = JsonConvert.DeserializeObject<List<BaseItemDetailsEntity>>(response);
            }

            return result;
        }

        public static List<BaseItemDetailsEntity> GetListByTargetTableByCache(BaseUserInfo userInfo, string systemCode, string tableName)
        {
            List<BaseItemDetailsEntity> result = null;

            if (!string.IsNullOrEmpty(tableName))
            {
                string key = "ItemDetails:" + tableName;
                result = BaseItemDetailsManager.GetListCache(key);
            }

            // 远程通过接口获取数据
            if (result == null)
            {
                result = GetListByTargetTable(userInfo, systemCode, tableName);
            }

            return result;
        }
    }
}
