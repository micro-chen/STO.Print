//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2015 , STO TECH, Ltd. 
//-----------------------------------------------------------------

using Newtonsoft.Json;

namespace STO.Print.Model
{

    public partial class KuaiDi100
    {

        [JsonProperty("comCode")]
        public string ComCode { get; set; }

        [JsonProperty("num")]
        public string Num { get; set; }

        [JsonProperty("auto")]
        public Auto2[] Auto { get; set; }
    }

}
