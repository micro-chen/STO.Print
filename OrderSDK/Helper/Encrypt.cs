using System;
using System.Security.Cryptography;
using System.Text;

namespace OrderSDK.Helper
{
    public class Encrypt
    {
        #region string GetMd5Hash(string input) 得到MD5散列
        /// <summary>
        /// 得到MD5散列
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <returns>MD5散列</returns>
        public static string GetMd5Hash(string input)
        {
            MD5 md5Hash = MD5.Create();
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            var sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }
        #endregion

        /// <summary>
        /// 生成base64
        /// </summary>
        /// <param name="encodeType">编码类型</param>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string EncodeBase64(string encodeType, string source)
        {
            string encode = "";
            byte[] bytes = Encoding.GetEncoding(encodeType).GetBytes(source);
            try
            {
                encode = Convert.ToBase64String(bytes);
            }
            catch
            {
                encode = source;
            }
            return encode;
        }

        /// <summary>
        /// 生成base64
        /// </summary>
        /// <param name="encodeType">编码类型</param>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string EncodeBase64(Encoding encodeType, string source)
        {
            string encode = "";
            byte[] bytes = encodeType.GetBytes(source);
            try
            {
                encode = Convert.ToBase64String(bytes);
            }
            catch
            {
                encode = source;
            }
            return encode;
        }

        /// <summary>
        /// 获取MD5字节数组UTF-8
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static byte[] GetMd5HashBytes(string input)
        {
            MD5 md5Hash = MD5.Create();
            return md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
        }

        /// <summary>
        /// 自定义编码类型
        /// </summary>
        /// <param name="input"></param>
        /// <param name="encodeType"></param>
        /// <returns></returns>
        public static byte[] GetMd5HashBytes(string input, string encodeType)
        {
            MD5 md5Hash = MD5.Create();
            return md5Hash.ComputeHash(Encoding.GetEncoding(encodeType).GetBytes(input));
        }

        /// <summary>
        /// 自定义编码类型
        /// </summary>
        /// <param name="input"></param>
        /// <param name="encodeType"></param>
        /// <returns></returns>
        public static byte[] GetMd5HashBytes(string input, Encoding encodeType)
        {
            MD5 md5Hash = MD5.Create();
            return md5Hash.ComputeHash(encodeType.GetBytes(input));
        }
    }
}
