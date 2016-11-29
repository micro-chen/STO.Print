//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2015 , STO TECH, Ltd. 
//-----------------------------------------------------------------

using Newtonsoft.Json;

namespace STO.Print.Model.ZtoOrderBatchEntityJsonTypes
{

    public class Sender
    {

        /// <summary>
        /// 发件人在合作商平台中的ID号
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// 发件人姓名
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// 发件公司名
        /// </summary>
        [JsonProperty("company")]
        public string Company { get; set; }

        /// <summary>
        /// 发件人手机号码
        /// </summary>
        [JsonProperty("mobile")]
        public object Mobile { get; set; }

        /// <summary>
        /// 发件人电话号码
        /// </summary>
        [JsonProperty("phone")]
        public string Phone { get; set; }

        /// <summary>
        /// 发件人区域ID，如提供区域ID，请参考申通速递提供的国家行政区划代码。
        /// </summary>
        [JsonProperty("area")]
        public string Area { get; set; }

        /// <summary>
        /// 发件人所在城市，必须逐级指定，用英文半角逗号分隔，目前至少需要指定到区县级，如能往下精确更好，如“上海市,上海市,青浦区,华新镇,华志路,123号”
        /// </summary>
        [JsonProperty("city")]
        public string City { get; set; }

        /// <summary>
        /// 发件人路名门牌等地址信息
        /// </summary>
        [JsonProperty("address")]
        public string Address { get; set; }

        /// <summary>
        /// 发件人邮政编码
        /// </summary>
        [JsonProperty("zipcode")]
        public string Zipcode { get; set; }

        /// <summary>
        /// 发件人电子邮件
        /// </summary>
        [JsonProperty("email")]
        public string Email { get; set; }

        /// <summary>
        /// 发件人即时通讯工具
        /// </summary>
        [JsonProperty("im")]
        public string Im { get; set; }

        /// <summary>
        /// 取件起始时间
        /// </summary>
        [JsonProperty("starttime")]
        public string Starttime { get; set; }

        /// <summary>
        /// 取件截至时间
        /// </summary>
        [JsonProperty("endtime")]
        public string Endtime { get; set; }

        /// <summary>
        /// 是否需要获取大头笔信息 true(默认为需要获取大头笔信息)
        /// </summary>
        [JsonProperty("isremark")]
        public string Isremark { get; set; }
    }

}
