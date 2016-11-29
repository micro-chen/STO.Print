//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2015 , STO TECH, Ltd. 
//-----------------------------------------------------------------
 
using Newtonsoft.Json;

namespace STO.Print.Model
{
    public partial class BaiduAddressEntity
    {
        public class PoiRegion
        {

            [JsonProperty("direction_desc")]
            public string DirectionDesc { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }
        }
    }

}
