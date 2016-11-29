using System;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace STO.Print.Utilities
{
    using DotNet.Utilities;
    /// <summary>
    /// 网络帮助类
    /// </summary>
    public class NetworkHelper
    {
        #region public static bool IsConnectedInternet() 检测本机是否联网（互联网）
        [DllImport("wininet")]
        //http://baike.baidu.com/view/560670.htm（这是百度对wininet的解释）
        private extern static bool InternetGetConnectedState(out int connectionDescription, int reservedValue);
        /// <summary>
        /// 检测本机是否联网
        /// </summary>
        /// <returns></returns>
        public static bool IsConnectedInternet()
        {
            try
            {
                int i;
                return InternetGetConnectedState(out i, 0);
            }
            catch (Exception ex)
            {
                LogUtil.WriteException(ex);
                return false;
            }
        }

        #endregion

        ///// <summary>
        ///// 获取本机IP和运营商信息
        ///// <remarks>您的IP是：[180.166.232.222] 来自：上海市 电信</remarks>
        ///// </summary>
        ///// <returns></returns>
        //public static string GetIpInfo()
        //{
        //    try
        //    {
        //        var responseText = GetResponseText("http://www.ip138.com/");
        //        var mc2 = Regex.Matches(responseText, "^((768|765|778|779|719|828|618|680|518|688|010|880|660|805|988|628|205|717|718|728|738|761|762|763|701|757|751|359|358|100|200|118|128|689|738|528|852)[0-9]{9})$|^((5711|2008|2009|2010|2013)[0-9]{8})$|^((8010|8021)[0-9]{6})$|^(1111[0-9]{10})$|^((a|b|h)[0-9]{13})$|^((90|10|19)[0-9]{12})$|^((5)[0-9]{9})$|^((80|88|89|91|92|93|94|95|96|97|99|110|111|112|113|114|115|116|117|118|119|200|21|22|120|121|122|123|124|125|126|127|128|129|130|131)[0-9]{8})$|^((8|9)[0-9]{7})$|^((91|92|93|94|95|98|36|68|39|50|53|37)[0-9]{10})$|^(100|101|102|103|104|105|106|107|503|504|505|506|507)[0-9]{10}$");
        //        string href = null;
        //        foreach (Match m in mc2)
        //        {
        //            href = m.Groups[1].Value;
        //        }
        //        if (string.IsNullOrEmpty(href))
        //        {
        //            return null;
        //        }
        //        var responseText2 = GetResponseText(href);
        //        return NoHtml(responseText2).Replace("您的IP地址", "");
        //    }
        //    catch (Exception ex)
        //    {
        //        LogUtil.WriteException(ex);
        //    }
        //    return "";
        //}


        /// <summary>
        /// 获取本机IP和运营商信息
        /// <remarks>您的IP是：[180.166.232.222] 来自：上海市 电信</remarks>
        /// </summary>
        /// <returns></returns>
        public static string GetIpInfo()
        {
            var responseText = GetResponseText("http://www.ip138.com/");
            var mc2 = Regex.Matches(responseText, "<iframe[\\s\\S]*?src=\"([\\s\\S]*?)\"[\\s\\S]*?>([\\s\\S]*?)</iframe>");
            string href = null;
            foreach (Match m in mc2)
            {
                // 获取到 iframe标签中的url地址
                href = m.Groups[1].Value;
            }
            if (string.IsNullOrEmpty(href))
            {
                return null;
            }
            var responseText2 = GetResponseText(href);
            return FilterHtml(responseText2).Replace("您的IP地址", "");
        }

        public static string GetIpAddress()
        {
            var ipInfo = GetIpInfo();
            if (string.IsNullOrEmpty(ipInfo)){
                return "";
            }
            var mc2 = Regex.Matches(ipInfo, @"((?:(?:25[0-5]|2[0-4]\d|((1\d{2})|([1-9]?\d)))\.){3}(?:25[0-5]|2[0-4]\d|((1\d{2})|([1-9]?\d))))");
            string ip = "";
            foreach (Match m in mc2)
            {
                ip = m.Groups[0].Value;
            }
            return ip;
        }
        /// <summary>
        /// 根据区县获取邮编
        /// <remarks>区县名称</remarks>
        /// </summary>
        /// <remarks>http://www.cnblogs.com/xdpxyxy/archive/2013/05/05/3061317.html 使用的GB2312编码格式</remarks>
        /// <returns></returns>
        public static string GetPostCodeByAddress(string city, string county)
        {
            try
            {
                if (city == "上海市" || city == "天津市" || city == "重庆市" || city == "北京市")
                {
                    city = city.Substring(0, city.Length - 1);
                }
                //请求的url http://www.ip138.com/post/search.asp?area=%C8%E7%B6%AB%CF%D8&action=area2zip
                var url = "http://www.ip138.com/post/search.asp?action=area2zip&area=" + System.Web.HttpUtility.UrlEncode(county, System.Text.Encoding.GetEncoding("GB2312"));
                var responseText = GetResponseText(url);
                var mc2 = Regex.Matches(responseText, "<td[\\s\\S]*?>([\\s\\S]*?)</td>");
                string href = null;
                foreach (Match m in mc2)
                {
                    // 获取到 iframe标签中的url地址
                    href = m.Groups[1].Value;
                    if (href.Contains("邮编"))
                    {
                        if (href.Contains(city))
                        {
                            break;
                        }
                        //  break;
                    }
                }
                if (string.IsNullOrEmpty(href))
                {
                    return null;
                }

                href = FilterHtml(href).Replace("&nbsp;", "").Replace("◎", "");
                var index = href.IndexOf("邮编", StringComparison.Ordinal);
                if (index > 0)
                {
                    return href.Substring(index + 3, 6);
                }
            }
            catch (Exception exception)
            {
                LogUtil.WriteException(exception);
            }
            return "";
        }

        /// <summary>
        /// Http请求Url获取响应字节流文字信息
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        static string GetResponseText(string url)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Timeout = 5000;
            request.Method = "GET";
            var response = (HttpWebResponse)request.GetResponse();
            var encode = System.Text.Encoding.GetEncoding("gb2312");
            var sr = new StreamReader(response.GetResponseStream(), encode);
            return sr.ReadToEnd();
        }

        /// <summary>
        /// 去除html标签
        /// </summary>
        /// <param name="htmlstring"></param>
        /// <returns></returns>
        public static string FilterHtml(string htmlstring)
        {
            Regex regex = new Regex("<[^>]+>", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            Regex regex2 = new Regex(@"\&nbsp\;", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            Regex regex3 = new Regex(@"\s{2,}|\ \;", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            Regex regex4 = new Regex("<style(.*?)</style>", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            Regex regex5 = new Regex("<script(.*?)</script>", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            htmlstring = regex4.Replace(htmlstring, string.Empty);
            htmlstring = regex5.Replace(htmlstring, string.Empty);
            htmlstring = regex.Replace(htmlstring, string.Empty);
            htmlstring = regex2.Replace(htmlstring, " ");
            htmlstring = regex3.Replace(htmlstring, " ");
            return htmlstring.Trim();
        }
    }
}
