//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd.  
//-----------------------------------------------------------------

using System;
using System.Collections.Generic;

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
    ///		2015.12.09 版本：2.1 JiRiGaLa	增加日志记录。
    ///		2014.02.08 版本：2.0 JiRiGaLa	密码加密方式改进。
    ///		2011.10.17 版本：1.0 JiRiGaLa	主键整理。
    /// 
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2015.12.09</date>
    /// </author> 
    /// </summary>
    public partial class BaseUserManager : BaseManager
    {
        /// <summary>
        /// 用户密码加密处理功能
        /// 
        /// 用户的密码到底如何加密，数据库中如何存储用户的密码？
        /// 若是明文方式存储，在管理上会有很多漏洞，虽然调试时不方便，当时加密的密码相对是安全的，
        /// 而且最好是密码是不可逆的，这样安全性更高一些，各种不同的系统，这里适当的处理一下就饿可以了。
        /// </summary>
        /// <param name="password">用户密码</param>
        /// <param name="salt">密码盐</param>
        /// <returns>处理后的密码</returns>
        public virtual string EncryptUserPassword(string password, string salt = null)
        {
            string result = string.Empty;

            result = DotNet.Utilities.SecretUtil.md5(password, 32).ToUpper();

            if (!string.IsNullOrEmpty(salt) && (salt.Length == 20))
            {
                result = salt.Substring(6) + result + salt.Substring(6, 10);
                result = DotNet.Utilities.SecretUtil.md5(result, 32).ToUpper();
                result = result + salt;
                result = DotNet.Utilities.SecretUtil.md5(result, 32).ToUpper();
            }

            return result;
        }

        /// <summary>
        /// 设置密码
        /// </summary>
        /// <param name="userId">被设置的用户主键</param>
        /// <param name="newPassword">新密码</param>
        /// <param name="unlock">解除锁定</param>
        /// <param name="autoAdd">数据缺少自动补充登录信息</param>
        /// <returns>影响行数</returns>
        public virtual int SetPassword(string userId, string newPassword, bool? unlock = null, bool? autoAdd = null, bool modifyRecord = true)
        {
            int result = 0;

            // 密码强度检查
            /*
            if (BaseSystemInfo.CheckPasswordStrength)
            {
                if (password.Length == 0)
                {
                    this.StatusCode = StatusCode.PasswordCanNotBeNull.ToString();
                    return result;
                }
            }
            */
            string encryptPassword = newPassword;
            string salt = string.Empty;
            // 加密密码
            if (BaseSystemInfo.ServerEncryptPassword)
            {
                salt = BaseRandom.GetRandomString(20);
                encryptPassword = this.EncryptUserPassword(newPassword, salt);
            }
            // 设置密码字段
            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>(BaseUserLogOnEntity.FieldUserPassword, encryptPassword));
            // 需要重新登录才可以，防止正在被人黑中，阻止已经在线上的人
            parameters.Add(new KeyValuePair<string, object>(BaseUserLogOnEntity.FieldOpenId, Guid.NewGuid().ToString("N")));
            if (BaseSystemInfo.ServerEncryptPassword)
            {
                parameters.Add(new KeyValuePair<string, object>(BaseUserLogOnEntity.FieldSalt, salt));
            }
            parameters.Add(new KeyValuePair<string, object>(BaseUserLogOnEntity.FieldChangePasswordDate, DateTime.Now));
            if (unlock.HasValue && unlock.Value == true)
            {
                parameters.Add(new KeyValuePair<string, object>(BaseUserLogOnEntity.FieldEnabled, 1));
                parameters.Add(new KeyValuePair<string, object>(BaseUserLogOnEntity.FieldLockStartDate, null));
                parameters.Add(new KeyValuePair<string, object>(BaseUserLogOnEntity.FieldLockEndDate, null));
            }
            BaseUserLogOnManager userLogOnManager = new BaseUserLogOnManager(this.DbHelper, this.UserInfo);
            result = userLogOnManager.SetProperty(new KeyValuePair<string, object>(BaseUserLogOnEntity.FieldId, userId), parameters);
            if (result == 0 && autoAdd.HasValue && autoAdd.Value == true)
            {
                BaseUserLogOnEntity userLogOnEntity = new BaseUserLogOnEntity();
                userLogOnEntity.Id = userId;
                userLogOnEntity.ChangePasswordDate = DateTime.Now;
                userLogOnEntity.UserPassword = encryptPassword;
                userLogOnEntity.Salt = salt;
                userLogOnEntity.Enabled = 1;
                userLogOnManager.AddObject(userLogOnEntity);
                result = 1;
            }

            // 2015-12-09 吉日嘎拉 增加日志功能、谁什么时候设置了谁的密码？
            if (modifyRecord)
            {
                var record = new BaseModifyRecordEntity();
                record.TableCode = BaseUserLogOnEntity.TableName.ToUpper();
                record.TableDescription = "用户登录信息表";
                record.ColumnCode = BaseUserLogOnEntity.FieldUserPassword;
                record.ColumnDescription = "用户密码";
                record.RecordKey = userId;
                record.NewValue = "设置密码";
                // record.OldValue = "";
                if (this.UserInfo != null)
                {
                    record.IPAddress = this.UserInfo.IPAddress;
                    record.CreateUserId = this.UserInfo.Id;
                    record.CreateOn = DateTime.Now;
                }
                BaseModifyRecordManager modifyRecordManager = new Business.BaseModifyRecordManager(this.UserInfo, BaseUserEntity.TableName + "_Log");
                modifyRecordManager.Add(record, true, false);
            }

            if (result == 1)
            {
                this.StatusCode = Status.SetPasswordOK.ToString();
                // 调用扩展
                if (BaseSystemInfo.OnInternet && BaseSystemInfo.ServerEncryptPassword)
                {
                    // AfterSetPassword(userId, salt, password);
                }
            }
            else
            {
                // 数据可能被删除
                this.StatusCode = Status.ErrorDeleted.ToString();
            }

            return result;
        }

        /// <summary>
        /// 批量设置密码
        /// </summary>
        /// <param name="userIds">被设置的员工主键</param>
        /// <param name="password">新密码</param>
        /// <returns>影响行数</returns>
        public virtual int BatchSetPassword(string[] userIds, string password, bool? unlock = null, bool? autoAdd = null)
        {
            int result = 0;

            if (userIds != null)
            {
                for (int i=0; i<userIds.Length; i++)
                {
                    result += SetPassword(userIds[i], password, unlock, autoAdd);
                }     
            }

            return result;

            /*
            
            int result = 0;
            // 密码强度检查
            
            if (BaseSystemInfo.CheckPasswordStrength)
            {
                if (password.Length == 0)
                {
                    statusCode = StatusCode.PasswordCanNotBeNull.ToString();
                    return result;
                }
            }

            /*

            string encryptPassword = password;
            // 加密密码
            if (BaseSystemInfo.ServerEncryptPassword)
            {
                encryptPassword = this.EncryptUserPassword(password);
            }

            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>(BaseUserLogOnEntity.FieldUserPassword, encryptPassword));
            // 需要重新登录才可以，防止正在被人黑中，阻止已经在线上的人
            parameters.Add(new KeyValuePair<string, object>(BaseUserLogOnEntity.FieldOpenId, Guid.NewGuid().ToString("N")));
            parameters.Add(new KeyValuePair<string, object>(BaseUserLogOnEntity.FieldChangePasswordDate, DateTime.Now));
            // 设置密码字段
            result += new BaseUserLogOnManager(this.DbHelper, this.UserInfo).SetProperty(userIds, parameters);

            if (result > 0)
            {
                this.StatusCode = Status.SetPasswordOK.ToString();
                // 调用扩展
                AfterBatchSetPassword(userIds, password);
            }
            else
            {
                // 数据可能被删除
                this.StatusCode = Status.ErrorDeleted.ToString();
            }

            return result;

            */
        }
    }
}