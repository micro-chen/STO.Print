//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2015 , STO TECH, Ltd. 
//-----------------------------------------------------------------

using Newtonsoft.Json;

namespace STO.Print.Model
{

    public partial class TaoBaoIpEntity
    {
        public class Data2
        {

            [JsonProperty("country")]
            public string Country { get; set; }

            [JsonProperty("country_id")]
            public string CountryId { get; set; }

            [JsonProperty("area")]
            public string Area { get; set; }

            [JsonProperty("area_id")]
            public string AreaId { get; set; }

            [JsonProperty("region")]
            public string Region { get; set; }

            [JsonProperty("region_id")]
            public string RegionId { get; set; }

            [JsonProperty("city")]
            public string City { get; set; }

            [JsonProperty("city_id")]
            public string CityId { get; set; }

            [JsonProperty("county")]
            public string County { get; set; }

            [JsonProperty("county_id")]
            public string CountyId { get; set; }

            [JsonProperty("isp")]
            public string Isp { get; set; }

            [JsonProperty("isp_id")]
            public string IspId { get; set; }

            [JsonProperty("ip")]
            public string Ip { get; set; }
        }
    }

}
