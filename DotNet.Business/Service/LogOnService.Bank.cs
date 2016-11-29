//-----------------------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2010 , Hairihan TECH, Ltd.  
//-----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.ServiceModel;

namespace DotNet.Business
{
    using DotNet.Utilities;

    /// <summary>
    /// LogOnService
    /// 
    /// 修改纪录
    /// 
	///		2013.09.03 版本：1.0 JiRiGaLa 分离接口定义。
    ///		
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2013.09.03</date>
    /// </author> 
    /// </summary>
    public partial class LogOnService : System.MarshalByRefObject, ILogOnService
    {
        #region public int ChangeCommunicationPassword(BaseUserInfo userInfo, string oldPassword, string newPassword, out string statusCode, out string statusMessage)
        /// <summary>
        /// 用户修改通讯密码
        /// </summary>
        /// <param name="userInfo">用户</param>
        /// <param name="oldPassword">原始密码</param>
        /// <param name="newPassword">新密码</param>
        /// <param name="statusCode">返回状态码</param>
        /// <param name="statusMessage">返回状消息</param>
        /// <returns>影响行数</returns>
        public int ChangeCommunicationPassword(BaseUserInfo userInfo, string oldPassword, string newPassword, out string statusCode, out string statusMessage)
        {
            var parameter = ServiceParameter.CreateWithLog(userInfo
                , MethodBase.GetCurrentMethod());
            int result = 0;
            string returnCode = string.Empty;
            string returnMessage = string.Empty;
            ServiceUtil.ProcessUserCenterWriteDb(parameter, (dbHelper) =>
            {
                // 事务开始
                // dbHelper.BeginTransaction();
                var userManager = new BaseUserLogOnManager(dbHelper, userInfo);
                result = userManager.ChangeCommunicationPassword(oldPassword, newPassword, out returnCode);
                // 获得状态消息
                returnMessage = userManager.GetStateMessage(returnCode);
                // 事务提交
                // dbHelper.CommitTransaction();
                BaseLogManager.Instance.Add(userInfo, this.serviceName, AppMessage.LogOnService_ChangeCommunicationPassword, MethodBase.GetCurrentMethod());
            });
            statusCode = returnCode;
            statusMessage = returnMessage;
            return result;
        }
        #endregion

        #region public bool CommunicationPassword(BaseUserInfo userInfo, string communicationPassword)
        /// <summary>
        /// 验证用户通讯密码
        /// </summary>
        /// <param name="userInfo">用户</param>
        /// <param name="communicationPassword">通讯密码</param>
        /// <param name="ipAddress">IP地址</param>
        /// <param name="statusCode">返回状态码</param>
        /// <param name="statusMessage">返回状消息</param>
        /// <returns>是否正确</returns>
        public bool CommunicationPassword(BaseUserInfo userInfo, string communicationPassword)
        {
            var parameter = ServiceParameter.CreateWithMessage(userInfo
                , MethodBase.GetCurrentMethod()
                , this.serviceName
                , AppMessage.LogOnService_CommunicationPassword);
            bool result = false;
            ServiceUtil.ProcessUserCenterReadDb(parameter, (dbHelper) =>
            {
                var userManager = new BaseUserLogOnManager(dbHelper, userInfo);
                result = userManager.CommunicationPassword(communicationPassword);
            });
            return result;
        }
        #endregion

        #region public int SetCommunicationPassword(BaseUserInfo userInfo, string[] userIds, string password, out string statusCode, out string statusMessage)
        /// <summary>
        /// 设置用户通讯密码
        /// </summary>
        /// <param name="userInfo">用户</param>
        /// <param name="userId">被设置的员工主键</param>
        /// <param name="password">新密码</param>
        /// <param name="statusCode">返回状态码</param>
        /// <param name="statusMessage">返回状消息</param>
        /// <returns>影响行数</returns>
        public int SetCommunicationPassword(BaseUserInfo userInfo, string[] userIds, string password, out string statusCode, out string statusMessage)
        {
            var parameter = ServiceParameter.CreateWithLog(userInfo
                , MethodBase.GetCurrentMethod());
            int result = 0;
            string returnCode = string.Empty;
            string returnMessage = string.Empty;
            ServiceUtil.ProcessUserCenterWriteDb(parameter, (dbHelper) =>
            {
                var userManager = new BaseUserLogOnManager(dbHelper, userInfo);
                result = userManager.BatchSetCommunicationPassword(userIds, password, out returnCode);
                // 获得状态消息
                returnMessage = userManager.GetStateMessage(returnCode);
                BaseLogManager.Instance.Add(userInfo, this.serviceName, AppMessage.LogOnService_SetPassword, MethodBase.GetCurrentMethod());
            });
            statusCode = returnCode;
            statusMessage = returnMessage;
            return result;
        }
        #endregion

        #region public string CreateDigitalSignature(BaseUserInfo userInfo, string password, out string statusCode, out string statusMessage)
        /// <summary>
        /// 创建数字证书签名
        /// </summary>
        /// <param name="userInfo">用户</param>
        /// <param name="password">密码</param>
        /// <param name="statusCode">返回状态码</param>
        /// <param name="statusMessage">返回状消息</param>
        /// <returns>私钥</returns>
        public string CreateDigitalSignature(BaseUserInfo userInfo, string password, out string statusCode, out string statusMessage)
        {
            var parameter = ServiceParameter.CreateWithMessage(userInfo
                , MethodBase.GetCurrentMethod()
                , this.serviceName
                , AppMessage.LogOnService_CreateDigitalSignature);
            string result = string.Empty;
            string returnCode = string.Empty;
            string returnMessage = string.Empty;
            ServiceUtil.ProcessUserCenterWriteDb(parameter, (dbHelper) =>
            {
                var userManager = new BaseUserLogOnManager(dbHelper, userInfo);
                result = userManager.CreateDigitalSignature(password, out returnCode);
                // 获得状态消息
                returnMessage = userManager.GetStateMessage(returnCode);
            });
            statusCode = returnCode;
            statusMessage = returnMessage;
            return result;
        }
        #endregion

        #region public string GetPublicKey(BaseUserInfo userInfo, string userId)
        /// <summary>
        /// 获取当前用户的公钥
        /// </summary>
        /// <param name="userId">用户主键</param>
        /// <returns>公钥</returns>
        public string GetPublicKey(BaseUserInfo userInfo, string userId)
        {
            var parameter = ServiceParameter.CreateWithMessage(userInfo
                , MethodBase.GetCurrentMethod()
                , this.serviceName
                , AppMessage.LogOnService_GetPublicKey);
            string result = string.Empty;
            ServiceUtil.ProcessUserCenterReadDb(parameter, (dbHelper) =>
            {
                var userManager = new BaseUserLogOnManager(dbHelper, userInfo);
                result = userManager.GetPublicKey(userId);
            });
            return result;
        }
        #endregion

        #region public int ChangeSignedPasswordd(BaseUserInfo userInfo, string oldPassword, string newPassword, out string statusCode, out string statusMessage)
        /// <summary>
        /// 用户修改签名密码
        /// </summary>
        /// <param name="userInfo">用户</param>
        /// <param name="oldPassword">原始密码</param>
        /// <param name="newPassword">新密码</param>
        /// <param name="statusCode">返回状态码</param>
        /// <param name="statusMessage">返回状消息</param>
        /// <returns>影响行数</returns>
        public int ChangeSignedPassword(BaseUserInfo userInfo, string oldPassword, string newPassword, out string statusCode, out string statusMessage)
        {
            var parameter = ServiceParameter.CreateWithMessage(userInfo
                , MethodBase.GetCurrentMethod()
                , this.serviceName
                , AppMessage.LogOnService_ChangeSignedPassword);
            int result = 0;
            string returnCode = string.Empty;
            string returnMessage = string.Empty;
            ServiceUtil.ProcessUserCenterWriteDb(parameter, (dbHelper) =>
            {
                // 事务开始
                // dbHelper.BeginTransaction();
                var userManager = new BaseUserLogOnManager(dbHelper, userInfo);
                result = userManager.ChangeSignedPassword(oldPassword, newPassword, out returnCode);
                // 获得状态消息
                returnMessage = userManager.GetStateMessage(returnCode);
                // 事务提交
                // dbHelper.CommitTransaction();
            });
            statusCode = returnCode;
            statusMessage = returnMessage;
            return result;
        }
        #endregion

        #region public bool SignedPassword(BaseUserInfo userInfo, string signedPassword)
        /// <summary>
        /// 验证用户数字签名密码
        /// </summary>
        /// <param name="userInfo">用户</param>
        /// <param name="signedPassword">验证数字签名密码</param>
        /// <param name="ipAddress">IP地址</param>
        /// <param name="statusCode">返回状态码</param>
        /// <param name="statusMessage">返回状消息</param>
        /// <returns>是否正确</returns>
        public bool SignedPassword(BaseUserInfo userInfo, string signedPassword)
        {
            var parameter = ServiceParameter.CreateWithMessage(userInfo
                , MethodBase.GetCurrentMethod()
                , this.serviceName
                , AppMessage.LogOnService_SignedPassword);
            bool result = false;
            ServiceUtil.ProcessUserCenterReadDb(parameter, (dbHelper) =>
            {
                var userManager = new BaseUserLogOnManager(dbHelper, userInfo);
                result = userManager.SignedPassword(signedPassword);
            });
            return result;
        }
        #endregion
    }
}