//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2015 , STO TECH, Ltd. 
//-----------------------------------------------------------------

using Newtonsoft.Json;

namespace STO.Print.Model
{

    public partial class BaiduWeatherEntity
    {

        [JsonProperty("error")]
        public int Error { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("results")]
        public Result[] Results { get; set; }
    }

}
