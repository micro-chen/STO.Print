//-----------------------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2010 , Hairihan TECH, Ltd.  
//-----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Reflection;
using System.ServiceModel;
using System.Text;

namespace DotNet.Business
{
    using DotNet.Utilities;

    /// <summary>
    /// LogOnService
    /// 
    /// 修改纪录
    /// 
    /// 	2014.02.13 崔文远增加(LDAP专用)
    ///		2013.06.06 张祈璟重构
    ///		2009.04.15 版本：1.0 JiRiGaLa 添加接口定义。
    ///		
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2009.04.15</date>
    /// </author> 
    /// </summary>
    public partial class LogOnService : ILogOnService
    {

        #region public BaseUserInfo LogOnByUserName(BaseUserInfo userInfo, string userName, out string statusCode, out string statusMessage)
        /// <summary>
        /// 按用户名登录(LDAP专用)
        /// </summary>
        /// <param name="userInfo">用户</param>
        /// <param name="userName">用户名</param>
        /// <param name="statusCode">返回状态码</param>
        /// <param name="statusMessage">返回状消息</param>
        /// <returns>用户实体</returns>
        public BaseUserInfo LogOnByUserName(BaseUserInfo userInfo, string userName, out string statusCode, out string statusMessage)
        {
            var parameter = ServiceParameter.CreateWithLog(userInfo
                , MethodBase.GetCurrentMethod());
            BaseUserInfo returnUserInfo = null;
            string returnCode = string.Empty;
            string returnMessage = string.Empty;
            ServiceUtil.ProcessUserCenterWriteDb(userInfo,parameter, (dbHelper) =>
            {
                // 先侦测是否在线
                userLogOnManager.CheckOnLine();
                // 然后获取用户密码
                var userManager = new BaseUserManager(userInfo);
                // 是否从角色判断管理员
                userManager.CheckIsAdministrator = true;
                BaseUserEntity userEntity = userManager.GetByUserName(userName);
                BaseUserLogOnEntity userLogOnEntity = userLogOnManager.GetObject(userEntity.Id);
                string password = userLogOnEntity.UserPassword;
                // 再进行登录
                returnUserInfo = userManager.LogOnByUserName(userName,password,null,false,userInfo.IPAddress,userInfo.MACAddress,false);
                returnCode = userManager.StatusCode;
                returnMessage = userManager.GetStateMessage();
                // 登录时会自动记录进行日志记录，所以不需要进行重复日志记录
                // BaseLogManager.Instance.Add(userInfo, this.serviceName, MethodBase.GetCurrentMethod());
            });
            statusCode = returnCode;
            statusMessage = returnMessage;
            return returnUserInfo;
        }
        #endregion

    }
}