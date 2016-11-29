using OrderSDK.ConfigSetting;
using OrderSDK.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OrderSDK
{
    /// <summary>
    ///     异步发送请求帮助类
    /// </summary>
    public class SendRequestAsync : ISendSdkAsync
    {
        private readonly HttpClient _httpClient;

        /// <summary>
        ///     使用默认构造函数，自动创建HttpClient
        /// </summary>
        public SendRequestAsync()
        {
            _httpClient = new HttpClient();
        }

        /// <summary>
        ///     自定义HttpClient
        /// </summary>
        /// <param name="client"></param>
        public SendRequestAsync(HttpClient client)
        {
            _httpClient = client;
        }

        public Task<string> SendAsync(string data, string url, Encoding encoding, bool isBase64, string sign, string messageType, string companyId)
        {
            var response = GetContent(data, url, encoding, isBase64, sign, messageType, companyId, null).ContinueWith(x => x.Result.Content.ReadAsStringAsync());
            return response.Result;
        }

        public Task<string> SendAsync(string data, string url, Encoding encoding, bool isBase64, string sign, string messageType, string companyId, IEnumerable<KeyValuePair<string, string>> extKv)
        {
            var response = GetContent(data, url, encoding, isBase64, sign, messageType, companyId, extKv);
            return response.ContinueWith(x => x.Result.Content.ReadAsStringAsync()).Result;
        }

        public Task<T> SendAsync<T>(string data, string url, Encoding encoding, bool isBase64, string sign, string messageType, string companyId, Func<string, T> converter)
        {
            var response = GetContent(data, url, encoding, isBase64, sign, messageType, companyId, null);
            return response.ContinueWith(x => x.Result.Content.ReadAsStringAsync().ContinueWith(j => converter(j.Result))).Result;
        }

        public Task<T> SendAsync<T>(string data, string url, Encoding encoding, bool isBase64, string sign, string messageType, string companyId, IEnumerable<KeyValuePair<string, string>> extKv, Func<string, T> converter)
        {
            var response = GetContent(data, url, encoding, isBase64, sign, messageType, companyId, extKv);
            return response.ContinueWith(x => x.Result.Content.ReadAsStringAsync().ContinueWith(j => converter(j.Result))).Result;
        }

        public Task<TOut> SendAsync<TOut, TIn>(TIn data, string url, Encoding encoding, bool isBase64, string sign, string messageType, string companyId, Func<string, TOut> converter, Func<TIn, string> serSerialize)
        {
            var response = GetContent(serSerialize(data), url, encoding, isBase64, sign, messageType, companyId, null);
            return response.ContinueWith(x => x.Result.Content.ReadAsStringAsync().ContinueWith(j => converter(j.Result))).Result;
        }

        public Task<TOut> SendAsync<TOut, TIn>(TIn data, string url, Encoding encoding, bool isBase64, string sign, string messageType, string companyId, IEnumerable<KeyValuePair<string, string>> extKv, Func<string, TOut> converter, Func<TIn, string> serSerialize)
        {
            var response = GetContent(serSerialize(data), url, encoding, isBase64, sign, messageType, companyId, extKv);
            return response.ContinueWith(x => x.Result.Content.ReadAsStringAsync().ContinueWith(j => converter(j.Result))).Result;
        }

        public Task<Stream> GetStreamAsync(string data, string url, Encoding encoding, bool isBase64, string sign,
            string messageType, string companyId, IEnumerable<KeyValuePair<string, string>> extKv = null)
        {
            var response = GetContent(data, url, encoding, isBase64, sign, messageType, companyId, extKv);
            return response.ContinueWith(x => x.Result.Content.ReadAsStreamAsync()).Result;
        }

        public Task<Stream> GetStreamAsync<T>(T data, string url, Encoding encoding, bool isBase64, string sign,
            string messageType, string companyId, Func<T, string> serSerialize,
            IEnumerable<KeyValuePair<string, string>> extKv = null)
        {
            var response = GetContent(serSerialize(data), url, encoding, isBase64, sign, messageType, companyId, extKv);
            return response.ContinueWith(x => x.Result.Content.ReadAsStreamAsync()).Result;
        }

        public IEnumerable<KeyValuePair<string, string>> GetRequestParam<T>(T data, Encoding encoding, bool isBase64,
            string sign, string messageType, string companyId, Func<T, string> serSerialize,
            IEnumerable<KeyValuePair<string, string>> extKv = null)
        {
            string json = serSerialize(data);
            return GetRequestParam(json, encoding, isBase64, sign, messageType, companyId, extKv);
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }


        public Task<string> SendAsync(ApiConfigElement config, string data, Encoding encoding, string messageType,
            IEnumerable<KeyValuePair<string, string>> extKv = null)
        {
            Task<HttpResponseMessage> response = GetContent(data, config.Url, encoding, config.IsBase64, config.Password, messageType, config.CompanyId, extKv);
            Task<Task<string>> read = response.ContinueWith(x => x.Result.Content.ReadAsStringAsync());
            return read.Result;
        }

        public IEnumerable<KeyValuePair<string, string>> GetRequestParam(string data, Encoding encoding, bool isBase64,
            string sign, string messageType, string companyId, IEnumerable<KeyValuePair<string, string>> extKv = null)
        {
            byte[] checkBytes = Encrypt.GetMd5HashBytes(data + sign, encoding);

            string checkStr = isBase64 ? Convert.ToBase64String(checkBytes) : encoding.GetString(checkBytes);

            IEnumerable<KeyValuePair<string, string>> kvp = new[]
            {
                new KeyValuePair<string, string>("data", data),
                new KeyValuePair<string, string>("data_digest", checkStr),
                new KeyValuePair<string, string>("msg_type", messageType),
                new KeyValuePair<string, string>("company_id", companyId)
            };

            if (extKv != null)
            {
                kvp = kvp.Concat(extKv);
            }
            return kvp;
        }

        private Task<HttpResponseMessage> GetContent(string data, string url, Encoding encoding, bool isBase64,
            string sign, string messageType, string companyId, IEnumerable<KeyValuePair<string, string>> extKv)
        {
            var kvp = GetRequestParam(data, encoding, isBase64, sign, messageType, companyId, extKv);

            return _httpClient.PostAsync(url, new FormUrlEncodedContent(kvp));
        }
    }
}