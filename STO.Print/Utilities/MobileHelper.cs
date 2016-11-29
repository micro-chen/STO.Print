//-------------------------------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2015 , STO , Ltd .
//-------------------------------------------------------------------------------------

using System;

namespace STO.Print.Utilities
{
    using Newtonsoft.Json;

    /// <summary>
    /// 手机号码帮助类
    ///
    /// 修改纪录
    ///
    ///		  2015-03-15  版本：1.0 YangHengLian 创建主键，注意命名空间的排序。
    ///     
    /// 
    /// 版本：1.0
    ///
    /// <author>
    ///		<name>YangHengLian</name>
    ///		<date>2015-03-15</date>
    /// </author>
    /// <remarks>
    /// 查询页面url：http://api.showji.com/Locating/query.aspx?m=15185856378
    /// 各接口对应的手机号码参数，可以识别大多数的手机号码格式，全角半角均可，例如：139、１３９１２３４、13912345678、0139-1234-5678 等
    /// Web 页面的 URL 中表示手机号码的参数 m，除了支持上述格式外，还支持以下编码的 URI 格式：UTF-8，ANSI（GB2312 / GBK / GB13000 / GB18030），Unicode。
    /// </remarks>
    /// </summary>
    public class MobileHelper
    {
        /// <summary>
        /// 获取手机号码归属地信息
        /// </summary>
        /// <param name="mobile">手机号码（11位数字）</param>
        /// <returns></returns>
        public static MobileBelongInfo GetMobileInfo(string mobile)
        {
            // 调用接口url，返回json字符串
            // json格式：{"Mobile":"15263632541","QueryResult":"True","TO":"中国移动","Corp":"中国移动","Province":"山东","City":"潍坊","AreaCode":"0536","PostCode":"261000","VNO":"","Card":""}
            // http://v.showji.com/Locating/showji.com2016234999234.aspx?m=15162406378&output=json&callback=querycallback&timestamp=1431853106275
            // http://v.showji.com/Locating/q.js 这一段js就是请求的url，返回json
            var url = string.Format("http://v.showji.com/Locating/showji.com2016234999234.aspx?m={0}&output=json&callback=querycallback&timestamp={1}", mobile, DateTime.Now.Ticks);
            var responseText = HttpClientHelper.Get(url);
            if (!string.IsNullOrEmpty(responseText))
            {
                responseText = responseText.Replace("querycallback(", "").Replace(");", "");
                var jsonEntity = JsonConvert.DeserializeObject<MobileBelongInfo>(responseText);
                return jsonEntity;
            }
            return null;
        }
    }

    public class MobileBelongInfo
    {
        /// <summary>
        /// 判断手机号码格式是否正确
        /// </summary>
        public bool QueryResult { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 所属省份
        /// </summary>
        public string Province { get; set; }

        /// <summary>
        /// 所属城市
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// 区号
        /// </summary>
        public string AreaCode { get; set; }

        /// <summary>
        /// 邮编
        /// </summary>
        public string PostCode { get; set; }

        /// <summary>
        /// 运营商
        /// </summary>
        public string Corp { get; set; }

        /// <summary>
        /// 卡类型
        /// </summary>
        public string Card { get; set; }
    }
}
