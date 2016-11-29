using Newtonsoft.Json;

namespace OrderSDK.Models
{
    public class ApiResultBase
    {
        /// <summary>
        /// 结果
        /// </summary>
        [JsonProperty(PropertyName = "result")]
        public bool? Result { get; set; }

        /// <summary>
        /// 原因
        /// </summary>
        [JsonProperty(PropertyName = "reason")]
        public string Reason { get; set; }

        [JsonProperty(PropertyName = "status")]
        public bool? Status { get; set; }

        [JsonProperty(PropertyName = "msg")]
        public string Msg { get; set; }
    }

    public class ApiResultBase<T> : ApiResultBase
    {
        [JsonProperty(PropertyName = "data")]
        public T Data { get; set; }
    }
}
