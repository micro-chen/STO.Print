//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd.  
//-----------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace DotNet.Business
{
    using DotNet.Utilities;
    using DotNet.Model;
    using DotNet.IService;

    /// <remarks>
    ///	BaseUserBillManager
    ///	标准单据的审批流程接口
    ///	
    ///	修改记录
    /// 
    ///     2012.12.29 版本：1.0 JiRiGaLa    发送邮件的接口。
    ///		
    /// </remarks>
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2012.12.29</date>
    /// </author> 
    /// </remarks>
    public partial class BaseUserBillManager : BaseWorkFlowBillTemplateManager, IWorkFlowManager
    {
        /// <summary>
        /// 发送邮件提醒
        /// </summary>
        /// <param name="workFlowCurrentEntity">当前审核流实体信息</param>
        /// <param name="auditStatus">审核状态</param>
        /// <param name="auditIdea">审核意见</param>
        /// <param name="userIds">发送给用户主键</param>
        /// <param name="roleIds">发送给角色主键</param>
        /// <returns>影响行数</returns>
        public virtual int SendMail(BaseWorkFlowCurrentEntity workFlowCurrentEntity, AuditStatus auditStatus, string[] userIds, string organizeId, string roleId)
        {
            int result = 0;

            // 这里是检查邮件服务器是否设置了，若没设置就没必要发送邮件了
            if (string.IsNullOrEmpty(BaseSystemInfo.MailServer)
                || string.IsNullOrEmpty(BaseSystemInfo.MailUserName)
                || string.IsNullOrEmpty(BaseSystemInfo.MailPassword))
            {
                return result;
            }

            // 这里是考虑了，同时发给多个人的情况
            List<string> userList = new List<string>();
            foreach (var userid in userIds)
            {
                if (!string.IsNullOrEmpty(userid))
                {
                    if (userid.IndexOf(',') > 0)
                    {
                        string[] users = userid.Split(',').Distinct<string>().Where(t => !string.IsNullOrEmpty(t)).ToArray();
                        foreach (var id in users)
                        {
                            if (!string.IsNullOrEmpty(id))
                            {
                                userList.Add(id);
                            }
                        }
                    }
                    else
                    {
                        userList.Add(userid);
                    }
                }
            }
            userIds = userList.ToArray();
            // 不用给自己发消息了，消息多了也烦恼
            userIds = StringUtil.Remove(userIds, this.UserInfo.Id);

            string mailTitle = workFlowCurrentEntity.ActivityFullName + " " + workFlowCurrentEntity.ObjectFullName + " " + workFlowCurrentEntity.AuditStatusName;
            // 邮件内容       
            SmtpClient smtpClient = new SmtpClient(BaseSystemInfo.MailServer);
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential(BaseSystemInfo.MailUserName, BaseSystemInfo.MailPassword);
            // 指定如何处理待发的邮件
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;

            string mailBody = string.Empty;
            string auditIdea = string.Empty;
            if (!string.IsNullOrEmpty(workFlowCurrentEntity.AuditIdea))
            {
                auditIdea = " 批示: " + workFlowCurrentEntity.AuditIdea;
            }
            if (BaseSystemInfo.SimpleReminders)
            {
                mailBody = "有单据" + BaseBusinessLogic.GetAuditStatus(auditStatus);
            }
            else
            {
                mailBody = workFlowCurrentEntity.CreateBy + " 发出审批申请： " + "<a title='点击这里，直接查看单据' target='_blank' href='" + this.GetUrl(workFlowCurrentEntity.Id) + "'>" + workFlowCurrentEntity.ObjectFullName + "</a> "
                    + Environment.NewLine
                    + this.UserInfo.RealName + " " + BaseBusinessLogic.GetAuditStatus(auditStatus) + " "
                    + Environment.NewLine
                    + auditIdea;
            }

            for (int i = 0; i < userIds.Length; i++)
            {
                string mailTo = BaseUserContactManager.GetEmailByCache(userIds[i]);
                if (!string.IsNullOrEmpty(mailTo))
                {
                    try
                    {
                        MailMessage mailMessage = new MailMessage(BaseSystemInfo.MailUserName, mailTo, mailTitle, mailBody);
                        mailMessage.BodyEncoding = Encoding.Default;
                        mailMessage.IsBodyHtml = true;
                        smtpClient.Send(mailMessage);
                        result++;
                    }
                    catch
                    {
                    }
                }
            }

            return result;
        }
    }
}