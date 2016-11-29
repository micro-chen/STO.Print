//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2015 , STO TECH, Ltd. 
//-----------------------------------------------------------------

using Newtonsoft.Json;

namespace STO.Print.Model
{
    public partial class BaiduAddressEntity
    {
        public class Result2
        {

            [JsonProperty("location")]
            public Location2 Location { get; set; }

            [JsonProperty("formatted_address")]
            public string FormattedAddress { get; set; }

            [JsonProperty("business")]
            public string Business { get; set; }

            [JsonProperty("addressComponent")]
            public AddressComponent2 AddressComponent { get; set; }

            [JsonProperty("poiRegions")]
            public PoiRegion[] PoiRegions { get; set; }

            [JsonProperty("sematic_description")]
            public string SematicDescription { get; set; }

            [JsonProperty("cityCode")]
            public int CityCode { get; set; }
        }
    }

}
