//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2015 , STO TECH, Ltd. 
//-----------------------------------------------------------------

using Newtonsoft.Json;
using STO.Print.Model.ZtoOrderBatchResponseEntityJsonTypes;

namespace STO.Print.Model
{

    public class ZtoOrderBatchResponseEntity
    {
        /// <summary>
        /// 返回状态 false 或者 true
        /// </summary>
        [JsonProperty("result")]
        public string Result { get; set; }

        /// <summary>
        /// 返回具体的实体数组
        /// </summary>
        [JsonProperty("keys")]
        public Keys[] Keys { get; set; }
    }

}
