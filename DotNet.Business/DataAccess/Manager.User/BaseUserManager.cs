//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd.  
//-----------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

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
    ///     2011.10.17 版本：4.5 JiRiGaLa   拆分代码，按核心业务逻辑进行划分，简化代码。
    ///     2011.10.05 版本：4.4 张广梁     增加 public DataTable SearchByDepartment(string departmentId,string searchValue) ，获得部门和子部门的人员
    ///     2011.09.22 版本：4.3 张广梁     完善public DataTable GetAuthorizeDT(string permissionCode, string userId = null) 增加有效期的验证
    ///     2011.07.21 版本：4.2 zgl        修正检查IP和MAC的业务逻辑，如果没有设置IP或MAC时不执行检查
    ///     2011.07.05 版本：4.1 zgl        增加几个检查Ip的方法。
    ///     2011.07.04 版本：4.0 JiRiGaLa	用户名、密码的登录程序改进。
    ///     2011.06.29 版本：3.9 JiRiGaLa	每次登录时是否产生了一个新的OpenId。
    ///     2011.06.14 版本：3.8 JiRiGaLa	用户登录时间限制、锁定日期限制。
    ///     2011.02.12 版本：3.7 JiRiGaLa	按备注也可以查询。
    ///     2009.09.11 版本：3.6 JiRiGaLa	用户的审核状态功能改进。
    ///     2008.05.13 版本：3.6 JiRiGaLa	登录时数据获取进行了优化配置。
    ///     2008.03.18 版本：3.4 JiRiGaLa	登录、重新登录、扮演时的在线状态进行更新。
    ///     2007.10.02 版本：3.3 JiRiGaLa	登录限制改进。
    ///     2007.10.01 版本：3.2 JiRiGaLa	参数传递方式改进 IDbHelper dbHelper, BaseUserInfo userInfo。
    ///     2007.06.11 版本：3.1 JiRiGaLa	设置密码，修改密码进行大修改。
    ///     2006.12.15 版本：3.0 JiRiGaLa	程序排版重新整理一次。
    ///     2006.12.11 版本：2.2 JiRiGaLa	登录部分写入日志。
    ///     2006.12.02 版本：2.1 JiRiGaLa	登录部分的主键改进。
    ///     2006.11.23 版本：2.0 JiRiGaLa	结构优化整理。
    ///		2006.02.02 版本：1.0 JiRiGaLa	书写格式进行整理。
    ///		2005.01.23 版本：1.0 JiRiGaLa	主键整理。
    /// 
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2011.10.17</date>
    /// </author> 
    /// </summary>
    public partial class BaseUserManager : BaseManager
    {
        public bool ShowUserLogOnInfo = false;

        public BaseUserInfo ConvertToUserInfo(BaseUserEntity userEntity, BaseUserLogOnEntity userLogOnEntity = null, bool validateUserOnly = false)
        {
            BaseUserInfo userInfo = new BaseUserInfo();
            return ConvertToUserInfo(userInfo, userEntity, userLogOnEntity, validateUserOnly);
        }

        public BaseUserInfo ConvertToUserInfo(BaseUserInfo userInfo, BaseUserEntity userEntity, BaseUserLogOnEntity userLogOnEntity = null, bool validateUserOnly = false)
        {
            if (userEntity == null)
            {
                return null;
            }
            userInfo.Id = userEntity.Id;
            userInfo.IsAdministrator = userEntity.IsAdministrator;
            userInfo.Code = userEntity.Code;
            userInfo.UserName = userEntity.UserName;
            userInfo.RealName = userEntity.RealName;
            userInfo.NickName = userEntity.NickName;
            if (userLogOnEntity != null)
            {
                userInfo.OpenId = userLogOnEntity.OpenId;
            }
            userInfo.CompanyId = userEntity.CompanyId;
            userInfo.CompanyName = userEntity.CompanyName;
            userInfo.DepartmentId = userEntity.DepartmentId;
            userInfo.DepartmentName = userEntity.DepartmentName;

            BaseOrganizeEntity organizeEntity = null;
            BaseOrganizeManager organizeManager = null;

            if (!string.IsNullOrEmpty(userInfo.CompanyId))
            {
#if Redis
                try
                {
                    organizeEntity = BaseOrganizeManager.GetObjectByCache(userInfo.CompanyId);
                }
                catch (System.Exception ex)
                {
                    string writeMessage = "BaseOrganizeManager.GetObjectByCache:发生时间:" + DateTime.Now
                        + System.Environment.NewLine + "CompanyId 无法缓存获取:" + userInfo.CompanyId
                        + System.Environment.NewLine + "Message:" + ex.Message
                        + System.Environment.NewLine + "Source:" + ex.Source
                        + System.Environment.NewLine + "StackTrace:" + ex.StackTrace
                        + System.Environment.NewLine + "TargetSite:" + ex.TargetSite
                        + System.Environment.NewLine;

                    FileUtil.WriteMessage(writeMessage, BaseSystemInfo.StartupPath + "//Exception//Exception" + DateTime.Now.ToString(BaseSystemInfo.DateFormat) + ".txt");
                }
#endif

                if (organizeEntity == null)
                {
                    organizeManager = new BaseOrganizeManager();
                    organizeEntity = organizeManager.GetObject(userInfo.CompanyId);
                    // 2015-12-06 吉日嘎拉 进行记录日志功能改进
                    if (organizeEntity == null)
                    {
                        string writeMessage = "BaseOrganizeManager.GetObject:发生时间:" + DateTime.Now
                        + System.Environment.NewLine + "CompanyId 无法缓存获取:" + userInfo.CompanyId
                        + System.Environment.NewLine + "BaseUserInfo:" + userInfo.Serialize();

                        FileUtil.WriteMessage(writeMessage, BaseSystemInfo.StartupPath + "Log//Log" + DateTime.Now.ToString(BaseSystemInfo.DateFormat) + ".txt");
                    }
                }
                if (organizeEntity != null)
                {
                    userInfo.CompanyCode = organizeEntity.Code;
                }
            }
            /*
             * 部门数据需要从部门表里读取
             * 
            if (!validateUserOnly && !string.IsNullOrEmpty(userInfo.DepartmentId))
            {
                organizeEntity = BaseOrganizeManager.GetObjectByCache(userInfo.DepartmentId);
            }
            else
            {
                if (organizeManager == null)
                {
                    organizeManager = new Business.BaseOrganizeManager();
                }
                organizeEntity = organizeManager.GetObject(userInfo.DepartmentId);
            }
            if (organizeEntity != null)
            {
                userInfo.DepartmentCode = organizeEntity.Code;
            }
            */

            return userInfo;
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="userCode">用户编号</param>
        /// <returns>用户实体</returns>
        public BaseUserEntity GetObjectByCode(string userCode)
        {
            BaseUserEntity entity = null;
            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>(BaseUserEntity.FieldCode, userCode));
            parameters.Add(new KeyValuePair<string, object>(BaseUserEntity.FieldEnabled, 1));
            parameters.Add(new KeyValuePair<string, object>(BaseUserEntity.FieldDeletionStateCode, 0));
            var dt = this.GetDataTable(parameters);
            if (dt.Rows.Count > 0)
            {
                entity = BaseEntity.Create<BaseUserEntity>(dt);
            }
            return entity;
        }

        public BaseUserEntity GetObjectByCompanyIdByCode(string companyId, string userCode)
        {
            BaseUserEntity entity = null;
            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>(BaseUserEntity.FieldCode, userCode));
            parameters.Add(new KeyValuePair<string, object>(BaseUserEntity.FieldCompanyId, companyId));
            parameters.Add(new KeyValuePair<string, object>(BaseUserEntity.FieldEnabled, 1));
            parameters.Add(new KeyValuePair<string, object>(BaseUserEntity.FieldDeletionStateCode, 0));
            var dt = this.GetDataTable(parameters);
            if (dt.Rows.Count > 0)
            {
                entity = BaseEntity.Create<BaseUserEntity>(dt);
            }
            return entity;
        }

        public BaseUserEntity GetObjectByCompanyCodeByCode(string companyCode, string userCode)
        {
            BaseUserEntity result = null;
            BaseOrganizeEntity organizeEntity = BaseOrganizeManager.GetObjectByCodeByCache(companyCode);
            if (organizeEntity == null)
            {
                return result;
            }
            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>(BaseUserEntity.FieldCode, userCode));
            parameters.Add(new KeyValuePair<string, object>(BaseUserEntity.FieldCompanyId, organizeEntity.Id));
            parameters.Add(new KeyValuePair<string, object>(BaseUserEntity.FieldEnabled, 1));
            parameters.Add(new KeyValuePair<string, object>(BaseUserEntity.FieldDeletionStateCode, 0));
            var dt = this.GetDataTable(parameters);
            if (dt.Rows.Count > 0)
            {
                result = BaseEntity.Create<BaseUserEntity>(dt);
            }
            return result;
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns>用户实体</returns>
        public BaseUserEntity GetObjectByUserName(string userName)
        {
            BaseUserEntity entity = null;
            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>(BaseUserEntity.FieldUserName, userName));
            parameters.Add(new KeyValuePair<string, object>(BaseUserEntity.FieldEnabled, 1));
            parameters.Add(new KeyValuePair<string, object>(BaseUserEntity.FieldDeletionStateCode, 0));
            var dt = this.GetDataTable(parameters);
            if (dt.Rows.Count > 0)
            {
                entity = BaseEntity.Create<BaseUserEntity>(dt);
            }
            else
            {
                // 用户没有找到状态
                this.StatusCode = Status.UserNotFound.ToString();
                this.StatusMessage = this.GetStateMessage(this.StatusCode);
            }
            return entity;
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="realName">姓名</param>
        /// <returns>用户实体</returns>
        public BaseUserEntity GetObjectByRealName(string realName)
        {
            BaseUserEntity entity = null;
            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>(BaseUserEntity.FieldRealName, realName));
            parameters.Add(new KeyValuePair<string, object>(BaseUserEntity.FieldEnabled, 1));
            parameters.Add(new KeyValuePair<string, object>(BaseUserEntity.FieldDeletionStateCode, 0));
            var dt = this.GetDataTable(parameters);
            if (dt.Rows.Count > 0)
            {
                entity = BaseEntity.Create<BaseUserEntity>(dt);
            }
            return entity;
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="nickName">昵称</param>
        /// <returns>用户实体</returns>
        public BaseUserEntity GetObjectByNickName(string nickName)
        {
            BaseUserEntity entity = null;
            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>(BaseUserEntity.FieldNickName, nickName));
            parameters.Add(new KeyValuePair<string, object>(BaseUserEntity.FieldEnabled, 1));
            parameters.Add(new KeyValuePair<string, object>(BaseUserEntity.FieldDeletionStateCode, 0));
            var dt = this.GetDataTable(parameters);
            if (dt.Rows.Count > 0)
            {
                entity = BaseEntity.Create<BaseUserEntity>(dt);
            }
            else
            {
                // 用户没有找到状态
                this.StatusCode = Status.UserNotFound.ToString();
                this.StatusMessage = this.GetStateMessage(this.StatusCode);
            }
            return entity;
        }

        public BaseUserEntity GetObjectByOpenId(string openId)
        {
            BaseUserEntity userEntity = null;

            // 用户没有找到状态
            this.StatusCode = Status.UserNotFound.ToString();
            this.StatusMessage = this.GetStateMessage(this.StatusCode);
            // 检查是否有效的合法的参数
            if (!String.IsNullOrEmpty(openId))
            {
                BaseUserLogOnManager userLogOnManager = new BaseUserLogOnManager();
                List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
                parameters.Add(new KeyValuePair<string, object>(BaseUserLogOnEntity.FieldOpenId, openId));
                string id = userLogOnManager.GetId(parameters);
                if (!string.IsNullOrEmpty(id))
                {
                    parameters = new List<KeyValuePair<string, object>>();
                    parameters.Add(new KeyValuePair<string, object>(BaseUserEntity.FieldId, id));
                    parameters.Add(new KeyValuePair<string, object>(BaseUserEntity.FieldDeletionStateCode, 0));
                    parameters.Add(new KeyValuePair<string, object>(BaseUserEntity.FieldEnabled, 1));
                    var dt = this.GetDataTable(parameters);
                    if (dt.Rows.Count == 1)
                    {
                        userEntity = BaseEntity.Create<BaseUserEntity>(dt);
                    }
                }
            }

            return userEntity;
        }

        public BaseUserEntity GetObjectByEmail(string email)
        {
            BaseUserEntity userEntity = null;

            // 用户没有找到状态
            this.StatusCode = Status.UserNotFound.ToString();
            this.StatusMessage = this.GetStateMessage(this.StatusCode);
            // 检查是否有效的合法的参数
            if (!String.IsNullOrEmpty(email) && ValidateUtil.IsEmail(email))
            {
                BaseUserContactManager userContactManager = new BaseUserContactManager();
                List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
                parameters.Add(new KeyValuePair<string, object>(BaseUserContactEntity.FieldEmail, email));
                string id = userContactManager.GetId(parameters);
                if (!string.IsNullOrEmpty(id))
                {
                    parameters = new List<KeyValuePair<string, object>>();
                    parameters.Add(new KeyValuePair<string, object>(BaseUserEntity.FieldId, id));
                    parameters.Add(new KeyValuePair<string, object>(BaseUserEntity.FieldDeletionStateCode, 0));
                    parameters.Add(new KeyValuePair<string, object>(BaseUserEntity.FieldEnabled, 1));
                    var dt = this.GetDataTable(parameters);
                    if (dt.Rows.Count == 1)
                    {
                        userEntity = BaseEntity.Create<BaseUserEntity>(dt);
                    }
                }
            }

            return userEntity;
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="userCode">用户编号</param>
        /// <returns>主键</returns>
        public override string GetIdByCode(string userCode)
        {
            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>(BaseUserEntity.FieldCode, userCode));
            parameters.Add(new KeyValuePair<string, object>(BaseUserEntity.FieldEnabled, 1));
            parameters.Add(new KeyValuePair<string, object>(BaseUserEntity.FieldDeletionStateCode, 0));
            return this.GetId(parameters);
        }

        /// <summary>
        /// 按用户名获取用户主键
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns>主键</returns>
        public string GetIdByUserName(string userName)
        {
            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>(BaseUserEntity.FieldUserName, userName));
            parameters.Add(new KeyValuePair<string, object>(BaseUserEntity.FieldEnabled, 1));
            parameters.Add(new KeyValuePair<string, object>(BaseUserEntity.FieldDeletionStateCode, 0));
            return this.GetId(parameters);
        }

        #region public static string GetRealNameByCache(string id) 通过主键获取姓名
        /// <summary>
        /// 通过主键获取姓名
        /// 这里是进行了内存缓存处理，减少数据库的I/O处理，提高程序的运行性能，
        /// 若有数据修改过，重新启动一下程序就可以了，这些基础数据也不是天天修改来修改去的，
        /// 所以没必要过度担忧，当然有需要时也可以写个刷新缓存的程序
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>显示值</returns>
        public static string GetRealNameByCache(string id)
        {
            string result = string.Empty;

            BaseUserEntity entity = GetObjectByCache(id);
            if (entity != null)
            {
                result = entity.RealName;
            }

            return result;
        }
        #endregion

        #region public static string GetUserCodeByCache(string id) 通过主键获取编号
        /// <summary>
        /// 通过主键获取编号
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>显示值</returns>
        public static string GetUserCodeByCache(string id)
        {
            string result = string.Empty;

            BaseUserEntity entity = GetObjectByCache(id);
            if (entity != null)
            {
                result = entity.Code;
            }

            return result;
        }
        #endregion

        #region public static string GetDepartmentNameByCache(string id) 通过主键获取部门名称
        /// <summary>
        /// 通过主键获取部门名称
        /// 这里是进行了内存缓存处理，减少数据库的I/O处理，提高程序的运行性能，
        /// 若有数据修改过，重新启动一下程序就可以了，这些基础数据也不是天天修改来修改去的，
        /// 所以没必要过度担忧，当然有需要时也可以写个刷新缓存的程序
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>显示值</returns>
        public static string GetDepartmentNameByCache(string id)
        {
            string result = string.Empty;

            BaseUserEntity entity = GetObjectByCache(id);
            if (entity != null)
            {
                result = entity.DepartmentName;
            }

            return result;
        }
        #endregion

        #region public static string GetCompanyIdByCache(string id)
        /// <summary>
        /// 通过主键获取公司主键
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>显示值</returns>
        public static string GetCompanyIdByCache(string id)
        {
            string result = string.Empty;

            BaseUserEntity entity = GetObjectByCache(id);
            if (entity != null)
            {
                result = entity.CompanyId;
            }

            return result;
        }
        #endregion

        public bool IsAdministrator(BaseUserEntity entity)
        {
            // 用户是超级管理员
            if (entity.Id.Equals("Administrator"))
            {
                return true;
            }
            if (!string.IsNullOrEmpty(entity.Code) && entity.Code.Equals("Administrator"))
            {
                return true;
            }
            if (!string.IsNullOrEmpty(entity.UserName) && entity.UserName.Equals("Administrator"))
            {
                return true;
            }
            if (!string.IsNullOrEmpty(entity.NickName) && entity.NickName.Equals("Administrator"))
            {
                return true;
            }
            if (!string.IsNullOrEmpty(entity.RealName) && entity.RealName.Equals("Administrator"))
            {
                return true;
            }

            // 不能获取当前操作元信息时，认为不是管理员
            if (entity != null)
            {
                // 用效率更高的方式进行判断，减少数据的输入输出
                if (IsAdministrator(entity.Id))
                {
                    return true;
                }
                /*
                // 用户在超级管理员群里
                List<BaseRoleEntity> roleList = GetRoleList(entity.Id);
                foreach (var roleEntity in roleList)
                {
                    if (roleEntity.Id.ToString().Equals(DefaultRole.Administrators.ToString()))
                    {
                        return true;
                    }
                    if (!string.IsNullOrEmpty(roleEntity.Code) && roleEntity.Code.Equals(DefaultRole.Administrators.ToString()))
                    {
                        return true;
                    }
                    if (!string.IsNullOrEmpty(roleEntity.RealName) && roleEntity.RealName.Equals(DefaultRole.Administrators.ToString()))
                    {
                        return true;
                    }
                }
                */
            }
            return false;
        }

        public bool IsAdministratorById(string userId)
        {
            BaseUserEntity entity = this.GetObject(userId);
            return IsAdministrator(entity);
        }

        public string GetUsersName(string userIdList)
        {
            string userRealNames = string.Empty;
            string[] ids = userIdList.Split(',').Distinct<string>().Where(t => !string.IsNullOrEmpty(t)).ToArray();
            BaseUserEntity entity = null;
            foreach (string id in ids)
            {
                entity = this.GetObject(id);
                if (entity != null && !string.IsNullOrEmpty(entity.RealName))
                {
                    userRealNames += "," + entity.RealName;
                }
            }
            if (!string.IsNullOrEmpty(userRealNames))
            {
                userRealNames = userRealNames.Substring(1);
            }
            return userRealNames;
        }

        /// <summary>
        /// 按上级主管获取下属用户列表
        /// </summary>
        /// <param name="managerId">主管主键</param>
        /// <returns>用户列表</returns>
        public List<BaseUserEntity> GetListByManager(string managerId)
        {
            DataTable dt = GetChildrens(BaseUserEntity.FieldId, managerId, BaseUserEntity.FieldManagerId, BaseUserEntity.FieldSortCode);
            return BaseUserEntity.GetList<BaseUserEntity>(dt);
        }

        /// <summary>
        /// 按上级主管获取下属用户主键数组
        /// </summary>
        /// <param name="managerId">主管主键</param>
        /// <returns>用户主键数组</returns>
        public string[] GetIdsByManager(string managerId)
        {
            return GetChildrensId(BaseUserEntity.FieldId, managerId, BaseUserEntity.FieldManagerId);
        }

        #region public BaseUserInfo AccountActivation(string openId)
        /// <summary>
        /// 激活帐户
        /// </summary>
        /// <param name="openId">唯一识别码</param>
        /// <returns>用户实体</returns>
        public BaseUserInfo AccountActivation(string openId)
        {
            // 1.用户是否存在？
            BaseUserInfo userInfo = null;
            // 用户没有找到状态
            this.StatusCode = Status.UserNotFound.ToString();
            // 检查是否有效的合法的参数
            if (!String.IsNullOrEmpty(openId))
            {
                BaseUserManager manager = new BaseUserManager(DbHelper);
                List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
                // parameters.Add(new KeyValuePair<string, object>(BaseUserEntity.FieldOpenId, openId));
                parameters.Add(new KeyValuePair<string, object>(BaseUserEntity.FieldDeletionStateCode, 0));
                var dt = manager.GetDataTable(parameters);
                if (dt.Rows.Count == 1)
                {
                    BaseUserEntity entity = BaseEntity.Create<BaseUserEntity>(dt);
                    // 3.用户是否被锁定？
                    if (entity.Enabled == 0)
                    {
                        this.StatusCode = Status.UserLocked.ToString();
                        return userInfo;
                    }
                    if (entity.Enabled == 1)
                    {
                        // 2.用户是否已经被激活？
                        this.StatusCode = Status.UserIsActivate.ToString();
                        return userInfo;
                    }
                    if (entity.Enabled == -1)
                    {
                        // 4.成功激活用户
                        this.StatusCode = Status.OK.ToString();
                        manager.SetProperty(new KeyValuePair<string, object>(BaseUserEntity.FieldId, entity.Id), new KeyValuePair<string, object>(BaseUserEntity.FieldEnabled, 1));
                        return userInfo;
                    }
                }
            }
            return userInfo;
        }
        #endregion

        #region public BaseUserInfo Impersonation(string id) 扮演用户
        /// <summary>
        /// 扮演用户
        /// </summary>
        /// <param name="id">用户主键</param>
        /// <returns>用户类</returns>
        public BaseUserInfo Impersonation(string id, out string statusCode)
        {
            BaseUserInfo userInfo = null;
            // 获得登录信息
            BaseUserLogOnEntity entity = new BaseUserLogOnManager(this.DbHelper, this.UserInfo).GetObject(id);
            // 只允许登录一次，需要检查是否自己重新登录了，或者自己扮演自己了
            if (!UserInfo.Id.Equals(id))
            {
                if (BaseSystemInfo.CheckOnLine)
                {
                    if (entity.UserOnLine > 0)
                    {
                        statusCode = Status.ErrorOnLine.ToString();
                        return userInfo;
                    }
                }
            }

            BaseUserEntity userEntity = this.GetObject(id);
            userInfo = this.ConvertToUserInfo(userEntity);
            if (userEntity.IsStaff.Equals("1"))
            {
                // 获得员工的信息
                BaseStaffEntity staffEntity = new BaseStaffEntity();
                BaseStaffManager staffManager = new BaseStaffManager(DbHelper, UserInfo);
                DataTable dataTableStaff = staffManager.GetDataTableById(id);
                staffEntity.GetSingle(dataTableStaff);
                userInfo = staffManager.ConvertToUserInfo(userInfo, staffEntity);
            }
            statusCode = Status.OK.ToString();
            // 登录、重新登录、扮演时的在线状态进行更新
            BaseUserLogOnManager userLogOnManager = new BaseUserLogOnManager(this.DbHelper, this.UserInfo);
            userLogOnManager.ChangeOnLine(id);
            return userInfo;
        }
        #endregion

        #region private int ResetData() 重置数据
        /// <summary>
        /// 重置数据
        /// </summary>
        /// <returns>影响行数</returns>
        private int ResetData()
        {
            // 删除不存在的数据，进行数据同步
            int result = 0;
            string sqlQuery = " DELETE FROM " + BaseUserEntity.TableName
                            + " WHERE Id NOT IN (SELECT Id FROM " + BaseStaffEntity.TableName + ") ";
            result += DbHelper.ExecuteNonQuery(sqlQuery);
            // 更新排序顺序情况
            sqlQuery = " UPDATE " + BaseUserEntity.TableName
                     + " SET SortCode = " + BaseStaffEntity.TableName + "." + BaseStaffEntity.FieldSortCode
                     + " FROM " + BaseStaffEntity.TableName
                     + " WHERE " + BaseUserEntity.TableName + "." + BaseUserEntity.FieldId + " = " + BaseStaffEntity.TableName + "." + BaseStaffEntity.FieldId;
            result += DbHelper.ExecuteNonQuery(sqlQuery);
            return result;
        }
        #endregion

        #region public int Reset() 重新设置数据
        /// <summary>
        /// 重新设置数据
        /// </summary>
        /// <returns>影响行数</returns>
        public int Reset()
        {
            int result = 0;
            result += this.ResetData();
            BaseUserLogOnManager manager = new BaseUserLogOnManager(this.DbHelper, this.UserInfo);
            result += manager.ResetVisitInfo();
            return result;
        }
        #endregion

        #region public int CheckUserStaff()
        /// <summary>
        /// 用户已经被删除的员工的UserId设置为Null，说白了，是需要整理数据
        /// </summary>
        /// <returns>影响行数</returns>
        public int CheckUserStaff()
        {
            string sqlQuery = " UPDATE BaseStaff SET UserId = null WHERE UserId NOT IN ( SELECT Id FROM " + BaseUserEntity.TableName + " WHERE DeletionStateCode = 0 ) ";
            return DbHelper.ExecuteNonQuery(sqlQuery);
        }
        #endregion

        /// <summary>
        /// 获取人数
        /// </summary>
        public string GetCount()
        {
            string sqlQuery = "SELECT COUNT(1) AS UserCount "
                            + "   FROM " + this.CurrentTableName
                            + "  WHERE DeletionStateCode = 0";
            return DbHelper.ExecuteScalar(sqlQuery).ToString();
        }

        public string GetRegistrationCount(int days)
        {
            string sqlQuery = "SELECT COUNT(Id) AS UserCount "
                            + "   FROM " + this.CurrentTableName
                            + "  WHERE Enabled = 1 AND (DATEADD(d, " + days.ToString() + ", " + BaseUserEntity.FieldCreateOn + ") > " + DbHelper.GetDbNow() + ")";
            return DbHelper.ExecuteScalar(sqlQuery).ToString();
        }

        #region public override int BatchSave(DataTable result) 批量保存
        /// <summary>
        /// 批量保存
        /// </summary>
        /// <param name="result">数据表</param>
        /// <returns>影响行数</returns>
        public override int BatchSave(DataTable dt)
        {
            int result = 0;
            BaseUserEntity userEntity = new BaseUserEntity();
            foreach (DataRow dr in dt.Rows)
            {
                // 删除状态
                if (dr.RowState == DataRowState.Deleted)
                {
                    string id = dr[BaseUserEntity.FieldId, DataRowVersion.Original].ToString();
                    if (id.Length > 0)
                    {
                        result += this.Delete(id);
                    }
                }
                // 被修改过
                if (dr.RowState == DataRowState.Modified)
                {
                    string id = dr[BaseUserEntity.FieldId, DataRowVersion.Original].ToString();
                    if (!String.IsNullOrEmpty(id))
                    {
                        userEntity.GetFrom(dr);
                        result += this.UpdateObject(userEntity);
                    }
                }
                // 添加状态
                if (dr.RowState == DataRowState.Added)
                {
                    userEntity.GetFrom(dr);
                    result += this.AddObject(userEntity).Length > 0 ? 1 : 0;
                }
                if (dr.RowState == DataRowState.Unchanged)
                {
                    continue;
                }
                if (dr.RowState == DataRowState.Detached)
                {
                    continue;
                }
            }
            return result;
        }
        #endregion

        /// <summary>
        /// 取得排名
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public int GetSortNum(int userId)
        {
            var entity = this.GetObject(userId);
            string sqlQuery = "SELECT COUNT(1) AS UserCount "
                            + "   FROM " + this.CurrentTableName
                            + " inner JOIN " + BaseStaffEntity.TableName + " ON " + BaseStaffEntity.TableName + ".Id = " + this.CurrentTableName + ".Id"
                            + "  WHERE " + this.CurrentTableName + ".DeletionStateCode = 0 AND " + this.CurrentTableName + ".Enabled = 1 and " + this.CurrentTableName + "." + BaseUserEntity.FieldGender + " is not null and " + BaseStaffEntity.TableName + "." + BaseStaffEntity.FieldCurrentProvince + " is not null AND (" + this.CurrentTableName + "." + BaseUserEntity.FieldScore
                            + " > " + entity.Score + " OR (" + this.CurrentTableName + "."
                            + BaseUserEntity.FieldSortCode + " < " + entity.SortCode + " AND " + this.CurrentTableName + "." + BaseUserEntity.FieldScore
                            + " = " + entity.Score + "))";
            object result = DbHelper.ExecuteScalar(sqlQuery) ?? 0;
            return BaseBusinessLogic.ConvertToInt(result) + 1;
        }

        public int GetPinYin()
        {
            int result = 0;
            var list = this.GetList<BaseUserEntity>();
            foreach (var entity in list)
            {
                if (string.IsNullOrEmpty(entity.QuickQuery))
                {
                    // 2015-12-11 吉日嘎拉 全部小写，提高Oracle的效率
                    entity.QuickQuery = StringUtil.GetPinyin(entity.RealName).ToLower();
                }
                if (string.IsNullOrEmpty(entity.SimpleSpelling))
                {
                    // 2015-12-11 吉日嘎拉 全部小写，提高Oracle的效率
                    entity.SimpleSpelling = StringUtil.GetSimpleSpelling(entity.RealName).ToLower();
                }
                result += this.UpdateObject(entity);
            }
            return result;
        }

        public static string GetNames(List<BaseUserEntity> list)
        {
            string result = string.Empty;

            foreach (BaseUserEntity entity in list)
            {
                result += "," + entity.RealName;
            }
            if (!string.IsNullOrEmpty(result))
            {
                result = result.Substring(1);
            }

            return result;
        }

        /// <summary>
        /// 重新设置缓存（重新强制设置缓存）可以提供外部调用的
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>用户信息</returns>
        public static BaseUserEntity SetCache(string id)
        {
            BaseUserEntity result = null;

            BaseUserManager manager = new BaseUserManager();
            result = manager.GetObject(id);

            if (result != null)
            {
                SetCache(result);
            }

            return result;
        }

        /// <summary>
        /// 缓存预热,强制重新缓存
        /// </summary>
        /// <returns>影响行数</returns>
        public static int CachePreheating()
        {
            int result = 0;

            // 把所有的数据都缓存起来的代码
            BaseUserManager manager = new BaseUserManager();
            using (IDataReader dataReader = manager.ExecuteReader(0, BaseUserEntity.FieldId))
            {
                while (dataReader.Read())
                {
                    BaseUserEntity entity = BaseEntity.Create<BaseUserEntity>(dataReader, false);
                    BaseUserManager.SetCache(entity);
                    result++;
                }
                dataReader.Close();
            }

            return result;
        }
    }
}