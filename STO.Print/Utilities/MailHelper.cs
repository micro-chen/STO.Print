//-------------------------------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2013 , DZD , Ltd .
//-------------------------------------------------------------------------------------

using System;
using System.Net;
using System.Net.Mail;
using System.Windows.Forms;

namespace STO.Print.Utilities
{
    using DevExpress.XtraEditors;
    using DotNet.Utilities;

    /// <summary>
    /// 邮件处理类
    ///
    /// 修改纪录
    ///
    ///		2013-10-28 版本：1.0 YangHengLian 创建主键。
    ///		2014-01-23 版本：1.0 YangHengLian修改异常处理，抛出异常给调用方，本身不处理异常信息
    ///
    /// 版本：1.0
    ///
    /// <author>
    ///		<name>YangHengLian</name>
    ///		<date>2013-10-28</date>
    /// </author>
    /// </summary>
    public class MailHelper
    {
        #region SendMail(string fromUserEmailAccount, string fromUserEmailPassword, string fromUserDisplayName, string[] toUserEmailAccounts, string title, string message, string[] attachmentNames = null, string host = "smtp.qq.com", int port = 25) 群发邮件
        /// <summary>
        ///  群发邮件
        /// </summary>
        /// <param name="fromUserEmailAccount">发件人邮箱地址</param>
        /// <param name="fromUserEmailPassword">发件人密码</param>
        /// <param name="fromUserDisplayName">发件人显示昵称</param>
        /// <param name="toUserEmailAccounts">收件人数组集合</param>
        /// <param name="title">标题</param>
        /// <param name="message">正文</param>
        /// <param name="attachmentNames">附件集合</param>
        /// <param name="host">smpt地址，这里默认是qq邮箱</param>
        /// <param name="port">端口号，默认是qq邮箱端口号：25</param>
        /// <returns>true/false</returns>
        public static bool SendMail(string fromUserEmailAccount, string fromUserEmailPassword, string fromUserDisplayName, string[] toUserEmailAccounts, string title, string message, string[] attachmentNames = null, string host = "smtp.qq.com", int port = 25)
        {
            try
            {
                SmtpClient smtp = new SmtpClient();
                smtp.Host = host;//邮箱的smtp地址
                smtp.Port = port;//端口号
                smtp.Credentials = new NetworkCredential(fromUserEmailAccount, fromUserEmailPassword);//构建发件人的身份凭据类
                MailMessage objMailMessage = new MailMessage();//构建消息类
                objMailMessage.Priority = MailPriority.High;//设置优先级
                objMailMessage.From = new MailAddress(fromUserEmailAccount, fromUserDisplayName, System.Text.Encoding.UTF8);//消息发送人
                if (toUserEmailAccounts.Length > 0)
                    foreach (var item in toUserEmailAccounts)
                    {
                        objMailMessage.To.Add(item);//收件人集合
                    }
                //标题
                objMailMessage.Subject = title.Trim();//标题
                objMailMessage.SubjectEncoding = System.Text.Encoding.UTF8;//字符编码
                //正文
                objMailMessage.Body = message.Trim();//正文
                objMailMessage.IsBodyHtml = true;
                objMailMessage.BodyEncoding = System.Text.Encoding.UTF8;
                if (attachmentNames != null)//添加附件
                    if (attachmentNames.Length > 0)
                        foreach (var item in attachmentNames)
                        {
                            objMailMessage.Attachments.Add(new Attachment(item));
                        }
                smtp.Send(objMailMessage);//发送
                return true;
            }
            catch (Exception ex)
            {
               // XtraMessageBox.Show(ex.Message + Environment.NewLine + ex.StackTrace, AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        #endregion
    }
}
