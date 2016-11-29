using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace STO.Print.Utilities
{
    //获取中文字首字拼写,随机发生器，按指定概率随机执行操作
    public sealed class Util
    {
        private Util()
        {
        }

        public static bool AreObjectsEqual(Object o1, Object o2)
        {
            Type t1 = o1.GetType();
            Type t2 = o2.GetType();

            PropertyInfo[] pi1 = t1.GetProperties();
            PropertyInfo[] pi2 = t2.GetProperties();

            for (int i = 0; i < pi1.Length; i++)
            {
                try
                {
                    PropertyInfo i1 = pi1[i];
                    PropertyInfo i2 = pi2[i];

                    if (!i1.GetValue(o1, null).Equals(null) && !i2.GetValue(o2, null).Equals(null))
                    {
                        if (!i1.GetValue(o1, null).Equals(i2.GetValue(o2, null)))
                        {
                            return false;
                        }
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }

            FieldInfo[] fi1 = t1.GetFields();
            FieldInfo[] fi2 = t2.GetFields();

            for (int i = 0; i < fi1.Length; i++)
            {
                try
                {
                    FieldInfo i1 = fi1[i];
                    FieldInfo i2 = fi2[i];

                    if (!i1.GetValue(o1).Equals(null) && !i2.GetValue(o2).Equals(null))
                    {
                        if (!i1.GetValue(o1).Equals(i2.GetValue(o2)))
                        {
                            return false;
                        }
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }

            return true;
        }

        public static bool IsNumeric(object o)
        {
            if (o is Int16 ||
                o is Int32 ||
                o is Int64 ||
                o is Decimal ||
                o is Double ||
                o is Byte ||
                o is SByte ||
                o is Single ||
                o is UInt16 ||
                o is UInt32 ||
                o is UInt64)
            {
                return true;
            }
            return false;
        }

        public static bool IsDateTime(object o)
        {
            if (o is DateTime) return true;
            return false;
        }

        #region 中文首字母

        /// <summary>
        /// 获取中文字首字拼写
        /// </summary>
        /// <param name="chinese"></param>
        /// <returns></returns>
        public static string GetAcronym(string chinese)
        {
            int length = chinese.Length;
            StringBuilder acronym = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                acronym.Append(getSingle(chinese[i].ToString()));
            }
            return acronym.ToString(); ;
        }

        /// <summary>
        /// 获取单个字首字母 (GB2312)
        /// </summary>
        /// <param name="cnChar"></param>
        /// <returns></returns>
        private static string getSingle(string cnChar)
        {
            byte[] arrCN = Encoding.GetEncoding("GB2312").GetBytes(cnChar);
            if (arrCN.Length > 1)
            {
                int area = (short)arrCN[0];
                int pos = (short)arrCN[1];
                int code = (area << 8) + pos;
                int[] areacode = { 45217, 45253, 45761, 46318, 46826, 47010, 47297, 47614, 48119, 48119, 49062, 49324, 49896, 50371, 50614, 50622, 50906, 51387, 51446, 52218, 52698, 52698, 52698, 52980, 53689, 54481 };
                for (int i = 0; i < 26; i++)
                {
                    int max = 55290;
                    if (i != 25) max = areacode[i + 1];
                    if (areacode[i] <= code && code < max)
                    {
                        return Encoding.GetEncoding("GB2312").GetString(new byte[] { (byte)(65 + i) });
                    }
                }
                return string.Empty;
            }
            else return cnChar;
        }

        #endregion

        #region 合并数组

        /// <summary>
        /// 用 , 分割 合并数组
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static string Join<T>(IList<T> list)
        {
            return Join<T>(list, ",");
        }

        /// <summary>
        /// 用 , 分割 合并数组
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static string Join(IList<string> list)
        {
            return Join<string>(list, ",");
        }

        /// <summary>
        /// 用 , 分割 合并数组
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static string Join(IList<string> list, string c)
        {
            return Join<string>(list, c);
        }


        /// <summary>
        /// 合并数组 分割符
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">数组列</param>
        /// <param name="c">分隔符</param>
        /// <returns></returns>
        public static string Join<T>(IList<T> list, string c)
        {
            if (null != list && list.Count > 0)
            {
                StringBuilder text = new StringBuilder();
                foreach (T t in list)
                {
                    text.Append(c);
                    text.Append(t.ToString());
                }

                if (!string.IsNullOrEmpty(c))
                    return text.ToString().Substring(c.Length);

                return text.ToString();
            }

            return string.Empty;
        }


        #endregion

        #region 获取Windows Form应用程序的名字
        /// <summary>
        /// 获取当前Windows Form应用程序的名字,不包括.exe
        /// </summary>
        public static string WinFormName
        {
            get
            {
                //获取应用程序的完全路径
                string appPath = Application.ExecutablePath;

                //找到最后一个'\'的位置
                int beginIndex = appPath.LastIndexOf(@"\");

                //找到".exe"的位置
                int endIndex = appPath.ToLower().LastIndexOf(".exe");

                //返回Windows Form应用程序的名字
                return appPath.Substring(beginIndex + 1, endIndex - beginIndex - 1);
            }
        }
        #endregion

        static Random m_random = new Random((int)DateTime.Now.Ticks);
        /// <summary>
        /// 随机发生器，按指定概率随机执行操作
        /// </summary>
        /// <param name="rate"></param>
        /// <returns></returns>
        public static bool RandomAction(int rate)
        {
            return m_random.Next(100) < rate;
        }
    }
}