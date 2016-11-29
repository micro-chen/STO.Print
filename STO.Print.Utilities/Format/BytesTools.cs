using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace STO.Print.Utilities
{
    /// <summary>
    /// byte字节数组操作辅助类
    /// </summary>
    public static class BytesTools
    {
        public static readonly Encoding GB2312 = Encoding.GetEncoding("GB2312");
        public static readonly Encoding ASCII = Encoding.ASCII;

        /// <summary>
        /// 半角转全角函数
        /// </summary>
        /// <param name="srcbuff"></param>
        /// <returns></returns>
        public static byte[] ToSBC(byte[] srcbuff)
        {
            List<byte> tmpbuff = new List<byte>();
            for (int i = 0; i < srcbuff.Length; )
            {
                if (srcbuff[i] == 0x20)
                {
                    tmpbuff.Add(0xA1);
                    tmpbuff.Add(0xA1);
                    i += 1;
                }
                else if (srcbuff[i] == 0x7E)
                {
                    tmpbuff.Add(0xA1);
                    tmpbuff.Add(0xAB);
                    i += 1;
                }
                else if (srcbuff[i] > 0x80)
                {
                    tmpbuff.Add(srcbuff[i]);
                    tmpbuff.Add(srcbuff[i + 1]);
                    i += 2;
                }
                else
                {
                    tmpbuff.Add(0xA3);
                    tmpbuff.Add((byte)(srcbuff[i] + 0x80));
                    i += 1;
                }
            }
            return tmpbuff.ToArray();
        } 

        /// <summary>
        /// 从 byte[] 中截取子串
        /// </summary>
        /// <param name="srcbuff"></param>
        /// <param name="start"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        internal static byte[] SubBuffer(byte[] srcbuff, int start, int len)
        {
            if (srcbuff.Length < start + len)
            {
                throw new ArgumentOutOfRangeException("SubBuffer");
            }
            byte[] retbuff = new byte[len];
            for (int i = 0; i < len; i++)
            {
                retbuff[i] = srcbuff[i + start];
            }
            return retbuff;
        }

        /// <summary>
        /// 将 byte[] 顺序反转
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static byte[] SwapBytes(byte[] bytes)
        {
            int l = bytes.Length;
            byte[] Newb = new byte[l];

            for (int i = 0; i < l; i++)
            {
                Newb[i] = bytes[l - i - 1];
            }
            return Newb;
        }

        /// <summary>
        /// 获取 ushort 的高低位反转 byte[]
        /// </summary>
        /// <param name="u"></param>
        /// <returns></returns>
        internal static byte[] GetSwapBytes(ushort u)
        {
            return SwapBytes(BitConverter.GetBytes(u));
        }

        /// <summary>
        /// 获取 int 的高低位反转 byte[]
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        internal static byte[] GetSwapBytes(int i)
        {
            return SwapBytes(BitConverter.GetBytes(i));
        }

        /// <summary>
        /// 转义特殊字符，即 '~'(0x7E)
        /// </summary>
        /// <param name="srcbuff"></param>
        /// <returns></returns>
        public static byte[] SpecCharConvert(byte[] srcbuff)
        {
            List<byte> tmpBuff = new List<byte>();
            foreach (byte b in srcbuff)
            {
                if (b == (byte)'~')
                {
                    // 转义可能出现的 ~
                    tmpBuff.Add(0x7D);// 之后转义 0x7D
                    tmpBuff.Add(0x5E);
                }
                else if (b == 0x7D)
                {
                    // 转义 0x7D
                    tmpBuff.Add(0x7D);
                    tmpBuff.Add(0x5D);
                }
                else
                {
                    tmpBuff.Add(b);
                }
            }
            return tmpBuff.ToArray();
        }

        /// <summary>
        /// 反转义特殊字符，即 '~'(0x7E)
        /// 0x7D0x5E->0x7E, 0x7D0x5D->0x7D
        /// </summary>
        /// <param name="srcbuff"></param>
        /// <returns></returns>
        public static byte[] SpecCharReverse(byte[] srcbuff)
        {
            List<byte> tmpBuff = new List<byte>();
            for (int i = 0; i < srcbuff.Length; )
            {
                if (srcbuff[i] == 0x7D)
                {
                    if (srcbuff[i + 1] == 0x5E)
                    {
                        tmpBuff.Add((byte)'~');
                    }
                    else if (srcbuff[i + 1] == 0x5D)
                    {
                        tmpBuff.Add(0x7D);
                    }
                    else
                    {
                        throw new ArgumentException("非法数据");
                    }
                    i += 2;
                }
                else
                {
                    tmpBuff.Add(srcbuff[i]);
                    i += 1;
                }
            }
            return tmpBuff.ToArray();
        }

        /// <summary>
        /// 在 srcbuff 中查找 subbuff 第一次出现的位置
        /// </summary>
        /// <param name="srcbuff"></param>
        /// <param name="subbuff"></param>
        /// <returns>找到则返回匹配开始的位置，没有找到返回 -1</returns>
        public static int BufferLookup(byte[] srcbuff, byte[] subbuff)
        {
            return BufferLookup(srcbuff, subbuff, 0);
        }

        /// <summary>
        /// 在 srcbuff 中查找 subbuff 第一次出现的位置
        /// </summary>
        /// <param name="srcbuff"></param>
        /// <param name="subbuff"></param>
        /// <param name="start">开始查找的位置</param>
        /// <returns>找到则返回匹配开始的位置，没有找到返回 -1</returns>
        public static int BufferLookup(byte[] srcbuff, byte[] subbuff, int start)
        {
            for (int i = start; i < srcbuff.Length - subbuff.Length + 1; i++)
            {
                for (int j = 0; j < subbuff.Length; j++)
                {
                    if (srcbuff[i + j] != subbuff[j])
                    {
                        break;
                    }
                    if (j == subbuff.Length - 1)
                    {
                        // 能运行到这里表明 subbuff 中的字节都已经被匹配过
                        return i;
                    }
                }
            }
            return -1;
        }

        /// <summary>
        /// 在 srcbuff 中查找 subchars 第一次出现的位置
        /// </summary>
        /// <param name="srcbuff"></param>
        /// <param name="subchars">ASCII字符串</param>
        /// <returns>找到则返回匹配开始的位置，没有找到返回 -1</returns>
        public static int BufferLookup(byte[] srcbuff, string subchars)
        {
            return BufferLookup(srcbuff, subchars, 0);
        }

        /// <summary>
        /// 在 srcbuff 中查找 subchars 第一次出现的位置
        /// </summary>
        /// <param name="srcbuff"></param>
        /// <param name="subchars">ASCII字符串</param>
        /// <param name="start">开始查找的位置</param>
        /// <returns>找到则返回匹配开始的位置，没有找到返回 -1</returns>
        public static int BufferLookup(byte[] srcbuff, string subchars, int start)
        {
            byte[] subbuff = Encoding.ASCII.GetBytes(subchars);
            return BufferLookup(srcbuff, subbuff, start);
        }

        /// <summary>
        /// 将二进制的数据转成Hex格式
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string BytesToHex(byte[] bytes)
        {
            if (bytes == null)
                return "";
            string str = "";
            for (int i = 0; i < bytes.Length; i++)
            {
                str += bytes[i].ToString("X2");
            }
            return str;
        }

        /// <summary>
        /// 将日期时间格式的字符串转换到数据库使用的日期类型
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static byte[] ToDBDate(DateTime dateTime)
        {
            Byte[] ret = new Byte[15]; //后面的字节没什么用，只是因为原先参考的定义使用了 15。
            ret[0] = (Byte)(dateTime.Year / 100 + 100);
            ret[1] = (Byte)(dateTime.Year % 100 + 100);
            ret[2] = (Byte)dateTime.Month;
            ret[3] = (Byte)dateTime.Day;
            ret[4] = (Byte)(dateTime.Hour + 1);
            ret[5] = (Byte)(dateTime.Minute + 1);
            ret[6] = (Byte)(dateTime.Second + 1);
            return ret;
        }

        /// <summary>
        /// 将倒序的ushort字节还原为ushort
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="start"></param>
        /// <returns></returns>
        internal static ushort GetSwappedUshort(byte[] buffer, int start)
        {
            byte[] tmp = new byte[2];
            tmp[0] = buffer[start];
            tmp[1] = buffer[start + 1];

            return BitConverter.ToUInt16(BytesTools.SwapBytes(tmp), 0);
        }

        /// <summary>
        /// 将倒序的uint字节还原为uint
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="start"></param>
        /// <returns></returns>
        internal static uint GetSwappedUint(byte[] buffer, int start)
        {
            byte[] tmp = BytesTools.SubBuffer(buffer, start, 4);
            return BitConverter.ToUInt32(BytesTools.SwapBytes(tmp), 0);
        }

        /// <summary>
        /// 转换时间格式
        /// 原始数据：FFFF-FF-FF FF:FF:FF 每个F对应YYYY-MM-DD hh:mm:ss中的一个数字
        ///              共7个字节。比如 0x20 0x08 0x09 0x20 0x23 0x12 0x34 表示 2008-09-20 23:12:34
        /// </summary>
        /// <param name="timeBuff"></param>
        /// <returns></returns>
        internal static DateTime Revert7BDateTime(byte[] timeBuff)
        {
            string timestr = "";
            for (int i = 0; i < 7; i++)
            {
                timestr+=timeBuff[i].ToString("X2");
            }
            timestr = timestr.Substring(0, 4) + "-" + timestr.Substring(4, 2) + "-" + timestr.Substring(6, 2) + " " + timestr.Substring(8, 2) + ":" + timestr.Substring(10, 2) + ":" + timestr.Substring(12, 2);
            try
            {
                return Convert.ToDateTime(timestr);
            }
            catch
            {
                return new DateTime(2000, 01, 01);
            }
        }

        /// <summary>
        /// Hex进制转二进制
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static byte[] HexToBytes(string input)
        {
            int i = input.Length % 2;
            if (i != 0)
            {
                throw new Exception("字符串的长度必需是偶数");
            }
            List<byte> ls = new List<byte>();
            for (int j = 0; j < input.Length; j += 2)
            {
                byte b = Convert.ToByte(input.Substring(j, 2), 16);
                ls.Add(b);
            }
            return ls.ToArray();
        }



        /// <summary>
        /// 确定两个字节数组是相等的。
        /// </summary>
        /// <param name="byte1">第一个字节数组进行比较。</param>
        /// <param name="byte2">字节数组的第一个比较。</param>
        /// <returns><see langword="true"/> 如果两个字节数组是相等的，否则 <see langword="false"/>.</returns>
        public static bool Compare(byte[] byte1, byte[] byte2)
        {
            if (byte1 == null)
            {
                return false;
            }

            if (byte2 == null)
            {
                return false;
            }

            if (byte1.Length != byte2.Length)
            {
                return false;
            }

            bool result = true;
            for (int i = 0; i < byte1.Length; i++)
            {
                if (byte1[i] != byte2[i])
                {
                    result = false;
                    break;
                }
            }

            return result;
        }

        /// <summary>
        /// 结合成一个两个字节数组。
        /// </summary>
        /// <param name="byte1">前缀字节。</param>
        /// <param name="byte2">后缀字节。</param>
        /// <returns>合并后的字节数组.</returns>
        public static byte[] Combine(byte[] byte1, byte[] byte2)
        {
            if (byte1 == null)
            {
                throw new ArgumentNullException("byte1");
            }

            if (byte2 == null)
            {
                throw new ArgumentNullException("byte2");
            }

            byte[] combinedBytes = new byte[byte1.Length + byte2.Length];
            Buffer.BlockCopy(byte1, 0, combinedBytes, 0, byte1.Length);
            Buffer.BlockCopy(byte2, 0, combinedBytes, byte1.Length, byte2.Length);

            return combinedBytes;
        }

        /// <summary>
        /// 复制到一个新的字节数组。
        /// </summary>
        /// <param name="byte1">字节数组创建新的阵列。</param>
        /// <returns>克隆的字节数组。</returns>
        public static byte[] Clone(byte[] byte1)
        {
            if (byte1 == null)
            {
                throw new ArgumentNullException("byte1");
            }

            byte[] copyBytes = new byte[byte1.Length];

            Buffer.BlockCopy(byte1, 0, copyBytes, 0, byte1.Length);

            return copyBytes;


        }

        /// <summary>
        /// <para>代表一个十六进制数字的字符串返回一个字节数组。</para>
        /// </summary>
        /// <param name="hexadecimalNumber">
        /// <para>该字符串包含一个有效的十六进制数字。</para>
        /// </param>
        /// <returns><para>代表的十六进制字节数组。</para></returns>
        public static byte[] GetBytesFromHexString(string hexadecimalNumber)
        {
            string InvalidHexString = "String must represent a valid hexadecimal (e.g. : 0F99DD)";
            if (String.IsNullOrEmpty(hexadecimalNumber))
            {
                throw new ArgumentNullException("hexadecimalNumber");
            }

            StringBuilder sb = new StringBuilder(hexadecimalNumber.ToUpper(CultureInfo.CurrentCulture));

            if (sb[0].Equals('0') && sb[1].Equals('X'))
            {
                sb.Remove(0, 2);
            }

            if (sb.Length % 2 != 0)
            {
                throw new ArgumentException(InvalidHexString);
            }

            byte[] hexBytes = new byte[sb.Length / 2];
            try
            {
                for (int i = 0; i < hexBytes.Length; i++)
                {
                    int stringIndex = i * 2;
                    hexBytes[i] = Convert.ToByte(sb.ToString(stringIndex, 2), 16);
                }
            }
            catch (FormatException ex)
            {
                throw new ArgumentException(InvalidHexString, ex);
            }

            return hexBytes;
        }

        /// <summary>
        /// <para>返回一个字符串的字节数组表示为十六进制数（如：0F351A）。</para>
        /// </summary>
        /// <param name="bytes">
        /// <para>字节数组转换为一个十六进制数字格式。</para>
        /// </param>
        /// <returns>
        /// <para>作为一个十六进制数的字节格式表示。</para>
        /// </returns>
        public static string GetHexStringFromBytes(byte[] bytes)
        {
            if (bytes == null)
            {
                throw new ArgumentNullException("bytes");
            }

            if (bytes.Length == 0)
            {
                throw new ArgumentOutOfRangeException("bytes", "The value must be greater than 0 bytes.");
            }


            StringBuilder sb = new StringBuilder(bytes.Length * 2);
            for (int i = 0; i < bytes.Length; i++)
            {
                sb.Append(bytes[i].ToString("X2", CultureInfo.CurrentCulture));
            }
            return sb.ToString();
        }
    }
}
