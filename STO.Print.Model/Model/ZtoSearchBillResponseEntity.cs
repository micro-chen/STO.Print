using Newtonsoft.Json;

namespace STO.Print.Model
{

    public partial class ZtoSearchBillResponseEntity
    {

        [JsonProperty("traces")]
        public Trace[] Traces { get; set; }
    }

}
