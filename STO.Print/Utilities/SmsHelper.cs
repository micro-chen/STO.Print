//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2015 , ZTO TECH, Ltd. 
//-----------------------------------------------------------------

using System;
using System.IO;
using System.Net;
using System.Text;

namespace ZTO.Print.Utilities
{
    public class SmsHelper
    {

        /**
         *   SmsHelper method = new SmsHelper();
                var result  = method.GetResult(method.PostSmsInfo(method.GetPostUrl("15162406378", "你好")));
         * */
        /// <summary>
        /// 用户名
        /// </summary>
        private const string TheUid = ""; 
        /// <summary>
        /// 接口秘钥
        /// </summary>
        private const string TheKey = ""; 

        /// <summary>
        /// 
        /// </summary>
        /// <param name="smsMob">手机号码</param>
        /// <param name="smsText">短信内容</param>
        /// <returns>返回UTF-8编码发送接口地址</returns>
        public string GetPostUrl(string smsMob, string smsText)
        {
            string postUrl = "http://utf8.sms.webchinese.cn/?Uid=" + TheUid + "&key=" + TheKey + "&smsMob=" + smsMob + "&smsText=" + smsText;
            return postUrl;
        }

        /// <summary>
        /// 发送短信，得到返回值
        /// </summary>
        /// <param name="url">请求的Url</param>
        /// <returns></returns>
        public string PostSmsInfo(string url)
        {
            //调用时只需要把拼成的URL传给该函数即可。判断返回值即可
            string strRet = null;
            if (url == null || url.Trim() == "")
            {
                return strRet;
            }
            string targeturl = url.Trim();
            try
            {
                HttpWebRequest hr = (HttpWebRequest)WebRequest.Create(targeturl);
                hr.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1)";
                hr.Method = "GET";
                hr.Timeout = 30 * 60 * 1000;
                WebResponse hs = hr.GetResponse();
                Stream sr = hs.GetResponseStream();
                StreamReader ser = new StreamReader(sr, Encoding.Default);
                strRet = ser.ReadToEnd();
            }
            catch (Exception ex)
            {
                strRet = null;
            }
            return strRet;
        }

        /// <summary>
        /// 确认返回信息
        /// </summary>
        /// <param name="strRet">返回状态值</param>
        /// <returns></returns>
        public string GetResult(string strRet)
        {
            int result = 0;
            try
            {
                result = int.Parse(strRet);
                switch (result)
                {
                    case -1:
                        strRet = "没有该用户账户";
                        break;
                    case -2:
                        strRet = "接口密钥不正确,不是账户登陆密码";
                        break;
                    case -21:
                        strRet = "MD5接口密钥加密不正确";
                        break;
                    case -3:
                        strRet = "短信数量不足";
                        break;
                    case -11:
                        strRet = "该用户被禁用";
                        break;
                    case -14:
                        strRet = "短信内容出现非法字符";
                        break;
                    case -4:
                        strRet = "手机号格式不正确";
                        break;
                    case -41:
                        strRet = "手机号码为空";
                        break;
                    case -42:
                        strRet = "短信内容为空";
                        break;
                    case -51:
                        strRet = "短信签名格式不正确,接口签名格式为：【签名内容】";
                        break;
                    case -6:
                        strRet = "IP限制";
                        break;
                    default:
                        strRet = "发送短信数量：" + result;
                        break;
                }
            }
            catch (Exception ex)
            {
                strRet = ex.Message;
            }
            return strRet;
        }
    }
}