//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2015 , STO TECH, Ltd. 
//-----------------------------------------------------------------

using Newtonsoft.Json;

namespace STO.Print.Model
{
    public class ZtoPrintMarkResult
    {

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("result")]
        public JavaResult Result { get; set; }

        [JsonProperty("status")]
        public bool Status { get; set; }

        [JsonProperty("statusCode")]
        public string StatusCode { get; set; }
    }
}
