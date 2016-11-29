//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2015 , STO TECH, Ltd. 
//-----------------------------------------------------------------

using Newtonsoft.Json;
using STO.Print.Model.BaiduLocationJsonTypes;

namespace STO.Print.Model
{
    public class BaiduLocation
    {

        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("result")]
        public Result Result { get; set; }
    }

}
