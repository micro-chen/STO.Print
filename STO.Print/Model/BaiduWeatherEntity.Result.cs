//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2015 , STO TECH, Ltd. 
//-----------------------------------------------------------------

using Newtonsoft.Json;

namespace STO.Print.Model
{

    public partial class BaiduWeatherEntity
    {
        public class Result
        {

            [JsonProperty("currentCity")]
            public string CurrentCity { get; set; }

            [JsonProperty("pm25")]
            public string Pm25 { get; set; }

            [JsonProperty("index")]
            public Index2[] Index { get; set; }

            [JsonProperty("weather_data")]
            public WeatherData[] WeatherData { get; set; }
        }
    }

}
