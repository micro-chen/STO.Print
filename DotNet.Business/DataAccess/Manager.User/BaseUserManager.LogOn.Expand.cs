//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd.  
//-----------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Caching;

namespace DotNet.Business
{
    using DotNet.Model;
    using DotNet.Utilities;

    /// <summary>
    /// BaseUserManager 
    /// 用户管理 扩展类 
    /// 
    /// 修改记录
    /// 
    ///		2015.01.25 版本：3.1 SongBiao 扩展登录提醒功能。
    /// 
    /// <author>
    ///		<name>SongBiao</name>
    ///		<date>2015.01.25</date>
    /// </author> 
    /// </summary>
    public partial class BaseUserManager : BaseManager
    {
        #region public void SendLogOnRemind(BaseUserInfo userInfo) 向登录用户发送登录提醒消息
        /// <summary>
        /// 宋彪 2015-01-22
        /// 向登录用户发送登录提醒消息
        /// 1、邮件提醒；、2手机短信提醒；3、吉信提醒
        /// 为了避免线程阻塞，使用一个新线程处理提醒消息的发送
        /// 所有超管及IT信息中心的人员全部强制提醒
        /// </summary>
        /// <param name="userInfo">用户登录信息</param>
        public void SendLogOnRemind(BaseUserInfo userInfo)
        {
            System.Threading.ThreadPool.QueueUserWorkItem(delegate
            {
                try
                {
                    //获取提醒实体信息 提醒要求已设置且启用
                    string systemName = userInfo.SystemCode;
                    BaseUserLogonExtendManager manager = new BaseUserLogonExtendManager();
                    BaseUserLogonExtendEntity userLogonRemind = manager.GetObject(userInfo.Id);
                    BaseUserContactEntity userContactEntity = new BaseUserContactManager().GetObject(userInfo.Id);
                    WebClient webClient = new WebClient();
                    //提醒对象实体和联系信息实体存在则进行下一步
                    if (userLogonRemind != null && userContactEntity != null)
                    {
                        //发送吉信消息提醒 有唯一账号而且设置了在登录时发送吉信登录提醒
                        if (!string.IsNullOrWhiteSpace(userInfo.NickName) && userLogonRemind.JixinRemind == 1)
                        {
                            //吉信接口地址 
                            string url = "http://jixin.zt-express.com:8280/mng/httpservices/msg-sendMessageToUsers.action";
                            NameValueCollection postValues = new NameValueCollection();
                            //为空则无发送者，客户无回复按钮+(v1.1)
                            postValues.Add("sender", string.Empty);
                            //关闭延迟 默认为30秒 +(v1.1)
                            postValues.Add("closeDelay", "30");
                            //显示延迟 默认为0秒 +(v1.1)
                            postValues.Add("showDelay", "0");
                            //接收者，以逗号分隔，包含中文需使用URL编码
                            // ReSharper disable once AssignNullToNotNullAttribute
                            postValues.Add("receivers", System.Web.HttpUtility.UrlEncode(userInfo.NickName, System.Text.Encoding.UTF8));
                            //显示位置,0表示居中，1表示右下角(默认0)
                            postValues.Add("position", "1");
                            //消息标题
                            postValues.Add("title", "中天系统账号登录提醒");
                            //消息内容
                            string content = "<div style='word-break:keep-all;'><font color='#FF7E00'>" + userInfo.NickName + "</font>，您的账号于<font color='#FF7E00'>" + DateTime.Now.ToString(BaseSystemInfo.DateTimeFormat) + "</font>登录了<font color='#FF7E00'>" + systemName + "</font></div>"
                                + "<div style='word-break:keep-all;margin-top:5px'>登录IP：<font color='#FF7E00'>" + userInfo.IPAddress + "</font></div>"
                                  + "<div style=' word-break:keep-all;margin-top:5px'>IP参考所在地：<font color='#FF7E00'>" + DotNet.Utilities.IpHelper.GetInstance().FindName(userInfo.IPAddress) + "</font></div>"
                                + "<div style=' word-break:keep-all;margin-top:5px'>如果不是您自己登录，请马上联系：021-31165566,或即刻<a href='http://security.zt-express.com' target='_blank'>登录安全中心</a>修改密码。</div>";
                            postValues.Add("content", content);
                            postValues.Add("width", "300");
                            postValues.Add("height", "180");
                            // 向服务器发送POST数据
                            webClient.UploadValues(url, postValues);
                        }
                        //用户邮箱存在，邮箱已经认证而且设置了使用登录时发送邮件提醒
                        if (!string.IsNullOrWhiteSpace(userContactEntity.Email) && userContactEntity.EmailValiated == 1 && userLogonRemind.EmailRemind == 1)
                        {
                            string subject = userInfo.CompanyName + " - " + userInfo.NickName + " 登录" + systemName + " 系统提醒";
                            string body = userInfo.UserName + System.Environment.NewLine + ":<br/>"
                                + DateTime.Now.ToString(BaseSystemInfo.DateTimeFormat) + "登录了" + systemName + "；<br/>" + System.Environment.NewLine
                                + "编号：" + userInfo.Code + "；<br/> " + System.Environment.NewLine
                                + "登录系统：" + systemName + "；<br/> " + System.Environment.NewLine
                                + "登录IP：" + userInfo.IPAddress + "；<br/> " + System.Environment.NewLine
                                + "MAC地址：" + userInfo.MACAddress + "；<br/>" + System.Environment.NewLine
                                + "如果不是您自己登录，请马上联系021-31165566，或即刻登录系统修改密码。";
                            SmtpClient smtp = new SmtpClient();
                            //邮箱的smtp地址
                            smtp.Host = "mail.zto.cn";//BaseSystemInfo.MailServer;
                            //端口号
                            smtp.Port = 25;
                            //构建发件人的身份凭据类
                            //smtp.Credentials = new NetworkCredential(BaseSystemInfo.MailUserName, BaseSystemInfo.MailPassword);
                            smtp.Credentials = new NetworkCredential("remind", "ztoremind#@!~");
                            //构建消息类
                            MailMessage objMailMessage = new MailMessage();
                            //设置优先级
                            objMailMessage.Priority = MailPriority.High;
                            //消息发送人
                            objMailMessage.From = new MailAddress("remind", "中通快递登录提醒", System.Text.Encoding.UTF8);
                            //收件人
                            objMailMessage.To.Add(userContactEntity.Email);
                            //标题
                            objMailMessage.Subject = subject;
                            //标题字符编码
                            objMailMessage.SubjectEncoding = System.Text.Encoding.UTF8;
                            //正文
                            objMailMessage.Body = body;
                            objMailMessage.IsBodyHtml = true;
                            //内容字符编码
                            objMailMessage.BodyEncoding = System.Text.Encoding.UTF8;
                            //发送
                            smtp.Send(objMailMessage);
                        }
                        //用户手机存在，已验证，而且设置了登录时发送手机短信提醒 需要对网点扣费
                        if (!string.IsNullOrWhiteSpace(userContactEntity.Mobile) && userContactEntity.MobileValiated == 1 && userLogonRemind.MobileRemind == 1)
                        {
                            //根据朱工建议，增加判断登陆地是否发生变化
                            //获取最近两次的登录记录 按时间降序查询
                            BaseLoginLogManager loginLogManager = new BaseLoginLogManager(userInfo);
                            List<BaseLoginLogEntity> loginLogEntities = loginLogManager.GetList<BaseLoginLogEntity>(new KeyValuePair<string, object>(BaseLoginLogEntity.FieldUserId, UserInfo.Id), 2, " CREATEON DESC ");
                            IpHelper ipHelper = new IpHelper();
                            string addressA = ipHelper.FindName(loginLogEntities[0].IPAddress);
                            if (string.IsNullOrWhiteSpace(addressA))
                            {
                                addressA = ipHelper.FindName(loginLogEntities[0].IPAddress);
                            }
                            string addressB = ipHelper.FindName(loginLogEntities[1].IPAddress);
                            if (string.IsNullOrWhiteSpace(addressB))
                            {
                                addressB = ipHelper.FindName(loginLogEntities[1].IPAddress);
                            }
                            if (loginLogEntities[0] != null
                                && loginLogEntities[1] != null
                                && (!string.Equals(loginLogEntities[0].IPAddress, loginLogEntities[1].IPAddress, StringComparison.OrdinalIgnoreCase)
                                || !string.Equals(addressA, addressB, StringComparison.OrdinalIgnoreCase)
                                ))
                            {
                                string url = "http://mas.zto.cn/WebAPIV42/API/Mobile/SendMessageByCompanyCode";
                                NameValueCollection postValues = new NameValueCollection();
                                postValues.Add("companyCode", userInfo.CompanyCode);
                                postValues.Add("mobiles", userContactEntity.Mobile);
                                string message = userInfo.NickName + "，您好！您的账号于" + DateTime.Now.ToString(BaseSystemInfo.DateTimeFormat) + "登录了" + systemName + "，登录IP：" + userInfo.IPAddress + "，如果不是您自己登录，请马上联系021-31165566，或即刻登录安全中心修改密码。";
                                postValues.Add("message", message);
                                postValues.Add("customerName", userInfo.NickName);
                                webClient.UploadValues(url, postValues);
                            }
                        }
                        //微信提醒
                        if (!string.IsNullOrWhiteSpace(userContactEntity.WeChat) && userContactEntity.WeChatValiated == 1 && userLogonRemind.WechatRemind == 1)
                        {
                            string url = "http://weixin.zto.cn/Template/WeiXinLogin";
                            NameValueCollection postValues = new NameValueCollection();
                            postValues.Add("first", "您已经成功登录系统");
                            postValues.Add("keyword1", userInfo.NickName);
                            postValues.Add("remark", userInfo.NickName + "，您的账号于" + DateTime.Now.ToString(BaseSystemInfo.DateTimeFormat) + "登录了" + systemName);
                            postValues.Add("OpenId", userContactEntity.WeChat);
                            //postValues.Add("url", "http://security.zt-express.com/changepassword"); 详情的链接
                            webClient.UploadValues(url, postValues);
                        }
                    }
                }
                catch (Exception ex)
                {
                    FileUtil.WriteMessage(userInfo.NickName + "登录提醒消息发送异常：" + ex.Message, System.Web.HttpContext.Current.Server.MapPath("~/Log/") + "Log" + DateTime.Now.ToString(BaseSystemInfo.DateFormat) + ".txt");
                }
            });
        }
        #endregion

        /// <summary>
        /// 2015-07-31 宋彪根据安全中心添加的密码检测
        /// 密码检测 强度检测
        /// PDA
        /// </summary>
        /// <param name="orginPassWord"></param>
        /// <param name="userEntity"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool IsWeakPassWord(BaseUserEntity userEntity, string orginPassWord, out string message)
        {
            message = string.Empty;
            if (!String.Equals("PDAUserLogOn",UserLogOnTable,StringComparison.CurrentCultureIgnoreCase))
            {
                //密码检测 强度检测
                //获取缓存中的弱密码字典
                List<string> list = GetCacheWeakPassWord();
                if (list.Count == 0)
                {
                    message = "弱密码字典没找到，请联系管理员添加。";
                    return true;
                }
                //密码是否在弱密码列表字典中 判断原始密码 需要用户登录成功判断才对
                bool bpassCon = list.Contains(orginPassWord);
                //密码强度
                int passStrength = GetRateUserPass(orginPassWord);
                if (bpassCon || passStrength < 3 || orginPassWord.Contains("1234") || orginPassWord.Contains(userEntity.UserName) || userEntity.UserName.Contains(orginPassWord))
                {
                    message = "不能登录的可能原因：密码强度太低，密码中包含用户名，请修改登录密码。";
                    return true;
                }
            }
            return false;
        }


        #region  public static List<string> GetCacheWeakPassWord() 获取全部符合弱密码的字符串
        private static readonly object Locker = new object();
        /// <summary>
        /// 获取全部符合弱密码的字符串
        /// </summary>
        /// <returns></returns>
        public List<string> GetCacheWeakPassWord()
        {
            string cacheKey = "GetWeakPassWord";
            if (HttpContext.Current.Cache[cacheKey] == null || string.Equals("null", HttpContext.Current.Cache[cacheKey].ToString(), StringComparison.OrdinalIgnoreCase))
            {
                lock (Locker)
                {
                    if (HttpContext.Current.Cache[cacheKey] == null)
                    {
                        try
                        {
                            List<string> listWeakPassWord = new List<string>();
                            string filePath = System.Web.HttpContext.Current.Server.MapPath("/App_Data/birthdayweakcommonwords.txt");
                            StreamReader sr = File.OpenText(filePath);
                            string nextLine;
                            while ((nextLine = sr.ReadLine()) != null)
                            {
                                //sb.Append(nextLine + ",");
                                listWeakPassWord.Add(nextLine);
                            }
                            sr.Close();
                            filePath = System.Web.HttpContext.Current.Server.MapPath("/App_Data/wpa2dictwordlist.txt");
                            sr = File.OpenText(filePath);
                            while ((nextLine = sr.ReadLine()) != null)
                            {
                                //sb.Append(nextLine + ",");
                                listWeakPassWord.Add(nextLine);
                            }
                            sr.Close();
                            //依赖最后一个文件
                            CacheDependency dep = new CacheDependency(filePath, DateTime.Now);
                            //HttpContext.Current.Cache.Insert(cacheKey, listWeakPassWord, dep);
                            HttpContext.Current.Cache.Insert(cacheKey, listWeakPassWord, dep, DateTime.Now.AddDays(3.00), Cache.NoSlidingExpiration, CacheItemPriority.NotRemovable, null);
                        }
                        catch (Exception ex)
                        {
                            FileUtil.WriteMessage("获取全部符合弱密码的字符串出现异常：" + ex.Message, System.Web.HttpContext.Current.Server.MapPath("~/Log/") + "Log" + DateTime.Now.ToString(BaseSystemInfo.DateFormat) + ".txt");
                            return null;
                        }
                    }
                }
            }
            return (List<string>)HttpContext.Current.Cache.Get(cacheKey);
        }
        #endregion

        #region public int GetRateUserPass(string pass) 密码强度级别
        /// <summary>
        /// 密码强度级别 小于3不允许登录
        /// </summary>
        /// <param name="pass">密码</param>
        /// <returns>强度级别</returns>
        public int GetRateUserPass(string pass)
        {
            /*  
             * 返回值值表示口令等级  
             * 0 不合法口令  
             * 1 太短  
             * 2 弱  
             * 3 一般  
             * 4 很好  
             * 5 极佳  
             */
            int i = 0;
            //if(pass==null || pass.length()==0)
            if (string.IsNullOrWhiteSpace(pass))
            {
                return 0;
            }
            int hasLetter = MatcherLength(pass, "[a-zA-Z]", "");
            int hasNumber = MatcherLength(pass, "[0-9]", "");
            int passLen = pass.Length;
            if (passLen >= 6)
            {
                /* 如果仅包含数字或仅包含字母 */
                if ((passLen - hasLetter) == 0 || (passLen - hasNumber) == 0)
                {
                    if (passLen < 8)
                    {
                        i = 2;
                    }
                    else
                    {
                        i = 3;
                    }
                }
                /* 如果口令大于6位且即包含数字又包含字母 */
                else if (hasLetter > 0 && hasNumber > 0)
                {
                    if (passLen >= 10)
                    {
                        i = 5;
                    }
                    else if (passLen >= 8)
                    {
                        i = 4;
                    }
                    else
                    {
                        i = 3;
                    }
                }
                /* 如果既不包含数字又不包含字母 */
                else if (hasLetter == 0 && hasNumber == 0)
                {
                    if (passLen >= 7)
                    {
                        i = 5;
                    }
                    else
                    {
                        i = 4;
                    }
                }
                /* 字母或数字有一方为0 */
                else if (hasNumber == 0 || hasLetter == 0)
                {
                    if ((passLen - hasLetter) == 0 || (passLen - hasNumber) == 0)
                    {
                        i = 2;
                    }
                    /*   
                     * 字母数字任意一种类型小于6且总长度大于等于6  
                     * 则说明此密码是字母或数字加任意其他字符组合而成  
                     */
                    else
                    {
                        if (passLen > 8)
                        {
                            i = 5;
                        }
                        else if (passLen == 8)
                        {
                            i = 4;
                        }
                        else
                        {
                            i = 3;
                        }
                    }
                }
            }
            else
            { //口令小于6位则显示太短  
                if (passLen > 0)
                {
                    i = 1; //口令太短  
                }
                else
                {
                    i = 0;
                }
            }
            return i;
        }
        #endregion

        #region public int MatcherLength(string str, string cp, string s) 查询匹配长度
        /// <summary>
        /// 查询匹配长度
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="cp">规则</param>
        /// <param name="s"></param>
        /// <returns></returns>
        public int MatcherLength(string str, string cp, string s)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return 0;
            }
            MatchCollection mc = Regex.Matches(str, cp);
            return mc.Count;
        }
        #endregion
    }
}
