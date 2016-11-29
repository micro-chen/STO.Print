//-------------------------------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2015 , STO , Ltd .
//-------------------------------------------------------------------------------------

using System;
using System.Collections.Specialized;
using System.Globalization;
using System.Net;
using System.Text;

namespace STO.Print.Utilities
{
    using Model;
    using Newtonsoft.Json;

    /// <summary>
    /// 百度接口帮助类
    ///
    /// 修改纪录
    ///
    ///		2015-08-21  版本：1.0 YangHengLian 创建主键，注意命名空间的排序。
    /// 
    /// 版本：1.0
    ///
    /// <author>
    ///		<name>YangHengLian</name>
    ///		<date>2015-08-21</date>
    /// </author>
    /// </summary>
    public class BaiduMapHelper
    {
        /// <summary>
        /// 百度接口Key
        /// </summary>
        public static string ApiKey = "SyG68R5W3MbsoINKwA2mcmfC";

        /// <summary>
        /// 请求百度地图的Url
        /// </summary>
        const string Url = "http://api.map.baidu.com/geocoder/v2/";

        /// <summary>
        /// 根据街道地址获取百度地图实际的地址实体信息
        /// </summary>
        /// <param name="address">街道地址（可以不详细）</param>
        /// <returns></returns>
        public static BaiduAddressEntity GetProvCityDistFromBaiduMap(string address)
        {
            try
            {
                WebClient webClient = new WebClient();
                NameValueCollection postValues = new NameValueCollection
                {
                    {"ak", ApiKey},
                    {"output", "json"}, 
                    {"address", address}
                };
                // 向服务器发送POST数据
                byte[] responseArray = webClient.UploadValues(Url, postValues);
                string response = Encoding.UTF8.GetString(responseArray);
                if (!string.IsNullOrEmpty(response))
                {
                    BaiduBackEntity backEntity = JsonConvert.DeserializeObject<BaiduBackEntity>(response);
                    if (backEntity != null && backEntity.Status == 1)
                    {
                        return null;
                    }
                    BaiduLocation location = JsonConvert.DeserializeObject<BaiduLocation>(response);
                    if (location != null)
                    {
                        return GetAddressFromBaidu(location.Result.Location.Lat.ToString(CultureInfo.InvariantCulture), location.Result.Location.Lng.ToString(CultureInfo.InvariantCulture));
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }
            return null;
        }

        /// <summary>
        /// 根据经纬度获取百度的省市区实体信息
        /// </summary>
        /// <param name="latitude">纬度</param>
        /// <param name="longitude">经度</param>
        /// <returns></returns>
        public static BaiduAddressEntity GetAddressFromBaidu(string latitude, string longitude)
        {
            WebClient webClient = new WebClient();
            NameValueCollection postValues = new NameValueCollection
            {
                {"ak", ApiKey}, 
                {"output", "json"},
                {"pois", "0"}, 
                {"location", latitude + "," + longitude}
            };
            // 向服务器发送POST数据
            byte[] responseArray = webClient.UploadValues(Url, postValues);
            string response = Encoding.UTF8.GetString(responseArray);
            if (!string.IsNullOrEmpty(response))
            {
                var baiduAddressEntity = JsonConvert.DeserializeObject<BaiduAddressEntity>(response);
                // 针对几个直辖市的省市区需要特殊处理（上海-上海市-青浦区）这是系统的，但是百度返回的是（上海市-上海市-青浦区），所以系统不认识
                if (baiduAddressEntity.Result.AddressComponent.City == baiduAddressEntity.Result.AddressComponent.Province)
                {
                    baiduAddressEntity.Result.AddressComponent.Province = baiduAddressEntity.Result.AddressComponent.Province.Substring(0, baiduAddressEntity.Result.AddressComponent.Province.Length - 1);
                    return baiduAddressEntity;
                }
                // 来自系统的：新疆维吾尔自治区-图木舒克市-图木舒克市
                // 来自百度的：新疆维吾尔自治区-新疆维吾尔自治区直辖县级行政单位-图木舒克市
                if (baiduAddressEntity.Result.AddressComponent.District == "图木舒克市" && baiduAddressEntity.Result.AddressComponent.City == "新疆维吾尔自治区直辖县级行政单位")
                {
                    baiduAddressEntity.Result.AddressComponent.City = baiduAddressEntity.Result.AddressComponent.District;
                }
                // 来自系统的：广东省-东莞市-东莞市
                // 来自百度的：广东省-东莞市-东莞市市辖区
                // 广东省东莞市莞城区创业新村31座A101
                if (baiduAddressEntity.Result.AddressComponent.District == "东莞市市辖区" && baiduAddressEntity.Result.AddressComponent.City == "东莞市")
                {
                    baiduAddressEntity.Result.AddressComponent.District = "东莞市";
                }
                // 来自系统的：湖北省-仙桃市-仙桃市
                // 来自百度的：湖北省-湖北省直辖县级行政单位-仙桃市
                // 湖北省直辖仙桃市市一路200号 
                // 这个qq反馈的错误（79170-南昌昌南-打印专家提出好的建议的  18779176845）
                if (baiduAddressEntity.Result.AddressComponent.City == "湖北省直辖县级行政单位" && baiduAddressEntity.Result.AddressComponent.District == "仙桃市")
                {
                    baiduAddressEntity.Result.AddressComponent.City = "仙桃市";
                }
                // 来自系统的：湖北省-神农架林区-神农架林区
                // 来自百度的：湖北省-湖北省直辖县级行政单位-神农架林区
                // 湖北省神农架林区林荫北街13号信息大厦808室
                // 这个qq反馈的错误（79170-南昌昌南-打印专家提出好的建议的  18779176845）
                if (baiduAddressEntity.Result.AddressComponent.City == "湖北省直辖县级行政单位" && baiduAddressEntity.Result.AddressComponent.District == "神农架林区")
                {
                    baiduAddressEntity.Result.AddressComponent.City = "神农架林区";
                }
                // 来自系统的：湖北省-潜江市-潜江市
                // 来自百度的：湖北省	湖北省-湖北省直辖县级行政单位-潜江市
                // 湖北省湖北省直辖潜江市薀川路5475号1983室
                // 这个qq反馈的错误（79170-南昌昌南-打印专家提出好的建议的  18779176845）
                if (baiduAddressEntity.Result.AddressComponent.City == "湖北省直辖县级行政单位" && baiduAddressEntity.Result.AddressComponent.District == "潜江市")
                {
                    baiduAddressEntity.Result.AddressComponent.City = "潜江市";
                }
                // 来自系统的：河南省-济源市-济源市
                // 来自百度的：河南省-河南省直辖县级行政单位-济源市
                // 河南省直辖县级行政单位济源市济源县
                // 这个qq反馈的错误（79170-南昌昌南-打印专家提出好的建议的  18779176845）
                if (baiduAddressEntity.Result.AddressComponent.City == "河南省直辖县级行政单位" && baiduAddressEntity.Result.AddressComponent.District == "济源市")
                {
                    baiduAddressEntity.Result.AddressComponent.City = "济源市";
                }
                return baiduAddressEntity;
            }
            return null;
        }
    }
}
