//-------------------------------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2015 , STO , Ltd .
//-------------------------------------------------------------------------------------

using System;
using STO.Print.AddBillForm;

namespace STO.Print
{
    using STO.Print.Model;
    using STO.Print.Utilities;

    /// <summary>
    ///  天气查询窗体
    ///
    /// 修改纪录
    ///
    ///		2015-09-16  版本：1.0 YangHengLian 创建主键，注意命名空间的排序。
    /// 
    /// 版本：1.0
    ///
    /// <author>
    ///		<name>YangHengLian</name>
    ///		<date>2015-09-16</date>
    /// </author>
    /// </summary>
    public partial class FrmBaiduWeather : BaseForm
    {

        public FrmBaiduWeather()
        {
            InitializeComponent();
        }

        private void FrmBaiduWeather_Load(object sender, EventArgs e)
        {
            var ip = NetworkHelper.GetIpAddress();
            BaiduWeatherEntity weatherEntity = WeatherHelper.GetBaiduWeather(ip);
            //{"error":0,"status":"success","date":"2015-11-03","results":[{"currentCity":"上海市","pm25":"147","index":[{"title":"穿衣","zs":"较舒适","tipt":"穿衣指数","des":"建议着薄外套、开衫牛仔衫裤等服装。年老体弱者应适当添加衣物，宜着夹克衫、薄毛衣等。"},{"title":"洗车","zs":"较适宜","tipt":"洗车指数","des":"较适宜洗车，未来一天无雨，风力较小，擦洗一新的汽车至少能保持一天。"},{"title":"旅游","zs":"适宜","tipt":"旅游指数","des":"天气较好，但丝毫不会影响您出行的心情。温度适宜又有微风相伴，适宜旅游。"},{"title":"感冒","zs":"较易发","tipt":"感冒指数","des":"天气较凉，较易发生感冒，请适当增加衣服。体质较弱的朋友尤其应该注意防护。"},{"title":"运动","zs":"较适宜","tipt":"运动指数","des":"天气较好，无雨水困扰，较适宜进行各种运动，但因气温较低，在户外运动请注意增减衣物。"},{"title":"紫外线强度","zs":"弱","tipt":"紫外线强度指数","des":"紫外线强度较弱，建议出门前涂擦SPF在12-15之间、PA+的防晒护肤品。"}],"weather_data":[{"date":"周二 11月03日 (实时：19℃)","dayPictureUrl":"http://api.map.baidu.com/images/weather/day/duoyun.png","nightPictureUrl":"http://api.map.baidu.com/images/weather/night/duoyun.png","weather":"多云","wind":"东风微风","temperature":"20 ~ 12℃"},{"date":"周三","dayPictureUrl":"http://api.map.baidu.com/images/weather/day/duoyun.png","nightPictureUrl":"http://api.map.baidu.com/images/weather/night/xiaoyu.png","weather":"多云转小雨","wind":"东南风微风","temperature":"21 ~ 17℃"},{"date":"周四","dayPictureUrl":"http://api.map.baidu.com/images/weather/day/xiaoyu.png","nightPictureUrl":"http://api.map.baidu.com/images/weather/night/xiaoyu.png","weather":"小雨","wind":"东南风微风","temperature":"23 ~ 18℃"},{"date":"周五","dayPictureUrl":"http://api.map.baidu.com/images/weather/day/xiaoyu.png","nightPictureUrl":"http://api.map.baidu.com/images/weather/night/xiaoyu.png","weather":"小雨","wind":"东南风微风","temperature":"22 ~ 19℃"}]}]}
            if (weatherEntity != null)
            {
                gridControl1.DataSource = weatherEntity.Results[0].WeatherData;
                this.Text = weatherEntity.Results[0].CurrentCity + "天气";
            }
        }
    }
}
