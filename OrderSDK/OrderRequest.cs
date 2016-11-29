using System.IO;
using DotNet.Utilities;
using Newtonsoft.Json;
using OrderSDK.Helper;
using System;
using System.Collections.Generic;
using System.Text;
using OrderSDK.HttpUtility;

namespace OrderSDK
{
    /// <summary>
    /// 
    /// </summary>
    public static class OrderRequest
    {
        /// <summary>
        /// 发送请求
        /// </summary>
        /// <param name="data">序列化后的数据</param>
        /// <param name="url">URL</param>
        /// <param name="encoding">编码</param>
        /// <param name="isBase64">是否Base64</param>
        /// <param name="sign">签名</param>
        /// <param name="messageType">消息类型</param>
        /// <param name="companyId">合作ID</param>
        /// <param name="extKv">扩展参数</param>
        /// <returns>执行结果</returns>
        public static string Send(string data, string url, Encoding encoding, bool isBase64, string sign, string messageType, string companyId, IEnumerable<KeyValuePair<string, string>> extKv = null)
        {
            var checkBytes = Encrypt.GetMd5HashBytes(data + sign, encoding);

            var checkStr = isBase64 ? Convert.ToBase64String(checkBytes) : encoding.GetString(checkBytes);

            //checkStr = System.Web.HttpUtility.UrlEncode(checkStr);

            var postDic = new Dictionary<string, string>
            {
                {"data", data},
                {"data_digest", checkStr},
                {"msg_type", messageType},
                {"company_id", companyId}
            };
            if (extKv != null)
            {
                foreach (var kv in extKv)
                {
                    postDic[kv.Key] = kv.Value;
                }
            }
            string result = string.Empty;
            try
            {

                  result = RequestUtility.HttpPost(url, null, postDic, encoding);
            }
            catch (Exception exception)
            {
                LogUtil.WriteException(exception);
            }
            return result;
        }
    }

}
