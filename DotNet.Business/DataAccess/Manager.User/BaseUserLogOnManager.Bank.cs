//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2013 , Hairihan TECH, Ltd. 
//-----------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;

namespace DotNet.Business
{
    using DotNet.Utilities;

    /// <summary>
    /// BaseUserLogOnManager
    /// 用户管理
    /// 
    /// 修改纪录
    /// 
    ///		2013.04.21 版本：1.0 JiRiGaLa	主键整理。
    /// 
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2013.04.21</date>
    /// </author> 
    /// </summary>
    public partial class BaseUserLogOnManager : BaseManager
    {
        /// <summary>
        /// 用户密码加密处理功能
        /// 
        /// 用户的密码到底如何加密，数据库中如何存储用户的密码？
        /// 若是明文方式存储，在管理上会有很多漏洞，虽然调试时不方便，当时加密的密码相对是安全的，
        /// 而且最好是密码是不可逆的，这样安全性更高一些，各种不同的系统，这里适当的处理一下就饿可以了。
        /// </summary>
        /// <param name="password">用户密码</param>
        /// <returns>处理后的密码</returns>
        public virtual string EncryptUserPassword(string password)
        {
            // 这里也可以选择不进行处理，不加密
            // return password;
            return DotNet.Utilities.SecretUtil.md5(password, 32);
        }

        public virtual string CreateDigitalSignature(string password, out string statusCode)
        {
            string result = string.Empty;
            statusCode = string.Empty;
            // 1:检查是否已经生成过数字签名
            // 2:加密密码
            password = this.EncryptUserPassword(password);
            // 3:设置密码
            this.SetProperty(UserInfo.Id, new KeyValuePair<string, object>(BaseUserLogOnEntity.FieldSignedPassword, password));
            // 4:产生私钥、公钥对
            RSACryptoServiceProvider cryptoServiceProvider = new RSACryptoServiceProvider();
            string publicKey = Convert.ToBase64String(cryptoServiceProvider.ExportCspBlob(false));
            string privateKey = Convert.ToBase64String(cryptoServiceProvider.ExportCspBlob(true));
            // 5:保存公钥
            this.SetProperty(UserInfo.Id, new KeyValuePair<string, object>(BaseUserLogOnEntity.FieldPublicKey, publicKey));
            // 6:返回私钥
            result = privateKey;
            // 7:写入正确状态
            statusCode = Status.OK.ToString();
            return result;
        }

        /// <summary>
        /// 获取当前用户的公钥
        /// </summary>
        /// <returns>公钥</returns>
        public string GetPublicKey()
        {
            return this.GetProperty(this.UserInfo.Id, BaseUserLogOnEntity.FieldPublicKey);
        }

        /// <summary>
        /// 获取当前用户的公钥
        /// </summary>
        /// <param name="userId">用户主键</param>
        /// <returns>公钥</returns>
        public string GetPublicKey(string userId)
        {
            return this.GetProperty(userId, BaseUserLogOnEntity.FieldPublicKey);
        }

        /// <summary>
        /// 更新数字签名密码
        /// </summary>
        /// <param name="oldPassword">原密码</param>
        /// <param name="newPassword">新密码</param>
        /// <param name="statusCode">返回状态码</param>
        /// <returns>影响行数</returns>
        public virtual int ChangeSignedPassword(string oldPassword, string newPassword, out string statusCode)
        {
            #if (DEBUG)
                int milliStart = Environment.TickCount;
            #endif

            int result = 0;
            // 密码强度检查
            if (BaseSystemInfo.CheckPasswordStrength)
            {
                if (String.IsNullOrEmpty(newPassword))
                {
                    statusCode = Status.PasswordCanNotBeNull.ToString();
                    return result;
                }
            }
            // 加密密码
            if (BaseSystemInfo.ServerEncryptPassword)
            {
                oldPassword = this.EncryptUserPassword(oldPassword);
                newPassword = this.EncryptUserPassword(newPassword);
            }
            // 判断输入原始密码是否正确
            BaseUserLogOnEntity entity = new BaseUserLogOnEntity();
            entity.GetSingle(this.GetDataTableById(UserInfo.Id));
            if (entity.SignedPassword == null)
            {
                entity.SignedPassword = string.Empty;
            }
            // 密码错误
            if (!entity.SignedPassword.Equals(oldPassword))
            {
                statusCode = Status.OldPasswordError.ToString();
                return result;
            }
            // 更改密码
            result = this.SetProperty(UserInfo.Id, new KeyValuePair<string, object>(BaseUserLogOnEntity.FieldSignedPassword, newPassword));
            if (result == 1)
            {
                statusCode = Status.ChangePasswordOK.ToString();
            }
            else
            {
                // 数据可能被删除
                statusCode = Status.ErrorDeleted.ToString();
            }

            // 写入调试信息
            #if (DEBUG)
                BaseUserManager userManager = new BaseUserManager(DbHelper, UserInfo);
                BaseUserEntity userEntity = userManager.GetObject(this.UserInfo.Id);
                int milliEnd = Environment.TickCount;
                Trace.WriteLine(DateTime.Now.ToString(BaseSystemInfo.TimeFormat) + " Ticks: " + TimeSpan.FromMilliseconds(milliEnd - milliStart).ToString() + " " + " BaseUserManager.ChangePassword(" + userEntity.Id + ")");
            #endif

            return result;
        }

        public virtual bool SignedPassword(string signedPassword)
        {
            bool result = false;
            // 密码强度检查
            if (BaseSystemInfo.CheckPasswordStrength)
            {
                if (String.IsNullOrEmpty(signedPassword))
                {
                    return result;
                }
            }
            // 加密密码
            if (BaseSystemInfo.ServerEncryptPassword)
            {
                signedPassword = this.EncryptUserPassword(signedPassword);
            }
            // 判断输入原始密码是否正确
            BaseUserLogOnEntity entity = new BaseUserLogOnEntity();
            entity.GetSingle(this.GetDataTableById(UserInfo.Id));
            if (!(entity.CommunicationPassword == null && signedPassword.Length == 0))
            {
                if (entity.SignedPassword.Equals(signedPassword))
                {
                    result = true;
                }
            }
            return result;
        }

        /// <summary>
        /// 更新通讯密码
        /// </summary>
        /// <param name="oldPassword">原密码</param>
        /// <param name="newPassword">新密码</param>
        /// <param name="statusCode">返回状态码</param>
        /// <returns>影响行数</returns>
        public virtual int ChangeCommunicationPassword(string oldPassword, string newPassword, out string statusCode)
        {
            #if (DEBUG)
                int milliStart = Environment.TickCount;
            #endif

            int result = 0;
            // 密码强度检查
            if (BaseSystemInfo.CheckPasswordStrength)
            {
                if (String.IsNullOrEmpty(newPassword))
                {
                    statusCode = Status.PasswordCanNotBeNull.ToString();
                    return result;
                }
            }
            // 加密密码
            if (BaseSystemInfo.ServerEncryptPassword)
            {
                oldPassword = this.EncryptUserPassword(oldPassword);
                newPassword = this.EncryptUserPassword(newPassword);
            }
            // 判断输入原始密码是否正确
            BaseUserLogOnEntity entity = new BaseUserLogOnEntity();
            entity.GetSingle(this.GetDataTableById(UserInfo.Id));
            if (entity.CommunicationPassword == null)
            {
                entity.CommunicationPassword = string.Empty;
            }
            // 密码错误
            if (!entity.CommunicationPassword.Equals(oldPassword))
            {
                statusCode = Status.OldPasswordError.ToString();
                return result;
            }
            // 更改密码
            result = this.SetProperty(UserInfo.Id, new KeyValuePair<string, object>(BaseUserLogOnEntity.FieldCommunicationPassword, newPassword));
            if (result == 1)
            {
                statusCode = Status.ChangePasswordOK.ToString();
            }
            else
            {
                // 数据可能被删除
                statusCode = Status.ErrorDeleted.ToString();
            }

            // 写入调试信息
            #if (DEBUG)
                BaseUserManager userManager = new BaseUserManager(DbHelper, UserInfo);
                BaseUserEntity userEntity = userManager.GetObject(this.UserInfo.Id);
                int milliEnd = Environment.TickCount;
                Trace.WriteLine(DateTime.Now.ToString(BaseSystemInfo.TimeFormat) + " Ticks: " + TimeSpan.FromMilliseconds(milliEnd - milliStart).ToString() + " " + " BaseUserManager.ChangePassword(" + userEntity.Id + ")");
            #endif

            return result;
        }

        public virtual bool CommunicationPassword(string communicationPassword)
        {
            bool result = false;
            // 密码强度检查
            if (BaseSystemInfo.CheckPasswordStrength)
            {
                if (String.IsNullOrEmpty(communicationPassword))
                {
                    return result;
                }
            }
            // 加密密码
            if (BaseSystemInfo.ServerEncryptPassword)
            {
                communicationPassword = this.EncryptUserPassword(communicationPassword);
            }
            // 判断输入原始密码是否正确
            BaseUserLogOnEntity entity = new BaseUserLogOnEntity();
            entity.GetSingle(this.GetDataTableById(UserInfo.Id));
            if (entity.CommunicationPassword == null)
            {
                entity.CommunicationPassword = string.Empty;
            }
            if (entity.CommunicationPassword.Equals(communicationPassword))
            {
                result = true;
            }
            return result;
        }

        /// <summary>
        /// 批量设置通讯密码
        /// </summary>
        /// <param name="userIds">被设置的员工主键</param>
        /// <param name="password">新密码</param>
        /// <param name="statusCode">返回状态码</param>
        /// <returns>影响行数</returns>
        public virtual int BatchSetCommunicationPassword(string[] userIds, string password, out string statusCode)
        {
            #if (DEBUG)
                int milliStart = Environment.TickCount;
            #endif

            int result = 0;
            // 密码强度检查
            if (BaseSystemInfo.CheckPasswordStrength)
            {
                if (password.Length == 0)
                {
                    statusCode = Status.PasswordCanNotBeNull.ToString();
                    return result;
                }
            }
            // 加密密码
            if (BaseSystemInfo.ServerEncryptPassword)
            {
                password = this.EncryptUserPassword(password);
            }
            for (int i = 0; i < userIds.Length; i++)
            {
                // 设置密码字段
                result = this.SetProperty(userIds, new KeyValuePair<string, object>(BaseUserLogOnEntity.FieldCommunicationPassword, password));
            }
            if (result > 0)
            {
                statusCode = Status.SetPasswordOK.ToString();
            }
            else
            {
                // 数据可能被删除
                statusCode = Status.ErrorDeleted.ToString();
            }

            // 写入调试信息
            #if (DEBUG)
                int milliEnd = Environment.TickCount;
                Trace.WriteLine(DateTime.Now.ToString(BaseSystemInfo.TimeFormat) + " Ticks: " + TimeSpan.FromMilliseconds(milliEnd - milliStart).ToString() + " " + " BaseUserManager.SetPassword()");
            #endif

            return result;
        }
    }
}