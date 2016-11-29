//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2015 , Hairihan TECH, Ltd. 
//-----------------------------------------------------------------

using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;

namespace DotNet.Business
{
    using DotNet.Utilities;

    /// <summary>
    /// BaseMessageManager（程序OK）
    /// 消息表
    ///
    /// 修改记录
    ///     
    ///     2009.03.16 版本：2.1 JiRiGaLa 已发信息查询功能整理。
    ///     2009.02.20 版本：2.0 JiRiGaLa 主键分类，表结构进行改进，主键重新整理。
    ///     2008.04.15 版本：1.0 JiRiGaLa 创建主键。
    ///
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2008.04.15</date>
    /// </author>
    /// </summary>
    public partial class BaseMessageManager : BaseManager
    {
        private static bool Send(string url, out string returnMsg)
        {
            string str = "";
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Timeout = 0x1388;
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(stream, Encoding.Default))
                    {
                        StringBuilder builder = new StringBuilder();
                        while (-1 != reader.Peek())
                        {
                            builder.Append(reader.ReadLine());
                        }
                        str = builder.ToString();
                        if (str.Contains("1,"))
                        {
                            str = "1";
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                returnMsg = exception.Message;
                return false;
            }
            bool flag = false;

            switch (str)
            {
                case "1":
                case "Success":
                    flag = true;
                    returnMsg = "提交短信成功";
                    return flag;
                case "0":
                    returnMsg = "失败";
                    return flag;
                case "-1":
                    returnMsg = "用户名或者密码不正确";
                    return flag;
                case "2":
                    returnMsg = "余额不够";
                    return flag;

                case "3":
                    returnMsg = "黑词审核中";
                    return flag;

                case "4":
                    returnMsg = "出现异常，人工处理中";
                    return flag;

                case "5":
                    returnMsg = "提交频率太快";
                    return flag;

                case "6":
                    returnMsg = "有效号码为空";
                    return flag;

                case "7":
                    returnMsg = "短信内容为空";
                    return flag;

                case "8":
                    returnMsg = "一级黑词";
                    return flag;

                case "9":
                    returnMsg = "没有url提交权限";
                    return flag;

                case "10":
                    returnMsg = "发送号码过多";
                    return flag;

                case "11":
                    returnMsg = "产品ID异常";
                    return flag;
            }
            returnMsg = "未知错误";
            return flag;
        }

        public bool Send(string[] phoneNumbers, string message, out string returnMsg)
        {
            if (((phoneNumbers == null) || (phoneNumbers.Length == 0)) || (string.IsNullOrEmpty(message) || (message.Trim().Length == 0)))
            {
                returnMsg = "手机号码和消息内容不能为空";
                return false;
            }
            StringBuilder builder = new StringBuilder();
            builder.Append("http://mes.sh-hstx.com:8800/sendXSms.do?");
            builder.Append("productid=").Append("695857");
            builder.Append("&username=").Append("jirigala");
            builder.Append("&password=").Append("110119");
            builder.Append("&mobile=").Append(string.Join(",", phoneNumbers));
            builder.Append("&content=");
            builder.Append(System.Web.HttpUtility.UrlEncode(message, Encoding.UTF8));
            builder.Append("&dstime=");
            return Send(builder.ToString(), out returnMsg);
        }

        public bool Send(string cellPhone, string message, out string returnMsg)
        {
            if ((string.IsNullOrEmpty(cellPhone) || string.IsNullOrEmpty(message)) || ((cellPhone.Trim().Length == 0) || (message.Trim().Length == 0)))
            {
                returnMsg = "手机号码和消息内容不能为空";
                return false;
            }
            StringBuilder builder = new StringBuilder();
            builder.Append("http://mes.sh-hstx.com:8800/sendXSms.do?");
            builder.Append("productid=").Append("695857");
            builder.Append("&username=").Append("jirigala");
            builder.Append("&password=").Append("110119");
            builder.Append("&mobile=").Append(cellPhone);
            builder.Append("&content=");
            builder.Append(System.Web.HttpUtility.UrlEncode(message, Encoding.UTF8));
            builder.Append("&dstime=");
            return Send(builder.ToString(), out returnMsg);
        }
    }
}