//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2015 , Hairihan TECH, Ltd. 
//-----------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace DotNet.Business
{
    using DotNet.Model;

    /// <summary>
    /// BaseUserManager
    /// 用户管理
    /// 
    /// 修改纪录
    /// 
    ///		2013.10.20 版本：2.0 JiRiGaLa	集成K8物流系统的登录功能。
    ///		2011.10.17 版本：1.0 JiRiGaLa	主键整理。
    /// 
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2011.10.17</date>
    /// </author> 
    /// </summary>
    public partial class BaseUserManager : BaseManager
    {
        public int ImportUser(System.Data.IDataReader dataReader, BaseOrganizeManager organizeManager, BaseUserLogOnManager userLogOnManager, BaseUserContactManager userContactManager)
        {
            int result = 0;
            BaseUserEntity userEntity = this.GetObject(dataReader["ID"].ToString());
            if (userEntity == null)
            {
                userEntity = new BaseUserEntity();
                userEntity.Id = dataReader["ID"].ToString();
            }
            userEntity.Id = dataReader["ID"].ToString();
            userEntity.UserFrom = "K8";
            userEntity.UserName = dataReader["USER_NAME"].ToString();
            userEntity.IDCard = dataReader["ID_Card"].ToString();
            userEntity.Code = dataReader["EMPLOYEE_CODE"].ToString();
            userEntity.RealName = dataReader["REAL_NAME"].ToString();
            if (string.IsNullOrWhiteSpace(userEntity.RealName))
            {
                userEntity.RealName = dataReader["EMPLOYEE_NAME"].ToString();
            }
            userEntity.NickName = dataReader["ONLY_USER_NAME"].ToString();
            userEntity.CompanyName = dataReader["OWNER_SITE"].ToString();
            userEntity.Description = dataReader["REMARK"].ToString();
            // 把被删除的数据恢复过来
            userEntity.DeletionStateCode = 0;
            if (string.IsNullOrEmpty(userEntity.CompanyId))
            {
                userEntity.CompanyId = organizeManager.GetProperty(new KeyValuePair<string, object>(BaseOrganizeEntity.FieldFullName, userEntity.CompanyName), BaseOrganizeEntity.FieldId);
                if (string.IsNullOrEmpty(userEntity.CompanyId))
                {
                    System.Console.WriteLine("无CompanyId " + userEntity.Id + ":" + userEntity.UserName + ":" + userEntity.RealName);
                    return 0;
                }
            }
            // 不是内部组织机构的才进行调整
            if (string.IsNullOrEmpty(userEntity.DepartmentId))
            {
                userEntity.DepartmentName = dataReader["DEPT_NAME"].ToString();
            }
            if (!string.IsNullOrEmpty(dataReader["IM_NAME"].ToString()))
            {
                // userEntity.QQ = dataReader["IM_NAME"].ToString();
            }

            userEntity.Enabled = int.Parse(dataReader["BL_LOCK_FLAG"].ToString());
            System.Console.WriteLine("ImportK8User:" + userEntity.Id + ":" + userEntity.RealName);
            // 02：可以把读取到的数据能写入到用户中心的。
            result = this.UpdateObject(userEntity);
            if (result == 0)
            {
                this.AddObject(userEntity);
            }
            // 添加用户密码表
            BaseUserLogOnEntity userLogOnEntity = userLogOnManager.GetObject(userEntity.Id);
            if (userLogOnEntity == null)
            {
                userLogOnEntity = new BaseUserLogOnEntity();
                userLogOnEntity.Id = userEntity.Id;
                // 邦定mac地址
                userLogOnEntity.CheckIPAddress = 1;
                userLogOnEntity.UserPassword = dataReader["USER_PASSWD"].ToString();
                userLogOnEntity.Salt = dataReader["SALT"].ToString();
                // 是否检查机器码 MAC地址
                int checkIPAddress = 1;
                int.TryParse(dataReader["BL_CHECK_COMPUTER"].ToString(), out checkIPAddress);
                userLogOnEntity.CheckIPAddress = checkIPAddress;
                if (!string.IsNullOrEmpty(dataReader["CHANGEPASSWORDDATE"].ToString()))
                {
                    userLogOnEntity.ChangePasswordDate = DateTime.Parse(dataReader["CHANGEPASSWORDDATE"].ToString());
                }
                userLogOnManager.AddObject(userLogOnEntity);
            }
            else
            {
                userLogOnEntity.Id = userEntity.Id;
                userLogOnEntity.UserPassword = dataReader["USER_PASSWD"].ToString();
                userLogOnEntity.Salt = dataReader["SALT"].ToString();
                if (!string.IsNullOrEmpty(dataReader["CHANGEPASSWORDDATE"].ToString()))
                {
                    userLogOnEntity.ChangePasswordDate = DateTime.Parse(dataReader["CHANGEPASSWORDDATE"].ToString());
                }
                result = userLogOnManager.UpdateObject(userLogOnEntity);
            }
            // 用户的联系方式
            BaseUserContactEntity userContactEntity = userContactManager.GetObject(userEntity.Id);
            if (userContactEntity == null)
            {
                userContactEntity = new BaseUserContactEntity();
                userContactEntity.Id = userEntity.Id;
                userContactEntity.QQ = dataReader["QQ"].ToString();
                userContactEntity.Mobile = dataReader["Mobile"].ToString();
                userContactEntity.Email = dataReader["Email"].ToString();
                userContactManager.AddObject(userContactEntity);
            }
            else
            {
                if (!string.IsNullOrEmpty(dataReader["QQ"].ToString()))
                {
                    userContactEntity.QQ = dataReader["QQ"].ToString();
                }
                if (!string.IsNullOrEmpty(dataReader["Mobile"].ToString()))
                {
                    userContactEntity.Mobile = dataReader["Mobile"].ToString();
                }
                if (!string.IsNullOrEmpty(dataReader["Email"].ToString()))
                {
                    userContactEntity.Email = dataReader["Email"].ToString();
                }
                userContactManager.UpdateObject(userContactEntity);
            }
            return result;
        }
    }
}