using System;
using System.Text;

namespace STO.Print.Utilities
{
    public class UnicodeHelper
    {
        /// <summary>
        /// 将原始字串转换为unicode,格式为\u.\u.
        /// </summary>
        /// <param name="srcText"></param>
        /// <returns></returns>
        public static string StringToUnicode(string str)
        {
            //中文转为UNICODE字符
            string outStr = "";
            if (!string.IsNullOrEmpty(str))
            {
                for (int i = 0; i < str.Length; i++)
                {
                    //将中文字符转为10进制整数，然后转为16进制unicode字符
                    outStr += "\\u" + ((int)str[i]).ToString("x");
                }
            }
            return outStr;
        }

        /// <summary>
        /// 将Unicode字串\u.\u.格式字串转换为原始字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string UnicodeToString(string str)
        {
            string outStr = "";

            str = CRegex.Replace(str, "[\r\n]", "", 0);

            if (!string.IsNullOrEmpty(str))
            {
                string[] strlist = str.Replace("\\u", "㊣").Split('㊣');
                try
                {
                    outStr += strlist[0];
                    for (int i = 1; i < strlist.Length; i++)
                    {
                        string strTemp = strlist[i];
                        if (!string.IsNullOrEmpty(strTemp) && strTemp.Length >= 4)
                        {
                            strTemp = strlist[i].Substring(0, 4);
                            //将unicode字符转为10进制整数，然后转为char中文字符
                            outStr += (char)int.Parse(strTemp , System.Globalization.NumberStyles.HexNumber);
                            outStr += strlist[i].Substring(4);
                        }
                    }
                }
                catch (FormatException ex)
                {
                    outStr += "Erorr";//ex.Message;
                }
            }
            return outStr;
        }

        /// <summary>
        /// GB2312转换成unicode编码 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GB2Unicode(string str)
        {
            string Hexs = "";
            string HH;
            Encoding GB = Encoding.GetEncoding("GB2312");
            Encoding unicode = Encoding.Unicode;
            byte[] GBBytes = GB.GetBytes(str);
            for (int i = 0; i < GBBytes.Length; i++)
            {
                HH = "%" + GBBytes[i].ToString("X");
                Hexs += HH;
            }
            return Hexs;
        }

        /// <summary>
        /// 得到单个字符的值
        /// </summary>
        /// <param name="ch"></param>
        /// <returns></returns>
        private static ushort GetByte(char ch)
        {
            ushort rtnNum = 0;
            switch (ch)
            {
                case 'a':
                case 'A': rtnNum = 10; break;
                case 'b':
                case 'B': rtnNum = 11; break;
                case 'c':
                case 'C': rtnNum = 12; break;
                case 'd':
                case 'D': rtnNum = 13; break;
                case 'e':
                case 'E': rtnNum = 14; break;
                case 'f':
                case 'F': rtnNum = 15; break;
                default: rtnNum = ushort.Parse(ch.ToString()); break;

            }
            return rtnNum;
        }

        /// <summary>
        /// 转换一个字符，输入如"Π"中的"03a0"
        /// </summary>
        /// <param name="unicodeSingle"></param>
        /// <returns></returns>
        public static string ConvertSingle(string unicodeSingle)
        {
            if (unicodeSingle.Length!=4) 
                return null ;
              Encoding unicode = Encoding.Unicode;
              byte[] unicodeBytes = new byte[2] { 0, 0 };
              for (int i = 0; i < 4; i++)
              {
                  switch (i)
                  {
                      case 0: unicodeBytes[1] += (byte)(GetByte(unicodeSingle[i]) * 16); break;
                      case 1: unicodeBytes[1] += (byte)GetByte(unicodeSingle[i]); break;
                      case 2: unicodeBytes[0] += (byte)(GetByte(unicodeSingle[i]) * 16); break;
                      case 3: unicodeBytes[0] += (byte)GetByte(unicodeSingle[i]); break;
                  }
              }

              char[] asciiChars = new char[unicode.GetCharCount(unicodeBytes, 0, unicodeBytes.Length)];
              unicode.GetChars(unicodeBytes, 0, unicodeBytes.Length, asciiChars, 0);
              string asciiString = new string(asciiChars);

              return asciiString;

        }

        /// <summary>
        /// unicode编码转换成GB2312汉字 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string UtoGB(string str)
        {
            string[] ss = str.Replace("\\", "").Split('u');
            byte[] bs = new Byte[ss.Length - 1];
            for (int i = 1; i < ss.Length; i++)
            {
                bs[i - 1] = Convert.ToByte(Convert2Hex(ss[i]));   //ss[0]为空串   
            }
            char[] chrs = System.Text.Encoding.GetEncoding("GB2312").GetChars(bs);
            string s = "";
            for (int i = 0; i < chrs.Length; i++)
            {
                s += chrs[i].ToString();
            }
            return s;
        }

        private static string Convert2Hex(string pstr)
        {
            if (pstr.Length == 2)
            {
                pstr = pstr.ToUpper();
                string hexstr = "0123456789ABCDEF";
                int cint = hexstr.IndexOf(pstr.Substring(0, 1)) * 16 + hexstr.IndexOf(pstr.Substring(1, 1));
                return cint.ToString();
            }
            else
            {
                return "";
            }
        }

    }
}
