//-------------------------------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2013 , YZ , Ltd .
//-------------------------------------------------------------------------------------

using System;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Xml;

namespace STO.Print.Utilities
{
    using DotNet.Utilities;
    using Newtonsoft.Json;
    using STO.Print.Model;

    /// <summary>
    /// 天气预报类
    ///
    /// 修改纪录
    ///
    ///		2014-3-29 版本：1.0 YangHengLian 创建主键，注意命名空间的排序。
    ///		
    /// 版本：1.0
    ///
    /// <author>
    ///		<name>YangHengLian</name>
    ///		<date>2014-3-29</date>
    /// </author>
    /// </summary>
    public static class WeatherHelper
    {
        #region public static string AutoGetWeatherInfo() 自动检测电脑所在城市获取天气文字信息
        /// <summary>
        /// 自动检测电脑所在城市获取天气文字信息
        /// </summary>
        /// <returns></returns>
        public static string AutoGetWeatherInfo()
        {
            try
            {
                if (!NetworkHelper.IsConnectedInternet())
                {
                    return "";
                }
                var webClient = new WebClient() { Encoding = Encoding.UTF8 };
                //Get location city
                var location = webClient.DownloadString("http://int.dpool.sina.com.cn/iplookup/iplookup.php?format=json");
                var json = new JavaScriptSerializer().Deserialize<dynamic>(location);
                //Read city from utf-8 format
                var city = HttpUtility.UrlDecode(json["city"]);
                //Get weather data(xml format)
                string weather = webClient.DownloadString(string.Format("http://php.weather.sina.com.cn/xml.php?city={0}&password=DJOYnieT8234jlsK&day=0", HttpUtility.UrlEncode(json["city"], Encoding.GetEncoding("GB2312"))));
                //Console.WriteLine(weather);
                var xml = new XmlDocument();
                xml.LoadXml(weather);
                //Get weather detail
                var root = xml.SelectSingleNode("/Profiles/Weather");
                if (root != null)
                {
                    var detail = root["status1"].InnerText + "，" + root["direction1"].InnerText + root["power1"].InnerText.Replace("-", "到") + "级，" + root["gm_s"].InnerText + root["yd_s"].InnerText;
                    return "今天是" + DateTime.Now.ToShortDateString() + "，" + city + " " + detail;
                }
            }
            catch (Exception ex)
            {
                LogUtil.WriteException(ex);
            }
            return null;
        }
        #endregion

        /// <summary>
        /// 获取百度天气信息信息
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static BaiduWeatherEntity GetBaiduWeather(string ip)
        {
            try
            {
                //获取所在城市
                var webClient = new WebClient() { Encoding = Encoding.UTF8 };
                var response = webClient.DownloadString(string.Format("http://ip.taobao.com/service/getIpInfo.php?ip={0}", ip));
                var ipEntity = JsonConvert.DeserializeObject<TaoBaoIpEntity>(response);
                if (ipEntity != null)
                {
                    //获取天气
                    string url = "http://api.map.baidu.com/telematics/v3/weather?location=" + ipEntity.Data.City + "&output=json&ak=" + BaiduMapHelper.ApiKey;
                    response = webClient.DownloadString(url);
                    return JsonConvert.DeserializeObject<BaiduWeatherEntity>(response);
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
