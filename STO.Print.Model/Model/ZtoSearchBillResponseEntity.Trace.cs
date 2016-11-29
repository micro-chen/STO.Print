using Newtonsoft.Json;

namespace STO.Print.Model
{

    public partial class ZtoSearchBillResponseEntity
    {
        public class Trace
        {

            [JsonProperty("acceptAddress")]
            public string AcceptAddress { get; set; }

            [JsonProperty("acceptTime")]
            public string AcceptTime { get; set; }

            [JsonProperty("remark")]
            public string Remark { get; set; }
        }
    }

}
