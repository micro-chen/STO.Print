//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd.  
//-----------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml;

namespace DotNet.Utilities
{
    /// <summary>
    /// UserConfigHelper
    /// 访问用户配置文件的类
    /// 
    /// 修改记录
    ///     2015.07.31 版本：1.5 lhy      增加保存多个历史登录用户的记录功能。
    ///     2011.07.06 版本：1.4 zgl      增加对 CheckIPAddress 的操作
    ///		2008.06.08 版本：1.3 JiRiGaLa 命名修改为 ConfigHelper。
    ///		2008.04.22 版本：1.2 JiRiGaLa 从指定的文件读取配置项。
    ///		2007.07.31 版本：1.1 JiRiGaLa 规范化 FielName 变量。
    ///		2007.04.14 版本：1.0 JiRiGaLa 专门读取注册表的类，主键书写格式改进。
    ///		
    ///	版本：1.2
    /// 
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2008.04.22</date>
    /// </author> 
    /// </summary>
    public class UserConfigHelper
    {
        public static string LogOnTo = "Config";

        public static string FileName
        {
            get
            {
                return LogOnTo + ".xml";
            }
        }

        public static string SelectPath = "//appSettings/add";

        public static string ConfigFielName
        {
            get
            {
                return FileName;
                // return Application.StartupPath + "\\" + FielName;
            }
        }

        #region public static Dictionary<String, String> GetLogOnTo() 获取配置文件选项
        /// <summary>
        /// 获取配置文件选项
        /// </summary>
        /// <returns>配置文件设置</returns>
        public static Dictionary<String, String> GetLogOnTo()
        {
            Dictionary<String, String> result = new Dictionary<String, String>();
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(ConfigFielName);
            XmlNodeList xmlNodeList = xmlDocument.SelectNodes(SelectPath);
            foreach (XmlNode xmlNode in xmlNodeList)
            {
                if (xmlNode.Attributes["key"].Value.ToUpper().Equals("LogOnTo".ToUpper()))
                {
                    result.Add(xmlNode.Attributes["value"].Value, xmlNode.Attributes["dispaly"].Value);
                }
            }
            return result;
        }
        #endregion      

        public static bool Exists(string key)
        {
            return !string.IsNullOrEmpty(GetValue(key));
        }

        public static string[] GetOptions(string key)
        {
            string option = string.Empty;
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(ConfigFielName);
            option = GetOption(xmlDocument, SelectPath, key);
            return option.Split(',').Distinct<string>().Where(t => !string.IsNullOrEmpty(t)).ToArray();
        }

        #region public static string GetOption(XmlDocument xmlDocument, string selectPath, string key) 设置配置项
        /// <summary>
        /// 读取配置项
        /// </summary>
        /// <param name="xmlDocument">配置文件</param>
        /// <param name="selectPath">查询条件</param>
        /// <param name="key">键</param>
        /// <returns>值</returns>
        public static string GetOption(XmlDocument xmlDocument, string selectPath, string key)
        {
            string result = string.Empty;
            XmlNodeList xmlNodeList = xmlDocument.SelectNodes(selectPath);
            foreach (XmlNode xmlNode in xmlNodeList)
            {
                if (xmlNode.Attributes["key"].Value.ToUpper().Equals(key.ToUpper()))
                {
                    if (xmlNode.Attributes["Options"] != null)
                    {
                        result = xmlNode.Attributes["Options"].Value;
                        break;
                    }
                }
            }
            return result;
        }
        #endregion

        #region public static string GetValue(string key) 读取配置项
        /// <summary>
        /// 读取配置项
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>值</returns>
        public static string GetValue(string key, bool encrypt = false)
        {
            string result = string.Empty;
            result = GetValue(xmlDocument, SelectPath, key);
            if (!string.IsNullOrEmpty(result) && encrypt)
            {
                result = SecretUtil.Decrypt(result);
            }
            return result;
        }
        #endregion

        #region public static string GetValue(string fileName, string key) 读取配置项
        /// <summary>
        /// 读取配置项
        /// </summary>
        /// <param name="fileName">配置文件</param>
        /// <param name="key">键</param>
        /// <returns>值</returns>
        public static string GetValue(string fileName, string key)
        {
            return GetValue(fileName, SelectPath, key);
        }
        #endregion

        #region public static string GetValue(string fileName, string selectPath, string key) 设置配置项
        /// <summary>
        /// 读取配置项
        /// </summary>
        /// <param name="fileName">配置文件</param>
        /// <param name="selectPath">查询条件</param>
        /// <param name="key">键</param>
        /// <returns>值</returns>
        public static string GetValue(string fileName, string selectPath, string key)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(fileName);
            return GetValue(xmlDocument, selectPath, key);
        }
        #endregion

        #region public static string GetValue(XmlDocument xmlDocument, string key) 读取配置项
        /// <summary>
        /// 读取配置项
        /// </summary>
        /// <param name="xmlDocument">配置文件</param>
        /// <param name="key">键</param>
        /// <returns>值</returns>
        public static string GetValue(XmlDocument xmlDocument, string key)
        {
            return GetValue(xmlDocument, SelectPath, key);
        }
        #endregion

        #region public static string GetValue(XmlDocument xmlDocument, string selectPath, string key) 设置配置项
        /// <summary>
        /// 读取配置项
        /// </summary>
        /// <param name="xmlDocument">配置文件</param>
        /// <param name="selectPath">查询条件</param>
        /// <param name="key">键</param>
        /// <returns>值</returns>
        public static string GetValue(XmlDocument xmlDocument, string selectPath, string key)
        {
            string result = string.Empty;
            XmlNodeList xmlNodeList = xmlDocument.SelectNodes(selectPath);
            foreach (XmlNode xmlNode in xmlNodeList)
            {
                if (xmlNode.Attributes["key"].Value.ToUpper().Equals(key.ToUpper()))
                {
                    result = xmlNode.Attributes["value"].Value;
                    break;
                }
            }
            return result;
        }
        #endregion

        public static bool Exists()
        {
            bool result = false;
            string fileName = BaseSystemInfo.StartupPath + "//" + ConfigFielName;
            if (System.IO.File.Exists(ConfigFielName))
            {
                result = true;
            }
            return result;
        }

        #region public static void GetConfig() 读取配置文件
        /// <summary>
        /// 读取配置文件
        /// </summary>
        public static void GetConfig()
        {
            if (Exists())
            {
                string fileName = ConfigFielName;
                if (!string.IsNullOrEmpty(BaseSystemInfo.StartupPath))
                {
                    fileName = BaseSystemInfo.StartupPath + "\\" + ConfigFielName;
                }
                GetConfig(fileName);
            }
        }
        #endregion

        private static XmlDocument xmlDocument = new XmlDocument();

        public static void GetConfig(Stream stream)
        {
            xmlDocument.Load(stream);
            GetConfig(xmlDocument);
        }

        /// <summary>
        /// 从指定的文件读取配置项
        /// </summary>
        /// <param name="fileName">配置文件</param>
        public static void GetConfig(string fileName)
        {
            xmlDocument.Load(fileName);
            GetConfig(xmlDocument);
        }

        #region public static void GetConfig() 从指定的文件读取配置项
        /// <summary>
        /// 从指定的文件读取配置项
        /// </summary>
        public static void GetConfig(XmlDocument document)
        {
            xmlDocument = document;
            if (Exists("ConfigFile"))
            {
                BaseSystemInfo.ConfigFile = GetValue(xmlDocument, "ConfigFile");
            }
            if (Exists("Host"))
            {
                BaseSystemInfo.Host = GetValue(xmlDocument, "Host");
            }
            if (Exists("Port"))
            {
                int.TryParse(GetValue(xmlDocument, "Port"), out BaseSystemInfo.Port);
            }
            if (Exists("MobileHost"))
            {
                if (!string.IsNullOrWhiteSpace(GetValue(xmlDocument, "MobileHost")))
                {
                    BaseSystemInfo.MobileHost = GetValue(xmlDocument, "MobileHost");
                }
            }
            // 客户信息配置
            if (Exists("NeedRegister"))
            {
                BaseSystemInfo.NeedRegister = (String.Compare(GetValue(xmlDocument, "NeedRegister"), "TRUE", true, CultureInfo.CurrentCulture) == 0);
            }
            // 客户信息配置
            if (Exists("CurrentCompany"))
            {
                BaseSystemInfo.CurrentCompany = GetValue(xmlDocument, "CurrentCompany");
            }
            if (Exists("CurrentUserName"))
            {
                BaseSystemInfo.CurrentUserName = GetValue(xmlDocument, "CurrentUserName");
            }
            if (Exists("CurrentNickName"))
            {
                BaseSystemInfo.CurrentNickName = GetValue(xmlDocument, "CurrentNickName");
            }
            if (Exists("CurrentPassword"))
            {
                BaseSystemInfo.CurrentPassword = GetValue(xmlDocument, "CurrentPassword");
            }
            //历史登录用户记录
            if (Exists("HistoryUsers"))
            {
                string strUserList = GetValue(xmlDocument, "HistoryUsers");
                string[] userInfoArray = strUserList.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                BaseSystemInfo.HistoryUsers = userInfoArray;
            }if (Exists("MultiLanguage"))
            {
                BaseSystemInfo.MultiLanguage = (String.Compare(GetValue(xmlDocument, "MultiLanguage"), "TRUE", true, CultureInfo.CurrentCulture) == 0);
            }
            if (Exists("CurrentLanguage"))
            {
                BaseSystemInfo.CurrentLanguage = GetValue(xmlDocument, "CurrentLanguage");
            }
            if (Exists("RememberPassword"))
            {
                BaseSystemInfo.RememberPassword = (String.Compare(GetValue(xmlDocument, "RememberPassword"), "TRUE", true, CultureInfo.CurrentCulture) == 0);
            }
            if (Exists("OnInternet"))
            {
                BaseSystemInfo.OnInternet = (String.Compare(GetValue(xmlDocument, "OnInternet"), "TRUE", true, CultureInfo.CurrentCulture) == 0);
            }
            if (Exists("AutoLogOn"))
            {
                BaseSystemInfo.AutoLogOn = (String.Compare(GetValue(xmlDocument, "AutoLogOn"), "TRUE", true, CultureInfo.CurrentCulture) == 0);
            }
            if (Exists("ClientEncryptPassword"))
            {
                BaseSystemInfo.ClientEncryptPassword = (String.Compare(GetValue(xmlDocument, "ClientEncryptPassword"), "TRUE", true, CultureInfo.CurrentCulture) == 0);
            }
            if (Exists("ServerEncryptPassword"))
            {
                BaseSystemInfo.ServerEncryptPassword = (String.Compare(GetValue(xmlDocument, "ServerEncryptPassword"), "TRUE", true, CultureInfo.CurrentCulture) == 0);
            }

            if (Exists("OpenNewWebWindow"))
            {
                BaseSystemInfo.OpenNewWebWindow = (String.Compare(GetValue(xmlDocument, "OpenNewWebWindow"), "TRUE", true, CultureInfo.CurrentCulture) == 0);
            }

            // add by zgl
            if (Exists("CheckIPAddress"))
            {
                BaseSystemInfo.CheckIPAddress = (String.Compare(GetValue(xmlDocument, "CheckIPAddress"), "TRUE", true, CultureInfo.CurrentCulture) == 0);
            }
            if (Exists("CheckOnLine"))
            {
                BaseSystemInfo.CheckOnLine = (String.Compare(GetValue(xmlDocument, "CheckOnLine"), "TRUE", true, CultureInfo.CurrentCulture) == 0);
            }
            if (Exists("UseMessage"))
            {
                BaseSystemInfo.UseMessage = (String.Compare(GetValue(xmlDocument, "UseMessage"), "TRUE", true, CultureInfo.CurrentCulture) == 0);
            }
            if (Exists("Synchronous"))
            {
                BaseSystemInfo.Synchronous = (String.Compare(GetValue(xmlDocument, "Synchronous"), "TRUE", true, CultureInfo.CurrentCulture) == 0);
            }
            if (Exists("CheckBalance"))
            {
                BaseSystemInfo.CheckBalance = (String.Compare(GetValue(xmlDocument, "CheckBalance"), "TRUE", true, CultureInfo.CurrentCulture) == 0);
            }
            if (Exists("AllowUserRegister"))
            {
                BaseSystemInfo.AllowUserRegister = (String.Compare(GetValue(xmlDocument, "AllowUserRegister"), "TRUE", true, CultureInfo.CurrentCulture) == 0);
            }
            if (Exists("RecordLog"))
            {
                BaseSystemInfo.RecordLog = (String.Compare(GetValue(xmlDocument, "RecordLog"), "TRUE", true, CultureInfo.CurrentCulture) == 0);
            }

            if (Exists("CustomerCompanyName"))
            {
                BaseSystemInfo.CustomerCompanyName = GetValue(xmlDocument, "CustomerCompanyName");
            }
            if (Exists("ConfigurationFrom"))
            {
                BaseSystemInfo.ConfigurationFrom = BaseConfiguration.GetConfiguration(GetValue(xmlDocument, "ConfigurationFrom"));
            }
            if (Exists("SoftName"))
            {
                BaseSystemInfo.SoftName = GetValue(xmlDocument, "SoftName");
            }
            if (Exists("SoftFullName"))
            {
                BaseSystemInfo.SoftFullName = GetValue(xmlDocument, "SoftFullName");
            }
            
            if (Exists("SystemCode"))
            {
                BaseSystemInfo.SystemCode = GetValue(xmlDocument, "SystemCode");
            }
            if (Exists("Version"))
            {
                BaseSystemInfo.Version = GetValue(xmlDocument, "Version");
            }
            
            if (Exists("UseOrganizePermission"))
            {
                BaseSystemInfo.UseOrganizePermission = (String.Compare(GetValue(xmlDocument, "UseOrganizePermission"), "TRUE", true, CultureInfo.CurrentCulture) == 0);
            }
            if (Exists("UseUserPermission"))
            {
                BaseSystemInfo.UseUserPermission = (String.Compare(GetValue(xmlDocument, "UseUserPermission"), "TRUE", true, CultureInfo.CurrentCulture) == 0);
            }
            if (Exists("UseTableColumnPermission"))
            {
                BaseSystemInfo.UseTableColumnPermission = (String.Compare(GetValue(xmlDocument, "UseTableColumnPermission"), "TRUE", true, CultureInfo.CurrentCulture) == 0);
            }
            if (Exists("UseTableScopePermission"))
            {
                BaseSystemInfo.UseTableScopePermission = (String.Compare(GetValue(xmlDocument, "UseTableScopePermission"), "TRUE", true, CultureInfo.CurrentCulture) == 0);
            }
            if (Exists("UsePermissionScope"))
            {
                BaseSystemInfo.UsePermissionScope = (String.Compare(GetValue(xmlDocument, "UsePermissionScope"), "TRUE", true, CultureInfo.CurrentCulture) == 0);
            }
            if (Exists("UseAuthorizationScope"))
            {
                BaseSystemInfo.UseAuthorizationScope = (String.Compare(GetValue(xmlDocument, "UseAuthorizationScope"), "TRUE", true, CultureInfo.CurrentCulture) == 0);
            }
            if (Exists("HandwrittenSignature"))
            {
                BaseSystemInfo.HandwrittenSignature = (String.Compare(GetValue(xmlDocument, "HandwrittenSignature"), "TRUE", true, CultureInfo.CurrentCulture) == 0);
            }

            //if (Exists("LoadAllUser"))
            //{
            //    BaseSystemInfo.LoadAllUser = (String.Compare(GetValue(xmlDocument, "LoadAllUser"), "TRUE", true, CultureInfo.CurrentCulture) == 0);
            //}

            if (Exists("Service"))
            {
                BaseSystemInfo.Service = GetValue(xmlDocument, "Service");
            }
            if (Exists("LogOnForm"))
            {
                BaseSystemInfo.LogOnForm = GetValue(xmlDocument, "LogOnForm");
            }
            if (Exists("MainForm"))
            {
                BaseSystemInfo.MainForm = GetValue(xmlDocument, "MainForm");
            }
            if (Exists("OnLineLimit"))
            {
                int.TryParse(GetValue(xmlDocument, "OnLineLimit"), out BaseSystemInfo.OnLineLimit);
            }
            if (Exists("UserCenterDbType"))
            {
                BaseSystemInfo.UserCenterDbType = DbHelper.GetDbType(GetValue(xmlDocument, "UserCenterDbType"));
                BaseSystemInfo.MessageDbType = BaseSystemInfo.UserCenterDbType;
                BaseSystemInfo.BusinessDbType = BaseSystemInfo.UserCenterDbType;
                BaseSystemInfo.WorkFlowDbType = BaseSystemInfo.UserCenterDbType;
                BaseSystemInfo.LoginLogDbType = BaseSystemInfo.UserCenterDbType;
            }
            // 打开数据库连接
            if (Exists("MessageDbType"))
            {
                BaseSystemInfo.MessageDbType = DbHelper.GetDbType(GetValue(xmlDocument, "MessageDbType"));
            }
            if (Exists("WorkFlowDbType"))
            {
                BaseSystemInfo.WorkFlowDbType = DbHelper.GetDbType(GetValue(xmlDocument, "WorkFlowDbType"));
            }
            if (Exists("BusinessDbType"))
            {
                BaseSystemInfo.BusinessDbType = DbHelper.GetDbType(GetValue(xmlDocument, "BusinessDbType"));
            }
            if (Exists("LoginLogDbType"))
            {
                BaseSystemInfo.LoginLogDbType = DbHelper.GetDbType(GetValue(xmlDocument, "LoginLogDbType"));
            }

            if (Exists("UserCenterDbConnection"))
            {
                BaseSystemInfo.UserCenterDbConnectionString = GetValue(xmlDocument, "UserCenterDbConnection");
                BaseSystemInfo.UserCenterReadDbConnection = BaseSystemInfo.UserCenterDbConnectionString;
                BaseSystemInfo.UserCenterWriteDbConnection = BaseSystemInfo.UserCenterDbConnectionString;
                BaseSystemInfo.MessageDbConnectionString = BaseSystemInfo.UserCenterDbConnectionString;
                BaseSystemInfo.LoginLogDbConnectionString = BaseSystemInfo.UserCenterDbConnectionString;
                BaseSystemInfo.BusinessDbConnectionString = BaseSystemInfo.UserCenterDbConnectionString;
                // BaseSystemInfo.WorkFlowDbConnectionString = BaseSystemInfo.UserCenterDbConnectionString;
            }

            if (Exists("OrganizeDynamicLoading"))
            {
                BaseSystemInfo.OrganizeDynamicLoading = (String.Compare(GetValue(xmlDocument, "OrganizeDynamicLoading"), "TRUE", true, CultureInfo.CurrentCulture) == 0);
            }
            if (Exists("MessageDbConnection"))
            {
                BaseSystemInfo.MessageDbConnectionString = GetValue(xmlDocument, "MessageDbConnection");
            }
            if (Exists("LoginLogDbConnection"))
            {
                BaseSystemInfo.LoginLogDbConnectionString = GetValue(xmlDocument, "LoginLogDbConnection");
            }
            if (Exists("WorkFlowDbConnection"))
            {
                BaseSystemInfo.WorkFlowDbConnectionString = GetValue(xmlDocument, "WorkFlowDbConnection");
            }
            if (Exists("BusinessDbConnection"))
            {
                BaseSystemInfo.BusinessDbConnectionString = GetValue(xmlDocument, "BusinessDbConnection");
            }

            BaseSystemInfo.UserCenterDbConnection = BaseSystemInfo.UserCenterDbConnectionString;
            BaseSystemInfo.LoginLogDbConnection = BaseSystemInfo.LoginLogDbConnectionString;
            BaseSystemInfo.MessageDbConnection = BaseSystemInfo.MessageDbConnectionString;
            BaseSystemInfo.BusinessDbConnection = BaseSystemInfo.BusinessDbConnectionString;
            BaseSystemInfo.WorkFlowDbConnection = BaseSystemInfo.WorkFlowDbConnectionString;
            
            if (Exists("EncryptDbConnection"))
            {
                BaseSystemInfo.EncryptDbConnection = (String.Compare(GetValue(xmlDocument, "EncryptDbConnection"), "TRUE", true, CultureInfo.CurrentCulture) == 0);

                if (BaseSystemInfo.EncryptDbConnection)
                {
                    BaseSystemInfo.UserCenterDbConnection = SecretUtil.Decrypt(BaseSystemInfo.UserCenterDbConnectionString);
                    BaseSystemInfo.LoginLogDbConnection = SecretUtil.Decrypt(BaseSystemInfo.LoginLogDbConnectionString);
                    BaseSystemInfo.MessageDbConnection = SecretUtil.Decrypt(BaseSystemInfo.MessageDbConnectionString);
                    BaseSystemInfo.BusinessDbConnection = SecretUtil.Decrypt(BaseSystemInfo.BusinessDbConnectionString);
                    BaseSystemInfo.WorkFlowDbConnection = SecretUtil.Decrypt(BaseSystemInfo.WorkFlowDbConnectionString);
                }
            }

            // 若是本地模式运行，然后还缺少数据库配置？
            if (BaseSystemInfo.Service.Equals("DotNet.Business"))
            {
                if (string.IsNullOrEmpty(BaseSystemInfo.UserCenterDbConnection))
                {
                    BaseSystemInfo.UserCenterDbConnection = "Data Source=localhost;Initial Catalog=UserCenterV42;Integrated Security=SSPI;";
                }
                if (string.IsNullOrEmpty(BaseSystemInfo.LoginLogDbConnection))
                {
                    BaseSystemInfo.LoginLogDbConnection = "Data Source=localhost;Initial Catalog=UserCenterV42;Integrated Security=SSPI;";
                }
                if (string.IsNullOrEmpty(BaseSystemInfo.MessageDbConnection))
                {
                    BaseSystemInfo.MessageDbConnection = BaseSystemInfo.UserCenterDbConnection;
                }
                if (string.IsNullOrEmpty(BaseSystemInfo.WorkFlowDbConnection))
                {
                    // BaseSystemInfo.WorkFlowDbConnection = "Data Source=localhost;Initial Catalog=WorkFlowV42;Integrated Security=SSPI;";
                }
                if (string.IsNullOrEmpty(BaseSystemInfo.BusinessDbConnection))
                {
                    // BaseSystemInfo.BusinessDbConnection = "Data Source=localhost;Initial Catalog=ProjectV42;Integrated Security=SSPI;";
                }
            }

            if (Exists("ServiceUserName"))
            {
                BaseSystemInfo.ServiceUserName = GetValue(xmlDocument, "ServiceUserName");
            }
            if (Exists("ServicePassword"))
            {
                BaseSystemInfo.ServicePassword = GetValue(xmlDocument, "ServicePassword");
            }
            if (Exists("RegisterKey"))
            {
                BaseSystemInfo.RegisterKey = GetValue(xmlDocument, "RegisterKey");
            }

            // 错误报告相关
            if (Exists("ErrorReportTo"))
            {
                BaseSystemInfo.ErrorReportTo = GetValue(xmlDocument, "ErrorReportTo");
            }
            if (Exists("MailServer"))
            {
                BaseSystemInfo.MailServer = GetValue(xmlDocument, "MailServer");
            }
            if (Exists("MailUserName"))
            {
                BaseSystemInfo.MailUserName = GetValue(xmlDocument, "MailUserName");
            }
            if (Exists("MailPassword"))
            {
                BaseSystemInfo.MailPassword = GetValue(xmlDocument, "MailPassword");
                // BaseSystemInfo.MailPassword = SecretUtil.Encrypt(BaseSystemInfo.MailPassword);
                // BaseSystemInfo.MailPassword = SecretUtil.Decrypt(BaseSystemInfo.MailPassword);
            }
            if (Exists("UploadBlockSize"))
            {
                BaseSystemInfo.UploadBlockSize = Convert.ToInt32(GetValue(xmlDocument, "UploadBlockSize"));
            }
            if (Exists("UploadStorageMode"))
            {
                BaseSystemInfo.UploadStorageMode = GetValue(xmlDocument, "UploadStorageMode");
            }
            if (Exists("UploadPath"))
            {
                BaseSystemInfo.UploadPath = GetValue(xmlDocument, "UploadPath");
            }
            if (Exists("UseWorkFlow"))
            {
                BaseSystemInfo.UseWorkFlow = (String.Compare(GetValue(xmlDocument, "UseWorkFlow"), "TRUE", true, CultureInfo.CurrentCulture) == 0);
            }
            
            // 这里重新给静态数据库连接对象进行赋值
            // DotNet.Utilities.DbHelper.DbConnection = BaseSystemInfo.BusinessDbConnection;
            // DotNet.Utilities.DbHelper.DbType = BaseSystemInfo.BusinessDbType;

            // 这里是处理读写分离功能，读取数据与写入数据进行分离的方式
            if (Exists("UserCenterReadDbConnection"))
            {
                BaseSystemInfo.UserCenterReadDbConnection = GetValue(xmlDocument, "UserCenterReadDbConnection");
            }
            if (Exists("UserCenterWriteDbConnection"))
            {
                BaseSystemInfo.UserCenterWriteDbConnection = GetValue(xmlDocument, "UserCenterWriteDbConnection");
            }
        }
        #endregion

        public static void SetValue(string key, string keyValue, bool checkExists = false)
        {
            if (System.IO.File.Exists(ConfigFielName))
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(ConfigFielName);
                SetValue(xmlDocument, key, keyValue, checkExists);
                xmlDocument.Save(ConfigFielName);
            }
        }

        public static bool SetValue(XmlDocument xmlDocument, string key, string keyValue, bool checkExists = false)
        {
            return SetValue(xmlDocument, SelectPath, key, keyValue, checkExists);
        }

        public static bool SetValue(XmlDocument xmlDocument, string selectPath, string key, string keyValue, bool checkExists = false)
        {
            bool result = false;
            bool exists = false;
            XmlNodeList xmlNodeList = xmlDocument.SelectNodes(selectPath);
            foreach (XmlNode xmlNode in xmlNodeList)
            {
                if (xmlNode.Attributes["key"].Value.ToUpper().Equals(key.ToUpper()))
                {
                    xmlNode.Attributes["value"].Value = keyValue;
                    result = true;
                    exists = true;
                    break;
                }
            }
            if (checkExists && !exists)
            {
                XmlElement xmlElement = xmlDocument.CreateElement("add");
                // xmlElement.Value = keyValue;
                xmlElement.SetAttribute("key", key);
                xmlElement.SetAttribute("value", keyValue);
                string selectPathRoot = "//appSettings";
                XmlNode xmlNode = xmlDocument.SelectSingleNode(selectPathRoot);
                xmlNode.AppendChild(xmlElement);
            }
            return result;
        }

        #region public static void SaveConfig() 保存配置文件
        /// <summary>
        /// 保存配置文件
        /// </summary>
        public static void SaveConfig()
        {
            if (System.IO.File.Exists(ConfigFielName))
            {
                SaveConfig(ConfigFielName);
            }
        }
        #endregion

        #region public static void SaveConfig(string fileName) 保存到指定的文件
        /// <summary>
        /// 保存到指定的文件
        /// </summary>
        /// <param name="fileName">配置文件</param>
        public static void SaveConfig(string fileName)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(fileName);

            SetValue(xmlDocument, "ConfigFile", BaseSystemInfo.ConfigFile);

            SetValue(xmlDocument, "CurrentNickName", BaseSystemInfo.CurrentNickName, true);
            SetValue(xmlDocument, "CurrentCompany", BaseSystemInfo.CurrentCompany);
            SetValue(xmlDocument, "CurrentUserName", BaseSystemInfo.CurrentUserName);
            SetValue(xmlDocument, "CurrentPassword", BaseSystemInfo.CurrentPassword);

            // 保存历史登录用户信息。
            SetValue(xmlDocument, "HistoryUsers", GetHistoryUsersSaveValue(), true);
            
            SetValue(xmlDocument, "MultiLanguage", BaseSystemInfo.MultiLanguage.ToString());
            SetValue(xmlDocument, "CurrentLanguage", BaseSystemInfo.CurrentLanguage);
            SetValue(xmlDocument, "OnInternet", BaseSystemInfo.OnInternet.ToString());
            SetValue(xmlDocument, "RememberPassword", BaseSystemInfo.RememberPassword.ToString());

            SetValue(xmlDocument, "ClientEncryptPassword", BaseSystemInfo.ClientEncryptPassword.ToString());
            SetValue(xmlDocument, "CheckIPAddress", BaseSystemInfo.CheckIPAddress.ToString());//add by zgl
            SetValue(xmlDocument, "UploadBlockSize", BaseSystemInfo.UploadBlockSize.ToString());//add by HJC

            SetValue(xmlDocument, "ServerEncryptPassword", BaseSystemInfo.ServerEncryptPassword.ToString());
            SetValue(xmlDocument, "PasswordMiniLength", BaseSystemInfo.PasswordMiniLength.ToString());
            SetValue(xmlDocument, "NumericCharacters", BaseSystemInfo.NumericCharacters.ToString());
            SetValue(xmlDocument, "PasswordChangeCycle", BaseSystemInfo.PasswordChangeCycle.ToString());
            SetValue(xmlDocument, "CheckOnLine", BaseSystemInfo.CheckOnLine.ToString());
            SetValue(xmlDocument, "AccountMinimumLength", BaseSystemInfo.AccountMinimumLength.ToString());
            SetValue(xmlDocument, "PasswordErrorLockLimit", BaseSystemInfo.PasswordErrorLockLimit.ToString());
            SetValue(xmlDocument, "PasswordErrorLockCycle", BaseSystemInfo.PasswordErrorLockCycle.ToString());
            SetValue(xmlDocument, "OpenNewWebWindow", BaseSystemInfo.OpenNewWebWindow.ToString());

            SetValue(xmlDocument, "UseMessage", BaseSystemInfo.UseMessage.ToString(), true);
            SetValue(xmlDocument, "Synchronous", BaseSystemInfo.Synchronous.ToString(), true);
            SetValue(xmlDocument, "CheckBalance", BaseSystemInfo.CheckBalance.ToString(), true);
            SetValue(xmlDocument, "AutoLogOn", BaseSystemInfo.AutoLogOn.ToString());
            SetValue(xmlDocument, "AllowUserRegister", BaseSystemInfo.AllowUserRegister.ToString());
            SetValue(xmlDocument, "RecordLog", BaseSystemInfo.RecordLog.ToString());

            // 客户信息配置
            SetValue(xmlDocument, "CustomerCompanyName", BaseSystemInfo.CustomerCompanyName);
            SetValue(xmlDocument, "SoftName", BaseSystemInfo.SoftName);
            SetValue(xmlDocument, "SoftFullName", BaseSystemInfo.SoftFullName);
            SetValue(xmlDocument, "Version", BaseSystemInfo.Version);

            SetValue(xmlDocument, "ConfigurationFrom", BaseSystemInfo.ConfigurationFrom.ToString());
            SetValue(xmlDocument, "Host", BaseSystemInfo.Host);
            SetValue(xmlDocument, "Port", BaseSystemInfo.Port.ToString());
            SetValue(xmlDocument, "MobileHost", BaseSystemInfo.MobileHost);
           
            SetValue(xmlDocument, "UseUserPermission", BaseSystemInfo.UseUserPermission.ToString());
            SetValue(xmlDocument, "UseAuthorizationScope", BaseSystemInfo.UseAuthorizationScope.ToString());
            SetValue(xmlDocument, "UsePermissionScope", BaseSystemInfo.UsePermissionScope.ToString());
            SetValue(xmlDocument, "UseOrganizePermission", BaseSystemInfo.UseOrganizePermission.ToString());
            SetValue(xmlDocument, "UseTableColumnPermission", BaseSystemInfo.UseTableColumnPermission.ToString());
            SetValue(xmlDocument, "UseTableScopePermission", BaseSystemInfo.UseTableScopePermission.ToString());
            SetValue(xmlDocument, "UseWorkFlow", BaseSystemInfo.UseWorkFlow.ToString());
            // SetValue(xmlDocument, "LoadAllUser", BaseSystemInfo.LoadAllUser.ToString());

            SetValue(xmlDocument, "Service", BaseSystemInfo.Service);

            SetValue(xmlDocument, "LogOnForm", BaseSystemInfo.LogOnForm);
            SetValue(xmlDocument, "MainForm", BaseSystemInfo.MainForm);

            SetValue(xmlDocument, "OnLineLimit", BaseSystemInfo.OnLineLimit.ToString());
            SetValue(xmlDocument, "DbType", BaseSystemInfo.BusinessDbType.ToString());
            
            // 保存数据库配置
            SetValue(xmlDocument, "UserCenterDbType", BaseSystemInfo.UserCenterDbType.ToString());
            SetValue(xmlDocument, "MessageDbType", BaseSystemInfo.MessageDbType.ToString());
            SetValue(xmlDocument, "BusinessDbType", BaseSystemInfo.BusinessDbType.ToString());
            SetValue(xmlDocument, "WorkFlowDbType", BaseSystemInfo.WorkFlowDbType.ToString());

            SetValue(xmlDocument, "EncryptDbConnection", BaseSystemInfo.EncryptDbConnection.ToString());
            SetValue(xmlDocument, "UserCenterDbConnection", BaseSystemInfo.UserCenterDbConnectionString);
            SetValue(xmlDocument, "MessageDbConnection", BaseSystemInfo.MessageDbConnectionString);
            SetValue(xmlDocument, "BusinessDbConnection", BaseSystemInfo.BusinessDbConnectionString);
            SetValue(xmlDocument, "WorkFlowDbConnection", BaseSystemInfo.WorkFlowDbConnectionString);

            SetValue(xmlDocument, "RegisterKey", BaseSystemInfo.RegisterKey);

            xmlDocument.Save(fileName);
        }

        /// <summary>
        /// 将历史登录用户信息数组转换成保存到配置文件中的字符串。
        /// </summary>
        /// <returns></returns>
        private static string GetHistoryUsersSaveValue()
        {
            string strUsers = "";
            for (int index = 0; index < BaseSystemInfo.HistoryUsers.Length; index++)
            {
                if (strUsers == "")
                    strUsers = BaseSystemInfo.HistoryUsers[index];
                else
                    strUsers = string.Format("{0};{1}", strUsers, BaseSystemInfo.HistoryUsers[index]);
            }
            return strUsers;
        }
        #endregion
    }
}