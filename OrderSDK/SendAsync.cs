using OrderSDK.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace OrderSDK
{
    public class SendAsync : ISendSdk
    {
        private readonly HttpClient _client;

        public SendAsync()
        {
            _client = new HttpClient();
        }

        public string Send(string data, string url, Encoding encoding, bool isBase64, string sign, string messageType, string companyId)
        {
            return Send(data, url, encoding, isBase64, sign, messageType, companyId, null);
        }

        private HttpContent GetContent(string data, string url, Encoding encoding, bool isBase64, string sign, string messageType, string companyId, IEnumerable<KeyValuePair<string, string>> extKv)
        {
            //add wangchun 添加try捕捉异常 2015-6-15
            try
            {
                var checkBytes = Encrypt.GetMd5HashBytes(data + sign, encoding);

                var checkStr = isBase64 ? Convert.ToBase64String(checkBytes) : encoding.GetString(checkBytes);

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

                return _client.PostAsync(url, new FormUrlEncodedContent(kvp)).Result.Content;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Dispose()
        {
            _client.Dispose();
        }


        public T Send<T>(string data, string url, Encoding encoding, bool isBase64, string sign, string messageType, string companyId, Func<string, T> converter)
        {
            var resultJson = Send(data, url, encoding, isBase64, sign, messageType, companyId);
            return converter(resultJson);
        }

        public T Send<T>(string data, string url, Encoding encoding, bool isBase64, string sign, string messageType, string companyId, IEnumerable<KeyValuePair<string, string>> extKv, Func<string, T> converter)
        {
            var resultJson = Send(data, url, encoding, isBase64, sign, messageType, companyId, extKv);
            return converter(resultJson);
        }


        public TOut Send<TOut, TIn>(TIn data, string url, Encoding encoding, bool isBase64, string sign, string messageType, string companyId, Func<string, TOut> converter, Func<TIn, string> serSerialize)
        {
            var entityJson = serSerialize(data);
            var resultJson = Send(entityJson, url, encoding, isBase64, sign, messageType, companyId);
            return converter(resultJson);
        }

        public TOut Send<TOut, TIn>(TIn data, string url, Encoding encoding, bool isBase64, string sign, string messageType, string companyId, IEnumerable<KeyValuePair<string, string>> extKv, Func<string, TOut> converter, Func<TIn, string> serSerialize)
        {
            var entityJson = serSerialize(data);
            var resultJson = Send(entityJson, url, encoding, isBase64, sign, messageType, companyId, extKv);
            return converter(resultJson);
        }

        public string Send(string data, string url, Encoding encoding, bool isBase64, string sign, string messageType, string companyId, IEnumerable<KeyValuePair<string, string>> extKv)
        {
            //add wangchun 添加try捕捉异常 2015-6-15
            try
            {
                 return GetContent(data, url, encoding, isBase64, sign, messageType, companyId, extKv).ReadAsStringAsync().Result;
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }

        public Stream GetStream(string data, string url, Encoding encoding, bool isBase64, string sign,
            string messageType, string companyId, IEnumerable<KeyValuePair<string, string>> extKv = null)
        {
            return
                GetContent(data, url, encoding, isBase64, sign, messageType, companyId, extKv)
                    .ReadAsStreamAsync()
                    .Result;
        }

        public Stream GetStream<T>(T data, string url, Encoding encoding, bool isBase64, string sign, string messageType, string companyId, Func<T, string> serSerialize, IEnumerable<KeyValuePair<string, string>> extKv = null)
        {
            var entityJson = serSerialize(data);
            return
                GetContent(entityJson, url, encoding, isBase64, sign, messageType, companyId, extKv)
                    .ReadAsStreamAsync()
                    .Result;
        }


        public IEnumerable<KeyValuePair<string, string>> GetRequestParam(string data, Encoding encoding, bool isBase64, string sign, string messageType, string companyId, IEnumerable<KeyValuePair<string, string>> extKv = null)
        {
            var checkBytes = Encrypt.GetMd5HashBytes(data + sign, encoding);

            var checkStr = isBase64 ? Convert.ToBase64String(checkBytes) : encoding.GetString(checkBytes);

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

        public IEnumerable<KeyValuePair<string, string>> GetRequestParam<T>(T data, Encoding encoding, bool isBase64, string sign, string messageType, string companyId, Func<T, string> serSerialize, IEnumerable<KeyValuePair<string, string>> extKv = null)
        {
            var json = serSerialize(data);
            return GetRequestParam(json, encoding, isBase64, sign, messageType, companyId, extKv);
        }
    }
}
