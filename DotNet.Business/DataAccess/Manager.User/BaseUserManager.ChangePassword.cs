//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd.  
//-----------------------------------------------------------------

using System;

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
    ///		2015.12.09 版本：2.1 JiRiGaLa	修改用户密码，多一个userId参数，增加修改日志。
    ///		2015.01.19 版本：2.0 JiRiGaLa	用户修改密码的日志。
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
        /// 更新密码
        /// </summary>
        /// <param name="userId">用户主键、方便外部系统调用，若能传递参数过来</param>
        /// <param name="oldPassword">原密码</param>
        /// <param name="newPassword">新密码</param>
        /// <returns>影响行数</returns>
        public virtual BaseUserInfo ChangePassword(string userId, string oldPassword, string newPassword)
        {
            #if (DEBUG)
                int milliStart = Environment.TickCount;
            #endif

            string encryptOldPassword = oldPassword;
            string encryptNewPassword = newPassword;

            BaseUserInfo userInfo = null;
            // 密码强度检查
            if (BaseSystemInfo.CheckPasswordStrength)
            {
                if (String.IsNullOrEmpty(newPassword))
                {
                    this.StatusCode = Status.PasswordCanNotBeNull.ToString();
                    return userInfo;
                }
            }
            // 判断输入原始密码是否正确
            BaseUserLogOnEntity entity = new BaseUserLogOnManager(this.DbHelper, this.UserInfo).GetObject(UserInfo.Id);
            if (entity.UserPassword == null)
            {
                entity.UserPassword = string.Empty;
            }

            // 加密密码
            if (BaseSystemInfo.ServerEncryptPassword)
            {
                encryptOldPassword = this.EncryptUserPassword(oldPassword, entity.Salt);
            }

            // 密码错误
            if (!entity.UserPassword.Equals(encryptOldPassword, StringComparison.CurrentCultureIgnoreCase))
            {
                this.StatusCode = Status.OldPasswordError.ToString();
                return userInfo;
            }
            // 对比是否最近2次用过这个密码
            if (BaseSystemInfo.CheckPasswordStrength)
            {
                /*
                int i = 0;
                BaseParameterManager manager = new BaseParameterManager(this.DbHelper, this.UserInfo);
                var dt = manager.GetDataTableParameterCode("User", this.UserInfo.Id, "Password");
                foreach (DataRow dr in dt.Rows)
                {
                    string parameter = dr[BaseParameterEntity.FieldParameterContent].ToString();
                    if (parameter.Equals(newPassword))
                    {
                        this.StatusCode = Status.PasswordCanNotBeRepeat.ToString();
                        return userInfo;
                    }
                    i++;
                    {
                        // 判断连续2个密码就是可以了
                        if (i > 2)
                        {
                            break;
                        }
                    }
                }
                */
            }
            
            // 更改密码，同时修改密码的修改日期，这里需要兼容多数据库
            string salt = string.Empty;
            if (BaseSystemInfo.ServerEncryptPassword)
            {
                salt = BaseRandom.GetRandomString(20);
                encryptNewPassword = this.EncryptUserPassword(newPassword, salt);
            }
            SQLBuilder sqlBuilder = new SQLBuilder(this.DbHelper);
            sqlBuilder.BeginUpdate(BaseUserLogOnEntity.TableName);
            if (BaseSystemInfo.ServerEncryptPassword)
            {
                sqlBuilder.SetValue(BaseUserLogOnEntity.FieldSalt, salt);
            }
            // 宋彪：此处增加更新密码强度级别
            sqlBuilder.SetValue(BaseUserLogOnEntity.FieldPasswordStrength, SecretUtil.GetUserPassWordRate(newPassword));
            sqlBuilder.SetValue(BaseUserLogOnEntity.FieldUserPassword, encryptNewPassword);
            // 2015-08-04 吉日嘎拉 修改了密码后,把需要修改密码字段设置为 0
            sqlBuilder.SetValue(BaseUserLogOnEntity.FieldNeedModifyPassword, 0);
            sqlBuilder.SetDBNow(BaseUserLogOnEntity.FieldChangePasswordDate);
            sqlBuilder.SetWhere(BaseUserLogOnEntity.FieldId, userId);
            int result = sqlBuilder.EndUpdate();

            if (result == 1)
            {
                // 2015-12-09 吉日嘎拉 确认已经记录了修改密码日志
                // BaseLoginLogManager.AddLog(this.UserInfo, Status.ChangePassword.ToDescription()); 

                // 2015-12-09 吉日嘎拉 增加日志功能、谁什么时候设置了谁的密码？
                var record = new BaseModifyRecordEntity();
                record.TableCode = BaseUserLogOnEntity.TableName.ToUpper();
                record.TableDescription = "用户登录信息表";
                record.ColumnCode = BaseUserLogOnEntity.FieldUserPassword;
                record.ColumnDescription = "用户密码";
                record.RecordKey = userId;
                record.NewValue = "修改密码";
                // record.OldValue = "";
                if (this.UserInfo != null)
                {
                    record.IPAddress = this.UserInfo.IPAddress;
                    record.CreateUserId = this.UserInfo.Id;
                    record.CreateOn = DateTime.Now;
                }
                BaseModifyRecordManager modifyRecordManager = new Business.BaseModifyRecordManager(this.UserInfo, BaseUserEntity.TableName + "_Log");
                modifyRecordManager.Add(record, true, false);

                /*
                // 若是强类型密码检查，那就保存密码修改历史，防止最近2-3次的密码相同的功能实现。
                if (BaseSystemInfo.CheckPasswordStrength)
                {
                    BaseParameterManager parameterManager = new BaseParameterManager(this.DbHelper, this.UserInfo);
                    BaseParameterEntity parameterEntity = new BaseParameterEntity();
                    parameterEntity.CategoryCode = "User";
                    parameterEntity.ParameterId = this.UserInfo.Id;
                    parameterEntity.ParameterCode = "Password";
                    parameterEntity.ParameterContent = newPassword;
                    parameterEntity.DeletionStateCode = 0;
                    parameterEntity.Enabled = true;
                    parameterEntity.Worked = true;
                    parameterManager.AddObject(parameterEntity);
                }
                */
                
                userInfo = this.LogOnByOpenId(this.UserInfo.OpenId, this.UserInfo.SystemCode).UserInfo;
                // 同步处理其他系统的密码修改动作
                if (BaseSystemInfo.ServerEncryptPassword)
                {
                    // AfterChangePassword(this.UserInfo.Id, salt, oldPassword, newPassword);
                }
                // 修改密码成功，写入状态
                this.StatusCode = Status.ChangePasswordOK.ToString();
            }
            else
            {
                // 数据可能被删除
                this.StatusCode = Status.ErrorDeleted.ToString();
            }

            return userInfo;
        }
    }
}