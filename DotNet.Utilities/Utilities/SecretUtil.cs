//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd.  
//-----------------------------------------------------------------

using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Security;
using System.Xml;

namespace DotNet.Utilities
{
    public partial class SecretUtil
    {
        #region public static string SqlSafe(string inputValue) 检查参数的安全性
        /// <summary>
        /// 检查参数的安全性
        /// </summary>
        /// <param name="inputValue">参数</param>
        /// <returns>安全的参数</returns>
        public static string SqlSafe(string inputValue)
        {
            inputValue = inputValue.Replace("'", "''");
            // value = value.Replace("%", "'%");
            return inputValue;
        }
        #endregion

        #region public static bool IsSqlSafe(string commandText) 检查参数的安全性
        /// <summary>
        /// 检查参数的安全性
        /// </summary>
        /// <param name="commandText">参数</param>
        /// <returns>安全的参数</returns>
        public static bool IsSqlSafe(string commandText)
        {
            bool result = true;
            if (!string.IsNullOrEmpty(commandText))
            {
                string[] unSafeText = new string[] { "Delete", "Insert", "Update", "Truncate"};
                for (int i = 0; i < unSafeText.Length; i++)
                {
                    string unSafeString = unSafeText[i];
                    if (commandText.IndexOf(unSafeString, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        result = false;
                        break;
                    }
                }
            }
            return result;
        }
        #endregion

        /// <summary>
        /// 对数据进行签名
        /// 
        /// 将来需要改进为，对散列值进行签名
        /// </summary>
        /// <param name="dataToSign">需要签名的数据</param>
        /// <param name="privateKey">私钥</param>
        /// <returns>签名结果</returns>
        public static string SignData(string dataToSign, string privateKey)
        {
            string result = string.Empty;

            ASCIIEncoding byteConverter = new ASCIIEncoding();
            byte[] buffer = byteConverter.GetBytes(dataToSign);
            try
            {
                RSACryptoServiceProvider cryptoServiceProvider = new RSACryptoServiceProvider();
                cryptoServiceProvider.ImportCspBlob(Convert.FromBase64String(privateKey));
                byte[] signedData = cryptoServiceProvider.SignData(buffer, new SHA1CryptoServiceProvider());
                result = Convert.ToBase64String(signedData);
            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.Message);
            }

            return result;
        }

        /// <summary>
        /// 验证数字签名
        /// 
        /// 将来需要改进为，按散列值进行验证
        /// </summary>
        /// <param name="dataToVerify">需要验证的数据</param>
        /// <param name="sign">签名</param>
        /// <param name="publicKey">公钥</param>
        /// <returns>正确</returns>
        public static bool VerifyData(string dataToVerify, string sign, string publicKey)
        {
            bool result = false;

            byte[] signedData = Convert.FromBase64String(sign);
            ASCIIEncoding byteConverter = new ASCIIEncoding();
            byte[] buffer = byteConverter.GetBytes(dataToVerify);
            try
            {
                RSACryptoServiceProvider cryptoServiceProvider = new RSACryptoServiceProvider();
                cryptoServiceProvider.ImportCspBlob(Convert.FromBase64String(publicKey));
                result = cryptoServiceProvider.VerifyData(buffer, new SHA1CryptoServiceProvider(), signedData);
            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.Message);
                return false;
            }

            return result;
        }

        #region public static bool CheckRegister() 检查注册码是否正确
        /// <summary>
        /// 检查注册码是否正确
        /// </summary>
        /// <returns>是否进行了注册</returns>
        public static bool CheckRegister()
        {
            bool result = true;
            // if (BaseConfiguration.Instance.CustomerCompanyName.Length == 0)
            // {
            //     result = false;
            // }
            // 只能先用一年再说,否则会惹很多麻烦
            if (BaseSystemInfo.NeedRegister)
            {
                if ((DateTime.Now.Year >= 2016) && (DateTime.Now.Month > 7))
                {
                    result = false;
                }
            }
            // 一定要检查注册码,否则这个软件到处别人复制,我的基类也得不到保障了,这是我的心血,得会珍惜自己的劳动成果.
            // 2007.04.14 JiRiGaLa 改进注册方式,让底层程序更安全一些
            //if (BaseConfiguration.Instance.RegisterKey.Equals(CodeChange(BaseConfiguration.Instance.DataBase + BaseConfiguration.Instance.CustomerCompanyName)))
            //{
            //    result = true;
            //}
            return result;
        }
        #endregion


        //
        // 一 用户密码加密函数
        //

        /// <summary>
        /// 用户密码加密函数
        /// </summary>
        /// <param name="password">密码</param>
        /// <returns>加密值</returns>
        public static string md5(string password)
        {
            return md5(password, 32);
        }

        /// <summary>
        /// 加密用户密码
        /// </summary>
        /// <param name="password">密码</param>
        /// <param name="codeLength">多少位</param>
        /// <returns>加密密码</returns>
        public static string md5(string password, int codeLength)
        {
            if (!string.IsNullOrEmpty(password))
            {
                // 16位MD5加密（取32位加密的9~25字符）
                if (codeLength == 16)
                {
                    return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(password, "MD5").ToLower().Substring(8, 16);
                }

                // 32位加密
                if (codeLength == 32)
                {
                    return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(password, "MD5").ToLower();
                }
            }
            return string.Empty;
        }


        #region public static string MD5Encrypt16(string password) 16位MD5加密
        /// <summary>
        /// 16位MD5加密
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string MD5Encrypt16(string password)
        {
            var md5 = new MD5CryptoServiceProvider();
            string t2 = BitConverter.ToString(md5.ComputeHash(Encoding.Default.GetBytes(password)), 4, 8);
            t2 = t2.Replace("-", "");
            return t2;
        }

        #endregion

        #region public static string MD5Encrypt32(string password) 32位MD5加密
        /// <summary>
        /// 32位MD5加密
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string MD5Encrypt32(string password)
        {
            string cl = password;
            string pwd = "";
            MD5 md5 = MD5.Create(); //实例化一个md5对像
            // 加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择　
            byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(cl));
            // 通过使用循环，将字节类型的数组转换为字符串，此字符串是常规字符格式化所得
            for (int i = 0; i < s.Length; i++)
            {
                // 将得到的字符串使用十六进制类型格式。格式后的字符是小写的字母，如果使用大写（X）则格式后的字符是大写字符 
                pwd = pwd + s[i].ToString("X");
            }
            return pwd;
        }
        #endregion

        // 编码
        public static string EncodeBase64(string codeType, string code)
        {
            string encode = "";
            byte[] bytes = Encoding.GetEncoding(codeType).GetBytes(code);
            try
            {
                encode = Convert.ToBase64String(bytes);
            }
            catch
            {
                encode = code;
            }
            return encode;
        }

        // 解码
        public static string DecodeBase64(string codeType, string code)
        {
            string decode = "";
            byte[] bytes = Convert.FromBase64String(code);
            try
            {
                decode = Encoding.GetEncoding(codeType).GetString(bytes);
            }
            catch
            {
                decode = code;
            }
            return decode;
        }

        /// <summary>
        /// DES数据加密
        /// </summary>
        /// <param name="targetValue">目标字段</param>
        /// <returns>加密</returns>
        public static string Encrypt(string targetValue)
        {
            return Encrypt(targetValue, BaseSystemInfo.SecurityKey);
        }

        /// <summary>
        /// DES数据加密
        /// </summary>
        /// <param name="targetValue">目标值</param>
        /// <param name="key">密钥</param>
        /// <returns>加密值</returns>
        public static string Encrypt(string targetValue, string key)
        {
            if (string.IsNullOrEmpty(targetValue))
            {
                return string.Empty;
            }

            var result = new StringBuilder();
            var des = new DESCryptoServiceProvider();
            byte[] inputByteArray = Encoding.Default.GetBytes(targetValue);
            // 通过两次哈希密码设置对称算法的初始化向量   
            des.Key = Encoding.ASCII.GetBytes(FormsAuthentication.HashPasswordForStoringInConfigFile
                                                  (FormsAuthentication.HashPasswordForStoringInConfigFile(key, "md5").
                                                       Substring(0, 8), "sha1").Substring(0, 8));
            // 通过两次哈希密码设置算法的机密密钥   
            des.IV = Encoding.ASCII.GetBytes(FormsAuthentication.HashPasswordForStoringInConfigFile
                                                 (FormsAuthentication.HashPasswordForStoringInConfigFile(key, "md5")
                                                      .Substring(0, 8), "md5").Substring(0, 8));
            var ms = new MemoryStream();
            var cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            foreach (byte b in ms.ToArray())
            {
                result.AppendFormat("{0:X2}", b);
            }
            return result.ToString();
        }


        /// <summary>
        /// DES数据解密
        /// </summary>
        /// <param name="targetValue">目标字段</param>
        /// <returns>解密</returns>
        public static string Decrypt(string targetValue)
        {
            return Decrypt(targetValue, BaseSystemInfo.SecurityKey);
        }

        /// <summary>
        /// DES数据解密
        /// 20140219 吉日嘎拉 就是出错了，也不能让程序崩溃
        /// </summary>
        /// <param name="targetValue"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string Decrypt(string targetValue, string key)
        {
            if (string.IsNullOrEmpty(targetValue))
            {
                return string.Empty;
            }
            // 定义DES加密对象
            try
            {
                var des = new DESCryptoServiceProvider();
                int len = targetValue.Length / 2;
                var inputByteArray = new byte[len];
                int x, i;
                for (x = 0; x < len; x++)
                {
                    i = Convert.ToInt32(targetValue.Substring(x * 2, 2), 16);
                    inputByteArray[x] = (byte)i;
                }
                // 通过两次哈希密码设置对称算法的初始化向量   
                des.Key = Encoding.ASCII.GetBytes(FormsAuthentication.HashPasswordForStoringInConfigFile
                                                      (FormsAuthentication.HashPasswordForStoringInConfigFile(key, "md5").
                                                           Substring(0, 8), "sha1").Substring(0, 8));
                // 通过两次哈希密码设置算法的机密密钥   
                des.IV = Encoding.ASCII.GetBytes(FormsAuthentication.HashPasswordForStoringInConfigFile
                                                     (FormsAuthentication.HashPasswordForStoringInConfigFile(key, "md5")
                                                          .Substring(0, 8), "md5").Substring(0, 8));
                // 定义内存流
                var ms = new MemoryStream();
                // 定义加密流
                var cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                return Encoding.Default.GetString(ms.ToArray());
            }
            catch
            {
            }
            return string.Empty;
        }

        /// <summary>
        /// 查询匹配长度
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="cp">规则</param>
        /// <param name="s">默认值</param>
        /// <returns>匹配长度</returns>
        public static int MatcherLength(string str, string cp, string s)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return 0;
            }
            MatchCollection mc = Regex.Matches(str, cp);
            return mc.Count;
        }


        /// <summary>
        /// 密码强度级别
        /// </summary>
        /// <param name="pass">密码</param>
        /// <returns>强度级别</returns>
        public static int GetUserPassWordRate(string passWord)
        {
            /*  
             * 返回值值表示口令等级  
             * 0 不合法口令  
             * 1 太短  
             * 2 弱  
             * 3 一般  
             * 4 很好  
             * 5 极佳  
             */
            int i = 0;
            //if(pass==null || pass.length()==0)
            if (string.IsNullOrWhiteSpace(passWord))
            {
                return 0;
            }
            int hasLetter = MatcherLength(passWord, "[a-zA-Z]", "");
            int hasNumber = MatcherLength(passWord, "[0-9]", "");
            int passLen = passWord.Length;
            if (passLen >= 6)
            {
                /* 如果仅包含数字或仅包含字母 */
                if ((passLen - hasLetter) == 0 || (passLen - hasNumber) == 0)
                {
                    if (passLen < 8)
                    {
                        i = 2;
                    }
                    else
                    {
                        i = 3;
                    }
                }
                /* 如果口令大于6位且即包含数字又包含字母 */
                else if (hasLetter > 0 && hasNumber > 0)
                {
                    if (passLen >= 10)
                    {
                        i = 5;
                    }
                    else if (passLen >= 8)
                    {
                        i = 4;
                    }
                    else
                    {
                        i = 3;
                    }
                }
                /* 如果既不包含数字又不包含字母 */
                else if (hasLetter == 0 && hasNumber == 0)
                {
                    if (passLen >= 7)
                    {
                        i = 5;
                    }
                    else
                    {
                        i = 4;
                    }
                }
                /* 字母或数字有一方为0 */
                else if (hasNumber == 0 || hasLetter == 0)
                {
                    if ((passLen - hasLetter) == 0 || (passLen - hasNumber) == 0)
                    {
                        i = 2;
                    }
                    /*   
                     * 字母数字任意一种类型小于6且总长度大于等于6  
                     * 则说明此密码是字母或数字加任意其他字符组合而成  
                     */
                    else
                    {
                        if (passLen > 8)
                        {
                            i = 5;
                        }
                        else if (passLen == 8)
                        {
                            i = 4;
                        }
                        else
                        {
                            i = 3;
                        }
                    }
                }
            }
            else
            { 
                //口令小于6位则显示太短  
                if (passLen > 0)
                {
                    i = 1; //口令太短  
                }
                else
                {
                    i = 0;
                }
            }
            return i;
        }
    }
}