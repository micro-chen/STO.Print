//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2015 , STO TECH, Ltd. 
//-----------------------------------------------------------------

using Newtonsoft.Json;

namespace STO.Print.Model
{
    public partial class BaiduAddressEntity
    {

        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("result")]
        public Result2 Result { get; set; }
    }

}
