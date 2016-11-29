using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace STO.Print.Utilities
{
    /// <summary>
    /// http://www.cnblogs.com/blqw/p/4852020.html
    /// 一种简单的md5加盐加密的方法(防止彩虹表撞库)
    /// </summary>
   public class MD5Helper
    {
        public static Guid ToRandomMD5(string input)
        {
            using (var md5Provider = new MD5CryptoServiceProvider())
            {
                //获取一个256以内的随机数,用于充当 "盐"
                var salt = (byte)Math.Abs(new object().GetHashCode() % 256);
                input += salt;
                var bytes = Encoding.UTF8.GetBytes(input);
                var hash = md5Provider.ComputeHash(bytes);
                hash[0] = salt;
                return new Guid(hash);
            }
        }

        public static bool EqualsRandomMD5(string input, Guid rmd5)
        {
            var arr = rmd5.ToByteArray();
            //将盐取出来
            var salt = arr[0];
            using (var md5Provider = new MD5CryptoServiceProvider())
            {
                input += salt;
                var bytes = Encoding.UTF8.GetBytes(input);
                var hash = md5Provider.ComputeHash(bytes);
                for (int i = 1; i < 16; i++)
                {
                    if (hash[i] != arr[i])
                    {
                        return false;
                    }
                }
                return true;
            }
        }
    }
}
