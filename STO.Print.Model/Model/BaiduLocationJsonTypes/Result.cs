//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2015 , STO TECH, Ltd. 
//-----------------------------------------------------------------

using Newtonsoft.Json;

namespace STO.Print.Model.BaiduLocationJsonTypes
{
    /// <summary>
    /// 百度地图解析返回的json实体
    /// </summary>
    public class Result
    {
        /// <summary>
        /// 经纬度实体
        /// </summary>
        [JsonProperty("location")]
        public Location Location { get; set; }

        /// <summary>
        /// 精密; 精确的; 清晰的; 正规的;
        /// </summary>
        [JsonProperty("precise")]
        public int Precise { get; set; }

        [JsonProperty("confidence")]
        public int Confidence { get; set; }

        /// <summary>
        /// 等级
        /// </summary>
        [JsonProperty("level")]
        public string Level { get; set; }
    }

}
