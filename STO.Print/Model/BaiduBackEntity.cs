//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2015 , STO TECH, Ltd. 
//-----------------------------------------------------------------

using Newtonsoft.Json;

namespace STO.Print.Model
{

    public partial class BaiduBackEntity
    {

        [JsonProperty("results")]
        public object[] Results { get; set; }

        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("msg")]
        public string Msg { get; set; }
    }

}
