//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd.  
//-----------------------------------------------------------------

using System;
using System.Linq;
using System.Collections.Generic;
using System.Data;

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
    ///		2016.01.28 版本：3.5 JiRiGaLa 按唯一用户名登录时，可以通过缓存登录的功能实现。
    ///		2015.07.13 版本：3.3 JiRiGaLa 登录日志加上日志、日志无法正常写入也需要系统能正常登录。
    ///		2015.04.27 版本：3.2 JiRiGaLa 支持多系统登录。
    ///		2014.08.05 版本：3.1 JiRiGaLa 登录优化。
    ///		2014.03.25 版本：3.0 JiRiGaLa CheckIsAdministrator 不是所有系统都需要验证是否超级管理员，去掉效率会更高，特别是针对登录接口可以优化了。
    ///		2013.10.20 版本：2.0 JiRiGaLa 集成K8物流系统的登录功能。
    ///		2011.10.17 版本：1.0 JiRiGaLa 主键整理。
    /// 
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2016.01.28</date>
    /// </author> 
    /// </summary>
    public partial class BaseUserManager : BaseManager
    {
        /// <summary>
        /// 是否从角色里判断是否超级管理员
        /// </summary>
        public bool CheckIsAdministrator = false;

        /// <summary>
        /// 是否更新登录信息
        /// </summary>
        public bool IsLogOnByOpenId = false;

        /// <summary>
        /// 当前子系统是否加密了用户密码
        /// </summary>
        public bool SystemEncryptPassword = true;

        /// <summary>
        /// 获取用户的权限
        /// </summary>
        public bool GetUserPermission = true;

        /// <summary>
        /// 登录验证的表对应的表
        /// </summary>
        public string UserLogOnTable = "BaseUserLogOn";

        /// <summary>
        /// 对应的缓存服务区分
        /// </summary>
        public string CachingSystemCode = "";

        #region public UserLogOnResult LogOnByNickName(string nickName, string password, string openId = null, bool createOpenId = false, string ipAddress = null, string macAddress = null, bool checkUserPassword = true) 进行登录操作
        /// <summary>
        /// 进行登录操作
        /// </summary>
        /// <param name="nickName">昵称</param>
        /// <param name="password">密码</param>
        /// <param name="openId">单点登录标识</param>
        /// <param name="createOpenId">重新创建单点登录标识</param>
        /// <param name="ipAddress">IP地址</param>
        /// <param name="macAddress">MAC地址</param>
        /// <param name="checkUserPassword">是否要检查用户密码</param>
        /// <returns>用户信息</returns>
        public UserLogOnResult LogOnByNickName(string nickName, string password, string openId = null, bool createOpenId = false, string systemCode = null, string ipAddress = null, string macAddress = null, string computerName = null, bool checkUserPassword = true, bool getOpenIdOnly = false, bool checkMacAddress = true)
        {
            UserLogOnResult result = new UserLogOnResult();
            int errorMark = 0;

            string ipAddressName = string.Empty;

            try
            {
                if (BaseSystemInfo.OnInternet && !string.IsNullOrEmpty(ipAddress))
                {
                    errorMark = 1;
                    ipAddressName = IpHelper.GetInstance().FindName(ipAddress);
                }

                // 大多人还是看小写比较敏感
                if (!string.IsNullOrWhiteSpace(nickName))
                {
                    nickName = nickName.ToLower();
                }
                nickName = this.DbHelper.SqlSafe(nickName);

                if (UserInfo != null)
                {
                    if (string.IsNullOrEmpty(ipAddress))
                    {
                        ipAddress = UserInfo.IPAddress;
                    }
                    // 得到MAC地址，否则会导致后面的MAC地址判断无效 赵秉杰 2012-09-02
                    if (string.IsNullOrEmpty(macAddress))
                    {
                        macAddress = UserInfo.MACAddress;
                    }
                }

                // 04. 默认为用户没有找到状态，查找用户
                // 这是为了达到安全要求，不能提示用户未找到，那容易让别人猜测到帐户
                if (BaseSystemInfo.CheckPasswordStrength)
                {
                    result.StatusCode = Status.ErrorLogOn.ToString();
                }
                else
                {
                    result.StatusCode = Status.UserNotFound.ToString();
                }
                result.StatusMessage = this.GetStateMessage(result.StatusCode);

                string sqlQuery = string.Empty;
                List<IDbDataParameter> dbParameters = new List<IDbDataParameter>();
                sqlQuery = "SELECT * "
                          + " FROM " + BaseUserEntity.TableName
                         + " WHERE " + BaseUserEntity.FieldNickName + " = " + DbHelper.GetParameter(BaseUserEntity.FieldNickName)
                                 + " AND " + BaseUserEntity.FieldDeletionStateCode + " = " + DbHelper.GetParameter(BaseUserEntity.FieldDeletionStateCode);
                dbParameters.Add(this.DbHelper.MakeParameter(BaseUserEntity.FieldNickName, nickName));
                dbParameters.Add(this.DbHelper.MakeParameter(BaseUserEntity.FieldDeletionStateCode, 0));
                errorMark = 2;
                var dt = this.DbHelper.Fill(sqlQuery, dbParameters.ToArray());
                // 若是有多条数据返回，把设置为无效的数据先过滤掉，防止数据有重复
                if (dt.Rows.Count > 1)
                {
                    dt = BaseBusinessLogic.SetFilter(dt, BaseUserEntity.FieldEnabled, "1");
                }

                if (dt.Rows.Count == 1)
                {
                    // 进行登录校验
                    BaseUserEntity userEntity = null;
                    errorMark = 3;
                    userEntity = BaseEntity.Create<BaseUserEntity>(dt.Rows[0]);
                    errorMark = 4;
                    result = LogOnByEntity(userEntity, password, openId, createOpenId, systemCode, ipAddress, ipAddressName, macAddress, computerName, checkUserPassword, false, checkMacAddress);
                }
                else if (dt.Rows.Count > 1)
                {
                    // 用户命重复了
                    errorMark = 5;
                    BaseLoginLogManager.AddLog(systemCode, string.Empty, nickName, ipAddress, ipAddressName, macAddress, Status.UserDuplicate.ToDescription());
                    result.StatusCode = Status.UserDuplicate.ToString();
                }
                else
                {
                    errorMark = 6;
                    // 用户没找到
                    BaseLoginLogManager.AddLog(systemCode, string.Empty, nickName, ipAddress, ipAddressName, macAddress, Status.UserNotFound.ToDescription());
                    result.StatusCode = Status.UserNotFound.ToString();
                }
                result.StatusMessage = this.GetStateMessage(result.StatusCode);
            }
            catch (System.Exception ex)
            {
                string writeMessage = "BaseUserManager.LogOnByNickName:发生时间:" + DateTime.Now
                    + System.Environment.NewLine + "errorMark = " + errorMark.ToString()
                    + System.Environment.NewLine + "Message:" + ex.Message
                    + System.Environment.NewLine + "Source:" + ex.Source
                    + System.Environment.NewLine + "StackTrace:" + ex.StackTrace
                    + System.Environment.NewLine + "TargetSite:" + ex.TargetSite
                    + System.Environment.NewLine;

                FileUtil.WriteMessage(writeMessage, BaseSystemInfo.StartupPath + "//Exception//Exception" + DateTime.Now.ToString(BaseSystemInfo.DateFormat) + ".txt");
            }

            return result;
        }
        #endregion

        #region public UserLogOnResult LogOnByNickNameByCache(string nickName, string password, string openId = null, bool createOpenId = false, string ipAddress = null, string macAddress = null, bool checkUserPassword = true) 进行登录操作
        /// <summary>
        /// 进行登录操作
        /// </summary>
        /// <param name="nickName">昵称</param>
        /// <param name="password">密码</param>
        /// <param name="openId">单点登录标识</param>
        /// <param name="createOpenId">重新创建单点登录标识</param>
        /// <param name="ipAddress">IP地址</param>
        /// <param name="macAddress">MAC地址</param>
        /// <param name="checkUserPassword">是否要检查用户密码</param>
        /// <returns>用户信息</returns>
        public UserLogOnResult LogOnByNickNameByCache(string nickName, string password, string openId = null, bool createOpenId = false, string systemCode = null, string ipAddress = null, string macAddress = null, string computerName = null, bool checkUserPassword = true, bool getOpenIdOnly = false, bool checkMacAddress = true)
        {
            UserLogOnResult result = new UserLogOnResult();
            int errorMark = 0;

            string ipAddressName = string.Empty;

            try
            {
                if (BaseSystemInfo.OnInternet && !string.IsNullOrEmpty(ipAddress))
                {
                    errorMark = 1;
                    ipAddressName = IpHelper.GetInstance().FindName(ipAddress);
                }

                // 大多人还是看小写比较敏感
                if (!string.IsNullOrWhiteSpace(nickName))
                {
                    nickName = nickName.ToLower();
                }
                nickName = this.DbHelper.SqlSafe(nickName);

                if (UserInfo != null)
                {
                    if (string.IsNullOrEmpty(ipAddress))
                    {
                        ipAddress = UserInfo.IPAddress;
                    }
                    // 得到MAC地址，否则会导致后面的MAC地址判断无效 赵秉杰 2012-09-02
                    if (string.IsNullOrEmpty(macAddress))
                    {
                        macAddress = UserInfo.MACAddress;
                    }
                }

                // 04. 默认为用户没有找到状态，查找用户
                // 这是为了达到安全要求，不能提示用户未找到，那容易让别人猜测到帐户
                if (BaseSystemInfo.CheckPasswordStrength)
                {
                    result.StatusCode = Status.ErrorLogOn.ToString();
                }
                else
                {
                    result.StatusCode = Status.UserNotFound.ToString();
                }
                result.StatusMessage = this.GetStateMessage(result.StatusCode);

                BaseUserEntity userEntity = BaseUserManager.GetObjectByNickNameByCache(nickName);
                if (userEntity != null)
                {
                    // 进行登录校验
                    errorMark = 4;
                    result = LogOnByEntity(userEntity, password, openId, createOpenId, systemCode, ipAddress, ipAddressName, macAddress, computerName, checkUserPassword, false, checkMacAddress);
                }
                else
                {
                    errorMark = 6;
                    // 用户没找到
                    BaseLoginLogManager.AddLog(systemCode, string.Empty, nickName, ipAddress, ipAddressName, macAddress, Status.UserNotFound.ToDescription());
                    result.StatusCode = Status.UserNotFound.ToString();
                }

                result.StatusMessage = this.GetStateMessage(result.StatusCode);
            }
            catch (System.Exception ex)
            {
                string writeMessage = "BaseUserManager.LogOnByNickNameByCache:发生时间:" + DateTime.Now
                    + System.Environment.NewLine + "errorMark = " + errorMark.ToString()
                    + System.Environment.NewLine + "Message:" + ex.Message
                    + System.Environment.NewLine + "Source:" + ex.Source
                    + System.Environment.NewLine + "StackTrace:" + ex.StackTrace
                    + System.Environment.NewLine + "TargetSite:" + ex.TargetSite
                    + System.Environment.NewLine;

                FileUtil.WriteMessage(writeMessage, BaseSystemInfo.StartupPath + "//Exception//Exception" + DateTime.Now.ToString(BaseSystemInfo.DateFormat) + ".txt");
            }

            return result;
        }
        #endregion

        /// <summary>
        /// 按公司编号、用户编号进行登录
        /// </summary>
        /// <param name="companyCode">公司编号</param>
        /// <param name="userCode">用户编号</param>
        /// <param name="userEntity">用户实体</param>
        /// <returns>登录信息</returns>
        public UserLogOnResult LogOnByCompanyByCode(string companyCode, string userCode, out BaseUserEntity userEntity)
        {
            UserLogOnResult result = new UserLogOnResult();

            int errorMark = 0;
            userEntity = null;
            try
            {
                companyCode = DotNet.Utilities.SecretUtil.SqlSafe(companyCode);
                userCode = DotNet.Utilities.SecretUtil.SqlSafe(userCode);

                // 设备在手里，认为是安全的，不是人人都能有设备在手里
                result.StatusCode = Status.UserNotFound.ToString();
                result.StatusMessage = this.GetStateMessage(result.StatusCode);
                if (string.IsNullOrEmpty(companyCode) || string.IsNullOrEmpty(userCode))
                {
                    return result;
                }
                // 大多人还是看小写比较敏感
                if (!string.IsNullOrWhiteSpace(userCode))
                {
                    // 2015-07-09 吉日嘎拉 这里不能转换成小写
                    // userCode = userCode.ToLower();
                }

                // 2015-11-11 吉日嘎拉 查询数据的同时把数据进行了缓存，提高下一个登录时的效率
                string companyId = string.Empty;

                errorMark = 1;
                companyId = BaseOrganizeManager.GetIdByCodeByCache(companyCode);
                if (string.IsNullOrEmpty(companyId))
                {
                    result.StatusCode = Status.CompanyNotFound.ToString();
                    result.StatusMessage = this.GetStateMessage(result.StatusCode);
                    return result;
                }

                string sqlQuery = string.Empty;
                List<IDbDataParameter> dbParameters = new List<IDbDataParameter>();
                sqlQuery = "SELECT * "
                         + "  FROM " + BaseUserEntity.TableName
                         + " WHERE " + BaseUserEntity.FieldDeletionStateCode + " = " + DbHelper.GetParameter(BaseUserEntity.FieldDeletionStateCode)
                         + "       AND " + BaseUserEntity.FieldCompanyId + " = " + DbHelper.GetParameter(BaseUserEntity.FieldCompanyId)
                         + "       AND " + BaseUserEntity.FieldCode + " = " + DbHelper.GetParameter(BaseUserEntity.FieldCode);

                dbParameters.Add(DbHelper.MakeParameter(BaseUserEntity.FieldDeletionStateCode, 0));
                dbParameters.Add(DbHelper.MakeParameter(BaseUserEntity.FieldCompanyId, companyId));
                dbParameters.Add(DbHelper.MakeParameter(BaseUserEntity.FieldCode, userCode));

                errorMark = 2;
                DataTable dt = this.DbHelper.Fill(sqlQuery, dbParameters.ToArray());
                // 若是有多条数据返回，把设置为无效的数据先过滤掉，防止数据有重复
                if (dt.Rows.Count > 1)
                {
                    dt = BaseBusinessLogic.SetFilter(dt, BaseUserEntity.FieldEnabled, "1");
                }
                if (dt.Rows.Count > 1)
                {
                    result.StatusCode = Status.UserDuplicate.ToString();
                    result.StatusMessage = this.GetStateMessage(result.StatusCode);
                    return result;
                }
                if (dt.Rows.Count == 1)
                {
                    errorMark = 3;
                    userEntity = BaseEntity.Create<BaseUserEntity>(dt);
                }
            }
            catch (System.Exception ex)
            {
                string writeMessage = "BaseUserManager.LogOnByCompanyByCode:发生时间:" + DateTime.Now
                    + System.Environment.NewLine + "errorMark = " + errorMark.ToString()
                    + System.Environment.NewLine + "Message:" + ex.Message
                    + System.Environment.NewLine + "Source:" + ex.Source
                    + System.Environment.NewLine + "StackTrace:" + ex.StackTrace
                    + System.Environment.NewLine + "TargetSite:" + ex.TargetSite
                    + System.Environment.NewLine;

                FileUtil.WriteMessage(writeMessage, BaseSystemInfo.StartupPath + "//Exception//Exception" + DateTime.Now.ToString(BaseSystemInfo.DateFormat) + ".txt");
            }

            return result;
        }

        public UserLogOnResult LogOnByCompany(string companyName, string userName, string password, string openId = null, bool createOpenId = false, string systemCode = null, string ipAddress = null, string macAddress = null, string computerName = null, bool checkUserPassword = true, bool validateUserOnly = false, bool checkMacAddress = true)
        {
            string ipAddressName = string.Empty;
            if (BaseSystemInfo.OnInternet && !string.IsNullOrEmpty(ipAddress))
            {
                ipAddressName = IpHelper.GetInstance().FindName(ipAddress);
            }

            UserLogOnResult result = new UserLogOnResult();
            result.StatusCode = "Error";
            result.StatusMessage = "请用唯一用户名登录、若不知道唯一用户名、请向公司的管理员索取。";

            if (string.IsNullOrEmpty(companyName) || string.IsNullOrEmpty(userName))
            {
                return result;
            }
            // 大多人还是看小写比较敏感
            if (!string.IsNullOrWhiteSpace(userName))
            {
                userName = userName.ToLower();
            }
            // 这里先同步数据？然后再登录，这样提高数据同步效果
            companyName = DotNet.Utilities.SecretUtil.SqlSafe(companyName);
            userName = DotNet.Utilities.SecretUtil.SqlSafe(userName);
            if (UserInfo != null)
            {
                if (string.IsNullOrEmpty(ipAddress))
                {
                    ipAddress = UserInfo.IPAddress;
                }
                // 得到MAC地址，否则会导致后面的MAC地址判断无效 赵秉杰 2012-09-02
                if (string.IsNullOrEmpty(macAddress))
                {
                    macAddress = UserInfo.MACAddress;
                }
            }

            if (BaseSystemInfo.CheckPasswordStrength)
            {
                result.StatusCode = Status.ErrorLogOn.ToString();
            }
            else
            {
                result.StatusCode = Status.UserNotFound.ToString();
            }
            result.StatusMessage = this.GetStateMessage(result.StatusCode);

            string sqlQuery = string.Empty;
            List<IDbDataParameter> dbParameters = new List<IDbDataParameter>();
            sqlQuery = "SELECT * "
                     + "  FROM " + BaseUserEntity.TableName
                     + " WHERE " + BaseUserEntity.FieldDeletionStateCode + " = " + DbHelper.GetParameter(BaseUserEntity.FieldDeletionStateCode);

            dbParameters.Add(DbHelper.MakeParameter(BaseUserEntity.FieldDeletionStateCode, 0));
            if (!string.IsNullOrEmpty(companyName))
            {
                sqlQuery += " AND " + BaseUserEntity.FieldCompanyName + " = " + DbHelper.GetParameter(BaseUserEntity.FieldCompanyName);
                dbParameters.Add(DbHelper.MakeParameter(BaseUserEntity.FieldCompanyName, companyName));
                if (!string.IsNullOrEmpty(userName))
                {
                    sqlQuery += " AND " + BaseUserEntity.FieldUserName + " = " + DbHelper.GetParameter(BaseUserEntity.FieldUserName);
                    dbParameters.Add(DbHelper.MakeParameter(BaseUserEntity.FieldUserName, userName));
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(userName))
                {
                    sqlQuery += " AND " + BaseUserEntity.FieldNickName + " = " + DbHelper.GetParameter(BaseUserEntity.FieldNickName);
                    dbParameters.Add(DbHelper.MakeParameter(BaseUserEntity.FieldNickName, userName));
                }
            }

            DataTable dt = this.DbHelper.Fill(sqlQuery, dbParameters.ToArray());
            // 若是有多条数据返回，把设置为无效的数据先过滤掉，防止数据有重复
            if (dt.Rows.Count > 1)
            {
                dt = BaseBusinessLogic.SetFilter(dt, BaseUserEntity.FieldEnabled, "1");
            }

            BaseUserEntity userEntity = null;
            if (dt.Rows.Count > 1)
            {
                result.StatusCode = Status.UserDuplicate.ToString();
            }
            else if (dt.Rows.Count == 1)
            {
                userEntity = BaseEntity.Create<BaseUserEntity>(dt);
                // 用户登录
                result = LogOnByEntity(userEntity, password, openId, createOpenId, systemCode, ipAddress, ipAddressName, macAddress, computerName, checkUserPassword, validateUserOnly, checkMacAddress);
            }
            else
            {
                // 若不能正常登录、看这个人是否有超级管理员的权限？若是超级管理员，可以登录任何一个网点
                sqlQuery = "SELECT * "
                          + " FROM " + BaseUserEntity.TableName
                         + " WHERE " + BaseUserEntity.FieldDeletionStateCode + " = 0 "
                                 + " AND " + BaseUserEntity.FieldEnabled + " = 1 "
                                 + " AND id IN (SELECT resourceid FROM basepermission WHERE resourcecategory = 'BaseUser' AND permissionid IN (SELECT id FROM basemodule WHERE code = 'LogOnAllCompany' AND enabled = 1 AND deletionstatecode = 0)) "
                                 + " AND (" + BaseUserEntity.FieldUserName + " = " + DbHelper.GetParameter(BaseUserEntity.FieldUserName)
                                            + " OR " + BaseUserEntity.FieldNickName + " = " + DbHelper.GetParameter(BaseUserEntity.FieldNickName) + ")";
                dbParameters = new List<IDbDataParameter>();
                dbParameters.Add(DbHelper.MakeParameter(BaseUserEntity.FieldUserName, userName));
                dbParameters.Add(DbHelper.MakeParameter(BaseUserEntity.FieldNickName, userName));
                dt = this.DbHelper.Fill(sqlQuery, dbParameters.ToArray());
                if (dt.Rows.Count > 1)
                {
                    result.StatusCode = Status.UserDuplicate.ToString();
                }
                else if (dt.Rows.Count == 1)
                {
                    userEntity = BaseEntity.Create<BaseUserEntity>(dt);
                    bool logOnAllCompany = true;
                    // var permissionManager = new BasePermissionManager();
                    // logOnAllCompany = permissionManager.IsAuthorized(userEntity.Id, "LogOnAllCompany", "登录所有网点权限");
                    if (logOnAllCompany)
                    {
                        // 用户登录
                        BaseOrganizeEntity organizeEntity = BaseOrganizeManager.GetObjectByNameByCache(companyName);
                        if (organizeEntity != null)
                        {
                            userEntity.CompanyId = organizeEntity.Id.ToString();
                            userEntity.CompanyName = organizeEntity.FullName;
                            result = LogOnByEntity(userEntity, password, openId, createOpenId, systemCode, ipAddress, ipAddressName, macAddress, computerName, checkUserPassword, validateUserOnly, checkMacAddress);
                        }
                    }
                }
            }
            result.StatusMessage = this.GetStateMessage(result.StatusCode);
            return result;
        }

        public UserLogOnResult LogOnByCompanyByCode(string companyCode, string userCode, string password, string openId = null, bool createOpenId = false, string systemCode = null, string ipAddress = null, string macAddress = null, string computerName = null, bool checkUserPassword = true, bool validateUserOnly = false, bool checkMacAddress = false)
        {
            UserLogOnResult result = new UserLogOnResult();

            string ipAddressName = string.Empty;
            if (!string.IsNullOrEmpty(ipAddress) && BaseSystemInfo.OnInternet)
            {
                ipAddressName = IpHelper.GetInstance().FindName(ipAddress);
            }
            if (UserInfo != null)
            {
                if (string.IsNullOrEmpty(ipAddress))
                {
                    ipAddress = UserInfo.IPAddress;
                }
                // 得到MAC地址，否则会导致后面的MAC地址判断无效 赵秉杰 2012-09-02
                if (string.IsNullOrEmpty(macAddress))
                {
                    macAddress = UserInfo.MACAddress;
                }
            }

            // 2015-11-11 吉日嘎拉 是否获取到了用户信息
            BaseUserEntity userEntity = null;

            result = LogOnByCompanyByCode(companyCode, userCode, out userEntity);

            if (userEntity != null)
            {
                // 用户登录
                result = LogOnByEntity(userEntity, password, openId, createOpenId, systemCode, ipAddress, ipAddressName, macAddress, computerName, checkUserPassword, validateUserOnly, checkMacAddress);
            }
            result.StatusMessage = this.GetStateMessage(result.StatusCode);

            return result;
        }

        public UserLogOnResult LogOnByVerificationCode(string companyCode, string userCode, string verificationCode, string openId = null, bool createOpenId = false, string systemCode = null, string ipAddress = null, string macAddress = null, string computerName = null, bool checkUserPassword = false, bool validateUserOnly = false, bool checkMacAddress = false)
        {
            UserLogOnResult result = new UserLogOnResult();

            string ipAddressName = string.Empty;
            if (!string.IsNullOrEmpty(ipAddress) && BaseSystemInfo.OnInternet)
            {
                ipAddressName = IpHelper.GetInstance().FindName(ipAddress);
            }
            if (UserInfo != null)
            {
                if (string.IsNullOrEmpty(ipAddress))
                {
                    ipAddress = UserInfo.IPAddress;
                }
                // 得到MAC地址，否则会导致后面的MAC地址判断无效 赵秉杰 2012-09-02
                if (string.IsNullOrEmpty(macAddress))
                {
                    macAddress = UserInfo.MACAddress;
                }
            }

            // 2015-11-11 吉日嘎拉 是否获取到了用户信息
            BaseUserEntity userEntity = null;
            result = LogOnByCompanyByCode(companyCode, userCode, out userEntity);
            if (userEntity != null)
            {
                // 2015-11-11 吉日嘎拉 进行手机验证
                bool mobileValidate = false;
                string mobile = BaseUserContactManager.GetMobileByCache(userEntity.Id);
                if (ValidateUtil.IsMobile(mobile))
                {
                    MobileService mobileService = new Business.MobileService();
                    mobileValidate = mobileService.ValidateVerificationCode(this.UserInfo, mobile, verificationCode);
                }
                if (!mobileValidate)
                {
                    result.StatusCode = Status.VerificationCodeError.ToString();
                    result.StatusMessage = this.GetStateMessage(result.StatusCode);
                    return result;
                }
                // 用户登录
                string password = string.Empty;
                checkUserPassword = false;
                result = LogOnByEntity(userEntity, password, openId, createOpenId, systemCode, ipAddress, ipAddressName, macAddress, computerName, checkUserPassword, validateUserOnly, checkMacAddress);
            }
            result.StatusMessage = this.GetStateMessage(result.StatusCode);

            return result;
        }


        /// <summary>
        /// 进行登录操作
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="openId">单点登录标识</param>
        /// <param name="createOpenId">重新创建单点登录标识</param>
        /// <param name="systemCode">系统编号</param>
        /// <param name="ipAddress">IP地址</param>
        /// <param name="macAddress">MAC地址</param>
        /// <param name="checkUserPassword">是否要检查用户密码</param>
        /// <returns>用户信息</returns>
        public UserLogOnResult LogOnByUserName(string userName, string password, string openId = null, bool createOpenId = false, string systemCode = null, string ipAddress = null, string macAddress = null, string computerName = null, bool checkUserPassword = true, bool getOpenIdOnly = false)
        {
            UserLogOnResult result = new UserLogOnResult();

            string ipAddressName = string.Empty;
            if (BaseSystemInfo.OnInternet && !string.IsNullOrEmpty(ipAddress))
            {
                ipAddressName = IpHelper.GetInstance().FindName(ipAddress);
            }

            string realname = string.Empty;
            if (UserInfo != null)
            {
                realname = UserInfo.RealName;
                if (string.IsNullOrEmpty(ipAddress))
                {
                    ipAddress = UserInfo.IPAddress;
                }
                // 得到MAC地址，否则会导致后面的MAC地址判断无效 赵秉杰 2012-09-02
                if (string.IsNullOrEmpty(macAddress))
                {
                    macAddress = UserInfo.MACAddress;
                }
            }

            /*
            #if (!DEBUG)
            if (BaseSystemInfo.OnLineLimit > 0)
            {
                if (userLogOnManager.CheckOnLineLimit())
                {
                    this.StatusCode = Status.ErrorOnLineLimit.ToString();
                    this.StatusMessage = this.GetStateMessage(this.StatusCode);
                    // BaseLogManager.Instance.Add(userName, RealName, "LogOn", AppMessage.BaseUserManager, "LogOn", AppMessage.BaseUserManager_LogOn, userName, ipAddress, AppMessage.MSG0089 + BaseSystemInfo.OnLineLimit.ToString());
                    return result;
                }
            }
            #endif
            */

            // 04. 默认为用户没有找到状态，查找用户
            // 这是为了达到安全要求，不能提示用户未找到，那容易让别人猜测到帐户
            if (BaseSystemInfo.CheckPasswordStrength)
            {
                result.StatusCode = Status.ErrorLogOn.ToString();
                result.StatusMessage = Status.ErrorLogOn.ToDescription();
            }
            else
            {
                result.StatusCode = Status.UserNotFound.ToString();
                result.StatusMessage = Status.UserNotFound.ToDescription();
            }

            // 02. 查询数据库中的用户数据？只查询未被删除的
            // 先按用户名登录
            string where = string.Empty;
            userName = this.DbHelper.SqlSafe(userName);
            where = BaseUserEntity.FieldUserName + " = '" + userName + "' AND " + BaseUserEntity.FieldDeletionStateCode + " = 0 ";
            var dt = this.GetDataTable(where);

            // 服务器上、本地都需要能登录才可以
            if (dt.Rows.Count == 0)
            {
                if (this.DbHelper.CurrentDbType == CurrentDbType.Oracle || this.DbHelper.CurrentDbType == CurrentDbType.SQLite)
                {
                    where = " Id > 0 AND " + BaseUserEntity.FieldNickName + " = '" + userName + "' AND " + BaseUserEntity.FieldDeletionStateCode + " = 0 ";
                    dt = this.GetDataTable(where);
                }

                /*
                if (dt.Rows.Count == 0)
                {
                    // 若没数据再按工号登录
                    dt = this.GetDataTable(new KeyValuePair<string, object>(BaseUserEntity.FieldCode, userName)
                        , new KeyValuePair<string, object>(BaseUserEntity.FieldDeletionStateCode, 0));
                }

                if (result.Rows.Count == 0)
                {
                    // 若没数据再按邮件登录
                    result = this.GetDataTable(new KeyValuePair<string, object>(BaseUserEntity.FieldEmail, userName)
                        , new KeyValuePair<string, object>(BaseUserEntity.FieldDeletionStateCode, 0));
                }
                else if (result.Rows.Count == 0)
                {
                    // 若没数据再按手机号码登录
                    result = this.GetDataTable(new KeyValuePair<string, object>(BaseUserEntity.FieldMobile, userName)
                        , new KeyValuePair<string, object>(BaseUserEntity.FieldDeletionStateCode, 0));
                }
                else if (result.Rows.Count == 0)
                {
                    // 若没数据再按手机号码登录
                    result = this.GetDataTable(new KeyValuePair<string, object>(BaseUserEntity.FieldTelephone, userName)
                        , new KeyValuePair<string, object>(BaseUserEntity.FieldDeletionStateCode, 0));
                }
                */
            }

            // 若是有多条数据返回，把设置为无效的数据先过滤掉，防止数据有重复
            if (dt.Rows.Count > 1)
            {
                dt = BaseBusinessLogic.SetFilter(dt, BaseUserEntity.FieldEnabled, "1");
            }

            if (dt.Rows.Count == 1)
            {
                // 进行登录校验
                BaseUserEntity userEntity = null;
                userEntity = BaseEntity.Create<BaseUserEntity>(dt.Rows[0]);
                result = LogOnByEntity(userEntity, password, openId, createOpenId, systemCode, ipAddress, ipAddressName, macAddress, computerName, checkUserPassword);
            }
            else if (dt.Rows.Count > 1)
            {
                // 用户命重复了
                BaseLoginLogManager.AddLog(systemCode, string.Empty, userName, ipAddress, ipAddressName, macAddress, Status.UserDuplicate.ToDescription());
                result.StatusCode = Status.UserDuplicate.ToString();
            }
            else
            {
                // 用户没找到
                BaseLoginLogManager.AddLog(systemCode, string.Empty, userName, ipAddress, ipAddressName, macAddress, Status.UserNotFound.ToDescription());
                result.StatusCode = Status.UserNotFound.ToString();
            }
            result.StatusMessage = this.GetStateMessage(result.StatusCode);

            return result;
        }


        #region public UserLogOnResult LogOnByEmail(string email, string password, string openId = null, bool createOpenId = false, string ipAddress = null, string macAddress = null, bool checkUserPassword = true) 进行登录操作
        /// <summary>
        /// 进行登录操作
        /// </summary>
        /// <param name="email">电子邮件</param>
        /// <param name="password">密码</param>
        /// <param name="openId">单点登录标识</param>
        /// <param name="createOpenId">重新创建单点登录标识</param>
        /// <param name="ipAddress">IP地址</param>
        /// <param name="macAddress">MAC地址</param>
        /// <param name="checkUserPassword">是否要检查用户密码</param>
        /// <returns>用户信息</returns>
        public UserLogOnResult LogOnByEmail(string email, string password, string openId = null, bool createOpenId = false, string systemCode = null, string ipAddress = null, string macAddress = null, string computerName = null, bool checkUserPassword = true, bool validateUserOnly = false, bool checkMacAddress = true)
        {
            string ipAddressName = string.Empty;
            if (BaseSystemInfo.OnInternet && !string.IsNullOrEmpty(ipAddress))
            {
                ipAddressName = IpHelper.GetInstance().FindName(ipAddress);
            }

            UserLogOnResult result = new UserLogOnResult();
            string realname = string.Empty;
            if (UserInfo != null)
            {
                realname = UserInfo.RealName;
                if (string.IsNullOrEmpty(ipAddress))
                {
                    ipAddress = UserInfo.IPAddress;
                }
                // 得到MAC地址，否则会导致后面的MAC地址判断无效 赵秉杰 2012-09-02
                if (string.IsNullOrEmpty(macAddress))
                {
                    macAddress = UserInfo.MACAddress;
                }
            }

            // 04. 默认为用户没有找到状态，查找用户
            // 这是为了达到安全要求，不能提示用户未找到，那容易让别人猜测到帐户
            if (BaseSystemInfo.CheckPasswordStrength)
            {
                result.StatusCode = Status.ErrorLogOn.ToString();
            }
            else
            {
                result.StatusCode = Status.UserNotFound.ToString();
            }
            result.StatusMessage = this.GetStateMessage(result.StatusCode);
            // 02. 查询数据库中的用户数据？只查询未被删除的
            // 先按用户名登录
            BaseUserContactManager manager = new BaseUserContactManager();
            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            // 验证通过的，才可以登录
            parameters.Add(new KeyValuePair<string, object>(BaseUserContactEntity.FieldEmail, email));
            // parameters.Add(new KeyValuePair<string, object>(BaseUserContactEntity.FieldEmailValiated, 1));
            string id = manager.GetId(parameters);

            if (!string.IsNullOrEmpty(id))
            {
                // 05. 判断密码，是否允许登录，是否离职是否正确
                BaseUserEntity userEntity = this.GetObject(id);
                result = LogOnByEntity(userEntity, password, openId, createOpenId, systemCode, ipAddress, ipAddressName, macAddress, computerName, checkUserPassword, validateUserOnly, checkMacAddress);
            }
            return result;
        }
        #endregion

        #region public UserLogOnResult LogOnByMobile(string mobile, string password, string openId = null, bool createOpenId = false, string systemCode = null, string ipAddress = null, string macAddress = null, bool checkUserPassword = true) 进行登录操作
        /// <summary>
        /// 进行登录操作
        /// </summary>
        /// <param name="mobile">手机号码</param>
        /// <param name="password">密码</param>
        /// <param name="openId">单点登录标识</param>
        /// <param name="createOpenId">重新创建单点登录标识</param>
        /// <param name="ipAddress">IP地址</param>
        /// <param name="macAddress">MAC地址</param>
        /// <param name="checkUserPassword">是否要检查用户密码</param>
        /// <returns>用户信息</returns>
        public UserLogOnResult LogOnByMobile(string mobile, string password, string openId = null, bool createOpenId = false, string systemCode = null, string ipAddress = null, string macAddress = null, string computerName = null, bool checkUserPassword = true, bool validateUserOnly = false, bool checkMacAddress = true)
        {
            string ipAddressName = string.Empty;
            if (BaseSystemInfo.OnInternet && !string.IsNullOrEmpty(ipAddress))
            {
                ipAddressName = IpHelper.GetInstance().FindName(ipAddress);
            }

            UserLogOnResult result = new UserLogOnResult();
            string realname = string.Empty;
            if (UserInfo != null)
            {
                realname = UserInfo.RealName;
                if (string.IsNullOrEmpty(ipAddress))
                {
                    ipAddress = UserInfo.IPAddress;
                }
                // 得到MAC地址，否则会导致后面的MAC地址判断无效 赵秉杰 2012-09-02
                if (string.IsNullOrEmpty(macAddress))
                {
                    macAddress = UserInfo.MACAddress;
                }
            }

            // 04. 默认为用户没有找到状态，查找用户
            // 这是为了达到安全要求，不能提示用户未找到，那容易让别人猜测到帐户
            if (BaseSystemInfo.CheckPasswordStrength)
            {
                result.StatusCode = Status.ErrorLogOn.ToString();
            }
            else
            {
                result.StatusCode = Status.UserNotFound.ToString();
            }
            result.StatusMessage = this.GetStateMessage(result.StatusCode);

            // 02. 查询数据库中的用户数据？只查询未被删除的
            // 先按用户名登录
            BaseUserContactManager manager = new BaseUserContactManager();
            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>(BaseUserContactEntity.FieldMobile, mobile));
            // 验证通过的，才可以登录
            // parameters.Add(new KeyValuePair<string, object>(BaseUserContactEntity.FieldMobileValiated, 1));
            string id = manager.GetId(parameters);

            if (!string.IsNullOrEmpty(id))
            {
                // 05. 判断密码，是否允许登录，是否离职是否正确
                BaseUserEntity userEntity = this.GetObject(id);
                result = LogOnByEntity(userEntity, password, openId, createOpenId, systemCode, ipAddress, ipAddressName, macAddress, computerName, checkUserPassword, validateUserOnly, checkMacAddress);
            }
            return result;
        }
        #endregion

        public UserLogOnResult LogOnByOpenId(string openId, string systemCode, string ipAddress = null, string macAddress = null, string computerName = null)
        {
            UserLogOnResult result = new UserLogOnResult();

            string ipAddressName = string.Empty;
            if (BaseSystemInfo.OnInternet && !string.IsNullOrEmpty(ipAddress))
            {
                ipAddressName = IpHelper.GetInstance().FindName(ipAddress);
            }

            this.IsLogOnByOpenId = true;

            // 用户没有找到状态
            result.StatusCode = Status.UserNotFound.ToString();
            result.StatusMessage = this.GetStateMessage(result.StatusCode);
            // 检查是否有效的合法的参数
            if (!String.IsNullOrEmpty(openId))
            {
                List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
                // parameters.Add(new KeyValuePair<string, object>(BaseUserEntity.FieldEnabled, 1));
                // parameters.Add(new KeyValuePair<string, object>(BaseUserEntity.FieldDeletionStateCode, 0));
                if (!string.IsNullOrEmpty(openId))
                {
                    parameters.Add(new KeyValuePair<string, object>(BaseUserLogOnEntity.FieldOpenId, openId));
                }
                // 若是单点登录，那就不能判断ip地址，因为不是直接登录，是间接登录
                if (!string.IsNullOrEmpty(ipAddress))
                {
                    // parameters.Add(new KeyValuePair<string, object>(BaseUserLogOnEntity.FieldIPAddress, ipAddress));
                }
                if (!string.IsNullOrEmpty(macAddress))
                {
                    // parameters.Add(new KeyValuePair<string, object>(BaseUserLogOnEntity.FieldMACAddress, macAddress));
                }
                var dt = new BaseUserLogOnManager(this.DbHelper, this.UserInfo).GetDataTable(parameters);
                if (dt.Rows.Count == 1)
                {
                    BaseUserLogOnEntity userLogOnEntity = new BaseUserLogOnEntity();
                    userLogOnEntity.GetFrom(dt.Rows[0]);
                    //下面的判断了openid的过期时间，sso登录时没有重新更新openid,直接取数据库中的openid,在此做判断时就有可能过期了，
                    //导致子系统通过openid登录时无法获取用户信息，登录不成功
                    //办法：1、sso登录时创建新的openid  2、此处不做openid过期判断 3，sso登录不创建新openid,但改变一下表中的过期时间
                    //此处判断openid过期时间可能有问题
                    if (userLogOnEntity.OpenIdTimeout.HasValue && userLogOnEntity.OpenIdTimeout > DateTime.Now)
                    {
                        BaseUserEntity userEntity = this.GetObject(dt.Rows[0][BaseUserLogOnEntity.FieldId].ToString());
                        result = this.LogOnByEntity(userEntity, userLogOnEntity.UserPassword, openId, false, systemCode, ipAddress, ipAddressName, macAddress, computerName, false);
                    }
                    else
                    {
                        result.StatusCode = Status.Timeout.ToString();
                    }
                }
            }
            result.StatusMessage = this.GetStateMessage(result.StatusCode);

            return result;
        }

        #region public UserLogOnResult LogOnByEntity(BaseUserEntity userEntity, string password, string openId = null, bool createOpenId = false, string systemCode = null, string ipAddress = null, string macAddress = null, bool checkUserPassword = true, bool validateUserOnly = false) 进行登录操作

        /// <summary>
        /// 进行登录操作
        /// 2015-12-06 吉日嘎拉 进行日志更新，方便优化代码
        /// </summary>
        /// <param name="userEntity">用户实体</param>
        /// <param name="password">密码</param>
        /// <param name="openId">单点登录标识</param>
        /// <param name="createOpenId">重新创建单点登录标识</param>
        /// <param name="systemCode"></param>
        /// <param name="ipAddress">IP地址</param>
        /// <param name="ipAddressName"></param>
        /// <param name="macAddress">MAC地址</param>
        /// <param name="computerName"></param>
        /// <param name="checkUserPassword">是否要检查用户密码</param>
        /// <param name="validateUserOnly">只要验证用户就可以了</param>
        /// <param name="checkMacAddress"></param>
        /// <returns>用户信息</returns>
        public UserLogOnResult LogOnByEntity(BaseUserEntity userEntity, string password, string openId = null, bool createOpenId = false, string systemCode = null, string ipAddress = null, string ipAddressName = null, string macAddress = null, string computerName = null, bool checkUserPassword = true, bool validateUserOnly = false, bool checkMacAddress = true)
        {
            UserLogOnResult result = new UserLogOnResult();

            // 2016-01-22 吉日嘎拉 这里是处理，多个mac的问题，处理外部传递过来的参数不正确，不只是自己的系统，还有外部调用的系统的问题
            if (!string.IsNullOrEmpty(ipAddress))
            {
                string[] ips = ipAddress.Split(';');
                ips = ips.Where(t => !string.IsNullOrEmpty(t) && !t.Equals("127.0.0.01")).OrderBy(ip => ip).Take(1).ToArray();
                ipAddress = string.Join(";", ips);
            }
            if (!string.IsNullOrEmpty(macAddress))
            {
                string[] mac = macAddress.Split(';');
                mac = mac.Where(t => !string.IsNullOrEmpty(t) && !t.Equals("00-00-00-00-00-00") && t.Length == 17).OrderBy(ip => ip).Take(2).ToArray();
                macAddress = string.Join(";", mac);
            }

            int errorMark = 0;
            try
            {
                string orginPassWord = password;
                List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();

                BaseUserLogOnManager userLogOnManager = new BaseUserLogOnManager(this.UserInfo, this.UserLogOnTable);
                BaseUserLogOnEntity userLogOnEntity = userLogOnManager.GetObject(userEntity.Id);
                // 2015-12-24 吉日嘎拉进行代码分离、重复利用这部分代码、需要检查接口安全认证
                result = CheckUser(userEntity, userLogOnEntity);
                // 2015-12-26 吉日嘎拉，修改状态判断，成功验证才可以。
                if (!result.StatusCode.Equals(Status.OK.ToString()))
                {
                    BaseLoginLogManager.AddLog(systemCode, userEntity, ipAddress, ipAddressName, macAddress, result.StatusMessage);
                    return result;
                }

                string commandText = string.Empty;
                // macAddress 地址变化的，会进行手机验证，没有macAddress的，再进行城市验证
                if (string.IsNullOrWhiteSpace(macAddress))
                {
                    if (!string.IsNullOrWhiteSpace(ipAddressName))
                    {
                        if (!string.IsNullOrWhiteSpace(userLogOnEntity.IPAddressName))
                        {
                            if (!ipAddressName.Equals("局域网") && !userLogOnEntity.IPAddressName.Equals(ipAddressName))
                            {
                                // TODO 开启手机验证功能！, 三天验证一次也可以了
                                errorMark = 10;
                                // BaseUserContactManager userContactManager = new BaseUserContactManager();
                                // BaseUserContactEntity userContactEntity = userContactManager.GetObject(userLogOnEntity.Id);
                                // 2015-12-08 吉日嘎拉 提高效率、从缓存获取数据
                                BaseUserContactEntity userContactEntity = BaseUserContactManager.GetObjectByCache(userLogOnEntity.Id);
                                bool needVerification = false;
                                if (userContactEntity.MobileVerificationDate.HasValue)
                                {
                                    TimeSpan timeSpan = DateTime.Now - userContactEntity.MobileVerificationDate.Value;
                                    if ((timeSpan.TotalDays) > 7)
                                    {
                                        needVerification = true;
                                    }
                                }
                                else
                                {
                                    needVerification = true;
                                }
                                // 需要进行手机验证
                                if (needVerification)
                                {
                                    commandText = " UPDATE " + BaseUserContactEntity.TableName
                                                + "    SET " + BaseUserContactEntity.FieldMobileValiated + " = " + DbHelper.GetParameter(BaseUserContactEntity.FieldMobileValiated)
                                                + "  WHERE " + BaseUserContactEntity.FieldId + " = " + DbHelper.GetParameter(BaseUserContactEntity.FieldId);
                                    // + "    AND " + BaseUserContactEntity.FieldMobileVerificationDate + " IS NULL "
                                    // + "    AND " + BaseUserContactEntity.FieldMobileValiated + " = 1 ";
                                    errorMark = 11;
                                    List<IDbDataParameter> dbParameters = new List<IDbDataParameter>();
                                    dbParameters.Add(DbHelper.MakeParameter(BaseUserContactEntity.FieldMobileValiated, 0));
                                    dbParameters.Add(DbHelper.MakeParameter(BaseUserContactEntity.FieldId, userContactEntity.Id));
                                    dbHelper.ExecuteNonQuery(commandText, dbParameters.ToArray());
                                }
                            }
                        }
                        if (!ipAddressName.Equals("局域网"))
                        {
                            userLogOnEntity.IPAddressName = ipAddressName;
                        }
                    }
                }


                // 08. 是否检查用户IP地址，是否进行访问限制？管理员不检查IP，不管是否检查，要把最后的登录地址等进行更新才对
                // && !this.IsAdministrator(userEntity.Id

                if (userLogOnEntity.CheckIPAddress.HasValue
                    && userLogOnEntity.CheckIPAddress == 1)
                {
                    // BaseParameterManager parameterManager = new BaseParameterManager(this.DbHelper, this.UserInfo);
                    // 内网不进行限制
                    // if (!ipAddress.StartsWith("192.168."))
                    // {
                    // }
                    // 没有设置IPAddress地址时不检查

                    /*
                 
                    if (!string.IsNullOrEmpty(ipAddress))
                    {
                        parameters = new List<KeyValuePair<string, object>>();
                        parameters.Add(new KeyValuePair<string, object>(BaseParameterEntity.FieldParameterId, userEntity.Id));
                        parameters.Add(new KeyValuePair<string, object>(BaseParameterEntity.FieldCategoryCode, "IPAddress"));
                        // 没有设置IP地址时不检查
                        errorMark = 12;
                        if (parameterManager.Exists(parameters))
                        {
                            if (!this.CheckIPAddress(ipAddress, userEntity.Id))
                            {
                                parameters = new List<KeyValuePair<string, object>>();
                                parameters.Add(new KeyValuePair<string, object>(BaseUserLogOnEntity.FieldIPAddress, ipAddress));
                                errorMark = 13;
                                userLogOnManager.SetProperty(userEntity.Id, parameters);
                                errorMark = 131;
                                result.StatusCode = Status.ErrorIPAddress.ToString();
                                errorMark = 132;
                                result.StatusMessage = this.GetStateMessage(result.StatusCode);
                                errorMark = 14;
                                BaseLoginLogManager.AddLog(systemCode, userEntity, ipAddress, ipAddressName, macAddress, Status.ErrorIPAddress.ToDescription());
                                return result;
                            }
                        }
                    }
                 
                    */

                    // 没有设置MAC地址时不检查
                    if (checkMacAddress && !string.IsNullOrEmpty(macAddress))
                    {
                        if (!CheckMACAddressByCache(userLogOnEntity.Id, macAddress))
                        {
                            result.StatusCode = Status.ErrorMacAddress.ToString();
                            result.StatusMessage = this.GetStateMessage(result.StatusCode);
                            errorMark = 17;
                            BaseLoginLogManager.AddLog(systemCode, userEntity, ipAddress, ipAddressName, macAddress, Status.ErrorMacAddress.ToDescription());
                            return result;
                        }
                    }
                }

                // 10. 只允许登录一次，需要检查是否自己重新登录了，或者自己扮演自己了，先确认cs系统只能运行一个
                if (BaseSystemInfo.CheckOnLine)
                {
                    if ((UserInfo != null)
                        && (!string.IsNullOrEmpty(UserInfo.MACAddress))
                        && (!UserInfo.Id.Equals(userEntity.Id)))
                    {
                        // 若检查在线，那就检查OpenId是否还是哪个人
                        if (userLogOnEntity.MultiUserLogin == 0)
                        {
                            if (userLogOnEntity.UserOnLine > 0)
                            {
                                // 自己是否登录了2次，在没下线的情况下
                                bool isSelf = false;
                                if (!string.IsNullOrEmpty(openId))
                                {
                                    if (!string.IsNullOrEmpty(userLogOnEntity.OpenId))
                                    {
                                        if (userLogOnEntity.OpenId.Equals(openId))
                                        {
                                            isSelf = true;
                                        }
                                    }
                                }
                                if (!isSelf)
                                {
                                    result.StatusCode = Status.ErrorOnLine.ToString();
                                    result.StatusMessage = this.GetStateMessage(result.StatusCode);
                                    errorMark = 19;
                                    BaseLoginLogManager.AddLog(systemCode, userEntity, ipAddress, ipAddressName, macAddress, Status.ErrorOnLine.ToDescription());
                                    return result;
                                }
                            }
                        }
                    }
                }

                // 03. 系统是否采用了密码加密策略？
                if (!this.IsLogOnByOpenId)
                {
                    // 2015-11-11 吉日嘎拉 是否检查密码，还有其他方式的登录、例如验证码登录，OpenId登录等
                    if (checkUserPassword)
                    {
                        if (BaseSystemInfo.ServerEncryptPassword && this.SystemEncryptPassword)
                        {
                            errorMark = 20;
                            password = this.EncryptUserPassword(password, userLogOnEntity.Salt);
                        }

                        // 11. 密码是否正确(null 与空看成是相等的)
                        if (!(string.IsNullOrEmpty(userLogOnEntity.UserPassword) && string.IsNullOrEmpty(password)))
                        {
                            bool userPasswordOK = true;
                            errorMark = 201;
                            // 用户密码是空的
                            if (string.IsNullOrEmpty(userLogOnEntity.UserPassword))
                            {
                                // 但是输入了不为空的密码
                                if (!string.IsNullOrEmpty(password))
                                {
                                    userPasswordOK = false;
                                }
                            }
                            else
                            {
                                // 用户的密码不为空，但是用户是输入了密码
                                if (string.IsNullOrEmpty(password))
                                {
                                    userPasswordOK = false;
                                }
                                else
                                {
                                    errorMark = 202;
                                    // 再判断用户的密码与输入的是否相同
                                    userPasswordOK = userLogOnEntity.UserPassword.ToUpper().Equals(password.ToUpper());
                                }
                            }
                            // 用户的密码不相等
                            if (!userPasswordOK)
                            {
                                // 这里更新用户连续输入错误密码次数
                                // 2015-12-07 吉日嘎拉 这里防止发生意外，数据库有为空字段。
                                if (!userLogOnEntity.PasswordErrorCount.HasValue)
                                {
                                    userLogOnEntity.PasswordErrorCount = 0;
                                }
                                errorMark = 203;
                                userLogOnEntity.PasswordErrorCount = userLogOnEntity.PasswordErrorCount + 1;
                                errorMark = 204;
                                if (BaseSystemInfo.PasswordErrorLockLimit > 0 && userLogOnEntity.PasswordErrorCount >= BaseSystemInfo.PasswordErrorLockLimit)
                                {
                                    parameters = new List<KeyValuePair<string, object>>();
                                    if (BaseSystemInfo.PasswordErrorLockCycle == 0)
                                    {
                                        // 设置为无效，需要管理员进行审核才可以
                                        parameters.Add(new KeyValuePair<string, object>(BaseUserEntity.FieldEnabled, 0));
                                        // 待审核状态
                                        parameters.Add(new KeyValuePair<string, object>(BaseUserEntity.FieldAuditStatus, AuditStatus.WaitForAudit.ToString()));
                                        errorMark = 21;
                                        this.SetProperty(userEntity.Id, parameters);
                                    }
                                    else
                                    {
                                        // 这个是进行锁定帐户设置。
                                        userLogOnEntity.LockStartDate = DateTime.Now;
                                        userLogOnEntity.LockEndDate = DateTime.Now.AddMinutes(BaseSystemInfo.PasswordErrorLockCycle);
                                        parameters.Add(new KeyValuePair<string, object>(BaseUserLogOnEntity.FieldLockStartDate, userLogOnEntity.LockStartDate));
                                        parameters.Add(new KeyValuePair<string, object>(BaseUserLogOnEntity.FieldLockEndDate, userLogOnEntity.LockEndDate));
                                        errorMark = 22;
                                        userLogOnManager.SetProperty(userEntity.Id, parameters);
                                    }
                                }
                                else
                                {
                                    parameters = new List<KeyValuePair<string, object>>();
                                    parameters.Add(new KeyValuePair<string, object>(BaseUserLogOnEntity.FieldPasswordErrorCount, userLogOnEntity.PasswordErrorCount));
                                    errorMark = 23;
                                    userLogOnManager.SetProperty(userEntity.Id, parameters);
                                }
                                // 密码错误后 1：应该记录日志
                                errorMark = 24;
                                BaseLoginLogManager.AddLog(systemCode, userEntity, ipAddress, ipAddressName, macAddress, Status.PasswordError.ToDescription());
                                // TODO: 密码错误后 2：看最近1个小时输入了几次错误了？24小时里。
                                // TODO: 密码错误后 3：若错误密码数量已经超过了指定的限制，那用户就需要被锁定1个小时。
                                // TODO: 密码错误后 4：同时需要处理返回值，是由于密码次数过多导致的被锁定，登录时也应该能读取这个状态比较，时间过期了，也应该进行处理一下状态。
                                // 密码强度检查，若是要有安全要求比较高的，返回的提醒消息要进行特殊处理，不能返回非常明确的提示信息。
                                if (BaseSystemInfo.CheckPasswordStrength)
                                {
                                    result.StatusCode = Status.ErrorLogOn.ToString();
                                }
                                else
                                {
                                    result.StatusCode = Status.PasswordError.ToString();
                                }
                                result.StatusMessage = this.GetStateMessage(result.StatusCode);
                                return result;
                            }
                        }
                        userLogOnEntity.PasswordErrorCount = 0;
                    }
                }

                //2015-07-31  宋彪添加的密码检测
                //密码检测 强度检测
                //string message;
                //if (IsWeakPassWord(userEntity, orginPassWord, out message))
                //{
                //    //需要修改密码
                //    userLogOnEntity.NeedModifyPassword = 1;
                //    result.StatusMessage = message;
                //}

                // 09. 更新IP地址，更新MAC地址，这里是为只执行一次更新优化数据库I/O，若登录成功自然连续输入密码错误就是0了。
                // parameters = new List<KeyValuePair<string, object>>();
                // parameters.Add(new KeyValuePair<string, object>(BaseUserLogOnEntity.FieldPasswordErrorCount, 0));
                if (!string.IsNullOrEmpty(ipAddress))
                {
                    userLogOnEntity.IPAddress = ipAddress;
                    // parameters.Add(new KeyValuePair<string, object>(BaseUserLogOnEntity.FieldIPAddress, ipAddress));
                }
                if (!string.IsNullOrEmpty(macAddress))
                {
                    userLogOnEntity.MACAddress = macAddress;
                    // parameters.Add(new KeyValuePair<string, object>(BaseUserLogOnEntity.FieldMACAddress, macAddress));
                }
                // userLogOnManager.SetProperty(userEntity.Id, parameters);

                // 可以正常登录了
                result.StatusCode = Status.OK.ToString();
                result.StatusMessage = this.GetStateMessage(result.StatusCode);

                // 13. 登录、重新登录、扮演时的在线状态进行更新
                // userLogOnManager.ChangeOnLine(userEntity.Id);

                errorMark = 25;
                result.UserInfo = this.ConvertToUserInfo(userEntity, userLogOnEntity, validateUserOnly);
                result.UserInfo.IPAddress = ipAddress;
                result.UserInfo.MACAddress = macAddress;
                // 2015-02-03 宋彪 设置 SystemCode
                result.UserInfo.SystemCode = systemCode;
                if (this.SystemEncryptPassword)
                {
                    result.UserInfo.Password = password;
                }

                // 这里是判断用户是否为系统管理员的
                if (!validateUserOnly || this.CheckIsAdministrator)
                {
                    // result.IsAdministrator = IsAdministrator(userEntity);
                    // 在没登录时才发邮件,重复登录的,看是否有必要发邮件?
                    // 向管理员发送登录提醒邮件
                    if (result.UserInfo.IsAdministrator)
                    {
                        // SendLoginMailToAdministrator(result.UserInfo, systemCode);
                    }
                }

                // 14. 记录系统访问日志
                if (result.StatusCode == Status.OK.ToString() && !string.IsNullOrEmpty(systemCode))
                {
                    // 2015-01-06 提高效率，写入缓存
                    // 在服务器上执行这个，在客户端不需要执行这个了
                    if (this.DbHelper.CurrentDbType == CurrentDbType.Oracle && !systemCode.ToUpper().Equals("PDA"))
                    {
                        // 获取每天可查看的淘宝信息等
                        if (GetUserPermission && !string.IsNullOrEmpty(result.UserInfo.Id))
                        {
                            errorMark = 26;
                            using (IDbHelper dbHelper = DbHelperFactory.GetHelper(BaseSystemInfo.UserCenterDbType, BaseSystemInfo.UserCenterDbConnection))
                            {
                                BaseManager manager = new BaseManager(dbHelper, "BASE_USER_IDENTITY_AUDIT");
                                errorMark = 27;
                                DataTable dt = manager.GetDataTable(new KeyValuePair<string, Object>(BaseBusinessLogic.FieldId, result.UserInfo.Id));
                                if (dt != null && dt.Rows.Count > 0)
                                {
                                    string auditStatus = dt.Rows[0]["AUDIT_STATUS"].ToString();
                                    if (string.Equals("已通过", auditStatus, StringComparison.OrdinalIgnoreCase))
                                    {
                                        result.UserInfo.Permission = true;
                                        result.UserInfo.Constraint = dt.Rows[0]["INTERFACE_DAY_LIMIT"].ToString();
                                        result.UserInfo.Message = dt.Rows[0]["AUDIT_IDEA"].ToString();
                                        result.UserInfo.IdentityAuthentication = true;
                                    }
                                    else
                                    {
                                        result.UserInfo.Permission = false;
                                        result.UserInfo.Message = dt.Rows[0]["AUDIT_IDEA"].ToString();
                                    }
                                }
                                else
                                {
                                    result.UserInfo.Permission = false;
                                    result.UserInfo.Message = "未提交身份证照片认证";
                                }

                                // if (string.IsNullOrEmpty(result.UserInfo.Constraint))
                                // {
                                //    result.UserInfo.Constraint = "50";
                                // }
                                // if (string.IsNullOrEmpty(result.UserInfo.Message))
                                // {
                                //    result.UserInfo.Message = "未提交身份证照片认证";
                                // }
                                // 2016-03-09 吉日嘎拉 没有权限的人工设置权限的、再获取权限
                                if (result.UserInfo.Permission == false)
                                {
                                    // errorMark = 28;
                                    var permissionManager = new BasePermissionManager();
                                    result.UserInfo.Permission = permissionManager.CheckPermissionByUser("KJGZ", result.UserInfo.Id, "TaoBao.View");
                                }
                            }
                        }

                        errorMark = 30;
                        BaseUserManager.LogOnStatistics(result.UserInfo);
                    }

                    userLogOnEntity.SystemCode = systemCode;
                    userLogOnEntity.ComputerName = computerName;
                    // 登录成功的日志文件
                    errorMark = 31;
                    BaseLoginLogManager.AddLog(systemCode, result.UserInfo, ipAddress, ipAddressName, macAddress, Status.UserLogOn.ToDescription());

                    if (string.IsNullOrEmpty(result.UserInfo.OpenId))
                    {
                        createOpenId = true;
                    }
                    if (!this.IsLogOnByOpenId)
                    {
                        if (createOpenId)
                        {
                            // TODO 是最近8个小时内登录的，不需要生成新的
                            // userLogOnEntity.LastVisit 
                            errorMark = 32;
                            result.UserInfo.OpenId = userLogOnManager.UpdateVisitDate(userLogOnEntity, createOpenId);
                        }
                        else
                        {
                            errorMark = 33;
                            userLogOnManager.UpdateVisitDate(userLogOnEntity);
                        }
                    }

                    // 这里统一进行缓存保存就可以了、提高效率、把整个用户的登录信息缓存起来，
                    // 这个里面要考虑多个系统的登录区别问题
                    errorMark = 29;
                    Utilities.SetUserInfoCaching(result.UserInfo, this.CachingSystemCode);
                }

                // 宋彪 这里增加登录提醒功能 新线程中处理 确保不影响登录主线程 暂时只在SSO上
                // SendLogOnRemind(result.UserInfo);
            }
            catch (System.Exception ex)
            {
                string writeMessage = "BaseUserManager.LogOnByEntity:发生时间:" + DateTime.Now
                    + System.Environment.NewLine + "errorMark = " + errorMark.ToString()
                    + System.Environment.NewLine + "Message:" + ex.Message
                    + System.Environment.NewLine + "Source:" + ex.Source
                    + System.Environment.NewLine + "StackTrace:" + ex.StackTrace
                    + System.Environment.NewLine + "TargetSite:" + ex.TargetSite
                    + System.Environment.NewLine;

                FileUtil.WriteMessage(writeMessage, BaseSystemInfo.StartupPath + "//Exception//Exception" + DateTime.Now.ToString(BaseSystemInfo.DateFormat) + ".txt");
            }

            return result;
        }
        #endregion

        #region public bool ValidateUser(string userName, string password) 进行密码验证
        /// <summary>
        /// 进行密码验证
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <returns>是否通过验证</returns>
        public bool ValidateUser(string userName, string password)
        {
            // 先按用户名登录
            var dt = this.GetDataTable(new KeyValuePair<string, object>(BaseUserEntity.FieldUserName, userName)
                , new KeyValuePair<string, object>(BaseUserEntity.FieldDeletionStateCode, 0)
                , new KeyValuePair<string, object>(BaseUserEntity.FieldEnabled, 1));

            if (dt.Rows.Count == 0)
            {
                // 若没数据再按工号登录
                dt = this.GetDataTable(new KeyValuePair<string, object>(BaseUserEntity.FieldCode, userName)
                    , new KeyValuePair<string, object>(BaseUserEntity.FieldDeletionStateCode, 0)
                    , new KeyValuePair<string, object>(BaseUserEntity.FieldEnabled, 1));

                /*
                if (result.Rows.Count == 0)
                {
                    // 若没数据再按邮件登录
                    result = this.GetDataTable(new KeyValuePair<string, object>(BaseUserEntity.FieldEmail, userName)
                        , new KeyValuePair<string, object>(BaseUserEntity.FieldDeletionStateCode, 0)
                        , new KeyValuePair<string, object>(BaseUserEntity.FieldEnabled, 1));
                }
                else if (result.Rows.Count == 0)
                {
                    // 若没数据再按手机号码登录
                    result = this.GetDataTable(new KeyValuePair<string, object>(BaseUserEntity.FieldMobile, userName)
                        , new KeyValuePair<string, object>(BaseUserEntity.FieldDeletionStateCode, 0)
                        , new KeyValuePair<string, object>(BaseUserEntity.FieldEnabled, 1));
                }
                else if (result.Rows.Count == 0)
                {
                    // 若没数据再按手机号码登录
                    result = this.GetDataTable(new KeyValuePair<string, object>(BaseUserEntity.FieldTelephone, userName)
                        , new KeyValuePair<string, object>(BaseUserEntity.FieldDeletionStateCode, 0)
                        , new KeyValuePair<string, object>(BaseUserEntity.FieldEnabled, 1));
                }
                */
            }
            BaseUserEntity userEntity = null;
            BaseUserLogOnEntity userLogOnEntity = null;
            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            if (dt.Rows.Count > 1)
            {
                return false;
            }
            else if (dt.Rows.Count == 1)
            {
                // 05. 判断密码，是否允许登录，是否离职是否正确
                userEntity = BaseEntity.Create<BaseUserEntity>(dt.Rows[0]);
                if (!string.IsNullOrEmpty(userEntity.AuditStatus)
                    && userEntity.AuditStatus.EndsWith(AuditStatus.WaitForAudit.ToString())
                    && userLogOnEntity.PasswordErrorCount == 0)
                {
                    return false;
                }
                BaseUserLogOnManager userLogOnManager = new BaseUserLogOnManager(this.DbHelper, this.UserInfo);
                userLogOnEntity = userLogOnManager.GetObject(userEntity.Id);
                // 06. 允许登录时间是否有限制
                if (userLogOnEntity.AllowEndTime != null)
                {
                    userLogOnEntity.AllowEndTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, userLogOnEntity.AllowEndTime.Value.Hour, userLogOnEntity.AllowEndTime.Value.Minute, userLogOnEntity.AllowEndTime.Value.Second);
                }
                if (userLogOnEntity.AllowStartTime != null)
                {
                    userLogOnEntity.AllowStartTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, userLogOnEntity.AllowStartTime.Value.Hour, userLogOnEntity.AllowStartTime.Value.Minute, userLogOnEntity.AllowStartTime.Value.Second);
                    if (DateTime.Now < userLogOnEntity.AllowStartTime)
                    {
                        return false;
                    }
                }
                if (userLogOnEntity.AllowEndTime != null)
                {
                    if (DateTime.Now > userLogOnEntity.AllowEndTime)
                    {
                        return false;
                    }
                }

                // 07. 锁定日期是否有限制
                if (userLogOnEntity.LockStartDate != null)
                {
                    if (DateTime.Now > userLogOnEntity.LockStartDate)
                    {
                        if (userLogOnEntity.LockEndDate == null || DateTime.Now < userLogOnEntity.LockEndDate)
                        {
                            return false;
                        }
                    }
                }
                if (userLogOnEntity.LockEndDate != null)
                {
                    if (DateTime.Now < userLogOnEntity.LockEndDate)
                    {
                        return false;
                    }
                }

                // 03. 系统是否采用了密码加密策略？
                if (BaseSystemInfo.ServerEncryptPassword)
                {
                    password = this.EncryptUserPassword(password);
                }

                // 11. 密码是否正确(null 与空看成是相等的)
                if (!(string.IsNullOrEmpty(userLogOnEntity.UserPassword) && string.IsNullOrEmpty(password)))
                {
                    bool userPasswordOK = true;
                    // 用户密码是空的
                    if (string.IsNullOrEmpty(userLogOnEntity.UserPassword))
                    {
                        // 但是输入了不为空的密码
                        if (!string.IsNullOrEmpty(password))
                        {
                            userPasswordOK = false;
                        }
                    }
                    else
                    {
                        // 用户的密码不为空，但是用户是输入了密码
                        if (string.IsNullOrEmpty(password))
                        {
                            userPasswordOK = false;
                        }
                        else
                        {
                            // 再判断用户的密码与输入的是否相同
                            userPasswordOK = userLogOnEntity.UserPassword.Equals(password);
                        }
                    }
                    // 用户的密码不相等
                    if (!userPasswordOK)
                    {
                        // 这里更新用户连续输入错误密码次数
                        return false;
                    }
                }
            }
            return true;
        }
        #endregion

        /// <summary>
        /// 判断用户是否已经登录了？
        /// </summary>
        /// <param name="userInfo">用户</param>
        /// <returns>是否已经登录了</returns>
        public bool UserIsLogOn(BaseUserInfo userInfo)
        {
            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>(BaseUserLogOnEntity.FieldId, userInfo.Id));
            parameters.Add(new KeyValuePair<string, object>(BaseUserLogOnEntity.FieldOpenId, userInfo.OpenId));
            BaseManager manager = new BaseManager(userInfo, BaseUserLogOnEntity.TableName);
            return manager.Exists(parameters);
        }
    }
}