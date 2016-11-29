//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2015 , STO TECH, Ltd. 
//-----------------------------------------------------------------

using Newtonsoft.Json;

namespace STO.Print.Model.ZtoOrderBatchResponseEntityJsonTypes
{

    public class Keys
    {

        /// <summary>
        /// 订单号
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// 订单号
        /// </summary>
        [JsonProperty("orderid")]
        public string Orderid { get; set; }

        /// <summary>
        /// 电子面单单号
        /// </summary>
        [JsonProperty("mailno")]
        public string Mailno { get; set; }

        /// <summary>
        /// 淘宝给的大头笔（目前是这样的，最好用自己的大头笔，这样省去很多的罚款）
        /// </summary>
        [JsonProperty("mark")]
        public string Mark { get; set; }

        /// <summary>
        /// 网点编号
        /// </summary>
        [JsonProperty("sitecode")]
        public string Sitecode { get; set; }

        /// <summary>
        /// 网点名称
        /// </summary>
        [JsonProperty("sitename")]
        public string Sitename { get; set; }
    }

}
