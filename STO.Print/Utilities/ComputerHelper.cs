//-------------------------------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2015 , STO , Ltd .
//-------------------------------------------------------------------------------------

using System;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace STO.Print.Utilities
{
    using DotNet.Utilities;

    /// <summary>
    /// 电脑信息处理类
    ///
    /// 修改纪录
    ///
    ///		2014-04-01 版本：1.0 YangHengLian 创建主键，注意命名空间的排序。
    /// 
    /// 版本：1.0
    ///
    /// <author>
    ///		<name>YangHengLian</name>
    ///		<date>2014-04-01</date>
    /// </author>
    /// </summary>
    public class ComputerHelper
    {
        /// <summary>
        /// 获取Ip，返回所在城市，和网络名称（电信，铁通，网通，联通，移动）
        /// </summary>
        /// <param name="url">请求的url</param>
        /// <param name="ipCity">ip所在城市</param>
        /// <param name="netWorkName">网络名称：电信，联通等</param>
        /// <returns></returns>
        public static string GetIpInfo(string url, ref string ipCity, ref string netWorkName)
        {
            if (string.IsNullOrEmpty(url))
            {
                url = "http://20140507.ip138.com/ic.asp";//这是ip138网站请求
            }
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(url);
                request.Timeout = 5000;
                request.Method = "GET";
                var response = (HttpWebResponse)request.GetResponse();
                var encode = Encoding.GetEncoding("gb2312");
                var sr = new StreamReader(response.GetResponseStream(), encode);
                var htmlstring = sr.ReadToEnd();
                htmlstring = Regex.Replace(htmlstring, "<.*?>", "", RegexOptions.IgnoreCase);
                htmlstring = htmlstring.Replace("[lt]", string.Empty).Replace("[gt]", string.Empty).Replace("[", string.Empty);
                htmlstring = htmlstring.Replace("\r", string.Empty).Replace("\n", string.Empty);
                //您的IP地址 116.228.70.118 上海市 电信
                htmlstring = htmlstring.Replace("您的IP是：", string.Empty).Replace("来自：", string.Empty).Trim().Replace("您的IP地址", string.Empty);
                var charIndex = htmlstring.IndexOf(']');
                if (charIndex > 0)
                {
                    ipCity = htmlstring.Substring(charIndex + 1).Trim();
                    netWorkName = ipCity.Substring(ipCity.Length - 2);
                    ipCity = ipCity.Substring(0, ipCity.Length - 2).Trim();
                    return htmlstring.Substring(0, charIndex);
                }
            }
            catch (Exception ex)
            {
                LogUtil.WriteException(ex);
            }
            return null;
        }

        #region public static string GetServerTime() 获取服务器时间，如果不正确就更新电脑的时间为服务器时间
        /// <summary>
        /// 获取服务器时间，如果不正确就更新电脑的时间为服务器时间
        /// </summary>
        /// <returns></returns>
        public static string GetServerDataTime()
        {
            try
            {
                string url = BaseSystemInfo.WebHost + @"UserCenterV42/LogOnService.ashx?" + "function=GetServerDateTime";
                string serverTime = string.Empty;
                if (BaseSystemInfo.OnInternet)
                {
                    serverTime = DotNet.Business.Utilities.GetResponse(url);
                    if (!string.IsNullOrEmpty(serverTime) && DotNet.Utilities.ValidateUtil.IsDateTime(serverTime))
                    {
                        MachineInfo.SetDateTimeFormat();
                        MachineInfo.SetLocalTime(DateTime.Parse(serverTime));
                    }
                }
                return serverTime;
            }
            catch (Exception)
            {
                return "";
            }
        }
        #endregion
    }
}
