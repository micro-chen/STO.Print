using System;
using System.Globalization ;
using System.Text;
using System.Text.RegularExpressions;

namespace STO.Print.Utilities
{
	/// <summary>
	/// CString 的摘要说明。
	/// </summary>
	public class CString
	{
		public CString() { }

        #region 常规字符串操作
        // 检查字符串是否为空
        public static bool IsEmpty(string str)
        {
            if (str == null || str == "")
                return true;
            else
                return false;
        }
        //检查字符串中是否包含非法字符
		public static bool CheckValidity(string s)
		{
			string str = s;
			if(str.IndexOf("'") >0 || str.IndexOf("&") >0 || str.IndexOf("%") >0 || str.IndexOf("+") >0 || str.IndexOf("\"") >0 || str.IndexOf("=") >0 || str.IndexOf("!") >0)
			{
				return false;
			}
			else
			{
				return true;
			}
		}

		/// <summary>
		/// 把价格精确至小数点两位
		/// </summary>
		/// <param name="str"></param>
		/// <returns></returns>
		public static string TransformPrice(double  dPrice)
		{		
			double  d = dPrice;
			NumberFormatInfo myNfi = new NumberFormatInfo();
			myNfi.NumberNegativePattern = 2;
			string s = d.ToString( "N", myNfi );
			return s;
		}

		public static string TransToStr(float f,int iNum)
		{		
			float fl = f;
			NumberFormatInfo myNfi = new NumberFormatInfo();
			myNfi.NumberNegativePattern = iNum ;
			string s = f.ToString( "N", myNfi );
			return s;
		}

        /// <summary> 
        /// 检测含有中文字符串的实际长度 
        /// </summary> 
        /// <param name="str">字符串</param> 
        public static int GetLength(string str)
        {
            System.Text.ASCIIEncoding n = new System.Text.ASCIIEncoding();
            byte[] b = n.GetBytes(str);
            int l = 0; // l 为字符串之实际长度 
            for (int i = 0; i <= b.Length - 1; i++)
            {
                if (b[i] == 63) //判断是否为汉字或全脚符号 
                {
                    l++;
                }
                l++;
            }
            return l;

        }

        //截取长度,num是英文字母的总数，一个中文算两个英文
        public static string GetLetter(string str, int iNum, bool bAddDot)
        {
            if (str == null || iNum <= 0) return "";

            if (str.Length < iNum && str.Length * 2 < iNum)
            {
                return str;
            }

            string sContent = str;
            int iTmp = iNum;

            char[] arrC;
            if (sContent.Length >= iTmp) //防止因为中文的原因使ToCharArray溢出
            {
                arrC = str.ToCharArray(0, iTmp);
            }
            else
            {
                arrC = str.ToCharArray(0, sContent.Length);
            }

            int i = 0;
            int iLength = 0;
            foreach (char ch in arrC)
            {
                iLength++;
                
                int k = (int)ch;
                if (k > 127 || k < 0)
                {
                    i += 2;
                }
                else
                {
                    i++;
                }

                if (i > iTmp)
                {
                    iLength--;
                    break;
                }
                else if (i == iTmp)
                {
                    break;
                }
            }

            if (iLength < str.Length && bAddDot)
                sContent = sContent.Substring(0, iLength - 3) + "...";
            else
                sContent = sContent.Substring(0, iLength);

            return sContent;
        }

        public static string GetDateString(DateTime dt)
        {
            return dt.Year.ToString()+dt.Month.ToString().PadLeft(2,'0')+dt.Day.ToString().PadLeft(2,'0');
        }
        
        //根据指定字符，截取相应字符串
        public static string GetStrByLast(string sOrg, string sLast)
        {
            int iLast = sOrg.LastIndexOf(sLast);
            if (iLast > 0)
                return sOrg.Substring(iLast+1);
            else
                return sOrg;
        }
        public static string GetPreStrByLast(string sOrg, string sLast)
        {
            int iLast = sOrg.LastIndexOf(sLast);
            if (iLast > 0)
                return sOrg.Substring(0, iLast);
            else
                return sOrg;
        }
        public static string RemoveEndWith(string sOrg, string sEnd)
        {
            if (sOrg.EndsWith(sEnd))
                sOrg = sOrg.Remove(sOrg.IndexOf(sEnd), sEnd.Length);
            return sOrg;
        }

        #endregion  常规字符串操作

        #region HTML相关操作
        public static string ClearTag(string sHtml)
        {
            if (sHtml == "")
                return "";
            string sTemp = sHtml;
            Regex re = new Regex(@"(<[^>\s]*\b(\w)+\b[^>]*>)|(<>)|(&nbsp;)|(&gt;)|(&lt;)|(&amp;)|\r|\n|\t", RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.IgnorePatternWhitespace);
            return   re.Replace(sHtml, ""); 
        }
        public static string ClearTag(string sHtml, string  sRegex)
        { 
            string sTemp = sHtml;
            Regex re = new Regex(sRegex, RegexOptions.IgnoreCase|RegexOptions.Multiline|RegexOptions.IgnorePatternWhitespace );
            return re.Replace(sHtml, "");            
        }
        public static string ConvertToJS(string sHtml)
        { 
            StringBuilder sText = new StringBuilder();
            Regex re;
            re = new Regex(@"\r\n",  RegexOptions.IgnoreCase);
            string[] strArray = re.Split(sHtml);
            foreach (string strLine in strArray)
            {
                sText.Append("document.writeln(\"" + strLine.Replace("\"","\\\"")+"\");\r\n");
            } 
            return sText.ToString();
        }
        public static string ReplaceNbsp(string str)
        {
            string sContent = str; 
            if (sContent.Length > 0)
            {
                sContent = sContent.Replace(" ", "");
                sContent = sContent.Replace("&nbsp;", "");
                sContent = "&nbsp;&nbsp;&nbsp;&nbsp;" + sContent;
            }
            return sContent;
        }
        public static string StringToHtml(string str)
        {
            string sContent = str; 
            if (sContent.Length > 0)
            {
                char csCr = (char)13; 
                sContent = sContent.Replace(csCr.ToString(), "<br>"); 
                sContent = sContent.Replace(" ", "&nbsp;");
                sContent = sContent.Replace("　", "&nbsp;&nbsp;");
            }
            return sContent;
        }

        //截取长度并转换为HTML
        public static string AcquireAssignString(string str, int num)
        {
            string sContent = str;
            sContent = GetLetter(sContent, num, false);
            sContent = StringToHtml(sContent);
            return sContent;
        }

        //此方法与AcquireAssignString的功能已经一样，为了不报错，故保留此方法
        public static string TranslateToHtmlString(string str, int num)
        {
            string sContent = str;
            sContent = GetLetter(sContent, num, false);
            sContent = StringToHtml(sContent);
            return sContent;
        }

        public static string AddBlankAtForefront(string str)
        {
            string sContent = str;
            return sContent;
        }

        /// <summary>
        /// 删除所有的html标记 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string DelHtmlString(string str)
        {
            string[] Regexs =
                                {
                                    @"<script[^>]*?>.*?</script>",
                                    @"<(\/\s*)?!?((\w+:)?\w+)(\w+(\s*=?\s*(([""'])(\\[""'tbnr]|[^\7])*?\7|\w+)|.{0})|\s)*?(\/\s*)?>",
                                    @"([\r\n])[\s]+",
                                    @"&(quot|#34);",
                                    @"&(amp|#38);",
                                    @"&(lt|#60);",
                                    @"&(gt|#62);",
                                    @"&(nbsp|#160);",
                                    @"&(iexcl|#161);",
                                    @"&(cent|#162);",
                                    @"&(pound|#163);",
                                    @"&(copy|#169);",
                                    @"&#(\d+);",
                                    @"-->",
                                    @"<!--.*\n"
                                };

            string[] Replaces =
                                {
                                    "",
                                    "",
                                    "",
                                    "\"",
                                    "&",
                                    "<",
                                    ">",
                                    " ",
                                    "\xa1", //chr(161),
                                    "\xa2", //chr(162),
                                    "\xa3", //chr(163),
                                    "\xa9", //chr(169),
                                    "",
                                    "\r\n",
                                    ""
                                };

            string s = str;
            for (int i = 0; i < Regexs.Length; i++)
            {
                s = new Regex(Regexs[i], RegexOptions.Multiline | RegexOptions.IgnoreCase).Replace(s, Replaces[i]);
            }
            s.Replace("<", "");
            s.Replace(">", "");
            s.Replace("\r\n", "");
            return s;
        }

        /// <summary>
        /// 删除字符串中的特定标记 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="tag"></param>
        /// <param name="isContent">是否清除内容 </param>
        /// <returns></returns>
        public static string DelTag(string str, string tag, bool isContent)
        {
            if (tag == null || tag == " ")
            {
                return str;
            }

            if (isContent) //要求清除内容 
            {
                return Regex.Replace(str, string.Format("<({0})[^>]*>([\\s\\S]*?)<\\/\\1>", tag), "", RegexOptions.IgnoreCase);
            }

            return Regex.Replace(str, string.Format(@"(<{0}[^>]*(>)?)|(</{0}[^>] *>)|", tag), "", RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// 删除字符串中的一组标记 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="tagA"></param>
        /// <param name="isContent">是否清除内容 </param>
        /// <returns></returns>
        public static string DelTagArray(string str, string tagA, bool isContent)
        {

            string[] tagAa = tagA.Split(',');

            foreach (string sr1 in tagAa) //遍历所有标记，删除 
            {
                str = DelTag(str, sr1, isContent);
            }
            return str;

        }

        #endregion HTML相关操作

        #region 其他字符串操作

        /// <summary>
        /// 格式化为版本号字符串
        /// </summary>
        /// <param name="sVersion"></param>
        /// <returns></returns>
		public static string SetVersionFormat(string sVersion)
		{
			if(sVersion==null || sVersion=="") return "";
			int n=0, k=0;
			
			string stmVersion = "";
			while(n<4 && k>-1)
			{
				k = sVersion.IndexOf(".",k+1);
				n++;
			}
			if(k>0)
			{
				stmVersion = sVersion.Substring(0,k);
			}
			else 
			{
				stmVersion = sVersion;
			}

			return stmVersion;
		}

        /// <summary>
        /// 格式化字符串为 SQL 语句字段
        /// </summary>
        /// <param name="fldList"></param>
        /// <returns></returns>
        public static string GetSQLFildList(string fldList)
        {
            if(fldList == null )
                return "*";
            if (fldList.Trim() == "")
                return "*";           
            if(fldList.Trim() =="*")
                return "*";
            //先去掉空格，[]符号
            string strTemp = fldList;
            strTemp = strTemp.Replace(" ", "");
            strTemp = strTemp.Replace("[", "").Replace("]", "");           
            //为防止使用保留字，给所有字段加上[]
            strTemp = "[" + strTemp + "]";
            strTemp = strTemp.Replace('，', ',');
            strTemp = strTemp.Replace(",","],[");
            return strTemp;
        }

        public static  string GetTimeDelay(DateTime dtStar, DateTime dtEnd)
        {
            long lTicks = (dtEnd.Ticks - dtStar.Ticks)/10000000;
            string sTemp =  (lTicks/3600).ToString().PadLeft(2, '0')+":";
            sTemp +=  ((lTicks%3600)/60).ToString().PadLeft(2, '0') + ":";
            sTemp += ((lTicks % 3600)%60).ToString().PadLeft(2, '0');
            return sTemp;
        }

        /// <summary>
        /// 在前面补0
        /// </summary>
        /// <returns></returns>
        public static string AddZero(int sheep,int length)
        {
            return AddZero(sheep.ToString(), length);
        }

        /// <summary>
        /// 在前面补0
        /// </summary>
        /// <returns></returns>
        public static string AddZero(string sheep, int length)
        {
            StringBuilder goat = new StringBuilder(sheep);
            for (int i = goat.Length; i < length; i++)
            {
                goat.Insert(0, "0");
            }

            return goat.ToString();
        }

        /// <summary>
        /// 简介：获得唯一的字符串
        /// </summary>
        /// <returns></returns>
        public static string GetUniqueString()
        {
            Random rand = new Random();
            return ((int)(rand.NextDouble() * 10000)).ToString() + DateTime.Now.Ticks.ToString();
        }

        //获得干净,无非法字符的字符串
        public static string GetCleanJsString(string str)
        {
            str = str.Replace("\"","“");
            str = str.Replace("'", "”");
            str = str.Replace("\\","\\\\");
            Regex re = new Regex(@"\r|\n|\t", RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.IgnorePatternWhitespace);
            str =  re.Replace(str, " ");

            return str;
        }
        //获得干净,无非法字符的字符串
        public static string GetCleanJsString2(string str)
        {
            str = str.Replace("\"", "\\\"");
            //str = str.Replace("'", "\\\'");
            //str = str.Replace("\\", "\\\\");
            Regex re = new Regex(@"\r|\n|\t", RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.IgnorePatternWhitespace);
            str = re.Replace(str, " ");

            return str;
        }
        #endregion 其他字符串操作

        /// <summary>
        /// 取得所有链接URL
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static string GetAllURL(string html)
        {
            StringBuilder sb = new StringBuilder();
            Match m = Regex.Match(html.ToLower(), "<a href=(.*?)>.*?</a>");

            while (m.Success)
            {
                sb.AppendLine(m.Result("$1"));
                m.NextMatch();
            }

            return sb.ToString();
        }

        /// <summary>
        /// 获取所有连接文本
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static string GetAllLinkText(string html)
        {
            StringBuilder sb = new StringBuilder();
            Match m = Regex.Match(html.ToLower(), "<a href=.*?>(1,100})</a>");

            while (m.Success)
            {
                sb.AppendLine(m.Result("$1"));
                m.NextMatch();
            }

            return sb.ToString();
        }
    }
}