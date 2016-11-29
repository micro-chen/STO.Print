//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd.  
//-----------------------------------------------------------------

using System.Configuration;

namespace DotNet.Utilities
{
    /// <summary>
    /// ConfigurationHelper
    /// 连接配置。
    /// 
    /// 修改记录
    /// 
    ///     2016.03.14 版本：2.1 JiRiGaLa  RecordLogOnLog、RecordLog 开关的读取完善。 
    ///     2014.01.16 版本：2.0 JiRiGaLa  读取加密连接串的方法。 
    ///     2011.07.05 版本：1.1 zgl 增加  BaseSystemInfo.CheckIPAddress。
    ///		2008.06.08 版本：1.0 JiRiGaLa 将程序从 BaseConfiguration 进行了分离。
    /// 
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2016.03.14</date>
    /// </author> 
    /// </summary>
    public class ConfigurationHelper
    {
        public static string AppSettings(string key, bool encrypt)
        {
            string result = string.Empty;

            if (ConfigurationManager.AppSettings[key] != null)
            {
                result = ConfigurationManager.AppSettings[key];
            }
            if (!string.IsNullOrEmpty(result))
            {
                if (encrypt)
                {
                    result = SecretUtil.Decrypt(result);
                }
            }

            return result;
        }

        #region public static void GetConfig()
        /// <summary>
        /// 从配置信息获取配置信息
        /// </summary>
        /// <param name="configuration">配置</param>
        public static void GetConfig()
        {
            // 读取注册码
            BaseSystemInfo.RegisterKey = ConfigurationManager.AppSettings["RegisterKey"];

            if (ConfigurationManager.AppSettings["MailServer"] != null)
            {
                BaseSystemInfo.MailServer = ConfigurationManager.AppSettings["MailServer"];
            }
            if (ConfigurationManager.AppSettings["MailUserName"] != null)
            {
                BaseSystemInfo.MailUserName = ConfigurationManager.AppSettings["MailUserName"];
            }
            if (ConfigurationManager.AppSettings["MailPassword"] != null)
            {
                BaseSystemInfo.MailPassword = ConfigurationManager.AppSettings["MailPassword"];
                if (BaseSystemInfo.EncryptDbConnection)
                {
                    BaseSystemInfo.MailPassword = SecretUtil.Decrypt(BaseSystemInfo.MailPassword);
                }
            }

            // 客户信息配置
            if (ConfigurationManager.AppSettings["CustomerCompanyName"] != null)
            {
                BaseSystemInfo.CustomerCompanyName = ConfigurationManager.AppSettings["CustomerCompanyName"];
            }
            if (ConfigurationManager.AppSettings["ConfigurationFrom"] != null)
            {
                BaseSystemInfo.ConfigurationFrom = BaseConfiguration.GetConfiguration(ConfigurationManager.AppSettings["ConfigurationFrom"]);
            }
            if (ConfigurationManager.AppSettings["SoftName"] != null)
            {
                BaseSystemInfo.SoftName = ConfigurationManager.AppSettings["SoftName"];
            }
            if (ConfigurationManager.AppSettings["CompanyName"] != null)
            {
                BaseSystemInfo.CompanyName = ConfigurationManager.AppSettings["CompanyName"];
            }
            if (ConfigurationManager.AppSettings["SoftFullName"] != null)
            {
                BaseSystemInfo.SoftFullName = ConfigurationManager.AppSettings["SoftFullName"];
            }
            if (ConfigurationManager.AppSettings["SystemCode"] != null)
            {
                BaseSystemInfo.SystemCode = ConfigurationManager.AppSettings["SystemCode"];
            }
            if (ConfigurationManager.AppSettings["ServiceUserName"] != null)
            {
                BaseSystemInfo.ServiceUserName = ConfigurationManager.AppSettings["ServiceUserName"];
            }
            if (ConfigurationManager.AppSettings["ServicePassword"] != null)
            {
                BaseSystemInfo.ServicePassword = ConfigurationManager.AppSettings["ServicePassword"];
            }

            if (ConfigurationManager.AppSettings["PasswordErrorLockLimit"] != null)
            {
                BaseSystemInfo.PasswordErrorLockLimit = int.Parse(ConfigurationManager.AppSettings["PasswordErrorLockLimit"]);
            }
            if (ConfigurationManager.AppSettings["PasswordErrorLockCycle"] != null)
            {
                BaseSystemInfo.PasswordErrorLockCycle = int.Parse(ConfigurationManager.AppSettings["PasswordErrorLockCycle"]);
            }

            // BaseSystemInfo.CurrentLanguage = ConfigurationManager.AppSettings[BaseConfiguration.CURRENT_LANGUAGE];
            // BaseSystemInfo.Version = ConfigurationManager.AppSettings[BaseConfiguration.VERSION];

            // BaseSystemInfo.UseModulePermission = (ConfigurationManager.AppSettings[BaseConfiguration.USE_MODULE_PERMISSION].ToUpper(), true.ToString().ToUpper(), true);
            // BaseSystemInfo.UsePermissionScope = (ConfigurationManager.AppSettings[BaseConfiguration.USE_PERMISSIONS_COPE].ToUpper(), true.ToString().ToUpper(), true);
            // BaseSystemInfo.UseTablePermission = (ConfigurationManager.AppSettings[BaseConfiguration.USE_TABLE_PERMISSION].ToUpper(), true.ToString().ToUpper(), true);

            // BaseSystemInfo.Service = ConfigurationManager.AppSettings[BaseConfiguration.SERVICE];

            if (ConfigurationManager.AppSettings["RecordLogOnLog"] != null)
            {
                BaseSystemInfo.RecordLogOnLog = ConfigurationManager.AppSettings["RecordLogOnLog"].ToUpper().Equals(true.ToString().ToUpper());
            }
            if (ConfigurationManager.AppSettings["RecordLog"] != null)
            {
                BaseSystemInfo.RecordLog = ConfigurationManager.AppSettings["RecordLog"].ToUpper().Equals(true.ToString().ToUpper());
            }
            if (ConfigurationManager.AppSettings["LogOnStatistics"] != null)
            {
                BaseSystemInfo.LogOnStatistics = ConfigurationManager.AppSettings["LogOnStatistics"].ToUpper().Equals(true.ToString().ToUpper());
            }
            if (ConfigurationManager.AppSettings["ServerEncryptPassword"] != null)
            {
                BaseSystemInfo.ServerEncryptPassword = ConfigurationManager.AppSettings["ServerEncryptPassword"].ToUpper().Equals(true.ToString().ToUpper());
            }
            if (ConfigurationManager.AppSettings["ClientEncryptPassword"] != null)
            {
                BaseSystemInfo.ClientEncryptPassword = ConfigurationManager.AppSettings["ClientEncryptPassword"].ToUpper().Equals(true.ToString().ToUpper());
            }
            if (ConfigurationManager.AppSettings["CookieExpires"] != null)
            {
                BaseSystemInfo.CookieExpires = int.Parse(ConfigurationManager.AppSettings["CookieExpires"].ToString());
            }
            if (ConfigurationManager.AppSettings["CheckIPAddress"] != null)
            {
                BaseSystemInfo.CheckIPAddress = ConfigurationManager.AppSettings["CheckIPAddress"].ToUpper().Equals(true.ToString().ToUpper());
            }
            if (ConfigurationManager.AppSettings["CheckPasswordStrength"] != null)
            {
                BaseSystemInfo.CheckPasswordStrength = ConfigurationManager.AppSettings["CheckPasswordStrength"].ToUpper().Equals(true.ToString().ToUpper());
            }
            if (ConfigurationManager.AppSettings["UseOrganizePermission"] != null)
            {
                BaseSystemInfo.UseOrganizePermission = ConfigurationManager.AppSettings["UseOrganizePermission"].ToUpper().Equals(true.ToString().ToUpper());
            }

            // BaseSystemInfo.AutoLogOn = (ConfigurationManager.AppSettings[BaseConfiguration.AUTO_LOGON].ToUpper(), true.ToString().ToUpper(), true);
            // BaseSystemInfo.LogOnAssembly = ConfigurationManager.AppSettings[BaseConfiguration.LOGON_ASSEMBLY];
            // BaseSystemInfo.LogOnForm = ConfigurationManager.AppSettings[BaseConfiguration.LOGON_FORM];
            // BaseSystemInfo.MainForm = ConfigurationManager.AppSettings[BaseConfiguration.MAIN_FORM];
            if (ConfigurationManager.AppSettings["CheckOnLine"] != null)
            {
                BaseSystemInfo.CheckOnLine = ConfigurationManager.AppSettings["CheckOnLine"].ToUpper().Equals(true.ToString().ToUpper());
            }
            if (ConfigurationManager.AppSettings["AllowUserRegister"] != null)
            {
                BaseSystemInfo.AllowUserRegister = ConfigurationManager.AppSettings["AllowUserRegister"].ToUpper().Equals(true.ToString().ToUpper());
            }

            if (ConfigurationManager.AppSettings["OpenNewWebWindow"] != null)
            {
                BaseSystemInfo.OpenNewWebWindow = ConfigurationManager.AppSettings["OpenNewWebWindow"].ToUpper().Equals(true.ToString().ToUpper());
            }

            // BaseSystemInfo.LoadAllUser = (ConfigurationManager.AppSettings[BaseConfiguration.LOAD_All_USER].ToUpper(), true.ToString().ToUpper(), true);
            
            // 数据库连接
            if (ConfigurationManager.AppSettings["ServerDbType"] != null)
            {
                BaseSystemInfo.ServerDbType = DbHelper.GetDbType(ConfigurationManager.AppSettings["ServerDbType"]);
            }
            if (ConfigurationManager.AppSettings["UserCenterDbType"] != null)
            {
                BaseSystemInfo.UserCenterDbType = DbHelper.GetDbType(ConfigurationManager.AppSettings["UserCenterDbType"]);
                BaseSystemInfo.LoginLogDbType = DbHelper.GetDbType(ConfigurationManager.AppSettings["UserCenterDbType"]);
                BaseSystemInfo.MessageDbType = DbHelper.GetDbType(ConfigurationManager.AppSettings["UserCenterDbType"]);
            }
            if (ConfigurationManager.AppSettings["BusinessDbType"] != null)
            {
                BaseSystemInfo.BusinessDbType = DbHelper.GetDbType(ConfigurationManager.AppSettings["BusinessDbType"]);
            }
            if (ConfigurationManager.AppSettings["WorkFlowDbType"] != null)
            {
                BaseSystemInfo.WorkFlowDbType = DbHelper.GetDbType(ConfigurationManager.AppSettings["WorkFlowDbType"]);
            }
            if (ConfigurationManager.AppSettings["LoginLogDbType"] != null)
            {
                BaseSystemInfo.LoginLogDbType = DbHelper.GetDbType(ConfigurationManager.AppSettings["LoginLogDbType"]);
            }
            if (ConfigurationManager.AppSettings["MessageDbType"] != null)
            {
                BaseSystemInfo.MessageDbType = DbHelper.GetDbType(ConfigurationManager.AppSettings["MessageDbType"]);
            }
            if (ConfigurationManager.AppSettings["EncryptDbConnection"] != null)
            {
                BaseSystemInfo.EncryptDbConnection = ConfigurationManager.AppSettings["EncryptDbConnection"].ToUpper().Equals(true.ToString().ToUpper());
            }
            
            if (ConfigurationManager.AppSettings["UserCenterDbConnection"] != null)
            {
                BaseSystemInfo.UserCenterDbConnectionString = ConfigurationManager.AppSettings["UserCenterDbConnection"];
                BaseSystemInfo.LoginLogDbConnectionString = ConfigurationManager.AppSettings["UserCenterDbConnection"];
                BaseSystemInfo.MessageDbConnectionString = ConfigurationManager.AppSettings["UserCenterDbConnection"];
            }

            if (ConfigurationManager.AppSettings["BusinessDbConnection"] != null)
            {
                BaseSystemInfo.BusinessDbConnectionString = ConfigurationManager.AppSettings["BusinessDbConnection"];
            }

            if (ConfigurationManager.AppSettings["MessageDbConnection"] != null)
            {
                BaseSystemInfo.MessageDbConnectionString = ConfigurationManager.AppSettings["MessageDbConnection"];
            }            
            if (ConfigurationManager.AppSettings["WorkFlowDbConnection"] != null)
            {
                BaseSystemInfo.WorkFlowDbConnectionString = ConfigurationManager.AppSettings["WorkFlowDbConnection"];
            }
            if (ConfigurationManager.AppSettings["LoginLogDbConnection"] != null)
            {
                BaseSystemInfo.LoginLogDbConnectionString = ConfigurationManager.AppSettings["LoginLogDbConnection"];
            }
            // 对加密的数据库连接进行解密操作
            if (BaseSystemInfo.EncryptDbConnection)
            {
                BaseSystemInfo.UserCenterDbConnection = SecretUtil.Decrypt(BaseSystemInfo.UserCenterDbConnectionString);
                BaseSystemInfo.BusinessDbConnection = SecretUtil.Decrypt(BaseSystemInfo.BusinessDbConnectionString);
                BaseSystemInfo.MessageDbConnection = SecretUtil.Decrypt(BaseSystemInfo.MessageDbConnectionString);
                BaseSystemInfo.WorkFlowDbConnection = SecretUtil.Decrypt(BaseSystemInfo.WorkFlowDbConnectionString);
                BaseSystemInfo.LoginLogDbConnection = SecretUtil.Decrypt(BaseSystemInfo.LoginLogDbConnectionString);
            }
            else
            {
                BaseSystemInfo.UserCenterDbConnection = BaseSystemInfo.UserCenterDbConnectionString;
                BaseSystemInfo.BusinessDbConnection = BaseSystemInfo.BusinessDbConnectionString;
                BaseSystemInfo.MessageDbConnection = BaseSystemInfo.MessageDbConnectionString;
                BaseSystemInfo.WorkFlowDbConnection = BaseSystemInfo.WorkFlowDbConnectionString;
                BaseSystemInfo.LoginLogDbConnection = BaseSystemInfo.LoginLogDbConnectionString;
            }

            BaseSystemInfo.UserCenterReadDbConnection = BaseSystemInfo.UserCenterDbConnection;
            BaseSystemInfo.UserCenterWriteDbConnection = BaseSystemInfo.UserCenterDbConnection;

            // 这里重新给静态数据库连接对象进行赋值
            // DotNet.Utilities.DbHelper.DbConnection = BaseSystemInfo.BusinessDbConnection;
            // DotNet.Utilities.DbHelper.DbType = BaseSystemInfo.BusinessDbType;

            // 这里是处理读写分离功能，读取数据与写入数据进行分离的方式
            if (ConfigurationManager.AppSettings["UserCenterReadDbConnection"] != null)
            {
                BaseSystemInfo.UserCenterReadDbConnection = ConfigurationManager.AppSettings["UserCenterReadDbConnection"];
            }
            if (ConfigurationManager.AppSettings["UserCenterWriteDbConnection"] != null)
            {
                BaseSystemInfo.UserCenterWriteDbConnection = ConfigurationManager.AppSettings["UserCenterWriteDbConnection"];
            }
        }
        #endregion
    }
}