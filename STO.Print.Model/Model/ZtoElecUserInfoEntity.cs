//-------------------------------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2015 , STO , Ltd .
//-------------------------------------------------------------------------------------

using Newtonsoft.Json;

namespace STO.Print.Model
{
    /// <summary>
    /// 申通线下商家ID实体
    ///
    /// 修改纪录
    ///
    ///	   2015-10-30  版本：1.0 YangHengLian 创建主键，注意命名空间的排序。
    /// 
    /// 版本：1.0
    ///
    /// <author>
    ///		<name>YangHengLian</name>
    ///		<date>2014-08-30</date>
    /// </author>
    /// </summary>
    public class ZtoElecUserInfoEntity
    {
        [JsonProperty("result")]
        public string Result { get; set; }

        /// <summary>
        /// 申通线下商家ID
        /// </summary>
        [JsonProperty("kehuid")]
        public string Kehuid { get; set; }

        /// <summary>
        /// 申通线下商家ID的密码
        /// </summary>
        [JsonProperty("pwd")]
        public string Pwd { get; set; }

        /// <summary>
        /// 申通线下商家ID的电话
        /// </summary>
        [JsonProperty("phone")]
        public string Phone { get; set; }

        /// <summary>
        /// 申通线下商家ID的省份
        /// </summary>
        [JsonProperty("province")]
        public string Province { get; set; }

        /// <summary>
        /// 申通线下商家ID的城市
        /// </summary>
        [JsonProperty("city")]
        public string City { get; set; }

        /// <summary>
        /// 申通线下商家ID的区县
        /// </summary>
        [JsonProperty("area")]
        public string Area { get; set; }

        [JsonProperty("interfaceType")]
        public string InterfaceType { get; set; }
    }

    /// <summary>
    /// 扩展类，2016年7月20日09:36:44，杨恒连
    /// </summary>
    public class ZtoElecUserInfoExtendEntity : ZtoElecUserInfoEntity
    {
        /// <summary>
        /// 申通线下商家ID所属网点名称
        /// </summary>
        [JsonProperty("kehuid")]
        public string siteName { get; set; }

        /// <summary>
        /// 申通线下商家ID所属网点编号
        /// </summary>
        [JsonProperty("pwd")]
        public string siteCode { get; set; }
    }

}
