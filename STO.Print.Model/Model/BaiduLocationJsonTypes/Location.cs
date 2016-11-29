//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2015 , STO TECH, Ltd. 
//-----------------------------------------------------------------

using Newtonsoft.Json;

namespace STO.Print.Model.BaiduLocationJsonTypes
{

    public class Location
    {

        [JsonProperty("lng")]
        public double Lng { get; set; }

        [JsonProperty("lat")]
        public double Lat { get; set; }
    }

}
