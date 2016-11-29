//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2016 , Hairihan TECH, Ltd.  
//-----------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

namespace DotNet.Business
{
    using DotNet.Model;
    using DotNet.Utilities;

    /// <summary>
    /// BaseUserPermissionManager
    /// 用户权限
    /// 
    /// 修改记录
    ///
    ///     2016.02.25 版本：2.2 JiRiGaLa 通过缓存获取权限的方法改进。
    ///     2016.01.06 版本：2.1 JiRiGaLa 分 systemCode 进行代码整顿。
    ///     2015.07.03 版本：2.0 JiRiGaLa 每个公司有查看每个公司自己授权情况的权限。
    ///     2010.04.23 版本：1.2 JiRiGaLa Enabled 不允许为NULL的错误解决。
    ///     2008.10.23 版本：1.1 JiRiGaLa 修改为用户权限。
    ///     2008.05.23 版本：1.0 JiRiGaLa 创建主键。
    ///
    /// <author>
    ///		<name>JiRiGaLa</name>
    ///		<date>2016.02.25</date>
    /// </author>
    /// </summary>
    public class BaseUserPermissionManager : BaseManager, IBaseManager
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public BaseUserPermissionManager()
        {
            base.CurrentTableName = BasePermissionEntity.TableName;
        }

        /// <summary>
        /// 构造函数
        /// <param name="tableName">指定表名</param>
        /// </summary>
        public BaseUserPermissionManager(string tableName)
        {
            base.CurrentTableName = tableName;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbHelper">数据库连接</param>
        public BaseUserPermissionManager(IDbHelper dbHelper)
            : this()
        {
            DbHelper = dbHelper;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        public BaseUserPermissionManager(BaseUserInfo userInfo)
            : this()
        {
            UserInfo = userInfo;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbHelper">数据库连接</param>
        /// <param name="userInfo">用户信息</param>
        public BaseUserPermissionManager(IDbHelper dbHelper, BaseUserInfo userInfo)
            : this(dbHelper)
        {
            UserInfo = userInfo;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbHelper">数据库连接</param>
        /// <param name="userInfo">用户信息</param>
        /// <param name="tableName">指定表名</param>
        public BaseUserPermissionManager(IDbHelper dbHelper, BaseUserInfo userInfo, string tableName)
            : this(dbHelper, userInfo)
        {
            base.CurrentTableName = tableName;
        }

        /// <summary>
        /// 判断用户是否有有相应的权限
        /// </summary>
        /// <param name="systemCode">系统编号</param>
        /// <param name="userId">用户主键</param>
        /// <param name="permissionCode">权限编号</param>
        /// <returns>有权限</returns>
        public bool CheckPermission(string systemCode, string userId, string permissionCode)
        {
            if (String.IsNullOrEmpty(systemCode))
            {
                return false;
            }

            if (String.IsNullOrEmpty(userId))
            {
                return false;
            }

            string permissionId = BaseModuleManager.GetIdByCodeByCache(systemCode, permissionCode);
            // 没有找到相应的权限
            if (String.IsNullOrEmpty(permissionId))
            {
                return false;
            }

            this.CurrentTableName = systemCode + "Permission";
            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>(BasePermissionEntity.FieldResourceCategory, BaseUserEntity.TableName));
            parameters.Add(new KeyValuePair<string, object>(BasePermissionEntity.FieldResourceId, userId));
            parameters.Add(new KeyValuePair<string, object>(BasePermissionEntity.FieldEnabled, 1));
            parameters.Add(new KeyValuePair<string, object>(BasePermissionEntity.FieldDeletionStateCode, 0));
            //宋彪注：permisssionId先没加上
            parameters.Add(new KeyValuePair<string, object>(BasePermissionEntity.FieldPermissionId, permissionId));
            return this.Exists(parameters);
        }

        public static string[] ResetPermissionByCache(string systemCode, string userId)
        {
            string key = "Permission:" + systemCode + ":User:" + userId;
            using (var redisClient = PooledRedisHelper.GetPermissionClient())
            {
                redisClient.Remove(key);
            }
            return GetPermissionIdsByCache(systemCode, userId);
        }

        public static string[] GetPermissionIdsByCache(string systemCode, string userId)
        {
            string[] result = null;

            string key = string.Empty;
            key = "Permission:" + systemCode + ":User:" + userId;

            List<string> items = null;
            using (var readOnlyRedisClient = PooledRedisHelper.GetPermissionReadOnlyClient())
            {
                HashSet<string> setItems = readOnlyRedisClient.GetAllItemsFromSet(key);
                if (setItems.Count == 0)
                {
                    BaseUserPermissionManager userPermissionManager = new Business.BaseUserPermissionManager();
                    result = userPermissionManager.GetPermissionIds(systemCode, userId);
                    if (result != null)
                    {
                        if (result.Length > 0)
                        {
                            items = new List<string>(result);
                        }
                        else
                        {
                            // 2016-02-26 吉日嘎拉 这里是为了防止重复读取，数据库被拖、效率低
                            items = new List<string>(new string[] { string.Empty });
                        }
#if ReadOnlyRedis
                        using (var redisClient = PooledRedisHelper.GetPermissionClient())
                        {
                            redisClient.AddRangeToSet(key, items);
                            redisClient.ExpireEntryAt(key, DateTime.Now.AddDays(7));
                        }
#else
                        readOnlyRedisClient.AddRangeToSet(key, items);
                        readOnlyRedisClient.ExpireEntryAt(key, DateTime.Now.AddDays(7));
#endif
                    }
                }
                else
                {
                    result = setItems.ToArray();
                }
            }

            return result;
        }

        #region public string[] GetPermissionIds(string systemCode, string userId) 获取用户的权限主键数组
        /// <summary>
        /// 获取用户的权限主键数组
        /// </summary>
        /// <param name="userId">用户主键</param>
        /// <returns>主键数组</returns>
        public string[] GetPermissionIds(string systemCode, string userId)
        {
            this.CurrentTableName = systemCode + "Permission";
            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>(BasePermissionEntity.FieldResourceCategory, BaseUserEntity.TableName));
            parameters.Add(new KeyValuePair<string, object>(BasePermissionEntity.FieldResourceId, userId));
            parameters.Add(new KeyValuePair<string, object>(BasePermissionEntity.FieldEnabled, 1));
            parameters.Add(new KeyValuePair<string, object>(BasePermissionEntity.FieldDeletionStateCode, 0));

            return this.GetProperties(parameters, BasePermissionEntity.FieldPermissionId);
        }
        #endregion

        #region public string[] GetUserIds(string systemCode, string permissionId) 获取用户主键数组
        /// <summary>
        /// 获取用户主键数组
        /// </summary>
        /// <param name="permissionId">操作权限主键</param>
        /// <returns>主键数组</returns>
        public string[] GetUserIds(string systemCode, string permissionId)
        {
            this.CurrentTableName = systemCode + "Permission";
            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>(BasePermissionEntity.FieldPermissionId, permissionId));
            parameters.Add(new KeyValuePair<string, object>(BasePermissionEntity.FieldResourceCategory, BaseUserEntity.TableName));
            parameters.Add(new KeyValuePair<string, object>(BasePermissionEntity.FieldEnabled, 1));
            parameters.Add(new KeyValuePair<string, object>(BasePermissionEntity.FieldDeletionStateCode, 0));

            return this.GetProperties(parameters, BasePermissionEntity.FieldResourceId);
        }
        #endregion

        //
        // 授予权限的实现部分
        //

        #region public string Grant(string systemCode, string userId, string permissionId, bool chekExists = true) 为了提高授权的运行速度
        /// <summary>
        /// 为了提高授权的运行速度
        /// </summary>
        /// <param name="systemCode">系统编号</param>
        /// <param name="userId">用户主键</param>
        /// <param name="result">权限主键</param>
        /// <param name="chekExists">判断是否存在</param>
        /// <returns>主键</returns>
        public string Grant(string systemCode, string userId, string permissionId, bool chekExists = true)
        {
            string result = string.Empty;

            if (String.IsNullOrEmpty(userId) && String.IsNullOrEmpty(permissionId))
            {
                return result;
            }

            this.CurrentTableName = systemCode + "Permission";

            string currentId = string.Empty;
            // 判断是否已经存在这个权限，若已经存在就不重复增加了
            if (chekExists)
            {
                List<KeyValuePair<string, object>> whereParameters = new List<KeyValuePair<string, object>>();
                whereParameters.Add(new KeyValuePair<string, object>(BasePermissionEntity.FieldResourceCategory, BaseUserEntity.TableName));
                whereParameters.Add(new KeyValuePair<string, object>(BasePermissionEntity.FieldResourceId, userId));
                whereParameters.Add(new KeyValuePair<string, object>(BasePermissionEntity.FieldPermissionId, permissionId));
                currentId = this.GetId(whereParameters);
                if (!string.IsNullOrEmpty(currentId))
                {
                    List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
                    parameters.Add(new KeyValuePair<string, object>(BasePermissionEntity.FieldEnabled, 1));
                    parameters.Add(new KeyValuePair<string, object>(BasePermissionEntity.FieldDeletionStateCode, 0));
                    parameters.Add(new KeyValuePair<string, object>(BasePermissionEntity.FieldModifiedUserId, this.UserInfo.Id));
                    parameters.Add(new KeyValuePair<string, object>(BasePermissionEntity.FieldModifiedBy, this.UserInfo.RealName));
                    parameters.Add(new KeyValuePair<string, object>(BasePermissionEntity.FieldModifiedOn, DateTime.Now));
                    // 更新状态，设置为有效、并取消删除标志，权限也不是天天变动的，所以可以更新一下
                    this.SetProperty(currentId, parameters);

                    result = currentId;
                }
            }

            if (string.IsNullOrEmpty(currentId))
            {
                BasePermissionEntity permissionEntity = new BasePermissionEntity();
                permissionEntity.ResourceCategory = BaseUserEntity.TableName;
                permissionEntity.ResourceId = userId;
                permissionEntity.PermissionId = permissionId;
                permissionEntity.Enabled = 1;
                // 2015-07-03 吉日嘎拉 若是没有公司相关的信息，就把公司区分出来，每个公司可以看每个公司的数据
                if (string.IsNullOrEmpty(permissionEntity.CompanyId))
                {
                    BaseUserEntity entity = BaseUserManager.GetObjectByCache(userId);
                    if (entity != null)
                    {
                        permissionEntity.CompanyId = entity.CompanyId;
                        permissionEntity.CompanyName = entity.CompanyName;
                    }
                }

                BasePermissionManager permissionManager = new BasePermissionManager(DbHelper, UserInfo, this.CurrentTableName);
                result = permissionManager.Add(permissionEntity);
            }

            // 2015-09-21 吉日嘎拉 这里增加变更日志
            string tableName = systemCode + ".Permission.User";
            SQLBuilder sqlBuilder = new SQLBuilder(this.DbHelper);
            sqlBuilder.BeginInsert(BaseModifyRecordEntity.TableName);
            sqlBuilder.SetValue(BaseModifyRecordEntity.FieldTableCode, tableName);
            if (this.DbHelper.CurrentDbType == CurrentDbType.Oracle)
            {
                sqlBuilder.SetFormula(BaseModifyRecordEntity.FieldId, "SEQ_" + BaseModifyRecordEntity.TableName + ".NEXTVAL");
            }
            sqlBuilder.SetValue(BaseModifyRecordEntity.FieldRecordKey, userId);
            sqlBuilder.SetValue(BaseModifyRecordEntity.FieldColumnCode, "授权");
            sqlBuilder.SetValue(BaseModifyRecordEntity.FieldColumnDescription, BaseModuleManager.GetNameByCache(systemCode, permissionId));
            sqlBuilder.SetValue(BaseModifyRecordEntity.FieldOldValue, null);
            sqlBuilder.SetValue(BaseModifyRecordEntity.FieldNewValue, permissionId);
            sqlBuilder.SetValue(BaseModifyRecordEntity.FieldCreateUserId, this.UserInfo.Id);
            sqlBuilder.SetValue(BaseModifyRecordEntity.FieldCreateBy, this.UserInfo.RealName);
            sqlBuilder.SetDBNow(BaseModifyRecordEntity.FieldCreateOn);
            sqlBuilder.SetValue(BaseModifyRecordEntity.FieldIPAddress, this.UserInfo.IPAddress);
            sqlBuilder.EndInsert();

            return result;
        }
        #endregion

        #region public string GrantByPermissionCode(string systemCode, string userId, string permissionCode) 用户授予权限
        /// <summary>
        /// 用户授予权限
        /// </summary>
        /// <param name="systemCode">系统编号</param>
        /// <param name="userId">用户主键</param>
        /// <param name="permissionCode">权限编号</param>
        public string GrantByPermissionCode(string systemCode, string userId, string permissionCode)
        {
            string result = string.Empty;

            string permissionId = BaseModuleManager.GetIdByCodeByCache(systemCode, permissionCode);
            if (!String.IsNullOrEmpty(permissionId))
            {
                result = this.Grant(systemCode, userId, permissionId);
            }

            return result;
        }
        #endregion

        public int Grant(string systemCode, string userId, string[] permissionIds)
        {
            int result = 0;

            for (int i = 0; i < permissionIds.Length; i++)
            {
                this.Grant(systemCode, userId, permissionIds[i]);
                result++;
            }

            return result;
        }

        public int Grant(string systemCode, string[] userIds, string permissionId)
        {
            int result = 0;

            for (int i = 0; i < userIds.Length; i++)
            {
                this.Grant(systemCode, userIds[i], permissionId);
                result++;
            }

            return result;
        }

        public int Grant(string systemCode, string[] userIds, string[] permissionIds)
        {
            int result = 0;

            for (int i = 0; i < userIds.Length; i++)
            {
                for (int j = 0; j < permissionIds.Length; j++)
                {
                    this.Grant(systemCode, userIds[i], permissionIds[j]);
                    result++;
                }
            }

            return result;
        }


        //
        //  撤销权限的实现部分
        //

        #region public int Revoke(string systemCode, string userId, string permissionId) 为了提高撤销的运行速度
        /// <summary>
        /// 为了提高撤销的运行速度
        /// </summary>
        /// <param name="systemCode">系统编号</param>
        /// <param name="userId">用户主键</param>
        /// <param name="permissionId">权限主键</param>
        /// <returns>影响行数</returns>
        public int Revoke(string systemCode, string userId, string permissionId)
        {
            int result = 0;

            if (String.IsNullOrEmpty(userId) && String.IsNullOrEmpty(permissionId))
            {
                return result;
            }

            this.CurrentTableName = systemCode + "Permission";

            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>(BasePermissionEntity.FieldResourceCategory, BaseUserEntity.TableName));
            parameters.Add(new KeyValuePair<string, object>(BasePermissionEntity.FieldResourceId, userId));
            parameters.Add(new KeyValuePair<string, object>(BasePermissionEntity.FieldPermissionId, permissionId));
            // 伪删除、数据有冗余，但是有历史记录
            // result = permissionManager.SetDeleted(parameters);
            // 真删除、执行效率高、但是没有历史记录
            result = this.Delete(parameters);

            // 2015-09-21 吉日嘎拉 这里增加变更日志
            string tableName = systemCode + ".Permission.User";
            SQLBuilder sqlBuilder = new SQLBuilder(this.DbHelper);
            sqlBuilder.BeginInsert(BaseModifyRecordEntity.TableName);
            sqlBuilder.SetValue(BaseModifyRecordEntity.FieldTableCode, tableName);
            if (DbHelper.CurrentDbType == CurrentDbType.Oracle)
            {
                sqlBuilder.SetFormula(BaseModifyRecordEntity.FieldId, "SEQ_" + BaseModifyRecordEntity.TableName + ".NEXTVAL");
            }
            sqlBuilder.SetValue(BaseModifyRecordEntity.FieldRecordKey, userId);
            sqlBuilder.SetValue(BaseModifyRecordEntity.FieldColumnCode, "撤销授权");
            sqlBuilder.SetValue(BaseModifyRecordEntity.FieldColumnDescription, BaseModuleManager.GetNameByCache(systemCode, permissionId));
            sqlBuilder.SetValue(BaseModifyRecordEntity.FieldOldValue, "1");
            sqlBuilder.SetValue(BaseModifyRecordEntity.FieldNewValue, permissionId);
            sqlBuilder.SetValue(BaseModifyRecordEntity.FieldCreateUserId, this.UserInfo.Id);
            sqlBuilder.SetValue(BaseModifyRecordEntity.FieldCreateBy, this.UserInfo.RealName);
            sqlBuilder.SetDBNow(BaseModifyRecordEntity.FieldCreateOn);
            sqlBuilder.SetValue(BaseModifyRecordEntity.FieldIPAddress, this.UserInfo.IPAddress);
            sqlBuilder.EndInsert();

            return result;
        }
        #endregion

        #region public int RevokeByPermissionCode(string systemCode, string userId, string permissionCode) 用户授予权限
        /// <summary>
        /// 用户授予权限
        /// </summary>
        /// <param name="systemCode">系统编号</param>
        /// <param name="userId">用户主键</param>
        /// <param name="permissionCode">权限编号</param>
        /// <returns>影响行数</returns>
        public int RevokeByPermissionCode(string systemCode, string userId, string permissionCode)
        {
            int result = 0;

            string permissionId = BaseModuleManager.GetIdByCodeByCache(systemCode, permissionCode);
            if (!String.IsNullOrEmpty(permissionId))
            {
                result = this.Revoke(systemCode, userId, permissionId);
            }

            return result;
        }
        #endregion

        public int Revoke(string systemCode, string userId, string[] permissionIds)
        {
            int result = 0;

            for (int i = 0; i < permissionIds.Length; i++)
            {
                result += this.Revoke(systemCode, userId, permissionIds[i]);
            }

            return result;
        }

        public int Revoke(string systemCode, string[] userIds, string permissionId)
        {
            int result = 0;

            for (int i = 0; i < userIds.Length; i++)
            {
                result += this.Revoke(systemCode, userIds[i], permissionId);
            }

            return result;
        }

        public int Revoke(string systemCode, string[] userIds, string[] permissionIds)
        {
            int result = 0;

            for (int i = 0; i < userIds.Length; i++)
            {
                for (int j = 0; j < permissionIds.Length; j++)
                {
                    result += this.Revoke(systemCode, userIds[i], permissionIds[j]);
                }
            }

            return result;
        }

        public int RevokeAll(string systemCode, string userId)
        {
            int result = 0;

            this.CurrentTableName = systemCode + "Permission";
            List<KeyValuePair<string, object>> parameters = new List<KeyValuePair<string, object>>();
            parameters.Add(new KeyValuePair<string, object>(BasePermissionEntity.FieldResourceCategory, BaseUserEntity.TableName));
            parameters.Add(new KeyValuePair<string, object>(BasePermissionEntity.FieldResourceId, userId));
            result = this.Delete(parameters);

            return result;
        }
    }
}