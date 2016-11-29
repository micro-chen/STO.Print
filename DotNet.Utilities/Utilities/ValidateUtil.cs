//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd.  
//-----------------------------------------------------------------

using System.IO;
using System.Text.RegularExpressions;

namespace DotNet.Utilities
{
    public class ValidateUtil
    {
        public static string PositiveInteger = @"^[0-9]*[1-9][0-9]*$";// 正整数正则表达式

        public static string Integer = @"^-?\d+$";// 整数正则表达式

        public static string Float = @"^[+|-]?\d*\.?\d*$";// 浮点数正则表达式

        /// <summary>
        /// 匹配是否是ipV4地址
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <returns>是返回 true 不是返回false</returns>
        public static bool IsIPV4(string ipAddress)
        {
            var match =
               new System.Text.RegularExpressions.Regex(@"^(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])$");
            if (!match.IsMatch(ipAddress))
            {
                return (IsIPV6(ipAddress));
            }
            return true;
        }

        /// <summary>
        /// 判断输入的字符串是否是合法的IPV6 地址
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        public static bool IsIPV6(string ipAddress)
        {
            string pattern = "";
            string temp = ipAddress;
            string[] strs = temp.Split(':');
            if (strs.Length > 8)
            {
                return false;
            }
            int count = GetStringCount(ipAddress, "::");
            if (count > 1)
            {
                return false;
            }
            else if (count == 0)
            {
                pattern = @"^([\da-f]{1,4}:){7}[\da-f]{1,4}$";

                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(pattern);
                return regex.IsMatch(ipAddress);
            }
            else
            {
                pattern = @"^([\da-f]{1,4}:){0,5}::([\da-f]{1,4}:){0,5}[\da-f]{1,4}$";
                System.Text.RegularExpressions.Regex regex1 = new System.Text.RegularExpressions.Regex(pattern);
                return regex1.IsMatch(ipAddress);
            }
        }

        /* *******************************************************************
         * 1、通过“:”来分割字符串看得到的字符串数组长度是否小于等于8
         * 2、判断输入的IPV6字符串中是否有“::”。
         * 3、如果没有“::”采用 ^([\da-f]{1,4}:){7}[\da-f]{1,4}$ 来判断
         * 4、如果有“::” ，判断"::"是否止出现一次
         * 5、如果出现一次以上 返回false
         * 6、^([\da-f]{1,4}:){0,5}::([\da-f]{1,4}:){0,5}[\da-f]{1,4}$
         * ******************************************************************/
        /// <summary>
        /// 判断字符串compare 在 input字符串中出现的次数
        /// </summary>
        /// <param name="input">源字符串</param>
        /// <param name="compare">用于比较的字符串</param>
        /// <returns>字符串compare 在 input字符串中出现的次数</returns>
        private static int GetStringCount(string input, string compare)
        {
            int index = input.IndexOf(compare);
            if (index != -1)
            {
                return 1 + GetStringCount(input.Substring(index + compare.Length), compare);
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 验证是否为正整数
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <returns></returns>
        public static bool UnsafeCharacter(string expression)
        {
            bool result = false;
            if (!result)
            {
                result = expression.IndexOf('\'') >= 0;
            }
            if (!result)
            {
                result = expression.IndexOf('<') >= 0;
            }
            if (!result)
            {
                result = expression.IndexOf('>') >= 0;
            }
            if (!result)
            {
                result = expression.IndexOf('%') >= 0;
            }
            if (!result)
            {
                result = expression.IndexOf('_') >= 0;
            }
            if (!result)
            {
                result = expression.IndexOf('?') >= 0;
            }
            return result;
        }

        /// <summary>
        /// 验证是否为正整数
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <returns></returns>
        public static bool IsInt(string expression)
        {
            return Regex.IsMatch(expression.Trim(), @"^[0-9]*$");
        }

        public static bool IsBoolean(string expression)
        {
            bool result = false;
            if (!string.IsNullOrWhiteSpace(expression))
            {
                if (expression.Equals(true.ToString())
                    || expression.Equals(false.ToString()))
                {
                    result = true;
                }
            }
            return result;
        }


        /// <summary>
        /// 判断对象是否为Int32类型的数字
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <returns></returns>
        public static bool IsNumeric(object expression)
        {
            if (expression != null)
            {
                string numericValue = expression.ToString();
                if (numericValue.Length > 0 && numericValue.Length <= 11 && Regex.IsMatch(numericValue, @"^[-]?[0-9]*[.]?[0-9]*$"))
                {
                    if ((numericValue.Length < 10) || (numericValue.Length == 10 && numericValue[0] == '1') || (numericValue.Length == 11 && numericValue[0] == '-' && numericValue[1] == '1'))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /*
        /// <summary>是否数字</summary>
        /// <param name="strInput">输入字符串</param>
        /// <returns>true/false</returns>
        public static bool IsNumeric(string inputNumeric)
        {
            if (string.IsNullOrEmpty(inputNumeric))
            {
                return false;
            }
            var reg = new Regex(@"^[-]?\d+[.]?\d*$");
            return reg.IsMatch(inputNumeric);
        }
        */

        public static bool IsDouble(object expression)
        {
            if (expression != null)
            {
                return Regex.IsMatch(expression.ToString(), @"^([0-9])[0-9]*(\.\w*)?$");
            }
            return false;
        }

        /// <summary>
        /// 判断给定的字符串数组(strNumber)中的数据是不是都为数值型
        /// </summary>
        /// <param name="numbers">要确认的字符串数组</param>
        /// <returns>是则返加true 不是则返回 false</returns>
        public static bool IsNumericArray(string[] numbers)
        {
            if (numbers == null || numbers.Length < 1)
            {
                return false;
            }
            foreach (string id in numbers)
            {
                if (!IsNumeric(id))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 是不是时间类型数据
        /// </summary>
        /// <param name="dateTime">表达式</param>
        /// <returns></returns>
        public static bool IsDateTime(string dateTime)
        {
            dateTime = dateTime.Trim();
            var reg =
                new Regex(
                    @"(((^((1[8-9]\d{2})|([2-9]\d{3}))([-\/\._])(10|12|0?[13578])([-\/\._])(3[01]|[12][0-9]|0?[1-9]))|(^((1[8-9]\d{2})|([2-9]\d{3}))([-\/\._])(11|0?[469])([-\/\._])(30|[12][0-9]|0?[1-9]))|(^((1[8-9]\d{2})|([2-9]\d{3}))([-\/\._])(0?2)([-\/\._])(2[0-8]|1[0-9]|0?[1-9]))|(^([2468][048]00)([-\/\._])(0?2)([-\/\._])(29))|(^([3579][26]00)([-\/\._])(0?2)([-\/\._])(29))|(^([1][89][0][48])([-\/\._])(0?2)([-\/\._])(29))|(^([2-9][0-9][0][48])([-\/\._])(0?2)([-\/\._])(29))|(^([1][89][2468][048])([-\/\._])(0?2)([-\/\._])(29))|(^([2-9][0-9][2468][048])([-\/\._])(0?2)([-\/\._])(29))|(^([1][89][13579][26])([-\/\._])(0?2)([-\/\._])(29))|(^([2-9][0-9][13579][26])([-\/\._])(0?2)([-\/\._])(29)))((\s+(0?[1-9]|1[012])(:[0-5]\d){0,2}(\s[AP]M))?$|(\s+([01]\d|2[0-3])(:[0-5]\d){0,2})?$))");
            return reg.IsMatch(dateTime);
        }

        /// <summary>
        /// 检查邮件是否何法 add by wxg 20090531
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static bool IsEmail(string email)
        {
            email = email.Trim();
            const string regexString =
               @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            // const string regexString =
            //    @"^\\w+((-\\w+)|(\\.\\w+))*\\@[A-Za-z0-9]+((\\.|-)[A-Za-z0-9]+)*\\.[A-Za-z0-9]+$";
            var regex = new Regex(regexString);
            return regex.IsMatch(email);
        }

        public static bool CheckEmail(string email)
        {
            bool result = true;
            if (email.Trim().Length == 0)
            {
                // 先按可以为空
                result = true;
            }
            else
            {
                Regex Regex = new Regex("[\\w-]+@([\\w-]+\\.)+[\\w-]+");
                if (!Regex.IsMatch(email))
                {
                    result = false;
                }
            }
            return result;
        }

        /// <summary>
        /// 检手机号码是否何法
        /// </summary>
        /// <param name="mobile">手机号码</param>
        /// <returns>格式正确</returns>
        public static bool IsMobile(string mobile)
        {
            bool result = false;

            // 2015-12-12 吉日嘎拉 手机号码是空的，认为不准确就可以了
            if (string.IsNullOrEmpty(mobile))
            {
                return result;
            }

            mobile = mobile.Trim();
            const string regexString = @"^(1(([34578][0-9])|(47)|[8][01236789]))\d{8}$";
            var regex = new Regex(regexString);
            result = regex.IsMatch(mobile);

            return result;
        }

        /// <summary>
        /// 是否身份证号码
        /// </summary>
        /// <param name="idCard"></param>
        /// <returns></returns>
        public static bool IsIDCard(string idCard)
        {
            idCard = idCard.Trim();
            if (idCard.Length == 15 || idCard.Length == 18)
            {
                return true;
            }
            return false;
            // const string regexString = @"[\d]{6}(19|20)*[\d]{2}((0[1-9])|(11|12))*[\d]{2}((0[1-9])|^2[\d]{1}([0-9])|(30|31))*[\d]{3}[xX]|[\d]{4}";
            // const string regexString = @"^(^[1-9]\d{7}((0\d)|(1[0-2]))(([0|1|2]\d)|3[0-1])\d{3}$)|(^[1-9]\d{5}[1-9]\d{3}((0\d)|(1[0-2]))(([0|1|2]\d)|3[0-1])((\d{4})|\d{3}[Xx])$)$";
            // var regex = new Regex(regexString);
            // return regex.IsMatch(idCard);
        }

        /// <summary>
        /// 是否英文或者数字
        /// </summary>
        /// <param name="userName">文字或者数字</param>
        /// <returns></returns>
        public static bool IsUserName(string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                userName = string.Empty;
            }
            const string regexString = @"^[0-9a-zA-Z._@]{3,30}$";
            var regex = new Regex(regexString);
            return regex.IsMatch(userName);
        }

        /// <summary>
        /// 是否英文或者数字
        /// </summary>
        /// <param name="letterOrIsDigit">文字或者数字</param>
        /// <returns></returns>
        public static bool IsLetterOrIsDigit(string letterOrIsDigit)
        {
            if (string.IsNullOrEmpty(letterOrIsDigit))
            {
                letterOrIsDigit = string.Empty;
            }
            const string regexString = @"^[0-9a-zA-Z]{3,30}$";
            var regex = new Regex(regexString);
            return regex.IsMatch(letterOrIsDigit);
        }

        /// <summary>
        /// 是否电话号码
        /// </summary>
        /// <param name="telephone">电话号码</param>
        /// <returns></returns>
        public static bool IsTelephone(string telephone)
        {
            bool result = true;
            if (string.IsNullOrEmpty(telephone))
            {
                for (int i = 0; i < telephone.Length; i++)
                {
                    if (telephone[i].Equals("-")
                        || telephone[i].Equals("0")
                        || telephone[i].Equals("1")
                        || telephone[i].Equals("2")
                        || telephone[i].Equals("3")
                        || telephone[i].Equals("4")
                        || telephone[i].Equals("5")
                        || telephone[i].Equals("6")
                        || telephone[i].Equals("7")
                        || telephone[i].Equals("8")
                        || telephone[i].Equals("9"))
                    {
                        result = true;
                    }
                    else
                    {
                        result = false;
                        break;
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 是否汉子姓名
        /// </summary>
        /// <param name="realName">姓名</param>
        /// <returns></returns>
        public static bool IsChineseCharacters(string realName)
        {
            if (string.IsNullOrEmpty(realName))
            {
                return false;
            }
            realName = realName.Trim();
            const string regexString = @"[\u4e00-\u9fa5]{2,}";
            var regex = new Regex(regexString);
            return regex.IsMatch(realName);
        }

        /// <summary>
        /// 文件夹名检查
        /// </summary>
        /// <param name="folderName">文件夹名</param>
        /// <returns>检查通过</returns>
        public static bool CheckFolderName(string folderName)
        {
            bool result = true;
            if (folderName.Trim().Length == 0)
            {
                result = false;
            }
            else
            {
                if ((folderName.IndexOfAny(Path.GetInvalidPathChars()) >= 0) || (folderName.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0))
                {
                    result = false;
                }
            }
            return result;
        }

        /// <summary>
        /// 密码强度检查
        /// </summary>
        /// <param name="password">密码</param>
        /// <returns>检查通过</returns>
        public static bool CheckPasswordStrength(string password, string userName = null)
        {
            bool result = true;
            if (string.IsNullOrEmpty(password))
            {
                result = false;
            }
            if (password.IndexOf("123") > -1)
            {
                result = false;
            }
            bool isDigit = false;
            bool isLetter = false;
            for (int i = 0; i < password.Length; i++)
            {
                if (!isDigit)
                {
                    isDigit = char.IsDigit(password[i]);
                }
                if (!isLetter)
                {
                    isLetter = char.IsLetter(password[i]);
                }
            }
            result = (isDigit && isLetter);
            // 密码至少为8位，为数字加字母
            if (password.Length < 8)
            {
                result = false;
            }
            return result;
        }

        /// <summary>
        /// 文件名检查
        /// </summary>
        /// <param name="folderName">文件名</param>
        /// <returns>检查通过</returns>
        public static bool CheckFileName(string fileName)
        {
            bool result = true;
            if (fileName.Trim().Length == 0)
            {
                result = false;
            }
            else
            {
                if (fileName.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
                {
                    result = false;
                }
            }
            return result;
        }
    }
}