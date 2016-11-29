//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2015 , STO TECH, Ltd. 
//-----------------------------------------------------------------

using Newtonsoft.Json;

namespace STO.Print.Model
{

    public partial class BaiduWeatherEntity
    {
        public class Index2
        {

            [JsonProperty("title")]
            public string Title { get; set; }

            [JsonProperty("zs")]
            public string Zs { get; set; }

            [JsonProperty("tipt")]
            public string Tipt { get; set; }

            [JsonProperty("des")]
            public string Des { get; set; }
        }
    }

}
