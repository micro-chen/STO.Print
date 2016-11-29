//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2015 , STO TECH, Ltd. 
//-----------------------------------------------------------------

using Newtonsoft.Json;

namespace STO.Print.Model
{

    public partial class TaoBaoIpEntity
    {

        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("data")]
        public Data2 Data { get; set; }
    }

}
