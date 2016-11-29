//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2015 , STO TECH, Ltd. 
//-----------------------------------------------------------------

using Newtonsoft.Json;

namespace STO.Print.Model
{
    public class JavaResult
    {
        [JsonProperty("mark")]
        public string Mark { get; set; }

        [JsonProperty("print_mark")]
        public string PrintMark { get; set; }
    }
}
