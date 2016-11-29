
namespace OrderSDK.Models
{
    public class ZtoApiResult
    {
        public bool status { get; set; }

        public string statusCode { get; set; }

        public string message { get; set; }
    }

    public class ZtoApiResult<T>:ZtoApiResult
    {
        public T result { get; set; }
    }
}
