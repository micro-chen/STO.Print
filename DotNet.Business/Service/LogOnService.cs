//-----------------------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd.  
//-----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;

namespace DotNet.Business
{
    using DotNet.IService;
    using DotNet.Model;
    using DotNet.Utilities;

    /// <summary>
    /// LogOnService
    /// 
    /// 修改记录
    /// 
    ///		2016.02.14 版本：2.0 JiRiGaLa 增加访问日志记录功能。
    ///		2015.12.09 版本：1.1 JiRiGaLa 增加修改密码设置密码的日志记录。
    ///		2013.06.06 版本：1.0 张祈璟   重构。
    ///		2009.04.15 版本：1.0 JiRiGaLa 添加接口定义。
    ///		
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2016.02.14</date>
    /// </author> 
    /// </summary>
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    [System.ServiceModel.Activation.AspNetCompatibilityRequirements(RequirementsMode = System.ServiceModel.Activation.AspNetCompatibilityRequirementsMode.Allowed)]
    public partial class LogOnService : ILogOnService
    {
        /// <summary>
        /// 获取系统版本号
        /// <param name="taskId">任务标识</param>
        /// </summary>
        /// <returns>版本号</returns>
        public string GetServerVersion(string taskId)
        {
            string result = string.Empty;

            Assembly assembly = Assembly.GetExecutingAssembly();
            FileVersionInfo fileVersionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
            result = fileVersionInfo.FileVersion;

            return result;
        }

        /// <summary>
        /// 获取服务器时间
        /// <param name="taskId">任务标识</param>
        /// </summary>
        /// <returns>当前时间</returns>
        public DateTime GetServerDateTime(string taskId)
        {
            return System.DateTime.Now;
        }

        /// <summary>
        /// 获取数据库服务器时间
        /// <param name="taskId">任务标识</param>
        /// </summary>
        /// <returns>数据库时间</returns>
        public DateTime GetDbDateTime(string taskId)
        {
            DateTime result = System.DateTime.Now;

            using (IDbHelper dbHelper = DbHelperFactory.GetHelper(BaseSystemInfo.UserCenterDbType, BaseSystemInfo.UserCenterDbConnection))
            {
                result = DateTime.Parse(dbHelper.GetDbDateTime());
            }

            return result;
        }

        public string GetRemoteIP()
        {
            string result = string.Empty;

            // 提供方法执行的上下文环境
            OperationContext context = OperationContext.Current;
            if (context != null)
            {
                // 获取传进的消息属性
                MessageProperties properties = context.IncomingMessageProperties;
                // 获取消息发送的远程终结点IP和端口
                RemoteEndpointMessageProperty endpoint = properties[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
                result = endpoint.Address;
            }
            if (result == "::1")
            {
                result = "127.0.0.1";
            }

            return result;
        }

        #region public UserLogOnResult UserLogOn(string taskId, BaseUserInfo userInfo, string userName, string password, string openId, bool createOpenId)
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="taskId">任务标识</param>
        /// <param name="userInfo">用户</param>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="openId">单点登录标识</param>
        /// <param name="createOpenId">重新创建单点登录标识</param>
        /// <param name="ipAddress">IP地址</param>
        /// <returns>登录实体类</returns>
        public UserLogOnResult UserLogOn(string taskId, BaseUserInfo userInfo, string userName, string password, string openId, bool createOpenId)
        {
            UserLogOnResult result = new UserLogOnResult();

            var parameter = ServiceInfo.Create(taskId, MethodBase.GetCurrentMethod());
            ServiceUtil.ProcessUserCenterWriteDb(userInfo, parameter, (dbHelper) =>
            {
                var userManager = new BaseUserManager(userInfo);
                userManager.CheckIsAdministrator = true;
                result = userManager.LogOnByUserName(userName, password, openId, createOpenId);
                // 2016-02-16 吉日嘎拉 记录用户日志用
                parameter.UserInfo = result.UserInfo;
            });

            return result;
        }
        #endregion

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="taskId">任务标识</param>
        /// <param name="userInfo">用户</param>
        /// <param name="companyName">单位名称</param>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="openId">单点登录标识</param>
        /// <param name="createOpenId">重新创建单点登录标识</param>
        /// <param name="ipAddress">IP地址</param>
        /// <returns>登录实体类</returns>
        public UserLogOnResult LogOnByCompany(string taskId, BaseUserInfo userInfo, string companyName, string userName, string password, string openId, bool createOpenId)
        {
            UserLogOnResult result = new UserLogOnResult();

            var parameter = ServiceInfo.Create(taskId, userInfo, MethodBase.GetCurrentMethod());
            ServiceUtil.ProcessUserCenterWriteDb(userInfo, parameter, (dbHelper) =>
            {
                // 先侦测是否在线
                // userLogOnManager.CheckOnLine();
                // 再进行登录
                var userManager = new BaseUserManager(userInfo);
                userManager.CheckIsAdministrator = true;
                result = userManager.LogOnByCompany(companyName, userName, password, openId, createOpenId, userInfo.SystemCode, GetRemoteIP());
                // 张祈璟20130619添加
                //if (returnUserInfo != null)
                //{
                //    returnUserInfo.CloneData(userInfo);
                //    result.UserInfo = returnUserInfo;
                //}
                // 登录时会自动记录进行日志记录，所以不需要进行重复日志记录
                // BaseLogManager.Instance.Add(result, this.serviceName, MethodBase.GetCurrentMethod());
            });

            return result;
        }

        #region public UserLogOnResult LogOnByNickName(string taskId, BaseUserInfo userInfo, string nickName, string password, string openId, bool createOpenId)
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="taskId">任务标识</param>
        /// <param name="userInfo">用户</param>
        /// <param name="nickName">昵称</param>
        /// <param name="password">密码</param>
        /// <param name="openId">单点登录标识</param>
        /// <param name="createOpenId">重新创建单点登录标识</param>
        /// <param name="ipAddress">IP地址</param>
        /// <returns>登录实体类</returns>
        public UserLogOnResult LogOnByNickName(string taskId, BaseUserInfo userInfo, string nickName, string password, string openId, bool createOpenId)
        {
            UserLogOnResult result = new UserLogOnResult();

            var parameter = ServiceInfo.Create(taskId, userInfo, MethodBase.GetCurrentMethod());
            ServiceUtil.ProcessUserCenterWriteDb(userInfo, parameter, (dbHelper) =>
            {
                // 先侦测是否在线
                // userLogOnManager.CheckOnLine();
                // 再进行登录
                var userManager = new BaseUserManager(userInfo);
                userManager.CheckIsAdministrator = true;
                result = userManager.LogOnByNickName(nickName, password, openId, createOpenId);
            });

            return result;
        }
        #endregion

        #region public UserLogOnResult LogOnByOpenId(string taskId, BaseUserInfo userInfo, string openId)
        /// <summary>
        /// 按唯一识别码登录
        /// </summary>
        /// <param name="taskId">任务标识</param>
        /// <param name="userInfo">用户</param>
        /// <param name="openId">唯一识别码</param>
        /// <returns>用户实体</returns>
        public UserLogOnResult LogOnByOpenId(string taskId, BaseUserInfo userInfo, string openId)
        {
            UserLogOnResult result = new UserLogOnResult();

            var parameter = ServiceInfo.Create(taskId, userInfo, MethodBase.GetCurrentMethod());
            ServiceUtil.ProcessUserCenterWriteDb(userInfo, parameter, (dbHelper) =>
            {
                // 先侦测是否在线
                BaseUserLogOnManager userLogOnManager = new BaseUserLogOnManager();
                userLogOnManager.CheckOnLine();
                // 若是单点登录，那就不能判断ip地址，因为不是直接登录，是间接登录
                var userManager = new BaseUserManager(userInfo);
                result = userManager.LogOnByOpenId(openId, string.Empty, string.Empty);
            });

            return result;
        }
        #endregion


        /// <summary>
        /// 获取新的OpenId
        /// </summary>
        /// <param name="taskId">任务标识</param>
        /// <param name="userInfo">用户</param>
        /// <returns>OpenId</returns>
        public string CreateOpenId(string taskId, BaseUserInfo userInfo)
        {
            string result = string.Empty;

            var parameter = ServiceInfo.Create(taskId, userInfo, MethodBase.GetCurrentMethod());
            ServiceUtil.ProcessUserCenterWriteDb(userInfo, parameter, (dbHelper) =>
            {
                BaseUserLogOnManager userLogOnManager = new BaseUserLogOnManager(userInfo);
                result = userLogOnManager.CreateOpenId();
            });

            return result;
        }

        #region public int ServerCheckOnLine(string taskId)
        /// <summary>
        /// 服务器端检查在线状态
        /// </summary>
        /// <param name="taskId">任务标识</param>
        /// <returns>离线人数</returns>
        public int ServerCheckOnLine(string taskId)
        {
            int result = 0;

            using (IDbHelper dbHelper = DbHelperFactory.GetHelper(BaseSystemInfo.UserCenterDbType))
            {
                try
                {
                    dbHelper.Open(BaseSystemInfo.UserCenterWriteDbConnection);
                    var userLogOnManager = new BaseUserLogOnManager(dbHelper);
                    result = userLogOnManager.CheckOnLine();
                }
                catch (Exception ex)
                {
                    LogUtil.WriteException(ex);
                    throw;
                }
                finally
                {
                    dbHelper.Close();
                }
            }

            return result;
        }
        #endregion

        #region public void OnLine(string taskId, BaseUserInfo userInfo, int onLineState = 1)
        /// <summary>
        /// 用户现在
        /// </summary>
        /// <param name="taskId">任务标识</param>
        /// <param name="userInfo">用户</param>
        /// <param name="onLineState">用户在线状态</param>
        public void OnLine(string taskId, BaseUserInfo userInfo, int onLineState = 1)
        {
            var parameter = ServiceInfo.Create(taskId, userInfo, MethodBase.GetCurrentMethod());
            ServiceUtil.ProcessUserCenterWriteDb(userInfo, parameter, (dbHelper) =>
            {
                BaseUserLogOnManager userLogOnManager = new BaseUserLogOnManager();
                userLogOnManager.OnLine(userInfo.Id, onLineState);
            });
        }
        #endregion

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="taskId">任务标识</param>
        /// <param name="userInfo">用户</param>
        /// <param name="id">主键</param>
        /// <returns>实体</returns>
        public BaseUserLogOnEntity GetObject(string taskId, BaseUserInfo userInfo, string id)
        {
            BaseUserLogOnEntity result = null;

            var parameter = ServiceInfo.Create(taskId, userInfo, MethodBase.GetCurrentMethod());
            ServiceUtil.ProcessUserCenterReadDb(userInfo, parameter, (dbHelper) =>
            {
                var userManager = new BaseUserManager(dbHelper, userInfo);
                // 判断是否已经登录的用户？
                if (userManager.UserIsLogOn(userInfo))
                {
                    BaseUserLogOnManager userLogOnManager = new BaseUserLogOnManager();
                    result = userLogOnManager.GetObject(id);
                }
            });

            return result;
        }

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="taskId">任务标识</param>
        /// <param name="userInfo">用户</param>
        /// <param name="entity">实体</param>
        /// <returns>影响行数</returns>
        public int Update(string taskId, BaseUserInfo userInfo, BaseUserLogOnEntity entity)
        {
            int result = 0;

            var parameter = ServiceInfo.Create(taskId, userInfo, MethodBase.GetCurrentMethod());
            ServiceUtil.ProcessUserCenterWriteDb(userInfo, parameter, (dbHelper) =>
            {
                // 调用方法，并且返回运行结果
                BaseUserLogOnManager userLogOnManager = new BaseUserLogOnManager();
                result = userLogOnManager.Update(entity);
            });

            return result;
        }

        #region public BaseUserInfo AccountActivation(string taskId, BaseUserInfo userInfo, string openId)
        /// <summary>
        /// 激活用户
        /// </summary>
        /// <param name="taskId">任务标识</param>
        /// <param name="userInfo">用户</param>
        /// <param name="openId">唯一识别码</param>
        /// <returns>用户实体</returns>
        public BaseUserInfo AccountActivation(string taskId, BaseUserInfo userInfo, string openId)
        {
            BaseUserInfo result = null;

            var parameter = ServiceInfo.Create(taskId, userInfo, MethodBase.GetCurrentMethod());
            ServiceUtil.ProcessUserCenterWriteDb(userInfo, parameter, (dbHelper) =>
            {
                var userLogOnManager = new BaseUserLogOnManager(dbHelper, userInfo);
                // 先侦测是否在线
                userLogOnManager.CheckOnLine();
                // 再进行登录
                var userManager = new BaseUserManager(dbHelper, userInfo);
                result = userManager.AccountActivation(openId);
            });
            
            return result;
        }
        #endregion

        #region public int SetPassword(string taskId, BaseUserInfo userInfo, string[] userIds, string password)
        /// <summary>
        /// 设置用户密码
        /// </summary>
        /// <param name="taskId">任务标识</param>
        /// <param name="userInfo">用户</param>
        /// <param name="userId">被设置的员工主键</param>
        /// <param name="newPassword">新密码</param>
        /// <returns>影响行数</returns>
        public int SetPassword(string taskId, BaseUserInfo userInfo, string[] userIds, string newPassword)
        {
            int result = 0;

            var parameter = ServiceInfo.Create(taskId, userInfo, MethodBase.GetCurrentMethod());
            ServiceUtil.ProcessUserCenterWriteDb(userInfo, parameter, (dbHelper) =>
            {
                var userManager = new BaseUserManager(dbHelper, userInfo);
                result = userManager.BatchSetPassword(userIds, newPassword, true, true);
            });

            return result;
        }
        #endregion

        #region public UserLogOnResult ChangePassword(string taskId, BaseUserInfo userInfo, string oldPassword, string newPassword)
        /// <summary>
        /// 用户修改密码
        /// </summary>
        /// <param name="taskId">任务标识</param>
        /// <param name="userInfo">用户</param>
        /// <param name="oldPassword">原始密码</param>
        /// <param name="newPassword">新密码</param>
        /// <returns>影响行数</returns>
        public UserLogOnResult ChangePassword(string taskId, BaseUserInfo userInfo, string oldPassword, string newPassword)
        {
            UserLogOnResult result = null;

            var parameter = ServiceInfo.Create(taskId, userInfo, MethodBase.GetCurrentMethod());
            ServiceUtil.ProcessUserCenterWriteDb(userInfo, parameter, (dbHelper) =>
            {
                // 事务开始
                // dbHelper.BeginTransaction();
                var userManager = new BaseUserManager(dbHelper, userInfo);
                result = new UserLogOnResult();
                result.UserInfo = userManager.ChangePassword(userInfo.Id, oldPassword, newPassword);

                // 获取登录后信息
                // result.Message = BaseParameterManager.GetParameterByCache("BaseNotice", "System", "LogOn", "Message");
                // 获得状态消息
                result.StatusCode = userManager.StatusCode;
                result.StatusMessage = userManager.GetStateMessage();
                // 事务提交
                // dbHelper.CommitTransaction();
            });

            return result;
        }
        #endregion

        #region public static bool UserIsLogOn(string taskId, BaseUserInfo userInfo)
        /// <summary>
        /// 用户是否已经登录了系统？
        /// </summary>
        /// <param name="taskId">任务标识</param>
        /// <param name="userInfo">用户</param>
        /// <returns>是否登录</returns>
        public static bool UserIsLogOn(string taskId, BaseUserInfo userInfo)
        {
            // 加强安全验证防止未授权匿名调用
            if (!BaseSystemInfo.IsAuthorized(userInfo))
            {
                throw new Exception(AppMessage.MSG0800);
            }
            // 这里表示是没登录过的用户
            // if (string.IsNullOrEmpty(result.OpenId))
            // {
            //    throw new Exception(AppMessage.MSG0900);            
            // }
            // 确认用户是否登录了？是否进行了匿名的破坏工作
            /*
            IDbHelper dbHelper = DbHelperFactory.GetHelper(BaseSystemInfo.UserCenterDbConnection);
            var userManager = new BaseUserManager(dbHelper, result);
            if (!userManager.UserIsLogOn(result))
            {
                throw new Exception(AppMessage.MSG0900);            
            }
            */
            return true;
        }
        #endregion

        #region public bool LockUser(string taskId, BaseUserInfo userInfo, string userName)
        /// <summary>
        /// 锁定用户
        /// </summary>
        /// <param name="taskId">任务标识</param>
        /// <param name="userInfo">用户</param>
        /// <param name="userName">用户名</param>
        /// <returns>是否成功锁定</returns>
        public bool LockUser(string taskId, BaseUserInfo userInfo, string userName)
        {
            bool result = false;

            var parameter = ServiceInfo.Create(taskId, userInfo, MethodBase.GetCurrentMethod());
            ServiceUtil.ProcessUserCenterWriteDb(userInfo, parameter, (dbHelper) =>
            {
                // BaseLogManager.Instance.Add(result, this.serviceName, AppMessage.LogOnService_LockUser, MethodBase.GetCurrentMethod());
                var userManager = new BaseUserManager(userInfo);
                List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
                parameters.Add(new KeyValuePair<string, object>(BaseUserEntity.FieldUserName, userName));
                parameters.Add(new KeyValuePair<string, object>(BaseUserEntity.FieldEnabled, 1));
                parameters.Add(new KeyValuePair<string, object>(BaseUserEntity.FieldDeletionStateCode, 0));
                BaseUserEntity userEntity = BaseEntity.Create<BaseUserEntity>(userManager.GetDataTable(parameters));
                // 判断是否为空的
                if (userEntity != null && !string.IsNullOrEmpty(userEntity.Id))
                {
                    // 被锁定15分钟，不允许15分钟内登录，这时间是按服务器的时间来的。
                    var userLogOnManager = new BaseUserLogOnManager();
                    BaseUserLogOnEntity userLogOnEntity = userLogOnManager.GetObject(userEntity.Id);
                    userLogOnEntity.LockStartDate = DateTime.Now;
                    userLogOnEntity.LockEndDate = DateTime.Now.AddMinutes(BaseSystemInfo.PasswordErrorLockCycle);
                    result = userLogOnManager.UpdateObject(userLogOnEntity) > 0;
                }
            });

            return result;
        }
        #endregion

        #region public DataTable GetUserDT(string taskId, BaseUserInfo userInfo)
        /// <summary>
        /// 获得用户列表
        /// </summary>
        /// <param name="taskId">任务标识</param>
        /// <param name="userInfo">用户</param>
        /// <returns>数据表</returns>
        public DataTable GetUserDT(string taskId, BaseUserInfo userInfo)
        {
            var result = new DataTable(BaseUserEntity.TableName);

            var parameter = ServiceInfo.Create(taskId, userInfo, MethodBase.GetCurrentMethod());
            ServiceUtil.ProcessUserCenterReadDb(userInfo, parameter, (dbHelper) =>
            {
                // 检查用户在线状态(服务器专用)
                BaseUserLogOnManager userLogOnManager = new BaseUserLogOnManager();
                userLogOnManager.CheckOnLine();
                var userManager = new BaseUserManager(dbHelper, userInfo);
                // 获取允许登录列表
                List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
                parameters.Add(new KeyValuePair<string, object>(BaseUserEntity.FieldEnabled, 1));
                parameters.Add(new KeyValuePair<string, object>(BaseUserEntity.FieldDeletionStateCode, 0));
                result = userManager.GetDataTable(parameters, BaseUserEntity.FieldSortCode);
                result.TableName = BaseUserEntity.TableName;
            });

            return result;
        }
        #endregion

        #region public bool ValidateVerificationCode(string taskId, BaseUserInfo userInfo, string code)
        /// <summary>
        /// 判断证码是否正确
        /// </summary>
        /// <param name="taskId">任务标识</param>
        /// <param name="userInfo">用户</param>
        /// <param name="code">验证码</param>
        /// <returns>正确</returns>
        public bool ValidateVerificationCode(string taskId, BaseUserInfo userInfo, string code)
        {
            bool result = false;

            BaseUserLogOnManager userLogOnManager = new BaseUserLogOnManager();
            string verificationCode = userLogOnManager.GetProperty(userInfo.Id, BaseUserLogOnEntity.FieldVerificationCode);
            if (!string.IsNullOrEmpty(verificationCode))
            {
                result = verificationCode.Equals(code);
            }

            return result;
        }
        #endregion

        /// <summary>
        /// 忘记密码按电子邮件获取
        /// </summary>
        /// <param name="taskId">任务标识</param>
        /// <param name="userInfo">用户信息</param>
        /// <param name="userName">用户名</param>
        /// <param name="email">电子邮件</param>
        /// <returns>成功</returns>
        public bool GetPasswordByEmail(string taskId, BaseUserInfo userInfo, string userName, string email)
        {
            bool result = false;

            BaseUserContactManager manager = new BaseUserContactManager();
            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            if (!string.IsNullOrEmpty(email))
            {
                parameters.Add(new KeyValuePair<string, object>(BaseUserContactEntity.FieldEmail, email));
            }
            string id = manager.GetId(parameters);
            if (!string.IsNullOrEmpty(id))
            {
                BaseUserManager userManager = new BaseUserManager();
                bool userNameOK = true;
                BaseUserEntity userEntity = userManager.GetObject(id);
                if (!string.IsNullOrEmpty(userName))
                {
                    if (!string.IsNullOrEmpty(userEntity.UserName) && !userEntity.UserName.Equals(userName))
                    {
                        userNameOK = false;
                        userInfo = null;
                    }
                }
                if (userNameOK)
                {
                    userInfo = userManager.ConvertToUserInfo(userEntity);
                }
            }
            if (!string.IsNullOrEmpty(id))
            {
                string userPassword = string.Empty;
                if (BaseSystemInfo.CheckPasswordStrength)
                {
                    userPassword = BaseRandom.GetRandomString(8).ToLower();
                }
                else
                {
                    userPassword = BaseRandom.GetRandomString(8).ToLower();
                    // Random random = new System.Random();
                    // userPassword = random.Next(100000, 999999).ToString();
                }

                // 邮件内容       
                SmtpClient smtpClient = new SmtpClient(BaseSystemInfo.MailServer);
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(BaseSystemInfo.MailUserName, BaseSystemInfo.MailPassword);
                // 指定如何处理待发的邮件
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;

                string mailTitle = BaseSystemInfo.SoftFullName + "忘记密码";

                string mailBody = "您的新密码为：" + userPassword + " " + System.Environment.NewLine
                    + "<br/> " + System.Environment.NewLine + BaseSystemInfo.SoftFullName + "访问地址： http://www.zto.cn/";
                // 读取模板文件
                string file = BaseSystemInfo.StartupPath + "\\Forgot.Mail.txt";
                if (System.IO.File.Exists(file))
                {
                    mailBody = System.IO.File.ReadAllText(file, Encoding.UTF8);
                    mailBody = mailBody.Replace("{Realname}", userInfo.RealName);
                    mailBody = mailBody.Replace("{UserPassword}", userPassword);
                }
                // 发送邮件
                MailMessage mailMessage = new MailMessage(BaseSystemInfo.MailUserName, email, mailTitle, mailBody);
                mailMessage.BodyEncoding = Encoding.Default;
                mailMessage.IsBodyHtml = true;
                smtpClient.Send(mailMessage);

                BaseUserManager userManager = new BaseUserManager(userInfo);
                userManager.SetPassword(userInfo.Id, userPassword);
                userManager.GetStateMessage();
                if (userManager.StatusCode == Status.SetPasswordOK.ToString())
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
            }

            return result;
        }

        #region public DataTable GetDataTableByPage(string taskId, BaseUserInfo userInfo, out int recordCount, int pageIndex, int pageSize, string whereClause, List<KeyValuePair<string, object>> dbParameters, string order = null)
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="taskId">任务标识</param>
        /// <param name="userInfo">用户</param>
        /// <param name="recordCount">记录数</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页显示</param>
        /// <param name="whereClause">条件</param>
        /// <param name="dbParameters">参数</param>
        /// <param name="order">排序</param>
        /// <returns>数据表</returns>
        public DataTable GetDataTableByPage(string taskId, BaseUserInfo userInfo, out int recordCount, int pageIndex, int pageSize, string whereClause, List<KeyValuePair<string, object>> dbParameters, string order = null)
        {
            var result = new DataTable(BaseLoginLogEntity.TableName);
            int myRecordCount = 0;

            var parameter = ServiceInfo.Create(taskId, userInfo, MethodBase.GetCurrentMethod());
            // 这里需要连接到登录日志数据库服务器
            ServiceUtil.ProcessLoginLogDb(userInfo, parameter, (dbHelper) =>
            {
                if (SecretUtil.IsSqlSafe(whereClause))
                {
                    var loginLogManager = new BaseLoginLogManager(dbHelper, userInfo);
                    result = loginLogManager.GetDataTableByPage(out myRecordCount, pageIndex, pageSize, whereClause, dbHelper.MakeParameters(dbParameters), order);
                    result.TableName = BaseLoginLogEntity.TableName;
                }
                else
                {
                    if (System.Web.HttpContext.Current != null)
                    {
                        // 记录注入日志
                        FileUtil.WriteMessage("userInfo:" + userInfo.Serialize() + " " + whereClause, System.Web.HttpContext.Current.Server.MapPath("~/Log/") + "SqlSafe" + DateTime.Now.ToString(BaseSystemInfo.DateFormat) + ".txt");
                    }
                }
            });
            recordCount = myRecordCount;

            return result;
        }
        #endregion

        #region public void SignOut(string taskId, BaseUserInfo userInfo)
        /// <summary>
        /// 用户离线(退出)
        /// </summary>
        /// <param name="taskId">任务标识</param>
        /// <param name="userInfo">用户</param>
        public void SignOut(string taskId, BaseUserInfo userInfo)
        {
            var parameter = ServiceInfo.Create(taskId, userInfo, MethodBase.GetCurrentMethod());
            ServiceUtil.ProcessUserCenterWriteDb(userInfo, parameter, (dbHelper) =>
            {
                // 2015-12-14 吉日嘎拉 用户的登录日志不用重复写日志
                BaseUserLogOnManager userLogOnManager = new BaseUserLogOnManager();
                userLogOnManager.SignOut(userInfo.OpenId);
            });
        }
        #endregion
    }
}