using Newtonsoft.Json;
using System;

namespace OrderSDK.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class OrderCountBase
    {
        /// <summary>
        /// 站点名称或编号
        /// </summary>
        [JsonProperty(PropertyName = "site")]
        public string Site { get; set; }

        /// <summary>
        /// 起始时间
        /// </summary>
        [JsonProperty(PropertyName = "starttime")]
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        [JsonProperty(PropertyName = "endtime")]
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// 订单数量
        /// </summary>
        [JsonProperty(PropertyName = "count")]
        public int? Count { get; set; }
    }
}
