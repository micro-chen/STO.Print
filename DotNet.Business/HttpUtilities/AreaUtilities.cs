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
    /// AreaUtilities 
    /// 区域服务，远程调用接口
    ///
    /// 修改记录
    ///
    ///		2016.01.11 版本：1.0 JiRiGaLa  远程调用服务。
    ///
    /// </summary>
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2016.01.11</date>
    /// </author>
    /// </summary>
    public class AreaUtilities
    {
        public static BaseAreaEntity GetObject(BaseUserInfo userInfo, string id)
        {
            BaseAreaEntity result = null;

            string url = BaseSystemInfo.UserCenterHost + "/UserCenterV42/AreaService.ashx";
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
                result = JsonConvert.DeserializeObject<BaseAreaEntity>(response);
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
        public static BaseAreaEntity GetObjectByCache(BaseUserInfo userInfo, string id)
        {
            BaseAreaEntity result = null;

            string key = "Area:" + id;
            result = BaseAreaManager.GetCache(key);

            // 远程通过接口获取数据
            if (result == null)
            {
                result = GetObject(userInfo, id);
            }

            return result;
        }

        public static List<BaseAreaEntity> GetProvinceList(BaseUserInfo userInfo)
        {
            List<BaseAreaEntity> result = null;

            string url = BaseSystemInfo.UserCenterHost + "/UserCenterV42/AreaService.ashx";
            WebClient webClient = new WebClient();
            NameValueCollection postValues = new NameValueCollection();
            postValues.Add("system", BaseSystemInfo.SoftFullName);
            postValues.Add("systemCode", BaseSystemInfo.SystemCode);
            postValues.Add("securityKey", BaseSystemInfo.SecurityKey);
            // 2015-11-25 吉日嘎拉，这里还是从缓存里获取就可以了，提高登录的效率。
            postValues.Add("function", "GetProvinceList");
            postValues.Add("userInfo", userInfo.Serialize());
            // 向服务器发送POST数据
            byte[] responseArray = webClient.UploadValues(url, postValues);
            string response = Encoding.UTF8.GetString(responseArray);
            if (!string.IsNullOrEmpty(response))
            {
                result = JsonConvert.DeserializeObject<List<BaseAreaEntity>>(response);
            }

            return result;
        }

        public static List<BaseAreaEntity> GetListByParent(BaseUserInfo userInfo, string parentId)
        {
            List<BaseAreaEntity> result = null;

            string url = BaseSystemInfo.UserCenterHost + "/UserCenterV42/AreaService.ashx";
            WebClient webClient = new WebClient();
            NameValueCollection postValues = new NameValueCollection();
            postValues.Add("system", BaseSystemInfo.SoftFullName);
            postValues.Add("systemCode", BaseSystemInfo.SystemCode);
            postValues.Add("securityKey", BaseSystemInfo.SecurityKey);
            // 2015-11-25 吉日嘎拉，这里还是从缓存里获取就可以了，提高登录的效率。
            postValues.Add("function", "GetListByParent");
            postValues.Add("userInfo", userInfo.Serialize());
            postValues.Add("parentId", parentId);
            // 向服务器发送POST数据
            byte[] responseArray = webClient.UploadValues(url, postValues);
            string response = Encoding.UTF8.GetString(responseArray);
            if (!string.IsNullOrEmpty(response))
            {
                result = JsonConvert.DeserializeObject<List<BaseAreaEntity>>(response);
            }

            return result;
        }
    }
}
