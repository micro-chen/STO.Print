//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2014 , Hairihan TECH, Ltd. 
//-----------------------------------------------------------------

using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;

namespace DotNet.Business
{
    using DotNet.Utilities;

    /// <summary>
    /// BaseMessageManager（程序OK）
    /// 消息表
    ///
    /// 修改记录
    ///     
    ///     2014.04.16 版本：1.0 JiRiGaLa 创建主键。
    ///
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2014.04.16</date>
    /// </author>
    /// </summary>
    public partial class BaseMessageManager : BaseManager
    {   
        public DataTable GetExternalUser(string id)
        {
            DataTable result = null;
            string connectionString = ConfigurationHelper.AppSettings("WeChatDbConnection", BaseSystemInfo.EncryptDbConnection);
            if (!string.IsNullOrEmpty(connectionString))
            {
                List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
                parameters.Add(new KeyValuePair<string, object>("Id", id));
                IDbHelper dbHelper = DbHelperFactory.GetHelper(CurrentDbType.Oracle, connectionString);
                result = DbLogic.GetDataTable(dbHelper, "ExternalUser", parameters);
                result.TableName = "ExternalUser";
            }
            return result;
        }

        public string GetExternalUserName(string id)
        {
            string result = null;
            string connectionString = ConfigurationHelper.AppSettings("WeChatDbConnection", BaseSystemInfo.EncryptDbConnection);
            if (!string.IsNullOrEmpty(connectionString))
            {
                List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
                parameters.Add(new KeyValuePair<string, object>("Id", id));
                IDbHelper dbHelper = DbHelperFactory.GetHelper(CurrentDbType.Oracle, connectionString);
                result = DbLogic.GetProperty(dbHelper, "ExternalUser", parameters, "NICKNAME");
            }
            return result;
        }
    }
}