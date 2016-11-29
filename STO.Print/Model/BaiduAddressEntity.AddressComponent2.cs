//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2015 , STO TECH, Ltd. 
//-----------------------------------------------------------------

using Newtonsoft.Json;

namespace STO.Print.Model
{
    public partial class BaiduAddressEntity
    {
        public class AddressComponent2
        {

            /// <summary>
            /// 城市
            /// </summary>
            [JsonProperty("city")]
            public string City { get; set; }

            [JsonProperty("country")]
            public string Country { get; set; }

            [JsonProperty("direction")]
            public string Direction { get; set; }

            [JsonProperty("distance")]
            public string Distance { get; set; }

            /// <summary>
            /// 区县
            /// </summary>
            [JsonProperty("district")]
            public string District { get; set; }

            /// <summary>
            /// 省份
            /// </summary>
            [JsonProperty("province")]
            public string Province { get; set; }

            /// <summary>
            /// 街道
            /// </summary>
            [JsonProperty("street")]
            public string Street { get; set; }

            [JsonProperty("street_number")]
            public string StreetNumber { get; set; }

            [JsonProperty("country_code")]
            public int CountryCode { get; set; }
        }
    }

}
