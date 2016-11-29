//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2015 , STO TECH, Ltd. 
//-----------------------------------------------------------------

using Newtonsoft.Json;

namespace STO.Print.Model
{

    public partial class KuaiDi100
    {
        public class Auto2
        {

            [JsonProperty("comCode")]
            public string ComCode { get; set; }

            [JsonProperty("id")]
            public string Id { get; set; }

            [JsonProperty("noCount")]
            public int NoCount { get; set; }

            [JsonProperty("noPre")]
            public string NoPre { get; set; }

            [JsonProperty("startTime")]
            public string StartTime { get; set; }
        }
    }

}
