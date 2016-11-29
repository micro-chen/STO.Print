//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd. 
//-----------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Caching;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace DotNet.Business
{
    using DotNet.Model;
    using DotNet.Utilities;

    /// <summary>
    /// 登录功能相关部分
    /// </summary>
    public partial class Utilities
    {
        /// <summary>
        /// 数据库连接串，改进性能只读取一次就可以了
        /// </summary>
        public static readonly string BusinessDbConnection = ConfigurationManager.AppSettings["BusinessDbConnection"];

        /// <summary>
        /// 用户中心数据库连接
        /// </summary>
        public static readonly string UserCenterDbConnection = ConfigurationManager.AppSettings["UserCenterDbConnection"];

        /// <summary>
        /// Cookie 名称
        /// </summary>
        // public static string CookieName = "Hairihan";
        public static string CookieName = ConfigurationManager.AppSettings["CookieName"];

        // public static string CookieDomain = ".Hairihan.com";
        public static string CookieDomain = ConfigurationManager.AppSettings["CookieDomain"];

        /// <summary>
        /// Cookie 用户名
        /// </summary>
        public static string CookieUserName = "UserName";
        /// <summary>
        /// Cookie 密码
        /// </summary>
        public static string CookiePassword = "Password";

        /// <summary>
        /// Session 名称
        /// </summary>
        public static string SessionName = "UserInfo";


        #region public static List<BaseModuleEntity> GetUserPermissionList(BaseUserInfo userInfo, string userId = null) 获用户拥有的操作权限列表
        /// <summary>
        /// 获用户拥有的操作权限列表
        /// </summary>
        /// <param name="result">当前操作员</param>
        /// <param name="userId">用户主键</param>
        public static List<BaseModuleEntity> GetUserPermissionList(BaseUserInfo userInfo, string userId = null)
        {
            if (string.IsNullOrEmpty(userId))
            {
                userId = userInfo.Id;
            }
            string cacheKey = "P" + userId;
            List<BaseModuleEntity> entityList = null;
            if (HttpContext.Current.Session == null || HttpContext.Current.Cache[cacheKey] == null)
            {
                // 这里是控制用户并发的，减少框架等重复读取数据库的效率问题
                lock (BaseSystemInfo.UserLock)
                {
                    if (HttpContext.Current.Session == null || HttpContext.Current.Cache[cacheKey] == null)
                    {
                        // 这个是默认的系统表名称
                        DotNetService dotNetService = new DotNetService();
                        entityList = dotNetService.PermissionService.GetPermissionListByUser(userInfo, userInfo.SystemCode, userInfo.Id, userInfo.CompanyId, true);
                        // 设置过期时间为8个小时，第2天若有不正常的自动就可以正常了
                        HttpContext.Current.Cache.Add(cacheKey, entityList, null, DateTime.Now.AddHours(8), TimeSpan.Zero, CacheItemPriority.Normal, null);
                    }
                }
            }
            entityList = HttpContext.Current.Cache[cacheKey] as List<BaseModuleEntity>;
            return entityList;
        }
        #endregion

        //
        // 当前操作员权限相关检查函数
        //

        #region public static bool UserIsAdministrator() 判断当前用户是否为系统管理员
        /// <summary>
        /// 判断当前用户是否为系统管理员
        /// </summary>
        /// <returns>是否</returns>
        public static bool UserIsAdministrator()
        {
            bool result = false;
            if (UserIsLogOn())
            {
                BaseUserInfo userInfo = (BaseUserInfo)HttpContext.Current.Session["UserInfo"];
                result = userInfo.IsAdministrator;
            }
            return result;
        }
        #endregion

        #region public static bool CheckIsLogOn(string accessDenyUrl = null) 检查是否已登录
        /// <summary>
        /// 检查是否已登录
        /// </summary>
        public static bool CheckIsLogOn(string accessDenyUrl = null)
        {
            if (!UserIsLogOn())
            {
                if (string.IsNullOrEmpty(accessDenyUrl))
                {
                    // 获取设置的当点登录页面
                    if (ConfigurationManager.AppSettings["SSO"] != null)
                    {
                        Utilities.UserNotLogOn = ConfigurationManager.AppSettings["SSO"];
                    }
                    // 获取当前页面

                    string url = HttpUtility.UrlEncode(HttpContext.Current.Request.Url.ToString());
                    //string url = Server.UrlEncode(HttpContext.Current.Request.Url.ToString()); 

                    string js = @"<Script language='JavaScript'>
                    top.window.location.replace('{0}');
                  </Script>";
                    js = string.Format(js, Utilities.UserNotLogOn + "?ReturnURL=" + url);
                    HttpContext.Current.Response.Write(js);
                    // 这里需要结束输出了，防止意外发生。
                    HttpContext.Current.Response.End();
                }
                else
                {
                    HttpContext.Current.Response.Redirect(accessDenyUrl);
                }
                return false;
            }
            return true;
        }
        #endregion

        #region public static void CheckIsAdministrator() 检查判断当前用户是否为系统管理员
        /// <summary> 
        /// 检查判断当前用户是否为系统管理员
        /// </summary>
        public static void CheckIsAdministrator()
        {
            // 检查是否已登录
            Utilities.CheckIsLogOn();
            // 是否系统管理员
            if (!UserIsAdministrator())
            {
                HttpContext.Current.Response.Redirect(Utilities.UserIsNotAdminPage);
            }
        }
        #endregion

        //
        // 一 用户注册部分 
        //

        #region private static string GetAfterUserRegisterBody(BaseUserEntity userEntity) 用户注册之后，给用户发的激活账户的邮件
        /// <summary>
        /// 用户注册之后，给用户发的激活账户的邮件
        /// </summary>
        /// <param name="userEntity">用户实体</param>
        /// <returns>邮件主题</returns>
        private static string GetAfterUserRegisterBody(BaseUserEntity userEntity)
        {
            // openId,有空时改进
            StringBuilder htmlBody = new StringBuilder();
            htmlBody.Append("<body style=\"font-size:10pt\">");
            htmlBody.Append("<div style=\"font-size:10pt; font-weight:bold\">尊敬的用户 " + userEntity.UserName + " 您好：</div>");
            htmlBody.Append("<br>");
            htmlBody.Append("<div>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 请点击此处激活您的账号：<a href='http://www.cdpsn.org.cn/Modules/User/Activation.aspx?Id=" + userEntity.Id + "'><font size=\"3\" color=\"#6699cc\">" + userEntity.UserName + "</font></a></div>");
            htmlBody.Append("<br>");
            htmlBody.Append("<div>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 也可以直接在url中输入网址下面的网址 http://www.cdpsn.org.cn/Modules/User/Activation.aspx?Id=" + userEntity.Id + " 激活账户</div>");
            htmlBody.Append("<br>");
            htmlBody.Append("<div>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 如有任何疑问，欢迎致浙大网新易盛客服热线：0571-88935961，我们将热情为您解答。</div>");
            htmlBody.Append("<br>");
            htmlBody.Append("<div style=\"text-align:center\">浙大网新易盛 用户服务中心</div>");
            htmlBody.Append("<div style=\"text-align:center\">" + System.DateTime.Now.Year + "年" + System.DateTime.Now.Month + "月" + System.DateTime.Now.Day + "日</div></body>");
            return htmlBody.ToString();
        }
        #endregion

        #region public static bool AfterUserRegister(BaseUserEntity userEntity) 用户注册之后，给用户发的激活账户的邮件
        /// <summary>
        /// 用户注册之后，给用户发的激活账户的邮件
        /// </summary>
        /// <param name="userEntity">用户实体</param>
        /// <returns>成功发送邮件</returns>
        public static bool AfterUserRegister(BaseUserEntity userEntity)
        {
            bool result = false;
            BaseUserInfo userInfo = null;
            using (IDbHelper dbHelper = DbHelperFactory.GetHelper(BaseSystemInfo.UserCenterDbType, BaseSystemInfo.UserCenterDbConnection))
            {
                try
                {
                    using (System.Net.Mail.MailMessage mailMessage = new System.Net.Mail.MailMessage())
                    {
                        // 接收人邮箱地址
                        // mailMessage.To.Add(new System.Net.Mail.MailAddress(userEntity.Email));
                        mailMessage.Body = GetAfterUserRegisterBody(userEntity);
                        mailMessage.From = new System.Net.Mail.MailAddress("xlf8255363@163.com", "中国残疾人服务网");
                        mailMessage.BodyEncoding = Encoding.GetEncoding("GB2312");
                        mailMessage.Subject = "中国残疾人服务网 新密码。";
                        mailMessage.IsBodyHtml = true;
                        System.Net.Mail.SmtpClient smtpclient = new System.Net.Mail.SmtpClient("SMTP.163.COM", 25);
                        smtpclient.Credentials = new System.Net.NetworkCredential("xlf8255363@163.com", "youxikuang");
                        smtpclient.EnableSsl = false;
                        smtpclient.Send(mailMessage);
                        result = true;
                    }
                }
                catch (System.Exception exception)
                {
                    // 若有异常，应该需要保存异常信息
                    BaseExceptionManager.LogException(dbHelper, userInfo, exception);
                    result = false;
                }
                finally
                {
                }
            }
            return result;
        }
        #endregion

        //
        // 二 判断用户是否已登录部分
        //

        #region public static bool UserIsLogOn() 判断用户是否已登录
        /// <summary>
        /// 判断用户是否已登录
        /// </summary>
        /// <returns>已登录</returns>
        public static bool UserIsLogOn()
        {
            // 先判断 Session 里是否有用户，若没有检查Cookie是没错
            if (HttpContext.Current.Session[SessionName] == null)
            {
                // 检查是否有Cookie？若密码有错，这里就无法登录成功了
                CheckCookie();
            }
            else
            {
                // 这里还需要检查用户在线过程中是否有设置被修改过，例如密码被修改过
                if (BaseSystemInfo.CheckPasswordStrength)
                {
                    // 密码不对，要退出，修改了密码，不能继续在线上了。
                    // 若是IP地址变了，也需要重新登录
                    // 检查是否有Cookie？，其实自己修改过密码，没必要重新登录的，所以需要检查 UserInfo.OpenId 是否有变过
                    CheckOpenId();
                }
            }
            // 若用户没，就是登录不成功了
            if (HttpContext.Current.Session != null && HttpContext.Current.Session[SessionName] != null)
            {
                return true;
            }
            return false;
        }
        #endregion

        #region public static void SetSession(BaseUserInfo userInfo)
        /// <summary>
        /// 保存Session
        /// </summary>
        /// <param name="userInfo">当前用户</param>
        public static void SetSession(BaseUserInfo userInfo)
        {
            // 检查是否有效用户
            if (userInfo != null && !string.IsNullOrEmpty(userInfo.Id))
            {
                // if (result.RoleId.Length == 0)
                // {
                //     result.RoleId = DefaultRole.OnlyOwnData.ToString();
                // }
                // 操作员
                if (HttpContext.Current.Session != null)
                {
                    // HttpContext.Current.Session[SessionName] = result;
                    HttpContext.Current.Session[SessionName] = new CurrentUserInfo(userInfo);
                }
            }
            else
            {
                HttpContext.Current.Session.Remove(SessionName);
            }
        }
        #endregion

        #region public static BaseUserInfo UserInfo 获取用户信息
        /// <summary>
        /// 获取用户信息
        /// </summary>
        public static BaseUserInfo UserInfo
        {
            get
            {
                return GetUserInfo();
            }
        }
        #endregion



        //
        // 三 判断当前的CheckCookie内容情况
        //

        #region public static BaseUserInfo CheckCookie()
        /// <summary>
        /// 检查当前的Cookie内容
        /// </summary>
        public static BaseUserInfo CheckCookie()
        {
            // 这里要考虑，从来没登录过的情况
            BaseUserInfo userInfo = GetUserCookie();
            if (userInfo != null && !string.IsNullOrEmpty(userInfo.IPAddress) && !userInfo.IPAddress.Equals(GetUserInfo().IPAddress))
            {
                userInfo = null;
            }
            else
            {
                // 这里应该再判断是否密码过期，用户过期等等，不只是用Cookie就可以了
                userInfo = CheckCookie(HttpContext.Current.Request);
                // 若是用户的Cookie的IPAddress与当前的IPAddress不一样了，那就是IP地址变过了
            }
            SetSession(userInfo);
            return userInfo;
        }
        #endregion

        #region public static BaseUserInfo CheckOpenId()
        /// <summary>
        /// 检查当前的OpenId内容，
        /// 若有密码等被修改时，会踢出前面的用户，密码不正确了，就会踢出用户
        /// </summary>
        public static BaseUserInfo CheckOpenId()
        {
            BaseUserInfo userInfo = GetUserInfo();
            // 已经登录的用户IP地址是否有变化了
            if (!string.IsNullOrEmpty(userInfo.OpenId))
            {
                BaseUserManager userManager = new BaseUserManager(userInfo);
                userInfo = userManager.LogOnByOpenId(userInfo.OpenId, userInfo.IPAddress).UserInfo;
                SetSession(userInfo);
            }
            return userInfo;
        }
        #endregion

        #region public static HttpCookie GetCookie(HttpRequest httpRequest) 获取Cookies
        /// <summary>
        /// 获取Cookies
        /// </summary>
        /// <param name="httpRequest">客户端请求</param>
        /// <returns></returns>
        public static HttpCookie GetCookie(HttpRequest httpRequest)
        {
            return httpRequest.Cookies[CookieName];
        }
        #endregion

        #region public static BaseUserInfo CheckCookie(HttpRequest httpRequest)
        /// <summary>
        /// 检查当前的Cookie内容
        /// </summary>
        /// <param name="httpRequest">当前页</param>
        /// <returns>Cookie内容</returns>
        public static BaseUserInfo CheckCookie(HttpRequest httpRequest)
        {
            BaseUserInfo userInfo = null;
            // 取得cookie的保存信息
            HttpCookie httpCookie = httpRequest.Cookies[Utilities.CookieName];
            if (httpCookie != null)
            {
                // 读取用户名
                if (!string.IsNullOrEmpty(httpCookie.Values[Utilities.CookieUserName]))
                {
                    // 2012.11.03 Pcsky修改，解决中文用户名无法自动登录的问题。
                    string username = httpCookie.Values[Utilities.CookieUserName].ToString();
                    username = HttpContext.Current.Server.UrlDecode(username);

                    if (string.IsNullOrEmpty(BaseSystemInfo.UserCenterDbConnection))
                    {
                        // 若没有能连接数据库，就直接从Cookie读取用户，这里应该重新定位一下用户信息那里，判断用户是否有效等等，密码是否修改了等等。
                        userInfo = GetUserCookie();
                    }
                    else
                    {
                        if (BaseSystemInfo.RememberPassword)
                        {
                            //if (BaseSystemInfo.CheckIPAddress)
                            //{
                            //    if (!string.IsNullOrEmpty(httpCookie.Values["IPAddress"]))
                            //    {
                            //        string ipAddress = httpCookie.Values["IPAddress"];
                            //        // 若IP地址变了，也需要重新登录，从数据库获取用户的登录信息
                            //        if (!string.IsNullOrEmpty(result.IPAddress))
                            //        {
                            //            if (!ipAddress.Equals(result.IPAddress))
                            //            {
                            //                result = null;
                            //                return result;
                            //            }
                            //        }
                            //    }
                            //}

                            // 读取密码
                            string password = string.Empty;
                            if (!string.IsNullOrEmpty(httpCookie.Values[Utilities.CookiePassword]))
                            {
                                password = httpCookie.Values[Utilities.CookiePassword].ToString();
                                password = Decrypt(password);
                            }

                            // 2013-02-20 吉日嘎拉
                            // 进行登录，这里是靠重新登录获取 Cookie，这里其实是判断密码是不是过期了，其实这里openId登录也可以
                            userInfo = LogOn(username, password, false);
                        }
                    }
                }
            }
            return userInfo;
        }
        #endregion

        //public static BaseUserInfo GetUserCookie()
        //{
        //    return GetUserCookie();
        //}

        #region public static BaseUserInfo GetUserCookie() 获取用户相应的Cookies信息
        /// <summary>
        /// 获取用户相应的Cookies信息
        /// </summary>
        /// <returns></returns>
        public static BaseUserInfo GetUserCookie()
        {
            BaseUserInfo userInfo = null;
            HttpRequest httpRequest = HttpContext.Current.Request;
            HttpCookie httpCookie = httpRequest.Cookies[Utilities.CookieName];
            if (httpCookie != null)
            {
                userInfo = new BaseUserInfo();
                userInfo.Id = httpCookie.Values["Id"];
                userInfo.OpenId = httpCookie.Values["OpenId"];

                /*
                 *  2013-02-20 吉日嘎拉
                 *  若有安全要求，以下信息都可以不从 Cookies 里读取，就是读取了，也重新从后台覆盖，若没有安全要求，才可以从 Cookies 读取
                 *  其实也是为了提高系统的效率，这些信息没反复从后台读取
                 */

                userInfo.RealName = HttpUtility.UrlDecode(httpCookie.Values["RealName"]);
                userInfo.UserName = HttpUtility.UrlDecode(httpCookie.Values[Utilities.CookieUserName]);
                userInfo.Code = httpCookie.Values["Code"];

                userInfo.ServicePassword = httpCookie.Values["ServicePassword"];
                userInfo.ServiceUserName = httpCookie.Values["ServiceUserName"];

                //result.TargetUserId = httpCookie.Values["TargetUserId"];
                //result.StaffId = httpCookie.Values["StaffId"];

                userInfo.CompanyCode = httpCookie.Values["CompanyCode"];
                if (!string.IsNullOrEmpty(httpCookie.Values["CompanyId"]))
                {
                    userInfo.CompanyId = httpCookie.Values["CompanyId"];
                }
                else
                {
                    userInfo.CompanyId = null;
                }
                userInfo.CompanyName = HttpUtility.UrlDecode(httpCookie.Values["CompanyName"]);

                if (!string.IsNullOrEmpty(httpCookie.Values["DepartmentId"]))
                {
                    userInfo.DepartmentId = httpCookie.Values["DepartmentId"];
                }
                else
                {
                    userInfo.DepartmentId = null;
                }
                userInfo.DepartmentCode = httpCookie.Values["DepartmentCode"];
                userInfo.DepartmentName = HttpUtility.UrlDecode(httpCookie.Values["DepartmentName"]);

                //if (!string.IsNullOrEmpty(httpCookie.Values["WorkgroupId"]))
                //{
                //    result.WorkgroupId = httpCookie.Values["WorkgroupId"];
                //}
                //else
                //{
                //    result.WorkgroupId = null;
                //}
                //result.WorkgroupCode = httpCookie.Values["WorkgroupCode"];
                //result.WorkgroupName = HttpUtility.UrlDecode(httpCookie.Values["WorkgroupName"]);

                if (!string.IsNullOrEmpty(httpCookie.Values["IsAdministrator"]))
                {
                    userInfo.IsAdministrator = httpCookie.Values["IsAdministrator"].ToString().Equals(true.ToString());
                }
                //if (!string.IsNullOrEmpty(httpCookie.Values["SecurityLevel"]))
                //{
                //    result.SecurityLevel = int.Parse(httpCookie.Values["SecurityLevel"]);
                //}
                //result.IPAddress = httpCookie.Values["IPAddress"];
                //result.CurrentLanguage = httpCookie.Values["CurrentLanguage"];
                //result.Themes = httpCookie.Values["Themes"];

                // 只要出错，应该删除Cookie，重新跳转到登录页面才正确
                if (string.IsNullOrEmpty(userInfo.Id))
                {
                    userInfo = null;
                }
            }
            return userInfo;
        }
        #endregion

        #region public static void SaveCookie(string userName, string password)
        /// <summary>
        /// 保存Cookie
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        public static void SaveCookie(string userName, string password)
        {
            // userName = Encrypt(userName);
            password = Encrypt(password);
            HttpCookie httpCookie = new HttpCookie(Utilities.CookieName);
            if (!string.IsNullOrEmpty(CookieDomain))
            {
                httpCookie.Domain = CookieDomain;
            }
            httpCookie.Values[Utilities.CookieUserName] = userName;
            if (BaseSystemInfo.RememberPassword)
            {
                httpCookie.Values[Utilities.CookiePassword] = password;
            }
            // 设置过期时间为30天，若需要关闭掉浏览器就要退出程序，这下面的2行代码注释掉就可以了
            if (BaseSystemInfo.CookieExpires != 0)
            {
                DateTime dateTime = DateTime.Now;
                httpCookie.Expires = dateTime.AddDays(BaseSystemInfo.CookieExpires);
            }
            HttpContext.Current.Response.Cookies.Add(httpCookie);
        }
        #endregion

        #region public static void SaveCookie(BaseUserInfo userInfo, bool allInfo = false)
        /// <summary>
        /// 保存Cookie
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        /// <param name="allInfo">是否保存所有的信息</param>
        public static void SaveCookie(BaseUserInfo userInfo, bool allInfo = false)
        {
            string password = Encrypt(userInfo.Password);
            HttpCookie httpCookie = new HttpCookie(Utilities.CookieName);
            if (!string.IsNullOrEmpty(CookieDomain))
            {
                httpCookie.Domain = CookieDomain;
            }
            httpCookie.Values[Utilities.CookieUserName] = HttpUtility.UrlEncode(userInfo.UserName);
            if (BaseSystemInfo.RememberPassword)
            {
                httpCookie.Values[Utilities.CookiePassword] = password;
            }
            httpCookie.Values["Id"] = userInfo.Id;
            httpCookie.Values["OpenId"] = userInfo.OpenId;
            httpCookie.Values["Code"] = userInfo.Code;
            httpCookie.Values["UserName"] = HttpUtility.UrlEncode(userInfo.UserName);
            httpCookie.Values["RealName"] = HttpUtility.UrlEncode(userInfo.RealName);
            if (HttpContext.Current.Response.Charset.ToLower().Equals("gb2312"))
            {
                System.Text.Encoding encoding = System.Text.Encoding.GetEncoding("gb2312");
                httpCookie.Values["RealName"] = HttpUtility.UrlEncode(userInfo.RealName, encoding);
            }
            httpCookie.Values["IsAdministrator"] = userInfo.IsAdministrator.ToString();
            httpCookie.Values["IPAddress"] = userInfo.IPAddress;

            /*
            <globalization requestEncoding="gb2312" responseEncoding="gb2312"/>

            // 写入cookies时
            HttpCookie cookie = new HttpCookie("bbslogin");
            System.Text.Encoding enc = System.Text.Encoding.GetEncoding("gb2312");
            cookie["userid"] = HttpUtility.UrlEncode(userid, enc);
            cookie["userpassword"] = HttpUtility.UrlEncode(a_userpassword, enc);

            // 读取cookies时
            t = 获取的中文cookies值;
            System.Text.Encoding enc = System.Text.Encoding.GetEncoding("gb2312");
            userid = HttpUtility.UrlDecode(cookie["userid"], enc);
            userpassword = HttpUtility.UrlDecode(cookie["userpassword"].ToString(), enc);
            */

            if (allInfo)
            {
                httpCookie.Values["ServiceUserName"] = userInfo.ServiceUserName;
                httpCookie.Values["ServicePassword"] = userInfo.ServicePassword;
                
                if (userInfo.CompanyId != null)
                {
                    httpCookie.Values["CompanyId"] = userInfo.CompanyId;
                }
                else
                {
                    httpCookie.Values["CompanyId"] = null;
                }
                httpCookie.Values["CompanyCode"] = userInfo.CompanyCode;
                httpCookie.Values["CompanyName"] = HttpUtility.UrlEncode(userInfo.CompanyName);
                httpCookie.Values["DepartmentCode"] = userInfo.DepartmentCode;
                if (userInfo.DepartmentId != null)
                {
                    httpCookie.Values["DepartmentId"] = userInfo.DepartmentId;
                }
                else
                {
                    httpCookie.Values["DepartmentId"] = null;
                }
                httpCookie.Values["DepartmentName"] = HttpUtility.UrlEncode(userInfo.DepartmentName);

                //if (result.WorkgroupId != null)
                //{
                //    httpCookie.Values["WorkgroupId"] = result.WorkgroupId;
                //}
                //else
                //{
                //    httpCookie.Values["WorkgroupId"] = null;
                //}
                // httpCookie.Values["WorkgroupCode"] = result.WorkgroupCode;
                // httpCookie.Values["WorkgroupName"] = HttpUtility.UrlEncode(result.WorkgroupName);

                // httpCookie.Values["SecurityLevel"] = result.SecurityLevel.ToString();
                // httpCookie.Values["StaffId"] = userInfo.StaffId;
                // httpCookie.Values["TargetUserId"] = result.TargetUserId;
                // httpCookie.Values["CurrentLanguage"] = result.CurrentLanguage;
            }

            // httpCookie.Values["Themes"] = result.Themes;
            // 设置过期时间为30天，若需要关闭掉浏览器就要退出程序，这下面的2行代码注释掉就可以了
            if (BaseSystemInfo.CookieExpires != 0)
            {
                DateTime dateTime = DateTime.Now;
                httpCookie.Expires = dateTime.AddDays(BaseSystemInfo.CookieExpires);
            }
            HttpContext.Current.Response.Cookies.Add(httpCookie);
        }
        #endregion

        #region public static int GetUserId() 用户主键
        /// <summary>
        /// 用户主键
        /// </summary>
        /// <returns></returns>
        public static int GetUserId()
        {
            // 如果当前用户登录
            DotNet.Utilities.BaseUserInfo userInfo = Utilities.CheckCookie();
            if (userInfo != null && !string.IsNullOrEmpty(userInfo.Id))
            {
                return int.Parse(userInfo.Id);
            }
            return -1;
        }
        #endregion

        //
        // 四 用OpenId登录部分
        //

        #region public static UserLogOnResult LogOnByOpenId(string openId, string ipAddress = null)
        /// <summary>
        /// 验证用户
        /// </summary>
        /// <param name="openId">当点登录识别码</param>
        /// <param name="transparent">是否使用了代理</param>
        public static UserLogOnResult LogOnByOpenId(string openId, bool transparent = false, bool useCaching = true, bool useDataBase = true, bool useUserCenterHost = true)
        {
            // 统一的登录服务
            UserLogOnResult userLogOnResult = null;

            if (useCaching)
            {
                // 先从缓存活取用户是否在？若在缓存里已经在了，就不需要再登录了，直接登录就可以了。
                BaseUserInfo result = GetUserInfoCaching(openId);
                if (result != null)
                {
                    userLogOnResult = new UserLogOnResult();
                    userLogOnResult.UserInfo = result;
                    userLogOnResult.StatusCode = Status.OK.ToString();
                    return userLogOnResult;
                }
            }

            if (useDataBase)
            {
            }

            if (useUserCenterHost)
            {
                // DotNetService dotNetService = new DotNetService();
                // result = dotNetService.LogOnService.LogOnByOpenId(GetUserInfo(), openId);
                string url = BaseSystemInfo.UserCenterHost + "/UserCenterV42/LogOnService.ashx";
                WebClient webClient = new WebClient();
                NameValueCollection postValues = new NameValueCollection();
                postValues.Add("function", "LogOnByOpenId");
                postValues.Add("userInfo", BaseSystemInfo.UserInfo.Serialize());
                postValues.Add("systemCode", BaseSystemInfo.SystemCode);
                // 若ip地址没有传递过来，就获取BS客户端ip地址
                postValues.Add("ipAddress", Utilities.GetIPAddress(transparent));
                // BS 登录容易引起混乱，
                // postValues.Add("macAddress", BaseSystemInfo.UserInfo.MACAddress);
                postValues.Add("securityKey", BaseSystemInfo.SecurityKey);
                postValues.Add("openId", openId);
                // 向服务器发送POST数据
                byte[] responseArray = webClient.UploadValues(url, postValues);
                string response = Encoding.UTF8.GetString(responseArray);
                if (!string.IsNullOrEmpty(response))
                {
                    JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                    userLogOnResult = javaScriptSerializer.Deserialize<UserLogOnResult>(response);
                }
                // 检查身份
                if (userLogOnResult != null && userLogOnResult.StatusCode.Equals(Status.OK.ToString()))
                {
                    LogOn(userLogOnResult.UserInfo, false);
                }
            }
            return userLogOnResult;
        }
        #endregion

        //
        // 五 用用户名密码登录部分
        //

        #region public static BaseUserInfo LogOn(string userName, string password, bool checkUserPassword = true)
        /// <summary>
        /// 验证用户
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="checkUserPassword">是否要检查用户密码</param>
        public static BaseUserInfo LogOn(string userName, string password, bool checkUserPassword = true)
        {
            BaseUserManager userManager = new BaseUserManager(Utilities.GetUserInfo());
            return userManager.LogOnByUserName(userName, password, string.Empty, false, "Base", HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"], string.Empty, null, checkUserPassword).UserInfo;
        }
        #endregion

        #region public static BaseUserInfo LogOn(string userName, string password, string openId, string permissionCode, bool persistCookie, bool formsAuthentication, out string statusCode, out string statusMessage)
        /// <summary>
        /// 验证用户
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="单点登录标识">openId</param>
        /// <param name="permissionCode">权限编号</param>
        /// <param name="persistCookie">是否保存密码</param>
        /// <param name="formsAuthentication">表单验证，是否需要重定位</param>
        /// <param name="statusCode"></param>
        /// <param name="statusMessage"></param>
        /// <returns></returns>
        public static BaseUserInfo LogOn(string userName, string password, string openId, string permissionCode, string ipAddress, string systemCode, bool persistCookie, bool formsAuthentication)
        {
            // 统一的登录服务
            string taskId = System.Guid.NewGuid().ToString("N");
            DotNetService dotNetService = new DotNetService();
            BaseUserInfo userInfo = Utilities.GetUserInfo();
            if (!string.IsNullOrEmpty(ipAddress))
            {
                userInfo.IPAddress = ipAddress;
            }
            if (!string.IsNullOrEmpty(systemCode))
            {
                userInfo.SystemCode = systemCode;
            }
            if (!string.IsNullOrEmpty(userInfo.IPAddress))
            {
                userInfo.IPAddress = GetIPAddress();
            }

            UserLogOnResult userLogOnResult = dotNetService.LogOnService.UserLogOn(taskId, userInfo, userName, password, openId, false);
            // 检查身份
            if (userLogOnResult != null && userLogOnResult.StatusCode.Equals(Status.OK.ToString()))
            {
                bool isAuthorized = true;
                // 用户是否有哪个相应的权限
                if (!string.IsNullOrEmpty(permissionCode))
                {
                    isAuthorized = dotNetService.PermissionService.IsAuthorized(userLogOnResult.UserInfo, permissionCode, null);
                }
                // 有相应的权限才可以登录
                if (isAuthorized)
                {
                    if (persistCookie)
                    {
                        // 相对安全的方式保存登录状态
                        // SaveCookie(userName, password);
                        // 内部单点登录方式
                        SaveCookie(userLogOnResult.UserInfo);
                    }
                    else
                    {
                        RemoveUserCookie();
                    }
                    LogOn(userLogOnResult.UserInfo, formsAuthentication);
                }
                else
                {
                    userLogOnResult.StatusCode = Status.LogOnDeny.ToString();
                    userLogOnResult.StatusMessage = "访问被拒绝、您的账户没有后台管理访问权限。";
                }
            }
            return userLogOnResult.UserInfo;
        }
        #endregion

        #region public static BaseUserInfo LogOnByCompany(string companyName, string userName, string password, string openId, string permissionCode, string ipAddress, string systemCode, bool persistCookie, bool formsAuthentication)
        /// <summary>
        /// 验证用户
        /// </summary>
        /// <param name="companyName">公司</param>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="单点登录标识">openId</param>
        /// <param name="permissionCode">权限编号</param>
        /// <param name="persistCookie">是否保存密码</param>
        /// <param name="formsAuthentication">表单验证，是否需要重定位</param>
        /// <returns></returns>
        public static BaseUserInfo LogOnByCompany(string companyName, string userName, string password, string openId, string permissionCode, string ipAddress, string systemCode, bool persistCookie, bool formsAuthentication)
        {
            string taskId = System.Guid.NewGuid().ToString("N");
            // 统一的登录服务
            BaseUserInfo userInfo = Utilities.GetUserInfo();
            if (!string.IsNullOrEmpty(ipAddress))
            {
                userInfo.IPAddress = ipAddress;
            }
            if (!string.IsNullOrEmpty(systemCode))
            {
                userInfo.SystemCode = systemCode;
            }
            if (!string.IsNullOrEmpty(userInfo.IPAddress))
            {
                userInfo.IPAddress = GetIPAddress();
            }
            DotNetService dotNetService = new DotNetService();
            UserLogOnResult userLogOnResult = dotNetService.LogOnService.LogOnByCompany(taskId, userInfo, companyName, userName, password, openId, false);
            // 检查身份
            if (userLogOnResult.StatusCode.Equals(Status.OK.ToString()))
            {
                bool isAuthorized = true;
                // 用户是否有哪个相应的权限
                if (!string.IsNullOrEmpty(permissionCode))
                {
                    isAuthorized = dotNetService.PermissionService.IsAuthorized(userInfo, permissionCode, null);
                }
                // 有相应的权限才可以登录
                if (isAuthorized)
                {
                    if (persistCookie)
                    {
                        // 相对安全的方式保存登录状态
                        // SaveCookie(userName, password);
                        // 内部单点登录方式
                        SaveCookie(userLogOnResult.UserInfo);
                    }
                    else
                    {
                        RemoveUserCookie();
                    }
                    LogOn(userLogOnResult.UserInfo, formsAuthentication);
                }
                else
                {
                    userLogOnResult.StatusCode = Status.LogOnDeny.ToString();
                    userLogOnResult.StatusMessage = "访问被拒绝、您的账户没有后台管理访问权限。";
                }
            }
            return userLogOnResult.UserInfo;
        }
        #endregion

        #region public static void LogOn(BaseUserInfo userInfo, bool formsAuthentication = false)
        /// <summary>
        /// 验证用户
        /// </summary>
        /// <param name="result">登录</param>
        /// <param name="redirectFrom">是否需要重定位</param>
        public static void LogOn(BaseUserInfo userInfo, bool formsAuthentication = false)
        {
            // 检查身份
            if ((userInfo != null) && (!string.IsNullOrEmpty(userInfo.Id)))
            {
                SetSession(userInfo);
                if (formsAuthentication)
                {
                    FormsAuthentication.RedirectFromLoginPage(CookieName, false);
                }
            }
            else
            {
                Logout(userInfo);
            }
        }
        #endregion


        //
        // 六 安全退出部分
        //

        #region public static void RemoveUserCookie()
        /// <summary>
        /// 清空cookie
        /// </summary>
        public static void RemoveUserCookie()
        {
            // 清空cookie
            HttpCookie httpCookie = new HttpCookie(CookieName);
            // 设置过期时间，1秒钟后删除cookie就不对了,得时间很长才可以服务器时间与客户时间的差距得考虑好
            httpCookie.Expires = new DateTime(1978, 05, 19);
            if (!string.IsNullOrEmpty(CookieDomain))
            {
                httpCookie.Domain = CookieDomain;
            }
            httpCookie.Values.Clear();
            HttpContext.Current.Response.Cookies.Add(httpCookie);
        }
        #endregion

        #region public static void RemoveUserSession()
        /// <summary>
        /// 清空cookie
        /// </summary>
        public static void RemoveUserSession()
        {
            // 用户信息清除
            HttpContext.Current.Session[SessionName] = null;
            // 模块菜单信息清除
            // HttpContext.Current.Session["_DTModule"] = null;
        }
        #endregion

        #region public static void Logout(BaseUserInfo userInfo)
        /// <summary>
        /// 退出登录部分
        /// <param name="userInfo">当前用户</param>
        /// <param name="createOpenId">重新生成令牌</param>
        /// </summary>
        public static void Logout(BaseUserInfo userInfo = null, bool createOpenId = true)
        {
            // 退出时，需要把用户的操作权限，模块权限清除
            if (userInfo == null)
            {
                userInfo = Utilities.GetUserCookie();
            }
            if (userInfo == null)
            {
                userInfo = HttpContext.Current.Session[SessionName] as BaseUserInfo;
            }
            if (userInfo != null)
            {
                string cacheKey = "P" + userInfo.Id;
                if (HttpContext.Current.Cache[cacheKey] != null)
                {
                    //HttpContext.Current.Cache[cacheKey] = null;
                    // 2012.11.18 Pcsky 修改，解决报值不能为 null。参数名: value错误
                    HttpContext.Current.Cache.Remove(cacheKey);
                }

                // 这里要考虑读写分离的处理
                // IDbHelper dbHelper = DbHelperFactory.GetHelper(BaseSystemInfo.UserCenterDbType, BaseSystemInfo.UserCenterWriteDbConnection);
                // BaseUserLogOnManager userLogOnManager = new BaseUserLogOnManager(dbHelper, userInfo);
                // userLogOnManager.SignOut(userInfo.Id, createOpenId);
            }

            // 清除Seesion对象
            RemoveUserSession();
            // HttpContext.Current.Session.Abandon();
            // HttpContext.Current.Session.Clear();

            // 清空cookie
            RemoveUserCookie();
            // Session.Abandon();
            // 在此处放置用户代码以初始化页面
            // FormsAuthentication.SignOut();
            // 重新定位到登录页面
            // HttpContext.Current.Response.Redirect("Default.aspx", true);

            string url = BaseSystemInfo.WebHost + "/UserCenterV42/LogOnService.ashx?";
            WebClient webClient = new WebClient();
            NameValueCollection postValues = new NameValueCollection();
            postValues.Add("function", "SignOut");
            postValues.Add("openId", userInfo.OpenId);
            postValues.Add("systemCode", BaseSystemInfo.SystemCode);
            postValues.Add("ipAddress", Utilities.GetIPAddress());
            postValues.Add("securityKey", BaseSystemInfo.SecurityKey);
            // 向服务器发送POST数据
            byte[] responseArray = webClient.UploadValues(url, postValues);
            // string response = Encoding.UTF8.GetString(responseArray);
            // if (!string.IsNullOrEmpty(response))
            // {
            //      result = response.Equals(true.ToString(), StringComparison.InvariantCultureIgnoreCase);
            // }
        }
        #endregion


        //
        //  七 用户修改密码部分
        //

        #region public static bool EmailExists(string email) 电子邮件是否存在
        /// <summary>
        /// 电子邮件是否存在
        /// </summary>
        /// <param name="email">邮件地址</param>
        /// <returns>存在</returns>
        public static bool EmailExists(string email)
        {
            bool result = false;
            return result;
        }
        #endregion

        #region public static bool EmailExists(string userId, string email) 是否已经被别人用了
        /// <summary>
        /// 是否已经被别人用了
        /// </summary>
        /// <param name="userId">用户主键</param>
        /// <param name="email">电子邮件地址</param>
        /// <returns>已经重复</returns>
        public static bool EmailExists(string userId, string email)
        {
            bool result = false;
            return result;
        }
        #endregion

        #region public static int SetPasswordByEmail(string email, string newPassword) 按邮件设置密码
        /// <summary>
        /// 按邮件设置密码
        /// </summary>
        /// <param name="email">邮件地址</param>
        /// <param name="newPassword">密码</param>
        /// <returns>影响行数</returns>
        public static int SetPasswordByEmail(string email, string newPassword)
        {
            int result = 0;
            return result;
        }
        #endregion

        #region private static int SetPassword(string userId, string newPassword) 更新用户的新密码
        /// <summary>
        /// 更新用户的新密码
        /// </summary>
        /// <param name="userId">用户主键</param>
        /// <param name="newPassword">新密码</param>
        /// <returns>影响行数</returns>
        private static int SetPassword(string userId, string newPassword)
        {
            int result = 0;
            return result;
        }
        #endregion

        #region private static string GetPassword(string userId) 获取密码
        /// <summary>
        /// 获取密码
        /// </summary>
        /// <param name="userId">用户主键</param>
        /// <returns>密码</returns>
        private static string GetPassword(string userId)
        {
            string result = string.Empty;
            return result;
        }
        #endregion

        #region public static int ChangePassword(BaseUserInfo userInfo, string oldPassword, string newPassword, out string statusCode) 更新密码
        /// <summary>
        /// 更新密码
        /// </summary>
        /// <param name="oldPassword">原密码</param>
        /// <param name="newPassword">新密码</param>
        /// <param name="statusCode">返回状态码</param>
        /// <returns>影响行数</returns>
        public static int ChangePassword(BaseUserInfo userInfo, string oldPassword, string newPassword, out string statusCode)
        {
            int result = 0;
            statusCode = string.Empty;
            // 新密码是否允许为空
            if (!BaseSystemInfo.CheckPasswordStrength)
            {
                if (String.IsNullOrEmpty(newPassword))
                {
                    statusCode = Status.PasswordCanNotBeNull.ToString();
                    return result;
                }
            }
            // 是否加密
            oldPassword = DotNet.Utilities.SecretUtil.md5(oldPassword);
            newPassword = DotNet.Utilities.SecretUtil.md5(newPassword);

            // 判断输入原始密码是否正确
            // 密码错误
            if (!GetPassword(userInfo.Id).Equals(oldPassword))
            {
                statusCode = Status.OldPasswordError.ToString();
                return result;
            }
            // 更改密码
            result = SetPassword(userInfo.Id, newPassword);
            if (result == 1)
            {
                statusCode = Status.ChangePasswordOK.ToString();
            }
            else
            {
                // 数据可能被删除
                statusCode = Status.ErrorDeleted.ToString();
            }
            return result;
        }
        #endregion


        //
        // 八 读取用户信息，更新用户信息部分
        //

        #region public static BaseUserEntity GetUser(string userId) 获取用户信息
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="userId">用户主键</param>
        /// <returns>用户信息</returns>
        public static BaseUserEntity GetUser(string userId)
        {
            BaseUserEntity userEntity = null;
            if (userEntity != null)
            {
                // 这里需要打开用户中心的数据
                using (IDbHelper userCenterDbHelper = DbHelperFactory.GetHelper(BaseSystemInfo.UserCenterDbType, BaseSystemInfo.UserCenterDbConnection))
                {
                    userCenterDbHelper.Open();
                    List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
                    parameters.Add(new KeyValuePair<string, object>(BaseUserEntity.FieldId, userId));
                    DataTable dtBaseUser = DbLogic.GetDataTable(userCenterDbHelper, BaseUserEntity.TableName, parameters);
                    if (dtBaseUser.Rows.Count > 0)
                    {
                        userEntity.WorkCategory = dtBaseUser.Rows[0][BaseUserEntity.FieldWorkCategory].ToString();
                        // userEntity.QQ = dtBaseUser.Rows[0][BaseUserEntity.FieldQQ].ToString();
                    }
                    userCenterDbHelper.Close();
                }
            }
            return userEntity;
        }
        #endregion

        #region public static int UpdateUser(BaseUserEntity userEntity) 更新用户信息，若不存在当前用户，那就新增一条这样数据库中的冗余相对少一些，更新自己信息的，才会保存到网上商城这边。
        /// <summary>
        /// 更新用户信息，若不存在当前用户，那就新增一条
        /// 这样数据库中的冗余相对少一些，更新自己信息的，才会保存到网上商城这边。
        /// </summary>
        /// <param name="userEntity">用户信息</param>
        /// <returns>影响行数</returns>
        public static int UpdateUser(BaseUserEntity userEntity)
        {
            int result = 0;
            return result;
        }
        #endregion

        //
        // 九 忘记密码部分
        //

        #region private static string GetSendPasswordBody(BaseUserEntity userEntity, string password) 获取忘记密码邮件主题内容部分
        /// <summary>
        /// 获取忘记密码邮件主题内容部分
        /// </summary>
        /// <param name="userEntity">用户实体</param>
        /// <param name="password">密码</param>
        /// <returns>邮件主题</returns>
        private static string GetSendPasswordBody(BaseUserEntity userEntity, string password)
        {
            StringBuilder htmlBody = new StringBuilder();
            htmlBody.Append("<body style=\"font-size:10pt\">");
            htmlBody.Append("<div style=\"font-size:10pt; font-weight:bold\">尊敬的用户 " + userEntity.UserName + " 您好：</div>");
            htmlBody.Append("<br>");
            htmlBody.Append("<div>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 您的新密码是：<font size=\"3\" color=\"#6699cc\">" + password + "</font></div>");
            htmlBody.Append("<br>");
            htmlBody.Append("<div>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 请重新登录系统 <a href='http://www.cdpsn.org.cn/Signin.aspx'>立即登录中国残疾人服务网</a></div>");
            htmlBody.Append("<br>");
            htmlBody.Append("<div>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 如有任何疑问，欢迎致浙大网新易盛客服热线：0571-88935961，我们将热情为您解答。</div>");
            htmlBody.Append("<br>");
            htmlBody.Append("<div style=\"text-align:center\">浙大网新易盛 用户服务中心</div>");
            htmlBody.Append("<div style=\"text-align:center\">" + System.DateTime.Now.Year + "年" + System.DateTime.Now.Month + "月" + System.DateTime.Now.Day + "日</div></body>");
            return htmlBody.ToString();
        }
        #endregion

        #region private static bool SendPassword(BaseUserEntity userEntity) 发送密码给指定的邮箱
        /// <summary>
        /// 发送密码给指定的邮箱
        /// </summary>
        /// <param name="userEntity">用户实体</param>
        /// <returns>成功发送邮件</returns>
        private static bool SendPassword(BaseUserEntity userEntity)
        {
            bool result = false;
            using (IDbHelper dbHelper = DbHelperFactory.GetHelper(BaseSystemInfo.UserCenterDbType, BaseSystemInfo.UserCenterDbConnection))
            {
                BaseUserInfo userInfo = null;
                try
                {
                    string password = BaseRandom.GetRandom(100000, 999999).ToString();
                    using (System.Net.Mail.MailMessage mailMessage = new System.Net.Mail.MailMessage())
                    {
                        // 接收人邮箱地址
                        // mailMessage.To.Add(new System.Net.Mail.MailAddress(userEntity.Email));
                        mailMessage.Body = GetSendPasswordBody(userEntity, password);
                        mailMessage.From = new System.Net.Mail.MailAddress("xlf8255363@163.com", "杭州海日涵科技有限公司");
                        mailMessage.BodyEncoding = Encoding.GetEncoding("GB2312");
                        mailMessage.Subject = "杭州海日涵科技有限公司 新密码。";
                        mailMessage.IsBodyHtml = true;
                        System.Net.Mail.SmtpClient smtpclient = new System.Net.Mail.SmtpClient("SMTP.163.COM", 25);
                        smtpclient.Credentials = new System.Net.NetworkCredential("xlf8255363@163.com", "youxikuang");
                        smtpclient.EnableSsl = false;
                        smtpclient.Send(mailMessage);
                        result = true;
                        // 修改用户的密码
                        // 用户数据库进行差找用户操作
                        dbHelper.Open();
                        BaseUserManager userManager = new BaseUserManager(dbHelper);
                        userInfo = userManager.ConvertToUserInfo(userEntity);
                        userManager.SetParameter(userInfo);
                        // 密码进行加密，读取网站的密钥
                        password = userManager.EncryptUserPassword(password);
                        userManager.SetPassword(userEntity.Id, password);
                    }
                }
                catch (System.Exception exception)
                {
                    // 若有异常，应该需要保存异常信息
                    BaseExceptionManager.LogException(dbHelper, userInfo, exception);
                    result = false;
                }
                finally
                {
                    dbHelper.Close();
                }
            }
            return result;
        }
        #endregion

        #region public static bool SendPassword(string userName, out string statusCode, out string statusMessage) 用户忘记密码，发送密码
        /// <summary>
        /// 用户忘记密码，发送密码
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="statusCode">状态码</param>
        /// <param name="statusMessage">状态信息</param>
        /// <returns>成功发送密码</returns>
        public static bool SendPassword(string userName, out string statusCode, out string statusMessage)
        {
            bool result = false;
            // 1.用户是否找到？默认是未找到用户状态
            statusCode = Status.UserNotFound.ToString();
            statusMessage = "用户未找到，请重新输入用户名。";
            // 用户数据库进行差找用户操作
            using (IDbHelper dbHelper = DbHelperFactory.GetHelper(BaseSystemInfo.UserCenterDbType, BaseSystemInfo.UserCenterDbConnection))
            {
                dbHelper.Open();
                BaseUserManager userManager = new BaseUserManager(dbHelper);
                // 2.用户是否已被删除？
                List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
                parameters.Add(new KeyValuePair<string, object>(BaseUserEntity.FieldUserName, userName));
                parameters.Add(new KeyValuePair<string, object>(BaseUserEntity.FieldDeletionStateCode, 0));
                BaseUserEntity userEntity = BaseEntity.Create<BaseUserEntity>(userManager.GetDataTable(parameters));
                dbHelper.Close();
                // 是否已找到了此用户
                if (userEntity != null && !string.IsNullOrEmpty(userEntity.Id))
                {
                    // 3.用户是否有效的？
                    if (userEntity.Enabled == 1)
                    {
                        /*
                        if (!string.IsNullOrEmpty(userEntity.Email))
                        {
                            // 5.重新产生随机密码？
                            // 6.发送邮件给用户？
                            // 7.重新设置用户密码？
                            result = SendPassword(userEntity);
                            statusCode = Status.OK.ToString();
                            statusMessage = "新密码已发送到您的注册邮箱" + userEntity.Email + "。";
                        }
                        else
                        {
                            // 4.用户是否有邮件账户？
                            statusCode = Status.UserNotEmail.ToString();
                            statusMessage = "用户没有电子邮件地址，无法从新设置密码，请您及时联系系统管理员。";
                        }
                        */
                    }
                    else
                    {
                        if (userEntity.Enabled == 0)
                        {
                            statusCode = Status.UserLocked.ToString();
                            statusMessage = "用户被锁定，不允许设置密码。";
                        }
                        else
                        {
                            statusCode = Status.UserNotActive.ToString();
                            statusMessage = "用户还未被激活，不允许设置密码。";
                        }
                    }
                }
            }
            return result;
        }
        #endregion

        //
        // 十 字符串加密解密部分
        //

        #region public static string Encrypt(string targetValue) DES数据加密
        /// <summary>
        /// DES数据加密
        /// </summary>
        /// <param name="targetValue">目标字段</param>
        /// <returns>加密</returns>
        public static string Encrypt(string targetValue)
        {
            return Encrypt(targetValue, "Hairihan TECH");
        }
        #endregion

        #region private static string Encrypt(string targetValue, string key) DES数据加密
        /// <summary>
        /// DES数据加密
        /// </summary>
        /// <param name="targetValue">目标值</param>
        /// <param name="key">密钥</param>
        /// <returns>加密值</returns>
        private static string Encrypt(string targetValue, string key)
        {
            return SecretUtil.Encrypt(targetValue, key);
        }
        #endregion

        #region public static string Decrypt(string targetValue) DES数据解密
        /// <summary>
        /// DES数据解密
        /// </summary>
        /// <param name="targetValue">目标字段</param>
        /// <returns>解密</returns>
        public static string Decrypt(string targetValue)
        {
            return Decrypt(targetValue, "Hairihan TECH");
        }
        #endregion

        #region private static string Decrypt(string targetValue, string key) DES数据解密
        /// <summary>
        /// DES数据解密
        /// </summary>
        /// <param name="targetValue"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        private static string Decrypt(string targetValue, string key)
        {
            return SecretUtil.Decrypt(targetValue, key);
        }
        #endregion
    }
}