//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd.  
//-----------------------------------------------------------------

using System.Configuration;

namespace DotNet.Utilities
{
    /// <summary>
    /// BaseSystemInfo
    /// 这是系统的核心基础信息部分
    /// 
    /// 修改记录
    ///		2012.04.14 版本：1.0 JiRiGaLa	主键创建。
    ///		
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2012.04.14</date>
    /// </author>
    /// </summary>
    public partial class BaseSystemInfo
    {
        private static string userCenterHost = string.Empty;

        public static string UserCenterHost
        {
            get
            {
                // 这个是测试用的
                // return "http://mas.zt-express.com/";

                if (string.IsNullOrEmpty(userCenterHost))
                {
                    // 针对内部服务调用配置，内部可以指定服务位置
                    if (ConfigurationManager.AppSettings["UserCenterHost"] != null)
                    {
                        userCenterHost = ConfigurationManager.AppSettings["UserCenterHost"];
                    }
                    // 若没配置用户中心？看是否选了明确的主机？针对CS客户端
                    if (string.IsNullOrEmpty(userCenterHost))
                    {
                        if (!string.IsNullOrWhiteSpace(Host))
                        {
                            if (Host.IndexOf("http") < 0)
                            {
                                userCenterHost = "http://" + Host + "/";
                            }
                            else
                            {
                                userCenterHost = Host + "/";
                            }
                        }
                    }
                    // 若还是都找不到配置，就用默认的配置文件
                    if (string.IsNullOrEmpty(userCenterHost))
                    {
                        userCenterHost = "https://userCenter.zt-express.com/";
                    }
                }
                return userCenterHost;
            }
            set
            {
                userCenterHost = value;
            }
        }

        /// <summary>
        /// 短信服务器器的地址
        /// 一般都是ip限制了，所以是不能负载均衡的独立的一个服务器
        /// </summary>
        // public static string MobileHost = "http://122.225.117.230/WebAPIV42/API/Mobile/";
        // public static string MobileHost = "http://123.157.107.232/WebAPIV42/API/Mobile/";
        // public static string MobileHost = "http://mas.zto.cn/WebAPIV42/API/Mobile/";

        private static string mobileHost = string.Empty;

        public static string MobileHost
        {
            get
            {
                if (ConfigurationManager.AppSettings["MobileHost"] != null)
                {
                    mobileHost = ConfigurationManager.AppSettings["MobileHost"];
                }
                if (string.IsNullOrEmpty(mobileHost))
                {
                    mobileHost = "http://mas.zto.cn/WebAPIV42/API/Mobile/";
                }
                return mobileHost;
            }
            set
            {
                mobileHost = value;
            }
        }

        public static string WebHost
        {
            get
            {
                // 这个是测试用的
                // return "http://mas.zt-express.com/";

                string webHost = "http://userCenter.zt-express.com/";
                if (ConfigurationManager.AppSettings["WebHost"] != null)
                {
                    webHost = ConfigurationManager.AppSettings["WebHost"];
                }
                if (!string.IsNullOrWhiteSpace(Host))
                {
                    if (Host.IndexOf("http") < 0)
                    {
                        webHost = "http://" + Host + "/";
                    }
                    else
                    {
                        webHost = Host + "/";
                    }
                }
                return webHost;
            }
        }

        /*
        <add key="RedisHosts" value="ztredis6482(*)134&amp;^%xswed@redis-Read.ztosys.com:6482"/>
        */

        private static string[] redisHosts = null;
        public static string[] RedisHosts
        {
            get
            {
                if (redisHosts == null)
                {
                    if (ConfigurationManager.AppSettings["RedisHosts"] != null)
                    {
                        redisHosts = ConfigurationManager.AppSettings["RedisHosts"].Split(',');
                    }
                    if (redisHosts == null)
                    {
                        redisHosts = new string[] { "ztredis6482(*)134&^%xswed@redis-Read.ztosys.com:6482" };
                    }
                }
                return redisHosts;
            }
            set
            {
                redisHosts = value;
            }
        }

        /*
        <add key="RedisHosts" value="ztredis6488(*)134&amp;^%xswed@redis.ztosys.com:6488"/>
        */

        private static string[] redisReadOnlyHosts = null;
        public static string[] RedisReadOnlyHosts
        {
            get
            {
                if (redisReadOnlyHosts == null)
                {
                    if (ConfigurationManager.AppSettings["RedisReadOnlyHosts"] != null)
                    {
                        redisReadOnlyHosts = ConfigurationManager.AppSettings["RedisReadOnlyHosts"].Split(',');
                    }
                    if (redisReadOnlyHosts == null)
                    {
                        redisReadOnlyHosts = new string[] { "ztredis6488(*)134&^%xswed@redis.ztosys.com:6488" };
                    }
                }
                return redisReadOnlyHosts;
            }
            set
            {
                redisReadOnlyHosts = value;
            }
        }

        /// <summary>
        /// 主机地址
        /// Host = "192.168.0.122";
        /// </summary>
        public static string Host = string.Empty;

        /// <summary>
        /// 端口号
        /// </summary>
        public static int Port = 0;

        /// <summary>
        /// 允许新用户注册
        /// </summary>
        public static bool AllowUserRegister = true;

        /// <summary>
        /// 禁止用户重复登录
        /// 只允许登录一次
        /// </summary>
        public static bool CheckOnLine = false;

        /// <summary>
        /// 软件是否需要注册
        /// </summary>
        public static bool NeedRegister = false;

        /// <summary>
        /// 注册码
        /// </summary>
        public static string RegisterKey = string.Empty;

        /// <summary>
        /// 是否采用服务器端缓存
        /// </summary>
        public static bool ServerCache = false;

        public static string systemCode = string.Empty;
        /// <summary>
        /// 这里是设置，读取哪个系统的菜单
        /// </summary>
        public static string SystemCode
        {
            get
            {
                if (string.IsNullOrEmpty(systemCode))
                {
                    if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["SystemCode"]))
                    {
                        systemCode = ConfigurationManager.AppSettings["SystemCode"];
                    }
                    if (string.IsNullOrEmpty(systemCode))
                    {
                        systemCode = "Base";
                    }
                }
                return systemCode;
            }
            set
            {
                systemCode = value;
            }
        }

        /// <summary>
        /// 检查周期5分钟内不在线的，就认为是已经没在线了，生命周期检查
        /// </summary>
        public static int OnLineTime0ut = 5 * 60 + 20;

        /// <summary>
        /// 每过1分钟，检查一次在线状态
        /// </summary>
        public static int OnLineCheck = 60;

        /// <summary>
        /// 锁不住记录时的循环次数(数据库相关)
        /// </summary>
        public static int LockNoWaitCount = 5;

        /// <summary>
        /// 锁不住记录时的等待时间(数据库相关)
        /// </summary>
        public static int LockNoWaitTickMilliSeconds = 30;

        /// <summary>
        /// 上传文件路径
        /// </summary>
        public static string UploadDirectory = "Document/";

        /// <summary>
        /// 服務實現包
        /// </summary>
        public static string Service = "DotNet.Business";

        /// <summary>
        /// 服務映射工廠
        /// </summary>
        public static string ServiceFactory = "ServiceFactory";

        /// <summary>
        /// 系统默认密码
        /// </summary>
        public static string DefaultPassword = "123456";
    }
}