//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2015 , STO TECH, Ltd. 
//-----------------------------------------------------------------

using Newtonsoft.Json;

namespace STO.Print.Model.ZtoOrderBatchEntityJsonTypes
{

    public class Receiver
    {

        /// <summary>
        /// 收件人在合作商平台中的ID号
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// 收件人姓名
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// 收件公司名
        /// </summary>
        [JsonProperty("company")]
        public string Company { get; set; }

        /// <summary>
        /// 收件人手机号码
        /// </summary>
        [JsonProperty("mobile")]
        public string Mobile { get; set; }

        /// <summary>
        /// 收件人电话号码
        /// </summary>
        [JsonProperty("phone")]
        public string Phone { get; set; }

        /// <summary>
        /// 收件人电话号码
        /// </summary>
        [JsonProperty("area")]
        public string Area { get; set; }

        /// <summary>
        /// 收件人区域ID，如提供区域ID，请参考申通速递提供的国家行政区划代码。
        /// 例子：四川省,成都市,武侯区
        /// </summary>
        [JsonProperty("city")]
        public string City { get; set; }

        /// <summary>
        /// 收件人所在城市，逐级指定，用英文半角逗号分隔
        /// 育德路497号
        /// </summary>
        [JsonProperty("address")]
        public string Address { get; set; }

        /// <summary>
        /// 收件人邮政编码
        /// 610012
        /// </summary>
        [JsonProperty("zipcode")]
        public string Zipcode { get; set; }

        /// <summary>
        /// 收件人电子邮件
        /// yyj@abc.com
        /// </summary>
        [JsonProperty("email")]
        public string Email { get; set; }

        /// <summary>
        /// 收件人即时通讯工具
        /// </summary>
        [JsonProperty("im")]
        public string Im { get; set; }
    }

}
