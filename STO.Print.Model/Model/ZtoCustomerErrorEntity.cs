//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2015 , STO TECH, Ltd. 
//-----------------------------------------------------------------

using Newtonsoft.Json;

namespace STO.Print.Model
{
    /// <summary>
    /// 请求申通批量获取电子面单接口返回异常信息实体
    /// 
    /// {"result": "false","id": "1305201420132343122311231","code": "s17","keys": "sender->name","remark": "数据类容太长"}
    /// </summary>
    public class ZtoCustomerErrorEntity
    {
        /// <summary>
        /// 返回值（true或者false）
        /// </summary>
        [JsonProperty("result")]
        public string Result { get; set; }

        /// <summary>
        /// 错误编码
        /// </summary>
        [JsonProperty("code")]
        public string Code { get; set; }

        /// <summary>
        /// 错误描述
        /// </summary>
        [JsonProperty("remark")]
        public string Remark { get; set; }

        /// <summary>
        /// 错误实体ID
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// 校验返回的Key
        /// </summary>
        [JsonProperty("keys")]
        public string Keys { get; set; }
    }

}
