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
    ///
    ///		2015.02.03 版本：1.1 JiRiGaLa	登录日志很庞大时，需要有专门的登录日志服务器，因为大家会查自己的登录日志是否安全。
    ///		2012.04.14 版本：1.0 JiRiGaLa	主键创建。
    ///		
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2015.02.03</date>
    /// </author>
    /// </summary>
    public partial class BaseSystemInfo
    {
        /// <summary>
        /// 用户数据库类别
        /// </summary>
        public static CurrentDbType ServerDbType = CurrentDbType.Oracle;

        /// <summary>
        /// 用户数据库类别
        /// </summary>
        public static CurrentDbType UserCenterDbType = CurrentDbType.Oracle;

        /// <summary>
        /// 消息数据库类别
        /// </summary>
        public static CurrentDbType MessageDbType = CurrentDbType.Oracle;

        /// <summary>
        /// 业务数据库类别
        /// </summary>
        public static CurrentDbType BusinessDbType = CurrentDbType.Oracle;

        /// <summary>
        /// 工作流数据库类别
        /// </summary>
        public static CurrentDbType WorkFlowDbType = CurrentDbType.Oracle;

        /// <summary>
        /// 登录日志数据库类别
        /// </summary>
        public static CurrentDbType LoginLogDbType = CurrentDbType.Oracle;

        /// <summary>
        /// 是否加密数据库连接
        /// </summary>
        public static bool EncryptDbConnection = false;


        private static string userCenterDbConnection = string.Empty;
        /// <summary>
        /// 数据库连接(不进行读写分离)
        /// </summary>
        public static string UserCenterDbConnection
        {
            get
            {
                if (string.IsNullOrEmpty(userCenterDbConnection))
                {
                    if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["UserCenterDbConnection"]))
                    {
                        userCenterDbConnection = ConfigurationManager.AppSettings["UserCenterDbConnection"];
                    }
                    if (string.IsNullOrEmpty(userCenterDbConnection))
                    {
                        userCenterDbConnection = "Data Source=localhost;Initial Catalog=UserCenterV42;Integrated Security=SSPI;";
                    }
                }
                // 默认的数据库连接
                return userCenterDbConnection;
            }
            set
            {
                // 写入的数据库连接
                userCenterWriteDbConnection = value;
                // 读取的数据库连接
                userCenterReadDbConnection = value;
                // 默认的数据库连接
                userCenterDbConnection = value;
            }
        }

        private static string userCenterWriteDbConnection = string.Empty;
        /// <summary>
        /// 数据库连接(读取的数据库连接、主要目标是为了读写分离)
        /// </summary>
        public static string UserCenterWriteDbConnection
        {
            get
            {
                if (string.IsNullOrEmpty(userCenterWriteDbConnection))
                {
                    if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["UserCenterDbConnection"]))
                    {
                        userCenterWriteDbConnection = ConfigurationManager.AppSettings["UserCenterDbConnection"];
                    }
                    if (string.IsNullOrEmpty(userCenterWriteDbConnection))
                    {
                        userCenterWriteDbConnection = "Data Source=localhost;Initial Catalog=UserCenterV42;Integrated Security=SSPI;";
                    }
                }
                return userCenterWriteDbConnection;
            }
            set
            {
                userCenterWriteDbConnection = value;
            }
        }

        private static string userCenterReadDbConnection = string.Empty;
        /// <summary>
        /// 数据库连接(读取的数据库连接、主要目标是为了读写分离)
        /// </summary>
        public static string UserCenterReadDbConnection
        {
            get
            {
                if (string.IsNullOrEmpty(userCenterReadDbConnection))
                {
                    if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["UserCenterDbConnection"]))
                    {
                        userCenterReadDbConnection = ConfigurationManager.AppSettings["UserCenterDbConnection"];
                    }
                    if (string.IsNullOrEmpty(userCenterReadDbConnection))
                    {
                        userCenterReadDbConnection = "Data Source=localhost;Initial Catalog=UserCenterV42;Integrated Security=SSPI;";
                    }
                }
                return userCenterReadDbConnection;
            }
            set
            {
                userCenterReadDbConnection = value;
            }
        }

        /// <summary>
        /// 数据库连接的字符串
        /// </summary>
        public static string UserCenterDbConnectionString = string.Empty;

        /// <summary>
        /// 消息数据库
        /// </summary>
        //public static string MessageDbConnection = "Data Source=localhost;Initial Catalog=MessageCenterV42;Integrated Security=SSPI;";
        private static string messageDbConnection = string.Empty;
        /// <summary>
        /// 消息数据库连接
        /// </summary>
        public static string MessageDbConnection
        {
            get
            {
                if (string.IsNullOrEmpty(messageDbConnection))
                {
                    if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["MessageDbConnection"]))
                    {
                        messageDbConnection = ConfigurationManager.AppSettings["MessageDbConnection"];
                    }
                    if (string.IsNullOrEmpty(messageDbConnection))
                    {
                        messageDbConnection = "Data Source=localhost;Initial Catalog=MessageCenterV42;Integrated Security=SSPI;";
                    }
                }
                // 默认的消息数据库连接
                return messageDbConnection;
            }
            set
            {
                // 默认的消息数据库连接
                messageDbConnection = value;
            }
        }

        /// <summary>
        /// 消息数据库连接的字符串
        /// </summary>
        public static string MessageDbConnectionString = string.Empty;

        /// <summary>
        /// 业务数据库
        /// </summary>
        //public static string BusinessDbConnection = "Data Source=localhost;Initial Catalog=ProjectV42;Integrated Security=SSPI;";
        private static string businessDbConnection = string.Empty;
        /// <summary>
        /// 业务数据库连接
        /// </summary>
        public static string BusinessDbConnection
        {
            get
            {
                if (string.IsNullOrEmpty(businessDbConnection))
                {
                    if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["BusinessDbConnection"]))
                    {
                        businessDbConnection = ConfigurationManager.AppSettings["BusinessDbConnection"];
                    }
                    if (string.IsNullOrEmpty(businessDbConnection))
                    {
                        businessDbConnection = "Data Source=localhost;Initial Catalog=ProjectV42;Integrated Security=SSPI;";
                    }
                }
                // 默认的业务数据库连接
                return businessDbConnection;
            }
            set
            {
                // 默认的业务数据库连接
                businessDbConnection = value;
            }
        }
        /// <summary>
        /// 业务数据库（连接串，可能是加密的）
        /// </summary>
        public static string BusinessDbConnectionString = string.Empty;

        /// <summary>
        /// 工作流数据库
        /// </summary>
        // public static string WorkFlowDbConnection = "Data Source=localhost;Initial Catalog=WorkFlowV42;Integrated Security=SSPI;";
        private static string workflowDbConnection = string.Empty;
        /// <summary>
        /// 工作流数据库连接
        /// </summary>
        public static string WorkFlowDbConnection
        {
            get
            {
                if (string.IsNullOrEmpty(workflowDbConnection))
                {
                    if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["WorkFlowDbConnection"]))
                    {
                        workflowDbConnection = ConfigurationManager.AppSettings["WorkFlowDbConnection"];
                    }
                    if (string.IsNullOrEmpty(workflowDbConnection))
                    {
                        workflowDbConnection = "Data Source=localhost;Initial Catalog=WorkFlowV42;Integrated Security=SSPI;";
                    }
                }
                // 默认的工作流数据库连接
                return workflowDbConnection;
            }
            set
            {
                // 默认的工作流数据库连接
                workflowDbConnection = value;
            }
        }
        /// <summary>
        /// 工作流数据库（连接串，可能是加密的）
        /// </summary>
        public static string WorkFlowDbConnectionString = string.Empty;


        /// <summary>
        /// 登录日志数据库
        /// </summary>
        private static string loginLogDbConnection = string.Empty;
        
        /// <summary>
        /// 登录日志数据库
        /// </summary>
        public static string LoginLogDbConnection
        {
            get
            {
                if (string.IsNullOrEmpty(loginLogDbConnection))
                {
                    if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["LoginLogDbConnection"]))
                    {
                        loginLogDbConnection = ConfigurationManager.AppSettings["LoginLogDbConnection"];
                    }
                    if (string.IsNullOrEmpty(loginLogDbConnection))
                    {
                        loginLogDbConnection = "Data Source=localhost;Initial Catalog=UserCenterV42;Integrated Security=SSPI;";
                    }
                }
                // 默认的登录日志数据库连接
                return loginLogDbConnection;
            }
            set
            {
                // 登录日志数据库连接
                loginLogDbConnection = value;
            }
        }

        /// <summary>
        /// 登录日志据库（连接串，可能是加密的）
        /// </summary>
        public static string LoginLogDbConnectionString = string.Empty;
    }
}