//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd.  
//-----------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace DotNet.Utilities
{
    public partial class StringUtil
    {
        /// <summary>
        /// 获取子查询条件，这需要处理多个模糊匹配的字符
        /// </summary>
        /// <param name="field">字段</param>
        /// <param name="search">模糊查询</param>
        /// <returns>表达式</returns>
        public static string GetLike(string field, string search)
        {
            string result = string.Empty;
            for (int i = 0; i < search.Length; i++)
            {
                result += field + " LIKE '%" + search[i] + "%' AND ";
            }
            if (!string.IsNullOrEmpty(result))
            {
                result = result.Substring(0, result.Length - 5);
            }
            result = "(" + result + ")";
            return result;
        }

        #region public static string GetSearchString(string searchValue, string allLike = null) 获取查询字符串
        /// <summary>
        /// 获取查询字符串
        /// </summary>
        /// <param name="search">查询字符</param>
        /// <param name="allLike">是否所有的匹配都查询，建议传递"%"字符</param>
        /// <returns>字符串</returns>
        public static string GetSearchString(string searchValue, bool allLike = false)
        {
            if (!string.IsNullOrEmpty(searchValue))
            {
                searchValue = searchValue.Trim();
                searchValue = SecretUtil.SqlSafe(searchValue);
                if (searchValue.Length > 0)
                {
                    searchValue = searchValue.Replace('[', '_');
                    searchValue = searchValue.Replace(']', '_');
                }
                if (searchValue == "%")
                {
                    searchValue = "[%]";
                }
                if ((searchValue.Length > 0) && (searchValue.IndexOf('%') < 0) && (searchValue.IndexOf('_') < 0))
                {
                    if (allLike)
                    {
                        string searchLike = string.Empty;
                        for (int i = 0; i < searchValue.Length; i++)
                        {
                            searchLike += "%" + searchValue[i];
                        }
                        searchValue = searchLike + "%";
                    }
                    else
                    {
                        searchValue = "%" + searchValue + "%";
                    }
                }
            }
            return searchValue;
        }
        #endregion

        #region  public static bool Exists(string[] ids, string targetString) 判断是否包含的方法
        /// <summary>
        /// 判断是否包含的方法
        /// </summary>
        /// <param name="ids">数组</param>
        /// <param name="targetString">目标值</param>
        /// <returns>包含</returns>
        public static bool Exists(string[] ids, string targetString)
        {
            bool result = false;
            if (ids != null && !string.IsNullOrEmpty(targetString))
            {
                for (int i = 0; i < ids.Length; i++)
                {
                    if (ids[i].Equals(targetString))
                    {
                        result = true;
                        break;
                    }
                }
            }
            return result;
        }
        #endregion

        public static string[] Concat(string[] ids, string id)
        {
            return Concat(ids, new string[] { id });
        }

        #region public static string[] Concat(params string[][] ids) 合并数组
        /// <summary>
        /// 合并数组
        /// </summary>
        /// <param name="ids">数组</param>
        /// <returns>数组</returns>
        public static string[] Concat(params string[][] ids)
        {
            // 进行合并
            List<string> result = new List<string>();

            if (ids != null)
            {
                for (int i = 0; i < ids.Length; i++)
                {
                    if (ids[i] != null)
                    {
                        for (int j = 0; j < ids[i].Length; j++)
                        {
                            if (!string.IsNullOrEmpty(ids[i][j]))
                            {
                                if (!result.Contains(ids[i][j]))
                                {
                                    result.Add(ids[i][j]);
                                }
                            }
                        }
                    }
                }
            }

            //  返回合并结果
            return result.ToArray();
        }
        #endregion

        #region public static string[] Remove(string[] ids, string[] removeIds) 从目标数组中去除某个值
        /// <summary>
        /// 从目标数组中去除某个数组
        /// </summary>
        /// <param name="ids">数组</param>
        /// <param name="removeIds">目标值数组</param>
        /// <returns>数组</returns>
        public static string[] Remove(string[] ids, string[] removeIds)
        {
            // 进行合并
            List<string> keys = new List<string>();
            if (ids != null)
            {
                for (int i = 0; i < ids.Length; i++)
                {
                    if (ids[i] != null)
                    {
                        if (!Exists(removeIds, ids[i]))
                        {
                            keys.Add(ids[i]);
                        }
                    }
                }
            }
            // 返回合并结果
            return keys.ToArray();
        }
        #endregion

        public static string[] Remove(string[] ids, string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                return Remove(ids, new string[] { id });
            }
            return ids;
        }

        public static string ArrayToList(string[] ids)
        {
            return ArrayToList(ids, string.Empty);
        }

        public static string ArrayToList(string[] ids, string separativeSign)
        {
            int rowCount = 0;
            string result = string.Empty;
            foreach (string id in ids)
            {
                rowCount++;
                result += separativeSign + id + separativeSign + ",";
            }
            if (rowCount == 0)
            {
                result = "";
            }
            else
            {
                result = result.TrimEnd(',');
            }
            return result;
        }

        #region public static string RepeatString(string targetString, int repeatCount) 重复字符串
        /// <summary>
        /// 重复字符串
        /// </summary>
        /// <param name="targetString">目标字符串</param>
        /// <param name="repeatCount">重复次数</param>
        /// <returns>结果字符串</returns>
        public static string RepeatString(string targetString, int repeatCount)
        {
            string result = string.Empty;
            for (int i = 0; i < repeatCount; i++)
            {
                result += targetString;
            }
            return result;
        }
        #endregion

        /// <summary>
        /// 删除不可见字符
        /// </summary>
        /// <param name="sourceString"></param>
        /// <returns></returns>
        public static string DeleteUnVisibleChar(string sourceString)
        {
            var sBuilder = new StringBuilder(131);
            for (int i = 0; i < sourceString.Length; i++)
            {
                int Unicode = sourceString[i];
                if (Unicode >= 16)
                {
                    sBuilder.Append(sourceString[i].ToString());
                }
            }
            return sBuilder.ToString();
        }


        public static string[] SplitMobile(string mobiles, bool mobileOnly = true, bool distinct = true)
        {
            // 把常用的风格符号都用上，全角半角的
            mobiles = Regex.Replace(mobiles.Trim(), "\\s+", " ");
            string[] mobile = mobiles.Replace("\r\n", "\r").Replace("，", ",").Replace("；", ",").Replace(";", ",").Split(' ');
            List<string> mobileList = new List<string>();
            foreach (var cellPhone in mobile)
            {
                string phones = cellPhone.Trim();
                if (!string.IsNullOrEmpty(phones))
                {
                    // 用回车分割，然后再用,符号分割
                    string[] phone = phones.Split(',');
                    foreach (var p in phone)
                    {
                        // 手机号码长度对的，才可以发送
                        if (mobileOnly && p.Trim().Length == 11)
                        {
                            mobileList.Add(p.Trim());
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(p.Trim()))
                            {
                                mobileList.Add(p.Trim());
                            }
                        }
                    }
                }
            }
            // 去掉重复，不要空的，有时候需要发重复的短信的，因为有多个包裹时，需要有重复的信息
            if (distinct)
            {
                mobile = mobileList.Distinct<string>().Where(t => !string.IsNullOrEmpty(t)).ToArray();
            }
            else
            {
                mobile = mobileList.Where(t => !string.IsNullOrEmpty(t)).ToArray();
            }
            return mobile;
        }
    }
}