//-------------------------------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2015 , STO , Ltd .
//-------------------------------------------------------------------------------------

using System;
using System.IO;
using System.Net;
using System.Text;

namespace STO.Print.Utilities
{
    using DotNet.Utilities;

    /// <summary>
    /// 网络请求类
    ///
    /// 修改纪录
    ///
    ///		  2014-08-31  版本：1.0 YangHengLian 创建主键，注意命名空间的排序。
    /// 
    /// 版本：1.0
    ///
    /// <author>
    ///		<name>YangHengLian</name>
    ///		<date>2014-08-31</date>
    /// </author>
    /// <remarks>
    /// 继承WebClient类 
    /// 提供向 URI 标识的资源发送数据和从 URI 标识的资源接收数据的公共方法 
    /// 支持以 http:、https:、ftp:、和 file: 方案标识符开头的 URI 
    /// </remarks>
    /// </summary>
    public class HttpClientHelper : WebClient
    {
        #region 远程POST数据并返回数据
        /// <summary> 
        /// 利用WebClient 远程POST数据并返回数据 
        /// </summary> 
        /// <param name="strUrl">远程URL地址</param> 
        /// <param name="strParams">参数</param> 
        /// <param name="respEncode">POST数据的编码</param> 
        /// <param name="reqEncode">获取数据的编码</param> 
        /// <returns></returns> 
        public static string PostData(string strUrl, string strParams, Encoding respEncode, Encoding reqEncode)
        {
            using (var httpclient = new HttpClientHelper())
            {
                try
                {
                    //打开页面 
                    httpclient.Credentials = CredentialCache.DefaultCredentials;
                    //从指定的URI下载资源 
                    byte[] responseData = httpclient.DownloadData(strUrl);
                    string srcString;
                    httpclient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                    string postString = strParams;
                    // 将字符串转换成字节数组 
                    byte[] postData = Encoding.ASCII.GetBytes(postString);
                    // 上传数据，返回页面的字节数组 
                    responseData = httpclient.UploadData(strUrl, "POST", postData);
                    srcString = reqEncode.GetString(responseData);
                    return srcString;
                }
                catch (Exception ex)
                {
                    LogUtil.WriteException(ex);
                    return string.Empty;
                }
            }
        }
        #endregion

        /// <summary> 
        /// 利用WebClient 远程POST XML数据并返回数据 
        /// </summary> 
        /// <param name="strUrl">远程URL地址</param> 
        /// <param name="strParams">参数</param> 
        /// <param name="respEncode">响应数据编码</param> 
        /// <param name="reqEncode">请求数据编码</param> 
        /// <returns></returns> 
        public static string PostXmlData(string strUrl, string strParams, Encoding reqEncode, Encoding respEncode)
        {
            try
            {
                //GC.Collect();
                //ServicePointManager.DefaultConnectionLimit = 50;
                var request = WebRequest.Create(strUrl) as HttpWebRequest;
                string reponseData = null;
                if (request != null)
                {
                    // 设置最大超时值
                   // request.Timeout = 5000;
                    request.Method = "POST";
                    request.KeepAlive = false;
                    request.ContentType = "multipart/form-data"; //ContentType.Text;request.KeepAlive = false;
                    request.ProtocolVersion = HttpVersion.Version10;
                    var writer = new StreamWriter(request.GetRequestStream(), reqEncode);
                    writer.Write(strParams);
                    writer.Close();
                    var response = request.GetResponse() as HttpWebResponse;
                    if (response != null)
                    {
                        var responseStream = response.GetResponseStream();
                        if (responseStream != null)
                        {
                            var reader = new StreamReader(responseStream, respEncode);
                            reponseData = reader.ReadToEnd();
                            reader.Close();
                            responseStream.Close();
                        }
                    }
                    if (response != null)
                    {
                        response.Close();
                    }
                    request.Abort();
                }
                return reponseData;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// 抓取网页html代码
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string Get(string url)
        {
            WebRequest wrt = WebRequest.Create(url);
            WebResponse wrse = wrt.GetResponse();
            Stream strM = wrse.GetResponseStream();
            if (strM != null)
            {
                var sr = new StreamReader(strM, Encoding.GetEncoding("UTF-8"));
                string strallstrm = sr.ReadToEnd();
                sr.Close();
                return strallstrm;
            }
            return null;
        }
    }
}
