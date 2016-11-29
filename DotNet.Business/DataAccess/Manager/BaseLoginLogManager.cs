//-----------------------------------------------------------------------
// <copyright file="BaseLoginLogManager.Auto.cs" company="Hairihan">
//     Copyright (C) 2016 , All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Specialized;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace DotNet.Business
{
    using DotNet.Model;
    using DotNet.Utilities;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// BaseLoginLogManager
    /// 系统登录日志表
    /// 
    /// 修改记录
    /// 
    /// 2014-09-05 版本：2.0 JiRiGaLa 获取IP地址城市的功能实现。
    /// 2014-03-18 版本：1.0 JiRiGaLa 创建文件。
    /// 
    /// <author>
    ///     <name>JiRiGaLa</name>
    ///     <date>2014-03-18</date>
    /// </author>
    /// </summary>
    public partial class BaseLoginLogManager : BaseManager, IBaseManager
    {
        /// <summary>
        /// 获取切割的表名
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        /// <returns>分割后的表名</returns>
        public static string GetSplitTableName(BaseUserInfo userInfo)
        {
            return GetSplitTableName(userInfo.CompanyId);
        }

        public static string GetSplitTableName(BaseUserEntity userInfo)
        {
            return GetSplitTableName(userInfo.CompanyId);
        }

        /// <summary>
        /// 获取切割的表名，这个是决定未来方向的函数，
        /// 不断改进完善，会是我们未来的方向
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        /// <returns>分割后的表名</returns>
        public static string GetSplitTableName(string companyId = null)
        {
            // 默认表名字
            string result = BaseLoginLogEntity.TableName;

            /*

            // 目前主要是Oracle数据库上进行且分、将来可以是所有的数据库上进行切分
            if (BaseSystemInfo.ServerDbType == CurrentDbType.Oracle)
            {
                // result = "BASE_LOGINLOG" + DateTime.Now.ToString("yyyy");
                result = "BASE_LOGINLOG";

                // 这里需要判断网点主键是否为空？
                if (!string.IsNullOrEmpty(companyId))
                {
                    // 对网点主键进行尾部2位截取
                    string db = companyId;
                    if (companyId.Length > 1)
                    {
                        db = companyId.Substring(companyId.Length - 2);
                    }
                    else
                    {
                        // 若不够2位补充前导0，补充为2位长，这里千万不能忘了，否则容易出错误。
                        db = "0" + db;
                    }
                    // 检查你截取到的是否为正确的数字类型？若不是数字类型的，就不能分发到表里去
                    if (ValidateUtil.IsInt(db))
                    {
                        // result = result + "_" + DateTime.Now.ToString("yyyy") + "_" + db;
                        result = result + "_" + db;
                    }
                }
            }
            
            */
            return result;
        }


        #region 选择服务商返回地址  1.百度 2.新浪 3.淘宝
        /// <summary>
        /// 选择服务商返回地址   1.百度 2.新浪 3.淘宝
        /// </summary>
        /// <param name="ipAddress">IP地址</param>
        /// <param name="serve">1.百度 2.新浪 3.淘宝</param>
        /// <returns></returns>
        public static string GetIpAddressNameByWebService(string ipAddress, int serve = 2)
        {
            try
            {
                //过滤掉端口号 
                if (ipAddress.IndexOf(':') > 0)
                {
                    ipAddress = ipAddress.Split(':')[0];
                }
                var match =
                    new Regex(@"((?:(?:25[0-5]|2[0-4]\d|((1\d{2})|([1-9]?\d)))\.){3}(?:25[0-5]|2[0-4]\d|((1\d{2})|([1-9]?\d))))");
                if (!match.IsMatch(ipAddress))
                {
                    return string.Empty;
                }
                var webClient = new System.Net.WebClient();
                NameValueCollection postValues = null;
                // 向服务器发送POST数据
                var url = string.Empty;
                if (serve == 1)
                {
                    url = "http://api.map.baidu.com/location/ip";
                    postValues = new NameValueCollection
                            {
                                {"ak", "MRkBd6jnGOf8O5F58KKrvit5"},
                                {"ip", ipAddress},
                                {"coor", "bd09ll"}
                            };
                }
                else if (serve == 2)
                {
                    //此处是个坑  只支持拼接字符串的请求 不支持form表单得到
                    url = "http://int.dpool.sina.com.cn/iplookup/iplookup.php?format=json&ip=" + ipAddress;
                    postValues = new NameValueCollection
                            {
                                {"format", "json"},
                                {"ip", ipAddress}
                            };
                }
                else
                {
                    url = "http://ip.taobao.com/service/getIpInfo.php";
                    postValues = new NameValueCollection
                            {
                                {"ip", ipAddress}
                            };
                }

                byte[] responseArray = webClient.UploadValues(url, postValues);

                string response = Encoding.UTF8.GetString(responseArray);

                var dataJson = JObject.Parse(response);  //动态解析  正常的解析无法生效
                string address = string.Empty;
                //百度接口
                if (serve == 1)
                {
                    if (dataJson["status"].ToString() == "0")
                    {
                        address = dataJson["content"]["address_detail"]["province"] + "-" + dataJson["content"]["address_detail"]["city"];
                    }
                }
                //新浪接口
                else if (serve == 2)
                {
                    if (dataJson["ret"].ToString() == "1")
                    {
                        address = dataJson["province"] + "-" + dataJson["city"];
                    }
                }
                //淘宝接口
                else
                {
                    if (dataJson["code"].ToString() == "0")
                    {
                        if (!string.IsNullOrEmpty(dataJson["data"]["region"].ToString()))
                            address = dataJson["data"]["region"] + "-" + dataJson["data"]["city"];
                    }
                }
                if (string.IsNullOrEmpty(address))
                {
                    address = "局域网";
                }
                return address;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return string.Empty;
            }
        }
        #endregion

        public int GetIPAddressName(IpHelper ipHelper, string tableName = null)
        {
            int result = 0;

            if (string.IsNullOrEmpty(tableName))
            {
                tableName = BaseLoginLogEntity.TableName;
                /*
                if (BaseSystemInfo.BusinessDbType == CurrentDbType.Oracle)
                {
                    tableName = BaseLoginLogEntity.TableName + DateTime.Now.ToString("yyyyMM");
                }
                */
            }

            using (IDbHelper dbHelper = DbHelperFactory.GetHelper(BaseSystemInfo.LoginLogDbType, BaseSystemInfo.LoginLogDbConnection))
            {
                string id = string.Empty;
                string ipaddress = string.Empty;
                string ipAddressName = string.Empty;
                string commandText = "SELECT id, ipaddress FROM " + tableName + " WHERE ipaddressname IS NULL AND ipaddress IS NOT NULL AND ROWNUM < 10000 ORDER BY createon DESC ";
                using (IDataReader dataReader = DbHelper.ExecuteReader(commandText))
                {
                    while (dataReader.Read())
                    {
                        id = dataReader["id"].ToString();
                        ipaddress = dataReader["ipaddress"].ToString();
                        ipAddressName = ipHelper.FindName(ipaddress);
                        if (!string.IsNullOrWhiteSpace(ipAddressName))
                        {
                            commandText = "UPDATE " + tableName + " SET ipAddressName = '" + ipAddressName + "' WHERE Id = '" + id + "'";
                            dbHelper.ExecuteNonQuery(commandText);
                            System.Console.WriteLine(ipaddress + ":" + ipAddressName);
                        }
                        else
                        {
                            ipAddressName = string.Empty;
                        }
                        result++;
                    }
                    dataReader.Close();
                }
            }
            return result;
        }

        private static void AddLogTaskByBaseUserInfo(object param)
        {
            var tuple = param as Tuple<string, BaseUserInfo, string, string, string, string>;
            string systemCode = tuple.Item1;
            BaseUserInfo userInfo = tuple.Item2;
            string ipAddress = tuple.Item3;
            string ipAddressName = tuple.Item4;
            string macAddress = tuple.Item5;
            string loginStatus = tuple.Item6;

            BaseLoginLogEntity entity = new BaseLoginLogEntity();
            entity.SystemCode = systemCode;
            entity.UserId = userInfo.Id;
            entity.UserName = userInfo.NickName;
            entity.RealName = userInfo.RealName;
            entity.CompanyId = userInfo.CompanyId;
            entity.CompanyName = userInfo.CompanyName;
            entity.CompanyCode = userInfo.CompanyCode;
            entity.IPAddress = ipAddress;
            entity.IPAddressName = ipAddressName;
            entity.MACAddress = macAddress;
            entity.LoginStatus = loginStatus;
            entity.LogLevel = LoginStatusToLogLevel(loginStatus);
            entity.CreateOn = DateTime.Now;

            string tableName = GetSplitTableName(userInfo);

            using (IDbHelper dbHelper = DbHelperFactory.GetHelper(BaseSystemInfo.LoginLogDbType, BaseSystemInfo.LoginLogDbConnection))
            {
                BaseLoginLogManager loginLogManager = new BaseLoginLogManager(dbHelper, tableName);
                try
                {
                    // 2015-07-13 把登录日志无法正常写入的，进行日志记录
                    loginLogManager.Add(entity, false, false);
                }
                catch (System.Exception ex)
                {
                    FileUtil.WriteMessage("AddLogTask: ipAddress:" + ipAddress + "macAddress:" + macAddress
                        + System.Environment.NewLine + "异常信息:" + ex.Message
                        + System.Environment.NewLine + "错误源:" + ex.Source
                        + System.Environment.NewLine + "堆栈信息:" + ex.StackTrace, System.Web.HttpContext.Current.Server.MapPath("~/Log/") + "Log" + DateTime.Now.ToString(BaseSystemInfo.DateFormat) + ".txt");
                }
            }
        }

        public static void AddLog(string systemCode, BaseUserInfo userInfo, string ipAddress, string ipAddressName, string macAddress, string loginStatus)
        {
            if (BaseSystemInfo.RecordLogOnLog)
            {
                // 吉日嘎拉 抛出一个线程，现在主库的性能有问题，临时屏蔽一下
                new Thread(AddLogTaskByBaseUserInfo).Start(new Tuple<string, BaseUserInfo, string, string, string, string>(systemCode, userInfo, ipAddress, ipAddressName, macAddress, loginStatus));
            }
        }

        public static string AddLog(string systemCode, BaseUserEntity userEntity, string ipAddress, string ipAddressName, string macAddress, string loginStatus)
        {
            if (!BaseSystemInfo.RecordLogOnLog)
            {
                return string.Empty;
            }
            if (userEntity == null)
            {
                return null;
            }

            string result = string.Empty;
            BaseLoginLogEntity entity = new BaseLoginLogEntity();
            entity.SystemCode = systemCode;
            entity.UserId = userEntity.Id;
            entity.UserName = userEntity.NickName;
            entity.RealName = userEntity.RealName;
            entity.CompanyId = userEntity.CompanyId;
            entity.CompanyName = userEntity.CompanyName;
            if (BaseSystemInfo.OnInternet && !string.IsNullOrEmpty(userEntity.CompanyId))
            {
                entity.CompanyCode = BaseOrganizeManager.GetCodeByCache(userEntity.CompanyId);
            }
            entity.IPAddress = ipAddress;
            entity.IPAddressName = ipAddressName;
            entity.MACAddress = macAddress;
            entity.LoginStatus = loginStatus;
            entity.LogLevel = LoginStatusToLogLevel(loginStatus);
            entity.CreateOn = DateTime.Now;

            string tableName = GetSplitTableName(userEntity);
            
            using (IDbHelper dbHelper = DbHelperFactory.GetHelper(BaseSystemInfo.LoginLogDbType, BaseSystemInfo.LoginLogDbConnection))
            {
                BaseLoginLogManager loginLogManager = new BaseLoginLogManager(dbHelper, tableName);
                try
                {
                    // 2015-07-13 把登录日志无法正常写入的，进行日志记录
                    result = loginLogManager.Add(entity, false, false);
                }
                catch (System.Exception ex)
                {
                    FileUtil.WriteMessage("AddLogTask: 异常信息:" + ex.Message
                        + System.Environment.NewLine + "错误源:" + ex.Source
                        + System.Environment.NewLine + "堆栈信息:" + ex.StackTrace, System.Web.HttpContext.Current.Server.MapPath("~/Log/") + "Log" + DateTime.Now.ToString(BaseSystemInfo.DateFormat) + ".txt");
                }
            }

            return result;
        }

        /// <summary>
        /// 登录级别（0，正常；1、注意；2，危险；3、攻击）
        /// </summary>
        /// <param name="loginStatus"></param>
        /// <returns></returns>
        private static int LoginStatusToLogLevel(string loginStatus)
        {
            // 
            int result = 1;
            
            if (!string.IsNullOrEmpty(loginStatus))
            {
                if (loginStatus == "用户登录")
                {
                    result = 0;
                }
                else if (loginStatus == "退出系统")
                {
                    result = 0;
                }
                else if (loginStatus == "密码错误")
                {
                    result = 2;
                }
                else if (loginStatus == "用户没有找到")
                {
                    result = 2;
                }
            }
            return result;
        }

        /// <summary>
        /// 添加日志
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        /// <param name="loginStatus">动作</param>
        public static void AddLog(BaseUserInfo userInfo, string loginStatus)
        {
            string systemCode = userInfo.SystemCode;
            string ipAddress = userInfo.IPAddress;
            string ipAddressName = IpHelper.GetInstance().FindName(userInfo.IPAddress);
            string macAddress = userInfo.MACAddress;
            AddLog(systemCode, userInfo, ipAddress, ipAddressName, macAddress, loginStatus);
        }

        private static void AddLogTask(object param)
        {
            var tuple = param as Tuple<string, string, string, string, string, string, string>;
            string systemCode = tuple.Item1;
            string userId = tuple.Item2;
            string userName = tuple.Item3;
            string ipAddress = tuple.Item4;
            string ipAddressName = tuple.Item5;
            string macAddress = tuple.Item6;
            string loginStatus = tuple.Item7;

            BaseLoginLogEntity entity = new BaseLoginLogEntity();
            entity.SystemCode = systemCode;
            entity.UserId = userId;
            entity.UserName = userName;
            entity.IPAddress = ipAddress;
            entity.IPAddressName = ipAddressName;
            entity.MACAddress = macAddress;
            entity.LoginStatus = loginStatus;
            entity.LogLevel = LoginStatusToLogLevel(loginStatus);
            entity.CreateOn = DateTime.Now;

            string tableName = GetSplitTableName();
            using (IDbHelper dbHelper = DbHelperFactory.GetHelper(BaseSystemInfo.LoginLogDbType, BaseSystemInfo.LoginLogDbConnection))
            {
                BaseLoginLogManager loginLogManager = new BaseLoginLogManager(dbHelper, tableName);
                try
                {
                    // 2015-07-13 把登录日志无法正常写入的，进行日志记录
                    loginLogManager.Add(entity, false, false);
                }
                catch (System.Exception ex)
                {
                    FileUtil.WriteMessage("AddLogTask: 异常信息:" + ex.Message
                        + System.Environment.NewLine + "错误源:" + ex.Source
                        + System.Environment.NewLine + "堆栈信息:" + ex.StackTrace, System.Web.HttpContext.Current.Server.MapPath("~/Log/") + "Log" + DateTime.Now.ToString(BaseSystemInfo.DateFormat) + ".txt");
                }
            }
        }

        public static void AddLog(string systemCode, string userId, string userName, string ipAddress, string ipAddressName, string macAddress, string loginStatus)
        {
            if (BaseSystemInfo.RecordLogOnLog)
            {
                // 抛出一个线程
                new Thread(AddLogTask).Start(new Tuple<string, string, string, string, string, string, string>(systemCode, userId, userName, ipAddress, ipAddressName, macAddress, loginStatus));
            }
        }
    }
}
