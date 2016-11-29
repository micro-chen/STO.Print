using System.Text.RegularExpressions;
using System.Web;

namespace STO.Print.Utilities
{
    public class SQLInjectionHelper
    {
        private const string StrKeyWord = @".*(select|insert|delete|from|count(|drop table|update|truncate|asc(|mid(|char(|xp_cmdshell|exec master|netlocalgroup administrators|:|net user|""|or|and).*";
        private const string StrRegex = @"[-|;|,|/|(|)|[|]|}|{|%|@|*|!|']";

        /// <summary>
        /// 获取Post的数据
        /// </summary>
        public static bool ValidUrlPostData()
        {
            bool result = false;

            for (int i = 0; i < HttpContext.Current.Request.Form.Count; i++)
            {
                result = ValidData(HttpContext.Current.Request.Form[i].ToString());
                if (result)
                {
                    LogHelper.Info("检测出POST恶意数据: 【" + HttpContext.Current.Request.Form[i].ToString() + "】 URL: 【" + HttpContext.Current.Request.RawUrl + "】来源: 【" + HttpContext.Current.Request.UserHostAddress + "】");
                    break;
                }//如果检测存在漏洞
            }
            return result;
        }

        /// <summary>
        /// 获取QueryString中的数据
        /// </summary>
        public static bool ValidUrlGetData()
        {
            bool result = false;

            for (int i = 0; i < HttpContext.Current.Request.QueryString.Count; i++)
            {
                result = ValidData(HttpContext.Current.Request.QueryString[i].ToString());
                if (result)
                {
                    LogHelper.Info("检测出GET恶意数据: 【" + HttpContext.Current.Request.QueryString[i].ToString() + "】 URL: 【" + HttpContext.Current.Request.RawUrl + "】来源: 【" + HttpContext.Current.Request.UserHostAddress + "】");
                    break;
                }//如果检测存在漏洞
            }
            return result;
        }

        /// <summary>
        /// 验证是否存在注入代码
        /// </summary>
        /// <param name="inputData"></param>
        public static bool ValidData(string inputData)
        {
            //里面定义恶意字符集合
            //验证inputData是否包含恶意集合
            if (Regex.IsMatch(inputData.ToLower(), GetRegexString()))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 获取正则表达式
        /// </summary>
        /// <param name="queryConditions"></param>
        /// <returns></returns>
        private static string GetRegexString()
        {
            //构造SQL的注入关键字符
            string[] strBadChar =
        {
            "and"
            ,"exec"
            ,"insert"
            ,"select"
            ,"delete"
            ,"update"
            ,"count"
            ,"from"
            ,"drop"
            ,"asc"
            ,"char"
            ,"or"
            ,"%"
            ,";"
            ,":"
            ,"\'"
            ,"\""
            ,"-"
            ,"chr"
            ,"mid"
            ,"master"
            ,"truncate"
            ,"char"
            ,"declare"
            ,"SiteName"
            ,"net user"
            ,"xp_cmdshell"
            ,"/add"
            ,"exec master.dbo.xp_cmdshell"
            ,"net localgroup administrators"
        };

            //构造正则表达式
            string str_Regex = ".*(";
            for (int i = 0; i < strBadChar.Length - 1; i++)
            {
                str_Regex += strBadChar[i] + "|";
            }
            str_Regex += strBadChar[strBadChar.Length - 1] + ").*";

            return str_Regex;
        }
    }
}
