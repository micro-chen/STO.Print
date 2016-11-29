//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd.  
//-----------------------------------------------------------------

using System.Collections.Specialized;
using System.Web;

namespace DotNet.Utilities
{
    public partial class SecretUtil
    {
        /// <summary>
        /// 生成加密url的方法参考
        /// request = "CompanyId=xx&PageIndex=1";
        /// </summary>
        /// <param name="request">请求</param>
        /// <returns>url</returns>
        public static string BuildSecurityRequest(string request)
        {
            return BuildSecurityRequest(BaseSystemInfo.ServiceUserName, request);
        }

        public static string BuildSecurityRequest(BaseUserInfo userInfo, string request)
        {
            return BuildSecurityRequest(userInfo.ServiceUserName, request);
        }

        public static string BuildSecurityRequest(string key, string request)
        {
            // 需要传输的url参数
            string result = string.Empty;
            // 把服务名传输过来，2边能对上才可以
            request = "ServiceUserName=" + key + "&" + request;
            // 把字符串进行加密
            result = SecretUtil.Encrypt(request);
            // 头部在加个8个随机字符
            VerifyCodeImage verifyCodeImage = new VerifyCodeImage();
            result = verifyCodeImage.CreateVerifyCode(8).ToUpper() + result;
            // 需要打开的网址
            return result;
        }

        public static NameValueCollection ProcessSecurityRequest(HttpContext context, out bool valid, out string function)
        {
            valid = false;
            function = string.Empty;

            NameValueCollection result = null;
            // 输入的参数是否有效，进行严格限制访问
            string key = string.Empty;
            if (context.Request["Key"] != null)
            {
                key = context.Request["Key"].ToString();
                if (!string.IsNullOrEmpty(key) && key.Length > 8)
                {
                    // 去掉头部8个多余的字符串
                    key = key.Substring(8);
                    key = SecretUtil.Decrypt(key);
                    if (!string.IsNullOrEmpty(key))
                    {
                        valid = true;
                    }
                }
                else
                {
                    valid = false;
                }
            }
            else
            {
                valid = false;
            }

            if (valid)
            {
                result = HttpUtility.ParseQueryString(key);
                if (!string.IsNullOrEmpty(result["ServiceUserName"]))
                {
                    string serviceUserName = result["ServiceUserName"];
                    valid = BaseSystemInfo.ServiceUserName == serviceUserName;
                }
                if (valid && !string.IsNullOrEmpty(result["Function"]))
                {
                    function = result["Function"];
                }
            }
            return result;
        }
    }
}