//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2014 , Hairihan TECH, Ltd. 
//-----------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Microsoft.International.Converters.PinYinConverter;

namespace DotNet.Utilities
{
    public partial class StringUtil
    {
        /// <summary>
        /// 获取汉字的拼音首字母
        /// </summary>
        /// <param name="targetValue">目标汉字</param>
        /// <returns>拼音</returns>
        public static string GetFirstPinyin(string targetValue)
        {
            string result = string.Empty;
            if (!string.IsNullOrEmpty(targetValue))
            {
                foreach (Char c in targetValue)
                {
                    if (ChineseChar.IsValidChar(c))
                    {
                        ChineseChar chineseChar = new ChineseChar(c);
                        // 汉字的所有拼音拼写
                        ReadOnlyCollection<string> pinyins = chineseChar.Pinyins;
                        result += pinyins[0].Substring(0, 1).ToUpper();
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 获取汉字的拼音
        /// </summary>
        /// <param name="targetValue">目标汉字</param>
        /// <param name="pinYinOnly">只要拼音</param>
        /// <returns>拼音</returns>
        public static string GetPinyin(string targetValue, bool pinYinOnly = true)
        {
            string result = string.Empty;
            if (!string.IsNullOrEmpty(targetValue))
            {
                foreach (Char c in targetValue)
                {
                    if (ChineseChar.IsValidChar(c))
                    {
                        ChineseChar chineseChar = new ChineseChar(c);
                        // 汉字的所有拼音拼写
                        ReadOnlyCollection<string> pinyins = chineseChar.Pinyins;
                        result += pinyins[0].Substring(0, pinyins[0].Length - 1).ToLower();
                    }
                    else
                    {
                        if (!pinYinOnly)
                        {
                            result += c;
                        }
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 获取汉字的简拼
        /// </summary>
        /// <param name="targetValue">目标汉字</param>
        /// <param name="pinYinOnly">只要拼音</param>
        /// <returns>拼音</returns>
        public static string GetSimpleSpelling(string targetValue, bool pinYinOnly = true)
        {
            string result = string.Empty;
            if (!string.IsNullOrEmpty(targetValue))
            {
                foreach (Char c in targetValue)
                {
                    if (ChineseChar.IsValidChar(c))
                    {
                        ChineseChar chineseChar = new ChineseChar(c);
                        // 汉字的所有拼音拼写
                        ReadOnlyCollection<string> pinyins = chineseChar.Pinyins;
                        result += pinyins[0].Substring(0, 1).ToLower();
                    }
                    else
                    {
                        if (!pinYinOnly)
                        {
                            result += c;
                        }
                    }
                }
            }
            return result;
        }
    }
}