using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace OrderSDK
{
    /// <summary>
    /// 中通快递标准接口调用SDK
    /// </summary>
    public interface ISendSdk : IDisposable
    {
        /// <summary>
        /// 调用接口
        /// </summary>
        /// <param name="data">传输的业务数据字符串（常见JSON）</param>
        /// <param name="url">接口调用URL</param>
        /// <param name="encoding">编码方式</param>
        /// <param name="isBase64">是否Base64</param>
        /// <param name="sign">密钥</param>
        /// <param name="messageType">消息类型</param>
        /// <param name="companyId">公司标识</param>
        /// <returns>调用结果</returns>
        string Send(
            string data,
            string url,
            Encoding encoding,
            bool isBase64,
            string sign,
            string messageType,
            string companyId);

        /// <summary>
        /// 调用接口
        /// </summary>
        /// <param name="data">传输的业务数据字符串（常见JSON）</param>
        /// <param name="url">接口调用URL</param>
        /// <param name="encoding">编码方式</param>
        /// <param name="isBase64">是否Base64</param>
        /// <param name="sign">密钥</param>
        /// <param name="messageType">消息类型</param>
        /// <param name="companyId">公司标识</param>
        /// <param name="extKv">扩展字段</param>
        /// <returns>调用结果</returns>
        string Send(
            string data,
            string url,
            Encoding encoding,
            bool isBase64,
            string sign,
            string messageType,
            string companyId, IEnumerable<KeyValuePair<string, string>> extKv);

        /// <summary>
        /// 接口调用
        /// </summary>
        /// <typeparam name="T">业务数据对象</typeparam>
        /// <param name="data">传输的业务数据字符串（常见JSON）</param>
        /// <param name="url">接口调用URL</param>
        /// <param name="encoding">编码方式</param>
        /// <param name="isBase64">是否Base64</param>
        /// <param name="sign">密钥</param>
        /// <param name="messageType">消息类型</param>
        /// <param name="companyId">公司标识</param>
        /// <param name="converter">序列化方法</param>
        /// <returns></returns>
        T Send<T>(
            string data,
            string url,
            Encoding encoding,
            bool isBase64,
            string sign,
            string messageType,
            string companyId,
            Func<string, T> converter);

        /// <summary>
        /// 接口调用
        /// </summary>
        /// <typeparam name="T">业务数据对象</typeparam>
        /// <param name="data">传输的业务数据对象</param>
        /// <param name="url">接口调用URL</param>
        /// <param name="encoding">编码方式</param>
        /// <param name="isBase64">是否Base64</param>
        /// <param name="sign">密钥</param>
        /// <param name="messageType">消息类型</param>
        /// <param name="companyId">公司标识</param>
        /// <param name="extKv">扩展字段</param>
        /// <param name="converter">序列化方法</param>
        /// <returns></returns>
        T Send<T>(
            string data,
            string url,
            Encoding encoding,
            bool isBase64,
            string sign,
            string messageType,
            string companyId, IEnumerable<KeyValuePair<string, string>> extKv, Func<string, T> converter);

        /// <summary>
        /// 接口调用
        /// </summary>
        /// <typeparam name="TOut">输出结果对象</typeparam>
        /// <typeparam name="TIn">输入对象</typeparam>
        /// <param name="data">传输的业务数据字符串（常见JSON）</param>
        /// <param name="url">接口调用URL</param>
        /// <param name="encoding">编码方式</param>
        /// <param name="isBase64">是否Base64</param>
        /// <param name="sign">密钥</param>
        /// <param name="messageType">消息类型</param>
        /// <param name="companyId">公司标识</param>
        /// <param name="converter">反序列化方法</param>
        /// <param name="serSerialize">序列化方法</param>
        /// <returns></returns>
        TOut Send<TOut, TIn>(
            TIn data,
            string url,
            Encoding encoding,
            bool isBase64,
            string sign,
            string messageType,
            string companyId,
            Func<string, TOut> converter,
            Func<TIn, string> serSerialize);

        /// <summary>
        /// 接口调用
        /// </summary>
        /// <typeparam name="TOut">输出结果对象</typeparam>
        /// <typeparam name="TIn">输入对象</typeparam>
        /// <param name="data">传输的业务数据对象</param>
        /// <param name="url">接口调用URL</param>
        /// <param name="encoding">编码方式</param>
        /// <param name="isBase64">是否Base64</param>
        /// <param name="sign">密钥</param>
        /// <param name="messageType">消息类型</param>
        /// <param name="companyId">公司标识</param>
        /// <param name="extKv">扩展字段</param>
        /// <param name="converter">反序列化方法</param>
        /// <param name="serSerialize">序列化方法</param>
        /// <returns></returns>
        TOut Send<TOut, TIn>(
            TIn data,
            string url,
            Encoding encoding,
            bool isBase64,
            string sign,
            string messageType,
            string companyId, 
            IEnumerable<KeyValuePair<string, string>> extKv, 
            Func<string, TOut> converter, 
            Func<TIn, string> serSerialize);

        /// <summary>
        /// 接口调用
        /// </summary>
        /// <param name="data">传输的业务数据字符串（常见JSON）</param>
        /// <param name="url">接口调用URL</param>
        /// <param name="encoding">编码方式</param>
        /// <param name="isBase64">是否Base64</param>
        /// <param name="sign">密钥</param>
        /// <param name="messageType">消息类型</param>
        /// <param name="companyId">公司标识</param>
        /// <param name="extKv">扩展字段</param>
        /// <returns></returns>
        Stream GetStream(
            string data, 
            string url, 
            Encoding encoding, 
            bool isBase64, 
            string sign,
            string messageType,
            string companyId, 
            IEnumerable<KeyValuePair<string, string>> extKv = null);

        /// <summary>
        /// 接口调用
        /// </summary>
        /// <typeparam name="T">业务数据对象</typeparam>
        /// <param name="data">传输的业务数据</param>
        /// <param name="url">接口调用URL</param>
        /// <param name="encoding">编码方式</param>
        /// <param name="isBase64">是否Base64</param>
        /// <param name="sign">密钥</param>
        /// <param name="messageType">消息类型</param>
        /// <param name="companyId">公司标识</param>
        /// <param name="serSerialize"></param>
        /// <param name="extKv">扩展字段</param>
        /// <returns></returns>
        Stream GetStream<T>(
            T data,
            string url,
            Encoding encoding,
            bool isBase64,
            string sign,
            string messageType,
            string companyId, 
            Func<T, string> serSerialize,
            IEnumerable<KeyValuePair<string, string>> extKv = null);

        /// <summary>
        /// 获取请求参数
        /// </summary>
        /// <param name="data">业务数据</param>
        /// <param name="encoding">编码方式</param>
        /// <param name="isBase64">是否Base64</param>
        /// <param name="sign">密钥</param>
        /// <param name="messageType">消息类型</param>
        /// <param name="companyId">公司标识</param>
        /// <param name="extKv">扩展字段</param>
        /// <returns></returns>
        IEnumerable<KeyValuePair<string, string>> GetRequestParam(string data, Encoding encoding, bool isBase64, string sign,
            string messageType, string companyId, IEnumerable<KeyValuePair<string, string>> extKv = null);

        /// <summary>
        /// 获取请求参数
        /// </summary>
        /// <typeparam name="T">业务数据对象</typeparam>
        /// <param name="data">业务数据对象</param>
        /// <param name="encoding"></param>
        /// <param name="isBase64"></param>
        /// <param name="sign"></param>
        /// <param name="messageType"></param>
        /// <param name="companyId"></param>
        /// <param name="serSerialize"></param>
        /// <param name="extKv"></param>
        /// <returns></returns>
        IEnumerable<KeyValuePair<string, string>> GetRequestParam<T>(T data, Encoding encoding, bool isBase64, string sign, string messageType, string companyId, Func<T, string> serSerialize, IEnumerable<KeyValuePair<string, string>> extKv = null);
    }
}
