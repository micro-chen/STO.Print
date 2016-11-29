//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2015 , STO TECH, Ltd. 
//-----------------------------------------------------------------

using Newtonsoft.Json;

namespace STO.Print.Model
{

    public partial class BaiduWeatherEntity
    {
        public class WeatherData
        {

            [JsonProperty("date")]
            public string Date { get; set; }

            [JsonProperty("dayPictureUrl")]
            public string DayPictureUrl { get; set; }

            [JsonProperty("nightPictureUrl")]
            public string NightPictureUrl { get; set; }

            [JsonProperty("weather")]
            public string Weather { get; set; }

            [JsonProperty("wind")]
            public string Wind { get; set; }

            [JsonProperty("temperature")]
            public string Temperature { get; set; }
        }
    }

}
