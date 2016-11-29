//-----------------------------------------------------------------------
// <copyright file="TAB_USER_Manager.Auto.cs" company="Hairihan">
//     Copyright (c) 2013 , All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace DotNet.Business
{
    using DotNet.Utilities;
    using DotNet.Model;
  
    /// <summary>
    /// TAB_USER_Manager
    /// 用户
    /// 修改纪录
    /// 
    /// 2015-03-24 版本：1.0 宋亮亮 创建文件。
    /// 
    /// <author>
    ///     <name>宋亮亮</name>
    ///     <date>2015-03-24</date>
    /// </author>
    /// </summary>
    public partial class TAB_USERManager
    {
        #region 增加用户账号(TAB_USER)中的信息 public bool AddUser(UserEntity user, bool isSiteAdmin, TAB_SITE_Entity userSite)
        /// <summary>
        /// 增加用户账号
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool AddUser(TAB_USEREntity user)
        {
 
            //1、先往中天里添加账号
            BaseUserEntity userEntity = new BaseUserEntity();
            BaseUserManager userManager = new BaseUserManager();
            BaseUserLogOnManager userLogOnManager = new BaseUserLogOnManager();
            userEntity.UserFrom = "K8";
            userEntity.UserName = user.USER_NAME;
            userEntity.Code = user.EMPLOYEE_CODE;
            userEntity.RealName = user.REAL_NAME;
            userEntity.CompanyName = user.OWNER_SITE;
            var organize = new BaseOrganizeManager().GetObjectByName(user.OWNER_SITE);
            if (organize != null)
            {
                userEntity.CompanyId = organize.Id.ToString();
            }
            userEntity.Description = user.REMARK;
            userEntity.DeletionStateCode = 0;
            userEntity.DepartmentName = user.DEPT_NAME;
            userEntity.Enabled = int.Parse(user.BL_LOCK_FLAG.ToString());
            bool identity = false;
            if(!string.IsNullOrEmpty(userEntity.Id))
            {
                identity = true;
            }
            userEntity.Id = userManager.Add(userEntity, identity, true);

            //添加用户密码表
            BaseUserLogOnEntity userLogOnEntity = userLogOnManager.GetObject(userEntity.Id);
            userLogOnEntity = new BaseUserLogOnEntity();
            userLogOnEntity.Id = userEntity.Id;
            //邦定mac地址
            userLogOnEntity.CheckIPAddress = 1;
            var salt = BaseRandom.GetRandomString(20);
            userLogOnEntity.Salt = salt;
            userLogOnEntity.UserPassword = userManager.EncryptUserPassword(user.USER_PASSWD, salt);

            //是否检查机器码 MAC地址
            int checkIPAddress = 1;
            int.TryParse(user.BL_CHECK_COMPUTER.ToString(), out checkIPAddress);
            userLogOnEntity.CheckIPAddress = checkIPAddress;
            userLogOnManager.AddObject(userLogOnEntity);

            //用户的联系方式
            BaseUserContactManager userContactManager = new BaseUserContactManager();
            BaseUserContactEntity userContactEntity = new BaseUserContactEntity();
            userContactEntity.Id = userEntity.Id;
            if (!string.IsNullOrEmpty(user.MOBILE))
            {
                userContactEntity.Mobile = user.MOBILE;
            }
            userContactManager.AddObject(userContactEntity);

            //2、再往K8里加用户
            TAB_USERManager tabUserManager = new TAB_USERManager();
            TAB_USERPOPEDOMManager userMenuManager = new TAB_USERPOPEDOMManager();
            user.CREATE_SITE = Utilities.UserInfo.CompanyName;
            user.CREATE_USER = Utilities.UserInfo.RealName;
            user.CREATE_DATE = DateTime.Now;
            user.USER_NAME = user.USER_NAME.Trim();
            user.ID = decimal.Parse(userEntity.Id);
            tabUserManager.Add(user, false, true);
            var sql = string.Format(" UPDATE TAB_USER SET USER_PASSWORD=NULL,USER_PASSWD='{0}',SALT ='{1}',  CHANGEPASSWORDDATE=to_date('{2}','yyyy-mm-dd-hh24:mi:ss')  WHERE ID = '{3}'",
                                     userEntity.UserPassword, salt, DateTime.Now, user.ID);
            tabUserManager.ExecuteNonQuery(sql);

            //3、新增账号的时候默认增加新员工的权限为网点员工
            var roleMenus = GetMenusByUserCode("网点员工", "", "上海");
            foreach (var roleMenu in roleMenus)
            {
                TAB_USERPOPEDOMEntity userPOPEDOM = new TAB_USERPOPEDOMEntity();
                userPOPEDOM.BL_INSERT = roleMenu.BL_INSERT;
                userPOPEDOM.BL_UPDATE = roleMenu.BL_UPDATE;
                userPOPEDOM.BL_DELETE = roleMenu.BL_DELETE;
                userPOPEDOM.USER_NAME = user.USER_NAME;
                userPOPEDOM.OWNER_SITE = user.OWNER_SITE;
                userPOPEDOM.MENU_GUID = roleMenu.MENU_GUID;
                userMenuManager.Add(userPOPEDOM);
            }
            return true;
        }
        #endregion

        #region 新增公司员工(TAB_EMPLOYEE)  public bool AddEmployee(TAB_EMPLOYEEEntity employee)
        /// <summary>
        /// 新增公司员工
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public bool AddEmployee(TAB_EMPLOYEEEntity employee)
        {
            try
            {
                BaseSequenceManager sequenceManager = new BaseSequenceManager();
                TAB_EMPLOYEEManager employeeManager = new TAB_EMPLOYEEManager();
                // 序列产生的ID，BaseUser表中员工的信息为负数
                employee.ID = (-1) * int.Parse(sequenceManager.Increment("TAB_USER"));
                employee.UPDATETIME = DateTime.Now;
                //新增公司员工到K8库中
                employeeManager.Add(employee, false, false);

                //添加公司员工成功成功后，同步到中天的baseuser表中
                AfterUpdateEmployee(employee);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 更新K8员工后，同步到中天baseuser表
        /// <summary>
        /// 更新K8员工后，同步到中天baseuser表
        /// </summary>
        /// <param name="entity"></param>
        public void AfterUpdateEmployee(TAB_EMPLOYEEEntity entity)
        {
            int count = 0;

            BaseUserEntity userEntity = new BaseUserEntity();
            userEntity.Id = entity.ID.ToString();
            userEntity.UserFrom = "PDA";
            userEntity.UserPassword = entity.BAR_PASSWORD;
            userEntity.Code = entity.EMPLOYEE_CODE;
            userEntity.UserName = entity.EMPLOYEE_NAME;
            userEntity.RealName = entity.EMPLOYEE_NAME;
            userEntity.Description = entity.PHONE;
            userEntity.CompanyName = entity.OWNER_SITE;
            // 1、更新baseuser中的所属网点
            if (string.IsNullOrEmpty(userEntity.CompanyId))
            {
                BaseOrganizeEntity organizeEntity = BaseOrganizeManager.GetObjectByNameByCache(userEntity.CompanyName);
                if (organizeEntity != null)
                {
                    userEntity.CompanyId = organizeEntity.Id.ToString();
                }
            }
            userEntity.DepartmentName = entity.DEPT_NAME;
            userEntity.WorkgroupName = entity.GROUP_NAME;
            userEntity.HomeAddress = entity.ADDRESS;
            userEntity.IDCard = entity.ID_CARD;

            // 2、员工操作类型，按签名处理
            userEntity.Signature = entity.EMPLOYEE_TYPE;
            userEntity.SortCode = int.Parse(entity.ID.ToString());
            userEntity.ModifiedOn = entity.UPDATETIME;
            BaseUserManager userManager = new BaseUserManager(Utilities.UserInfo);
            // if (!userManager.Exists(userEntity.Id))
            count = userManager.UpdateObject(userEntity);
            if (count == 0)
            {
                userManager.AddObject(userEntity);
            }
            // 3、如果有电话，同步到baseusercontact中
            BaseUserContactEntity userContactEntity = new BaseUserContactEntity();
            userContactEntity.Id = entity.ID.ToString();
            userContactEntity.Telephone = entity.PHONE;
            BaseUserContactManager userContactManager = new DotNet.Business.BaseUserContactManager(Utilities.UserInfo);
            count = userContactManager.UpdateObject(userContactEntity);
            if (count == 0)
            {
                userContactManager.AddObject(userContactEntity);
            }

            // 4、新增员工时，同步baseuser员工的密码
            BaseUserLogOnEntity userLogOnEntity = new BaseUserLogOnEntity();
            userLogOnEntity.Id = entity.ID.ToString();
            userLogOnEntity.UserPassword = entity.BAR_PASSWORD;
            BaseUserLogOnManager userLogOnManager = new BaseUserLogOnManager(Utilities.UserInfo);
            count = userLogOnManager.UpdateObject(userLogOnEntity);
            if (count == 0)
            {
                userLogOnManager.AddObject(userLogOnEntity);
            }
            // 5、中转附加费和派件附加费，再确认
            BaseUserExpressEntity userExpressEntity = new BaseUserExpressEntity();
            userExpressEntity.ID = entity.ID;
            userExpressEntity.DISPATCH_ADD_FEE = entity.DISPATCH__ADD_FEE;
            userExpressEntity.TRANSFER_ADD_FEE = entity.TRANSFER_ADD_FEE;
            userExpressEntity.OWNER_RANGE = entity.OWNER_RANGE;
            BaseUserExpressManager userExpressManager = new BaseUserExpressManager(Utilities.UserInfo);
            count = userExpressManager.UpdateObject(userExpressEntity);
            if (count == 0)
            {
                userExpressManager.Add(userExpressEntity, false);
            }
        }

        #endregion
       
        #region 根据用户编号获取用户所有的菜单，或根据用户编号和菜单ID，获取某一用户菜单 public List<TAB_USER_POPEDOMEntity> GetMenusByUserCode(string userName, string menuID = "")
        /// <summary>
        /// 根据用户编号获取用户所有的菜单，或根据用户编号和菜单ID，获取某一用户菜单
        /// 后者根据角色ID获取角色下面所有的权限
        /// </summary>
        /// <param name="employeeCode">可以是用户编号或者角色的ID</param>
        /// <param name="menuID"></param>
        /// <returns></returns>
        public List<TAB_USERPOPEDOMEntity> GetMenusByUserCode(string userName, string menuID = "", string ownerSite = "")
        {
            TAB_USERPOPEDOMManager userMenuManager = new TAB_USERPOPEDOMManager();

            if (string.IsNullOrEmpty(ownerSite))
                return null;
            var sql = "SELECT * FROM TAB_USER_POPEDOM WHERE USER_NAME='" + DbLogic.SqlSafe(userName) + "' AND OWNER_SITE='" + ownerSite + "'"; ;
            if (!string.IsNullOrEmpty(menuID))
                sql = sql + "  AND MENU_GUID='" + DbLogic.SqlSafe(menuID) + "'";

            var resultList = userMenuManager.Fill(sql).ToList<TAB_USERPOPEDOMEntity>();
            return resultList;
        }
        #endregion
    }
}
