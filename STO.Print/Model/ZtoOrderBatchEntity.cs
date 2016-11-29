//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2015 , STO TECH, Ltd. 
//-----------------------------------------------------------------

using Newtonsoft.Json;
using STO.Print.Model.ZtoOrderBatchEntityJsonTypes;

namespace STO.Print.Model
{
    /// <summary>
    /// 批量下单，修改接口 (order.batch_submit)实体类
    /// 接口地址 http://testpartner.zto.cn/
    /// </summary>
    public class ZtoOrderBatchEntity
    {
        /// <summary>
        /// 订单号，由合作商平台产生，具有平台唯一性。
        /// 130520142013234
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// 订单类型:空字符串=普通订单；bland=线下订单；cod=COD订单；limit=限时物流；ensure=快递保障订单。一般订单都为空，具体请联系技术人员
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        /// 交易号，由合作商平台产生。
        /// </summary>
        [JsonProperty("tradeid")]
        public string Tradeid { get; set; }

        /// <summary>
        /// 运单号，如下单时已有，请提供。
        /// </summary>
        [JsonProperty("mailno")]
        public string Mailno { get; set; }

        /// <summary>
        /// 卖家，最好是卖家ID
        /// </summary>
        [JsonProperty("seller")]
        public string Seller { get; set; }

        /// <summary>
        /// 买家，最好是买家ID
        /// </summary>
        [JsonProperty("buyer")]
        public string Buyer { get; set; }

        /// <summary>
        /// 发件人实体
        /// </summary>
        [JsonProperty("sender")]
        public Sender Sender { get; set; }

        /// <summary>
        /// 收件人实体
        /// </summary>
        [JsonProperty("receiver")]
        public Receiver Receiver { get; set; }

        /// <summary>
        /// 获取实体信息，这里可以不上传为null就可以
        /// </summary>
        [JsonProperty("items")]
        public object Items { get; set; }
    }

}
