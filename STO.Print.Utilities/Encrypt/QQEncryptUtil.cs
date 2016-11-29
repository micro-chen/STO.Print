using System;

namespace STO.Print.Utilities
{
    //QQ的EncryptUtil
    public class QQEncryptUtil
    {
        public static string EncodePasswordWithVerifyCode(string password, string verifyCode)
        {
            return MD5(MD5_3(password) + verifyCode.ToUpper());
        }

        static string MD5_3(string arg)
        {
            System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();

            byte[] buffer = System.Text.Encoding.ASCII.GetBytes(arg);
            buffer = md5.ComputeHash(buffer);
            buffer = md5.ComputeHash(buffer);
            buffer = md5.ComputeHash(buffer);

            return BitConverter.ToString(buffer).Replace("-", "").ToUpper();
        }
        static string MD5(string arg)
        {
            System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();

            byte[] buffer = System.Text.Encoding.ASCII.GetBytes(arg);
            buffer = md5.ComputeHash(buffer);

            return BitConverter.ToString(buffer).Replace("-", "").ToUpper();
        }
    }
}
