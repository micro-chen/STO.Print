//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd.  
//-----------------------------------------------------------------

using System;
using System.Linq;
using System.Collections.Generic;

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
    ///     2012.10.12 版本：1.0 JiRiGaLa    优化消息提醒功能开关。
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
        /// 发送即时通讯提醒
        /// </summary>
        /// <param name="workFlowCurrentEntity">当前审核流实体信息</param>
        /// <param name="auditStatus">审核状态</param>
        /// <param name="auditIdea">审核意见</param>
        /// <param name="userIds">发送给用户主键</param>
        /// <param name="roleIds">发送给角色主键</param>
        /// <returns>影响行数</returns>
        public virtual int SendMessage(BaseWorkFlowCurrentEntity workFlowCurrentEntity, AuditStatus auditStatus, string[] userIds, string organizeId, string roleId)
        {
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
            // string currentId, string objectId, string objectFullName
            int result = 0;
            // 不用给自己发消息了，消息多了也烦恼
            userIds = StringUtil.Remove(userIds, this.UserInfo.Id);
            // BaseUserEntity userEntity = userManager.GetObject(userId);
            // 发送请求审核的信息
            BaseMessageEntity messageEntity = new BaseMessageEntity();
			//messageEntity.Id = BaseBusinessLogic.NewGuid();
            // 这里是回调的类，用反射要回调的
            messageEntity.FunctionCode = MessageFunction.Remind.ToString();
            // messageEntity.FunctionCode = this.GetType().ToString();
            messageEntity.ObjectId = workFlowCurrentEntity.ObjectId;
            // 这里是网页上的显示地址
            // messageEntity.Title = this.GetUrl(id);
            string auditIdea = string.Empty;
            if (!string.IsNullOrEmpty(workFlowCurrentEntity.AuditIdea))
            {
                auditIdea = " 批示: " + workFlowCurrentEntity.AuditIdea;
            }
            // messageEntity.Contents = userEntity.DepartmentName + " " + userEntity.RealName
            if (!BaseSystemInfo.SimpleReminders)
            {
                messageEntity.Contents
                    = workFlowCurrentEntity.CreateBy + " 发出审批申请： " + "<a title='点击这里，直接查看单据' target='_blank' href='" + this.GetUrl(workFlowCurrentEntity.Id) + "'>" + workFlowCurrentEntity.ObjectFullName + "</a> "
                    + Environment.NewLine
                    + this.UserInfo.RealName + " " + BaseBusinessLogic.GetAuditStatus(auditStatus) + " "
                    + Environment.NewLine
                    + auditIdea;
            }
            else
            {
                messageEntity.Contents = "有单据" + BaseBusinessLogic.GetAuditStatus(auditStatus);
            }
            messageEntity.IsNew = 1;
            messageEntity.ReadCount = 0;
            messageEntity.DeletionStateCode = 0;
            BaseMessageManager messageManager = new BaseMessageManager(this.UserInfo);
			messageManager.Identity = true;
            result = messageManager.BatchSend(userIds, organizeId, roleId, messageEntity, false);
            return result;
        }
    }
}