//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2015 , STO TECH, Ltd. 
//-----------------------------------------------------------------

using Newtonsoft.Json;

namespace STO.Print.Model
{
    public partial class BaiduAddressEntity
    {
        /// <summary>
        /// 经纬度
        /// </summary>
        public class Location2
        {

            [JsonProperty("lng")]
            public double Lng { get; set; }

            [JsonProperty("lat")]
            public double Lat { get; set; }
        }
    }

}
