//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd.  
//-----------------------------------------------------------------

using System;

namespace DotNet.Business
{
    using DotNet.Model;
    using DotNet.Utilities;

    /// <summary>
    /// BaseUserManager
    /// 用户管理
    /// 
    /// 修改记录
    /// 
    ///		2012.04.12 版本：1.0 JiRiGaLa	主键整理。
    /// 
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2012.04.12</date>
    /// </author> 
    /// </summary>
    public partial class BaseUserManager : BaseManager
    {
        /// <summary>
        /// 向管理员发送登录提醒邮件
        /// </summary>
        /// <param name="result"></param>
        /// <param name="returnUserInfo"></param>
        /// <param name="StatusCode"></param>
        public static void SendLoginMailToAdministrator(BaseUserInfo userInfo, string systemCode = "Base")
        {
            //如果是系统管理员登录则发送EMAIL  
            if (userInfo != null && userInfo.IsAdministrator)
            {
                // 登录成功发送
                BaseUserContactEntity userContactEntity = new BaseUserContactManager().GetObject(userInfo.Id);
                if (userContactEntity != null)
                {
                    string emailAddress = userContactEntity.Email;
                    if (string.IsNullOrEmpty(emailAddress))
                    {
                        emailAddress = BaseSystemInfo.ErrorReportTo;
                    }
                    if (!string.IsNullOrEmpty(emailAddress))
                    {
                        // 没有邮箱则给管理员发邮件
                        // 使用线程发送邮件
                        string subject = "超级管理员 " + userInfo.CompanyName + " - " + userInfo.UserName + " 登录" + BaseSystemInfo.SoftFullName + " 系统提醒";
                        string body = userInfo.UserName + System.Environment.NewLine + ":<br/>"
                            + DateTime.Now.ToString(BaseSystemInfo.DateTimeFormat) + "登录了" + BaseSystemInfo.SoftFullName + "；<br/>" + System.Environment.NewLine
                            + "编号：" + userInfo.Code + "；<br/> " + System.Environment.NewLine
                            + "登录系统：" + systemCode + "；<br/> " + System.Environment.NewLine
                            + "登录IP：" + userInfo.IPAddress + "；<br/> " + System.Environment.NewLine
                            + "MAC地址：" + userInfo.MACAddress + "；<br/>" + System.Environment.NewLine
                            + "如果不是您自己登录，请马上联系中天系统管理员电话021-3116 5566-9582，QQ:707055073，请即刻修改密码。";
                        MailUtil.SendByThread(emailAddress, subject, body);
                    }
                }
            }
        }
    }
}